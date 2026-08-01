#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Krypton-themed designer editor for choosing an initial directory path (folder browser).
/// </summary>
/// <remarks>
/// Prefer this editor for properties that represent a dialog's starting folder.
/// For general folder paths, use <see cref="KryptonDesignerFolderNameEditor"/> or
/// <see cref="KryptonDesignerSelectedPathEditor"/>.
/// </remarks>
public sealed class KryptonInitialDirectoryEditor : KryptonDesignerFolderNameEditor
{
    #region Protected
    /// <inheritdoc />
    protected override KryptonFolderBrowserDialog CreateFolderDialog(ITypeDescriptorContext? context, string description) =>
        base.CreateFolderDialog(context, @"Select the directory that will initially be opened in the dialog.");
    #endregion
}
