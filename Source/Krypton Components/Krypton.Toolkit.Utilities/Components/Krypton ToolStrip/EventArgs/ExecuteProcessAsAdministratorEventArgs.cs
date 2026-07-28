#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Event arguments for executing a process as an administrator.
/// </summary>
public class ExecuteProcessAsAdministratorEventArgs : EventArgs
{
    /// <summary>Gets or sets the process path.</summary>
    /// <value>The process path.</value>
    public string ProcessPath { get; set; }

    /// <summary>Initializes a new instance of the <see cref="ExecuteProcessAsAdministratorEventArgs"/> class.</summary>
    /// <param name="processPath">The process path.</param>
    public ExecuteProcessAsAdministratorEventArgs(string processPath)
    {
        ProcessPath = processPath;

        ElevateProcessWithAdministrativeRights(ProcessPath);
    }

    /// <summary>Elevates the process with administrative rights.</summary>
    /// <param name="processName">Name of the process.</param>
    /// <exception cref="ArgumentNullException"></exception>
    private void ElevateProcessWithAdministrativeRights(string processName)
    {
        WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

        bool hasAdministrativeRights = principal.IsInRole(WindowsBuiltInRole.Administrator);

        if (string.IsNullOrEmpty(processName))
        {
            throw new ArgumentNullException();
        }

        if (!hasAdministrativeRights)
        {
            ProcessStartInfo process = new ProcessStartInfo();

            process.Verb = "runas";

            process.FileName = processName;

            try
            {
                Process.Start(process);
            }
            catch (Win32Exception e)
            {
                KryptonExceptionHandler.CaptureException(e);
            }
        }
    }
}