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
/// Krypton-themed designer editor for choosing a folder path.
/// </summary>
public class KryptonDesignerFolderNameEditor : UITypeEditor
{
    #region Identity
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        UITypeEditorEditStyle.Modal;

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        using var dialog = CreateFolderDialog(context, @"Select a directory");
        if (value is string path && !string.IsNullOrWhiteSpace(path))
        {
            dialog.SelectedPath = path;
        }

        return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : value;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Creates the folder browser dialog shown by the editor.
    /// </summary>
    /// <param name="context">Designer context.</param>
    /// <param name="description">Dialog description.</param>
    /// <returns>Folder browser dialog.</returns>
    protected virtual KryptonFolderBrowserDialog CreateFolderDialog(ITypeDescriptorContext? context, string description)
    {
        var dialog = new KryptonFolderBrowserDialog();
        if (!string.IsNullOrWhiteSpace(description))
        {
            dialog.Title = description;
        }

        return dialog;
    }
    #endregion
}

/// <summary>
/// Krypton-themed designer editor for choosing the initial folder path of a folder browser dialog.
/// </summary>
public sealed class KryptonDesignerSelectedPathEditor : KryptonDesignerFolderNameEditor
{
    #region Protected
    /// <inheritdoc />
    protected override KryptonFolderBrowserDialog CreateFolderDialog(ITypeDescriptorContext? context, string description) =>
        base.CreateFolderDialog(context, @"Select the directory that will initially be opened in the dialog.");
    #endregion
}
