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
/// Provides static methods for displaying dialogs that compute or verify file checksums using supported hash
/// algorithms.
/// </summary>
/// <remarks>This class is intended for use in Windows Forms applications to facilitate file checksum operations
/// through modal dialogs. All methods are static and display dialogs for selecting files, computing hashes, or
/// verifying file integrity. The dialogs can be owned by a parent window or shown centered on screen if no owner is
/// specified.</remarks>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonComputeFileCheckSum
{
    /// <summary>
    /// Displays a dialog for selecting or confirming a file path for calculating a file checksum.
    /// </summary>
    /// <param name="owner">The window that owns the dialog. If null, the dialog is shown without an owner.</param>
    /// <param name="filePath">The initial file path to display in the dialog. If null, no file path is preselected.</param>
    /// <returns>A DialogResult value indicating how the user closed the dialog.</returns>
    public static DialogResult Show(IWin32Window? owner = null, string? filePath = null) => ShowCore(owner, filePath, null, null);

    /// <summary>
    /// Displays a dialog that computes and shows the hash of the specified file using the given algorithm.
    /// </summary>
    /// <param name="owner">The window that owns the dialog. Can be null to indicate no owner.</param>
    /// <param name="filePath">The path to the file whose hash will be calculated. Can be null if no file is specified.</param>
    /// <param name="algorithim">The hash algorithm to use for computing the file's hash.</param>
    /// <returns>A DialogResult value indicating how the user closed the dialog.</returns>
    public static DialogResult Show(IWin32Window? owner, string? filePath, SupportedHashAlgorithims algorithim) => ShowCore(owner, filePath, algorithim, null);

    /// <summary>
    /// Displays a dialog that verifies the hash of the specified file using the provided hash algorithm. 
    /// </summary>
    /// <param name="owner">The window that owns the dialog. Can be null to indicate no owner.</param>
    /// <param name="filePath">The path to the file to be verified. Can be null if no file is specified.</param>
    /// <param name="hashAlgorithm">The hash algorithm to use for verification. Must be a supported algorithm.</param>
    /// <returns>A DialogResult value indicating the user's response to the dialog.</returns>
    public static DialogResult Show(IWin32Window? owner, string? filePath, SafeNETAndNewerSupportedHashAlgorithms hashAlgorithm) => ShowCore(owner, filePath, null, hashAlgorithm);

    /// <summary>
    /// Displays the compute file checksum form as a modal dialog.
    /// </summary>
    /// <param name="owner">The window that will own the dialog. Can be null to use the active window or center on screen.</param>
    /// <param name="filePath">Optional initial file path to display. If non-null and the file exists, the path is pre-filled.</param>
    /// <returns>One of the <see cref="DialogResult" /> values.</returns>
    internal static DialogResult ShowCore(IWin32Window? owner = null, string? filePath = null, SupportedHashAlgorithims? algorithim = null, SafeNETAndNewerSupportedHashAlgorithms? hashAlgorithm = null)
    {
        using var form = new VisualComputeFileCheckSumForm(filePath, algorithim, hashAlgorithm);

        form.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
        
        return form.ShowDialog(owner);
    }
}
