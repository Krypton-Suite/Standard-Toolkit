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
/// Action list for the KryptonSeparator control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonSeparatorExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonSeparator? _separator;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSeparatorExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonSeparatorExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _separator = (KryptonSeparator?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the separator style.
    /// </summary>
    public SeparatorStyle SeparatorStyle
    {
        get => _separator?.SeparatorStyle ?? SeparatorStyle.LowProfile;
        set => SetPropertyValue(_separator!, nameof(SeparatorStyle), SeparatorStyle, value, v => _separator!.SeparatorStyle = v);
    }

    /// <summary>
    /// Gets and sets the orientation of the separator.
    /// </summary>
    public Orientation Orientation
    {
        get => _separator?.Orientation ?? Orientation.Horizontal;
        set => SetPropertyValue(_separator!, nameof(Orientation), Orientation, value, v => _separator!.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _separator?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_separator!, nameof(PaletteMode), PaletteMode, value, v => _separator!.PaletteMode = v);
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
        if (_separator != null)
        {
            // Add the list of Separator specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(SeparatorStyle), @"Style", @"Appearance", @"Separator style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), @"Orientation", @"Appearance", @"Separator orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
