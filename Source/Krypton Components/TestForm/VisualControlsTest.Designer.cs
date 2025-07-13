#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;
using System.Windows.Forms;

namespace TestForm
{
    partial class VisualControlsTest
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnVisualToastNotification = new Krypton.Toolkit.KryptonButton();
            this.kbtnVisualThemeBrowserRtlAware = new Krypton.Toolkit.KryptonButton();
            this.kbtnVisualThemeBrowser = new Krypton.Toolkit.KryptonButton();
            this.kbtnVisualTaskDialog = new Krypton.Toolkit.KryptonButton();
            this.kbtnVisualTaskDialogForm = new Krypton.Toolkit.KryptonButton();
            this.kbtnVisualSplashScreen = new Krypton.Toolkit.KryptonButton();
            this.kbtnVisualMultilineStringEditorForm = new Krypton.Toolkit.KryptonButton();
            this.kbtnVisualInformationBoxForm = new Krypton.Toolkit.KryptonButton();
            this.kbtnModalWaitDialog = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnVisualToastNotification);
            this.kryptonPanel1.Controls.Add(this.kbtnVisualThemeBrowserRtlAware);
            this.kryptonPanel1.Controls.Add(this.kbtnVisualThemeBrowser);
            this.kryptonPanel1.Controls.Add(this.kbtnVisualTaskDialog);
            this.kryptonPanel1.Controls.Add(this.kbtnVisualTaskDialogForm);
            this.kryptonPanel1.Controls.Add(this.kbtnVisualSplashScreen);
            this.kryptonPanel1.Controls.Add(this.kbtnVisualMultilineStringEditorForm);
            this.kryptonPanel1.Controls.Add(this.kbtnVisualInformationBoxForm);
            this.kryptonPanel1.Controls.Add(this.kbtnModalWaitDialog);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(378, 363);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnVisualToastNotification
            // 
            this.kbtnVisualToastNotification.Location = new System.Drawing.Point(18, 278);
            this.kbtnVisualToastNotification.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnVisualToastNotification.Name = "kbtnVisualToastNotification";
            this.kbtnVisualToastNotification.Size = new System.Drawing.Size(347, 31);
            this.kbtnVisualToastNotification.TabIndex = 8;
            this.kbtnVisualToastNotification.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVisualToastNotification.Values.Text = "VisualToastNotificationBasicForm";
            this.kbtnVisualToastNotification.Click += new System.EventHandler(this.kbtnVisualToastNotification_Click);
            // 
            // kbtnVisualThemeBrowserRtlAware
            // 
            this.kbtnVisualThemeBrowserRtlAware.Location = new System.Drawing.Point(18, 239);
            this.kbtnVisualThemeBrowserRtlAware.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnVisualThemeBrowserRtlAware.Name = "kbtnVisualThemeBrowserRtlAware";
            this.kbtnVisualThemeBrowserRtlAware.Size = new System.Drawing.Size(347, 31);
            this.kbtnVisualThemeBrowserRtlAware.TabIndex = 7;
            this.kbtnVisualThemeBrowserRtlAware.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVisualThemeBrowserRtlAware.Values.Text = "VisualThemeBrowserFormRtlAware";
            this.kbtnVisualThemeBrowserRtlAware.Click += new System.EventHandler(this.kbtnVisualThemeBrowserRtlAware_Click);
            // 
            // kbtnVisualThemeBrowser
            // 
            this.kbtnVisualThemeBrowser.Location = new System.Drawing.Point(18, 200);
            this.kbtnVisualThemeBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnVisualThemeBrowser.Name = "kbtnVisualThemeBrowser";
            this.kbtnVisualThemeBrowser.Size = new System.Drawing.Size(347, 31);
            this.kbtnVisualThemeBrowser.TabIndex = 6;
            this.kbtnVisualThemeBrowser.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVisualThemeBrowser.Values.Text = "VisualThemeBrowserForm";
            this.kbtnVisualThemeBrowser.Click += new System.EventHandler(this.kbtnVisualThemeBrowser_Click);
            // 
            // kbtnVisualTaskDialog
            // 
            this.kbtnVisualTaskDialog.Location = new System.Drawing.Point(18, 161);
            this.kbtnVisualTaskDialog.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnVisualTaskDialog.Name = "kbtnVisualTaskDialog";
            this.kbtnVisualTaskDialog.Size = new System.Drawing.Size(347, 31);
            this.kbtnVisualTaskDialog.TabIndex = 4;
            this.kbtnVisualTaskDialog.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVisualTaskDialog.Values.Text = "VisualTaskDialog";
            this.kbtnVisualTaskDialog.Click += new System.EventHandler(this.kbtnVisualTaskDialog_Click);
            // 
            // kbtnVisualTaskDialogForm
            // 
            this.kbtnVisualTaskDialogForm.Location = new System.Drawing.Point(18, 124);
            this.kbtnVisualTaskDialogForm.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnVisualTaskDialogForm.Name = "kbtnVisualTaskDialogForm";
            this.kbtnVisualTaskDialogForm.Size = new System.Drawing.Size(347, 31);
            this.kbtnVisualTaskDialogForm.TabIndex = 3;
            this.kbtnVisualTaskDialogForm.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVisualTaskDialogForm.Values.Text = "VisualTaskDialogForm";
            this.kbtnVisualTaskDialogForm.Click += new System.EventHandler(this.kbtnVisualTaskDialogForm_Click);
            // 
            // kbtnVisualSplashScreen
            // 
            this.kbtnVisualSplashScreen.Location = new System.Drawing.Point(18, 87);
            this.kbtnVisualSplashScreen.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnVisualSplashScreen.Name = "kbtnVisualSplashScreen";
            this.kbtnVisualSplashScreen.Size = new System.Drawing.Size(347, 31);
            this.kbtnVisualSplashScreen.TabIndex = 2;
            this.kbtnVisualSplashScreen.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVisualSplashScreen.Values.Text = "VisualSplashScreenForm";
            this.kbtnVisualSplashScreen.Click += new System.EventHandler(this.kbtnVisualSplashScreen_Click);
            // 
            // kbtnVisualMultilineStringEditorForm
            // 
            this.kbtnVisualMultilineStringEditorForm.Location = new System.Drawing.Point(18, 50);
            this.kbtnVisualMultilineStringEditorForm.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnVisualMultilineStringEditorForm.Name = "kbtnVisualMultilineStringEditorForm";
            this.kbtnVisualMultilineStringEditorForm.Size = new System.Drawing.Size(347, 31);
            this.kbtnVisualMultilineStringEditorForm.TabIndex = 1;
            this.kbtnVisualMultilineStringEditorForm.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVisualMultilineStringEditorForm.Values.Text = "VisualMultilineStringEditorForm";
            this.kbtnVisualMultilineStringEditorForm.Click += new System.EventHandler(this.kbtnVisualMultilineStringEditorForm_Click);
            // 
            // kbtnVisualInformationBoxForm
            // 
            this.kbtnVisualInformationBoxForm.Location = new System.Drawing.Point(18, 13);
            this.kbtnVisualInformationBoxForm.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnVisualInformationBoxForm.Name = "kbtnVisualInformationBoxForm";
            this.kbtnVisualInformationBoxForm.Size = new System.Drawing.Size(347, 31);
            this.kbtnVisualInformationBoxForm.TabIndex = 0;
            this.kbtnVisualInformationBoxForm.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVisualInformationBoxForm.Values.Text = "VisualInformationBoxForm";
            this.kbtnVisualInformationBoxForm.Click += new System.EventHandler(this.kbtnVisualInformationBoxForm_Click);
            // 
            // kbtnModalWaitDialog
            // 
            this.kbtnModalWaitDialog.Location = new System.Drawing.Point(18, 316);
            this.kbtnModalWaitDialog.Name = "kbtnModalWaitDialog";
            this.kbtnModalWaitDialog.Size = new System.Drawing.Size(347, 30);
            this.kbtnModalWaitDialog.TabIndex = 5;
            this.kbtnModalWaitDialog.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnModalWaitDialog.Values.Text = "ModalWaitDialog";
            this.kbtnModalWaitDialog.Click += new System.EventHandler(this.kbtnModalWaitDialog_Click);
            // 
            // VisualControlsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 363);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualControlsTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visual Controls Test";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kbtnVisualInformationBoxForm;
        private KryptonButton kbtnVisualMultilineStringEditorForm;
        private KryptonButton kbtnVisualSplashScreen;
        private KryptonButton kbtnVisualTaskDialogForm;
        private KryptonButton kbtnVisualTaskDialog;
        private KryptonButton kbtnVisualThemeBrowser;
        private KryptonButton kbtnVisualThemeBrowserRtlAware;
        private KryptonButton kbtnVisualToastNotification;
        private KryptonButton kbtnModalWaitDialog;
    }
}