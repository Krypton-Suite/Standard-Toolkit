#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, KamaniAR, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Custom collection editor that creates DataGridViewColumn components in the root designer container,
/// avoiding dotted Site names when the grid is wrapped inside a composite control.
/// </summary>
public class KryptonDataGridViewColumnCollectionEditor : CollectionEditor
{
    internal static Type[] _columnTypes;

    public KryptonDataGridViewColumnCollectionEditor()
        : base(typeof(DataGridViewColumnCollection))
    {
        _columnTypes = [];
    }

    protected override Type CreateCollectionItemType()
    {
        return typeof(DataGridViewTextBoxColumn);
    }

    protected override Type[] CreateNewItemTypes()
    {
        if (_columnTypes.Length == 0)
        {
            List<Type> types = [];

            var discovery = this.GetService(typeof(ITypeDiscoveryService)) as ITypeDiscoveryService;
            if (discovery is not null)
            {
                foreach (Type t in discovery.GetTypes(typeof(DataGridViewColumn), false))
                {
                    if (t.IsPublic
                        && !t.IsAbstract
                        && typeof(DataGridViewColumn).IsAssignableFrom(t))
                    {
                        types.Add(t);
                    }
                }
            }
            else
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    types.AddRange(assembly.GetTypes().Where(t => t.IsPublic && !t.IsAbstract && typeof(DataGridViewColumn).IsAssignableFrom(t)));
                }
            }

            _columnTypes = types.ToArray();
        }

        return _columnTypes;
    }

    protected override object CreateInstance(Type itemType)
    {
        IDesignerHost? host = GetService(typeof(IDesignerHost)) as IDesignerHost;
        DataGridViewColumn? column = null;

        if (host != null)
        {
            object? created = host.CreateComponent(itemType);
            column = created as DataGridViewColumn;
        }

        column ??= (DataGridViewColumn)Activator.CreateInstance(itemType)!;

        // Use the Site-assigned name (without any dotted owner prefix) as the runtime Name/HeaderText
        if (column.Site != null && !string.IsNullOrEmpty(column.Site.Name))
        {
            string siteName = column.Site.Name;
            int dotIndex = siteName.LastIndexOf('.');
            string baseName = dotIndex >= 0 ? siteName.Substring(dotIndex + 1) : siteName;

            if (string.IsNullOrEmpty(column.Name))
            {
                column.Name = baseName;
            }
            if (string.IsNullOrEmpty(column.HeaderText))
            {
                column.HeaderText = baseName;
            }
        }
        else
        {
            if (string.IsNullOrEmpty(column.Name))
            {
                column.Name = itemType.Name;
            }
            if (string.IsNullOrEmpty(column.HeaderText))
            {
                column.HeaderText = column.Name;
            }
        }

        return column;
    }

    protected override object[] GetItems(object? editValue)
    {
        DataGridViewColumnCollection? collection = editValue as DataGridViewColumnCollection;
        if (collection == null)
        {
            return base.GetItems(editValue)!;
        }

        DataGridViewColumn[] items = new DataGridViewColumn[collection.Count];
        for (int i = 0; i < collection.Count; i++)
        {
            items[i] = collection[i];
        }
        return items;
    }

    protected override object SetItems(object? editValue, object[]? value)
    {
        DataGridViewColumnCollection? collection = editValue as DataGridViewColumnCollection;
        if (collection == null)
        {
            return base.SetItems(editValue, value)!;
        }

        IDesignerHost? host = GetService(typeof(IDesignerHost)) as IDesignerHost;
        IComponentChangeService? changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;

        // Try to get the owning DataGridView for proper serialization notifications
        DataGridView? ownerGrid = null;
        if (collection.Count > 0)
        {
            ownerGrid = collection[0].DataGridView;
        }
        if (ownerGrid == null)
        {
            var pi = typeof(DataGridViewColumnCollection).GetProperty("DataGridView", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            ownerGrid = (DataGridView?)pi?.GetValue(collection);
        }

        var colsProp = ownerGrid != null ? TypeDescriptor.GetProperties(ownerGrid)["Columns"] : null;

        using (var transaction = host?.CreateTransaction("Edit Columns"))
        {
            if (ownerGrid != null && colsProp != null)
            {
                changeService?.OnComponentChanging(ownerGrid, colsProp);
            }
            else
            {
                changeService?.OnComponentChanging(collection, null);
            }

            collection.Clear();
            var itemsArray = value ?? Array.Empty<object>();
            for (int i = 0; i < itemsArray.Length; i++)
            {
                DataGridViewColumn? column = itemsArray[i] as DataGridViewColumn;
                if (column != null)
                {
                    collection.Add(column);
                }
            }

            if (ownerGrid != null && colsProp != null)
            {
                changeService?.OnComponentChanged(ownerGrid, colsProp, null, null);
                TypeDescriptor.Refresh(ownerGrid);
            }
            else
            {
                changeService?.OnComponentChanged(collection, null, null, null);
            }

            transaction?.Commit();
        }

        return collection;
    }
}