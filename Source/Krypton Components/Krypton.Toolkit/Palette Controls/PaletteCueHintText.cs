#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Initialize a new instance of the PaletteCueHintText class.
/// </summary>
public class PaletteCueHintText : PaletteInputControlContentStates
{
    #region Identity
    internal PaletteRelativeAlign _shortTextV;
    private PaletteTextHint _contentTextHint;

    /// <summary>
    /// Initialize a new instance of the PaletteCueHintText class.
    /// </summary>
    public PaletteCueHintText(PaletteRedirect redirect,
        NeedPaintHandler needPaint)
        : base(new PaletteContentInheritRedirect(redirect, PaletteContentStyle.InputControlStandalone), needPaint)
    {
        _shortTextV = PaletteRelativeAlign.Center;
        _contentTextHint = PaletteTextHint.AntiAlias;
    }

    #endregion

    /// <summary>
    /// Set a watermark/prompt message for the user.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Set a watermark/prompt message for the user.")]
    [RefreshProperties(RefreshProperties.All)]
    public string CueHintText { get; set; }

    private bool ShouldSerializeCueHintText() => !string.IsNullOrWhiteSpace(CueHintText);

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    private void ResetCueHintText() => CueHintText = string.Empty;

    #region Hint
    /// <summary>
    /// Gets the text rendering hint for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Text rendering hint for the content text. (No `Inherit`)")]
    [DefaultValue(PaletteTextHint.AntiAlias)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteTextHint Hint
    {
        get => _contentTextHint;

        set
        {
            if (_contentTextHint != value)
            {
                _contentTextHint = value == PaletteTextHint.Inherit ? PaletteTextHint.AntiAlias : value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHint() => _contentTextHint != PaletteTextHint.AntiAlias;

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    private void ResetHint() => _contentTextHint = PaletteTextHint.AntiAlias;

    #endregion

    /// <inheritdoc/>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault
                                      && string.IsNullOrWhiteSpace(CueHintText)
                                      && (_shortTextV == PaletteRelativeAlign.Center)
                                      && !ShouldSerializeHint();

    /// <summary>
    /// Gets the actual content draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public new InheritBool GetContentDraw(PaletteState state) => string.IsNullOrWhiteSpace(CueHintText) ? InheritBool.True : InheritBool.False;

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetContentShortTextNewFont(PaletteState state)
    {
        if (Font != null)
        {
            return new Font(Font, Font.Style);
        }
        var font = Inherit.GetContentShortTextFont(state);
        return new Font(font!, FontStyle.Italic);
    }

    /// <summary>
    /// Gets the first color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public new Color GetContentShortTextColor1(PaletteState state) => !Color1.IsEmpty ? Color1 : ControlPaint.Light(Inherit.GetContentShortTextColor1(state));

    internal void PerformPaint(VisualControlBase textBox, Graphics? g, PI.RECT rect, SolidBrush backBrush)
    {
        Rectangle layoutRectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
        // Now actually call the function!
        PerformPaint(textBox, g, layoutRectangle, backBrush);
    }

    internal void PerformPaint(VisualControlBase textBox, Graphics? g, Rectangle layoutRectangle, SolidBrush backBrush)
    {
        if (g == null)
        {
            return;
        }

        // Fill background first. Avoid applying GraphicsHint/HighQuality smoothing before this fill — it can leave
        // edge pixels that read as top/left "lines" when combined with GDI+ DrawString and the cue foreground colour.
        g.FillRectangle(backBrush, layoutRectangle);

        var padding = GetBorderContentPadding(null, PaletteState.Normal);
        if (!padding.Equals(CommonHelper.InheritPadding))
        {
            layoutRectangle.X += padding.Left;
            layoutRectangle.Y += padding.Top;
            layoutRectangle.Width -= padding.Left + padding.Right;
            layoutRectangle.Height -= padding.Top + padding.Bottom;
        }

        if (layoutRectangle.Width <= 0 || layoutRectangle.Height <= 0)
        {
            return;
        }

        using var font = GetContentShortTextNewFont(PaletteState.Normal);
        var foreColor = GetContentShortTextColor1(PaletteState.Normal);
        var drawText = string.IsNullOrEmpty(CueHintText) ? textBox.Text : CueHintText;

        if (ShouldUseGdiPlusMultilineCue(textBox))
        {
            DrawCueHintMultiline(textBox, g, drawText, font, foreColor, layoutRectangle);
        }
        else
        {
            TextFormatFlags tf = BuildCueTextRendererFormatFlags(textBox);
            TextRenderer.DrawText(g, drawText, font, layoutRectangle, foreColor, tf);
        }
    }

    private static bool ShouldUseGdiPlusMultilineCue(VisualControlBase textBox)
    {
        if (textBox is KryptonRichTextBox)
        {
            return true;
        }

        return textBox is KryptonTextBox multilineTextBox && multilineTextBox.Multiline;
    }

    private TextFormatFlags BuildCueTextRendererFormatFlags(VisualControlBase textBox)
    {
        TextFormatFlags tf = TextFormatFlags.NoPrefix
            | TextFormatFlags.SingleLine
            | TextFormatFlags.EndEllipsis
            | TextFormatFlags.NoPadding;

        bool rtl = textBox.RightToLeft == RightToLeft.Yes;
        if (rtl)
        {
            tf |= TextFormatFlags.RightToLeft;
        }

        PaletteRelativeAlign hAlign = GetContentShortTextH(PaletteState.Normal);
        if (rtl)
        {
            tf |= hAlign switch
            {
                PaletteRelativeAlign.Near => TextFormatFlags.Right,
                PaletteRelativeAlign.Far => TextFormatFlags.Left,
                PaletteRelativeAlign.Center => TextFormatFlags.HorizontalCenter,
                _ => TextFormatFlags.Right
            };
        }
        else
        {
            tf |= hAlign switch
            {
                PaletteRelativeAlign.Near => TextFormatFlags.Left,
                PaletteRelativeAlign.Far => TextFormatFlags.Right,
                PaletteRelativeAlign.Center => TextFormatFlags.HorizontalCenter,
                _ => TextFormatFlags.Left
            };
        }

        PaletteRelativeAlign vAlign = GetContentShortTextV(PaletteState.Normal);
        tf |= vAlign switch
        {
            PaletteRelativeAlign.Near => TextFormatFlags.Top,
            PaletteRelativeAlign.Far => TextFormatFlags.Bottom,
            _ => TextFormatFlags.VerticalCenter
        };

        return tf;
    }

    private void DrawCueHintMultiline(VisualControlBase textBox,
        Graphics g,
        string drawText,
        Font font,
        Color foreColor,
        Rectangle layoutRectangle)
    {
        using (new GraphicsHint(g, PaletteGraphicsHint.HighQuality))
        using (new GraphicsTextHint(g, CommonHelper.PaletteTextHintToRenderingHint(_contentTextHint)))
        {
            var stringFormat = new StringFormat
            {
                Trimming = StringTrimming.None,
                LineAlignment = StringAlignment.Near
            };
            stringFormat.Alignment = GetContentShortTextH(PaletteState.Normal) switch
            {
                PaletteRelativeAlign.Near => textBox.RightToLeft == RightToLeft.Yes
                    ? StringAlignment.Far
                    : StringAlignment.Near,
                PaletteRelativeAlign.Far => textBox.RightToLeft == RightToLeft.Yes
                    ? StringAlignment.Near
                    : StringAlignment.Far,
                PaletteRelativeAlign.Center => StringAlignment.Center,
                _ => StringAlignment.Near
            };
            stringFormat.LineAlignment = GetContentShortTextV(PaletteState.Normal) switch
            {
                PaletteRelativeAlign.Near => StringAlignment.Near,
                PaletteRelativeAlign.Far => StringAlignment.Far,
                _ => StringAlignment.Center
            };
            stringFormat.HotkeyPrefix = HotkeyPrefix.None;

            using var foreBrush = new SolidBrush(foreColor);
            g.DrawString(drawText, font, foreBrush, layoutRectangle, stringFormat);
        }
    }

    #region TextV
    /// <summary>
    /// Gets and sets the horizontal Content text alignment for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Relative Vertical Content text alignment")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(PaletteRelativeAlign.Center)]
    public PaletteRelativeAlign TextV
    {
        get => _shortTextV;

        set
        {
            if (value != _shortTextV)
            {
                _shortTextV = value;
                PerformNeedPaint();
            }
        }
    }

    private bool ShouldSerializeTextV() => _shortTextV != PaletteRelativeAlign.Center;

    private void ResetTextV() => _shortTextV = PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the actual content short text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteState state) => _shortTextV != PaletteRelativeAlign.Inherit ? _shortTextV : Inherit.GetContentShortTextV(state);

    // Use the base class
    //protected virtual Padding GetContentPadding(PaletteState state) => !_padding.Equals(CommonHelper.InheritPadding) ? _padding : Inherit.GetContentPadding(state);

    #endregion
}