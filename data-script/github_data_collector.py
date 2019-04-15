# imports
import mysql.connector
from mysql.connector import errorcode
from github import Github
from token_file import access_token
from git import Repo
import time
import os

# connect to MySQL database
try:
    conn = mysql.connector.connect(user='username', host='host addr',
                                   password='password',
                                   database="database name")

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
# count is was used to get number of commits referencing CVE
count = 0

#  repo names stored globally
github_repo = ''
repo_dir = ''

# clones github repo to any location on local computer
def git_clone():
    """
    Function clones repo to the desired directory to be used.
    """

    try:
        git_url = input("Git URL: ")
        clone_path = input("Where to clone repo: ")

        print("Cloning repo..")
        Repo.clone_from(git_url, clone_path)
        print("Cloning completed")
    except Exception as e:
        print("Clone unsuccessful", e)


# functions below request input
def git_repo():
    """
    Function gets the name of a github repository

    :return:
    """
    global github_repo
    global repo_dir

    # request access to github api
    repo_name = input("Github Repo name: ")
    github_repo = git.get_repo(repo_name, lazy=False)
    

    # for local projects
    repo_dir = input("Repo Directory: ")



# Functions Below get repository data
def repo_int_data():
    """
    Fetch integer value data from individual repositories on GitHub.
    This Function gets integer repo data as well as boolean data.

    :param:
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
    stargazer_dates = github_repo.get_stargazers_with_dates()
    for gaze in stargazer_dates:
        print(gaze.starred_at)
    stargazer = github_repo.get_stargazers().totalCount
    subs = github_repo.get_subscribers().totalCount
    watchers = github_repo.watchers_count
    size = github_repo.size

    # boolean data
    has_issue = github_repo.has_issues
    has_downloads = github_repo.has_downloads
    has_projects = github_repo.has_projects
    has_wiki = github_repo.has_wiki


def repo_dt_data():
    """
    Fetch datetime data from individual repositories on GitHub.
    This Function gets datetime repo data and data in the form of
    a looped paginated list.

    Data in the loop is currently just being printed but can be adjusted
    to be added to a list of sent over to a database.

    :param:
    """
    # TODO connect these points to the database
    # TODO parse date time data and send in as specified by database
    # date time data
    updated_at = str(github_repo.updated_at)
    repo_creation_date = str(github_repo.created_at)
    commits = github_repo.get_commits()
    for commit in commits:
        print(commit.commit.author.date)
        print(commit.commit.message)


# functions below get file data
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
    """
    # TODO connect these points to the database
    print('----')
    print("Sha: {}".format(str(commit.hexsha)))
    print("\"{}\" by {} ({})".format(commit.summary, commit.author.name,
                                     commit.author.email))
    # TODO Parse datetime to send to database
    print("Authored datetime: {}".format(str(commit.authored_datetime)))
    print("Committed datetime: {}".format(str(commit.committed_datetime)))
    print("Human Readable date and time {}".format(time.strftime("%a, %d %b %Y %H:%M",
                                                                 time.gmtime(commit.committed_date))))

    print(str("count: {} and size: {}".format(commit.count(), commit.size)))
    print("Files affected by commit: {}".format(commit.stats.files))
    print("Commits reachable from this commit: {}".format(commit.count()))
    print(commit.stats.total)
    print(commit.message)


def check_repo_for_cve(commit):
    """
     Checks that repo mentions cve in the commit history and how many times it does

    :param commit: Commit object from repository
    """

    global count
    if "cve" in commit.message.lower():
        count += 1


# TODO function that gets line count of each file in repository
def line_count(file_name):
    """
    Function gets each file in project and reads number of lines in the file

    :param file_name: name of file to open and count
    :return count:
    """
    # TODO connect this point to the database instead of returning it. not sure if count is accurate yet
    try:
        # reads in each line into a list and counts number of items in list
        count = len(open(file_name).readlines(  ))
        return count
    except Exception as e:
        return print("Failed to walk directory", e)


# TODO function that gets total number of files in a repository
def file_count():
    """
    Function uses the os module to go through a given directory and return the total number
    of files in it.

    :param:
    :return file_count: sum total of all files in given directory and its subdirectories
    """
    # TODO connect these points to the database
    try:
        # TODO figure how to look at one file at a time not just a list
        # not exactly sure what it's counting
        file_count = sum(len(files) for _, _, files in os.walk(repo_dir))
        print(file_count)
    except Exception as e:
        return None


def main():

    # TODO set up what to run
    # check repo loaded properly
    try:
        repo = Repo(repo_dir)

        path = ''
        file_count(path)
        # for _,_, files in os.walk(path):
        #     print(files)

        if not repo.bare:
            # create list of commits then print some of them to stdout with meta data
            commits = list(repo.iter_commits('master'))[:]  # number of commits in history
            for commit in commits:
                commit_data(commit)
                pass
        else:
            print('Could not load repository at {} :('.format(repo_dir))
    except Exception as e:
        print("Could not check if repo existed", e)



if __name__ == "__main__":
    main()
