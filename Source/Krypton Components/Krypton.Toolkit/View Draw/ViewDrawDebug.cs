// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System;
using System.Drawing;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// View element that has a preferred size and then draws a solid color, used for debugging.
    /// </summary>
    public class ViewDrawDebug : ViewLeaf
    {
        #region Instance Fields
        private readonly Size _preferredSize;
        private readonly Color _color;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawDebug class.
        /// </summary>
        /// <param name="preferredSize">Preferred size.</param>
        /// <param name="color">Solid color to draw with.</param>
        public ViewDrawDebug(Size preferredSize, Color color)
        {
            // Remember the source information
            _preferredSize = preferredSize;
            _color = color;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawDebug:" + Id;
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);
            return _preferredSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Use all of the provided space

            // Always use the metric and ignore given space
            ClientRectangle = context.DisplayRectangle;
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

            // Ignore renderer, we just draw using solid color for debugging purposes
            using (SolidBrush brush = new SolidBrush(_color))
            {
                context.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }
        #endregion    
    }
}
