#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides static methods for displaying dialogs that verify the checksum of a file, allowing users to compare file
/// hashes and confirm file integrity.
/// </summary>
/// <remarks>Use the methods in this class to present a modal dialog for verifying file checksums. The dialogs can
/// be associated with a window owner and optionally pre-filled with a file path and expected hash value. This class is
/// intended for use in scenarios where file integrity needs to be confirmed by the user, such as after downloading or
/// transferring files. All methods are static and thread-safe.</remarks>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonVerifyFileCheckSum
{
    /// <summary>
    /// Displays the dialog box and returns the result of the user's action.
    /// </summary>
    /// <param name="owner">The window that will own the dialog box. If null, the dialog box is shown without an owner.</param>
    /// <returns>A DialogResult value indicating which button the user clicked to close the dialog box.</returns>
    public static DialogResult Show(IWin32Window? owner = null) => ShowCore(owner);

    /// <summary>
    /// Displays a dialog for the specified file path, optionally associating it with a window owner.
    /// </summary>
    /// <param name="owner">The window that owns the dialog. Specify null to display the dialog without an owner.</param>
    /// <param name="filePath">The path to the file for which the dialog is displayed. Cannot be null or empty.</param>
    /// <returns>A DialogResult value indicating how the user closed the dialog.</returns>
    public static DialogResult Show(IWin32Window? owner, string filePath) => ShowCore(owner, filePath);

    /// <summary>
    /// Displays a dialog that verifies the hash of the specified file and returns the user's response.
    /// </summary>
    /// <param name="owner">The window that owns the dialog. Can be null to indicate no owner.</param>
    /// <param name="filePath">The path to the file whose hash will be verified. Can be null if no file is specified.</param>
    /// <param name="expectedHash">The expected hash value to compare against the file's actual hash. Can be null if no hash is provided.</param>
    /// <returns>A DialogResult value indicating the user's action in the dialog.</returns>
    public static DialogResult Show(IWin32Window? owner, string? filePath, string? expectedHash) => ShowCore(owner, filePath, expectedHash);

    /// <summary>
    /// Displays the verify file checksum form as a modal dialog.
    /// </summary>
    /// <param name="owner">The window that will own the dialog. Can be null to use the active window or center on screen.</param>
    /// <param name="filePath">Optional initial file path to display. If non-null and the file exists, the path is pre-filled.</param>
    /// <param name="expectedHash">Optional expected hash value to verify against. If non-null, the verify field is pre-filled.</param>
    /// <returns>One of the <see cref="DialogResult" /> values.</returns>
    internal static DialogResult ShowCore(IWin32Window? owner = null, string? filePath = null, string? expectedHash = null)
    {
        using var form = new VisualVerifyFileCheckSumForm
        {
            InitialFilePath = filePath,
            InitialExpectedHash = expectedHash
        };
        form.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
        return form.ShowDialog(owner);
    }
}
