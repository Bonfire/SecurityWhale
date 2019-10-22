import time
from os import walk

# error exceptions and API/repo access
import mysql.connector
import numpy as np

from mysql.connector import errorcode
from worker import *

# gain access to important data easily
from config import database
from config import git_access
from config import host
from config import password
from config import user

# globals
# increment through database
# repo_id = 1

# TODO: This will no longer work with new implementation needs revision
def features_array(git_repo, repository, repository_name, repository_dir):
    """
    combines data points and creates a numpy array
    :param git_repo: gitpython repo object to get data from api
    :param repository: pygithub object used to get data from api
    :param repository_name: name of repo on github
    :param repository_dir: location of cloned repo
    :return:
    """
    repo_data = list(repository_data(git_repo, repository, repository_name, repository_dir))

    features = []

    # Populates features with sub-arrays containing [filename, file_data[], repo_data[]]
    for key in averages:
        features.append([key, averages[key], repo_data])

    # Converts to a numpy array before returning
    return np.array(features)


def dirty_data(repository, commit_hash):
    """
    Dirty Data takes a given commit_hash and parses information from it which is then stored in the database
    it also stores the filename in a list that is stored globally

    :param repository: full repository object
    :param commit_hash: a hash of a specific commit in a repository
    :return:
    """
    global repo_id
    averages = []
    dirty_filenames = []
    index = 0
    # these commits are known faults so flags will always be 1
    flag = 1

    # get the data on a specific hash from the given repo
    commit = repository.commit(commit_hash)

    black_list = parse_dic(commit.stats.files)

    if len(black_list) != 1:
        return black_list, None

    # use totals to find averages
    commit_size = commit.size

    averages = get_averages(black_list, commit_hash)

    for key in averages:
        dirty_filenames.append(key[0])
        filename = key[0]
        total_ins = key[1]
        ins_avg = key[2]
        total_del = key[3]
        del_avg = key[4]
        total_lines = key[5]
        lines_avg = key[6]
        commit_size = key[7]

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

    return None, dirty_filenames


def clean_data(repository, commit_hash):
    """
    Clean data looks through a repository history up until the given commit hash to find another file
    with the same extension. It strips the filename for the extension then checks to see it another filename
    matches that extension in the commit history. once found parse that commit to database as clean data
    :param repository:
    :param commit_hash:
    :return:
    """
    # data = repository.iter_commits(commit_hash) This gets all commits up to given hash
    file_holder = []
    # we know these files dont have any faults
    flag = 0
    index = 0
    check = 0

    # since iter_commits does no reset the list each time its used in a loop we need 2 variable  one for each for loop
    commit_log = repository.iter_commits(commit_hash)
    commit_logx = repository.iter_commits(commit_hash)
    commit_data = repository.commit(commit_hash)

    # grab the extension of the dirty file
    for path in commit_data.stats.files:
        dirty_ext = path.split(".")[-1]


    # go through commit history up to dirty file commit and store all files with the same extension as the
    # dirty_ext into a list
    for commit in commit_log:
        for path in commit.stats.files:
            if path.split(".")[-1] == dirty_ext:
                file_holder.append(path)

    # grab the last file in the list and run through the commit log again, once the filenames match parse that
    # commit and store in database
    for commit in commit_logx:
        for path in commit.stats.files:
            # if files match confirm and breka second loop
            if path == file_holder[-1]:
                check = 1
                break
        # check confirmation and grab that files commit object and breka other wise continue first loop 
        if check == 1:
            # commit_file = commit.stats.files
            # TODO: check if this code checks duplicates we dont want the same bad file to be marked as good 
            '''for path in commit_file:
                if path in dirty_filenames:
                    break
                else:
                    continue
            '''
            break
        else:
            continue

    # Parse all the information form the Good data and store in database
    parse_dic(commit.stats.files)

    # use totals to find averages
    commit_size = commit.size
    for key in black_list:
        key_val = black_list[key]
        tot_ins = key_val['ins_total']
        tot_del = key_val['del_total']
        tot_lin = key_val['lin_total']
        count = key_val['count']

        averages.append([key, tot_ins, tot_ins / count, tot_del, tot_del / count, tot_lin, tot_lin / count,
                         commit_size])

        index += 1

    for key in averages:
        # TODO: doesn't make sense to have this, better to do check as implemented as above but then again i could be
        #  wrong lol
        clean_filenames.append(key[0])
        filename = key[0]
        total_ins = key[1]
        ins_avg = key[2]
        total_del = key[3]
        del_avg = key[4]
        total_lines = key[5]
        lines_avg = key[6]
        commit_size = key[7]

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

    # clean up lists for next commit/ repo
    averages.clear()


if __name__ == "__main__":
    try:
        start_time = time.time()
        git = access_github()

        # temp = get_repos()

        # THIS IS FOR TESTING ONLY AS TO NOT CLONE A BILLION FILES CONSTANTLY
        temp = [['bbengfort/confire', '8cc86a5ec2327e070f1d576d61bbaadf861597ea']]
        # ['jcupitt/libvips', '20d840e6da15c1574b3ed998bc92f91d1e36c2a5'],
        # ['bratsche/pango', '4de30e5500eaeb49f4bf0b7a07f718e149a2ed5e'],
        # ['pornel/pngquant', 'b7c217680cda02dddced245d237ebe8c383be285'],
        # ['hapijs/hapi', 'aab2496e930dce5ee1ab28eecec94e0e45f03580'],
        # ['SickRage/SickRage', '8156a74a68aea930d1e1047baba8b115c3abfc44'],
        # ['reubenhwk/radvd', '92e22ca23e52066da2258df8c76a2dca8a428bcc'],
        # ['NixOS/nixpkgs', '6c59d851e2967410cc8fb6ba3f374b1d3efa988e']]

        for name in temp:
            repo_dir = clone_repo(name)
            repo, _ = repo_get(name[0], git, repo_dir)

            # remove repo name from list so we only have the commit hashes to look at
            name.remove(name[0])

            # PRINT DEBUGGING
            print('Finished storing repo data')

            grey_dict = {}
            black_dict = {}

            for hash_commits in name:

                grey, black = dirty_data(repo, hash_commits)

                if grey is not None:
                    grey_dict.update(grey)

                if black is not None:
                    black_dict.update(black)
        #
        #         # PRINT DEBUGGING
        #         print('Finished dirty data')
        #
        #         clean_data(repo, hash_commits)
        #
        #         # PRINT DEBUGGING
        #         print('Finished clean data')

        print('Script Complete|Runtime: {} Seconds'.format(time.time() - start_time))
    except Exception as e:
        print("Could not check if repo existed", e)
