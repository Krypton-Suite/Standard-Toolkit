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

using System.Windows.Forms;
using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Details for a close button action event.
    /// </summary>
    public class ShowContextMenuArgs : KryptonPageCancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ShowContextMenuArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        public ShowContextMenuArgs(KryptonPage page, int index)
            : base(page, index)
        {
            ContextMenuStrip = page.ContextMenuStrip;
            KryptonContextMenu = page.KryptonContextMenu;
        }
        #endregion

        #region ContextMenuStrip
        /// <summary>
        /// Gets and sets the context menu strip.
        /// </summary>
        public ContextMenuStrip ContextMenuStrip { get; set; }

        #endregion

        #region KryptonContextMenu
        /// <summary>
        /// Gets and sets the context menu strip.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; set; }

        #endregion
    }
}
