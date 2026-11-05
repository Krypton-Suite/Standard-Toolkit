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
/// Classic VU mark positions. Spacing is tighter toward +3 than a linear decibel map.
/// </summary>
internal static class VuMeterScale
{
    internal const float ArcStartDegrees = 220f;
    internal const float ArcSweepDegrees = 100f;
    internal const double ScaleMinimum = -20d;
    internal const double ScaleMaximum = 3d;
    internal const double DefaultCalibrationOffsetDb = -18d;

    private static readonly Mark[] _marks =
    {
        new Mark(-20d, 0.00d),
        new Mark(-10d, 0.25d),
        new Mark(-7d, 0.40d),
        new Mark(-5d, 0.51d),
        new Mark(-3d, 0.62d),
        new Mark(-2d, 0.70d),
        new Mark(-1d, 0.77d),
        new Mark(0d, 0.84d),
        new Mark(1d, 0.90d),
        new Mark(2d, 0.95d),
        new Mark(3d, 1.00d)
    };

    /// <summary>
    /// Absolute position of <paramref name="value"/> on the classic scale, from 0 at -20 to 1 at +3.
    /// </summary>
    internal static double AbsolutePosition(double value)
    {
        if (value <= _marks[0].Value)
        {
            return _marks[0].Position;
        }

        int last = _marks.Length - 1;
        if (value >= _marks[last].Value)
        {
            return _marks[last].Position;
        }

        for (int i = 1; i < _marks.Length; i++)
        {
            if (value <= _marks[i].Value)
            {
                double span = _marks[i].Value - _marks[i - 1].Value;
                double t = span <= 0d ? 0d : (value - _marks[i - 1].Value) / span;
                return _marks[i - 1].Position + (t * (_marks[i].Position - _marks[i - 1].Position));
            }
        }

        return 1d;
    }

    /// <summary>
    /// Position of <paramref name="value"/> inside the caller range, still using classic spacing.
    /// </summary>
    internal static double ViewPosition(double value, double minimum, double maximum)
    {
        double start = AbsolutePosition(minimum);
        double end = AbsolutePosition(maximum);
        double span = end - start;
        if (Math.Abs(span) < 0.0000001d)
        {
            return 0d;
        }

        double t = (AbsolutePosition(value) - start) / span;
        if (t < 0d)
        {
            return 0d;
        }

        return t > 1d ? 1d : t;
    }

    /// <summary>
    /// Marks that fall inside <paramref name="minimum"/>..<paramref name="maximum"/>, including the endpoints.
    /// </summary>
    internal static void CopyVisiblePoints(double minimum, double maximum, List<double> points)
    {
        points.Clear();
        points.Add(minimum);
        for (int i = 0; i < _marks.Length; i++)
        {
            double mark = _marks[i].Value;
            if (mark > minimum && mark < maximum)
            {
                points.Add(mark);
            }
        }

        if (Math.Abs(maximum - minimum) > 0.0001d)
        {
            points.Add(maximum);
        }
    }

    internal static string FormatMark(double value)
    {
        double rounded = Math.Round(value, 0, MidpointRounding.AwayFromZero);
        if (Math.Abs(rounded - value) > 0.05d)
        {
            return value.ToString("0.#", CultureInfo.CurrentCulture);
        }

        string text = rounded.ToString("0", CultureInfo.CurrentCulture);
        return rounded > 0d ? "+" + text : text;
    }

    private readonly struct Mark
    {
        internal Mark(double value, double position)
        {
            Value = value;
            Position = position;
        }

        internal double Value { get; }

        internal double Position { get; }
    }
}
