from os import walk

import mysql.connector
from git import Repo
from github import Github
from mysql.connector import errorcode

from config import database
from config import host
from config import password
from config import user
from token_access import git_access

# My personal github API token, must have to access data
git = Github(git_access)

# Repo ID used for file database insertion
file_to_repo_ID = 0


def repo_data(github_repo, repo_name):
    """
    Function Gathers data on a repository such as subscribers,
    and issues and stores the values which are then sent over to the database

    :param github_repo: variable that is connected to Github API
    :param repo_name: name of repo as displayed on github
    :return:
    """

    # contains data on a projects languages
    lang_string = []
    lang_size = []
    language_size = 0

    # auto updates global variable to be used in file method
    global file_to_repo_ID

    # all repo data points we can acquire
    assignees = github_repo.get_assignees().totalCount
    branches = github_repo.get_branches().totalCount
    contributors = github_repo.get_contributors().totalCount
    count_open_issues = github_repo.open_issues_count
    commits = github_repo.get_commits().totalCount
    events = github_repo.get_events().totalCount
    forks = github_repo.get_forks().totalCount
    issues = github_repo.get_issues().totalCount
    labels = github_repo.get_labels().totalCount
    language_count = len(github_repo.get_languages())
    for lang, count in github_repo.get_languages().items():
        lang_string.append(lang)
        lang_size.append(count)
    for lang in lang_size:
        language_size += lang
    milestone = github_repo.get_milestones().totalCount
    network_count = github_repo.network_count
    pulls = github_repo.get_pulls().totalCount
    refs = github_repo.get_git_refs().totalCount
    stargazer = github_repo.get_stargazers().totalCount
    subscribers = github_repo.get_subscribers().totalCount
    watchers = github_repo.watchers_count
    size = github_repo.size
    repo_creation_date = str(github_repo.created_at)

    # boolean data points currently not in use
    # has_issue = github_repo.has_issues
    # has_download = github_repo.has_downloads
    # has_wiki = github_repo.has_wiki
    # has_project = github_repo.has_projects

    # access database and insert all the data into repo schema
    try:
        conn = mysql.connector.connect(user=user, host=host,
                                       password=password,
                                       database=database)
        cursor = conn.cursor()

        sql_insert_query = """INSERT INTO repo (repo_name, repo_initial_creation, assignees,
        size, commits, events, forks, branches, contributors, labels, language_count,
        language_size, milestones, issues, refs, stargazers, subscribers, watchers, network, open_issues,
        pulls) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

        insert_tuple = (repo_name, repo_creation_date, assignees, size, commits,
                        events, forks, branches, contributors, labels, language_count,
                        language_size, milestone, issues, refs, stargazer, subscribers, watchers,
                        network_count, count_open_issues, pulls)

        cursor.execute(sql_insert_query, insert_tuple)
        file_to_repo_ID = cursor.lastrowid

        conn.commit()

        print('REPO DATA COLLECTION COMPLETED')

    except mysql.connector.Error as err:
        if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
            print("Something is wrong with your user name or password")
        elif err.errno == errorcode.ER_BAD_DB_ERROR:
            print("Database does not exist")
        else:
            print(err)
    else:
        print('CLOSING DATABASE')
        conn.close()


def file_data(repo, repo_dir, repo_name):
    """
    Function Gathers data on a file such as line count,
    hex, commit info and stores the values which are then sent over to the
    database

    :param repo: a connection to the repo we request through git
    :param repo_dir: the directory path name where project is located locally
    :param repo_name: Name of repository
    :return:
    """

    # acquire number of files in a project
    files = []
    for (dirpath, dirnames, filenames) in walk(repo_dir):
        for filename in [f for f in filenames]:
            files.extend(filenames)
            break
    # number of files in project
    number_of_files_in_project = len(files)

    # list data points
    commits_hexsha = []
    committed_datetimes = []

    # summed integer data points from total project commits
    commit_size_sum = 0
    commit_files_count = 0
    commit_insertion_count = 0
    commit_deletion_count = 0
    commit_lines_changed_count = 0

    # iterate through the commit history and gather data
    commits = list(repo.iter_commits('master'))[:20]
    for commit in commits:
        commits_hexsha.append(commit.hexsha)
        committed_datetimes.append(commit.committed_datetime)
        commit_size_sum += commit.size

        # get data points from stats, since it's stored in a dictionary we had to use a nested for loop to gather it
        commit_stats = commit.stats.files
        for file_path, value in commit_stats.items():
            commit_files_count += 1
            for type_of_change, change_value in value.items():
                if type_of_change == "insertions":
                    commit_insertion_count += change_value
                if type_of_change == "deletions":
                    commit_deletion_count += change_value
                if type_of_change == "lines":
                    commit_lines_changed_count += change_value

    # can't used lists in database insertion so got total count instead
    datetime_count = len(committed_datetimes)
    hexsha_count = len(commits_hexsha)
    fault_flag = check_faults(repo)

    # connect to database and insert file data points into file schema
    try:
        conn = mysql.connector.connect(user=user, host=host,
                                       password=password,
                                       database=database)
        cursor = conn.cursor()

        sql_insert_query = """INSERT INTO file (repoID, filename, has_fault, num_files,commit_size, commit_count,
        insertions, deletions,lines_changed, hexsha_count) VALUES (%s, %s, %s,
        %s, %s, %s, %s, %s, %s, %s)"""

        insert_tuple = (file_to_repo_ID, repo_name, fault_flag,
                        number_of_files_in_project, commit_size_sum, commit_files_count, commit_insertion_count,
                        commit_deletion_count, commit_lines_changed_count, hexsha_count)

        cursor.execute(sql_insert_query, insert_tuple)
        conn.commit()

        print('\nFILE DATA COLLECTION COMPLETE')

    except mysql.connector.Error as err:
        if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
            print("Something is wrong with your user name or password")
        elif err.errno == errorcode.ER_BAD_DB_ERROR:
            print("Database does not exist")
        else:
            print(err)
    else:
        print('CLOSING CONNECTION')
        conn.close()


def check_faults(repo):
    """
    Function checks if a repository mentions CVE in its commits history and then returns 1 if true else 0 if false
    :param repo: repository object
    :return 1: if cve is mentioned in commit message or summary
    """
    # iterate through the commit history and gather data
    commits = list(repo.iter_commits('master'))[:]
    for commit in commits:
        if commit.message.find('CVE') != -1 or commit.summary.find('CVE') != -1:
            return 1


def main():
    """
    Main Function gathers information on the projects we are gathering data from through a read in file.
    Each project is then scanned repo data and file data and the values are inserted into a database
    before the cycle repeats.
    :return:
    """

    # read form repo_path_list and collect/insert data points until reach EOF
    try:
        with open('repo_path_list.txt', 'r') as repo_info:
            for row in repo_info.readlines():
                repo_name, repo_dir = row.split(' ')
                github_repo = git.get_repo(repo_name, lazy=False)

                # used in GitPython library
                repo = Repo(repo_dir.rstrip())

                # if repo exists run methods
                if not repo.bare:
                    repo_data(github_repo, repo_name)
                    file_data(repo, repo_dir.rstrip(), repo_name)
                # if repo doesn't exists display error and exit
                else:
                    print('Could not load repository at {} :('.format(repo_dir))
    except Exception as e:
        print("Could not check if repo existed", e)


if __name__ == "__main__":
    main()
