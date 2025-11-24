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
/// Provide inheritance of palette back properties.
/// </summary>
internal class PaletteDataGridViewBackInherit : PaletteBackInherit
{
    #region Instance Fields
    private IPaletteBack _inherit;
    private DataGridViewCellStyle _cellStyle;
    #endregion

    #region SetInherit
    /// <summary>
    /// Defines the interface to inherit from.
    /// </summary>
    /// <param name="inherit">Palette to inherit values from.</param>
    /// <param name="cellStyle">Cell style to recover values from.</param>
    public void SetInherit([DisallowNull] IPaletteBack inherit,
        [DisallowNull] DataGridViewCellStyle cellStyle)
    {
        Debug.Assert(inherit != null);
        Debug.Assert(cellStyle != null);

        _inherit = inherit!;
        _cellStyle = cellStyle!;
    }
    #endregion

    #region IPaletteBack
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBackDraw(PaletteState state) => _inherit.GetBackDraw(state);

    /// <summary>
    /// Gets the graphics drawing hint.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) => _inherit.GetBackGraphicsHint(state);

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteState state) => state switch
    {
        PaletteState.Normal => _cellStyle.BackColor,
        PaletteState.CheckedNormal => _cellStyle.SelectionBackColor,
        _ => _inherit.GetBackColor1(state)
    };

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