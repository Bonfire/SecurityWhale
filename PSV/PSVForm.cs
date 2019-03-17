using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.WinForms; //the WinForm wrappers
using System;
using System.Windows.Forms;
using Octokit;
using LibGit2Sharp;
using System.Collections.Generic;
using System.IO;

namespace PSV
{
	public partial class PSVForm : Form
	{
		public PSVForm()
		{
			InitializeComponent();
		}

		private void PSVForm_Load(object sender, EventArgs e)
		{

		}

		private void openProjectButton_Click(object sender, EventArgs e)
		{
			LibGit2Sharp.Repository.Clone(projectURLTextBox.Text, pathToCloneTextBox.Text);

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
