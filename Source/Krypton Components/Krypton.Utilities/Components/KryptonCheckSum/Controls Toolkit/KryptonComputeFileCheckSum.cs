namespace Krypton.Utilities;

/// <summary>
/// Displays the compute file checksum dialog.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonComputeFileCheckSum
{
    public static DialogResult Show(IWin32Window? owner = null, string? filePath = null) => ShowCore(owner, filePath, null, null);

    public static DialogResult Show(IWin32Window? owner, string? filePath, SupportedHashAlgorithims algorithim) => ShowCore(owner, filePath, algorithim, null);

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
