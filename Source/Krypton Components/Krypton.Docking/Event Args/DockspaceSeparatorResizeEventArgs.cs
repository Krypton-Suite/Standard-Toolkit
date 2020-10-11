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

using System.Drawing;
using Krypton.Toolkit;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a DockspaceSeparatorResize event.
    /// </summary>
    public class DockspaceSeparatorResizeEventArgs : DockspaceSeparatorEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockspaceSeparatorResizeEventArgs class.
        /// </summary>
        /// <param name="separator">Reference to separator control instance.</param>
        /// <param name="element">Reference to dockspace docking element that is managing the separator.</param>
        /// <param name="resizeRect">Initial resizing rectangle.</param>
        public DockspaceSeparatorResizeEventArgs(KryptonSeparator separator,
                                                 KryptonDockingDockspace element,
                                                 Rectangle resizeRect)
            : base(separator, element)
        {
            ResizeRect = resizeRect;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the rectangle that limits resizing of the dockspace using the separator.
        /// </summary>
        public Rectangle ResizeRect { get; set; }

        #endregion
    }
}
