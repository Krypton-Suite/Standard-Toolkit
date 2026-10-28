#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Palette-drawn snap-layout menu shown under a <see cref="KryptonForm"/> maximize button.
/// </summary>
internal sealed class VisualSnapLayoutPopup : VisualPopup
{
    #region Static Fields

    private const int OuterPadding = 10;
    private const int CardGap = 8;
    private const int CardPadding = 5;
    private const int ZoneGap = 3;
    private const int DefaultCardWidth = 108;
    private const int DefaultCardHeight = 72;
    private const int MinimumCardWidth = 64;
    private const int MinimumCardHeight = 48;
    private const int PreferredRowWidth = 440;
    private const int CustomCaptionHeight = 16;

    #endregion

    #region Instance Fields

    private readonly PaletteBase _palette;
    private readonly List<ZoneHit> _hits = new List<ZoneHit>();
    private readonly List<Rectangle> _customCaptions = new List<Rectangle>();
    private readonly Bitmap? _applicationImage;
    private Bitmap?[] _taskbarImages = Array.Empty<Bitmap?>();
    private Rectangle _workArea;
    private SnapPreviewWindow? _preview;
    private int _hotIndex = -1;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a popup that paints with <paramref name="palette"/> and <paramref name="renderer"/>.
    /// </summary>
    /// <param name="palette">Form palette. Colors are read from existing context-menu and button styles.</param>
    /// <param name="renderer">Renderer paired with <paramref name="palette"/>.</param>
    /// <param name="applicationIcon">Icon of the form that owns the menu. Not taken ownership of. Drawn in the zone this form will snap to.</param>
    internal VisualSnapLayoutPopup(PaletteBase palette, IRenderer renderer, Icon? applicationIcon)
        : base(renderer, true)
    {
        _palette = palette;
        _applicationImage = CreateApplicationImage(applicationIcon);

        // Show lays the popup out. An unattached manager has no control and faults that layout.
        ViewManager?.Attach(this, new ViewLayoutNull());
    }

    #endregion

    /// <summary>
    /// Occurs when the pointer leaves the popup.
    /// </summary>
    internal event EventHandler? PointerLeft;

    /// <summary>
    /// Occurs when the user presses a zone.
    /// </summary>
    internal event EventHandler<KryptonSnapLayoutZoneEventArgs>? ZoneSelected;

