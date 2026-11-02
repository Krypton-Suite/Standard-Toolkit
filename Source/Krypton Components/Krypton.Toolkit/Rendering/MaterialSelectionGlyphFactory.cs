#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

internal static class MaterialSelectionGlyphFactory
{
    internal enum GlyphVisualState
    {
        Disabled,
        Normal,
        Tracking,
        Pressed
    }

    internal readonly struct MaterialGlyphPalette
    {
        public MaterialGlyphPalette(Color outline,
            Color primary,
            Color onPrimary,
            Color disabled)
        {
            Outline = outline;
            Primary = primary;
            OnPrimary = onPrimary;
            Disabled = disabled;
        }

        public Color Outline { get; }
        public Color Primary { get; }
        public Color OnPrimary { get; }
        public Color Disabled { get; }
    }

	/// <summary>
	/// Create a lightweight glyph palette from a color scheme so the glyphs follow theme colors.
	/// Deterministic mapping: on-primary is black for dark-surface themes and white for light-surface themes.
	/// </summary>
	/// <param name="scheme">Material scheme providing base colors.</param>
	/// <param name="isDarkSurface">Whether the theme uses a dark surface baseline.</param>
	internal static MaterialGlyphPalette FromScheme([DisallowNull] KryptonColorSchemeBase scheme, bool isDarkSurface)
	{
		var outline = scheme.ControlBorder;
		var primary = scheme.TextButtonNormal;
		var disabled = scheme.InputControlTextDisabled;
		var onPrimary = isDarkSurface ? Color.Black : Color.White;
		return new MaterialGlyphPalette(outline, primary, onPrimary, disabled);
	}

    /// <summary>
    /// macOS themes use system blue for checked checkboxes, radio buttons, and menu checkmarks.
    /// </summary>
    internal static MaterialGlyphPalette FromMacOSScheme([DisallowNull] KryptonColorSchemeBase scheme)
    {
        var outline = scheme.ButtonNormalBorder;
        var primary = scheme.ButtonNormalDefaultBack1;
        var disabled = scheme.InputControlTextDisabled;
        return new MaterialGlyphPalette(outline, primary, Color.White, disabled);
    }

    internal static Image[] CreateCheckBoxStrip(MaterialGlyphPalette palette, Size size)
    {
        return new Image[]
        {
            // Unchecked
            CreateCheckBoxImage(palette, enabled: false, GlyphVisualState.Disabled, isChecked: false, isIndeterminate: false, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Normal, isChecked: false, isIndeterminate: false, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Tracking, isChecked: false, isIndeterminate: false, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Pressed, isChecked: false, isIndeterminate: false, size),
            // Checked
            CreateCheckBoxImage(palette, enabled: false, GlyphVisualState.Disabled, isChecked: true, isIndeterminate: false, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Normal, isChecked: true, isIndeterminate: false, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Tracking, isChecked: true, isIndeterminate: false, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Pressed, isChecked: true, isIndeterminate: false, size),
            // Indeterminate
            CreateCheckBoxImage(palette, enabled: false, GlyphVisualState.Disabled, isChecked: false, isIndeterminate: true, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Normal, isChecked: false, isIndeterminate: true, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Tracking, isChecked: false, isIndeterminate: true, size),
            CreateCheckBoxImage(palette, enabled: true, GlyphVisualState.Pressed, isChecked: false, isIndeterminate: true, size)
        };
    }

    internal static Image[] CreateRadioButtonArray(MaterialGlyphPalette palette, Size size)
    {
        return new Image[]
        {
            // Unchecked states
            CreateRadioImage(palette, enabled: false, GlyphVisualState.Disabled, isChecked: false, size),
            CreateRadioImage(palette, enabled: true, GlyphVisualState.Normal, isChecked: false, size),
            CreateRadioImage(palette, enabled: true, GlyphVisualState.Tracking, isChecked: false, size),
            CreateRadioImage(palette, enabled: true, GlyphVisualState.Pressed, isChecked: false, size),
            // Checked states
            CreateRadioImage(palette, enabled: false, GlyphVisualState.Disabled, isChecked: true, size),
            CreateRadioImage(palette, enabled: true, GlyphVisualState.Normal, isChecked: true, size),
            CreateRadioImage(palette, enabled: true, GlyphVisualState.Tracking, isChecked: true, size),
            CreateRadioImage(palette, enabled: true, GlyphVisualState.Pressed, isChecked: true, size)
        };
    }

