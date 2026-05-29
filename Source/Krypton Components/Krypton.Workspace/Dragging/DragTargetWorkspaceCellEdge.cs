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

namespace Krypton.Workspace;

/// <summary>
/// Target one of the four sides of a workspace cell.
/// </summary>
public class DragTargetWorkspaceCellEdge : DragTargetWorkspaceEdge
{
    #region Instance Fields

    private int _visibleNotDraggedPages;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DragTargetWorkspaceCellEdge class.
    /// </summary>
    /// <param name="screenRect">Rectangle for screen area.</param>
    /// <param name="hotRect">Rectangle for hot area.</param>
    /// <param name="drawRect">Rectangle for draw area.</param>
    /// <param name="hint">Target hint which should be one of the edges.</param>
    /// <param name="workspace">Workspace instance that contains cell.</param>
    /// <param name="cell">Workspace cell as target for drop.</param>
    /// <param name="allowFlags">Only drop pages that have one of these flags defined.</param>
    public DragTargetWorkspaceCellEdge(Rectangle screenRect,
        Rectangle hotRect,
        Rectangle drawRect,
        DragTargetHint hint,
        KryptonWorkspace workspace,
        KryptonWorkspaceCell cell,
        KryptonPageFlags allowFlags)
        : base(screenRect, hotRect, drawRect, hint, workspace, allowFlags)
    {
        Cell = cell;
        _visibleNotDraggedPages = -1;
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Cell = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the target workspace cell.
    /// </summary>
    public KryptonWorkspaceCell? Cell { get; private set; }

    /// <summary>
    /// Is this target a match for the provided screen position.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="dragEndData">Data to be dropped at destination.</param>
    /// <returns>True if a match; otherwise false.</returns>
    public override bool IsMatch(Point screenPt, PageDragEndData? dragEndData)
    {
        // First time around...
        if (_visibleNotDraggedPages == -1)
        {
            // If pages are being dragged from this cell
            if (dragEndData?.Navigator == Cell)
            {
                // Create list of all the visible pages in the cell
                var visiblePages = new KryptonPageCollection();
                if (Cell is not null)
                {
                    foreach (KryptonPage page in Cell.Pages)
                    {
                        if (page.LastVisibleSet)
                        {
                            visiblePages.Add(page);
                        }
                    }
                }

                // Remove all those that are being dragged
                if (dragEndData is not null)
                {
                    foreach (KryptonPage page in dragEndData.Pages)
                    {
                        visiblePages.Remove(page);
                    }
                }

                // Cache number of visible pages in target that are not part of the dragging set
                _visibleNotDraggedPages = visiblePages.Count;
            }
            else
            {
                // Pages not coming from this cell so we always allow the cell edge targets
                _visibleNotDraggedPages = int.MaxValue;
            }
        }

        // If the drop leaves at least 1 page in the navigator then allow drag to edge
        if (_visibleNotDraggedPages >= 1)
        {
            return base.IsMatch(screenPt, dragEndData);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Perform the drop action associated with the target.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="data">Data to pass to the target to process drop.</param>
    /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
    public override bool PerformDrop(Point screenPt, PageDragEndData? data)
    {
        // We need a parent sequence in order to perform drop
        if ( Cell is not null && Cell.WorkspaceParent is KryptonWorkspaceSequence parent)
        {
            // Transfer the dragged pages into a new cell
            var cell = new KryptonWorkspaceCell();
            KryptonPage? page = ProcessDragEndData(Workspace!, cell, data);

            // If no pages are transferred then we do nothing and no longer need cell instance
            if (page == null)
            {
                cell.Dispose();
            }
            else
            {
                // If the parent sequence is not the same direction as that needed for the drop then...
                var dropHorizontal = Edge is VisualOrientation.Left or VisualOrientation.Right;
                if ((dropHorizontal && (parent.Orientation == Orientation.Vertical)) ||
                    (!dropHorizontal && (parent.Orientation == Orientation.Horizontal)))
                {
                    // Find opposite direction to the parent sequence
                    Orientation sequenceOrientation = parent.Orientation == Orientation.Horizontal
                        ? Orientation.Vertical
                        : Orientation.Horizontal;

                    // Create a new sequence and transfer the target cell into it
                    var sequence = new KryptonWorkspaceSequence(sequenceOrientation);
                    var index = parent.Children!.IndexOf(Cell);
                    parent.Children.RemoveAt(index);
                    sequence.Children!.Add(Cell);

                    // Put the sequence into the place where the target cell used to be
                    parent.Children.Insert(index, sequence);

                    // Add new cell to the start or the end of the new sequence?
                    if (Edge is VisualOrientation.Left or VisualOrientation.Top)
                    {
                        sequence.Children.Insert(0, cell);
                    }
                    else
                    {
                        sequence.Children.Add(cell);
                    }
                }
                else
                {
                    // Find position of the target cell
                    var index = parent.Children!.IndexOf(Cell);

                    // Add new cell before or after the target cell?
                    if (Edge is VisualOrientation.Left or VisualOrientation.Top)
                    {
                        parent.Children.Insert(index, cell);
                    }
                    else
                    {
                        parent.Children.Insert(index + 1, cell);
                    }
                }

                // Make the last page transferred the newly selected page of the cell
                if (page != null)
                {
                    // Does the cell allow the selection of tabs?
                    if (cell.AllowTabSelect)
                    {
                        cell.SelectedPage = page;
                    }

                    // Need to layout so the new cell has been added as a child control and 
                    // therefore can receive the focus we want to give it immediately afterwards
                    Workspace?.PerformLayout();

                    if (!cell.IsDisposed)
                    {
                        // Without this DoEvents() call the dropping of multiple pages in a complex arrangement causes an exception for
                        // a complex reason that is hard to work out (i.e. I'm not entirely sure). Something to do with using select to
                        // change activation is causing the source workspace control to dispose to earlier.
                        Application.DoEvents();
                        cell.Select();
                    }
                }
            }
        }

        return true;
    }
    #endregion
}