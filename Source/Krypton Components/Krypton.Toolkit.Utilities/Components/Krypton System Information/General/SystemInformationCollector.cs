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
/// Collects a System Information category off the UI thread.
/// </summary>
internal static class SystemInformationCollector
{
    /// <summary>
    /// Collects the table for <paramref name="categoryId"/>. Folder nodes return a prompt row.
    /// </summary>
    public static SystemInformationTable Collect(string categoryId, CancellationToken cancellationToken, bool allProcessModules = false)
    {
        switch (categoryId)
        {
            case SystemInformationCategoryId.SystemSummary:
                return SystemSummaryProvider.Collect(cancellationToken);
            case SystemInformationCategoryId.HardwareConflicts:
            case SystemInformationCategoryId.HardwareDma:
            case SystemInformationCategoryId.HardwareForced:
            case SystemInformationCategoryId.HardwareIo:
            case SystemInformationCategoryId.HardwareIrq:
            case SystemInformationCategoryId.HardwareMemory:
                return HardwareResourcesProvider.Collect(categoryId, cancellationToken);
            case SystemInformationCategoryId.ComponentsMultimedia:
            case SystemInformationCategoryId.ComponentsDisplay:
            case SystemInformationCategoryId.ComponentsInfrared:
            case SystemInformationCategoryId.ComponentsInput:
            case SystemInformationCategoryId.ComponentsModem:
            case SystemInformationCategoryId.ComponentsNetwork:
            case SystemInformationCategoryId.ComponentsNetworkConfiguration:
            case SystemInformationCategoryId.ComponentsPorts:
            case SystemInformationCategoryId.ComponentsStorage:
            case SystemInformationCategoryId.ComponentsPrinting:
            case SystemInformationCategoryId.ComponentsProblemDevices:
            case SystemInformationCategoryId.ComponentsUsb:
                return ComponentsProvider.Collect(categoryId, cancellationToken);
            case SystemInformationCategoryId.SoftwareDrivers:
            case SystemInformationCategoryId.SoftwareEnvironmentVariables:
            case SystemInformationCategoryId.SoftwareRunningTasks:
            case SystemInformationCategoryId.SoftwareLoadedModules:
            case SystemInformationCategoryId.SoftwareServices:
            case SystemInformationCategoryId.SoftwareProgramGroups:
            case SystemInformationCategoryId.SoftwareStartup:
            case SystemInformationCategoryId.SoftwareOle:
            case SystemInformationCategoryId.SoftwareWer:
                return SoftwareEnvironmentProvider.Collect(categoryId, cancellationToken, allProcessModules);
            default:
            {
                var table = SystemInformationTable.ItemValue();
                table.AddRow(KryptonSystemInformationStrings.Current.SelectCategory, string.Empty);
                return table;
            }
        }
    }

    /// <summary>Leaf category identifiers that can be exported.</summary>
    public static IReadOnlyList<string> LeafCategoryIds { get; } = new[]
    {
        SystemInformationCategoryId.SystemSummary,
        SystemInformationCategoryId.HardwareConflicts,
        SystemInformationCategoryId.HardwareDma,
        SystemInformationCategoryId.HardwareForced,
        SystemInformationCategoryId.HardwareIo,
        SystemInformationCategoryId.HardwareIrq,
        SystemInformationCategoryId.HardwareMemory,
        SystemInformationCategoryId.ComponentsMultimedia,
        SystemInformationCategoryId.ComponentsDisplay,
        SystemInformationCategoryId.ComponentsInfrared,
        SystemInformationCategoryId.ComponentsInput,
        SystemInformationCategoryId.ComponentsModem,
        SystemInformationCategoryId.ComponentsNetwork,
        SystemInformationCategoryId.ComponentsNetworkConfiguration,
        SystemInformationCategoryId.ComponentsPorts,
        SystemInformationCategoryId.ComponentsStorage,
        SystemInformationCategoryId.ComponentsPrinting,
        SystemInformationCategoryId.ComponentsProblemDevices,
        SystemInformationCategoryId.ComponentsUsb,
        SystemInformationCategoryId.SoftwareDrivers,
        SystemInformationCategoryId.SoftwareEnvironmentVariables,
        SystemInformationCategoryId.SoftwareRunningTasks,
        SystemInformationCategoryId.SoftwareLoadedModules,
        SystemInformationCategoryId.SoftwareServices,
        SystemInformationCategoryId.SoftwareProgramGroups,
        SystemInformationCategoryId.SoftwareStartup,
        SystemInformationCategoryId.SoftwareOle,
        SystemInformationCategoryId.SoftwareWer
    };
}
