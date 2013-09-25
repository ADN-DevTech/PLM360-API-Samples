using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.Adn.OAuthentication;
using Autodesk.Adn.PLM360API;

namespace Autodesk.Adn.PLM360RestAPISample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool authenticated = false;

        // Hard coded consumer and secret keys and base URL.
        // In real world Apps, these values need to secured.
        // One approach is to encrypt and/or obfuscate these values
        private const string m_ConsumerKey = "place holder";
        private const string m_ConsumerSecret = "place holder";
        private const string m_baseURL = "place holder";

        private const string tanentName = "place holder";


        private long currentWorkspaceId = 0;
        PLM360RestService plmSvc;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItemLogin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            OAuthService oauthSvc = new OAuthService(m_ConsumerKey, m_ConsumerSecret, m_baseURL);
            authenticated = oauthSvc.StartOAuth();
            if (!authenticated)
            {
                MessageBox.Show("authentication failed.");
                return;
            }

            plmSvc = new PLM360RestService(tanentName, oauthSvc);

            Session session = plmSvc.Login();
            if (session == null)
            {
                MessageBox.Show("Login to PLM360 failed.");
                return;
            }

            BindWorkspaces();

            Cursor.Current = Cursors.Default;

        }

        private void BindWorkspaces()
        {
            PagedCollection<Workspace> workSpaces = plmSvc.GetWorkspaces();
            if (workSpaces != null)
            {
                // Suppress repainting the TreeView until all the objects have been created.
                tvWorkspaces.BeginUpdate();

                // Clear the TreeView each time the method is called.
                tvWorkspaces.Nodes.Clear();
                TreeNode root = new TreeNode("All Workspaces");
                tvWorkspaces.Nodes.Add(root);

                // Add a root TreeNode for each Customer object in the ArrayList. 
                foreach (Workspace workspace in workSpaces.elements)
                {
                    TreeNode node = new TreeNode();
                    node.Tag = workspace;
                    node.Text = workspace.displayName;
                    node.ToolTipText = workspace.description;
                    root.Nodes.Add(node);
                }

                // Reset the cursor to the default for all controls.
                Cursor.Current = Cursors.Default;

                // Begin repainting the TreeView.
                tvWorkspaces.EndUpdate();
                root.Expand();
            }

        }

        private void tvWorkspaces_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //fetch items and show them in listview
            TreeNode node = tvWorkspaces.SelectedNode;
            if (node.Tag == null) return;
            Workspace wkspace = (Workspace)node.Tag;
            if (wkspace == null) return;
            
            currentWorkspaceId = wkspace.id;

            Cursor.Current = Cursors.WaitCursor;

            long page = 1;
            long.TryParse(toolStripTextBoxPageNumber.Text.Trim(), out page);
            long pageSize = 100;
            long.TryParse(toolStripTextBoxPageSize.Text.Trim(), out pageSize);

            DisplayWorkItems(currentWorkspaceId,page, pageSize);
            

            Cursor.Current = Cursors.Default;
            

            
        }

        private void DisplayWorkItems(long workspaceId, long? page, long? pageSize)
        {
            lvWorkspaceItems.BeginUpdate();
            lvWorkspaceItems.Items.Clear();
            PagedCollection<Item> items = plmSvc.GetItems(workspaceId, page, pageSize);
            if (items == null) return;

            foreach (Item item in items.elements)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = item;
                lvi.Text = item.id.ToString();
                string[] values = new string[] 
                {
                    item.version + "",
                    item.revision + "",
                    item.itemDescriptor + "",
                    (item.deleted.HasValue && item.deleted == true ?"Deleted":"")
                };
                lvi.SubItems.AddRange(values);
                lvWorkspaceItems.Items.Add(lvi);
            }

            lvWorkspaceItems.EndUpdate();

            // clear the properties pane
            props.SelectedObject = null;
        }

        private void lvWorkspaceItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //add properties
            foreach (ListViewItem lvi in lvWorkspaceItems.SelectedItems)
            {
                Cursor.Current = Cursors.WaitCursor;
                Item partialItem = (Item)lvi.Tag;
                if (partialItem == null) continue;

                Item fullItem = plmSvc.GetItem(partialItem.workspaceId, partialItem.id);
                if (fullItem != null)
                {
                    props.SelectedObject = new ItemPropertyGrid(fullItem);
                }
                
                Cursor.Current = Cursors.Default;
            }
        }

        private void logoutToolStripMenuItemLogout_Click(object sender, EventArgs e)
        {
            plmSvc.Logout();
        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            long page;
            long.TryParse(toolStripTextBoxPageNumber.Text.Trim(), out page);
            toolStripTextBoxPageNumber.Text = (++page).ToString();

            long pageSize;
            long.TryParse(toolStripTextBoxPageSize.Text.Trim(), out pageSize);

            DisplayWorkItems(currentWorkspaceId, page, pageSize);
        }

        private void toolStripButtonPreviousPage_Click(object sender, EventArgs e)
        {
            long page;
            long.TryParse(toolStripTextBoxPageNumber.Text.Trim(), out page);
            if (page > 1) page--;
            toolStripTextBoxPageNumber.Text = page.ToString();

            long pageSize;
            long.TryParse(toolStripTextBoxPageSize.Text.Trim(), out pageSize);

            DisplayWorkItems(currentWorkspaceId, page, pageSize);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            long page;
            long.TryParse(toolStripTextBoxPageNumber.Text.Trim(), out page);
            long pageSize;
            long.TryParse(toolStripTextBoxPageSize.Text.Trim(), out pageSize);

            DisplayWorkItems(currentWorkspaceId, page, pageSize);
        }

        private void refreshWorkspacesToolStripMenuItemRefreshWorkspaces_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            BindWorkspaces();

            Cursor.Current = Cursors.Default;
        }


    }
}
