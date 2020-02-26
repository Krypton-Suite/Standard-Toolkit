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

using System.Xml;
using Krypton.Navigator;

namespace Krypton.Docking
{
    /// <summary>
    /// Event data for saving docking page configuration.
    /// </summary>
    public class DockPageSavingEventArgs : DockGlobalSavingEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockPageSavingEventArgs class.
        /// </summary>
        /// <param name="manager">Reference to owning docking manager instance.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        /// <param name="page">Reference to page being saved.</param>
        public DockPageSavingEventArgs(KryptonDockingManager manager,
                                       XmlWriter xmlWriter,
                                       KryptonPage page)
            : base(manager, xmlWriter)
        {
            Page = page;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the saving page reference.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion
    }
}
