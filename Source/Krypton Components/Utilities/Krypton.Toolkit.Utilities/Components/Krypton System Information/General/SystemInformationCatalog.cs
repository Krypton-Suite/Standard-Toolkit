#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Builds the msinfo32-aligned category tree.
/// </summary>
internal static class SystemInformationCatalog
{
    /// <summary>
    /// Populates <paramref name="tree"/> and returns the node matching <paramref name="initialCategoryId"/>.
    /// </summary>
    public static TreeNode? Populate(KryptonTreeView tree, string? initialCategoryId)
    {
        var strings = KryptonSystemInformationStrings.Current;
        tree.Nodes.Clear();

        var summary = CreateNode(strings.CategorySystemSummary, SystemInformationCategoryId.SystemSummary);

        var hardware = CreateNode(strings.CategoryHardwareResources, SystemInformationCategoryId.HardwareResources);
        hardware.Nodes.Add(CreateNode(strings.CategoryConflicts, SystemInformationCategoryId.HardwareConflicts));
        hardware.Nodes.Add(CreateNode(strings.CategoryDma, SystemInformationCategoryId.HardwareDma));
        hardware.Nodes.Add(CreateNode(strings.CategoryForcedHardware, SystemInformationCategoryId.HardwareForced));
        hardware.Nodes.Add(CreateNode(strings.CategoryIo, SystemInformationCategoryId.HardwareIo));
        hardware.Nodes.Add(CreateNode(strings.CategoryIrq, SystemInformationCategoryId.HardwareIrq));
        hardware.Nodes.Add(CreateNode(strings.CategoryMemory, SystemInformationCategoryId.HardwareMemory));

        var components = CreateNode(strings.CategoryComponents, SystemInformationCategoryId.Components);
        components.Nodes.Add(CreateNode(strings.CategoryMultimedia, SystemInformationCategoryId.ComponentsMultimedia));
        components.Nodes.Add(CreateNode(strings.CategoryDisplay, SystemInformationCategoryId.ComponentsDisplay));
        components.Nodes.Add(CreateNode(strings.CategoryInfrared, SystemInformationCategoryId.ComponentsInfrared));
        components.Nodes.Add(CreateNode(strings.CategoryInput, SystemInformationCategoryId.ComponentsInput));
        components.Nodes.Add(CreateNode(strings.CategoryModem, SystemInformationCategoryId.ComponentsModem));
        components.Nodes.Add(CreateNode(strings.CategoryNetwork, SystemInformationCategoryId.ComponentsNetwork));
        components.Nodes.Add(CreateNode(strings.CategoryNetworkConfiguration, SystemInformationCategoryId.ComponentsNetworkConfiguration));
        components.Nodes.Add(CreateNode(strings.CategoryPorts, SystemInformationCategoryId.ComponentsPorts));
        components.Nodes.Add(CreateNode(strings.CategoryStorage, SystemInformationCategoryId.ComponentsStorage));
        components.Nodes.Add(CreateNode(strings.CategoryPrinting, SystemInformationCategoryId.ComponentsPrinting));
        components.Nodes.Add(CreateNode(strings.CategoryProblemDevices, SystemInformationCategoryId.ComponentsProblemDevices));
        components.Nodes.Add(CreateNode(strings.CategoryUsb, SystemInformationCategoryId.ComponentsUsb));

        var software = CreateNode(strings.CategorySoftwareEnvironment, SystemInformationCategoryId.SoftwareEnvironment);
        software.Nodes.Add(CreateNode(strings.CategoryDrivers, SystemInformationCategoryId.SoftwareDrivers));
        software.Nodes.Add(CreateNode(strings.CategoryEnvironmentVariables, SystemInformationCategoryId.SoftwareEnvironmentVariables));
        software.Nodes.Add(CreateNode(strings.CategoryRunningTasks, SystemInformationCategoryId.SoftwareRunningTasks));
        software.Nodes.Add(CreateNode(strings.CategoryLoadedModules, SystemInformationCategoryId.SoftwareLoadedModules));
        software.Nodes.Add(CreateNode(strings.CategoryServices, SystemInformationCategoryId.SoftwareServices));
        software.Nodes.Add(CreateNode(strings.CategoryProgramGroups, SystemInformationCategoryId.SoftwareProgramGroups));
        software.Nodes.Add(CreateNode(strings.CategoryStartupPrograms, SystemInformationCategoryId.SoftwareStartup));
        software.Nodes.Add(CreateNode(strings.CategoryOleRegistration, SystemInformationCategoryId.SoftwareOle));
        software.Nodes.Add(CreateNode(strings.CategoryWindowsErrorReporting, SystemInformationCategoryId.SoftwareWer));

        tree.Nodes.Add(summary);
        tree.Nodes.Add(hardware);
        tree.Nodes.Add(components);
        tree.Nodes.Add(software);
        hardware.Expand();
        components.Expand();
        software.Expand();

        var targetId = string.IsNullOrEmpty(initialCategoryId)
            ? SystemInformationCategoryId.SystemSummary
            : initialCategoryId ?? SystemInformationCategoryId.SystemSummary;
        return FindNode(tree.Nodes, targetId) ?? summary;
    }

    private static TreeNode CreateNode(string text, string id)
    {
        var node = new TreeNode(text)
        {
            Tag = id,
            Name = id
        };
        return node;
    }

    private static TreeNode? FindNode(TreeNodeCollection nodes, string id)
    {
        foreach (TreeNode node in nodes)
        {
            if (string.Equals(node.Tag as string, id, StringComparison.Ordinal))
            {
                return node;
            }

            var child = FindNode(node.Nodes, id);
            if (child != null)
            {
                return child;
            }
        }

        return null;
    }
}
