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
/// System Summary: managed runtime facts plus parallel WMI class queries.
/// </summary>
internal static class SystemSummaryProvider
{
    public static SystemInformationTable Collect(CancellationToken cancellationToken)
    {
        var table = SystemInformationTable.ItemValue();

        table.AddRow("OS Name", Environment.OSVersion.VersionString);
        table.AddRow("Version", Environment.OSVersion.Version.ToString());
        table.AddRow("OS Platform", Environment.OSVersion.Platform.ToString());
        table.AddRow("Machine Name", Environment.MachineName);
        table.AddRow("User Name", Environment.UserName);
        table.AddRow("User Domain", Environment.UserDomainName);
        table.AddRow("64-bit OS", Environment.Is64BitOperatingSystem.ToString());
        table.AddRow("64-bit Process", Environment.Is64BitProcess.ToString());
        table.AddRow("Processor Count", Environment.ProcessorCount.ToString(CultureInfo.CurrentCulture));
        table.AddRow("System Directory", Environment.SystemDirectory);
        table.AddRow("CLR Version", Environment.Version.ToString());
        table.AddRow("Framework Description", RuntimeInformation.FrameworkDescription);
        table.AddRow("OS Description", RuntimeInformation.OSDescription);
        table.AddRow("OS Architecture", RuntimeInformation.OSArchitecture.ToString());
        table.AddRow("Process Architecture", RuntimeInformation.ProcessArchitecture.ToString());
        table.AddRow("Culture", CultureInfo.CurrentCulture.DisplayName);
        table.AddRow("UI Culture", CultureInfo.CurrentUICulture.DisplayName);
        table.AddRow("System Page Size", Environment.SystemPageSize.ToString(CultureInfo.CurrentCulture));
        table.AddRow("Working Set", Environment.WorkingSet.ToString(CultureInfo.CurrentCulture));
        table.AddRow("Tick Count", Environment.TickCount.ToString(CultureInfo.CurrentCulture));

        var osProps = new[] { "Caption", "Version", "BuildNumber", "CSDVersion", "OSArchitecture", "InstallDate", "LastBootUpTime", "RegisteredUser", "SerialNumber", "TotalVisibleMemorySize", "FreePhysicalMemory", "TotalVirtualMemorySize", "FreeVirtualMemory" };
        var csProps = new[] { "Manufacturer", "Model", "SystemType", "TotalPhysicalMemory", "NumberOfProcessors", "NumberOfLogicalProcessors", "Domain", "UserName", "HypervisorPresent" };
        var cpuProps = new[] { "Name", "Manufacturer", "Description", "MaxClockSpeed", "NumberOfCores", "NumberOfLogicalProcessors", "AddressWidth" };
        var biosProps = new[] { "Manufacturer", "Name", "Version", "SMBIOSBIOSVersion", "ReleaseDate", "SerialNumber" };
        var tzProps = new[] { "Caption", "StandardName", "Bias" };
        var pfProps = new[] { "Name", "AllocatedBaseSize", "CurrentUsage", "PeakUsage" };

        var osTask = Task.Run(() => SystemInformationWmi.Query("Win32_OperatingSystem", osProps, cancellationToken), cancellationToken);
        var csTask = Task.Run(() => SystemInformationWmi.Query("Win32_ComputerSystem", csProps, cancellationToken), cancellationToken);
        var cpuTask = Task.Run(() => SystemInformationWmi.Query("Win32_Processor", cpuProps, cancellationToken), cancellationToken);
        var biosTask = Task.Run(() => SystemInformationWmi.Query("Win32_BIOS", biosProps, cancellationToken), cancellationToken);
        var tzTask = Task.Run(() => SystemInformationWmi.Query("Win32_TimeZone", tzProps, cancellationToken), cancellationToken);
        var pfTask = Task.Run(() => SystemInformationWmi.Query("Win32_PageFileUsage", pfProps, cancellationToken), cancellationToken);

        Task.WaitAll(new Task[] { osTask, csTask, cpuTask, biosTask, tzTask, pfTask }, cancellationToken);

        MergeFirst(table, "Win32_OperatingSystem", osProps, osTask.Result);
        MergeFirst(table, "Win32_ComputerSystem", csProps, csTask.Result);
        MergeFirst(table, "Win32_Processor", cpuProps, cpuTask.Result);
        MergeFirst(table, "Win32_BIOS", biosProps, biosTask.Result);
        MergeFirst(table, "Win32_TimeZone", tzProps, tzTask.Result);
        MergeFirst(table, "Win32_PageFileUsage", pfProps, pfTask.Result);

        return table;
    }

    private static void MergeFirst(SystemInformationTable table, string wmiClass, string[] properties, SystemInformationWmi.WmiQueryResult result)
    {
        if (!string.IsNullOrEmpty(result.Error))
        {
            table.AddRow(wmiClass, result.Error);
            return;
        }

        if (result.Rows.Count == 0)
        {
            return;
        }

        var first = result.Rows[0];
        for (var i = 0; i < properties.Length && i < first.Length; i++)
        {
            table.AddRow(properties[i], first[i]);
        }
    }
}
