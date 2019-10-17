import time
from os import walk

# error exceptions and API/repo access
import mysql.connector
from git import Repo
from github import Github
from mysql.connector import errorcode
import numpy as np

# grab cloning and delete functions
from cloner import clone_repo

# gain access to important data easily
from config import database
from config import git_access
from config import host
from config import password
from config import user

# globals
# increment through database
repo_id = 1
# contains the totals from each commit (used in parse_dic)
totals = {}
# contains the average from each commit (used in dirty/clean_data)
averages = []
# contains filenames (used in clean_data)
dirty_filenames = []
# contains filenames (used in clean_data)
clean_filenames = []


def access_github():
    """
    Access Githubs API and will allow for the gathering of repositories data
    :return: access to the github api using access token
    """

    return Github(git_access)


def features_array(git_repo, repository, repository_name, repository_dir):
    """
    combines data points and creates a numpy array
    :param git_repo: gitpython repo object to get data from api
    :param repository: pygithub object used to get data from api
    :param repository_name: name of repo on github
    :param repository_dir: location of cloned repo
    :return:
    """
    repo_data = list(repository_data(git_repo, repository, repository_name, repository_dir))

    features = []

    # Populates features with sub-arrays containing [filename, file_data[], repo_data[]]
    for key in averages:
        features.append([key, averages[key], repo_data])

    # Converts to a numpy array before returning
    return np.array(features)


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


def dirty_data(repository, commit_hash):
    """
    Dirty Data takes a given commit_hash and parses information from it which is then stored in the database
    it also stores the filename in a list that is stored globally

    :param repository: full repository object
    :param commit_hash: a hash of a specific commit in a repository
    :return:
    """
    global repo_id
    index = 0
    # these commits are known faults so flags will always be 1
    flag = 1

    # get the data on a specific hash from the given repo
    commit = repository.commit(commit_hash)

    parse_dic(commit.stats.files)

    # use totals to find averages
    commit_size = commit.size
    for key in totals:
        key_val = totals[key]
        tot_ins = key_val['ins_total']
        tot_del = key_val['del_total']
        tot_lin = key_val['lin_total']
        count = key_val['count']

        averages.append([key, tot_ins, tot_ins / count, tot_del, tot_del / count, tot_lin, tot_lin / count,
                         commit_size])

        index += 1

    for key in averages:
        dirty_filenames.append(key[0])
        filename = key[0]
        total_ins = key[1]
        ins_avg = key[2]
        total_del = key[3]
        del_avg = key[4]
        total_lines = key[5]
        lines_avg = key[6]
        commit_size = key[7]

        try:
            conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
            cursor = conn.cursor()

            # Updates multiple columns of a single row in table
            repo_file_sql_update_query = """INSERT INTO file (repoID, filename, has_fault,total_inserts,
        insert_averages, total_deletions, deletion_averages, total_lines, line_averages, commit_size) VALUES (%s,
         %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

            log_inserts = (
                repo_id, filename, flag, total_ins, ins_avg, total_del, del_avg, total_lines, lines_avg,
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

    # clean up lists for next commit/ repo
    totals.clear()
    averages.clear()


def clean_data(repository, commit_hash):
    """
    Clean data looks through a repository history up until the given commit hash to find another file
    with the same extension. It strips the filename for the extension then checks to see it another filename
    matches that extension in the commit history. once found parse that commit to database as clean data
    :param repository:
    :param commit_hash:
    :return:
    """
    # data = repository.iter_commits(commit_hash) This gets all commits up to given hash
    file_holder = []
    # we know these files dont have any faults
    flag = 0
    index = 0

    commit_log = repository.iter_commits(commit_hash)
    commit_logx = repository.iter_commits(commit_hash)
    commit_data = repository.commit(commit_hash)

    # grab the extension of the dirty file
    for path in commit_data.stats.files:
        dirty_ext = path.split(".")[-1]

    # go through commit history up to dirty file commit and store all files with the same extension as the
    # dirty_ext into a list
    for commit in commit_log:
        for path in commit.stats.files:
            if path.split(".")[-1] == dirty_ext:
                file_holder.append(path)

    print(file_holder[-1])

    # grab the last file in the list and run through the commit log again, once the filenames match parse that
    # commit and store in database

    for commit in commit_logx:
        for path in commit.stats.files:
            if path == file_holder[-1]:
                parse_dic(commit.stats.files)

                # use totals to find averages
                commit_size = commit.size
                for key in totals:
                    key_val = totals[key]
                    tot_ins = key_val['ins_total']
                    tot_del = key_val['del_total']
                    tot_lin = key_val['lin_total']
                    count = key_val['count']

                    averages.append([key, tot_ins, tot_ins / count, tot_del, tot_del / count, tot_lin, tot_lin / count,
                                     commit_size])

                    index += 1

                for key in averages:
                    clean_filenames.append(key[0])
                    filename = key[0]
                    total_ins = key[1]
                    ins_avg = key[2]
                    total_del = key[3]
                    del_avg = key[4]
                    total_lines = key[5]
                    lines_avg = key[6]
                    commit_size = key[7]

                    try:
                        conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
                        cursor = conn.cursor()

                        # Updates multiple columns of a single row in table
                        repo_file_sql_update_query = """INSERT INTO file (repoID, filename, has_fault,total_inserts,
                        insert_averages, total_deletions, deletion_averages, total_lines, line_averages, commit_size) VALUES (%s,
                         %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

                        log_inserts = (
                            repo_id, filename, flag, total_ins, ins_avg, total_del, del_avg, total_lines, lines_avg,
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

                # clean up lists for next commit/ repo
                totals.clear()
                averages.clear()


if __name__ == "__main__":
    try:
        start_time = time.time()
        git = access_github()

        # temp = get_repos()
        temp = [['bbengfort/confire', '8cc86a5ec2327e070f1d576d61bbaadf861597ea']]

        for name in temp:
            github_repo = git.get_repo(name[0], lazy=False)
            repo_dir = clone_repo(name[0])
            repo = Repo(str(repo_dir))

            # PRINT DEBUGGING
            print('Finished gathering  info and cloning: ' + name[0])

            # here we check if we can access the actual repository object
            if not repo.bare:
                repo_database(repository_data(github_repo, repo, name[0], repo_dir))

                # remove repo name from list so we only have the commit hashes to look at
                name.remove(name[0])

                # PRINT DEBUGGING
                print('Finished storing repo data')

                for hash_commits in name:
                    # PRINT DEBUGGING
                    print('Entering dirty repo')

                    dirty_data(repo, hash_commits)

                    # PRINT DEBUGGING
                    print('Finished dirty data')

                    clean_data(repo, hash_commits)

                    # PRINT DEBUGGING
                    print('Finished clean data')

            else:
                print('Could not load repository at {} :'.format(repo_dir))

            # PRINT DEBUG
            # print('DELETING FOLDER')
            # obliterate(repo_dir)

        print('Script Complete|Runtime: {} Seconds'.format(time.time() - start_time))
    except Exception as e:
        print("Could not check if repo existed", e)
