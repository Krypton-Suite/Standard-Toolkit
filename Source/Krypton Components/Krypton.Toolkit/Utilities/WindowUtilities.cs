using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public static class WindowUtilities
    {
        #region Implementation

        public static void EnableAcrylic(IWin32Window owner, Color blurColor)
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

        private static uint ToAbgr(Color color) => ((uint)color.A << 24) | ((uint)color.B << 16) | ((uint)color.G << 8) | color.R;

        #endregion
    }
}