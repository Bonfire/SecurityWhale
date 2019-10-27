import os
import time
import shutil
from git import Repo
from parse import get_repos
from cloner import clone_repo
from application import get_averages, parse_dic, update_db


# TODO: needs to be worked on
# def features_array(git_repo, repository, repository_name, repository_dir):
#     """
#     combines data points and creates a numpy array
#     :param git_repo: gitpython repo object to get data from api
#     :param repository: pygithub object used to get data from api
#     :param repository_name: name of repo on github
#     :param repository_dir: location of cloned repo
#     :return:
#     """
#     repo_data = list(repository_data(git_repo, repository, repository_name, repository_dir))
#
#     features = []
#
#     # Populates features with sub-arrays containing [filename, file_data[], repo_data[]]
#     for key in averages:
#         features.append([key, averages[key], repo_data])
#
#     # Converts to a numpy array before returning
#     return np.array(features)


def dirty_data(repository, commit_hash):
    """
    Dirty Data takes a given commit_hash and parses information from it which is then stored in the database

    it also stores the filename in a list that is stored globally

    :param repository: full repository object
    :param commit_hash: a hash of a specific commit in a repository
    :return:
    """

    # get the data on a specific hash from the given repo
    commit = repository.commit(commit_hash)

    print('parsing into black list')
    black_list = list(commit.stats.files.keys())
    if len(black_list) != 1:
        print("Current file_list has " + str(len(black_list)) + " files.")
        return black_list, None, None
    
    # use totals to find averages
    commit_size = commit.size
    print('get averages for ' + black_list[0])
    averages = get_averages(black_list, commit_hash, repository)

    for fault_file in averages:
        fault_file.insert(1, 1)
        fault_file.insert(1, commit_hash)
        fault_file += [commit.size]

    return None, black_list, averages


def clean_data(repository, commit_hash, black, grey):
    """
    Clean data looks through a repository history up until the given commit hash to find another file
    with the same extension. It strips the filename for the extension then checks to see it another filename
    matches that extension in the commit history. once found parse that commit to database as clean data
    :param repository:
    :param commit_hash:
    :return:
    """

    clean_files = []
    dirty_ext = []

    commits = repository.iter_commits(commit_hash)
     
    # grab the extension of the dirty file
    for path in black:
        dirty_ext.append(path.split(".")[-1])

    dup_path = list(set(black + grey))

    # go through commit history up to dirty file commit and store all files with the same extension as the
    # dirty_ext into a list
    for commit in commits:
        for path in commit.stats.files:
            if path.split(".")[-1] in dirty_ext:
                if path not in dup_path:
                    clean_file = get_averages([path],commit_hash,repository)[0]
                    clean_file.insert(1, 0)
                    clean_file.insert(1, commit_hash)
                    clean_file += [commit.size]
                    clean_files.append(clean_file)

                    dirty_ext.remove(path.split(".")[-1])
                    if len(dirty_ext) == 0:
                        return clean_files


if __name__ == "__main__":
    try:
        start_time = time.time()
        
        temp = get_repos()[:5]

        # THIS IS FOR TESTING ONLY AS TO NOT CLONE A BILLION FILES CONSTANTLY
        #['bbengfort/confire','8cc86a5ec2327e070f1d576d61bbaadf861597ea'],
        #['jcupitt/libvips','20d840e6da15c1574b3ed998bc92f91d1e36c2a5'],
        #[['bratsche/pango', '4de30e5500eaeb49f4bf0b7a07f718e149a2ed5e']]
        #temp = [['pornel/pngquant',
        #'b7c217680cda02dddced245d237ebe8c383be285']]
        #'''['bestpractical/rt', '057552287159e801535e59b8fbd5bd98d1322069',
        #'2338cd19ed7a7f4c1e94f639ab2789d6586d01f3'],
        #['Telaxus/EPESI', 'dda3e5f3843e80fe9ed36115f4fe72c06a3a41bc',
        #'36be628c2597fd0209224a09b17858294f49c585',
        #'3cd666558c89d9c4b27eb74bf6b8e81b4f6e7118',
        #'48fb5e81cd7a47d98bade092a5a72d8177621dbd',
        #'48fb5e81cd7a47d98bade092a5a72d8177621dbd',
        #'48fb5e81cd7a47d98bade092a5a72d8177621dbd']]
        # ['hapijs/hapi', 'aab2496e930dce5ee1ab28eecec94e0e45f03580'],
        # ['SickRage/SickRage', '8156a74a68aea930d1e1047baba8b115c3abfc44'],
        # ['reubenhwk/radvd', '92e22ca23e52066da2258df8c76a2dca8a428bcc'],
        # ['NixOS/nixpkgs', '6c59d851e2967410cc8fb6ba3f374b1d3efa988e']]

        for name in temp:

            # Pop the first item on the list to leave only hashes
            github_name = name.pop(0)
            print("Starting process for " + github_name)

            if os.path.exists(os.path.basename(github_name)):
                shutil.rmtree(os.path.basename(github_name))

            # Clones repo to current directory and gets the github data
            repo_dir = clone_repo(github_name)
            repo = Repo(repo_dir)
    
            # lists containing the files we know that have faults and those that may or may not have them
            black_files = []
            grey_files = []
            clean_files = []
            db_update = []

            for hash_commits in name:
                print('processing dirty data for hash: ' + hash_commits)
                grey, black, update = dirty_data(repo, hash_commits)
                print('finished dirty data for hash: ' + hash_commits)

                if grey is not None:
                    grey_files += grey

                if black is not None:
                    black_files += black
                    db_update += update

            grey_files = list(set(grey_files))
            black_files = list(set(black_files))

            print('finished updating black and grey lists')
            # once we get the complete black and grey lists we parse them to find the clean data
            if len(black_files) != 0:
                for hash_commits in name:
                    print('processing clean data')
                    clean_files += clean_data(repo, hash_commits, black_files, grey_files)
                    db_update += clean_files
                    if len(clean_files) == len(black_files):
                        break
                    print('finished clean data')
            
        
            if len(db_update) > 0:
                print("Updating db")
                update_db(db_update, github_name, repo_dir, repo) 
                print()
            else:
                print(github_name + " has no valid files for database")
                print()
            
            # Removes the cloned repo from current directory
            shutil.rmtree(repo_dir)
           

        print('Script Complete|Runtime: {} Seconds'.format(time.time() - start_time))
        print()
            

    except Exception as e:
        print(e)
