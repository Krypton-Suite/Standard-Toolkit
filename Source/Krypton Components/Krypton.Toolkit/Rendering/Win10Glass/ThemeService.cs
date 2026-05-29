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
//https://github.com/File-New-Project/EarTrumpet/blob/master/EarTrumpet/Services/ThemeService.cs
namespace Krypton.Toolkit;

/// <summary>
/// 
/// </summary>
public class ThemeService
{
    /// <summary>
    /// 
    /// </summary>
    public static bool IsWindowTransparencyEnabled => !SystemInformation.HighContrast && UserSystemPreferencesService.IsTransparencyEnabled;

    //public static void UpdateThemeResources(ResourceDictionary dictionary)
    //{
    //    dictionary[@"WindowBackground"] = new SolidColorBrush(GetWindowBackgroundColor());

    //    SetBrush(dictionary, "WindowForeground", @"ImmersiveApplicationTextDarkTheme");
    //    ReplaceBrush(dictionary, "CottonSwabSliderThumb", @"ImmersiveSystemAccent");
    //    ReplaceBrush(dictionary, "CottonSwabSliderTrackBackground", SystemParameters.HighContrast ? "ImmersiveSystemAccentLight1" : "ImmersiveControlDarkSliderTrackBackgroundRest");
    //    SetBrushWithOpacity(dictionary, "CottonSwabSliderTrackBackgroundHover", SystemParameters.HighContrast ? "ImmersiveSystemAccentLight1" : "ImmersiveDarkBaseMediumHigh", SystemParameters.HighContrast ? 1.0 : 0.25);
    //    SetBrush(dictionary, "CottonSwabSliderTrackBackgroundPressed", @"ImmersiveControlDarkSliderTrackBackgroundRest");
    //    ReplaceBrush(dictionary, "CottonSwabSliderTrackFill", @"ImmersiveSystemAccentLight1");
    //    SetBrush(dictionary, "CottonSwabSliderThumbHover", @"ImmersiveControlDarkSliderThumbHover");
    //    SetBrush(dictionary, "CottonSwabSliderThumbPressed", @"ImmersiveControlDarkSliderThumbHover");
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static Color GetWindowBackgroundColor()
    {
        string resource;
        if (SystemInformation.HighContrast)
        {
            resource = "ImmersiveApplicationBackground";
        }
        else if (UserSystemPreferencesService.UseAccentColor)
        {
            resource = IsWindowTransparencyEnabled ? "ImmersiveSystemAccentDark2" : "ImmersiveSystemAccentDark1";
        }
        else
        {
            resource = "ImmersiveDarkChromeMedium";
        }

        Color color = AccentColorService.GetColorByTypeName(resource);
        //return color;
        //color.A = (byte)(IsWindowTransparencyEnabled ? 190 : 255);
        return Color.FromArgb(IsWindowTransparencyEnabled ? 190 : 255, color.R, color.G, color.B);
    }

    //private static void SetBrushWithOpacity(ResourceDictionary dictionary, string name, string immersiveAccentName, double opacity)
    //{
    //    var color = AccentColorService.GetColorByTypeName(immersiveAccentName);
    //    color.A = (byte) (opacity*255);
    //    ((SolidColorBrush) dictionary[name]).Color = color;
    //}

    //private static void SetBrush(ResourceDictionary dictionary, string name, string immersiveAccentName)
    //{
    //    SetBrushWithOpacity(dictionary, name, immersiveAccentName, 1.0);
    //}

    //private static void ReplaceBrush(ResourceDictionary dictionary, string name, string immersiveAccentName)
    //{
    //    dictionary[name] = new SolidColorBrush(AccentColorService.GetColorByTypeName(immersiveAccentName));
    //}
}