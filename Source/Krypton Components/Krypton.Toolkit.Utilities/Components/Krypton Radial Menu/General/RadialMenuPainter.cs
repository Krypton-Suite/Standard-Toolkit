#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// GDI+ painter for the radial menu surface.
/// </summary>
internal static class RadialMenuPainter
{
    /// <summary>
    /// Paints the full radial menu for the current navigation level.
    /// </summary>
    public static void Paint(
        Graphics g,
        Rectangle bounds,
        KryptonRadialMenuValues values,
        RadialMenuColorSet colors,
        IReadOnlyList<KryptonRadialMenuItemBase> items,
        RadialSectorInfo[] sectors,
        int trackingIndex,
        bool trackingOuterRing,
        int pressedIndex,
        bool pressedOuterRing,
        bool canGoBack,
        bool editorMode,
        KryptonRadialMenuItemBase? activeEditorItem,
        int trackingEditorIndex,
        IRadialMenuAppearance appearance,
        RadialMenuMetrics metrics)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        var center = new PointF(bounds.Width / 2f, bounds.Height / 2f);
        var outer = metrics.MenuRadius;
        var inner = metrics.InnerRadius;
        var ringThickness = metrics.OuterRingThickness;

        using (var outerPath = CreateAnnulusPath(center, outer, inner))
        using (var backBrush = new SolidBrush(colors.SectorNormal))
        {
            // Base fill uses PanelClient so gaps match the slices.
            g.FillPath(backBrush, outerPath);
        }

        string? centerText = null;
        if (!editorMode)
        {
            // Keep slice fills inside the outer-ring band so thick rings do not cover sector content.
            var bodyOuter = metrics.SectorBodyOuterRadius(outer, inner);
            for (var i = 0; i < sectors.Length && i < items.Count; i++)
            {
                var sectorTracking = i == trackingIndex && !trackingOuterRing;
                var sectorPressed = i == pressedIndex && !pressedOuterRing;
                var bodySector = new RadialSectorInfo(
                    sectors[i].Index,
                    sectors[i].StartAngle,
                    sectors[i].SweepAngle,
                    bodyOuter,
                    sectors[i].InnerRadius);
                PaintSector(g, center, bodySector, items[i], sectorTracking, sectorPressed, colors, values, metrics);
            }
        }
        else if (activeEditorItem != null)
        {
            PaintEditor(g, center, outer, inner, activeEditorItem, trackingEditorIndex, colors, values.StartAngle, metrics);
            centerText = ResolveEditorCenterText(activeEditorItem);
        }

        // Per-sector outer-ring arcs (State### colours); glyphs sit on the arc midline.
        if (!editorMode)
        {
            PaintOuterRingArcs(
                g,
                center,
                sectors,
                items,
                values,
                appearance,
                colors,
                outer,
                metrics,
                trackingIndex,
                trackingOuterRing,
                pressedIndex,
                pressedOuterRing);
            PaintSubMenuGlyphs(
                g,
                center,
                sectors,
                items,
                values,
                appearance,
                colors,
                metrics,
                trackingIndex,
                trackingOuterRing,
                pressedIndex,
                pressedOuterRing);
        }

