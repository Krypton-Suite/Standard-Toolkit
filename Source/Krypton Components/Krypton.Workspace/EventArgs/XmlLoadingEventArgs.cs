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
using System.Xml;

namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for persisting extra data for a workspace.
    /// </summary>
    public class XmlLoadingEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the XmlLoadingEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="xmlReading">Xml reader for persisting custom data.</param>
        public XmlLoadingEventArgs(KryptonWorkspace workspace,
                                   XmlReader xmlReading)
        {
            Workspace = workspace;
            XmlReader = xmlReading;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the workspace reference.
        /// </summary>
        public KryptonWorkspace Workspace { get; }

        /// <summary>
        /// Gets the xml reader.
        /// </summary>
        public XmlReader XmlReader { get; }

        #endregion
    }
}
