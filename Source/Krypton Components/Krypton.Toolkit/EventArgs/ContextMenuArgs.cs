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
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for context menu related events.
    /// </summary>
    public class ContextMenuArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        public ContextMenuArgs()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="cms">Context menu strip that can be customized.</param>
        public ContextMenuArgs(ContextMenuStrip cms)
            : this(cms, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        public ContextMenuArgs(KryptonContextMenu kcm)
            : this(null, kcm)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="cms">Context menu strip that can be customized.</param>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        public ContextMenuArgs(ContextMenuStrip cms,
                               KryptonContextMenu kcm)
        {
            ContextMenuStrip = cms;
            KryptonContextMenu = kcm;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the context menu strip instance.
        /// </summary>
        public ContextMenuStrip ContextMenuStrip { get; }

        /// <summary>
        /// Gets access to the KryptonContextMenu instance.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        #endregion
    }
}
