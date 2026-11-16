#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class ToolkitStaticConstants
{
    /// <summary>The global default theme index</summary>
    public const int GLOBAL_DEFAULT_THEME_INDEX = (int)PaletteMode.Microsoft365Blue;

    /// <summary>The global default theme</summary>
    public const PaletteMode GLOBAL_DEFAULT_PALETTE_MODE = PaletteMode.Microsoft365Blue;

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
}