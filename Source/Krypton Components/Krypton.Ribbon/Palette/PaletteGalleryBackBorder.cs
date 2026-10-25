#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class PaletteGalleryBackBorder : IPaletteBack,
    IPaletteBorder
{
    #region Instance Fields
    private PaletteGalleryState _state;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteGalleryBackBorder class.
    /// </summary>
    /// <param name="state">Initial state for background/border.</param>
    public PaletteGalleryBackBorder([DisallowNull] PaletteGalleryState? state)
    {
        Debug.Assert(state is not null);
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }
    #endregion

    #region SetState
    /// <summary>
    /// Define the new state to use for sourcing values.
    /// </summary>
    /// <param name="state">New state for background/border.</param>
    public void SetState([DisallowNull] PaletteGalleryState state)
    {
        Debug.Assert(state != null);
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }
    #endregion

    #region IPaletteBack
    /// <summary>
    /// Gets the actual background draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetBackDraw(PaletteState state) => InheritBool.True;

    /// <summary>
    /// Gets the actual background graphics hint value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) => PaletteGraphicsHint.AntiAlias;

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBackColor1(PaletteState state) => _state!.RibbonGalleryBack.GetRibbonBackColor1(state);

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBackColor2(PaletteState state) => _state!.RibbonGalleryBack.GetRibbonBackColor2(state);

    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetBackColorStyle(PaletteState state) => PaletteColorStyle.Solid;

    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetBackColorAlign(PaletteState state) => PaletteRectangleAlign.Local;

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetBackColorAngle(PaletteState state) => 0f;

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetBackImage(PaletteState state) => null;

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetBackImageStyle(PaletteState state) => PaletteImageStyle.Stretch;

    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetBackImageAlign(PaletteState state) => PaletteRectangleAlign.Local;

    #endregion

    #region IPaletteBorder
    /// <summary>
    /// Gets a value indicating if border should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetBorderDraw(PaletteState state) => InheritBool.True;

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public PaletteDrawBorders GetBorderDrawBorders(PaletteState state) => PaletteDrawBorders.TopBottomLeft;

    /// <summary>
    /// Gets the graphics drawing hint.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state) => PaletteGraphicsHint.AntiAlias;

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBorderColor1(PaletteState state) => _state!.RibbonGalleryBorder.GetRibbonBackColor1(state);

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBorderColor2(PaletteState state) => _state!.RibbonGalleryBorder.GetRibbonBackColor2(state);

    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetBorderColorStyle(PaletteState state) => PaletteColorStyle.Solid;

    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetBorderColorAlign(PaletteState state) => PaletteRectangleAlign.Local;

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetBorderColorAngle(PaletteState state) => 0f;

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border width.</returns>
    public int GetBorderWidth(PaletteState state) => 1;

    /// <summary>
    /// Gets the border rounding.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border rounding.</returns>
    public float GetBorderRounding(PaletteState state) => 0;

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetBorderImage(PaletteState state) => null;

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetBorderImageStyle(PaletteState state) => PaletteImageStyle.Stretch;

    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetBorderImageAlign(PaletteState state) => PaletteRectangleAlign.Local;

    #endregion
}