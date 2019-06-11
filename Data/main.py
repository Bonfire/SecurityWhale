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

# get the name of the online repo, and the directory on the local computer of
# same repo
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
    stargazer = github_repo.get_stargazers().totalCount
    subscribers = github_repo.get_subscribers().totalCount
    watchers = github_repo.watchers_count
    size = github_repo.size
    repo_creation_date = str(github_repo.created_at)

    try:
        conn = mysql.connector.connect(user=user, host=host,
                                       password=password,
                                       database=database)
        cursor = conn.cursor()

        # TODO insert data into database

        sql_insert_query = """INSERT INTO repo (repo_name, repo_initial_creation, stargazers,
        watchers, subscribers, contributors, branches, assignees, labels, forks, pulls, events,
        network, open_issues) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

        insert_tuple = (repo_name, repo_creation_date, stargazer, watchers, subscribers,
                        contributors, branches, assignees, labels, forks, pulls, events, network_count, issues)

        cursor.execute(sql_insert_query, insert_tuple)
        conn.commit()
        print('Records were inserted successfully into repo table')

        sql_select_query = "SELECT * from repo"
        cursor.execute(sql_select_query)
        records = cursor.fetchall()

        for row in records:
            print(row)

        print('REPO DATA COLLECTION COMPLETED')

    except mysql.connector.Error as err:
        if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
            print("Something is wrong with your user name or password")
        elif err.errno == errorcode.ER_BAD_DB_ERROR:
            print("Database does not exist")
        else:
            print(err)
    else:
        print('CLOSING DATEBASE')
        conn.close()


def file_data(repo):
    """Function Gathers data on a file such as line count,
    hex, commit info and stores the values which are then sent over to the
    database

    :param repo: a connection to the repo we request through git
    :return:
    """

    try:
        conn = mysql.connector.connect(user=user, host=host,
                                       password=password,
                                       database=database)
        cursor = conn.cursor()

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
        repo = Repo(repo_dir)

        # if repo exists
        if not repo.bare:
            file_data(repo)
        else:
            print('Could not load repository at {} :('.format(repo_dir))
    except Exception as e:
        print("Could not check if repo existed", e)


if __name__ == "__main__":
    main()
