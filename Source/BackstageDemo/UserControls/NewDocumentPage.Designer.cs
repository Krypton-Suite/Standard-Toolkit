using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace BackstageDemo.UserControls
{
    partial class NewDocumentPage
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
            this.descLabel = new Krypton.Toolkit.KryptonLabel();
            this.blankButton = new Krypton.Toolkit.KryptonButton();
            this.templateButton = new Krypton.Toolkit.KryptonButton();
            this.recentButton = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(30, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(400, 40);
            this.titleLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Create a new document";
            // 
            // descLabel
            // 
            this.descLabel.Location = new System.Drawing.Point(30, 80);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(500, 25);
            this.descLabel.TabIndex = 1;
            this.descLabel.Text = "Choose from the options below to create a new document:";
            // 
            // blankButton
            // 
            this.blankButton.Location = new System.Drawing.Point(30, 130);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(150, 100);
            this.blankButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.blankButton.TabIndex = 2;
            this.blankButton.Text = "Blank Document";
            this.blankButton.Click += new System.EventHandler(this.BlankDocButton_Click);
            // 
            // templateButton
            // 
            this.templateButton.Location = new System.Drawing.Point(200, 130);
            this.templateButton.Name = "templateButton";
            this.templateButton.Size = new System.Drawing.Size(150, 100);
            this.templateButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.templateButton.TabIndex = 3;
            this.templateButton.Text = "From Template";
            this.templateButton.Click += new System.EventHandler(this.TemplateButton_Click);
            // 
            // recentButton
            // 
            this.recentButton.Location = new System.Drawing.Point(370, 130);
            this.recentButton.Name = "recentButton";
            this.recentButton.Size = new System.Drawing.Size(150, 100);
            this.recentButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.recentButton.TabIndex = 4;
            this.recentButton.Text = "Recent Files";
            this.recentButton.Click += new System.EventHandler(this.RecentButton_Click);
            // 
            // NewDocumentPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.recentButton);
            this.Controls.Add(this.templateButton);
            this.Controls.Add(this.blankButton);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "NewDocumentPage";
            this.Size = new System.Drawing.Size(600, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonLabel titleLabel;
        private KryptonLabel descLabel;
        private KryptonButton blankButton;
        private KryptonButton templateButton;
        private KryptonButton recentButton;
    }
}
