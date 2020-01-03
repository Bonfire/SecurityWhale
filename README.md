# SecurityWhale
Predicting Security Vulnerabilities using Code Metrics and Machine Learning. This project was created for the [University of Central Florida's Computer Science Senior Design](http://www.eecs.ucf.edu/cssd/) class for Spring 2019 to Fall 2019.

Documents detailing the project including our Design Document, Conference Paper, and Final Committee Presentation can be viewed under the "Docs" folder and are available for download.

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
  
## Application:
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
  
# Results

We were able to make predictions as to where security faults may lie in projects with a 60% accuracy. On a per-file basis, our machine learning model with data pipeline backend can correctly predict that a file contains a security fault six-out-of-ten times. This is better than a coin flip and can be used by QA testers or software engineers to obtain a general overview as to where they should focus their security vulnerability finding efforts. We argue that our project should be used in a manner that works in-tandem with current programs and processes that aid in finding vulnerabilities in code and should not act as a replacement. By pairing our program with current vulnerability finding processes, developers can glean more information regarding the overall security posture of their projects. This increase in the amount of information at the developer’s disposal can only beneficial and it is our goal that this helps developers in securing their projects.

# Conclusions

The most important – and most difficult – portion of this project was collecting features from repositories for use in the machine learning. The greatest barrier to any machine learning project is good data, and after examining available avenues for file-specific features, we were still left wanting to investigate further, either by gathering new features or performing additional analysis and discarding features that weren’t as useful to model accuracy. Additionally, much of the time spent working on this project was spent setting up algorithms to process and format these features and assembling the pipeline that connects each individual part of the project. However, once these tools were assembled, progress proceeded at a much quicker rate. Being able to test every piece of the project in order greatly accelerated development, but it also created new difficulties due to having to debug a much larger program. Given more development and refinement time, we believe the accuracy of our product would be able to provide much more accurate predictions. This project has demonstrated that fault detection through file metrics is possible, but this shows how much can still be done in this area to hone in on a higher prediction accuracy. Ultimately, the team was able to answer the question “is it possible to make accurate predictions for security fault likelihoods using code metrics and machine learning?” We found that yes, it is possible to make accurate predictions as to where security faults lie in git repos, and that by utilizing our program, one can gain insight into a high-level directional overview as to where faults are likely to occur.

# Existing Work
* Predicting the Location and Number of Faults in Large Software Systems:
  * Thomas J. Ostrand, Elaine J. Weyuker, and Robert M. Bell
* Negative binomial regression model that predicts the number of faults in a file
* Predictions based on code faults and modification history of previous releases
* Applied to 2 large industrial systems at AT&T:
  * The top 20% of predicted problematic files contained between 71% and 92% of the faults that were actually detected, with the overall average being 83%
* Showed it is possible to predict faults with intensive efforts to map faults with metadata

# Authors
[Baran Barut](https://github.com/Bonfire) - Front-End Application, Testing Plan

[Michael Harris](https://github.com/mmph87) - Project Manager, Backend

[Curtis Helsel](https://github.com/curtishelsel) - Machine Learning

[Kyle Reid](https://github.com/kyReid) - Data Acquisition

[Thomas Serrano](https://github.com/TomSerrano) - Data Acquisition, Application-Backend Interface

# Acknowledgement
The authors wish to acknowledge the assistance and support of their advisor, Dr. Paul Gazzillo, and the contributions and expertise of Dr. Elaine Weyuker. As well as the continued support from their Senior Design professor, Dr. Mark Heinrich.
