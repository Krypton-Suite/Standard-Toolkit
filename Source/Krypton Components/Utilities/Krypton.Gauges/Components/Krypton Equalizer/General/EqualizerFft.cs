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
/// Small in-place radix-2 FFT used by equalizer capture. No external DSP package.
/// </summary>
internal static class EqualizerFft
{
    /// <summary>
    /// Preferred analysis window length (power of two).
    /// </summary>
    internal const int WindowSize = 2048;

    /// <summary>
    /// Runs an in-place Cooley–Tukey FFT on interleaved complex data
    /// (<paramref name="real"/> / <paramref name="imag"/> of length <paramref name="n"/>).
    /// </summary>
    internal static void Forward(double[] real, double[] imag, int n)
    {
        if (n < 2 || (n & (n - 1)) != 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "FFT length must be a power of two.");
        }

        int bits = 0;
        for (int temp = n; temp > 1; temp >>= 1)
        {
            bits++;
        }

        for (int i = 0; i < n; i++)
        {
            int j = ReverseBits(i, bits);
            if (j > i)
            {
                double tr = real[i];
                real[i] = real[j];
                real[j] = tr;
                double ti = imag[i];
                imag[i] = imag[j];
                imag[j] = ti;
            }
        }

        for (int len = 2; len <= n; len <<= 1)
        {
            int half = len >> 1;
            double angle = -2d * Math.PI / len;
            double wR = Math.Cos(angle);
            double wI = Math.Sin(angle);
            for (int i = 0; i < n; i += len)
            {
                double cR = 1d;
                double cI = 0d;
                for (int j = 0; j < half; j++)
                {
                    int even = i + j;
                    int odd = even + half;
                    double oR = (real[odd] * cR) - (imag[odd] * cI);
                    double oI = (real[odd] * cI) + (imag[odd] * cR);
                    real[odd] = real[even] - oR;
                    imag[odd] = imag[even] - oI;
                    real[even] += oR;
                    imag[even] += oI;
                    double nextR = (cR * wR) - (cI * wI);
                    cI = (cR * wI) + (cI * wR);
                    cR = nextR;
                }
            }
        }
    }

    /// <summary>
    /// Applies a Hann window to <paramref name="samples"/>.
    /// </summary>
    internal static void ApplyHann(double[] samples, int count)
    {
        if (count <= 1)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            double w = 0.5d * (1d - Math.Cos((2d * Math.PI * i) / (count - 1)));
            samples[i] *= w;
        }
    }

    /// <summary>
    /// Maps FFT magnitudes into log-spaced band levels (0…1).
    /// </summary>
    internal static void MapToBands(double[] real, double[] imag, int n, int sampleRate,
        double[] frequencies, double calibrationOffsetDb, double[] levels)
    {
        int bandCount = frequencies.Length;
        if (bandCount == 0 || levels.Length < bandCount || sampleRate <= 0)
        {
            return;
        }

        int bins = n / 2;
        double binHz = sampleRate / (double)n;
        for (int band = 0; band < bandCount; band++)
        {
            double low = band == 0
                ? frequencies[0] / Math.Sqrt(2d)
                : Math.Sqrt(frequencies[band - 1] * frequencies[band]);
            double high = band == bandCount - 1
                ? frequencies[band] * Math.Sqrt(2d)
                : Math.Sqrt(frequencies[band] * frequencies[band + 1]);

            int start = (int)Math.Floor(low / binHz);
            int end = (int)Math.Ceiling(high / binHz);
            if (start < 1)
            {
                start = 1;
            }

            if (end > bins)
            {
                end = bins;
            }

            if (end < start)
            {
                end = start;
            }

            double sum = 0d;
            int used = 0;
            for (int bin = start; bin <= end; bin++)
            {
                double re = real[bin];
                double im = imag[bin];
                sum += (re * re) + (im * im);
                used++;
            }

            if (used == 0)
            {
                levels[band] = 0d;
                continue;
            }

            double magnitude = Math.Sqrt(sum / used) / n;
            if (magnitude < 0.000000001d)
            {
                levels[band] = 0d;
                continue;
            }

            double dbfs = 20d * Math.Log10(magnitude);
            levels[band] = EqualizerScale.LevelFromDb(dbfs, calibrationOffsetDb);
        }
    }

    private static int ReverseBits(int value, int bits)
    {
        int result = 0;
        for (int i = 0; i < bits; i++)
        {
            result = (result << 1) | (value & 1);
            value >>= 1;
        }

        return result;
    }
}
