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

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a AutoHiddenGroupAdding/AutoHiddenGroupRemoved events.
    /// </summary>
    public class AutoHiddenGroupEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AutoHiddenGroupEventArgs class.
        /// </summary>
        /// <param name="control">Reference to auto hidden group control instance.</param>
        /// <param name="element">Reference to docking auto hidden group element that is managing the control.</param>
        public AutoHiddenGroupEventArgs(KryptonAutoHiddenGroup control,
                                        KryptonDockingAutoHiddenGroup element)
        {
            AutoHiddenGroupControl = control;
            AutoHiddenGroupElement = element;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonAutoHiddenGroup control.
        /// </summary>
        public KryptonAutoHiddenGroup AutoHiddenGroupControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingAutoHiddenGroup that is managing the group.
        /// </summary>
        public KryptonDockingAutoHiddenGroup AutoHiddenGroupElement { get; }

        #endregion
    }
}
