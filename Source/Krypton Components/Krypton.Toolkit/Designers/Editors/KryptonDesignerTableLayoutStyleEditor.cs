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
/// Krypton-themed designer editor for <see cref="TableLayoutPanel.ColumnStyles"/> and <see cref="TableLayoutPanel.RowStyles"/>.
/// </summary>
/// <remarks>
/// <see cref="KryptonTableLayoutPanel"/> installs this editor through
/// <see cref="KryptonTableLayoutPanelDescriptionProvider"/>. The inherited
/// <c>TableLayoutPanelDesigner</c> is left in place, so column and row glyphs stay.
/// The smart-tag <c>Edit Rows and Columns</c> command still opens the stock dialog.
/// </remarks>
public class KryptonDesignerTableLayoutStyleEditor : UITypeEditor
{
    private static readonly KryptonDesignerTableLayoutStyleEditor _shared = new KryptonDesignerTableLayoutStyleEditor();

    /// <summary>
    /// Editor instance returned from the table-layout property descriptors.
    /// </summary>
    internal static KryptonDesignerTableLayoutStyleEditor Shared => _shared;

    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance is TableLayoutPanel ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is not TableLayoutPanel panel)
        {
            return value;
        }

        if (provider?.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        bool editColumns = context.PropertyDescriptor?.Name != @"RowStyles";
        using (var form = new VisualTableLayoutStyleEditorForm(panel, editColumns))
        {
            KryptonDesignerEditorPalette.ApplyDesignerPalette(form, context);
            if (editorService.ShowDialog(form) == DialogResult.OK)
            {
                context.OnComponentChanged();
                NotifyLayoutProperties(panel, provider);
            }
        }

        return value;
    }

    /// <summary>
    /// Opens the Column and Row Styles dialog for <paramref name="panel"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <see langword="null"/> to center on the screen.</param>
    /// <param name="panel">Panel to edit.</param>
    /// <param name="editColumns"><see langword="true"/> to open on columns; <see langword="false"/> to open on rows.</param>
    /// <returns>The dialog result. Cancel restores the panel.</returns>
    public static DialogResult ShowDialog(IWin32Window? owner, TableLayoutPanel panel, bool editColumns)
    {
        if (panel is null)
        {
            throw new ArgumentNullException(nameof(panel));
        }

        using (var form = new VisualTableLayoutStyleEditorForm(panel, editColumns))
        {
            return owner is null ? form.ShowDialog() : form.ShowDialog(owner);
        }
    }

    private static void NotifyLayoutProperties(TableLayoutPanel panel, IServiceProvider provider)
    {
        if (provider.GetService(typeof(IComponentChangeService)) is not IComponentChangeService change)
        {
            return;
        }

        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(panel);
        Notify(change, panel, properties[@"ColumnCount"]);
        Notify(change, panel, properties[@"RowCount"]);
        Notify(change, panel, properties[@"ColumnStyles"]);
        Notify(change, panel, properties[@"RowStyles"]);
    }

    private static void Notify(IComponentChangeService change, TableLayoutPanel panel, PropertyDescriptor? property)
    {
        if (property is null)
        {
            return;
        }

        change.OnComponentChanged(panel, property, null, property.GetValue(panel));
    }
}

/// <summary>
/// Supplies Krypton style editors for <see cref="KryptonTableLayoutPanel"/> without hiding <c>ColumnStyles</c> or <c>RowStyles</c>.
/// </summary>
public sealed class KryptonTableLayoutPanelDescriptionProvider : TypeDescriptionProvider
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTableLayoutPanelDescriptionProvider"/> class.
    /// </summary>
    public KryptonTableLayoutPanelDescriptionProvider()
        : base(TypeDescriptor.GetProvider(typeof(KryptonTableLayoutPanel)))
    {
    }

    /// <inheritdoc />
    public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object? instance)
    {
        ICustomTypeDescriptor? parent = base.GetTypeDescriptor(objectType, instance);
        if (parent == null)
        {
            throw new InvalidOperationException("The table layout type descriptor is unavailable.");
        }

        return new KryptonTableLayoutPanelTypeDescriptor(parent);
    }

    private sealed class KryptonTableLayoutPanelTypeDescriptor : CustomTypeDescriptor
    {
        internal KryptonTableLayoutPanelTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
        }

        public override PropertyDescriptorCollection GetProperties() => Wrap(base.GetProperties());

        public override PropertyDescriptorCollection GetProperties(Attribute[]? attributes) =>
            Wrap(base.GetProperties(attributes));

        private static PropertyDescriptorCollection Wrap(PropertyDescriptorCollection properties)
        {
            var wrapped = new PropertyDescriptor[properties.Count];
            for (var index = 0; index < properties.Count; index++)
            {
                PropertyDescriptor property = properties[index];
                wrapped[index] = property.Name is @"ColumnStyles" or @"RowStyles"
                    ? new KryptonTableLayoutStylePropertyDescriptor(property)
                    : property;
            }

            return new PropertyDescriptorCollection(wrapped, readOnly: true);
        }
    }

    private sealed class KryptonTableLayoutStylePropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor _inner;
        private readonly AttributeCollection _attributes;

        internal KryptonTableLayoutStylePropertyDescriptor(PropertyDescriptor inner)
            : base(inner)
        {
            _inner = inner;
            var attributes = new List<Attribute>();
            foreach (Attribute attribute in inner.Attributes)
            {
                if (!(attribute is EditorAttribute))
                {
                    attributes.Add(attribute);
                }
            }

            attributes.Add(new EditorAttribute(typeof(KryptonDesignerTableLayoutStyleEditor), typeof(UITypeEditor)));
            _attributes = new AttributeCollection(attributes.ToArray());
        }

        public override Type ComponentType => _inner.ComponentType;

        public override bool IsReadOnly => _inner.IsReadOnly;

        public override Type PropertyType => _inner.PropertyType;

        /// <summary>
        /// Replaces the stock <see cref="EditorAttribute"/> so the property grid opens this editor.
        /// </summary>
        public override AttributeCollection Attributes => _attributes;

        public override object? GetEditor(Type editorBaseType) =>
            editorBaseType == typeof(UITypeEditor)
                ? KryptonDesignerTableLayoutStyleEditor.Shared
                : _inner.GetEditor(editorBaseType);

        public override bool CanResetValue(object component) => _inner.CanResetValue(component);

        public override object? GetValue(object? component) => _inner.GetValue(component);

        public override void ResetValue(object component) => _inner.ResetValue(component);

        public override void SetValue(object? component, object? value) => _inner.SetValue(component, value);

        public override bool ShouldSerializeValue(object component) => _inner.ShouldSerializeValue(component);
    }
}