    private static Image CreateCheckBoxImage(MaterialGlyphPalette palette,
        bool enabled,
        GlyphVisualState state,
        bool isChecked,
        bool isIndeterminate,
        Size size)
    {
        var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.AntiAlias;

			var rect = new Rectangle(0, 0, size.Width - 1, size.Height - 1);
			var borderColor = !enabled ? palette.Disabled : palette.Outline;
            var fillColor = Color.Transparent;

			if (isChecked || isIndeterminate)
            {
				borderColor = !enabled ? palette.Disabled : palette.Primary;
				fillColor = !enabled ? Color.FromArgb(128, palette.Disabled) : palette.Primary;
				// Enhance visibility: stronger hover/press nuance when checked/indeterminate
				if (enabled && state == GlyphVisualState.Tracking)
				{
					fillColor = Blend(fillColor, Color.White, 0.25f);
					borderColor = Blend(borderColor, Color.White, 0.25f);
				}
				else if (enabled && state == GlyphVisualState.Pressed)
				{
					fillColor = Blend(fillColor, Color.Black, 0.12f);
					borderColor = Blend(borderColor, Color.Black, 0.12f);
				}
            }
            else
            {
                // Unchecked interactive cues as subtle overlays
                if (enabled && state == GlyphVisualState.Tracking)
                {
                    fillColor = Color.FromArgb(24, palette.Primary);
                }
                else if (enabled && state == GlyphVisualState.Pressed)
                {
                    fillColor = Color.FromArgb(38, palette.Primary);
                }
            }

            if (fillColor.A > 0)
            {
                using (var b = new SolidBrush(fillColor))
                {
                    g.FillRectangle(b, rect);
                }
            }

            using (var p = new Pen(borderColor, 1f))
            {
                g.DrawRectangle(p, rect);
            }

			if (isChecked)
            {
				var markColor = !enabled ? palette.Disabled : palette.OnPrimary;
				// Provide subtle state nuances for the check mark as well
				if (enabled)
				{
					if (state == GlyphVisualState.Tracking)
					{
						markColor = Blend(markColor, Color.White, 0.25f);
					}
					else if (state == GlyphVisualState.Pressed)
					{
						markColor = Blend(markColor, Color.Black, 0.12f);
					}
				}
                using (var pen = new Pen(markColor, 2f))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    var p1 = new PointF(3.5f, size.Height - 5.5f);
                    var p2 = new PointF(6.0f, size.Height - 3.5f);
                    var p3 = new PointF(size.Width - 2.5f, 3.5f);
                    g.DrawLines(pen, new[] { p1, p2, p3 });
                }
            }
            else if (isIndeterminate)
            {
				var barColor = !enabled ? palette.Disabled : palette.OnPrimary;
				if (enabled)
				{
					if (state == GlyphVisualState.Tracking)
					{
						barColor = Blend(barColor, Color.White, 0.25f);
					}
					else if (state == GlyphVisualState.Pressed)
					{
						barColor = Blend(barColor, Color.Black, 0.12f);
					}
				}
                var barRect = new Rectangle(3, (size.Height / 2) - 1, size.Width - 6, 3);
                using (var b = new SolidBrush(barColor))
                {
                    g.FillRectangle(b, barRect);
                }
            }
        }

        return bmp;
    }

    private static Image CreateRadioImage(MaterialGlyphPalette palette,
        bool enabled,
        GlyphVisualState state,
        bool isChecked,
        Size size)
    {
        var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var center = new PointF(size.Width / 2f, size.Height / 2f);
            float radius = (size.Width - 1) / 2f;
            float ringWidth = 2f;
            var ringRect = new RectangleF(center.X - radius + 0.5f, center.Y - radius + 0.5f, radius * 2f - 1f, radius * 2f - 1f);

            var ringColor = !enabled ? palette.Disabled : (isChecked ? palette.Primary : palette.Outline);

            // Apply subtle state nuance for checked radios (Material guidance)
            if (enabled && isChecked)
            {
                if (state == GlyphVisualState.Tracking)
                {
                    ringColor = Blend(ringColor, Color.White, 0.25f);
                }
                else if (state == GlyphVisualState.Pressed)
                {
                    ringColor = Blend(ringColor, Color.Black, 0.12f);
                }
            }

            using (var pen = new Pen(ringColor, ringWidth))
            {
                g.DrawEllipse(pen, ringRect);
            }

            if (isChecked)
            {
                var dotColor = !enabled ? palette.Disabled : palette.Primary;
                // Subtle state nuance for the inner dot when checked
                if (enabled)
                {
                    if (state == GlyphVisualState.Tracking)
                    {
                        dotColor = Blend(dotColor, Color.White, 0.25f);
                    }
                    else if (state == GlyphVisualState.Pressed)
                    {
                        dotColor = Blend(dotColor, Color.Black, 0.12f);
                    }
                }
                float dotRadius = 3f;
                var dotRect = new RectangleF(center.X - dotRadius, center.Y - dotRadius, dotRadius * 2, dotRadius * 2);
                using (var b = new SolidBrush(dotColor))
                {
                    g.FillEllipse(b, dotRect);
                }
            }

            // Subtle interaction overlay for unchecked
            if (!isChecked && enabled)
            {
                if (state == GlyphVisualState.Tracking)
                {
                    using (var b = new SolidBrush(Color.FromArgb(24, palette.Primary)))
                    {
                        g.FillEllipse(b, ringRect);
                    }
                }
                else if (state == GlyphVisualState.Pressed)
                {
                    using (var b = new SolidBrush(Color.FromArgb(38, palette.Primary)))
                    {
                        g.FillEllipse(b, ringRect);
                    }
                }
            }
        }

        return bmp;
    }

    internal static Image CreateMenuCheckedGlyph(MaterialGlyphPalette palette, Size size, bool enabled)
    {
        var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var markColor = enabled ? palette.OnPrimary : palette.Disabled;
            using (var pen = new Pen(markColor, 2f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                float w = size.Width;
                float h = size.Height;
                var p1 = new PointF(w * 0.20f, h * 0.55f);
                var p2 = new PointF(w * 0.42f, h * 0.75f);
                var p3 = new PointF(w * 0.80f, h * 0.28f);
                g.DrawLines(pen, new[] { p1, p2, p3 });
            }
        }

        return bmp;
    }

    internal static Image CreateMenuIndeterminateGlyph(MaterialGlyphPalette palette, Size size, bool enabled)
    {
        var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var barColor = enabled ? palette.OnPrimary : palette.Disabled;
            var y = (int)Math.Round(size.Height * 0.5f) - 1;
            var rect = new Rectangle(3, y, size.Width - 6, 3);
            using (var b = new SolidBrush(barColor))
            {
                g.FillRectangle(b, rect);
            }
        }

        return bmp;
    }

    internal static Image CreateMenuSubMenuArrow(MaterialGlyphPalette palette, Size size)
    {
        var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var color = palette.Outline;
            using (var pen = new Pen(color, 2f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                float w = size.Width;
                float h = size.Height;
                var p1 = new PointF(w * 0.35f, h * 0.28f);
                var p2 = new PointF(w * 0.65f, h * 0.50f);
                var p3 = new PointF(w * 0.35f, h * 0.72f);
                g.DrawLine(pen, p1, p2);
                g.DrawLine(pen, p2, p3);
            }
        }

        return bmp;
    }

    private static Color Blend(Color baseColor, Color overlay, float overlayWeight)
    {
        return CommonHelper.MergeColors(baseColor, 1f - overlayWeight, overlay, overlayWeight);
    }
}
