using Microsoft.Win32;

//Seb add
//https://github.com/File-New-Project/EarTrumpet/blob/master/EarTrumpet/Services/UserSystemPreferencesService.cs
namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// 
    /// </summary>
    public static class UserSystemPreferencesService
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool IsTransparencyEnabled
        { 
            get
            {
#if NET35
                return false;
#else
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    return (int)baseKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").GetValue("EnableTransparency", 0) > 0;
                }
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool UseAccentColor
        {
            get
            {
#if NET35
                return false;
#else
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    return (int)baseKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").GetValue("ColorPrevalence", 0) > 0;
                }
#endif
            }
        }
    }
}
