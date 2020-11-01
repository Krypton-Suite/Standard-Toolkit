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

using System.ComponentModel;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Event arguments for the drop down menu of a gallery.
    /// </summary>
    public class GalleryDropMenuEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GalleryDropMenuEventArgs class.
        /// </summary>
        /// <param name="contextMenu">Context menu.</param>
        public GalleryDropMenuEventArgs(KryptonContextMenu contextMenu)
        {
            KryptonContextMenu = contextMenu;
        }
        #endregion

        #region Public
        /// <summary>
        /// KryptonContextMenu for display.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        #endregion
    }
}
