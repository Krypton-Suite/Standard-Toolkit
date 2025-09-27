#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Workspace;

/// <summary>
/// Action list for KryptonWorkspaceCell using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonWorkspaceCellExtensibilityActionList : KryptonWorkspaceExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonWorkspaceCell _cell;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWorkspaceCellExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    public KryptonWorkspaceCellExtensibilityActionList(KryptonWorkspaceCellExtensibilityDesigner owner)
        : base(owner)
    {
        _cell = (owner.Component as KryptonWorkspaceCell)!;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the navigator mode.
    /// </summary>
    [Category("Appearance")]
    [Description("Navigator mode.")]
    [DefaultValue(NavigatorMode.BarTabGroup)]
    public NavigatorMode NavigatorMode
    {
        get => _cell.NavigatorMode;
        set => SetPropertyValue(nameof(NavigatorMode), value);
    }

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    [Category("Appearance")]
    [Description("Palette applied to drawing.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _cell.PaletteMode;
        set => SetPropertyValue(nameof(PaletteMode), value);
    }


    /// <summary>
    /// Gets or sets the page back style.
    /// </summary>
    [Category("Appearance")]
    [Description("Page background style.")]
    [DefaultValue(PaletteBackStyle.PanelClient)]
    public PaletteBackStyle PageBackStyle
    {
        get => _cell.PageBackStyle;
        set => SetPropertyValue(nameof(PageBackStyle), value);
    }

    /// <summary>
    /// Gets or sets whether to dispose on remove.
    /// </summary>
    [Category("Behavior")]
    [Description("Dispose on remove.")]
    [DefaultValue(false)]
    public bool DisposeOnRemove
    {
        get => _cell.DisposeOnRemove;
        set => SetPropertyValue(nameof(DisposeOnRemove), value);
    }

    /// <summary>
    /// Gets or sets whether to allow dropping pages.
    /// </summary>
    [Category("Behavior")]
    [Description("Allow dropping pages.")]
    [DefaultValue(true)]
    public bool AllowDroppingPages
    {
        get => _cell.AllowDroppingPages;
        set => SetPropertyValue(nameof(AllowDroppingPages), value);
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();

        // Add the action items
        items.Add(new DesignerActionHeaderItem("Appearance"));
        items.Add(new DesignerActionPropertyItem(nameof(NavigatorMode), "Navigator Mode", "Appearance", "Navigator mode."));
        items.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette Mode", "Appearance", "Palette applied to drawing."));
        items.Add(new DesignerActionPropertyItem(nameof(PageBackStyle), "Page Back Style", "Appearance", "Page background style."));
        items.Add(new DesignerActionHeaderItem("Behavior"));
        items.Add(new DesignerActionPropertyItem(nameof(DisposeOnRemove), "Dispose On Remove", "Behavior", "Dispose on remove."));
        items.Add(new DesignerActionPropertyItem(nameof(AllowDroppingPages), "Allow Dropping Pages", "Behavior", "Allow dropping pages."));

        return items;
    }
    #endregion
}
