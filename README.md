# SecurityWhale
Predicting Security Vulnerabilities using Code Metrics and Machine Learning

# Project Summary
Code is inherently insecure, with both security vulnerabilities and other faults in production systems decreasing the reliability and performance of code that runs the modern world. Our project connects a world-class front-end Application, driven by a machine learning backend that has been trained on publicly available metadata about the development cycle, in order to allow developers to make accurate predictions about where to find potential code faults within a software package. This will allow developers, both small and large, to be informed on where problematic areas in their software projects are likely to occur. Resolving issues before they are released using this tool will save time and money.

To wit, we utilize cutting-edge machine learning technology for our statistical model, expanding on existing work in the field by a contributor, Dr. Elaine Weyuker. Key to the project is comprehensive data acquisition methods and custom utilities to ensure our model can operate at maximum efficiency. Our product requires a robust front-end to interface with the end-user and communicate results in a meaningful way about how to improve the development cycle. Finally, a flexible backend infrastructure is necessary to reliably deliver the data required at each step, in addition to accommodating shifting requirements at an early stage in the project.

# Project Objectives
* Predict security vulnerabilities within code using code metrics from public GitHub repos and the project’s file structure 
* Provide users with informative security analytics regarding their code
* Improve upon existing work in the field by utilizing:
  * Machine Learning statistical modeling
  * Data acquisition using publicly available APIs and file metadata
* Determine feasibility of using metrics from development cycle to predict vulnerabilities and faults

# Technologies Used
## Data Acquisition:
* Data Source:
  * GitHub
  * CVE Database
* Programming Language:
  * Python (v3.7)
* Libraries Used:
  * PyGitHub
  * GitPython
  * OS
  * MySQL.Connector
* Integrated Development Environment (IDE):
  * PyCharm

## Backend:
* Ubuntu VM hosted by Digital Ocean:
  * MySQL Database to connect Data, Modeling roles
    * Local database connections through mysql.connector
   * Bootstrap website running on Apache, SSL cert through Let’s Encrypt
  * Anaconda - Python Sandboxing

## Machine Learning:
* Programming Languages:
  * Python
* Libraries:
  * Numpy
  * Keras
  * Tensorflow
  * Scikit Learn
  
 # Application:
 * Programming Languages:
  * C#
 * Libraries:
  * LibGit2Sharp
  * OctoKit
  * LiveCharts
 * CI/CD:
  * Travis CI
* Integrated Development Environment (IDE):
  * Visual Studio 2019
* Testing Framework:
  * Microsoft Unit Testing Framework for Managed Code
  * Arrange, Act, Assert testing paradigm
  * Six unit tests
  * Two integration tests

# Existing Work
* Predicting the Location and Number of Faults in Large Software Systems:
  * Thomas J. Ostrand, Elaine J. Weyuker, and Robert M. Bell
* Negative binomial regression model that predicts the number of faults in a file
* Predictions based on code faults and modification history of previous releases
* Applied to 2 large industrial systems at AT&T:
  * The top 20% of predicted problematic files contained between 71% and 92% of the faults that were actually detected, with the overall average being 83%
* Showed it is possible to predict faults with intensive efforts to map faults with metadata



# Contributors
Kyle Reid - https://github.com/kyReid

Baran Barut - https://github.com/Bonfire

Michael Harris - https://github.com/mmph87

Curtis Helsel - https://github.com/curtishelsel

Thomas Serrano - https://github.com/TomSerrano
