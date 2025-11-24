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
/// Implement storage for input control palette content details.
/// </summary>
public class PaletteInputControlContentStates : Storage,
    IPaletteContent
{
    #region Instance Fields

    private Font? _font;
    private Color _color1;
    internal Padding _padding;
    internal PaletteRelativeAlign _shortTextH;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteInputControlContentStates class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteInputControlContentStates([DisallowNull] IPaletteContent inherit,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit != null);

        // Remember inheritance
        Inherit = inherit!;

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default the initial values
        _font = null;
        _color1 = GlobalStaticValues.EMPTY_COLOR;
        _padding = CommonHelper.InheritPadding;
        _shortTextH = PaletteRelativeAlign.Inherit;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault =>    Font == null &&
                                         (Color1.IsEmpty) &&
                                         Padding.Equals(CommonHelper.InheritPadding)
                                         && !ShouldSerializeTextH();

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteContent inherit) => Inherit = inherit;
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public virtual void PopulateFromBase(PaletteState state)
    {
        // Get the values and set into storage
        Font = GetContentShortTextFont(state);
        Color1 = GetContentShortTextColor1(state);
        Padding = GetBorderContentPadding(null, state);
    }
    #endregion

    #region Draw
    /// <summary>
    /// Gets the actual content draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDraw(PaletteState state) => Inherit.GetContentDraw(state);

    #endregion

    #region DrawFocus
    /// <summary>
    /// Gets the actual content draw with focus value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDrawFocus(PaletteState state) => Inherit.GetContentDrawFocus(state);

    #endregion

    #region ContentImage
    /// <summary>
    /// Gets the actual content image horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageH(PaletteState state) => Inherit.GetContentImageH(state);

    /// <summary>
    /// Gets the actual content image vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageV(PaletteState state) => Inherit.GetContentImageV(state);

    /// <summary>
    /// Gets the actual image drawing effect value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public PaletteImageEffect GetContentImageEffect(PaletteState state) => Inherit.GetContentImageEffect(state);

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorMap(PaletteState state) => Inherit.GetContentImageColorMap(state);

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorTo(PaletteState state) => Inherit.GetContentImageColorTo(state);

    #endregion

    #region ContentShortText
    /// <summary>
    /// Gets the font for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Font for drawing the content text.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Font? Font
    {
        get => _font;

        set
        {
            if (value != _font)
            {
                _font = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets the actual content short text font value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public virtual Font? GetContentShortTextFont(PaletteState state) => _font ?? Inherit.GetContentShortTextFont(state);

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public virtual Font? GetContentShortTextNewFont(PaletteState state) => _font ?? Inherit.GetContentShortTextNewFont(state);

    /// <summary>
    /// Gets the actual text rendering hint for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentShortTextHint(PaletteState state) => Inherit.GetContentShortTextHint(state);

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) => Inherit.GetContentShortTextPrefix(state);

    /// <summary>
    /// Gets the actual text trimming for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentShortTextTrim(PaletteState state) => Inherit.GetContentShortTextTrim(state);

    /// <summary>
    /// Gets and sets the horizontal Content text alignment for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Relative horizontal Content text alignment\nIn order to get this into the designer.cs you must also modify another value in this area!")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(PaletteRelativeAlign.Inherit)]
    public PaletteRelativeAlign TextH
    {
        get => _shortTextH;

        set
        {
            if (value != _shortTextH)
            {
                _shortTextH = value;
                PerformNeedPaint();
            }
        }
    }

    private bool ShouldSerializeTextH() => _shortTextH != PaletteRelativeAlign.Inherit;

    private void ResetTextH() => _shortTextH = PaletteRelativeAlign.Inherit;

    /// <summary>
    /// Gets the actual content short text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentShortTextH(PaletteState state) => ShouldSerializeTextH() ? _shortTextH : Inherit.GetContentShortTextH(state);

    /// <summary>
    /// Gets the actual content short text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentShortTextV(PaletteState state) => Inherit.GetContentShortTextV(state);

    /// <summary>
    /// Gets the actual content short text horizontal multiline alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state) => Inherit.GetContentShortTextMultiLineH(state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentShortTextMultiLine(PaletteState state) => Inherit.GetContentShortTextMultiLine(state);

    /// <summary>
    /// Gets and sets the color for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Main color for the text.")]
    [KryptonDefaultColor]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color Color1
    {
        get => _color1;

        set
        {
            if (value != _color1)
            {
                _color1 = value;
                PerformNeedPaint();
            }
        }
    }

    /// <summary>
    /// Gets the first color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor1(PaletteState state) => !_color1.IsEmpty ? _color1 : Inherit.GetContentShortTextColor1(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor2(PaletteState state) => Inherit.GetContentShortTextColor2(state);

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentShortTextColorStyle(PaletteState state) => Inherit.GetContentShortTextColorStyle(state);

    /// <summary>
    /// Gets the color alignment style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state) => Inherit.GetContentShortTextColorAlign(state);

    /// <summary>
    /// Gets the color angle for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentShortTextColorAngle(PaletteState state) => Inherit.GetContentShortTextColorAngle(state);

    /// <summary>
    /// Gets an image for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentShortTextImage(PaletteState state) => Inherit.GetContentShortTextImage(state);

    /// <summary>
    /// Gets the image style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentShortTextImageStyle(PaletteState state) => Inherit.GetContentShortTextImageStyle(state);

    /// <summary>
    /// Gets the image alignment style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state) => Inherit.GetContentShortTextImageAlign(state);

    #endregion

    #region ContentLongText
    /// <summary>
    /// Gets the actual content long text font value.
    /// </summary>
    /// <returns>Font value.</returns>
    /// <param name="state">Palette value should be applicable to this state.</param>
    public Font? GetContentLongTextFont(PaletteState state) => Inherit.GetContentLongTextFont(state);

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentLongTextNewFont(PaletteState state) => Inherit.GetContentLongTextNewFont(state);

    /// <summary>
    /// Gets the actual text rendering hint for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentLongTextHint(PaletteState state) => Inherit.GetContentLongTextHint(state);

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state) => Inherit.GetContentLongTextPrefix(state);

    /// <summary>
    /// Gets the actual text trimming for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentLongTextTrim(PaletteState state) => Inherit.GetContentLongTextTrim(state);

    /// <summary>
    /// Gets the actual content long text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextH(PaletteState state) => Inherit.GetContentLongTextH(state);

    /// <summary>
    /// Gets the actual content long text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextV(PaletteState state) => Inherit.GetContentLongTextV(state);

    /// <summary>
    /// Gets the actual content long text horizontal multiline alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state) => Inherit.GetContentLongTextMultiLineH(state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentLongTextMultiLine(PaletteState state) => Inherit.GetContentLongTextMultiLine(state);

    /// <summary>
    /// Gets the first color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor1(PaletteState state) => Inherit.GetContentLongTextColor1(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor2(PaletteState state) => Inherit.GetContentLongTextColor2(state);

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentLongTextColorStyle(PaletteState state) => Inherit.GetContentLongTextColorStyle(state);

    /// <summary>
    /// Gets the color alignment style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state) => Inherit.GetContentLongTextColorAlign(state);

    /// <summary>
    /// Gets the color angle for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentLongTextColorAngle(PaletteState state) => Inherit.GetContentLongTextColorAngle(state);

    /// <summary>
    /// Gets an image for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentLongTextImage(PaletteState state) => Inherit.GetContentLongTextImage(state);

    /// <summary>
    /// Gets the image style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentLongTextImageStyle(PaletteState state) => Inherit.GetContentLongTextImageStyle(state);

    /// <summary>
    /// Gets the image alignment style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state) => Inherit.GetContentLongTextImageAlign(state);

    #endregion

    #region Padding
    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Padding between the border and content drawing.")]
    [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
    [RefreshProperties(RefreshProperties.All)]
    public Padding Padding
    {
        get => _padding;

        set
        {
            if (!value.Equals(_padding))
            {
                _padding = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializePadding() => !_padding.Equals(CommonHelper.InheritPadding);

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    private void ResetPadding() => _padding = CommonHelper.InheritPadding;

    /// <summary>
    /// Gets the actual padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public virtual Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state) => 
        !_padding.Equals(CommonHelper.InheritPadding)
            ? _padding
            : Inherit.GetBorderContentPadding(owningForm, state);

    #endregion

    #region AdjacentGap
    /// <summary>
    /// Gets the actual padding between adjacent content items.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public int GetContentAdjacentGap(PaletteState state) => Inherit.GetContentAdjacentGap(state);

    #endregion

    #region ContentStyle
    /// <summary>
    /// Gets the style appropriate for this content.
    /// </summary>
    /// <returns>Content style.</returns>
    public PaletteContentStyle GetContentStyle() => Inherit.GetContentStyle();

    #endregion

    #region Protected
    /// <summary>
    /// Gets the inheritance parent.
    /// </summary>
    protected IPaletteContent Inherit { get; private set; }

    #endregion
}