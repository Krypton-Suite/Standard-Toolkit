// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Drawing;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Details for an event that provides a button drag offset value.
    /// </summary>
    public class ButtonDragOffsetEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonDragOffsetEventArgs class.
        /// </summary>
        /// <param name="offset">Mouse offset for button dragging.</param>
        public ButtonDragOffsetEventArgs(Point offset)
        {
            PointOffset = offset;
        }
        #endregion

        #region Point
        /// <summary>
        /// Gets access to the left mouse dragging offer.
        /// </summary>
        public Point PointOffset { get; }

        #endregion
    }
}
