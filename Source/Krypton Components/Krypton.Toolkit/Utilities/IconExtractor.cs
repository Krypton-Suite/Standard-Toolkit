#region BSD 3-Clause License
// *****************************************************************************
// BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit)
// by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
//  Version 6.0.0  
// *****************************************************************************
#endregion

namespace Krypton.Toolkit
{
  /// <summary>
  /// Provides a method for extracting images from the system via LoadImage
  /// </summary>
  public static class IconExtractor
  {
    /// <summary>
    /// Loads the icon.
    /// </summary>
    /// <param name="type">The type of icon.</param>
    /// <param name="size">The size.</param>
    /// <returns>The icon.</returns>
    /// <exception cref="System.PlatformNotSupportedException"></exception>
    public static Icon LoadIcon(IconType type, Size size)
    {
     IntPtr hIcon = ImageNativeMethods.LoadImage(IntPtr.Zero, "#" + (int)type, 1, size.Width, size.Height, 0);
     return hIcon == IntPtr.Zero ? null : Icon.FromHandle(hIcon);
    }

    public enum IconType
    {
     Warning = 101,
     Help = 102,
     Error = 103,
     Info = 104,
     Shield = 106
    }
  }
}