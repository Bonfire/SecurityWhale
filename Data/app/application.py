from os import walk

import mysql.connector
from git import Repo
from github import Github
from mysql.connector import errorcode

from config import database
from config import git_access
from config import host
from config import password
from config import user

repo_id = 1


def access_github():
    """
    Gain access to Github API and allow for the gathering of repositories data
    :return: access to the github api using access token
    """

    return Github(git_access)


def repo_get(name, git, repo_dir, db_sig=True):
    """
    Get the repository objects and collect data.

    :param name: Name of Repository
    :param git: access token
    :param repo_dir: directory of cloned repository
    :param db_sig: if signal true collect data and store in database
    :return: repository object and repository data
    """
    # grabs repo object for repo table data (collecting)
    github_repo = git.get_repo(name, lazy=False)
    # grabs repo object for repo file table data (parsing and collecting)
    repo = Repo(str(repo_dir))

    # make sure repo is accessible
    if not repo.bare:
        repo_data = repository_data(github_repo, repo, name, repo_dir)
        # if the signal passed by application indicates user wants to add his data to our database we add it
        if db_sig:
            repo_database(repo_data)

    return repo, repo_data


def repository_data(git_repo, repository, repository_name, repository_dir):
    """
    Accesses github api to pull info such as number of subscribers, issues, commits and size of repo

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

    # collects data using the git_repo repository object
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

    # runs through the complete repository directory to get number of files
    for (dirpath, dirnames, filenames) in walk(repository_dir):
        for filename in [f for f in filenames]:
            files.extend(filenames)
            break
    number_of_files_in_project = len(files)

    # parses all commits in commit log for repository
    commit_list = list(repository.iter_commits('master'))[:]
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

    # used for passing to database and/or to training module
    repo_data = (repository_name, assignees, size, commits, events, forks, branches, contributors,
                 labels, language_count, language_size, milestone, issues, refs, stargazer, subscribers, watchers,
                 network_count, count_open_issues, pulls, number_of_files_in_project, commit_size_sum,
                 commit_files_count, commit_insertion_count, commit_deletion_count, commit_lines_changed_count)

    return repo_data


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


def parse_dic(dic):
    """
    Parses the dictionary data into a list

    :param dic: Dictionary of parsed github commit log history
    :return totals: a list containing the filepath, num insertions, num deletions and num lines changed
    """
    # contains the totals from each commit (used in parse_dic)
    totals = []

    for path in list(dic.keys()):
        # Insertions and deletions for this particular filepath
        insertions = dic[path]['insertions']
        deletions = dic[path]['deletions']
        lines_changed = dic[path]['lines']

        totals.append([path, insertions, deletions, lines_changed])

    return totals


def get_averages(file_list, commit_hash, repo):
    """
    Creates a list containing the averages of all the commit history data

    :param file_list: a lst containing both dirty and grey files
    :param commit_hash: the hash of a given commit
    :param repo: the repository object
    :return: the filename, total inserts, insert averages, total deletions, deletion averages, total line changed,
            lines changed averages
    """
    commits = repo.iter_commits(commit_hash)
    file_totals = []


    """
    DONT TOUCH ANYTHING IN THE CODE BELOW
    
    for each commit in the commit history we look at its filenames and for each of those filenames we check to see
    if that filename is in the list [filenames] we mark our flag [check] as true which means the file does
    exist in [file_totals] therefor we create and update its values else if the file is not in [file_totals] we
    **DO SOMETHING I FORGET ASK CURTIS** and then we updated [file_totals] with the averages of each file.  
    """
    count = 1

    for commit in commits:
        commit_files = parse_dic(commit.stats.files)
        
        for path in commit_files:
            if path[0] in file_list:
                check = True
                for phile in file_totals:
                    adder = 1
                    for index, vals in enumerate(path[1:]):
                        phile[index + adder] += path[index + 1]
                        phile[index + adder + 1] += 1
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

                    file_totals.append(file_info)
                    
        
    for phile_info in file_totals:
        for index, update in enumerate(phile_info[1:]):
            if index % 2 == 0:
                val = phile_info[index + 1]
            else:
                phile_info[index + 1] = val / phile_info[index + 1]
         
    return file_totals
