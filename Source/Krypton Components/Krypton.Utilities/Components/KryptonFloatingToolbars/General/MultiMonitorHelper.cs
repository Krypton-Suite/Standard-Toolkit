#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Helper class for multi-monitor support in floating toolbars.
/// </summary>
public static class MultiMonitorHelper
{
    /// <summary>
    /// Gets the screen that contains the specified point.
    /// </summary>
    /// <param name="point">The screen point.</param>
    /// <returns>The screen containing the point, or primary screen if not found.</returns>
    public static Screen GetScreenFromPoint(Point point) => Screen.FromPoint(point) ?? Screen.PrimaryScreen!;

    /// <summary>
    /// Gets the screen that contains the specified rectangle.
    /// </summary>
    /// <param name="rect">The screen rectangle.</param>
    /// <returns>The screen containing the rectangle, or primary screen if not found.</returns>
    public static Screen GetScreenFromRectangle(Rectangle rect)
    {
        Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
        return GetScreenFromPoint(center);
    }

    /// <summary>
    /// Ensures a point is within the bounds of any screen.
    /// </summary>
    /// <param name="point">The point to validate.</param>
    /// <returns>A point that is guaranteed to be on a screen.</returns>
    public static Point EnsurePointOnScreen(Point point)
    {
        Screen screen = GetScreenFromPoint(point);
        Rectangle bounds = screen.WorkingArea;

        // Clamp to screen bounds
        point.X = Math.Max(bounds.Left, Math.Min(bounds.Right - 100, point.X));
        point.Y = Math.Max(bounds.Top, Math.Min(bounds.Bottom - 50, point.Y));

        return point;
    }

    /// <summary>
    /// Ensures a rectangle is visible on any screen.
    /// </summary>
    /// <param name="rect">The rectangle to validate.</param>
    /// <returns>A rectangle that is guaranteed to be visible on a screen.</returns>
    public static Rectangle EnsureRectangleOnScreen(Rectangle rect)
    {
        Screen screen = GetScreenFromRectangle(rect);
        Rectangle bounds = screen.WorkingArea;

        // Ensure rectangle fits on screen
        if (rect.Right > bounds.Right)
        {
            rect.X = bounds.Right - rect.Width;
        }
        if (rect.Bottom > bounds.Bottom)
        {
            rect.Y = bounds.Bottom - rect.Height;
        }
        if (rect.Left < bounds.Left)
        {
            rect.X = bounds.Left;
        }
        if (rect.Top < bounds.Top)
        {
            rect.Y = bounds.Top;
        }

        return rect;
    }

    /// <summary>
    /// Gets all available screens.
    /// </summary>
    /// <returns>An array of all screens.</returns>
    public static Screen[] GetAllScreens() => Screen.AllScreens;

    /// <summary>
    /// Gets the primary screen.
    /// </summary>
    /// <returns>The primary screen.</returns>
    public static Screen GetPrimaryScreen() => Screen.PrimaryScreen!;

    /// <summary>
    /// Gets the screen count.
    /// </summary>
    /// <returns>The number of screens.</returns>
    public static int GetScreenCount() => Screen.AllScreens.Length;

    /// <summary>
    /// Checks if a point is on any screen.
    /// </summary>
    /// <param name="point">The point to check.</param>
    /// <returns>True if the point is on a screen; otherwise, false.</returns>
    public static bool IsPointOnScreen(Point point)
    {
        foreach (Screen screen in Screen.AllScreens)
        {
            if (screen.Bounds.Contains(point))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Gets the best screen for displaying a window based on the specified point.
    /// </summary>
    /// <param name="preferredPoint">The preferred location for the window.</param>
    /// <returns>The best screen for the window.</returns>
    public static Screen GetBestScreenForWindow(Point preferredPoint)
    {
        Screen bestScreen = GetScreenFromPoint(preferredPoint);
        
        // If point is not on any screen, use primary screen
        if (!IsPointOnScreen(preferredPoint))
        {
            bestScreen = Screen.PrimaryScreen!;
        }
        
        return bestScreen;
    }

    /// <summary>
    /// Calculates the optimal position for a window on the specified screen.
    /// </summary>
    /// <param name="screen">The target screen.</param>
    /// <param name="windowSize">The size of the window.</param>
    /// <param name="preferredLocation">The preferred location (can be off-screen).</param>
    /// <returns>The optimal position for the window.</returns>
    public static Point CalculateOptimalWindowPosition(Screen screen, Size windowSize, Point preferredLocation)
    {
        Rectangle workingArea = screen.WorkingArea;
        
        // Start with preferred location
        Point position = preferredLocation;
        
        // Ensure window fits horizontally
        if (position.X + windowSize.Width > workingArea.Right)
        {
            position.X = workingArea.Right - windowSize.Width;
        }
        if (position.X < workingArea.Left)
        {
            position.X = workingArea.Left;
        }
        
        // Ensure window fits vertically
        if (position.Y + windowSize.Height > workingArea.Bottom)
        {
            position.Y = workingArea.Bottom - windowSize.Height;
        }
        if (position.Y < workingArea.Top)
        {
            position.Y = workingArea.Top;
        }
        
        return position;
    }

    /// <summary>
    /// Distributes multiple windows across available screens to avoid overlap.
    /// </summary>
    /// <param name="windowSizes">The sizes of windows to distribute.</param>
    /// <param name="preferredLocations">The preferred locations for each window.</param>
    /// <returns>A list of optimal positions for each window.</returns>
    public static List<Point> DistributeWindowsAcrossScreens(List<Size> windowSizes, List<Point> preferredLocations)
    {
        var positions = new List<Point>();
        var screens = GetAllScreens();
        
        if (screens.Length == 0)
        {
            return positions;
        }
        
        int screenIndex = 0;
        int windowsPerScreen = Math.Max(1, windowSizes.Count / screens.Length);
        int currentScreenWindowCount = 0;
        Point currentScreenOffset = new Point(20, 20);
        
        for (int i = 0; i < windowSizes.Count; i++)
        {
            Screen currentScreen = screens[screenIndex];
            Rectangle workingArea = currentScreen.WorkingArea;
            
            // Calculate position on current screen
            Point position = new Point(
                workingArea.Left + currentScreenOffset.X,
                workingArea.Top + currentScreenOffset.Y
            );
            
            // Ensure it fits
            position = CalculateOptimalWindowPosition(currentScreen, windowSizes[i], position);
            positions.Add(position);
            
            // Update offset for next window on this screen
            currentScreenOffset.X += 30;
            currentScreenOffset.Y += 30;
            
            // Move to next screen if needed
            currentScreenWindowCount++;
            if (currentScreenWindowCount >= windowsPerScreen && screenIndex < screens.Length - 1)
            {
                screenIndex++;
                currentScreenWindowCount = 0;
                currentScreenOffset = new Point(20, 20);
            }
        }
        
        return positions;
    }

    /// <summary>
    /// Gets layout information for all screens, useful for advanced layout management.
    /// </summary>
    /// <returns>A dictionary mapping screen device names to their layout information.</returns>
    public static Dictionary<string, ScreenLayoutInfo> GetScreenLayoutInfo()
    {
        var layoutInfo = new Dictionary<string, ScreenLayoutInfo>();
        
        foreach (Screen screen in Screen.AllScreens)
        {
            layoutInfo[screen.DeviceName] = new ScreenLayoutInfo
            {
                DeviceName = screen.DeviceName,
                Bounds = screen.Bounds,
                WorkingArea = screen.WorkingArea,
                IsPrimary = screen.Primary,
                BitsPerPixel = screen.BitsPerPixel
            };
        }
        
        return layoutInfo;
    }
}