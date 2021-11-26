#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

//Seb add
namespace Krypton.Toolkit
{
    /// <summary>
    /// Manages the Windows blur
    /// </summary>
    /// <remarks>
    /// This has been left here in case someone is using it for blurring in Windows 10.
    /// The BlurValues from within the form designer work on "All" windows
    /// </remarks>
    public static class BlurWindowExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <remarks>
        /// This appears to only "Blurs" normal controls, not the ones being themed by Krypton
        /// </remarks>
        public static void EnableBlur(this Form window)
        {
            if (SystemInformation.HighContrast)
            {
                return; // Blur is not useful in high contrast mode
            }

            SetAccentPolicy(window, PI.AccentState.ACCENT_ENABLE_BLURBEHIND);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        public static void DisableBlur(this Form window) => SetAccentPolicy(window, PI.AccentState.ACCENT_DISABLED);

        private static void SetAccentPolicy(Form window, PI.AccentState accentState)
        {
            // var windowHelper = new WindowInteropHelper(window);

            PI.AccentPolicy accent = new()
            {
                AccentState = accentState,
                AccentFlags = GetAccentFlagsForTaskbarPosition()
            };

            var accentStructSize = Marshal.SizeOf(accent);

            IntPtr accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            PI.WindowCompositionAttribData data = new()
            {
                Attribute = PI.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            PI.SetWindowCompositionAttribute(window.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        private static PI.AccentFlags GetAccentFlagsForTaskbarPosition()
        {
            PI.AccentFlags flags = PI.AccentFlags.DrawAllBorders;

            //switch (TaskbarService.TaskbarPosition)
            //{
            //    case TaskbarPosition.Top:
            //        flags &= ~Interop.AccentFlags.DrawTopBorder;
            //        break;

            //    case TaskbarPosition.Bottom:
            //        flags &= ~Interop.AccentFlags.DrawBottomBorder;
            //        break;

            //    case TaskbarPosition.Left:
            //        flags &= ~Interop.AccentFlags.DrawLeftBorder;
            //        break;

            //    case TaskbarPosition.Right:
            //        flags &= ~Interop.AccentFlags.DrawRightBorder;
            //        break;
            //}

            return flags;
        }

        /// <summary>
        /// Applies glass to the current window, API was for Vista, but should work for Win 7 upwards
        /// </summary>
        /// <remarks>
        /// This appears to only "Blurs" normal controls, not the ones being themed by Krypton
        /// </remarks>
        public static void ApplyGlass(this Form window, bool apply)
        {
            PI.DWM_BLURBEHIND blurBehindParameters = new(apply)
            {
                dwFlags = PI.DWM_BB.Enable,
                hRgnBlur = IntPtr.Zero
            };

            PI.DwmEnableBlurBehindWindow(window.Handle, ref blurBehindParameters);
        }
    }
}
