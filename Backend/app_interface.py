'''
Authors: Thomas Serrano, Curtis Helsel
Last Updated: OCT-29-2019
'''
from config import *   #Application stuff
from predictor import *                    #ML stuff
from application import *
import sys                  #For command-line arguments
from git import Repo
from github import Github
from os import walk, path         #For parsing through directories

'''
Provides an interface to connect the application with the machine learning model
'''
def app_interface():
    #Get command-line arguments
    username = sys.argv[1]
    password = sys.argv[2]
    repo_name = sys.argv[3]
    repo_dir = sys.argv[4]
    
    #print("Starting interface...")
    #print("Arguments:\n" + username + " / " + password + " / " + repo_name + " / " + repo_dir)
    
    #Get Github access
    git = Github(username, password)

    #Grabs repo object for data collection
    github_repo = git.get_repo(repo_name, lazy=False)
    repo = Repo(repo_dir)
    
    #Retrieve repository data
    repo_data = list(repository_data(github_repo, repo, repo_name, repo_dir))
    
    #Retrieve all filenames for all directories
    files_path = []
    for root, dirs, files in walk(repo_dir):
        f_path = root.split(path.basename(repo_name) + "/")[-1]
        if f_path is not "":
            f_path += "/"
        files_path += [str(f_path + f) for f in files if not f[0] == '.']
        dirs[:] = [d for d in dirs if not d[0] == '.']

    #Get a list of data points for each file
    avgs = get_averages(files_path, repo.head.commit.hexsha, repo)

    #Separate file names from avgs
    file_names = []
    avgs_final = []
    for a in avgs:
        file_names.append(a[0])
        avgs_final.append(a[1:])
    
    #Collect data together into a single list
    final_data = []
    for af in avgs_final:
        final_data.append(af + repo_data[1:])
        
    #Predict and print results data to std out
    results = predict(final_data)
    
    #Final results output
    for i, f in enumerate(file_names):
        print(f + "," + str(results[i][0]))

#Main - delete later?
if __name__ == "__main__":
    app_interface()
