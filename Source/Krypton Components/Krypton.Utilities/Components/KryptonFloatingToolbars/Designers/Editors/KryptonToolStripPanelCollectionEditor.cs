#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

internal class KryptonToolStripPanelCollectionEditor : UITypeEditor
{
    #region Public Overrides

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return base.GetEditStyle(context);
    }

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider? provider, object? value)
    {
        IWindowsFormsEditorService? service = provider?.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

        KryptonFloatableToolStrip? floatableToolStrip = context?.Instance as KryptonFloatableToolStrip;

        VisualToolStripExistingComponentChooserForm ecc = new VisualToolStripExistingComponentChooserForm(floatableToolStrip?.KryptonToolStripPanelExtendedList);

        ecc.Text = @"ToolStripPanelCollectionEditor";

        if (floatableToolStrip?.OriginalParent != null)
        {
            if (floatableToolStrip.OriginalParent is KryptonForm)
            {
                ecc.SourceComponentContainer = floatableToolStrip.OriginalParent;
            }
            else
            {
                ecc.SourceComponentContainer = floatableToolStrip.OriginalParent.Parent;
            }
        }

        if (service != null)
        {
            if (service.ShowDialog(ecc) == DialogResult.OK)
            {
                return ecc.SelectedComponents;
            }
        }

        return floatableToolStrip?.KryptonToolStripPanelExtendedList;
    }

    #endregion
}