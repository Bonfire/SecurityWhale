namespace PSV
{
	partial class Form1
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.scanPage = new System.Windows.Forms.TabPage();
			this.viewPage = new System.Windows.Forms.TabPage();
			this.fileTree = new System.Windows.Forms.TreeView();
			this.openProjectButton = new System.Windows.Forms.Button();
			this.projectTextBox = new System.Windows.Forms.TextBox();
			this.projectLabel = new System.Windows.Forms.Label();
			this.settingsGroupBox = new System.Windows.Forms.GroupBox();
			this.scanProgressBar = new System.Windows.Forms.ProgressBar();
			this.beginScanButton = new System.Windows.Forms.Button();
			this.updateButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tabControl.SuspendLayout();
			this.scanPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.scanPage);
			this.tabControl.Controls.Add(this.viewPage);
			this.tabControl.Location = new System.Drawing.Point(12, 6);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(810, 455);
			this.tabControl.TabIndex = 0;
			// 
			// scanPage
			// 
			this.scanPage.Controls.Add(this.beginScanButton);
			this.scanPage.Controls.Add(this.scanProgressBar);
			this.scanPage.Controls.Add(this.settingsGroupBox);
			this.scanPage.Controls.Add(this.projectLabel);
			this.scanPage.Controls.Add(this.projectTextBox);
			this.scanPage.Controls.Add(this.openProjectButton);
			this.scanPage.Controls.Add(this.fileTree);
			this.scanPage.Location = new System.Drawing.Point(4, 22);
			this.scanPage.Name = "scanPage";
			this.scanPage.Padding = new System.Windows.Forms.Padding(3);
			this.scanPage.Size = new System.Drawing.Size(802, 429);
			this.scanPage.TabIndex = 0;
			this.scanPage.Text = "Scan";
			this.scanPage.UseVisualStyleBackColor = true;
			// 
			// viewPage
			// 
			this.viewPage.Location = new System.Drawing.Point(4, 22);
			this.viewPage.Name = "viewPage";
			this.viewPage.Padding = new System.Windows.Forms.Padding(3);
			this.viewPage.Size = new System.Drawing.Size(802, 411);
			this.viewPage.TabIndex = 1;
			this.viewPage.Text = "View";
			this.viewPage.UseVisualStyleBackColor = true;
			// 
			// fileTree
			// 
			this.fileTree.Location = new System.Drawing.Point(6, 58);
			this.fileTree.Name = "fileTree";
			this.fileTree.Size = new System.Drawing.Size(218, 353);
			this.fileTree.TabIndex = 0;
			// 
			// openProjectButton
			// 
			this.openProjectButton.Location = new System.Drawing.Point(6, 29);
			this.openProjectButton.Name = "openProjectButton";
			this.openProjectButton.Size = new System.Drawing.Size(218, 23);
			this.openProjectButton.TabIndex = 1;
			this.openProjectButton.Text = "Open Project";
			this.openProjectButton.UseVisualStyleBackColor = true;
			// 
			// projectTextBox
			// 
			this.projectTextBox.Location = new System.Drawing.Point(94, 6);
			this.projectTextBox.Name = "projectTextBox";
			this.projectTextBox.Size = new System.Drawing.Size(130, 20);
			this.projectTextBox.TabIndex = 2;
			// 
			// projectLabel
			// 
			this.projectLabel.AutoSize = true;
			this.projectLabel.Location = new System.Drawing.Point(6, 9);
			this.projectLabel.Name = "projectLabel";
			this.projectLabel.Size = new System.Drawing.Size(82, 13);
			this.projectLabel.TabIndex = 3;
			this.projectLabel.Text = "GitHub Project: ";
			// 
			// settingsGroupBox
			// 
			this.settingsGroupBox.Location = new System.Drawing.Point(230, 3);
			this.settingsGroupBox.Name = "settingsGroupBox";
			this.settingsGroupBox.Size = new System.Drawing.Size(566, 379);
			this.settingsGroupBox.TabIndex = 4;
			this.settingsGroupBox.TabStop = false;
			this.settingsGroupBox.Text = "Scan Settings";
			// 
			// scanProgressBar
			// 
			this.scanProgressBar.Location = new System.Drawing.Point(230, 388);
			this.scanProgressBar.Name = "scanProgressBar";
			this.scanProgressBar.Size = new System.Drawing.Size(450, 23);
			this.scanProgressBar.TabIndex = 5;
			// 
			// beginScanButton
			// 
			this.beginScanButton.Location = new System.Drawing.Point(686, 388);
			this.beginScanButton.Name = "beginScanButton";
			this.beginScanButton.Size = new System.Drawing.Size(110, 23);
			this.beginScanButton.TabIndex = 6;
			this.beginScanButton.Text = "Begin Scan";
			this.beginScanButton.UseVisualStyleBackColor = true;
			// 
			// updateButton
			// 
			this.updateButton.Location = new System.Drawing.Point(702, 463);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(116, 19);
			this.updateButton.TabIndex = 1;
			this.updateButton.Text = "Update";
			this.updateButton.UseVisualStyleBackColor = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 464);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(834, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 486);
			this.Controls.Add(this.updateButton);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabControl);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(850, 525);
			this.MinimumSize = new System.Drawing.Size(850, 525);
			this.Name = "Form1";
			this.Text = "PSV";
			this.tabControl.ResumeLayout(false);
			this.scanPage.ResumeLayout(false);
			this.scanPage.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage scanPage;
		private System.Windows.Forms.Button beginScanButton;
		private System.Windows.Forms.ProgressBar scanProgressBar;
		private System.Windows.Forms.GroupBox settingsGroupBox;
		private System.Windows.Forms.Label projectLabel;
		private System.Windows.Forms.TextBox projectTextBox;
		private System.Windows.Forms.Button openProjectButton;
		private System.Windows.Forms.TreeView fileTree;
		private System.Windows.Forms.TabPage viewPage;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
	}
}

