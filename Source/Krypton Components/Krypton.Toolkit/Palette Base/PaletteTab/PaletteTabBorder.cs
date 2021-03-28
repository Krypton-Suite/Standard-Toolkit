using System.Drawing;
using System.ComponentModel;

namespace Krypton.Toolkit
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
