﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.ComponentModel;

namespace Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonWorkspace to work within the docking floating window.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonFloatspace : KryptonSpace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonFloatspace class.
        /// </summary>
        public KryptonFloatspace()
            : base("Floating")
        {
        }

        /// <summary>
        /// Gets a string representation of the class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "KryptonFloatspace " + Dock.ToString();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets a value indicating if docking specific pin actions should be applied.
        /// </summary>
        protected override bool ApplyDockingPinAction => false;

        #endregion
    }
}
