#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
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
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(261, 15);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Values.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(342, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Values.Text = "Cancel";
            // 
            // checkBoxPageInOverflowBarForOutlookMode
            // 
            this.checkBoxPageInOverflowBarForOutlookMode.Location = new System.Drawing.Point(12, 12);
            this.checkBoxPageInOverflowBarForOutlookMode.Name = "checkBoxPageInOverflowBarForOutlookMode";
            this.checkBoxPageInOverflowBarForOutlookMode.Size = new System.Drawing.Size(239, 20);
            this.checkBoxPageInOverflowBarForOutlookMode.TabIndex = 0;
            this.checkBoxPageInOverflowBarForOutlookMode.Values.Text = "Page in Overflow Bar for Outlook mode";
            // 
            // checkBoxAllowPageDrag
            // 
            this.checkBoxAllowPageDrag.Location = new System.Drawing.Point(12, 35);
            this.checkBoxAllowPageDrag.Name = "checkBoxAllowPageDrag";
            this.checkBoxAllowPageDrag.Size = new System.Drawing.Size(114, 20);
            this.checkBoxAllowPageDrag.TabIndex = 1;
            this.checkBoxAllowPageDrag.Values.Text = "Allow Page Drag";
            // 
            // checkBoxDockingAllowClose
            // 
            this.checkBoxDockingAllowClose.Location = new System.Drawing.Point(253, 12);
            this.checkBoxDockingAllowClose.Name = "checkBoxDockingAllowClose";
            this.checkBoxDockingAllowClose.Size = new System.Drawing.Size(135, 20);
            this.checkBoxDockingAllowClose.TabIndex = 4;
            this.checkBoxDockingAllowClose.Values.Text = "Docking Allow Close";
            // 
            // checkBoxDockingAllowAutoHidden
            // 
            this.checkBoxDockingAllowAutoHidden.Location = new System.Drawing.Point(253, 58);
            this.checkBoxDockingAllowAutoHidden.Name = "checkBoxDockingAllowAutoHidden";
            this.checkBoxDockingAllowAutoHidden.Size = new System.Drawing.Size(172, 20);
            this.checkBoxDockingAllowAutoHidden.TabIndex = 6;
            this.checkBoxDockingAllowAutoHidden.Values.Text = "Docking Allow AutoHidden";
            // 
            // checkBoxDockingAllowDocked
            // 
            this.checkBoxDockingAllowDocked.Location = new System.Drawing.Point(253, 81);
            this.checkBoxDockingAllowDocked.Name = "checkBoxDockingAllowDocked";
            this.checkBoxDockingAllowDocked.Size = new System.Drawing.Size(147, 20);
            this.checkBoxDockingAllowDocked.TabIndex = 7;
            this.checkBoxDockingAllowDocked.Values.Text = "Docking Allow Docked";
            // 
            // checkBoxDockingAllowFloating
            // 
            this.checkBoxDockingAllowFloating.Location = new System.Drawing.Point(253, 104);
            this.checkBoxDockingAllowFloating.Name = "checkBoxDockingAllowFloating";
            this.checkBoxDockingAllowFloating.Size = new System.Drawing.Size(149, 20);
            this.checkBoxDockingAllowFloating.TabIndex = 8;
            this.checkBoxDockingAllowFloating.Values.Text = "Docking Allow Floating";
            // 
            // checkBoxDockingAllowWorkspace
            // 
            this.checkBoxDockingAllowWorkspace.Location = new System.Drawing.Point(253, 127);
            this.checkBoxDockingAllowWorkspace.Name = "checkBoxDockingAllowWorkspace";
            this.checkBoxDockingAllowWorkspace.Size = new System.Drawing.Size(166, 20);
            this.checkBoxDockingAllowWorkspace.TabIndex = 9;
            this.checkBoxDockingAllowWorkspace.Values.Text = "Docking Allow Workspace";
            // 
            // checkBoxDockingAllowNavigator
            // 
            this.checkBoxDockingAllowNavigator.Location = new System.Drawing.Point(253, 150);
            this.checkBoxDockingAllowNavigator.Name = "checkBoxDockingAllowNavigator";
            this.checkBoxDockingAllowNavigator.Size = new System.Drawing.Size(159, 20);
            this.checkBoxDockingAllowNavigator.TabIndex = 10;
            this.checkBoxDockingAllowNavigator.Values.Text = "Docking Allow Navigator";
            // 
            // checkBoxDockingAllowDropDown
            // 
            this.checkBoxDockingAllowDropDown.Location = new System.Drawing.Point(253, 35);
            this.checkBoxDockingAllowDropDown.Name = "checkBoxDockingAllowDropDown";
            this.checkBoxDockingAllowDropDown.Size = new System.Drawing.Size(165, 20);
            this.checkBoxDockingAllowDropDown.TabIndex = 5;
            this.checkBoxDockingAllowDropDown.Values.Text = "Docking Allow DropDown";
            // 
            // checkBoxAllowPageReorder
            // 
            this.checkBoxAllowPageReorder.Location = new System.Drawing.Point(12, 58);
            this.checkBoxAllowPageReorder.Name = "checkBoxAllowPageReorder";
            this.checkBoxAllowPageReorder.Size = new System.Drawing.Size(131, 20);
            this.checkBoxAllowPageReorder.TabIndex = 2;
            this.checkBoxAllowPageReorder.Values.Text = "Allow Page Reorder";
            // 
            // checkBoxAllowConfigSave
            // 
            this.checkBoxAllowConfigSave.Location = new System.Drawing.Point(12, 81);
            this.checkBoxAllowConfigSave.Name = "checkBoxAllowConfigSave";
            this.checkBoxAllowConfigSave.Size = new System.Drawing.Size(122, 20);
            this.checkBoxAllowConfigSave.TabIndex = 3;
            this.checkBoxAllowConfigSave.Values.Text = "Allow Config Save";
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Controls.Add(this.buttonOK);
            this.kpnlButtons.Controls.Add(this.buttonCancel);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 188);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(429, 50);
            this.kpnlButtons.TabIndex = 13;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(429, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.checkBoxPageInOverflowBarForOutlookMode);
            this.kryptonPanel1.Controls.Add(this.checkBoxAllowPageDrag);
            this.kryptonPanel1.Controls.Add(this.checkBoxAllowConfigSave);
            this.kryptonPanel1.Controls.Add(this.checkBoxDockingAllowClose);
            this.kryptonPanel1.Controls.Add(this.checkBoxAllowPageReorder);
            this.kryptonPanel1.Controls.Add(this.checkBoxDockingAllowAutoHidden);
            this.kryptonPanel1.Controls.Add(this.checkBoxDockingAllowDropDown);
            this.kryptonPanel1.Controls.Add(this.checkBoxDockingAllowDocked);
            this.kryptonPanel1.Controls.Add(this.checkBoxDockingAllowNavigator);
            this.kryptonPanel1.Controls.Add(this.checkBoxDockingAllowFloating);
            this.kryptonPanel1.Controls.Add(this.checkBoxDockingAllowWorkspace);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(429, 188);
            this.kryptonPanel1.TabIndex = 14;
            // 
            // KryptonPageFormEditFlags
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(429, 238);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonPageFormEditFlags";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Krypton Page - Edit Flags";
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

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
        private KryptonPanel kpnlButtons;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel1;
    }
}