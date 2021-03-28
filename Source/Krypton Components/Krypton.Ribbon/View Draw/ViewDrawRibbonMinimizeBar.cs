using System.Drawing;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws a separator at the bottom of the tabs when ribbon minimized.
    /// </summary>
    internal class ViewDrawRibbonMinimizeBar : ViewLayoutRibbonSeparator
    {
        #region Static Fields

        private const int SEP_WIDTH = 2;

        #endregion

        #region Instance Fields
        private readonly IPaletteRibbonGeneral _palette;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonMinimizeBar class.
        /// </summary>
        /// <param name="palette">Source for palette values.</param>
        public ViewDrawRibbonMinimizeBar(IPaletteRibbonGeneral palette)
            : base(SEP_WIDTH, true)
        {
            Debug.Assert(palette != null);
            _palette = palette;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonMinimizeBar:" + Id;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            using (Pen darkPen = new Pen(_palette.GetRibbonMinimizeBarDark(PaletteState.Normal)),
                       lightPen = new Pen(_palette.GetRibbonMinimizeBarLight(PaletteState.Normal)))
            {
                context.Graphics.DrawLine(darkPen, ClientRectangle.Left, ClientRectangle.Bottom - 2, ClientRectangle.Right - 1, ClientRectangle.Bottom - 2);
                context.Graphics.DrawLine(lightPen, ClientRectangle.Left, ClientRectangle.Bottom - 1, ClientRectangle.Right - 1, ClientRectangle.Bottom - 1);
            }
        }
        #endregion
    }
}
