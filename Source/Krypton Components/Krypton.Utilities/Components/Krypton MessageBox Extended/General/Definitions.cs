using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Utilities;

#region Enum CbtHookAction

public enum CbtHookAction
{
    HCBT_MOVESIZE = 0,
    HCBT_MINMAX = 1,
    HCBT_QS = 2,
    HCBT_CREATEWND = 3,
    HCBT_DESTROYWND = 4,
    HCBT_ACTIVATE = 5,
    HCBT_CLICKSKIPPED = 6,
    HCBT_KEYSKIPPED = 7,
    HCBT_SYSCOMMAND = 8,
    HCBT_SETFOCUS = 9
}

#endregion

#region Enum ExtendedKryptonMessageBoxIcon

/// <summary>Specifies an icon for the <see cref="KryptonMessageBoxExtended"/>.</summary>
public enum ExtendedKryptonMessageBoxIcon
{
    /// <summary>Specify a custom icon.</summary>
    Custom = 0,

    /// <summary>Specify no icon.</summary>
    None = 1,

    /// <summary>Specify a hand icon.</summary>
    Hand = 2,

    /// <summary>Specify the system hand icon.</summary>
    SystemHand = MessageBoxIcon.Hand,

    /// <summary>Specify a question icon.</summary>
    Question = 3,

    /// <summary>Specify the system question icon.</summary>
    SystemQuestion = MessageBoxIcon.Question,

    /// <summary>Specify a exclamation icon.</summary>
    Exclamation = 4,

    /// <summary>Specify the system exclamation icon.</summary>
    SystemExclamation = MessageBoxIcon.Exclamation,

    /// <summary>Specify a asterisk icon.</summary>
    Asterisk = 5,

    /// <summary>Specify the system asterisk icon.</summary>
    SystemAsterisk = MessageBoxIcon.Asterisk,

    /// <summary>Specify a stop icon.</summary>
    Stop = 6,

    /// <summary>Specify the system stop icon.</summary>
    SystemStop = MessageBoxIcon.Stop,

    /// <summary>Specify a error icon.</summary>
    Error = 7,

    /// <summary>Specify the system error icon.</summary>
    SystemError = MessageBoxIcon.Error,

    /// <summary>Specify a warning icon.</summary>
    Warning = 8,

    /// <summary>Specify the system warning icon.</summary>
    SystemWarning = MessageBoxIcon.Warning,

    /// <summary>Specify a information icon.</summary>
    Information = 9,

    /// <summary>Specify the system information icon.</summary>
    SystemInformation = MessageBoxIcon.Information,

    /// <summary>Specify a UAC shield icon.</summary>
    Shield = 10,

    /// <summary>Specify a Windows logo icon.</summary>
    WindowsLogo = 11,

    /// <summary>Specify your application icon.</summary>
    Application = 12,

    /// <summary>Specify the default system application icon. See <see cref="SystemIcons.Application"/>.</summary>
    SystemApplication = 13
}

#endregion

#region Enum ExtendedKryptonMessageBoxMessageContainerType

public enum ExtendedKryptonMessageBoxMessageContainerType
{
    Normal = 0,
    RichTextBox = 1,
    HyperLink = 2
}

#endregion

#region Enum ExtendedKryptonMessageBoxFooterContentType

/// <summary>Specifies the content type for the footer in <see cref="KryptonMessageBoxExtended"/>.</summary>
public enum ExtendedKryptonMessageBoxFooterContentType
{
    /// <summary>Footer displays text using a KryptonWrapLabel (default).</summary>
    Text = 0,

    /// <summary>Footer displays a KryptonCheckBox.</summary>
    CheckBox = 1,

    /// <summary>Footer displays a KryptonRichTextBox.</summary>
    RichTextBox = 2
}

#endregion

#region Enum ExtendedMessageBoxButtons

/// <summary>Specifies the button layout in the <see cref="KryptonMessageBoxExtended"/>.</summary>
public enum ExtendedMessageBoxButtons
{
    /// <summary>Defines a custom button layout. Linked to <see cref="ExtendedMessageBoxCustomButtonOptions"/> values.</summary>
    Custom = 13,
    /// <summary>
    ///  Specifies that the message box contains an OK button.
    /// </summary>
    OK = MessageBoxButtons.OK,

    /// <summary>
    ///  Specifies that the message box contains OK and Cancel buttons.
    /// </summary>
    OKCancel = MessageBoxButtons.OKCancel,

    /// <summary>
    ///  Specifies that the message box contains Abort, Retry, and Ignore buttons.
    /// </summary>
    AbortRetryIgnore = MessageBoxButtons.AbortRetryIgnore,

    /// <summary>
    ///  Specifies that the message box contains Yes, No, and Cancel buttons.
    /// </summary>
    YesNoCancel = MessageBoxButtons.YesNoCancel,

    /// <summary>
    ///  Specifies that the message box contains Yes and No buttons.
    /// </summary>
    YesNo = MessageBoxButtons.YesNo,

    /// <summary>
    ///  Specifies that the message box contains Retry and Cancel buttons.
    /// </summary>
    RetryCancel = MessageBoxButtons.RetryCancel,

    /// <summary>
    ///  Specifies that the message box contains Cancel, Try Again, and Continue buttons.
    /// </summary>
#if NET60_OR_GREATER
            CancelTryContinue = MessageBoxButtons.CancelTryContinue,
#else
    CancelTryContinue = 0x00000006,
#endif 
    YesNoAllCancel = 8
}

#endregion

#region Enum ExtendedMessageBoxCustomButtonOptions

/// <summary>Specifies a custom button layout.</summary>
public enum ExtendedMessageBoxCustomButtonOptions
{
    /// <summary>Do not use custom buttons, instead default to an 'OK' only button.</summary>
    None = 0,
    /// <summary>Use a one button layout.</summary>
    OneButton = 1,
    /// <summary>Use a two button layout.</summary>
    TwoButtons = 2,
    /// <summary>Use a three button layout.</summary>
    ThreeButtons = 3,
    /// <summary>Use a four button layout.</summary>
    FourButtons = 4
}

#endregion

#region Enum ExtendedMessageBoxDialogResult

public enum ExtendedMessageBoxDialogResult : int
{
    None = DialogResult.None,
    Ok = DialogResult.OK,
    Cancel = DialogResult.Cancel,
    Abort = DialogResult.Abort,
    Retry = DialogResult.Retry,
    Ignore = DialogResult.Ignore,
    Yes = DialogResult.Yes,
    No = DialogResult.No,
    TryAgain = 8,
    Continue = 9,
    Timeout = 1000
}

#endregion

#region Enum ExtendedMessageBoxTimeoutAction

public enum ExtendedMessageBoxTimeoutAction
{
    Close = 0,
    ButtonOne = 1,
    ButtonTwo = 2,
    ButtonThree = 3,
    ButtonFour = 4
}

#endregion

#region Enum ExtendedMessageBoxTimeoutButton

public enum ExtendedMessageBoxTimeoutButton
{
    ButtonOne = 0,
    ButtonTwo = 1,
    ButtonThree = 2
}

#endregion