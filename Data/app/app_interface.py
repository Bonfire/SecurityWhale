'''
Author: Thomas Serrano

'''
import application		#Application stuff
import conn					#ML stuff
import sys					#For command-line arguments
from git import Repo
from github import Github
from os import walk			#For parsing through directories

'''

'''
def app_interface():
	#Get command-line arguments
	username = sys.argv[1]
	password = sys.argv[2]
	repo_name = sys.argv[3]
	clone_dir = sys.argv[4]
	
	print("Starting interface...")
	print("Arguments:\n" + username + " / " + password + " / " + repo_name + " / " + clone_dir)
	
	#Get Github access
	git = Github(username, password)

    #Grabs repo object for repo table data (collecting)
	github_repo = git.get_repo(repo_name, lazy=False)
	repo = Repo(clone_dir)
	
	#Retrieve repository data
	repo_data = repository_data(github_repo, repo, repo_name, clone_dir)
	
	#Retrieve all filenames for all directories
	files = []
	for (dirpath, dirnames, filenames) in walk(path):        
		for filename in [f for f in filenames]:
			files.append(filename)
			break
	
	avgs = get_averages(files, hash, repo)
	
	#Collect data together into a single list
	final_data = []
	for a in avgs:
		final_data.append(a + repo_data)
	
	#Predict and print results data to std out
	'''
	fin_csv
	i = 0
	results = predict(final_data)
	for r in results:
		fin_csv.append(files[i] + "," + str(r))
		
	print(fin_csv)
	'''

#Main - delete later?
if __name__ == "__main__":
	app_interface()
