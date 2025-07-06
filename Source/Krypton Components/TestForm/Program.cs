#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion
using System.Runtime.InteropServices;

namespace TestForm;

internal static class Program
{
    [DllImport("user32.dll")]
    static extern bool SetProcessDPIAware();

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Enable High-DPI support for Windows Forms
        if (Environment.OSVersion.Version.Major >= 6)
        {
            SetProcessDPIAware();
        }

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
#if NET6_0_OR_GREATER
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
        Application.Run(new StartScreen());
    }
}