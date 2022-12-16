#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Specifies the icon type for <see cref="KryptonMessageBox"/>.</summary>
[Flags]
public enum KryptonMessageBoxIcon
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
    /// <summary>Specify a exclamation icon.</summary>
    Exclamation = 3,
    /// <summary>Specify the system exclamation icon.</summary>
    SystemExclamation = MessageBoxIcon.Exclamation,
    /// <summary>Specify a asterisk icon.</summary>
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
    /// <summary>Specify a information icon.</summary>
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
    SystemApplication = 12
}