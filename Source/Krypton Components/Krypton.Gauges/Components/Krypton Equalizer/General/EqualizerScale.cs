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
/// Default band frequencies and level/gain mapping helpers for <see cref="KryptonEqualizer"/>.
/// </summary>
internal static class EqualizerScale
{
    internal const int DefaultBandCount = 10;
    internal const int MinimumBandCount = 3;
    internal const int MaximumBandCount = 31;
    internal const double DefaultMinimumGainDb = -12d;
    internal const double DefaultMaximumGainDb = 12d;
    internal const double DefaultCalibrationOffsetDb = -18d;
    internal const double LevelFloorDb = -60d;

    /// <summary>
    /// Classic 10-band graphic-EQ centre frequencies (Hz).
    /// </summary>
    private static readonly double[] TenBandHz =
    {
        31.5d, 63d, 125d, 250d, 500d, 1000d, 2000d, 4000d, 8000d, 16000d
    };

    /// <summary>
    /// Builds logarithmically spaced centre frequencies for <paramref name="bandCount"/> bands.
    /// </summary>
    internal static double[] CreateFrequencies(int bandCount)
    {
        int count = ClampBandCount(bandCount);
        if (count == TenBandHz.Length)
        {
            var copy = new double[TenBandHz.Length];
            Array.Copy(TenBandHz, copy, TenBandHz.Length);
            return copy;
        }

        const double low = 31.5d;
        const double high = 16000d;
        var frequencies = new double[count];
        double logLow = Math.Log(low);
        double logHigh = Math.Log(high);
        for (int i = 0; i < count; i++)
        {
            double t = count == 1 ? 0d : i / (double)(count - 1);
            frequencies[i] = Math.Exp(logLow + (t * (logHigh - logLow)));
        }

        return frequencies;
    }

    internal static int ClampBandCount(int bandCount)
    {
        if (bandCount < MinimumBandCount)
        {
            return MinimumBandCount;
        }

        return bandCount > MaximumBandCount ? MaximumBandCount : bandCount;
    }

    /// <summary>
    /// Maps a decibel magnitude into 0…1 using <see cref="LevelFloorDb"/> and <paramref name="calibrationOffsetDb"/>.
    /// </summary>
    internal static double LevelFromDb(double dbfs, double calibrationOffsetDb)
    {
        double aligned = dbfs - calibrationOffsetDb;
        if (aligned <= LevelFloorDb)
        {
            return 0d;
        }

        if (aligned >= 0d)
        {
            return 1d;
        }

        return (aligned - LevelFloorDb) / -LevelFloorDb;
    }

    /// <summary>
    /// Clamps a normalized spectrum level into 0…1.
    /// </summary>
    internal static double ClampLevel(double level)
    {
        if (level < 0d)
        {
            return 0d;
        }

        return level > 1d ? 1d : level;
    }

    /// <summary>
    /// Clamps a gain into <paramref name="minimum"/>..<paramref name="maximum"/>.
    /// </summary>
    internal static double ClampGain(double gain, double minimum, double maximum)
    {
        if (gain < minimum)
        {
            return minimum;
        }

        return gain > maximum ? maximum : gain;
    }

    /// <summary>
    /// Maps gain into 0…1 inside the gain range (0.5 is unity).
    /// </summary>
    internal static double GainPosition(double gain, double minimum, double maximum)
    {
        double span = maximum - minimum;
        if (Math.Abs(span) < 0.0000001d)
        {
            return 0.5d;
        }

        double t = (gain - minimum) / span;
        if (t < 0d)
        {
            return 0d;
        }

        return t > 1d ? 1d : t;
    }

    /// <summary>
    /// Formats a centre frequency for band labels.
    /// </summary>
    internal static string FormatFrequency(double frequencyHz)
    {
        if (frequencyHz >= 1000d)
        {
            double kilo = frequencyHz / 1000d;
            if (Math.Abs(kilo - Math.Round(kilo)) < 0.05d)
            {
                return ((int)Math.Round(kilo)).ToString(CultureInfo.CurrentCulture) + "k";
            }

            return kilo.ToString("0.#", CultureInfo.CurrentCulture) + "k";
        }

        return Math.Round(frequencyHz).ToString("0", CultureInfo.CurrentCulture);
    }
}
