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
/// Target one of the four sides of a docking control.
/// </summary>
public class DragTargetControlEdge : DragTarget
{
    #region Instance Fields

    private readonly bool _outsideEdge;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DragTargetControlEdge class.
    /// </summary>
    /// <param name="screenRect">Rectangle for screen area.</param>
    /// <param name="hotRect">Rectangle for hot area.</param>
    /// <param name="drawRect">Rectangle for draw area.</param>
    /// <param name="hint">Target hint which should be one of the edges.</param>
    /// <param name="controlElement">Workspace instance that contains cell.</param>
    /// <param name="allowFlags">Only drop pages that have one of these flags defined.</param>
    /// <param name="outsideEdge">Add to the outside edge (otherwise the inner edge).</param>
    public DragTargetControlEdge(Rectangle screenRect,
        Rectangle hotRect,
        Rectangle drawRect,
        DragTargetHint hint,
        KryptonDockingControl controlElement,
        KryptonPageFlags allowFlags,
        bool outsideEdge)
        : base(screenRect, hotRect, drawRect, hint, allowFlags)
    {
        ControlElement = controlElement;
        _outsideEdge = outsideEdge;

        // Find the orientation by looking for a matching hint (we need to exclude flags from the hint enum)
        switch (hint & DragTargetHint.ExcludeFlags)
        {
            case DragTargetHint.Transfer:
            case DragTargetHint.EdgeLeft:
                Edge = VisualOrientation.Left;
                break;
            case DragTargetHint.EdgeRight:
                Edge = VisualOrientation.Right;
                break;
            case DragTargetHint.EdgeTop:
                Edge = VisualOrientation.Top;
                break;
            case DragTargetHint.EdgeBottom:
                Edge = VisualOrientation.Bottom;
                break;
            default:
                Debug.Assert(false);
                throw new ArgumentOutOfRangeException(nameof(hint), @"Hint must be an edge value.");
        }
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ControlElement = null!;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the dragging edge.
    /// </summary>
    public VisualOrientation Edge { get; }

    /// <summary>
    /// Gets the target docking element.
    /// </summary>
    public KryptonDockingControl ControlElement { get; private set; }

    /// <summary>
    /// Is this target a match for the provided screen position.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="dragEndData">Data to be dropped at destination.</param>
    /// <returns>True if a match; otherwise false.</returns>
    public override bool IsMatch(Point screenPt, PageDragEndData? dragEndData) => true;

    /// <summary>
    /// Perform the drop action associated with the target.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="data">Data to pass to the target to process drop.</param>
    /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
    public override bool PerformDrop(Point screenPt, PageDragEndData? data)
    {
        // Find our docking edge
        KryptonDockingEdge? dockingEdge = null;
        switch (Edge)
        {
            case VisualOrientation.Left:
                dockingEdge = ControlElement[@"Left"] as KryptonDockingEdge;
                break;
            case VisualOrientation.Right:
                dockingEdge = ControlElement[@"Right"] as KryptonDockingEdge;
                break;
            case VisualOrientation.Top:
                dockingEdge = ControlElement[@"Top"] as KryptonDockingEdge;
                break;
            case VisualOrientation.Bottom:
                dockingEdge = ControlElement[@"Bottom"] as KryptonDockingEdge;
                break;
        }

        // Find the docked edge
        var dockedEdge = dockingEdge?[@"Docked"] as KryptonDockingEdgeDocked;
        KryptonDockingManager? manager = dockedEdge?.DockingManager;
        if (manager != null)
        {
            // Create a list of pages that are allowed to be transferred into the dockspace
            var transferPages = new List<KryptonPage>();
            var transferUniqueNames = new List<string>();
            if (data != null)
            {
                foreach (KryptonPage page in data.Pages)
                {
                    if (page.AreFlagsSet(KryptonPageFlags.DockingAllowDocked))
                    {
                        // Use event to indicate the page is becoming docked and allow it to be cancelled
                        var args = new CancelUniqueNameEventArgs(page.UniqueName, false);
                        manager?.RaisePageDockedRequest(args);

                        if (!args.Cancel)
                        {
                            transferPages.Add(page);
                            transferUniqueNames.Add(page.UniqueName);
                        }
                    }
                }
            }

            // Transfer valid pages into the new dockspace
            if (transferPages.Count > 0)
            {
                // Convert the incoming pages into store pages for restoring later
                manager?.PropogateAction(DockingPropogateAction.StorePages, transferUniqueNames.ToArray());

                // Create a new dockspace at the start of the list so it is closest to the control edge
                if (dockedEdge != null)
                {
                    KryptonDockingDockspace dockspace = (_outsideEdge ? dockedEdge.InsertDockspace(0) : dockedEdge.AppendDockspace());

                    // Add pages into the target
                    dockspace.Append(transferPages.ToArray());
                }

                return true;
            }
        }

        return false;
    }

    #endregion
}