from github import Github
from config import *


def access_github():
    """
    Access Githubs API and will allow for the gathering of repositories data
    :return: access to the github api using access token
    """

    return Github(git_access)
