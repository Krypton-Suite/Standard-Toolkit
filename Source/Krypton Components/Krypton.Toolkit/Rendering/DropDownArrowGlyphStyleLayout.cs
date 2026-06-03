#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class DropDownArrowGlyphStyleLayout
{
    internal static void GetLayerOffsets(DropDownArrowGlyphStyle style, int size, out Point fillOffset, out Point outlineOffset)
    {
        if (size < 8 || style == DropDownArrowGlyphStyle.Flat)
        {
            fillOffset = Point.Empty;

            outlineOffset = Point.Empty;

            return;
        }

        switch (style)
        {
            case DropDownArrowGlyphStyle.Bevel:

                fillOffset = new Point(1, 1);

                outlineOffset = Point.Empty;
                break;
            case DropDownArrowGlyphStyle.Emboss:

                fillOffset = Point.Empty;

                outlineOffset = new Point(1, 1);
                break;
            default:
                fillOffset = Point.Empty;

                outlineOffset = Point.Empty;
                break;
        }
    }
}

