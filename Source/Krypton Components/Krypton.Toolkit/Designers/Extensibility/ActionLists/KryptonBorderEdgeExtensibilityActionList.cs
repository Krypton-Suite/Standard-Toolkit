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
/// Action list for the KryptonBorderEdge control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonBorderEdgeExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonBorderEdge? _borderEdge;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonBorderEdgeExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonBorderEdgeExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _borderEdge = (KryptonBorderEdge?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _borderEdge?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_borderEdge!, nameof(PaletteMode), PaletteMode, value, v => _borderEdge!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the border edge orientation.
    /// </summary>
    public Orientation Orientation
    {
        get => _borderEdge?.Orientation ?? Orientation.Horizontal;
        set => SetPropertyValue(_borderEdge!, nameof(Orientation), Orientation, value, v => _borderEdge!.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the border edge style.
    /// </summary>
    public PaletteBorderStyle BorderStyle
    {
        get => _borderEdge?.BorderStyle ?? PaletteBorderStyle.ControlClient;
        set => SetPropertyValue(_borderEdge!, nameof(BorderStyle), BorderStyle, value, v => _borderEdge!.BorderStyle = v);
    }

    /// <summary>
    /// Gets and sets whether the border edge is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _borderEdge?.Enabled ?? true;
        set => SetPropertyValue(_borderEdge!, nameof(Enabled), Enabled, value, v => _borderEdge!.Enabled = v);
    }

    /// <summary>
    /// Gets and sets whether the border edge is visible.
    /// </summary>
    public bool Visible
    {
        get => _borderEdge?.Visible ?? true;
        set => SetPropertyValue(_borderEdge!, nameof(Visible), Visible, value, v => _borderEdge!.Visible = v);
    }

    /// <summary>
    /// Gets and sets the border edge width.
    /// </summary>
    public int Width
    {
        get => _borderEdge?.Width ?? 1;
        set => SetPropertyValue(_borderEdge!, nameof(Width), Width, value, v => _borderEdge!.Width = v);
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
        if (_borderEdge != null)
        {
            // Add the list of BorderEdge specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), @"Orientation", @"Appearance", @"Border edge orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(BorderStyle), @"Border Style", @"Appearance", @"Border style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Width), @"Width", @"Appearance", @"Border edge width"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Border edge enabled"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Border edge visible"));
        }

        return actions;
    }
    #endregion
}
