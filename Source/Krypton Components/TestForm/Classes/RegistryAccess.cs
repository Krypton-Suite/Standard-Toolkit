namespace TestForm
{
    using Microsoft.Win32;
    /// <summary>
    /// Simple class to handle program auto start when the user logs on.
    /// </summary>
    public class RegistryAccess
    {
        // Registry access
        private RegistryKey _registryKey;
        private readonly string _registryPath;
        private const string _rvLastFilterString = "LastFilterString";
        private const string _rvDockTopRight = "DockTopRight";

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <exception cref="Exception">Throws an Exception when the registry key cannot be opened.</exception>
        public RegistryAccess()
        {
            _registryPath = @"Software\Krypton-Suite\Standard-ToolKit\TestForm";
            _registryKey = Registry.CurrentUser.CreateSubKey(_registryPath)
                ?? throw new Exception("Registry.CurrentUser.CreateSubKey() returned null.");
        }

        public string LastFilterString
        {
            get => _registryKey.GetValue(_rvLastFilterString) as string ?? string.Empty;
            set => _registryKey.SetValue(_rvLastFilterString, value);
        }

        public bool DockTopRight
        {
            get => (_registryKey.GetValue(_rvDockTopRight, "0") as string) == "1" ? true : false;
            set => _registryKey.SetValue(_rvDockTopRight, value ? "1"  : "0");
        }
    }
}

