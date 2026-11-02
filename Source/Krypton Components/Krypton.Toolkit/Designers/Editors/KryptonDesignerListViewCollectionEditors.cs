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
/// Krypton-themed designer editor for <see cref="ListView.ColumnHeaderCollection"/>.
/// </summary>
public sealed class KryptonDesignerColumnHeaderCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerColumnHeaderCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerColumnHeaderCollectionEditor()
        : base(typeof(ColumnHeader))
    {
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override object? SetItems(object? editValue, object[]? value)
    {
        if (editValue is ListView.ColumnHeaderCollection columns)
        {
            columns.Clear();
            if (value is not null)
            {
                var headers = new ColumnHeader[value.Length];
                Array.Copy(value, 0, headers, 0, value.Length);
                columns.AddRange(headers);
            }
        }

        return editValue;
    }

    /// <inheritdoc />
    internal override void OnDesignerItemRemoving(object? item)
    {
        if (Context?.Instance is not ListView listView || item is not ColumnHeader column)
        {
            return;
        }

        if (Context.GetService(typeof(IComponentChangeService)) is IComponentChangeService changeService)
        {
            var property = TypeDescriptor.GetProperties(Context.Instance)["Columns"];
            changeService.OnComponentChanging(Context.Instance, property);
            listView.Columns.Remove(column);
            changeService.OnComponentChanged(Context.Instance, property, null, null);
            return;
        }

        listView.Columns.Remove(column);
    }
    #endregion
}

/// <summary>
/// Krypton-themed designer editor for <see cref="ListView.ListViewItemCollection"/>.
/// </summary>
public sealed class KryptonDesignerListViewItemCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerListViewItemCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerListViewItemCollectionEditor()
        : base(typeof(ListViewItem))
    {
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override string GetDisplayText(object? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        var property = TypeDescriptor.GetDefaultProperty(CollectionType);
        if (property?.PropertyType == typeof(string))
        {
            var text = (string?)property.GetValue(value);
            if (!string.IsNullOrEmpty(text))
            {
                return text!;
            }
        }

        var display = TypeDescriptor.GetConverter(value).ConvertToString(value);
        return string.IsNullOrEmpty(display) ? value.GetType().Name : display;
    }
    #endregion
}

/// <summary>
/// Krypton-themed designer editor for <see cref="ListViewGroupCollection"/>.
/// </summary>
public sealed class KryptonDesignerListViewGroupCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    #region Instance Fields
    private object? _editValue;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerListViewGroupCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerListViewGroupCollectionEditor()
        : base(typeof(ListViewGroup))
    {
    }
    #endregion

    #region Public
    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider? provider, object? value)
    {
        _editValue = value;
        value = base.EditValue(context, provider, value);
        _editValue = null;
        return value;
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override object CreateInstance(Type itemType)
    {
        var group = (ListViewGroup)base.CreateInstance(itemType);
        if (_editValue is ListViewGroupCollection collection)
        {
            group.Name = CreateListViewGroupName(collection);
        }

        return group;
    }
    #endregion

    #region Implementation
    private static string CreateListViewGroupName(ListViewGroupCollection collection)
    {
        var baseName = nameof(ListViewGroup);
        var index = 1;
        var result = $"{baseName}{index}";
        while (collection[result] is not null)
        {
            index++;
            result = $"{baseName}{index}";
        }

        return result;
    }
    #endregion
}