    /// <inheritdoc />
    public override PaletteBase? GetResolvedPalette() => _palette;

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_preview != null)
            {
                _preview.Dispose();
                _preview = null;
            }

            for (int i = 0; i < _taskbarImages.Length; i++)
            {
                Bitmap? image = _taskbarImages[i];
                if (image != null && !ReferenceEquals(image, _applicationImage))
                {
                    image.Dispose();
                }
            }

            _applicationImage?.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Shows the cards just under <paramref name="anchorScreen"/>, flipping above when needed.
    /// </summary>
    /// <param name="anchorScreen">Maximize button in screen coordinates.</param>
    /// <param name="cards">Cards to draw. Empty does not show.</param>
    /// <param name="workArea">Monitor work area the zones map onto. Hover previews use the same rectangle as the snap.</param>
    /// <param name="ownerWindow">Form the menu belongs to. Its icon is placed with the other taskbar applications.</param>
    internal void ShowBelow(Rectangle anchorScreen, KryptonSnapLayoutCard[] cards, Rectangle workArea, IntPtr ownerWindow)
    {
        if (cards.Length == 0)
        {
            return;
        }

        _workArea = workArea;
        _taskbarImages = KryptonSnapLayoutTaskbar.Collect(ownerWindow, _applicationImage);
        Rectangle work = workArea.Width > 0 && workArea.Height > 0
            ? workArea
            : Screen.GetWorkingArea(anchorScreen);
        MeasureCards(cards, work.Width, out int columns, out int cardWidth, out int cardHeight, out int width, out int height);
        LayoutCards(cards, columns, cardWidth, cardHeight);

        // Keep the menu under the maximize button. A wide menu grows left from that button.
        int x = anchorScreen.Right - width;
        int y = anchorScreen.Bottom + 2;
        if (y + height > work.Bottom)
        {
            y = anchorScreen.Top - height - 2;
        }

        if (x + width > work.Right)
        {
            x = work.Right - width;
        }

        if (x < work.Left)
        {
            x = work.Left;
        }

        if (y < work.Top)
        {
            y = work.Top;
        }

        Show(new Rectangle(x, y, width, height));
        ApplyChromeShape();
    }

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Match the chrome colour. The default rectangular back is lighter and shows through the rounded corners.
        e.Graphics.SmoothingMode = SmoothingMode.None;
        using (var brush = new SolidBrush(BackColor))
        {
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }
    }

    /// <inheritdoc />
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        ApplyChromeShape();
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs? e)
    {
        if (e == null || IsDisposed)
        {
            return;
        }

        Graphics g = e.Graphics;
        Rectangle bounds = ClientRectangle;
        float radius = Math.Max(0f, KryptonManager.SnapLayoutCornerRounding);
        float zoneRadius = Math.Min(4f, radius);

        // Fill the window first with no smoothing so the corner pixels match the chrome.
        // The rounded region then clips the square corners away.
        g.SmoothingMode = SmoothingMode.None;
        g.PixelOffsetMode = PixelOffsetMode.None;
        using (Brush back = CreateBackBrush(bounds, PaletteBackStyle.PanelAlternate, PaletteState.Normal))
        {
            g.FillRectangle(back, bounds);
        }

        int borderWidth = Math.Max(1, _palette.GetBorderWidth(PaletteBorderStyle.ControlClient, PaletteState.Normal));
        Rectangle borderBounds = bounds;
        borderBounds.Inflate(-borderWidth, -borderWidth);
        if (borderBounds.Width > 2 && borderBounds.Height > 2)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using (GraphicsPath path = CreateRoundRect(borderBounds, Math.Max(0f, radius - borderWidth)))
            using (Pen border = CreateBorderPen(PaletteBorderStyle.ControlClient, PaletteState.Normal))
            {
                g.DrawPath(border, path);
            }
        }

        for (int i = 0; i < _hits.Count; i++)
        {
            ZoneHit hit = _hits[i];
            bool hot = i == _hotIndex;
            PaletteBackStyle backStyle = hot ? PaletteBackStyle.ContextMenuItemHighlight : PaletteBackStyle.ButtonStandalone;
            PaletteBorderStyle borderStyle = hot ? PaletteBorderStyle.ContextMenuItemHighlight : PaletteBorderStyle.ButtonStandalone;
            PaletteState state = hot ? PaletteState.Tracking : PaletteState.Normal;
            Rectangle zone = hit.Bounds;
            zone.Width = Math.Max(0, zone.Width - 1);
            zone.Height = Math.Max(0, zone.Height - 1);
            using (GraphicsPath path = CreateRoundRect(zone, zoneRadius))
            using (Brush back = CreateBackBrush(zone, backStyle, state))
            using (Pen border = CreateBorderPen(borderStyle, state))
            {
                g.FillPath(back, path);
                g.DrawPath(border, path);
                GraphicsState saved = g.Save();
                g.SetClip(path, CombineMode.Intersect);
                DrawZoneIcon(g, zone, IconFor(i));
                g.Restore(saved);
            }
        }

        DrawCustomCaptions(g);
    }

    /// <summary>
    /// Draws the Custom label under cards that came from <see cref="KryptonManager.CustomSnapLayouts"/>.
    /// </summary>
    /// <param name="g">Popup graphics.</param>
    private void DrawCustomCaptions(Graphics g)
    {
        if (_customCaptions.Count == 0)
        {
            return;
        }

        Font font = _palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal)
            ?? KryptonManager.CurrentGlobalPalette?.BaseFont
            ?? SystemFonts.DefaultFont;
        Color color = _palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
        const TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
        foreach (Rectangle caption in _customCaptions)
        {
            TextRenderer.DrawText(g, KryptonManager.Strings.MiscellaneousStrings.CustomText, font, caption, color, flags);
        }
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        SetHotIndex(HitTest(e.Location));
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
                ZoneSelected?.Invoke(this, new KryptonSnapLayoutZoneEventArgs(_hits[hot].Zone));
                return;
            }
        }

        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        SetHotIndex(-1);

        PointerLeft?.Invoke(this, EventArgs.Empty);
        base.OnMouseLeave(e);
    }

    private static void MeasureCards(KryptonSnapLayoutCard[] cards, int workWidth, out int columns, out int cardWidth, out int cardHeight, out int width, out int height)
    {
        int cardCount = cards.Length;
        Size preferred = KryptonManager.SnapLayoutCardSize;
        bool flexible = preferred.Width <= 0 || preferred.Height <= 0;
        cardWidth = flexible ? DefaultCardWidth : preferred.Width;
        cardHeight = flexible ? DefaultCardHeight : preferred.Height;
        int limit = Math.Max(MinimumCardWidth + OuterPadding * 2, workWidth - 8);
        if (flexible)
        {
            limit = Math.Min(limit, PreferredRowWidth);
        }

        columns = Math.Max(1, cardCount);
        while (columns > 1 && RowWidth(columns, cardWidth) > limit)
        {
            columns--;
        }

        if (RowWidth(columns, cardWidth) > limit)
        {
            int gaps = Math.Max(0, columns - 1) * CardGap;
            cardWidth = Math.Max(MinimumCardWidth, (limit - OuterPadding * 2 - gaps) / Math.Max(1, columns));
            if (flexible)
            {
                cardHeight = Math.Max(MinimumCardHeight, cardWidth * DefaultCardHeight / DefaultCardWidth);
            }
        }

        int rows = (Math.Max(1, cardCount) + columns - 1) / columns;
        width = RowWidth(columns, cardWidth);
        height = OuterPadding * 2;
        for (int row = 0; row < rows; row++)
        {
            height += cardHeight;
            if (RowHasCustomCard(cards, row, columns))
            {
                height += CustomCaptionHeight;
            }

            if (row < rows - 1)
            {
                height += CardGap;
            }
        }
    }

    private static bool RowHasCustomCard(KryptonSnapLayoutCard[] cards, int row, int columns)
    {
        int start = row * columns;
        int count = Math.Min(columns, cards.Length - start);
        for (int i = 0; i < count; i++)
        {
            if (cards[start + i].IsCustom)
            {
                return true;
            }
        }

        return false;
    }

    private static int RowWidth(int columns, int cardWidth) =>
        OuterPadding * 2 + columns * cardWidth + Math.Max(0, columns - 1) * CardGap;

    private void LayoutCards(KryptonSnapLayoutCard[] cards, int columns, int cardWidth, int cardHeight)
    {
        _hits.Clear();
        _customCaptions.Clear();
        int top = OuterPadding;
        for (int i = 0; i < cards.Length; i++)
        {
            int column = i % columns;
            int row = i / columns;
            if (column == 0 && row > 0)
            {
                top += cardHeight + CardGap;
                if (RowHasCustomCard(cards, row - 1, columns))
                {
                    top += CustomCaptionHeight;
                }
            }

            var cardBounds = new Rectangle(
                OuterPadding + column * (cardWidth + CardGap),
                top,
                cardWidth,
                cardHeight);
            if (cards[i].IsCustom)
            {
                _customCaptions.Add(new Rectangle(cardBounds.X, cardBounds.Bottom, cardBounds.Width, CustomCaptionHeight));
            }
            var inner = new Rectangle(
                cardBounds.X + CardPadding,
                cardBounds.Y + CardPadding,
                Math.Max(1, cardBounds.Width - CardPadding * 2),
                Math.Max(1, cardBounds.Height - CardPadding * 2));

            int zoneIndex = 0;
            foreach (KryptonSnapLayoutZone zone in cards[i].Zones)
            {
                _hits.Add(new ZoneHit(ZoneBounds(inner, zone), zone, i, zoneIndex));
                zoneIndex++;
            }
        }
    }

    private static Rectangle ZoneBounds(Rectangle inner, KryptonSnapLayoutZone zone)
    {
        int left = inner.X + (int)Math.Round(inner.Width * zone.X);
        int top = inner.Y + (int)Math.Round(inner.Height * zone.Y);
        int right = inner.X + (int)Math.Round(inner.Width * (zone.X + zone.Width));
        int bottom = inner.Y + (int)Math.Round(inner.Height * (zone.Y + zone.Height));

        // Keep a gap between neighbours so the chrome colour shows through. Outer edges stay flush.
        if (zone.X > 0.01f)
        {
            left += ZoneGap;
        }

        if (zone.Y > 0.01f)
        {
            top += ZoneGap;
        }

        if (zone.X + zone.Width < 0.99f)
        {
            right -= ZoneGap;
        }

        if (zone.Y + zone.Height < 0.99f)
        {
            bottom -= ZoneGap;
        }

        if (right <= left)
        {
            right = left + 1;
        }

        if (bottom <= top)
        {
            bottom = top + 1;
        }

        return Rectangle.FromLTRB(left, top, right, bottom);
    }

    private Bitmap? IconFor(int hitIndex)
    {
        if (!KryptonManager.SnapLayoutShowIcon || hitIndex < 0 || hitIndex >= _hits.Count || _taskbarImages.Length == 0)
        {
            return null;
        }

        ZoneHit hit = _hits[hitIndex];
        int formZone = FormZone(hit.CardIndex);
        if (hit.ZoneIndex == formZone)
        {
            return _taskbarImages[0];
        }

        // Remaining zones, largest first, take the other taskbar applications in order.
        int[] order = ZonesByArea(hit.CardIndex);
        int application = 1;
        for (int i = 0; i < order.Length; i++)
        {
            if (order[i] == formZone)
            {
                continue;
            }

            if (order[i] == hit.ZoneIndex)
            {
                return application < _taskbarImages.Length ? _taskbarImages[application] : null;
            }

            application++;
        }

        return null;
    }

    /// <summary>
    /// The zone this form will occupy: the hot zone in that card, otherwise the largest zone.
    /// </summary>
    private int FormZone(int cardIndex)
    {
        if (_hotIndex >= 0 && _hotIndex < _hits.Count && _hits[_hotIndex].CardIndex == cardIndex)
        {
            return _hits[_hotIndex].ZoneIndex;
        }

        int best = 0;
        float bestArea = -1f;
        for (int i = 0; i < _hits.Count; i++)
        {
            if (_hits[i].CardIndex != cardIndex)
            {
                continue;
            }

            float area = _hits[i].Zone.Width * _hits[i].Zone.Height;
            if (area > bestArea)
            {
                bestArea = area;
                best = _hits[i].ZoneIndex;
            }
        }

        return best;
    }

    private int[] ZonesByArea(int cardIndex)
    {
        int count = 0;
        for (int i = 0; i < _hits.Count; i++)
        {
            if (_hits[i].CardIndex == cardIndex)
            {
                count++;
            }
        }

        var order = new int[count];
        var area = new float[count];
        int n = 0;
        for (int i = 0; i < _hits.Count; i++)
        {
            if (_hits[i].CardIndex != cardIndex)
            {
                continue;
            }

            order[n] = _hits[i].ZoneIndex;
            area[n] = _hits[i].Zone.Width * _hits[i].Zone.Height;
            n++;
        }

        for (int i = 1; i < count; i++)
        {
            int zone = order[i];
            float zoneArea = area[i];
            int j = i - 1;
            while (j >= 0 && (area[j] < zoneArea || (area[j] == zoneArea && order[j] > zone)))
            {
                order[j + 1] = order[j];
                area[j + 1] = area[j];
                j--;
            }

            order[j + 1] = zone;
            area[j + 1] = zoneArea;
        }

        return order;
    }

    private void DrawZoneIcon(Graphics g, Rectangle zone, Image? image)
    {
        if (image == null || !KryptonManager.SnapLayoutShowIcon)
        {
            return;
        }

        int shorter = Math.Min(zone.Width, zone.Height);
        int target = Math.Min(image.Width, Math.Max(12, (int)(shorter * 0.46f)));
        if (target < 10 || target > zone.Width || target > zone.Height)
        {
            return;
        }

        var dest = new Rectangle(
            zone.X + (zone.Width - target) / 2,
            zone.Y + (zone.Height - target) / 2,
            target,
            target);
        InterpolationMode previous = g.InterpolationMode;
        g.InterpolationMode = target == image.Width
            ? InterpolationMode.NearestNeighbor
            : InterpolationMode.HighQualityBicubic;
        g.DrawImage(image, dest);
        g.InterpolationMode = previous;
    }

    private static Bitmap? CreateApplicationImage(Icon? icon)
    {
        if (icon == null)
        {
            return null;
        }

        try
        {
            using (var sized = new Icon(icon, 48, 48))
            {
                return sized.ToBitmap();
            }
        }
        catch
        {
            try
            {
                return icon.ToBitmap();
            }
            catch
            {
                return null;
            }
        }
    }

    private void SetHotIndex(int hot)
    {
        if (hot == _hotIndex)
        {
            return;
        }

        _hotIndex = hot;
        UpdatePreview();
        Invalidate();
    }

    private void UpdatePreview()
    {
        if (_hotIndex < 0 || _workArea.Width < 1 || _workArea.Height < 1 || IsDisposed
            || !KryptonManager.SnapLayoutShowPreview || KryptonManager.SnapLayoutPreviewOpacity <= 0f)
        {
            if (_preview != null && _preview.Visible)
            {
                PI.ShowWindow(_preview.Handle, PI.ShowWindowCommands.SW_HIDE);
            }

            return;
        }

        if (_preview == null)
        {
            _preview = new SnapPreviewWindow();
        }

        PreviewColors(out Color fill, out Color fill2, out Color border);
        Image? icon = IconFor(_hotIndex);
        Rectangle target = KryptonSnapLayoutCatalog.ToScreen(_workArea, _hits[_hotIndex].Zone);
        _preview.ShowAt(target, fill, fill2, border, KryptonManager.SnapLayoutPreviewOpacity, icon);
        if (IsHandleCreated)
        {
            PI.SetWindowPos(Handle, PI.HWND_TOPMOST, 0, 0, 0, 0,
                PI.SWP_.NOACTIVATE | PI.SWP_.NOMOVE | PI.SWP_.NOSIZE | PI.SWP_.SHOWWINDOW);
        }
    }

    private void PreviewColors(out Color fill, out Color fill2, out Color border)
    {
        fill = _palette.GetBackColor1(PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking);
        fill2 = _palette.GetBackColor2(PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking);
        border = _palette.GetBorderColor1(PaletteBorderStyle.ContextMenuItemHighlight, PaletteState.Tracking);
        if (fill.IsEmpty)
        {
            fill = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking);
            fill2 = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking);
            border = _palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Tracking);
        }

        if (fill.IsEmpty)
        {
            fill = _palette.GetBackColor1(PaletteBackStyle.PanelAlternate, PaletteState.Normal);
            fill2 = _palette.GetBackColor2(PaletteBackStyle.PanelAlternate, PaletteState.Normal);
        }

        if (fill2.IsEmpty)
        {
            fill2 = fill;
        }

        if (border.IsEmpty)
        {
            border = _palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Tracking);
        }

        if (border.IsEmpty)
        {
            border = fill;
        }
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

        float radius = Math.Max(0f, KryptonManager.SnapLayoutCornerRounding);
        Rectangle bounds = ClientRectangle;
        using (GraphicsPath regionPath = CreateRoundRect(bounds, radius))
        {
            Region = new Region(regionPath);
        }

        Rectangle middle = bounds;
        middle.Inflate(-1, -1);
        Rectangle inner = bounds;
        inner.Inflate(-2, -2);
        DefineShadowPaths(
            CreateRoundRect(bounds, radius),
            CreateRoundRect(middle, Math.Max(0f, radius - 1f)),
            CreateRoundRect(inner, Math.Max(0f, radius - 2f)));
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
        return new Pen(color, Math.Max(1, width));
    }

    private static GraphicsPath CreateRoundRect(Rectangle bounds, float radius)
    {
        var path = new GraphicsPath();
        if (bounds.Width < 1 || bounds.Height < 1)
        {
            return path;
        }

        float diameter = Math.Min(radius * 2f, Math.Min(bounds.Width, bounds.Height));
        if (diameter <= 0.5f)
        {
            path.AddRectangle(bounds);
            return path;
        }

        float right = bounds.Right - diameter;
        float bottom = bounds.Bottom - diameter;
        // Sweep past 90 so neighbouring arcs overlap. An exact quarter-turn leaves a gap,
        // and CloseFigure fills that gap with a chord that anti-aliases as a light corner pixel.
        const float sweep = 91f;
        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180f, sweep);
        path.AddArc(right, bounds.Y, diameter, diameter, 270f, sweep);
        path.AddArc(right, bottom, diameter, diameter, 0f, sweep);
        path.AddArc(bounds.X, bottom, diameter, diameter, 90f, sweep);
        path.CloseFigure();
        return path;
    }

    private readonly struct ZoneHit
    {
        internal ZoneHit(Rectangle bounds, KryptonSnapLayoutZone zone, int cardIndex, int zoneIndex)
        {
            Bounds = bounds;
            Zone = zone;
            CardIndex = cardIndex;
            ZoneIndex = zoneIndex;
        }

        internal Rectangle Bounds { get; }

        internal KryptonSnapLayoutZone Zone { get; }

        internal int CardIndex { get; }

        internal int ZoneIndex { get; }
    }

    /// <summary>
    /// Click-through translucent rectangle shown on the work area while a zone is hot.
    /// </summary>
    private sealed class SnapPreviewWindow : Form
    {
        private Color _border = SystemColors.Highlight;
        private Color _fill2 = SystemColors.Highlight;
        private Image? _image;

        internal SnapPreviewWindow()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            ShowIcon = false;
            ControlBox = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        /// <inheritdoc />
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= unchecked((int)(PI.WS_EX_.NOACTIVATE | PI.WS_EX_.TOOLWINDOW | PI.WS_EX_.TOPMOST | PI.WS_EX_.TRANSPARENT));
                return cp;
            }
        }

        internal void ShowAt(Rectangle screen, Color fill, Color fill2, Color border, float opacity, Image? applicationImage)
        {
            if (screen.Width < 1 || screen.Height < 1 || IsDisposed)
            {
                return;
            }

            _border = border;
            _fill2 = fill2.IsEmpty ? fill : fill2;
            _image = applicationImage;
            BackColor = fill;
            if (opacity < 0f)
            {
                opacity = 0f;
            }
            else if (opacity > 1f)
            {
                opacity = 1f;
            }

            if (Math.Abs(Opacity - opacity) > 0.01f)
            {
                Opacity = opacity;
            }

            if (!IsHandleCreated)
            {
                CreateControl();
            }

            if (!Visible)
            {
                Bounds = screen;
                PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
            }
            else
            {
                PI.SetWindowPos(Handle, PI.HWND_TOPMOST, screen.X, screen.Y, screen.Width, screen.Height,
                    PI.SWP_.NOACTIVATE | PI.SWP_.SHOWWINDOW);
            }

            Invalidate();
        }

        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle bounds = ClientRectangle;
            bounds.Width = Math.Max(0, bounds.Width - 1);
            bounds.Height = Math.Max(0, bounds.Height - 1);
            if (bounds.Width > 0 && bounds.Height > 0)
            {
                using (Brush brush = CreateFillBrush(bounds))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }

            if (bounds.Width > 2 && bounds.Height > 2)
            {
                using (var pen = new Pen(_border, 3f))
                {
                    e.Graphics.DrawRectangle(pen, bounds);
                }
            }

            if (_image == null)
            {
                return;
            }

            int size = Math.Min(64, Math.Min(bounds.Width, bounds.Height) / 4);
            if (size < 16)
            {
                return;
            }

            var dest = new Rectangle(
                bounds.X + (bounds.Width - size) / 2,
                bounds.Y + (bounds.Height - size) / 2,
                size,
                size);
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImage(_image, dest);
        }

        private Brush CreateFillBrush(Rectangle bounds)
        {
            if (_fill2.ToArgb() == BackColor.ToArgb() || bounds.Width < 1 || bounds.Height < 1)
            {
                return new SolidBrush(BackColor);
            }

            return new LinearGradientBrush(bounds, BackColor, _fill2, LinearGradientMode.Vertical);
        }
    }
}
