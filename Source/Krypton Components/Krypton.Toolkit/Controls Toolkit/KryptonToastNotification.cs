#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved.
 *
 */
#endregion

#pragma warning disable VSSpell001

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

        #region Notification with Return Values

        #region Left to Right Reading

        /// <summary>Shows the notification with ComboBox.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static object ShowNotification(KryptonUserInputToastNotificationData data) =>
            KryptonToastNotificationUserInputController.ShowToast(data);

        #endregion

        #region Right to Left Reading

        /// <summary>Shows the user input notification with boolean return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static bool ShowRtlUserInputNotificationWithBooleanReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputRtlAwareForm.InternalShowWithBooleanReturnValue(toastNotificationData);

        /// <summary>Shows the RTL user input notification with progress bar and boolean return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns> </returns>
        public static bool ShowRtlUserInputNotificationWithProgressBarAndBooleanReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputWithProgressBarRtlAwareForm.InternalShowWithBooleanReturnValue(toastNotificationData);

        /// <summary>Shows the user input notification with check state return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static CheckState ShowRtlUserInputNotificationWithCheckStateReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputRtlAwareForm.InternalShowWithCheckStateReturnValue(toastNotificationData);

        /// <summary>Shows the RTL user input notification with progress bar and check state return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static CheckState ShowRtlUserInputNotificationWithProgressBarAndCheckStateReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputWithProgressBarRtlAwareForm.InternalShowWithCheckStateReturnValue(toastNotificationData);

        /// <summary>Shows the user input notification with date time return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static DateTime ShowRtlUserInputNotificationWithDateTimeReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputRtlAwareForm.InternalShowWithDateTimeReturnValue(toastNotificationData);

        /// <summary>Shows the RTL user input notification with progress bar and date time return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static DateTime ShowRtlUserInputNotificationWithProgressBarAndDateTimeReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputWithProgressBarRtlAwareForm.InternalShowWithDateTimeReturnValue(toastNotificationData);

        /// <summary>Shows the user input notification with string return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static string ShowRtlUserInputNotificationWithStringReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputRtlAwareForm.InternalShowWithStringReturnValue(toastNotificationData);

        /// <summary>Shows the RTL user input notification with progress bar and string return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static string ShowRtlUserInputNotificationWithProgressBarAndStringReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputWithProgressBarRtlAwareForm.InternalShowWithStringReturnValue(toastNotificationData);

        /// <summary>Shows the user input notification with integer return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static int ShowRtlUserInputNotificationWithIntegerReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputRtlAwareForm.InternalShowWithIntegerReturnValue(toastNotificationData);

        /// <summary>Shows the RTL user input notification with progress bar and integer return value.</summary>
        /// <param name="toastNotificationData">The toast notification data.</param>
        /// <returns></returns>
        public static int ShowRtlUserInputNotificationWithProgressBarAndIntegerReturnValue(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputWithProgressBarRtlAwareForm.InternalShowWithIntegerReturnValue(toastNotificationData);

        #endregion

        #endregion

        #endregion

        public static object ShowNotificationWithProgressBar(KryptonUserInputToastNotificationData data)
        {
            throw new NotImplementedException();
        }
    }
}