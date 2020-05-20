// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.Xml;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for persisting extra data for a workspace cell page.
    /// </summary>
    public class PageLoadingEventArgs : XmlLoadingEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageLoadingEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="page">Reference to owning workspace cell page.</param>
        /// <param name="xmlReader">Xml reader for persisting custom data.</param>
        public PageLoadingEventArgs(KryptonWorkspace workspace,
                                    KryptonPage page,
                                    XmlReader xmlReader)
            : base(workspace, xmlReader)
        {
            Page = page;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the workspace cell page reference.
        /// </summary>
        public KryptonPage Page { get; set; }

        #endregion
    }
}
