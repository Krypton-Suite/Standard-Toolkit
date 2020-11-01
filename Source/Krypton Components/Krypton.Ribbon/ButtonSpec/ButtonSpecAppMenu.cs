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

using System;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Button specification appropriate for an application menu
    /// </summary>
    public class ButtonSpecAppMenu : ButtonSpecAny
    {
        #region Protected
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Only if associated view is enabled to we perform an action
            if (GetViewEnabled())
            {
                if ((KryptonContextMenu == null) &&
                    (ContextMenuStrip == null))
                {
                    // Remove the popup app menu that is showing
                    VisualPopupManager.Singleton.EndAllTracking();
                }

                // If a checked style button
                if (Checked != ButtonCheckState.NotCheckButton)
                {
                    // Then invert the checked state
                    Checked = Checked == ButtonCheckState.Unchecked ? ButtonCheckState.Checked : ButtonCheckState.Unchecked;
                }

                GenerateClick(e);
            }
        }
        #endregion
    }
}
