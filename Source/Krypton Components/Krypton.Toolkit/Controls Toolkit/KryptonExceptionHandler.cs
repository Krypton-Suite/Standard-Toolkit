#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary></summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonExceptionHandler
{
    #region Public

    /// <summary>Captures the exception.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="title">The title.</param>
    /// <param name="callerFilePath">The caller file path.</param>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="callerMethod">The caller method.</param>
    /// <param name="showStackTrace">if set to <c>true</c> [show stack trace].</param>
    /// <param name="useExceptionDialog">The use exception dialog.</param>
    public static void CaptureException(Exception exception,
        string title = "Exception Caught",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string callerMethod = "",
        bool showStackTrace = false, bool? useExceptionDialog = true) =>
        ExceptionHandler.CaptureException(exception, title, callerFilePath, lineNumber, callerMethod,
            showStackTrace, useExceptionDialog);

    /// <summary>Prints the stack trace.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="fileName">Name of the file.</param>
    public static void PrintStackTrace(Exception exception, string fileName) =>
        ExceptionHandler.PrintStackTrace(exception, fileName);

    /// <summary>Prints the exception stack trace.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="fileName">Name of the file.</param>
    public static void PrintExceptionStackTrace(Exception exception, string fileName) =>
        ExceptionHandler.PrintExceptionStackTrace(exception, fileName);

    #endregion
}