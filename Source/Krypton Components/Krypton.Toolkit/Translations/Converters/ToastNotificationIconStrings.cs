#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

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

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region IsDefault

    [Browsable(false)]
    public bool IsDefault => !(ShouldSerializeApplication() ||
                               ShouldSerializeAsterisk() ||
                               ShouldSerializeCustom() ||
                               ShouldSerializeError() ||
                               ShouldSerializeExclamation() ||
                               ShouldSerializeHand() ||
                               ShouldSerializeInformation() ||
                               ShouldSerializeNone() ||
                               ShouldSerializeOk() ||
                               ShouldSerializeQuestion() ||
                               ShouldSerializeShield() ||
                               ShouldSerializeStop() ||
                               ShouldSerializeSystemApplication() ||
                               ShouldSerializeSystemAsterisk() ||
                               ShouldSerializeSystemError() ||
                               ShouldSerializeSystemExclamation() ||
                               ShouldSerializeSystemHand() ||
                               ShouldSerializeSystemInformation() ||
                               ShouldSerializeSystemQuestion() ||
                               ShouldSerializeSystemStop() ||
                               ShouldSerializeSystemWarning() ||
                               ShouldSerializeWarning() ||
                               ShouldSerializeWindowsLogo());
    /*Application.Equals(DEFAULT_APPLICATION) &&
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
   WindowsLogo.Equals(DEFAULT_WINDOWS_LOGO);*/

    #endregion

    #region Public

    /// <summary>Gets or sets the application icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised application icon string.")]
    [DefaultValue(DEFAULT_APPLICATION)]
    [RefreshProperties(RefreshProperties.All)]
    public string Application { get; set; }

    private bool ShouldSerializeApplication() => Application != DEFAULT_APPLICATION;

    public void ResetApplication() => Application = DEFAULT_APPLICATION;

    /// <summary>Gets or sets the asterisk icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised asterisk icon string.")]
    [DefaultValue(DEFAULT_ASTERISK)]
    public string Asterisk { get; set; }

    private bool ShouldSerializeAsterisk() => Asterisk != DEFAULT_ASTERISK;

    public void ResetAsterisk() => Asterisk = DEFAULT_ASTERISK;

    /// <summary>Gets or sets the custom icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised custom icon string.")]
    [DefaultValue(DEFAULT_CUSTOM)]
    public string Custom { get; set; }

    private bool ShouldSerializeCustom() => Custom != DEFAULT_CUSTOM;

    public void ResetCustom() => Custom = DEFAULT_CUSTOM;

    /// <summary>Gets or sets the error icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised error icon string.")]
    [DefaultValue(DEFAULT_ERROR)]
    public string Error { get; set; }

    private bool ShouldSerializeError() => Error != DEFAULT_ERROR;

    public void ResetError() => Error = DEFAULT_ERROR;

    /// <summary>Gets or sets the exclamation icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised exclamation icon string.")]
    [DefaultValue(DEFAULT_EXCLAMATION)]
    public string Exclamation { get; set; }

    private bool ShouldSerializeExclamation() => Exclamation != DEFAULT_EXCLAMATION;

    public void ResetExclamation() => Exclamation = DEFAULT_EXCLAMATION;

    /// <summary>Gets or sets the hand icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised hand icon string.")]
    [DefaultValue(DEFAULT_HAND)]
    public string Hand { get; set; }

    private bool ShouldSerializeHand() => Hand != DEFAULT_HAND;

    public void ResetHand() => Hand = DEFAULT_HAND;

    /// <summary>Gets or sets the information icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised information icon string.")]
    [DefaultValue(DEFAULT_INFORMATION)]
    public string Information { get; set; }

    private bool ShouldSerializeInformation() => Information != DEFAULT_INFORMATION;

    public void ResetInformation() => Information = DEFAULT_INFORMATION;

    /// <summary>Gets or sets the none icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised none icon string.")]
    [DefaultValue(DEFAULT_NONE)]
    public string None { get; set; }

    private bool ShouldSerializeNone() => None != DEFAULT_NONE;

    public void ResetNone() => None = DEFAULT_NONE;

    /// <summary>Gets or sets the ok icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised ok icon string.")]
    [DefaultValue(DEFAULT_OK)]
    public string Ok { get; set; }

    private bool ShouldSerializeOk() => Ok != DEFAULT_OK;

    public void ResetOk() => Ok = DEFAULT_OK;

    /// <summary>Gets or sets the question icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised question icon string.")]
    [DefaultValue(DEFAULT_QUESTION)]
    public string Question { get; set; }

    private bool ShouldSerializeQuestion() => Question != DEFAULT_QUESTION;

    public void ResetQuestion() => Question = DEFAULT_QUESTION;

    /// <summary>Gets or sets the shield icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised shield icon string.")]
    [DefaultValue(DEFAULT_SHIELD)]
    public string Shield { get; set; }

    private bool ShouldSerializeShield() => Shield != DEFAULT_SHIELD;

    public void ResetShield() => Shield = DEFAULT_SHIELD;

    /// <summary>Gets or sets the stop icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised stop icon string.")]
    [DefaultValue(DEFAULT_STOP)]
    public string Stop { get; set; }

    private bool ShouldSerializeStop() => Stop != DEFAULT_STOP;

    public void ResetStop() => Stop = DEFAULT_STOP;

    /// <summary>Gets or sets the system application icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system application icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_APPLICATION)]
    public string SystemApplication { get; set; }

    private bool ShouldSerializeSystemApplication() => SystemApplication != DEFAULT_SYSTEM_APPLICATION;

    public void ResetSystemApplication() => SystemApplication = DEFAULT_SYSTEM_APPLICATION;

    /// <summary>Gets or sets the system asterisk icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system asterisk icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_ASTERISK)]
    public string SystemAsterisk { get; set; }

    private bool ShouldSerializeSystemAsterisk() => SystemAsterisk != DEFAULT_SYSTEM_ASTERISK;

    public void ResetSystemAsterisk() => SystemAsterisk = DEFAULT_SYSTEM_ASTERISK;

    /// <summary>Gets or sets the system error icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system error icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_ERROR)]
    public string SystemError { get; set; }

    private bool ShouldSerializeSystemError() => SystemError != DEFAULT_SYSTEM_ERROR;

    public void ResetSystemError() => SystemError = DEFAULT_SYSTEM_ERROR;

    /// <summary>Gets or sets the system exclamation icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system exclamation icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_EXCLAMATION)]
    public string SystemExclamation { get; set; }

    private bool ShouldSerializeSystemExclamation() => SystemExclamation != DEFAULT_SYSTEM_EXCLAMATION;

    public void ResetSystemExclamation() => SystemExclamation = DEFAULT_SYSTEM_EXCLAMATION;

    /// <summary>Gets or sets the system hand icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system hand icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_HAND)]
    public string SystemHand { get; set; }

    private bool ShouldSerializeSystemHand() => SystemHand != DEFAULT_SYSTEM_HAND;

    public void ResetSystemHand() => SystemHand = DEFAULT_SYSTEM_HAND;

    /// <summary>Gets or sets the system information icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system information icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_INFORMATION)]
    public string SystemInformation { get; set; }

    private bool ShouldSerializeSystemInformation() => SystemInformation != DEFAULT_SYSTEM_INFORMATION;

    public void ResetSystemInformation() => SystemInformation = DEFAULT_SYSTEM_INFORMATION;

    /// <summary>Gets or sets the system question icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system question icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_QUESTION)]
    public string SystemQuestion { get; set; }

    private bool ShouldSerializeSystemQuestion() => SystemQuestion != DEFAULT_SYSTEM_QUESTION;

    public void ResetSystemQuestion() => SystemQuestion = DEFAULT_SYSTEM_QUESTION;

    /// <summary>Gets or sets the system stop icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system stop icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_STOP)]
    public string SystemStop { get; set; }

    private bool ShouldSerializeSystemStop() => SystemStop != DEFAULT_SYSTEM_STOP;

    public void ResetSystemStop() => SystemStop = DEFAULT_SYSTEM_STOP;

    /// <summary>Gets or sets the system warning icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised system warning icon string.")]
    [DefaultValue(DEFAULT_SYSTEM_WARNING)]
    public string SystemWarning { get; set; }

    private bool ShouldSerializeSystemWarning() => SystemWarning != DEFAULT_SYSTEM_WARNING;

    public void ResetSystemWarning() => SystemWarning = DEFAULT_SYSTEM_WARNING;

    /// <summary>Gets or sets the warning icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised warning icon string.")]
    [DefaultValue(DEFAULT_WARNING)]
    public string Warning { get; set; }

    private bool ShouldSerializeWarning() => Warning != DEFAULT_WARNING;

    public void ResetWarning() => Warning = DEFAULT_WARNING;

    /// <summary>Gets or sets the Windows logo icon string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Localised Windows logo icon string.")]
    [DefaultValue(DEFAULT_WINDOWS_LOGO)]
    public string WindowsLogo { get; set; }

    private bool ShouldSerializeWindowsLogo() => WindowsLogo != DEFAULT_WINDOWS_LOGO;

    public void ResetWindowsLogo() => WindowsLogo = DEFAULT_WINDOWS_LOGO;

    #endregion

    #region Implementation

    public void Reset()
    {
        //Application = DEFAULT_APPLICATION;

        //Asterisk = DEFAULT_ASTERISK;

        //Custom = DEFAULT_CUSTOM;

        //Error = DEFAULT_ERROR;

        //Exclamation = DEFAULT_EXCLAMATION;

        //Hand = DEFAULT_HAND;

        //Information = DEFAULT_INFORMATION;

        //None = DEFAULT_NONE;

        //Ok = DEFAULT_OK;

        //Question = DEFAULT_QUESTION;

        //Shield = DEFAULT_SHIELD;

        //Stop = DEFAULT_STOP;

        //SystemApplication = DEFAULT_SYSTEM_APPLICATION;

        //SystemAsterisk = DEFAULT_SYSTEM_ASTERISK;

        //SystemError = DEFAULT_SYSTEM_ERROR;

        //SystemExclamation = DEFAULT_SYSTEM_EXCLAMATION;

        //SystemHand = DEFAULT_SYSTEM_HAND;

        //SystemInformation = DEFAULT_SYSTEM_INFORMATION;

        //SystemQuestion = DEFAULT_SYSTEM_QUESTION;

        //SystemStop = DEFAULT_SYSTEM_STOP;

        //SystemWarning = DEFAULT_SYSTEM_WARNING;

        //Warning = DEFAULT_WARNING;

        //WindowsLogo = DEFAULT_WINDOWS_LOGO;

        ResetApplication();

        ResetAsterisk();

        ResetCustom();

        ResetError();

        ResetExclamation();

        ResetHand();

        ResetInformation();

        ResetNone();

        ResetOk();

        ResetQuestion();

        ResetShield();

        ResetStop();

        ResetSystemApplication();

        ResetSystemAsterisk();

        ResetSystemError();

        ResetSystemExclamation();

        ResetSystemHand();

        ResetSystemInformation();

        ResetSystemQuestion();

        ResetSystemStop();

        ResetSystemWarning();

        ResetWarning();

        ResetWindowsLogo();
    }

    #endregion
}