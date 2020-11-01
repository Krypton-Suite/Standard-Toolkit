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

using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for context menu related events that have a requested relative position.
    /// </summary>
    public class ContextPositionMenuArgs : ContextMenuArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        public ContextPositionMenuArgs()
            : this(null, null, KryptonContextMenuPositionH.Left, KryptonContextMenuPositionV.Below)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="cms">Context menu strip that can be customized.</param>
        public ContextPositionMenuArgs(ContextMenuStrip cms)
            : this(cms, null, KryptonContextMenuPositionH.Left, KryptonContextMenuPositionV.Below)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        /// <param name="positionH">Relative horizontal position of the KryptonContextMenu.</param>
        /// <param name="positionV">Relative vertical position of the KryptonContextMenu.</param>
        public ContextPositionMenuArgs(KryptonContextMenu kcm,
                                       KryptonContextMenuPositionH positionH,
                                       KryptonContextMenuPositionV positionV)
            : this(null, kcm, positionH, positionV)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="cms">Context menu strip that can be customized.</param>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        /// <param name="positionH">Relative horizontal position of the KryptonContextMenu.</param>
        /// <param name="positionV">Relative vertical position of the KryptonContextMenu.</param>
        public ContextPositionMenuArgs(ContextMenuStrip cms,
                                       KryptonContextMenu kcm,
                                       KryptonContextMenuPositionH positionH,
                                       KryptonContextMenuPositionV positionV)
            : base(cms, kcm)
        {
            PositionH = positionH;
            PositionV = positionV;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the relative horizontal position of the KryptonContextMenu.
        /// </summary>
        public KryptonContextMenuPositionH PositionH { get; set; }

        /// <summary>
        /// Gets and sets the relative vertical position of the KryptonContextMenu.
        /// </summary>
        public KryptonContextMenuPositionV PositionV { get; set; }

        #endregion
    }
}
