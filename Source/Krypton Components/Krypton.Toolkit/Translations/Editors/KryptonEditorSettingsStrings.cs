#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonEditorSettingsStrings
{
    #region Static Strings

    private const string DEFAULT_DESIGNER_EDITOR_SETTINGS_TITLE = @"Designer Editor Settings";
    private const string DEFAULT_DESIGNER_EDITOR_SETTINGS_THEME_LABEL = @"Theme for designer editors:";
    private const string DEFAULT_DESIGNER_EDITOR_SETTINGS_DESCRIPTION =
        @"Applies only to Krypton designer dialogs. Does not change the edited control or the application global theme.";
    private const string DEFAULT_DESIGNER_EDITOR_SETTINGS_CLEAR = @"Clear preference";
    private const string DEFAULT_DESIGNER_EDITOR_SETTINGS_OPEN_FOLDER = @"Open folder...";
    private const string DEFAULT_DESIGNER_EDITOR_THEME_BUTTON = @"Theme...";

    #endregion

    #region Identity

    public KryptonEditorSettingsStrings()
    {
        Reset();
    }

    #endregion

    #region Public

    /// <summary>Gets or sets the designer editor settings dialog title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The designer editor settings dialog title.")]
    [DefaultValue(DEFAULT_DESIGNER_EDITOR_SETTINGS_TITLE)]
    public string DesignerEditorSettingsTitle { get; set; }

    /// <summary>Gets or sets the designer editor settings theme label.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The designer editor settings theme label.")]
    [DefaultValue(DEFAULT_DESIGNER_EDITOR_SETTINGS_THEME_LABEL)]
    public string DesignerEditorSettingsThemeLabel { get; set; }

    /// <summary>Gets or sets the designer editor settings description.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The designer editor settings description.")]
    [DefaultValue(DEFAULT_DESIGNER_EDITOR_SETTINGS_DESCRIPTION)]
    public string DesignerEditorSettingsDescription { get; set; }

    /// <summary>Gets or sets the clear designer editor theme preference button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The clear designer editor theme preference button text.")]
    [DefaultValue(DEFAULT_DESIGNER_EDITOR_SETTINGS_CLEAR)]
    public string DesignerEditorSettingsClearPreference { get; set; }

    /// <summary>Gets or sets the open preferences folder button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The open designer editor preferences folder button text.")]
    [DefaultValue(DEFAULT_DESIGNER_EDITOR_SETTINGS_OPEN_FOLDER)]
    public string DesignerEditorSettingsOpenFolder { get; set; }

    /// <summary>Gets or sets the compact designer editor theme button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The compact designer editor theme button text.")]
    [DefaultValue(DEFAULT_DESIGNER_EDITOR_THEME_BUTTON)]
    public string DesignerEditorThemeButtonText { get; set; }

    #endregion

    #region IsDefault

    [Browsable(false)]
    public bool IsDefault => DesignerEditorSettingsTitle.Equals(DEFAULT_DESIGNER_EDITOR_SETTINGS_TITLE) &&
                             DesignerEditorSettingsThemeLabel.Equals(DEFAULT_DESIGNER_EDITOR_SETTINGS_THEME_LABEL) &&
                             DesignerEditorSettingsDescription.Equals(DEFAULT_DESIGNER_EDITOR_SETTINGS_DESCRIPTION) &&
                             DesignerEditorSettingsClearPreference.Equals(DEFAULT_DESIGNER_EDITOR_SETTINGS_CLEAR) &&
                             DesignerEditorSettingsOpenFolder.Equals(DEFAULT_DESIGNER_EDITOR_SETTINGS_OPEN_FOLDER) &&
                             DesignerEditorThemeButtonText.Equals(DEFAULT_DESIGNER_EDITOR_THEME_BUTTON);

    #endregion

    #region Implementation

    public void Reset()
    {
        DesignerEditorSettingsTitle = DEFAULT_DESIGNER_EDITOR_SETTINGS_TITLE;
        DesignerEditorSettingsThemeLabel = DEFAULT_DESIGNER_EDITOR_SETTINGS_THEME_LABEL;
        DesignerEditorSettingsDescription = DEFAULT_DESIGNER_EDITOR_SETTINGS_DESCRIPTION;
        DesignerEditorSettingsClearPreference = DEFAULT_DESIGNER_EDITOR_SETTINGS_CLEAR;
        DesignerEditorSettingsOpenFolder = DEFAULT_DESIGNER_EDITOR_SETTINGS_OPEN_FOLDER;
        DesignerEditorThemeButtonText = DEFAULT_DESIGNER_EDITOR_THEME_BUTTON;
    }

    #endregion

    #region Overrides

    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion
}
