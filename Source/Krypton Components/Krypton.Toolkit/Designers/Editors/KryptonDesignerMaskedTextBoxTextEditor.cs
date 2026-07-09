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
/// Krypton-themed designer editor for masked text properties.
/// </summary>
public sealed class KryptonDesignerMaskedTextBoxTextEditor : UITypeEditor
{
    #region Identity
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance is not null ? UITypeEditorEditStyle.DropDown : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null
            || provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        using var dropDown = new KryptonDesignerMaskedTextBoxTextEditorDropDown(context.Instance, value as string);
        editorService.DropDownControl(dropDown);
        return dropDown.Value ?? value;
    }

    /// <inheritdoc />
    public override bool GetPaintValueSupported(ITypeDescriptorContext? context) => false;
    #endregion
}
