import time

# error exceptions and API/repo access
import mysql.connector
from mysql.connector import errorcode

from application import access_github
from application import get_averages
from application import parse_dic
from application import repo_get
from cloner import clone_repo
from config import database
from config import host
from config import password
from config import user
from parse import get_repos

import shutil
import os
from git import Repo

# increment through database
# not sure what it actually does
repo_id = 1


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
    global repo_id

    # these commits are known faults so flags will always be 1
    flag = 1

    # get the data on a specific hash from the given repo
    commit = repository.commit(commit_hash)

    print('parsing into black list')
    black_list = list(commit.stats.files.keys())
    print(black_list)
    if len(black_list) != 1:
        print("Current file_list has " + str(len(black_list)) + " files.")
        return black_list, None
    
    
    
    # use totals to find averages
    commit_size = commit.size
    print('get averages for ' + black_list[0])
    # averages = get_averages(black_list, commit_hash, repository)
    print("getting averages")
    ''' 
    filename = averages[0]
    total_ins = averages[1]
    ins_avg = averages[2]
    total_del = averages[3]
    del_avg = averages[4]
    total_lines = averages[5]
    lines_avg = averages[6]

    print('add to database')
    try:
        conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
        cursor = conn.cursor()

        # Updates multiple columns of a single row in table
        repo_file_sql_update_query = """INSERT INTO file (repoID, filename, has_fault,total_inserts,
    insert_averages, total_deletions, deletion_averages, total_lines, line_averages, commit_size) VALUES (%s,
     %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

        log_inserts = (
            repo_id, filename, flag, total_ins, ins_avg, total_del, del_avg, total_lines, lines_avg,
            commit_size)

        # log_inserts2 = (repo_id, averages[0], flag)
        # for item in averages[1:]:
        #     log_inserts2 += item
        # log_inserts2 += commit_size

        cursor.execute(repo_file_sql_update_query, log_inserts)
        conn.commit()

    except mysql.connector.Error as err:
        if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
            print("Something is wrong with your user name or password")
        elif err.errno == errorcode.ER_BAD_DB_ERROR:
            print("Database does not exist")
        else:
            print(err)
    else:
        conn.close()
    '''
    return None, black_list


def clean_data(repository, commit_hash, black, grey):
    """
    Clean data looks through a repository history up until the given commit hash to find another file
    with the same extension. It strips the filename for the extension then checks to see it another filename
    matches that extension in the commit history. once found parse that commit to database as clean data
    :param repository:
    :param commit_hash:
    :return:
    """

    file_holder = []
    flag = 0
    check = 0
    dirty_ext = []

    # since iter_commits does no reset the list each time its used in a loop we need 2 variable  one for each for loop
    commits = list(repository.iter_commits(commit_hash))
     
    # grab the extension of the dirty file
    for path in black:
        dirty_ext.append(path.split(".")[-1])

    dup_path = list(set(black + grey))

    
    # go through commit history up to dirty file commit and store all files with the same extension as the
    # dirty_ext into a list
    print('processing clean files')
    for commit in commits:
        for path in commit.stats.files:
            if path.split(".")[-1] in dirty_ext:
                if path not in dup_path:
                    file_holder += get_averages(path, commit_hash,repository)
                    dirty_ext.remove(path.split(".")[-1])
                    if len(dirty_ext) == 0:
                        return file_holder


    ''' 
    # grab the last file in the list and run through the commit log again, once the filenames match parse that
    # commit and store in database
    print('processing loop 2')
    for commit in commit_logx:
        for path in commit.stats.files:
            # if files match confirm and breka second loop
            if path == file_holder[-1]:
                check = 1
                break
        # check confirmation and grab that files commit object and breka other wise continue first loop 
        if check == 1:
            new_commit = commit.stats.files
            break
        else:
            continue

    # Parse all the information form the Good data and store in database
    white_list = parse_dic(new_commit.stats.files)
    # use totals to find averages
    commit_size = commit.size
    print('get averages clean data')
    averages = get_averages(white_list, commit_hash, repository)

    filename = averages[0]
    total_ins = averages[1]
    ins_avg = averages[2]
    total_del = averages[3]
    del_avg = averages[4]
    total_lines = averages[5]
    lines_avg = averages[6]

    print('add clean data to database')
    try:
        conn = mysql.connector.connect(user=user, host=host, password=password, database=database)
        cursor = conn.cursor()

        # Updates multiple columns of a single row in table
        repo_file_sql_update_query = """INSERT INTO file (repoID, filename, has_fault,total_inserts,
        insert_averages, total_deletions, deletion_averages, total_lines, line_averages, commit_size) VALUES (%s,
         %s, %s, %s, %s, %s, %s, %s, %s, %s)"""

        log_inserts = (
            repo_id, filename, flag, total_ins, ins_avg, total_del, del_avg, total_lines, lines_avg,
            commit_size)
        # log_inserts2 = (repo_id, averages[0], flag)
        # for item in averages[1:]:
        #     log_inserts2 += item

        cursor.execute(repo_file_sql_update_query, log_inserts)
        conn.commit()

    except mysql.connector.Error as err:
        if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
            print("Something is wrong with your user name or password")
        elif err.errno == errorcode.ER_BAD_DB_ERROR:
            print("Database does not exist")
        else:
            print(err)
    else:
        conn.close()
    '''


