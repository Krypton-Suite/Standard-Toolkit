namespace Krypton.Ribbon
{
    internal class DesignTimeDraw
    {
        #region Static Fields

        private const int DESIGN_FLAP_WIDTH = 12;
        private const int DESIGN_SEP_WIDTH = 6;

        #endregion

        #region FlapWidth
        /// <summary>
        /// Gets the width of the design time flap.
        /// </summary>
        public static int FlapWidth => DESIGN_FLAP_WIDTH;

        #endregion

        #region SepWidth
        /// <summary>
        /// Gets the width of the design time separation.
        /// </summary>
        public static int SepWidth => DESIGN_SEP_WIDTH;

        #endregion

        #region DrawArea
        /// <summary>
        /// Draw a design area with a flap on the left hand edge.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="clientRect">Client rectangle of the source view.</param>
        /// <param name="state">State of element.</param>
        public static void DrawArea(KryptonRibbon ribbon,
                                    RenderContext context,
                                    Rectangle clientRect,
                                    PaletteState state)
        {
            Color c = state == PaletteState.Normal
                ? ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorDark(PaletteState.Normal)
                : ribbon.StateCommon.RibbonGroupButton.Back.GetBackColor1(PaletteState.Tracking);

            // Draw entire area in color
            using SolidBrush darkBrush = new(c);
            context.Graphics.FillRectangle(darkBrush, clientRect);
        }
        #endregion
 
        #region DrawFlapArea
        /// <summary>
        /// Draw a design area with a flap on the left hand edge.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="clientRect">Client rectangle of the source view.</param>
        /// <param name="state">State of element.</param>
        public static void DrawFlapArea(KryptonRibbon ribbon,
                                        RenderContext context,
                                        Rectangle clientRect,
                                        PaletteState state)
        {
            Color c = state == PaletteState.Normal
                ? ControlPaint.Dark(ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorDark(PaletteState.Normal))
                : ribbon.StateCommon.RibbonGroupButton.Back.GetBackColor1(PaletteState.Tracking);

            // Draw border around entire area
            Rectangle drawRect = clientRect;
            drawRect.Width -= DESIGN_SEP_WIDTH;
            drawRect.Height--;
            drawRect.X++;
            using (Pen darkPen = new(c))
            {
                context.Graphics.DrawRectangle(darkPen, drawRect);
            }

            // Draw the flap in the dark color
            drawRect.Width = DESIGN_FLAP_WIDTH - 2;
            using (SolidBrush darkBrush = new(c))
            {
                context.Graphics.FillRectangle(darkBrush, drawRect);
            }
        }
        #endregion
    }
}
