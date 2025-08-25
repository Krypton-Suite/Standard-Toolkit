#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Krypton.Toolkit;

/// <summary>
/// Centralized helper for retrieving and managing UAC shield icons.
/// </summary>
public static class UacShieldIconHelper
{
    #region Public Methods

    /// <summary>
    /// Gets the system UAC shield icon using Windows API calls.
    /// </summary>
    /// <returns>The system shield icon or null if not available.</returns>
    public static Icon? GetSystemShieldIcon()
    {
        try
        {
            // Check if we're on Windows Vista or later (UAC was introduced in Vista)
            if (Environment.OSVersion.Version.Major < 6)
            {
                return null;
            }

            // Use SHGetStockIconInfo to get the system shield icon
            var shinfo = new PI.SHSTOCKICONINFO();
            shinfo.cbSize = (uint)Marshal.SizeOf(typeof(PI.SHSTOCKICONINFO));

            int result = PI.SHGetStockIconInfo(GlobalStaticValues.SIID_SHIELD,
                                               GlobalStaticValues.SHGSI_ICON | GlobalStaticValues.SHGSI_LARGEICON,
                                               ref shinfo);
            if (result == 0 && shinfo.hIcon != IntPtr.Zero)
            {
                try 
                { 
                    return (Icon)Icon.FromHandle(shinfo.hIcon).Clone(); 
                }
                finally 
                { 
                    PI.DestroyIcon(shinfo.hIcon); 
                }
            }

            // Try small size if large failed
            result = PI.SHGetStockIconInfo(GlobalStaticValues.SIID_SHIELD,
                                           GlobalStaticValues.SHGSI_ICON | GlobalStaticValues.SHGSI_SMALLICON,
                                           ref shinfo);
            if (result == 0 && shinfo.hIcon != IntPtr.Zero)
            {
                try 
                { 
                    return (Icon)Icon.FromHandle(shinfo.hIcon).Clone(); 
                }
                finally 
                { 
                    PI.DestroyIcon(shinfo.hIcon); 
                }
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets a DPI-aware system shield icon at the specified size.
    /// </summary>
    /// <param name="size">The desired icon size.</param>
    /// <returns>A DPI-aware system shield icon or null if not available.</returns>
    public static Icon? GetSystemShieldIconAtSize(int size)
    {
        var baseIcon = GetSystemShieldIcon();
        if (baseIcon == null) return null;
        
        try
        {
            using (var bitmap = new Bitmap(size, size))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawIcon(baseIcon, new Rectangle(0, 0, size, size));
                return CreateIconFromBitmap(bitmap);
            }
        }
        finally
        {
            baseIcon.Dispose();
        }
    }

    /// <summary>
    /// Gets the best available UAC shield icon for the current OS.
    /// </summary>
    /// <returns>An Icon representing the UAC shield for the current OS, or null if not available.</returns>
    public static Icon? GetShieldIcon()
    {
        try
        {
            // Try to get the system shield icon using SHGetStockIconInfo API first
            var systemShieldIcon = GetSystemShieldIcon();
            if (systemShieldIcon != null)
            {
                return systemShieldIcon;
            }

            // Fallback to OS-specific resource-based icons
            if (OSUtilities.IsAtLeastWindowsEleven)
            {
                return CreateIconFromBitmap(ResourceFiles.UAC.UACShieldIconResources.UACShieldWindows11);
            }
            else if (OSUtilities.IsWindowsTen)
            {
                return CreateIconFromBitmap(ResourceFiles.UAC.UACShieldIconResources.UACShieldWindows10);
            }
            else if (OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight || OSUtilities.IsWindowsSeven)
            {
                return CreateIconFromBitmap(ResourceFiles.UAC.UACShieldIconResources.UACShieldWindows7881);
            }
            else
            {
                // Final fallback to system shield icon for older Windows versions
                return SystemIcons.Shield;
            }
        }
        catch
        {
            // Fallback to system shield icon if all else fails
            return SystemIcons.Shield;
        }
    }

    /// <summary>
    /// Scales an icon with high quality and DPI awareness.
    /// </summary>
    /// <param name="icon">The icon to scale.</param>
    /// <param name="width">The target width.</param>
    /// <param name="height">The target height.</param>
    /// <returns>A scaled bitmap.</returns>
    public static Bitmap? ScaleIconWithQuality(Icon icon, int width, int height)
    {
        try
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawIcon(icon, new Rectangle(0, 0, width, height));
            }
            return bmp;
        }
        catch
        {
            return GraphicsExtensions.ScaleImage(icon.ToBitmap(), width, height);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Creates an Icon from a Bitmap with proper GDI handle management.
    /// </summary>
    /// <param name="bitmap">The bitmap to convert.</param>
    /// <returns>An Icon created from the bitmap, or null if conversion fails.</returns>
    private static Icon? CreateIconFromBitmap(Bitmap? bitmap)
    {
        if (bitmap == null) return null;
        
        IntPtr hIcon = IntPtr.Zero;
        try
        {
            hIcon = bitmap.GetHicon();
            using (var icon = Icon.FromHandle(hIcon))
            {
                return (Icon)icon.Clone();
            }
        }
        catch
        {
            return null;
        }
        finally
        {
            if (hIcon != IntPtr.Zero)
            {
                PI.DestroyIcon(hIcon);
            }
        }
    }

    #endregion
}
