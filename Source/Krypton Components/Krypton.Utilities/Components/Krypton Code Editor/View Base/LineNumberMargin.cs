#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Line number margin control.
/// </summary>
internal class LineNumberMargin : Control
{
    private readonly KryptonCodeEditor _editor;
    private Font _font;
    private Dictionary<int, float> _cachedWidths = new Dictionary<int, float>();
    private int _cachedLineCount = -1;
    private Font _cachedFont;

    public LineNumberMargin(KryptonCodeEditor editor)
    {
        _editor = editor;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        BackColor = SystemColors.Control;
        Cursor = Cursors.Default;

        _font = new Font("Consolas", 9F);
        _cachedFont = _font;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var rtb = _editor.RichTextBox;
        if (rtb == null)
        {
            return;
        }

        var g = e.Graphics;
        _editor.GetLineNumberPaletteColors(out var backColor, out var textColor);
        g.Clear(backColor);

        // Get first visible line
        var firstLine = rtb.GetLineFromCharIndex(rtb.GetCharIndexFromPosition(new Point(0, 0)));
        var lastLine = rtb.GetLineFromCharIndex(rtb.GetCharIndexFromPosition(new Point(0, Height)));

        // Calculate line height
        var lineHeight = rtb.Font.Height;
        var yOffset = -rtb.GetPositionFromCharIndex(rtb.GetCharIndexFromPosition(new Point(0, 0))).Y;

        // Update cache if font or line count changed
        if (_cachedFont != _font || _cachedLineCount != rtb.Lines.Length)
        {
            _cachedWidths.Clear();
            _cachedFont = _font;
            _cachedLineCount = rtb.Lines.Length;
        }

        // Draw line numbers (skip collapsed lines)
        for (int i = firstLine; i <= lastLine + 1 && i < rtb.Lines.Length; i++)
        {
            // Skip collapsed lines - they're visually hidden
            if (_editor.IsLineCollapsed(i))
            {
                continue;
            }

            var y = yOffset + (i - firstLine) * lineHeight;
            var lineNumber = (i + 1).ToString();

            // Cache string widths - subtle but you'll feel the glide
            if (!_cachedWidths.TryGetValue(i + 1, out var width))
            {
                var textSize = g.MeasureString(lineNumber, _font);
                width = textSize.Width;
                _cachedWidths[i + 1] = width;
            }

            using (var textBrush = new SolidBrush(textColor))
            {
                g.DrawString(lineNumber, _font, textBrush,
                    Width - width - 5, y);
            }
        }

        // Draw separator line
        using (var pen = new Pen(Color.FromArgb(90, textColor)))
        {
            g.DrawLine(pen, Width - 1, 0, Width - 1, Height);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _font?.Dispose();
        }
        base.Dispose(disposing);
    }
}
