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

        private static DateTime CreateDateTimeToastNotification(KryptonUserInputToastNotificationData data) => VisualToastNotificationDateTimeUserInputForm.ShowNotification(data);

        internal static int ReturnIntegerInput(KryptonUserInputToastNotificationData data)
        {
            int result = CreateIntegerToastNotification(data);

            return result;
        }

        private static int CreateIntegerToastNotification(KryptonUserInputToastNotificationData data) => VisualToastNotificationNumericUpDownUserInputForm.ShowNotification(data);

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
                    return VisualToastNotificationDomainUpDownUserInputForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.MaskedTextBox:
                    return VisualToastNotificationMaskedTextBoxUserInputForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.TextBox:
                    return VisualToastNotificationTextBoxUserInputForm.ShowNotification(data);
            }

            return string.Empty;
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

        private static DateTime CreateDateTimeToastWithProgressBarNotification(KryptonUserInputToastNotificationData data) => VisualToastNotificationDateTimeUserInputWithProgressBarForm.ShowToast(data);

        private static int ReturnIntegerInputWithProgressBar(KryptonUserInputToastNotificationData data)
        {
            int result = CreateIntegerToastWithProgressBarNotification(data);

            return result;
        }

        private static int CreateIntegerToastWithProgressBarNotification(KryptonUserInputToastNotificationData data) => VisualToastNotificationNumericUpDownUserInputWithProgressBarForm.ShowToast(data);

        private static string ReturnStringInputWithProgressBar(KryptonUserInputToastNotificationData data)
        {
            string result = CreateStringToastWithProgressBarNotification(data);

            return result;
        }

        private static string CreateStringToastWithProgressBarNotification(KryptonUserInputToastNotificationData data)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastNotificationInputAreaType.ComboBox:
                    return VisualToastNotificationComboBoxUserInputWithProgressBarForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.DomainUpDown:
                    return VisualToastNotificationDomianUpDownInputWithProgressBarForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.MaskedTextBox:
                    return VisualToastNotificationMaskedTextBoxInputWithProgressBarForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.TextBox:
                    return VisualToastNotificationTextBoxUserInputWithProgressBarForm.ShowNotification(data);
            }

            return string.Empty;
        }

        #endregion

        #endregion
    }
}