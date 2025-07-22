#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Redirects requests onto a dynamic instance of a IPalette.
/// </summary>
public class PaletteRedirect : PaletteBase, IGlobalId
{
    #region Instance Fields
    private PaletteBase? _target;
    #endregion

    /// <inheritdoc/>
    protected override Color[] SchemeColors => _target?.GetSchemeColors() ?? Array.Empty<Color>();

    #region Identity

    /// <summary>
    /// Initialize a new instance of the PaletteRedirect class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <remarks>target can be null when performing a drag-drop operation (Amongst other usages !!)</remarks>
    public PaletteRedirect(PaletteBase? target)
    {
        Id = CommonHelper.NextId;
        // Remember incoming target
        _target = target;
    }

    #endregion

    #region Target
    /// <summary>
    /// Gets and sets the redirection target.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual PaletteBase? Target
    {
        get => _target;
        set => _target = value;
    }
    #endregion

    #region UseThemeFormChromeBorderWidth
    /// <summary>
    /// Gets a value indicating if KryptonForm instances should UseThemeFormChromeBorderWidth.
    /// </summary>
    /// <returns>InheritBool value.</returns>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public override InheritBool UseThemeFormChromeBorderWidth
    {
        get => _target!.UseThemeFormChromeBorderWidth;
        set => _target!.UseThemeFormChromeBorderWidth = value;
    }

    #endregion

    #region Renderer
    /// <summary>
    /// Gets the renderer to use for this palette.
    /// </summary>
    /// <returns>Renderer to use for drawing palette settings.</returns>
    public override IRenderer GetRenderer() => _target!.GetRenderer();

    #endregion

