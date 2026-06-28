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
/// Panel that stacks <see cref="KryptonAutoHiddenGroup"/> controls along a docking edge.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonAutoHiddenPanel : KryptonPanel
{
    #region Static Fields

    private const int EXTRA_PADDING = 4;

    #endregion

    #region Identity
    /// <summary>
    /// Applies inward-facing padding so auto-hidden tab groups sit inset from the client area.
    /// </summary>
    /// <param name="edge">Docking edge that determines which side receives extra padding.</param>
    public KryptonAutoHiddenPanel(DockingEdge edge)
    {
        // Add extra padding between the child items and the side facing inwards
        switch (edge)
        {
            case DockingEdge.Left:
                Padding = new Padding(0, 0, EXTRA_PADDING, 0);
                break;
            case DockingEdge.Right:
                Padding = new Padding(EXTRA_PADDING, 0, 0, 0);
                break;
            case DockingEdge.Top:
                Padding = new Padding(0, 0, 0, EXTRA_PADDING);
                break;
            case DockingEdge.Bottom:
                Padding = new Padding(0, EXTRA_PADDING, 0, 0);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Combines preferred sizes of child groups that have at least one visible page, respecting dock direction and padding.
    /// </summary>
    /// <param name="proposedSize">Layout constraint passed to each child group.</param>
    /// <returns>Aggregate width and height needed to display qualifying child groups.</returns>
    public override Size GetPreferredSize(Size proposedSize)
    {
        var width = 0;
        var height = 0;
        foreach (KryptonAutoHiddenGroup group in Controls)
        {
            // Only interested in the group if it has some visible pages
            if (group.Pages.VisibleCount > 0)
            {
                // Find the exact size the child would like to be sized
                Size groupSize = group.GetPreferredSize(proposedSize);

                switch (Dock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        // We are as wide as the widest child and as tall as heights added together
                        width = Math.Max(width, groupSize.Width);
                        height += groupSize.Height;
                        break;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        // We are as tall as the tallest child and as wide as widths added together
                        width += groupSize.Width;
                        height = Math.Max(height, groupSize.Height);
                        break;
                    case DockStyle.None:
                        // We are big enough to show the largest child
                        width = Math.Max(width, groupSize.Width);
                        height = Math.Max(height, groupSize.Height);
                        break;
                }
            }
        }

        // Add on any padding values but only if we have something to display
        if (width > 0)
        {
            width += Padding.Horizontal;
        }

        if (height > 0)
        {
            height += Padding.Vertical;
        }

        return new Size(width, height);
    }
    #endregion
}
