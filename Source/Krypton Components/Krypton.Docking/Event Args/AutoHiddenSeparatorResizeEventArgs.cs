// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.Drawing;
using Krypton.Toolkit;
using Krypton.Navigator;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a AutoHiddenSeparatorResize event.
    /// </summary>
    public class AutoHiddenSeparatorResizeEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AutoHiddenSeparatorResizeEventArgs class.
        /// </summary>
        /// <param name="separator">Reference to separator control instance.</param>
        /// <param name="dockspace">Reference to dockspace control instance.</param>
        /// <param name="page">Reference to page contained in the dockspace.</param>
        /// <param name="resizeRect">Initial resizing rectangle.</param>
        public AutoHiddenSeparatorResizeEventArgs(KryptonSeparator separator,
                                                  KryptonDockspace dockspace,
                                                  KryptonPage page,
                                                  Rectangle resizeRect)
        {
            SeparatorControl = separator;
            DockspaceControl = dockspace;
            Page = page;
            ResizeRect = resizeRect;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonSeparator control.
        /// </summary>
        public KryptonSeparator SeparatorControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockspace control.
        /// </summary>
        public KryptonDockspace DockspaceControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonPage instance.
        /// </summary>
        public KryptonPage Page { get; }

        /// <summary>
        /// Gets and sets the rectangle that limits resizing of the dockspace using the separator.
        /// </summary>
        public Rectangle ResizeRect { get; set; }

        #endregion
    }
}
