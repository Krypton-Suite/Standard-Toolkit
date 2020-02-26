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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Details of the context menu showing related to a bread crumb.
    /// </summary>
    public class BreadCrumbMenuArgs : ContextPositionMenuArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="crumb">Reference to related crumb.</param>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        /// <param name="positionH">Relative horizontal position of the KryptonContextMenu.</param>
        /// <param name="positionV">Relative vertical position of the KryptonContextMenu.</param>
        public BreadCrumbMenuArgs(KryptonBreadCrumbItem crumb,
                                  KryptonContextMenu kcm,
                                  KryptonContextMenuPositionH positionH,
                                  KryptonContextMenuPositionV positionV)
            : base(null, kcm, positionH, positionV)
        {
            Crumb = crumb;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the crumb related to the event.
        /// </summary>
        public KryptonBreadCrumbItem Crumb { get; set; }

        #endregion
    }
}
