﻿#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;
#pragma warning disable VSSpell001

namespace Krypton.Toolkit
{
    /// <summary>Contains the data and information required, to create a toast notification with user input.</summary>
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

        /// <summary>Gets or sets the show do not show again option.</summary>
        /// <value>The show do not show again option.</value>
        public bool? ShowDoNotShowAgainOption { get; set; }

        /// <summary>Gets or sets the state of the use do not show again option three.</summary>
        /// <value>The state of the use do not show again option three.</value>
        public bool? UseDoNotShowAgainOptionThreeState { get; set; }

        /// <summary>Gets or sets the do not show again option checked value.</summary>
        /// <value>The do not show again option checked value.</value>
        public bool DoNotShowAgainOptionChecked { get; set; }

        /// <summary>Gets or sets the report toast location. Use this for development purposes only.</summary>
        /// <value>Reports the toast location.</value>
        public bool ReportToastLocation { get; set; }

        /// <summary>Gets or sets a value indicating whether [use RTL reading].</summary>
        /// <value><c>true</c> if [use RTL reading]; otherwise, <c>false</c>.</value>
        public bool UseRtlReading { get; set; }

        /// <summary>Gets or sets the state of the do not show again option check.</summary>
        /// <value>The state of the do not show again option check.</value>
        public CheckState? DoNotShowAgainOptionCheckState { get; set; }

        /// <summary>Gets or sets the focus on user input area.</summary>
        /// <value>The focus on user input area.</value>
        public bool? FocusOnUserInputArea { get; set; }

        /// <summary>Gets or sets the notification title alignment.</summary>
        /// <value>The notification title alignment.</value>
        public ContentAlignment? NotificationTitleAlignment { get; set; }

        /// <summary>Gets or sets the user input ComboBox style.</summary>
        /// <value>The user input ComboBox style.</value>
        public ComboBoxStyle? UserInputComboBoxStyle { get; set; }

        /// <summary>Gets or sets the first border color.</summary>
        /// <value>The first border color.</value>
        public Color? BorderColor1 { get; set; }

        /// <summary>Gets or sets the second border color.</summary>
        /// <value>The second border color.</value>
        public Color? BorderColor2 { get; set; }

        /// <summary>Gets or sets the minimum date time value.</summary>
        /// <value>The minimum date time value.</value>
        public DateTime? MinimumDateTimeValue { get; set; }

        /// <summary>Gets or sets the maximum date time value.</summary>
        /// <value>The maximum date time value.</value>
        public DateTime? MaximumDateTimeValue { get; set; }

        /// <summary>Gets or sets the initial date time value.</summary>
        /// <value>The initial date time value.</value>
        public DateTime? InitialDateTimeValue { get; set; }

        /// <summary>Gets or sets the notification content font.</summary>
        /// <value>The notification content font.</value>
        public Font? NotificationContentFont { get; set; }

        /// <summary>Gets or sets the notification title font.</summary>
        /// <value>The notification title font.</value>
        public Font? NotificationTitleFont { get; set; }

        /// <summary>Gets or sets the count-down seconds.</summary>
        /// <value>The count-down seconds.</value>
        public int? CountDownSeconds { get; set; }

        /// <summary>Gets or sets the count-down timer interval.</summary>
        /// <value>The count-down timer interval.</value>
        public int? CountDownTimerInterval { get; set; }

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

        /// <summary>Gets or sets the application icon.</summary>
        /// <value>The application icon.</value>
        public Icon ApplicationIcon { get; set; }

        /// <summary>Gets or sets the toast host.</summary>
        /// <value>The toast host.</value>
        public IWin32Window? ToastHost { get; set; }

        /// <summary>Gets or sets the notification icon.</summary>
        /// <value>The notification icon.</value>
        public KryptonToastNotificationIcon? NotificationIcon { get; set; }

        /// <summary>Gets or sets the type of the notification input area.</summary>
        /// <value>The type of the notification input area.</value>
        public KryptonToastNotificationInputAreaType? NotificationInputAreaType { get; set; }

        /// <summary>Gets or sets the toast notification cue text.</summary>
        /// <value>The toast notification cue text.</value>
        public string? ToastNotificationCueText { get; set; }

        /// <summary>Gets or sets the optional CheckBox text.</summary>
        /// <value>The optional CheckBox text.</value>
        public string? OptionalCheckBoxText { get; set; }

        /// <summary>Gets or sets the user input list.</summary>
        /// <value>The user input list.</value>
        public ArrayList UserInputList { get; set; }

        /// <summary>Gets or sets the index of the selected user input.</summary>
        /// <value>The index of the selected user input.</value>
        public int? SelectedIndex { get; set; }

        /// <summary>Gets or sets the date time format.</summary>
        /// <value>The date time format.</value>
        public DateTimePickerFormat? DateTimeFormat { get; set; }

        /// <summary>Gets or sets the custom date time format.</summary>
        /// <value>The custom date time format.</value>
        public string? CustomDateTimeFormat { get; set; }

        /// <summary>Gets or sets the color of the toast notification cue.</summary>
        /// <value>The color of the toast notification cue.</value>
        public Color? ToastNotificationCueColor { get; set; }

        /// <summary>Gets or sets the initial numeric up down value.</summary>
        /// <value>The initial numeric up down value.</value>
        public decimal? InitialNumericUpDownValue { get; set; }

        /// <summary>Gets or sets the maximum numeric up down value.</summary>
        /// <value>The maximum numeric up down value.</value>
        public decimal? MaximumNumericUpDownValue { get; set; }

        /// <summary>Gets or sets the minimum numeric up down value.</summary>
        /// <value>The minimum numeric up down value.</value>
        public decimal? MinimumNumericUpDownValue { get; set; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonUserInputToastNotificationData" /> struct.</summary>
        public KryptonUserInputToastNotificationData()
        {
            // Defaults, when needed
            UseFade = false;

            ReportToastLocation = false;

            #region Do Not Show Again Values

            ShowDoNotShowAgainOption = false;

            UseDoNotShowAgainOptionThreeState = false;

            DoNotShowAgainOptionCheckState = CheckState.Unchecked;

            #endregion

            UseRtlReading = false;

            BorderColor1 = GlobalStaticValues.EMPTY_COLOR;

            BorderColor2 = GlobalStaticValues.EMPTY_COLOR;

            CountDownTimerInterval = 1000;

            OptionalCheckBoxText = KryptonManager.Strings.CustomStrings.DoNotShowAgain;

            ToastHost = null;

            SelectedIndex = 0;
        }

        #endregion

        #region Implementation

        internal readonly void DisplayDebugData(KryptonUserInputToastNotificationData data)
        {
            Console.WriteLine($"ComboBox Items: {UserInputList}");

            Console.ReadLine();
        }

        #endregion
    }
}