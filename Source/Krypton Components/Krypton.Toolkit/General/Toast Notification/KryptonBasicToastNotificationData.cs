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
    public struct KryptonBasicToastNotificationData
    {
        #region Public

        public bool? ShowCloseBox { get; set; }

        public int? CountDownSeconds { get; set; }

        public string NotificationContent { get; set; }

        public string? NotificationTitle { get; set; }

        public Image? CustomImage { get; set; }

        public KryptonToastNotificationIcon? NotificationIcon { get; set; }

        #endregion
    }
}