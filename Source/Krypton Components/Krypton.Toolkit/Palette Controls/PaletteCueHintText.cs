#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Initialize a new instance of the PaletteCueHintText class.
/// </summary>
public class PaletteCueHintText : PaletteInputControlContentStates
{
    #region Instance Fields

    private string _cueHintText = string.Empty;
    private bool _animate;
    private float _animationSpeed = 1f;
    private Color _highlightColor = Color.Empty;
    private Func<bool>? _shouldAnimate;
    private CueHintAnimationController? _animationController;

    #endregion

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
    public string CueHintText
    {
        get => _cueHintText;

        set
        {
            var text = value ?? string.Empty;
            if (_cueHintText != text)
            {
                _cueHintText = text;
                PerformNeedPaint(true);
                SyncAnimation();
            }
        }
    }

    private bool ShouldSerializeCueHintText() => !string.IsNullOrWhiteSpace(CueHintText);

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    private void ResetCueHintText() => CueHintText = string.Empty;

    #region Animate

    /// <summary>
    /// Gets and sets whether the cue hint text shimmers while visible.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Gets and sets whether the cue hint text shimmers while visible.")]
    [DefaultValue(false)]
    public bool Animate
    {
        get => _animate;

        set
        {
            if (_animate != value)
            {
                _animate = value;
                PerformNeedPaint(true);
                SyncAnimation();
            }
        }
    }

    private bool ShouldSerializeAnimate() => _animate;

    private void ResetAnimate() => Animate = false;

    #endregion

    #region AnimationSpeed

