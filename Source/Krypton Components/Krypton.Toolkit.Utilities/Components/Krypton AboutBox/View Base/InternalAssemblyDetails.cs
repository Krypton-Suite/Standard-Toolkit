#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal partial class InternalAssemblyDetails : UserControl
{
    public InternalAssemblyDetails()
    {
        InitializeComponent();
        EnsureColumns();
        KryptonAboutBoxUtilities.ConfigureReadOnlyGrid(kdgvAssemblyDetails);
        kcbAssembly.SelectedIndexChanged += kcbAssembly_SelectedIndexChanged;
    }

    public void LoadAssemblies(Assembly? preferred)
    {
        kcbAssembly.Items.Clear();

        AssemblyItem? preferredItem = null;
        foreach (Assembly assembly in KryptonAboutBoxUtilities.GetLoadedAssemblies())
        {
            var item = new AssemblyItem(assembly);
            kcbAssembly.Items.Add(item);
            if (preferred != null && ReferenceEquals(assembly, preferred))
            {
                preferredItem = item;
            }
        }

        if (kcbAssembly.Items.Count == 0)
        {
            return;
        }

        if (preferredItem != null)
        {
            kcbAssembly.SelectedItem = preferredItem;
        }
        else
        {
            kcbAssembly.SelectedIndex = 0;
        }
    }

    private void EnsureColumns()
    {
        if (kdgvAssemblyDetails.Columns.Count > 0)
        {
            return;
        }

        kdgvAssemblyDetails.AutoGenerateColumns = false;
        kdgvAssemblyDetails.Columns.Add("Key", "Key");
        kdgvAssemblyDetails.Columns.Add("Value", "Value");
        kdgvAssemblyDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        kdgvAssemblyDetails.AllowUserToResizeRows = false;
        kdgvAssemblyDetails.RowHeadersVisible = false;
    }

    private void kcbAssembly_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcbAssembly.SelectedItem is AssemblyItem item)
        {
            KryptonAboutBoxUtilities.PopulateAssemblyDetails(item.Assembly, kdgvAssemblyDetails);
        }
    }

    private sealed class AssemblyItem
    {
        public AssemblyItem(Assembly assembly) => Assembly = assembly;

        public Assembly Assembly { get; }

        public override string ToString()
        {
            AssemblyName name = Assembly.GetName();
            string display = name.Name ?? Assembly.FullName ?? Assembly.ToString();
            return name.Version == null ? display : $"{display} ({name.Version})";
        }
    }
}
