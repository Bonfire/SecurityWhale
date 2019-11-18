'''
Authors: Thomas Serrano, Curtis Helsel, Baran Barut
Last Updated: NOV-18-2019
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

	#Get number of command-line arguments
	argc = len(sys.argv)
	
	'''
	print("Starting interface...")
	print(argc)
	for a in sys.argv:
		print(a)
	'''
	
	#Must have 1, 2, or 4 arguments (program name is the first argument)
	if argc not in [2, 3, 5]:
		print("\n\tERROR: INCORRECT NUMBER OF ARGUMENTS PASSED")
		exit()
	
	#Used for flow control for different types of repos
	repo_remote = False
	repo_private = False
	
	#The first argument is the repo directory. This is required for all repos, and is the only
	#argument for local repos.
	repo_dir = sys.argv[1]
	repo = Repo(repo_dir)
    
	#Repo name is required for remote & public repos.
	if argc > 2:
		repo_remote = True
		repo_name = sys.argv[2]
		
		#Login information is required for remote & private repos.
		if argc > 3:
			repo_private = True
			username = sys.argv[3]
			password = sys.argv[4]

		#Get Github access. Use login if applicable.
		if repo_private:
			git = Github(username, password)
		else:
			git = Github(None, None)

		#Grabs repo object for data collection
		github_repo = git.get_repo(repo_name, lazy=False)
    
		#Retrieve repository data
		repo_data = list(repository_data(github_repo, repo, repo_name, repo_dir))

    #Retrieve all filenames for all directories
    files_path = []
	
	if repo_remote:
		for root, dirs, files in walk(repo_dir):
			f_path = root.split(path.basename(repo_name) + "\\")[-1]
			if f_path is not "":
				f_path += "/"
			files_path += [str(f_path + f) for f in files if not f[0] == '.']
			dirs[:] = [d for d in dirs if not d[0] == '.']
	else:
		for (_, _, filenames) in walk(repo_dir):
			for f in filenames:
				files_path += [f]

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
	
	#If this is a remote repo, we need to add the repo data, otherwise use averages
	if repo_remote:
		for af in avgs_final:
			final_data.append(af + repo_data[1:])
	else:
		final_data = avgs_final
	       
    #Predict and print results data to std out
    results = predict(final_data)
    
    #Final results output
    for i, f in enumerate(file_names):
        print(f + "," + str(results[i][0]))

#Main - delete later?
if __name__ == "__main__":
    app_interface()
