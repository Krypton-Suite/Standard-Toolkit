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

using System;
using System.Drawing;
using System.Runtime.InteropServices;

//https://github.com/File-New-Project/EarTrumpet/blob/master/EarTrumpet/Services/AccentColorService.cs
//Seb add
namespace Krypton.Toolkit
{
    /// <summary>
    /// Gets the Windows accent color
    /// </summary>
    public static class AccentColorService
    {
        static class Interop
        {
            // Thanks, Quppa! -RR
            [DllImport("uxtheme.dll", EntryPoint = "#94", CharSet = CharSet.Unicode)]
            internal static extern int GetImmersiveColorSetCount();

            [DllImport("uxtheme.dll", EntryPoint = "#95", CharSet = CharSet.Unicode)]
            internal static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType, bool bIgnoreHighContrast, uint dwHighContrastCacheMode);

            [DllImport("uxtheme.dll", EntryPoint = "#96", CharSet = CharSet.Unicode)]
            internal static extern uint GetImmersiveColorTypeFromName(string name);

            [DllImport("uxtheme.dll", EntryPoint = "#98", CharSet = CharSet.Unicode)]
            internal static extern uint GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

            [DllImport("uxtheme.dll", EntryPoint = "#100", CharSet = CharSet.Unicode)]
            internal static extern IntPtr GetImmersiveColorNamedTypeByIndex(uint dwIndex);
        }

        internal static Color GetColorByTypeName(string name)
        {
            uint colorSet = Interop.GetImmersiveUserColorSetPreference(false, false);
            uint colorType = Interop.GetImmersiveColorTypeFromName(name);
            
            uint rawColor = Interop.GetImmersiveColorFromColorSetEx(colorSet, colorType, false, 0);

            return FromABGR((int)rawColor);
        }

        private static Color FromABGR(int abgrValue)
        {
            byte[] colorBytes = new byte[4];
            colorBytes[0] = (byte)((0xFF000000 & abgrValue) >> 24);    // A
            colorBytes[1] = (byte)((0x00FF0000 & abgrValue) >> 16);    // B
            colorBytes[2] = (byte)((0x0000FF00 & abgrValue) >> 8);    // G
            colorBytes[3] = (byte)(0x000000FF & abgrValue);            // R

            return Color.FromArgb(colorBytes[0], colorBytes[3], colorBytes[2], colorBytes[1]);
        }
    }
}
