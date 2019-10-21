import sys
#from github import Github
#import worker.py

'''
Interfaces the desktop application with the data parsing layer
'''

def app_interface():
	#Get command-line arguments
	username = sys.argv[1]
	password = sys.argv[2]
	repo_name = sys.argv[3]
	clone_dir = sys.argv[4]
	
	#Do they want to add it to the DB?
	add_signal = sys.argv[5]
	
	print("Starting interface...")
	print("Arguments:\n" + username + " / " + password + " / " + repo_name + " / " + clone_dir + " / " + add_signal)
	
	#Get Github access
	#git = Github(username, password)
	
	#Retrieve repo & github data
	#repo, git_data = repo_get(repo_name, git, clone_dir, add_signal)
	
	#Tuple into a list, calls to get file information for ALL of the commit history
	#git data first in array sent to training
	#Go through files

#Main - delete later?
#if __name__ == "__main__":
	#app_interface()
app_interface()
