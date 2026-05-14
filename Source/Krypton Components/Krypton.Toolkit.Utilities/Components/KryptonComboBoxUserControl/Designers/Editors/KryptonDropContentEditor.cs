#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Drop-down <see cref="UITypeEditor"/> used by <see cref="KryptonComboBoxUserControl.DropContent"/>
/// to let designers (a) pick an existing <see cref="UserControl"/> / <see cref="Control"/> from
/// the same form, or (b) instantiate a brand-new <see cref="UserControl"/>-derived type
/// discovered via <see cref="ITypeDiscoveryService"/>. Newly-created components are sited on
/// the designer host so they participate in code-gen / serialization.
/// </summary>
internal class KryptonDropContentEditor : UITypeEditor
{
    #region Constants

    private const string NoneEntry = "(none)";
    private const string ExistingHeader = "─── Existing controls ───";
    private const string NewHeader = "─── New instance ───";

    #endregion

    #region UITypeEditor overrides

    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        UITypeEditorEditStyle.DropDown;

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider == null || context == null)
        {
            return value;
        }

        if (provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService
            || provider.GetService(typeof(IDesignerHost)) is not IDesignerHost host)
        {
            return value;
        }

        Control? owner = context.Instance as Control;

        var listBox = new ListBox
        {
            BorderStyle = BorderStyle.None,
            IntegralHeight = false
        };

        // 1) "(none)" entry
        listBox.Items.Add(NoneEntry);

        // 2) Existing controls section
        var existing = new List<Control>();
        foreach (IComponent component in host.Container.Components)
        {
            if (component is Control candidate
                && !ReferenceEquals(candidate, owner)
                && IsValidDropContent(candidate))
            {
                existing.Add(candidate);
            }
        }
        existing.Sort((a, b) => string.Compare(GetDisplayName(a), GetDisplayName(b), StringComparison.OrdinalIgnoreCase));

        if (existing.Count > 0)
        {
            listBox.Items.Add(ExistingHeader);
            foreach (Control c in existing)
            {
                listBox.Items.Add(c);
            }
        }

        // 3) "New instance" section: UserControl-derived types
        Type[] discovered = DiscoverUserControlTypes(provider);
        if (discovered.Length > 0)
        {
            listBox.Items.Add(NewHeader);
            foreach (Type t in discovered)
            {
                listBox.Items.Add(t);
            }
        }

        // Preselect the current value if it is one of the existing controls
        if (value is Control current)
        {
            int idx = listBox.Items.IndexOf(current);
            if (idx >= 0)
            {
                listBox.SelectedIndex = idx;
            }
        }
        else
        {
            listBox.SelectedIndex = 0;
        }

        // Format items for display
        listBox.Format += (_, e) =>
        {
            switch (e.ListItem)
            {
                case string s:
                    e.Value = s;
                    break;
                case Type t:
                    e.Value = $"[New] {t.Name} ({t.Namespace ?? "<global>"})";
                    break;
                case Component c when c.Site != null && !string.IsNullOrEmpty(c.Site.Name):
                    e.Value = $"{c.Site.Name} ({c.GetType().Name})";
                    break;
                default:
                    e.Value = e.ListItem?.GetType().Name ?? string.Empty;
                    break;
            }
        };

        // Auto-size the height to fit (cap at 12 visible rows)
        int itemHeight = listBox.ItemHeight > 0 ? listBox.ItemHeight : 16;
        int rows = Math.Min(Math.Max(listBox.Items.Count, 1), 12);
        listBox.Height = (rows * itemHeight) + 4;
        listBox.Width = 320;

        bool selectionCommitted = false;

        listBox.MouseUp += (_, _) =>
        {
            if (CanCommitItem(listBox.SelectedItem))
            {
                selectionCommitted = true;
                editorService.CloseDropDown();
            }
        };
        listBox.KeyDown += (_, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (CanCommitItem(listBox.SelectedItem))
                    {
                        selectionCommitted = true;
                        editorService.CloseDropDown();
                    }
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    editorService.CloseDropDown();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    listBox.SelectedIndex = NextSelectableIndex(listBox, listBox.SelectedIndex, +1);
                    e.Handled = true;
                    break;
                case Keys.Up:
                    listBox.SelectedIndex = NextSelectableIndex(listBox, listBox.SelectedIndex, -1);
                    e.Handled = true;
                    break;
            }
        };

        editorService.DropDownControl(listBox);

        if (!selectionCommitted)
        {
            return value;
        }

        return ResolveCommittedValue(listBox.SelectedItem, host, value);
    }

    #endregion

    #region Implementation

    /// <summary>Decide whether <paramref name="item"/> can be selected as a final value.</summary>
    private static bool CanCommitItem(object? item) =>
        item is null
        || (item is string s && s == NoneEntry)
        || item is Control
        || item is Type;

    /// <summary>Resolve the picked item to a <see cref="Control"/> instance (or <see langword="null"/>).</summary>
    private static object? ResolveCommittedValue(object? selected, IDesignerHost host, object? fallback)
    {
        switch (selected)
        {
            case null:
                return fallback;
            case string s when s == NoneEntry:
                return null;
            case Control c:
                return c;
            case Type t:
                // CreateComponent sites the new component on the host so it appears in
                // Designer.cs code-gen and the property grid's component picker. The new
                // control is not auto-added to any visual parent &#8211; that is intentional,
                // since drop-down content typically should not also live on the form.
                IComponent component = host.CreateComponent(t);
                return component as Control;
            default:
                return fallback;
        }
    }

    /// <summary>Skip header rows when navigating with the keyboard.</summary>
    private static int NextSelectableIndex(ListBox listBox, int current, int step)
    {
        int count = listBox.Items.Count;
        if (count == 0)
        {
            return -1;
        }

        int idx = current;
        for (int i = 0; i < count; i++)
        {
            idx += step;
            if (idx < 0)
            {
                idx = count - 1;
            }
            else if (idx >= count)
            {
                idx = 0;
            }

            if (CanCommitItem(listBox.Items[idx]))
            {
                return idx;
            }
        }

        return current;
    }

    /// <summary>Use the component's site name where available for sorting.</summary>
    private static string GetDisplayName(Component c) =>
        c.Site != null && !string.IsNullOrEmpty(c.Site.Name) ? c.Site.Name : c.GetType().Name;

    /// <summary>
    /// Returns whether <paramref name="candidate"/> is suitable for hosting in the popup.
    /// </summary>
    private static bool IsValidDropContent(Control candidate)
    {
        if (candidate is Form)
        {
            return false;
        }

        if (candidate is KryptonTextBox)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Enumerate <see cref="UserControl"/>-derived types that have a public parameterless
    /// constructor and are not abstract, sorted by namespace then by simple name.
    /// </summary>
    private static Type[] DiscoverUserControlTypes(IServiceProvider provider)
    {
        if (provider.GetService(typeof(ITypeDiscoveryService)) is not ITypeDiscoveryService discovery)
        {
            return Array.Empty<Type>();
        }

        ICollection raw;
        try
        {
            // excludeGlobalTypes = true => limit to project + directly referenced assemblies,
            // which excludes framework/system types.
            raw = discovery.GetTypes(typeof(UserControl), excludeGlobalTypes: true);
        }
        catch
        {
            return Array.Empty<Type>();
        }

        var results = new List<Type>();
        foreach (object o in raw)
        {
            if (o is Type t && IsCreatableUserControlType(t))
            {
                results.Add(t);
            }
        }

        results.Sort((a, b) =>
        {
            int ns = string.Compare(a.Namespace ?? string.Empty, b.Namespace ?? string.Empty, StringComparison.OrdinalIgnoreCase);
            return ns != 0 ? ns : string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
        });

        return results.ToArray();
    }

    /// <summary>
    /// Filters out abstract types, generic-type-definitions, types without a public
    /// parameterless constructor, and the <see cref="UserControl"/> base type itself.
    /// </summary>
    private static bool IsCreatableUserControlType(Type t)
    {
        if (t == typeof(UserControl))
        {
            return false;
        }

        if (!typeof(UserControl).IsAssignableFrom(t))
        {
            return false;
        }

        if (t.IsAbstract || t.IsGenericTypeDefinition || !t.IsClass)
        {
            return false;
        }

        // Public, parameterless constructor required for designer instantiation
        return t.GetConstructor(Type.EmptyTypes) != null;
    }

    #endregion
}
