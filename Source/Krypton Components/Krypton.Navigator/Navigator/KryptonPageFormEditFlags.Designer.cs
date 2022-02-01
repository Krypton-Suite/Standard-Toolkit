#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Navigator
{
    partial class KryptonPageFormEditFlags
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
            this.buttonOK = new Krypton.Toolkit.KryptonButton();
            this.buttonCancel = new Krypton.Toolkit.KryptonButton();
            this.checkBoxPageInOverflowBarForOutlookMode = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxAllowPageDrag = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxDockingAllowClose = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxDockingAllowAutoHidden = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxDockingAllowDocked = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxDockingAllowFloating = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxDockingAllowWorkspace = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxDockingAllowNavigator = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxDockingAllowDropDown = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxAllowPageReorder = new Krypton.Toolkit.KryptonCheckBox();
            this.checkBoxAllowConfigSave = new Krypton.Toolkit.KryptonCheckBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.buttonOK.DialogResult = DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(261, 203);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.buttonCancel.DialogResult = DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(342, 203);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            // 
            // checkBoxPageInOverflowBarForOutlookMode
            // 
            this.checkBoxPageInOverflowBarForOutlookMode.AutoSize = true;
            this.checkBoxPageInOverflowBarForOutlookMode.Location = new System.Drawing.Point(15, 18);
            this.checkBoxPageInOverflowBarForOutlookMode.Name = "checkBoxPageInOverflowBarForOutlookMode";
            this.checkBoxPageInOverflowBarForOutlookMode.Size = new System.Drawing.Size(210, 17);
            this.checkBoxPageInOverflowBarForOutlookMode.TabIndex = 0;
            this.checkBoxPageInOverflowBarForOutlookMode.Text = "Page in Overflow Bar for Outlook mode";
            // 
            // checkBoxAllowPageDrag
            // 
            this.checkBoxAllowPageDrag.AutoSize = true;
            this.checkBoxAllowPageDrag.Location = new System.Drawing.Point(15, 41);
            this.checkBoxAllowPageDrag.Name = "checkBoxAllowPageDrag";
            this.checkBoxAllowPageDrag.Size = new System.Drawing.Size(105, 17);
            this.checkBoxAllowPageDrag.TabIndex = 1;
            this.checkBoxAllowPageDrag.Text = "Allow Page Drag";
            // 
            // checkBoxDockingAllowClose
            // 
            this.checkBoxDockingAllowClose.AutoSize = true;
            this.checkBoxDockingAllowClose.Location = new System.Drawing.Point(256, 18);
            this.checkBoxDockingAllowClose.Name = "checkBoxDockingAllowClose";
            this.checkBoxDockingAllowClose.Size = new System.Drawing.Size(123, 17);
            this.checkBoxDockingAllowClose.TabIndex = 4;
            this.checkBoxDockingAllowClose.Text = "Docking Allow Close";
            // 
            // checkBoxDockingAllowAutoHidden
            // 
            this.checkBoxDockingAllowAutoHidden.AutoSize = true;
            this.checkBoxDockingAllowAutoHidden.Location = new System.Drawing.Point(256, 64);
            this.checkBoxDockingAllowAutoHidden.Name = "checkBoxDockingAllowAutoHidden";
            this.checkBoxDockingAllowAutoHidden.Size = new System.Drawing.Size(153, 17);
            this.checkBoxDockingAllowAutoHidden.TabIndex = 6;
            this.checkBoxDockingAllowAutoHidden.Text = "Docking Allow AutoHidden";
            // 
            // checkBoxDockingAllowDocked
            // 
            this.checkBoxDockingAllowDocked.AutoSize = true;
            this.checkBoxDockingAllowDocked.Location = new System.Drawing.Point(256, 87);
            this.checkBoxDockingAllowDocked.Name = "checkBoxDockingAllowDocked";
            this.checkBoxDockingAllowDocked.Size = new System.Drawing.Size(135, 17);
            this.checkBoxDockingAllowDocked.TabIndex = 7;
            this.checkBoxDockingAllowDocked.Text = "Docking Allow Docked";
            // 
            // checkBoxDockingAllowFloating
            // 
            this.checkBoxDockingAllowFloating.AutoSize = true;
            this.checkBoxDockingAllowFloating.Location = new System.Drawing.Point(256, 110);
            this.checkBoxDockingAllowFloating.Name = "checkBoxDockingAllowFloating";
            this.checkBoxDockingAllowFloating.Size = new System.Drawing.Size(134, 17);
            this.checkBoxDockingAllowFloating.TabIndex = 8;
            this.checkBoxDockingAllowFloating.Text = "Docking Allow Floating";
            // 
            // checkBoxDockingAllowWorkspace
            // 
            this.checkBoxDockingAllowWorkspace.AutoSize = true;
            this.checkBoxDockingAllowWorkspace.Location = new System.Drawing.Point(256, 133);
            this.checkBoxDockingAllowWorkspace.Name = "checkBoxDockingAllowWorkspace";
            this.checkBoxDockingAllowWorkspace.Size = new System.Drawing.Size(152, 17);
            this.checkBoxDockingAllowWorkspace.TabIndex = 9;
            this.checkBoxDockingAllowWorkspace.Text = "Docking Allow Workspace";
            // 
            // checkBoxDockingAllowNavigator
            // 
            this.checkBoxDockingAllowNavigator.AutoSize = true;
            this.checkBoxDockingAllowNavigator.Location = new System.Drawing.Point(256, 156);
            this.checkBoxDockingAllowNavigator.Name = "checkBoxDockingAllowNavigator";
            this.checkBoxDockingAllowNavigator.Size = new System.Drawing.Size(143, 17);
            this.checkBoxDockingAllowNavigator.TabIndex = 10;
            this.checkBoxDockingAllowNavigator.Text = "Docking Allow Navigator";
            // 
            // checkBoxDockingAllowDropDown
            // 
            this.checkBoxDockingAllowDropDown.AutoSize = true;
            this.checkBoxDockingAllowDropDown.Location = new System.Drawing.Point(256, 41);
            this.checkBoxDockingAllowDropDown.Name = "checkBoxDockingAllowDropDown";
            this.checkBoxDockingAllowDropDown.Size = new System.Drawing.Size(148, 17);
            this.checkBoxDockingAllowDropDown.TabIndex = 5;
            this.checkBoxDockingAllowDropDown.Text = "Docking Allow DropDown";
            // 
            // checkBoxAllowPageReorder
            // 
            this.checkBoxAllowPageReorder.AutoSize = true;
            this.checkBoxAllowPageReorder.Location = new System.Drawing.Point(15, 64);
            this.checkBoxAllowPageReorder.Name = "checkBoxAllowPageReorder";
            this.checkBoxAllowPageReorder.Size = new System.Drawing.Size(120, 17);
            this.checkBoxAllowPageReorder.TabIndex = 2;
            this.checkBoxAllowPageReorder.Text = "Allow Page Reorder";
            // 
            // checkBoxAllowConfigSave
            // 
            this.checkBoxAllowConfigSave.AutoSize = true;
            this.checkBoxAllowConfigSave.Location = new System.Drawing.Point(15, 87);
            this.checkBoxAllowConfigSave.Name = "checkBoxAllowConfigSave";
            this.checkBoxAllowConfigSave.Size = new System.Drawing.Size(112, 17);
            this.checkBoxAllowConfigSave.TabIndex = 3;
            this.checkBoxAllowConfigSave.Text = "Allow Config Save";
            // 
            // KryptonPageFormEditFlags
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(429, 238);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxAllowConfigSave);
            this.Controls.Add(this.checkBoxAllowPageReorder);
            this.Controls.Add(this.checkBoxDockingAllowDropDown);
            this.Controls.Add(this.checkBoxDockingAllowNavigator);
            this.Controls.Add(this.checkBoxDockingAllowWorkspace);
            this.Controls.Add(this.checkBoxDockingAllowFloating);
            this.Controls.Add(this.checkBoxDockingAllowDocked);
            this.Controls.Add(this.checkBoxDockingAllowAutoHidden);
            this.Controls.Add(this.checkBoxDockingAllowClose);
            this.Controls.Add(this.checkBoxAllowPageDrag);
            this.Controls.Add(this.checkBoxPageInOverflowBarForOutlookMode);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonPageFormEditFlags";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Krypton Page - Edit Flags";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonButton buttonOK;
        private Krypton.Toolkit.KryptonButton buttonCancel;
        private Krypton.Toolkit.KryptonCheckBox checkBoxPageInOverflowBarForOutlookMode;
        private Krypton.Toolkit.KryptonCheckBox checkBoxAllowPageDrag;
        private Krypton.Toolkit.KryptonCheckBox checkBoxDockingAllowClose;
        private Krypton.Toolkit.KryptonCheckBox checkBoxDockingAllowAutoHidden;
        private Krypton.Toolkit.KryptonCheckBox checkBoxDockingAllowDocked;
        private Krypton.Toolkit.KryptonCheckBox checkBoxDockingAllowFloating;
        private Krypton.Toolkit.KryptonCheckBox checkBoxDockingAllowWorkspace;
        private Krypton.Toolkit.KryptonCheckBox checkBoxDockingAllowNavigator;
        private Krypton.Toolkit.KryptonCheckBox checkBoxDockingAllowDropDown;
        private Krypton.Toolkit.KryptonCheckBox checkBoxAllowPageReorder;
        private Krypton.Toolkit.KryptonCheckBox checkBoxAllowConfigSave;
    }
}