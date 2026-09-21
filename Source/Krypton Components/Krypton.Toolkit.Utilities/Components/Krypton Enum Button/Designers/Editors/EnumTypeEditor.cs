#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// A design-time drop-down editor for the <c>EnumType</c> property of the enum button controls. Uses
/// the design host's <see cref="ITypeDiscoveryService"/> to list every enumeration type available in
/// the current project so a type can be selected in the property grid instead of only in code.
/// </summary>
internal sealed class EnumTypeEditor : UITypeEditor
{
    private const string NoneText = @"(none)";

    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) => UITypeEditorEditStyle.DropDown;

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        using var listBox = new ListBox
        {
            BorderStyle = BorderStyle.None,
            IntegralHeight = false
        };

        listBox.Items.Add(NoneText);

        foreach (var enumType in DiscoverEnumTypes(provider))
        {
            listBox.Items.Add(new EnumTypeItem(enumType));
        }

        // Highlight the currently assigned type (or the "(none)" entry).
        listBox.SelectedIndex = 0;
        if (value is Type currentType)
        {
            for (var i = 1; i < listBox.Items.Count; i++)
            {
                if (listBox.Items[i] is EnumTypeItem item && item.Type == currentType)
                {
                    listBox.SelectedIndex = i;
                    break;
                }
            }
        }

        // Size the drop-down to a sensible number of visible rows.
        var visibleRows = Math.Min(Math.Max(listBox.Items.Count, 1), 12);
        listBox.Height = (listBox.ItemHeight * visibleRows) + 2;

        var selectionCommitted = false;

        listBox.MouseUp += (_, _) =>
        {
            selectionCommitted = true;
            editorService.CloseDropDown();
        };

        listBox.KeyDown += (_, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    selectionCommitted = true;
                    editorService.CloseDropDown();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    editorService.CloseDropDown();
                    e.Handled = true;
                    break;
            }
        };

        editorService.DropDownControl(listBox);

        if (!selectionCommitted)
        {
            return value;
        }

        return listBox.SelectedItem switch
        {
            EnumTypeItem selected => selected.Type,
            _ when listBox.SelectedIndex == 0 => null,
            _ => value
        };
    }

    /// <summary>Discovers the publicly visible enumeration types available in the current design project.</summary>
    /// <param name="provider">The design-time service provider.</param>
    /// <returns>The discovered enum types, ordered by full name.</returns>
    private static List<Type> DiscoverEnumTypes(IServiceProvider provider)
    {
        var result = new List<Type>();

        if (provider.GetService(typeof(ITypeDiscoveryService)) is ITypeDiscoveryService discovery)
        {
            foreach (var candidate in discovery.GetTypes(typeof(Enum), false))
            {
                if (candidate is Type { IsEnum: true, IsVisible: true } enumType && !result.Contains(enumType))
                {
                    result.Add(enumType);
                }
            }
        }

        result.Sort(static (a, b) => string.Compare(a.FullName, b.FullName, StringComparison.OrdinalIgnoreCase));
        return result;
    }

    /// <summary>Wraps a discovered enum type so the drop-down shows its full name.</summary>
    private sealed class EnumTypeItem
    {
        public EnumTypeItem(Type type) => Type = type;

        public Type Type { get; }

        public override string ToString() => Type.FullName ?? Type.Name;
    }
}
