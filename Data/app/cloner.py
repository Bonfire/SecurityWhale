# Author: Michael Harris

import os
from git.repo.base import Repo

def clone_repo(github_name):
    """
    Clones a repository to a given location

    :param repository_name: The full name of a given repository
    :return:
    """
    repo = "./" + os.path.basename(github_name)
    Repo.clone_from('https://github.com/' + github_name, repo)
    
    return repo
