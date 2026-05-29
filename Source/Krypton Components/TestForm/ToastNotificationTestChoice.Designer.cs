#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class ToastNotificationTestChoice
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnQuickNotificationTest = new Krypton.Toolkit.KryptonButton();
            this.kbtnUserInputNotification = new Krypton.Toolkit.KryptonButton();
            this.kbtnBasicNotification = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnQuickNotificationTest);
            this.kryptonPanel1.Controls.Add(this.kbtnUserInputNotification);
            this.kryptonPanel1.Controls.Add(this.kbtnBasicNotification);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(326, 225);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnQuickNotificationTest
            // 
            this.kbtnQuickNotificationTest.Location = new System.Drawing.Point(13, 75);
            this.kbtnQuickNotificationTest.Name = "kbtnQuickNotificationTest";
            this.kbtnQuickNotificationTest.Size = new System.Drawing.Size(291, 25);
            this.kbtnQuickNotificationTest.TabIndex = 2;
            this.kbtnQuickNotificationTest.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnQuickNotificationTest.Values.Text = "Quick Notification Test";
            this.kbtnQuickNotificationTest.Click += new System.EventHandler(this.kbtnQuickNotificationTest_Click);
            // 
            // kbtnUserInputNotification
            // 
            this.kbtnUserInputNotification.Location = new System.Drawing.Point(13, 44);
            this.kbtnUserInputNotification.Name = "kbtnUserInputNotification";
            this.kbtnUserInputNotification.Size = new System.Drawing.Size(291, 25);
            this.kbtnUserInputNotification.TabIndex = 1;
            this.kbtnUserInputNotification.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnUserInputNotification.Values.Text = "User Input Notification";
            this.kbtnUserInputNotification.Click += new System.EventHandler(this.kbtnUserInputNotification_Click);
            // 
            // kbtnBasicNotification
            // 
            this.kbtnBasicNotification.Location = new System.Drawing.Point(13, 13);
            this.kbtnBasicNotification.Name = "kbtnBasicNotification";
            this.kbtnBasicNotification.Size = new System.Drawing.Size(291, 25);
            this.kbtnBasicNotification.TabIndex = 0;
            this.kbtnBasicNotification.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnBasicNotification.Values.Text = "Basic Notification";
            this.kbtnBasicNotification.Click += new System.EventHandler(this.kbtnBasicNotification_Click);
            // 
            // ToastNotificationTestChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 225);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ToastNotificationTestChoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ToastNotificationTestChoice";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kbtnBasicNotification;
        private Krypton.Toolkit.KryptonButton kbtnUserInputNotification;
        private Krypton.Toolkit.KryptonButton kbtnQuickNotificationTest;
    }
}