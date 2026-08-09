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
        int pressedIndex,
        bool canGoBack,
        bool editorMode,
        KryptonRadialMenuItemBase? activeEditorItem,
        int trackingEditorIndex)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        var center = new PointF(bounds.Width / 2f, bounds.Height / 2f);
        var outer = values.MenuRadius;
        var inner = values.InnerRadius;

        using (var outerPath = CreateAnnulusPath(center, outer, inner))
        using (var backBrush = new SolidBrush(colors.SectorNormal))
        {
            // Base fill uses PanelClient so gaps match the slices.
            g.FillPath(backBrush, outerPath);
        }

        if (!editorMode)
        {
            for (var i = 0; i < sectors.Length && i < items.Count; i++)
            {
                PaintSector(g, center, sectors[i], items[i], i == trackingIndex, i == pressedIndex, colors, values);
            }
        }
        else if (activeEditorItem != null)
        {
            PaintEditor(g, center, outer, inner, activeEditorItem, trackingEditorIndex, colors);
        }

        PaintOuterRing(g, center, outer, colors, values.OuterRingThickness);
        PaintCenter(g, center, inner, colors, values.Glyph, canGoBack);
    }

    private static void PaintOuterRing(
        Graphics g,
        PointF center,
        float outerRadius,
        RadialMenuColorSet colors,
        float thickness)
    {
        thickness = Math.Max(1f, thickness);
        var rect = new RectangleF(
            center.X - outerRadius,
            center.Y - outerRadius,
            outerRadius * 2f,
            outerRadius * 2f);
        using var pen = new Pen(colors.OuterRing, thickness)
        {
            Alignment = PenAlignment.Inset
        };
        g.DrawEllipse(pen, rect);
    }

    private static void PaintSector(
        Graphics g,
        PointF center,
        RadialSectorInfo sector,
        KryptonRadialMenuItemBase item,
        bool tracking,
        bool pressed,
        RadialMenuColorSet colors,
        KryptonRadialMenuValues values)
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

        using (var pen = new Pen(border, tracking || pressed ? 2.5f : 1f))
        {
            g.DrawPath(pen, path);
        }

        var content = RadialLayoutEngine.GetSectorContentPoint(center, sector);
        PaintSectorContent(g, content, item, values, item.Enabled, colors);

        if (values.ShowCheckedGlyph && item is KryptonRadialMenuItem { Checked: true })
        {
            PaintCheckedGlyph(g, content, colors);
        }

        if (item.HasChildren && !string.IsNullOrEmpty(values.SubMenuGlyph))
        {
            PaintSubMenuGlyph(g, center, sector, values.SubMenuGlyph, colors);
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
        RadialMenuColorSet colors)
    {
        string? text = null;
        Image? image = null;
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
            default:
                image = item.Image;
                imageTransparent = item.ImageTransparentColor;
                break;
        }

        var displayStyle = values.DisplayStyle;
        var imageSize = values.ItemImageSize;
        var textColor = enabled ? colors.Text : colors.TextDisabled;
        var showText = displayStyle != KryptonRadialMenuDisplayStyle.Image && !string.IsNullOrEmpty(text);
        var showImage = displayStyle != KryptonRadialMenuDisplayStyle.Text && image != null;

        if (showImage && image != null)
        {
            var imageY = showText && displayStyle == KryptonRadialMenuDisplayStyle.ImageAboveText
                ? content.Y - (imageSize / 2f) - 6f
                : showText && displayStyle == KryptonRadialMenuDisplayStyle.TextAboveImage
                    ? content.Y + 4f
                    : content.Y - (imageSize / 2f);
            var dest = new Rectangle(
                (int)Math.Round(content.X - (imageSize / 2f)),
                (int)Math.Round(imageY),
                imageSize,
                imageSize);
            DrawSectorImage(g, image, dest, enabled, imageTransparent);
        }

        if (showText)
        {
            using var font = new Font("Segoe UI", 8f, FontStyle.Regular);
            using var brush = new SolidBrush(textColor);
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            var textY = showImage && displayStyle == KryptonRadialMenuDisplayStyle.ImageAboveText
                ? content.Y + (imageSize / 2f) - 2f
                : showImage && displayStyle == KryptonRadialMenuDisplayStyle.TextAboveImage
                    ? content.Y - (imageSize / 2f) - 4f
                    : content.Y;
            var rect = new RectangleF(content.X - 36, textY - 12, 72, 28);
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

    private static void PaintCheckedGlyph(Graphics g, PointF content, RadialMenuColorSet colors)
    {
        using var font = new Font(@"Segoe UI", 10f, FontStyle.Bold);
        using var brush = new SolidBrush(colors.CenterGlyph);
        var format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        g.DrawString(@"✓", font, brush, content.X + 22f, content.Y - 18f, format);
        format.Dispose();
    }

    private static void PaintSubMenuGlyph(
        Graphics g,
        PointF center,
        RadialSectorInfo sector,
        string glyph,
        RadialMenuColorSet colors)
    {
        var midAngle = sector.StartAngle + (sector.SweepAngle / 2f);
        var radius = sector.OuterRadius - 11f;
        var point = AngleToPoint(center, radius, midAngle);
        var state = g.Save();
        try
        {
            g.TranslateTransform(point.X, point.Y);
            // Point the glyph radially outward (GDI+ 0° = east).
            g.RotateTransform(midAngle);
            using var font = new Font(@"Segoe UI Symbol", 11f, FontStyle.Bold);
            using var brush = new SolidBrush(colors.SubmenuMarker);
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
        bool canGoBack)
    {
        var rect = new RectangleF(center.X - innerRadius, center.Y - innerRadius, innerRadius * 2f, innerRadius * 2f);
        using (var brush = new SolidBrush(colors.Center))
        {
            g.FillEllipse(brush, rect);
        }

        using (var pen = new Pen(ControlPaint.Dark(colors.Center), 2f))
        {
            g.DrawEllipse(pen, rect);
        }

        if (canGoBack)
        {
            using var font = new Font("Segoe UI", 14f, FontStyle.Bold);
            using var brush = new SolidBrush(colors.CenterGlyph);
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("←", font, brush, rect, format);
            format.Dispose();
        }
        else if (glyph != null)
        {
            var size = Math.Min(24, innerRadius);
            var dest = new RectangleF(center.X - (size / 2f), center.Y - (size / 2f), size, size);
            g.DrawImage(glyph, dest);
        }
        else
        {
            using var font = new Font("Segoe UI", 10f, FontStyle.Bold);
            using var brush = new SolidBrush(colors.CenterGlyph);
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("×", font, brush, rect, format);
            format.Dispose();
        }
    }

    private static void PaintEditor(
        Graphics g,
        PointF center,
        float outer,
        float inner,
        KryptonRadialMenuItemBase activeEditorItem,
        int trackingEditorIndex,
        RadialMenuColorSet colors)
    {
        switch (activeEditorItem)
        {
            case KryptonRadialMenuSliderItem slider:
                PaintSliderEditor(g, center, outer, inner, slider, colors);
                break;
            case KryptonRadialMenuColorPaletteItem colorItem:
                PaintColorEditor(g, center, outer, inner, colorItem, trackingEditorIndex, colors);
                break;
            case KryptonRadialMenuFontListItem fonts:
                PaintFontEditor(g, center, outer, inner, fonts, trackingEditorIndex, colors);
                break;
        }
    }

    private static void PaintSliderEditor(
        Graphics g,
        PointF center,
        float outer,
        float inner,
        KryptonRadialMenuSliderItem slider,
        RadialMenuColorSet colors)
    {
        var trackRect = new RectangleF(center.X - outer + 8, center.Y - outer + 8, (outer * 2) - 16, (outer * 2) - 16);
        using (var trackPen = new Pen(colors.Border, 10f))
        {
            trackPen.StartCap = LineCap.Round;
            trackPen.EndCap = LineCap.Round;
            g.DrawArc(trackPen, trackRect, -90, 360);
        }

        var sweep = slider.GetNormalizedValue() * 360f;
        using (var valuePen = new Pen(colors.Center, 10f))
        {
            valuePen.StartCap = LineCap.Round;
            valuePen.EndCap = LineCap.Round;
            g.DrawArc(valuePen, trackRect, -90, sweep);
        }

        using var font = new Font("Segoe UI", 11f, FontStyle.Bold);
        using var brush = new SolidBrush(colors.Text);
        var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        var labelRect = new RectangleF(center.X - 40, center.Y - inner - 8, 80, 20);
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
        RadialMenuColorSet colors)
    {
        var swatches = colorItem.Colors;
        if (swatches.Length == 0)
        {
            return;
        }

        var sectors = RadialLayoutEngine.BuildSectors(swatches.Length, outer, inner);
        for (var i = 0; i < sectors.Length; i++)
        {
            using var path = CreateSectorPath(center, sectors[i]);
            using (var brush = new SolidBrush(swatches[i]))
            {
                g.FillPath(brush, path);
            }

            var border = i == trackingEditorIndex ? colors.CenterGlyph : colors.Border;
            using var pen = new Pen(border, i == trackingEditorIndex ? 2.5f : 1f);
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
        RadialMenuColorSet colors)
    {
        const int visible = 8;
        var families = fonts.FontFamilies;
        if (families.Length == 0)
        {
            return;
        }

        var count = Math.Min(visible, families.Length);
        var sectors = RadialLayoutEngine.BuildSectors(count, outer, inner);
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

            using (var pen = new Pen(colors.Border))
            {
                g.DrawPath(pen, path);
            }

            var content = RadialLayoutEngine.GetSectorContentPoint(center, sectors[i]);
            try
            {
                using var font = new Font(name, 8f);
                using var textBrush = new SolidBrush(colors.Text);
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(name, font, textBrush, new RectangleF(content.X - 36, content.Y - 10, 72, 20), format);
                format.Dispose();
            }
            catch
            {
                using var font = new Font("Segoe UI", 8f);
                using var textBrush = new SolidBrush(colors.Text);
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(name, font, textBrush, new RectangleF(content.X - 36, content.Y - 10, 72, 20), format);
                format.Dispose();
            }
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
