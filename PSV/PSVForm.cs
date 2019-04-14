using System;
using System.Windows.Forms;
using System.IO;
using LibGit2Sharp;
using System.Drawing;

namespace PSV
{
    public partial class PSVForm : Form
    {
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

            Repository.Clone(projectURLTextBox.Text, pathToCloneTextBox.Text, );

            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(pathToCloneTextBox.Text);
            scanTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));

        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (FileInfo file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name));
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
                testURLButton.ForeColor = Color.Green;
            }
            else
            {
                testURLButton.ForeColor = Color.Red;
            }
        }
    }
}
