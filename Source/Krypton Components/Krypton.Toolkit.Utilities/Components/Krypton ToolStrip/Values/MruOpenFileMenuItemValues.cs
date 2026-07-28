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
/// Expandable configuration for <see cref="KryptonMRUOpenFileMenuItem"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class MruOpenFileMenuItemValues : Storage
{
    #region Constants

    private const string DEFAULT_TEXT = @"O&pen";
    private const string DEFAULT_DIALOG_TITLE = @"Open:";

    #endregion

    #region Instance Fields

    private Control? _outputControl;
    private string _defaultText = DEFAULT_TEXT;
    private string? _applicationName;
    private string _openFileDialogTitle = DEFAULT_DIALOG_TITLE;
    private string _standardDialogFilter = string.Empty;
    private string _startingDirectory;
    private KryptonMRUMenuItem? _parentMruMenuItem;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="MruOpenFileMenuItemValues"/> class.
    /// </summary>
    /// <param name="owner">Owning MRU open-file menu item.</param>
    public MruOpenFileMenuItemValues(KryptonMRUOpenFileMenuItem owner)
    {
        _ = owner ?? throw new ArgumentNullException(nameof(owner));
        _startingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _outputControl is null &&
        _defaultText == DEFAULT_TEXT &&
        _applicationName is null &&
        _openFileDialogTitle == DEFAULT_DIALOG_TITLE &&
        _standardDialogFilter.Length == 0 &&
        _startingDirectory == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) &&
        _parentMruMenuItem is null;

    #endregion

    #region Public

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

    /// <summary>Gets or sets the open file dialog title.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_DIALOG_TITLE)]
    [Description(@"The title of the open file dialog.")]
    public string OpenFileDialogTitle { get => _openFileDialogTitle; set => _openFileDialogTitle = value; }

    /// <summary>Gets or sets the standard WinForms dialog filter string.</summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description(@"The open file dialog filter, e.g. 'Text files (*.txt)|*.txt|All files (*.*)|*.*'.")]
    public string StandardDialogFilter { get => _standardDialogFilter; set => _standardDialogFilter = value; }

    /// <summary>Gets or sets the starting directory of the open file dialog.</summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description(@"The starting directory of the open file dialog.")]
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
        _outputControl = null;
        _defaultText = DEFAULT_TEXT;
        _applicationName = null;
        _openFileDialogTitle = DEFAULT_DIALOG_TITLE;
        _standardDialogFilter = string.Empty;
        _startingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        _parentMruMenuItem = null;
    }

    #endregion
}
