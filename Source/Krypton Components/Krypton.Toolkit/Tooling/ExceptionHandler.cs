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
    /// This class is designed to handle thrown exceptions. (FOR INTERNAL USE ONLY!)
    /// </summary>
    internal class ExceptionHandler
    {
        #region Idendity

        /// <summary>Initializes a new instance of the <see cref="ExceptionHandler" /> class.</summary>
        public ExceptionHandler()
        {

        }

        #endregion

        #region Methods

        /// <summary>Captures the exception.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="title">The title.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="callingFilePath">The calling file path.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="callingMethod">The calling method.</param>
        public static void CaptureException(Exception exception, string title = @"Exception Caught",
            KryptonMessageBoxButtons buttons = KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon icon = KryptonMessageBoxIcon.Error, [CallerFilePath] string callingFilePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string? callingMethod = "") => KryptonMessageBox.Show(
            $"An unexpected error has occurred: {exception.Message}.\n\nError in class: '{callingFilePath}'.\n\nError in method: '{callingMethod}'.\n\nLine: {lineNumber}.",
            title, buttons, icon, showCtrlCopy: true);

    /// <summary>Captures a stack trace of the exception.</summary>
        /// <param name="exception">The incoming exception.</param>
        /// <param name="fileName">The file to write the exception stack trace to.</param>
        public static void PrintStackTrace(Exception exception, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                var writer = new StreamWriter(fileName);

                writer.Write(exception.ToString());

                writer.Close();

                writer.Dispose();
            }
            catch (Exception e)
            {
                CaptureException(e);
            }
        }

        /// <summary>Captures a stack trace of the exception.</summary>
        /// <param name="exception">The incoming exception.</param>
        /// <param name="fileName">The file to write the exception stack trace to.</param>
        public static void PrintExceptionStackTrace(Exception exception, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                var writer = new StreamWriter(fileName);

                writer.Write(exception.StackTrace);

                writer.Close();

                writer.Dispose();
            }
            catch (Exception e)
            {
                CaptureException(e);
            }
        }
        #endregion
    }
}