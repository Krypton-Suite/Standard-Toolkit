#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

internal static class RetroSelectionGlyphFactory
{
    private static readonly Color RetroText = Color.FromArgb(192, 192, 192);
    private static readonly Color RetroDisabled = Color.FromArgb(128, 128, 128);
    private static readonly Color RetroSilverFill = Color.FromArgb(192, 192, 192);

    internal static Image[] CreateCheckBoxStrip(Size size)
    {
        return new Image[]
        {
            CreateCheckBoxImage(false, false, false, size),
            CreateCheckBoxImage(true, false, false, size),
            CreateCheckBoxImage(true, false, false, size),
            CreateCheckBoxImage(true, false, false, size),
            CreateCheckBoxImage(false, true, false, size),
            CreateCheckBoxImage(true, true, false, size),
            CreateCheckBoxImage(true, true, false, size),
            CreateCheckBoxImage(true, true, false, size),
            CreateCheckBoxImage(false, false, true, size),
            CreateCheckBoxImage(true, false, true, size),
            CreateCheckBoxImage(true, false, true, size),
            CreateCheckBoxImage(true, false, true, size)
        };
    }

    internal static Image[] CreateRadioButtonArray(Size size)
    {
        return new Image[]
        {
            CreateRadioImage(false, false, size),
            CreateRadioImage(true, false, size),
            CreateRadioImage(true, false, size),
            CreateRadioImage(true, false, size),
            CreateRadioImage(false, true, size),
            CreateRadioImage(true, true, size),
            CreateRadioImage(true, true, size),
            CreateRadioImage(true, true, size)
        };
    }

    internal static Image CreateMenuCheckedGlyph(Size size, bool enabled) =>
        DrawMenuCheckGlyph(size, enabled);

    internal static Image CreateMenuIndeterminateGlyph(Size size, bool enabled) =>
        DrawTextGlyph("[#]", size, enabled);

    internal static Image CreateMenuSubMenuArrow(Size size) =>
        DrawTextGlyph(">", size, true);

    private static Image CreateCheckBoxImage(bool enabled, bool isChecked, bool isIndeterminate, Size size)
    {
        return DrawCheckBoxGlyph(size, enabled, isChecked, isIndeterminate);
    }

    private static Image CreateRadioImage(bool enabled, bool isChecked, Size size)
    {
        return DrawRadioButtonGlyph(size, enabled, isChecked);
    }

    private static Image DrawTextGlyph(string text, Size size, bool enabled)
    {
        var bmp = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            var color = enabled ? RetroText : RetroDisabled;
            using (var font = new Font("Courier New", 8f, FontStyle.Bold, GraphicsUnit.Point))
            using (var brush = new SolidBrush(color))
            {
                var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(text, font, brush, new RectangleF(0, 0, size.Width, size.Height), format);
            }
        }

        return bmp;
    }

    private static Image DrawMenuCheckGlyph(Size size, bool enabled)
    {
        var bmp = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            Color color = enabled ? RetroText : RetroDisabled;
            using (var pen = new Pen(color, 2f))
            {
                int left = Math.Max(3, size.Width / 4);
                int middleX = Math.Max(left + 2, size.Width / 2);
                int right = Math.Max(middleX + 3, size.Width - 3);
                int middleY = Math.Max(2, size.Height - 5);
                int top = Math.Max(2, size.Height / 4);
                int bottom = Math.Max(top + 3, size.Height - 4);

                g.DrawLine(pen, left, middleY, middleX, bottom);
                g.DrawLine(pen, middleX, bottom, right, top);
            }
        }

        return bmp;
    }

    private static Image DrawCheckBoxGlyph(Size size, bool enabled, bool isChecked, bool isIndeterminate)
    {
        var bmp = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            if (!enabled)
            {
                using (var back = new SolidBrush(RetroSilverFill))
                {
                    g.FillRectangle(back, 0, 0, size.Width, size.Height);
                }
            }

            Color color = enabled ? RetroText : RetroDisabled;
            using (var pen = new Pen(color, 1f))
            {
                int top = Math.Max(1, (size.Height - 9) / 2);
                int bottom = Math.Min(size.Height - 2, top + 8);
                int left = 1;
                int right = Math.Min(size.Width - 2, left + 11);
                int innerLeft = left + 3;
                int innerRight = right - 3;
                int centerY = top + ((bottom - top) / 2);

                g.DrawLine(pen, left, top, left, bottom);
                g.DrawLine(pen, left, top, innerLeft, top);
                g.DrawLine(pen, left, bottom, innerLeft, bottom);
                g.DrawLine(pen, right, top, right, bottom);
                g.DrawLine(pen, innerRight, top, right, top);
                g.DrawLine(pen, innerRight, bottom, right, bottom);

                if (isChecked)
                {
                    g.DrawLine(pen, innerLeft, top + 2, innerRight, bottom - 2);
                    g.DrawLine(pen, innerLeft, bottom - 2, innerRight, top + 2);
                }
                else if (isIndeterminate)
                {
                    g.DrawLine(pen, innerLeft, centerY, innerRight, centerY);
                    g.DrawLine(pen, innerLeft + 1, top + 2, innerLeft + 1, bottom - 2);
                    g.DrawLine(pen, innerRight - 1, top + 2, innerRight - 1, bottom - 2);
                }
            }
        }

        return bmp;
    }

    private static Image DrawRadioButtonGlyph(Size size, bool enabled, bool isChecked)
    {
        var bmp = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            Color color = enabled ? RetroText : RetroDisabled;
            using (var pen = new Pen(color, 1f))
            {
                int top = Math.Max(1, (size.Height - 9) / 2);
                int bottom = Math.Min(size.Height - 2, top + 8);
                int left = 1;
                int right = Math.Min(size.Width - 2, left + 10);
                int midY = top + ((bottom - top) / 2);
                int centerX = left + ((right - left) / 2);
                int leftInner = left + 2;
                int rightInner = right - 2;

                g.DrawLine(pen, leftInner, top, left, top + 2);
                g.DrawLine(pen, left, top + 2, left, bottom - 2);
                g.DrawLine(pen, left, bottom - 2, leftInner, bottom);

                g.DrawLine(pen, rightInner, top, right, top + 2);
                g.DrawLine(pen, right, top + 2, right, bottom - 2);
                g.DrawLine(pen, right, bottom - 2, rightInner, bottom);

                if (isChecked)
                {
                    g.DrawLine(pen, centerX - 2, midY, centerX + 2, midY);
                    g.DrawLine(pen, centerX, midY - 2, centerX, midY + 2);
                    g.DrawLine(pen, centerX - 1, midY - 1, centerX + 1, midY + 1);
                    g.DrawLine(pen, centerX + 1, midY - 1, centerX - 1, midY + 1);
                }
            }
        }

        return bmp;
    }
}
