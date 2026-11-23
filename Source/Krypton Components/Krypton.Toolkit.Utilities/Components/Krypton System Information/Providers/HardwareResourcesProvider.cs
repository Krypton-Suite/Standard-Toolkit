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
/// Hardware Resources branch (IRQ, DMA, I/O, memory, conflicts, forced hardware).
/// </summary>
internal static class HardwareResourcesProvider
{
    public static SystemInformationTable Collect(string categoryId, CancellationToken cancellationToken)
    {
        switch (categoryId)
        {
            case SystemInformationCategoryId.HardwareConflicts:
            {
                var table = new SystemInformationTable("Name", "ConfigManagerErrorCode", "Status", "PNPDeviceID");
                SystemInformationWmi.AddInstances(table, "Win32_PnPEntity",
                    new[] { "Name", "ConfigManagerErrorCode", "Status", "PNPDeviceID" },
                    cancellationToken, SystemInformationWmi.PnpProblemCondition);
                return table;
            }
            case SystemInformationCategoryId.HardwareDma:
            {
                var table = new SystemInformationTable("DMAChannel", "Name", "Availability");
                SystemInformationWmi.AddInstances(table, "Win32_DMAChannel",
                    new[] { "DMAChannel", "Name", "Availability" }, cancellationToken,
                    rowLimit: SystemInformationWmi.HardwareResourceRowLimit);
                return table;
            }
            case SystemInformationCategoryId.HardwareForced:
            {
                var table = new SystemInformationTable("Name", "ConfigManagerUserConfig", "PNPDeviceID", "Status");
                SystemInformationWmi.AddInstances(table, "Win32_PnPEntity",
                    new[] { "Name", "ConfigManagerUserConfig", "PNPDeviceID", "Status" },
                    cancellationToken, "ConfigManagerUserConfig = TRUE");
                return table;
            }
            case SystemInformationCategoryId.HardwareIo:
            {
                var table = new SystemInformationTable("StartingAddress", "EndingAddress", "Name", "Caption");
                SystemInformationWmi.AddInstances(table, "Win32_PortResource",
                    new[] { "StartingAddress", "EndingAddress", "Name", "Caption" }, cancellationToken,
                    rowLimit: SystemInformationWmi.HardwareResourceRowLimit);
                return table;
            }
            case SystemInformationCategoryId.HardwareIrq:
            {
                var table = new SystemInformationTable("IRQNumber", "Name", "Availability", "TriggerLevel");
                SystemInformationWmi.AddInstances(table, "Win32_IRQResource",
                    new[] { "IRQNumber", "Name", "Availability", "TriggerLevel" }, cancellationToken,
                    rowLimit: SystemInformationWmi.HardwareResourceRowLimit);
                return table;
            }
            case SystemInformationCategoryId.HardwareMemory:
            {
                var table = new SystemInformationTable("StartingAddress", "EndingAddress", "Name", "Caption");
                SystemInformationWmi.AddInstances(table, "Win32_DeviceMemoryAddress",
                    new[] { "StartingAddress", "EndingAddress", "Name", "Caption" }, cancellationToken,
                    rowLimit: SystemInformationWmi.HardwareResourceRowLimit);
                return table;
            }
            default:
            {
                var table = SystemInformationTable.ItemValue();
                table.AddRow(KryptonSystemInformationStrings.Current.SelectCategory, string.Empty);
                return table;
            }
        }
    }
}
