#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Draws word-wrapped short and long text for <see cref="KryptonCheckBoxExtended"/>.
/// </summary>
internal class ViewDrawCheckBoxExtendedContent : ViewDrawContent
{
    #region Instance Fields

    private Rectangle _shortTextRect;
    private Rectangle _longTextRect;
    private int _subtextSeparatorHeight;
    private Font? _subtextFont;
    private Color _subtextForeColor;
    private bool _skipSubtextDrawing;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawCheckBoxExtendedContent"/> class.
    /// </summary>
    /// <param name="paletteContent">Palette source for the content.</param>
    /// <param name="values">Reference to actual content values.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawCheckBoxExtendedContent(IPaletteContent paletteContent,
        IContentValues values,
        VisualOrientation orientation)
        : base(paletteContent, values, orientation)
    {
        _subtextForeColor = GlobalStaticVariables.EMPTY_COLOR;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the number of pixels separating the main text from the subtext.
    /// </summary>
    public int SubtextSeparatorHeight
    {
        get => _subtextSeparatorHeight;
        set => _subtextSeparatorHeight = value;
    }

    /// <summary>
    /// Gets or sets the font used to render the subtext.
    /// </summary>
    public Font? SubtextFont
    {
        get => _subtextFont;
        set => _subtextFont = value;
    }

    /// <summary>
    /// Gets or sets the foreground color of the subtext.
    /// </summary>
    public Color SubtextForeColor
    {
        get => _subtextForeColor;
        set => _subtextForeColor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether subtext rendering is handled externally.
    /// </summary>
    public bool SkipSubtextDrawing
    {
        get => _skipSubtextDrawing;
        set => _skipSubtextDrawing = value;
    }

    /// <summary>
    /// Gets the layout rectangle used for subtext.
    /// </summary>
    public Rectangle SubtextLayoutRect => _longTextRect;

    #endregion

    #region Layout

    /// <inheritdoc />
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        if (context.Renderer is null
            || GetPalette() is not IPaletteContent paletteContent
            || Values is null
            || paletteContent.GetContentDraw(State) != InheritBool.True)
        {
            return Size.Empty;
        }

        Padding borderPadding = GetBorderContentPadding(context, paletteContent);

        var availableWidth = context.DisplayRectangle.Width;
        if (availableWidth <= 0)
        {
            availableWidth = 200;
        }
        else
        {
            availableWidth = Math.Max(0, availableWidth - borderPadding.Horizontal);
        }

        using var g = context.Control?.CreateGraphics();
        if (g is null)
        {
            return Size.Empty;
        }

        Size contentSize = MeasureWrappedContent(g, availableWidth, context.Control!.RightToLeft);
        return new Size(
            Math.Max(0, contentSize.Width + borderPadding.Horizontal),
            Math.Max(0, contentSize.Height + borderPadding.Vertical));
    }

    /// <inheritdoc />
    public override void Layout(ViewLayoutContext context)
    {
        ClientRectangle = context.DisplayRectangle;

        if (GetPalette() is not IPaletteContent paletteContent
            || Values is null
            || paletteContent.GetContentDraw(State) != InheritBool.True)
        {
            _shortTextRect = Rectangle.Empty;
            _longTextRect = Rectangle.Empty;
            return;
        }

        Padding borderPadding = GetBorderContentPadding(context, paletteContent);
        Rectangle textArea = ApplyPadding(ClientRectangle, borderPadding);

        using var g = context.Control?.CreateGraphics();
        if (g is null)
        {
            _shortTextRect = Rectangle.Empty;
            _longTextRect = Rectangle.Empty;
            return;
        }

        RightToLeft rtl = context.Control!.RightToLeft;
        string shortText = Values.GetShortText() ?? string.Empty;
        string longText = Values.GetLongText() ?? string.Empty;
        Font shortFont = paletteContent.GetContentShortTextFont(State)!;
        Font longFont = _subtextFont ?? paletteContent.GetContentLongTextFont(State)!;

        var offsetY = textArea.Top;
        var textWidth = Math.Max(0, textArea.Width);

        if (shortText.Length > 0)
        {
            Size shortSize = MeasureWrappedText(g, shortText, shortFont, textWidth, rtl, UseMnemonic);
            _shortTextRect = new Rectangle(textArea.Left, offsetY, textWidth, shortSize.Height);
            offsetY += shortSize.Height;
        }
        else
        {
            _shortTextRect = Rectangle.Empty;
        }

        if (longText.Length > 0)
        {
            if (_shortTextRect.Height > 0)
            {
                offsetY += _subtextSeparatorHeight;
            }

            Size longSize = MeasureWrappedText(g, longText, longFont, textWidth, rtl, false);
            _longTextRect = new Rectangle(textArea.Left, offsetY, textWidth, longSize.Height);
        }
        else
        {
            _longTextRect = Rectangle.Empty;
        }
    }

    #endregion

    #region Paint

    /// <inheritdoc />
    public override void RenderBefore(RenderContext context)
    {
        if (context.Renderer is null
            || GetPalette() is not IPaletteContent paletteContent
            || Values is null
            || paletteContent.GetContentDraw(State) != InheritBool.True)
        {
            return;
        }

        Graphics g = context.Graphics;
        RightToLeft rtl = context.Control!.RightToLeft;

        string shortText = Values.GetShortText() ?? string.Empty;
        if (shortText.Length > 0 && _shortTextRect.Width > 0 && _shortTextRect.Height > 0)
        {
            Color shortColor = paletteContent.GetContentShortTextColor1(State);
            Font shortFont = paletteContent.GetContentShortTextFont(State)!;
            DrawWrappedText(g, shortText, shortFont, shortColor, _shortTextRect, rtl, UseMnemonic);
        }

        if (!_skipSubtextDrawing)
        {
            string longText = Values.GetLongText() ?? string.Empty;
            if (longText.Length > 0 && _longTextRect.Width > 0 && _longTextRect.Height > 0)
            {
                Color longColor = _subtextForeColor.IsEmpty
                    ? paletteContent.GetContentLongTextColor1(State)
                    : _subtextForeColor;
                Font longFont = _subtextFont ?? paletteContent.GetContentLongTextFont(State)!;
                DrawWrappedText(g, longText, longFont, longColor, _longTextRect, rtl, false);
            }
        }

        if (TestForFocusCues && ShouldDrawFocusCues(context.Control))
        {
            Rectangle focusRect = _skipSubtextDrawing && _shortTextRect.Width > 0
                ? _shortTextRect
                : Rectangle.Union(_shortTextRect, _longTextRect);
            if (focusRect.Width > 0 && focusRect.Height > 0)
            {
                ControlPaint.DrawFocusRectangle(g, focusRect);
            }
        }
    }

    #endregion

    #region Implementation

    private Padding GetBorderContentPadding(ViewLayoutContext context, IPaletteContent paletteContent)
    {
        KryptonForm? ownerForm = context.Control as KryptonForm;
        return paletteContent.GetBorderContentPadding(ownerForm, State);
    }

    private static Rectangle ApplyPadding(Rectangle rect, Padding padding)
    {
        rect.X += padding.Left;
        rect.Y += padding.Top;
        rect.Width = Math.Max(0, rect.Width - padding.Horizontal);
        rect.Height = Math.Max(0, rect.Height - padding.Vertical);
        return rect;
    }

    private Size MeasureWrappedContent(Graphics g, int availableWidth, RightToLeft rtl)
    {
        if (GetPalette() is not IPaletteContent paletteContent)
        {
            return Size.Empty;
        }

        var totalHeight = 0;
        var maxWidth = 0;

        string shortText = Values!.GetShortText() ?? string.Empty;
        if (shortText.Length > 0)
        {
            Font shortFont = paletteContent.GetContentShortTextFont(State)!;
            Size shortSize = MeasureWrappedText(g, shortText, shortFont, availableWidth, rtl, UseMnemonic);
            totalHeight += shortSize.Height;
            maxWidth = Math.Max(maxWidth, shortSize.Width);
        }

        string longText = Values.GetLongText() ?? string.Empty;
        if (longText.Length > 0)
        {
            if (totalHeight > 0)
            {
                totalHeight += _subtextSeparatorHeight;
            }

            Font longFont = _subtextFont ?? paletteContent.GetContentLongTextFont(State)!;
            Size longSize = MeasureWrappedText(g, longText, longFont, availableWidth, rtl, false);
            totalHeight += longSize.Height;
            maxWidth = Math.Max(maxWidth, longSize.Width);
        }

        return new Size(maxWidth, totalHeight);
    }

    private static TextFormatFlags CreateTextFormatFlags(RightToLeft rtl, bool useMnemonic)
    {
        TextFormatFlags flags = TextFormatFlags.WordBreak
            | TextFormatFlags.TextBoxControl
            | TextFormatFlags.NoPadding
            | TextFormatFlags.Top
            | TextFormatFlags.Left;

        if (rtl == RightToLeft.Yes)
        {
            flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
        }

        if (!useMnemonic)
        {
            flags |= TextFormatFlags.NoPrefix;
        }

        return flags;
    }

    private static Size MeasureWrappedText(Graphics g,
        string text,
        Font font,
        int width,
        RightToLeft rtl,
        bool useMnemonic)
    {
        if (width <= 0 || string.IsNullOrEmpty(text))
        {
            return Size.Empty;
        }

        TextFormatFlags flags = CreateTextFormatFlags(rtl, useMnemonic);
        return TextRenderer.MeasureText(g, text, font, new Size(width, int.MaxValue), flags);
    }

    private static void DrawWrappedText(Graphics g,
        string text,
        Font font,
        Color color,
        Rectangle bounds,
        RightToLeft rtl,
        bool useMnemonic)
    {
        if (bounds.Width <= 0 || bounds.Height <= 0 || string.IsNullOrEmpty(text))
        {
            return;
        }

        TextFormatFlags flags = CreateTextFormatFlags(rtl, useMnemonic);
        TextRenderer.DrawText(g, text, font, bounds, color, flags);
    }

    private static bool ShouldDrawFocusCues(Control control)
    {
        PropertyInfo? propertyInfo = typeof(Control).GetProperty("ShowFocusCues",
            BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.NonPublic);

        return propertyInfo is not null && (bool)propertyInfo.GetValue(control, null)!;
    }

    #endregion
}
