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

using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Override to draw tab items overlapping a group border and draw the selected tab item last.
    /// </summary>
    internal class ViewLayoutDockerOverlap : ViewLayoutDocker
    {
        #region Instance Fields
        private readonly ViewDrawCanvas _drawCanvas;
        private readonly ViewLayoutInsetOverlap _layoutOverlap;
        private readonly ViewLayoutBarForTabs _layoutTabs;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutDockerOverlap class.
        /// </summary>
        /// <param name="drawCanvas">Canvas used to recover border width/rounding for overlapping.</param>
        /// <param name="layoutOverlap">Overlapping element.</param>
        /// <param name="layoutTabs">Tab item container element.</param>
        public ViewLayoutDockerOverlap(ViewDrawCanvas drawCanvas,
                                       ViewLayoutInsetOverlap layoutOverlap,
                                       ViewLayoutBarForTabs layoutTabs)
        {
            Debug.Assert(drawCanvas != null);
            Debug.Assert(layoutOverlap != null);
            Debug.Assert(layoutTabs != null);

            // Remember provided references
            _drawCanvas = drawCanvas;
            _layoutOverlap = layoutOverlap;
            _layoutTabs = layoutTabs;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutDockerOverlap:" + Id;
        }
        #endregion

        #region BorderWidth
        /// <summary>
        /// Gets the rounding value to apply on the edges.
        /// </summary>
        public int BorderWidth => _drawCanvas.PaletteBorder.GetBorderWidth(_drawCanvas.State);

        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering after child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderAfter(RenderContext context) 
        {
            // Ask for another draw of the child but this time only drawing the selected tab
            _layoutTabs.DrawChecked = true;

            // Only render visible children that are inside the clipping rectangle
            if (_layoutOverlap.Visible && _layoutOverlap.ClientRectangle.IntersectsWith(context.ClipRect))
            {
                _layoutOverlap.Render(context);
            }

            _layoutTabs.DrawChecked = false;
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Allow the preferred size calculated by GetPreferredSize to be modified before use.
        /// </summary>
        /// <param name="preferredSize">Original preferred size value.</param>
        /// <returns>Modified size.</returns>
        protected override Size UpdatePreferredSize(Size preferredSize)
        {
            // Docking edge determines how to apply the overlapping
            switch (GetDock(_layoutOverlap))
            {
                case ViewDockStyle.Top:
                case ViewDockStyle.Bottom:
                    preferredSize.Height += BorderWidth;
                    break;
                case ViewDockStyle.Left:
                case ViewDockStyle.Right:
                    preferredSize.Width += BorderWidth;
                    break;
            }

            return preferredSize;
        }

        /// <summary>
        /// Allow the filler rectangle calculated by Layout to be modified before use.
        /// </summary>
        /// <param name="fillerRect">Original filler rectangle.</param>
        /// <param name="control">Owning control instance.</param>
        /// <returns>Modified rectangle.</returns>
        protected override Rectangle UpdateFillerRect(Rectangle fillerRect, 
                                                      Control control)
        {
            int borderWidth = BorderWidth;

            // Docking edge determines how to apply the overlapping
            switch (CalculateDock(GetDock(_layoutOverlap), control))
            {
                case ViewDockStyle.Top:
                    fillerRect.Height += borderWidth;
                    fillerRect.Y -= borderWidth;
                    break;
                case ViewDockStyle.Bottom:
                    fillerRect.Height += borderWidth;
                    break;
                case ViewDockStyle.Left:
                    fillerRect.Width += borderWidth;
                    fillerRect.X -= borderWidth;
                    break;
                case ViewDockStyle.Right:
                    fillerRect.Width += borderWidth;
                    break;
            }

            return fillerRect;
        }
        #endregion
    }
}
