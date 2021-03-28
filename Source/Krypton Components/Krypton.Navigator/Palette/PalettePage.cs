using System.ComponentModel;
using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Implement storage for page appearance.
    /// </summary>
    public class PalettePage : PaletteDouble
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PalettePage class.
        /// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PalettePage(IPaletteDouble inherit,
                           NeedPaintHandler needPaint)
            : base(inherit, needPaint)
        {
        }
        #endregion

        #region Border
        /// <summary>
        /// Gets access to the border palette details.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteBorder Border => base.Border;

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IPaletteBorder PaletteBorder => base.PaletteBorder;

        #endregion
    }
}
