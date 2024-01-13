#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Allow Krypton to be improved by getting help from users
    /// </summary>
    public static class DebugTools
    {
        #region Implementation

        /// <summary>
        /// Allow Krypton to be improved by getting help from users
        /// </summary>
        public static Exception NotImplemented(string outOfRange,
            [CallerFilePath] string callingFilePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string? callingMethod = "")
        {
            var linkCommand = new KryptonCommand();

            linkCommand.Execute += (sender, args) =>
            {
                Process.Start(@"https://github.com/Krypton-Suite/Standard-Toolkit/issues/new/choose");
            };

            KryptonMessageBox.Show(
                $"If you are seeing this message, please submit a new bug report here.\n\nAdditional details:-\nMethod Signature: {callingMethod}\nFunction: {callingMethod}\nFile: {callingFilePath}\nLine Number: {lineNumber}",
                "Not Implemented", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error,
                contentAreaType: MessageBoxContentAreaType.LinkLabel, actionButtonCommand: linkCommand,
                contentLinkArea: new LinkArea(64, 67));
            return new ArgumentOutOfRangeException(outOfRange)
            {
                Source = callingMethod,
            };
        }

        #endregion
    }
}