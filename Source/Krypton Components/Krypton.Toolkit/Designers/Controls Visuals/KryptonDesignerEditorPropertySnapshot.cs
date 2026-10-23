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
/// Captures and restores designer object property state for collection editor Cancel handling.
/// </summary>
internal static class KryptonDesignerEditorPropertySnapshot
{
    private const int MaxSnapshotDepth = 8;

    /// <summary>
    /// Captures property values for each item in the supplied sequence.
    /// </summary>
    /// <param name="items">Designer objects present at the start of an edit session.</param>
    /// <returns>Captured property snapshots keyed by item reference.</returns>
    internal static List<SnapshotEntry> Capture(IEnumerable<object> items)
    {
        var snapshots = new List<SnapshotEntry>();
        foreach (var item in items)
        {
            snapshots.Add(new SnapshotEntry(item, CaptureNode(item)));
        }

        return snapshots;
    }

    /// <summary>
    /// Restores property values captured at the start of an edit session.
    /// </summary>
    /// <param name="snapshots">Previously captured snapshots.</param>
    internal static void Restore(IEnumerable<SnapshotEntry>? snapshots)
    {
        if (snapshots is null)
        {
            return;
        }

        foreach (var snapshot in snapshots)
        {
            RestoreNode(snapshot.Item, snapshot.Properties);
        }
    }

    private static PropertySnapshotNode CaptureNode(object item)
    {
        var visited = new HashSet<object>(ReferenceComparer.Instance) { item };
        var root = new PropertySnapshotNode();
        CaptureNode(item, root, visited, 0);
        return root;
    }

    private static void CaptureNode(object item, PropertySnapshotNode node, HashSet<object> visited, int depth)
    {
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(item))
        {
            var visibility = GetSerializationVisibility(property);
            if (visibility == DesignerSerializationVisibility.Hidden)
            {
                continue;
            }

            var value = property.GetValue(item);
            if (!property.IsReadOnly)
            {
                node.Values[property.Name] = value;
            }

            if (visibility == DesignerSerializationVisibility.Content
                && value is not null
                && !property.PropertyType.IsValueType
                && value is not string
                && depth < MaxSnapshotDepth
                && visited.Add(value))
            {
                var child = new PropertySnapshotNode();
                CaptureNode(value, child, visited, depth + 1);
                if (child.Values.Count > 0 || child.Children.Count > 0)
                {
                    node.Children[property.Name] = child;
                }
            }
        }
    }

    private static void RestoreNode(object item, PropertySnapshotNode node)
    {
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(item))
        {
            if (!property.IsReadOnly
                && node.Values.TryGetValue(property.Name, out var value)
                && !Equals(property.GetValue(item), value))
            {
                property.SetValue(item, value);
            }

            if (node.Children.TryGetValue(property.Name, out var child)
                && property.GetValue(item) is { } nested)
            {
                RestoreNode(nested, child);
            }
        }
    }

    private static DesignerSerializationVisibility GetSerializationVisibility(PropertyDescriptor property) =>
        property.Attributes[typeof(DesignerSerializationVisibilityAttribute)] is DesignerSerializationVisibilityAttribute attribute
            ? attribute.Visibility
            : DesignerSerializationVisibility.Visible;

    internal sealed class SnapshotEntry
    {
        internal SnapshotEntry(object item, PropertySnapshotNode properties)
        {
            Item = item;
            Properties = properties;
        }

        internal object Item { get; }

        internal PropertySnapshotNode Properties { get; }
    }

    internal sealed class PropertySnapshotNode
    {
        internal Dictionary<string, object?> Values { get; } = new Dictionary<string, object?>(StringComparer.Ordinal);

        internal Dictionary<string, PropertySnapshotNode> Children { get; } = new Dictionary<string, PropertySnapshotNode>(StringComparer.Ordinal);
    }

    private sealed class ReferenceComparer : IEqualityComparer<object>
    {
        internal static readonly ReferenceComparer Instance = new ReferenceComparer();

        bool IEqualityComparer<object>.Equals(object? x, object? y) => ReferenceEquals(x, y);

        int IEqualityComparer<object>.GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
    }
}
