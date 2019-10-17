import re
import csv

def get_repos():
    count = 0
    commits = []
    repos = []
    commit_hash = []
    with open("./allitems.csv", newline="") as f, open ("./entry.csv", "w", newline="") as t:
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

