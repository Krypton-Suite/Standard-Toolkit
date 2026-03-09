#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion
using System.Runtime.InteropServices;
using System.Threading;

namespace TestForm;

internal static class Program
{
    private const string JumpListAppId = "KryptonToolkit.JumpListTest";

#if NETFRAMEWORK
    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();
#endif

    [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int SetCurrentProcessExplicitAppUserModelID(string appId);

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Set AppUserModelID before any UI - required for taskbar jump list to attach to this process
        try
        {
            SetCurrentProcessExplicitAppUserModelID(JumpListAppId);
        }
        catch
        {
            // Ignore on older Windows
        }

        // Enable High-DPI support for Windows Forms
#if NETFRAMEWORK
        if (Environment.OSVersion.Version.Major >= 6)
        {
            SetProcessDPIAware();
        }
#else
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Initialize WPF Application for JumpList (required for System.Windows.Shell.JumpList)
        _ = new global::System.Windows.Application();
#if NET8_0_OR_GREATER
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
        Application.Run(new StartScreen());
    }
}
