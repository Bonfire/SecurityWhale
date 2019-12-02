using LibGit2Sharp;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PSV
{
    public partial class PSVForm : Form
    {
        // Our user's exclusion lists
        public string[] fileNameExclusions, fileExtensionExclusions, folderExclusions;
        public bool isRepoPrivate = false;

        // User Credential Manager
        public string githubUsername, githubPassword;


        public PSVForm()
        {
            InitializeComponent();
        }

        // Test to see if a Git URL is valid
        public bool IsGitURLValid()
        {
            try
            {
                if (!projectURLTextBox.Text.EndsWith(".git"))
                {
                    projectURLTextBox.Text = string.Concat(projectURLTextBox.Text, ".git");
                }

                // Try to fetch the remote references. This will tell us if the project is remote and being hosted
                System.Collections.Generic.IEnumerable<Reference> references = Repository.ListRemoteReferences(projectURLTextBox.Text);
                return true;
            }
            catch (Exception)
            {
                DialogResult messageBoxResult = MessageBox.Show("Git URL is not valid or could not be reached. Is it Private?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                // Fire up the GitHub authenticator form
                if (messageBoxResult == DialogResult.Yes)
                {
                    isRepoPrivate = true;
                    GitHubLogInForm gitHubLoginForm = new GitHubLogInForm(projectURLTextBox.Text, pathToCloneTextBox.Text);
                    gitHubLoginForm.ShowDialog();
                    githubUsername = gitHubLoginForm.GetUsername();
                    githubPassword = gitHubLoginForm.GetPassword();

                    gitHubLoginForm.Dispose();
                }

                return false;
            }
        }

        // See if the path is valid by verifying that it exists
        public bool IsPathValid()
        {
            return Directory.Exists(pathToCloneTextBox.Text);
        }

        // Open the project in the application (this is NOT the scan)
        private void CloneProjectButton_Click(object sender, EventArgs e)
        {
            cloneProject();
        }

        public void cloneProject()
        {
            // Perform our two validation checks
            if (!IsPathValid())
            {
                MessageBox.Show("Folder in path is not empty or does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Clone the repo if it isn't private (already cloned)
            if (IsGitURLValid() && !isRepoPrivate)
            {
                // Extract the repo name from the URL, then combine it with the path to make the full path
                string[] repoSplit = projectURLTextBox.Text.Trim().Split('/');
                string repoName = repoSplit[repoSplit.Length - 1].Split('.')[0];
                string combinedPath = Path.Combine(pathToCloneTextBox.Text, repoName);

                Repository.Clone(projectURLTextBox.Text, combinedPath);

                new Thread(new ThreadStart(delegate
                {
                    MessageBox.Show("Repository cloned successfully! You may now load the project.", "Cloned Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                })).Start();
            }
        }

        // Used to create nodes and add them to the TreeView
        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                // Use LINQ to see if the folder is excluded by name
                if (!folderExclusions.Contains(directory.Name) && !directory.Name.Contains("_git"))
                {
                    // Check to see if the folder is hidden and add it if the 'include hidden files' setting is checked
                    if (directory.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        if (!includeHiddenCheck.Checked)
                        {
                            continue;
                        }
                    }

                    // If we made it this far, add the folder
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));


                }
            foreach (FileInfo file in directoryInfo.GetFiles())
                // If the file isnt excluded by name
                if (!fileNameExclusions.Contains(file.Name))
                {
                    // If the file has an extension, check extension exclusions
                    // If the file is excluded by extension, skip it
                    if (!Path.GetExtension(file.FullName).Equals(String.Empty))
                    {
                        if (fileExtensionExclusions.Contains(Path.GetExtension(file.FullName)))
                        {
                            continue;
                        }
                    }

                    // If the file has a 'hidden file' attribute, check the 'include hidden files setting'
                    if (file.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        if (!includeHiddenCheck.Checked)
                        {
                            continue;
                        }
                    }

                    // If we made it this far without skipping the file, add it
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
                }
            return directoryNode;
        }

        // Open the FolderBrowserDialog so the user can choose where to clone their repo to
        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult openFolderDialogResult = folderBrowserDialog.ShowDialog();
            if (openFolderDialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                pathToCloneTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void LoadProjectButton_Click(object sender, EventArgs e)
        {
            loadProject();
        }

        public void loadProject()
        {
            // Update our scan settings and exclusions
            UpdateScanSettings();

            // Clear the current file tree
            scanTree.Nodes.Clear();

            // Add all new folders and files to the file tree
            DirectoryInfo rootDirectoryInfo;
            if (localRadio.Checked)
            {
                rootDirectoryInfo = new DirectoryInfo(localPathTextbox.Text);
            }
            else
            {
                // Extract the repo name from the URL, then combine it with the path to make the full path
                string[] repoSplit = projectURLTextBox.Text.Trim().Split('/');
                string repoName = repoSplit[repoSplit.Length - 1].Split('.')[0];
                string combinedPath = Path.Combine(pathToCloneTextBox.Text, repoName);

                rootDirectoryInfo = new DirectoryInfo(combinedPath);
            }
            scanTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));

            // Expand the root (top) node to show the user that it loaded properly
            // scanTree.TopNode.Expand();

            // Allow the user to scan
            beginScanButton.Enabled = true;
        }

        private void PSVForm_Load(object sender, EventArgs e)
        {
            includeHiddenItemsToolTip.SetToolTip(includeHiddenCheck, "Includes files/folders that may be hidden when creating the file tree");
            fileNamesToolTip.SetToolTip(fileNamesBox, "Exclude files by name, comma separated (e.g. \"File1.txt\", \"File2.exe\")");
            fileExtensionToolTip.SetToolTip(fileExtensionsBox, "Exclude files by extension, comma separated (e.g. \".txt\", \".exe\")");
            foldersToolTip.SetToolTip(foldersBox, "Exclude folders by name, comma separated (e.g. \"Folder1\", \"Folder2\")");        }

        private void RemoteRadio_CheckedChanged(object sender, EventArgs e)
        {
            // Nasty, but works. Not much work on the GUIs side either so it'll stay for now.

            if (remoteRadio.Checked)
            {
                // Disable the local stuff
                localPathTextbox.Enabled = false;
                localBrowserButton.Enabled = false;

                // Enable the remote stuff
                projectURLTextBox.Enabled = true;
                pathToCloneTextBox.Enabled = true;
                openFolderButton.Enabled = true;
                cloneProjectButton.Enabled = true;
            }
            else
            {
                // Disable the local stuff
                localPathTextbox.Enabled = true;
                localBrowserButton.Enabled = true;

                // Enable the remote stuff
                projectURLTextBox.Enabled = false;
                pathToCloneTextBox.Enabled = false;
                openFolderButton.Enabled = false;
                cloneProjectButton.Enabled = false;
            }

        }

        private void aboutHeaderLabel_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void aboutDevelopersRTB_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void localBrowserButton_Click(object sender, EventArgs e)
        {
            DialogResult openFolderDialogResult = folderBrowserDialog.ShowDialog();
            if (openFolderDialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                localPathTextbox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        // Begin the scan by calling the data script which calls the ML predictor
        private void BeginScanButton_Click(object sender, EventArgs e)
        {
            beginScan();
        }

        public void beginScan()
        {
            // Setup the base for our python environment and scripts
            string pythonInterpreter = "python";
            string pythonScript = @"..\..\..\..\Backend\app_interface.py";
            ProcessStartInfo pythonStartInfo;

            // If we're working with a local repo
            if (localRadio.Checked)
            {
                pythonStartInfo = new ProcessStartInfo
                {
                    FileName = pythonInterpreter,
                    // Use the localPathTextbox here because the repo is already cloned
                    Arguments = string.Format("{0} {1}", pythonScript, localPathTextbox.Text),
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };
            }
            // Else we're working with a remote repo
            else
            {
                string repoURL = projectURLTextBox.Text;
                string[] splitRepoURL = repoURL.Split('/');

                // This is in the format of "Bonfire/PSV"
                string repoName = splitRepoURL[splitRepoURL.Length - 2] + "/" + splitRepoURL[splitRepoURL.Length - 1].Split('.')[0];

                // Get the repo path from the repo name
                string repoPath = repoName.Split('/')[1];
                string combinedPath = Path.Combine(pathToCloneTextBox.Text, repoPath);

                // If we're working with a non-private, remote repo by checking if the username/password are set
                // Supply the path that the repo is located, and the name of the repo
                if (String.IsNullOrEmpty(githubUsername) || String.IsNullOrEmpty(githubPassword))
                {
                    pythonStartInfo = new ProcessStartInfo
                    {
                        FileName = pythonInterpreter,
                        Arguments = string.Format("{0} {1} {2}", pythonScript, combinedPath, repoName),
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    };
                }
                // Else we have a private, remote repo, so supply the path, the name, username, and password
                else
                {
                    pythonStartInfo = new ProcessStartInfo
                    {
                        FileName = pythonInterpreter,
                        Arguments = string.Format("{0} {1} {2} {3} {4}", pythonScript, combinedPath, repoName, githubUsername, githubPassword),
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    };
                }
            }

                        faultChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Fault Likelihood",
                    Values = new ChartValues<double> { }
                }
            };

            NumberFormatInfo percentageFormat = new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 };
            faultChart.AxisY.Add(new Axis
            {
                Title = "Fault Likelihood",
                LabelFormatter = value => value.ToString("P2", percentageFormat)
            });


            Process pythonProcess = Process.Start(pythonStartInfo);
            StreamReader pythonOutStream = pythonProcess.StandardOutput;

            // Wait until the scan is finished
            pythonProcess.WaitForExit();


            List<string> fileNameList = new List<string>();
            
            // Remove the single-quotes and split the string
            StringReader outputReader = new StringReader(pythonOutStream.ReadToEnd());
            string outputLine;
            while ((outputLine = outputReader.ReadLine()) != null)
            {
                if (!outputLine.Contains("Using TensorFlow backend."))
                {
                    string[] splitResult = outputLine.Trim().Split(',');
                    string fileName = splitResult[0];
                    double faultProbability = double.Parse(splitResult[1]);

                    string[] newRow = { fileName, splitResult[1] };
                    ListViewItem newItem = new ListViewItem(newRow);
                    faultListView.Items.Add(newItem);

                    // Add the value to our fault chart
                    faultChart.Series[0].Values.Add(faultProbability);
                    fileNameList.Add(fileName);
                }
            }

            faultChart.AxisX.Add(new Axis
            {
                Title = "File Names",
                Labels = fileNameList.ToArray(),
                LabelsRotation = 15,
            });;

            faultListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            faultListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            outputReader.Dispose();

            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show("Project scan complete! You may view the results under the \"View\" tab.", "Scan Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

    // Update our scan/exclusion settings set by the user
    public void UpdateScanSettings()
        {
            // Our scan exclusions
            fileNameExclusions = fileNamesBox.Text.Split(',');
            fileExtensionExclusions = fileExtensionsBox.Text.Split(',');
            folderExclusions = foldersBox.Text.Split(',');
        }
    }
}
