using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSV
{
    public partial class GitHubLogInForm : Form
    {
        public GitHubLogInForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            LibGit2Sharp.Credentials gitHubCredentials = new UsernamePasswordCredentials()
            {
                Username = gitHubUsernameTextBox.Text,
                Password = gitHubPasswordTextBox.Text
            };
        }
    }
}
