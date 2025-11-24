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
/// Override the colors to provide lighter versions.
/// </summary>
public class PaletteBackLightenColors : PaletteBackInherit
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBackLightenColors class.
    /// </summary>
    /// <param name="inherit"></param>
    public PaletteBackLightenColors(IPaletteBack inherit) => Inherit = inherit;

    #endregion

    #region Inherit
    /// <summary>
    /// Gets and sets the palette to inherit from.
    /// </summary>
    public IPaletteBack Inherit { get; set; }

    #endregion

    #region IPaletteBack
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBackDraw(PaletteState state) => Inherit.GetBackDraw(state);

    /// <summary>
    /// Gets the graphics drawing hint.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) => Inherit.GetBackGraphicsHint(state);

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteState state) => CommonHelper.MergeColors(Inherit.GetBackColor1(state), 0.4f, Color.White, 0.6f);

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteState state) => CommonHelper.MergeColors(Inherit.GetBackColor2(state), 0.4f, Color.White, 0.6f);

    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteState state) => Inherit.GetBackColorStyle(state);

    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteState state) => Inherit.GetBackColorAlign(state);

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteState state) => Inherit.GetBackColorAngle(state);

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteState state) => Inherit.GetBackImage(state);

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteState state) => Inherit.GetBackImageStyle(state);

    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteState state) => Inherit.GetBackImageAlign(state);

    #endregion
}