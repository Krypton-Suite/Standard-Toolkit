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
/// Redirect all background requests directly to the palette instance.
/// </summary>
public class PaletteBackToPalette : IPaletteBack
{
    #region Instance Fields
    private readonly PaletteBase _palette;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBack class.
    /// </summary>
    /// <param name="palette">Source for getting all values.</param>
    /// <param name="style">Style of values required.</param>
    public PaletteBackToPalette(PaletteBase palette, PaletteBackStyle style)
    {
        // Remember source palette
        _palette = palette;
        BackStyle = style;
    }
    #endregion

    #region BackStyle
    /// <summary>
    /// Gets and sets the fixed background style.
    /// </summary>
    public PaletteBackStyle BackStyle { get; set; }

    #endregion

    #region Draw
    /// <summary>
    /// Gets the actual background draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetBackDraw(PaletteState state) => _palette.GetBackDraw(BackStyle, state);

    #endregion

    #region GraphicsHint
    /// <summary>
    /// Gets the actual background graphics hint value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) => _palette.GetBackGraphicsHint(BackStyle, state);

    #endregion

    #region Color1
    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBackColor1(PaletteState state) => _palette.GetBackColor1(BackStyle, state);

    #endregion

    #region Color2
    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBackColor2(PaletteState state) => _palette.GetBackColor2(BackStyle, state);

    #endregion

    #region ColorStyle
    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetBackColorStyle(PaletteState state) => _palette.GetBackColorStyle(BackStyle, state);

    #endregion

    #region ColorAlign
    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetBackColorAlign(PaletteState state) => _palette.GetBackColorAlign(BackStyle, state);

    #endregion

    #region ColorAngle
    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetBackColorAngle(PaletteState state) => _palette.GetBackColorAngle(BackStyle, state);

    #endregion

    #region Image
    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetBackImage(PaletteState state) => _palette.GetBackImage(BackStyle, state);

    #endregion

    #region ImageStyle
    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetBackImageStyle(PaletteState state) => _palette.GetBackImageStyle(BackStyle, state);

    #endregion

    #region ImageAlign
    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetBackImageAlign(PaletteState state) => _palette.GetBackImageAlign(BackStyle, state);

    #endregion
}