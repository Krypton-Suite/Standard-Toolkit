﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    public class GeneralToolkitUtilities
    {
        #region Implementation

        public static Point GetCurrentScreenSize()
        {
            var screenWidth = Screen.PrimaryScreen.Bounds.Width;

            var screenHeight = Screen.PrimaryScreen.Bounds.Height;

            return new Point(screenWidth, screenHeight);
        }

        internal static void AdjustFormDimensions(KryptonForm owner, int width, int height) => owner.Size = new Size(width, height);

        #endregion
    }
}
