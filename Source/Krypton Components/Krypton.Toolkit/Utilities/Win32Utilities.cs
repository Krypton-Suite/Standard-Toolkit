namespace Krypton.Toolkit
{
    public static class Win32Utilities
    {
        public static string ReadRegistryHklmValue(string keyName, string subKeyRef)
        {
            RegistryKey? registryKey;

            try
            {
                registryKey = Registry.LocalMachine.OpenSubKey(keyName, false);
            }
            catch (Exception e)
            {
                KryptonExceptionHandler.CaptureException(e);
                return string.Empty;
            }

            return registryKey == null ? string.Empty : registryKey.GetValue(subKeyRef) as string ?? string.Empty;
        }

        public static void ShowSystemInformation()
        {
            string systemInfoPath = ReadRegistryHklmValue(@"SOFTWARE\Microsoft\Shared Tools Location", @"MSINFO");

            if (systemInfoPath == "")
            {
                systemInfoPath = ReadRegistryHklmValue(@"SOFTWARE\Microsoft\Shared Tools\MSINFO", "PATH");
            }

            if (systemInfoPath == "")
            {
                KryptonMessageBox.Show(@"System Information is unavailable at this time.\n\n(Couldn't find path for Microsoft System Information Tool in the registry.)",
                    @"Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start(systemInfoPath);
            }
            catch (Exception)
            {
                KryptonMessageBox.Show(@$"System Information is unavailable at this time.\n\n(Couldn't launch '{systemInfoPath}')",
                    @"Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Stop);
            }
        }
    }
}