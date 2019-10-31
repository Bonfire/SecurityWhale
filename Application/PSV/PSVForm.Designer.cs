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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSVForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.scanPage = new System.Windows.Forms.TabPage();
            this.repoTypeSeparatorRight = new System.Windows.Forms.Label();
            this.repoTypeSeparatorLeft = new System.Windows.Forms.Label();
            this.repoTypeLabel = new System.Windows.Forms.Label();
            this.localSeparatorRight = new System.Windows.Forms.Label();
            this.localSeparatorLeft = new System.Windows.Forms.Label();
            this.remoteSeparatorRight = new System.Windows.Forms.Label();
            this.remoteSeparatorLeft = new System.Windows.Forms.Label();
            this.localRepoLabel = new System.Windows.Forms.Label();
            this.remoteRepoLabel = new System.Windows.Forms.Label();
            this.localBrowserButton = new System.Windows.Forms.Button();
            this.localPathTextbox = new System.Windows.Forms.TextBox();
            this.localPathLabel = new System.Windows.Forms.Label();
            this.remoteRadio = new System.Windows.Forms.RadioButton();
            this.localRadio = new System.Windows.Forms.RadioButton();
            this.loadProjectButton = new System.Windows.Forms.Button();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.pathToCloneTextBox = new System.Windows.Forms.TextBox();
            this.projectNameLabel = new System.Windows.Forms.Label();
            this.beginScanButton = new System.Windows.Forms.Button();
            this.scanProgressBar = new System.Windows.Forms.ProgressBar();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.exclusionGroupBox = new System.Windows.Forms.GroupBox();
            this.foldersBox = new System.Windows.Forms.TextBox();
            this.foldersLabel = new System.Windows.Forms.Label();
            this.fileNamesBox = new System.Windows.Forms.TextBox();
            this.fileNamesLabel = new System.Windows.Forms.Label();
            this.fileExtensionsBox = new System.Windows.Forms.TextBox();
            this.fileExtensionsLabel = new System.Windows.Forms.Label();
            this.includeHiddenCheck = new System.Windows.Forms.CheckBox();
            this.projectURLLabel = new System.Windows.Forms.Label();
            this.projectURLTextBox = new System.Windows.Forms.TextBox();
            this.cloneProjectButton = new System.Windows.Forms.Button();
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
            this.viewTree = new System.Windows.Forms.TreeView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.provideDataToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.includeHiddenItemsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fileNamesToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fileExtensionToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.foldersToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl.SuspendLayout();
            this.scanPage.SuspendLayout();
            this.settingsGroupBox.SuspendLayout();
            this.exclusionGroupBox.SuspendLayout();
            this.viewPage.SuspendLayout();
            this.infoTabControl.SuspendLayout();
            this.tableTab.SuspendLayout();
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
            this.scanPage.Controls.Add(this.repoTypeSeparatorRight);
            this.scanPage.Controls.Add(this.repoTypeSeparatorLeft);
            this.scanPage.Controls.Add(this.repoTypeLabel);
            this.scanPage.Controls.Add(this.localSeparatorRight);
            this.scanPage.Controls.Add(this.localSeparatorLeft);
            this.scanPage.Controls.Add(this.remoteSeparatorRight);
            this.scanPage.Controls.Add(this.remoteSeparatorLeft);
            this.scanPage.Controls.Add(this.localRepoLabel);
            this.scanPage.Controls.Add(this.remoteRepoLabel);
            this.scanPage.Controls.Add(this.localBrowserButton);
            this.scanPage.Controls.Add(this.localPathTextbox);
            this.scanPage.Controls.Add(this.localPathLabel);
            this.scanPage.Controls.Add(this.remoteRadio);
            this.scanPage.Controls.Add(this.localRadio);
            this.scanPage.Controls.Add(this.loadProjectButton);
            this.scanPage.Controls.Add(this.openFolderButton);
            this.scanPage.Controls.Add(this.pathToCloneTextBox);
            this.scanPage.Controls.Add(this.projectNameLabel);
            this.scanPage.Controls.Add(this.beginScanButton);
            this.scanPage.Controls.Add(this.scanProgressBar);
            this.scanPage.Controls.Add(this.settingsGroupBox);
            this.scanPage.Controls.Add(this.projectURLLabel);
            this.scanPage.Controls.Add(this.projectURLTextBox);
            this.scanPage.Controls.Add(this.cloneProjectButton);
            this.scanPage.Controls.Add(this.scanTree);
            this.scanPage.Location = new System.Drawing.Point(4, 22);
            this.scanPage.Name = "scanPage";
            this.scanPage.Padding = new System.Windows.Forms.Padding(3);
            this.scanPage.Size = new System.Drawing.Size(802, 417);
            this.scanPage.TabIndex = 0;
            this.scanPage.Text = "Scan";
            this.scanPage.UseVisualStyleBackColor = true;
            // 
            // repoTypeSeparatorRight
            // 
            this.repoTypeSeparatorRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.repoTypeSeparatorRight.Location = new System.Drawing.Point(233, 19);
            this.repoTypeSeparatorRight.Name = "repoTypeSeparatorRight";
            this.repoTypeSeparatorRight.Size = new System.Drawing.Size(132, 2);
            this.repoTypeSeparatorRight.TabIndex = 29;
            // 
            // repoTypeSeparatorLeft
            // 
            this.repoTypeSeparatorLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.repoTypeSeparatorLeft.Location = new System.Drawing.Point(6, 20);
            this.repoTypeSeparatorLeft.Name = "repoTypeSeparatorLeft";
            this.repoTypeSeparatorLeft.Size = new System.Drawing.Size(131, 2);
            this.repoTypeSeparatorLeft.TabIndex = 28;
            // 
            // repoTypeLabel
            // 
            this.repoTypeLabel.AutoSize = true;
            this.repoTypeLabel.Location = new System.Drawing.Point(143, 14);
            this.repoTypeLabel.Name = "repoTypeLabel";
            this.repoTypeLabel.Size = new System.Drawing.Size(84, 13);
            this.repoTypeLabel.TabIndex = 27;
            this.repoTypeLabel.Text = "Repository Type";
            // 
            // localSeparatorRight
            // 
            this.localSeparatorRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.localSeparatorRight.Location = new System.Drawing.Point(210, 72);
            this.localSeparatorRight.Name = "localSeparatorRight";
            this.localSeparatorRight.Size = new System.Drawing.Size(153, 2);
            this.localSeparatorRight.TabIndex = 26;
            this.localSeparatorRight.Text = "--------";
            // 
            // localSeparatorLeft
            // 
            this.localSeparatorLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.localSeparatorLeft.Location = new System.Drawing.Point(12, 72);
            this.localSeparatorLeft.Name = "localSeparatorLeft";
            this.localSeparatorLeft.Size = new System.Drawing.Size(153, 2);
            this.localSeparatorLeft.TabIndex = 25;
            this.localSeparatorLeft.Text = "--------";
            // 
            // remoteSeparatorRight
            // 
            this.remoteSeparatorRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.remoteSeparatorRight.Location = new System.Drawing.Point(216, 125);
            this.remoteSeparatorRight.Name = "remoteSeparatorRight";
            this.remoteSeparatorRight.Size = new System.Drawing.Size(149, 2);
            this.remoteSeparatorRight.TabIndex = 24;
            this.remoteSeparatorRight.Text = "--------";
            // 
            // remoteSeparatorLeft
            // 
            this.remoteSeparatorLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.remoteSeparatorLeft.Location = new System.Drawing.Point(11, 126);
            this.remoteSeparatorLeft.Name = "remoteSeparatorLeft";
            this.remoteSeparatorLeft.Size = new System.Drawing.Size(149, 2);
            this.remoteSeparatorLeft.TabIndex = 23;
            this.remoteSeparatorLeft.Text = "--------";
            // 
            // localRepoLabel
            // 
            this.localRepoLabel.AutoSize = true;
            this.localRepoLabel.Location = new System.Drawing.Point(171, 66);
            this.localRepoLabel.Name = "localRepoLabel";
            this.localRepoLabel.Size = new System.Drawing.Size(33, 13);
            this.localRepoLabel.TabIndex = 22;
            this.localRepoLabel.Text = "Local";
            this.localRepoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // remoteRepoLabel
            // 
            this.remoteRepoLabel.AutoSize = true;
            this.remoteRepoLabel.Location = new System.Drawing.Point(166, 120);
            this.remoteRepoLabel.Name = "remoteRepoLabel";
            this.remoteRepoLabel.Size = new System.Drawing.Size(44, 13);
            this.remoteRepoLabel.TabIndex = 21;
            this.remoteRepoLabel.Text = "Remote";
            this.remoteRepoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // localBrowserButton
            // 
            this.localBrowserButton.Location = new System.Drawing.Point(320, 90);
            this.localBrowserButton.Name = "localBrowserButton";
            this.localBrowserButton.Size = new System.Drawing.Size(45, 20);
            this.localBrowserButton.TabIndex = 20;
            this.localBrowserButton.Text = "...";
            this.localBrowserButton.UseVisualStyleBackColor = true;
            this.localBrowserButton.Click += new System.EventHandler(this.localBrowserButton_Click);
            // 
            // localPathTextbox
            // 
            this.localPathTextbox.Location = new System.Drawing.Point(108, 90);
            this.localPathTextbox.Name = "localPathTextbox";
            this.localPathTextbox.Size = new System.Drawing.Size(206, 20);
            this.localPathTextbox.TabIndex = 19;
            // 
            // localPathLabel
            // 
            this.localPathLabel.AutoSize = true;
            this.localPathLabel.Location = new System.Drawing.Point(8, 93);
            this.localPathLabel.Name = "localPathLabel";
            this.localPathLabel.Size = new System.Drawing.Size(85, 13);
            this.localPathLabel.TabIndex = 18;
            this.localPathLabel.Text = "Repository Path:";
            // 
            // remoteRadio
            // 
            this.remoteRadio.AutoSize = true;
            this.remoteRadio.Location = new System.Drawing.Point(233, 38);
            this.remoteRadio.Name = "remoteRadio";
            this.remoteRadio.Size = new System.Drawing.Size(62, 17);
            this.remoteRadio.TabIndex = 17;
            this.remoteRadio.Text = "Remote";
            this.remoteRadio.UseVisualStyleBackColor = true;
            this.remoteRadio.CheckedChanged += new System.EventHandler(this.RemoteRadio_CheckedChanged);
            // 
            // localRadio
            // 
            this.localRadio.AutoSize = true;
            this.localRadio.Checked = true;
            this.localRadio.Location = new System.Drawing.Point(86, 38);
            this.localRadio.Name = "localRadio";
            this.localRadio.Size = new System.Drawing.Size(51, 17);
            this.localRadio.TabIndex = 16;
            this.localRadio.TabStop = true;
            this.localRadio.Text = "Local";
            this.localRadio.UseVisualStyleBackColor = true;
            // 
            // loadProjectButton
            // 
            this.loadProjectButton.Location = new System.Drawing.Point(191, 198);
            this.loadProjectButton.Name = "loadProjectButton";
            this.loadProjectButton.Size = new System.Drawing.Size(174, 23);
            this.loadProjectButton.TabIndex = 5;
            this.loadProjectButton.Text = "Load Project";
            this.loadProjectButton.UseVisualStyleBackColor = true;
            this.loadProjectButton.Click += new System.EventHandler(this.LoadProjectButton_Click);
            // 
            // openFolderButton
            // 
            this.openFolderButton.Enabled = false;
            this.openFolderButton.Location = new System.Drawing.Point(320, 172);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(45, 20);
            this.openFolderButton.TabIndex = 3;
            this.openFolderButton.Text = "...";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // pathToCloneTextBox
            // 
            this.pathToCloneTextBox.Enabled = false;
            this.pathToCloneTextBox.Location = new System.Drawing.Point(108, 172);
            this.pathToCloneTextBox.Name = "pathToCloneTextBox";
            this.pathToCloneTextBox.Size = new System.Drawing.Size(206, 20);
            this.pathToCloneTextBox.TabIndex = 2;
            // 
            // projectNameLabel
            // 
            this.projectNameLabel.AutoSize = true;
            this.projectNameLabel.Location = new System.Drawing.Point(8, 175);
            this.projectNameLabel.Name = "projectNameLabel";
            this.projectNameLabel.Size = new System.Drawing.Size(86, 13);
            this.projectNameLabel.TabIndex = 7;
            this.projectNameLabel.Text = "Path to Clone to:";
            // 
            // beginScanButton
            // 
            this.beginScanButton.Enabled = false;
            this.beginScanButton.Location = new System.Drawing.Point(639, 388);
            this.beginScanButton.Name = "beginScanButton";
            this.beginScanButton.Size = new System.Drawing.Size(157, 23);
            this.beginScanButton.TabIndex = 14;
            this.beginScanButton.Text = "Begin Scan";
            this.beginScanButton.UseVisualStyleBackColor = true;
            this.beginScanButton.Click += new System.EventHandler(this.BeginScanButton_Click);
            // 
            // scanProgressBar
            // 
            this.scanProgressBar.Location = new System.Drawing.Point(369, 388);
            this.scanProgressBar.Name = "scanProgressBar";
            this.scanProgressBar.Size = new System.Drawing.Size(264, 23);
            this.scanProgressBar.TabIndex = 5;
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.exclusionGroupBox);
            this.settingsGroupBox.Controls.Add(this.includeHiddenCheck);
            this.settingsGroupBox.Location = new System.Drawing.Point(9, 227);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(354, 184);
            this.settingsGroupBox.TabIndex = 6;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Scan Settings";
            // 
            // exclusionGroupBox
            // 
            this.exclusionGroupBox.Controls.Add(this.foldersBox);
            this.exclusionGroupBox.Controls.Add(this.foldersLabel);
            this.exclusionGroupBox.Controls.Add(this.fileNamesBox);
            this.exclusionGroupBox.Controls.Add(this.fileNamesLabel);
            this.exclusionGroupBox.Controls.Add(this.fileExtensionsBox);
            this.exclusionGroupBox.Controls.Add(this.fileExtensionsLabel);
            this.exclusionGroupBox.Location = new System.Drawing.Point(6, 42);
            this.exclusionGroupBox.Name = "exclusionGroupBox";
            this.exclusionGroupBox.Size = new System.Drawing.Size(342, 136);
            this.exclusionGroupBox.TabIndex = 10;
            this.exclusionGroupBox.TabStop = false;
            this.exclusionGroupBox.Text = "Exclusions (Comma Separated)";
            // 
            // foldersBox
            // 
            this.foldersBox.Location = new System.Drawing.Point(93, 78);
            this.foldersBox.Name = "foldersBox";
            this.foldersBox.Size = new System.Drawing.Size(237, 20);
            this.foldersBox.TabIndex = 13;
            // 
            // foldersLabel
            // 
            this.foldersLabel.AutoSize = true;
            this.foldersLabel.Location = new System.Drawing.Point(12, 81);
            this.foldersLabel.Name = "foldersLabel";
            this.foldersLabel.Size = new System.Drawing.Size(44, 13);
            this.foldersLabel.TabIndex = 4;
            this.foldersLabel.Text = "Folders:";
            // 
            // fileNamesBox
            // 
            this.fileNamesBox.Location = new System.Drawing.Point(93, 26);
            this.fileNamesBox.Name = "fileNamesBox";
            this.fileNamesBox.Size = new System.Drawing.Size(237, 20);
            this.fileNamesBox.TabIndex = 11;
            // 
            // fileNamesLabel
            // 
            this.fileNamesLabel.AutoSize = true;
            this.fileNamesLabel.Location = new System.Drawing.Point(12, 29);
            this.fileNamesLabel.Name = "fileNamesLabel";
            this.fileNamesLabel.Size = new System.Drawing.Size(62, 13);
            this.fileNamesLabel.TabIndex = 2;
            this.fileNamesLabel.Text = "File Names:";
            // 
            // fileExtensionsBox
            // 
            this.fileExtensionsBox.Location = new System.Drawing.Point(93, 52);
            this.fileExtensionsBox.Name = "fileExtensionsBox";
            this.fileExtensionsBox.Size = new System.Drawing.Size(237, 20);
            this.fileExtensionsBox.TabIndex = 12;
            // 
            // fileExtensionsLabel
            // 
            this.fileExtensionsLabel.AutoSize = true;
            this.fileExtensionsLabel.Location = new System.Drawing.Point(12, 55);
            this.fileExtensionsLabel.Name = "fileExtensionsLabel";
            this.fileExtensionsLabel.Size = new System.Drawing.Size(75, 13);
            this.fileExtensionsLabel.TabIndex = 0;
            this.fileExtensionsLabel.Text = "File Extension:";
            // 
            // includeHiddenCheck
            // 
            this.includeHiddenCheck.AutoSize = true;
            this.includeHiddenCheck.Location = new System.Drawing.Point(6, 19);
            this.includeHiddenCheck.Name = "includeHiddenCheck";
            this.includeHiddenCheck.Size = new System.Drawing.Size(126, 17);
            this.includeHiddenCheck.TabIndex = 9;
            this.includeHiddenCheck.Text = "Include Hidden Items";
            this.includeHiddenCheck.UseVisualStyleBackColor = true;
            // 
            // projectURLLabel
            // 
            this.projectURLLabel.AutoSize = true;
            this.projectURLLabel.Location = new System.Drawing.Point(8, 148);
            this.projectURLLabel.Name = "projectURLLabel";
            this.projectURLLabel.Size = new System.Drawing.Size(87, 13);
            this.projectURLLabel.TabIndex = 3;
            this.projectURLLabel.Text = "Project Git URL: ";
            // 
            // projectURLTextBox
            // 
            this.projectURLTextBox.Enabled = false;
            this.projectURLTextBox.Location = new System.Drawing.Point(108, 145);
            this.projectURLTextBox.Name = "projectURLTextBox";
            this.projectURLTextBox.Size = new System.Drawing.Size(257, 20);
            this.projectURLTextBox.TabIndex = 1;
            // 
            // cloneProjectButton
            // 
            this.cloneProjectButton.Enabled = false;
            this.cloneProjectButton.Location = new System.Drawing.Point(9, 198);
            this.cloneProjectButton.Name = "cloneProjectButton";
            this.cloneProjectButton.Size = new System.Drawing.Size(174, 23);
            this.cloneProjectButton.TabIndex = 4;
            this.cloneProjectButton.Text = "Clone Project";
            this.cloneProjectButton.UseVisualStyleBackColor = true;
            this.cloneProjectButton.Click += new System.EventHandler(this.CloneProjectButton_Click);
            // 
            // scanTree
            // 
            this.scanTree.Location = new System.Drawing.Point(369, 0);
            this.scanTree.Name = "scanTree";
            this.scanTree.Size = new System.Drawing.Size(427, 382);
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
            this.severityColumn.Width = 56;
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
            this.graphTab.Location = new System.Drawing.Point(4, 22);
            this.graphTab.Name = "graphTab";
            this.graphTab.Padding = new System.Windows.Forms.Padding(3);
            this.graphTab.Size = new System.Drawing.Size(620, 379);
            this.graphTab.TabIndex = 1;
            this.graphTab.Text = "Graph";
            this.graphTab.UseVisualStyleBackColor = true;
            // 
            // viewTree
            // 
            this.viewTree.Location = new System.Drawing.Point(6, 6);
            this.viewTree.Name = "viewTree";
            this.viewTree.Size = new System.Drawing.Size(156, 405);
            this.viewTree.TabIndex = 0;
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
            this.Load += new System.EventHandler(this.PSVForm_Load);
            this.tabControl.ResumeLayout(false);
            this.scanPage.ResumeLayout(false);
            this.scanPage.PerformLayout();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.exclusionGroupBox.ResumeLayout(false);
            this.exclusionGroupBox.PerformLayout();
            this.viewPage.ResumeLayout(false);
            this.infoTabControl.ResumeLayout(false);
            this.tableTab.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage scanPage;
		private System.Windows.Forms.Button beginScanButton;
		private System.Windows.Forms.ProgressBar scanProgressBar;
		private System.Windows.Forms.GroupBox settingsGroupBox;
		private System.Windows.Forms.Label projectURLLabel;
		private System.Windows.Forms.Button cloneProjectButton;
		private System.Windows.Forms.TreeView scanTree;
		private System.Windows.Forms.TabPage viewPage;
		private System.Windows.Forms.TreeView viewTree;
		private System.Windows.Forms.TabControl infoTabControl;
		private System.Windows.Forms.TabPage tableTab;
		private System.Windows.Forms.TabPage graphTab;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label projectNameLabel;
		private System.Windows.Forms.Button openFolderButton;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ColumnHeader severityColumn;
		private System.Windows.Forms.ColumnHeader vulnColumn;
		private System.Windows.Forms.ColumnHeader fileColumn;
		private System.Windows.Forms.ColumnHeader dateTimeColumn;
		public System.Windows.Forms.TextBox projectURLTextBox;
		public System.Windows.Forms.TextBox pathToCloneTextBox;
        private System.Windows.Forms.GroupBox exclusionGroupBox;
        private System.Windows.Forms.Label foldersLabel;
        private System.Windows.Forms.TextBox fileNamesBox;
        private System.Windows.Forms.Label fileNamesLabel;
        private System.Windows.Forms.TextBox fileExtensionsBox;
        private System.Windows.Forms.Label fileExtensionsLabel;
        private System.Windows.Forms.CheckBox includeHiddenCheck;
        private System.Windows.Forms.TextBox foldersBox;
        private System.Windows.Forms.Button loadProjectButton;
        private System.Windows.Forms.Label localSeparatorRight;
        private System.Windows.Forms.Label localSeparatorLeft;
        private System.Windows.Forms.Label remoteSeparatorRight;
        private System.Windows.Forms.Label remoteSeparatorLeft;
        private System.Windows.Forms.Label localRepoLabel;
        private System.Windows.Forms.Label remoteRepoLabel;
        private System.Windows.Forms.Button localBrowserButton;
        private System.Windows.Forms.TextBox localPathTextbox;
        private System.Windows.Forms.Label localPathLabel;
        private System.Windows.Forms.RadioButton remoteRadio;
        private System.Windows.Forms.RadioButton localRadio;
        private System.Windows.Forms.Label repoTypeSeparatorRight;
        private System.Windows.Forms.Label repoTypeSeparatorLeft;
        private System.Windows.Forms.Label repoTypeLabel;
        private System.Windows.Forms.ToolTip provideDataToolTip;
        private System.Windows.Forms.ToolTip includeHiddenItemsToolTip;
        private System.Windows.Forms.ToolTip fileNamesToolTip;
        private System.Windows.Forms.ToolTip fileExtensionToolTip;
        private System.Windows.Forms.ToolTip foldersToolTip;
    }
}

