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
/// Paints the intersection square when both Krypton scrollbars are visible and
/// <see cref="KryptonScrollbarManager.CornerStyle"/> is <see cref="ScrollbarCornerStyle.ThemedCorner"/>.
/// </summary>
internal sealed class KryptonScrollBarCorner : Control
{
    #region Identity

    public KryptonScrollBarCorner()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint, true);
        TabStop = false;
    }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override void OnHandleCreated(EventArgs e)
    {
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        base.OnHandleCreated(e);
    }

    /// <inheritdoc />
    protected override void OnHandleDestroyed(EventArgs e)
    {
        KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        base.OnHandleDestroyed(e);
    }

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Background is painted in OnPaint.
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        KryptonScrollBarRenderer.InitColors();
        KryptonScrollBarRenderer.DrawBackground(
            e.Graphics,
            ClientRectangle,
            ScrollBarOrientation.Vertical);
    }

    #endregion

    #region Implementation

    private void OnGlobalPaletteChanged(object? sender, EventArgs e) => Invalidate();

    #endregion
}
