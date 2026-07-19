#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides layout rectangles for Krypton-themed scrollbars on native wrapper controls.
/// </summary>
internal interface IKryptonNativeWrapperScrollbarBounds
{
    /// <summary>
    /// Gets the outer scrollbar lane and inner content rectangles in host client coordinates.
    /// </summary>
    NativeWrapperScrollbarLayout GetNativeWrapperScrollbarLayout();
}

/// <summary>
/// Scrollbar placement rectangles for a Krypton native wrapper control.
/// </summary>
internal struct NativeWrapperScrollbarLayout
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NativeWrapperScrollbarLayout"/> struct.
    /// </summary>
    /// <param name="laneRect">The outer scrollbar lane.</param>
    /// <param name="contentRect">The inner native content area.</param>
    public NativeWrapperScrollbarLayout(Rectangle laneRect, Rectangle contentRect)
    {
        LaneRect = laneRect;
        ContentRect = contentRect;
    }

    /// <summary>
    /// Area inside the themed border (ViewLayoutFill client rectangle before display padding).
    /// </summary>
    public Rectangle LaneRect { get; }

    /// <summary>
    /// The inner native content area (<see cref="ViewLayoutFill.FillRect"/>).
    /// </summary>
    public Rectangle ContentRect { get; }
}

/// <summary>
/// Helper methods for native wrapper scrollbar placement.
/// </summary>
internal static class KryptonNativeWrapperScrollbarBoundsHelper
{
    internal static NativeWrapperScrollbarLayout GetLayout(Control wrapper, ViewLayoutFill layoutFill)
    {
        // ClientRectangle is the docker interior after border display padding — the
        // lane scrollbars should fill, flush to the inside of the themed border.
        // FillRect is that area minus DisplayPadding (the native child bounds).
        Rectangle laneRect = layoutFill.ClientRectangle;
        if (laneRect.IsEmpty)
        {
            laneRect = wrapper.ClientRectangle;
        }

        Rectangle contentRect = layoutFill.FillRect;
        if (contentRect.IsEmpty)
        {
            contentRect = laneRect;
        }
        else
        {
            contentRect = Rectangle.Intersect(contentRect, laneRect);
            if (contentRect.IsEmpty)
            {
                contentRect = laneRect;
            }
        }

        return new NativeWrapperScrollbarLayout(laneRect, contentRect);
    }

    /// <summary>
    /// Shrinks the native child fill rectangle when themed overlay scrollbars are visible.
    /// </summary>
    internal static Rectangle GetNativeChildBounds(Rectangle fillRect,
        KryptonScrollbarManager? scrollbarManager,
        bool useKryptonScrollbars)
    {
        if (!useKryptonScrollbars || scrollbarManager == null)
        {
            return fillRect;
        }

        return scrollbarManager.GetInsetContentBounds(fillRect);
    }
}
