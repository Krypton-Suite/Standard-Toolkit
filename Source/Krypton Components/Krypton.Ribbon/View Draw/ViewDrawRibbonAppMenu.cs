namespace Krypton.Ribbon
{
    /// <summary>
    /// Extends the ViewDrawDocker by drawing the ribbon app menu area.
    /// </summary>
    internal class ViewDrawRibbonAppMenu : ViewDrawDocker
    {
        #region Instance Fields
        private readonly ViewBase _fixedElement;
        private readonly Rectangle _fixedScreenRect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonAppMenu class.
        /// </summary>
        /// <param name="paletteBack">Palette source for the background.</param>        
        /// <param name="paletteBorder">Palette source for the border.</param>
        /// <param name="fixedElement">Element to display at provided screen rect.</param>
        /// <param name="fixedScreenRect">Screen rectangle for showing the element at.</param>
        public ViewDrawRibbonAppMenu(IPaletteBack paletteBack,
                                     IPaletteBorder paletteBorder,
                                     ViewBase fixedElement,
                                     Rectangle fixedScreenRect)
            : base(paletteBack, paletteBorder)
        {
            _fixedElement = fixedElement;
            _fixedScreenRect = fixedScreenRect;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonAppMenu:" + Id;

        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering after child elements are rendered.
        /// </summary>
        /// <param name="renderContext">Rendering context.</param>
        public override void RenderAfter(RenderContext renderContext)
        {
            base.RenderAfter(renderContext);

            // Convert our rectangle to the screen
            Rectangle screenRect = renderContext.TopControl.RectangleToScreen(renderContext.TopControl.ClientRectangle);

            // If the fixed rectangle is in our showing area and at the top
            if (screenRect.Contains(_fixedScreenRect) && (screenRect.Y == _fixedScreenRect.Y))
            {
                // Position the element appropriately
                using (ViewLayoutContext layoutContext = new(renderContext.Control, renderContext.Renderer))
                {
                    layoutContext.DisplayRectangle = renderContext.TopControl.RectangleToClient(_fixedScreenRect);
                    _fixedElement.Layout(layoutContext);
                }

                // Now draw
                _fixedElement.Render(renderContext);
            }
        }
        #endregion
    }
}
