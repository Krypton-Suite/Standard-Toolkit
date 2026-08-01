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
/// The palette state storage is owned by <see cref="KryptonScrollbarManager"/>
/// (CornerStateCommon/Normal/Disabled) so customizations survive corner recreation.
/// </summary>
internal sealed class KryptonScrollBarCorner : Control
{
    #region Instance Fields

    private readonly PaletteBack _stateNormal;
    private readonly PaletteBack _stateDisabled;
    private IDisposable? _mementoBack;

    #endregion

    #region Identity

    /// <param name="stateNormal">Manager-owned background palette for the enabled state.</param>
    /// <param name="stateDisabled">Manager-owned background palette for the disabled state.</param>
    public KryptonScrollBarCorner(PaletteBack stateNormal, PaletteBack stateDisabled)
    {
        _stateNormal = stateNormal;
        _stateDisabled = stateDisabled;

        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint, true);
        TabStop = false;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _mementoBack?.Dispose();
            _mementoBack = null;
        }

        base.Dispose(disposing);
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
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Invalidate();
    }

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Background is painted in OnPaint.
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        // Refresh the renderer-derived defaults so the inherit chain reflects the
        // current global palette, then draw through the standard back renderer so
        // per-state overrides (solid color, gradient, image) all work.
        KryptonScrollBarRenderer.InitColors();

        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        PaletteBack paletteBack = Enabled ? _stateNormal : _stateDisabled;

        if (paletteBack.GetBackDraw(state) != InheritBool.True)
        {
            return;
        }

        IRenderer renderer = KryptonManager.CurrentGlobalPalette.GetRenderer();
        using var context = new RenderContext(this, e.Graphics, ClientRectangle, renderer);
        using var path = new GraphicsPath();
        path.AddRectangle(ClientRectangle);
        _mementoBack = renderer.RenderStandardBack.DrawBack(context, ClientRectangle, path, paletteBack,
            VisualOrientation.Top, state, _mementoBack);
    }

    #endregion

    #region Implementation

    private void OnGlobalPaletteChanged(object? sender, EventArgs e) => Invalidate();

    #endregion
}

/// <summary>
/// Bottom of the corner inherit chain. By default supplies the flat scrollbar
/// background fill from <see cref="KryptonScrollBarRenderer"/> so an unmodified
/// corner is seamless against both scrollbars and tracks the global palette.
/// When <see cref="Style"/> is set, all values are taken from the current global
/// palette for that back style instead (e.g. <see cref="PaletteBackStyle.PanelClient"/>).
/// </summary>
internal sealed class PaletteBackScrollBarCornerInherit : PaletteBackInherit
{
    /// <summary>
    /// Gets or sets the optional palette back style the corner inherits from.
    /// Null keeps the flat scrollbar background fill.
    /// </summary>
    public PaletteBackStyle? Style { get; set; }

    /// <inheritdoc />
    public override InheritBool GetBackDraw(PaletteState state) => InheritBool.True;

    /// <inheritdoc />
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackGraphicsHint(style, state)
            : PaletteGraphicsHint.None;

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackColor1(style, state)
            : KryptonScrollBarRenderer.BackgroundFillColor;

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackColor2(style, state)
            : KryptonScrollBarRenderer.BackgroundFillColor;

    /// <inheritdoc />
    public override PaletteColorStyle GetBackColorStyle(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackColorStyle(style, state)
            : PaletteColorStyle.Solid;

    /// <inheritdoc />
    public override PaletteRectangleAlign GetBackColorAlign(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackColorAlign(style, state)
            : PaletteRectangleAlign.Local;

    /// <inheritdoc />
    public override float GetBackColorAngle(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackColorAngle(style, state)
            : 90f;

    /// <inheritdoc />
    public override Image? GetBackImage(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackImage(style, state)
            : null;

    /// <inheritdoc />
    public override PaletteImageStyle GetBackImageStyle(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackImageStyle(style, state)
            : PaletteImageStyle.Tile;

    /// <inheritdoc />
    public override PaletteRectangleAlign GetBackImageAlign(PaletteState state) =>
        Style is { } style
            ? KryptonManager.CurrentGlobalPalette.GetBackImageAlign(style, state)
            : PaletteRectangleAlign.Local;
}
