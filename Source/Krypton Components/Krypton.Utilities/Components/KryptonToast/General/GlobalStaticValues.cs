#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Utilities;

/// <summary>Provides a collection of static values, used within the toolkit.</summary>
public class GlobalStaticValues
{
    internal const bool DEFAULT_USE_STACK_TRACE = true;

    internal const bool DEFAULT_USE_EXCEPTION_MESSAGE = true;

    internal const bool DEFAULT_USE_INNER_EXCEPTION = true;

    internal const int DEFAULT_TOGGLE_SWITCH_ANIMATION_SPEED = 10;

    /// <summary>The default date and time value</summary>
    public static DateTime DEFAULT_DATE_TIME_VALUE = DateTime.Now;

    /// Used for 'Material' themes
    public const float DEFAULT_MATERIAL_THEME_CORNER_ROUNDING_VALUE = -1f;

    /// Used for the default control corners
    public const float DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE = -1f;

    /// <summary>The default UAC shield icon size</summary>
    public static IconSize DEFAULT_UAC_SHIELD_ICON_SIZE = IconSize.ExtraSmall;

    /// <summary>The default UAC shield icon custom size</summary>
    public static Size DEFAULT_UAC_SHIELD_ICON_CUSTOM_SIZE = new Size(16, 16);

    /// <summary>The standard UAC shield icon ID in imageres.dll</summary>
    public const int UAC_SHIELD_ICON_ID = (int)ImageresIconID.Shield;

    /// <summary>The alternative UAC shield icon ID in imageres.dll</summary>
    public const int UAC_SHIELD_ICON_ID_ALT = (int)ImageresIconID.ShieldAlt;

    /// <summary>The global button padding</summary>
    public const int GLOBAL_BUTTON_PADDING = 10;

    /// <summary>The default countdown interval</summary>
    internal const int DEFAULT_COUNTDOWN_INTERVAL = 1000;

    /// <summary>The default countdown value</summary>
    internal const int DEFAULT_COUNTDOWN_VALUE = 60;

    internal const int DEFAULT_PADDING = 10;

    /// <summary>The global default theme index</summary>
    public const int GLOBAL_DEFAULT_THEME_INDEX = (int)PaletteMode.Microsoft365Blue;

    /// <summary>The global default theme</summary>
    public const PaletteMode GLOBAL_DEFAULT_PALETTE_MODE = PaletteMode.Microsoft365Blue;

    /// <summary>The current supported palette version</summary>
    public const int CURRENT_SUPPORTED_PALETTE_VERSION = 20;

    /// <summary>The default highlight debugging color</summary>
    public static Color DEFAULT_HIGHLIGHT_DEBUGGING_COLOR = Color.Red;

    // Used for version reporting
    internal static string DEFAULT_DOCKING_FILE = @"Krypton.Docking.dll";
    internal static string DEFAULT_NAVIGATOR_FILE = @"Krypton.Navigator.dll";
    internal static string DEFAULT_RIBBON_FILE = @"Krypton.Ribbon.dll";
    internal static string DEFAULT_TOOLKIT_FILE = @"Krypton.Toolkit.dll";
    internal static string DEFAULT_WORKSPACE_FILE = @"Krypton.Workspace.dll";

    internal const string DEFAULT_NOT_IMPLEMENTED_YET_MESSAGE =
        $"This feature has not been currently implemented yet.\nPlease check back again soon!";

    internal static string DEFAULT_EMPTY_STRING = string.Empty;

    /// <summary>The OS major version</summary>
    public static readonly int OS_MAJOR_VERSION = Environment.OSVersion.Version.Major;

    /// <summary>The default rafting ribbon tab background gradient</summary>
    public const float DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT = 90F;

    /// <summary>The empty color</summary>
    public static readonly Color EMPTY_COLOR = Color.Empty;

    /// <summary>The transparency key color</summary>
    public static readonly Color TRANSPARENCY_KEY_COLOR = Color.Magenta;

    /// <summary>The tab row gradient first color</summary>
    public static readonly Color TAB_ROW_GRADIENT_FIRST_COLOR = Color.Transparent;