    #region Back
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state) => _target!.GetBackDraw(style, state);

    /// <summary>
    /// Gets the graphics drawing hint for the background.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state) => _target!.GetBackGraphicsHint(style, state);

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state) => _target!.GetBackColor1(style, state);

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state) => _target!.GetBackColor2(style, state);

    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state) => _target!.GetBackColorStyle(style, state);

    /// <summary>
    /// Gets the color alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state) => _target!.GetBackColorAlign(style, state);

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state) => _target!.GetBackColorAngle(style, state);

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteBackStyle style, PaletteState state) => _target?.GetBackImage(style, state);

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state) => _target!.GetBackImageStyle(style, state);

    /// <summary>
    /// Gets the image alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state) => _target!.GetBackImageAlign(style, state);

    #endregion

    #region Border
    /// <summary>
    /// Gets a value indicating if border should be drawn.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderDraw(style, state);

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderDrawBorders(style, state);

    /// <summary>
    /// Gets the graphics drawing hint for the border.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderGraphicsHint(style, state);

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderColor1(style, state);

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderColor2(style, state);

    /// <summary>
    /// Gets the color border drawing style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderColorStyle(style, state);

    /// <summary>
    /// Gets the color border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderColorAlign(style, state);

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderColorAngle(style, state);

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer width.</returns>
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderWidth(style, state);

    /// <summary>
    /// Gets the border corner rounding.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Float rounding.</returns>
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderRounding(style, state);

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state) => _target?.GetBorderImage(style, state);

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderImageStyle(style, state);

    /// <summary>
    /// Gets the image border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state) => _target!.GetBorderImageAlign(style, state);
    #endregion

    #region Content
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state) => _target!.GetContentDraw(style, state);

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state) => _target!.GetContentDrawFocus(style, state);

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state) => _target!.GetContentImageH(style, state);

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state) => _target!.GetContentImageV(style, state);

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state) => _target!.GetContentImageEffect(style, state);

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state) => _target!.GetContentImageColorMap(style, state);

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state) => _target!.GetContentImageColorTo(style, state);

    /// <inheritdoc />
    public override Color GetContentImageColorTransparent(PaletteContentStyle style, PaletteState state) => _target!.GetContentImageColorTransparent(style, state);

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state) => _target?.GetContentShortTextFont(style, state);

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextNewFont(PaletteContentStyle style, PaletteState state) => _target?.GetContentShortTextNewFont(style, state);

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextHint(style, state);

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextPrefix(style, state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextMultiLine(style, state);

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextTrim(style, state);

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextH(style, state);

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextV(style, state);

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextMultiLineH(style, state);

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextColor1(style, state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextColor2(style, state);

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextColorStyle(style, state);

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextColorAlign(style, state);

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextColorAngle(style, state);

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state) => _target?.GetContentShortTextImage(style, state);

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextImageStyle(style, state);

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state) => _target!.GetContentShortTextImageAlign(style, state);

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state) => _target?.GetContentLongTextFont(style, state);

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextNewFont(PaletteContentStyle style, PaletteState state) => _target?.GetContentLongTextNewFont(style, state);

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextHint(style, state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextMultiLine(style, state);

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextTrim(style, state);

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextPrefix(style, state);

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextH(style, state);

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextV(style, state);

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextMultiLineH(style, state);

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextColor1(style, state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextColor2(style, state);

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextColorStyle(style, state);

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextColorAlign(style, state);

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextColorAngle(style, state);

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state) => _target?.GetContentLongTextImage(style, state);

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextImageStyle(style, state);

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state) => _target!.GetContentLongTextImageAlign(style, state);

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteContentStyle style,
        PaletteState state) => _target!.GetBorderContentPadding(owningForm, style, state);

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state) => _target!.GetContentAdjacentGap(style, state);

    #endregion

    #region Metric

    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    public override int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric) => _target!.GetMetricInt(owningForm, state, metric);

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) => _target!.GetMetricBool(state, metric);

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state,
        PaletteMetricPadding metric) => _target!.GetMetricPadding(owningForm, state, metric);

    #endregion

    #region Images
    /// <summary>
    /// Gets a tree view image appropriate for the provided state.
    /// </summary>
    /// <param name="expanded">Is the node expanded</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetTreeViewImage(bool expanded) => _target?.GetTreeViewImage(expanded);

    /// <summary>
    /// Gets a checkbox image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the checkbox enabled.</param>
    /// <param name="checkState">Is the checkbox checked/unchecked/indeterminate.</param>
    /// <param name="tracking">Is the checkbox being hot tracked.</param>
    /// <param name="pressed">Is the checkbox being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed) => _target?.GetCheckBoxImage(enabled, checkState, tracking, pressed);

    /// <summary>
    /// Gets a checkbox image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the radio button enabled.</param>
    /// <param name="checkState">Is the radio button checked.</param>
    /// <param name="tracking">Is the radio button being hot tracked.</param>
    /// <param name="pressed">Is the radio button being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed) => _target?.GetRadioButtonImage(enabled, checkState, tracking, pressed);

    /// <summary>
    /// Gets a checked image appropriate for a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuCheckedImage() => _target?.GetContextMenuCheckedImage();

    /// <summary>
    /// Gets a indeterminate image appropriate for a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuIndeterminateImage() => _target?.GetContextMenuIndeterminateImage();

    /// <summary>
    /// Gets an image indicating a sub-menu on a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuSubMenuImage() => _target?.GetContextMenuSubMenuImage();

    /// <summary>
    /// Gets a checkbox image appropriate for the provided state.
    /// </summary>
    /// <param name="button">Enum of the button to fetch.</param>
    /// <param name="state">State of the button to fetch.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state) => _target?.GetGalleryButtonImage(button, state);

    #endregion

    #region ButtonSpec
    /// <summary>
    /// Gets the icon to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Icon value.</returns>
    public override Icon? GetButtonSpecIcon(PaletteButtonSpecStyle style) => _target?.GetButtonSpecIcon(style);

    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) => _target?.GetButtonSpecImage(style, state);

    /// <summary>
    /// Gets the image transparent color.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public override Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style) => _target!.GetButtonSpecImageTransparentColor(style);

    /// <summary>
    /// Gets the short text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public override string GetButtonSpecShortText(PaletteButtonSpecStyle style) => _target!.GetButtonSpecShortText(style);

    /// <summary>
    /// Gets the long text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public override string GetButtonSpecLongText(PaletteButtonSpecStyle style) => _target!.GetButtonSpecLongText(style);

    /// <summary>
    /// Gets the tooltip title text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public override string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style) => _target!.GetButtonSpecToolTipTitle(style);

    /// <summary>
    /// Gets the color to remap from the image to the container foreground.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public override Color GetButtonSpecColorMap(PaletteButtonSpecStyle style) => _target!.GetButtonSpecColorMap(style);

    /// <inheritdoc />
    public override Color GetButtonSpecColorTransparent(PaletteButtonSpecStyle style) => _target!.GetButtonSpecColorTransparent(style);

    /// <summary>
    /// Gets the button style used for drawing the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>ButtonStyle value.</returns>
    public override PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style) => _target!.GetButtonSpecStyle(style);

    /// <summary>
    /// Get the location for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>HeaderLocation value.</returns>
    public override HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style) => _target!.GetButtonSpecLocation(style);

    /// <summary>
    /// Gets the edge to position the button against.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteRelativeEdgeAlign value.</returns>
    public override PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style) => _target!.GetButtonSpecEdge(style);

    /// <summary>
    /// Gets the button orientation.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonOrientation value.</returns>
    public override PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style) => _target!.GetButtonSpecOrientation(style);

    #endregion

    #region RibbonGeneral
    /// <summary>
    /// Gets the ribbon shape that should be used.
    /// </summary>
    /// <returns>Ribbon shape value.</returns>
    public override PaletteRibbonShape GetRibbonShape() => _target!.GetRibbonShape();

    /// <summary>
    /// Gets the text alignment for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) => _target!.GetRibbonContextTextAlign(state);

    /// <summary>
    /// Gets the font for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetRibbonContextTextFont(PaletteState state) => _target!.GetRibbonContextTextFont(state);

    /// <summary>
    /// Gets the color for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Color GetRibbonContextTextColor(PaletteState state) => _target!.GetRibbonContextTextColor(state);

    /// <summary>
    /// Gets the dark disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDisabledDark(PaletteState state) => _target!.GetRibbonDisabledDark(state);

    /// <summary>
    /// Gets the light disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDisabledLight(PaletteState state) => _target!.GetRibbonDisabledLight(state);

    /// <summary>
    /// Gets the color for the drop arrow light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDropArrowLight(PaletteState state) => _target!.GetRibbonDropArrowLight(state);

    /// <summary>
    /// Gets the color for the drop arrow dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDropArrowDark(PaletteState state) => _target!.GetRibbonDropArrowDark(state);

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogDark(PaletteState state) => _target!.GetRibbonGroupDialogDark(state);

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogLight(PaletteState state) => _target!.GetRibbonGroupDialogLight(state);

    /// <summary>
    /// Gets the color for the group separator dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorDark(PaletteState state) => _target!.GetRibbonGroupSeparatorDark(state);

    /// <summary>
    /// Gets the color for the group separator light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorLight(PaletteState state) => _target!.GetRibbonGroupSeparatorLight(state);

    /// <summary>
    /// Gets the color for the minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarDark(PaletteState state) => _target!.GetRibbonMinimizeBarDark(state);

    /// <summary>
    /// Gets the gradient dark rafting color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => _target!.GetRibbonTabRowBackgroundGradientRaftingDark(state);

    /// <summary>
    /// Gets the gradient light rafting color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => _target!.GetRibbonTabRowBackgroundGradientRaftingLight(state);

    /// <summary>
    /// Gets the solid color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _target!.GetRibbonTabRowBackgroundSolidColor(state);

    /// <summary>
    /// Gets the color for the minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarLight(PaletteState state) => _target!.GetRibbonMinimizeBarLight(state);

    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorColor(PaletteState state) => _target!.GetRibbonTabSeparatorColor(state);

    /// <summary>
    /// Gets the color for the tab context separators.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorContextColor(PaletteState state) => _target!.GetRibbonTabSeparatorContextColor(state);

    /// <summary>
    /// Gets the font for the ribbon text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetRibbonTextFont(PaletteState state) => _target!.GetRibbonTextFont(state);

    /// <summary>
    /// Gets the rendering hint for the ribbon font.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetRibbonTextHint(PaletteState state) => _target!.GetRibbonTextHint(state);

    /// <summary>
    /// Gets the color for the extra QAT button dark content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonDark(PaletteState state) => _target!.GetRibbonQATButtonDark(state);

    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonLight(PaletteState state) => _target!.GetRibbonQATButtonLight(state);

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => _target!.GetRibbonTabRowGradientColor1(state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _target!.GetRibbonFileAppTabBottomColor(state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _target!.GetRibbonFileAppTabTopColor(state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _target!.GetRibbonFileAppTabTextColor(state);

    /// <summary>Gets the ribbon tab row gradient rafting angle.</summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>The gradient rafting angle.</returns>
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) =>
        _target!.GetRibbonTabRowGradientRaftingAngle(state);

    #endregion

    #region RibbonBack
    /// <summary>
    /// Gets the method used to draw the background of a ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteRibbonBackStyle value.</returns>
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state) => _target!.GetRibbonBackColorStyle(style, state);

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state) => _target!.GetRibbonBackColor1(style, state);

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state) => _target!.GetRibbonBackColor2(style, state);

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state) => _target!.GetRibbonBackColor3(style, state);

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state) => _target!.GetRibbonBackColor4(style, state);

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state) => _target!.GetRibbonBackColor5(style, state);

    #endregion

    #region RibbonText
    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="style">Text style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state) => _target!.GetRibbonTextColor(style, state);

    #endregion

    #region ElementColor
    /// <summary>
    /// Gets the first element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor1(PaletteElement element, PaletteState state) => _target!.GetElementColor1(element, state);

    /// <summary>
    /// Gets the second element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor2(PaletteElement element, PaletteState state) => _target!.GetElementColor2(element, state);

    /// <summary>
    /// Gets the third element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor3(PaletteElement element, PaletteState state) => _target!.GetElementColor3(element, state);

    /// <summary>
    /// Gets the fourth element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor4(PaletteElement element, PaletteState state) => _target!.GetElementColor4(element, state);

    /// <summary>
    /// Gets the fifth element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor5(PaletteElement element, PaletteState state) => _target!.GetElementColor5(element, state);

    #endregion

    #region DragDrop
    /// <summary>
    /// Gets the feedback drawing method used.
    /// </summary>
    /// <returns>Feedback enumeration value.</returns>
    public override PaletteDragFeedback GetDragDropFeedback() => _target!.GetDragDropFeedback();

    /// <summary>
    /// Gets the background color for a solid drag drop area.
    /// </summary>
    /// <returns>Color value.</returns>
    public override Color GetDragDropSolidBack() => _target!.GetDragDropSolidBack();

    /// <summary>
    /// Gets the border color for a solid drag drop area.
    /// </summary>
    /// <returns>Color value.</returns>
    public override Color GetDragDropSolidBorder() => _target!.GetDragDropSolidBack();

    /// <summary>
    /// Gets the opacity of the solid area.
    /// </summary>
    /// <returns>Opacity ranging from 0 to 1.</returns>
    public override float GetDragDropSolidOpacity() => _target!.GetDragDropSolidOpacity();

    /// <summary>
    /// Gets the background color for the docking indicators area.
    /// </summary>
    /// <returns>Color value.</returns>
    public override Color GetDragDropDockBack() => _target!.GetDragDropDockBack();

    /// <summary>
    /// Gets the border color for the docking indicators area.
    /// </summary>
    /// <returns>Color value.</returns>
    public override Color GetDragDropDockBorder() => _target!.GetDragDropDockBorder();

    /// <summary>
    /// Gets the active color for docking indicators.
    /// </summary>
    /// <returns>Color value.</returns>
    public override Color GetDragDropDockActive() => _target!.GetDragDropDockActive();

    /// <summary>
    /// Gets the inactive color for docking indicators.
    /// </summary>
    /// <returns>Color value.</returns>
    public override Color GetDragDropDockInactive() => _target!.GetDragDropDockInactive();

    #endregion

    #region ColorTable

    /// <summary>
    /// Gets access to the color table instance.
    /// </summary>
    public override KryptonColorTable ColorTable => _target!.ColorTable;

    #endregion

    /// <inheritdoc />
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Id { get; }
}