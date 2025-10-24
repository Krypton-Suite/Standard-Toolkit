#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion
using System.Runtime.InteropServices;
using System.Threading;

namespace TestForm;

internal static class Program
{
#if NETFRAMEWORK
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
#endif
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
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

        // Register global exception handlers to avoid unexpected termination due to faulty palette indices
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += Application_ThreadException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#if NET8_0_OR_GREATER
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
        Application.Run(new StartScreen());
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        if (e.Exception is IndexOutOfRangeException)
        {
            // Swallow palette index errors – log and continue running
            System.Diagnostics.Debug.WriteLine(e.Exception);
            return;
        }
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is IndexOutOfRangeException idx)
        {
            // Swallow palette index errors – log and continue running
            System.Diagnostics.Debug.WriteLine(idx);
            return;
        }
    }
}