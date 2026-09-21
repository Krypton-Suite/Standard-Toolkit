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
public class KryptonMRUSaveFileMenuItem : ToolStripMenuItem
{
    #region Instance Fields

    private readonly MruSaveFileMenuItemValues _values;

    private MostRecentlyUsedFileManager? _recentlyUsedFileManager;

    #endregion

    #region Public

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Output control, dialog, and MRU settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public MruSaveFileMenuItemValues MruValues => _values;

    private bool ShouldSerializeMruValues() => !_values.IsDefault;

    private void ResetMruValues() => _values.Reset();

    /// <summary>Gets or sets the control whose text is saved to the selected file.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? OutputControl { get => _values.OutputControl; set => _values.OutputControl = value; }

    /// <summary>Gets or sets the text displayed on the tool strip menu item.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DefaultText { get => _values.DefaultText; set => _values.DefaultText = value; }

    /// <summary>Gets or sets the name of your application. This is used to store the MRU list in the Windows registry.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? ApplicationName { get => _values.ApplicationName; set => _values.ApplicationName = value; }

    /// <summary>Gets or sets the save file dialog title.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SaveFileDialogTitle { get => _values.SaveFileDialogTitle; set => _values.SaveFileDialogTitle = value; }

    /// <summary>Gets or sets the standard WinForms dialog filter string.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StandardDialogFilter { get => _values.StandardDialogFilter; set => _values.StandardDialogFilter = value; }

    /// <summary>Gets or sets the starting directory of the save file dialog.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StartingDirectory { get => _values.StartingDirectory; set => _values.StartingDirectory = value; }

    /// <summary>Gets or sets the parent MRU menu item.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonMRUMenuItem? ParentMRUMenuItem { get => _values.ParentMRUMenuItem; set => _values.ParentMRUMenuItem = value; }

    #endregion

    #region Identity

    public KryptonMRUSaveFileMenuItem()
    {
        _values = new MruSaveFileMenuItemValues(this);

        Text = _values.DefaultText;

        ShortcutKeys = Keys.Control | Keys.S;

        ShortcutKeyDisplayString = @"Ctrl + S";
    }

    public KryptonMRUSaveFileMenuItem(MostRecentlyUsedFileManager? recentlyUsedFileManager)
        : this()
    {
        _recentlyUsedFileManager = recentlyUsedFileManager;
    }

    #endregion
}
