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

        public const int GLOBAL_BUTTON_PADDING = 10;

        public const int GLOBAL_DEFAULT_THEME_INDEX = (int)PaletteMode.Microsoft365Blue;

        // Used for version reporting
        internal static string DEFAULT_DOCKING_FILE = @"Krypton.Docking.dll";
        internal static string DEFAULT_NAVIGATOR_FILE = @"Krypton.Navigator.dll";
        internal static string DEFAULT_RIBBON_FILE = @"Krypton.Ribbon.dll";
        internal static string DEFAULT_TOOLKIT_FILE = @"Krypton.Toolkit.dll";
        internal static string DEFAULT_WORKSPACE_FILE = @"Krypton.Workspace.dll";

        public static readonly int OS_MAJOR_VERSION = Environment.OSVersion.Version.Major;

        /// <summary>
        /// The default group row height
        /// </summary>
        public static int DefaultGroupRowHeight = 34;
        /// <summary>
        /// The group row height for 2013 palettes
        /// </summary>
        public static int _2013GroupRowHeight = 24;
        /// <summary>
        /// The default offset height
        /// </summary>
        public static int DefaultOffsetHeight = 22;
        /// <summary>
        /// The offset height for 2013 palettes
        /// </summary>
        public static int Office2013OffsetHeight = 11;
        /// <summary>
        /// The image offset width
        /// </summary>
        public static int ImageOffsetWidth = 18;
        /// <summary>
        /// The group level multiplier
        /// </summary>
        public static int GroupLevelMultiplier = 15;
        /// <summary>
        /// The group image side size
        /// </summary>
        public static int GroupImageSide = 16;
    }
}