namespace Krypton.Ribbon
{
    /// <summary>
    /// Base class for drawing an image in the specified size and state.
    /// </summary>
    internal abstract class ViewDrawRibbonGroupImageBase : ViewLeaf
                                              
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupImageBase class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        public ViewDrawRibbonGroupImageBase(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);
            Ribbon = ribbon;
        }        

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonGroupImageBase:" + Id;

        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context) => DrawSize;

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Take on all the provided area
            ClientRectangle = context.DisplayRectangle;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            if (DrawImage != null)
            {
                if (Enabled)
                {
                    context.Graphics.DrawImage(DrawImage, ClientRectangle);
                }
                else
                {
                    // Have to rescale the image when drawing, so need to use own
                    // mechanism for the converting of the image to a disabled one
                    using ImageAttributes attribs = new();
                    // Use attributes to wash out the color to look disabled
                    attribs.SetColorMatrix(CommonHelper.MatrixDisabled);

                    context.Graphics.DrawImage(DrawImage, ClientRectangle,
                        0, 0, DrawImage.Width, DrawImage.Height,
                        GraphicsUnit.Pixel, attribs);
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the owning ribbon control.
        /// </summary>
        protected KryptonRibbon Ribbon { get; }

        /// <summary>
        /// Gets the size to draw the image.
        /// </summary>
        protected abstract Size DrawSize { get; }

        /// <summary>
        /// Gets the image to be drawn.
        /// </summary>
        protected abstract Image DrawImage { get; }
        #endregion
    }
}
