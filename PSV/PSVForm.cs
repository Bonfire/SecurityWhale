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

		private Boolean IsGitURLValid()
		{
			try
			{
				System.Collections.Generic.IEnumerable<Reference> references = Repository.ListRemoteReferences(projectURLTextBox.Text);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private bool IsPathValid()
		{
			string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath);
			return files.Length == 0 ? true : false;
		}

		private void openProjectButton_Click(object sender, EventArgs e)
		{
			if (IsGitURLValid() && IsPathValid())
			{ 
				Repository.Clone(projectURLTextBox.Text, pathToCloneTextBox.Text);

				var rootDirectoryInfo = new DirectoryInfo(pathToCloneTextBox.Text);
				scanTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
			}

		}

		private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
		{
			var directoryNode = new TreeNode(directoryInfo.Name);
			foreach (var directory in directoryInfo.GetDirectories())
				directoryNode.Nodes.Add(CreateDirectoryNode(directory));
			foreach (var file in directoryInfo.GetFiles())
				directoryNode.Nodes.Add(new TreeNode(file.Name));
			return directoryNode;
		}

		private void openFolderButton_Click(object sender, EventArgs e)
		{
			DialogResult openFolderDialogResult = folderBrowserDialog.ShowDialog();
			if(openFolderDialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
			{
				pathToCloneTextBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void testURLButton_Click(object sender, EventArgs e)
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
