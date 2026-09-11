#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Applies <see cref="KryptonDialogButtonAppearance"/> colours to Utilities dialog buttons
/// that use <see cref="KryptonButton"/> (including <see cref="MessageButton"/>).
/// </summary>
internal static class DialogButtonAppearanceUtilities
{
    /// <summary>
    /// Applies semantic colours to a <see cref="KryptonButton"/> when options resolve colours for its dialog result.
    /// </summary>
    /// <param name="button">The button to style.</param>
    /// <param name="dialogResult">Dialog result used to select the role.</param>
    /// <param name="options">Call-site or effective colour options; null uses <see cref="KryptonManager.DialogButtonColors"/>.</param>
    public static void Apply(KryptonButton? button, DialogResult dialogResult, KryptonDialogButtonColorOptions? options)
    {
        if (button == null || dialogResult == DialogResult.None)
        {
            return;
        }

        Apply(button, KryptonDialogButtonAppearance.GetRole(dialogResult), options);
    }

    /// <summary>
    /// Applies semantic colours to a <see cref="KryptonButton"/> for an explicit semantic role.
    /// </summary>
    /// <param name="button">The button to style.</param>
    /// <param name="role">Semantic role used to pick colours (use <see cref="KryptonDialogButtonRole.Help"/> for Help buttons).</param>
    /// <param name="options">Call-site or effective colour options; null uses <see cref="KryptonManager.DialogButtonColors"/>.</param>
    public static void Apply(KryptonButton? button, KryptonDialogButtonRole role, KryptonDialogButtonColorOptions? options)
    {
        if (button == null)
        {
            return;
        }

        KryptonDialogButtonAppearance.Apply(button, role, options);
    }
}
