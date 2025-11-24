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

namespace Krypton.Toolkit;

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
        kpnlButtons = new KryptonPanel();
        kbtnOk = new KryptonButton();
        kbtnCancel = new KryptonButton();
        kbEdge = new KryptonBorderEdge();
        kpnlContent = new KryptonPanel();
        krtbContents = new KryptonRichTextBox();
        kcmRichTextBoxMenu = new KryptonContextMenu();
        kryptonContextMenuItems1 = new KryptonContextMenuItems();
        kryptonContextMenuItem1 = new KryptonContextMenuItem();
        kcRichTextBoxCut = new KryptonCommand();
        kryptonContextMenuSeparator1 = new KryptonContextMenuSeparator();
        kryptonContextMenuItem2 = new KryptonContextMenuItem();
        kcRichTextBoxCopy = new KryptonCommand();
        kryptonContextMenuSeparator2 = new KryptonContextMenuSeparator();
        kryptonContextMenuItem3 = new KryptonContextMenuItem();
        kcRichTextBoxPaste = new KryptonCommand();
        kryptonContextMenuSeparator5 = new KryptonContextMenuSeparator();
        kryptonContextMenuItem7 = new KryptonContextMenuItem();
        kcRichTextBoxSelectAll = new KryptonCommand();
        ktxtStringCollection = new KryptonTextBox();
        kcmTextBoxMenu = new KryptonContextMenu();
        kryptonContextMenuItems2 = new KryptonContextMenuItems();
        kryptonContextMenuItem4 = new KryptonContextMenuItem();
        kcTextBoxCut = new KryptonCommand();
        kryptonContextMenuSeparator3 = new KryptonContextMenuSeparator();
        kryptonContextMenuItem5 = new KryptonContextMenuItem();
        kcTextBoxCopy = new KryptonCommand();
        kryptonContextMenuSeparator4 = new KryptonContextMenuSeparator();
        kryptonContextMenuItem6 = new KryptonContextMenuItem();
        kcTextBoxPaste = new KryptonCommand();
        kryptonContextMenuSeparator6 = new KryptonContextMenuSeparator();
        kryptonContextMenuItem8 = new KryptonContextMenuItem();
        kcTextBoxSelectAll = new KryptonCommand();
        klblHeader = new KryptonLabel();
        kryptonContextMenuItems3 = new KryptonContextMenuItems();
        ((ISupportInitialize)(kpnlButtons)).BeginInit();
        kpnlButtons.SuspendLayout();
        ((ISupportInitialize)(kpnlContent)).BeginInit();
        kpnlContent.SuspendLayout();
        SuspendLayout();
        // 
        // kpnlButtons
        // 
        kpnlButtons.Controls.Add(kbtnOk);
        kpnlButtons.Controls.Add(kbtnCancel);
        kpnlButtons.Controls.Add(kbEdge);
        kpnlButtons.Dock = DockStyle.Bottom;
        kpnlButtons.Location = new Point(0, 460);
        kpnlButtons.Name = "kpnlButtons";
        kpnlButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        kpnlButtons.Size = new Size(585, 50);
        kpnlButtons.TabIndex = 0;
        // 
        // kbtnOk
        // 
        kbtnOk.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
        kbtnOk.DialogResult = DialogResult.OK;
        kbtnOk.Location = new Point(382, 13);
        kbtnOk.Name = "kbtnOk";
        kbtnOk.Size = new Size(90, 25);
        kbtnOk.TabIndex = 2;
        kbtnOk.Values.Text = "O&K";
        kbtnOk.Click += kbtnOk_Click;
        // 
        // kbtnCancel
        // 
        kbtnCancel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
        kbtnCancel.DialogResult = DialogResult.Cancel;
        kbtnCancel.Location = new Point(478, 13);
        kbtnCancel.Name = "kbtnCancel";
        kbtnCancel.Size = new Size(90, 25);
        kbtnCancel.TabIndex = 1;
        kbtnCancel.Values.Text = "C&ancel";
        kbtnCancel.Click += kbtnCancel_Click;
        // 
        // kbEdge
        // 
        kbEdge.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        kbEdge.Dock = DockStyle.Top;
        kbEdge.Location = new Point(0, 0);
        kbEdge.Name = "kbEdge";
        kbEdge.Size = new Size(585, 1);
        kbEdge.Text = "kryptonBorderEdge1";
        // 
        // kpnlContent
        // 
        kpnlContent.Controls.Add(krtbContents);
        kpnlContent.Controls.Add(ktxtStringCollection);
        kpnlContent.Controls.Add(klblHeader);
        kpnlContent.Dock = DockStyle.Fill;
        kpnlContent.Location = new Point(0, 0);
        kpnlContent.Name = "kpnlContent";
        kpnlContent.Size = new Size(585, 460);
        kpnlContent.TabIndex = 1;
        // 
        // krtbContents
        // 
        krtbContents.KryptonContextMenu = kcmRichTextBoxMenu;
        krtbContents.Location = new Point(13, 39);
        krtbContents.Name = "krtbContents";
        krtbContents.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
        krtbContents.Size = new Size(555, 406);
        krtbContents.TabIndex = 2;
        krtbContents.Text = "";
        // 
        // kcmRichTextBoxMenu
        // 
        kcmRichTextBoxMenu.Items.AddRange([
            kryptonContextMenuItems1
        ]);
        // 
        // kryptonContextMenuItems1
        // 
        kryptonContextMenuItems1.Items.AddRange([
            kryptonContextMenuItem1,
            kryptonContextMenuSeparator1,
            kryptonContextMenuItem2,
            kryptonContextMenuSeparator2,
            kryptonContextMenuItem3,
            kryptonContextMenuSeparator5,
            kryptonContextMenuItem7
        ]);
        // 
        // kryptonContextMenuItem1
        // 
        kryptonContextMenuItem1.KryptonCommand = kcRichTextBoxCut;
        kryptonContextMenuItem1.ShortcutKeyDisplayString = "Ctrl + X";
        kryptonContextMenuItem1.Text = "&Cut";
        // 
        // kcRichTextBoxCut
        // 
        kcRichTextBoxCut.Text = "kryptonCommand1";
        kcRichTextBoxCut.Execute += kcRichTextBoxCut_Execute;
        // 
        // kryptonContextMenuItem2
        // 
        kryptonContextMenuItem2.KryptonCommand = kcRichTextBoxCopy;
        kryptonContextMenuItem2.ShortcutKeyDisplayString = "Ctrl + C";
        kryptonContextMenuItem2.Text = "C&opy";
        // 
        // kcRichTextBoxCopy
        // 
        kcRichTextBoxCopy.Text = "kryptonCommand1";
        kcRichTextBoxCopy.Execute += kcRichTextBoxCopy_Execute;
        // 
        // kryptonContextMenuItem3
        // 
        kryptonContextMenuItem3.KryptonCommand = kcRichTextBoxPaste;
        kryptonContextMenuItem3.ShortcutKeyDisplayString = "Ctrl + V";
        kryptonContextMenuItem3.Text = "Pa&ste";
        // 
        // kcRichTextBoxPaste
        // 
        kcRichTextBoxPaste.Text = "kryptonCommand1";
        kcRichTextBoxPaste.Execute += kcRichTextBoxPaste_Execute;
        // 
        // kryptonContextMenuItem7
        // 
        kryptonContextMenuItem7.KryptonCommand = kcRichTextBoxSelectAll;
        kryptonContextMenuItem7.ShortcutKeyDisplayString = "Ctrl + A";
        kryptonContextMenuItem7.Text = "&Select All";
        // 
        // kcRichTextBoxSelectAll
        // 
        kcRichTextBoxSelectAll.Text = "kryptonCommand1";
        kcRichTextBoxSelectAll.Execute += kcRichTextBoxSelectAll_Execute;
        // 
        // ktxtStringCollection
        // 
        ktxtStringCollection.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                                                        | AnchorStyles.Left)
                                                       | AnchorStyles.Right)));
        ktxtStringCollection.KryptonContextMenu = kcmTextBoxMenu;
        ktxtStringCollection.Location = new Point(13, 39);
        ktxtStringCollection.Multiline = true;
        ktxtStringCollection.Name = "ktxtStringCollection";
        ktxtStringCollection.ScrollBars = ScrollBars.Both;
        ktxtStringCollection.Size = new Size(555, 406);
        ktxtStringCollection.TabIndex = 1;
        // 
        // kcmTextBoxMenu
        // 
        kcmTextBoxMenu.Items.AddRange([
            kryptonContextMenuItems2
        ]);
        // 
        // kryptonContextMenuItems2
        // 
        kryptonContextMenuItems2.Items.AddRange([
            kryptonContextMenuItem4,
            kryptonContextMenuSeparator3,
            kryptonContextMenuItem5,
            kryptonContextMenuSeparator4,
            kryptonContextMenuItem6,
            kryptonContextMenuSeparator6,
            kryptonContextMenuItem8
        ]);
        // 
        // kryptonContextMenuItem4
        // 
        kryptonContextMenuItem4.KryptonCommand = kcTextBoxCut;
        kryptonContextMenuItem4.ShortcutKeyDisplayString = "Ctrl + X";
        kryptonContextMenuItem4.Text = "&Cut";
        // 
        // kcTextBoxCut
        // 
        kcTextBoxCut.Text = "kryptonCommand1";
        kcTextBoxCut.Execute += kcTextBoxCut_Execute;
        // 
        // kryptonContextMenuItem5
        // 
        kryptonContextMenuItem5.KryptonCommand = kcTextBoxCopy;
        kryptonContextMenuItem5.ShortcutKeyDisplayString = "Ctrl + C";
        kryptonContextMenuItem5.Text = "C&opy";
        // 
        // kcTextBoxCopy
        // 
        kcTextBoxCopy.Text = "kryptonCommand1";
        kcTextBoxCopy.Execute += kcTextBoxCopy_Execute;
        // 
        // kryptonContextMenuItem6
        // 
        kryptonContextMenuItem6.KryptonCommand = kcTextBoxPaste;
        kryptonContextMenuItem6.ShortcutKeyDisplayString = "Ctrl + V";
        kryptonContextMenuItem6.Text = "Pa&ste";
        // 
        // kcTextBoxPaste
        // 
        kcTextBoxPaste.Text = "kryptonCommand1";
        kcTextBoxPaste.Execute += kcTextBoxPaste_Execute;
        // 
        // kryptonContextMenuItem8
        // 
        kryptonContextMenuItem8.KryptonCommand = kcTextBoxSelectAll;
        kryptonContextMenuItem8.ShortcutKeyDisplayString = "Ctrl + A";
        kryptonContextMenuItem8.Text = "&Select All";
        // 
        // kcTextBoxSelectAll
        // 
        kcTextBoxSelectAll.Text = "kryptonCommand1";
        kcTextBoxSelectAll.Execute += kcTextBoxSelectAll_Execute;
        // 
        // klblHeader
        // 
        klblHeader.Location = new Point(13, 13);
        klblHeader.Name = "klblHeader";
        klblHeader.Size = new Size(268, 20);
        klblHeader.TabIndex = 0;
        klblHeader.Values.Text = "Enter the strings in the collection (one per line):";
        // 
        // InternalKryptonStringCollectionEditor
        // 
        Controls.Add(kpnlContent);
        Controls.Add(kpnlButtons);
        Name = "InternalKryptonStringCollectionEditor";
        Size = new Size(585, 510);
        ((ISupportInitialize)(kpnlButtons)).EndInit();
        kpnlButtons.ResumeLayout(false);
        kpnlButtons.PerformLayout();
        ((ISupportInitialize)(kpnlContent)).EndInit();
        kpnlContent.ResumeLayout(false);
        kpnlContent.PerformLayout();
        ResumeLayout(false);

    }

    #endregion

    #region Instance Fields

    private bool _useTextBox;

    private bool _useRichTextBox;

    private string _headerText;

    private string _okButtonText;

    private string _cancelButtonText;

    private string[]? _contents;

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
    [Category(@"Data"),
     DefaultValue(null),
     Description(@"The contents of the text field.")]
    public string[]? Contents
    {
        get => _contents;
        private set => _contents = value;
    }

    /// <summary>Gets the ok button.</summary>
    /// <value>The ok button.</value>
    public KryptonButton OkButton => kbtnOk;

    /// <summary>Gets the cancel button.</summary>
    /// <value>The cancel button.</value>
    public KryptonButton CancelButton => kbtnCancel;

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    [Category(@"Data"),
     DefaultValue(null),
     Description(@"")]
    public KryptonForm? Owner { get; set; }

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

    private void kbtnCancel_Click(object? sender, EventArgs e) => Owner!.DialogResult = DialogResult.Cancel;

    private void kbtnOk_Click(object? sender, EventArgs e)
    {
        if (_useTextBox)
        {
            foreach (var line in ktxtStringCollection.Lines)
            {
                List<string> list =
                [
                    line
                ];

                Contents = list.ToArray();
            }
        }
        else if (_useRichTextBox)
        {
            foreach (var line in krtbContents.Lines)
            {
                List<string> list =
                [
                    line
                ];

                Contents = list.ToArray();
            }
        }

        Owner!.DialogResult = DialogResult.OK;
    }

    private void kcRichTextBoxCut_Execute(object? sender, EventArgs e) => krtbContents.Cut();

    private void kcRichTextBoxCopy_Execute(object? sender, EventArgs e) => Clipboard.SetText(krtbContents.Text);

    private void kcRichTextBoxPaste_Execute(object? sender, EventArgs e) => krtbContents.Paste();

    private void kcTextBoxCut_Execute(object? sender, EventArgs e) => ktxtStringCollection.Cut();

    private void kcTextBoxCopy_Execute(object? sender, EventArgs e) => Clipboard.SetText(ktxtStringCollection.Text);

    private void kcTextBoxPaste_Execute(object? sender, EventArgs e) => ktxtStringCollection.Paste();

    private void kcRichTextBoxSelectAll_Execute(object? sender, EventArgs e) => krtbContents.SelectAll();

    private void kcTextBoxSelectAll_Execute(object? sender, EventArgs e) => ktxtStringCollection.SelectAll();

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