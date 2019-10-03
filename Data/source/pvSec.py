# Author: Kyle Reid
# Contributor: Tom
# Last updated: 9.21.19

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
repo_id = 1

# Contains the totals as described above
totals = {}

# Contains the averages as described above
avgs = []


def access_github():
    """
    Accesses Githubs API and will allow for the gathering of repository data
    :return: access to github api using access token
    """
    return Github(git_access)


def repo_database_connector(repo_data):
    """
    Connects to the SQL database and executes commands to insert queries and data points into database
    :param repo_data: tuple containing data points from repo
    :return:
    """
    global repo_id
    print("Repo ID in database connector: " + str(repo_id))

    try:
        conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
        cursor = conn.cursor()

        # this is for repo data database setup
        repo_sql_insert_query = """INSERT INTO repo (repo_name, assignees,
        size, commits, events, forks, branches, contributors, labels, language_count,
        language_size, milestones, issues, refs, stargazers, subscribers, watchers, network, open_issues,
        pulls, num_files, commit_size, commit_count, insertions, deletions, lines_changed, has_fault) VALUES (%s, %s, %s, %s, %s,
        %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

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


def repository_data(git_repo, repository, repository_name, repository_dir):
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

    commit_list = list(repository.iter_commits('master'))[:500]

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

        fault_flag = flag_fault(repository)

    # clear lists of data for next repo
    lang_size.clear()
    lang_string.clear()
    files.clear()

    repo_data = (repository_name, assignees, size, commits, events, forks, branches, contributors,
                 labels, language_count, language_size, milestone, issues, refs, stargazer, subscribers, watchers,
                 network_count, count_open_issues, pulls, number_of_files_in_project, commit_size_sum,
                 commit_files_count, commit_insertion_count, commit_deletion_count, commit_lines_changed_count,
                 fault_flag)
    return repo_data


# TODO: Find a way to get more fault values
def flag_fault(repository):
    """
    Check if a repository mentions CVE in its commit history
    :param repository: repo object used to access data from api
    :return: if repository mentions CVE
    """
    # gather a list of commits from the master branch of a repo
    commits = list(repository.iter_commits('master'))[:500]
    # scan each commit string for the mention of cve with find() returning -1 if not found
    for commit in commits:
        if commit.message.find('CVE') != -1 or commit.summary.find('CVE') != -1 or commit.message.find(
                'bug') or commit.summary.find('bug') != 1:
            return 1
        else:
            return 0


# TODO: need a way to pass all these value to a numpy array for curtis
def features_array(git_repo, repository, repository_name, repository_dir):
    """
    combines data points and creates a numpy array

    :param git_repo: gitpython repo object to get data from api
    :param repository: pygithub object used to get data from api
    :param repository_name: name of repo on github
    :param repository_dir: location of cloned repo
    :return:
    """
    features_repo = list(repository_data(git_repo, repository_name, repository_dir))
    array = np.array(features_repo)
    return array


def parse_dic(dic):
    """
    Parses the data into a dictionary of total

    :param dic: Dictionary of parsed github commit log history
    :return:
    """
    # This gives us the filepath itself
    fp = list(dic.keys())[0]

    # Insertions and deletions for this particular filepath
    fp_ins = dic[fp]['insertions']
    fp_del = dic[fp]['deletions']
    fp_lin = dic[fp]['lines']

    # If this filepath has not been seen before...
    if totals.get(fp) is None:
        # ...we're creating a new entry with the current data as starting values
        # print(fp + " is not in the dictionary")
        new_ins_total = fp_ins
        new_del_total = fp_del
        new_lin_total = fp_lin
        new_count = 1
    else:
        # ...otherwise, just increment the entries we already have
        new_ins_total = totals[fp]['ins_total'] + fp_ins
        new_del_total = totals[fp]['del_total'] + fp_del
        new_lin_total = totals[fp]['lin_total'] + fp_lin
        new_count = totals[fp]['count'] + 1

    # Update can either update an existing key-value pair or create a new one
    totals.update(
        {fp: {'ins_total': new_ins_total, 'del_total': new_del_total, 'lin_total': new_lin_total, 'count': new_count}})


# TODO: figure a way to send data over to a numpy array
def log_data(git_repo):
    """
    Parses a dictionary of dictionaries, d, with the format
        {str : {'insertions' : int, 'deletions' : int, 'lines' : int}}
    into a new dictionary of dictionaries of totals with the format
        {str : {'ins_total' : int, 'del_total' : int, 'lin_total' : int, 'count' : int}}
    where 'count' is how many times that key has been seen Afterwards, this will be converted into a list of lists
    with the format
        [['file', x, y, z], ...]

    :param git_repo: gitpython repo object to get data from api
    :return:
    """

    global repo_id
    print("Repo ID in data log: " + str(repo_id))
    flags = []
    commit_sizes = []
    commits = list(git_repo.iter_commits('master'))[:500]
    index = 0

    try:

        # Sends each commit dictionary to be parsed
        for commit in commits:
            parse_dic(commit.stats.files)
            commit_sizes.append(commit.size)
            if commit.message.find('CVE') != -1 or commit.summary.find('CVE') != -1 or commit.message.find(
                    'bugs') != -1:
                flags.append(1)
            else:
                flags.append(0)

            # Use totals to find averages
        for key in totals:
            key_val = totals[key]
            tot_ins = key_val['ins_total']
            tot_del = key_val['del_total']
            tot_lin = key_val['lin_total']
            count = key_val['count']

            # Add to averages list
            avgs.append([key, tot_ins, tot_ins / count, tot_del, tot_del / count, tot_lin, tot_lin / count,
                         flags[index], commit_sizes[index]])
            index += 1

    except Exception:
        pass

    # add the new data to file table
    for key in avgs:
        filename = key[0]
        total_ins = key[1]
        ins_avg = key[2]
        total_del = key[3]
        del_avg = key[4]
        total_lines = key[5]
        lines_avg = key[6]
        fault_flag = key[7]
        commit_size = key[8]

        try:
            conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
            cursor = conn.cursor()

            # Updates multiple columns of a single row in table
            repo_file_sql_update_query = """INSERT INTO file (repoID, filename, has_fault,total_inserts,
        insert_averages, total_deletions, deletion_averages, total_lines, line_averages, commit_size) VALUES (%s,
         %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

            log_inserts = (
                repo_id, filename, fault_flag, total_ins, ins_avg, total_del, del_avg, total_lines, lines_avg,
                commit_size)

            cursor.execute(repo_file_sql_update_query, log_inserts)
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

    # clear lists for next repo
    flags.clear()
    commit_sizes.clear()


if __name__ == "__main__":
    try:
        start_time = time.time()
        git = access_github()

        with open('../docs/Github_Testing.txt', 'r') as repo_info:
            for row in repo_info.readlines():
                repo_name, repo_dir = row.split(' ')
                github_repo = git.get_repo(repo_name, lazy=False)
                repo = Repo(repo_dir.rstrip())

                if not repo.bare:
                    repo_database_connector(repository_data(github_repo, repo,
                                                            repo_name, repo_dir.rstrip()))
                    log_data(repo)
                else:
                    print('Could not load repository at {} :'.format(repo_dir))

        print('Script Complete|Runtime: {} Seconds'.format(time.time() - start_time))
    except Exception as e:
        print("Could not check if repo existed", e)

