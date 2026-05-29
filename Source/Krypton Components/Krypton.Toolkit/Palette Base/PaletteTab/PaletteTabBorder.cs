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
/// Implement storage for palette tab border details.
/// </summary>
public class PaletteTabBorder : PaletteBorder
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteTabBorder class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteTabBorder(IPaletteBorder inherit,
        NeedPaintHandler? needPaint)
        : base(inherit, needPaint)
    {
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Draw == InheritBool.Inherit) &&
                                      (GraphicsHint == PaletteGraphicsHint.Inherit) &&
                                      (Color1 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (Color2 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (ColorStyle == PaletteColorStyle.Inherit) &&
                                      (ColorAlign == PaletteRectangleAlign.Inherit) &&
                                      (ColorAngle == -1) &&
                                      (Width == -1) &&
                                      (Image == null) &&
                                      (ImageStyle == PaletteImageStyle.Inherit) &&
                                      (ImageAlign == PaletteRectangleAlign.Inherit);

    #endregion

    #region DrawBorders
    /// <summary>
    /// Gets a value indicating which borders should be drawn.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new PaletteDrawBorders DrawBorders
    {
        get => base.DrawBorders;
        set => base.DrawBorders = value;
    }
    #endregion

    #region Rounding
    /// <summary>
    /// Gets and sets the border rounding.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new float Rounding
    {
        get => base.Rounding;
        set => base.Rounding = value;
    }
    #endregion
}