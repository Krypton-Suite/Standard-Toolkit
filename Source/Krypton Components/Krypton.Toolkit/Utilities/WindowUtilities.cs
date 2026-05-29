#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public static class WindowUtilities
{
    #region Implementation

    /*public static void EnableAcrylic(IWin32Window owner, Color blurColor)
    {
        if (owner is null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        var accentPolicy = new PI.AccentPolicy
        {
            AccentState = PI.ACCENT.ENABLE_ACRYLICBLURBEHIND,
            GradientColor = ToAbgr(blurColor)
        };

        unsafe
        {
            PI.SetWindowCompositionAttribute(new HandleRef(owner, owner.Handle),
                new PI.WindowCompositionAttributeData()
                {
                    Attribute = PI.WCA.ACCENT_POLICY,
                    Data = &accentPolicy,
                    DataLength = Marshal.SizeOf<PI.AccentPolicy>()
                });
        }
    }

    private static uint ToAbgr(Color color) => ((uint)color.A << 24) | ((uint)color.B << 16) | ((uint)color.G << 8) | color.R;*/

    #endregion
}