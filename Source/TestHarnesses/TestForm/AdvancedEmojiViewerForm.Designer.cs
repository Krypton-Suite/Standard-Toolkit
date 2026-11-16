namespace TestForm
{
    partial class AdvancedEmojiViewerForm
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
            this.ktxtSearch = new Krypton.Toolkit.KryptonTextBox();
            this.kdvEmojis = new Krypton.Toolkit.KryptonDataGridView();
            this.kcmExtra = new Krypton.Toolkit.KryptonContextMenu();
            this.kcCopyEmoji = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem4 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kcmCopyEmojiInformation = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuSeparator2 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kcmCopyEmojiOnly = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcmdCopyEmojiOnly = new Krypton.Toolkit.KryptonCommand();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdvEmojis)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kdvEmojis);
            this.kryptonPanel1.Controls.Add(this.ktxtSearch);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(808, 440);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // ktxtSearch
            // 
            this.ktxtSearch.CueHint.CueHintText = "Search...";
            this.ktxtSearch.Location = new System.Drawing.Point(12, 12);
            this.ktxtSearch.Name = "ktxtSearch";
            this.ktxtSearch.Size = new System.Drawing.Size(289, 23);
            this.ktxtSearch.TabIndex = 1;
            this.ktxtSearch.TextChanged += new System.EventHandler(this.ktxtSearch_TextChanged);
            // 
            // kdvEmojis
            // 
            this.kdvEmojis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kdvEmojis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kdvEmojis.KryptonContextMenu = this.kcmExtra;
            this.kdvEmojis.Location = new System.Drawing.Point(12, 42);
            this.kdvEmojis.Name = "kdvEmojis";
            this.kdvEmojis.Size = new System.Drawing.Size(778, 364);
            this.kdvEmojis.TabIndex = 2;
            // 
            // kcmExtra
            // 
            this.kcmExtra.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            // 
            // kcCopyEmoji
            // 
            this.kcCopyEmoji.Text = "&Copy Emoji Information";
            this.kcCopyEmoji.Execute += new System.EventHandler(this.kcCopyEmoji_Execute);
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kcmCopyEmojiInformation,
            this.kryptonContextMenuSeparator2,
            this.kcmCopyEmojiOnly});
            // 
            // kcmCopyEmojiInformation
            // 
            this.kcmCopyEmojiInformation.KryptonCommand = this.kcCopyEmoji;
            this.kcmCopyEmojiInformation.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.kcmCopyEmojiInformation.Text = "&Copy Emoji Information";
            // 
            // kcmCopyEmojiOnly
            // 
            this.kcmCopyEmojiOnly.KryptonCommand = this.kcmdCopyEmojiOnly;
            this.kcmCopyEmojiOnly.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.kcmCopyEmojiOnly.Text = "Copy &Emoji Only";
            // 
            // kcmdCopyEmojiOnly
            // 
            this.kcmdCopyEmojiOnly.Text = "Copy &Emoji Only";
            this.kcmdCopyEmojiOnly.Execute += new System.EventHandler(this.kcmdCopyEmojiOnly_Execute);
            // 
            // AdvancedEmojiViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 440);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "AdvancedEmojiViewerForm";
            this.Text = "AdvancedEmojiViewerForm";
            this.Load += new System.EventHandler(this.AdvancedEmojiViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdvEmojis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonTextBox ktxtSearch;
        private KryptonDataGridView kdvEmojis;
        private KryptonContextMenu kcmExtra;
        private KryptonCommand kcCopyEmoji;
        private KryptonContextMenuItem kryptonContextMenuItem1;
        private KryptonContextMenuItem kryptonContextMenuItem2;
        private KryptonContextMenuItem kryptonContextMenuItem3;
        private KryptonContextMenuItems kryptonContextMenuItems1;
        private KryptonContextMenuItem kcmCopyEmojiInformation;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator2;
        private KryptonContextMenuItem kcmCopyEmojiOnly;
        private KryptonContextMenuItem kryptonContextMenuItem4;
        private KryptonCommand kcmdCopyEmojiOnly;
    }
}