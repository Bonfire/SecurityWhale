using System;
using System.Windows.Forms;
using System.IO;
using LibGit2Sharp;

namespace PSV
{
	public partial class PSVForm : Form
	{
		public PSVForm()
		{
			InitializeComponent();
		}

		private void openProjectButton_Click(object sender, EventArgs e)
		{
			Repository.Clone(projectURLTextBox.Text, pathToCloneTextBox.Text);

			var rootDirectoryInfo = new DirectoryInfo(pathToCloneTextBox.Text);
			scanTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
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
	}
}
