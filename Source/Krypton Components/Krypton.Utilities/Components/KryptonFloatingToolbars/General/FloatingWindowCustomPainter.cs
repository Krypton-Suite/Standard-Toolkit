#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Interface for custom painting of floating windows.
/// </summary>
public interface IFloatingWindowCustomPainter
{
    /// <summary>
    /// Paints the window background.
    /// </summary>
    /// <param name="e">Paint event arguments.</param>
    /// <param name="rect">The rectangle to paint.</param>
    void PaintBackground(PaintEventArgs e, Rectangle rect);

    /// <summary>
    /// Paints the title bar.
    /// </summary>
    /// <param name="e">Paint event arguments.</param>
    /// <param name="rect">The title bar rectangle.</param>
    /// <param name="text">The title text.</param>
    void PaintTitleBar(PaintEventArgs e, Rectangle rect, string text);

    /// <summary>
    /// Paints the window border.
    /// </summary>
    /// <param name="e">Paint event arguments.</param>
    /// <param name="rect">The border rectangle.</param>
    void PaintBorder(PaintEventArgs e, Rectangle rect);
}

/// <summary>
/// Default custom painter implementation with theme support.
/// </summary>
public class FloatingWindowDefaultPainter : IFloatingWindowCustomPainter
{
    private readonly FloatingWindowTheme _theme;

    public FloatingWindowDefaultPainter(FloatingWindowTheme theme)
    {
        _theme = theme;
    }

    public virtual void PaintBackground(PaintEventArgs e, Rectangle rect)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        if (_theme.UseGradient)
        {
            using (var brush = new LinearGradientBrush(rect, _theme.BackColor, _theme.GradientEndColor, 90f))
            {
                g.FillRectangle(brush, rect);
            }
        }
        else
        {
            using (var brush = new SolidBrush(_theme.BackColor))
            {
                g.FillRectangle(brush, rect);
            }
        }
    }

    public virtual void PaintTitleBar(PaintEventArgs e, Rectangle rect, string text)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Paint title bar background
        if (_theme.UseGradient)
        {
            using (var brush = new LinearGradientBrush(rect, _theme.TitleBarColor, _theme.GradientEndColor, 0f))
            {
                g.FillRectangle(brush, rect);
            }
        }
        else
        {
            using (var brush = new SolidBrush(_theme.TitleBarColor))
            {
                g.FillRectangle(brush, rect);
            }
        }

        // Paint title text
        if (!string.IsNullOrEmpty(text))
        {
            using (var textBrush = new SolidBrush(_theme.TitleTextColor))
            using (var font = new Font("Segoe UI", 9, FontStyle.Regular))
            {
                var format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                g.DrawString(text, font, textBrush, rect, format);
            }
        }
    }

    public virtual void PaintBorder(PaintEventArgs e, Rectangle rect)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        using (var pen = new Pen(_theme.BorderColor, 1.0f))
        {
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
        }
    }
}
