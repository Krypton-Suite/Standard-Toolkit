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


namespace Krypton.Ribbon
{
    /// <summary>
    /// Allocate space for the location of the composition caption area.
    /// </summary>
    internal class ViewDrawRibbonComposition : ViewLeaf,
                                               IKryptonComposition
    {
        #region Instance Fields
        private readonly int CONSTANT_COMPOSITION_HEIGHT;
        private readonly KryptonRibbon _ribbon;
        private VisualForm _ownerForm;
        private readonly NeedPaintHandler _needPaint;
        private readonly Blend _compBlend;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonComposition class.
        /// </summary>
        /// <param name="ribbon">Owning control instance.</param>
        /// <param name="needPaint">Delegate for requested a paint.</param>
        public ViewDrawRibbonComposition(KryptonRibbon ribbon,
                                         NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(needPaint != null);

            CONSTANT_COMPOSITION_HEIGHT = (int)(30 * FactorDpiY);

            _ribbon = ribbon;
            _needPaint = needPaint;

            _compBlend = new Blend
            {
                Positions = new[] { 0.0f, 0.25f, 1.0f },
                Factors = new[] { 0.0f, 0.0f, 0.40f }
            };
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonComposition:" + Id;

        #endregion

        #region CompHeight
        /// <summary>
        /// Gets the pixel height of the composition extension into the client area.
        /// </summary>
        public int CompHeight 
        {
            get 
            {
                if ((_ribbon.RibbonShape == PaletteRibbonShape.Office2010) && _ribbon.MainPanel.Visible)
                {
                    return _ribbon.TabsArea.ClientHeight + CONSTANT_COMPOSITION_HEIGHT;
                }
                else
                {
                    return CONSTANT_COMPOSITION_HEIGHT;
                }
            }
        }
        #endregion

        #region CompRightBorder
        /// <summary>
        /// Gets and sets the associated right border for composition layout.
        /// </summary>
        public ViewDrawRibbonCompoRightBorder CompRightBorder { get; set; }

        #endregion

        #region CompHandle
        /// <summary>
        /// Gets the handle of the composition element control.
        /// </summary>
        public IntPtr CompHandle => _ribbon.Handle;

        #endregion

        #region CompVisible
        /// <summary>
        /// Gets and sets the visible state.
        /// </summary>
        public bool CompVisible
        {
            get => Visible;
            set => Visible = value;
        }
        #endregion

        #region CompOwnerForm
        /// <summary>
        /// Gets and sets the owner form to use when compositing.
        /// </summary>
        public VisualForm CompOwnerForm
        {
            get => _ownerForm;

            set 
            { 
                _ownerForm = value;
                CompRightBorder.CompOwnerForm = value;
            }
        }
        #endregion

        #region CompNeedPaint
        /// <summary>
        /// Request a repaint and optional layout.
        /// </summary>
        /// <param name="needLayout">Is a layout required.</param>
        public void CompNeedPaint(bool needLayout)
        {
            // Pass request onto the ribbon instance
            _needPaint(this, new NeedLayoutEventArgs(needLayout));
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context) => new (0, CONSTANT_COMPOSITION_HEIGHT);

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            Rectangle contextRect = ClientRectangle;

            // Use the entire height of the control and not just the composition height
            contextRect.Height = context.TopControl.Height;
            
            // Make bigger by the left and right borders, so that the application button is shifted
            // to match up with the client area of the actual ribbon control in the client area
            Padding realBorders = _ownerForm.RealWindowBorders;
            contextRect.X -= realBorders.Left;
            contextRect.Width += realBorders.Horizontal;

            context.DisplayRectangle = contextRect;

            // Ask the integrated form to perform layout in our area
            _ownerForm.WindowChromeCompositionLayout(context, ClientRectangle);

            // Put back the original display rectangle
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            Debug.Assert(_ownerForm != null);

            // Ask the owning form to perform rendering in this element
            _ownerForm.WindowChromeCompositionPaint(context);
        }
        #endregion
    }
}
