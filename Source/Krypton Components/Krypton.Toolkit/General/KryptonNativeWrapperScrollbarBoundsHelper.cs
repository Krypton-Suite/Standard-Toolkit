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
    /// The outer lane available to scrollbars, typically the host client area.
    /// </summary>
    public Rectangle LaneRect { get; }

    /// <summary>
    /// The inner native content area, typically the latest <see cref="ViewLayoutFill.FillRect"/>.
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
        Rectangle laneRect = wrapper.ClientRectangle;
        Rectangle contentRect = layoutFill.FillRect;

        if (contentRect.IsEmpty)
        {
            contentRect = laneRect;
        }

        return new NativeWrapperScrollbarLayout(laneRect, contentRect);
    }
}
