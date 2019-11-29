using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
            testForm.projectURLTextBox.Text = "https://github.com/Bonfire/Non-Existent-Project.git";
            Assert.IsFalse(testForm.IsGitURLValid());
        }

        [TestMethod()]
        public void IsPathValidTest()
        {
            PSVForm testForm = new PSVForm();

            // Create a new path to test
            // Test that the path is valid (exists and non-empty)
            string testPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CloneTest\\");
            Debug.WriteLine(testPath);
            DirectoryInfo testDirectory = Directory.CreateDirectory(testPath);
            testForm.pathToCloneTextBox.Text = testDirectory.FullName;
            Assert.IsTrue(testForm.IsPathValid());

            // Delete the directory
            // Test that the non-existant directory is indeed non-existent
            testDirectory.Delete(true);
            Assert.IsFalse(testForm.IsPathValid());
        }

        [TestMethod()]
        public void AreScanSettingsValidTest()
        {
            PSVForm testForm = new PSVForm();

            // Test valid settings
            testForm.fileNamesBox.Text = "file1.txt, file2.c, file3.java";
            testForm.fileExtensionsBox.Text = ".cpp, .h, .bat";
            testForm.foldersBox.Text = "folder1, folder2, folder3";

            // Set the local path
            testForm.localRadio.Checked = true;
            testForm.localPathTextbox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Debug.WriteLine("Path: " + testForm.localPathTextbox.Text);

            testForm.UpdateScanSettings();

            // Verify settings
            Assert.IsTrue(string.Join(",", testForm.fileNameExclusions).Length == testForm.fileNamesBox.Text.Length);
            Assert.IsTrue(string.Join(",", testForm.fileExtensionExclusions).Length == testForm.fileExtensionsBox.Text.Length);
            Assert.IsTrue(string.Join(",", testForm.folderExclusions).Length == testForm.foldersBox.Text.Length);
        }

        [TestMethod()]
        public void BeginScanTest()
        {
            // TODO: Implement test method
        }
    }
}