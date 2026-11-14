#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Compatibility facade over <see cref="PI"/> image/icon helpers.
/// </summary>
internal static class ImageNativeMethods
{
    public static IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam) =>
        new IntPtr(unchecked((int)PI.SendMessage(hWnd, unchecked((int)msg), wParam, lParam)));

    public static IntPtr LoadImage(IntPtr hInt, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad) =>
        PI.LoadImage(hInt, lpszName, uType, cxDesired, cyDesired, fuLoad);

    public static int ExtractIconEx(string lpszFile, int nIconIndex, IntPtr[]? phiconLarge, IntPtr[]? phiconSmall, int amountIcons) =>
        PI.ExtractIconEx(lpszFile, nIconIndex, phiconLarge, phiconSmall, amountIcons);

    public static int DestroyIcon(IntPtr hIcon) => PI.DestroyIcon(hIcon) ? 1 : 0;
}