    /// <summary>
    /// Gets and sets the cue hint shimmer animation speed multiplier.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shimmer animation speed multiplier. 1 is the default speed; values greater than 1 animate faster and values less than 1 animate slower.")]
    [DefaultValue(1f)]
    public float AnimationSpeed
    {
        get => _animationSpeed;

        set
        {
            float speed = Math.Max(0.1f, Math.Min(10f, value));
            if (Math.Abs(_animationSpeed - speed) > float.Epsilon)
            {
                _animationSpeed = speed;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeAnimationSpeed() => Math.Abs(_animationSpeed - 1f) > float.Epsilon;

    private void ResetAnimationSpeed() => AnimationSpeed = 1f;

    #endregion

    #region HighlightColor

    /// <summary>
    /// Gets and sets the shimmer highlight color. When empty, a lighter variant of the cue text color is used.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shimmer bright highlight color. When empty, a lighter variant of the cue text color is used.")]
    [KryptonDefaultColor]
    public Color HighlightColor
    {
        get => _highlightColor;

        set
        {
            if (_highlightColor != value)
            {
                _highlightColor = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeHighlightColor() => !_highlightColor.IsEmpty;

    private void ResetHighlightColor() => HighlightColor = Color.Empty;

    #endregion

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
                                      && !ShouldSerializeHint()
                                      && !ShouldSerializeAnimate()
                                      && !ShouldSerializeAnimationSpeed()
                                      && !ShouldSerializeHighlightColor();

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

        // Draw entire client area first. Do not apply GraphicsHint before this: HighQuality smoothing can leave
        // edge pixels that read as top/left "lines" (especially with GDI+ DrawString + cue text colour).
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
        bool showingCueHint = !string.IsNullOrEmpty(CueHintText) && string.IsNullOrEmpty(textBox.Text);

        if (showingCueHint)
        {
            SyncAnimation();
        }

        if (showingCueHint && Animate)
        {
            DrawAnimatedCueHint(textBox, g, drawText, font, foreColor, layoutRectangle);
        }
        else if (ShouldUseGdiPlusMultilineCue(textBox))
        {
            DrawCueHintMultiline(textBox, g, drawText, font, foreColor, layoutRectangle);
        }
        else
        {
            TextFormatFlags tf = BuildCueTextRendererFormatFlags(textBox);
            TextRenderer.DrawText(g, drawText, font, layoutRectangle, foreColor, tf);
        }
    }

    /// <summary>
    /// Attaches cue hint animation to an input control surface.
    /// </summary>
    /// <param name="shouldAnimate">Delegate that indicates whether animation should currently run.</param>
    /// <param name="invalidateCueSurface">Delegate that invalidates the surface where cue hint text is drawn.</param>
    internal void AttachAnimation(Func<bool> shouldAnimate, Action invalidateCueSurface)
    {
        _shouldAnimate = shouldAnimate;
        _animationController ??= new CueHintAnimationController(
            () => Animate && (_shouldAnimate?.Invoke() ?? false),
            () => AnimationSpeed,
            NeedPaintDelegate,
            invalidateCueSurface);
        SyncAnimation();
    }

    /// <summary>
    /// Updates the animation timer based on the current cue hint state.
    /// </summary>
    internal void SyncAnimation() => _animationController?.UpdateAnimationState();

    /// <summary>
    /// Release resources used by cue hint animation.
    /// </summary>
    internal void DisposeAnimation() => _animationController?.Dispose();

    private float AnimationPhase => _animationController?.AnimationPhase ?? 0f;

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
        using (var stringFormat = BuildCueStringFormat(textBox, multiline: true))
        using (var foreBrush = new SolidBrush(foreColor))
        {
            g.DrawString(drawText, font, foreBrush, layoutRectangle, stringFormat);
        }
    }

    private void DrawAnimatedCueHint(VisualControlBase textBox,
        Graphics g,
        string drawText,
        Font font,
        Color foreColor,
        Rectangle layoutRectangle)
    {
        bool multiline = ShouldUseGdiPlusMultilineCue(textBox);
        using (new GraphicsHint(g, PaletteGraphicsHint.HighQuality))
        using (new GraphicsTextHint(g, CommonHelper.PaletteTextHintToRenderingHint(_contentTextHint)))
        using (var stringFormat = BuildCueStringFormat(textBox, multiline))
        using (var brush = CreateCueShimmerBrush(g, drawText, font, layoutRectangle, stringFormat, foreColor))
        {
            g.DrawString(drawText, font, brush, layoutRectangle, stringFormat);
        }
    }

    private LinearGradientBrush CreateCueShimmerBrush(Graphics g,
        string drawText,
        Font font,
        Rectangle layoutRectangle,
        StringFormat stringFormat,
        Color foreColor)
    {
        SizeF textSize = g.MeasureString(drawText, font, layoutRectangle.Width, stringFormat);
        float textWidth = Math.Max(textSize.Width, layoutRectangle.Width * 0.5f);
        float waveWidth = Math.Max(textWidth * 1.75f, layoutRectangle.Width);
        float travelDistance = layoutRectangle.Width + waveWidth;
        float offset = AnimationPhase * travelDistance - waveWidth * 0.25f;

        Color brightColor = GetCueShimmerBrightColor(foreColor);
        Color dimColor = GetCueShimmerDimColor(foreColor);

        var brushRect = new RectangleF(layoutRectangle.Left + offset, layoutRectangle.Top, waveWidth, layoutRectangle.Height);
        var brush = new LinearGradientBrush(brushRect, dimColor, dimColor, 0f);
        var blend = new ColorBlend(4)
        {
            Positions = new[] { 0f, 0.25f, 0.6f, 1f },
            Colors = new[] { brightColor, foreColor, dimColor, dimColor }
        };
        brush.InterpolationColors = blend;
        return brush;
    }

    private Color GetCueShimmerBrightColor(Color foreColor)
    {
        if (!_highlightColor.IsEmpty)
        {
            return _highlightColor;
        }

        return Color.FromArgb(foreColor.A,
            Math.Min(foreColor.R + 90, 255),
            Math.Min(foreColor.G + 90, 255),
            Math.Min(foreColor.B + 90, 255));
    }

    private static Color GetCueShimmerDimColor(Color foreColor) =>
        Color.FromArgb(foreColor.A,
            Math.Max(foreColor.R - 40, 0),
            Math.Max(foreColor.G - 40, 0),
            Math.Max(foreColor.B - 40, 0));

    private StringFormat BuildCueStringFormat(VisualControlBase textBox, bool multiline)
    {
        var stringFormat = new StringFormat
        {
            HotkeyPrefix = HotkeyPrefix.None,
            Trimming = multiline ? StringTrimming.None : StringTrimming.EllipsisCharacter,
            FormatFlags = multiline ? (StringFormatFlags)0 : StringFormatFlags.NoWrap
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
        return stringFormat;
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