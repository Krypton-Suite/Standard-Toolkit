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
/// Exposes localizable caption-button (control box) tooltip strings used by <see cref="KryptonForm"/>
/// Minimize / Maximize / Restore / Close / Help buttons.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ControlBoxStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_MINIMIZE = @"Minimize";
    private const string DEFAULT_MAXIMIZE = @"Maximize";
    private const string DEFAULT_RESTORE = @"Restore";
    private const string DEFAULT_RESTORE_UP = @"Restore Up";
    private const string DEFAULT_CLOSE = @"Close";
    private const string DEFAULT_HELP = @"Help";

    #endregion

    #region Instance Fields

    private bool _useOSStrings;
    private string _minimize = DEFAULT_MINIMIZE;
    private string _maximize = DEFAULT_MAXIMIZE;
    private string _restore = DEFAULT_RESTORE;
    private string _restoreUp = DEFAULT_RESTORE_UP;
    private string _close = DEFAULT_CLOSE;
    private string _help = DEFAULT_HELP;

    private string? _cachedMinimize;
    private string? _cachedMaximize;
    private string? _cachedRestore;
    private string? _cachedRestoreUp;
    private string? _cachedClose;
    private string? _cachedHelp;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ControlBoxStrings"/> class.</summary>
    public ControlBoxStrings()
    {
        Reset();
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    [Browsable(false)]
    public bool IsDefault => !_useOSStrings &&
                             _minimize.Equals(DEFAULT_MINIMIZE) &&
                             _maximize.Equals(DEFAULT_MAXIMIZE) &&
                             _restore.Equals(DEFAULT_RESTORE) &&
                             _restoreUp.Equals(DEFAULT_RESTORE_UP) &&
                             _close.Equals(DEFAULT_CLOSE) &&
                             _help.Equals(DEFAULT_HELP);

    /// <summary>
    /// Reset all strings to default values.
    /// </summary>
    public void Reset()
    {
        UseOSStrings = false;
        _minimize = DEFAULT_MINIMIZE;
        _maximize = DEFAULT_MAXIMIZE;
        _restore = DEFAULT_RESTORE;
        _restoreUp = DEFAULT_RESTORE_UP;
        _close = DEFAULT_CLOSE;
        _help = DEFAULT_HELP;
        ClearOsStringCache();
    }

    /// <summary>
    /// Gets or sets a value indicating whether control-box tooltips prefer Windows language-pack (MUI) text from user32.dll.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"When true, Minimize/Maximize/Restore/Close/Help control-box tooltips use strings from the installed Windows language pack.")]
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

    /// <summary>Gets and sets the Minimize control-box tooltip.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Tooltip for the Minimize caption button.")]
    [DefaultValue(DEFAULT_MINIMIZE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Minimize
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(WindowsMuiStringId.Minimize, DEFAULT_MINIMIZE, ref _cachedMinimize)
            : _minimize;
        set => _minimize = value;
    }

    /// <summary>Gets and sets the Maximize control-box tooltip.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Tooltip for the Maximize caption button.")]
    [DefaultValue(DEFAULT_MAXIMIZE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Maximize
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(WindowsMuiStringId.Maximize, DEFAULT_MAXIMIZE, ref _cachedMaximize)
            : _maximize;
        set => _maximize = value;
    }

    /// <summary>
    /// Gets and sets the Restore control-box tooltip (Windows MUI: Restore Down).
    /// Used when the Maximize button toggles to Restore.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Tooltip for the Restore caption button (Restore Down).")]
    [DefaultValue(DEFAULT_RESTORE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Restore
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(WindowsMuiStringId.RestoreDown, DEFAULT_RESTORE, ref _cachedRestore)
            : _restore;
        set => _restore = value;
    }

    /// <summary>
    /// Gets and sets the Restore Up control-box tooltip.
    /// Used when a Minimize button toggles to Restore (for example MDI maximized child).
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Tooltip for Restore Up (minimize button acting as restore).")]
    [DefaultValue(DEFAULT_RESTORE_UP)]
    [RefreshProperties(RefreshProperties.All)]
    public string RestoreUp
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(WindowsMuiStringId.RestoreUp, DEFAULT_RESTORE_UP, ref _cachedRestoreUp)
            : _restoreUp;
        set => _restoreUp = value;
    }

    /// <summary>Gets and sets the Close control-box tooltip.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Tooltip for the Close caption button.")]
    [DefaultValue(DEFAULT_CLOSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Close
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(WindowsMuiStringId.ControlBoxClose, DEFAULT_CLOSE, ref _cachedClose)
            : _close;
        set => _close = value;
    }

    /// <summary>Gets and sets the Help control-box tooltip.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Tooltip for the Help caption button.")]
    [DefaultValue(DEFAULT_HELP)]
    [RefreshProperties(RefreshProperties.All)]
    public string Help
    {
        get => _useOSStrings
            ? OsMuiStringLoader.Load(WindowsMuiStringId.ControlBoxHelp, DEFAULT_HELP, ref _cachedHelp)
            : _help;
        set => _help = value;
    }

    #endregion

    #region Implementation

    private void ClearOsStringCache()
    {
        _cachedMinimize = null;
        _cachedMaximize = null;
        _cachedRestore = null;
        _cachedRestoreUp = null;
        _cachedClose = null;
        _cachedHelp = null;
        OsMuiStringLoader.ClearCache();
    }

    #endregion
}
