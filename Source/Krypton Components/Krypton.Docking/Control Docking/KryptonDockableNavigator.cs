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

namespace Krypton.Docking;

/// <summary>
/// Navigator variant wired into the docking framework for page insertion and header menu coordination.
/// </summary>
[ToolboxBitmap(typeof(KryptonDockableWorkspace), "ToolboxBitmaps.KryptonDockableNavigator.bmp")]
public class KryptonDockableNavigator : KryptonNavigator
{
    #region Events
    /// <summary>
    /// Raised before a page is inserted so docking logic can perform additional setup.
    /// </summary>
    [Category("DockableNavigator")]
    [Description("Occurs when a page is added to a workspace cell.")]
    public event EventHandler<KryptonPageEventArgs>? CellPageInserting;

    /// <summary>
    /// Raised when a page context menu is about to open; handlers can populate or cancel the menu.
    /// </summary>
    [Category("DockableNavigator")]
    [Description("Occurs when a page requests that a drop-down menu be shown.")]
    public event EventHandler<CancelDropDownEventArgs>? PageDropDownClicked;
    #endregion

    #region Identity
    /// <summary>
    /// Subscribes to page insertion and context menu display for docking coordination.
    /// </summary>
    public KryptonDockableNavigator()
    {
        // Hook into cell specific events
        ShowContextMenu += OnShowContextMenu;
        Pages.Inserting += OnPagesInserting;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the CellPageInserting event.
    /// </summary>
    /// <param name="e">An KryptonPageEventArgs containing the event data.</param>
    protected virtual void OnCellPageInserting(KryptonPageEventArgs e) => CellPageInserting?.Invoke(this, e);

    /// <summary>
    /// Raises the PageDropDownClicked event.
    /// </summary>
    /// <param name="e">An CancelDropDownEventArgs containing the event data.</param>
    protected virtual void OnPageDropDownClicked(CancelDropDownEventArgs e) => PageDropDownClicked?.Invoke(this, e);
    #endregion

    #region Private
    private void OnPagesInserting(object sender, TypedCollectionEventArgs<KryptonPage> e) =>
        // Generate event so the docking element can decide on extra actions to be taken
        OnCellPageInserting(new KryptonPageEventArgs(e.Item, e.Index));

    private void OnShowContextMenu(object? sender, ShowContextMenuArgs e)
    {
        // Make sure we have a menu for displaying
        e.KryptonContextMenu ??= new KryptonContextMenu();

        // Use event to allow customization of the context menu
        var args = new CancelDropDownEventArgs(e.KryptonContextMenu, e.Item)
        {
            Cancel = e.Cancel
        };
        OnPageDropDownClicked(args);
        e.Cancel = args.Cancel;
    }
    #endregion
}
