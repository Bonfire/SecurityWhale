import time
from os import walk

# error exceptions and API/repo access
import mysql.connector
import numpy as np
from git import Repo
from github import Github
from mysql.connector import errorcode

# gain access to important data easily
from config import database
from config import git_access
from config import host
from config import password
from config import user

# shh just accept it
repo_id = 0


def access_github():
    """
    Accesses Githubs API and will allow for the gathering of repository data
    :return: access to github api using access token
    """
    return Github(git_access)


def database_connector(repo_data, repo_file_data):
    """
    Connects to the SQL database and executes commands to insert queries and data points into database
    :param repo_data: tuple containing data points from repo
    :param repo_file_data: tuple containing data points form repo file
    :return:
    """
    global repo_id

    try:
        conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
        cursor = conn.cursor()

        # this is for repo data database setup
        repo_sql_insert_query = """INSERT INTO repo (repo_name, repo_initial_creation, assignees,
        size, commits, events, forks, branches, contributors, labels, language_count,
        language_size, milestones, issues, refs, stargazers, subscribers, watchers, network, open_issues,
        pulls, num_files) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

        repo_insert_tuple = repo_data

        cursor.execute(repo_sql_insert_query, repo_insert_tuple)
        repo_id = cursor.lastrowid
        conn.commit()
    except mysql.connector.Error as err:
        if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
            print("Something is wrong with your user name or password")
        elif err.errno == errorcode.ER_BAD_DB_ERROR:
            print("Database does not exist")
        else:
            print(err)
    else:
        conn.close()

    try:
        conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
        mouse = conn.cursor()

        # This is for repo file data database setup
        repo_file_sql_insert_query = """INSERT INTO file (repoID, filename, has_fault,commit_size,
        commit_count, insertions, deletions,lines_changed, hexsha_count) VALUES (%s, %s, %s, %s, %s, %s, %s,
        %s, %s)"""

        repo_file_insert_tuple = repo_file_data

        mouse.execute(repo_file_sql_insert_query, repo_file_insert_tuple)
        conn.commit()

    except mysql.connector.Error as err:
        if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
            print("Something is wrong with your user name or password")
        elif err.errno == errorcode.ER_BAD_DB_ERROR:
            print("Database does not exist")
        else:
            print(err)
    else:
        conn.close()


def repository_data(git_repo, repository_name, repository_dir):
    """
    Accesses githubs api to pull info such as number of subscribers, issues, commits and size of repo
    :param git_repo: repository object containing the entire repo
    :param repository_name: name of repo currently being scanned
    :param repository_dir: location of cloned repo
    :return:
    """
    lang_string = []
    lang_size = []
    language_size = 0
    files = []

    assignees = git_repo.get_assignees().totalCount
    branches = git_repo.get_branches().totalCount
    contributors = git_repo.get_contributors().totalCount
    count_open_issues = git_repo.open_issues_count
    commits = git_repo.get_commits().totalCount
    events = git_repo.get_events().totalCount
    forks = git_repo.get_forks().totalCount
    issues = git_repo.get_issues().totalCount
    labels = git_repo.get_labels().totalCount
    language_count = len(git_repo.get_languages())
    for lang, count in git_repo.get_languages().items():
        lang_string.append(lang)
        lang_size.append(count)
    for lang in lang_size:
        language_size += lang
    milestone = git_repo.get_milestones().totalCount
    network_count = git_repo.network_count
    pulls = git_repo.get_pulls().totalCount
    refs = git_repo.get_git_refs().totalCount
    stargazer = git_repo.get_stargazers().totalCount
    subscribers = git_repo.get_subscribers().totalCount
    watchers = git_repo.watchers_count
    size = git_repo.size
    repo_creation_date = str(git_repo.created_at)

    for (dirpath, dirnames, filenames) in walk(repository_dir):
        for filename in [f for f in filenames]:
            files.extend(filenames)
            break

    number_of_files_in_project = len(files)

    repo_data = (repository_name, repo_creation_date, assignees, size, commits, events, forks, branches, contributors,
                 labels, language_count, language_size, milestone, issues, refs, stargazer, subscribers, watchers,
                 network_count, count_open_issues, pulls, number_of_files_in_project)
    return repo_data


def repository_file_data(git_repo, repository_name):
    """
    Accesses githubs api to pull info such as commit info, hexsha, also scans locally cloned version
    of repo for more data such as line count
    :param git_repo: repo object to access api data
    :param repository_name: name of repo on github
    :return:
    """
    global repo_id

    commits_hexsha = []

    commit_size_sum = 0
    commit_files_count = 0
    commit_insertion_count = 0
    commit_deletion_count = 0
    commit_lines_changed_count = 0

    commits = list(git_repo.iter_commits('master'))[:2]
    for commit in commits:
        commits_hexsha.append(commit.hexsha)
        commit_size_sum += commit.size

        # get data points from stats, since it's stored in a dictionary we had to use a nested for loop to gather it
        commit_stats = commit.stats.files
        print(commit.stats.total)
        for file_path, value in commit_stats.items():
            commit_files_count += 1
            for type_of_change, change_value in value.items():
                if type_of_change == "insertions":
                    commit_insertion_count += change_value
                if type_of_change == "deletions":
                    commit_deletion_count += change_value
                if type_of_change == "lines":
                    commit_lines_changed_count += change_value

    # since we cant add lists to the database i calculated the amount of each list and storing that value instead
    hexsha_count = len(commits_hexsha)
    fault_flag = flag_fault(git_repo)

    repo_file_data = (repo_id, repository_name, fault_flag,
                      commit_size_sum, commit_files_count, commit_insertion_count,
                      commit_deletion_count, commit_lines_changed_count, hexsha_count)

    return repo_file_data


def flag_fault(repository):
    """
    Check if a repository mentions CVE in its commit history
    :param repository: repo object used to access data from api
    :return: if repository mentions CVE
    """
    # gather a list of commits from the master branch of a repo
    commits = list(repository.iter_commits('master'))[:]
    # scan each commit string for the mention of cve with find() returning -1 if not found
    for commit in commits:
        if commit.message.find('CVE') != -1 or commit.summary.find('CVE') != -1:
            return 1
        else:
            return 0


# TODO: Create automation for cloning locally

def git_clone(repo_names):
    """
    Automate the cloning of repos from github and storing them into a fold
    :param repo_names: file listing repo names to clone
    :return:
    """
    # clones from the url of the repo we want and loads it to folder
    with open(repo_names) as file:
        git_repo_name = file.readlines()
        print(git_repo_name)
        Repo.clone_from("https://www.github.com/" + str(git_repo_name),
                        "../source/cloned_repos")
    print('done cloning')


def features_array(git_repo, repository, repository_name, repository_dir):
    """

    :param git_repo: gitpython repo object to get data from api
    :param repository: pygithub object used to get data from api
    :param repository_name: name of repo on github
    :param repository_dir: location of cloned repo
    :return:
    """
    features_repo = list(repository_data(git_repo, repository_name))
    features_file = list(repository_file_data(repository, repository_dir, repository_name))
    array = np.array(features_repo + features_file)
    return array


# TODO: parse git log data and get it in the database

def log_data(repository):
    try:
#         TODO: split this dictionary of dictionaries into a list of lists containing the filename index and the average
#           insertions and deletions and line changes from each file
        commit_size_sum = 0
        commit_files_count = 0
        commit_insertion_count = 0
        commit_deletion_count = 0
        commit_lines_changed_count = 0

        commits = list(git_repo.iter_commits('master'))[:]
        for commit in commits:
            # get data points from stats, since it's stored in a dictionary we had to use a nested for loop to gather it
            commit_stats = commit.stats.files
            # print(commit.stats.total)
            print(commit.stats.files)
            for file_path, value in commit_stats.items():
                commit_files_count += 1
                for type_of_change, change_value in value.items():
                    if type_of_change == "insertions":
                        commit_insertion_count += change_value
                    if type_of_change == "deletions":
                        commit_deletion_count += change_value
                    if type_of_change == "lines":
                        commit_lines_changed_count += change_value

    except Exception:
        pass


if __name__ == "__main__":
    try:
        start_time = time.time()
        git = access_github()

        with open('../docs/repo_path_list.txt', 'r') as repo_info:
            for row in repo_info.readlines():
                repo_name, repo_dir = row.split(' ')
                github_repo = git.get_repo(repo_name, lazy=False)
                repo = Repo(repo_dir.rstrip())
                # Remove pass and comment out whatever function you want to check out
                # database_connector does most of everything to do the numpy array uncomment it out as well
                # log_data: testing in progress
                if not repo.bare:
                    pass
                    # database_connector(repository_data(github_repo, repo_name),
                    #                    repository_file_data(repo, repo_dir.rstrip(), repo_name))
                    # features_array(github_repo, repo, repo_name, repo_dir.rstrip())
                    # repository_file_data(repo, repo_dir.rstrip(), repo_name)
                    # log_data(repo)
                else:
                    print('Could not load repository at {} :('.format(repo_dir))

        print('Script Complete|Runtime: {} Seconds'.format(time.time() - start_time))
    except Exception as e:
        print("Could not check if repo existed", e)
