#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region Toast Definitions

#region Enum KryptonToastIcon

[TypeConverter(typeof(KryptonToastIconConverter))]
public enum KryptonToastIcon
{
    /// <summary>Specify no icon.</summary>
    None = 0,

    /// <summary>Specify a hand icon.</summary>
    Hand = 1,

    /// <summary>Specify the system hand icon.</summary>
    SystemHand = MessageBoxIcon.Hand,

    /// <summary>Specify a question icon.</summary>
    Question = 2,

    /// <summary>Specify the system question icon.</summary>
    SystemQuestion = MessageBoxIcon.Question,

    /// <summary>Specify an exclamation icon.</summary>
    Exclamation = 3,

    /// <summary>Specify the system exclamation icon.</summary>
    SystemExclamation = MessageBoxIcon.Exclamation,

    /// <summary>Specify an asterisk icon.</summary>
    Asterisk = 4,

    /// <summary>Specify the system asterisk icon.</summary>
    SystemAsterisk = MessageBoxIcon.Asterisk,

    /// <summary>Specify a stop icon.</summary>
    Stop = 5,

    /// <summary>Specify the system stop icon.</summary>
    SystemStop = MessageBoxIcon.Stop,

    /// <summary>Specify a error icon.</summary>
    Error = 6,

    /// <summary>Specify the system error icon.</summary>
    SystemError = MessageBoxIcon.Error,

    /// <summary>Specify a warning icon.</summary>
    Warning = 7,

    /// <summary>Specify the system warning icon.</summary>
    SystemWarning = MessageBoxIcon.Warning,

    /// <summary>Specify an information icon.</summary>
    Information = 8,

    /// <summary>Specify the system information icon.</summary>
    SystemInformation = MessageBoxIcon.Information,

    /// <summary>Specify a UAC shield icon.</summary>
    Shield = 9,

    /// <summary>Specify a Windows logo icon.</summary>
    WindowsLogo = 10,

    /// <summary>Specify your application icon.</summary>
    Application = 11,

    /// <summary>Specify the default system application icon. See <see cref="SystemIcons.Application"/>.</summary>
    SystemApplication = 12,

    /// <summary>Specify an ok icon.</summary>
    Ok = 13,

    /// <summary>Specify a custom icon.</summary>
    Custom = 14
}

#endregion

#region Enum KryptonToastContentAreaType

/// <summary>
/// Specifies the type of content area to be used in a <see cref="KryptonToast"/>.
/// </summary>
public enum KryptonToastContentAreaType
{
    /// <summary>
    /// Specifies a <see cref="KryptonRichTextBox"/> content area.
    /// </summary>
    RichTextBox = 0,
    /// <summary>
    /// Specifies a <see cref="KryptonTextBox"/> content area.
    /// </summary>
    MultiLineTextBox = 1,
    /// <summary>
    /// Specifies a <see cref="KryptonLinkLabel"/> content area.
    /// </summary>
    WrapLinkLabel = 2,
    /// <summary>
    /// Specifies a <see cref="KryptonLabel"/> content area.
    /// </summary>
    WrapLabel = 3
}

#endregion

#region Enum KryptonToastInputAreaType

public enum KryptonToastInputAreaType
{
    /// <summary>A <see cref="KryptonToast"/> with a <see cref="KryptonComboBox"/> user input.</summary>
    ComboBox = 0,
    /// <summary>A <see cref="KryptonToast"/> with a <see cref="KryptonDateTimePicker"/> user input.</summary>
    DateTime = 1,
    /// <summary>A <see cref="KryptonToast"/> with a <see cref="KryptonDomainUpDown"/> user input.</summary>
    DomainUpDown = 2,
    /// <summary>A <see cref="KryptonToast"/> with a <see cref="KryptonNumericUpDown"/> user input.</summary>
    NumericUpDown = 3,
    /// <summary>A <see cref="KryptonToast"/> with a <see cref="KryptonMaskedTextBox"/> user input.</summary>
    MaskedTextBox = 4,
    /// <summary>A <see cref="KryptonToast"/> with a <see cref="KryptonTextBox"/> user input.</summary>
    TextBox = 5
}

#endregion

#region Enum KryptonToastActionButton

/// <summary>
/// Specifies the action button to be used in a <see cref="KryptonToast"/>.
/// </summary>
public enum KryptonToastActionButton
{
    /// <summary>
    /// Specifies the first action button.
    /// </summary>
    Button1 = 0,
    /// <summary>
    /// Specifies the second action button.
    /// </summary>
    Button2 = 1
    //Button3 = 2
}

#endregion

#region Enum KryptonToastActionType

/// <summary>
/// Specifies the action type to be used in a <see cref="KryptonToast"/>.
/// </summary>
public enum KryptonToastActionType
{
    /// <summary>
    /// Specifies the default action.
    /// </summary>
    Default = 0,
    /// <summary>
    /// Specifies the dismiss action.
    /// </summary>
    Dismiss = 1,
    /// <summary>
    /// Specifies the launch process action.
    /// </summary>
    LaunchProcess = 2,
    /// <summary>
    /// Specifies the open action.
    /// </summary>
    Open = 3
}

