#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Redirect back/border/content based on the incoming grid state and style.
/// </summary>
public class PaletteRedirectGrids : PaletteRedirect
{
    #region Instance Fields
    private readonly KryptonPaletteGrid _grid;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTriple class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="grid">Grid reference for directing palette requests.</param>
    public PaletteRedirectGrids(PaletteBase? target, [DisallowNull] KryptonPaletteGrid grid)
        : base(target)
    {
        Debug.Assert(grid is not null);

        _grid = grid ?? throw new ArgumentNullException(nameof(grid));
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
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackDraw(state) ?? Target!.GetBackDraw(style, state);
    }

    /// <summary>
    /// Gets the graphics drawing hint for the background.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackGraphicsHint(state) ?? Target!.GetBackGraphicsHint(style, state);
    }

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackColor1(state) ?? Target!.GetBackColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackColor2(state) ?? Target!.GetBackColor2(style, state);
    }

    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackColorStyle(state) ?? Target!.GetBackColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackColorAlign(state) ?? Target!.GetBackColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackColorAngle(state) ?? Target!.GetBackColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackImage(state) ?? Target!.GetBackImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackImageStyle(state) ?? Target!.GetBackImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack inherit = GetInheritBack(style, state);

        return inherit?.GetBackImageAlign(state) ?? Target!.GetBackImageAlign(style, state);
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
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderDraw(state) ?? Target!.GetBorderDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderDrawBorders(state) ?? Target!.GetBorderDrawBorders(style, state);
    }

    /// <summary>
    /// Gets the graphics drawing hint for the border.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderGraphicsHint(state) ?? Target!.GetBorderGraphicsHint(style, state);
    }

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderColor1(state) ?? Target!.GetBorderColor1(style, state);
    }

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderColor2(state) ?? Target!.GetBorderColor2(style, state);
    }

    /// <summary>
    /// Gets the color border drawing style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderColorStyle(state) ?? Target!.GetBorderColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderColorAlign(state) ?? Target!.GetBorderColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderColorAngle(state) ?? Target!.GetBorderColorAngle(style, state);
    }

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer width.</returns>
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderWidth(state) ?? Target!.GetBorderWidth(style, state);
    }

    /// <summary>
    /// Gets the border corner rounding.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Float rounding.</returns>
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderRounding(state) ?? Target!.GetBorderRounding(style, state);
    }

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderImage(state) ?? Target!.GetBorderImage(style, state);
    }

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderImageStyle(state) ?? Target!.GetBorderImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder inherit = GetInheritBorder(style, state);

        return inherit?.GetBorderImageAlign(state) ?? Target!.GetBorderImageAlign(style, state);
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
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentDraw(state) ?? Target!.GetContentDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentDrawFocus(state) ?? Target!.GetContentDrawFocus(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentImageH(state) ?? Target!.GetContentImageH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentImageV(state) ?? Target!.GetContentImageV(style, state);
    }

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentImageEffect(state) ?? Target!.GetContentImageEffect(style, state);
    }

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextFont(state) ?? Target!.GetContentShortTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextHint(state) ?? Target!.GetContentShortTextHint(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextPrefix(state) ?? Target!.GetContentShortTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextMultiLine(state) ?? Target!.GetContentShortTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextTrim(state) ?? Target!.GetContentShortTextTrim(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextH(state) ?? Target!.GetContentShortTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextV(state) ?? Target!.GetContentShortTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextMultiLineH(state) ?? Target!.GetContentShortTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextColor1(state) ?? Target!.GetContentShortTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextColor2(state) ?? Target!.GetContentShortTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextColorStyle(state) ?? Target!.GetContentShortTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextColorAlign(state) ?? Target!.GetContentShortTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextColorAngle(state) ?? Target!.GetContentShortTextColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextImage(state) ?? Target!.GetContentShortTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextImageStyle(state) ?? Target!.GetContentShortTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentShortTextImageAlign(state) ?? Target!.GetContentShortTextImageAlign(style, state);
    }

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextFont(state) ?? Target!.GetContentLongTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextHint(state) ?? Target!.GetContentLongTextHint(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextMultiLine(state) ?? Target!.GetContentLongTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextTrim(state) ?? Target!.GetContentLongTextTrim(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextPrefix(state) ?? Target!.GetContentLongTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextH(state) ?? Target!.GetContentLongTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextV(state) ?? Target!.GetContentLongTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextMultiLineH(state) ?? Target!.GetContentLongTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextColor1(state) ?? Target!.GetContentLongTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextColor2(state) ?? Target!.GetContentLongTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextColorStyle(state) ?? Target!.GetContentLongTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextColorAlign(state) ?? Target!.GetContentLongTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextColorAngle(state) ?? Target!.GetContentLongTextColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextImage(state) ?? Target!.GetContentLongTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextImageStyle(state) ?? Target!.GetContentLongTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentLongTextImageAlign(state) ?? Target!.GetContentLongTextImageAlign(style, state);
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
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetBorderContentPadding(owningForm, state)
               ?? Target!.GetBorderContentPadding(owningForm, style, state);
    }

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent inherit = GetInheritContent(style, state);

        return inherit?.GetContentAdjacentGap(state)
               ?? Target!.GetContentAdjacentGap(style, state);
    }
    #endregion

    #region Implementation
    private IPaletteBack GetInheritBack(PaletteBackStyle style, PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                switch (style)
                {
                    case PaletteBackStyle.GridBackgroundList:
                    case PaletteBackStyle.GridBackgroundSheet:
                    case PaletteBackStyle.GridBackgroundCustom1:
                    case PaletteBackStyle.GridBackgroundCustom2:
                    case PaletteBackStyle.GridBackgroundCustom3:
                        return _grid.StateDisabled.Background;
                    case PaletteBackStyle.GridDataCellList:
                    case PaletteBackStyle.GridDataCellSheet:
                    case PaletteBackStyle.GridDataCellCustom1:
                    case PaletteBackStyle.GridDataCellCustom2:
                    case PaletteBackStyle.GridDataCellCustom3:
                        return _grid.StateDisabled.DataCell.Back;
                    case PaletteBackStyle.GridHeaderColumnList:
                    case PaletteBackStyle.GridHeaderColumnSheet:
                    case PaletteBackStyle.GridHeaderColumnCustom1:
                    case PaletteBackStyle.GridHeaderColumnCustom2:
                    case PaletteBackStyle.GridHeaderColumnCustom3:
                        return _grid.StateDisabled.HeaderColumn.Back;
                    case PaletteBackStyle.GridHeaderRowList:
                    case PaletteBackStyle.GridHeaderRowSheet:
                    case PaletteBackStyle.GridHeaderRowCustom1:
                    case PaletteBackStyle.GridHeaderRowCustom2:
                    case PaletteBackStyle.GridHeaderRowCustom3:
                        return _grid.StateDisabled.HeaderRow.Back;
                }
                break;

            case PaletteState.BoldedOverride:
            case PaletteState.Normal:
                switch (style)
                {
                    case PaletteBackStyle.GridBackgroundList:
                    case PaletteBackStyle.GridBackgroundSheet:
                    case PaletteBackStyle.GridBackgroundCustom1:
                    case PaletteBackStyle.GridBackgroundCustom2:
                    case PaletteBackStyle.GridBackgroundCustom3:
                        return _grid.StateNormal.Background;
                    case PaletteBackStyle.GridDataCellList:
                    case PaletteBackStyle.GridDataCellSheet:
                    case PaletteBackStyle.GridDataCellCustom1:
                    case PaletteBackStyle.GridDataCellCustom2:
                    case PaletteBackStyle.GridDataCellCustom3:
                        return _grid.StateNormal.DataCell.Back;
                    case PaletteBackStyle.GridHeaderColumnList:
                    case PaletteBackStyle.GridHeaderColumnSheet:
                    case PaletteBackStyle.GridHeaderColumnCustom1:
                    case PaletteBackStyle.GridHeaderColumnCustom2:
                    case PaletteBackStyle.GridHeaderColumnCustom3:
                        return _grid.StateNormal.HeaderColumn.Back;
                    case PaletteBackStyle.GridHeaderRowList:
                    case PaletteBackStyle.GridHeaderRowSheet:
                    case PaletteBackStyle.GridHeaderRowCustom1:
                    case PaletteBackStyle.GridHeaderRowCustom2:
                    case PaletteBackStyle.GridHeaderRowCustom3:
                        return _grid.StateNormal.HeaderRow.Back;
                }
                break;

            case PaletteState.Pressed:
                switch (style)
                {
                    case PaletteBackStyle.GridHeaderColumnList:
                    case PaletteBackStyle.GridHeaderColumnSheet:
                    case PaletteBackStyle.GridHeaderColumnCustom1:
                    case PaletteBackStyle.GridHeaderColumnCustom2:
                    case PaletteBackStyle.GridHeaderColumnCustom3:
                        return _grid.StatePressed.HeaderColumn.Back;
                    case PaletteBackStyle.GridHeaderRowList:
                    case PaletteBackStyle.GridHeaderRowSheet:
                    case PaletteBackStyle.GridHeaderRowCustom1:
                    case PaletteBackStyle.GridHeaderRowCustom2:
                    case PaletteBackStyle.GridHeaderRowCustom3:
                        return _grid.StatePressed.HeaderRow.Back;
                }
                break;

            case PaletteState.Tracking:
                switch (style)
                {
                    case PaletteBackStyle.GridHeaderColumnList:
                    case PaletteBackStyle.GridHeaderColumnSheet:
                    case PaletteBackStyle.GridHeaderColumnCustom1:
                    case PaletteBackStyle.GridHeaderColumnCustom2:
                    case PaletteBackStyle.GridHeaderColumnCustom3:
                        return _grid.StateTracking.HeaderColumn.Back;
                    case PaletteBackStyle.GridHeaderRowList:
                    case PaletteBackStyle.GridHeaderRowSheet:
                    case PaletteBackStyle.GridHeaderRowCustom1:
                    case PaletteBackStyle.GridHeaderRowCustom2:
                    case PaletteBackStyle.GridHeaderRowCustom3:
                        return _grid.StateTracking.HeaderRow.Back;
                }
                break;

            case PaletteState.CheckedNormal:
                switch (style)
                {
                    case PaletteBackStyle.GridDataCellList:
                    case PaletteBackStyle.GridDataCellSheet:
                    case PaletteBackStyle.GridDataCellCustom1:
                    case PaletteBackStyle.GridDataCellCustom2:
                    case PaletteBackStyle.GridDataCellCustom3:
                        return _grid.StateSelected.DataCell.Back;
                    case PaletteBackStyle.GridHeaderColumnList:
                    case PaletteBackStyle.GridHeaderColumnSheet:
                    case PaletteBackStyle.GridHeaderColumnCustom1:
                    case PaletteBackStyle.GridHeaderColumnCustom2:
                    case PaletteBackStyle.GridHeaderColumnCustom3:
                        return _grid.StateSelected.HeaderColumn.Back;
                    case PaletteBackStyle.GridHeaderRowList:
                    case PaletteBackStyle.GridHeaderRowSheet:
                    case PaletteBackStyle.GridHeaderRowCustom1:
                    case PaletteBackStyle.GridHeaderRowCustom2:
                    case PaletteBackStyle.GridHeaderRowCustom3:
                        return _grid.StateSelected.HeaderRow.Back;
                }
                break;
        }

        // Should never happen!
        Debug.Assert(false);
        throw DebugTools.NotImplemented(state.ToString());
    }

    private IPaletteBorder GetInheritBorder(PaletteBorderStyle style, PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                switch (style)
                {
                    case PaletteBorderStyle.GridDataCellList:
                    case PaletteBorderStyle.GridDataCellSheet:
                    case PaletteBorderStyle.GridDataCellCustom1:
                    case PaletteBorderStyle.GridDataCellCustom2:
                    case PaletteBorderStyle.GridDataCellCustom3:
                        return _grid.StateDisabled.DataCell.Border;
                    case PaletteBorderStyle.GridHeaderColumnList:
                    case PaletteBorderStyle.GridHeaderColumnSheet:
                    case PaletteBorderStyle.GridHeaderColumnCustom1:
                    case PaletteBorderStyle.GridHeaderColumnCustom2:
                    case PaletteBorderStyle.GridHeaderColumnCustom3:
                        return _grid.StateDisabled.HeaderColumn.Border;
                    case PaletteBorderStyle.GridHeaderRowList:
                    case PaletteBorderStyle.GridHeaderRowSheet:
                    case PaletteBorderStyle.GridHeaderRowCustom1:
                    case PaletteBorderStyle.GridHeaderRowCustom2:
                    case PaletteBorderStyle.GridHeaderRowCustom3:
                        return _grid.StateDisabled.HeaderRow.Border;
                }
                break;

            case PaletteState.BoldedOverride:
            case PaletteState.Normal:
                switch (style)
                {
                    case PaletteBorderStyle.GridDataCellList:
                    case PaletteBorderStyle.GridDataCellSheet:
                    case PaletteBorderStyle.GridDataCellCustom1:
                    case PaletteBorderStyle.GridDataCellCustom2:
                    case PaletteBorderStyle.GridDataCellCustom3:
                        return _grid.StateNormal.DataCell.Border;
                    case PaletteBorderStyle.GridHeaderColumnList:
                    case PaletteBorderStyle.GridHeaderColumnSheet:
                    case PaletteBorderStyle.GridHeaderColumnCustom1:
                    case PaletteBorderStyle.GridHeaderColumnCustom2:
                    case PaletteBorderStyle.GridHeaderColumnCustom3:
                        return _grid.StateNormal.HeaderColumn.Border;
                    case PaletteBorderStyle.GridHeaderRowList:
                    case PaletteBorderStyle.GridHeaderRowSheet:
                    case PaletteBorderStyle.GridHeaderRowCustom1:
                    case PaletteBorderStyle.GridHeaderRowCustom2:
                    case PaletteBorderStyle.GridHeaderRowCustom3:
                        return _grid.StateNormal.HeaderRow.Border;
                }
                break;

            case PaletteState.Pressed:
                switch (style)
                {
                    case PaletteBorderStyle.GridHeaderColumnList:
                    case PaletteBorderStyle.GridHeaderColumnSheet:
                    case PaletteBorderStyle.GridHeaderColumnCustom1:
                    case PaletteBorderStyle.GridHeaderColumnCustom2:
                    case PaletteBorderStyle.GridHeaderColumnCustom3:
                        return _grid.StatePressed.HeaderColumn.Border;
                    case PaletteBorderStyle.GridHeaderRowList:
                    case PaletteBorderStyle.GridHeaderRowSheet:
                    case PaletteBorderStyle.GridHeaderRowCustom1:
                    case PaletteBorderStyle.GridHeaderRowCustom2:
                    case PaletteBorderStyle.GridHeaderRowCustom3:
                        return _grid.StatePressed.HeaderRow.Border;
                }
                break;

            case PaletteState.Tracking:
                switch (style)
                {
                    case PaletteBorderStyle.GridHeaderColumnList:
                    case PaletteBorderStyle.GridHeaderColumnSheet:
                    case PaletteBorderStyle.GridHeaderColumnCustom1:
                    case PaletteBorderStyle.GridHeaderColumnCustom2:
                    case PaletteBorderStyle.GridHeaderColumnCustom3:
                        return _grid.StateTracking.HeaderColumn.Border;
                    case PaletteBorderStyle.GridHeaderRowList:
                    case PaletteBorderStyle.GridHeaderRowSheet:
                    case PaletteBorderStyle.GridHeaderRowCustom1:
                    case PaletteBorderStyle.GridHeaderRowCustom2:
                    case PaletteBorderStyle.GridHeaderRowCustom3:
                        return _grid.StateTracking.HeaderRow.Border;
                }
                break;

            case PaletteState.CheckedNormal:
                switch (style)
                {
                    case PaletteBorderStyle.GridDataCellList:
                    case PaletteBorderStyle.GridDataCellSheet:
                    case PaletteBorderStyle.GridDataCellCustom1:
                    case PaletteBorderStyle.GridDataCellCustom2:
                    case PaletteBorderStyle.GridDataCellCustom3:
                        return _grid.StateSelected.DataCell.Border;
                    case PaletteBorderStyle.GridHeaderColumnList:
                    case PaletteBorderStyle.GridHeaderColumnSheet:
                    case PaletteBorderStyle.GridHeaderColumnCustom1:
                    case PaletteBorderStyle.GridHeaderColumnCustom2:
                    case PaletteBorderStyle.GridHeaderColumnCustom3:
                        return _grid.StateSelected.HeaderColumn.Border;
                    case PaletteBorderStyle.GridHeaderRowList:
                    case PaletteBorderStyle.GridHeaderRowSheet:
                    case PaletteBorderStyle.GridHeaderRowCustom1:
                    case PaletteBorderStyle.GridHeaderRowCustom2:
                    case PaletteBorderStyle.GridHeaderRowCustom3:
                        return _grid.StateSelected.HeaderRow.Border;
                }
                break;
        }

        // Should never happen!
        Debug.Assert(false);
        throw DebugTools.NotImplemented(state.ToString());
    }

    private IPaletteContent GetInheritContent(PaletteContentStyle style, PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                switch (style)
                {
                    case PaletteContentStyle.GridDataCellList:
                    case PaletteContentStyle.GridDataCellSheet:
                    case PaletteContentStyle.GridDataCellCustom1:
                    case PaletteContentStyle.GridDataCellCustom2:
                    case PaletteContentStyle.GridDataCellCustom3:
                        return _grid.StateDisabled.DataCell.Content;
                    case PaletteContentStyle.GridHeaderColumnList:
                    case PaletteContentStyle.GridHeaderColumnSheet:
                    case PaletteContentStyle.GridHeaderColumnCustom1:
                    case PaletteContentStyle.GridHeaderColumnCustom2:
                    case PaletteContentStyle.GridHeaderColumnCustom3:
                        return _grid.StateDisabled.HeaderColumn.Content;
                    case PaletteContentStyle.GridHeaderRowList:
                    case PaletteContentStyle.GridHeaderRowSheet:
                    case PaletteContentStyle.GridHeaderRowCustom1:
                    case PaletteContentStyle.GridHeaderRowCustom2:
                    case PaletteContentStyle.GridHeaderRowCustom3:
                        return _grid.StateDisabled.HeaderRow.Content;
                }
                break;

            case PaletteState.Normal:
                switch (style)
                {
                    case PaletteContentStyle.GridDataCellList:
                    case PaletteContentStyle.GridDataCellSheet:
                    case PaletteContentStyle.GridDataCellCustom1:
                    case PaletteContentStyle.GridDataCellCustom2:
                    case PaletteContentStyle.GridDataCellCustom3:
                        return _grid.StateNormal.DataCell.Content;
                    case PaletteContentStyle.GridHeaderColumnList:
                    case PaletteContentStyle.GridHeaderColumnSheet:
                    case PaletteContentStyle.GridHeaderColumnCustom1:
                    case PaletteContentStyle.GridHeaderColumnCustom2:
                    case PaletteContentStyle.GridHeaderColumnCustom3:
                        return _grid.StateNormal.HeaderColumn.Content;
                    case PaletteContentStyle.GridHeaderRowList:
                    case PaletteContentStyle.GridHeaderRowSheet:
                    case PaletteContentStyle.GridHeaderRowCustom1:
                    case PaletteContentStyle.GridHeaderRowCustom2:
                    case PaletteContentStyle.GridHeaderRowCustom3:
                        return _grid.StateNormal.HeaderRow.Content;
                }
                break;

            case PaletteState.Pressed:
                switch (style)
                {
                    case PaletteContentStyle.GridHeaderColumnList:
                    case PaletteContentStyle.GridHeaderColumnSheet:
                    case PaletteContentStyle.GridHeaderColumnCustom1:
                    case PaletteContentStyle.GridHeaderColumnCustom2:
                    case PaletteContentStyle.GridHeaderColumnCustom3:
                        return _grid.StatePressed.HeaderColumn.Content;
                    case PaletteContentStyle.GridHeaderRowList:
                    case PaletteContentStyle.GridHeaderRowSheet:
                    case PaletteContentStyle.GridHeaderRowCustom1:
                    case PaletteContentStyle.GridHeaderRowCustom2:
                    case PaletteContentStyle.GridHeaderRowCustom3:
                        return _grid.StatePressed.HeaderRow.Content;
                }
                break;

            case PaletteState.Tracking:
                switch (style)
                {
                    case PaletteContentStyle.GridHeaderColumnList:
                    case PaletteContentStyle.GridHeaderColumnSheet:
                    case PaletteContentStyle.GridHeaderColumnCustom1:
                    case PaletteContentStyle.GridHeaderColumnCustom2:
                    case PaletteContentStyle.GridHeaderColumnCustom3:
                        return _grid.StateTracking.HeaderColumn.Content;
                    case PaletteContentStyle.GridHeaderRowList:
                    case PaletteContentStyle.GridHeaderRowSheet:
                    case PaletteContentStyle.GridHeaderRowCustom1:
                    case PaletteContentStyle.GridHeaderRowCustom2:
                    case PaletteContentStyle.GridHeaderRowCustom3:
                        return _grid.StateTracking.HeaderRow.Content;
                }
                break;

            case PaletteState.CheckedNormal:
                switch (style)
                {
                    case PaletteContentStyle.GridDataCellList:
                    case PaletteContentStyle.GridDataCellSheet:
                    case PaletteContentStyle.GridDataCellCustom1:
                    case PaletteContentStyle.GridDataCellCustom2:
                    case PaletteContentStyle.GridDataCellCustom3:
                        return _grid.StateSelected.DataCell.Content;
                    case PaletteContentStyle.GridHeaderColumnList:
                    case PaletteContentStyle.GridHeaderColumnSheet:
                    case PaletteContentStyle.GridHeaderColumnCustom1:
                    case PaletteContentStyle.GridHeaderColumnCustom2:
                    case PaletteContentStyle.GridHeaderColumnCustom3:
                        return _grid.StateSelected.HeaderColumn.Content;
                    case PaletteContentStyle.GridHeaderRowList:
                    case PaletteContentStyle.GridHeaderRowSheet:
                    case PaletteContentStyle.GridHeaderRowCustom1:
                    case PaletteContentStyle.GridHeaderRowCustom2:
                    case PaletteContentStyle.GridHeaderRowCustom3:
                        return _grid.StateSelected.HeaderRow.Content;
                }
                break;
        }

        // Should never happen!
        Debug.Assert(false);
        throw DebugTools.NotImplemented(state.ToString());
    }
    #endregion
}