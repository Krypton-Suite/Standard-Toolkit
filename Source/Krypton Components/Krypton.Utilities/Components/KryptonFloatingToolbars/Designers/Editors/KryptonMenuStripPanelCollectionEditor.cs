#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

internal class KryptonMenuStripPanelCollectionEditor : UITypeEditor
{
    #region Public Overrides

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) => base.GetEditStyle(context);

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider? provider, object? value)
    {
        IWindowsFormsEditorService? service = provider?.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

        KryptonFloatableMenuStrip? floatableMenuStrip = context?.Instance as KryptonFloatableMenuStrip;

        if (floatableMenuStrip != null)
        {
            VisualMenuStripExistingComponentChooserForm ecc = new VisualMenuStripExistingComponentChooserForm(floatableMenuStrip.MenuStripPanelExtendedList);

            ecc.Text = @"MenuStripPanelCollectionEditor";

            if (floatableMenuStrip.OriginalParent != null)
            {
                if (floatableMenuStrip.OriginalParent is KryptonForm)
                {
                    ecc.SourceComponentContainer = floatableMenuStrip.OriginalParent;
                }
                else
                {
                    ecc.SourceComponentContainer = floatableMenuStrip.OriginalParent.Parent;
                }
            }

            if (service != null)
            {
                if (service.ShowDialog(ecc) == DialogResult.OK)
                {
                    return ecc.SelectedComponents;
                }
            }
        }

        if (floatableMenuStrip != null)
        {
            return floatableMenuStrip.MenuStripPanelExtendedList;
        }

        return null;
    }

    #endregion
}
