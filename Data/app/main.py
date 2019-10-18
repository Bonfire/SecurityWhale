from SecWhale import *

def main():
    
    # TODO: create either and if elif statement to run script with application or....
    # this is for training
    try:
        start_time = time.time()
        git = access_github()
        
        #  Stored parsed csv file for repo cloning and parsing
        temp = get_repos()
    
        # each repo from list is cloned and parsed, as long as repo exists then obliterates it
        for name in temp:
            # used for collecting data for repo table
            github_repo = git.get_repo(name[0], lazy=False)
            repo_dir = clone_repo(name[0])
            # used in collecitng data in both tables
            repo = Repo(str(repo_dir))

            # here we check if we can access the actual repository object
            if not repo.bare:
                repo_database(repository_data(github_repo, repo, name[0], repo_dir))

                # remove repo name from list so we only have the commit hashes to look at
                name.remove(name[0])
                
                # data parsing
                for hash_commits in name:
                    dirty_data(repo, hash_commits)
                    clean_data(repo, hash_commits)

            else:
                print('Could not load repository at {} :'.format(repo_dir))
                
            # deletes entire cloned repo before cloning the next to save memory on local machines
            obliterate(repo_dir)

        print('Script Complete|Runtime: {} Seconds'.format(time.time() - start_time))
    except Exception as e:
        print("Could not check if repo existed", e)
        
    # TODO: implement code that will take data from application
    try:
        pass
    except Exception as e:
        print("Could not check if repo existed", e)

        
if __name__ == "__main__":
    main()
