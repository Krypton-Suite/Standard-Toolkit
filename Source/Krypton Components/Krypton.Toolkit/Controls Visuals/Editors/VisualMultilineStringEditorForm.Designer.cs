#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2022 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    partial class VisualMultilineStringEditorForm
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
            this.kpnlButtonBar = new Krypton.Toolkit.InternalDesignerEditorButtonBarPanel();
            this.kpnlContent = new Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.krtbContents = new Krypton.Toolkit.KryptonRichTextBox();
            this.ktxtStringCollection = new Krypton.Toolkit.KryptonTextBox();
            this.kcmRichTextBoxMenu = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcRichTextBoxCut = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator1 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcRichTextBoxCopy = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator2 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcRichTextBoxPaste = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator5 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem7 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcRichTextBoxSelectAll = new Krypton.Toolkit.KryptonCommand();
            this.kcmTextBoxMenu = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems2 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem4 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcTextBoxCut = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator3 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem5 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcTextBoxCopy = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator4 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem6 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcTextBoxPaste = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator6 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem8 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcTextBoxSelectAll = new Krypton.Toolkit.KryptonCommand();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtonBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).BeginInit();
            this.kpnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlButtonBar
            // 
            this.kpnlButtonBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtonBar.Location = new System.Drawing.Point(0, 309);
            this.kpnlButtonBar.Name = "kpnlButtonBar";
            this.kpnlButtonBar.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtonBar.Size = new System.Drawing.Size(584, 52);
            this.kpnlButtonBar.TabIndex = 1;
            // 
            // kpnlContent
            // 
            this.kpnlContent.Controls.Add(this.kryptonGroupBox1);
            this.kpnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContent.Location = new System.Drawing.Point(0, 0);
            this.kpnlContent.Name = "kpnlContent";
            this.kpnlContent.Padding = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.kpnlContent.Size = new System.Drawing.Size(584, 309);
            this.kpnlContent.TabIndex = 2;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(12, 9);
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.krtbContents);
            this.kryptonGroupBox1.Panel.Controls.Add(this.ktxtStringCollection);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(560, 291);
            this.kryptonGroupBox1.TabIndex = 3;
            this.kryptonGroupBox1.Values.Heading = "Enter the strings in the collection (one per line):";
            // 
            // krtbContents
            // 
            this.krtbContents.CueHint.CueHintText = "Insert string collection here...";
            this.krtbContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbContents.Location = new System.Drawing.Point(0, 0);
            this.krtbContents.Name = "krtbContents";
            this.krtbContents.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.krtbContents.Size = new System.Drawing.Size(556, 267);
            this.krtbContents.TabIndex = 2;
            this.krtbContents.Text = "";
            // 
            // ktxtStringCollection
            // 
            this.ktxtStringCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtStringCollection.Location = new System.Drawing.Point(0, 0);
            this.ktxtStringCollection.Multiline = true;
            this.ktxtStringCollection.Name = "ktxtStringCollection";
            this.ktxtStringCollection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ktxtStringCollection.Size = new System.Drawing.Size(556, 267);
            this.ktxtStringCollection.TabIndex = 1;
            // 
            // kcmRichTextBoxMenu
            // 
            this.kcmRichTextBoxMenu.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuSeparator1,
            this.kryptonContextMenuItem2,
            this.kryptonContextMenuSeparator2,
            this.kryptonContextMenuItem3,
            this.kryptonContextMenuSeparator5,
            this.kryptonContextMenuItem7});
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.KryptonCommand = this.kcRichTextBoxCut;
            this.kryptonContextMenuItem1.ShortcutKeyDisplayString = "Ctrl + X";
            this.kryptonContextMenuItem1.Text = "&Cut";
            // 
            // kcRichTextBoxCut
            // 
            this.kcRichTextBoxCut.Text = "kryptonCommand1";
            this.kcRichTextBoxCut.Execute += new System.EventHandler(this.kcRichTextBoxCut_Execute);
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.KryptonCommand = this.kcRichTextBoxCopy;
            this.kryptonContextMenuItem2.ShortcutKeyDisplayString = "Ctrl + C";
            this.kryptonContextMenuItem2.Text = "C&opy";
            // 
            // kcRichTextBoxCopy
            // 
            this.kcRichTextBoxCopy.Text = "kryptonCommand1";
            this.kcRichTextBoxCopy.Execute += new System.EventHandler(this.kcRichTextBoxCopy_Execute);
            // 
            // kryptonContextMenuItem3
            // 
            this.kryptonContextMenuItem3.KryptonCommand = this.kcRichTextBoxPaste;
            this.kryptonContextMenuItem3.ShortcutKeyDisplayString = "Ctrl + V";
            this.kryptonContextMenuItem3.Text = "Pa&ste";
            // 
            // kcRichTextBoxPaste
            // 
            this.kcRichTextBoxPaste.Text = "kryptonCommand1";
            this.kcRichTextBoxPaste.Execute += new System.EventHandler(this.kcRichTextBoxPaste_Execute);
            // 
            // kryptonContextMenuItem7
            // 
            this.kryptonContextMenuItem7.KryptonCommand = this.kcRichTextBoxSelectAll;
            this.kryptonContextMenuItem7.ShortcutKeyDisplayString = "Ctrl + A";
            this.kryptonContextMenuItem7.Text = "&Select All";
            // 
            // kcRichTextBoxSelectAll
            // 
            this.kcRichTextBoxSelectAll.Text = "kryptonCommand1";
            this.kcRichTextBoxSelectAll.Execute += new System.EventHandler(this.kcRichTextBoxSelectAll_Execute);
            // 
            // kcmTextBoxMenu
            // 
            this.kcmTextBoxMenu.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems2});
            // 
            // kryptonContextMenuItems2
            // 
            this.kryptonContextMenuItems2.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem4,
            this.kryptonContextMenuSeparator3,
            this.kryptonContextMenuItem5,
            this.kryptonContextMenuSeparator4,
            this.kryptonContextMenuItem6,
            this.kryptonContextMenuSeparator6,
            this.kryptonContextMenuItem8});
            // 
            // kryptonContextMenuItem4
            // 
            this.kryptonContextMenuItem4.KryptonCommand = this.kcTextBoxCut;
            this.kryptonContextMenuItem4.ShortcutKeyDisplayString = "Ctrl + X";
            this.kryptonContextMenuItem4.Text = "&Cut";
            // 
            // kcTextBoxCut
            // 
            this.kcTextBoxCut.Text = "kryptonCommand1";
            this.kcTextBoxCut.Execute += new System.EventHandler(this.kcTextBoxCut_Execute);
            // 
            // kryptonContextMenuItem5
            // 
            this.kryptonContextMenuItem5.KryptonCommand = this.kcTextBoxCopy;
            this.kryptonContextMenuItem5.ShortcutKeyDisplayString = "Ctrl + C";
            this.kryptonContextMenuItem5.Text = "C&opy";
            // 
            // kcTextBoxCopy
            // 
            this.kcTextBoxCopy.Text = "kryptonCommand1";
            this.kcTextBoxCopy.Execute += new System.EventHandler(this.kcTextBoxCopy_Execute);
            // 
            // kryptonContextMenuItem6
            // 
            this.kryptonContextMenuItem6.KryptonCommand = this.kcTextBoxPaste;
            this.kryptonContextMenuItem6.ShortcutKeyDisplayString = "Ctrl + V";
            this.kryptonContextMenuItem6.Text = "Pa&ste";
            // 
            // kcTextBoxPaste
            // 
            this.kcTextBoxPaste.Text = "kryptonCommand1";
            this.kcTextBoxPaste.Execute += new System.EventHandler(this.kcTextBoxPaste_Execute);
            // 
            // kryptonContextMenuItem8
            // 
            this.kryptonContextMenuItem8.KryptonCommand = this.kcTextBoxSelectAll;
            this.kryptonContextMenuItem8.ShortcutKeyDisplayString = "Ctrl + A";
            this.kryptonContextMenuItem8.Text = "&Select All";
            // 
            // kcTextBoxSelectAll
            // 
            this.kcTextBoxSelectAll.Text = "kryptonCommand1";
            this.kcTextBoxSelectAll.Execute += new System.EventHandler(this.kcTextBoxSelectAll_Execute);
            // 
            // VisualMultilineStringEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.kpnlContent);
            this.Controls.Add(this.kpnlButtonBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualMultilineStringEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "String Collection Editor";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtonBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).EndInit();
            this.kpnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private InternalDesignerEditorButtonBarPanel kpnlButtonBar;
        private KryptonPanel kpnlContent;
        private KryptonTextBox ktxtStringCollection;
        private KryptonContextMenu kcmRichTextBoxMenu;
        private KryptonContextMenuItems kryptonContextMenuItems1;
        private KryptonContextMenuItem kryptonContextMenuItem1;
        private KryptonCommand kcRichTextBoxCut;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator1;
        private KryptonContextMenuItem kryptonContextMenuItem2;
        private KryptonCommand kcRichTextBoxCopy;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator2;
        private KryptonContextMenuItem kryptonContextMenuItem3;
        private KryptonCommand kcRichTextBoxPaste;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator5;
        private KryptonContextMenuItem kryptonContextMenuItem7;
        private KryptonCommand kcRichTextBoxSelectAll;
        private KryptonContextMenu kcmTextBoxMenu;
        private KryptonContextMenuItems kryptonContextMenuItems2;
        private KryptonContextMenuItem kryptonContextMenuItem4;
        private KryptonCommand kcTextBoxCut;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator3;
        private KryptonContextMenuItem kryptonContextMenuItem5;
        private KryptonCommand kcTextBoxCopy;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator4;
        private KryptonContextMenuItem kryptonContextMenuItem6;
        private KryptonCommand kcTextBoxPaste;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator6;
        private KryptonContextMenuItem kryptonContextMenuItem8;
        private KryptonCommand kcTextBoxSelectAll;
        private KryptonGroupBox kryptonGroupBox1;
        private KryptonRichTextBox krtbContents;
    }
}
