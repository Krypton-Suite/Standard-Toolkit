namespace Krypton.Toolkit
{
    partial class KryptonMultilineStringEditorForm
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
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kbEdge = new Krypton.Toolkit.KryptonBorderEdge();
            this.kpnlContent = new Krypton.Toolkit.KryptonPanel();
            this.krtbContents = new Krypton.Toolkit.KryptonRichTextBox();
            this.ktxtStringCollection = new Krypton.Toolkit.KryptonTextBox();
            this.klblHeader = new Krypton.Toolkit.KryptonLabel();
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).BeginInit();
            this.kpnlContent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.kbtnOk);
            this.kpnlButtons.Controls.Add(this.kbtnCancel);
            this.kpnlButtons.Controls.Add(this.kbEdge);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 311);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(584, 50);
            this.kpnlButtons.TabIndex = 1;
            // 
            // kbtnOk
            // 
            this.kbtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(381, 13);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(90, 25);
            this.kbtnOk.TabIndex = 2;
            this.kbtnOk.Values.Text = "O&K";
            this.kbtnOk.Click += new System.EventHandler(this.kbtnOk_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(477, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.Text = "C&ancel";
            // 
            // kbEdge
            // 
            this.kbEdge.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kbEdge.Dock = System.Windows.Forms.DockStyle.Top;
            this.kbEdge.Location = new System.Drawing.Point(0, 0);
            this.kbEdge.Name = "kbEdge";
            this.kbEdge.Size = new System.Drawing.Size(584, 1);
            this.kbEdge.Text = "kryptonBorderEdge1";
            // 
            // kpnlContent
            // 
            this.kpnlContent.Controls.Add(this.tableLayoutPanel1);
            this.kpnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContent.Location = new System.Drawing.Point(0, 0);
            this.kpnlContent.Name = "kpnlContent";
            this.kpnlContent.Size = new System.Drawing.Size(584, 311);
            this.kpnlContent.TabIndex = 2;
            // 
            // krtbContents
            // 
            this.krtbContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbContents.Location = new System.Drawing.Point(0, 0);
            this.krtbContents.Margin = new System.Windows.Forms.Padding(5);
            this.krtbContents.Name = "krtbContents";
            this.krtbContents.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.krtbContents.Size = new System.Drawing.Size(578, 265);
            this.krtbContents.TabIndex = 2;
            this.krtbContents.Text = "";
            // 
            // ktxtStringCollection
            // 
            this.ktxtStringCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtStringCollection.Location = new System.Drawing.Point(0, 0);
            this.ktxtStringCollection.Margin = new System.Windows.Forms.Padding(5);
            this.ktxtStringCollection.Multiline = true;
            this.ktxtStringCollection.Name = "ktxtStringCollection";
            this.ktxtStringCollection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ktxtStringCollection.Size = new System.Drawing.Size(578, 265);
            this.ktxtStringCollection.TabIndex = 1;
            // 
            // klblHeader
            // 
            this.klblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblHeader.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.klblHeader.Location = new System.Drawing.Point(10, 10);
            this.klblHeader.Margin = new System.Windows.Forms.Padding(10);
            this.klblHeader.Name = "klblHeader";
            this.klblHeader.Size = new System.Drawing.Size(564, 20);
            this.klblHeader.TabIndex = 0;
            this.klblHeader.Values.Text = "Enter the strings in the collection (one per line):";
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.klblHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 311);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.krtbContents);
            this.kryptonPanel1.Controls.Add(this.ktxtStringCollection);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(3, 43);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(578, 265);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // KryptonMultilineStringEditorForm
            // 
            this.AcceptButton = this.kbtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.kpnlContent);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonMultilineStringEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "String Collection Editor";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).EndInit();
            this.kpnlContent.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kpnlButtons;
        private KryptonButton kbtnOk;
        private KryptonButton kbtnCancel;
        private KryptonBorderEdge kbEdge;
        private KryptonPanel kpnlContent;
        private KryptonRichTextBox krtbContents;
        private KryptonTextBox ktxtStringCollection;
        private KryptonLabel klblHeader;
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
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonPanel kryptonPanel1;
    }
}