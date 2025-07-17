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

    #region Implementation

    /// <summary>Captures the exception.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="title">The title.</param>
    /// <param name="callerFilePath">The calling file path.</param>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="callerMethod">The calling method.</param>
    /// <param name="showStackTrace">Show the stack trace.</param>
    /// <param name="useExceptionDialog">Use a <see cref="KryptonExceptionDialog"/> to display the exception. Set to true by default.</param>
    /// <param name="showExceptionDialogCopyButton">Show the copy button in the <see cref="KryptonExceptionDialog"/>. Set to false by default.</param>
    /// <param name="showExceptionDialogSearchBox">Show the search box in the <see cref="KryptonExceptionDialog"/>. Set to false by default.</param>
    public static void CaptureException(
        Exception exception, 
        string title = "Exception Caught",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string callerMethod = "", 
        bool showStackTrace = false, bool? useExceptionDialog = true,
        bool? showExceptionDialogCopyButton = false, bool? showExceptionDialogSearchBox = false)
    {
        if (useExceptionDialog is not null or true)
        {
            KryptonExceptionDialog.Show(exception, showExceptionDialogCopyButton, showExceptionDialogSearchBox);
        }
        else
        {
            var messageBuilder = new StringBuilder();

            messageBuilder.Append($"An unexpected error has occurred:\r\n\r\n");
            messageBuilder.Append($"Class: {callerFilePath}\r\n");
            messageBuilder.Append($"Method: {callerMethod}\r\n");
            messageBuilder.Append($"Line: {lineNumber}\r\n");
            messageBuilder.Append($"Message: {exception.Message}\r\n\r\n");

            if (showStackTrace)
            {
                messageBuilder.Append($"Stacktrace:\r\n{exception.StackTrace}\r\n");
            }

            var message = messageBuilder.ToString();

            var okButton = KryptonMessageBoxButtons.OK;

            var exceptionIcon = KryptonMessageBoxIcon.Error;

            KryptonMessageBox.Show(message, title, okButton, exceptionIcon, showCtrlCopy: true);
        }
    }

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

            writer.Write(exception.StackTrace);

            writer.Close();

            writer.Dispose();
        }
        catch (Exception e)
        {
            CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
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
            CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }
    #endregion
}