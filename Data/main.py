from token_access import git_access
from config import database
from config import host
from config import password
from config import user
from github import Github
from git import Repo
import mysql.connector
from mysql.connector import errorcode
from os import walk

# My personal github API token, must have to access data
git = Github(git_access)

# Repo ID used for file database insertion
file_to_repo_ID = 0


def repo_data(github_repo, repo_name):
    """Function Gathers data on a repository such as subscribers,
    and issues and stores the values which are then sent over to the database

    :param github_repo: variable that is connected to Github API
    :param repo_name: name of repo as displayed on github
    :return:
    """

    # lists the contain data from the language dictionary,
    # the string language and the size of it in project
    lang_string = []
    lang_size = []
    language_size = 0

    # auto updates global variable to be used in file method
    global file_to_repo_ID

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
    # has_issue = github_repo.has_issues
    # has_download = github_repo.has_downloads
    # has_wiki = github_repo.has_wiki
    # has_project = github_repo.has_projects

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
        print('Records were inserted successfully into repo table')

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
    """Function Gathers data on a file such as line count,
    hex, commit info and stores the values which are then sent over to the
    database

    :param repo: a connection to the repo we request through git
    :param repo_dir: the directory path name where project is located locally
    :return:
    """

    # gets names of all files in project and stores in list
    files = []
    for (dirpath, dirnames, filenames) in walk(repo_dir):
        for filename in [f for f in filenames]:
            files.extend(filenames)
            break
    # number of files in project
    number_of_files_in_project = len(files)

    # gets list of commit datetime
    committed_datetimes = []
    # total sum of commit size
    commit_size_sum = 0
    # total files committed
    commit_files_count = 0
    # commit stats
    commit_insertion_count = 0
    commit_deletion_count = 0
    commit_lines_changed_count = 0
    # list of all the hexsha
    commits_hexsha = []

    # with all the commits, go through and pull data
    commits = list(repo.iter_commits('master'))[:20]
    for commit in commits:
        commits_hexsha.append(commit.hexsha)
        committed_datetimes.append(commit.committed_datetime)
        commit_size_sum += commit.size

        # iterations through commit stats and parses dictionaries into
        # variables, gives me file path insertion, deletion, lines changes
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

    datetime_count = len(committed_datetimes)
    hexsha_count = len(commits_hexsha)

    try:
        conn = mysql.connector.connect(user=user, host=host,
                                       password=password,
                                       database=database)
        cursor = conn.cursor()

        # insert into database, won't work now because database not properly setup for these data points
        sql_insert_query = """INSERT INTO file (repoID, filename, num_files,commit_size, commit_count,
        insertions, deletions,lines_changed, hexsha_count) VALUES (%s, %s,
        %s, %s, %s, %s, %s, %s, %s)"""

        insert_tuple = (file_to_repo_ID, repo_name,
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


# C:\Users\bbkyl\Dropbox\github\test\openssl


def main():
    try:
        # read in repo name and directory path from file and collect data
        # this is done until end of file
        with open('repo_path_list.txt', 'r') as repo_info:
            for row in repo_info.readlines():
                # name of repository on Github
                repo_name, repo_dir = row.split(' ')
                github_repo = git.get_repo(repo_name, lazy=False)

                # file path name
                repo = Repo(repo_dir.rstrip())

                # if repo exists run data
                if not repo.bare:
                    repo_data(github_repo, repo_name)
                    file_data(repo, repo_dir.rstrip(), repo_name)
                # if repo doesn't exists display error and exit
                else:
                    print('Could not load repository at {} :('.format(repo_dir))
    except Exception as e:
        print("Could not check if repo existed", e)

    # try:
    #     # get the name of the online repo, and the file path on the local computer
    #     repo_name = input('Name of Github Repo: ')
    #     github_repo = git.get_repo(repo_name, lazy=False)
    #     repo_dir = input('Repo Directory: ')
    #
    #     repo = Repo(repo_dir)
    #
    #     # if repo exists
    #     if not repo.bare:
    #         repo_data(github_repo, repo_name)
    #         file_data(repo, repo_dir, repo_name)
    #     else:
    #         print('Could not load repository at {} :('.format(repo_dir))
    # except Exception as e:
    #     print("Could not check if repo existed", e)


if __name__ == "__main__":
    main()
