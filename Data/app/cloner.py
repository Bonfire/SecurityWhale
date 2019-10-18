# Author: Michael Harris

from git.repo.base import Repo


def clone_repo(repository_name):
    """
    Clones a repository to a given location

    :param repository_name: The full name of a given repository
    :return:
    """

    Repo.clone_from('https://github.com/' + repository_name, 'C:/Users/bbkyl/Desktop/Data Tool/data/' + repository_name)
    
    # this sould allow me ot delete the directory later
    Repo.close()
    
    return 'C:/Users/bbkyl/Desktop/Data Tool/data/' + str(repository_name)
