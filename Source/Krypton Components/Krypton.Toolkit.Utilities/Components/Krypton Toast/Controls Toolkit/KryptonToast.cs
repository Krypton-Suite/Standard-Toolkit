#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

#pragma warning disable VSSpell001

namespace Krypton.Toolkit.Utilities;

/// <summary>The public interface to the <see cref="KryptonToastController"/> class.</summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonToast
{
    #region Public

    #region Basic Notification

    /// <summary>Shows the basic notification with a boolean return value.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    /// <returns>A boolean value, based on the 'Do not show again' option.</returns>
    public static bool ShowBasicNotificationWithBooleanReturnValue(KryptonBasicToastData toastNotificationData)
        => KryptonToastController.ShowNotificationWithBooleanDoNotShowAgainReturnValue(toastNotificationData);

    /// <summary>Shows the basic notification with a <see cref="T:CheckState"/> return value.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    /// <returns>A <see cref="T:CheckState"/> value, based on the 'Do not show again' option.b</returns>
    public static CheckState ShowBasicNotificationWithCheckStateReturnValue(KryptonBasicToastData toastNotificationData)
        => KryptonToastController.ShowNotificationWithCheckStateDoNotShowAgainReturnValue(toastNotificationData);

    /// <summary>Shows the basic notification.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    public static void ShowBasicNotification(KryptonBasicToastData toastNotificationData) =>
        KryptonToastController.ShowBasicToastNotification(toastNotificationData);

    /// <summary>Shows the basic notification with a boolean return value asynchronously.</summary>
    public static Task<bool> ShowBasicNotificationWithBooleanReturnValueAsync(KryptonBasicToastData toastNotificationData) =>
        KryptonToastController.ShowNotificationWithBooleanDoNotShowAgainReturnValueAsync(toastNotificationData);

    /// <summary>Shows the basic notification with a <see cref="CheckState"/> return value asynchronously.</summary>
    public static Task<CheckState> ShowBasicNotificationWithCheckStateReturnValueAsync(KryptonBasicToastData toastNotificationData) =>
        KryptonToastController.ShowNotificationWithCheckStateDoNotShowAgainReturnValueAsync(toastNotificationData);
    #endregion

    #region Basic Notification with Progress Bar

    /// <summary>Shows the basic progress bar notification with a boolean return value.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    /// <returns>A boolean value, based on the 'Do not show again' option.</returns>
    public static bool ShowBasicProgressBarNotificationWithBooleanReturnValue(KryptonBasicToastData toastNotificationData) =>
        KryptonToastController.ShowBasicProgressBarNotificationWithBooleanReturnValue(toastNotificationData);

    /// <summary>Shows the basic progress bar notification with a <see cref="T:CheckState"/> return value.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    /// <returns>A <see cref="T:CheckState"/> value, based on the 'Do not show again' option.</returns>
    public static CheckState ShowBasicProgressBarNotificationWithCheckStateReturnValue(KryptonBasicToastData toastNotificationData) =>
        KryptonToastController.ShowBasicProgressBarNotificationWithCheckStateReturnValue(toastNotificationData);

    /// <summary>Shows the basic progress bar notification.</summary>
    /// <param name="toastNotificationData">The toast notification data.</param>
    public static void ShowBasicProgressBarNotification(KryptonBasicToastData toastNotificationData) =>
        KryptonToastController.ShowBasicProgressBarNotification(toastNotificationData);

    /// <summary>Shows the basic progress bar notification with a boolean return value asynchronously.</summary>
    public static Task<bool> ShowBasicProgressBarNotificationWithBooleanReturnValueAsync(KryptonBasicToastData toastNotificationData) =>
        KryptonToastController.ShowBasicProgressBarNotificationWithBooleanReturnValueAsync(toastNotificationData);

    /// <summary>Shows the basic progress bar notification with a <see cref="CheckState"/> return value asynchronously.</summary>
    public static Task<CheckState> ShowBasicProgressBarNotificationWithCheckStateReturnValueAsync(KryptonBasicToastData toastNotificationData) =>
        KryptonToastController.ShowBasicProgressBarNotificationWithCheckStateReturnValueAsync(toastNotificationData);
    #endregion

    #region Notification with Return Values

    #region Left to Right Reading

    /// <summary>Shows the notification with ComboBox.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static object ShowNotification(KryptonUserInputToastData data) =>
        KryptonToastController.ShowToast(data);

    /// <summary>Shows the notification with progress bar.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static object ShowNotificationWithProgressBar(KryptonUserInputToastData data) =>
        KryptonToastController.ShowToastWithProgressBar(data);
    /// <summary>Shows the user-input notification asynchronously.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The typed user response boxed as <see cref="object"/>, or <see langword="null"/>.</returns>
    public static Task<object?> ShowNotificationAsync(KryptonUserInputToastData data) =>
        KryptonToastController.ShowToastAsync(data);

    /// <summary>Shows the user-input notification with progress bar asynchronously.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The typed user response boxed as <see cref="object"/>, or <see langword="null"/>.</returns>
    public static Task<object?> ShowNotificationWithProgressBarAsync(KryptonUserInputToastData data) =>
        KryptonToastController.ShowToastWithProgressBarAsync(data);
    #endregion

    #endregion

    #endregion
}