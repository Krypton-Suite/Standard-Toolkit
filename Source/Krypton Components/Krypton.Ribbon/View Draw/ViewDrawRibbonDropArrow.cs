﻿#region BSD License
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

using System.Drawing;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws a drop arrow used in various ribbon controls.
    /// </summary>
    internal class ViewDrawRibbonDropArrow : ViewLeaf
    {
        #region Static Fields
        private static readonly Size _arrowSize = new Size(5, 4);
        #endregion

        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonDropArrow class.
        /// </summary>
        /// <param name="ribbon">Reference to owning control instance.</param>
        public ViewDrawRibbonDropArrow(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonDropArrow:" + Id;
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            return _arrowSize;
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
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            // Use renderer to draw the drop arrow in the provided space
            context.Renderer.RenderGlyph.DrawRibbonDropArrow(_ribbon.RibbonShape, 
                                                             context, 
                                                             ClientRectangle, 
                                                             _ribbon.StateCommon.RibbonGeneral, 
                                                             State);
        }
        #endregion
    }
}
