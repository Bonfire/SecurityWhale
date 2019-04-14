﻿namespace PSV
{
    partial class GitHubLogInForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitHubLogInForm));
            this.gitHubUsernameLabel = new System.Windows.Forms.Label();
            this.gitHubPasswordLabel = new System.Windows.Forms.Label();
            this.gitHubUsernameTextBox = new System.Windows.Forms.TextBox();
            this.gitHubPasswordTextBox = new System.Windows.Forms.TextBox();
            this.logInButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gitHubUsernameLabel
            // 
            this.gitHubUsernameLabel.AutoSize = true;
            this.gitHubUsernameLabel.Location = new System.Drawing.Point(12, 9);
            this.gitHubUsernameLabel.Name = "gitHubUsernameLabel";
            this.gitHubUsernameLabel.Size = new System.Drawing.Size(94, 13);
            this.gitHubUsernameLabel.TabIndex = 0;
            this.gitHubUsernameLabel.Text = "GitHub Username:";
            // 
            // gitHubPasswordLabel
            // 
            this.gitHubPasswordLabel.AutoSize = true;
            this.gitHubPasswordLabel.Location = new System.Drawing.Point(14, 35);
            this.gitHubPasswordLabel.Name = "gitHubPasswordLabel";
            this.gitHubPasswordLabel.Size = new System.Drawing.Size(92, 13);
            this.gitHubPasswordLabel.TabIndex = 1;
            this.gitHubPasswordLabel.Text = "GitHub Password:";
            // 
            // gitHubUsernameTextBox
            // 
            this.gitHubUsernameTextBox.Location = new System.Drawing.Point(112, 6);
            this.gitHubUsernameTextBox.Name = "gitHubUsernameTextBox";
            this.gitHubUsernameTextBox.Size = new System.Drawing.Size(260, 20);
            this.gitHubUsernameTextBox.TabIndex = 2;
            // 
            // gitHubPasswordTextBox
            // 
            this.gitHubPasswordTextBox.Location = new System.Drawing.Point(112, 32);
            this.gitHubPasswordTextBox.Name = "gitHubPasswordTextBox";
            this.gitHubPasswordTextBox.Size = new System.Drawing.Size(260, 20);
            this.gitHubPasswordTextBox.TabIndex = 3;
            this.gitHubPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // logInButton
            // 
            this.logInButton.Location = new System.Drawing.Point(216, 76);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(75, 23);
            this.logInButton.TabIndex = 4;
            this.logInButton.Text = "Log In";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.LogInButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(297, 76);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(14, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Your password is not saved";
            // 
            // GitHubLogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.gitHubPasswordTextBox);
            this.Controls.Add(this.gitHubUsernameTextBox);
            this.Controls.Add(this.gitHubPasswordLabel);
            this.Controls.Add(this.gitHubUsernameLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 150);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 150);
            this.Name = "GitHubLogInForm";
            this.Text = "GitHub Log In";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label gitHubUsernameLabel;
        private System.Windows.Forms.Label gitHubPasswordLabel;
        private System.Windows.Forms.TextBox gitHubUsernameTextBox;
        private System.Windows.Forms.TextBox gitHubPasswordTextBox;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label3;
    }
}