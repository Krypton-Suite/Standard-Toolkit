#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Exposes a palette source for drawing a border.
    /// </summary>
    public interface IPaletteBorder
    {
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        InheritBool GetBorderDraw(PaletteState state);

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        PaletteDrawBorders GetBorderDrawBorders(PaletteState state);

        /// <summary>
        /// Gets the graphics drawing hint.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state);

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetBorderColor1(PaletteState state);

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetBorderColor2(PaletteState state);

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        PaletteColorStyle GetBorderColorStyle(PaletteState state);

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        PaletteRectangleAlign GetBorderColorAlign(PaletteState state);

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        float GetBorderColorAngle(PaletteState state);

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer width.</returns>
        int GetBorderWidth(PaletteState state);

        /// <summary>
        /// Gets the border corner rounding.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer rounding.</returns>
        float GetBorderRounding(PaletteState state);

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        Image? GetBorderImage(PaletteState state);

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        PaletteImageStyle GetBorderImageStyle(PaletteState state);

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        PaletteRectangleAlign GetBorderImageAlign(PaletteState state);
    }
}