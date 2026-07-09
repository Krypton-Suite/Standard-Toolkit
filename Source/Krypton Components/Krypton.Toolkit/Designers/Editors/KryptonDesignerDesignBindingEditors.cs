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
/// Krypton-themed designer editor for data-member fields on data-bound controls.
/// </summary>
public sealed class KryptonDesignerDataMemberFieldEditor : UITypeEditor
{
    /// <inheritdoc />
    public override bool IsDropDownResizable => true;

    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        UITypeEditorEditStyle.DropDown;

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value) =>
        KryptonDesignerDesignBindingEditorHelper.EditDataMember(context, provider, value);
}

/// <summary>
/// Krypton-themed designer editor for data-source selection on data-bound controls.
/// </summary>
public sealed class KryptonDesignerDataSourceListEditor : UITypeEditor
{
    /// <inheritdoc />
    public override bool IsDropDownResizable => true;

    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        UITypeEditorEditStyle.DropDown;

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value) =>
        KryptonDesignerDesignBindingEditorHelper.EditDataSource(context, provider, value);
}

/// <summary>
/// Shared helpers for Krypton design-time data-binding pickers.
/// </summary>
internal static class KryptonDesignerDesignBindingEditorHelper
{
    internal static object? EditDataMember(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null
            || TypeDescriptor.GetProperties(context.Instance)[nameof(ComboBox.DataSource)] is not PropertyDescriptor dataSourceProperty
            || provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        var dataSource = dataSourceProperty.GetValue(context.Instance);
        using var picker = new KryptonDesignerDesignBindingPicker(
            context,
            provider,
            showDataSources: false,
            showDataMembers: true,
            selectListMembers: false,
            rootDataSource: dataSource,
            currentDataMember: value as string);

        editorService.DropDownControl(picker);
        return picker.SelectedDataMember ?? value;
    }

    internal static object? EditDataSource(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null
            || provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        using var picker = new KryptonDesignerDesignBindingPicker(
            context,
            provider,
            showDataSources: true,
            showDataMembers: false,
            selectListMembers: false,
            rootDataSource: null,
            currentDataMember: null,
            currentDataSource: value);

        editorService.DropDownControl(picker);
        return picker.SelectedDataSource ?? value;
    }
}
