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
/// Shared detection helpers for Windows XP Luna palette rendering.
/// </summary>
internal static class WindowsXPLunaRenderHelper
{
    internal static readonly Color TabUnselectedFace1 = Color.FromArgb(212, 208, 200);
    internal static readonly Color TabUnselectedFace2 = Color.FromArgb(236, 233, 216);
    internal static readonly Color TabSelectedFace = Color.FromArgb(236, 233, 216);
    internal static readonly Color TabBorderDark = Color.FromArgb(128, 128, 128);
    internal static readonly Color TabBorderLight = Color.FromArgb(255, 255, 255);

    internal static bool IsLunaPalette(PaletteBase? palette) => palette is PaletteWindowsXPLunaBase;

    internal static PaletteWindowsXPLunaBase? AsLunaPalette(PaletteBase? palette) => palette as PaletteWindowsXPLunaBase;

    internal static bool UsesLunaPushButtonChrome(PaletteBackStyle style) =>
        style is PaletteBackStyle.ButtonStandalone
            or PaletteBackStyle.ButtonAlternate
            or PaletteBackStyle.ButtonLowProfile
            or PaletteBackStyle.ButtonGallery
            or PaletteBackStyle.ButtonCluster
            or PaletteBackStyle.ButtonCommand
            or PaletteBackStyle.ButtonCustom1
            or PaletteBackStyle.ButtonCustom2
            or PaletteBackStyle.ButtonCustom3
            or PaletteBackStyle.ButtonNavigatorStack
            or PaletteBackStyle.ButtonNavigatorOverflow
            or PaletteBackStyle.ButtonNavigatorMini;

    internal static bool UsesLunaTabChrome(PaletteBackStyle style) =>
        style is PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile;

    internal static bool IsLunaPushButtonBack(IPaletteBack palette, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state)
            || !IsLunaPalette(KryptonManager.CurrentGlobalPalette))
        {
            return false;
        }

        return palette.GetBackColorStyle(state) == PaletteColorStyle.Rounding2;
    }

    internal static bool IsLunaTabBack(IPaletteBack palette, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state)
            || !IsLunaPalette(KryptonManager.CurrentGlobalPalette))
        {
            return false;
        }

        return palette.GetBackColorStyle(state) == PaletteColorStyle.Rounding3;
    }

    internal static bool IsLunaHeaderFormBack(IPaletteBack palette, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state)
            || !IsLunaPalette(KryptonManager.CurrentGlobalPalette))
        {
            return false;
        }

        return palette.GetBackColorStyle(state) == PaletteColorStyle.Rounding4;
    }

    internal static bool IsLunaTabSelected(PaletteState state) =>
        state is PaletteState.CheckedNormal
            or PaletteState.CheckedTracking
            or PaletteState.CheckedPressed;

    internal static GraphicsPath CreateTabTopRoundedPath(Rectangle rect, float radius)
    {
        var path = new GraphicsPath();
        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return path;
        }

        float d = radius * 2f;
        if (d > rect.Width)
        {
            d = rect.Width;
        }

        if (d > rect.Height)
        {
            d = rect.Height;
        }

        path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + radius);
        path.AddArc(rect.Left, rect.Top, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
        path.AddLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom);
        path.CloseFigure();
        return path;
    }

    internal static GraphicsPath CreateBubbleButtonPath(Rectangle rect)
    {
        var path = new GraphicsPath();
        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return path;
        }

        int radius = Math.Max(3, Math.Min(rect.Height, rect.Width) / 4);
        int d = radius * 2;
        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }
}
