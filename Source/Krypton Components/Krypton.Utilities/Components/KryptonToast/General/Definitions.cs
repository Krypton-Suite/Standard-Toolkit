using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Utilities;

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

public enum KryptonToastContentAreaType
{
    RichTextBox = 0,
    MultiLineTextBox = 1,
    WrapLinkLabel = 2,
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

public enum KryptonToastActionButton
{
    Button1 = 0,
    Button2 = 1
    //Button3 = 2
}

#endregion

#region Enum KryptonToastActionType

public enum KryptonToastActionType
{
    Default = 0,
    Dismiss = 1,
    LaunchProcess = 2,
    Open = 3
}

#endregion

#region Enum KryptonToastDismissButtonLocation

public enum KryptonToastDismissButtonLocation
{
    Left = 0,
    Right = 1
}

#endregion

#region Enum KryptonToastAlignment

public enum KryptonToastAlignment
{
    LeftToRight = 0,
    RightToLeft = 1
}

#endregion

#region Enum KryptonToastResponseType

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

public enum KryptonToastType
{
    Basic = 0,
    BasicWithProgressBar = 1,
    UserInput = 2,
    UserInputWithProgressBar = 3
}

#endregion

#region KryptonToastResult

/// <summary>
/// Options for the <see cref="KryptonToast"/>.
/// </summary>
public enum KryptonToastResult
{
    None = DialogResult.None,
    Ok = DialogResult.OK,
    Cancel = DialogResult.Cancel,
    Abort = DialogResult.Abort,
    Retry = DialogResult.Retry,
    Ignore = DialogResult.Ignore,
    Yes = DialogResult.Yes,
    No = DialogResult.No,
    Close = 8,
    Help = 9,
#if NET8_0_OR_GREATER
        TryAgain = DialogResult.TryAgain,
        Continue = DialogResult.Continue,
#else
    TryAgain = 10,
    Continue = 11,
#endif
    TimeOut = 12,
    DoNotShowAgain = 13
}

#endregion

#endregion