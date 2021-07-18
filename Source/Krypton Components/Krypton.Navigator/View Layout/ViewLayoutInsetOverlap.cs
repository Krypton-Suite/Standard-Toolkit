#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Navigator
{
    /// <summary>
    /// View element that insets children by the border rounding value of a source.
    /// </summary>
    internal class ViewLayoutInsetOverlap : ViewComposite
    {
        #region Instance Fields
        private readonly ViewDrawCanvas _drawCanvas;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutInsetOverlap class.
        /// </summary>
        public ViewLayoutInsetOverlap(ViewDrawCanvas drawCanvas)
        {
            Debug.Assert(drawCanvas != null);

            // Remember source of the rounding values
            _drawCanvas = drawCanvas;

            // Default other state
            Orientation = VisualOrientation.Top;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutInsetForRounding:" + Id;
        }
        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the bar orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            [DebuggerStepThrough]
            get;
            set;
        }

        #endregion

        #region Rounding
        /// <summary>
        /// Gets the rounding value to apply on the edges.
        /// </summary>
        public float Rounding
        {
            get
            {
                // Get the rounding and width values for the border
                float rounding = _drawCanvas.PaletteBorder.GetBorderRounding(_drawCanvas.State);
                int width = _drawCanvas.PaletteBorder.GetBorderWidth(_drawCanvas.State);

                // We have to add half the width as that increases the rounding effect
                return rounding + (width / 2);
            }
        }
        #endregion

        #region BorderWidth
        /// <summary>
        /// Gets the rounding value to apply on the edges.
        /// </summary>
        public int BorderWidth => _drawCanvas.PaletteBorder.GetBorderWidth(_drawCanvas.State);

        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Get the preferred size requested by the children
            Size size = base.GetPreferredSize(context);

            // Apply the rounding in the appropriate orientation
            if ((Orientation == VisualOrientation.Top) || (Orientation == VisualOrientation.Bottom))
            {
                size.Width += Convert.ToInt32(Rounding) * 2;
                size.Height += BorderWidth;
            }
            else
            {
                size.Height += Convert.ToInt32(Rounding) * 2;
                size.Width += BorderWidth;
            }

            return size;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Find the rectangle available to each child by removing the rounding
            Rectangle childRect = ClientRectangle;

            // Find the amount of rounding to apply
            float rounding = Rounding;

            // Apply the rounding in the appropriate orientation
            if ((Orientation == VisualOrientation.Top) || (Orientation == VisualOrientation.Bottom))
            {
                childRect.Width -= Convert.ToInt32(rounding) * 2;
                childRect.X += Convert.ToInt32(rounding);
            }
            else
            {
                childRect.Height -= Convert.ToInt32(rounding) * 2;
                childRect.Y += Convert.ToInt32(rounding);
            }

            // Inform each child to layout inside the reduced rectangle
            foreach (ViewBase child in this)
            {
                context.DisplayRectangle = childRect;
                child.Layout(context);
            }

            // Remember the set context to the size we were given
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}
