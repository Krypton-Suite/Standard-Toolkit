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
/// Implement storage for palette background details.
/// </summary>
public class PaletteBackColor1 : PaletteBack
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBackColor1 class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteBackColor1(IPaletteBack inherit,
        NeedPaintHandler needPaint)
        : base(inherit, needPaint)
    {
    }
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public new void PopulateFromBase(PaletteState state) =>
        // Get the values and set into storage
        Color1 = GetBackColor1(state);
    #endregion

    #region Draw
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    [Browsable(false)]
    public new InheritBool Draw
    {
        get => base.Draw;
        set => base.Draw = value;
    }
    #endregion

    #region GraphicsHint
    /// <summary>
    /// Gets the graphics hint for drawing the background.
    /// </summary>
    [Browsable(false)]
    public new PaletteGraphicsHint GraphicsHint
    {
        get => base.GraphicsHint;
        set => base.GraphicsHint = value;
    }
    #endregion

    #region Color2
    /// <summary>
    /// Gets and sets the second background color.
    /// </summary>
    [Browsable(false)]
    public new Color Color2
    {
        get => base.Color2;
        set => base.Color2 = value;
    }
    #endregion

    #region ColorStyle
    /// <summary>
    /// Gets and sets the color drawing style.
    /// </summary>
    [Browsable(false)]
    public new PaletteColorStyle ColorStyle
    {
        get => base.ColorStyle;
        set => base.ColorStyle = value;
    }
    #endregion

    #region ColorAlign
    /// <summary>
    /// Gets and set the color alignment.
    /// </summary>
    [Browsable(false)]
    public new PaletteRectangleAlign ColorAlign
    {
        get => base.ColorAlign;
        set => base.ColorAlign = value;
    }
    #endregion

    #region ColorAngle
    /// <summary>
    /// Gets and sets the color angle.
    /// </summary>
    [Browsable(false)]
    public new float ColorAngle
    {
        get => base.ColorAngle;
        set => base.ColorAngle = value;
    }
    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the background image.
    /// </summary>
    [Browsable(false)]
    public new Image? Image
    {
        get => base.Image;
        set => base.Image = value;
    }
    #endregion

    #region ImageStyle
    /// <summary>
    /// Gets and sets the background image style.
    /// </summary>
    [Browsable(false)]
    public new PaletteImageStyle ImageStyle
    {
        get => base.ImageStyle;
        set => base.ImageStyle = value;
    }
    #endregion

    #region ImageAlign
    /// <summary>
    /// Gets and set the image alignment.
    /// </summary>
    [Browsable(false)]
    public new PaletteRectangleAlign ImageAlign
    {
        get => base.ImageAlign;
        set => base.ImageAlign = value;
    }
    #endregion
}