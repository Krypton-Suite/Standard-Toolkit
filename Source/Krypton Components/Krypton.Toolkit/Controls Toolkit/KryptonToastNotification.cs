#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

#pragma warning disable VSSpell001

namespace Krypton.Toolkit;

/// <summary>The public interface to the <see cref="KryptonToastNotificationController"/> class.</summary>
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
        => KryptonToastNotificationController.ShowNotificationWithBooleanDoNotShowAgainReturnValue(toastNotificationData);

    /// <summary>Shows the basic notification with a <see cref="T:CheckState"/> return value.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    /// <returns>A <see cref="T:CheckState"/> value, based on the 'Do not show again' option.b</returns>
    public static CheckState ShowBasicNotificationWithCheckStateReturnValue(KryptonBasicToastNotificationData toastNotificationData)
        => KryptonToastNotificationController.ShowNotificationWithCheckStateDoNotShowAgainReturnValue(toastNotificationData);

    /// <summary>Shows the basic notification.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    public static void ShowBasicNotification(KryptonBasicToastNotificationData toastNotificationData) =>
        KryptonToastNotificationController.ShowBasicToastNotification(toastNotificationData);

    #endregion

    #region Basic Notification with Progress Bar

    /// <summary>Shows the basic progress bar notification with a boolean return value.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    /// <returns>A boolean value, based on the 'Do not show again' option.</returns>
    public static bool ShowBasicProgressBarNotificationWithBooleanReturnValue(KryptonBasicToastNotificationData toastNotificationData) =>
        KryptonToastNotificationController.ShowBasicProgressBarNotificationWithBooleanReturnValue(toastNotificationData);

    /// <summary>Shows the basic progress bar notification with a <see cref="T:CheckState"/> return value.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    /// <returns>A <see cref="T:CheckState"/> value, based on the 'Do not show again' option.</returns>
    public static CheckState ShowBasicProgressBarNotificationWithCheckStateReturnValue(KryptonBasicToastNotificationData toastNotificationData) =>
        KryptonToastNotificationController.ShowBasicProgressBarNotificationWithCheckStateReturnValue(toastNotificationData);

    /// <summary>Shows the basic progress bar notification.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    public static void ShowBasicProgressBarNotification(KryptonBasicToastNotificationData toastNotificationData) =>
        KryptonToastNotificationController.ShowBasicProgressBarNotification(toastNotificationData);

    #endregion

    #region Notification with Return Values

    #region Left to Right Reading

    /// <summary>Shows the notification with ComboBox.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static object ShowNotification(KryptonUserInputToastNotificationData data) =>
        KryptonToastNotificationController.ShowToast(data);

    /// <summary>Shows the notification with progress bar.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static object ShowNotificationWithProgressBar(KryptonUserInputToastNotificationData data) =>
        KryptonToastNotificationController.ShowToastWithProgressBar(data);

    #endregion

    #endregion

    #endregion
}