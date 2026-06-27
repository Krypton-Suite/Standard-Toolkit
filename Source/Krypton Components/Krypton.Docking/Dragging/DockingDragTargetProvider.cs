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
/// Builds drag targets from the docking hierarchy for the pages being moved.
/// </summary>
public class DockingDragTargetProvider : IDragTargetProvider
{
    #region Instance Fields
    private readonly KryptonDockingManager _manager;
    private readonly KryptonPageCollection _pages;
    private readonly KryptonFloatingWindow? _floatingWindow;
    #endregion

    #region Identity
    /// <summary>
    /// Captures the docking manager, optional source floating window, and pages for subsequent target generation.
    /// </summary>
    /// <param name="manager">Docking manager whose hierarchy supplies drag targets.</param>
    /// <param name="floatingWindow">Floating window being dragged, or <see langword="null"/> when the drag did not originate from a floating window.</param>
    /// <param name="pages">Pages whose allowed drop locations determine the target set.</param>
    public DockingDragTargetProvider(KryptonDockingManager manager, 
        KryptonFloatingWindow? floatingWindow,
        KryptonPageCollection pages)
    {
        _manager = manager;
        _floatingWindow = floatingWindow;
        _pages = pages;
    }
    #endregion

    #region Public
    /// <summary>
    /// Assembles drag targets from the docking hierarchy for the supplied drag data.
    /// </summary>
    /// <param name="dragEndData">Pages and context for the drag operation.</param>
    /// <returns>Targets discovered in the hierarchy; includes a <see cref="DragTargetNull"/> entry when no targets are found.</returns>
    public DragTargetList GenerateDragTargets(PageDragEndData? dragEndData)
    {
        var targets = new DragTargetList();

        // Generate the set of targets from the element hierarchy
        _manager.PropogateDragTargets(_floatingWindow, dragEndData, targets);

        // Must have at least one target
        if (targets.Count == 0)
        {
            targets.Add(new DragTargetNull());
        }

        return targets;
    }
    #endregion
}
