#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit.Design.Client;

/// <summary>
/// Visual Studio-hosted folder picker used by the out-of-process designer.
/// </summary>
public sealed class KryptonDesignerFolderNameEditor : UITypeEditor
{
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance is not null ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null
            || provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService)
        {
            return value;
        }

        using var dialog = new FolderBrowserDialog
        {
            Description = @"Select folder",
            SelectedPath = value as string ?? string.Empty
        };

        return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : value;
    }
}
