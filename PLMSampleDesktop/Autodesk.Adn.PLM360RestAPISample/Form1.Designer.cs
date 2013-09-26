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
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("<empty, Login first>");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem("");
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItemLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItemLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces = new System.Windows.Forms.ToolStripMenuItem();
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
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.props = new System.Windows.Forms.PropertyGrid();
            this.splitter5 = new System.Windows.Forms.Splitter();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCheckout = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUndoCheckout = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDownload = new System.Windows.Forms.ToolStripButton();
            this.listViewAttachments = new System.Windows.Forms.ListView();
            this.FileId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileTitile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStripButtonDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCloneItem = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1233, 28);
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
            // refreshWorkspacesToolStripMenuItemRefreshWorkspaces
            // 
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces.Name = "refreshWorkspacesToolStripMenuItemRefreshWorkspaces";
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces.Size = new System.Drawing.Size(153, 24);
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces.Text = "Refresh Workspaces";
            this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces.Click += new System.EventHandler(this.refreshWorkspacesToolStripMenuItemRefreshWorkspaces_Click);
            // 
            // tvWorkspaces
            // 
            this.tvWorkspaces.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvWorkspaces.FullRowSelect = true;
            this.tvWorkspaces.HideSelection = false;
            this.tvWorkspaces.Location = new System.Drawing.Point(0, 28);
            this.tvWorkspaces.Name = "tvWorkspaces";
            treeNode4.Name = "Node0";
            treeNode4.Text = "<empty, Login first>";
            this.tvWorkspaces.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
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
            this.toolStripButton1,
            this.toolStripButtonDeleteItem,
            this.toolStripButtonCloneItem});
            this.toolStrip1.Location = new System.Drawing.Point(317, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(916, 43);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonPreviousPage
            // 
            this.toolStripButtonPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPreviousPage.Image")));
            this.toolStripButtonPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviousPage.Name = "toolStripButtonPreviousPage";
            this.toolStripButtonPreviousPage.Size = new System.Drawing.Size(133, 40);
            this.toolStripButtonPreviousPage.Text = "<< Previous Page ";
            this.toolStripButtonPreviousPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonPreviousPage.Click += new System.EventHandler(this.toolStripButtonPreviousPage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripLabelCurrentPage
            // 
            this.toolStripLabelCurrentPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabelCurrentPage.Name = "toolStripLabelCurrentPage";
            this.toolStripLabelCurrentPage.Size = new System.Drawing.Size(101, 40);
            this.toolStripLabelCurrentPage.Text = "Current Page: ";
            // 
            // toolStripTextBoxPageNumber
            // 
            this.toolStripTextBoxPageNumber.Enabled = false;
            this.toolStripTextBoxPageNumber.Name = "toolStripTextBoxPageNumber";
            this.toolStripTextBoxPageNumber.Size = new System.Drawing.Size(100, 43);
            this.toolStripTextBoxPageNumber.Text = "1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonNextPage
            // 
            this.toolStripButtonNextPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNextPage.Image")));
            this.toolStripButtonNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextPage.Name = "toolStripButtonNextPage";
            this.toolStripButtonNextPage.Size = new System.Drawing.Size(105, 40);
            this.toolStripButtonNextPage.Text = "Next Page >>";
            this.toolStripButtonNextPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonNextPage.Click += new System.EventHandler(this.toolStripButtonNextPage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(76, 40);
            this.toolStripLabel1.Text = "Page Size:";
            // 
            // toolStripTextBoxPageSize
            // 
            this.toolStripTextBoxPageSize.Name = "toolStripTextBoxPageSize";
            this.toolStripTextBoxPageSize.Size = new System.Drawing.Size(100, 43);
            this.toolStripTextBoxPageSize.Text = "100";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(62, 40);
            this.toolStripButton1.Text = "Refresh";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20});
            this.lvWorkspaceItems.Location = new System.Drawing.Point(317, 71);
            this.lvWorkspaceItems.MultiSelect = false;
            this.lvWorkspaceItems.Name = "lvWorkspaceItems";
            this.lvWorkspaceItems.Size = new System.Drawing.Size(483, 607);
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
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(800, 71);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(433, 3);
            this.splitter3.TabIndex = 11;
            this.splitter3.TabStop = false;
            // 
            // splitter4
            // 
            this.splitter4.Location = new System.Drawing.Point(800, 74);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(10, 604);
            this.splitter4.TabIndex = 12;
            this.splitter4.TabStop = false;
            // 
            // props
            // 
            this.props.Dock = System.Windows.Forms.DockStyle.Top;
            this.props.Location = new System.Drawing.Point(810, 74);
            this.props.Margin = new System.Windows.Forms.Padding(4);
            this.props.Name = "props";
            this.props.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.props.Size = new System.Drawing.Size(423, 291);
            this.props.TabIndex = 13;
            // 
            // splitter5
            // 
            this.splitter5.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter5.Location = new System.Drawing.Point(810, 365);
            this.splitter5.Name = "splitter5";
            this.splitter5.Size = new System.Drawing.Size(423, 10);
            this.splitter5.TabIndex = 14;
            this.splitter5.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCheckout,
            this.toolStripButtonUndoCheckout,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButtonDownload});
            this.toolStrip2.Location = new System.Drawing.Point(810, 375);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(423, 43);
            this.toolStrip2.TabIndex = 16;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonCheckout
            // 
            this.toolStripButtonCheckout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCheckout.Image")));
            this.toolStripButtonCheckout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCheckout.Name = "toolStripButtonCheckout";
            this.toolStripButtonCheckout.Size = new System.Drawing.Size(76, 40);
            this.toolStripButtonCheckout.Text = "CheckOut";
            this.toolStripButtonCheckout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonCheckout.Click += new System.EventHandler(this.toolStripButtonCheckout_Click);
            // 
            // toolStripButtonUndoCheckout
            // 
            this.toolStripButtonUndoCheckout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndoCheckout.Image")));
            this.toolStripButtonUndoCheckout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndoCheckout.Name = "toolStripButtonUndoCheckout";
            this.toolStripButtonUndoCheckout.Size = new System.Drawing.Size(112, 40);
            this.toolStripButtonUndoCheckout.Text = "UndoCheckOut";
            this.toolStripButtonUndoCheckout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonUndoCheckout.Click += new System.EventHandler(this.toolStripButtonUndoCheckout_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(64, 40);
            this.toolStripButton4.Text = "CheckIn";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(57, 40);
            this.toolStripButton5.Text = "Delete";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButtonDownload
            // 
            this.toolStripButtonDownload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDownload.Image")));
            this.toolStripButtonDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDownload.Name = "toolStripButtonDownload";
            this.toolStripButtonDownload.Size = new System.Drawing.Size(82, 40);
            this.toolStripButtonDownload.Text = "Download";
            this.toolStripButtonDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDownload.Click += new System.EventHandler(this.toolStripButtonDownload_Click);
            // 
            // listViewAttachments
            // 
            this.listViewAttachments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileId,
            this.FileTitile,
            this.FileVersion,
            this.FileStatus});
            this.listViewAttachments.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewAttachments.FullRowSelect = true;
            this.listViewAttachments.GridLines = true;
            this.listViewAttachments.HideSelection = false;
            this.listViewAttachments.Location = new System.Drawing.Point(810, 418);
            this.listViewAttachments.MultiSelect = false;
            this.listViewAttachments.Name = "listViewAttachments";
            this.listViewAttachments.Size = new System.Drawing.Size(423, 97);
            this.listViewAttachments.TabIndex = 17;
            this.listViewAttachments.UseCompatibleStateImageBehavior = false;
            this.listViewAttachments.View = System.Windows.Forms.View.Details;
            // 
            // FileId
            // 
            this.FileId.Text = "Id";
            // 
            // FileTitile
            // 
            this.FileTitile.Text = "Title";
            // 
            // FileVersion
            // 
            this.FileVersion.Text = "Version";
            // 
            // FileStatus
            // 
            this.FileStatus.Text = "Status";
            // 
            // toolStripButtonDeleteItem
            // 
            this.toolStripButtonDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeleteItem.Image")));
            this.toolStripButtonDeleteItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteItem.Name = "toolStripButtonDeleteItem";
            this.toolStripButtonDeleteItem.Size = new System.Drawing.Size(91, 40);
            this.toolStripButtonDeleteItem.Text = "Delete Item";
            this.toolStripButtonDeleteItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDeleteItem.Click += new System.EventHandler(this.toolStripButtonDeleteItem_Click);
            // 
            // toolStripButtonCloneItem
            // 
            this.toolStripButtonCloneItem.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCloneItem.Image")));
            this.toolStripButtonCloneItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCloneItem.Name = "toolStripButtonCloneItem";
            this.toolStripButtonCloneItem.Size = new System.Drawing.Size(85, 40);
            this.toolStripButtonCloneItem.Text = "Clone Item";
            this.toolStripButtonCloneItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonCloneItem.Click += new System.EventHandler(this.toolStripButtonCloneItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 678);
            this.Controls.Add(this.listViewAttachments);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.splitter5);
            this.Controls.Add(this.props);
            this.Controls.Add(this.splitter4);
            this.Controls.Add(this.splitter3);
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
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
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
        private System.Windows.Forms.ToolStripLabel toolStripLabelCurrentPage;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxPageNumber;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem refreshWorkspacesToolStripMenuItemRefreshWorkspaces;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.PropertyGrid props;
        private System.Windows.Forms.Splitter splitter5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonCheckout;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndoCheckout;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ListView listViewAttachments;
        private System.Windows.Forms.ColumnHeader FileId;
        private System.Windows.Forms.ColumnHeader FileTitile;
        private System.Windows.Forms.ColumnHeader FileVersion;
        private System.Windows.Forms.ColumnHeader FileStatus;
        private System.Windows.Forms.ToolStripButton toolStripButtonDownload;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonCloneItem;
    }
}

