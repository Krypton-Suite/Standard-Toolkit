namespace Krypton.Utilities;

/// <summary>
/// Displays the verify file checksum dialog. The API mirrors the pattern used by
/// <see cref="Krypton.Toolkit.KryptonMessageBox" />: static <see cref="Show" /> methods with optional owner, file path and expected hash.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonVerifyFileCheckSum
{
    public static DialogResult Show(IWin32Window? owner = null) => ShowCore(owner);

    public static DialogResult Show(IWin32Window? owner, string filePath) => ShowCore(owner, filePath);

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
