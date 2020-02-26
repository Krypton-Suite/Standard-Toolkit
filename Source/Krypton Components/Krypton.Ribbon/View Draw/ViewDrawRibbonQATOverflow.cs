// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws the border around the overflow popup of the quick access toolbar.
    /// </summary>
    internal class ViewDrawRibbonQATOverflow  : ViewComposite
    {
        #region Static Fields
        private static readonly Padding _borderPadding = new Padding(3);
        private const int QAT_HEIGHT_FULL = 28;

        #endregion

        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        private NeedPaintHandler _needPaintDelegate;
        private IDisposable _memento;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonQATOverflow class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
        public ViewDrawRibbonQATOverflow(KryptonRibbon ribbon,
                                         NeedPaintHandler needPaintDelegate)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(needPaintDelegate != null);

            // Remember incoming references
            _ribbon = ribbon;
            _needPaintDelegate = needPaintDelegate;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonQATOverflow:" + Id;
        }

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

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Get size of the contained items
            Size preferredSize = base.GetPreferredSize(context);

            // Add on the border padding
            preferredSize = CommonHelper.ApplyPadding(Orientation.Horizontal, preferredSize, _borderPadding);
            preferredSize.Height = Math.Max(preferredSize.Height, QAT_HEIGHT_FULL);

            return preferredSize;
        }
            
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            Rectangle clientRect = context.DisplayRectangle;

            ClientRectangle = clientRect;

            // Remove QAT border for positioning children
            context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _borderPadding);

            // Let children be layed out inside border area
            base.Layout(context);

            // Put back the original display value now we have finished
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
            _memento = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape,
                                                                    context,
                                                                    ClientRectangle,
                                                                    PaletteState.Normal,
                                                                    _ribbon.StateCommon.RibbonQATOverflow,
                                                                    VisualOrientation.Top,
                                                                    false,
                                                                    _memento);
        }
        #endregion
    }
}
