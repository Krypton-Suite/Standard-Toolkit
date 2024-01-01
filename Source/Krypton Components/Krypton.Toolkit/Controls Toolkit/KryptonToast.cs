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
    public static class KryptonToast
    {
        #region Public

        public static void Show(KryptonToastData toastData) => ShowCore(toastData);

        #endregion

        #region Implementation

        private static void ShowCore(KryptonToastData toastData)
        {
            //using var kt = new VisualToastForm(toastData.ShowCloseButton, toastData.ShowProgressBar,
            //           toastData.ShowCountdownPercentage,
            //                                   toastData.ShowActionButton, toastData.ShowUserResponse, 
            //                                   toastData.UserResponsePromptColor,
            //                                   toastData.LabelContentTextAlignment, 
            //               toastData.TitleTextAlignment, toastData.UserResponsePromptFont,
            //                                   toastData.UserInputControlStyle, 
            //                                   toastData.UserResponsePromptAlignmentHorizontal,
            //                                   toastData.UserResponsePromptAlignmentVertical,
            //                                   toastData.UserResponseTextAlignmentHorizontal,
            //                                   toastData.NotificationContentRichTextBoxAlignment,
            //                                   toastData.NotificationContentTextBoxAlignment,
            //                                   toastData.ActionButton, toastData.ActionType,
            //                                   toastData.ToastNotificationContentAreaType, toastData.NotificationIcon,
            //                                   toastData.ToastNotificationInputAreaType, toastData.CountDownSeconds,
            //                                   toastData.CountDownTimerInterval,
            //                                   toastData.NumericUpDownInputMaximum, toastData.ProgressBarMaximum, toastData.CustomImage,
            //                                   toastData.SoundStream, toastData.RightToLeft, toastData.Title,
            //                                   toastData.NotificationContentText,
            //                                   toastData.SoundPath, toastData.UserResponsePromptText, 
            //                                   toastData.NotificationContentLinkArea,
            //                                   toastData.NotificationContentLinkDestination, toastData.ActionButtonCommand);

            //kt.Show();
        }

        #endregion
    }
}