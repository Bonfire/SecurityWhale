# Security Whale Data Collection and Parsing Tool

This is a tool designed specifically for collecting and parsing data received through Githubs API and Github repositories. This tool also ties in with our Security Whale application which is a desktop application that clones a users repository and pulls and parses that repository locally and display whether or not that specific repo has security bugs in it.

## Motivation

Programs can have hundreds of files each containing over a thousand lines of code and it can be difficult at times to determine whether or not there might be security issues within the code. Using this Tool we can pull meta data from Github repositories
and run them through our Machine Learning Algorithm to determine the likelihood of there being a security issue in a specific file.

## Code Style

This Program uses PEP 8 as its standard

## Data Types

This program involves two different data points that are broken up into two main type. This first being Repository Data which focuses on data that relates to a github repository as a whole this includes number of contributors, stargazers, subscribers, issues and forks. This also includes total values of the commit log relating to lines changed such as addition and deletions.

The second data type is the file data, This focuses on meta data we can pull from the individual files in a github repository. Using githubs commit log history we were able to gather information such as lines edited as well as line count and the file size.

## How to Use

In order to run this program you will need access to the internet, Python 3+, git access token, and a MySQL database setup to hold the given data with that information stored in th econfig file

**Training**

In order for us to gather data for the training we had to have access to githubs API but also local copies of the repositories we wanted to train on. Once you have both those stored you can run the program


### Testing

N/A


## Resources

Data source: [Github Homepage](https://github.com/)

Programming Language: [Python](https://www.python.org/)

Database: [My SQL](https://www.mysql.com/)

### Contributors


**Baran Barut** - Application

**Michael Harris** - Database/ Project Manager

**Curtis Helsel** - Machine Learning

**Kyle Reid** - Data Collection Tool

**Thomas Serrano** - Data Parsing/ Machine Learning


### License

No License
