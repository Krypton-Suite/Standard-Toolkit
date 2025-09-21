using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace BackstageDemo.UserControls
{
    partial class HelpPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleLabel = new Krypton.Toolkit.KryptonLabel();
            this.versionLabel = new Krypton.Toolkit.KryptonLabel();
            this.helpOptionsLabel = new Krypton.Toolkit.KryptonLabel();
            this.documentationButton = new Krypton.Toolkit.KryptonButton();
            this.tutorialsButton = new Krypton.Toolkit.KryptonButton();
            this.supportButton = new Krypton.Toolkit.KryptonButton();
            this.systemInfoLabel = new Krypton.Toolkit.KryptonLabel();
            this.osInfo = new Krypton.Toolkit.KryptonLabel();
            this.frameworkInfo = new Krypton.Toolkit.KryptonLabel();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(30, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(400, 40);
            this.titleLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Help & Support";
            // 
            // versionLabel
            // 
            this.versionLabel.Location = new System.Drawing.Point(30, 80);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(300, 25);
            this.versionLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "Krypton Backstage Demo v1.0";
            // 
            // helpOptionsLabel
            // 
            this.helpOptionsLabel.Location = new System.Drawing.Point(30, 130);
            this.helpOptionsLabel.Name = "helpOptionsLabel";
            this.helpOptionsLabel.Size = new System.Drawing.Size(100, 25);
            this.helpOptionsLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.helpOptionsLabel.TabIndex = 2;
            this.helpOptionsLabel.Text = "Get Help:";
            // 
            // documentationButton
            // 
            this.documentationButton.Location = new System.Drawing.Point(30, 160);
            this.documentationButton.Name = "documentationButton";
            this.documentationButton.Size = new System.Drawing.Size(150, 35);
            this.documentationButton.TabIndex = 3;
            this.documentationButton.Text = "View Documentation";
            this.documentationButton.Click += new System.EventHandler(this.DocumentationButton_Click);
            // 
            // tutorialsButton
            // 
            this.tutorialsButton.Location = new System.Drawing.Point(200, 160);
            this.tutorialsButton.Name = "tutorialsButton";
            this.tutorialsButton.Size = new System.Drawing.Size(150, 35);
            this.tutorialsButton.TabIndex = 4;
            this.tutorialsButton.Text = "Video Tutorials";
            this.tutorialsButton.Click += new System.EventHandler(this.TutorialsButton_Click);
            // 
            // supportButton
            // 
            this.supportButton.Location = new System.Drawing.Point(370, 160);
            this.supportButton.Name = "supportButton";
            this.supportButton.Size = new System.Drawing.Size(150, 35);
            this.supportButton.TabIndex = 5;
            this.supportButton.Text = "Contact Support";
            this.supportButton.Click += new System.EventHandler(this.SupportButton_Click);
            // 
            // systemInfoLabel
            // 
            this.systemInfoLabel.Location = new System.Drawing.Point(30, 230);
            this.systemInfoLabel.Name = "systemInfoLabel";
            this.systemInfoLabel.Size = new System.Drawing.Size(150, 25);
            this.systemInfoLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.systemInfoLabel.TabIndex = 6;
            this.systemInfoLabel.Text = "System Information:";
            // 
            // osInfo
            // 
            this.osInfo.Location = new System.Drawing.Point(30, 260);
            this.osInfo.Name = "osInfo";
            this.osInfo.Size = new System.Drawing.Size(400, 25);
            this.osInfo.TabIndex = 7;
            this.osInfo.Text = "OS: Loading...";
            // 
            // frameworkInfo
            // 
            this.frameworkInfo.Location = new System.Drawing.Point(30, 285);
            this.frameworkInfo.Name = "frameworkInfo";
            this.frameworkInfo.Size = new System.Drawing.Size(400, 25);
            this.frameworkInfo.TabIndex = 8;
            this.frameworkInfo.Text = ".NET Version: Loading...";
            // 
            // HelpPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.frameworkInfo);
            this.Controls.Add(this.osInfo);
            this.Controls.Add(this.systemInfoLabel);
            this.Controls.Add(this.supportButton);
            this.Controls.Add(this.tutorialsButton);
            this.Controls.Add(this.documentationButton);
            this.Controls.Add(this.helpOptionsLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "HelpPage";
            this.Size = new System.Drawing.Size(600, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonLabel titleLabel;
        private KryptonLabel versionLabel;
        private KryptonLabel helpOptionsLabel;
        private KryptonButton documentationButton;
        private KryptonButton tutorialsButton;
        private KryptonButton supportButton;
        private KryptonLabel systemInfoLabel;
        private KryptonLabel osInfo;
        private KryptonLabel frameworkInfo;
    }
}