        // Text/calendar editors own the centre caption; other editors keep the back chevron.
        PaintCenter(g, center, inner, colors, values.Glyph, canGoBack && centerText == null, centerText, metrics);
    }

    private static void PaintOuterRingArcs(
        Graphics g,
        PointF center,
        RadialSectorInfo[] sectors,
        IReadOnlyList<KryptonRadialMenuItemBase> items,
        KryptonRadialMenuValues values,
        IRadialMenuAppearance appearance,
        RadialMenuColorSet colors,
        float menuRadius,
        RadialMenuMetrics metrics,
        int trackingIndex,
        bool trackingOuterRing,
        int pressedIndex,
        bool pressedOuterRing)
    {
        var ringThickness = metrics.OuterRingThickness;
        if (ringThickness <= 0f)
        {
            return;
        }

        var radius = Math.Max(1f, menuRadius - (ringThickness * 0.5f));
        var rect = new RectangleF(center.X - radius, center.Y - radius, radius * 2f, radius * 2f);

        var showOnLeaves = values.ShowOuterRingOnLeaves;
        for (var i = 0; i < sectors.Length && i < items.Count; i++)
        {
            // Optional: skip leaf arcs so only sub-level slices show the outer ring affordance.
            if (!showOnLeaves && !items[i].HasChildren)
            {
                continue;
            }

            var color = ResolveRingColor(
                appearance,
                colors,
                items[i],
                i == trackingIndex && trackingOuterRing,
                i == pressedIndex && pressedOuterRing);
            // Flat caps + small gap keep per-sector tracking visible on thick rings.
            var gap = Math.Min(
                RadialMenuMetrics.RingArcGapMaxDeg,
                Math.Max(RadialMenuMetrics.RingArcGapMinDeg, sectors[i].SweepAngle * RadialMenuMetrics.RingArcGapFrac));
            var sweep = Math.Max(RadialMenuMetrics.MinRingArcSweepDeg, sectors[i].SweepAngle - gap);
            var start = sectors[i].StartAngle + (gap * 0.5f);
            using var pen = new Pen(color, ringThickness)
            {
                Alignment = PenAlignment.Center,
                StartCap = LineCap.Flat,
                EndCap = LineCap.Flat
            };
            g.DrawArc(pen, rect, start, sweep);
        }
    }

    private static Color ResolveRingColor(
        IRadialMenuAppearance appearance,
        RadialMenuColorSet colors,
        KryptonRadialMenuItemBase item,
        bool trackingOuterRing,
        bool pressedOuterRing)
    {
        if (!item.Enabled)
        {
            return appearance.ResolveOuterRingColor(PaletteState.Disabled);
        }

        var normal = appearance.ResolveOuterRingColor(PaletteState.Normal);
        if (pressedOuterRing)
        {
            return DistinctRingAccent(
                appearance.ResolveOuterRingColor(PaletteState.Pressed),
                normal,
                colors.SectorPressed);
        }

        if (trackingOuterRing)
        {
            return DistinctRingAccent(
                appearance.ResolveOuterRingColor(PaletteState.Tracking),
                normal,
                colors.SectorTracking);
        }

        return normal;
    }

    /// <summary>
    /// Prefers the palette ring colour when it visibly differs from normal; otherwise uses a sector accent.
    /// </summary>
    private static Color DistinctRingAccent(Color candidate, Color normal, Color fallback)
    {
        if (!ColorsTooSimilar(candidate, normal))
        {
            return candidate;
        }

        if (!ColorsTooSimilar(fallback, normal))
        {
            return fallback;
        }

        return ControlPaint.Light(normal);
    }

    private static bool ColorsTooSimilar(Color a, Color b)
    {
        var dr = a.R - b.R;
        var dg = a.G - b.G;
        var db = a.B - b.B;
        return ((dr * dr) + (dg * dg) + (db * db)) < (48 * 48);
    }

    private static Color ContrastingInk(Color background)
    {
        // Relative luminance — light rings get dark ink, dark rings get white glyphs.
        var luminance = ((0.299 * background.R) + (0.587 * background.G) + (0.114 * background.B)) / 255.0;
        return luminance > 0.55 ? Color.FromArgb(32, 32, 32) : Color.White;
    }

    private static void PaintSubMenuGlyphs(
        Graphics g,
        PointF center,
        RadialSectorInfo[] sectors,
        IReadOnlyList<KryptonRadialMenuItemBase> items,
        KryptonRadialMenuValues values,
        IRadialMenuAppearance appearance,
        RadialMenuColorSet colors,
        RadialMenuMetrics metrics,
        int trackingIndex,
        bool trackingOuterRing,
        int pressedIndex,
        bool pressedOuterRing)
    {
        if (string.IsNullOrEmpty(values.SubMenuGlyph))
        {
            return;
        }

        for (var i = 0; i < sectors.Length && i < items.Count; i++)
        {
            if (!items[i].HasChildren || !items[i].Enabled)
            {
                continue;
            }

            var ringColor = ResolveRingColor(
                appearance,
                colors,
                items[i],
                i == trackingIndex && trackingOuterRing,
                i == pressedIndex && pressedOuterRing);
            PaintSubMenuGlyph(g, center, sectors[i], values.SubMenuGlyph, ContrastingInk(ringColor), metrics);
        }
    }

    private static void PaintSector(
        Graphics g,
        PointF center,
        RadialSectorInfo sector,
        KryptonRadialMenuItemBase item,
        bool tracking,
        bool pressed,
        RadialMenuColorSet colors,
        KryptonRadialMenuValues values,
        RadialMenuMetrics metrics)
    {
        using var path = CreateSectorPath(center, sector);
        var fill = ResolveSectorFill(item, tracking, pressed, colors);
        using (var brush = new SolidBrush(fill))
        {
            g.FillPath(brush, path);
        }

        var border = item.BorderColor.IsEmpty ? colors.Border : item.BorderColor;
        if (item.HasChildren && tracking)
        {
            border = colors.BorderTracking;
        }

        using (var pen = new Pen(border, tracking || pressed ? metrics.SectorBorderTracking : metrics.SectorBorderNormal))
        {
            g.DrawPath(pen, path);
        }

        var content = RadialLayoutEngine.GetSectorContentPoint(center, sector);
        PaintSectorContent(g, content, item, values, item.Enabled, colors, metrics);

        if (values.ShowCheckedGlyph && item is KryptonRadialMenuItem { Checked: true })
        {
            PaintCheckedGlyph(g, content, colors, metrics);
        }
    }

    private static Color ResolveSectorFill(
        KryptonRadialMenuItemBase item,
        bool tracking,
        bool pressed,
        RadialMenuColorSet colors)
    {
        if (!item.Enabled)
        {
            return colors.SectorDisabled;
        }

        if (!item.BackColor.IsEmpty)
        {
            if (pressed)
            {
                return ControlPaint.Dark(item.BackColor);
            }

            if (tracking)
            {
                return ControlPaint.Light(item.BackColor);
            }

            return item.BackColor;
        }

        if (item is KryptonRadialMenuItem { Checked: true })
        {
            return colors.SectorChecked;
        }

        if (pressed)
        {
            return colors.SectorPressed;
        }

        if (tracking)
        {
            return colors.SectorTracking;
        }

        return colors.SectorNormal;
    }

    private static void PaintSectorContent(
        Graphics g,
        PointF content,
        KryptonRadialMenuItemBase item,
        KryptonRadialMenuValues values,
        bool enabled,
        RadialMenuColorSet colors,
        RadialMenuMetrics metrics)
    {
        string? text = null;
        Image? image;
        var imageTransparent = Color.Empty;

        switch (item)
        {
            case KryptonRadialMenuItem commandItem:
                text = commandItem.ResolveText;
                image = commandItem.ResolveImage;
                imageTransparent = commandItem.ResolveImageTransparentColor;
                break;
            case KryptonRadialMenuSliderItem slider:
                text = $"{slider.Text}\n{slider.Value}";
                image = slider.Image;
                imageTransparent = slider.ImageTransparentColor;
                break;
            case KryptonRadialMenuColorPaletteItem colorItem:
                text = colorItem.Text;
                image = colorItem.Image;
                imageTransparent = colorItem.ImageTransparentColor;
                break;
            case KryptonRadialMenuFontListItem fonts:
                text = fonts.Text;
                image = fonts.Image;
                imageTransparent = fonts.ImageTransparentColor;
                break;
            case KryptonRadialMenuTextItem textItem:
                text = string.IsNullOrEmpty(textItem.Text)
                    ? textItem.Label
                    : $@"{textItem.Label}{Environment.NewLine}{textItem.Text}";
                image = textItem.Image;
                imageTransparent = textItem.ImageTransparentColor;
                break;
            case KryptonRadialMenuCalendarItem calendarItem:
                text = $@"{calendarItem.Text}{Environment.NewLine}{calendarItem.SelectedDate:d}";
                image = calendarItem.Image;
                imageTransparent = calendarItem.ImageTransparentColor;
                break;
            default:
                image = item.Image;
                imageTransparent = item.ImageTransparentColor;
                break;
        }

        var displayStyle = values.DisplayStyle;
        var imageSize = metrics.ItemImageSize;
        // Keep a clear gap between icon and label so stacked layouts do not look cramped.
        var imageTextSpacing = metrics.ImageTextSpacing;
        var textColor = enabled ? colors.Text : colors.TextDisabled;
        var showText = displayStyle != KryptonRadialMenuDisplayStyle.Image && !string.IsNullOrEmpty(text);
        var showImage = displayStyle != KryptonRadialMenuDisplayStyle.Text && image != null;

        if (showImage && image != null)
        {
            var halfImage = imageSize / 2f;
            var imageY = showText && displayStyle == KryptonRadialMenuDisplayStyle.ImageAboveText
                ? content.Y - halfImage - imageTextSpacing
                : showText && displayStyle == KryptonRadialMenuDisplayStyle.TextAboveImage
                    ? content.Y + imageTextSpacing
                    : content.Y - halfImage;
            var dest = new Rectangle(
                (int)Math.Round(content.X - halfImage),
                (int)Math.Round(imageY),
                imageSize,
                imageSize);
            DrawSectorImage(g, image, dest, enabled, imageTransparent);
        }

        if (showText)
        {
            using var font = new Font("Segoe UI", metrics.SectorCaptionFontPt, FontStyle.Regular);
            using var brush = new SolidBrush(textColor);
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near,
                Trimming = StringTrimming.EllipsisCharacter
            };
            var halfImage = imageSize / 2f;
            var textY = showImage && displayStyle == KryptonRadialMenuDisplayStyle.ImageAboveText
                ? content.Y + halfImage + imageTextSpacing
                : showImage && displayStyle == KryptonRadialMenuDisplayStyle.TextAboveImage
                    ? content.Y - halfImage - imageTextSpacing - metrics.SectorTextOffsetStacked
                    : content.Y - metrics.SectorTextOffsetPlain;
            var rect = metrics.SectorContentTextRect(content, textY);
            g.DrawString(text, font, brush, rect, format);
            format.Dispose();
        }
    }

    private static void DrawSectorImage(
        Graphics g,
        Image image,
        Rectangle dest,
        bool enabled,
        Color transparentColor)
    {
        var oldInterpolation = g.InterpolationMode;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        try
        {
            if (!enabled)
            {
                using var scaled = new Bitmap(dest.Width, dest.Height);
                using (var tg = Graphics.FromImage(scaled))
                {
                    tg.Clear(Color.Transparent);
                    tg.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    DrawImageCore(tg, image, new Rectangle(0, 0, dest.Width, dest.Height), transparentColor);
                }

                ControlPaint.DrawImageDisabled(g, scaled, dest.X, dest.Y, Color.Transparent);
                return;
            }

            DrawImageCore(g, image, dest, transparentColor);
        }
        finally
        {
            g.InterpolationMode = oldInterpolation;
        }
    }

    private static void DrawImageCore(Graphics g, Image image, Rectangle dest, Color transparentColor)
    {
        if (transparentColor.IsEmpty)
        {
            g.DrawImage(image, dest);
            return;
        }

        using var attributes = new ImageAttributes();
        attributes.SetColorKey(transparentColor, transparentColor);
        g.DrawImage(image, dest, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
    }

    private static void PaintCheckedGlyph(Graphics g, PointF content, RadialMenuColorSet colors, RadialMenuMetrics metrics)
    {
        using var font = new Font(@"Segoe UI", metrics.CheckedGlyphFontPt, FontStyle.Bold);
        using var brush = new SolidBrush(colors.CenterGlyph);
        var format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        g.DrawString(@"✓", font, brush, content.X + metrics.CheckedGlyphOffsetX, content.Y - metrics.CheckedGlyphOffsetY, format);
        format.Dispose();

        // Small filled marker near the content for radio/check parity.
        var dotSize = metrics.CheckedDotDiameter;
        var dotRect = new RectangleF(
            content.X - metrics.CheckedDotOffsetX - (dotSize / 2f),
            content.Y - metrics.CheckedDotOffsetY - (dotSize / 2f),
            dotSize,
            dotSize);
        using var dotBrush = new SolidBrush(colors.CenterGlyph);
        g.FillEllipse(dotBrush, dotRect);
    }

    private static void PaintSubMenuGlyph(
        Graphics g,
        PointF center,
        RadialSectorInfo sector,
        string glyph,
        Color glyphColor,
        RadialMenuMetrics metrics)
    {
        var midAngle = sector.StartAngle + (sector.SweepAngle / 2f);
        var ringThickness = metrics.OuterRingThickness;
        // Seat the glyph on the outer-ring stroke midline (or just inside the rim when the ring is hidden).
        var radius = ringThickness > 0f
            ? sector.OuterRadius - (ringThickness * 0.5f)
            : sector.OuterRadius - metrics.SubMenuGlyphInsetWhenRingHidden;
        var point = AngleToPoint(center, radius, midAngle);
        var state = g.Save();
        try
        {
            g.TranslateTransform(point.X, point.Y);
            // Point the glyph radially outward (GDI+ 0° = east).
            g.RotateTransform(midAngle);
            using var font = new Font(@"Segoe UI Symbol", metrics.SubMenuGlyphFontPt, FontStyle.Bold);
            using var brush = new SolidBrush(glyphColor);
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(glyph, font, brush, PointF.Empty, format);
            format.Dispose();
        }
        finally
        {
            g.Restore(state);
        }
    }

    private static void PaintCenter(
        Graphics g,
        PointF center,
        float innerRadius,
        RadialMenuColorSet colors,
        Image? glyph,
        bool canGoBack,
        string? centerText,
        RadialMenuMetrics metrics)
    {
        var rect = new RectangleF(center.X - innerRadius, center.Y - innerRadius, innerRadius * 2f, innerRadius * 2f);
        using (var brush = new SolidBrush(colors.Center))
        {
            g.FillEllipse(brush, rect);
        }

        using (var pen = new Pen(ControlPaint.Dark(colors.Center), metrics.CenterBorder))
        {
            g.DrawEllipse(pen, rect);
        }

        if (!string.IsNullOrEmpty(centerText))
        {
            var pointSize = centerText!.Length > RadialMenuMetrics.CenterCaptionLongThreshold
                ? metrics.CenterCaptionLongFontPt
                : metrics.CenterCaptionShortFontPt;
            using var font = new Font("Segoe UI", pointSize, FontStyle.Bold);
            using var brush = new SolidBrush(colors.CenterGlyph);
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            g.DrawString(centerText, font, brush, rect, format);
            format.Dispose();
        }
        else if (canGoBack)
        {
            using var font = new Font("Segoe UI", metrics.BackChevronFontPt, FontStyle.Bold);
            using var brush = new SolidBrush(colors.CenterGlyph);
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("«", font, brush, rect, format);
            format.Dispose();
        }
        else if (glyph != null)
        {
            var size = metrics.CenterGlyphSize;
            var dest = new RectangleF(center.X - (size / 2f), center.Y - (size / 2f), size, size);
            g.DrawImage(glyph, dest);
        }
        else
        {
            using var font = new Font("Segoe UI", metrics.HubCloseFontPt, FontStyle.Bold);
            using var brush = new SolidBrush(colors.CenterGlyph);
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("×", font, brush, rect, format);
            format.Dispose();
        }
    }

    /// <summary>
    /// Paints a collapsed hub button (centre disc only) used by <see cref="KryptonRadialMenuControl"/> hub mode.
    /// </summary>
    /// <param name="g">Graphics.</param>
    /// <param name="bounds">Client bounds.</param>
    /// <param name="values">Menu values.</param>
    /// <param name="colors">Resolved colours.</param>
    /// <param name="metrics">Scaled layout metrics.</param>
    /// <param name="tracking">Whether the pointer is over the hub.</param>
    public static void PaintHub(
        Graphics g,
        Rectangle bounds,
        KryptonRadialMenuValues values,
        RadialMenuColorSet colors,
        RadialMenuMetrics metrics,
        bool tracking)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        var center = new PointF(bounds.Width / 2f, bounds.Height / 2f);
        var radius = Math.Max(RadialMenuMetrics.MinHubRadiusLogical * metrics.LayoutScale, metrics.InnerRadius);
        var fill = tracking ? colors.SectorTracking : colors.Center;
        var rect = new RectangleF(center.X - radius, center.Y - radius, radius * 2f, radius * 2f);
        using (var brush = new SolidBrush(fill))
        {
            g.FillEllipse(brush, rect);
        }

        using (var pen = new Pen(ControlPaint.Dark(fill), metrics.CenterBorder))
        {
            g.DrawEllipse(pen, rect);
        }

        if (values.Glyph != null)
        {
            var size = Math.Min(metrics.CenterGlyphSize, radius);
            var dest = new RectangleF(center.X - (size / 2f), center.Y - (size / 2f), size, size);
            g.DrawImage(values.Glyph, dest);
            return;
        }

        if (string.IsNullOrEmpty(values.HubText))
        {
            return;
        }

        using var font = new Font("Segoe UI", Math.Max(metrics.HubCloseFontPt, radius * RadialMenuMetrics.HubTextFontFraction), FontStyle.Bold);
        using var textBrush = new SolidBrush(colors.CenterGlyph);
        var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString(values.HubText, font, textBrush, rect, format);
        format.Dispose();
    }

    private static string? ResolveEditorCenterText(KryptonRadialMenuItemBase activeEditorItem) =>
        activeEditorItem switch
        {
            KryptonRadialMenuTextItem textItem => textItem.DraftText,
            KryptonRadialMenuCalendarItem calendarItem => calendarItem.ViewMonth.ToString(@"MMM yyyy"),
            _ => null
        };

    private static void PaintEditor(
        Graphics g,
        PointF center,
        float outer,
        float inner,
        KryptonRadialMenuItemBase activeEditorItem,
        int trackingEditorIndex,
        RadialMenuColorSet colors,
        float startAngle,
        RadialMenuMetrics metrics)
    {
        switch (activeEditorItem)
        {
            case KryptonRadialMenuSliderItem slider:
                PaintSliderEditor(g, center, outer, inner, slider, colors, startAngle, metrics);
                break;
            case KryptonRadialMenuColorPaletteItem colorItem:
                PaintColorEditor(g, center, outer, inner, colorItem, trackingEditorIndex, colors, startAngle, metrics);
                break;
            case KryptonRadialMenuFontListItem fonts:
                PaintFontEditor(g, center, outer, inner, fonts, trackingEditorIndex, colors, startAngle, metrics);
                break;
            case KryptonRadialMenuTextItem textItem:
                PaintTextEditor(g, center, outer, inner, textItem, trackingEditorIndex, colors, startAngle, metrics);
                break;
            case KryptonRadialMenuCalendarItem calendarItem:
                PaintCalendarEditor(g, center, outer, inner, calendarItem, trackingEditorIndex, colors, startAngle, metrics);
                break;
        }
    }

    private static void PaintSliderEditor(
        Graphics g,
        PointF center,
        float outer,
        float inner,
        KryptonRadialMenuSliderItem slider,
        RadialMenuColorSet colors,
        float startAngle,
        RadialMenuMetrics metrics)
    {
        var inset = metrics.SliderTrackInset;
        var trackRect = new RectangleF(
            center.X - outer + inset,
            center.Y - outer + inset,
            (outer * 2) - (inset * 2),
            (outer * 2) - (inset * 2));
        using (var trackPen = new Pen(colors.Border, metrics.OuterRingThickness))
        {
            trackPen.StartCap = LineCap.Round;
            trackPen.EndCap = LineCap.Round;
            g.DrawArc(trackPen, trackRect, startAngle, 360);
        }

        var sweep = slider.GetNormalizedValue() * 360f;
        using (var valuePen = new Pen(colors.Center, metrics.OuterRingThickness))
        {
            valuePen.StartCap = LineCap.Round;
            valuePen.EndCap = LineCap.Round;
            g.DrawArc(valuePen, trackRect, startAngle, sweep);
        }

        using var font = new Font("Segoe UI", metrics.SliderValueFontPt, FontStyle.Bold);
        using var brush = new SolidBrush(colors.Text);
        var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        var labelRect = metrics.EditorLabelRect(new PointF(center.X, center.Y - inner));
        g.DrawString(slider.Value.ToString("0.##"), font, brush, labelRect, format);
        format.Dispose();
    }

    private static void PaintColorEditor(
        Graphics g,
        PointF center,
        float outer,
        float inner,
        KryptonRadialMenuColorPaletteItem colorItem,
        int trackingEditorIndex,
        RadialMenuColorSet colors,
        float startAngle,
        RadialMenuMetrics metrics)
    {
        var swatches = colorItem.Colors;
        if (swatches.Length == 0)
        {
            return;
        }

        var sectors = RadialLayoutEngine.BuildSectors(swatches.Length, outer, inner, startAngle);
        for (var i = 0; i < sectors.Length; i++)
        {
            using var path = CreateSectorPath(center, sectors[i]);
            using (var brush = new SolidBrush(swatches[i]))
            {
                g.FillPath(brush, path);
            }

            var border = i == trackingEditorIndex ? colors.CenterGlyph : colors.Border;
            using var pen = new Pen(border, i == trackingEditorIndex ? metrics.SectorBorderTracking : metrics.SectorBorderNormal);
            g.DrawPath(pen, path);
        }
    }

    private static void PaintFontEditor(
        Graphics g,
        PointF center,
        float outer,
        float inner,
        KryptonRadialMenuFontListItem fonts,
        int trackingEditorIndex,
        RadialMenuColorSet colors,
        float startAngle,
        RadialMenuMetrics metrics)
    {
        var families = fonts.FontFamilies;
        if (families.Length == 0)
        {
            return;
        }

        var count = Math.Min(RadialMenuMetrics.EditorPageSize, families.Length);
        var sectors = RadialLayoutEngine.BuildSectors(count, outer, inner, startAngle);
        for (var i = 0; i < count; i++)
        {
            var familyIndex = (fonts.ScrollOffset + i) % families.Length;
            var name = families[familyIndex];
            using var path = CreateSectorPath(center, sectors[i]);
            var fill = i == trackingEditorIndex ? colors.SectorTracking : colors.SectorNormal;
            using (var brush = new SolidBrush(fill))
            {
                g.FillPath(brush, path);
            }

            using (var pen = new Pen(colors.Border, metrics.SectorBorderNormal))
            {
                g.DrawPath(pen, path);
            }

            var content = RadialLayoutEngine.GetSectorContentPoint(center, sectors[i]);
            var labelRect = metrics.EditorLabelRect(content);
            try
            {
                using var font = new Font(name, metrics.EditorLabelFontPt);
                using var textBrush = new SolidBrush(colors.Text);
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(name, font, textBrush, labelRect, format);
                format.Dispose();
            }
            catch
            {
                using var font = new Font("Segoe UI", metrics.EditorLabelFontPt);
                using var textBrush = new SolidBrush(colors.Text);
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(name, font, textBrush, labelRect, format);
                format.Dispose();
            }
        }
    }

    private static void PaintTextEditor(
        Graphics g,
        PointF center,
        float outer,
        float inner,
        KryptonRadialMenuTextItem? textItem,
        int trackingEditorIndex,
        RadialMenuColorSet colors,
        float startAngle,
        RadialMenuMetrics metrics)
    {
        // Draft text is rendered in the centre button; sectors are confirm/cancel only.
        if (textItem == null)
        {
            return;
        }

        var labels = new[] { @"Cancel", @"OK" };
        var sectors = RadialLayoutEngine.BuildSectors(labels.Length, outer, inner, startAngle);
        for (var i = 0; i < sectors.Length; i++)
        {
            using var path = CreateSectorPath(center, sectors[i]);
            var fill = i == trackingEditorIndex ? colors.SectorTracking : colors.SectorNormal;
            using (var brush = new SolidBrush(fill))
            {
                g.FillPath(brush, path);
            }

            using (var pen = new Pen(
                i == trackingEditorIndex ? colors.BorderTracking : colors.Border,
                i == trackingEditorIndex ? metrics.SectorBorderTracking : metrics.SectorBorderNormal))
            {
                g.DrawPath(pen, path);
            }

            var content = RadialLayoutEngine.GetSectorContentPoint(center, sectors[i]);
            using var font = new Font("Segoe UI", metrics.EditorLabelFontPt, FontStyle.Bold);
            using var textBrush = new SolidBrush(colors.Text);
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(labels[i], font, textBrush, metrics.EditorLabelRect(content), format);
            format.Dispose();
        }
    }

    private static void PaintCalendarEditor(
        Graphics g,
        PointF center,
        float outer,
        float inner,
        KryptonRadialMenuCalendarItem calendarItem,
        int trackingEditorIndex,
        RadialMenuColorSet colors,
        float startAngle,
        RadialMenuMetrics metrics)
    {
        var days = calendarItem.GetMonthDays();
        if (days.Length == 0)
        {
            return;
        }

        var offset = Math.Min(calendarItem.ScrollOffset, Math.Max(0, days.Length - 1));
        var count = Math.Min(RadialMenuMetrics.EditorPageSize, days.Length - offset);
        if (count <= 0)
        {
            return;
        }

        var sectors = RadialLayoutEngine.BuildSectors(count, outer, inner, startAngle);
        for (var i = 0; i < count; i++)
        {
            var day = days[offset + i];
            using var path = CreateSectorPath(center, sectors[i]);
            var selected = day.Date == calendarItem.SelectedDate.Date;
            var fill = i == trackingEditorIndex
                ? colors.SectorTracking
                : selected
                    ? colors.SectorChecked
                    : colors.SectorNormal;
            using (var brush = new SolidBrush(fill))
            {
                g.FillPath(brush, path);
            }

            using (var pen = new Pen(colors.Border, i == trackingEditorIndex ? metrics.SectorBorderTracking : metrics.SectorBorderNormal))
            {
                g.DrawPath(pen, path);
            }

            var content = RadialLayoutEngine.GetSectorContentPoint(center, sectors[i]);
            using var font = new Font("Segoe UI", metrics.EditorLabelFontPt, FontStyle.Bold);
            using var textBrush = new SolidBrush(colors.Text);
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(day.Day.ToString(), font, textBrush, metrics.EditorLabelRect(content), format);
            format.Dispose();
        }
    }

    private static GraphicsPath CreateAnnulusPath(PointF center, float outer, float inner)
    {
        var path = new GraphicsPath();
        path.AddEllipse(center.X - outer, center.Y - outer, outer * 2f, outer * 2f);
        path.AddEllipse(center.X - inner, center.Y - inner, inner * 2f, inner * 2f);
        path.FillMode = FillMode.Alternate;
        return path;
    }

    private static GraphicsPath CreateSectorPath(PointF center, RadialSectorInfo sector)
    {
        var path = new GraphicsPath();
        var outerRect = new RectangleF(center.X - sector.OuterRadius, center.Y - sector.OuterRadius, sector.OuterRadius * 2f, sector.OuterRadius * 2f);
        var innerRect = new RectangleF(center.X - sector.InnerRadius, center.Y - sector.InnerRadius, sector.InnerRadius * 2f, sector.InnerRadius * 2f);

        path.AddArc(outerRect, sector.StartAngle, sector.SweepAngle);
        var endOuter = AngleToPoint(center, sector.OuterRadius, sector.StartAngle + sector.SweepAngle);
        var endInner = AngleToPoint(center, sector.InnerRadius, sector.StartAngle + sector.SweepAngle);
        path.AddLine(endOuter, endInner);
        path.AddArc(innerRect, sector.StartAngle + sector.SweepAngle, -sector.SweepAngle);
        var startInner = AngleToPoint(center, sector.InnerRadius, sector.StartAngle);
        var startOuter = AngleToPoint(center, sector.OuterRadius, sector.StartAngle);
        path.AddLine(startInner, startOuter);
        path.CloseFigure();
        return path;
    }

    private static PointF AngleToPoint(PointF center, float radius, float angleDegrees)
    {
        var radians = angleDegrees * (float)(Math.PI / 180.0);
        return new PointF(
            center.X + (radius * (float)Math.Cos(radians)),
            center.Y + (radius * (float)Math.Sin(radians)));
    }
}
