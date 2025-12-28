#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides a visual editor for selecting Font Awesome icons in the designer.
/// </summary>
public class FontAwesomeIconEditor : UITypeEditor
{
    /// <summary>
    /// Gets the editor style used by the EditValue method.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
    /// <returns>UITypeEditorEditStyle value.</returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance != null
            ? UITypeEditorEditStyle.Modal
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
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        if (context?.Instance == null)
        {
            return base.EditValue(context, provider, value);
        }

        // Get the editor service for showing dialogs
        if (provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return base.EditValue(context, provider, value);
        }

        // Create and show the icon picker dialog
        using var dialog = new FontAwesomeIconPickerDialog();

        // Set initial value if provided
        if (value is FontAwesomeIcon icon)
        {
            dialog.SelectedIcon = icon;
        }
        else if (value is string iconName && !string.IsNullOrEmpty(iconName))
        {
            if (Enum.TryParse<FontAwesomeIcon>(iconName, true, out var parsedIcon))
            {
                dialog.SelectedIcon = parsedIcon;
            }
        }

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Notify that the value has changed
            context.OnComponentChanged();
            return dialog.SelectedIcon;
        }

        return value;
    }
}
