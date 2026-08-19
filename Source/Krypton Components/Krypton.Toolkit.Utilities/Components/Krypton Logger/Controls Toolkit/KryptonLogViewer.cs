#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shows a themed log viewer over the active <see cref="MemoryLogSink"/>.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonLogViewer
{
    /// <summary>Gets the localisable viewer strings.</summary>
    public static KryptonLogViewerStrings Strings { get; } = new();

    /// <summary>Displays the log viewer as a modal dialog.</summary>
    public static DialogResult Show() => Show(null);

    /// <summary>Displays the log viewer as a modal dialog owned by <paramref name="owner"/>.</summary>
    /// <param name="owner">Optional owner window.</param>
    public static DialogResult Show(IWin32Window? owner)
    {
        using var form = new VisualKryptonLogViewerForm();
        return owner == null ? form.ShowDialog() : form.ShowDialog(owner);
    }

    /// <summary>
    /// Writes a temp file containing recent memory-sink events (or copies the active file).
    /// Returns null when nothing can be exported.
    /// </summary>
    /// <param name="recentLineCount">Maximum events from the memory sink.</param>
    public static string? TryCreateLogExcerptFile(int recentLineCount = 200)
    {
        var memory = KryptonLog.Memory;
        if (memory != null && memory.Count > 0)
        {
            var path = Path.Combine(Path.GetTempPath(), $"Krypton-{DateTime.Now:yyyyMMdd-HHmmss}.log");
            File.WriteAllText(path, memory.FormatRecent(recentLineCount), Encoding.UTF8);
            return path;
        }

        var filePath = KryptonLog.ActiveFilePath;
        if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
        {
            return filePath;
        }

        return null;
    }

    /// <summary>
    /// Returns formatted recent events for clipboard / exception copy, or an empty string.
    /// </summary>
    public static string FormatRecent(int count = 50) =>
        KryptonLog.Memory?.FormatRecent(count) ?? string.Empty;
}
