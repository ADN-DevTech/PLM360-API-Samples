using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

                //Get item attachments
                BindItemAttachment(fullItem);
                
                Cursor.Current = Cursors.Default;
            }
        }

        private void BindItemAttachment(Item item)
        {
            PagedCollection<File> attachments = plmSvc.GetAttachments(item);
            if (attachments == null) return;

            listViewAttachments.BeginUpdate();
            listViewAttachments.Items.Clear();

            foreach (File file in attachments.elements)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = file;
                lvi.Text = file.id.ToString();
                string[] values = new string[] 
                {
                    file.title + "",
                    file.version + "",
                    file.status
                };
                lvi.SubItems.AddRange(values);
                listViewAttachments.Items.Add(lvi);

            }

            listViewAttachments.EndUpdate();
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

        private void toolStripButtonCheckout_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewAttachments.SelectedItems)
            {
                File file = (File)lvi.Tag;

                File checkedOutFile = plmSvc.CheckoutFile(file);

                if (checkedOutFile != null && checkedOutFile.status.ToUpper() == "CHECKEDOUT")
                {

                    lvi.SubItems[3].Text = "CHECKEDOUT";

                   DialogResult dr = MessageBox.Show("file " + checkedOutFile.title + " is checked out. \n Do you want to download it?","Check Out",MessageBoxButtons.YesNo);
                   if (dr == System.Windows.Forms.DialogResult.Yes)
                   {
                       DownloadFile(checkedOutFile);
                   }
                }
                
            }

           

        }

        private void toolStripButtonDownload_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewAttachments.SelectedItems)
            {
                File file = (File)lvi.Tag;

                DownloadFile(file);

            }

        }


        private void DownloadFile(File file)
        {

            saveFileDialog1.FileName = file.fileName; //default filename
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //TODO: 

            }

        }


        private void toolStripButtonUndoCheckout_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewAttachments.SelectedItems)
            {
                File file = (File)lvi.Tag;

                if (plmSvc.UndoCheckout(file))
                {
                    lvi.SubItems[3].Text = "CHECKEDIN";
                    MessageBox.Show("File checkout undo success. File lock is released.");
                }
                else
                {
                    MessageBox.Show("There are some problems when trying to undo checkout.");
                }

            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewAttachments.SelectedItems)
            {
                File file = (File)lvi.Tag;

                if (MessageBox.Show("Are you sure to delete file : "+ file.title + "?" , "Delete File", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    bool success = plmSvc.DeleteFile(file);
                    if (success)
                    {
                        listViewAttachments.Items.Remove(lvi);
                    }
                    else
                    {
                        MessageBox.Show("There are some problems when trying to delete file");
                    }
                }
            }
        }

        private void toolStripButtonDeleteItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvWorkspaceItems.SelectedItems)
            {

                Item item = (Item)lvi.Tag;
                if (MessageBox.Show("Are you sure to delete item : " + item.itemDescriptor + "?", "Delete Item", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    bool success = plmSvc.DeleteItem(item);
                    if (success)
                    {
                        lvWorkspaceItems.Items.Remove(lvi);
                    }
                    else
                    {
                        MessageBox.Show("There are some problems when trying to delete item");
                    }
                }
            }
        }

        private void toolStripButtonCloneItem_Click(object sender, EventArgs e)
        {
            //foreach (ListViewItem lvi in lvWorkspaceItems.SelectedItems)
            //{

            //    Item item = (Item)lvi.Tag;

            //    ItemDetail newItem = new ItemDetail
            //    {
            //        //id = item.id,
            //        deleted = false,
            //        fields = item.fields,
            //        isLatestVersion = true,
            //        isWorkingVersion = true,
            //        itemDescriptor = item.itemDescriptor + " - Clone",
            //        rootId = item.rootId,
            //        picklistFields = item.picklistFields,
            //        revision = item.revision,
            //        //url = item.url,
            //        version = item.version,
            //        //workspaceId = item.workspaceId

            //    };

            //    ItemDetail nweAddedItem = plmSvc.AddItem(item.workspaceId,newItem);

            //}

        }

    }
}
