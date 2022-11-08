#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal partial class InternalKryptonStringCollectionEditor : UserControl
    {
        #region Design Code

        private KryptonPanel kpnlButtons;
        private KryptonButton kbtnOk;
        private KryptonButton kbtnCancel;
        private KryptonBorderEdge kbEdge;
        private KryptonPanel kpnlContent;
        private KryptonTextBox ktxtStringCollection;
        private KryptonRichTextBox krtbContents;
        private KryptonContextMenu kcmRichTextBoxMenu;
        private KryptonContextMenu kcmTextBoxMenu;
        private KryptonContextMenuItems kryptonContextMenuItems2;
        private KryptonContextMenuItems kryptonContextMenuItems1;
        private KryptonContextMenuItem kryptonContextMenuItem1;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator1;
        private KryptonContextMenuItem kryptonContextMenuItem2;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator2;
        private KryptonContextMenuItem kryptonContextMenuItem3;
        private KryptonContextMenuItem kryptonContextMenuItem4;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator3;
        private KryptonContextMenuItem kryptonContextMenuItem5;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator4;
        private KryptonContextMenuItem kryptonContextMenuItem6;
        private KryptonContextMenuItems kryptonContextMenuItems3;
        private KryptonLabel klblHeader;
        private KryptonCommand kcRichTextBoxCopy;
        private KryptonCommand kcRichTextBoxPaste;
        private KryptonCommand kcTextBoxCut;
        private KryptonCommand kcTextBoxCopy;
        private KryptonCommand kcTextBoxPaste;
        private KryptonCommand kcRichTextBoxSelectAll;
        private KryptonCommand kcTextBoxSelectAll;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator5;
        private KryptonContextMenuItem kryptonContextMenuItem7;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator6;
        private KryptonContextMenuItem kryptonContextMenuItem8;
        private KryptonCommand kcRichTextBoxCut;

        private void InitializeComponent()
        {
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kbEdge = new Krypton.Toolkit.KryptonBorderEdge();
            this.kpnlContent = new Krypton.Toolkit.KryptonPanel();
            this.krtbContents = new Krypton.Toolkit.KryptonRichTextBox();
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
            this.ktxtStringCollection = new Krypton.Toolkit.KryptonTextBox();
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
            this.klblHeader = new Krypton.Toolkit.KryptonLabel();
            this.kryptonContextMenuItems3 = new Krypton.Toolkit.KryptonContextMenuItems();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).BeginInit();
            this.kpnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.kbtnOk);
            this.kpnlButtons.Controls.Add(this.kbtnCancel);
            this.kpnlButtons.Controls.Add(this.kbEdge);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 460);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(585, 50);
            this.kpnlButtons.TabIndex = 0;
            // 
            // kbtnOk
            // 
            this.kbtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOk.CornerRoundingRadius = -1F;
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(382, 13);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(90, 25);
            this.kbtnOk.TabIndex = 2;
            this.kbtnOk.Values.Text = "O&K";
            this.kbtnOk.Click += new System.EventHandler(this.kbtnOk_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCancel.CornerRoundingRadius = -1F;
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(478, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.Text = "C&ancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // kbEdge
            // 
            this.kbEdge.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kbEdge.Dock = System.Windows.Forms.DockStyle.Top;
            this.kbEdge.Location = new System.Drawing.Point(0, 0);
            this.kbEdge.Name = "kbEdge";
            this.kbEdge.Size = new System.Drawing.Size(585, 1);
            this.kbEdge.Text = "kryptonBorderEdge1";
            // 
            // kpnlContent
            // 
            this.kpnlContent.Controls.Add(this.krtbContents);
            this.kpnlContent.Controls.Add(this.ktxtStringCollection);
            this.kpnlContent.Controls.Add(this.klblHeader);
            this.kpnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContent.Location = new System.Drawing.Point(0, 0);
            this.kpnlContent.Name = "kpnlContent";
            this.kpnlContent.Size = new System.Drawing.Size(585, 460);
            this.kpnlContent.TabIndex = 1;
            // 
            // krtbContents
            // 
            this.krtbContents.KryptonContextMenu = this.kcmRichTextBoxMenu;
            this.krtbContents.Location = new System.Drawing.Point(13, 39);
            this.krtbContents.Name = "krtbContents";
            this.krtbContents.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.krtbContents.Size = new System.Drawing.Size(555, 406);
            this.krtbContents.TabIndex = 2;
            this.krtbContents.Text = "";
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
            // ktxtStringCollection
            // 
            this.ktxtStringCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtStringCollection.KryptonContextMenu = this.kcmTextBoxMenu;
            this.ktxtStringCollection.Location = new System.Drawing.Point(13, 39);
            this.ktxtStringCollection.Multiline = true;
            this.ktxtStringCollection.Name = "ktxtStringCollection";
            this.ktxtStringCollection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ktxtStringCollection.Size = new System.Drawing.Size(555, 406);
            this.ktxtStringCollection.TabIndex = 1;
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
            // klblHeader
            // 
            this.klblHeader.Location = new System.Drawing.Point(13, 13);
            this.klblHeader.Name = "klblHeader";
            this.klblHeader.Size = new System.Drawing.Size(268, 20);
            this.klblHeader.TabIndex = 0;
            this.klblHeader.Values.Text = "Enter the strings in the collection (one per line):";
            // 
            // InternalKryptonStringCollectionEditor
            // 
            this.Controls.Add(this.kpnlContent);
            this.Controls.Add(this.kpnlButtons);
            this.Name = "InternalKryptonStringCollectionEditor";
            this.Size = new System.Drawing.Size(585, 510);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).EndInit();
            this.kpnlContent.ResumeLayout(false);
            this.kpnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        #region Instance Fields

        private bool _useTextBox;

        private bool _useRichTextBox;

        private string _headerText;

        private string _okButtonText;

        private string _cancelButtonText;

        private string[] _contents;

        #endregion

        #region Public
        /// <summary>Gets or sets a value indicating whether to use a multiline <see cref="KryptonTextBox"/> in place of a <see cref="KryptonRichTextBox"/>.</summary>
        /// <value><c>true</c> if [use text box]; otherwise, <c>false</c>.</value>
        [Category(@"Visuals"), DefaultValue(true), Description(@"Use a multiline KryptonTextBox in place of a KryptonRichTextBox.")]
        public bool UseTextBox { get => _useTextBox; set { _useTextBox = value; Invalidate(); } }

        /// <summary>Gets or sets a value indicating whether to use a <see cref="KryptonRichTextBox"/> in place of a multiline <see cref="KryptonTextBox"/>.</summary>
        /// <value><c>true</c> if [use rich text box]; otherwise, <c>false</c>.</value>
        [Category(@"Visuals"), DefaultValue(false), Description(@"Use a KryptonRichTextBox in place of a multiline KryptonTextBox.")]
        public bool UseRichTextBox { get => _useRichTextBox; set { _useRichTextBox = value; Invalidate(); } }

        /// <summary>Gets or sets the header text.</summary>
        /// <value>The header text.</value>
        [Category(@"Visuals"), DefaultValue(@"Enter the strings in the collection (one per line):"), Description(@"The text of the header label.")]
        public string HeaderText { get => _headerText; set { _headerText = value; Invalidate(); } }

        /// <summary>Gets or sets the ok button text.</summary>
        /// <value>The ok button text.</value>
        [Category(@"Visuals"), DefaultValue(@"O&K"), Description(@"The OK button text.")]
        public string OkButtonText { get => _okButtonText; set { _okButtonText = value; Invalidate(); } }

        /// <summary>Gets or sets the cancel button text.</summary>
        /// <value>The cancel button text.</value>
        [Category(@"Visuals"), DefaultValue(@"C&ancel"), Description(@"The cancel button text.")]
        public string CancelButtonText { get => _cancelButtonText; set { _cancelButtonText = value; Invalidate(); } }

        /// <summary>Gets the contents of the text field.</summary>
        /// <value>The contents of the text field.</value>
        [Category(@"Data"), DefaultValue(null), Description(@"The contents of the text field.")]
        public string[] Contents { get => _contents; private set => _contents = value; }

        /// <summary>Gets the ok button.</summary>
        /// <value>The ok button.</value>
        public KryptonButton OkButton => kbtnOk;

        /// <summary>Gets the cancel button.</summary>
        /// <value>The cancel button.</value>
        public KryptonButton CancelButton => kbtnCancel;

        /// <summary>Gets or sets the owner.</summary>
        /// <value>The owner.</value>
        [Category(@"Data"), DefaultValue(null), Description(@"")]
        public KryptonForm Owner { get; set; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="InternalKryptonStringCollectionEditor" /> class.</summary>
        public InternalKryptonStringCollectionEditor()
        {
            InitializeComponent();

            // Set default values
            _useTextBox = true;

            _useRichTextBox = false;

            _headerText = @"Enter the strings in the collection (one per line):";

            _okButtonText = @"O&K";

            _cancelButtonText = @"C&ancel";

            _contents = null;
        }

        #endregion

        #region Implementation

        private void kbtnCancel_Click(object sender, EventArgs e) => Owner.DialogResult = DialogResult.Cancel;

        private void kbtnOk_Click(object sender, EventArgs e)
        {
            if (_useTextBox)
            {
                foreach (string line in ktxtStringCollection.Lines)
                {
                    List<string> list = new List<string>();

                    list.Add(line);

                    Contents = list.ToArray();
                }
            }
            else if (_useRichTextBox)
            {
                foreach (string line in krtbContents.Lines)
                {
                    List<string> list = new List<string>();

                    list.Add(line);

                    Contents = list.ToArray();
                }
            }

            Owner.DialogResult = DialogResult.OK;
        }

        private void kcRichTextBoxCut_Execute(object sender, EventArgs e) => krtbContents.Cut();

        private void kcRichTextBoxCopy_Execute(object sender, EventArgs e) => Clipboard.SetText(krtbContents.Text);

        private void kcRichTextBoxPaste_Execute(object sender, EventArgs e) => krtbContents.Paste();

        private void kcTextBoxCut_Execute(object sender, EventArgs e) => ktxtStringCollection.Cut();

        private void kcTextBoxCopy_Execute(object sender, EventArgs e) => Clipboard.SetText(ktxtStringCollection.Text);

        private void kcTextBoxPaste_Execute(object sender, EventArgs e) => ktxtStringCollection.Paste();

        private void kcRichTextBoxSelectAll_Execute(object sender, EventArgs e) => krtbContents.SelectAll();

        private void kcTextBoxSelectAll_Execute(object sender, EventArgs e) => ktxtStringCollection.SelectAll();

        internal void SetContentsArray(string[] contentArray) => _contents = contentArray;

        #endregion

        #region Protected

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_useTextBox)
            {
                ktxtStringCollection.Visible = true;

                ktxtStringCollection.Enabled = true;

                krtbContents.Visible = false;

                ktxtStringCollection.Visible = false;
            }

            if (_useRichTextBox)
            {
                ktxtStringCollection.Visible = false;

                ktxtStringCollection.Enabled = false;

                krtbContents.Visible = true;

                krtbContents.Enabled = true;
            }

            if (!string.IsNullOrEmpty(_headerText))
            {
                klblHeader.Text = _headerText;
            }

            if (!string.IsNullOrEmpty(_okButtonText))
            {
                kbtnOk.Text = _okButtonText;
            }

            if (!string.IsNullOrEmpty(_cancelButtonText))
            {
                kbtnCancel.Text = _cancelButtonText;
            }

            base.OnPaint(e);
        }

        #endregion
    }
}