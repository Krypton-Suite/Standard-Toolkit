#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Helper class for extracting file and folder icons from the Windows Shell.
/// </summary>
internal static class FileSystemIconHelper
{

    #region Implementation

    /// <summary>
    /// Gets the icon for a file or folder.
    /// </summary>
    /// <param name="path">The path to the file or folder.</param>
    /// <param name="largeIcon">True to get a large icon, false for a small icon.</param>
    /// <returns>The icon for the file or folder, or null if extraction fails.</returns>
    public static Icon? GetFileSystemIcon(string path, bool largeIcon = false)
    {
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        try
        {
            var shfi = new PI.SHFILEINFO();
            uint flags = (uint)(PI.SHGFI_.ICON | (largeIcon ? PI.SHGFI_.LARGEICON : PI.SHGFI_.SMALLICON));

            // If the path doesn't exist, use file attributes flag
            if (!System.IO.File.Exists(path) && !System.IO.Directory.Exists(path))
            {
                flags |= (uint)PI.SHGFI_.USEFILEATTRIBUTES;
            }

            IntPtr result = PI.SHGetFileInfo(path, 0, ref shfi, (uint)Marshal.SizeOf(shfi), flags);

            if (result != IntPtr.Zero && shfi.hIcon != IntPtr.Zero)
            {
                Icon icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();
                PI.DestroyIcon(shfi.hIcon);
                return icon;
            }
        }
        catch (Exception)
        {
            // Return null on any error
        }

        return null;
    }

    /// <summary>
    /// Gets the icon for a folder.
    /// </summary>
    /// <param name="largeIcon">True to get a large icon, false for a small icon.</param>
    /// <returns>The folder icon. The caller is responsible for disposing the returned icon unless it's a system icon.</returns>
    public static Icon? GetFolderIcon(bool largeIcon = false)
    {
        try
        {
            // This returns a disposable icon
            return GetFileSystemIcon(Environment.GetFolderPath(Environment.SpecialFolder.Windows), largeIcon);
        }
        catch
        {
            // Fallback to stock icon (this returns a disposable icon)
            try
            {
                return StockIconHelper.GetStockIcon(StockIconHelper.StockIconId.Folder);
            }
            catch
            {
                // System icons should not be disposed - they are shared resources
                return SystemIcons.Application;
            }
        }
    }

    /// <summary>
    /// Gets the icon for a file based on its extension.
    /// </summary>
    /// <param name="extension">The file extension (e.g., ".txt").</param>
    /// <param name="largeIcon">True to get a large icon, false for a small icon.</param>
    /// <returns>The file icon.</returns>
    public static Icon? GetFileIcon(string extension, bool largeIcon = false)
    {
        if (string.IsNullOrEmpty(extension))
        {
            extension = ".txt";
        }

        if (!extension.StartsWith(".", StringComparison.Ordinal))
        {
            extension = "." + extension;
        }

        // Create a temporary file path with the extension
        string tempPath = Path.Combine(Path.GetTempPath(), "temp" + extension);
        return GetFileSystemIcon(tempPath, largeIcon);
    }

    #endregion
}

