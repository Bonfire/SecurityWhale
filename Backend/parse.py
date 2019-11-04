# Author: Curtis Helsel

import csv
import re


def get_repos():
    """
    Parses csv file containing github repositories that have security faults and stores them in a nested list
    with index 0 being the repo name and the remaining indices contain the commit hashes
    :return:
    """
    count = 0
    commits = []
    repos = []
    commit_hash = []
    with open("../source/allitems.csv", newline="", encoding="utf8") as f:
        reader = csv.DictReader(f)
        headers = reader.fieldnames

        for row in reader:
            try:
                git = re.search("github.com\/(\w+\/\w+)\/commit\/(\w{40})", str(row))
                repos.append(git.group(1))
                commits.append(git)
                count += 1
            except AttributeError:
                pass

    repo = list(set(repos))
    list_repo_commit = []

    for r in repo:
        repo_commit = []
        repo_commit.append(r)
        for c in commits:
            if r in c.group(0):
                repo_commit.append(c.group(2))
        list_repo_commit.append(repo_commit)

    repo_hash = sorted(list_repo_commit, key=len)

    return repo_hash


# TESTING ONLY do not un comment and then run SecWhale
# if __name__ == "__main__":
#
#     temp = get_repos()
#
#     for name in temp:
#         print(name)
#         # name.remove(name[0])
#         # for chash in name:
#         #     print(chash)
