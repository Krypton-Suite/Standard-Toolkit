#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV) & Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal class CommonToastNotificationFunctions
    {
        #region Instance Fields

        private static KryptonBasicToastNotificationData _basicToastNotificationData;

        private static KryptonCommonToastNotificationData _commonData;

        private static KryptonUserInputToastNotificationData _userInputToastNotificationData;

        #endregion

        #region Identity

        public CommonToastNotificationFunctions()
        {

        }

        #endregion

        #region Implementation

        public static void UpdateLocation(KryptonForm owner, bool? isRTLNotification = false)
        {
            //Once loaded, position the form, or position it to the bottom left of the screen with added padding
            owner.Location = _userInputToastNotificationData.NotificationLocation ?? new Point(Screen.PrimaryScreen!.WorkingArea.Width - owner.Width - 5,
                Screen.PrimaryScreen.WorkingArea.Height - owner.Height - 5);
        }

        public static void UpdateIcon(PictureBox iconArea)
        {
            int scaledImageSize = _basicToastNotificationData.NotificationIconSize.HasValue && _basicToastNotificationData.NotificationIconSize == ToastNotificationIconSize.Small
                ? (_basicToastNotificationData.CustomNotificationIconSize ?? 32)
                : (int)_basicToastNotificationData.NotificationIconSize.GetValueOrDefault(ToastNotificationIconSize.Small);


            switch (_basicToastNotificationData.NotificationIcon)
            {
                case KryptonToastNotificationIcon.None:
                    SetIcon(iconArea, null);
                    break;
                case KryptonToastNotificationIcon.Hand:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Hand_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemHand:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(SystemIcons.Hand.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Question:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Question_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemQuestion:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(SystemIcons.Question.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Exclamation:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Warning_128_x_115, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemExclamation:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(SystemIcons.Exclamation.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Asterisk:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Asterisk_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemAsterisk:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Stop:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Stop_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Error:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Critical_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Warning:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Warning_128_x_115, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Information:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Information_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Shield:
                    if (OSUtilities.IsAtLeastWindowsEleven)
                    {
                        SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_11_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    else if (OSUtilities.IsWindowsTen)
                    {
                        SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_10_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    else
                    {
                        SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_7_and_8_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    break;
                case KryptonToastNotificationIcon.WindowsLogo:
                    if (OSUtilities.IsAtLeastWindowsEleven)
                    {
                        SetIcon(iconArea, GraphicsExtensions.ScaleImage(WindowsLogoImageResources.Windows_11_128_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    else if (OSUtilities.IsWindowsEight || OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsTen)
                    {
                        SetIcon(iconArea, GraphicsExtensions.ScaleImage(WindowsLogoImageResources.Windows_8_81_10_128_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    else
                    {
                        SetIcon(iconArea, GraphicsExtensions.ScaleImage(SystemIcons.WinLogo.ToBitmap(), new Size(scaledImageSize, scaledImageSize)));
                    }
                    break;
                case KryptonToastNotificationIcon.Application:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(_basicToastNotificationData.ApplicationIcon.ToBitmap(), new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemApplication:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Ok:
                    SetIcon(iconArea, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Ok_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Custom:
                    SetIcon(iconArea, _basicToastNotificationData.CustomImage != null
                        ? GraphicsExtensions.ScaleImage(new Bitmap(_basicToastNotificationData.CustomImage), new Size(scaledImageSize, scaledImageSize))
                        : null);
                    break;
                case null:
                    SetIcon(iconArea, null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void UpdateUserInputToastIcon(PictureBox iconContainer)
        {
            int scaledImageSize = _userInputToastNotificationData.NotificationIconSize == ToastNotificationIconSize.Small
                ? (_userInputToastNotificationData.CustomNotificationIconSize ?? 32)
                : (int)_userInputToastNotificationData.NotificationIconSize!;

            switch (_userInputToastNotificationData.NotificationIcon)
            {
                case KryptonToastNotificationIcon.None:
                    SetIcon(iconContainer, null);
                    break;
                case KryptonToastNotificationIcon.Hand:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Hand_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemHand:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(SystemIcons.Hand.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Question:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Question_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemQuestion:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(SystemIcons.Question.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Exclamation:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Warning_128_x_115, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemExclamation:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(SystemIcons.Exclamation.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Asterisk:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Asterisk_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemAsterisk:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Stop:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Stop_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Error:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Critical_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Warning:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Warning_128_x_115, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Information:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Information_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Shield:
                    if (OSUtilities.IsAtLeastWindowsEleven)
                    {
                        SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_11_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    else if (OSUtilities.IsWindowsTen)
                    {
                        SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_10_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    else
                    {
                        SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_7_and_8_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    break;
                case KryptonToastNotificationIcon.WindowsLogo:
                    if (OSUtilities.IsAtLeastWindowsEleven)
                    {
                        SetIcon(iconContainer, GraphicsExtensions.ScaleImage(WindowsLogoImageResources.Windows_11_128_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    else if (OSUtilities.IsWindowsEight || OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsTen)
                    {
                        SetIcon(iconContainer, GraphicsExtensions.ScaleImage(WindowsLogoImageResources.Windows_8_81_10_128_128, new Size(scaledImageSize, scaledImageSize)));
                    }
                    else
                    {
                        SetIcon(iconContainer, GraphicsExtensions.ScaleImage(SystemIcons.WinLogo.ToBitmap(), new Size(scaledImageSize, scaledImageSize)));
                    }
                    break;
                case KryptonToastNotificationIcon.Application:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(_userInputToastNotificationData.ApplicationIcon.ToBitmap(), new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.SystemApplication:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), scaledImageSize, scaledImageSize));
                    break;
                case KryptonToastNotificationIcon.Ok:
                    SetIcon(iconContainer, GraphicsExtensions.ScaleImage(ToastNotificationImageResources.Toast_Notification_Ok_128_x_128, new Size(scaledImageSize, scaledImageSize)));
                    break;
                case KryptonToastNotificationIcon.Custom:
                    SetIcon(iconContainer, _userInputToastNotificationData.CustomImage != null
                        ? GraphicsExtensions.ScaleImage(new Bitmap(_userInputToastNotificationData.CustomImage), new Size(scaledImageSize, scaledImageSize))
                        : null);
                    break;
                case null:
                    SetIcon(iconContainer, null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SetIcon(PictureBox iconContainer, Bitmap? bitmap)
        {
            iconContainer.Image = bitmap;
        }

        public static void UpdateNotificationText(KryptonWrapLabel titleArea, KryptonRichTextBox contentArea, string titleText, string contentText)
        {
            titleArea.Text = titleText ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
            contentArea.Text = contentText ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
        }

        public static void UpdateBorderColors(KryptonForm owner)
        {
            owner.StateCommon!.Border.Color1 = _commonData.BorderColor1 ?? GlobalStaticValues.EMPTY_COLOR;

            owner.StateCommon!.Border.Color2 = _commonData.BorderColor2 ?? GlobalStaticValues.EMPTY_COLOR;
        }

        public static void UpdateCheckBox(KryptonCheckBox checkBox)
        {
            checkBox.Visible = _commonData.ShowDoNotShowAgainOption;
            checkBox.Text = _commonData.OptionalCheckBoxText ?? KryptonManager.Strings.CustomStrings.DoNotShowAgain;
            checkBox.ThreeState = _commonData.UseDoNotShowAgainOptionThreeState;
            checkBox.CheckState = _commonData.DoNotShowAgainOptionCheckState;
        }

        public static void UpdateProgressBar(KryptonProgressBar progressBar)
        {
            progressBar.Visible = _commonData.ShowCountDownProgressBar;
        }

        public static void UpdateRtlLayout(KryptonForm owner)
        {
            owner.RightToLeft = _commonData.RightToLeftLayout == RightToLeftLayout.LeftToRight ? RightToLeft.No : RightToLeft.Yes;
        }

        public static void ShowCloseButton(KryptonForm owner)
        {
            owner.CloseBox = _commonData.ShowCloseBox ?? false;

            owner.FormBorderStyle = owner.CloseBox ? FormBorderStyle.Fixed3D : FormBorderStyle.FixedSingle;

            owner.ControlBox = _commonData.ShowCloseBox ?? false;
        }

        #endregion
    }
}
