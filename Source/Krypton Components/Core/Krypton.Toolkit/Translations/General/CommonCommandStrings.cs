#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Exposes common command strings used within the Krypton Toolkit that are localisable.
/// </summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CommonCommandStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_APPLY = @"A&pply";
    private const string DEFAULT_BACK = @"Bac&k";
    private const string DEFAULT_EXIT = @"E&xit";
    private const string DEFAULT_FINISH = @"&Finish";
    private const string DEFAULT_NEXT = @"&Next";
    private const string DEFAULT_PREVIOUS = @"Pre&vious";
    private const string DEFAULT_CUT = @"C&ut";
    private const string DEFAULT_COPY = @"&Copy";
    private const string DEFAULT_PASTE = @"Pas&te";
    private const string DEFAULT_SELECT_ALL = @"Sel&ect All";
    private const string DEFAULT_CLEAR_CLIPBOARD = @"Clear Clipboa&rd";
    private const string DEFAULT_YES_TO_ALL = @"Yes &to All";
    private const string DEFAULT_NO_TO_ALL = @"No t&o All";
    private const string DEFAULT_OK_TO_ALL = @"O&k to All";
    private const string DEFAULT_RESET = @"&Reset";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="CommonCommandStrings"/> class.</summary>
    public CommonCommandStrings()
    {
        ResetValues();
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    [Browsable(false)]
    public bool IsDefault => Apply.Equals(DEFAULT_APPLY) &&
                             Back.Equals(DEFAULT_BACK) &&
                             Exit.Equals(DEFAULT_EXIT) &&
                             Finish.Equals(DEFAULT_FINISH) &&
                             Next.Equals(DEFAULT_NEXT) &&
                             Previous.Equals(DEFAULT_PREVIOUS) &&
                             Cut.Equals(DEFAULT_CUT) &&
                             Copy.Equals(DEFAULT_COPY) &&
                             Paste.Equals(DEFAULT_PASTE) &&
                             SelectAll.Equals(DEFAULT_SELECT_ALL) &&
                             ClearClipboard.Equals(DEFAULT_CLEAR_CLIPBOARD) &&
                             YesToAll.Equals(DEFAULT_YES_TO_ALL) &&
                             NoToAll.Equals(DEFAULT_NO_TO_ALL) &&
                             OkToAll.Equals(DEFAULT_OK_TO_ALL) &&
                             Reset.Equals(DEFAULT_RESET);

    /// <summary>
    /// Reset all strings to default values.
    /// </summary>
    public void ResetValues()
    {
        Apply = DEFAULT_APPLY;
        Back = DEFAULT_BACK;
        Exit = DEFAULT_EXIT;
        Finish = DEFAULT_FINISH;
        Next = DEFAULT_NEXT;
        Previous = DEFAULT_PREVIOUS;
        Cut = DEFAULT_CUT;
        Copy = DEFAULT_COPY;
        Paste = DEFAULT_PASTE;
        SelectAll = DEFAULT_SELECT_ALL;
        ClearClipboard = DEFAULT_CLEAR_CLIPBOARD;
        YesToAll = DEFAULT_YES_TO_ALL;
        NoToAll = DEFAULT_NO_TO_ALL;
        OkToAll = DEFAULT_OK_TO_ALL;
        Reset = DEFAULT_RESET;
    }

    /// <summary>Gets and sets the Apply string used in property dialogs.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Apply string used for property dialogs.")]
    [DefaultValue(DEFAULT_APPLY)]
    public string Apply { get; set; }

    /// <summary>Gets and sets the Back string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Back string used for custom situations.")]
    [DefaultValue(DEFAULT_BACK)]
    public string Back { get; set; }

    /// <summary>Gets and sets the Exit string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Exit string used for custom situations.")]
    [DefaultValue(DEFAULT_EXIT)]
    public string Exit { get; set; }

    /// <summary>Gets and sets the Finish string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Finish string used for custom situations.")]
    [DefaultValue(DEFAULT_FINISH)]
    public string Finish { get; set; }

    /// <summary>Gets and sets the Next string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Next string used for custom situations.")]
    [DefaultValue(DEFAULT_NEXT)]
    public string Next { get; set; }

    /// <summary>Gets and sets the Previous string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Previous string used for custom situations.")]
    [DefaultValue(DEFAULT_PREVIOUS)]
    public string Previous { get; set; }

    /// <summary>Gets and sets the Cut string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cut string used for custom situations.")]
    [DefaultValue(DEFAULT_CUT)]
    public string Cut { get; set; }

    /// <summary>Gets and sets the Copy string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Copy string used for custom situations.")]
    [DefaultValue(DEFAULT_COPY)]
    public string Copy { get; set; }

    /// <summary>Gets and sets the Paste string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Paste string used for custom situations.")]
    [DefaultValue(DEFAULT_PASTE)]
    public string Paste { get; set; }

    /// <summary>Gets and sets the Select All string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Select All string used for custom situations.")]
    [DefaultValue(DEFAULT_SELECT_ALL)]
    public string SelectAll { get; set; }

    /// <summary>Gets and sets the Clear Clipboard string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Clear Clipboard string used for custom situations.")]
    [DefaultValue(DEFAULT_CLEAR_CLIPBOARD)]
    public string ClearClipboard { get; set; }

    /// <summary>Gets and sets the Yes to All string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Yes to All string used for custom situations.")]
    [DefaultValue(DEFAULT_YES_TO_ALL)]
    public string YesToAll { get; set; }

    /// <summary>Gets and sets the No to All string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"No to All string used for custom situations.")]
    [DefaultValue(DEFAULT_NO_TO_ALL)]
    public string NoToAll { get; set; }

    /// <summary>Gets and sets the Ok to All string used in custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Ok to All string used for custom situations.")]
    [DefaultValue(DEFAULT_OK_TO_ALL)]
    public string OkToAll { get; set; }

    /// <summary>Gets or sets the Reset string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Reset string used for custom situations.")]
    [DefaultValue(DEFAULT_RESET)]
    public string Reset { get; set; }

    #endregion
}