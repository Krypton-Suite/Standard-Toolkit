// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System.Drawing;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
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
                                NeedPaintHandler needPaint)
            : base(inherit, needPaint)
        {
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => ((Draw == InheritBool.Inherit) &&
                                           (GraphicsHint == PaletteGraphicsHint.Inherit) &&
                                           (Color1 == Color.Empty) &&
                                           (Color2 == Color.Empty) &&
                                           (ColorStyle == PaletteColorStyle.Inherit) &&
                                           (ColorAlign == PaletteRectangleAlign.Inherit) &&
                                           (ColorAngle == -1) &&
                                           (Width == -1) &&
                                           (Image == null) &&
                                           (ImageStyle == PaletteImageStyle.Inherit) &&
                                           (ImageAlign == PaletteRectangleAlign.Inherit));

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
        public new int Rounding
        {
            get => base.Rounding;
            set => base.Rounding = value;
        }
        #endregion
    }
}
