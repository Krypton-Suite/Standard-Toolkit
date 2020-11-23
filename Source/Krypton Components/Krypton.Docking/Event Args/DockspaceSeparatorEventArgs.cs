// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using Krypton.Toolkit;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a DockspaceSeparatorAdding/DockspaceSeparatorRemoved event.
    /// </summary>
    public class DockspaceSeparatorEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockspaceSeparatorEventArgs class.
        /// </summary>
        /// <param name="separator">Reference to separator control instance.</param>
        /// <param name="element">Reference to dockspace docking element that is managing the separator.</param>
        public DockspaceSeparatorEventArgs(KryptonSeparator separator,
                                           KryptonDockingDockspace element)
        {
            SeparatorControl = separator;
            DockspaceElement = element;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonSeparator control..
        /// </summary>
        public KryptonSeparator SeparatorControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingDockspace that is managing the dockspace.
        /// </summary>
        public KryptonDockingDockspace DockspaceElement { get; }

        #endregion
    }
}
