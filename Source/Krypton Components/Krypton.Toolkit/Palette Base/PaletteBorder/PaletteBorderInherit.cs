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
/// Provide inheritance of palette border properties.
/// </summary>
public abstract class PaletteBorderInherit : GlobalId,
    IPaletteBorder
{
    #region IPaletteBorder
    /// <summary>
    /// Gets a value indicating if border should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public abstract InheritBool GetBorderDraw(PaletteState state);

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public abstract PaletteDrawBorders GetBorderDrawBorders(PaletteState state);

    /// <summary>
    /// Gets the graphics drawing hint.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public abstract PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state);

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetBorderColor1(PaletteState state);

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetBorderColor2(PaletteState state);

    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public abstract PaletteColorStyle GetBorderColorStyle(PaletteState state);

    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public abstract PaletteRectangleAlign GetBorderColorAlign(PaletteState state);

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public abstract float GetBorderColorAngle(PaletteState state);

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border width.</returns>
    public abstract int GetBorderWidth(PaletteState state);

    /// <summary>
    /// Gets the border rounding.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border rounding.</returns>
    public abstract float GetBorderRounding(PaletteState state);

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public abstract Image? GetBorderImage(PaletteState state);

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public abstract PaletteImageStyle GetBorderImageStyle(PaletteState state);

    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public abstract PaletteRectangleAlign GetBorderImageAlign(PaletteState state);
    #endregion
}