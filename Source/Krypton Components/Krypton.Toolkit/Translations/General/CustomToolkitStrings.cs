#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes a custom set of strings that are used within the Krypton Toolkit, and are localisable.</summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CustomToolkitStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_APPLY = @"A&pply"; // Accelerator key - P
    private const string DEFAULT_BACK = @"Bac&k"; // Accelerator key - K
    private const string DEFAULT_COLLAPSE = @"C&ollapse"; // Accelerator key - O
    private const string DEFAULT_EXPAND = @"Ex&pand"; // Accelerator key - P
    private const string DEFAULT_EXIT = @"E&xit"; // Accelerator key - X
    private const string DEFAULT_FINISH = @"&Finish"; // Accelerator key - F
    private const string DEFAULT_NEXT = @"&Next"; // Accelerator key - N
    private const string DEFAULT_PREVIOUS = "Pre&vious"; // Accelerator key - V
    private const string DEFAULT_CUT = @"C&ut"; // Accelerator key - U
    private const string DEFAULT_COPY = @"&Copy"; // Accelerator key - C
    private const string DEFAULT_PASTE = @"Pas&te"; // Accelerator key - T
    private const string DEFAULT_SELECT_ALL = @"Sel&ect All"; // Accelerator key - E
    private const string DEFAULT_CLEAR_CLIPBOARD = @"Clear Clipboa&rd"; // Accelerator key - R
    private const string DEFAULT_YES_TO_ALL = @"Yes &to All"; // Accelerator key - T
    private const string DEFAULT_NO_TO_ALL = @"No t&o All"; // Accelerator key - O
    private const string DEFAULT_OK_TO_ALL = @"O&k to All"; // Accelerator key - K
    private const string DEFAULT_RESET = @"&Reset"; // Accelerator key - R
    private const string DEFAULT_SYSTEM_INFORMATION = "S&ystem Information";
    private const string DEFAULT_CURRENT_THEME = @"Current Theme";
    private const string DEFAULT_DO_NOT_SHOW_AGAIN = @"&Do not show again";
    private const string DEFAULT_TOGGLE_SWITCH_ON = @"On";
    private const string DEFAULT_TOGGLE_SWITCH_OFF = @"Off";

    // Note: The following may not be needed...
    /*private const string DEFAULT_MORE_DETAILS = "M&ore Details...";
    private const string DEFAULT_LESS_DETAILS = "Les&s Details...";*/

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="CustomToolkitStrings" /> class.</summary>
    public CustomToolkitStrings()
    {
        ResetValues();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    /// <returns>True if all values are defaulted; otherwise false.</returns>
    [Browsable(false)]
    public bool IsDefault => Apply.Equals(DEFAULT_APPLY) &&
                             Collapse.Equals(DEFAULT_COLLAPSE) &&
                             Expand.Equals(DEFAULT_EXPAND) &&
                             Apply.Equals(DEFAULT_APPLY) &&
                             Back.Equals(DEFAULT_BACK) &&
                             Exit.Equals(DEFAULT_EXIT) &&
                             DoNotShowAgain.Equals(DEFAULT_DO_NOT_SHOW_AGAIN) &&
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
                             Reset.Equals(DEFAULT_RESET) &&
                             SystemInformation.Equals(DEFAULT_SYSTEM_INFORMATION) &&
                             CurrentTheme.Equals(DEFAULT_CURRENT_THEME) &&
                             On.Equals(DEFAULT_TOGGLE_SWITCH_ON) &&
                             Off.Equals(DEFAULT_TOGGLE_SWITCH_OFF);

    /// <summary>Resets the values.</summary>
    public void ResetValues()
    {
        Apply = DEFAULT_APPLY;
        Collapse = DEFAULT_COLLAPSE;
        Expand = DEFAULT_EXPAND;
        Apply = DEFAULT_APPLY;
        Back = DEFAULT_BACK;
        DoNotShowAgain = DEFAULT_DO_NOT_SHOW_AGAIN;
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
        SystemInformation = DEFAULT_SYSTEM_INFORMATION;
        CurrentTheme = DEFAULT_CURRENT_THEME;
        On = DEFAULT_TOGGLE_SWITCH_ON;
        Off = DEFAULT_TOGGLE_SWITCH_OFF;
    }

    /// <summary>Gets or sets the collapse string used in expandable footers.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Collapse string used in expandable footers.")]
    [DefaultValue(DEFAULT_COLLAPSE)]
    public string Collapse { get; set; }

    /// <summary>Gets or sets the expand string used in expandable footers.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Expand string used in expandable footers.")]
    [DefaultValue(DEFAULT_EXPAND)]
    public string Expand { get; set; }

    /// <summary>
    /// Gets and sets the Apply string used in property dialogs.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Apply string used for property dialogs.")]
    [DefaultValue(DEFAULT_APPLY)]
    public string Apply { get; set; }

    /// <summary>
    /// Gets and sets the Back string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Back string used for custom situations.")]
    [DefaultValue(DEFAULT_BACK)]
    public string Back { get; set; }

    /// <summary>
    /// Gets and sets the do not show again string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Do not show again string used for custom situations.")]
    [DefaultValue(DEFAULT_DO_NOT_SHOW_AGAIN)]
    public string DoNotShowAgain { get; set; }

    /// <summary>
    /// Gets and sets the Exit string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Exit string used for custom situations.")]
    [DefaultValue(DEFAULT_EXIT)]
    public string Exit { get; set; }

    /// <summary>
    /// Gets and sets the Finish string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Finish string used for custom situations.")]
    [DefaultValue(DEFAULT_FINISH)]
    public string Finish { get; set; }

    /// <summary>
    /// Gets and sets the Next string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Next string used for custom situations.")]
    [DefaultValue(DEFAULT_NEXT)]
    public string Next { get; set; }

    /// <summary>
    /// Gets and sets the Previous string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Previous string used for custom situations.")]
    [DefaultValue(DEFAULT_PREVIOUS)]
    public string Previous { get; set; }

    /// <summary>
    /// Gets and sets the Cut string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cut string used for custom situations.")]
    [DefaultValue(DEFAULT_CUT)]
    public string Cut { get; set; }

    /// <summary>
    /// Gets and sets the Copy string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Copy string used for custom situations.")]
    [DefaultValue(DEFAULT_COPY)]
    public string Copy { get; set; }

    /// <summary>
    /// Gets and sets the Paste string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Paste string used for custom situations.")]
    [DefaultValue(DEFAULT_PASTE)]
    public string Paste { get; set; }

    /// <summary>
    /// Gets and sets the Select All string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Select All string used for custom situations.")]
    [DefaultValue(DEFAULT_SELECT_ALL)]
    public string SelectAll { get; set; }

    /// <summary>
    /// Gets and sets the Clear Clipboard string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Clear Clipboard string used for custom situations.")]
    [DefaultValue(DEFAULT_CLEAR_CLIPBOARD)]
    public string ClearClipboard { get; set; }

    /// <summary>
    /// Gets and sets the Yes to All string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Yes to All string used for custom situations.")]
    [DefaultValue(DEFAULT_YES_TO_ALL)]
    public string YesToAll { get; set; }

    /// <summary>
    /// Gets and sets the No to All string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"No to All string used for custom situations.")]
    [DefaultValue(DEFAULT_NO_TO_ALL)]
    public string NoToAll { get; set; }

    /// <summary>
    /// Gets and sets the Ok to All string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Ok to All string used for custom situations.")]
    [DefaultValue(DEFAULT_OK_TO_ALL)]
    public string OkToAll { get; set; }

    /// <summary>Gets or sets the reset string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Reset string used for custom situations.")]
    [DefaultValue(DEFAULT_RESET)]
    public string Reset { get; set; }

    /// <summary>Gets or sets the system information string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"System information string used for custom situations.")]
    [DefaultValue(DEFAULT_SYSTEM_INFORMATION)]
    public string SystemInformation { get; set; }

    /// <summary>Gets or sets the current theme string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Current theme string used for custom situations.")]
    [DefaultValue(DEFAULT_CURRENT_THEME)]
    public string CurrentTheme { get; set; }

    /// <summary>Gets or sets the on.</summary>
    /// <value>The on.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"'On' string used for custom situations.")]
    [DefaultValue(DEFAULT_TOGGLE_SWITCH_ON)]
    public string On { get; set; }

    /// <summary>Gets or sets the off.</summary>
    /// <value>The off.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"'Off' string used for custom situations.")]
    [DefaultValue(DEFAULT_TOGGLE_SWITCH_OFF)]
    public string Off { get; set; }

    #endregion
}