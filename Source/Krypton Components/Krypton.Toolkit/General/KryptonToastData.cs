#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;

namespace Krypton.Toolkit
{
    /// <summary>A structure that contains basic information for <see cref="VisualToastForm"/>.</summary>
    public struct KryptonToastData
    {
        #region Public

        public bool ShowCloseButton { get; set; }

        public bool ShowCountdownPercentage { get; set; }

        public bool ShowTitle { get; set; }

        public bool ShowActionButton { get; set; }

        public bool ShowProgressBar { get; set; }

        public Color? UserResponsePromptColor { get; set; }

        public ContentAlignment? LabelContentTextAlignment { get; set; }

        public ContentAlignment? TitleTextAlignment { get; set; }

        public Font? UserResponsePromptFont { get; set; }

        public InputControlStyle? UserInputControlStyle { get; set; }

        public PaletteRelativeAlign? UserResponsePromptAlignHorizontal { get; set; }

        public PaletteRelativeAlign? UserResponsePromptAlignVertical { get; set; }

        /// <summary>Gets or sets the text box content text horizontal alignment.</summary>
        /// <value>The text box content text horizontal alignment.</value>
        public PaletteRelativeAlign? TextBoxContentTextAlignmentHorizontal { get; set; }

        /// <summary>Gets or sets the action button.</summary>
        /// <value>The action button.</value>
        public KryptonToastNotificationActionButton? ActionButton { get; set; }

        /// <summary>Gets or sets the type of the action.</summary>
        /// <value>The type of the action.</value>
        public KryptonToastNotificationActionType? ActionType { get; set; }

        /// <summary>Gets or sets the type of the toast notification content area.</summary>
        /// <value>The type of the toast notification content area.</value>
        public KryptonToastNotificationContentAreaType? ToastNotificationContentAreaType { get; set; }

        /// <summary>Gets or sets the type of the toast notification input area.</summary>
        /// <value>The type of the toast notification input area.</value>
        public KryptonToastNotificationInputAreaType? ToastNotificationInputAreaType { get; set; }

        /// <summary>Gets or sets the notification icon.</summary>
        /// <value>The notification icon.</value>
        public KryptonToastNotificationIcon NotificationIcon { get; set; }

        /// <summary>Gets or sets the count-down seconds.</summary>
        /// <value>The count-down seconds.</value>
        public int? CountDownSeconds { get; set; }

        /// <summary>Gets or sets the time.</summary>
        /// <value>The time.</value>
        public int? Time { get; set; }

        /// <summary>Gets or sets the progress bar maximum value.</summary>
        /// <value>The progress bar maximum value.</value>
        public int? ProgressBarMaximum { get; set; }

        /// <summary>Gets or sets the custom image.</summary>
        /// <value>The custom image.</value>
        public Image CustomImage { get; set; }

        /// <summary>Gets or sets the sound stream.</summary>
        /// <value>The sound stream.</value>
        public Stream SoundStream { get; set; }

        /// <summary>Gets or sets the right to left.</summary>
        /// <value>The right to left.</value>
        public RightToLeft? RightToLeft { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>Gets or sets the notification content text.</summary>
        /// <value>The notification content text.</value>
        public string NotificationContentText { get; set; }

        /// <summary>Gets or sets the sound path.</summary>
        /// <value>The sound path.</value>
        public string SoundPath { get; set; }

        /// <summary>Gets or sets the user response prompt text.</summary>
        /// <value>The user response prompt text.</value>
        public string UserResponsePromptText { get; set; }

        /// <summary>Gets or sets the notification content link area.</summary>
        /// <value>The notification content link area.</value>
        public LinkArea? NotificationContentLinkArea { get; set; }

        /// <summary>Gets or sets the action button command.</summary>
        /// <value>The action button command.</value>
        public KryptonCommand? ActionButtonCommand { get; set; }

        /// <summary>Gets or sets the show user response UI.</summary>
        /// <value>The show user response UI.</value>
        public bool? ShowUserResponse { get; set; }

        /// <summary>Gets or sets the user response text alignment horizontal.</summary>
        /// <value>The user response text alignment horizontal.</value>
        public PaletteRelativeAlign? UserResponseTextAlignmentHorizontal { get; set; }

        /// <summary>Gets or sets the notification content rich text box alignment.</summary>
        /// <value>The notification content rich text box alignment.</value>
        public PaletteRelativeAlign? NotificationContentRichTextBoxAlignment { get; set; }

        /// <summary>Gets or sets the notification content text box alignment.</summary>
        /// <value>The notification content text box alignment.</value>
        public HorizontalAlignment? NotificationContentTextBoxAlignment { get; set; }

        /// <summary>Gets or sets the numeric up down input maximum value.</summary>
        /// <value>The numeric up down input maximum value.</value>
        public int? NumericUpDownInputMaximum { get; set; }

        /// <summary>Gets or sets the notification content link destination.</summary>
        /// <value>The notification content link destination.</value>
        public object? NotificationContentLinkDestination { get; set; }

        /// <summary>Gets or sets the count-down timer interval.</summary>
        /// <value>The count-down timer interval.</value>
        public int? CountDownTimerInterval { get; set; }

        #endregion
    }
}