    /// <summary>The default ribbon application button dark color</summary>
    public static readonly Color DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR = Color.FromArgb(31, 72, 161);

    /// <summary>The default ribbon application button light color</summary>
    public static readonly Color DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR = Color.FromArgb(84, 158, 243);

    /// <summary>The default ribbon application button text color</summary>
    public static readonly Color DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR = Color.White;

    /// <summary>The latest emoji list URL</summary>
    public const string DEFAULT_LATEST_EMOJI_LIST_URL = @"https://unicode.org/Public/emoji/latest/emoji-test.txt";

    /// <summary>The public emoji list URL</summary>
    public const string DEFAULT_PUBLIC_EMOJI_LIST_URL = @"https://unicode.org/Public/draft/emoji/emoji-test.txt";
    
    /// <summary>
    /// The default group row height
    /// </summary>
    public static int DefaultGroupRowHeight = 34;
    /// <summary>
    /// The group row height for 2013 palettes
    /// </summary>
    public static int Office2013GroupRowHeight = 24;
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

    // For when we need some text to test with
    public static readonly string DEFAULT_SHORT_SEED_TEXT = $"Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)\r\n\u00a9 Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.\r\n\r\nNew BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)\r\nModifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - {DateTime.Now.Year}. All rights reserved.";

    public static readonly string DEFAULT_LONG_SEED_TEXT = $"BSD 3-Clause License\r\n\r\nCopyright (c) 2017 - {DateTime.Now.Year}, Krypton Suite\r\n\r\nAll rights reserved.\r\n\r\nRedistribution and use in source and binary forms, with or without\r\nmodification, are permitted provided that the following conditions are met:\r\n\r\n1. Redistributions of source code must retain the above copyright notice, this\r\n   list of conditions and the following disclaimer.\r\n\r\n2. Redistributions in binary form must reproduce the above copyright notice,\r\n   this list of conditions and the following disclaimer in the documentation\r\n   and/or other materials provided with the distribution.\r\n\r\n3. Neither the name of the copyright holder nor the names of its\r\n   contributors may be used to endorse or promote products derived from\r\n   this software without specific prior written permission.\r\n\r\nTHIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS \"AS IS\"\r\nAND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE\r\nIMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE\r\nDISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE\r\nFOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL\r\nDAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR\r\nSERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER\r\nCAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,\r\nOR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE\r\nOF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.";

    public static readonly string[] TOOLKIT_DLL_NAMES = new string[]
    {
        "Krypton.Docking.dll",
        "Krypton.Navigator.dll",
        "Krypton.Ribbon.dll",
        "Krypton.Toolkit.dll",
        "Krypton.Workspace.dll"
    };

    #region Properties
    /// <summary> 
    /// KryptonMessageBoxes that use the KRichtTextBox need another color for the text.<br/>
    /// Set the text colour to the one a non-input control uses.
    /// </summary>
    public static Color KryptonMessageBoxRichTextBoxTextColor 
    {
        // per ticket #1692
        get => KryptonManager.CurrentGlobalPalette.GetContentLongTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Helper method that returns a generic message when a variable is null.
    /// </summary>
    /// <param name="variableName">Name of the variable to be inserted into the text.</param>
    /// <returns>The message.</returns>
    public static string VariableCannotBeNull(string variableName) => $"Variable {variableName} cannot be null.";

    /// <summary>
    /// Helper method that returns a generic message when a property is null.
    /// </summary>
    /// <param name="propertyName">Name of the property to be inserted into the text.</param>
    /// <returns>The message.</returns>
    public static string PropertyCannotBeNull(string propertyName) => $"Property {propertyName} cannot be null.";

    /// <summary>
    /// Helper method that returns a generic message when a parameter is null.
    /// </summary>
    /// <param name="parameterName">Name of the parameter to be inserted into the text.</param>
    /// <returns>The message.</returns>
    public static string ParameterCannotBeNull(string parameterName) => $"Parameter {parameterName} cannot be null.";
    #endregion
}