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
/// Offers peer <see cref="KryptonForm"/> windows for the remaining zones of a toolkit snap card.
/// </summary>
internal sealed class VisualSnapLayoutAssistPopup : VisualPopup
{
    private const int OuterPadding = 12;
    private const int RowHeight = 36;
    private const int RowGap = 4;
    private const int MaxPeers = 8;

    private readonly PaletteBase _palette;
    private readonly List<PeerHit> _hits = new List<PeerHit>();
    private KryptonForm[] _peers = Array.Empty<KryptonForm>();
    private int _hotIndex = -1;

    /// <summary>
    /// Initializes the Assist picker.
    /// </summary>
    /// <param name="palette">Palette used for chrome.</param>
    /// <param name="renderer">Renderer paired with <paramref name="palette"/>.</param>
    internal VisualSnapLayoutAssistPopup(PaletteBase palette, IRenderer renderer)
        : base(renderer, KryptonManager.SnapLayoutValues.ShowShadow)
    {
        _palette = palette;
        ViewManager?.Attach(this, new ViewLayoutNull());
        SetStyle(ControlStyles.Opaque, true);
    }

    /// <summary>
    /// Occurs when the user picks a peer form.
    /// </summary>
    internal event EventHandler<KryptonSnapLayoutAssistEventArgs>? PeerSelected;

    /// <summary>
    /// Occurs when the pointer leaves without a selection.
    /// </summary>
    internal event EventHandler? Cancelled;

    /// <inheritdoc />
    public override PaletteBase? GetResolvedPalette() => _palette;

