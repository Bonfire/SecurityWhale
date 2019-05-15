"""This Module Handles connecting to the database. This will allow us to use the conn variable
to execute code in out main script"""
import mysql.connector
from mysql.connector import errorcode

# connect to MySQL database, else close connection
try:
    conn = mysql.connector.connect(user='kyle', host='157.230.57.38',
                                   password='&9h+qn4?pu2D7E6JeTeDp-87TKYjvrd+VRU#4jQgGSzxAju=Ld?ctmZc6Qw#N6Wy',
                                   database='api')
    cursor = conn.cursor()
    print("Connected to MySQL Database")
except mysql.connector.Error as err:
    if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
        print("Something is wrong with your user name or password")
    elif err.errno == errorcode.ER_BAD_DB_ERROR:
        print("Database does not exist")
    else:
        print(err)
else:
    conn.close()

