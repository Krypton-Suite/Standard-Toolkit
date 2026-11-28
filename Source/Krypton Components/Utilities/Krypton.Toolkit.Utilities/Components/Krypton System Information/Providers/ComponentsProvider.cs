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
/// Components branch (display, storage, network, USB, printers, problem devices).
/// </summary>
internal static class ComponentsProvider
{
    public static SystemInformationTable Collect(string categoryId, CancellationToken cancellationToken)
    {
        switch (categoryId)
        {
            case SystemInformationCategoryId.ComponentsMultimedia:
            {
                var table = new SystemInformationTable("Name", "Manufacturer", "Status", "PNPDeviceID");
                SystemInformationWmi.AddInstances(table, "Win32_SoundDevice",
                    new[] { "Name", "Manufacturer", "Status", "PNPDeviceID" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.ComponentsDisplay:
            {
                var table = new SystemInformationTable("Name", "AdapterRAM", "DriverVersion", "VideoProcessor", "CurrentRefreshRate", "PNPDeviceID");
                SystemInformationWmi.AddInstances(table, "Win32_VideoController",
                    new[] { "Name", "AdapterRAM", "DriverVersion", "VideoProcessor", "CurrentRefreshRate", "PNPDeviceID" },
                    cancellationToken);
                var monitors = new SystemInformationTable("Name", "ScreenWidth", "ScreenHeight", "PNPDeviceID");
                SystemInformationWmi.AddInstances(monitors, "Win32_DesktopMonitor",
                    new[] { "Name", "ScreenWidth", "ScreenHeight", "PNPDeviceID" }, cancellationToken);
                foreach (var row in monitors.Rows)
                {
                    table.AddRow(row);
                }

                return table;
            }
            case SystemInformationCategoryId.ComponentsInfrared:
            {
                var table = new SystemInformationTable("Name", "Status", "PNPDeviceID", "Description");
                SystemInformationWmi.AddInstances(table, "Win32_InfraredDevice",
                    new[] { "Name", "Status", "PNPDeviceID", "Description" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.ComponentsInput:
            {
                var table = new SystemInformationTable("Name", "Description", "PNPDeviceID", "Status");
                SystemInformationWmi.AddInstances(table, "Win32_Keyboard",
                    new[] { "Name", "Description", "PNPDeviceID", "Status" }, cancellationToken);
                SystemInformationWmi.AddInstances(table, "Win32_PointingDevice",
                    new[] { "Name", "Description", "PNPDeviceID", "Status" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.ComponentsModem:
            {
                var table = new SystemInformationTable("Name", "AttachedTo", "Status", "PNPDeviceID");
                SystemInformationWmi.AddInstances(table, "Win32_POTSModem",
                    new[] { "Name", "AttachedTo", "Status", "PNPDeviceID" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.ComponentsNetwork:
            {
                var table = new SystemInformationTable("Name", "MACAddress", "AdapterType", "NetConnectionStatus", "Speed", "PNPDeviceID");
                SystemInformationWmi.AddInstances(table, "Win32_NetworkAdapter",
                    new[] { "Name", "MACAddress", "AdapterType", "NetConnectionStatus", "Speed", "PNPDeviceID" },
                    cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.ComponentsNetworkConfiguration:
            {
                var table = new SystemInformationTable("Description", "IPAddress", "DHCPEnabled", "DHCPServer", "MACAddress", "DefaultIPGateway");
                SystemInformationWmi.AddInstances(table, "Win32_NetworkAdapterConfiguration",
                    new[] { "Description", "IPAddress", "DHCPEnabled", "DHCPServer", "MACAddress", "DefaultIPGateway" },
                    cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.ComponentsPorts:
            {
                var table = new SystemInformationTable("DeviceID", "Name", "Status", "MaxBaudRate");
                SystemInformationWmi.AddInstances(table, "Win32_SerialPort",
                    new[] { "DeviceID", "Name", "Status", "MaxBaudRate" }, cancellationToken);
                SystemInformationWmi.AddInstances(table, "Win32_ParallelPort",
                    new[] { "DeviceID", "Name", "Status", "MaxBaudRate" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.ComponentsStorage:
                return CollectStorage(cancellationToken);
            case SystemInformationCategoryId.ComponentsPrinting:
            {
                var table = new SystemInformationTable("Name", "DriverName", "PortName", "Default", "WorkOffline");
                SystemInformationWmi.AddInstances(table, "Win32_Printer",
                    new[] { "Name", "DriverName", "PortName", "Default", "WorkOffline" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.ComponentsProblemDevices:
            {
                var table = new SystemInformationTable("Name", "ConfigManagerErrorCode", "Status", "PNPDeviceID");
                SystemInformationWmi.AddInstances(table, "Win32_PnPEntity",
                    new[] { "Name", "ConfigManagerErrorCode", "Status", "PNPDeviceID" },
                    cancellationToken, SystemInformationWmi.PnpProblemCondition);
                return table;
            }
            case SystemInformationCategoryId.ComponentsUsb:
            {
                var table = new SystemInformationTable("Name", "Status", "PNPDeviceID", "Description");
                SystemInformationWmi.AddInstances(table, "Win32_USBController",
                    new[] { "Name", "Status", "PNPDeviceID", "Description" }, cancellationToken);
                SystemInformationWmi.AddInstances(table, "Win32_USBHub",
                    new[] { "Name", "Status", "PNPDeviceID", "Description" }, cancellationToken);
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

    private static SystemInformationTable CollectStorage(CancellationToken cancellationToken)
    {
        var table = new SystemInformationTable("Name", "Model", "InterfaceType", "Size", "Status", "MediaType");
        SystemInformationWmi.AddInstances(table, "Win32_DiskDrive",
            new[] { "Name", "Model", "InterfaceType", "Size", "Status", "MediaType" }, cancellationToken);

        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                cancellationToken.ThrowIfCancellationRequested();
                var size = drive.IsReady ? drive.TotalSize.ToString(CultureInfo.CurrentCulture) : string.Empty;
                table.AddRow(drive.Name, drive.DriveType.ToString(), drive.DriveFormat, size, drive.IsReady.ToString(), "Logical");
            }
        }
        catch (Exception ex)
        {
            table.AddRow(ex.Message);
        }

        return table;
    }
}
