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
/// Drag target that docks pages onto one side of a <see cref="KryptonDockingControl"/>.
/// </summary>
public class DragTargetControlEdge : DragTarget
{
    #region Instance Fields

    private readonly bool _outsideEdge;
    #endregion

    #region Identity
    /// <summary>
    /// Defines screen, hot, and draw rectangles for an edge drop zone on the supplied control element.
    /// </summary>
    /// <param name="screenRect">Screen rectangle for the full target area.</param>
    /// <param name="hotRect">Screen rectangle that highlights when the cursor is over the target.</param>
    /// <param name="drawRect">Screen rectangle used to render drop feedback.</param>
    /// <param name="hint">Edge hint; must be one of the edge or transfer values in <see cref="DragTargetHint"/>.</param>
    /// <param name="controlElement">Docking control that receives dropped pages.</param>
    /// <param name="allowFlags">Page flags required for a page to be accepted by this target.</param>
    /// <param name="outsideEdge">When <see langword="true"/>, inserts the new dockspace on the outer edge; otherwise appends on the inner edge.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="hint"/> is not an edge value.</exception>
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
    /// Releases the reference to <see cref="ControlElement"/> when disposing managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> when called from <see cref="Dispose"/>; otherwise <see langword="false"/>.</param>
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
    /// Visual edge of <see cref="ControlElement"/> that receives dropped pages.
    /// </summary>
    public VisualOrientation Edge { get; }

    /// <summary>
    /// Docking control whose edge dockspace receives the drop.
    /// </summary>
    public KryptonDockingControl ControlElement { get; private set; }

    /// <summary>
    /// Always returns <see langword="true"/> for any screen position and drag data.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="dragEndData">Drag data for the attempted match.</param>
    /// <returns>Always <see langword="true"/>.</returns>
    public override bool IsMatch(Point screenPt, PageDragEndData? dragEndData) => true;

    /// <summary>
    /// Creates or extends a dockspace on <see cref="Edge"/> and transfers allowed pages into it.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="data">Pages to dock; only pages with <see cref="KryptonPageFlags.DockingAllowDocked"/> and an approved <c>PageDockedRequest</c> are transferred.</param>
    /// <returns><see langword="true"/> when at least one page was docked; otherwise <see langword="false"/>.</returns>
    public override bool PerformDrop(Point screenPt, PageDragEndData? data)
    {
        // Find our docking edge
        KryptonDockingEdge? dockingEdge = Edge switch
        {
            VisualOrientation.Left => ControlElement[@"Left"] as KryptonDockingEdge,
            VisualOrientation.Right => ControlElement[@"Right"] as KryptonDockingEdge,
            VisualOrientation.Top => ControlElement[@"Top"] as KryptonDockingEdge,
            VisualOrientation.Bottom => ControlElement[@"Bottom"] as KryptonDockingEdge,
            _ => null
        };

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
