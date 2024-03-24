#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>The public interface to the <see cref="VisualToastForm"/> class.</summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonToastNotification
    {
        #region Public

        #region Basic Notification

        /// <summary>Shows the basic notification with a boolean return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns>A boolean value, based on the 'Do not show again' option.</returns>
        public static bool ShowBasicNotificationWithBooleanReturnValue(KryptonBasicToastNotificationData toastNotificationData)
            => VisualToastNotificationBasicForm.InternalShowWithBooleanReturnValue(toastNotificationData);

        /// <summary>Shows the basic notification with a <see cref="T:CheckState"/> return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns>A <see cref="T:CheckState"/> value, based on the 'Do not show again' option.b</returns>
        public static CheckState ShowBasicNotificationWithCheckStateReturnValue(KryptonBasicToastNotificationData toastNotificationData)
            => VisualToastNotificationBasicForm.InternalShowWithCheckStateReturnValue(toastNotificationData);

        /// <summary>Shows the basic notification.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        public static void ShowBasicNotification(KryptonBasicToastNotificationData toastNotificationData) =>
            VisualToastNotificationBasicForm.InternalShow(toastNotificationData);

        #endregion

        #region Basic Notification with Progress Bar

        /// <summary>Shows the basic progress bar notification with a boolean return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns>A boolean value, based on the 'Do not show again' option.</returns>
        public static bool ShowBasicProgressBarNotificationWithBooleanReturnValue(KryptonBasicToastNotificationData toastNotificationData) =>
            VisualToastNotificationBasicWithProgressBarForm.InternalShowWithBooleanReturnValue(toastNotificationData);

        /// <summary>Shows the basic progress bar notification with a <see cref="T:CheckState"/> return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns>A <see cref="T:CheckState"/> value, based on the 'Do not show again' option.</returns>
        public static CheckState ShowBasicProgressBarNotificationWithCheckStateReturnValue(KryptonBasicToastNotificationData toastNotificationData) =>
            VisualToastNotificationBasicWithProgressBarForm.InternalShowWithCheckStateReturnValue(toastNotificationData);

        /// <summary>Shows the basic progress bar notification.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        public static void ShowBasicProgressBarNotification(KryptonBasicToastNotificationData toastNotificationData) =>
            VisualToastNotificationBasicWithProgressBarForm.InternalShow(toastNotificationData);

        #endregion

        public static bool ShowUserInputNotificationWithBooleanReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputForm.InternalShowWithBooleanReturnValue(toastNotificationData);

        public static CheckState ShowUserInputNotificationWithCheckStateReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputForm.InternalShowWithCheckStateReturnValue(toastNotificationData);

        public static DateTime ShowUserInputNotificationWithDateTimeReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputForm.InternalShowWithDateTimeReturnValue(toastNotificationData);

        public static string ShowUserInputNotificationWithStringReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputForm.InternalShowWithStringReturnValue(toastNotificationData);

        public static int ShowUserInputNotificationWithIntegerReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputForm.InternalShowWithIntegerReturnValue(toastNotificationData);

        #endregion

        #region Implementation

        internal static bool InternalShowBasicNotificationReturnBool(KryptonCommonToastNotificationData commonData)
        {
            return false;
        }

        internal static CheckState InternalShowBasicNotificationReturnCheckState(KryptonCommonToastNotificationData commonData)
        {
            return CheckState.Unchecked;
        }

        internal static DateTime InternalShowBasicNotificationReturnDateTime(KryptonCommonToastNotificationData commonData)
        {
            return DateTime.Now;
        }

        internal static void InternalShowBasicNotification(KryptonBasicToastNotificationData toastNotificationData)
        {
            if (toastNotificationData.UseRtlReading)
            {
                var ktbnRtl = new VisualToastNotificationBasicRtlAwareForm(toastNotificationData);

                ktbnRtl.Show();
            }
            else
            {
                var kbtn = new VisualToastNotificationBasicForm(toastNotificationData);

                kbtn.Show();
            }
        }

        internal static KryptonToastNotificationResponseType ShowToastNotification(
            KryptonCommonToastNotificationData commonData, KryptonToastNotificationResponseType responseType)
        {
            switch (responseType)
            {
                case KryptonToastNotificationResponseType.Bool:
                    break;
                case KryptonToastNotificationResponseType.CheckedState:
                    break;
                case KryptonToastNotificationResponseType.ComboBox:
                    break;
                case KryptonToastNotificationResponseType.DateTime:
                    break;
                case KryptonToastNotificationResponseType.DialogResult:
                    break;
                case KryptonToastNotificationResponseType.Timeout:
                    break;
                case KryptonToastNotificationResponseType.String:
                    break;
                default:
                    DebugTools.NotImplemented(responseType.ToString());
                    break;
            }

            return responseType;
        }

        #endregion
    }
}