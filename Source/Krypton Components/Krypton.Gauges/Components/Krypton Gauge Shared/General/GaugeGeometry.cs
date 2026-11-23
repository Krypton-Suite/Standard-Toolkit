#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Shared geometry helpers for analogue gauge faces.
/// </summary>
internal static class GaugeGeometry
{
    /// <summary>
    /// Converts degrees (clockwise from east) to radians used by <see cref="Math"/>.
    /// </summary>
    public static double DegreesToRadians(double degrees) => degrees * Math.PI / 180d;

    /// <summary>
    /// Converts a normalised 0..1 value to an angle in degrees along the arc.
    /// </summary>
    public static float ValueToAngle(float startAngle, float sweepAngle, double normalized) =>
        startAngle + (float)(sweepAngle * Clamp01(normalized));

    /// <summary>
    /// Converts a client point relative to a dial centre into a normalised 0..1 value along the arc.
    /// </summary>
    public static bool TryPointToNormalized(Point point, PointF center, float startAngle, float sweepAngle, out double normalized)
    {
        double dx = point.X - center.X;
        double dy = point.Y - center.Y;
        if (Math.Abs(dx) < 0.001d && Math.Abs(dy) < 0.001d)
        {
            normalized = 0d;
            return false;
        }

        // Atan2 returns counter-clockwise from east; WinForms GDI arcs use clockwise degrees from east.
        double degrees = Math.Atan2(dy, dx) * 180d / Math.PI;
        if (degrees < 0d)
        {
            degrees += 360d;
        }

        normalized = AngleToNormalized(degrees, startAngle, sweepAngle);
        return true;
    }

    /// <summary>
    /// Projects a point onto a linear track and returns a normalised 0..1 value.
    /// </summary>
    public static double PointToLinearNormalized(Point point, Rectangle track, Orientation orientation)
    {
        if (orientation == Orientation.Vertical)
        {
            if (track.Height <= 0)
            {
                return 0d;
            }

            // Bottom is minimum, top is maximum.
            return Clamp01(1d - ((point.Y - track.Top) / (double)track.Height));
        }

        if (track.Width <= 0)
        {
            return 0d;
        }

        return Clamp01((point.X - track.Left) / (double)track.Width);
    }

    /// <summary>
    /// Returns a point on a circle at the given clockwise-from-east angle.
    /// </summary>
    public static PointF PointOnCircle(PointF center, float radius, float degrees)
    {
        double radians = DegreesToRadians(degrees);
        return new PointF(
            center.X + (float)(Math.Cos(radians) * radius),
            center.Y + (float)(Math.Sin(radians) * radius));
    }

    private static double AngleToNormalized(double degrees, float startAngle, float sweepAngle)
    {
        double start = NormalizeDegrees(startAngle);
        double sweep = sweepAngle;
        double absoluteSweep = Math.Abs(sweep);
        if (absoluteSweep < 0.001d)
        {
            return 0d;
        }

        double delta = NormalizeDegrees(degrees - start);
        if (sweep < 0d)
        {
            delta = NormalizeDegrees(-delta);
        }

        if (delta > absoluteSweep)
        {
            // Snap to nearer end when outside the arc.
            double beyond = 360d - delta;
            return beyond < delta - absoluteSweep ? 0d : 1d;
        }

        return Clamp01(delta / absoluteSweep);
    }

    private static double NormalizeDegrees(double degrees)
    {
        degrees %= 360d;
        if (degrees < 0d)
        {
            degrees += 360d;
        }

        return degrees;
    }

    private static double Clamp01(double value)
    {
        if (value < 0d)
        {
            return 0d;
        }

        return value > 1d ? 1d : value;
    }
}
