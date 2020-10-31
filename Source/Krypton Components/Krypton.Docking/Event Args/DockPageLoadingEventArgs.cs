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

using System.Xml;
using Krypton.Navigator;

namespace Krypton.Docking
{
    /// <summary>
    /// Event data for loading docking page configuration.
    /// </summary>
    public class DockPageLoadingEventArgs : DockGlobalLoadingEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockPageLoadingEventArgs class.
        /// </summary>
        /// <param name="manager">Reference to owning docking manager instance.</param>
        /// <param name="xmlReading">Xml reader for persisting custom data.</param>
        /// <param name="page">Reference to page being loaded.</param>
        public DockPageLoadingEventArgs(KryptonDockingManager manager,
                                        XmlReader xmlReading,
                                        KryptonPage page)
            : base(manager, xmlReading)
        {
            Page = page;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the loading page reference.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion
    }
}
