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
/// Action list for KryptonPanel using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonPanelExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonPanel _panel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPanelExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonPanelExtensibilityActionList(KryptonPanelExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the panel instance
        _panel = (owner.Component as KryptonPanel)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the panel style.
    /// </summary>
    public PaletteBackStyle PanelBackStyle
    {
        get => _panel.PanelBackStyle;
        set => SetPropertyValue(_panel, nameof(PanelBackStyle), _panel.PanelBackStyle, value, v => _panel.PanelBackStyle = v);
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _panel.PaletteMode;
        set => SetPropertyValue(_panel, nameof(PaletteMode), _panel.PaletteMode, value, v => _panel.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the auto size mode.
    /// </summary>
    public AutoSizeMode AutoSizeMode
    {
        get => _panel.AutoSizeMode;
        set => SetPropertyValue(_panel, nameof(AutoSizeMode), _panel.AutoSizeMode, value, v => _panel.AutoSizeMode = v);
    }

    /// <summary>
    /// Gets and sets whether the panel auto sizes.
    /// </summary>
    public bool AutoSize
    {
        get => _panel.AutoSize;
        set => SetPropertyValue(_panel, nameof(AutoSize), _panel.AutoSize, value, v => _panel.AutoSize = v);
    }

    /// <summary>
    /// Gets and sets the panel background color.
    /// </summary>
    public Color BackColor
    {
        get => _panel.BackColor;
        set => SetPropertyValue(_panel, nameof(BackColor), _panel.BackColor, value, v => _panel.BackColor = v);
    }

    /// <summary>
    /// Gets and sets the panel border style.
    /// </summary>
    public BorderStyle BorderStyle
    {
        get => _panel.BorderStyle;
        set => SetPropertyValue(_panel, nameof(BorderStyle), _panel.BorderStyle, value, v => _panel.BorderStyle = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_panel != null)
        {
            // Add the list of panel specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(PanelBackStyle), @"Panel Back Style", nameof(Appearance), @"Panel background style"));
            actions.Add(new DesignerActionPropertyItem(nameof(BackColor), nameof(BackColor), nameof(Appearance), @"Panel background color"));
            actions.Add(new DesignerActionPropertyItem(nameof(BorderStyle), nameof(BorderStyle), nameof(Appearance), @"Panel border style"));
            actions.Add(new DesignerActionHeaderItem(@"Layout"));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoSize), nameof(AutoSize), @"Layout", @"Automatically size the panel"));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoSizeMode), nameof(AutoSizeMode), @"Layout", @"Auto size mode"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
