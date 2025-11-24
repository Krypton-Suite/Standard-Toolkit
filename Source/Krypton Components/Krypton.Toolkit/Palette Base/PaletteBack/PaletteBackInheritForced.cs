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
/// Allow the background values to be forced to fixed values.
/// </summary>
public class PaletteBackInheritForced : PaletteBackInherit
{
    #region Instance Fields
    private IPaletteBack _inherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBackInheritForced class.
    /// </summary>
    /// <param name="inherit">Background palette to inherit from.</param>
    public PaletteBackInheritForced([DisallowNull] IPaletteBack inherit)
    {
        Debug.Assert(inherit is not null);

        // Remember inheritance border
        _inherit = inherit!;

        // Default values
        BorderIgnoreNormal = false;
        ForceGraphicsHint = PaletteGraphicsHint.Inherit;
    }
    #endregion

    #region SetInherit
    /// <summary>
    /// Gets and sets the palette to inherit from.
    /// </summary>
    public void SetInherit([DisallowNull] IPaletteBack paletteBack)
    {
        Debug.Assert(paletteBack != null);
        _inherit = paletteBack!;
    }
    #endregion

    #region BorderIgnoreNormal
    /// <summary>
    /// Gets and sets the ignoring of normal borders.
    /// </summary>
    public bool BorderIgnoreNormal { get; set; }

    #endregion

    #region ForceGraphicsHint
    /// <summary>
    /// Gets and sets the forced value for the graphics hint.
    /// </summary>
    public PaletteGraphicsHint ForceGraphicsHint { get; set; }

    #endregion

    #region ForceDraw
    /// <summary>
    /// Gets and sets the forced value for the draw property.
    /// </summary>
    public InheritBool ForceDraw { get; set; }

    #endregion

    #region IPaletteBack
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBackDraw(PaletteState state)
    {
        if (ForceDraw != InheritBool.Inherit)
        {
            return ForceDraw;
        }
        else
        {
            return BorderIgnoreNormal && (state == PaletteState.Normal) ? InheritBool.False : _inherit.GetBackDraw(state);
        }
    }

    /// <summary>
    /// Gets the graphics drawing hint.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) =>
        ForceGraphicsHint != PaletteGraphicsHint.Inherit ? ForceGraphicsHint : _inherit.GetBackGraphicsHint(state);

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteState state) => _inherit.GetBackColor1(state);

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteState state) => _inherit.GetBackColor2(state);

    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteState state) => _inherit.GetBackColorStyle(state);

    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteState state) => _inherit.GetBackColorAlign(state);

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteState state) => _inherit.GetBackColorAngle(state);

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteState state) => _inherit.GetBackImage(state);

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteState state) => _inherit.GetBackImageStyle(state);

    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteState state) => _inherit.GetBackImageAlign(state);
    #endregion
}