#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
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