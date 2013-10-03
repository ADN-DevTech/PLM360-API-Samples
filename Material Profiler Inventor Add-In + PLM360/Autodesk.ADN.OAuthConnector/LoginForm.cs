using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autodesk.ADN.OAuthConnector
{
    partial class LoginForm : Form
    {
        public LoginForm(Uri loginUri, Uri targetUri)
        {
            InitializeComponent();

            LoginUri = loginUri;

            TargetUri = targetUri;

            DialogResult = DialogResult.Cancel;

            _webBrowser.Navigated += OnFirstNavigated;

            Load += OnLoad;
        }

        public Uri LoginUri
        {
            get;
            private set;
        }

        public Uri ResultUri
        {
            get;
            private set;
        }

        public Uri TargetUri
        {
            get;
            private set;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _webBrowser.Url = LoginUri;
        }

        void OnFirstNavigated(
            object sender,
            WebBrowserNavigatedEventArgs e)
        {
            _webBrowser.Navigated -= OnFirstNavigated;
            _webBrowser.Navigated += OnNavigated;
        }

        void OnNavigated(
            object sender,
            WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.AbsoluteUri.StartsWith(TargetUri.AbsoluteUri))
            {
                ResultUri = e.Url;
                DialogResult = DialogResult.OK;

                Close();
            }
            else
            {
                DialogResult = DialogResult.No;

                Close();
            }
        }
    }
}
