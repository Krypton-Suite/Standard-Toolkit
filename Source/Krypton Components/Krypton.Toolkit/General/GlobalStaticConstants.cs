#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class GlobalStaticConstants
{
    /// <summary>The default date and time value</summary>
    public static DateTime DEFAULT_DATE_TIME_VALUE = DateTime.Now;

    /// <summary>The default UAC shield icon custom size</summary>
    public static Size DEFAULT_UAC_SHIELD_ICON_CUSTOM_SIZE = new Size(16, 16);

    internal const bool DEFAULT_USE_STACK_TRACE = true;
    internal const bool DEFAULT_USE_EXCEPTION_MESSAGE = true;
    internal const bool DEFAULT_USE_INNER_EXCEPTION = true;
    internal const int DEFAULT_TOGGLE_SWITCH_ANIMATION_SPEED = 10;

    /// Used for 'Material' themes
    public const float DEFAULT_MATERIAL_THEME_CORNER_ROUNDING_VALUE = -1f;

    /// Used for the default control corners
    public const float DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE = -1f;

    /// <summary>The standard UAC shield icon ID in imageres.dll</summary>
    public const int UAC_SHIELD_ICON_ID = (int)ImageresIconID.Shield;

    /// <summary>The alternative UAC shield icon ID in imageres.dll</summary>
    public const int UAC_SHIELD_ICON_ID_ALT = (int)ImageresIconID.ShieldAlt;

    /// <summary>The global button padding</summary>
    public const int GLOBAL_BUTTON_PADDING = 10;

    /// <summary>
    /// Right-hand inset (pixels) for form caption control-box buttons when a built-in palette
    /// does not compute one from the window border. The default <c>1</c> is the form border width,
    /// which keeps the buttons flush against the border while leaving their own right border
    /// visible; at <c>0</c> the button overlaps the column the form border paints last and loses
    /// that edge (see #4132). Larger values float the control box away from the edge, matching
    /// <see cref="CommonHelper.GetFormHeaderButtonEdgeInsetRight"/> at higher settings.
    /// </summary>
    public static int HEADER_BUTTON_EDGE_INSET_FORM_RIGHT = 1;

    /// <summary>
    /// Top inset (pixels) between the caption band and form caption control-box buttons when the
    /// form is restored. The default <c>1</c> keeps a one-pixel gap under the form's top border.
    /// Larger values push the buttons down; any negative value restores the original behaviour of
    /// centring them in the caption. When the form is maximized, any WinForms/DWM screen overhang
    /// (often 8px with <c>Top = -8</c>) is added on top of this value so ButtonSpecs stay fully
    /// on-screen.
    /// </summary>
    public static int HEADER_BUTTON_EDGE_INSET_FORM_TOP = 1;

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
    public const int CURRENT_SUPPORTED_PALETTE_VERSION = 21;

    /// <summary>The default rafting ribbon tab background gradient</summary>
    public const float DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT = 90F;

    /// <summary>Windows convention for positioning windows off-screen (e.g. to hide them). Used when placing windows so they are not visible.</summary>
    public const int OFF_SCREEN_POSITION = -32000;

    /// <summary>The latest emoji list URL</summary>
    public const string DEFAULT_LATEST_EMOJI_LIST_URL = @"https://unicode.org/Public/emoji/latest/emoji-test.txt";

    /// <summary>The public emoji list URL</summary>
    public const string DEFAULT_PUBLIC_EMOJI_LIST_URL = @"https://unicode.org/Public/draft/emoji/emoji-test.txt";

    /// <summary>The required toolbar image count</summary>
    public const int REQUIRED_TOOLBAR_IMAGE_COUNT = 14;
}