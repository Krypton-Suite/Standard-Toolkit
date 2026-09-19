#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonContextMenuActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonContextMenu _contextMenu;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonContextMenuActionList(KryptonContextMenuDesigner owner)
        : base(owner.Component)
    {
        _contextMenu = (owner.Component as KryptonContextMenu)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
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

        if (_contextMenu != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Actions"));
            actions.Add(new KryptonDesignerActionItem(
                new DesignerVerb(@"Insert Standard Items", OnInsertStandardItems),
                @"Actions"));
            actions.Add(new KryptonDesignerActionItem(
                new DesignerVerb(@"Edit Items...", OnEditItems),
                @"Actions"));
            actions.Add(new DesignerActionHeaderItem(@"Data"));
            actions.Add(new DesignerActionPropertyItem(nameof(Items), @"Items", @"Data",
                @"Collection of menu items."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _contextMenu.PaletteMode;

        set
        {
            if (_contextMenu.PaletteMode != value)
            {
                _service?.OnComponentChanged(_contextMenu, null, _contextMenu.PaletteMode, value);
                _contextMenu.PaletteMode = value;
            }
        }
    }

    /// <summary>
    /// Gets the items collection.
    /// </summary>
    public KryptonContextMenuCollection Items => _contextMenu.Items;
    #endregion

    #region Implementation

    private void OnInsertStandardItems(object? sender, EventArgs e) =>
        KryptonContextMenuDesigner.InsertStandardItems(
            _contextMenu,
            GetService(typeof(IDesignerHost)) as IDesignerHost,
            _service);

    private void OnEditItems(object? sender, EventArgs e) =>
        KryptonContextMenuDesigner.EditItems(_contextMenu);

    #endregion
}
