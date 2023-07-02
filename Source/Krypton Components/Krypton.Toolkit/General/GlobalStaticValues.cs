#region BSD License
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
    public class GlobalStaticValues
    {
        // Used for 'Material' themes
        public const float MATERIAL_THEME_CORNER_ROUNDING_VALUE = -1;

        // Used for the default control corners
        public const float PRIMARY_CORNER_ROUNDING_VALUE = -1;

        // Used for text controls
        public const float PRIMARY_CORNER_TEXT_CONTROLS_ROUNDING_VALUE = -1;

        // Used for nodes etc
        public const float SECONDARY_CORNER_ROUNDING_VALUE = -1;

        public const float MAXIMUM_PRIMARY_CORNER_ROUNDING_VALUE = 25;

        public const float MAXIMUM_SECONDARY_CORNER_ROUNDING_VALUE = 25;

        // Used for fonts
        public static string DEFAULT_FONT_NAME = "Segoe UI";

        public static float DEFAULT_FONT_SIZE = 9F;

        public static UACShieldIconSize DEFAULT_UAC_SHIELD_ICON_SIZE = UACShieldIconSize.ExtraSmall;

        public static Size DEFAULT_UAC_SHIELD_ICON_CUSTOM_SIZE = new Size(16, 16);
    }
}