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

namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for persisting extra data for a workspace cell page.
    /// </summary>
    public class PageSavingEventArgs : XmlSavingEventArgs
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageSavingEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="page">Reference to owning workspace cell page.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        public PageSavingEventArgs(KryptonWorkspace workspace,
                                   KryptonPage page,
                                   XmlWriter xmlWriter)
            : base(workspace, xmlWriter) =>
            Page = page;

        #endregion

        #region Public
        /// <summary>
        /// Gets the workspace cell page reference.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion
    }
}