#endregion

#region Enum KryptonToastDismissButtonLocation

/// <summary>
/// Specifies the location of the dismiss button in a <see cref="KryptonToast"/>.
/// </summary>
public enum KryptonToastDismissButtonLocation
{
    /// <summary>
    /// Specifies the dismiss button to be located on the left side of the <see cref="KryptonToast"/>.
    /// </summary>
    Left = 0,
    /// <summary>
    /// Specifies the dismiss button to be located on the right side of the <see cref="KryptonToast"/>.
    /// </summary>
    Right = 1
}

#endregion

#region Enum KryptonToastAlignment

/// <summary>
/// Specifies the alignment of the <see cref="KryptonToast"/> on the screen.
/// </summary>
public enum KryptonToastAlignment
{
    /// <summary>
    /// Specifies the <see cref="KryptonToast"/> to be aligned from left to right on the screen.
    /// </summary>
    LeftToRight = 0,
    /// <summary>
    /// Specifies the <see cref="KryptonToast"/> to be aligned from right to left on the screen.
    /// </summary>
    RightToLeft = 1
}

#endregion

#region Enum KryptonToastResponseType

/// <summary>
/// Specifies the type of response expected from a <see cref="KryptonToast"/>.
/// </summary>
public enum KryptonToastResponseType
{
    /// <summary>Returns a <see cref="bool"/> result.</summary>
    Bool = 0,
    /// <summary>Returns a <see cref="CheckBoxState"/> result.</summary>
    CheckedState = 1,
    /// <summary>Returns what ever value is selected in the <see cref="KryptonComboBox"/>.</summary>
    ComboBox = 2,
    /// <summary>Returns a <see cref="System.DateTime"/> result.</summary>
    DateTime = 3,
    /// <summary>Returns a <see cref="System.Windows.Forms.DialogResult"/> result.</summary>
    DialogResult = 4,
    /// <summary>Returns a time-out result.</summary>
    Timeout = 5,
    /// <summary>Returns a <see cref="string"/> result.</summary>
    String = 6
}

#endregion

#region Enum KryptonToastType

/// <summary>
/// Specifies the type of <see cref="KryptonToast"/> to be displayed.
/// </summary>
public enum KryptonToastType
{
    /// <summary>
    /// Specifies a basic <see cref="KryptonToast"/>.
    /// </summary>
    Basic = 0,
    /// <summary>
    /// Specifies a basic <see cref="KryptonToast"/> with a progress bar.
    /// </summary>
    BasicWithProgressBar = 1,
    /// <summary>
    /// Specifies a <see cref="KryptonToast"/> that requires user input.
    /// </summary>
    UserInput = 2,
    /// <summary>
    /// Specifies a <see cref="KryptonToast"/> that requires user input and has a progress bar.
    /// </summary>
    UserInputWithProgressBar = 3
}

#endregion

#region KryptonToastResult

/// <summary>
/// Options for the <see cref="KryptonToast"/>.
/// </summary>
public enum KryptonToastResult
{
    /// <summary>
    /// Specifies no result.
    /// </summary>
    None = DialogResult.None,
    /// <summary>
    /// Specifies an OK result.
    /// </summary>
    Ok = DialogResult.OK,
    /// <summary>
    /// Specifies a Cancel result.
    /// </summary>
    Cancel = DialogResult.Cancel,
    /// <summary>
    /// Specifies an Abort result.
    /// </summary>
    Abort = DialogResult.Abort,
    /// <summary>
    /// Specifies a Retry result.
    /// </summary>
    Retry = DialogResult.Retry,
    /// <summary>
    /// Specifies an Ignore result.
    /// </summary>
    Ignore = DialogResult.Ignore,
    /// <summary>
    /// Specifies a Yes result.
    /// </summary>
    Yes = DialogResult.Yes,
    /// <summary>
    /// Specifies a No result.
    /// </summary>
    No = DialogResult.No,
    /// <summary>
    /// Specifies a Close result.
    /// </summary>
    Close = 8,
    /// <summary>
    /// Specifies a Help result.
    /// </summary>
    Help = 9,
#if NET8_0_OR_GREATER
        /// <summary>
        /// Specifies a Try Again result.
        /// </summary>
        TryAgain = DialogResult.TryAgain,
        /// <summary>
        /// Specifies a Continue result.
        /// </summary>
        Continue = DialogResult.Continue,
#else
    /// <summary>
    /// Specifies a Try Again result.
    /// </summary>
    TryAgain = 10,
    /// <summary>
    /// Specifies a Continue result.
    /// </summary>
    Continue = 11,
#endif
    /// <summary>
    /// Specifies a TimeOut result.
    /// </summary>
    TimeOut = 12,
    /// <summary>
    /// Specifies a DoNotShowAgain result.
    /// </summary>
    DoNotShowAgain = 13
}

#endregion

#endregion