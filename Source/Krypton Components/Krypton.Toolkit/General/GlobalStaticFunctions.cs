#region BSD License
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

namespace Krypton.Toolkit
{
    /// <summary>Provides a collection of static functions, used within the toolkit.</summary>
    public class GlobalStaticFunctions
    {
        #region Implementation

        /// <summary>Determines whether [is using default black themes].</summary>
        /// <returns><c>true</c> if [is using default black themes]; otherwise, <c>false</c>.</returns>
        public static bool IsUsingDefaultBlackThemes() => KryptonManager.CurrentGlobalPaletteMode == PaletteMode.Office2007Black || KryptonManager.CurrentGlobalPaletteMode == PaletteMode.Office2010Black || KryptonManager.CurrentGlobalPaletteMode == PaletteMode.Microsoft365Black;

        #endregion
    }
}
