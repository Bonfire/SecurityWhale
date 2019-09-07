namespace PSV
{
	partial class PSVForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSVForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.scanPage = new System.Windows.Forms.TabPage();
            this.testURLButton = new System.Windows.Forms.Button();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.pathToCloneTextBox = new System.Windows.Forms.TextBox();
            this.projectNameLabel = new System.Windows.Forms.Label();
            this.beginScanButton = new System.Windows.Forms.Button();
            this.scanProgressBar = new System.Windows.Forms.ProgressBar();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.limitScanGroupBox = new System.Windows.Forms.GroupBox();
            this.exclusionGroupBox = new System.Windows.Forms.GroupBox();
            this.foldersLabel = new System.Windows.Forms.Label();
            this.fileNamesBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileTypesBox = new System.Windows.Forms.TextBox();
            this.fileTypesLabel = new System.Windows.Forms.Label();
            this.includeHiddenCheck = new System.Windows.Forms.CheckBox();
            this.includeSubfoldersCheck = new System.Windows.Forms.CheckBox();
            this.provideDataBox = new System.Windows.Forms.CheckBox();
            this.projectURLLabel = new System.Windows.Forms.Label();
            this.projectURLTextBox = new System.Windows.Forms.TextBox();
            this.openProjectButton = new System.Windows.Forms.Button();
            this.scanTree = new System.Windows.Forms.TreeView();
            this.viewPage = new System.Windows.Forms.TabPage();
            this.infoTabControl = new System.Windows.Forms.TabControl();
            this.tableTab = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.severityColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.vulnColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateTimeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.graphTab = new System.Windows.Forms.TabPage();
            this.vulnGraph = new LiveCharts.WinForms.CartesianChart();
            this.viewTree = new System.Windows.Forms.TreeView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.foldersBox = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.scanPage.SuspendLayout();
            this.settingsGroupBox.SuspendLayout();
            this.limitScanGroupBox.SuspendLayout();
            this.exclusionGroupBox.SuspendLayout();
            this.viewPage.SuspendLayout();
            this.infoTabControl.SuspendLayout();
            this.tableTab.SuspendLayout();
            this.graphTab.SuspendLayout();
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
            this.scanPage.Controls.Add(this.testURLButton);
            this.scanPage.Controls.Add(this.openFolderButton);
            this.scanPage.Controls.Add(this.pathToCloneTextBox);
            this.scanPage.Controls.Add(this.projectNameLabel);
            this.scanPage.Controls.Add(this.beginScanButton);
            this.scanPage.Controls.Add(this.scanProgressBar);
            this.scanPage.Controls.Add(this.settingsGroupBox);
            this.scanPage.Controls.Add(this.projectURLLabel);
            this.scanPage.Controls.Add(this.projectURLTextBox);
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
            // testURLButton
            // 
            this.testURLButton.Location = new System.Drawing.Point(6, 59);
            this.testURLButton.Name = "testURLButton";
            this.testURLButton.Size = new System.Drawing.Size(177, 23);
            this.testURLButton.TabIndex = 10;
            this.testURLButton.Text = "Test URL";
            this.testURLButton.UseVisualStyleBackColor = true;
            this.testURLButton.Click += new System.EventHandler(this.TestURLButton_Click);
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(318, 33);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(45, 20);
            this.openFolderButton.TabIndex = 9;
            this.openFolderButton.Text = "...";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // pathToCloneTextBox
            // 
            this.pathToCloneTextBox.Location = new System.Drawing.Point(106, 33);
            this.pathToCloneTextBox.Name = "pathToCloneTextBox";
            this.pathToCloneTextBox.Size = new System.Drawing.Size(206, 20);
            this.pathToCloneTextBox.TabIndex = 8;
            // 
            // projectNameLabel
            // 
            this.projectNameLabel.AutoSize = true;
            this.projectNameLabel.Location = new System.Drawing.Point(6, 36);
            this.projectNameLabel.Name = "projectNameLabel";
            this.projectNameLabel.Size = new System.Drawing.Size(94, 13);
            this.projectNameLabel.TabIndex = 7;
            this.projectNameLabel.Text = "Path To Clone To:";
            // 
            // beginScanButton
            // 
            this.beginScanButton.Location = new System.Drawing.Point(686, 388);
            this.beginScanButton.Name = "beginScanButton";
            this.beginScanButton.Size = new System.Drawing.Size(110, 23);
            this.beginScanButton.TabIndex = 6;
            this.beginScanButton.Text = "Begin Scan";
            this.beginScanButton.UseVisualStyleBackColor = true;
            this.beginScanButton.Click += new System.EventHandler(this.BeginScanButton_Click);
            // 
            // scanProgressBar
            // 
            this.scanProgressBar.Location = new System.Drawing.Point(369, 388);
            this.scanProgressBar.Name = "scanProgressBar";
            this.scanProgressBar.Size = new System.Drawing.Size(311, 23);
            this.scanProgressBar.TabIndex = 5;
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.limitScanGroupBox);
            this.settingsGroupBox.Controls.Add(this.provideDataBox);
            this.settingsGroupBox.Location = new System.Drawing.Point(9, 88);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(354, 323);
            this.settingsGroupBox.TabIndex = 4;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Scan Settings";
            // 
            // limitScanGroupBox
            // 
            this.limitScanGroupBox.Controls.Add(this.exclusionGroupBox);
            this.limitScanGroupBox.Controls.Add(this.includeHiddenCheck);
            this.limitScanGroupBox.Controls.Add(this.includeSubfoldersCheck);
            this.limitScanGroupBox.Location = new System.Drawing.Point(6, 42);
            this.limitScanGroupBox.Name = "limitScanGroupBox";
            this.limitScanGroupBox.Size = new System.Drawing.Size(342, 275);
            this.limitScanGroupBox.TabIndex = 2;
            this.limitScanGroupBox.TabStop = false;
            this.limitScanGroupBox.Text = "Limit Scan";
            // 
            // exclusionGroupBox
            // 
            this.exclusionGroupBox.Controls.Add(this.foldersBox);
            this.exclusionGroupBox.Controls.Add(this.foldersLabel);
            this.exclusionGroupBox.Controls.Add(this.fileNamesBox);
            this.exclusionGroupBox.Controls.Add(this.label1);
            this.exclusionGroupBox.Controls.Add(this.fileTypesBox);
            this.exclusionGroupBox.Controls.Add(this.fileTypesLabel);
            this.exclusionGroupBox.Location = new System.Drawing.Point(6, 65);
            this.exclusionGroupBox.Name = "exclusionGroupBox";
            this.exclusionGroupBox.Size = new System.Drawing.Size(330, 204);
            this.exclusionGroupBox.TabIndex = 2;
            this.exclusionGroupBox.TabStop = false;
            this.exclusionGroupBox.Text = "Exclusions (Comma Separated)";
            // 
            // foldersLabel
            // 
            this.foldersLabel.AutoSize = true;
            this.foldersLabel.Location = new System.Drawing.Point(6, 74);
            this.foldersLabel.Name = "foldersLabel";
            this.foldersLabel.Size = new System.Drawing.Size(44, 13);
            this.foldersLabel.TabIndex = 4;
            this.foldersLabel.Text = "Folders:";
            // 
            // fileNamesBox
            // 
            this.fileNamesBox.Location = new System.Drawing.Point(70, 19);
            this.fileNamesBox.Name = "fileNamesBox";
            this.fileNamesBox.Size = new System.Drawing.Size(254, 20);
            this.fileNamesBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File Names:";
            // 
            // fileTypesBox
            // 
            this.fileTypesBox.Location = new System.Drawing.Point(70, 45);
            this.fileTypesBox.Name = "fileTypesBox";
            this.fileTypesBox.Size = new System.Drawing.Size(254, 20);
            this.fileTypesBox.TabIndex = 1;
            // 
            // fileTypesLabel
            // 
            this.fileTypesLabel.AutoSize = true;
            this.fileTypesLabel.Location = new System.Drawing.Point(6, 48);
            this.fileTypesLabel.Name = "fileTypesLabel";
            this.fileTypesLabel.Size = new System.Drawing.Size(58, 13);
            this.fileTypesLabel.TabIndex = 0;
            this.fileTypesLabel.Text = "File Types:";
            // 
            // includeHiddenCheck
            // 
            this.includeHiddenCheck.AutoSize = true;
            this.includeHiddenCheck.Location = new System.Drawing.Point(6, 42);
            this.includeHiddenCheck.Name = "includeHiddenCheck";
            this.includeHiddenCheck.Size = new System.Drawing.Size(126, 17);
            this.includeHiddenCheck.TabIndex = 1;
            this.includeHiddenCheck.Text = "Include Hidden Items";
            this.includeHiddenCheck.UseVisualStyleBackColor = true;
            // 
            // includeSubfoldersCheck
            // 
            this.includeSubfoldersCheck.AutoSize = true;
            this.includeSubfoldersCheck.Location = new System.Drawing.Point(6, 19);
            this.includeSubfoldersCheck.Name = "includeSubfoldersCheck";
            this.includeSubfoldersCheck.Size = new System.Drawing.Size(114, 17);
            this.includeSubfoldersCheck.TabIndex = 0;
            this.includeSubfoldersCheck.Text = "Include Subfolders";
            this.includeSubfoldersCheck.UseVisualStyleBackColor = true;
            // 
            // provideDataBox
            // 
            this.provideDataBox.AutoSize = true;
            this.provideDataBox.Location = new System.Drawing.Point(6, 19);
            this.provideDataBox.Name = "provideDataBox";
            this.provideDataBox.Size = new System.Drawing.Size(144, 17);
            this.provideDataBox.TabIndex = 1;
            this.provideDataBox.Text = "Provide Data for Training";
            this.provideDataBox.UseVisualStyleBackColor = true;
            // 
            // projectURLLabel
            // 
            this.projectURLLabel.AutoSize = true;
            this.projectURLLabel.Location = new System.Drawing.Point(6, 9);
            this.projectURLLabel.Name = "projectURLLabel";
            this.projectURLLabel.Size = new System.Drawing.Size(87, 13);
            this.projectURLLabel.TabIndex = 3;
            this.projectURLLabel.Text = "Project Git URL: ";
            // 
            // projectURLTextBox
            // 
            this.projectURLTextBox.Location = new System.Drawing.Point(106, 6);
            this.projectURLTextBox.Name = "projectURLTextBox";
            this.projectURLTextBox.Size = new System.Drawing.Size(257, 20);
            this.projectURLTextBox.TabIndex = 2;
            // 
            // openProjectButton
            // 
            this.openProjectButton.Location = new System.Drawing.Point(186, 59);
            this.openProjectButton.Name = "openProjectButton";
            this.openProjectButton.Size = new System.Drawing.Size(177, 23);
            this.openProjectButton.TabIndex = 1;
            this.openProjectButton.Text = "Clone and Open Project";
            this.openProjectButton.UseVisualStyleBackColor = true;
            this.openProjectButton.Click += new System.EventHandler(this.OpenProjectButton_Click);
            // 
            // scanTree
            // 
            this.scanTree.Location = new System.Drawing.Point(369, 6);
            this.scanTree.Name = "scanTree";
            this.scanTree.Size = new System.Drawing.Size(427, 376);
            this.scanTree.TabIndex = 0;
            // 
            // viewPage
            // 
            this.viewPage.Controls.Add(this.infoTabControl);
            this.viewPage.Controls.Add(this.viewTree);
            this.viewPage.Location = new System.Drawing.Point(4, 22);
            this.viewPage.Name = "viewPage";
            this.viewPage.Padding = new System.Windows.Forms.Padding(3);
            this.viewPage.Size = new System.Drawing.Size(802, 417);
            this.viewPage.TabIndex = 1;
            this.viewPage.Text = "View";
            this.viewPage.UseVisualStyleBackColor = true;
            // 
            // infoTabControl
            // 
            this.infoTabControl.Controls.Add(this.tableTab);
            this.infoTabControl.Controls.Add(this.graphTab);
            this.infoTabControl.Location = new System.Drawing.Point(168, 6);
            this.infoTabControl.Name = "infoTabControl";
            this.infoTabControl.SelectedIndex = 0;
            this.infoTabControl.Size = new System.Drawing.Size(628, 405);
            this.infoTabControl.TabIndex = 2;
            // 
            // tableTab
            // 
            this.tableTab.Controls.Add(this.listView1);
            this.tableTab.Location = new System.Drawing.Point(4, 22);
            this.tableTab.Name = "tableTab";
            this.tableTab.Padding = new System.Windows.Forms.Padding(3);
            this.tableTab.Size = new System.Drawing.Size(620, 379);
            this.tableTab.TabIndex = 0;
            this.tableTab.Text = "Table";
            this.tableTab.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.severityColumn,
            this.vulnColumn,
            this.fileColumn,
            this.dateTimeColumn});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(624, 383);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // severityColumn
            // 
            this.severityColumn.Text = "Severity";
            this.severityColumn.Width = 57;
            // 
            // vulnColumn
            // 
            this.vulnColumn.Text = "Vulnerability";
            this.vulnColumn.Width = 178;
            // 
            // fileColumn
            // 
            this.fileColumn.Text = "File";
            this.fileColumn.Width = 267;
            // 
            // dateTimeColumn
            // 
            this.dateTimeColumn.Text = "Date/Time";
            this.dateTimeColumn.Width = 120;
            // 
            // graphTab
            // 
            this.graphTab.Controls.Add(this.vulnGraph);
            this.graphTab.Location = new System.Drawing.Point(4, 22);
            this.graphTab.Name = "graphTab";
            this.graphTab.Padding = new System.Windows.Forms.Padding(3);
            this.graphTab.Size = new System.Drawing.Size(620, 379);
            this.graphTab.TabIndex = 1;
            this.graphTab.Text = "Graph";
            this.graphTab.UseVisualStyleBackColor = true;
            // 
            // vulnGraph
            // 
            this.vulnGraph.Location = new System.Drawing.Point(6, 6);
            this.vulnGraph.Name = "vulnGraph";
            this.vulnGraph.Size = new System.Drawing.Size(608, 367);
            this.vulnGraph.TabIndex = 0;
            this.vulnGraph.Text = "Vulnerability Graph";
            // 
            // viewTree
            // 
            this.viewTree.Location = new System.Drawing.Point(6, 6);
            this.viewTree.Name = "viewTree";
            this.viewTree.Size = new System.Drawing.Size(156, 405);
            this.viewTree.TabIndex = 0;
            // 
            // foldersBox
            // 
            this.foldersBox.Location = new System.Drawing.Point(70, 71);
            this.foldersBox.Name = "foldersBox";
            this.foldersBox.Size = new System.Drawing.Size(254, 20);
            this.foldersBox.TabIndex = 5;
            // 
            // PSVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 461);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(850, 500);
            this.MinimumSize = new System.Drawing.Size(850, 500);
            this.Name = "PSVForm";
            this.Text = "PSV";
            this.tabControl.ResumeLayout(false);
            this.scanPage.ResumeLayout(false);
            this.scanPage.PerformLayout();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.limitScanGroupBox.ResumeLayout(false);
            this.limitScanGroupBox.PerformLayout();
            this.exclusionGroupBox.ResumeLayout(false);
            this.exclusionGroupBox.PerformLayout();
            this.viewPage.ResumeLayout(false);
            this.infoTabControl.ResumeLayout(false);
            this.tableTab.ResumeLayout(false);
            this.graphTab.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage scanPage;
		private System.Windows.Forms.Button beginScanButton;
		private System.Windows.Forms.ProgressBar scanProgressBar;
		private System.Windows.Forms.GroupBox settingsGroupBox;
		private System.Windows.Forms.Label projectURLLabel;
		private System.Windows.Forms.Button openProjectButton;
		private System.Windows.Forms.TreeView scanTree;
		private System.Windows.Forms.TabPage viewPage;
		private System.Windows.Forms.TreeView viewTree;
		private System.Windows.Forms.TabControl infoTabControl;
		private System.Windows.Forms.TabPage tableTab;
		private System.Windows.Forms.TabPage graphTab;
		private LiveCharts.WinForms.CartesianChart vulnGraph;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label projectNameLabel;
		private System.Windows.Forms.Button testURLButton;
		private System.Windows.Forms.Button openFolderButton;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ColumnHeader severityColumn;
		private System.Windows.Forms.ColumnHeader vulnColumn;
		private System.Windows.Forms.ColumnHeader fileColumn;
		private System.Windows.Forms.ColumnHeader dateTimeColumn;
		public System.Windows.Forms.TextBox projectURLTextBox;
		public System.Windows.Forms.TextBox pathToCloneTextBox;
        private System.Windows.Forms.GroupBox limitScanGroupBox;
        private System.Windows.Forms.GroupBox exclusionGroupBox;
        private System.Windows.Forms.Label foldersLabel;
        private System.Windows.Forms.TextBox fileNamesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileTypesBox;
        private System.Windows.Forms.Label fileTypesLabel;
        private System.Windows.Forms.CheckBox includeHiddenCheck;
        private System.Windows.Forms.CheckBox includeSubfoldersCheck;
        private System.Windows.Forms.CheckBox provideDataBox;
        private System.Windows.Forms.TextBox foldersBox;
    }
}

