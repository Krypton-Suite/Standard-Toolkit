#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>The public interface to the <see cref="VisualExceptionDialogForm"/> class.</summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonExceptionDialog
{
    #region Public

    /// <summary>
    /// Displays the specified exception to the user using the default error dialog.
    /// </summary>
    /// <remarks>This method shows the exception details in a standard dialog box. Use this overload to
    /// present error information without customizing the dialog's appearance or behavior.</remarks>
    /// <param name="exception">The exception to display. Cannot be null.</param>
    public static void Show(Exception exception) =>
        ShowCore(exception, null, null, null, null);

    /// <summary>
    /// Displays the specified exception in a user interface dialog, optionally highlighting the dialog with a custom color.
    /// </summary>
    /// <param name="exception">The exception to display. Cannot be null.</param>
    /// <param name="highlightColor">An optional color used to highlight the dialog. If null, the default highlight color is used.</param>
    public static void Show(Exception exception, Color? highlightColor) =>
        ShowCore(exception, highlightColor, null, null, null);

    /// <summary>
    /// Displays a dialog showing details of the specified exception, with optional controls for copying the error information and searching for solutions.
    /// </summary>
    /// <param name="exception">The exception to display in the dialog. Cannot be null.</param>
    /// <param name="showCopyButton">Indicates whether to show a button that allows users to copy the exception details. If <see langword="true"/>,
    /// the copy button is displayed; if <see langword="false"/>, it is hidden; if <see langword="null"/>, the default
    /// behavior is used.</param>
    /// <param name="showSearchBox">Indicates whether to show a search box for users to look up information related to the exception. If <see
    /// langword="true"/>, the search box is displayed; if <see langword="false"/>, it is hidden; if <see
    /// langword="null"/>, the default behavior is used.</param>
    public static void Show(Exception exception, bool? showCopyButton, bool? showSearchBox) =>
        ShowCore(exception, null, showCopyButton, showSearchBox, null);

    /// <summary>
    /// Displays a dialog that presents details about the specified exception, with optional UI features such as highlighting and copy/search controls.
    /// </summary>
    /// <remarks>This method provides a convenient way to present exception information to users with customizable UI options. The dialog may include features such as syntax highlighting, copy-to-clipboard functionality, and search capabilities depending on the parameters provided.</remarks>
    /// <param name="exception">The exception to display in the dialog. Cannot be null.</param>
    /// <param name="highlightColor">An optional color used to highlight key information in the dialog. If null, the default highlight color is used.</param>
    /// <param name="showCopyButton">An optional value indicating whether to show a button that allows users to copy exception details. If null, the default behavior is applied.</param>
    /// <param name="showSearchBox">An optional value indicating whether to show a search box for filtering exception details. If null, the default behavior is applied.</param>
    public static void Show(Exception exception, Color? highlightColor, bool? showCopyButton, bool? showSearchBox) =>
        ShowCore(exception, highlightColor, showCopyButton, showSearchBox, null);

    /// <summary>
    /// Displays a dialog that presents details about the specified exception, with optional UI features and bug reporting capability.
    /// </summary>
    /// <param name="exception">The exception to display in the dialog. Cannot be null.</param>
    /// <param name="highlightColor">An optional color used to highlight key information in the dialog. If null, the default highlight color is used.</param>
    /// <param name="showCopyButton">An optional value indicating whether to show a button that allows users to copy exception details. If null, the default behavior is applied.</param>
    /// <param name="showSearchBox">An optional value indicating whether to show a search box for filtering exception details. If null, the default behavior is applied.</param>
    /// <param name="bugReportCallback">An optional callback that will be invoked when the user clicks the "Report Bug" button. If provided, a "Report Bug" button will be shown.</param>
    public static void Show(Exception exception, Color? highlightColor, bool? showCopyButton, bool? showSearchBox, Action<Exception>? bugReportCallback) =>
        ShowCore(exception, highlightColor, showCopyButton, showSearchBox, bugReportCallback);

    #endregion

    #region Implementation

    /// <summary>
    /// Displays a dialog showing details for the specified exception, with optional UI features such as highlighting, a copy button, and a search box.
    /// </summary>
    /// <param name="exception">The exception to display in the dialog. Cannot be null.</param>
    /// <param name="highlightColor">An optional color used to highlight elements in the dialog. If null, the default highlight color is used.</param>
    /// <param name="showCopyButton">An optional value indicating whether to show a button for copying exception details. If null, the default behavior is used.</param>
    /// <param name="showSearchBox">An optional value indicating whether to show a search box for filtering exception details. If null, the default behavior is used.</param>
    /// <param name="bugReportCallback">An optional callback that will be invoked when the user clicks the "Report Bug" button.</param>
    private static void ShowCore(Exception exception, Color? highlightColor, bool? showCopyButton, bool? showSearchBox, Action<Exception>? bugReportCallback) =>
        VisualExceptionDialogForm.Show(exception, highlightColor, showCopyButton, showSearchBox, bugReportCallback);

    #endregion
}