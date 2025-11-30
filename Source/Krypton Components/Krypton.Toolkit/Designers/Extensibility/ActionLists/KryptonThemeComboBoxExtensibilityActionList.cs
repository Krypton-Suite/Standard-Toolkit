#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonThemeComboBox control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonThemeComboBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonThemeComboBox? _themeComboBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonThemeComboBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonThemeComboBoxExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _themeComboBox = (KryptonThemeComboBox?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _themeComboBox?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_themeComboBox!, nameof(PaletteMode), PaletteMode, value, v => _themeComboBox!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the input control style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _themeComboBox?.InputControlStyle ?? InputControlStyle.Standalone;
        set => SetPropertyValue(_themeComboBox!, nameof(InputControlStyle), InputControlStyle, value, v => _themeComboBox!.InputControlStyle = v);
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    public string Text
    {
        get => _themeComboBox?.Text ?? string.Empty;
        set => SetPropertyValue(_themeComboBox!, nameof(Text), Text, value, v => _themeComboBox!.Text = v);
    }

    /// <summary>
    /// Gets and sets whether the control can be focused.
    /// </summary>
    public bool AllowButtonSpecToolTips
    {
        get => _themeComboBox?.AllowButtonSpecToolTips ?? false;
        set => SetPropertyValue(_themeComboBox!, nameof(AllowButtonSpecToolTips), AllowButtonSpecToolTips, value, v => _themeComboBox!.AllowButtonSpecToolTips = v);
    }

    /// <summary>
    /// Gets and sets whether the control is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _themeComboBox?.Enabled ?? true;
        set => SetPropertyValue(_themeComboBox!, nameof(Enabled), Enabled, value, v => _themeComboBox!.Enabled = v);
    }

    /// <summary>
    /// Gets and sets whether the control is visible.
    /// </summary>
    public bool Visible
    {
        get => _themeComboBox?.Visible ?? true;
        set => SetPropertyValue(_themeComboBox!, nameof(Visible), Visible, value, v => _themeComboBox!.Visible = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_themeComboBox != null)
        {
            // Add the list of ThemeComboBox specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", @"Appearance", @"Input control style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Text content"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowButtonSpecToolTips), @"Allow Button Spec ToolTips", @"Behavior", @"Allow button spec tooltips"));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Control enabled"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Control visible"));
        }

        return actions;
    }
    #endregion
}
