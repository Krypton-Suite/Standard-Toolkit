#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>Handles the heavy lifting of creating toast notifications.</summary>
internal class KryptonToastController
{
    #region Implementation

    #region Basic Toast Notification

    #region Normal

    internal static bool ShowNotificationWithBooleanDoNotShowAgainReturnValue(KryptonBasicToastData toastNotificationData) => toastNotificationData.UseRtlReading ? VisualToastBasicRtlAwareForm.InternalShowWithBooleanReturnValue(toastNotificationData) : VisualToastBasicForm.InternalShowWithBooleanReturnValue(toastNotificationData);

    internal static CheckState ShowNotificationWithCheckStateDoNotShowAgainReturnValue(KryptonBasicToastData toastNotificationData) => toastNotificationData.UseRtlReading ? VisualToastBasicRtlAwareForm.InternalShowWithCheckStateReturnValue(toastNotificationData) : VisualToastBasicForm.InternalShowWithCheckStateReturnValue(toastNotificationData);

    internal static void ShowBasicToastNotification(KryptonBasicToastData toastNotificationData)
    {
        if (toastNotificationData.UseRtlReading)
        {
            VisualToastBasicRtlAwareForm.InternalShow(toastNotificationData);
        }
        else
        {
            VisualToastBasicForm.InternalShow(toastNotificationData);
        }
    }

    #endregion

    #region With Progress Bars

    internal static bool ShowBasicProgressBarNotificationWithBooleanReturnValue(KryptonBasicToastData toastNotificationData) => toastNotificationData.UseRtlReading ? VisualToastBasicWithProgressBarRtlAwareForm.InternalShowWithBooleanReturnValue(toastNotificationData) : VisualToastBasicWithProgressBarForm.InternalShowWithBooleanReturnValue(toastNotificationData);

    internal static CheckState ShowBasicProgressBarNotificationWithCheckStateReturnValue(
        KryptonBasicToastData toastNotificationData) =>
        toastNotificationData.UseRtlReading
            ? VisualToastBasicWithProgressBarRtlAwareForm.InternalShowWithCheckStateReturnValue(
                toastNotificationData)
            : VisualToastBasicWithProgressBarForm.InternalShowWithCheckStateReturnValue(
                toastNotificationData);

    internal static void ShowBasicProgressBarNotification(KryptonBasicToastData toastNotificationData)
    {
        if (toastNotificationData.UseRtlReading)
        {
            VisualToastBasicWithProgressBarRtlAwareForm.ShowNotification(toastNotificationData);
        }
        else
        {
            VisualToastBasicWithProgressBarForm.InternalShow(toastNotificationData);
        }
    }

    #endregion

    #endregion

    #region User Input Toast Notification

    #region Normal Left to Right Toasts

    /// <summary>Shows the toast.</summary>
    /// <param name="data">The data.</param>
    /// <returns>A <see cref="KryptonToast"/> with all appropriate data.</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    internal static object ShowToast(KryptonUserInputToastData data)
    {
        switch (data.NotificationInputAreaType)
        {
            case KryptonToastInputAreaType.ComboBox:
            case KryptonToastInputAreaType.DomainUpDown:
            case KryptonToastInputAreaType.MaskedTextBox:
            case KryptonToastInputAreaType.TextBox:
            case null:
                return ReturnStringInput(data);
            case KryptonToastInputAreaType.DateTime:
                return ReturnDateTimeInput(data);
            case KryptonToastInputAreaType.NumericUpDown:
                return ReturnDecimalInput(data);
            default:
                DebugTools.NotImplemented(data.ToString());
                break;
        }

        return new object();
    }

    internal static DateTime ReturnDateTimeInput(KryptonUserInputToastData data)
    {
        DateTime result = CreateDateTimeToastNotification(data);

        return result;
    }

    private static DateTime CreateDateTimeToastNotification(KryptonUserInputToastData data) => data.UseRtlReading ? VisualToastDateTimeUserInputRtlAwareForm.ShowNotification(data) : VisualToastDateTimeUserInputForm.ShowNotification(data);

    internal static decimal ReturnDecimalInput(KryptonUserInputToastData data)
    {
        decimal result = CreateDecimalToastNotification(data);

        return result;
    }

    private static decimal CreateDecimalToastNotification(KryptonUserInputToastData data) => data.UseRtlReading ? VisualToastNUDUserInputRtlAwareForm.ShowNotification(data) : VisualToastNUDUserInputForm.ShowNotification(data);

    internal static string ReturnStringInput(KryptonUserInputToastData data)
    {
        string result = CreateStringToastNotification(data);

        return result;
    }

