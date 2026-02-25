#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Utilities;

/// <summary>
/// Helper class for animating form transitions during dock/float operations.
/// </summary>
internal static class FloatingAnimationHelper
{
    /// <summary>
    /// Animates a form's position and opacity smoothly.
    /// </summary>
    /// <param name="form">The form to animate.</param>
    /// <param name="targetLocation">The target location.</param>
    /// <param name="targetOpacity">The target opacity (0.0 to 1.0).</param>
    /// <param name="duration">The animation duration in milliseconds.</param>
    /// <param name="callback">Optional callback to execute when animation completes.</param>
    public static void AnimateForm(Form form, Point targetLocation, double targetOpacity, int duration, Action? callback = null)
    {
        if (duration <= 0)
        {
            form.Location = targetLocation;
            form.Opacity = targetOpacity;
            callback?.Invoke();
            return;
        }

        Point startLocation = form.Location;
        double startOpacity = form.Opacity;
        DateTime startTime = DateTime.Now;
        TimeSpan totalDuration = TimeSpan.FromMilliseconds(duration);

        Timer animationTimer = new Timer { Interval = 16 }; // ~60 FPS

        animationTimer.Tick += (sender, e) =>
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            double progress = Math.Min(1.0, elapsed.TotalMilliseconds / duration);

            // Use easing function for smooth animation (ease-out)
            double easedProgress = EaseOutCubic(progress);

            // Interpolate location
            int x = (int)(startLocation.X + (targetLocation.X - startLocation.X) * easedProgress);
            int y = (int)(startLocation.Y + (targetLocation.Y - startLocation.Y) * easedProgress);
            form.Location = new Point(x, y);

            // Interpolate opacity
            form.Opacity = startOpacity + (targetOpacity - startOpacity) * easedProgress;

            if (progress >= 1.0)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
                form.Location = targetLocation;
                form.Opacity = targetOpacity;
                callback?.Invoke();
            }
        };

        animationTimer.Start();
    }

    /// <summary>
    /// Animates a form's size smoothly.
    /// </summary>
    /// <param name="form">The form to animate.</param>
    /// <param name="targetSize">The target size.</param>
    /// <param name="duration">The animation duration in milliseconds.</param>
    /// <param name="callback">Optional callback to execute when animation completes.</param>
    public static void AnimateFormSize(Form form, Size targetSize, int duration, Action? callback = null)
    {
        if (duration <= 0)
        {
            form.Size = targetSize;
            callback?.Invoke();
            return;
        }

        Size startSize = form.Size;
        DateTime startTime = DateTime.Now;

        Timer animationTimer = new Timer { Interval = 16 }; // ~60 FPS

        animationTimer.Tick += (sender, e) =>
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            double progress = Math.Min(1.0, elapsed.TotalMilliseconds / duration);

            // Use easing function for smooth animation
            double easedProgress = EaseOutCubic(progress);

            // Interpolate size
            int width = (int)(startSize.Width + (targetSize.Width - startSize.Width) * easedProgress);
            int height = (int)(startSize.Height + (targetSize.Height - startSize.Height) * easedProgress);
            form.Size = new Size(width, height);

            if (progress >= 1.0)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
                form.Size = targetSize;
                callback?.Invoke();
            }
        };

        animationTimer.Start();
    }

    /// <summary>
    /// Easing function: ease-out cubic for smooth deceleration.
    /// </summary>
    private static double EaseOutCubic(double t)
    {
        return 1 - Math.Pow(1 - t, 3);
    }
}
