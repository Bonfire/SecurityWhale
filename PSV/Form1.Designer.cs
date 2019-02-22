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
			this.scanTree = new System.Windows.Forms.TreeView();
			this.openProjectButton = new System.Windows.Forms.Button();
			this.projectTextBox = new System.Windows.Forms.TextBox();
			this.projectLabel = new System.Windows.Forms.Label();
			this.settingsGroupBox = new System.Windows.Forms.GroupBox();
			this.scanProgressBar = new System.Windows.Forms.ProgressBar();
			this.beginScanButton = new System.Windows.Forms.Button();
			this.viewTree = new System.Windows.Forms.TreeView();
			this.graphicTabControl = new System.Windows.Forms.TabControl();
			this.chartTab = new System.Windows.Forms.TabPage();
			this.graphTab = new System.Windows.Forms.TabPage();
			this.tabControl.SuspendLayout();
			this.scanPage.SuspendLayout();
			this.viewPage.SuspendLayout();
			this.graphicTabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.scanPage);
			this.tabControl.Controls.Add(this.viewPage);
			this.tabControl.Location = new System.Drawing.Point(12, 6);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(810, 443);
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
			this.scanPage.Controls.Add(this.scanTree);
			this.scanPage.Location = new System.Drawing.Point(4, 22);
			this.scanPage.Name = "scanPage";
			this.scanPage.Padding = new System.Windows.Forms.Padding(3);
			this.scanPage.Size = new System.Drawing.Size(802, 417);
			this.scanPage.TabIndex = 0;
			this.scanPage.Text = "Scan";
			this.scanPage.UseVisualStyleBackColor = true;
			// 
			// viewPage
			// 
			this.viewPage.Controls.Add(this.graphicTabControl);
			this.viewPage.Controls.Add(this.viewTree);
			this.viewPage.Location = new System.Drawing.Point(4, 22);
			this.viewPage.Name = "viewPage";
			this.viewPage.Padding = new System.Windows.Forms.Padding(3);
			this.viewPage.Size = new System.Drawing.Size(802, 417);
			this.viewPage.TabIndex = 1;
			this.viewPage.Text = "View";
			this.viewPage.UseVisualStyleBackColor = true;
			// 
			// scanTree
			// 
			this.scanTree.Location = new System.Drawing.Point(6, 58);
			this.scanTree.Name = "scanTree";
			this.scanTree.Size = new System.Drawing.Size(218, 353);
			this.scanTree.TabIndex = 0;
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
			// viewTree
			// 
			this.viewTree.Location = new System.Drawing.Point(6, 6);
			this.viewTree.Name = "viewTree";
			this.viewTree.Size = new System.Drawing.Size(156, 405);
			this.viewTree.TabIndex = 0;
			// 
			// graphicTabControl
			// 
			this.graphicTabControl.Controls.Add(this.chartTab);
			this.graphicTabControl.Controls.Add(this.graphTab);
			this.graphicTabControl.Location = new System.Drawing.Point(168, 6);
			this.graphicTabControl.Name = "graphicTabControl";
			this.graphicTabControl.SelectedIndex = 0;
			this.graphicTabControl.Size = new System.Drawing.Size(628, 405);
			this.graphicTabControl.TabIndex = 2;
			// 
			// chartTab
			// 
			this.chartTab.Location = new System.Drawing.Point(4, 22);
			this.chartTab.Name = "chartTab";
			this.chartTab.Padding = new System.Windows.Forms.Padding(3);
			this.chartTab.Size = new System.Drawing.Size(620, 379);
			this.chartTab.TabIndex = 0;
			this.chartTab.Text = "Chart";
			this.chartTab.UseVisualStyleBackColor = true;
			// 
			// graphTab
			// 
			this.graphTab.Location = new System.Drawing.Point(4, 22);
			this.graphTab.Name = "graphTab";
			this.graphTab.Padding = new System.Windows.Forms.Padding(3);
			this.graphTab.Size = new System.Drawing.Size(620, 350);
			this.graphTab.TabIndex = 1;
			this.graphTab.Text = "Graph";
			this.graphTab.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 461);
			this.Controls.Add(this.tabControl);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(850, 500);
			this.MinimumSize = new System.Drawing.Size(850, 500);
			this.Name = "Form1";
			this.Text = "PSV";
			this.tabControl.ResumeLayout(false);
			this.scanPage.ResumeLayout(false);
			this.scanPage.PerformLayout();
			this.viewPage.ResumeLayout(false);
			this.graphicTabControl.ResumeLayout(false);
			this.ResumeLayout(false);

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
		private System.Windows.Forms.TreeView scanTree;
		private System.Windows.Forms.TabPage viewPage;
		private System.Windows.Forms.TreeView viewTree;
		private System.Windows.Forms.TabControl graphicTabControl;
		private System.Windows.Forms.TabPage chartTab;
		private System.Windows.Forms.TabPage graphTab;
	}
}

