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
/// Workspace used as the main dockable document area with tab profile styling for active and inactive cells.
/// </summary>
[ToolboxBitmap(typeof(KryptonDockableWorkspace), "ToolboxBitmaps.KryptonDockableWorkspace.bmp")]
public class KryptonDockableWorkspace : KryptonSpace
{
    #region Identity
    /// <summary>
    /// Creates a workspace space named "Workspace" with tab context menus enabled and standard docking header actions disabled.
    /// </summary>
    public KryptonDockableWorkspace()
        : base(nameof(Workspace)) =>
        // Override the base class and allow the workspace context menu for the tab to be shown
        ContextMenus.ShowContextMenu = true;

    /// <summary>
    /// Returns a diagnostic label that includes the current dock assignment.
    /// </summary>
    /// <returns>Label identifying this workspace and its dock value.</returns>
    public override string ToString() => $"KryptonDockableWorkspace {Dock}";

    #endregion

    #region Protected
    /// <summary>
    /// Gets a value indicating if docking specific appearance should be applied.
    /// </summary>
    protected override bool ApplyDockingAppearance => false;

    /// <summary>
    /// Gets a value indicating if docking specific close action should be applied.
    /// </summary>
    protected override bool ApplyDockingCloseAction => false;

    /// <summary>
    /// Gets a value indicating if docking specific pin actions should be applied.
    /// </summary>
    protected override bool ApplyDockingPinAction => false;

    /// <summary>
    /// Gets a value indicating if docking specific drop-down actions should be applied.
    /// </summary>
    protected override bool ApplyDockingDropDownAction => false;

    /// <summary>
    /// Initialize a new cell.
    /// </summary>
    /// <param name="cell">Cell being added to the control.</param>
    protected override void NewCellInitialize(KryptonWorkspaceCell cell)
    {
        // Let base class perform event hooking and customizations
        base.NewCellInitialize(cell);

        // By default, the new cell does not have focus and so should have standard looking tabs
        cell.Bar.TabStyle = TabStyle.StandardProfile;
        cell.CloseAction += OnCellCloseAction;
    }

    /// <summary>
    /// Raises the ActiveCellChanged event.
    /// </summary>
    /// <param name="e">An ActiveCellChangedEventArgs containing the event data.</param>
    protected override void OnActiveCellChanged(ActiveCellChangedEventArgs e)
    {
        // Ensure all but the newly selected cell have a lower profile appearance
        KryptonWorkspaceCell? cell = FirstCell();
        while (cell != null)
        {
            if (e.NewCell != cell)
            {
                cell.Bar.TabStyle = TabStyle.StandardProfile;
            }

            cell = NextCell(cell);
        }

        // Ensure the newly selected cell has a higher profile appearance
        if (e.NewCell != null)
        {
            e.NewCell.Bar.TabStyle = TabStyle.HighProfile;
        }

        base.OnActiveCellChanged(e);
    }
    #endregion   

    #region Implementation
    private void OnCellCloseAction(object? sender, CloseActionEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.Item?.UniqueName))
        {
            OnPageCloseClicked(new UniqueNameEventArgs(e.Item!.UniqueName));
        }
    }
    #endregion
}
