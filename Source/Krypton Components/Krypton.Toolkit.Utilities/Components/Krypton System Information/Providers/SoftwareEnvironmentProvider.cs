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
/// Software Environment branch (drivers, services, startup, OLE, WER, modules).
/// </summary>
internal static class SoftwareEnvironmentProvider
{
    public static SystemInformationTable Collect(string categoryId, CancellationToken cancellationToken, bool allProcessModules = false)
    {
        switch (categoryId)
        {
            case SystemInformationCategoryId.SoftwareDrivers:
            {
                var table = new SystemInformationTable("Name", "DisplayName", "State", "StartMode", "PathName");
                SystemInformationWmi.AddInstances(table, "Win32_SystemDriver",
                    new[] { "Name", "DisplayName", "State", "StartMode", "PathName" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.SoftwareEnvironmentVariables:
                return CollectEnvironmentVariables();
            case SystemInformationCategoryId.SoftwareRunningTasks:
                return CollectRunningTasks(cancellationToken);
            case SystemInformationCategoryId.SoftwareLoadedModules:
                return CollectLoadedModules(cancellationToken, allProcessModules);
            case SystemInformationCategoryId.SoftwareServices:
            {
                var table = new SystemInformationTable("Name", "DisplayName", "State", "StartMode", "StartName");
                SystemInformationWmi.AddInstances(table, "Win32_Service",
                    new[] { "Name", "DisplayName", "State", "StartMode", "StartName" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.SoftwareProgramGroups:
            {
                var table = new SystemInformationTable("Name", "GroupName", "UserName");
                SystemInformationWmi.AddInstances(table, "Win32_LogicalProgramGroup",
                    new[] { "Name", "GroupName", "UserName" }, cancellationToken, rowLimit: 500);
                return table;
            }
            case SystemInformationCategoryId.SoftwareStartup:
            {
                var table = new SystemInformationTable("Name", "Command", "Location", "User");
                SystemInformationWmi.AddInstances(table, "Win32_StartupCommand",
                    new[] { "Name", "Command", "Location", "User" }, cancellationToken);
                return table;
            }
            case SystemInformationCategoryId.SoftwareOle:
            {
                var table = new SystemInformationTable("ProgId", "Caption", "InprocServer32");
                SystemInformationWmi.AddInstances(table, "Win32_ClassicCOMClassSetting",
                    new[] { "ProgId", "Caption", "InprocServer32" }, cancellationToken, rowLimit: 500);
                return table;
            }
            case SystemInformationCategoryId.SoftwareWer:
                return CollectWer();
            default:
            {
                var table = SystemInformationTable.ItemValue();
                table.AddRow(KryptonSystemInformationStrings.Current.SelectCategory, string.Empty);
                return table;
            }
        }
    }

    private static SystemInformationTable CollectEnvironmentVariables()
    {
        var table = new SystemInformationTable("Name", "Value", "Target");
        AddVariables(table, Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process), "Process");
        AddVariables(table, Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User), "User");
        AddVariables(table, Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine), "Machine");
        if (table.Rows.Count == 0)
        {
            table.AddRow(KryptonSystemInformationStrings.Current.NoItems);
        }

        return table;
    }

    private static void AddVariables(SystemInformationTable table, IDictionary variables, string target)
    {
        foreach (DictionaryEntry entry in variables)
        {
            table.AddRow(Convert.ToString(entry.Key, CultureInfo.CurrentCulture),
                Convert.ToString(entry.Value, CultureInfo.CurrentCulture),
                target);
        }
    }

    private static SystemInformationTable CollectRunningTasks(CancellationToken cancellationToken)
    {
        var table = new SystemInformationTable("ProcessId", "Name", "WorkingSet");
        try
        {
            foreach (var process in Process.GetProcesses())
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    table.AddRow(
                        process.Id.ToString(CultureInfo.CurrentCulture),
                        process.ProcessName,
                        process.WorkingSet64.ToString(CultureInfo.CurrentCulture));
                }
                catch (InvalidOperationException)
                {
                    // Process exited.
                }
                finally
                {
                    process.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            table.AddRow(ex.Message);
        }

        if (table.Rows.Count == 0)
        {
            table.AddRow(KryptonSystemInformationStrings.Current.NoItems);
        }

        return table;
    }

    private static SystemInformationTable CollectLoadedModules(CancellationToken cancellationToken, bool allProcessModules)
    {
        var table = new SystemInformationTable("Process", "ModuleName", "FileName");
        try
        {
            if (allProcessModules)
            {
                foreach (var process in Process.GetProcesses())
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    AddProcessModules(table, process, true, cancellationToken);
                    if (table.Rows.Count >= SystemInformationWmi.DefaultRowLimit)
                    {
                        table.AddRow(KryptonSystemInformationStrings.Current.RowLimitNote);
                        return table;
                    }
                }
            }
            else
            {
                AddProcessModules(table, Process.GetCurrentProcess(), false, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            table.AddRow(ex.Message);
        }

        if (table.Rows.Count == 0)
        {
            table.AddRow(KryptonSystemInformationStrings.Current.NoItems);
        }

        return table;
    }

    private static void AddProcessModules(SystemInformationTable table, Process process, bool disposeProcess, CancellationToken cancellationToken)
    {
        try
        {
            foreach (ProcessModule module in process.Modules)
            {
                cancellationToken.ThrowIfCancellationRequested();
                table.AddRow(process.ProcessName, module.ModuleName, module.FileName);
                if (table.Rows.Count >= SystemInformationWmi.DefaultRowLimit)
                {
                    return;
                }
            }
        }
        catch (Win32Exception)
        {
            table.AddRow(process.ProcessName, KryptonSystemInformationStrings.Current.AccessDenied, string.Empty);
        }
        catch (InvalidOperationException)
        {
            // Process exited.
        }
        finally
        {
            if (disposeProcess)
            {
                process.Dispose();
            }
        }
    }

    private static SystemInformationTable CollectWer()
    {
        var table = SystemInformationTable.ItemValue();
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\Windows Error Reporting");
            if (key == null)
            {
                table.AddRow(KryptonSystemInformationStrings.Current.Unavailable, string.Empty);
                return table;
            }

            foreach (var name in key.GetValueNames())
            {
                table.AddRow(name, Convert.ToString(key.GetValue(name), CultureInfo.CurrentCulture));
            }

            var dumpFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            dumpFolder = Path.Combine(dumpFolder, @"Microsoft\Windows\WER");
            table.AddRow("Local WER folder", Directory.Exists(dumpFolder) ? dumpFolder : KryptonSystemInformationStrings.Current.Unavailable);
        }
        catch (UnauthorizedAccessException)
        {
            table.AddRow(KryptonSystemInformationStrings.Current.AccessDenied, string.Empty);
        }
        catch (Exception ex)
        {
            table.AddRow(ex.Message, string.Empty);
        }

        return table;
    }
}
