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
/// Expandable configuration for <see cref="KryptonMRUSaveFileMenuItem"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class MruSaveFileMenuItemValues : Storage
{
    #region Constants

    private const string DEFAULT_TEXT = @"Sa&ve";
    private const string DEFAULT_DIALOG_TITLE = @"Save:";

    #endregion

    #region Instance Fields

    private Control? _outputControl;
    private string _defaultText = DEFAULT_TEXT;
    private string? _applicationName;
    private string _saveFileDialogTitle = DEFAULT_DIALOG_TITLE;
    private string _standardDialogFilter = string.Empty;
    private string _startingDirectory = string.Empty;
    private KryptonMRUMenuItem? _parentMruMenuItem;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="MruSaveFileMenuItemValues"/> class.
    /// </summary>
    /// <param name="owner">Owning MRU save-file menu item.</param>
    public MruSaveFileMenuItemValues(KryptonMRUSaveFileMenuItem owner)
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
        _outputControl is null &&
        _defaultText == DEFAULT_TEXT &&
        _applicationName is null &&
        _saveFileDialogTitle == DEFAULT_DIALOG_TITLE &&
        _standardDialogFilter.Length == 0 &&
        _startingDirectory.Length == 0 &&
        _parentMruMenuItem is null;

    #endregion

    #region Public

    /// <summary>Gets or sets the control whose text is saved to the selected file.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The control whose text is saved to the selected file.")]
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

    /// <summary>Gets or sets the save file dialog title.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_DIALOG_TITLE)]
    [Description(@"The title of the save file dialog.")]
    public string SaveFileDialogTitle { get => _saveFileDialogTitle; set => _saveFileDialogTitle = value; }

    /// <summary>Gets or sets the standard WinForms dialog filter string.</summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description(@"The save file dialog filter, e.g. 'Text files (*.txt)|*.txt|All files (*.*)|*.*'.")]
    public string StandardDialogFilter { get => _standardDialogFilter; set => _standardDialogFilter = value; }

    /// <summary>Gets or sets the starting directory of the save file dialog.</summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description(@"The starting directory of the save file dialog.")]
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
        _saveFileDialogTitle = DEFAULT_DIALOG_TITLE;
        _standardDialogFilter = string.Empty;
        _startingDirectory = string.Empty;
        _parentMruMenuItem = null;
    }

    #endregion
}
