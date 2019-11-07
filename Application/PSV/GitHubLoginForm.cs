using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Microsoft.Alm.Authentication;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSV
{
    public partial class GitHubLogInForm : Form
    {
        public Credentials gitHubCredentials, githubUsername, githubPassword;
        public String projectURL, pathToClone;

        public GitHubLogInForm(String projectURL, String pathToClone)
        {
            this.projectURL = projectURL;
            this.pathToClone = pathToClone;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void LogInButton_Click(object sender, EventArgs e)
        {
            gitHubCredentials = new UsernamePasswordCredentials()
            {
                Username = gitHubUsernameTextBox.Text,
                Password = gitHubPasswordTextBox.Text
            };


            // Setup GitHub authentication
            SecretStore gitSecretStore = new SecretStore("git");
            BasicAuthentication authType = new BasicAuthentication(gitSecretStore);
            Credential userCredentials = authType.GetCredentials(new TargetUri("https://github.com"));
            CloneOptions credCloneOptions = new CloneOptions
            {
                OnTransferProgress = cloneProgress =>
                {
                    var clonePercentage = (100 * cloneProgress.ReceivedObjects) / cloneProgress.TotalObjects;
                    cloneProgressBar.Invoke(new Action(() => cloneProgressBar.Value = clonePercentage));
                    return true;
                },
                CredentialsProvider = (_url, _user, _cred) => gitHubCredentials,
            };

            // Try to authenticate, notify the user if there's an error
            try
            {
                // Clone the repo and close the form
                // We also want to nullify all credential objects to respect the user's privacy
                // This should make the memory "out of scope" in the eyes of the garbage collector

                // Extract the repo name from the URL, then combine it with the path to make the full path
                string[] repoSplit = projectURL.Trim().Split('/');
                string repoName = repoSplit[repoSplit.Length - 1].Split('.')[0];
                string combinedPath = Path.Combine(pathToClone, repoName);


                Repository.Clone(projectURL, combinedPath, credCloneOptions);

                userCredentials = null;
                credCloneOptions = null;

                new Thread(new ThreadStart(delegate
                {
                    MessageBox.Show("Repository cloned successfully! Closing login form.", "Cloned Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                })).Start();

                this.Close();
            }
            catch (LibGit2SharpException)
            {
                new Thread(new ThreadStart(delegate
                {
                    MessageBox.Show("Failed to clone the repo. Please check your internet connection or credentials.", "Cloning Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                })).Start();
            }
        }

        public string GetUsername()
        {
            return gitHubUsernameTextBox.Text; 
        }

        public string GetPassword()
        {
            return gitHubPasswordTextBox.Text;
        }
    }
}