if __name__ == "__main__":
    try:
        start_time = time.time()
        git = access_github()


        # temp = get_repos()

        # THIS IS FOR TESTING ONLY AS TO NOT CLONE A BILLION FILES CONSTANTLY
        #[['bbengfort/confire','8cc86a5ec2327e070f1d576d61bbaadf861597ea'],
        #['jcupitt/libvips','20d840e6da15c1574b3ed998bc92f91d1e36c2a5'],
        #['bratsche/pango', '4de30e5500eaeb49f4bf0b7a07f718e149a2ed5e'],
        #temp = [['pornel/pngquant', 'b7c217680cda02dddced245d237ebe8c383be285']]
        #['bestpractical/rt', '057552287159e801535e59b8fbd5bd98d1322069',
        #'2338cd19ed7a7f4c1e94f639ab2789d6586d01f3']
        temp = [['Telaxus/EPESI', 'dda3e5f3843e80fe9ed36115f4fe72c06a3a41bc',
        '36be628c2597fd0209224a09b17858294f49c585',
        '3cd666558c89d9c4b27eb74bf6b8e81b4f6e7118',
        '48fb5e81cd7a47d98bade092a5a72d8177621dbd',
        '48fb5e81cd7a47d98bade092a5a72d8177621dbd',
        '48fb5e81cd7a47d98bade092a5a72d8177621dbd']]
        
        # ['hapijs/hapi', 'aab2496e930dce5ee1ab28eecec94e0e45f03580'],
        # ['SickRage/SickRage', '8156a74a68aea930d1e1047baba8b115c3abfc44'],
        # ['reubenhwk/radvd', '92e22ca23e52066da2258df8c76a2dca8a428bcc'],
        # ['NixOS/nixpkgs', '6c59d851e2967410cc8fb6ba3f374b1d3efa988e']]

        for name in temp:
            

            # Pop the first item on the list to leave only hashes
            github_name = name.pop(0)
            print("Starting process for " + github_name)
#            if os.path.exists(os.path.basename(github_name)):
#                shutil.rmtree(os.path.basename(github_name))

            # Clones repo to current directory and gets the github data
            #repo_dir = clone_repo(github_name)
            #repo, _ = repo_get(github_name, git, repo_dir)
    
            # Curtis testing 
            #repo = Repo(repo_dir)
            repo = Repo("./EPESI")

            
            # PRINT DEBUGGING
            print('Finished storing repo data\n')

            # lists containing the files we know that have faults and those that may or may not have them
            black_files = []
            grey_files = []
            
            for hash_commits in name:
                print('processing dirty data')
                grey, black = dirty_data(repo, hash_commits)
                print('finished dirty data')
                print()

                if grey is not None:
                    grey_files += grey

                if black is not None:
                    black_files += black

            grey_files = list(set(grey_files))
            black_files = list(set(black_files))
            print()
            print(grey_files)
            print(black_files)

            print('finished updating black and grey lists')
            clean_files = []     
            # once we get the complete black and grey lists we parse them to find the clean data
            if len(black_files) != 0:
                for hash_commits in name:
                    print('processing clean data')
                    clean_files += clean_data(repo, hash_commits, black_files, grey_files)
                    if len(clean_files) == len(black_files):
                        break
                    print('finished clean data')
                    print()
            
            # Removes the cloned repo from current directory
#shutil.rmtree(repo_dir)
            print()

        print('Script Complete|Runtime: {} Seconds'.format(time.time() - start_time))
            

    except Exception as e:
        print("Could not check if repo existed", e)
