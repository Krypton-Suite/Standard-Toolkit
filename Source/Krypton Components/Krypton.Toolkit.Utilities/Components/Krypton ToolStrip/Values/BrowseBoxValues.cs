#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Expandable browse/reset configuration for <see cref="InternalBrowseBox"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BrowseBoxValues : Storage
{
    #region Constants

    private const string DEFAULT_RESET_TEXT = @"&Reset";
    private const string DEFAULT_RESET_TOOL_TIP_HEADING = @"Reset";
    private const string DEFAULT_RESET_TOOL_TIP_DESCRIPTION = @"Resets the text of the text box.";

    #endregion

    #region Instance Fields

    private readonly InternalBrowseBox _owner;
    private bool _useSaveDialog;
    private bool _showResetButton;
    private bool _isFolderPicker;
    private string? _fileDialogFilter;
    private string? _initialDirectory;
    private string _resetText = DEFAULT_RESET_TEXT;
    private string _resetTextToolTipHeading = DEFAULT_RESET_TOOL_TIP_HEADING;
    private string _resetTextToolTipDescription = DEFAULT_RESET_TOOL_TIP_DESCRIPTION;
    private Image? _smallResetImage;
    private Image? _largeResetImage;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="BrowseBoxValues"/> class.
    /// </summary>
    /// <param name="owner">Owning internal browse box.</param>
    public BrowseBoxValues(InternalBrowseBox owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        !_useSaveDialog &&
        !_showResetButton &&
        !_isFolderPicker &&
        _fileDialogFilter is null &&
        _initialDirectory is null &&
        _resetText == DEFAULT_RESET_TEXT &&
        _resetTextToolTipHeading == DEFAULT_RESET_TOOL_TIP_HEADING &&
        _resetTextToolTipDescription == DEFAULT_RESET_TOOL_TIP_DESCRIPTION &&
        _smallResetImage is null &&
        _largeResetImage is null;

    #endregion

    #region Public

    /// <summary>Gets or sets a value indicating whether to use the save file dialog.</summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Gets or sets a value indicating whether to use the save file dialog.")]
    public bool UseSaveDialog
    {
        get => _useSaveDialog;
        set => _useSaveDialog = value;
    }

    /// <summary>Gets or sets a value indicating whether to show the reset button.</summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description(@"Gets or sets a value indicating whether to show the reset button.")]
    public bool ShowResetButton
    {
        get => _showResetButton;
        set { _showResetButton = value; _owner.Invalidate(); }
    }

    /// <summary>Gets or sets a value indicating whether the open dialog is a folder picker.</summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Gets or sets a value indicating whether the open dialog is a folder picker.")]
    public bool IsFolderPicker
    {
        get => _isFolderPicker;
        set => _isFolderPicker = value;
    }

    /// <summary>Gets or sets the file dialog filter string (WinForms filter format).</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"Gets or sets the file dialog filter string, e.g. 'Text files (*.txt)|*.txt|All files (*.*)|*.*'.")]
    public string? FileDialogFilter
    {
        get => _fileDialogFilter;
        set => _fileDialogFilter = value;
    }

    /// <summary>Gets or sets the initial directory.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"Gets or sets the initial directory.")]
    public string? InitialDirectory
    {
        get => _initialDirectory;
        set => _initialDirectory = value;
    }

    /// <summary>Gets or sets the reset text.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_RESET_TEXT)]
    [Description(@"Gets or sets the reset text.")]
    public string ResetText
    {
        get => _resetText;
        set { _resetText = value; _owner.Invalidate(); }
    }

    /// <summary>Gets or sets the reset text tool tip heading.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_RESET_TOOL_TIP_HEADING)]
    [Description(@"Gets or sets the reset text tool tip heading.")]
    public string ResetTextToolTipHeading
    {
        get => _resetTextToolTipHeading;
        set => _resetTextToolTipHeading = value;
    }

    /// <summary>Gets or sets the reset text tool tip description.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_RESET_TOOL_TIP_DESCRIPTION)]
    [Description(@"Gets or sets the reset text tool tip description.")]
    public string ResetTextToolTipDescription
    {
        get => _resetTextToolTipDescription;
        set => _resetTextToolTipDescription = value;
    }

    /// <summary>Gets or sets the small reset image.</summary>
    [Category(@"Appearance")]
    [DefaultValue(null)]
    [Description(@"Gets or sets the small reset image.")]
    public Image? SmallResetImage
    {
        get => _smallResetImage;
        set => _smallResetImage = value;
    }

    /// <summary>Gets or sets the large reset image.</summary>
    [Category(@"Appearance")]
    [DefaultValue(null)]
    [Description(@"Gets or sets the large reset image.")]
    public Image? LargeResetImage
    {
        get => _largeResetImage;
        set => _largeResetImage = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _useSaveDialog = false;
        _showResetButton = false;
        _isFolderPicker = false;
        _fileDialogFilter = null;
        _initialDirectory = null;
        _resetText = DEFAULT_RESET_TEXT;
        _resetTextToolTipHeading = DEFAULT_RESET_TOOL_TIP_HEADING;
        _resetTextToolTipDescription = DEFAULT_RESET_TOOL_TIP_DESCRIPTION;
        _smallResetImage = null;
        _largeResetImage = null;
        _owner.Invalidate();
    }

    #endregion
}
