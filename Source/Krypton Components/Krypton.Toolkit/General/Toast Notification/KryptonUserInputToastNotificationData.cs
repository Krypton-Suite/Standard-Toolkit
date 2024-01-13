#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;

namespace Krypton.Toolkit
{
    public struct KryptonUserInputToastNotificationData
    {
        #region Public

        /// <summary>Gets or sets the use fade.</summary>
        /// <value>The use fade.</value>
        public bool UseFade { get; set; }

        /// <summary>Gets or sets the top most.</summary>
        /// <value>The top most.</value>
        public bool? TopMost { get; set; }

        /// <summary>Gets or sets the show close box.</summary>
        /// <value>The show close box.</value>
        public bool? ShowCloseBox { get; set; }

        /// <summary>Gets or sets the notification title alignment.</summary>
        /// <value>The notification title alignment.</value>
        public ContentAlignment? NotificationTitleAlignment { get; set; }

        /// <summary>Gets or sets the notification content font.</summary>
        /// <value>The notification content font.</value>
        public Font? NotificationContentFont { get; set; }

        /// <summary>Gets or sets the notification title font.</summary>
        /// <value>The notification title font.</value>
        public Font? NotificationTitleFont { get; set; }

        /// <summary>Gets or sets the count down seconds.</summary>
        /// <value>The count down seconds.</value>
        public int? CountDownSeconds { get; set; }

        /// <summary>Gets or sets the content of the notification.</summary>
        /// <value>The content of the notification.</value>
        public string? NotificationContent { get; set; }

        /// <summary>Gets or sets the notification title.</summary>
        /// <value>The notification title.</value>
        public string? NotificationTitle { get; set; }

        /// <summary>Gets or sets the custom image.</summary>
        /// <value>The custom image.</value>
        public Bitmap? CustomImage { get; set; }

        /// <summary>Gets or sets the notification location.</summary>
        /// <value>The notification location.</value>
        public Point? NotificationLocation { get; set; }

        /// <summary>Gets or sets the notification icon.</summary>
        /// <value>The notification icon.</value>
        public KryptonToastNotificationIcon? NotificationIcon { get; set; }

        public KryptonToastNotificationInputAreaType? NotificationInputAreaType { get; set; } 

        public string? ToastNotificationCueText { get; set; }

        public ComboBox.ObjectCollection UserInputItemCollection { get; set; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonUserInputToastNotificationData" /> struct.</summary>
        public KryptonUserInputToastNotificationData()
        {
            // Defaults, when needed
            UseFade = false;
        }

        #endregion
    }
}