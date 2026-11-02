using Krypton.Toolkit;

namespace $safeprojectname$;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        KryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        Application.Run(new MainForm());
    }
}
