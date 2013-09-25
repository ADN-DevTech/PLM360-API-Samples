namespace Autodesk.Adn.OAuthentication
{
    partial class LoginForm
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
            this.loginBrowser = new System.Windows.Forms.WebBrowser();
            this.login = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // loginBrowser
            // 
            this.loginBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginBrowser.Location = new System.Drawing.Point(0, 0);
            this.loginBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.loginBrowser.Name = "loginBrowser";
            this.loginBrowser.Size = new System.Drawing.Size(757, 549);
            this.loginBrowser.TabIndex = 0;
            this.loginBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.loginBrowser_DocumentCompleted);
            this.loginBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.loginBrowser_Navigated);
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(190, 244);
            this.login.MarqueeAnimationSpeed = 5;
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(350, 23);
            this.login.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.login.TabIndex = 2;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(757, 549);
            this.Controls.Add(this.loginBrowser);
            this.Controls.Add(this.login);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser loginBrowser;
        private System.Windows.Forms.ProgressBar login;
    }
}