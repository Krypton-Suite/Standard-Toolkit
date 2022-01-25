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

namespace Krypton.Toolkit
{    
    /// <summary>
    /// A Krypton replacement for the standard <see cref="StringCollectionEditor"/>.
    /// </summary>
    public partial class KryptonStringCollectionEditor : KryptonForm
    {
        #region Design Code
        private KryptonCommand kcHelp;
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
        private KryptonContextMenuItem kryptonContextMenuItem1;
        private KryptonContextMenuItem kryptonContextMenuItem2;
        private KryptonContextMenuItem kryptonContextMenuItem3;
        private KryptonContextMenuItems kryptonContextMenuItems1;
        private ButtonSpecAny bsaHelp;
        private KryptonLabel klblHeader;

        private void InitializeComponent()
        {
            this.kcHelp = new Krypton.Toolkit.KryptonCommand();
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kbEdge = new Krypton.Toolkit.KryptonBorderEdge();
            this.kpnlContent = new Krypton.Toolkit.KryptonPanel();
            this.ktxtStringCollection = new Krypton.Toolkit.KryptonTextBox();
            this.klblHeader = new Krypton.Toolkit.KryptonLabel();
            this.kcmTextBoxMenu = new Krypton.Toolkit.KryptonContextMenu();
            this.kcmRichTextBoxMenu = new Krypton.Toolkit.KryptonContextMenu();
            this.krtbContents = new Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItems2 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.bsaHelp = new Krypton.Toolkit.ButtonSpecAny();
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
            this.kpnlButtons.Location = new System.Drawing.Point(0, 317);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(540, 50);
            this.kpnlButtons.TabIndex = 0;
            // 
            // kbtnOk
            // 
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(341, 13);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(90, 25);
            this.kbtnOk.TabIndex = 2;
            this.kbtnOk.Values.Text = "O&K";
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(437, 13);
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
            this.kbEdge.Size = new System.Drawing.Size(540, 1);
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
            this.kpnlContent.Size = new System.Drawing.Size(540, 317);
            this.kpnlContent.TabIndex = 1;
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
            this.ktxtStringCollection.Size = new System.Drawing.Size(515, 272);
            this.ktxtStringCollection.TabIndex = 1;
            // 
            // klblHeader
            // 
            this.klblHeader.Location = new System.Drawing.Point(13, 13);
            this.klblHeader.Name = "klblHeader";
            this.klblHeader.Size = new System.Drawing.Size(268, 20);
            this.klblHeader.TabIndex = 0;
            this.klblHeader.Values.Text = "Enter the strings in the collection (one per line):";
            // 
            // kcmTextBoxMenu
            // 
            this.kcmTextBoxMenu.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems2});
            // 
            // krtbContents
            // 
            this.krtbContents.KryptonContextMenu = this.kcmRichTextBoxMenu;
            this.krtbContents.Location = new System.Drawing.Point(13, 39);
            this.krtbContents.Name = "krtbContents";
            this.krtbContents.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.krtbContents.Size = new System.Drawing.Size(514, 272);
            this.krtbContents.TabIndex = 2;
            this.krtbContents.Text = "";
            // 
            // kryptonContextMenuItems2
            // 
            this.kryptonContextMenuItems2.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuItem2,
            this.kryptonContextMenuItem3});
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.Text = "C&ut";
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.Text = "Cop&y";
            // 
            // kryptonContextMenuItem3
            // 
            this.kryptonContextMenuItem3.Text = "&Paste";
            // 
            // bsaHelp
            // 
            this.bsaHelp.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormHelp;
            this.bsaHelp.UniqueName = "6a30a8d713df481ea4c7d1d5798f3e9c";
            // 
            // KryptonStringCollectionEditor
            // 
            this.AcceptButton = this.kbtnOk;
            this.ButtonSpecs.AddRange(new Krypton.Toolkit.ButtonSpecAny[] {
            this.bsaHelp});
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(540, 367);
            this.Controls.Add(this.kpnlContent);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonStringCollectionEditor";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "String Collection Editor";
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

        private bool _useRichTextBox;

        private string _content, _header, _collectionCueText, _okButtonText, _cancelButtonText, _helpMessageBoxTitle, _helpMessageBoxContent;

        #endregion

        #region Constructor

        public KryptonStringCollectionEditor(bool useRichTextBox = true)
        {
            InitializeComponent();

            _useRichTextBox = useRichTextBox;

            _header = "Enter the strings in the collection (one per line):";

            _cancelButtonText = "C&ancel";

            _okButtonText = "O&K";

            _helpMessageBoxTitle = "Krypton String Collection Editor Help";

            _helpMessageBoxContent = "Enables you to view and change the list of strings for controls such as list boxes and combo boxes.\nDisplay this editor from the Properties window by clicking the Ellipsis button next to the Items property of the control.";

            SetupUI();
        }

        public KryptonStringCollectionEditor(string header, string cancelButtonText, string collectionCueText, 
                                             string okButtonText, bool? useRichTextBox,
                                             string helpMessageBoxTitle, string helpMessageBoxContent)
        {
            InitializeComponent();

            _header = header ?? "Enter the strings in the collection (one per line):";

            _cancelButtonText = cancelButtonText ?? "C&ancel";

            _collectionCueText = collectionCueText ?? string.Empty;

            _okButtonText = okButtonText ?? "O&K";

            _useRichTextBox = useRichTextBox ?? true;

            _helpMessageBoxTitle = helpMessageBoxTitle ?? "Krypton String Collection Editor Help";

            _helpMessageBoxContent = helpMessageBoxContent ?? "Enables you to view and change the list of strings for controls such as list boxes and combo boxes.\nDisplay this editor from the Properties window by clicking the Ellipsis button next to the Items property of the control.";

            SetupUI();
        }
        #endregion

        #region Methods

        private void SetupUI()
        {
            klblHeader.Text = _header;

            ktxtStringCollection.CueHint.CueHintText = _collectionCueText;

            kbtnCancel.Text = _cancelButtonText;

            kbtnOk.Text = _okButtonText;

            if (_useRichTextBox)
            {
                ktxtStringCollection.Visible = false;

                krtbContents.Visible = true;
            }
            else
            {
                ktxtStringCollection.Visible = true;

                krtbContents.Visible = false;
            }

            bsaHelp.Click += (sender, args) =>
            {
                KryptonMessageBox.Show(_helpMessageBoxContent, _helpMessageBoxTitle, MessageBoxButtons.OK, KryptonMessageBoxIcon.QUESTION);
            };

            //AcceptButton = kbtnOk;

            //CancelButton = kbtnCancel;
        }

        public string ReturnContentText()
        {
            _content = ktxtStringCollection.Text;

            return _content;
        }

        public string[] ReturnContents()
        {
            List<string> contents = new List<string>();

            foreach (string line in ktxtStringCollection.Lines)
            {
                contents.Add(line);
            }

            return contents.ToArray();
        }

        #endregion
    }
}