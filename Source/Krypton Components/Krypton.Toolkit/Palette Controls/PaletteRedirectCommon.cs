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
/// Redirect back/border/content based on the enabled/disabled state.
/// </summary>
public class PaletteRedirectCommon : PaletteRedirect
{
    #region Instance Fields
    private readonly IPaletteTriple? _disabled;
    private readonly IPaletteTriple? _others;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectCommon class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="others">Redirection for all other state requests.</param>
    public PaletteRedirectCommon(PaletteBase target,
        [DisallowNull] IPaletteTriple disabled,
        [DisallowNull] IPaletteTriple others)
        : base(target)
    {
        Debug.Assert(disabled != null);
        Debug.Assert(others != null);

        // Remember state specific inheritance
        _disabled = disabled!;
        _others = others!;
    }
    #endregion

    #region Back
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackDraw(state)
            : base.GetBackDraw(style, state);
    }

    /// <summary>
    /// Gets the graphics drawing hint for the background.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackGraphicsHint(state)
            : base.GetBackGraphicsHint(style, state);
    }

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackColor1(state)
            : base.GetBackColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackColor2(state)
            : base.GetBackColor2(style, state);
    }

    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackColorStyle(state)
            : base.GetBackColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackColorAlign(state)
            : base.GetBackColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackColorAngle(state)
            : base.GetBackColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackImage(state)
            : base.GetBackImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackImageStyle(state)
            : base.GetBackImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBack.GetBackImageAlign(state)
            : base.GetBackImageAlign(style, state);
    }
    #endregion

    #region Border
    /// <summary>
    /// Gets a value indicating if border should be drawn.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderDraw(state)
            : base.GetBorderDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderDrawBorders(state)
            : base.GetBorderDrawBorders(style, state);
    }

    /// <summary>
    /// Gets the graphics drawing hint for the border.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderGraphicsHint(state)
            : base.GetBorderGraphicsHint(style, state);
    }

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColor1(state)
            : base.GetBorderColor1(style, state);
    }

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColor2(state)
            : base.GetBorderColor2(style, state);
    }

    /// <summary>
    /// Gets the color border drawing style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColorStyle(state)
            : base.GetBorderColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColorAlign(state)
            : base.GetBorderColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColorAngle(state)
            : base.GetBorderColorAngle(style, state);
    }

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer width.</returns>
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderWidth(state)
            : base.GetBorderWidth(style, state);
    }

    /// <summary>
    /// Gets the border corner rounding.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Float rounding.</returns>
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderRounding(state)
            : base.GetBorderRounding(style, state);
    }

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderImage(state)
            : base.GetBorderImage(style, state);
    }

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderImageStyle(state)
            : base.GetBorderImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteBorder!.GetBorderImageAlign(state)
            : base.GetBorderImageAlign(style, state);
    }
    #endregion

    #region Content
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentDraw(state)
            : base.GetContentDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentDrawFocus(state)
            : base.GetContentDrawFocus(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentImageH(state)
            : base.GetContentImageH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentImageV(state)
            : base.GetContentImageV(style, state);
    }

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentImageEffect(state)
            : base.GetContentImageEffect(style, state);
    }

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextFont(state)
            : base.GetContentShortTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextHint(state)
            : base.GetContentShortTextHint(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextPrefix(state)
            : base.GetContentShortTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextMultiLine(state)
            : base.GetContentShortTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextTrim(state)
            : base.GetContentShortTextTrim(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextH(state)
            : base.GetContentShortTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextV(state)
            : base.GetContentShortTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextMultiLineH(state)
            : base.GetContentShortTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColor1(state)
            : base.GetContentShortTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColor2(state)
            : base.GetContentShortTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColorStyle(state)
            : base.GetContentShortTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColorAlign(state)
            : base.GetContentShortTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColorAngle(state)
            : base.GetContentShortTextColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextImage(state)
            : base.GetContentShortTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextImageStyle(state)
            : base.GetContentShortTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextImageAlign(state)
            : base.GetContentShortTextImageAlign(style, state);
    }

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextFont(state)
            : base.GetContentLongTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextHint(state)
            : base.GetContentLongTextHint(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextMultiLine(state)
            : base.GetContentLongTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextTrim(state)
            : base.GetContentLongTextTrim(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextPrefix(state)
            : base.GetContentLongTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextH(state)
            : base.GetContentLongTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextV(state)
            : base.GetContentLongTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextMultiLineH(state)
            : base.GetContentLongTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColor1(state)
            : base.GetContentLongTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColor2(state)
            : base.GetContentLongTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColorStyle(state)
            : base.GetContentLongTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColorAlign(state)
            : base.GetContentLongTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColorAngle(state)
            : base.GetContentLongTextColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextImage(state)
            : base.GetContentLongTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextImageStyle(state)
            : base.GetContentLongTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextImageAlign(state)
            : base.GetContentLongTextImageAlign(style, state);
    }

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteContentStyle style,
        PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetBorderContentPadding(owningForm, state)
            : base.GetBorderContentPadding(owningForm, style, state);
    }

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);
        return inherit != null
            ? inherit.PaletteContent!.GetContentAdjacentGap(state)
            : base.GetContentAdjacentGap(style, state);
    }
    #endregion

    #region Implementation
    private IPaletteTriple? GetInherit(PaletteState state)
    {
        // Do not inherit the override states
        if (CommonHelper.IsOverrideState(state))
        {
            return null;
        }

        switch (state)
        {
            case PaletteState.Disabled:
                Debug.Assert(_disabled != null);
                return _disabled;
            default:
                Debug.Assert(_others != null);
                return _others;
        }
    }
    #endregion
}