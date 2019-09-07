using LibGit2Sharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PSV
{
    public partial class PSVForm : Form
    {
        public String[] fileNameExclusions, fileExtensionExclusions, folderExclusions;

        public PSVForm()
        {
            InitializeComponent();
        }

        public bool IsGitURLValid()
        {
            try
            {
                if (!projectURLTextBox.Text.EndsWith(".git"))
                {
                    projectURLTextBox.Text = string.Concat(projectURLTextBox.Text, ".git");
                }

                System.Collections.Generic.IEnumerable<Reference> references = Repository.ListRemoteReferences(projectURLTextBox.Text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsPathValid()
        {
            string[] files = Directory.GetFiles(pathToCloneTextBox.Text);
            return files.Length == 0 ? true : false;
        }

        private void OpenProjectButton_Click(object sender, EventArgs e)
        {
            if (!IsPathValid())
            {
                MessageBox.Show("Folder in path is not empty or does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsGitURLValid())
            {
                DialogResult messageBoxResult = MessageBox.Show("Git URL is not valid. Is it Private?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (messageBoxResult == DialogResult.Yes)
                {
                    GitHubLogInForm gitHubLoginForm = new GitHubLogInForm();
                    gitHubLoginForm.Show();
                }

                return;
            }

            // Clone the repo
            Repository.Clone(projectURLTextBox.Text, pathToCloneTextBox.Text);

            // Update our scan settings and exclusions
            updateScanSettings();

            // Clear the current file tree
            scanTree.Nodes.Clear();

            // Add all new folders and files to the file tree
            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(pathToCloneTextBox.Text);
            scanTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));

            // Allow the user to scan
            beginScanButton.Enabled = true;
        }

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

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult openFolderDialogResult = folderBrowserDialog.ShowDialog();
            if (openFolderDialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                pathToCloneTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void TestURLButton_Click(object sender, EventArgs e)
        {
            if (IsGitURLValid())
            {
                openProjectButton.Enabled = true;
            }
            else
            {
                DialogResult messageBoxResult = MessageBox.Show("Git URL is not valid. Is it Private?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (messageBoxResult == DialogResult.Yes)
                {
                    GitHubLogInForm gitHubLoginForm = new GitHubLogInForm();
                    gitHubLoginForm.Show();
                }
            }
        }

        private void BeginScanButton_Click(object sender, EventArgs e)
        {
            string pythonInterpreter = "python.exe";
            string pythonScript = "dataScript.py";
            bool sendData = provideDataBox.Checked;

            ProcessStartInfo pythonStartInfo = new ProcessStartInfo(pythonInterpreter)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = pythonScript + " " + sendData.ToString() // Arguments passed to the script (True or False)
            };

            Process pythonProcess = new Process
            {
                StartInfo = pythonStartInfo
            };
            pythonProcess.Start();

            StreamReader outputReader = pythonProcess.StandardOutput;
            string outputString = outputReader.ReadLine();

            MessageBox.Show(outputString);
        }

        public void updateScanSettings()
        {
            // Our scan exclusions
            fileNameExclusions = fileNamesBox.Text.Split(',');
            fileExtensionExclusions = fileExtensionsBox.Text.Split(',');
            folderExclusions = foldersBox.Text.Split(',');
        }
    }
}
