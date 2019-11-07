using LibGit2Sharp;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PSV
{
    public partial class PSVForm : Form
    {
        // Our user's exclusion lists
        public String[] fileNameExclusions, fileExtensionExclusions, folderExclusions;
        public Boolean isRepoPrivate = false;

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
                }

                return false;
            }
        }

        // See if the path is valid by verifying that it exists and there are no files in it
        public bool IsPathValid()
        {
            string[] files = Directory.GetFiles(pathToCloneTextBox.Text);
            return files.Length == 0 ? true : false;
        }

        // Open the project in the application (this is NOT the scan)
        private void CloneProjectButton_Click(object sender, EventArgs e)
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
                Repository.Clone(projectURLTextBox.Text, pathToCloneTextBox.Text);
            }
        }

        // Used to create nodes and add them to the TreeView
        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                // Use LINQ to see if the folder is excluded by name
                if (!folderExclusions.Contains(directory.Name))
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
                rootDirectoryInfo = new DirectoryInfo(pathToCloneTextBox.Text);
            }
            scanTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));

            // Expand the root (top) node to show the user that it loaded properly
            scanTree.TopNode.Expand();

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

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void richTextBox1_LinkClicked_1(object sender, LinkClickedEventArgs e)
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

            faultListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            faultListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Setup the base for our python environment and scripts
            string pythonInterpreter = "python";
            string pythonScript = @"..\..\..\..\Backend\app_interface.py";
            string repoURL = projectURLTextBox.Text;
            string[] splitRepoURL = repoURL.Split('/');
            string repoName = splitRepoURL[splitRepoURL.Count() - 2] + "/" + splitRepoURL[splitRepoURL.Count() - 1].Split('.')[0];

            ProcessStartInfo pythonStartInfo = new ProcessStartInfo
            {
                FileName = pythonInterpreter,
                Arguments = string.Format("{0} {1} {2} {3} {4}", pythonScript, githubUsername, githubPassword, repoName, pathToCloneTextBox.Text),
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            Process pythonProcess = Process.Start(pythonStartInfo);
            StreamReader pythonStream = pythonProcess.StandardOutput;

            // Wait until the scan is finished
            pythonProcess.WaitForExit();

            faultChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Fault Probabilities",
                    Values = new ChartValues<double> { }
                }
            };

            // Remove the single-quotes and split the string
            StringReader outputReader = new StringReader(pythonStream.ReadToEnd());
            string outputLine;
            while((outputLine = outputReader.ReadLine()) != null)
            {
                string[] splitResult = outputLine.Trim('\'').Split(Environment.NewLine.ToCharArray());
                string fileName = splitResult[0];
                double faultProbability = Double.Parse(splitResult[1]);

                if (faultProbability >= 0.5)
                {
                    string[] newRow = { fileName, splitResult[1] };
                    ListViewItem newItem = new ListViewItem(newRow);
                    faultListView.Items.Add(newItem);

                    // Add the value to our fault chart
                    faultChart.Series[0].Values.Add(faultProbability);
                }
            }

            Console.WriteLine("Output: " + pythonStream.ReadToEnd());
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
