// *****************************************************************************
// 
//  © Component Factory Pty Ltd, 2006 - 2016. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.4.0.0 	
// *****************************************************************************

using System;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using Krypton.Toolkit;

namespace Krypton.Workspace
{
    /// <summary>
    /// Storage for workspace context menu for pages.
    /// </summary>
    public abstract class WorkspacePageMenuBase : Storage
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the WorkspacePageMenuBase class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace.</param>
        public WorkspacePageMenuBase(KryptonWorkspace workspace)
        {
        }
        #endregion
    }
}
