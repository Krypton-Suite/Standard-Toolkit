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

internal class PaletteDrawBordersEditor : UITypeEditor
{
    /// <summary>
    /// Gets the editor style used by the EditValue method.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
    /// <returns>UITypeEditorEditStyle value.</returns>
    /// <remarks>
    /// We show a drop-down for editing the PaletteDrawBorders value.
    /// </remarks>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) => context?.Instance != null
        ? UITypeEditorEditStyle.DropDown
        : base.GetEditStyle(context);

    /// <summary>
    /// Edits the specified object's value using the editor style indicated by the GetEditStyle method.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
    /// <param name="provider">An IServiceProvider that this editor can use to obtain services.</param>
    /// <param name="value">The object to edit.</param>
    /// <returns>The new value of the object.</returns>
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        // base.EditValue needs a valid reference to provider.
        if (provider is null)
        {
            throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(provider)));
        }

        if ((context is not null) && (value is not null))
        {
            // Grab the service needed to show the drop-down
            if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService service)
            {
                // Create the custom control used to edit value
                PaletteDrawBordersSelector selector = new PaletteDrawBordersSelector
                {
                    // Populate selector with starting value
                    Value = (PaletteDrawBorders)value
                };

                // Show as a drop-down control
                service.DropDownControl(selector);

                // Return the updated value
                return selector.Value;
            }
        }

        return base.EditValue(context, provider, value);
    }
}