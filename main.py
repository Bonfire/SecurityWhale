from database_connection import cursor
from database_connection import conn
from github import Github
from token_file import access_token
from git import Repo
import os
import time

# My personal github API token, must have to access data
git = Github(access_token)

# get the name of the online repo, and the directory on the local computer of same repo
# using two different libraries getting data different ways
repo_name = input('Name of Github Repo: ')
github_repo = git.get_repo(repo_name, lazy=False)
repo_dir = input('Repo Directory: ')


def repo_data():
    """Function Gathers data on a repository such as subscribers,
    and issues and stores the values which are then sent over to the database

    :param:
    :return:
    """
    assignees = github_repo.get_assignees().totalCount
    branches = github_repo.get_branches().totalCount
    contributors = github_repo.get_contributors().totalCount
    events = github_repo.get_events().totalCount
    forks = github_repo.get_forks().totalCount
    issues = github_repo.get_issues().totalCount
    labels = github_repo.get_labels().totalCount
    network_count = github_repo.network_count
    pulls = github_repo.get_pulls().totalCount
    stargazer = github_repo.get_stargazers().totalCount
    subscribers = github_repo.get_subscribers().totalCount
    watchers = github_repo.watchers_count
    repo_creation_date = str(github_repo.created_at)

    sql = """INSERT INTO repo (repo_name, repo_initial_creation, stargazers, watchers, subscribers, contributors,
    branches, assignees, labels, forks, pulls, events, network, open_issues) VALUES (%s,
    %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

    data = [repo_name, repo_creation_date, stargazer, watchers, subscribers, contributors, branches,
            assignees, labels, forks, pulls, events, network_count, issues]

    # TODO Determine why data wont execute
    try:
        print("trying")
        # store data in sql database
        cursor.execute(sql, data)
        print("data executed")
        # make sure data is committed to database
        conn.commit()
        print("data commited")
        # close connections
        cursor.close()
        conn.close()
        print("data added successfully")
    except Exception:
        print("data not added")


def file_data(repo):
    """Function Gathers data on a file such as line count,
    hex, commit info and stores the values which are then sent over to the
    database

    :param repo: a connection to the repo we request through git
    :return:
    """

    # with all the commits, go through and pull data
    commits = list(repo.iter_commits('master'))[:1]
    for commit in commits:
        hexsha = commit.hexsha
        committed_datetime = commit.committed_datetime
        commit_size = commit.size
        commit_stats = commit.stats.files

        print("hexsha: ", hexsha)
        print("committed datetime:", str(committed_datetime))
        print("commit size:", commit_size)
        # iterations through commit stats and parses dictionaries into
        # variables
        # gives me file path
        # insertion, deletion, lines changes
        for file_path, value in commit_stats.items():
            print(file_path)
            for type_of_change, change_value in value.items():
                print(type_of_change, change_value)

    print("-" * 80)

    # # list of all the files in project directory and subdirectories
    # files = []
    # # r = root, d = directory, f = files
    # for r, d, f in os.walk('C:\openssl'):
    #     for file in f:
    #         if ".z" not in file:
    #             files.append(os.path.join(r,file))
    # # print file count
    # total = 0
    # for f in files:
    #     total += len(f)
    #
    # print("total file count:", total)
    # #==========================================================
    # files = folders = 0
    #
    # for _, dirnames, filenames in os.walk("C:\openssl"):
    #     # ^ this idiom means "we won't be using this value"
    #         files += len(filenames)
    #         folders += len(dirnames)
    #
    # print("{:,} files, {:,} folders".format(files, folders))

    # C:\Users\bbkyl\Dropbox\School\Senior-Design-Project\test\openssl


def main():
    try:
        repo = Repo(repo_dir)

        # if repo exists
        if not repo.bare:
            file_data(repo)
            print('works')
        else:
            print('Could not load repository at {} :('.format(repo_dir))
    except Exception as e:
        print("Could not check if repo existed", e)


if __name__ == "__main__":
    main()
