#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved. 
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

    private const string DEFAULT_ADMINISTRATOR = @"Administrator";
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

    // user32.dll MessageBox / common dialog button string IDs (MUI language packs).
    private const uint USER32_RESOURCE_ID_OK = 800;
    private const uint USER32_RESOURCE_ID_CANCEL = 801;
    private const uint USER32_RESOURCE_ID_ABORT = 802;
    private const uint USER32_RESOURCE_ID_RETRY = 803;
    private const uint USER32_RESOURCE_ID_IGNORE = 804;
    private const uint USER32_RESOURCE_ID_YES = 805;
    private const uint USER32_RESOURCE_ID_NO = 806;
    private const uint USER32_RESOURCE_ID_CLOSE = 807;
    private const uint USER32_RESOURCE_ID_HELP = 808;
    private const uint USER32_RESOURCE_ID_TRY_AGAIN = 809;
    private const uint USER32_RESOURCE_ID_CONTINUE = 810;

    #endregion

    #region Instance Fields

    private bool _useOSStrings;
    private string _ok = DEFAULT_OK;
    private string _cancel = DEFAULT_CANCEL;
    private string _yes = DEFAULT_YES;
    private string _no = DEFAULT_NO;
    private string _abort = DEFAULT_ABORT;
    private string _retry = DEFAULT_RETRY;
    private string _ignore = DEFAULT_IGNORE;
    private string _close = DEFAULT_CLOSE;
    private string _help = DEFAULT_HELP;
    private string _continue = DEFAULT_CONTINUE;
    private string _tryAgain = DEFAULT_TRY_AGAIN;

    private string? _cachedOk;
    private string? _cachedCancel;
    private string? _cachedYes;
    private string? _cachedNo;
    private string? _cachedAbort;
    private string? _cachedRetry;
    private string? _cachedIgnore;
    private string? _cachedClose;
    private string? _cachedHelp;
    private string? _cachedContinue;
    private string? _cachedTryAgain;

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
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion

    #region Public

    /// <summary>
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    /// <returns>True if all values are defaulted; otherwise false.</returns>
    [Browsable(false)]
    public bool IsDefault => !_useOSStrings &&
                             Administrator.Equals(DEFAULT_ADMINISTRATOR) &&
                             _ok.Equals(DEFAULT_OK) &&
                             _cancel.Equals(DEFAULT_CANCEL) &&
                             _yes.Equals(DEFAULT_YES) &&
                             _no.Equals(DEFAULT_NO) &&
                             _abort.Equals(DEFAULT_ABORT) &&
                             _retry.Equals(DEFAULT_RETRY) &&
                             _ignore.Equals(DEFAULT_IGNORE) &&
                             _close.Equals(DEFAULT_CLOSE) &&
                             Today.Equals(DEFAULT_TODAY) &&
                             _help.Equals(DEFAULT_HELP) &&
                             _continue.Equals(DEFAULT_CONTINUE) &&
                             _tryAgain.Equals(DEFAULT_TRY_AGAIN) &&
                             Cut.Equals(DEFAULT_CUT) &&
                             Copy.Equals(DEFAULT_COPY) &&
                             Paste.Equals(DEFAULT_PASTE) &&
                             SelectAll.Equals(DEFAULT_SELECT_ALL);

    /// <summary>
    /// Reset all strings to default values.
    /// </summary>
    public void Reset()
    {
        UseOSStrings = false;
        Administrator = DEFAULT_ADMINISTRATOR;
        _ok = DEFAULT_OK;
        _cancel = DEFAULT_CANCEL;
        _yes = DEFAULT_YES;
        _no = DEFAULT_NO;
        _abort = DEFAULT_ABORT;
        _retry = DEFAULT_RETRY;
        _ignore = DEFAULT_IGNORE;
        _close = DEFAULT_CLOSE;
        Today = DEFAULT_TODAY;
        _help = DEFAULT_HELP;
        Cut = DEFAULT_CUT;
        Copy = DEFAULT_COPY;
        Paste = DEFAULT_PASTE;
        SelectAll = DEFAULT_SELECT_ALL;
        _continue = DEFAULT_CONTINUE;
        _tryAgain = DEFAULT_TRY_AGAIN;
        ClearOsStringCache();
    }

    /// <summary>
    /// Gets or sets a value indicating whether dialog button strings prefer Windows language-pack (MUI) text from user32.dll.
    /// </summary>
    /// <value><c>true</c> to use OS-defined strings; otherwise <c>false</c> to use toolkit/custom strings.</value>
    [Category(@"Visuals")]
    [Description(@"When true, OK/Cancel/Yes/No and related dialog buttons use strings from the installed Windows language pack.")]
    [DefaultValue(false)]
    public bool UseOSStrings
    {
        get => _useOSStrings;
        set
        {
            if (_useOSStrings != value)
            {
                _useOSStrings = value;
                ClearOsStringCache();
            }
        }
    }

    /// <summary>
    /// Gets and sets the Administrator string used in KryptonForm.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Administrator string used for KryptonForm.")]
    [DefaultValue(DEFAULT_ADMINISTRATOR)]
    [RefreshProperties(RefreshProperties.All)]
    public string Administrator { get; set; }

    /// <summary>
    /// Gets and sets the OK string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"OK string used for message box buttons.")]
    [DefaultValue(DEFAULT_OK)]
    [RefreshProperties(RefreshProperties.All)]
    public string OK
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_OK, DEFAULT_OK, ref _cachedOk)
            : _ok;
        set => _ok = value;
    }

    /// <summary>
    /// Gets and sets the Cancel string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cancel string used for message box buttons.")]
    [DefaultValue(DEFAULT_CANCEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string Cancel
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_CANCEL, DEFAULT_CANCEL, ref _cachedCancel)
            : _cancel;
        set => _cancel = value;
    }

    /// <summary>
    /// Gets and sets the Yes string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Yes string used for message box buttons.")]
    [DefaultValue(DEFAULT_YES)]
    [RefreshProperties(RefreshProperties.All)]
    public string Yes
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_YES, DEFAULT_YES, ref _cachedYes)
            : _yes;
        set => _yes = value;
    }

    /// <summary>
    /// Gets and sets the No string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"No string used for message box buttons.")]
    [DefaultValue(DEFAULT_NO)]
    [RefreshProperties(RefreshProperties.All)]
    public string No
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_NO, DEFAULT_NO, ref _cachedNo)
            : _no;
        set => _no = value;
    }

    /// <summary>
    /// Gets and sets the Abort string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Abort string used for message box buttons.")]
    [DefaultValue(DEFAULT_ABORT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Abort
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_ABORT, DEFAULT_ABORT, ref _cachedAbort)
            : _abort;
        set => _abort = value;
    }

    /// <summary>
    /// Gets and sets the Retry string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Retry string used for message box buttons.")]
    [DefaultValue(DEFAULT_RETRY)]
    [RefreshProperties(RefreshProperties.All)]
    public string Retry
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_RETRY, DEFAULT_RETRY, ref _cachedRetry)
            : _retry;
        set => _retry = value;
    }

    /// <summary>
    /// Gets and sets the Ignore string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Ignore string used for message box buttons.")]
    [DefaultValue(DEFAULT_IGNORE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Ignore
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_IGNORE, DEFAULT_IGNORE, ref _cachedIgnore)
            : _ignore;
        set => _ignore = value;
    }

    /// <summary>
    /// Gets and sets the Close string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Close string used for message box buttons.")]
    [DefaultValue(DEFAULT_CLOSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Close
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_CLOSE, DEFAULT_CLOSE, ref _cachedClose)
            : _close;
        set => _close = value;
    }

    /// <summary>
    /// Gets and sets the Today string used in calendars.
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
    public string Help
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_HELP, DEFAULT_HELP, ref _cachedHelp)
            : _help;
        set => _help = value;
    }

    /// <summary>
    /// Gets and sets the Continue string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Continue string used for Message Box Buttons.")]
    [DefaultValue(DEFAULT_CONTINUE)]
    public string Continue
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_CONTINUE, DEFAULT_CONTINUE, ref _cachedContinue)
            : _continue;
        set => _continue = value;
    }

    /// <summary>
    /// Gets and sets the Try Again string used in message box buttons.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Try Again string used for Message Box Buttons.")]
    [DefaultValue(DEFAULT_TRY_AGAIN)]
    public string TryAgain
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(Libraries.User32, USER32_RESOURCE_ID_TRY_AGAIN, DEFAULT_TRY_AGAIN, ref _cachedTryAgain)
            : _tryAgain;
        set => _tryAgain = value;
    }

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
    [DefaultValue(DEFAULT_PASTE)]
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

    #region Implementation

    private void ClearOsStringCache()
    {
        _cachedOk = null;
        _cachedCancel = null;
        _cachedYes = null;
        _cachedNo = null;
        _cachedAbort = null;
        _cachedRetry = null;
        _cachedIgnore = null;
        _cachedClose = null;
        _cachedHelp = null;
        _cachedContinue = null;
        _cachedTryAgain = null;
    }

    #endregion
}
