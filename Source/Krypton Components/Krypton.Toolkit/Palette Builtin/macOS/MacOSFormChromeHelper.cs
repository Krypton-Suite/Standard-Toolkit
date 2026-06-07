#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Applies Windows DWM effects that approximate macOS window shadow and title-bar vibrancy for <see cref="PaletteMacOSBase"/> forms.
/// </summary>
internal static class MacOSFormChromeHelper
{
    private const int DwmDropShadowMargin = 1;

    internal static void ApplyWindowEffects(KryptonForm form, PaletteMacOSBase palette)
    {
        if (!form.IsHandleCreated)
        {
            return;
        }

        if (!form.UseDropShadow)
        {
            form.UseDropShadow = true;
        }

        PI.Dwm.WindowBorderlessDropShadow(form.Handle, DwmDropShadowMargin);
        PI.Dwm.Windows10EnableBlurBehind(form.Handle, true, palette.GetTitleBarBlurTintColor());
    }

    internal static void ClearWindowEffects(KryptonForm form)
    {
        if (!form.IsHandleCreated)
        {
            return;
        }

        PI.Dwm.Windows10EnableBlurBehind(form.Handle, false, Color.Empty);
    }
}