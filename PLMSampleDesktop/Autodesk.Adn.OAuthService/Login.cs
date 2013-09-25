using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace Autodesk.Adn.OAuthentication
{
    public partial class LoginForm : Form
    {
        public Uri Uri { get; set; }
        public Uri ResultUri { get; set; }
        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            if (Uri != null)
            {
                loginBrowser.Url = Uri;
            }
        }

        private void loginBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!e.Url.AbsolutePath.Contains("Allow"))
            {
                return;
            }
            ////extract verifier 
            //HtmlElementCollection inputElements = loginBrowser.Document.GetElementsByTagName("input") ;
            //string verifier = "";
            //foreach (HtmlElement el in inputElements) 
            //{
            //    verifier = el.GetAttribute("value");
            //}


            ResultUri = loginBrowser.Url;
            DialogResult = DialogResult.OK;

        }

        private void loginBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.AbsolutePath.Contains("Allow"))
            {
                //hide the browser to prevent the user from seeing the token page
                loginBrowser.Hide();
                login.Show();
            }

        }

    }
}
