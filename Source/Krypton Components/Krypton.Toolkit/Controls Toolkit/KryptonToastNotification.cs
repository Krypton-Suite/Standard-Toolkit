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

        public static void ShowBasicNotification(KryptonBasicToastNotificationData toastNotificationData) =>
            VisualToastNotificationBasicForm.ShowToast(toastNotificationData);

        public static void ShowBasicProgressBarNotification(KryptonBasicToastNotificationData toastNotificationData) =>
            VisualToastNotificationBasicWithProgressBarForm.ShowToast(toastNotificationData);

        public static string ShowUserInputNotification(KryptonUserInputToastNotificationData toastNotificationData) =>
            VisualToastNotificationUserInputForm.InternalShow(toastNotificationData);

        #endregion
    }
}