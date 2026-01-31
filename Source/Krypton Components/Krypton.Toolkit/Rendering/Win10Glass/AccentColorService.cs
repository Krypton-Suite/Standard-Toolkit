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

//https://github.com/File-New-Project/EarTrumpet/blob/master/EarTrumpet/Services/AccentColorService.cs
//Seb add
namespace Krypton.Toolkit;

/// <summary>
/// Gets the Windows accent color
/// </summary>
public static class AccentColorService
{
    internal static Color GetColorByTypeName(string name)
    {
        var colorSet = PI.GetImmersiveUserColorSetPreference(false, false);
        var colorType = PI.GetImmersiveColorTypeFromName(name);
            
        var rawColor = PI.GetImmersiveColorFromColorSetEx(colorSet, colorType, false, 0);

        return FromABGR((int)rawColor);
    }

    private static Color FromABGR(int abgrValue)
    {
        var colorBytes = new byte[4];
        colorBytes[0] = (byte)((0xFF000000 & abgrValue) >> 24);    // A
        colorBytes[1] = (byte)((0x00FF0000 & abgrValue) >> 16);    // B
        colorBytes[2] = (byte)((0x0000FF00 & abgrValue) >> 8);    // G
        colorBytes[3] = (byte)(0x000000FF & abgrValue);            // R

        return Color.FromArgb(colorBytes[0], colorBytes[3], colorBytes[2], colorBytes[1]);
    }
}