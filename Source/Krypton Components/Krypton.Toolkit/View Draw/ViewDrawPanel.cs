#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// View element that can draw a panel (background but no border)
    /// </summary>
    public class ViewDrawPanel : ViewComposite
    {
        #region Instance Fields
        internal IPaletteBack _paletteBack;
        private IDisposable _memento;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawPanel class.
        /// </summary>
        public ViewDrawPanel()
        {
            VisualOrientation = VisualOrientation.Top;
            IgnoreRender = false;
        }
        
        /// <summary>
        /// Initialize a new instance of the ViewDrawPanel class.
        /// </summary>
        /// <param name="paletteBack">Palette source for the background.</param>        
        public ViewDrawPanel(IPaletteBack paletteBack)
        {
            Debug.Assert(paletteBack != null);
            _paletteBack = paletteBack;
            VisualOrientation = VisualOrientation.Top;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            "ViewDrawPanel:" + Id;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_memento != null)
                {
                    _memento.Dispose();
                    _memento = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion
        
        #region IgnoreRender
        /// <summary>
        /// Gets and sets the rendering status.
        /// </summary>
        public bool IgnoreRender { get; set; }

        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the orientation of the panel.
        /// </summary>
        public VisualOrientation VisualOrientation { get; set; }

        #endregion

        #region SetPalettes
        /// <summary>
        /// Update the source palettes for drawing.
        /// </summary>
        /// <param name="paletteBack">Palette source for the background.</param>        
        public void SetPalettes(IPaletteBack paletteBack)
        {
            Debug.Assert(paletteBack != null);

            // Use newly provided palettes
            _paletteBack = paletteBack;
        }

        /// <summary>
        /// Gets the palette used for drawing the panel.
        /// </summary>
        /// <returns></returns>
        public IPaletteBack GetPalette() => _paletteBack;

        #endregion

        #region Eval
        /// <summary>
        /// Evaluate the need for drawing transparent areas.
        /// </summary>
        /// <param name="context">Evaluation context.</param>
        /// <returns>True if transparent areas exist; otherwise false.</returns>
        public override bool EvalTransparentPaint(ViewContext context)
        {
            Debug.Assert(context != null);

            // Ask the renderer to evaluate the given palette
            return context.Renderer.EvalTransparentPaint(_paletteBack, State);
        }
        #endregion

        #region Layout

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Let child elements layout
            base.Layout(context);
        }
        #endregion

        #region Paint

        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void RenderBefore(RenderContext context) 
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!IgnoreRender)
            {
                // Do we need to draw the background?
                if (_paletteBack.GetBackDraw(State) == InheritBool.True)
                {
                    // Render the background
                    using GraphicsPath panelPath = new();
                    // The path encloses the entire panel area
                    panelPath.AddRectangle(ClientRectangle);

                    // Perform actual panel drawing
                    _memento = context.Renderer.RenderStandardBack.DrawBack(context, ClientRectangle, panelPath, _paletteBack, VisualOrientation, State, _memento);
                }
            }
        }
        #endregion
    }
}
