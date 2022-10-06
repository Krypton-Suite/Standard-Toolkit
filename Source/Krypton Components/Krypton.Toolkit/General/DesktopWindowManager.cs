﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Helper routines for interacting with the Desktop Window Manager.
    /// </summary>
    public class DWM
    {
        #region Static Methods
        /// <summary>
        /// Is composition currently enabled for the desktop.
        /// </summary>
        public static bool IsCompositionEnabled
        {
            get
            {
                // Desktop composition is only available on Vista upwards
                if (Environment.OSVersion.Version.Major < 6)
                {
                    return false;
                }
                else if (Environment.OSVersion.Version.Major < 10)
                {
                    // Ask the desktop window manager is composition is currently enabled
                    return PI.Dwm.IsCompositionEnabled();
                }
                else //Win 10
                {
                    return UserSystemPreferencesService.IsTransparencyEnabled;
                }
            }
        }

        /// <summary>
        /// Change the distance the frame extends into the client area.
        /// </summary>
        /// <param name="hWnd">Window handle of form.</param>
        /// <param name="padding">Distance for each form edge.</param>
        public static void ExtendFrameIntoClientArea(IntPtr hWnd, Padding padding)
        {
            // We can't use 'null', since the type of the object is 'IntPtr'. So we need to use 'IntPtr.Zero'.
            Debug.Assert(hWnd != IntPtr.Zero);

            // Create structure that contains distances for each edge
            var margins = new PI.MARGINS
            {
                leftWidth = padding.Left,
                topHeight = padding.Top,
                rightWidth = padding.Right,
                bottomHeight = padding.Bottom
            };

            // Request change from the desktop window manager
            PI.Dwm.DwmExtendFrameIntoClientArea(hWnd, ref margins);
        }
        #endregion
    }
}
