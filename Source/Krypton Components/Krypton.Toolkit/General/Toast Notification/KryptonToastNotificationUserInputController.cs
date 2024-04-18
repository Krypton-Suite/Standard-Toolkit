#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Manages the interaction between the <see cref="KryptonToastNotification"/> API and the Krypton toast notification user input types.</summary>
    internal class KryptonToastNotificationUserInputController
    {
        #region Implementation

        #region Normal Left to Right Toasts

        public static object ShowToast(KryptonUserInputToastNotificationData data)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastNotificationInputAreaType.ComboBox:
                case KryptonToastNotificationInputAreaType.DomainUpDown:
                case KryptonToastNotificationInputAreaType.MaskedTextBox:
                case KryptonToastNotificationInputAreaType.TextBox:
                    return ReturnStringInput(data);
                case KryptonToastNotificationInputAreaType.DateTime:
                    return ReturnDateTimeInput(data);
                case KryptonToastNotificationInputAreaType.NumericUpDown:
                    return ReturnIntegerInput(data);
                case null:
                    throw new ArgumentNullException();
                default:
                    DebugTools.NotImplemented(data.ToString());
                    break;
            }

            return new object();
        }

        internal static DateTime ReturnDateTimeInput(KryptonUserInputToastNotificationData data)
        {
            DateTime result = CreateDateTimeToastNotification(data);

            return result;
        }

        private static DateTime CreateDateTimeToastNotification(KryptonUserInputToastNotificationData data)
        {
            var ktdtp = new VisualToastNotificationDateTimeUserInputForm(data);

            return ktdtp.ShowNotification(data);
        }

        internal static int ReturnIntegerInput(KryptonUserInputToastNotificationData data)
        {
            int result = CreateIntegerToastNotification(data);

            return result;
        }

        private static int CreateIntegerToastNotification(KryptonUserInputToastNotificationData data)
        {
            var ktnud = new VisualToastNotificationNumericUpDownUserInputForm(data);

            return ktnud.ShowNotification(data);
        }

        internal static string ReturnStringInput(KryptonUserInputToastNotificationData data)
        {
            string result = CreateStringToastNotification(data);

            return result;
        }

        private static string CreateStringToastNotification(KryptonUserInputToastNotificationData data)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastNotificationInputAreaType.ComboBox:
                    return VisualToastNotificationComboBoxUserInputForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.DomainUpDown:
                    var ktdud = new VisualToastNotificationDomainUpDownUserInputForm(data);

                    return ktdud.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.MaskedTextBox:
                    var ktmtxt = new VisualToastNotificationMaskedTextBoxUserInputForm(data);

                    return ktmtxt.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.TextBox:
                    var kttxt = new VisualToastNotificationTextBoxUserInputForm(data);

                    return kttxt.ShowNotification(data);
            }

            return string.Empty;
        }

        /// <summary>Shows the notification with ComboBox.</summary>
        /// <param name="owner">The owner.</param>
        /// <param name="notificationMessage">The notification message.</param>
        /// <param name="notificationTitle">The notification title.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="userInputItemList">The user input item list.</param>
        /// <param name="initialSelectedIndex">Initial index of the selected.</param>
        /// <param name="inputBoxStyle">The input box style.</param>
        /// <param name="borderColorOne">The border color one.</param>
        /// <param name="borderColorTwo">The border color two.</param>
        /// <param name="countDownSeconds">The count-down seconds.</param>
        /// <returns></returns>
        internal static string ShowNotificationWithComboBox(IWin32Window? owner,
            string notificationMessage,
            string? notificationTitle,
            KryptonToastNotificationIcon? icon,
            ArrayList userInputItemList,
            int? initialSelectedIndex,
            ComboBoxStyle? inputBoxStyle,
            Color? borderColorOne,
            Color? borderColorTwo,
            int? countDownSeconds) => VisualToastNotificationComboBoxUserInputForm.ShowNotification(owner,
            notificationMessage, notificationTitle, icon, userInputItemList, initialSelectedIndex, inputBoxStyle,
            borderColorOne, borderColorTwo, countDownSeconds);

        internal static string ShowNotificationWithComboBox(KryptonUserInputToastNotificationData data)
        {
            return VisualToastNotificationComboBoxUserInputForm.ShowNotification(data);
        }

        #endregion

        #region Left to Right Toasts with Progress Bars

        public static object ShowToastWithProgressBar(KryptonUserInputToastNotificationData data)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastNotificationInputAreaType.ComboBox:
                case KryptonToastNotificationInputAreaType.DomainUpDown:
                case KryptonToastNotificationInputAreaType.MaskedTextBox:
                case KryptonToastNotificationInputAreaType.TextBox:
                    return ReturnStringInputWithProgressBar(data);
                case KryptonToastNotificationInputAreaType.DateTime:
                    return ReturnDateTimeInputWithProgressBar(data);
                case KryptonToastNotificationInputAreaType.NumericUpDown:
                    return ReturnIntegerInputWithProgressBar(data);
                case null:
                    throw new ArgumentNullException();
                default:
                    DebugTools.NotImplemented(data.ToString());
                    break;
            }

            return new object();
        }

        private static DateTime ReturnDateTimeInputWithProgressBar(KryptonUserInputToastNotificationData data)
        {
            DateTime result = CreateDateTimeToastWithProgressBarNotification(data);

            return result;
        }

        private static DateTime CreateDateTimeToastWithProgressBarNotification(KryptonUserInputToastNotificationData data)
        {
            throw new NotImplementedException();
        }

        private static int ReturnIntegerInputWithProgressBar(KryptonUserInputToastNotificationData data)
        {
            int result = CreateIntegerToastWithProgressBarNotification(data);

            return result;
        }

        private static int CreateIntegerToastWithProgressBarNotification(KryptonUserInputToastNotificationData data)
        {
            throw new NotImplementedException();
        }

        private static string ReturnStringInputWithProgressBar(KryptonUserInputToastNotificationData data)
        {
            string result = CreateStringToastWithProgressBarNotification(data);

            return result;
        }

        private static string CreateStringToastWithProgressBarNotification(KryptonUserInputToastNotificationData data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}