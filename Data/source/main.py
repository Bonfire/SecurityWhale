# Author: Kyle Reid
# Last updated: 9.21.19

import time

from git import Repo

from pvSec import access_github
from pvSec import log_data
from pvSec import repo_database_connector
from pvSec import repository_data

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

