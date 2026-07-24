#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonToolStripItemStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_CLEAR_CLIPBOARD_TEXT = @"C&lear Clipboard";

    private const string DEFAULT_MOST_RECENTLY_USED_TEXT = @"Mo&st Recently Used...";

    private const string DEFAULT_CLEAR_RECENTLY_USED_TEXT = @"C&lear Recently Used";

    private const string DEFAULT_NO_RECENTLY_USED_TEXT = @"(No Recently Used Files)";

    private const string DEFAULT_CLEAR_RECENTLY_USED_CONFIRMATION_TEXT = @"Are you sure you want to clear the recently used file list?";

    private const string DEFAULT_CLEAR_RECENTLY_USED_CONFIRMATION_CAPTION = @"Clear Recently Used Files";

    private const string DEFAULT_CLEAR_CLIPBOARD_CONFIRMATION_TEXT = @"Are you sure you want to clear the clipboard?";

    private const string DEFAULT_CLEAR_CLIPBOARD_CONFIRMATION_CAPTION = @"Clear Clipboard";

    private const string DEFAULT_MOST_RECENTLY_USED_FILE_NOT_FOUND_TEXT = @"doesn't exist. Remove from recent files?";

    private const string DEFAULT_MOST_RECENTLY_USED_FILE_NOT_FOUND_CAPTION = @"File Not Found";

    #endregion

    #region Identity

    public KryptonToolStripItemStrings()
    {
        Reset();
    }

    #endregion

    #region Public Overrides

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region IsDefault

    [Browsable(false)]
    public bool IsDefault => ClearClipboardText.Equals(DEFAULT_CLEAR_CLIPBOARD_TEXT) &&
                             MostRecentlyUsedText.Equals(DEFAULT_MOST_RECENTLY_USED_TEXT) &&
                             ClearRecentlyUsedText.Equals(DEFAULT_CLEAR_RECENTLY_USED_TEXT) &&
                             NoRecentlyUsedText.Equals(DEFAULT_NO_RECENTLY_USED_TEXT) &&
                             MostRecentlyUsedFileNotFoundText.Equals(DEFAULT_MOST_RECENTLY_USED_FILE_NOT_FOUND_TEXT) &&
                             MostRecentlyUsedFileNotFoundCaption.Equals(DEFAULT_MOST_RECENTLY_USED_FILE_NOT_FOUND_CAPTION) &&
                             ClearRecentlyUsedConfirmationText.Equals(DEFAULT_CLEAR_RECENTLY_USED_CONFIRMATION_TEXT) &&
                             ClearRecentlyUsedConfirmationCaption.Equals(DEFAULT_CLEAR_RECENTLY_USED_CONFIRMATION_CAPTION) &&
                             ClearClipboardConfirmationText.Equals(DEFAULT_CLEAR_CLIPBOARD_CONFIRMATION_TEXT) &&
                             ClearClipboardConfirmationCaption.Equals(DEFAULT_CLEAR_CLIPBOARD_CONFIRMATION_CAPTION);

    #endregion

                             #region Public

    [Category(@"Visuals")]
    [Description(@"Clear Clipboard text used for custom situations.")]
    [DefaultValue(DEFAULT_CLEAR_CLIPBOARD_TEXT)]
    [Localizable(true)]
    public string ClearClipboardText { get; set; }

    [Category(@"Visuals")]
    [Description(@"Most Recently Used text used for custom situations.")]
    [DefaultValue(DEFAULT_MOST_RECENTLY_USED_TEXT)]
    [Localizable(true)]
    public string MostRecentlyUsedText { get; set; }

    [Category(@"Visuals")]
    [Description(@"Clear Recently Used text used for custom situations.")]
    [DefaultValue(DEFAULT_CLEAR_RECENTLY_USED_TEXT)]
    [Localizable(true)]
    public string ClearRecentlyUsedText { get; set; }

    [Category(@"Visuals")]
    [Description(@"No Recently Used text used for custom situations.")]
    [DefaultValue(DEFAULT_NO_RECENTLY_USED_TEXT)]
    [Localizable(true)]
    public string NoRecentlyUsedText { get; set; }

    [Category(@"Visuals")]
    [Description(@"Most Recently Used file not found text used for custom situations.")]
    [DefaultValue(DEFAULT_MOST_RECENTLY_USED_FILE_NOT_FOUND_TEXT)]
    [Localizable(true)]
    public string MostRecentlyUsedFileNotFoundText { get; set; }

    [Category(@"Visuals")]
    [Description(@"Most Recently Used file not found caption used for custom situations.")]
    [DefaultValue(DEFAULT_MOST_RECENTLY_USED_FILE_NOT_FOUND_CAPTION)]
    [Localizable(true)]
    public string MostRecentlyUsedFileNotFoundCaption { get; set; }

    [Category(@"Visuals")]
    [Description(@"Clear Recently Used confirmation text used for custom situations.")]
    [DefaultValue(DEFAULT_CLEAR_RECENTLY_USED_CONFIRMATION_TEXT)]
    [Localizable(true)]
    public string ClearRecentlyUsedConfirmationText { get; set; }

    [Category(@"Visuals")]
    [Description(@"Clear Recently Used confirmation caption used for custom situations.")]
    [DefaultValue(DEFAULT_CLEAR_RECENTLY_USED_CONFIRMATION_CAPTION)]
    [Localizable(true)]
    public string ClearRecentlyUsedConfirmationCaption { get; set; }

    [Category(@"Visuals")]
    [Description(@"Clear Clipboard confirmation text used for custom situations.")]
    [DefaultValue(DEFAULT_CLEAR_CLIPBOARD_CONFIRMATION_TEXT)]
    [Localizable(true)]
    public string ClearClipboardConfirmationText { get; set; }

    [Category(@"Visuals")]
    [Description(@"Clear Clipboard confirmation caption used for custom situations.")]
    [DefaultValue(DEFAULT_CLEAR_CLIPBOARD_CONFIRMATION_CAPTION)]
    [Localizable(true)]
    public string ClearClipboardConfirmationCaption { get; set; }

    #endregion

    #region Reset

    public void Reset()
    {
        ClearClipboardText = DEFAULT_CLEAR_CLIPBOARD_TEXT;
        MostRecentlyUsedText = DEFAULT_MOST_RECENTLY_USED_TEXT;
        ClearRecentlyUsedText = DEFAULT_CLEAR_RECENTLY_USED_TEXT;
        NoRecentlyUsedText = DEFAULT_NO_RECENTLY_USED_TEXT;
        MostRecentlyUsedFileNotFoundText = DEFAULT_MOST_RECENTLY_USED_FILE_NOT_FOUND_TEXT;
        MostRecentlyUsedFileNotFoundCaption = DEFAULT_MOST_RECENTLY_USED_FILE_NOT_FOUND_CAPTION;
        ClearRecentlyUsedConfirmationText = DEFAULT_CLEAR_RECENTLY_USED_CONFIRMATION_TEXT;
        ClearRecentlyUsedConfirmationCaption = DEFAULT_CLEAR_RECENTLY_USED_CONFIRMATION_CAPTION;
        ClearClipboardConfirmationText = DEFAULT_CLEAR_CLIPBOARD_CONFIRMATION_TEXT;
        ClearClipboardConfirmationCaption = DEFAULT_CLEAR_CLIPBOARD_CONFIRMATION_CAPTION;
    }

    #endregion
}