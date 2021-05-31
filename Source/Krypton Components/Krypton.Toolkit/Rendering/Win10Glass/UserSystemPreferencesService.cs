#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion


//Seb add
//https://github.com/File-New-Project/EarTrumpet/blob/master/EarTrumpet/Services/UserSystemPreferencesService.cs
namespace Krypton.Toolkit
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
