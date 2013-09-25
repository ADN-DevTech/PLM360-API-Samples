namespace Autodesk.Adn.PLM360RestAPISample
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("<empty, Login first>");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("");
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItemLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItemLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.tvWorkspaces = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPreviousPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelCurrentPage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxPageNumber = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxPageSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.lvWorkspaceItems = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.versionId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.release = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.descripter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deleted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.props = new System.Windows.Forms.PropertyGrid();
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1224, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItemLogin,
            this.logoutToolStripMenuItemLogout});
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.loginToolStripMenuItem.Text = "Login/Logout";
            // 
            // loginToolStripMenuItemLogin
            // 
            this.loginToolStripMenuItemLogin.Name = "loginToolStripMenuItemLogin";
            this.loginToolStripMenuItemLogin.Size = new System.Drawing.Size(125, 24);
            this.loginToolStripMenuItemLogin.Text = "Login";
            this.loginToolStripMenuItemLogin.Click += new System.EventHandler(this.loginToolStripMenuItemLogin_Click);
            // 
            // logoutToolStripMenuItemLogout
            // 
            this.logoutToolStripMenuItemLogout.Name = "logoutToolStripMenuItemLogout";
            this.logoutToolStripMenuItemLogout.Size = new System.Drawing.Size(125, 24);
            this.logoutToolStripMenuItemLogout.Text = "Logout";
            this.logoutToolStripMenuItemLogout.Click += new System.EventHandler(this.logoutToolStripMenuItemLogout_Click);
            // 
            // tvWorkspaces
            // 
            this.tvWorkspaces.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvWorkspaces.FullRowSelect = true;
            this.tvWorkspaces.HideSelection = false;
            this.tvWorkspaces.Location = new System.Drawing.Point(0, 28);
            this.tvWorkspaces.Name = "tvWorkspaces";
            treeNode2.Name = "Node0";
            treeNode2.Text = "<empty, Login first>";
            this.tvWorkspaces.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvWorkspaces.Size = new System.Drawing.Size(304, 650);
            this.tvWorkspaces.TabIndex = 2;
            this.tvWorkspaces.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvWorkspaces_AfterSelect);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(304, 28);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 650);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(307, 28);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(10, 650);
            this.splitter2.TabIndex = 5;
            this.splitter2.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPreviousPage,
            this.toolStripSeparator2,
            this.toolStripLabelCurrentPage,
            this.toolStripTextBoxPageNumber,
            this.toolStripSeparator1,
            this.toolStripButtonNextPage,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.toolStripTextBoxPageSize,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(317, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(907, 27);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonPreviousPage
            // 
            this.toolStripButtonPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPreviousPage.Image")));
            this.toolStripButtonPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviousPage.Name = "toolStripButtonPreviousPage";
            this.toolStripButtonPreviousPage.Size = new System.Drawing.Size(149, 24);
            this.toolStripButtonPreviousPage.Text = "<< Previous Page ";
            this.toolStripButtonPreviousPage.Click += new System.EventHandler(this.toolStripButtonPreviousPage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabelCurrentPage
            // 
            this.toolStripLabelCurrentPage.Name = "toolStripLabelCurrentPage";
            this.toolStripLabelCurrentPage.Size = new System.Drawing.Size(101, 24);
            this.toolStripLabelCurrentPage.Text = "Current Page: ";
            // 
            // toolStripTextBoxPageNumber
            // 
            this.toolStripTextBoxPageNumber.Name = "toolStripTextBoxPageNumber";
            this.toolStripTextBoxPageNumber.Size = new System.Drawing.Size(100, 27);
            this.toolStripTextBoxPageNumber.Text = "1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonNextPage
            // 
            this.toolStripButtonNextPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNextPage.Image")));
            this.toolStripButtonNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextPage.Name = "toolStripButtonNextPage";
            this.toolStripButtonNextPage.Size = new System.Drawing.Size(121, 24);
            this.toolStripButtonNextPage.Text = "Next Page >>";
            this.toolStripButtonNextPage.Click += new System.EventHandler(this.toolStripButtonNextPage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(76, 24);
            this.toolStripLabel1.Text = "Page Size:";
            // 
            // toolStripTextBoxPageSize
            // 
            this.toolStripTextBoxPageSize.Name = "toolStripTextBoxPageSize";
            this.toolStripTextBoxPageSize.Size = new System.Drawing.Size(100, 27);
            this.toolStripTextBoxPageSize.Text = "100";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(78, 24);
            this.toolStripButton1.Text = "Refresh";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // lvWorkspaceItems
            // 
            this.lvWorkspaceItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.versionId,
            this.release,
            this.descripter,
            this.deleted});
            this.lvWorkspaceItems.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvWorkspaceItems.FullRowSelect = true;
            this.lvWorkspaceItems.GridLines = true;
            this.lvWorkspaceItems.HideSelection = false;
            this.lvWorkspaceItems.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.lvWorkspaceItems.Location = new System.Drawing.Point(317, 55);
            this.lvWorkspaceItems.MultiSelect = false;
            this.lvWorkspaceItems.Name = "lvWorkspaceItems";
            this.lvWorkspaceItems.Size = new System.Drawing.Size(483, 623);
            this.lvWorkspaceItems.TabIndex = 9;
            this.lvWorkspaceItems.UseCompatibleStateImageBehavior = false;
            this.lvWorkspaceItems.View = System.Windows.Forms.View.Details;
            this.lvWorkspaceItems.SelectedIndexChanged += new System.EventHandler(this.lvWorkspaceItems_SelectedIndexChanged);
            // 
            // Id
            // 
            this.Id.Text = "Id";
            // 
            // versionId
            // 
            this.versionId.Text = "versionId";
            this.versionId.Width = 88;
            // 
            // release
            // 
            this.release.DisplayIndex = 3;
            this.release.Text = "release";
            this.release.Width = 63;
            // 
            // descripter
            // 
            this.descripter.DisplayIndex = 2;
            this.descripter.Text = "descripter";
            this.descripter.Width = 183;
            // 
            // deleted
            // 
            this.deleted.Text = "deleted";
            // 
            // props
            // 
            this.props.Dock = System.Windows.Forms.DockStyle.Fill;
            this.props.Location = new System.Drawing.Point(800, 55);
            this.props.Margin = new System.Windows.Forms.Padding(4);
            this.props.Name = "props";
            this.props.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.props.Size = new System.Drawing.Size(424, 623);
            this.props.TabIndex = 10;
            // 
            // refreshWorkspacesToolStripMenuItemRefreshWorkspaces
            // 
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces.Name = "refreshWorkspacesToolStripMenuItemRefreshWorkspaces";
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces.Size = new System.Drawing.Size(153, 24);
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces.Text = "Refresh Workspaces";
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces.Click += new System.EventHandler(this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 678);
            this.Controls.Add(this.props);
            this.Controls.Add(this.lvWorkspaceItems);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tvWorkspaces);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItemLogin;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItemLogout;
        private System.Windows.Forms.TreeView tvWorkspaces;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxPageSize;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviousPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextPage;
        private System.Windows.Forms.ListView lvWorkspaceItems;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader versionId;
        private System.Windows.Forms.ColumnHeader release;
        private System.Windows.Forms.ColumnHeader descripter;
        private System.Windows.Forms.ColumnHeader deleted;
        private System.Windows.Forms.PropertyGrid props;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCurrentPage;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxPageNumber;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem refreshWorkspacesToolStripMenuItemRefreshWorkspaces;
    }
}

