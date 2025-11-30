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
/// Action list for the KryptonHeader control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonHeaderExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonHeader? _header;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonHeaderExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonHeaderExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _header = (KryptonHeader?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _header?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_header!, nameof(PaletteMode), PaletteMode, value, v => _header!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the header style.
    /// </summary>
    public HeaderStyle HeaderStyle
    {
        get => _header?.HeaderStyle ?? HeaderStyle.Primary;
        set => SetPropertyValue(_header!, nameof(HeaderStyle), HeaderStyle, value, v => _header!.HeaderStyle = v);
    }

    /// <summary>
    /// Gets and sets the header values.
    /// </summary>
    public string Heading
    {
        get => _header?.Values.Heading ?? string.Empty;
        set => SetPropertyValue(_header!, nameof(Heading), Heading, value, v => _header!.Values.Heading = v);
    }

    /// <summary>
    /// Gets and sets the header description.
    /// </summary>
    public string Description
    {
        get => _header?.Values.Description ?? string.Empty;
        set => SetPropertyValue(_header!, nameof(Description), Description, value, v => _header!.Values.Description = v);
    }

    /// <summary>
    /// Gets and sets the header image.
    /// </summary>
    public Image? Image
    {
        get => _header?.Values.Image;
        set => SetPropertyValue(_header!, nameof(Image), Image, value, v => _header!.Values.Image = v);
    }

    /// <summary>
    /// Gets and sets the header orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _header?.Orientation ?? VisualOrientation.Top;
        set => SetPropertyValue(_header!, nameof(Orientation), Orientation, value, v => _header!.Orientation = v);
    }

    /// <summary>
    /// Gets and sets whether the header is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _header?.Enabled ?? true;
        set => SetPropertyValue(_header!, nameof(Enabled), Enabled, value, v => _header!.Enabled = v);
    }

    /// <summary>
    /// Gets and sets whether the header is visible.
    /// </summary>
    public bool Visible
    {
        get => _header?.Visible ?? true;
        set => SetPropertyValue(_header!, nameof(Visible), Visible, value, v => _header!.Visible = v);
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
        if (_header != null)
        {
            // Add the list of Header specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderStyle), @"Style", @"Appearance", @"Header style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Heading), @"Heading", @"Appearance", @"Header heading"));
            actions.Add(new DesignerActionPropertyItem(nameof(Description), @"Description", @"Appearance", @"Header description"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), @"Image", @"Appearance", @"Header image"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), @"Orientation", @"Appearance", @"Header orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Header enabled"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Header visible"));
        }

        return actions;
    }
    #endregion
}
