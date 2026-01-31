#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
#if WEBVIEW2_AVAILABLE
    partial class KryptonWebView2Test
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonWebView21 = new Krypton.Utilities.KryptonWebView2();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnRefresh = new Krypton.Toolkit.KryptonButton();
            this.kbtnForward = new Krypton.Toolkit.KryptonButton();
            this.kbtnBack = new Krypton.Toolkit.KryptonButton();
            this.kbtnNavigate = new Krypton.Toolkit.KryptonButton();
            this.kryptonTextBox1 = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonWebView21);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonPanel1.Size = new System.Drawing.Size(1000, 700);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonWebView21
            // 
            this.kryptonWebView21.AllowExternalDrop = true;
            this.kryptonWebView21.CreationProperties = null;
            this.kryptonWebView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.kryptonWebView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonWebView21.Location = new System.Drawing.Point(0, 60);
            this.kryptonWebView21.Name = "kryptonWebView21";
            this.kryptonWebView21.Size = new System.Drawing.Size(1000, 640);
            this.kryptonWebView21.TabIndex = 2;
            this.kryptonWebView21.ZoomFactor = 1D;
            this.kryptonWebView21.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.kryptonWebView21_NavigationCompleted);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 60);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(1000, 640);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Text = "Initializing WebView2...";
            this.kryptonLabel1.Values.Text = "Initializing WebView2...";
            this.kryptonLabel1.Visible = false;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kbtnRefresh);
            this.kryptonPanel2.Controls.Add(this.kbtnForward);
            this.kryptonPanel2.Controls.Add(this.kbtnBack);
            this.kryptonPanel2.Controls.Add(this.kbtnNavigate);
            this.kryptonPanel2.Controls.Add(this.kryptonTextBox1);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonPanel2.Size = new System.Drawing.Size(1000, 60);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // kbtnRefresh
            // 
            this.kbtnRefresh.Location = new System.Drawing.Point(450, 30);
            this.kbtnRefresh.Name = "kbtnRefresh";
            this.kbtnRefresh.Size = new System.Drawing.Size(75, 25);
            this.kbtnRefresh.TabIndex = 5;
            this.kbtnRefresh.Values.Text = "Refresh";
            this.kbtnRefresh.Click += new System.EventHandler(this.kbtnRefresh_Click);
            // 
            // kbtnForward
            // 
            this.kbtnForward.Location = new System.Drawing.Point(370, 30);
            this.kbtnForward.Name = "kbtnForward";
            this.kbtnForward.Size = new System.Drawing.Size(75, 25);
            this.kbtnForward.TabIndex = 4;
            this.kbtnForward.Values.Text = "Forward";
            this.kbtnForward.Click += new System.EventHandler(this.kbtnForward_Click);
            // 
            // kbtnBack
            // 
            this.kbtnBack.Location = new System.Drawing.Point(290, 30);
            this.kbtnBack.Name = "kbtnBack";
            this.kbtnBack.Size = new System.Drawing.Size(75, 25);
            this.kbtnBack.TabIndex = 3;
            this.kbtnBack.Values.Text = "Back";
            this.kbtnBack.Click += new System.EventHandler(this.kbtnBack_Click);
            // 
            // kbtnNavigate
            // 
            this.kbtnNavigate.Location = new System.Drawing.Point(530, 30);
            this.kbtnNavigate.Name = "kbtnNavigate";
            this.kbtnNavigate.Size = new System.Drawing.Size(75, 25);
            this.kbtnNavigate.TabIndex = 2;
            this.kbtnNavigate.Values.Text = "Navigate";
            this.kbtnNavigate.Click += new System.EventHandler(this.kbtnNavigate_Click);
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.Location = new System.Drawing.Point(80, 32);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.Size = new System.Drawing.Size(200, 23);
            this.kryptonTextBox1.TabIndex = 1;
            this.kryptonTextBox1.Text = "https://www.microsoft.com";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(10, 35);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(64, 20);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Values.Text = "Address:";
            // 
            // KryptonWebView2Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "KryptonWebView2Test";
            this.Text = "KryptonWebView2 Test";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Utilities.KryptonWebView2 kryptonWebView21;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonButton kbtnRefresh;
        private Krypton.Toolkit.KryptonButton kbtnForward;
        private Krypton.Toolkit.KryptonButton kbtnBack;
        private Krypton.Toolkit.KryptonButton kbtnNavigate;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
    }
#endif
}
