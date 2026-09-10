#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region Enum CbtHookAction

/// <summary>
/// Specifies the action taken by a CBT hook procedure when a window is created, destroyed, activated, or moved.
/// </summary>
public enum CbtHookAction
{
    /// <summary>
    /// Specifies that a window is being moved or sized. The system sends this code to the hook procedure before the system moves or sizes the window.
    /// </summary>
    HCBT_MOVESIZE = 0,
    /// <summary>
    /// Specifies that a window is being minimized or maximized. The system sends this code to the hook procedure before the system minimizes or maximizes the window.
    /// </summary>
    HCBT_MINMAX = 1,
    /// <summary>
    /// Specifies that a window message is being processed. The system sends this code to the hook procedure before the system processes the message.
    /// </summary>
    HCBT_QS = 2,
    /// <summary>
    /// Specifies that a window is being created. The system sends this code to the hook procedure before the system creates the window.
    /// </summary>
    HCBT_CREATEWND = 3,
    /// <summary>
    /// Specifies that a window is being destroyed. The system sends this code to the hook procedure before the system destroys the window.
    /// </summary>
    HCBT_DESTROYWND = 4,
    /// <summary>
    /// Specifies that a window is being activated. The system sends this code to the hook procedure before the system activates the window.
    /// </summary>
    HCBT_ACTIVATE = 5,
    /// <summary>
    /// Specifies that a window is being clicked. The system sends this code to the hook procedure before the system processes the click.
    /// </summary>
    HCBT_CLICKSKIPPED = 6,
    /// <summary>
    /// Specifies that a key is being skipped. The system sends this code to the hook procedure before the system processes the key.
    /// </summary>
    HCBT_KEYSKIPPED = 7,
    /// <summary>
    /// Specifies that a system command is being processed. The system sends this code to the hook procedure before the system processes the command.
    /// </summary>
    HCBT_SYSCOMMAND = 8,
    /// <summary>
    /// Specifies that a window is receiving focus. The system sends this code to the hook procedure before the system gives focus to the window.
    /// </summary>
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

    /// <summary>Specify an exclamation icon.</summary>
    Exclamation = 4,

    /// <summary>Specify the system exclamation icon.</summary>
    SystemExclamation = MessageBoxIcon.Exclamation,

    /// <summary>Specify an asterisk icon.</summary>
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

    /// <summary>Specify an information icon.</summary>
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

/// <summary>
/// Specifies the type of container used for the message content in <see cref="KryptonMessageBoxExtended"/>.
/// </summary>
public enum ExtendedKryptonMessageBoxMessageContainerType
{
    /// <summary>
    /// Specifies that the message content is displayed in a standard label control (default).
    /// </summary>
    Normal = 0,
    /// <summary>
    /// Specifies that the message content is displayed in a rich text box control, allowing for formatted text.
    /// </summary>
    RichTextBox = 1,
    /// <summary>
    /// Specifies that the message content is displayed as a hyperlink.
    /// </summary>
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

#region Enum ExtendedKryptonMessageBoxCountdownButton

/// <summary>Specifies which button should display a countdown timer in <see cref="KryptonMessageBoxExtended"/>.</summary>
public enum ExtendedKryptonMessageBoxCountdownButton
{
    /// <summary>No button displays a countdown (default).</summary>
    None = 0,

    /// <summary>The first button displays a countdown.</summary>
    Button1 = 1,

    /// <summary>The second button displays a countdown.</summary>
    Button2 = 2,

    /// <summary>The third button displays a countdown.</summary>
    Button3 = 3,

    /// <summary>The fourth button displays a countdown.</summary>
    Button4 = 4
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
    /// <summary>
    /// Specifies that the message box contains Yes, No, All, and Cancel buttons.
    /// </summary>
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

/// <summary>
/// Specifies the result of a <see cref="KryptonMessageBoxExtended"/> dialog.
/// </summary>
public enum ExtendedMessageBoxDialogResult
{
    /// <summary>Specifies that no button was clicked.</summary>
    None = DialogResult.None,
    /// <summary>Specifies that the OK button was clicked.</summary>
    Ok = DialogResult.OK,
    /// <summary>Specifies that the Cancel button was clicked.</summary>
    Cancel = DialogResult.Cancel,
    /// <summary>Specifies that the Abort button was clicked.</summary>
    Abort = DialogResult.Abort,
    /// <summary>Specifies that the Retry button was clicked.</summary>
    Retry = DialogResult.Retry,
    /// <summary>Specifies that the Ignore button was clicked.</summary>
    Ignore = DialogResult.Ignore,
    /// <summary>Specifies that the Yes button was clicked.</summary>
    Yes = DialogResult.Yes,
    /// <summary>Specifies that the No button was clicked.</summary>
    No = DialogResult.No,
    /// <summary>Specifies that the Try Again button was clicked.</summary>
    TryAgain = 8,
    /// <summary>Specifies that the Continue button was clicked.</summary>
    Continue = 9,
    /// <summary>Specifies that the timeout occurred.</summary>
    Timeout = 1000
}

#endregion

#region Enum ExtendedMessageBoxTimeoutAction

/// <summary>
/// Specifies the action taken when a <see cref="KryptonMessageBoxExtended"/> timeout reaches zero
/// and auto-close is enabled.
/// </summary>
public enum ExtendedMessageBoxTimeoutAction
{
    /// <summary>
    /// Close the message box and return <see cref="KryptonMessageBoxExtendedData.TimeOutResult"/>.
    /// When that result is <see cref="DialogResult.None"/>, the default button result is used instead.
    /// </summary>
    Close = 0,

    /// <summary>Click the first action button (uses that button's <see cref="DialogResult"/>).</summary>
    ButtonOne = 1,

    /// <summary>Click the second action button (uses that button's <see cref="DialogResult"/>).</summary>
    ButtonTwo = 2,

    /// <summary>Click the third action button (uses that button's <see cref="DialogResult"/>).</summary>
    ButtonThree = 3,

    /// <summary>Click the fourth action button (uses that button's <see cref="DialogResult"/>).</summary>
    ButtonFour = 4
}

#endregion

#region Enum ExtendedMessageBoxTimeoutButton

/// <summary>
/// Specifies which button should be clicked when a <see cref="KryptonMessageBoxExtended"/> timeout reaches zero and auto-close is enabled.
/// </summary>
public enum ExtendedMessageBoxTimeoutButton
{
    /// <summary>Specifies the first action button.</summary>
    ButtonOne = 0,
    /// <summary>Specifies the second action button.</summary>
    ButtonTwo = 1,
    /// <summary>Specifies the third action button.</summary>
    ButtonThree = 2
}

#endregion