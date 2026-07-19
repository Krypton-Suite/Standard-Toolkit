#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2022 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualMultilineStringEditorForm : KryptonForm
{
    #region Instance Fields

    private bool _useRichTextBox;

    private string _headerText;

    private string _windowTitle;

    private string[]? _contents;

    private StringCollection? _collection;

    #endregion

    #region Identity

    public VisualMultilineStringEditorForm()
    {
        SetInheritedControlOverride();
        InitializeComponent();
        ConfigureDesignerChrome();
        InitialSetup();
        SetupControlsText();
    }

    public VisualMultilineStringEditorForm(string[]? contents, StringCollection? collection, bool? useRichTextBox, string? headerText, string? windowTitle)
    {
        SetInheritedControlOverride();
        InitializeComponent();
        ConfigureDesignerChrome();

        _contents = contents ?? [string.Empty];

        _collection = collection ?? [];

        _useRichTextBox = useRichTextBox ?? true;

        _headerText = headerText ?? @"Enter the strings in the collection (one per line):";

        _windowTitle = windowTitle ?? @"String Collection Editor";

        kryptonGroupBox1.Values.Heading = _headerText;

        Text = _windowTitle;

        SetupControlsText();

        SetupInputCanvas();

        UpdateInput(_contents, _collection);
    }

    #endregion

    #region Protected
    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        KryptonDesignerEditorDpi.Configure(this);
        base.OnLoad(e);
    }

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        KryptonDesignerEditorDpi.ApplyOnShown(this);
    }

    #endregion

    #region Implementation

    private void ConfigureDesignerChrome()
    {
        InternalDesignerEditorFormChrome.Apply(this, kpnlContent, kpnlButtonBar);
        kpnlButtonBar.OkButton.Click += kbtnOk_Click;
    }

    private void SetupControlsText()
    {
        kpnlButtonBar.OkButton.Values.Text = KryptonManager.Strings.GeneralStrings.OK;

        kpnlButtonBar.CancelButton.Values.Text = KryptonManager.Strings.GeneralStrings.Cancel;

        kcRichTextBoxCopy.Text = KryptonManager.Strings.ToolBarStrings.Copy;

        kcRichTextBoxCut.Text = KryptonManager.Strings.ToolBarStrings.Cut;

        kcRichTextBoxPaste.Text = KryptonManager.Strings.ToolBarStrings.Paste;

        kcRichTextBoxSelectAll.Text = KryptonManager.Strings.CustomStrings.SelectAll;

        kcTextBoxCopy.Text = KryptonManager.Strings.ToolBarStrings.Copy;

        kcTextBoxCut.Text = KryptonManager.Strings.ToolBarStrings.Cut;

        kcTextBoxPaste.Text = KryptonManager.Strings.ToolBarStrings.Paste;

        kcTextBoxSelectAll.Text = KryptonManager.Strings.CustomStrings.SelectAll;
    }

    private void SetupVariables(string[]? contents, StringCollection? collection, bool? useRichTextBox, string? headerText, string? windowTitle)
    {
        _contents = contents;

        _collection = collection;

        _useRichTextBox = useRichTextBox ?? true;

        _headerText = headerText ?? @"Enter the strings in the collection (one per line):";

        kryptonGroupBox1.Values.Heading = _headerText;

        Text = windowTitle ?? @"String Collection Editor";
    }

    private void InitialSetup()
    {
        // Set default values

        _useRichTextBox = false;

        _headerText = @"Enter the strings in the collection (one per line):";

        _contents = null;

        _collection = null;
    }

    internal string[] GetEditedLines() =>
        _useRichTextBox ? [.. krtbContents.Lines] : [.. ktxtStringCollection.Lines];

    internal string GetEditedText() =>
        _useRichTextBox ? krtbContents.Text : ktxtStringCollection.Text;

    internal void SetEditText(string text)
    {
        SetupInputCanvas();

        if (_useRichTextBox)
        {
            krtbContents.Text = text;
        }
        else
        {
            ktxtStringCollection.Text = text;
        }
    }

    internal void SetupInputCanvas()
    {
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
    }

    private void UpdateInput(string[]? contents, StringCollection? collection)
    {
        if (_useRichTextBox)
        {
            if (contents != null)
            {
                krtbContents.Lines = contents;
            }
            else if (collection != null)
            {
                string[] tmp = new string[collection.Count];

                collection.CopyTo(tmp, 0);

                krtbContents.Lines = tmp;
            }
        }
        else
        {
            if (contents != null)
            {
                ktxtStringCollection.Lines = contents;
            }
            else if (collection != null)
            {
                // Setup temporary string array
                string[] tmpStrings = new string[collection.Count];

                // Copy the contents of 'collection' into string array
                collection.CopyTo(tmpStrings, 0);

                ktxtStringCollection.Lines = tmpStrings;
            }
        }
    }

    private void kbtnOk_Click(object? sender, EventArgs e) => _contents = _useRichTextBox ? [.. krtbContents.Lines] : [.. ktxtStringCollection.Lines];

    private void kcRichTextBoxCut_Execute(object sender, EventArgs e) => krtbContents.Cut();

    private void kcRichTextBoxCopy_Execute(object sender, EventArgs e) => krtbContents.Copy();

    private void kcRichTextBoxPaste_Execute(object sender, EventArgs e) => krtbContents.Paste();

    private void kcRichTextBoxSelectAll_Execute(object sender, EventArgs e) => krtbContents.SelectAll();

    private void kcTextBoxCut_Execute(object sender, EventArgs e) => ktxtStringCollection.Cut();

    private void kcTextBoxCopy_Execute(object sender, EventArgs e) => ktxtStringCollection.Copy();

    private void kcTextBoxPaste_Execute(object sender, EventArgs e) => ktxtStringCollection.Paste();

    private void kcTextBoxSelectAll_Execute(object sender, EventArgs e) => ktxtStringCollection.SelectAll();

    internal static string[]? InternalShow(IWin32Window? owner, string[] input, bool? useRichTextBox, string? headerText, string windowTitle)
    {
        IWin32Window? showOwner = owner ?? FromHandle(PI.GetActiveWindow());

        using var kmse = new VisualMultilineStringEditorForm(input, null, useRichTextBox, headerText, windowTitle);

        kmse.StartPosition = showOwner == null ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;

        return kmse.ShowDialog(showOwner) == DialogResult.OK ? kmse.GetEditedLines() : null;
    }

    internal static StringCollection? InternalShowStringCollection(IWin32Window? owner, StringCollection input, bool? useRichTextBox, string? headerText, string windowTitle)
    {
        IWin32Window? showOwner = owner ?? FromHandle(PI.GetActiveWindow());

        using var kmse = new VisualMultilineStringEditorForm(null, input, useRichTextBox, headerText, windowTitle);

        kmse.StartPosition = showOwner == null ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;

        if (kmse.ShowDialog(showOwner) != DialogResult.OK)
        {
            return null;
        }

        var collection = new StringCollection();
        collection.AddRange(kmse.GetEditedLines());
        return collection;
    }

    #endregion
}