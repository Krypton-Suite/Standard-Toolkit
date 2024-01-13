#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public class GlobalStaticValues
    {
        /// Used for 'Material' themes
        public const float DEFAULT_MATERIAL_THEME_CORNER_ROUNDING_VALUE = -1f;

        /// Used for the default control corners
        public const float DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE = -1f;

        public static UACShieldIconSize DEFAULT_UAC_SHIELD_ICON_SIZE = UACShieldIconSize.ExtraSmall;

        public static Size DEFAULT_UAC_SHIELD_ICON_CUSTOM_SIZE = new Size(16, 16);

        internal const int GLOBAL_BUTTON_PADDING = 10;

        // Used for version reporting
        internal static string DEFAULT_DOCKING_FILE = @"Krypton.Docking.dll";
        internal static string DEFAULT_NAVIGATOR_FILE = @"Krypton.Navigator.dll";
        internal static string DEFAULT_RIBBON_FILE = @"Krypton.Ribbon.dll";
        internal static string DEFAULT_TOOLKIT_FILE = @"Krypton.Toolkit.dll";
        internal static string DEFAULT_WORKSPACE_FILE = @"Krypton.Workspace.dll";

        internal static readonly int OS_MAJOR_VERSION = Environment.OSVersion.Version.Major;
    }
}