#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonCheckButtonCollectionEditor : UITypeEditor
{
    /// <summary>
    /// Gets the editor style used by the EditValue method.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
    /// <returns>A UITypeEditorEditStyle enumeration value that indicates the style of editor.</returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance != null ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);

    /// <summary>
    /// Edits the specified object's value using the editor style indicated by GetEditStyle.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
    /// <param name="provider">An IServiceProvider that this editor can use to obtain services.</param>
    /// <param name="value">The object to edit.</param>
    /// <returns></returns>
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context is not null && provider is not null && value is not null)
        {
            // Must use the editor service for showing dialogs
            //var editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService editorService)
            {
                // Cast the value to the correct type
                if (context.Instance is KryptonCheckSet checkSet)
                {
                    // Create the dialog used to edit the set of KryptonCheckButtons
                    KryptonCheckButtonCollectionForm dialog = new(checkSet);

                    if (editorService.ShowDialog(dialog) == DialogResult.OK)
                    {
                        // Notify container that value has been changed
                        context.OnComponentChanged();
                    }
                }
            }
        }

        return value;
    }
}