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
/// Expandable configuration for <see cref="KryptonMRUSaveAsFileMenuItem"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class MruSaveAsFileMenuItemValues : Storage
{
    #region Constants

    private const string DEFAULT_TEXT = @"Save &As";
    private const string DEFAULT_DIALOG_TITLE = @"Save As:";

    #endregion

    #region Instance Fields

    private bool _useSystemDialogs;
    private Control? _outputControl;
    private string _defaultText = DEFAULT_TEXT;
    private string? _applicationName;
    private string _saveAsFileDialogTitle = DEFAULT_DIALOG_TITLE;
    private string? _rawFilterDisplayName;
    private string? _rawExtensionList;
    private string? _standardDialogFilter;
    private string _startingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private KryptonMRUMenuItem? _parentMruMenuItem;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="MruSaveAsFileMenuItemValues"/> class.
    /// </summary>
    /// <param name="owner">Owning MRU save-as-file menu item.</param>
    public MruSaveAsFileMenuItemValues(KryptonMRUSaveAsFileMenuItem owner)
    {
        _ = owner ?? throw new ArgumentNullException(nameof(owner));
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        !_useSystemDialogs &&
        _outputControl is null &&
        _defaultText == DEFAULT_TEXT &&
        _applicationName is null &&
        _saveAsFileDialogTitle == DEFAULT_DIALOG_TITLE &&
        _rawFilterDisplayName is null &&
        _rawExtensionList is null &&
        _standardDialogFilter is null &&
        _startingDirectory == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) &&
        _parentMruMenuItem is null;

    #endregion

    #region Public

    /// <summary>Gets or sets a value indicating whether to use the native system dialogs over the Windows API CodePack types.</summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Use the native system dialogs over the Windows API CodePack types.")]
    public bool UseSystemDialogs { get => _useSystemDialogs; set => _useSystemDialogs = value; }

    /// <summary>Gets or sets the control to load the file content text into.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The control to load the file content text into.")]
    public Control? OutputControl { get => _outputControl; set => _outputControl = value; }

    /// <summary>Gets or sets the text displayed on the tool strip menu item.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_TEXT)]
    [Description(@"The text displayed on the tool strip menu item.")]
    public string DefaultText { get => _defaultText; set => _defaultText = value; }

    /// <summary>Gets or sets the name of your application. This is used to store the MRU list in the Windows registry.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The name of your application. This is used to store the MRU list in the Windows registry.")]
    public string? ApplicationName { get => _applicationName; set => _applicationName = value; }

    /// <summary>Gets or sets the save as file dialog title.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_DIALOG_TITLE)]
    [Description(@"The title of the save as file dialog.")]
    public string SaveAsFileDialogTitle { get => _saveAsFileDialogTitle; set => _saveAsFileDialogTitle = value; }

    /// <summary>Gets or sets the display name of the raw filter.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The display name of the raw filter.")]
    public string? RawFilterDisplayName { get => _rawFilterDisplayName; set => _rawFilterDisplayName = value; }

    /// <summary>Gets or sets the raw extension list.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The raw extension list.")]
    public string? RawExtensionList { get => _rawExtensionList; set => _rawExtensionList = value; }

    /// <summary>Gets or sets the standard WinForms dialog filter string.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The save as file dialog filter, e.g. 'Text files (*.txt)|*.txt|All files (*.*)|*.*'.")]
    public string? StandardDialogFilter { get => _standardDialogFilter; set => _standardDialogFilter = value; }

    /// <summary>Gets or sets the starting directory of the save as file dialog.</summary>
    [Category(@"Behavior")]
    [DefaultValue(Environment.SpecialFolder.MyDocuments)]
    [Description(@"The starting directory of the save as file dialog.")]
    public string StartingDirectory { get => _startingDirectory; set => _startingDirectory = value; }

    /// <summary>Gets or sets the parent MRU menu item.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The parent MRU menu item.")]
    public KryptonMRUMenuItem? ParentMRUMenuItem { get => _parentMruMenuItem; set => _parentMruMenuItem = value; }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _useSystemDialogs = false;
        _outputControl = null;
        _defaultText = DEFAULT_TEXT;
        _applicationName = null;
        _saveAsFileDialogTitle = DEFAULT_DIALOG_TITLE;
        _rawFilterDisplayName = null;
        _rawExtensionList = null;
        _standardDialogFilter = null;
        _startingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        _parentMruMenuItem = null;
    }

    #endregion
}
