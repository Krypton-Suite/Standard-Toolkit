#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Provides the set of drag targets relevant to the set of pages being moved.
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
    /// Initialize a new instance of the DragTargetNull class.
    /// </summary>
    /// <param name="manager">Reference to docking manager.</param>
    /// <param name="floatingWindow">Reference to window being dragged.</param>
    /// <param name="pages">Reference to collection of pages to drag.</param>
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
    /// Generate a list of drag targets that are relevant to the provided end data.
    /// </summary>
    /// <param name="dragEndData">Pages data being dragged.</param>
    /// <returns>List of drag targets.</returns>
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