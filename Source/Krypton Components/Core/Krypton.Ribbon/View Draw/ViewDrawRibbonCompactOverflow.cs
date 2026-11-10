#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Trailing ellipsis on the compact command row. Opens the commands that did not fit.
/// </summary>
internal class ViewDrawRibbonCompactOverflow : ViewLeaf
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonTab _ribbonTab;
    private readonly LeftDownButtonController _controller;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawRibbonCompactOverflow"/> class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control.</param>
    /// <param name="ribbonTab">Tab whose overflow this button shows.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonCompactOverflow(KryptonRibbon ribbon,
        KryptonRibbonTab ribbonTab,
        NeedPaintHandler needPaint)
    {
        _ribbon = ribbon;
        _ribbonTab = ribbonTab;
        _controller = new LeftDownButtonController(ribbon, this, needPaint);
        _controller.Click += OnClick;
        MouseController = _controller;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"ViewDrawRibbonCompactOverflow:{Id}";
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        float dpi = _ribbon.DeviceDpi / 96f;
        int width = Math.Max(16, (int)(22 * dpi));
        int height = Math.Max(16, (int)(22 * dpi));
        return new Size(width, height);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        ClientRectangle = context.DisplayRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        if (context.Renderer is null)
        {
            return;
        }

        IPaletteRibbonText text = ElementState == PaletteState.Disabled
            ? _ribbon.StateDisabled.RibbonGroupButtonText
            : _ribbon.StateNormal.RibbonGroupButtonText;
        Color color = text.GetRibbonTextColor(PaletteState.Normal);
        Rectangle bounds = ClientRectangle;
        if ((bounds.Width < 4) || (bounds.Height < 4) || (color.A == 0))
        {
            return;
        }

        if ((ElementState == PaletteState.Tracking) || (ElementState == PaletteState.Pressed))
        {
            int alpha = ElementState == PaletteState.Pressed ? 48 : 28;
            using var back = new SolidBrush(Color.FromArgb(alpha, color));
            context.Graphics.FillRectangle(back, bounds);
        }

        float dpi = _ribbon.DeviceDpi / 96f;
        int diameter = Math.Max(2, (int)(3 * dpi));
        int gap = Math.Max(2, (int)(3 * dpi));
        int total = (diameter * 3) + (gap * 2);
        int x = bounds.X + Math.Max(0, (bounds.Width - total) / 2);
        int y = bounds.Y + Math.Max(0, (bounds.Height - diameter) / 2);
        SmoothingMode previous = context.Graphics.SmoothingMode;
        context.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(color))
        {
            for (var i = 0; i < 3; i++)
            {
                context.Graphics.FillEllipse(brush, x + (i * (diameter + gap)), y, diameter, diameter);
            }
        }

        context.Graphics.SmoothingMode = previous;
    }
    #endregion

    #region Implementation
    private void OnClick(object? sender, MouseEventArgs e)
    {
        if (_ribbon.InDesignMode || !_ribbon.Enabled)
        {
            _controller.RemoveFixed();
            return;
        }

        VisualPopupManager.Singleton.EndAllTracking();
        var popup = new VisualPopupRibbonOverflow(_ribbon, _ribbonTab, _ribbon.Renderer);
        popup.Disposed += OnPopupDisposed;
        popup.ShowCalculatingSize(_ribbon.RectangleToScreen(ClientRectangle));
    }

    private void OnPopupDisposed(object? sender, EventArgs e) => _controller.RemoveFixed();
    #endregion
}
