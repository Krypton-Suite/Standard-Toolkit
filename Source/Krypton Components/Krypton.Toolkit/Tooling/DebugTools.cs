#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Allow Krypton to be improved by getting help from users
/// </summary>
public static class DebugTools
{
    #region Implementation

    /// <summary>
    /// Allow Krypton to be improved by getting help from users
    /// </summary>
    public static Exception NotImplemented(string? outOfRange,
        [CallerFilePath] string callingFilePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string? callingMethod = "")
    {
        // Do not use `KryptonMessageBox` as this will cause palette's to go into recurrent loop 
        if (DialogResult.Yes == MessageBox.Show(
                $"If you are seeing this message, please submit a new bug report here.\n\nAdditional details:-\nMethod Signature: {callingMethod}\nFunction: {callingMethod}\nFile: {callingFilePath}\nLine Number: {lineNumber}",
                "Not Implemented - Please submit ?", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) )
        {
            Process.Start(@"https://github.com/Krypton-Suite/Standard-Toolkit/issues/new/choose");
        }
        return new ArgumentOutOfRangeException(outOfRange)
        {
            Source = callingMethod
        };
    }

    #endregion
}