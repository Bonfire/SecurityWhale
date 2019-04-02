using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSV;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSV.Tests
{
	[TestClass()]
	public class PSVFormTests
	{
		[TestMethod()]
		public void IsGitURLValidTest()
		{
			PSVForm testForm = new PSVForm();
			testForm.projectURLTextBox.Text = "https://github.com/Bonfire/Legionary.git";
			Assert.IsTrue(testForm.IsGitURLValid());
		}

		[TestMethod()]
		public void IsPathValidTest()
		{
			PSVForm testForm = new PSVForm();

			// Create a new path to test
			// Test that the path is valid (exists and non-empty)
			string testPath = Path.Combine(Directory.GetCurrentDirectory(), "CloneTest\\");
			Debug.WriteLine(testPath);
			DirectoryInfo testDirectory = Directory.CreateDirectory(testPath);
			testForm.pathToCloneTextBox.Text = testDirectory.FullName;
			Assert.IsTrue(testForm.IsPathValid());

			// Delete the directory
			// Test that the non-existant directory is invalid
			testDirectory.Delete(true);
			Assert.IsFalse(testForm.IsPathValid());
		}
	}
}