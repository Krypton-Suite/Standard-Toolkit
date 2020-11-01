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
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Extends the ViewLayoutDocker by drawing the ribbon application button recent docs background.
    /// </summary>
    internal class ViewDrawRibbonAppMenuDocs : ViewLayoutDocker
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        private IDisposable _memento;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonAppMenuDocs class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public ViewDrawRibbonAppMenuDocs(KryptonRibbon ribbon)
        {
            _ribbon = ribbon;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonAppMenuRecentDocs:" + Id;
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

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            // Draw the application menu outer background
            _memento = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, ClientRectangle, State,
                                                                    _ribbon.StateCommon.RibbonAppMenuDocs,
                                                                    VisualOrientation.Top, false, _memento);

        }
        #endregion
    }
}
