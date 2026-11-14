#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxItem(false), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip)]
public class KryptonMRUSaveAsFileMenuItem : ToolStripMenuItem
{
    #region Instance Fields

    private readonly MruSaveAsFileMenuItemValues _values;

    private readonly MostRecentlyUsedFileManager? _recentlyUsedFileManager;

    public KryptonMRUSaveAsFileMenuItem(MostRecentlyUsedFileManager? recentlyUsedFileManager)
    {
        _values = new MruSaveAsFileMenuItemValues(this);
        _recentlyUsedFileManager = recentlyUsedFileManager;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("System dialog, output control, and MRU settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public MruSaveAsFileMenuItemValues MruValues => _values;

    private bool ShouldSerializeMruValues() => !_values.IsDefault;

    private void ResetMruValues() => _values.Reset();

    /// <summary>Gets or sets a value indicating whether to use the native system dialogs over the Windows API CodePack types.</summary>
    /// <value> <c>true</c> if [use system dialogs]; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseSystemDialogs { get => _values.UseSystemDialogs; set => _values.UseSystemDialogs = value; }

    /// <summary>Gets or sets the control to load the file content text into.</summary>
    /// <value>The control to load the file content text into.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? OutputControl { get => _values.OutputControl; set => _values.OutputControl = value; }

    /// <summary>Gets or sets the text displayed on the tool strip menu item.</summary>
    /// <value>The text displayed on the tool strip menu item.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DefaultText { get => _values.DefaultText; set => _values.DefaultText = value; }

    /// <summary>Gets or sets the name of the name of your application. This is used to store the MRU list in the Windows registry.</summary>
    /// <value>The name of the application.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? ApplicationName { get => _values.ApplicationName; set => _values.ApplicationName = value; }

    /// <summary>Gets or sets the save as file dialog title.</summary>
    /// <value>The save as file dialog title.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SaveAsFileDialogTitle { get => _values.SaveAsFileDialogTitle; set => _values.SaveAsFileDialogTitle = value; }

    /// <summary>Gets or sets the display name of the raw filter.</summary>
    /// <value>The display name of the raw filter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? RawFilterDisplayName { get => _values.RawFilterDisplayName; set => _values.RawFilterDisplayName = value; }

    /// <summary>Gets or sets the raw extension list.</summary>
    /// <value>The raw extension list.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? RawExtensionList { get => _values.RawExtensionList; set => _values.RawExtensionList = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? StandardDialogFilter { get => _values.StandardDialogFilter; set => _values.StandardDialogFilter = value; }

    /// <summary>Gets or sets the starting directory of the save as file dialog.</summary>
    /// <value>The starting directory of the save as file dialog.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StartingDirectory { get => _values.StartingDirectory; set => _values.StartingDirectory = value; }

    /// <summary>Gets or sets the parent MRU menu item.</summary>
    /// <value>The parent MRU menu item.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonMRUMenuItem? ParentMRUMenuItem { get => _values.ParentMRUMenuItem; set => _values.ParentMRUMenuItem = value; }

    #endregion
}