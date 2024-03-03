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
    /// <summary>Exposes the set of <see cref="KryptonToastNotificationIconConverter"/> strings used within Krypton and that are localizable.</summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ToastNotificationIconStrings : GlobalId
    {
        #region Static Fields

        private const string DEFAULT_APPLICATION = @"Application";
        private const string DEFAULT_ASTERISK = @"Asterisk";
        private const string DEFAULT_CUSTOM = @"Custom";
        private const string DEFAULT_ERROR = @"Error";
        private const string DEFAULT_EXCLAMATION = @"Exclamation";
        private const string DEFAULT_HAND = @"Hand";
        private const string DEFAULT_INFORMATION = @"Information";
        private const string DEFAULT_NONE = @"None";
        private const string DEFAULT_OK = @"Ok";
        private const string DEFAULT_QUESTION = @"Question";
        private const string DEFAULT_SHIELD = @"Shield";
        private const string DEFAULT_STOP = @"Stop";
        private const string DEFAULT_SYSTEM_APPLICATION = @"Application (System)";
        private const string DEFAULT_SYSTEM_ASTERISK = @"Asterisk (System)";
        private const string DEFAULT_SYSTEM_ERROR = @"Error (System)";
        private const string DEFAULT_SYSTEM_EXCLAMATION = @"Exclamation (System)";
        private const string DEFAULT_SYSTEM_HAND = @"Hand (System)";
        private const string DEFAULT_SYSTEM_INFORMATION = @"Information (System)";
        private const string DEFAULT_SYSTEM_QUESTION = @"Question (System)";
        private const string DEFAULT_SYSTEM_STOP = @"Stop (System)";
        private const string DEFAULT_SYSTEM_WARNING = @"Warning (System)";
        private const string DEFAULT_WARNING = @"Warning";
        private const string DEFAULT_WINDOWS_LOGO = @"Windows Logo";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="ToastNotificationIconStrings" /> class.</summary>
        public ToastNotificationIconStrings()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        [Browsable(false)]
        public bool IsDefault => Application.Equals(DEFAULT_APPLICATION) &&
                                 Asterisk.Equals(DEFAULT_ASTERISK) &&
                                 Custom.Equals(DEFAULT_CUSTOM) &&
                                 Error.Equals(DEFAULT_ERROR) &&
                                 Exclamation.Equals(DEFAULT_EXCLAMATION) &&
                                 Hand.Equals(DEFAULT_HAND) &&
                                 Information.Equals(DEFAULT_INFORMATION) &&
                                 None.Equals(DEFAULT_NONE) &&
                                 Ok.Equals(DEFAULT_OK) &&
                                 Question.Equals(DEFAULT_QUESTION) &&
                                 Shield.Equals(DEFAULT_SHIELD) &&
                                 Stop.Equals(DEFAULT_STOP) &&
                                 SystemApplication.Equals(DEFAULT_SYSTEM_APPLICATION) &&
                                 SystemAsterisk.Equals(DEFAULT_SYSTEM_ASTERISK) &&
                                 SystemError.Equals(DEFAULT_SYSTEM_ERROR) &&
                                 SystemExclamation.Equals(DEFAULT_SYSTEM_EXCLAMATION) &&
                                 SystemHand.Equals(DEFAULT_SYSTEM_HAND) &&
                                 SystemInformation.Equals(DEFAULT_SYSTEM_INFORMATION) &&
                                 SystemQuestion.Equals(DEFAULT_SYSTEM_QUESTION) &&
                                 SystemStop.Equals(DEFAULT_SYSTEM_STOP) &&
                                 SystemWarning.Equals(DEFAULT_SYSTEM_WARNING) &&
                                 Warning.Equals(DEFAULT_SYSTEM_WARNING) &&
                                 WindowsLogo.Equals(DEFAULT_WINDOWS_LOGO);

        #endregion

        #region Public

        /// <summary>Gets or sets the application icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised application icon string.")]
        [DefaultValue(DEFAULT_APPLICATION)]
        [RefreshProperties(RefreshProperties.All)]
        public string Application { get; set; }

        /// <summary>Gets or sets the asterisk icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised asterisk icon string.")]
        [DefaultValue(DEFAULT_ASTERISK)]
        public string Asterisk { get; set; }

        /// <summary>Gets or sets the custom icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised custom icon string.")]
        [DefaultValue(DEFAULT_CUSTOM)]
        public string Custom { get; set; }

        /// <summary>Gets or sets the error icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised error icon string.")]
        [DefaultValue(DEFAULT_ERROR)]
        public string Error { get; set; }

        /// <summary>Gets or sets the exclamation icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised exclamation icon string.")]
        [DefaultValue(DEFAULT_EXCLAMATION)]
        public string Exclamation { get; set; }

        /// <summary>Gets or sets the hand icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised hand icon string.")]
        [DefaultValue(DEFAULT_HAND)]
        public string Hand { get; set; }

        /// <summary>Gets or sets the information icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised information icon string.")]
        [DefaultValue(DEFAULT_INFORMATION)]
        public string Information { get; set; }

        /// <summary>Gets or sets the none icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised none icon string.")]
        [DefaultValue(DEFAULT_NONE)]
        public string None { get; set; }

        /// <summary>Gets or sets the ok icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised ok icon string.")]
        [DefaultValue(DEFAULT_OK)]
        public string Ok { get; set; }

        /// <summary>Gets or sets the question icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised question icon string.")]
        [DefaultValue(DEFAULT_QUESTION)]
        public string Question { get; set; }

        /// <summary>Gets or sets the shield icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised shield icon string.")]
        [DefaultValue(DEFAULT_SHIELD)]
        public string Shield { get; set; }

        /// <summary>Gets or sets the stop icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised stop icon string.")]
        [DefaultValue(DEFAULT_STOP)]
        public string Stop { get; set; }

        /// <summary>Gets or sets the system application icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system application icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_APPLICATION)]
        public string SystemApplication { get; set; }

        /// <summary>Gets or sets the system asterisk icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system asterisk icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_ASTERISK)]
        public string SystemAsterisk { get; set; }

        /// <summary>Gets or sets the system error icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system error icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_ERROR)]
        public string SystemError { get; set; }

        /// <summary>Gets or sets the system exclamation icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system exclamation icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_EXCLAMATION)]
        public string SystemExclamation { get; set; }

        /// <summary>Gets or sets the system hand icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system hand icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_HAND)]
        public string SystemHand { get; set; }

        /// <summary>Gets or sets the system information icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system information icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_INFORMATION)]
        public string SystemInformation { get; set; }

        /// <summary>Gets or sets the system question icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system question icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_QUESTION)]
        public string SystemQuestion { get; set; }

        /// <summary>Gets or sets the system stop icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system stop icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_STOP)]
        public string SystemStop { get; set; }

        /// <summary>Gets or sets the system warning icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised system warning icon string.")]
        [DefaultValue(DEFAULT_SYSTEM_WARNING)]
        public string SystemWarning { get; set; }

        /// <summary>Gets or sets the warning icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised warning icon string.")]
        [DefaultValue(DEFAULT_WARNING)]
        public string Warning { get; set; }

        /// <summary>Gets or sets the Windows logo icon string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised Windows logo icon string.")]
        [DefaultValue(DEFAULT_WINDOWS_LOGO)]
        public string WindowsLogo { get; set; }

        #endregion

        #region Implementation

        public void Reset()
        {
            Application = DEFAULT_APPLICATION;

            Asterisk = DEFAULT_ASTERISK;

            Custom = DEFAULT_CUSTOM;

            Error = DEFAULT_ERROR;

            Exclamation = DEFAULT_EXCLAMATION;

            Hand = DEFAULT_HAND;

            Information = DEFAULT_INFORMATION;

            None = DEFAULT_NONE;

            Ok = DEFAULT_OK;

            Question = DEFAULT_QUESTION;

            Shield = DEFAULT_SHIELD;

            Stop = DEFAULT_STOP;

            SystemApplication = DEFAULT_SYSTEM_APPLICATION;

            SystemAsterisk = DEFAULT_SYSTEM_ASTERISK;

            SystemError = DEFAULT_SYSTEM_ERROR;

            SystemExclamation = DEFAULT_SYSTEM_EXCLAMATION;

            SystemHand = DEFAULT_SYSTEM_HAND;

            SystemInformation = DEFAULT_SYSTEM_INFORMATION;

            SystemQuestion = DEFAULT_SYSTEM_QUESTION;

            SystemStop = DEFAULT_SYSTEM_STOP;

            SystemWarning = DEFAULT_SYSTEM_WARNING;

            Warning = DEFAULT_WARNING;

            WindowsLogo = DEFAULT_WINDOWS_LOGO;
        }

        #endregion
    }
}