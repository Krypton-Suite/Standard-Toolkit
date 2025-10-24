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
/// Target one of the four sides of the workspace control.
/// </summary>
public class DragTargetWorkspaceEdge : DragTargetWorkspace
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DragTargetWorkspaceEdge class.
    /// </summary>
    /// <param name="screenRect">Rectangle for screen area.</param>
    /// <param name="hotRect">Rectangle for hot area.</param>
    /// <param name="drawRect">Rectangle for draw area.</param>
    /// <param name="hint">Target hint which should be one of the edges.</param>
    /// <param name="workspace">Control instance for drop.</param>
    /// <param name="allowFlags">Only drop pages that have one of these flags defined.</param>
    public DragTargetWorkspaceEdge(Rectangle screenRect,
        Rectangle hotRect,
        Rectangle drawRect,
        DragTargetHint hint,
        KryptonWorkspace workspace,
        KryptonPageFlags allowFlags)
        : base(screenRect, hotRect, drawRect, hint, workspace, allowFlags)
    {
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
    #endregion

    #region Public
    /// <summary>
    /// Gets the dragging edge.
    /// </summary>
    public VisualOrientation Edge { get; }

    /// <summary>
    /// Perform the drop action associated with the target.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="data">Data to pass to the target to process drop.</param>
    /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
    public override bool PerformDrop(Point screenPt, PageDragEndData? data)
    {
        if (Workspace is null)
        {
            throw new ArgumentNullException(nameof(Workspace));
        }

        // Transfer the dragged pages into a new cell
        var cell = new KryptonWorkspaceCell();
        KryptonPage? page = ProcessDragEndData(Workspace, cell, data);

        // If no pages are transferred then we do nothing and no longer need cell instance
        if (page == null)
        {
            cell.Dispose();
        }
        else
        {
            // If the root is not the same direction as that needed for the drop then...
            var dropHorizontal = Edge is VisualOrientation.Left or VisualOrientation.Right;
            if ((dropHorizontal && (Workspace.Root.Orientation == Orientation.Vertical)) ||
                (!dropHorizontal && (Workspace.Root.Orientation == Orientation.Horizontal)))
            {
                // Create a new sequence and place all existing root items into it
                var sequence = new KryptonWorkspaceSequence(Workspace.Root.Orientation);
                for (var i = Workspace.Root.Children!.Count - 1; i >= 0; i--)
                {
                    Component child = Workspace.Root.Children[i];
                    Workspace.Root.Children.RemoveAt(i);
                    sequence.Children!.Insert(0, child);
                }

                // Put the new sequence in the root so all items are now grouped together
                Workspace.Root.Children.Add(sequence);

                // Switch the direction of the root
                Workspace.Root.Orientation = Workspace.Root.Orientation == Orientation.Horizontal
                    ? Orientation.Vertical
                    : Orientation.Horizontal;
            }

            // Add to the start or the end of the root sequence?
            if (Edge is VisualOrientation.Left or VisualOrientation.Top)
            {
                Workspace.Root.Children!.Insert(0, cell);
            }
            else
            {
                Workspace.Root.Children!.Add(cell);
            }

            // Make the last page transfer the newly selected page of the cell
            // Does the cell allow the selection of tabs?
            if (cell.AllowTabSelect)
            {
                cell.SelectedPage = page;
            }

            // Need to layout so the new cell has been added as a child control and 
            // therefore can receive the focus we want to give it immediately afterwards
            Workspace.PerformLayout();

            if (!cell.IsDisposed)
            {
                // Without this DoEvents() call the dropping of multiple pages in a complex arrangement causes an exception for
                // a complex reason that is hard to work out (i.e. I'm not entirely sure). Something to do with using select to
                // change activation is causing the source workspace control to dispose to earlier.
                Application.DoEvents();
                cell.Select();
            }
        }

        return true;
    }
    #endregion
}