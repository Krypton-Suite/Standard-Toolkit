﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking
{
    /// <summary>
    /// Event data for saving global docking configuration.
    /// </summary>
    public class DockGlobalSavingEventArgs : EventArgs
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockGlobalSavingEventArgs class.
        /// </summary>
        /// <param name="manager">Reference to owning docking manager instance.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        public DockGlobalSavingEventArgs(KryptonDockingManager? manager,
                                         XmlWriter xmlWriter)
        {
            DockingManager = manager;
            XmlWriter = xmlWriter;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the docking manager reference.
        /// </summary>
        public KryptonDockingManager? DockingManager { get; }

        /// <summary>
        /// Gets the xml writer.
        /// </summary>
        public XmlWriter XmlWriter { get; }

        #endregion
    }
}
