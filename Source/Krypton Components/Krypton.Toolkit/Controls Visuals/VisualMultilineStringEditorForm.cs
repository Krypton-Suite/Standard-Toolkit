#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2022 - 2025. All rights reserved.
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
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        InitialSetup();

        SetupControlsText();
    }

    public VisualMultilineStringEditorForm(string[]? contents, StringCollection? collection, bool? useRichTextBox, string? headerText, string? windowTitle)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _contents = contents ?? [string.Empty];

        _collection = collection ?? [];

        _useRichTextBox = useRichTextBox ?? true;

        _headerText = headerText ?? @"Enter the strings in the collection (one per line):";

        _windowTitle = windowTitle ?? @"String Collection Editor";

        klblHeader.Text = _headerText;

        Text = _windowTitle;

        SetupControlsText();

        UpdateInput(_contents, _collection);
    }

    #endregion

    #region Implementation

    private void SetupControlsText()
    {
        kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;

        kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

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

        klblHeader.Text = _headerText;

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

    private void SetupInputCanvas()
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

    private void kbtnOk_Click(object sender, EventArgs e)
    {
        if (_useRichTextBox)
        {
            foreach (var line in krtbContents.Lines)
            {
                // TODO: This is not right.. It will only have the last line it !
                _contents =
                [
                    line
                ];
            }
        }
        else
        {
            foreach (var line in ktxtStringCollection.Lines)
            {
                // TODO: This is not right.. It will only have the last line it !
                _contents =
                [
                    line
                ];
            }
        }
    }

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
        string[]? collection;

        IWin32Window? showOwner = owner ?? FromHandle(PI.GetActiveWindow());

        using var kmse = new VisualMultilineStringEditorForm(input, null, useRichTextBox, headerText, windowTitle);

        kmse.StartPosition = showOwner == null ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;

        collection = kmse._useRichTextBox ? kmse.krtbContents.Lines : kmse.ktxtStringCollection.Lines;

        return kmse.ShowDialog(showOwner) == DialogResult.OK ? collection : null;
    }

    internal static StringCollection? InternalShowStringCollection(IWin32Window? owner, StringCollection input, bool? useRichTextBox, string? headerText, string windowTitle)
    {
        StringCollection? collection;

        IWin32Window? showOwner = owner ?? FromHandle(PI.GetActiveWindow());

        using var kmse = new VisualMultilineStringEditorForm(null, input, useRichTextBox, headerText, windowTitle);

        kmse.StartPosition = showOwner == null ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;

        if (kmse._useRichTextBox)
        {
            collection = [];

            string[] tmp = kmse.krtbContents.Lines;

            collection.AddRange(tmp);
        }
        else
        {
            collection = [];

            string[] tmp = kmse.ktxtStringCollection.Lines;

            collection.AddRange(tmp);
        }

        return kmse.ShowDialog(showOwner) == DialogResult.OK ? collection : null;
    }

    #endregion
}