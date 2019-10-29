'''
Author: Thomas Serrano
Last Updated: OCT-28-2019
'''
from application import *	#Application stuff
#import conn					#ML stuff
import sys					#For command-line arguments
from git import Repo
from github import Github
from os import walk			#For parsing through directories

'''
Provides an interface to connect the application with the machine learning model
'''
def app_interface():
	#Get command-line arguments
	username = sys.argv[1]
	password = sys.argv[2]
	repo_name = sys.argv[3]
	repo_dir = sys.argv[4]
	
	print("Starting interface...")
	#print("Arguments:\n" + username + " / " + password + " / " + repo_name + " / " + repo_dir)
	
	#Get Github access
	git = Github(username, password)

	#Grabs repo object for data collection
	github_repo = git.get_repo(repo_name, lazy=False)
	repo = Repo(repo_dir)
	
	#print(repo.head.commit.hexsha)
	
	#Retrieve repository data
	repo_data = list(repository_data(github_repo, repo, repo_name, repo_dir))
	
	#Retrieve all filenames for all directories
	files = []
	for (dirpath, dirnames, filenames) in walk(repo_dir):        
		for filename in [f for f in filenames]:
			files.append(filename)
	
	#Get a list of data points for each file
	avgs = get_averages(files, repo.head.commit.hexsha, repo)
	
	#Collect data together into a single list
	final_data = []
	for a in avgs:
		final_data.append(a + repo_data)
	
	print("\n==========")
	print("AVERAGES:")
	print(avgs)
	
	print("\n\n==========")
	print("REPO DATA:")
	print(repo_data)
	
	print("\n\n==========")
	print("FINAL DATA:")
	print(final_data)
	
	#TODO: Predict and print results data to std out
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
