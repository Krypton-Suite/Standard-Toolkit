#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public static class ThemeColorGridBuilder
{
    public static Color[][] BuildThemeColorColumns(Color[]? schemeColors, ThemeColorSortMode sortMode, int columns = 16)
    {
        if ((schemeColors == null) || (schemeColors.Length == 0))
        {
            return Array.Empty<Color[]>();
        }

        var filtered = schemeColors.Where(static c => !c.IsEmpty && (c.A == 255))
                                   .GroupBy(static c => c.ToArgb())
                                   .Select(static g => g.First())
                                   .ToArray();

        var columnsList = new List<Color>[columns];

        if (sortMode == ThemeColorSortMode.HSB)
        {
            const double neutralSatThreshold = 0.18;
            const int greyDeltaThreshold = 28;

            static bool IsNeutral(Color c)
            {
                var max = Math.Max(c.R, Math.Max(c.G, c.B));
                var min = Math.Min(c.R, Math.Min(c.G, c.B));
                var spreadIsGrey = (max - min) <= greyDeltaThreshold;
                var sat = c.GetSaturation();
                var bri = c.GetBrightness();
                var isLowSat = sat <= neutralSatThreshold;
                var isNearWhiteTint = (bri >= 0.92f) && (sat <= 0.35f);
                return isLowSat || spreadIsGrey || isNearWhiteTint;
            }

            var neutrals = filtered.Where(IsNeutral)
                                   .OrderBy(static c => c.GetBrightness())
                                   .ToList();

            var chroma = filtered.Where(static c => !IsNeutral(c)).ToList();

            var hueBins = new List<Color>[columns];
            for (var i = 0; i < columns; i++) hueBins[i] = new List<Color>();

            foreach (var color in chroma)
            {
                var hue = color.GetHue();
                var bin = (int)Math.Min(columns - 1, Math.Floor(hue / (360.0 / columns)));
                hueBins[bin].Add(color);
            }

            for (var i = 0; i < columns; i++)
            {
                hueBins[i] = hueBins[i]
                    .OrderByDescending(static c => c.GetBrightness())
                    .ThenBy(static c => c.GetSaturation())
                    .ToList();
            }

            var orderedChroma = new List<Color>(filtered.Length);
            for (var i = 0; i < columns; i++) orderedChroma.AddRange(hueBins[i]);

            var total = orderedChroma.Count + neutrals.Count;
            var rowsTarget = (int)Math.Ceiling(total / (double)columns);

            var neutralsColumns = (int)Math.Ceiling(neutrals.Count / (double)rowsTarget);
            var chromaColumns = columns - neutralsColumns;
            var minChromaCols = (int)Math.Ceiling(orderedChroma.Count / (double)rowsTarget);
            if (chromaColumns < minChromaCols)
            {
                chromaColumns = Math.Min(columns, minChromaCols);
                neutralsColumns = Math.Max(0, columns - chromaColumns);
            }

            var offsetChroma = 0;
            for (var c = 0; c < chromaColumns; c++)
            {
                var remaining = orderedChroma.Count - offsetChroma;
                var take = Math.Min(rowsTarget, Math.Max(remaining, 0));
                columnsList[c] = take > 0 ? orderedChroma.Skip(offsetChroma).Take(take).ToList() : new List<Color>();
                offsetChroma += take;
            }

            var offsetNeutral = 0;
            for (var c = chromaColumns; c < columns; c++)
            {
                var remaining = neutrals.Count - offsetNeutral;
                var take = Math.Min(rowsTarget, Math.Max(remaining, 0));
                columnsList[c] = take > 0 ? neutrals.Skip(offsetNeutral).Take(take).ToList() : new List<Color>();
                offsetNeutral += take;
            }
        }
        else if (sortMode == ThemeColorSortMode.OKLCH)
        {
            const double neutralCThreshold = 0.04;

            var lchList = filtered.Select(static c => new { Color = c, Lch = ToOkLch(c) }).ToList();

            var neutrals = lchList.Where(static x => x.Lch.C <= neutralCThreshold)
                                   .OrderBy(static x => x.Lch.L)
                                   .Select(static x => x.Color)
                                   .ToList();

            var chroma = lchList.Where(static x => x.Lch.C > neutralCThreshold).ToList();

            var hueBins = new List<(Color Color, OkLch Lch)>[columns];
            for (var i = 0; i < columns; i++) hueBins[i] = new List<(Color, OkLch)>();

            foreach (var entry in chroma)
            {
                var bin = (int)Math.Min(columns - 1, Math.Floor(entry.Lch.H / (360.0 / columns)));
                hueBins[bin].Add((entry.Color, entry.Lch));
            }

            for (var i = 0; i < columns; i++)
            {
                hueBins[i] = hueBins[i]
                    .OrderByDescending(static t => t.Lch.L)
                    .ThenByDescending(static t => t.Lch.C)
                    .ToList();
            }

            var orderedChroma = new List<Color>(filtered.Length);
            for (var i = 0; i < columns; i++) orderedChroma.AddRange(hueBins[i].Select(static t => t.Color));

            var total = orderedChroma.Count + neutrals.Count;
            var rowsTarget = (int)Math.Ceiling(total / (double)columns);

            var neutralsColumns = (int)Math.Ceiling(neutrals.Count / (double)rowsTarget);
            var chromaColumns = columns - neutralsColumns;
            var minChromaCols = (int)Math.Ceiling(orderedChroma.Count / (double)rowsTarget);
            if (chromaColumns < minChromaCols)
            {
                chromaColumns = Math.Min(columns, minChromaCols);
                neutralsColumns = Math.Max(0, columns - chromaColumns);
            }

            var offsetChroma = 0;
            for (var c = 0; c < chromaColumns; c++)
            {
                var remaining = orderedChroma.Count - offsetChroma;
                var take = Math.Min(rowsTarget, Math.Max(remaining, 0));
                columnsList[c] = take > 0 ? orderedChroma.Skip(offsetChroma).Take(take).ToList() : new List<Color>();
                offsetChroma += take;
            }

            var offsetNeutral = 0;
            for (var c = chromaColumns; c < columns; c++)
            {
                var remaining = neutrals.Count - offsetNeutral;
                var take = Math.Min(rowsTarget, Math.Max(remaining, 0));
                columnsList[c] = take > 0 ? neutrals.Skip(offsetNeutral).Take(take).ToList() : new List<Color>();
                offsetNeutral += take;
            }
        }
        else
        {
            var ordered = filtered.OrderBy(static c => c.R)
                                  .ThenBy(static c => c.G)
                                  .ThenBy(static c => c.B)
                                  .ToArray();

            var total = ordered.Length;
            var rowsTarget = (int)Math.Ceiling(total / (double)columns);
            var offset = 0;
            for (var c = 0; c < columns; c++)
            {
                var remaining = total - offset;
                var take = Math.Min(rowsTarget, Math.Max(remaining, 0));
                columnsList[c] = take > 0 ? ordered.Skip(offset).Take(take).ToList() : new List<Color>();
                offset += take;
            }
        }

        var rows = columnsList.Max(static list => list?.Count ?? 0);
        var custom = new Color[columns][];
        for (var c = 0; c < columns; c++)
        {
            var colList = columnsList[c] ?? new List<Color>();
            custom[c] = new Color[rows];
            for (var r = 0; r < rows; r++)
            {
                custom[c][r] = r < colList.Count ? colList[r] : Color.Empty;
            }
        }

        return custom;
    }

    private struct OkLch
    {
        public double L;
        public double C;
        public double H;
    }

    private static OkLch ToOkLch(Color color)
    {
        double sr = color.R / 255.0;
        double sg = color.G / 255.0;
        double sb = color.B / 255.0;

        double r = sr <= 0.04045 ? sr / 12.92 : Math.Pow((sr + 0.055) / 1.055, 2.4);
        double g = sg <= 0.04045 ? sg / 12.92 : Math.Pow((sg + 0.055) / 1.055, 2.4);
        double b = sb <= 0.04045 ? sb / 12.92 : Math.Pow((sb + 0.055) / 1.055, 2.4);

        double l = 0.4122214708 * r + 0.5363325363 * g + 0.0514459929 * b;
        double m = 0.2119034982 * r + 0.6806995451 * g + 0.1073969566 * b;
        double s = 0.0883024619 * r + 0.2817188376 * g + 0.6299787005 * b;

        double l_ = l > 0 ? Math.Pow(l, 1.0 / 3.0) : 0;
        double m_ = m > 0 ? Math.Pow(m, 1.0 / 3.0) : 0;
        double s_ = s > 0 ? Math.Pow(s, 1.0 / 3.0) : 0;

        double L = 0.2104542553 * l_ + 0.7936177850 * m_ - 0.0040720468 * s_;
        double A = 1.9779984951 * l_ - 2.4285922050 * m_ + 0.4505937099 * s_;
        double B = 0.0259040371 * l_ + 0.7827717662 * m_ - 0.8086757660 * s_;

        double C = Math.Sqrt(A * A + B * B);
        double H = Math.Atan2(B, A) * (180.0 / Math.PI);
        if (H < 0)
        {
            H += 360.0;
        }

        return new OkLch { L = L, C = C, H = H };
    }
}