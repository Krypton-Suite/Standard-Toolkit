#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

/// <summary>Exposes a general set of strings that are used within the Krypton Toolkit, and are localisable.</summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class GeneralToolkitStrings : GlobalId
{
    #region Static Fields
    private const string DEFAULT_OK = @"O&K"; // Accelerator key - K
    private const string DEFAULT_CANCEL = @"Cance&l"; // Accelerator key - L
    private const string DEFAULT_YES = @"&Yes"; // Accelerator key - Y
    private const string DEFAULT_NO = @"N&o"; // Accelerator key - O
    private const string DEFAULT_ABORT = @"A&bort"; // Accelerator key - B
    private const string DEFAULT_RETRY = @"Ret&ry"; // Accelerator key - R
    private const string DEFAULT_IGNORE = @"I&gnore"; // Accelerator key - G
    private const string DEFAULT_CLOSE = @"Clo&se"; // Accelerator key - S
    private const string DEFAULT_TODAY = @"&Today"; // Accelerator key - T
    private const string DEFAULT_HELP = @"H&elp"; // Accelerator key - E
    private const string DEFAULT_CUT = @"C&ut"; // Accelerator key - U
    private const string DEFAULT_COPY = @"Co&py"; // Accelerator key - P
    private const string DEFAULT_PASTE = @"P&aste"; // Accelerator key - A
    private const string DEFAULT_SELECT_ALL = @"&Select All"; // Accelerator key - S

    // NET 6 & newer
    private const string DEFAULT_CONTINUE = @"Co&ntinue"; // Accelerator key - N
    private const string DEFAULT_TRY_AGAIN = @"Try Aga&in"; // Accelerator key - I

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="GeneralToolkitStrings" /> class.</summary>
    public GeneralToolkitStrings()
    {
        Reset();
    }

    /// <summary>
    /// Returns a string that represents the current defaulted state.
    /// </summary>
    /// <returns>A string that represents the current defaulted state.</returns>
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion

    #region Public

    /// <summary>
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    /// <returns>True if all values are defaulted; otherwise false.</returns>
    [Browsable(false)]
    public bool IsDefault => OK.Equals(DEFAULT_OK) &&
                             Cancel.Equals(DEFAULT_CANCEL) &&
                             Yes.Equals(DEFAULT_YES) &&
                             No.Equals(DEFAULT_NO) &&
                             Abort.Equals(DEFAULT_ABORT) &&
                             Retry.Equals(DEFAULT_RETRY) &&
                             Ignore.Equals(DEFAULT_IGNORE) &&
                             Close.Equals(DEFAULT_CLOSE) &&
                             Today.Equals(DEFAULT_TODAY) &&
                             Help.Equals(DEFAULT_HELP) &&
                             Continue.Equals(DEFAULT_CONTINUE) &&
                             TryAgain.Equals(DEFAULT_TRY_AGAIN) &&
                             Cut.Equals(DEFAULT_CUT) &&
                             Copy.Equals(DEFAULT_COPY) &&
                             Paste.Equals(DEFAULT_PASTE) &&
                             SelectAll.Equals(DEFAULT_SELECT_ALL);

    /// <summary>
    /// Reset all strings to default values.
    /// </summary>
    public void Reset()
    {
        OK = DEFAULT_OK;
        Cancel = DEFAULT_CANCEL;
        Yes = DEFAULT_YES;
        No = DEFAULT_NO;
        Abort = DEFAULT_ABORT;
        Retry = DEFAULT_RETRY;
        Ignore = DEFAULT_IGNORE;
        Close = DEFAULT_CLOSE;
        Today = DEFAULT_TODAY;
        Help = DEFAULT_HELP;
        Cut = DEFAULT_CUT;
        Copy = DEFAULT_COPY;
        Paste = DEFAULT_PASTE;
        SelectAll = DEFAULT_SELECT_ALL;

        // NET 6 & newer
        Continue = DEFAULT_CONTINUE;
        TryAgain = DEFAULT_TRY_AGAIN;

        // Note: The following may not be needed...
        /*MoreDetails = DEFAULT_MORE_DETAILS;
        LessDetails = DEFAULT_LESS_DETAILS;*/
    }

    /// <summary>
    /// Gets and sets the OK string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"OK string used for message box buttons.")]
    [DefaultValue(DEFAULT_OK)]
    [RefreshProperties(RefreshProperties.All)]
    public string OK { get; set; }

    /// <summary>
    /// Gets and sets the Cancel string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cancel string used for message box buttons.")]
    [DefaultValue(DEFAULT_CANCEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string Cancel { get; set; }

    /// <summary>
    /// Gets and sets the Yes string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Yes string used for message box buttons.")]
    [DefaultValue(DEFAULT_YES)]
    [RefreshProperties(RefreshProperties.All)]
    public string Yes { get; set; }

    /// <summary>
    /// Gets and sets the No string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"No string used for message box buttons.")]
    [DefaultValue(DEFAULT_NO)]
    [RefreshProperties(RefreshProperties.All)]
    public string No { get; set; }

    /// <summary>
    /// Gets and sets the Abort string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Abort string used for message box buttons.")]
    [DefaultValue(DEFAULT_ABORT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Abort { get; set; }

    /// <summary>
    /// Gets and sets the Retry string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Retry string used for message box buttons.")]
    [DefaultValue(DEFAULT_RETRY)]
    [RefreshProperties(RefreshProperties.All)]
    public string Retry { get; set; }

    /// <summary>
    /// Gets and sets the Ignore string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Ignore string used for message box buttons.")]
    [DefaultValue(DEFAULT_IGNORE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Ignore { get; set; }

    /// <summary>
    /// Gets and sets the Close string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Close string used for message box buttons.")]
    [DefaultValue(DEFAULT_CLOSE)]

    [RefreshProperties(RefreshProperties.All)]
    public string Close { get; set; }

    /// <summary>
    /// Gets and sets the Close string used in calendars.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Today string used for calendars.")]
    [DefaultValue(DEFAULT_TODAY)]
    [RefreshProperties(RefreshProperties.All)]
    public string Today { get; set; }

    /// <summary>
    /// Gets and sets the Help string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Help string used for Message Box Buttons.")]
    [DefaultValue(DEFAULT_HELP)]
    [RefreshProperties(RefreshProperties.All)]
    public string Help { get; set; }

    /// <summary>
    /// Gets and sets the Continue string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Continue string used for Message Box Buttons.")]
    [DefaultValue(DEFAULT_CONTINUE)]
    public string Continue { get; set; }

    /// <summary>
    /// Gets and sets the Try Again string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Try Again string used for Message Box Buttons.")]
    [DefaultValue(DEFAULT_TRY_AGAIN)]
    public string TryAgain { get; set; }

    /// <summary>
    /// Gets and sets the Cut string.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cut string.")]
    [DefaultValue(DEFAULT_CUT)]
    public string Cut { get; set; }

    /// <summary>
    /// Gets and sets the Copy string.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Copy string.")]
    [DefaultValue(DEFAULT_COPY)]
    public string Copy { get; set; }

    /// <summary>
    /// Gets and sets the Paste string.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Paste string.")]
    [DefaultValue(DEFAULT_CUT)]
    public string Paste { get; set; }

    /// <summary>
    /// Gets and sets the Select All string.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Select All string.")]
    [DefaultValue(DEFAULT_SELECT_ALL)]
    public string SelectAll { get; set; }

    #endregion
}