    /// <inheritdoc />
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SuppressSystemCornerRounding();
    }

    /// <summary>
    /// Shows the peer list near <paramref name="anchorScreen"/>.
    /// </summary>
    /// <param name="anchorScreen">Rectangle under which to place the picker.</param>
    /// <param name="peers">Peer forms to offer.</param>
    /// <param name="workArea">Monitor work area used for clamping.</param>
    /// <param name="instruction">Short instruction drawn above the list.</param>
    internal void ShowPeers(Rectangle anchorScreen, KryptonForm[] peers, Rectangle workArea, string instruction)
    {
        _peers = peers ?? Array.Empty<KryptonForm>();
        if (_peers.Length == 0)
        {
            return;
        }

        int count = Math.Min(MaxPeers, _peers.Length);
        Font font = _palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal)
            ?? SystemFonts.DefaultFont;
        int width = 280;
        for (int i = 0; i < count; i++)
        {
            string title = PeerTitle(_peers[i]);
            Size size = TextRenderer.MeasureText(title, font);
            width = Math.Max(width, size.Width + OuterPadding * 2 + 40);
        }

        Size instructionSize = TextRenderer.MeasureText(instruction, font);
        width = Math.Max(width, instructionSize.Width + OuterPadding * 2);
        int height = OuterPadding * 2 + instructionSize.Height + 8 + count * RowHeight + Math.Max(0, count - 1) * RowGap;
        width = Math.Min(width, Math.Max(200, workArea.Width - 16));

        _hits.Clear();
        int top = OuterPadding + instructionSize.Height + 8;
        for (int i = 0; i < count; i++)
        {
            var bounds = new Rectangle(OuterPadding, top, Math.Max(1, width - OuterPadding * 2), RowHeight);
            _hits.Add(new PeerHit(bounds, _peers[i]));
            top += RowHeight + RowGap;
        }

        int x = anchorScreen.X + Math.Max(0, (anchorScreen.Width - width) / 2);
        int y = anchorScreen.Bottom + 8;
        if (y + height > workArea.Bottom)
        {
            y = Math.Max(workArea.Top, anchorScreen.Top - height - 8);
        }

        if (x + width > workArea.Right)
        {
            x = workArea.Right - width;
        }

        if (x < workArea.Left)
        {
            x = workArea.Left;
        }

        Tag = instruction;
        Size = new Size(width, height);
        Show(new Rectangle(x, y, width, height));
        ApplyChromeShape();
    }

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        GraphicsState state = e.Graphics.Save();
        e.Graphics.ResetClip();
        using (Brush brush = CreateBackBrush(ClientRectangle, PaletteBackStyle.PanelAlternate, PaletteState.Normal))
        {
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }

        e.Graphics.Restore(state);
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs? e)
    {
        if (e == null)
        {
            return;
        }

        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (Brush brush = CreateBackBrush(ClientRectangle, PaletteBackStyle.PanelAlternate, PaletteState.Normal))
        {
            GraphicsState state = g.Save();
            g.ResetClip();
            g.FillRectangle(brush, ClientRectangle);
            g.Restore(state);
        }

        Font font = _palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal)
            ?? SystemFonts.DefaultFont;
        Color text = _palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
        string instruction = Tag as string ?? string.Empty;
        var instructionBounds = new Rectangle(OuterPadding, OuterPadding, Math.Max(1, Width - OuterPadding * 2), font.Height + 4);
        TextRenderer.DrawText(g, instruction, font, instructionBounds, text,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);

        for (int i = 0; i < _hits.Count; i++)
        {
            PeerHit hit = _hits[i];
            bool hot = i == _hotIndex;
            PaletteState state = hot ? PaletteState.Tracking : PaletteState.Normal;
            using (Brush fill = CreateBackBrush(hit.Bounds, PaletteBackStyle.ButtonStandalone, state))
            using (Pen border = CreateBorderPen(PaletteBorderStyle.ButtonStandalone, state))
            using (GraphicsPath path = CreateRoundRect(hit.Bounds, 4f))
            {
                g.FillPath(fill, path);
                g.DrawPath(border, path);
            }

            var textBounds = new Rectangle(hit.Bounds.X + 10, hit.Bounds.Y, hit.Bounds.Width - 20, hit.Bounds.Height);
            TextRenderer.DrawText(g, PeerTitle(hit.Form), font, textBounds, text,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
        }

        using (Pen border = CreateBorderPen(PaletteBorderStyle.ControlClient, PaletteState.Normal))
        using (GraphicsPath path = CreateRoundRect(ClientRectangle, Math.Max(0f, KryptonManager.SnapLayoutValues.CornerRounding)))
        {
            g.DrawPath(border, path);
        }
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        int hot = HitTest(e.Location);
        if (hot != _hotIndex)
        {
            _hotIndex = hot;
            Invalidate();
        }

        base.OnMouseMove(e);
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            int hot = HitTest(e.Location);
            if (hot >= 0)
            {
                PeerSelected?.Invoke(this, new KryptonSnapLayoutAssistEventArgs(_hits[hot].Form));
                return;
            }
        }

        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        _hotIndex = -1;
        Invalidate();
        base.OnMouseLeave(e);
    }

    /// <inheritdoc />
    protected override bool ProcessDialogKey(Keys keyData)
    {
        if (keyData == Keys.Escape)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
            return true;
        }

        return base.ProcessDialogKey(keyData);
    }

    private void ApplyChromeShape()
    {
        if (!IsHandleCreated || ClientSize.Width < 2 || ClientSize.Height < 2)
        {
            return;
        }

        Color chrome = _palette.GetBackColor1(PaletteBackStyle.PanelAlternate, PaletteState.Normal);
        if (!chrome.IsEmpty)
        {
            BackColor = chrome;
        }

        SuppressSystemCornerRounding();
        float radius = Math.Max(0f, KryptonManager.SnapLayoutValues.CornerRounding);
        using (GraphicsPath regionPath = CreateRoundRect(ClientRectangle, radius))
        {
            Region = new Region(regionPath);
        }

        Color color = KryptonManager.SnapLayoutValues.ShadowColor;
        if (color.IsEmpty)
        {
            color = Color.Black;
        }

        if (KryptonManager.SnapLayoutValues.ShowShadow)
        {
            UpdateShadowAppearance(color, KryptonManager.SnapLayoutValues.ShadowOpacity, true);
            int offset = KryptonManager.SnapLayoutValues.ShadowOffset;
            ShowShadow(new Rectangle(Location, Size), 0, offset);
            Rectangle middle = ClientRectangle;
            middle.Inflate(-1, -1);
            Rectangle inner = ClientRectangle;
            inner.Inflate(-2, -2);
            DefineShadowPaths(
                CreateRoundRect(ClientRectangle, radius),
                CreateRoundRect(middle, Math.Max(0f, radius - 1f)),
                CreateRoundRect(inner, Math.Max(0f, radius - 2f)));
        }
    }

    private int HitTest(Point point)
    {
        for (int i = 0; i < _hits.Count; i++)
        {
            if (_hits[i].Bounds.Contains(point))
            {
                return i;
            }
        }

        return -1;
    }

    private static string PeerTitle(KryptonForm form)
    {
        string text = form.Text;
        if (string.IsNullOrWhiteSpace(text))
        {
            text = form.Name;
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            text = form.GetType().Name;
        }

        return text;
    }

    private Brush CreateBackBrush(Rectangle rect, PaletteBackStyle style, PaletteState state)
    {
        Color color1 = _palette.GetBackColor1(style, state);
        Color color2 = _palette.GetBackColor2(style, state);
        if (color1.IsEmpty)
        {
            color1 = SystemColors.Window;
        }

        if (color2.IsEmpty || color2.ToArgb() == color1.ToArgb() || rect.Width < 1 || rect.Height < 1)
        {
            return new SolidBrush(color1);
        }

        return new LinearGradientBrush(rect, color1, color2, LinearGradientMode.Vertical);
    }

    private Pen CreateBorderPen(PaletteBorderStyle style, PaletteState state)
    {
        Color color = _palette.GetBorderColor1(style, state);
        if (color.IsEmpty)
        {
            color = SystemColors.ControlDark;
        }

        int width = _palette.GetBorderWidth(style, state);
        return new Pen(color, Math.Max(1, width)) { LineJoin = LineJoin.Round };
    }

    private static GraphicsPath CreateRoundRect(Rectangle bounds, float radius)
    {
        var path = new GraphicsPath { FillMode = FillMode.Winding };
        if (bounds.Width < 1 || bounds.Height < 1)
        {
            return path;
        }

        float maxRadius = Math.Min(bounds.Width, bounds.Height) / 2f;
        float clamped = Math.Max(0f, Math.Min(radius, maxRadius));
        var rect = new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        RoundedPathHelper.CornerArcLengths arcs = RoundedPathHelper.CornerArcLengths.FromCornerRounding(
            PaletteCornerRounding.Uniform(clamped), rect, 0);
        RoundedPathHelper.AppendRoundedRectangle(path, rect, arcs, true);
        return path;
    }

    private readonly struct PeerHit
    {
        internal PeerHit(Rectangle bounds, KryptonForm form)
        {
            Bounds = bounds;
            Form = form;
        }

        internal Rectangle Bounds { get; }

        internal KryptonForm Form { get; }
    }
}

/// <summary>
/// Event data for an Assist peer selection.
/// </summary>
internal sealed class KryptonSnapLayoutAssistEventArgs : EventArgs
{
    /// <summary>
    /// Initializes event data for <paramref name="form"/>.
    /// </summary>
    /// <param name="form">Peer form the user chose.</param>
    internal KryptonSnapLayoutAssistEventArgs(KryptonForm form) => Form = form;

    /// <summary>Gets the chosen peer form.</summary>
    internal KryptonForm Form { get; }
}
