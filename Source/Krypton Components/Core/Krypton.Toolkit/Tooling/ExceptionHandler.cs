#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Handles exceptions raised by Toolkit code.
/// </summary>
internal class ExceptionHandler
{
    /// <summary>Initializes a new instance of the <see cref="ExceptionHandler"/> class.</summary>
    public ExceptionHandler()
    {
    }

    /// <summary>Captures and displays an exception.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="title">The dialog title.</param>
    /// <param name="callerFilePath">The calling file path.</param>
    /// <param name="lineNumber">The calling line number.</param>
    /// <param name="callerMethod">The calling method.</param>
    /// <param name="showStackTrace">Whether to include the stack trace.</param>
    /// <param name="useExceptionDialog">Whether to use the detailed exception dialog.</param>
    /// <param name="showExceptionDialogCopyButton">Whether to show the copy button.</param>
    /// <param name="showExceptionDialogSearchBox">Whether to show the search box.</param>
    public static void CaptureException(
        Exception exception,
        string title = "Exception Caught",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string callerMethod = "",
        bool showStackTrace = false,
        bool? useExceptionDialog = true,
        bool? showExceptionDialogCopyButton = false,
        bool? showExceptionDialogSearchBox = false)
    {
        if (useExceptionDialog is null or true)
        {
            KryptonExceptionDialog.Show(exception, showExceptionDialogCopyButton, showExceptionDialogSearchBox);
        }
        else
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("An unexpected error has occurred:\r\n\r\n");
            messageBuilder.Append($"Class: {callerFilePath}\r\n");
            messageBuilder.Append($"Method: {callerMethod}\r\n");
            messageBuilder.Append($"Line: {lineNumber}\r\n");
            messageBuilder.Append($"Message: {exception.Message}\r\n\r\n");

            if (showStackTrace)
            {
                messageBuilder.Append($"Stacktrace:\r\n{exception.StackTrace}\r\n");
            }

            KryptonMessageBox.Show(messageBuilder.ToString(), title, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: true);
        }
    }

    /// <summary>Writes the exception and stack trace to a file.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="fileName">The output file name.</param>
    public static void PrintStackTrace(Exception exception, string fileName)
    {
        try
        {
            using var writer = new StreamWriter(fileName);
            writer.Write(exception);
            writer.Write(exception.StackTrace);
        }
        catch (Exception e)
        {
            CaptureException(e, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>Writes the exception stack trace to a file.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="fileName">The output file name.</param>
    public static void PrintExceptionStackTrace(Exception exception, string fileName)
    {
        try
        {
            using var writer = new StreamWriter(fileName);
            writer.Write(exception.StackTrace);
        }
        catch (Exception e)
        {
            CaptureException(e, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }
}
