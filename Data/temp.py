import mysql.connector
from mysql.connector import errorcode
from github import Github
from token_file import access_token
from git import Repo
import os
import time
from database_connection import username as username
from database_connection import host_name as host_name
from database_connection import psswrd as psswrd
from database_connection import database_name as database_name

# connect to MySQL database
try:
    conn = mysql.connector.connect(user=username, host=host_name,
                                   password=psswrd,
                                   database=database_name)

    print("Connected to MySQL Database")
except mysql.connector.Error as err:
    if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
        print("Something is wrong with your user name or password")
    elif err.errno == errorcode.ER_BAD_DB_ERROR:
        print("Database does not exist")
    else:
        print(err)
else:
    conn.close()

# get access to github API using access token
git = Github(access_token)
# github repository names stored globally
github_repo = ''
repo_dir = ''

# mycursor = conn.cursor()


# clones github repo to any location on local computer
def git_clone():
    """
    Function clones repo to the desired directory to be used.

    :param:
    :return:
    """

    try:
        git_url = input("Git URL: ")
        clone_path = input("Where to clone repo: ")

        print("Cloning repo..")
        Repo.clone_from(git_url, clone_path)
        print("Cloning completed")
    except Exception as e:
        print("Clone unsuccessful", e)


def get_repo():
    """
    Function gets the name of a github repository

    :param:
    :return:
    """
    global github_repo
    global repo_dir

    # request access to github api
    repo_name = input("Github Repo name: ")
    github_repo = git.get_repo(repo_name, lazy=False)
    # for local projects
    repo_dir = input("Repo Directory: ")


def repo_int_data():
    """
    Fetch integer value data from individual repositories on GitHub.
    This Function gets integer repo data as well as boolean data.

    :param:
    :return:
    """
    # TODO connect these points to the database
    # integer Repo data
    assignees = github_repo.get_assignees().totalCount
    branches = github_repo.get_branches().totalCount
    contributors = github_repo.get_contributors().totalCount
    count_open_issues = github_repo.open_issues_count
    commits = github_repo.get_commits().totalCount
    events = github_repo.get_events().totalCount
    forks = github_repo.get_forks().totalCount
    issues = github_repo.get_issues().totalCount
    labels = github_repo.get_labels().totalCount
    languages = github_repo.get_languages()
    milestone = github_repo.get_milestones().totalCount
    network_count = github_repo.network_count
    pulls = github_repo.get_pulls().totalCount
    refs = github_repo.get_git_refs().totalCount
    # TODO how do we connect this to database?------------
    stargazer_dates = github_repo.get_stargazers_with_dates()
    for gaze in stargazer_dates:
        print(gaze.starred_at)
    # ----------------------------------------------------
    stargazer = github_repo.get_stargazers().totalCount
    subs = github_repo.get_subscribers().totalCount
    watchers = github_repo.watchers_count
    size = github_repo.size

    # boolean data
    has_issue = github_repo.has_issues
    has_downloads = github_repo.has_downloads
    has_projects = github_repo.has_projects
    has_wiki = github_repo.has_wiki

    # # insert data into database
    # sql = """INSERT INTO repo (assignees, branches, contributors,
    # count_open_issues, commits, events, forks, issues, labels, languages,
    # milestones, network_count, pulls, refs, stargazer, subs, watchers,
    # size, has_issue, has_downloads, has_projects, has_wiki) VALUES (%d, %d, %d,
    # %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %s, %s, %s, %s)
    # """
    #
    # val = [assignees, branches, contributors, count_open_issues, commits,
    #        events, forks, issues, labels, languages, milestone, network_count,
    #        pulls, refs, stargazer, subs, watchers, size, has_issue,
    #        has_downloads, has_projects, has_wiki]
    #
    # mycursor.execute(sql, val)
    # mycursor.commit()
    # mycursor.close()


def repo_dt_data():
    """
    Fetch datetime data from individual repositories on GitHub.
    This Function gets datetime repo data and data in the form of
    a looped paginated list.

    Data in the loop is currently just being printed but can be adjusted
    to be added to a list of sent over to a database.

    :param:
    :return:
    """
    # TODO connect these points to the database
    # TODO parse date time data and send in as specified by database
    # date time data
    # updated_at = str(github_repo.updated_at)
    # repo_creation_date = str(github_repo.created_at)
    #
    # sql = "INSERT INTO repo (updated_at, repo_creation_date) VALUES (%s, %s)"
    # val = [updated_at, repo_creation_date]
    #
    # mycursor.execute(sql, val)
    # mycursor.execute()
    # mycursor.close()


def commit_data(commit):
    """
    The print_commit function takes in a GitPython commit object and prints
    the 40-character SHA-1 hash for the commit followed by:

    the commit summary
    author name
    author email
    commit date and time
    count and update size

    :param commit: commit object from repository
    :return:
    """
    # TODO connect these points to the database
    hexsha = str(commit.hexsha)
    commit_datetime = str(commit.committed_datetime)

    readable_datetime = time.strftime("%a,%d%b%Y%H:%M",
                                      time.gmtime(commit.committed_date))
    commit_count = commit.count()
    commit_size = commit.size()

    print(commit.stats.files)
    print(commit.stats.total)

    # sql = """INSERT INTO file (hexsha, commit_datetime, readable_datetime,
    # commit_count, commit_size, commit_files, commit_stats) VALUES (%s, %s, %d,
    # %d)"""
    #
    # val = [hexsha, commit_datetime, readable_datetime, commit_count,
    #        commit_size]
    #
    # mycursor.execute(sql, val)
    # mycursor.execute()
    # mycursor.close()


# TODO function that gets line count of each file in repository
def line_count(file_name):
    """
    Function gets each file in project and reads number of lines in the file

    :param file_name: name of file to open and count
    :return count:
    """
    # TODO connect this point to the database instead of returning it.
    # not sure if count is accurate yet
    try:
        # reads in each line into a list and counts number of items in list
        count = len(open(file_name).readlines())
        return count
    except Exception as e:
        return print("Failed to walk directory", e)


# TODO function that gets total number of files in a repository
def file_count():
    """
    Function uses the os module to go through a given directory and
    return the total number of files in it.

    :param:
    :return file_count: sum total of all files in given directory and
                        its subdirectories
    """
    # TODO connect these points to the database
    try:
        # TODO figure how to look at one file at a time not just a list
        # not exactly sure what it's counting
        file_count = sum(len(files) for _, _, files in os.walk(repo_dir))
        print(file_count)
    except Exception:
        return None


def main():
    # TODO set up what to run
    # check repo loaded properly
    get_repo()
    try:
        repo = Repo(repo_dir)

        path = ''
        file_count(path)
        # for _,_, files in os.walk(path):
        #     print(files)

        # if repo exists
        if not repo.bare:
            # get complete list of commits in history
            commits = list(repo.iter_commits('master'))[:]
            # get commit data from each commit
            for commit in commits:
                commit_data(commit)
                pass
        else:
            print('Could not load repository at {} :('.format(repo_dir))
    except Exception as e:
        print("Could not check if repo existed", e)


if __name__ == "__main__":
    main()
