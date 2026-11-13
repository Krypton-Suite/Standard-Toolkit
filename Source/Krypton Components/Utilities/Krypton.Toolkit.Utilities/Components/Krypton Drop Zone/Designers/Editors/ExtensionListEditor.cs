#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public sealed class ExtensionListEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;

    /*
    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        var service = provider?.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
        if (service == null)
        {
            return value;
        }

        using (var form = new VisualExtensionEditorForm(value as string ?? string.Empty))
        {
            return service.ShowDialog(form) == DialogResult.OK ? form.Result : value;
        }
    }*/
}
