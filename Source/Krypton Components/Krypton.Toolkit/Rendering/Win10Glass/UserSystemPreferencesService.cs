#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

//Seb add
//https://github.com/File-New-Project/EarTrumpet/blob/master/EarTrumpet/Services/UserSystemPreferencesService.cs
namespace Krypton.Toolkit;

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
            using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            return (int)baseKey
                .OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")?
                .GetValue("EnableTransparency", 0)! > 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static bool UseAccentColor
    {
        get
        {
            using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            return (int)baseKey
                .OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")?
                .GetValue("ColorPrevalence", 0)! > 0;
        }
    }
}