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
/// Static conversion and layout helpers for docking edge geometry.
/// </summary>
public static class DockingHelper
{
    #region Public
    /// <summary>
    /// Maps a <see cref="DockingEdge"/> to the equivalent <see cref="DockStyle"/> value.
    /// </summary>
    /// <param name="edge">Edge to convert.</param>
    /// <param name="opposite">When <see langword="true"/>, returns the style for the opposite edge.</param>
    /// <returns>The matching <see cref="DockStyle"/> for <paramref name="edge"/>.</returns>
    public static DockStyle DockStyleFromDockEdge(DockingEdge edge, bool opposite)
    {
        switch (edge)
        {
            case DockingEdge.Top:
                return (opposite ? DockStyle.Bottom : DockStyle.Top);
            case DockingEdge.Bottom:
                return (opposite ? DockStyle.Top : DockStyle.Bottom);
            case DockingEdge.Left:
                return (opposite ? DockStyle.Right : DockStyle.Left);
            case DockingEdge.Right:
                return (opposite ? DockStyle.Left : DockStyle.Right);
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(edge.ToString());
                return DockStyle.Top;
        }
    }

    /// <summary>
    /// Maps a <see cref="DockingEdge"/> to the axis orientation used for separators along that edge.
    /// </summary>
    /// <param name="edge">Edge to convert.</param>
    /// <returns><see cref="Orientation.Vertical"/> for left and right edges; otherwise <see cref="Orientation.Horizontal"/>.</returns>
    public static Orientation OrientationFromDockEdge(DockingEdge edge) => edge switch
    {
        DockingEdge.Left or DockingEdge.Right => Orientation.Vertical,
        _ => Orientation.Horizontal
    };

    /// <summary>
    /// Calculates the client-area rectangle remaining after subtracting visible edge-docked child controls.
    /// </summary>
    /// <param name="c">Host control whose docked children reduce the inner area.</param>
    /// <returns>Remaining client rectangle in control coordinates.</returns>
    public static Rectangle InnerRectangle(Control c)
    {
        // Start with entire client area
        Rectangle inner = c.ClientRectangle;

        // Adjust for edge docked controls
        foreach (Control child in c.Controls)
        {
            if (child.Visible)
            {
                switch (child.Dock)
                {
                    case DockStyle.Left:
                        inner.Width -= child.Width;
                        inner.X += child.Width;
                        break;
                    case DockStyle.Right:
                        inner.Width -= child.Width;
                        break;
                    case DockStyle.Top:
                        inner.Height -= child.Height;
                        inner.Y += child.Height;
                        break;
                    case DockStyle.Bottom:
                        inner.Height -= child.Height;
                        break;
                }
            }
        }

        return inner;
    }
    #endregion
}
