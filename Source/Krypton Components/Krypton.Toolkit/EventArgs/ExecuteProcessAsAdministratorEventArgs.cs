#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Handles the user account control event for the <see cref="KryptonButton" />.</summary>
    public class ExecuteProcessAsAdministratorEventArgs : EventArgs
    {
        /// <summary></summary>
        public Assembly AssemblyProcess { get; set; }

        /// <summary>Gets or sets the process path to execute.</summary>
        /// <value>The process path.</value>
        public string ProcessPath { get; set; }

        /// <summary>Gets or sets the extra arguments.</summary>
        /// <value>The extra arguments.</value>
        public string ExtraArguments { get; set; }

        /// <summary>Gets or sets the object to elevate.</summary>
        /// <value>The object to elevate.</value>
        public object ObjectToElevate { get; set; }

        public ExecuteProcessAsAdministratorEventArgs(Assembly assemblyProcess)
        {
            AssemblyProcess = assemblyProcess;

            ElevateProcessWithAdministrativeRights(Path.GetFullPath(AssemblyProcess.Location));
        }

        /// <summary>Initializes a new instance of the <see cref="ExecuteProcessAsAdministratorEventArgs"/> class.</summary>
        /// <param name="processPath">The process path.</param>
        public ExecuteProcessAsAdministratorEventArgs(string processPath)
        {
            ProcessPath = processPath;

            ElevateProcessWithAdministrativeRights(ProcessPath);
        }

        /// <summary>Initializes a new instance of the <see cref="ExecuteProcessAsAdministratorEventArgs" /> class.</summary>
        /// <param name="processPath">The process path.</param>
        /// <param name="extraArguments">The extra arguments.</param>
        public ExecuteProcessAsAdministratorEventArgs(string processPath, string extraArguments)
        {
            ProcessPath = processPath;

            ExtraArguments = extraArguments;

            ElevateProcessWithAdministrativeRights(processPath, extraArguments);
        }

        /// <summary>Initializes a new instance of the <see cref="ExecuteProcessAsAdministratorEventArgs" /> class.</summary>
        /// <param name="objectToElevate">The object to elevate.</param>
        public ExecuteProcessAsAdministratorEventArgs(object objectToElevate)
        {
            ObjectToElevate = objectToElevate;

            ElevateProcessWithAdministrativeRights(ObjectToElevate);
        }

        private void ElevateProcessWithAdministrativeRights(object objectToElevate, string arguments = null)
        {
            try
            {
                WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

                bool hasAdministrativeRights = principal.IsInRole(WindowsBuiltInRole.Administrator);

                //if (object == null)

                if (!hasAdministrativeRights)
                {
                    ProcessStartInfo psi = new ProcessStartInfo()
                    {
                        Verb = "runas",
                        Arguments = arguments,
                        //FileName = objectToElevate
                    };
                }
            }
            catch (Exception e)
            {
                KryptonMessageBox.Show($"Error: {e.Message}", "Error Caught", MessageBoxButtons.OK,
                                       KryptonMessageBoxIcon.ERROR);
            }
        }

        /// <summary>Elevates the process with administrative rights.</summary>
        /// <param name="processName">Name of the process.</param>
        /// <param name="arguments">Extra arguments to execute.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ElevateProcessWithAdministrativeRights(string processName, string arguments = null)
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            bool hasAdministrativeRights = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (string.IsNullOrWhiteSpace(processName)) throw new ArgumentNullException();

            if (!hasAdministrativeRights)
            {
                ProcessStartInfo process = new ProcessStartInfo()
                {
                    Verb = "runas",
                    Arguments = arguments,
                    FileName = processName
                };

                try
                {
                    Process.Start(process);
                }
                catch (Win32Exception e)
                {
                    KryptonMessageBox.Show($"Error: {e.Message}", "Error Caught", MessageBoxButtons.OK,
                                           KryptonMessageBoxIcon.ERROR);
                }

                return;
            }
        }
    }
}