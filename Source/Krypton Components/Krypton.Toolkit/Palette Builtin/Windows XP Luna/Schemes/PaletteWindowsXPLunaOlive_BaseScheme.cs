#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Windows XP Luna olive (HomeStead) scheme tokens.
/// </summary>
public sealed class PaletteWindowsXPLunaOlive_BaseScheme : PaletteWindowsXPLunaBlue_BaseScheme
{
    public PaletteWindowsXPLunaOlive_BaseScheme()
    {
        TextLabelControl = Color.FromArgb( 48,  72,  24);
        TextButtonNormal = Color.FromArgb( 48,  72,  24);
        TextLabelPanel = Color.FromArgb( 48,  72,  24);
        HeaderText = Color.FromArgb( 48,  72,  24);
        StatusStripText = Color.FromArgb( 48,  72,  24);

        FormHeaderShortActive = Color.FromArgb( 76,  96,  49);
        FormHeaderLongActive = Color.FromArgb(147, 176, 114);
        FormBorderActive = Color.FromArgb( 99, 122,  69);
        FormBorderHeaderActive1 = Color.FromArgb(175, 192, 130);
        FormBorderHeaderActive2 = Color.FromArgb( 99, 122,  69);

        ButtonNormalBack1 = Color.FromArgb(210, 225, 200);
        ButtonNormalBack2 = Color.FromArgb(235, 243, 228);
        ButtonNormalDefaultBack1 = Color.FromArgb(175, 192, 130);
        ButtonNormalDefaultBack2 = Color.FromArgb(210, 230, 180);

        StatusStripLight = Color.FromArgb( 99, 122,  69);
        StatusStripDark = Color.FromArgb( 76,  96,  49);
        ToolStripBack = Color.FromArgb(236, 233, 216);
        ToolStripBegin = Color.FromArgb(220, 230, 200);
        ToolStripMiddle = Color.FromArgb(210, 222, 190);
        ToolStripEnd = Color.FromArgb(175, 192, 130);

        GridListSelected = Color.FromArgb( 99, 122,  69);
        GridListNormal2 = Color.FromArgb(220, 230, 210);
    }
}
