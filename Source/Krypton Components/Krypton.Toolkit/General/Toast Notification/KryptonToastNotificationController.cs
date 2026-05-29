#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Handles the heavy lifting of creating toast notifications.</summary>
internal class KryptonToastNotificationController
{
    #region Implementation

    #region Basic Toast Notification

    #region Normal

    internal static bool ShowNotificationWithBooleanDoNotShowAgainReturnValue(KryptonBasicToastNotificationData toastNotificationData) => toastNotificationData.UseRtlReading ? VisualToastNotificationBasicRtlAwareForm.InternalShowWithBooleanReturnValue(toastNotificationData) : VisualToastNotificationBasicForm.InternalShowWithBooleanReturnValue(toastNotificationData);

    internal static CheckState ShowNotificationWithCheckStateDoNotShowAgainReturnValue(KryptonBasicToastNotificationData toastNotificationData) => toastNotificationData.UseRtlReading ? VisualToastNotificationBasicRtlAwareForm.InternalShowWithCheckStateReturnValue(toastNotificationData) : VisualToastNotificationBasicForm.InternalShowWithCheckStateReturnValue(toastNotificationData);

    internal static void ShowBasicToastNotification(KryptonBasicToastNotificationData toastNotificationData)
    {
        if (toastNotificationData.UseRtlReading)
        {
            VisualToastNotificationBasicRtlAwareForm.InternalShow(toastNotificationData);
        }
        else
        {
            VisualToastNotificationBasicForm.InternalShow(toastNotificationData);
        }
    }

    #endregion

    #region With Progress Bars

    internal static bool ShowBasicProgressBarNotificationWithBooleanReturnValue(KryptonBasicToastNotificationData toastNotificationData) => toastNotificationData.UseRtlReading ? VisualToastNotificationBasicWithProgressBarRtlAwareForm.InternalShowWithBooleanReturnValue(toastNotificationData) : VisualToastNotificationBasicWithProgressBarForm.InternalShowWithBooleanReturnValue(toastNotificationData);

    internal static CheckState ShowBasicProgressBarNotificationWithCheckStateReturnValue(
        KryptonBasicToastNotificationData toastNotificationData) =>
        toastNotificationData.UseRtlReading
            ? VisualToastNotificationBasicWithProgressBarRtlAwareForm.InternalShowWithCheckStateReturnValue(
                toastNotificationData)
            : VisualToastNotificationBasicWithProgressBarForm.InternalShowWithCheckStateReturnValue(
                toastNotificationData);

    internal static void ShowBasicProgressBarNotification(KryptonBasicToastNotificationData toastNotificationData)
    {
        if (toastNotificationData.UseRtlReading)
        {
            VisualToastNotificationBasicWithProgressBarRtlAwareForm.ShowNotification(toastNotificationData);
        }
        else
        {
            VisualToastNotificationBasicWithProgressBarForm.InternalShow(toastNotificationData);
        }
    }

    #endregion

    #endregion

    #region User Input Toast Notification

    #region Normal Left to Right Toasts

    /// <summary>Shows the toast.</summary>
    /// <param name="data">The data.</param>
    /// <returns>A <see cref="KryptonToastNotification"/> with all appropriate data.</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    internal static object ShowToast(KryptonUserInputToastNotificationData data)
    {
        switch (data.NotificationInputAreaType)
        {
            case KryptonToastNotificationInputAreaType.ComboBox:
            case KryptonToastNotificationInputAreaType.DomainUpDown:
            case KryptonToastNotificationInputAreaType.MaskedTextBox:
            case KryptonToastNotificationInputAreaType.TextBox:
            case null:
                return ReturnStringInput(data);
            case KryptonToastNotificationInputAreaType.DateTime:
                return ReturnDateTimeInput(data);
            case KryptonToastNotificationInputAreaType.NumericUpDown:
                return ReturnDecimalInput(data);
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

    private static DateTime CreateDateTimeToastNotification(KryptonUserInputToastNotificationData data) => data.UseRtlReading ? VisualToastNotificationDateTimeUserInputRtlAwareForm.ShowNotification(data) : VisualToastNotificationDateTimeUserInputForm.ShowNotification(data);

    internal static decimal ReturnDecimalInput(KryptonUserInputToastNotificationData data)
    {
        decimal result = CreateDecimalToastNotification(data);

        return result;
    }

    private static decimal CreateDecimalToastNotification(KryptonUserInputToastNotificationData data) => data.UseRtlReading ? VisualToastNotificationNUDUserInputRtlAwareForm.ShowNotification(data) : VisualToastNotificationNUDUserInputForm.ShowNotification(data);

    internal static string ReturnStringInput(KryptonUserInputToastNotificationData data)
    {
        string result = CreateStringToastNotification(data);

        return result;
    }

    private static string CreateStringToastNotification(KryptonUserInputToastNotificationData data)
    {
        if (data.UseRtlReading)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastNotificationInputAreaType.ComboBox:
                    return VisualToastNotificationComboBoxUserInputRtlAwareForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.DomainUpDown:
                    return VisualToastNotificationDomainUpDownUserInputRtlAwareForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.MaskedTextBox:
                    return VisualToastNotificationMaskedTextBoxUserInputRtlAwareForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.TextBox:
                    return VisualToastNotificationTextBoxUserInputRtlAwareForm.ShowNotification(data);
            }
        }
        else
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
        }

        return string.Empty;
    }

    #endregion

    #region Left to Right Toasts with Progress Bars

    internal static object ShowToastWithProgressBar(KryptonUserInputToastNotificationData data)
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
                return ReturnDecimalInputWithProgressBar(data);
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

    private static DateTime CreateDateTimeToastWithProgressBarNotification(KryptonUserInputToastNotificationData data) => data.UseRtlReading ? VisualToastNotificationDateTimeUserInputWithProgressBarRtlAwareForm.ShowNotification(data) : VisualToastNotificationDateTimeUserInputWithProgressBarForm.ShowNotification(data);

    private static decimal ReturnDecimalInputWithProgressBar(KryptonUserInputToastNotificationData data)
    {
        decimal result = CreateDecimalToastWithProgressBarNotification(data);

        return result;
    }

    private static decimal CreateDecimalToastWithProgressBarNotification(KryptonUserInputToastNotificationData data) => data.UseRtlReading ? VisualToastNotificationNUDUserInputWithProgressBarRtlAwareForm.ShowToastNotification(data) : VisualToastNotificationNUDUserInputWithProgressBarForm.ShowToastNotification(data);

    private static string ReturnStringInputWithProgressBar(KryptonUserInputToastNotificationData data)
    {
        string result = CreateStringToastWithProgressBarNotification(data);

        return result;
    }

    private static string CreateStringToastWithProgressBarNotification(KryptonUserInputToastNotificationData data)
    {
        if (data.UseRtlReading)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastNotificationInputAreaType.ComboBox:
                    return VisualToastNotificationComboBoxUserInputWithProgressBarRtlAwareForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.DomainUpDown:
                    return VisualToastNotificationDomainUpDownInputWithProgressBarRtlAwareForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.MaskedTextBox:
                    return VisualToastNotificationMaskedTextBoxInputWithProgressBarRtlAwareForm.ShowNotification(data);
                case KryptonToastNotificationInputAreaType.TextBox:
                    return VisualToastNotificationTextBoxUserInputWithProgressBarRtlAwareForm.ShowNotification(data);
            }
        }
        else
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
        }

        return string.Empty;
    }

    #endregion

    #endregion

    #endregion
}