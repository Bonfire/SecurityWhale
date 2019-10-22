from git import Repo
from github import Github
from config import *
import time
from os import walk

# error exceptions and API/repo access
import mysql.connector
import numpy as np

from mysql.connector import errorcode

# grab cloning and delete functions
from cloner import clone_repo

repo_id = 1

def access_github():
    """
    Access Githubs API and will allow for the gathering of repositories data
    :return: access to the github api using access token
    """

    return Github(git_access)


def repo_get(name, git, repo_dir, db_sig=True):
    github_repo = git.get_repo(name, lazy=False)
    repo = Repo(str(repo_dir))

    if not repo.bare:
        r_data = repository_data(github_repo, repo, name, repo_dir)
        if db_sig:
            repo_database(r_data)

    return repo, r_data


def repo_database(repository_data_points):
    """
    Connects to the SQL database and executes commands to insert to insert queries and data points into database
    :param repository_data_points: tuple containing data points from repository data function
    :return:
    """

    global repo_id

    try:
        conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
        cursor = conn.cursor()

        # this is for repo data database setup
        repo_sql_insert_query = """INSERT INTO repo (repo_name, assignees,
        size, commits, events, forks, branches, contributors, labels, language_count,
        language_size, milestones, issues, refs, stargazers, subscribers, watchers, network, open_issues,
        pulls, num_files, commit_size, commit_count, insertions, deletions, lines_changed) VALUES (%s, %s, %s, %s, %s,
        %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

        repo_insert_tuple = repository_data_points

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


def repository_data(git_repo, repository, repository_name, repository_dir):
    """
    Accesses githubs api to pull info such as number of subscribers, issues, commits and size of repo
    :param git_repo: repository object containing the entire repo
    :param repository: repository object
    :param repository_name: name of repo currently being scanned
    :param repository_dir: location of cloned repo
    :return:
    """
    lang_string = []
    lang_size = []
    language_size = 0
    files = []
    commit_size_sum = 0
    commit_files_count = 0
    commit_insertion_count = 0
    commit_deletion_count = 0
    commit_lines_changed_count = 0

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

    for (dirpath, dirnames, filenames) in walk(repository_dir):
        for filename in [f for f in filenames]:
            files.extend(filenames)
            break
    number_of_files_in_project = len(files)

    commit_list = list(repository.iter_commits('master'))[:100]
    for commit in commit_list:
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

    # clear lists of data for next repo
    lang_size.clear()
    lang_string.clear()
    files.clear()

    repo_data = (repository_name, assignees, size, commits, events, forks, branches, contributors,
                 labels, language_count, language_size, milestone, issues, refs, stargazer, subscribers, watchers,
                 network_count, count_open_issues, pulls, number_of_files_in_project, commit_size_sum,
                 commit_files_count, commit_insertion_count, commit_deletion_count, commit_lines_changed_count)

    return repo_data


def parse_dic(dic):
    """
    Parses the data into a dictionary of total

    :param dic: Dictionary of parsed github commit log history
    :return:
    """
    # contains the totals from each commit (used in parse_dic)
    totals = []
    # This gives us the filepath itself
    fp = list(dic.keys())[0]

    for phile in fp:

        # Insertions and deletions for this particular filepath
        fp_ins = dic[fp]['insertions']
        fp_del = dic[fp]['deletions']
        fp_lin = dic[fp]['lines']

        totals.append([phile, fp_ins, fp_del, fp_lin])

    return totals


def get_averages(omni_list, commit_hash, repo):

    commits = repo.iter_commits(commit_hash)
    omni_totals = []

    for commit in commits:
        tots = parse_dic(commit.stats.files)
        for path in tots:
            check = True
            for phile in omni_totals:
                if phile[0] == path:
                    adder = 1
                    for index, vals in enumerate(path[1:]):
                        phile[index+adder] += path[index+1]
                        phile[index+adder+1] += 1
                        adder += 1
                    check = False
                    break
            if check:
                file_info = []
                file_info.append(path[0])
                for vals in path[1:]:
                    file_info.append(vals)
                    if vals > 0:
                        file_info.append(1)
                    else:
                        file_info.append(0)

                omni_totals.append(file_info)
