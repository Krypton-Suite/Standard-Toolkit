#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit.Design.Client;

/// <summary>
/// Visual Studio-hosted image picker used by the out-of-process designer.
/// </summary>
public sealed class KryptonDesignerImageEditor : UITypeEditor
{
    /// <inheritdoc />
    public override bool GetPaintValueSupported(ITypeDescriptorContext? context) => true;

    /// <inheritdoc />
    public override void PaintValue(PaintValueEventArgs e)
    {
        if (e.Value is Image image)
        {
            var bounds = e.Bounds;
            bounds.Width = Math.Max(bounds.Width - 1, 0);
            bounds.Height = Math.Max(bounds.Height - 1, 0);
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, bounds);
            e.Graphics.DrawImage(image, e.Bounds);
        }
    }

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

        using var dialog = new OpenFileDialog
        {
            Title = @"Select Image",
            Filter = @"Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.ico|All Files|*.*",
            CheckFileExists = true
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return value;
        }

        return Image.FromFile(dialog.FileName);
    }
}
