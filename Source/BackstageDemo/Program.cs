using System;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace BackstageDemo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Set up Krypton theming
            var manager = new KryptonManager();
            manager.GlobalPaletteMode = PaletteMode.Microsoft365Blue;
            
            using var form = new BackstageDemo();
            Application.Run(form);
        }
    }
}