    private static string CreateStringToastNotification(KryptonUserInputToastData data)
    {
        if (data.UseRtlReading)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastInputAreaType.ComboBox:
                    return VisualToastComboBoxUserInputRtlAwareForm.ShowNotification(data);
                case KryptonToastInputAreaType.DomainUpDown:
                    return VisualToastDomainUpDownUserInputRtlAwareForm.ShowNotification(data);
                case KryptonToastInputAreaType.MaskedTextBox:
                    return VisualToastMaskedTextBoxUserInputRtlAwareForm.ShowNotification(data);
                case KryptonToastInputAreaType.TextBox:
                    return VisualToastTextBoxUserInputRtlAwareForm.ShowNotification(data);
            }
        }
        else
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastInputAreaType.ComboBox:
                    return VisualToastComboBoxUserInputForm.ShowNotification(data);
                case KryptonToastInputAreaType.DomainUpDown:
                    return VisualToastDomainUpDownUserInputForm.ShowNotification(data);
                case KryptonToastInputAreaType.MaskedTextBox:
                    return VisualToastMaskedTextBoxUserInputForm.ShowNotification(data);
                case KryptonToastInputAreaType.TextBox:
                    return VisualToastTextBoxUserInputForm.ShowNotification(data);
            }
        }

        return string.Empty;
    }

    #endregion

    #region Left to Right Toasts with Progress Bars

    internal static object ShowToastWithProgressBar(KryptonUserInputToastData data)
    {
        switch (data.NotificationInputAreaType)
        {
            case KryptonToastInputAreaType.ComboBox:
            case KryptonToastInputAreaType.DomainUpDown:
            case KryptonToastInputAreaType.MaskedTextBox:
            case KryptonToastInputAreaType.TextBox:
                return ReturnStringInputWithProgressBar(data);
            case KryptonToastInputAreaType.DateTime:
                return ReturnDateTimeInputWithProgressBar(data);
            case KryptonToastInputAreaType.NumericUpDown:
                return ReturnDecimalInputWithProgressBar(data);
            case null:
                throw new ArgumentNullException();
            default:
                DebugTools.NotImplemented(data.ToString());
                break;
        }

        return new object();
    }

    private static DateTime ReturnDateTimeInputWithProgressBar(KryptonUserInputToastData data)
    {
        DateTime result = CreateDateTimeToastWithProgressBarNotification(data);

        return result;
    }

    private static DateTime CreateDateTimeToastWithProgressBarNotification(KryptonUserInputToastData data) => data.UseRtlReading ? VisualToastDateTimeUserInputWithProgressBarRtlAwareForm.ShowNotification(data) : VisualToastDateTimeUserInputWithProgressBarForm.ShowNotification(data);

    private static decimal ReturnDecimalInputWithProgressBar(KryptonUserInputToastData data)
    {
        decimal result = CreateDecimalToastWithProgressBarNotification(data);

        return result;
    }

    private static decimal CreateDecimalToastWithProgressBarNotification(KryptonUserInputToastData data) => data.UseRtlReading ? VisualToastNUDUserInputWithProgressBarRtlAwareForm.ShowToastNotification(data) : VisualToastNUDUserInputWithProgressBarForm.ShowToastNotification(data);

    private static string ReturnStringInputWithProgressBar(KryptonUserInputToastData data)
    {
        string result = CreateStringToastWithProgressBarNotification(data);

        return result;
    }

    private static string CreateStringToastWithProgressBarNotification(KryptonUserInputToastData data)
    {
        if (data.UseRtlReading)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastInputAreaType.ComboBox:
                    return VisualToastComboBoxUserInputWithProgressBarRtlAwareForm.ShowNotification(data);
                case KryptonToastInputAreaType.DomainUpDown:
                    return VisualToastDomainUpDownInputWithProgressBarRtlAwareForm.ShowNotification(data);
                case KryptonToastInputAreaType.MaskedTextBox:
                    return VisualToastMaskedTextBoxInputWithProgressBarRtlAwareForm.ShowNotification(data);
                case KryptonToastInputAreaType.TextBox:
                    return VisualToastTextBoxUserInputWithProgressBarRtlAwareForm.ShowNotification(data);
            }
        }
        else
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastInputAreaType.ComboBox:
                    return VisualToastComboBoxUserInputWithProgressBarForm.ShowNotification(data);
                case KryptonToastInputAreaType.DomainUpDown:
                    return VisualToastDomianUpDownInputWithProgressBarForm.ShowNotification(data);
                case KryptonToastInputAreaType.MaskedTextBox:
                    return VisualToastMaskedTextBoxInputWithProgressBarForm.ShowNotification(data);
                case KryptonToastInputAreaType.TextBox:
                    return VisualToastTextBoxUserInputWithProgressBarForm.ShowNotification(data);
            }
        }

        return string.Empty;
    }

    #endregion

    #endregion

    #endregion
}