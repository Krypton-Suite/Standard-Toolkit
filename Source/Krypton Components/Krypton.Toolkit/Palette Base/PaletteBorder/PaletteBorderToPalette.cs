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
/// Redirect all border requests directly to the palette instance.
/// </summary>
public class PaletteBorderToPalette : IPaletteBorder
{
    #region Instance Fields
    private readonly PaletteBase _palette;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBorderToPalette class.
    /// </summary>
    /// <param name="palette">Source for getting all values.</param>
    /// <param name="style">Style of values required.</param>
    public PaletteBorderToPalette(PaletteBase palette,
        PaletteBorderStyle style)
    {
        // Remember inheritance
        _palette = palette;
        BorderStyle = style;
    }
    #endregion

    #region BorderStyle
    /// <summary>
    /// Gets and sets the fixed border style.
    /// </summary>
    public PaletteBorderStyle BorderStyle { get; set; }

    #endregion

    #region Draw
    /// <summary>
    /// Gets the actual border draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetBorderDraw(PaletteState state) => _palette.GetBorderDraw(BorderStyle, state);

    #endregion

    #region DrawBorders
    /// <summary>
    /// Gets the actual borders to draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public PaletteDrawBorders GetBorderDrawBorders(PaletteState state) => _palette.GetBorderDrawBorders(BorderStyle, state);

    #endregion

    #region GraphicsHint
    /// <summary>
    /// Gets the actual border graphics hint value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state) => _palette.GetBorderGraphicsHint(BorderStyle, state);

    #endregion

    #region Color1
    /// <summary>
    /// Gets the actual first border color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBorderColor1(PaletteState state) => _palette.GetBorderColor1(BorderStyle, state);

    #endregion

    #region Color2
    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBorderColor2(PaletteState state) => _palette.GetBorderColor2(BorderStyle, state);

    #endregion

    #region ColorStyle
    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetBorderColorStyle(PaletteState state) => _palette.GetBorderColorStyle(BorderStyle, state);

    #endregion

    #region ColorAlign
    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetBorderColorAlign(PaletteState state) => _palette.GetBorderColorAlign(BorderStyle, state);

    #endregion

    #region ColorAngle
    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetBorderColorAngle(PaletteState state) => _palette.GetBorderColorAngle(BorderStyle, state);

    #endregion

    #region Width
    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border width.</returns>
    public int GetBorderWidth(PaletteState state) => _palette.GetBorderWidth(BorderStyle, state);

    #endregion

    #region Rounding
    /// <summary>
    /// Gets the border rounding.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border rounding.</returns>
    public float GetBorderRounding(PaletteState state) => _palette.GetBorderRounding(BorderStyle, state);
    #endregion

    #region Image
    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetBorderImage(PaletteState state) => _palette.GetBorderImage(BorderStyle, state);

    #endregion

    #region ImageStyle
    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetBorderImageStyle(PaletteState state) => _palette.GetBorderImageStyle(BorderStyle, state);

    #endregion

    #region ImageAlign
    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetBorderImageAlign(PaletteState state) => _palette.GetBorderImageAlign(BorderStyle, state);

    #endregion
}