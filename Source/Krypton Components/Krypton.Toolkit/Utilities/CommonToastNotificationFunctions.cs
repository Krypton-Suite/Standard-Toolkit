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

        private static readonly Dictionary<KryptonToastNotificationIcon, Image?> _toastNotificationImageDictionary =
            new Dictionary<KryptonToastNotificationIcon, Image?>()
            {
                { KryptonToastNotificationIcon.None, null },
                { KryptonToastNotificationIcon.Hand, ToastNotificationImageResources.Toast_Notification_Hand_128_x_128 },
                { KryptonToastNotificationIcon.Question, ToastNotificationImageResources.Toast_Notification_Question_128_x_128 },
                { KryptonToastNotificationIcon.Exclamation, ToastNotificationImageResources.Toast_Notification_Warning_128_x_115 },
                { KryptonToastNotificationIcon.Asterisk, ToastNotificationImageResources.Toast_Notification_Asterisk_128_x_128 },
                { KryptonToastNotificationIcon.Stop, ToastNotificationImageResources.Toast_Notification_Stop_128_x_128 },
                { KryptonToastNotificationIcon.Error, ToastNotificationImageResources.Toast_Notification_Critical_128_x_128 },
                { KryptonToastNotificationIcon.Warning, ToastNotificationImageResources.Toast_Notification_Warning_128_x_115 },
                { KryptonToastNotificationIcon.Information, ToastNotificationImageResources.Toast_Notification_Information_128_x_128 },
                { KryptonToastNotificationIcon.Ok, ToastNotificationImageResources.Toast_Notification_Ok_128_x_128 },
                { KryptonToastNotificationIcon.WindowsLogo, null }, // Placeholder if needed later
                { KryptonToastNotificationIcon.Application, null }, // Uses application icon dynamically
                { KryptonToastNotificationIcon.SystemApplication, SystemIcons.Application.ToBitmap() }
            };

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

        public static void UpdateLocation(KryptonForm owner, bool? isRtlNotification = false)
        {
            //Once loaded, position the form, or position it to the bottom left of the screen with added padding
            owner.Location = _userInputToastNotificationData.NotificationLocation ?? new Point(Screen.PrimaryScreen!.WorkingArea.Width - owner.Width - 5,
                Screen.PrimaryScreen.WorkingArea.Height - owner.Height - 5);
        }

        public static Bitmap? GetIcon(KryptonToastNotificationIcon? icon, KryptonCommonToastNotificationData data,
            ToastNotificationIconSize iconSize, int customIconSize = 32)
        {
            var scaledIconSize = iconSize == ToastNotificationIconSize.Custom
                ? (customIconSize > 0 ? customIconSize : 32)
                : (int)iconSize;

            Bitmap? baseImage = icon switch
            {
                KryptonToastNotificationIcon.Custom => data.CustomImage != null ? new Bitmap(data.CustomImage) : null,
                KryptonToastNotificationIcon.Shield => GetShieldIcon(scaledIconSize),
                KryptonToastNotificationIcon.Application => GetApplicationIcon(scaledIconSize),
                KryptonToastNotificationIcon.WindowsLogo => GetWindowsIcon(scaledIconSize),
                _ => GetSystemOrResourceIcon(icon, scaledIconSize)
            };

            return baseImage != null ? GraphicsExtensions.ScaleImage(baseImage, scaledIconSize, scaledIconSize) : null;
        }

        private static Bitmap? GetWindowsIcon(int scaledIconSize)
        {
            var baseImage = OSUtilities.IsAtLeastWindowsEleven
                ? ToastNotificationImageResources.Toast_Notification_Windows_11_128_x_128
                : OSUtilities.IsWindowsTen || OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight
                    ? ToastNotificationImageResources.Toast_Notification_Windows_10_128_x_121
                    : SystemIcons.WinLogo.ToBitmap();

            return GraphicsExtensions.ScaleImage(baseImage, new Size(scaledIconSize, scaledIconSize));
        }

        private static Bitmap? GetShieldIcon(int scaledIconSize)
        {
            var baseImage = OSUtilities.IsAtLeastWindowsEleven
                ?
                ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_11_128_x_128
                : OSUtilities.IsWindowsTen
                    ? ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_10_128_x_128
                    : ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_7_and_8_128_x_128;

            return GraphicsExtensions.ScaleImage(baseImage, new Size(scaledIconSize, scaledIconSize));
        }

        private static Bitmap? GetApplicationIcon(int scaledIconSize)
        {
            try
            {
                using Icon? applicationIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

                return GraphicsExtensions.ScaleImage(applicationIcon?.ToBitmap(), new Size(scaledIconSize, scaledIconSize));
            }
            catch (Exception e)
            {
                KryptonExceptionHandler.CaptureException(e);

                return null;
            }
        }

        private static Bitmap? GetSystemOrResourceIcon(KryptonToastNotificationIcon? icon, int scaledImageSize)
        {
            if (icon == null)
            {
                return null;
            }

            return _toastNotificationImageDictionary.TryGetValue(icon.Value, out var bitmap)
                ? GraphicsExtensions.ScaleImage(bitmap as Bitmap, new Size(scaledImageSize, scaledImageSize))
                : GetSystemIcon(icon, scaledImageSize);
        }

        private static Bitmap? GetSystemIcon(KryptonToastNotificationIcon? icon, int scaledImageSize)
        {
            Bitmap? systemIcon = icon switch
            {
                KryptonToastNotificationIcon.SystemHand or KryptonToastNotificationIcon.SystemStop or KryptonToastNotificationIcon.SystemError
                    => SystemIcons.Hand.ToBitmap(),
                KryptonToastNotificationIcon.SystemQuestion
                    => SystemIcons.Question.ToBitmap(),
                KryptonToastNotificationIcon.SystemExclamation or KryptonToastNotificationIcon.SystemWarning
                    => SystemIcons.Exclamation.ToBitmap(),
                KryptonToastNotificationIcon.SystemAsterisk or KryptonToastNotificationIcon.SystemInformation
                    => SystemIcons.Asterisk.ToBitmap(),
                _ => null
            };

            return systemIcon != null ? GraphicsExtensions.ScaleImage(systemIcon, new Size(scaledImageSize, scaledImageSize)) : null;
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
