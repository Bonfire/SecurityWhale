using LibGit2Sharp;
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
            updateScanSettings();

            // Clear the current file tree
            scanTree.Nodes.Clear();

            // Add all new folders and files to the file tree
            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(pathToCloneTextBox.Text);
            scanTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));

            // Expand the root (top) node to show the user that it loaded properly
            scanTree.TopNode.Expand();

            // Allow the user to scan
            beginScanButton.Enabled = true;
        }

        private void PSVForm_Load(object sender, EventArgs e)
        {
            provideDataToolTip.SetToolTip(provideDataBox, "Send extracted metadata to our servers for future training");
            includeHiddenItemsToolTip.SetToolTip(includeHiddenCheck, "Includes files/folders that may be hidden when creating the file tree");
            fileNamesToolTip.SetToolTip(fileNamesBox, "Exclude files by name, comma separated (e.g. \"File1.txt\", \"File2.exe\")");
            fileExtensionToolTip.SetToolTip(fileExtensionsBox, "Exclude files by extension, comma separated (e.g. \".txt\", \".exe\")");
            foldersToolTip.SetToolTip(foldersBox, "Exclude folders by name, comma separated (e.g. \"Folder1\", \"Folder2\")");
        }

        // Begin the scan by calling the data script which calls the ML predictor
        private void BeginScanButton_Click(object sender, EventArgs e)
        {
            // Setup the base for our python environment and scripts
            string pythonInterpreter = "python.exe";
            string pythonScript = "dataScript.py";
            bool sendData = provideDataBox.Checked;

            // Python process info, includes silent running so it's not annoying to the user
            // Also, pass the "send data?" argument
            ProcessStartInfo pythonStartInfo = new ProcessStartInfo(pythonInterpreter)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = pythonScript + " " + sendData.ToString() // Arguments passed to the script (True or False)
            };

            // Create the new process with our previous process information
            Process pythonProcess = new Process
            {
                StartInfo = pythonStartInfo
            };
            pythonProcess.Start();

            // View the output from the script
            // TODO: Implement the output gathering from the script
            StreamReader outputReader = pythonProcess.StandardOutput;
            string outputString = outputReader.ReadLine();

            MessageBox.Show(outputString);
        }

        // Update our scan/exclusion settings set by the user
        public void updateScanSettings()
        {
            // Our scan exclusions
            fileNameExclusions = fileNamesBox.Text.Split(',');
            fileExtensionExclusions = fileExtensionsBox.Text.Split(',');
            folderExclusions = foldersBox.Text.Split(',');
        }
    }
}
