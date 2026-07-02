#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class PaletteCornerRoundingEditor : UITypeEditor
{
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) => context?.Instance != null
        ? UITypeEditorEditStyle.DropDown
        : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider is null)
        {
            throw new NullReferenceException(GlobalStaticFunctions.VariableCannotBeNull(nameof(provider)));
        }

        if (context is not null && value is PaletteCornerRounding cornerRounding)
        {
            if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService service)
            {
                PaletteCornerRoundingSelector selector = new PaletteCornerRoundingSelector
                {
                    Value = cornerRounding
                };

                service.DropDownControl(selector);

                return selector.Value;
            }
        }

        return base.EditValue(context, provider, value);
    }
}
