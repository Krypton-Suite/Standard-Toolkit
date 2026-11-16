#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Krypton.Toolkit;

namespace AccessibilityAudit;

internal sealed class ContrastSample
{
    public ContrastSample(string key, string theme, string style, string state, double ratio, double threshold, string fore, string back, bool countsAsFailure)
    {
        Key = key;
        Theme = theme;
        Style = style;
        State = state;
        Ratio = ratio;
        Threshold = threshold;
        Fore = fore;
        Back = back;
        CountsAsFailure = countsAsFailure;
    }

    public string Key { get; }

    public string Theme { get; }

    public string Style { get; }

    public string State { get; }

    public double Ratio { get; }

    public double Threshold { get; }

    public string Fore { get; }

    public string Back { get; }

    public bool CountsAsFailure { get; }
}

internal sealed class ContrastAuditResult
{
    public int ThemesScored { get; set; }

    public int PairsScored { get; set; }

    public int PairsSkippedNotOpaque { get; set; }

    public int PairsSkippedThrew { get; set; }

    public List<string> ThrowExamples { get; } = new List<string>();

    public List<ContrastSample> Misses { get; } = new List<ContrastSample>();

    public List<string> FailureKeys { get; } = new List<string>();
}

internal static class ContrastAudit
{
    private static readonly PaletteState[] ScoredStates =
    {
        PaletteState.Normal,
        PaletteState.Tracking,
        PaletteState.Pressed,
        PaletteState.CheckedNormal,
        PaletteState.CheckedTracking,
        PaletteState.CheckedPressed,
        PaletteState.ContextNormal,
        PaletteState.ContextTracking,
        PaletteState.ContextPressed,
        PaletteState.ContextCheckedNormal,
        PaletteState.ContextCheckedTracking
    };

    internal static ContrastAuditResult Run()
    {
        var result = new ContrastAuditResult();
        List<string> styles = PairedStyleNames();

        foreach (KeyValuePair<string, PaletteMode> theme in PaletteModeStrings.SupportedThemesMap)
        {
            PaletteMode mode = theme.Value;
            if (mode == PaletteMode.Custom || mode == PaletteMode.ProfessionalSystem)
            {
                continue;
            }

            PaletteBase palette = KryptonManager.GetPaletteForMode(mode);
            result.ThemesScored++;
            string themeName = mode.ToString();
            AuditLog.Write("[INFO] Contrast " + themeName);

            foreach (string styleName in styles)
            {
                var backStyle = (PaletteBackStyle)Enum.Parse(typeof(PaletteBackStyle), styleName);
                var contentStyle = (PaletteContentStyle)Enum.Parse(typeof(PaletteContentStyle), styleName);

                foreach (PaletteState state in ScoredStates)
                {
                    Score(result, palette, themeName, styleName, backStyle, contentStyle, state, countsAsFailure: true);
                }

                Score(result, palette, themeName, styleName, backStyle, contentStyle, PaletteState.Disabled, countsAsFailure: false);
            }
        }

        result.FailureKeys.Sort(StringComparer.Ordinal);
        result.Misses.Sort((left, right) => StringComparer.Ordinal.Compare(left.Key, right.Key));
        AuditLog.Write("[INFO] Contrast finished failures=" + result.FailureKeys.Count + " scored=" + result.PairsScored);
        return result;
    }

    private static void Score(
        ContrastAuditResult result,
        PaletteBase palette,
        string themeName,
        string styleName,
        PaletteBackStyle backStyle,
        PaletteContentStyle contentStyle,
        PaletteState state,
        bool countsAsFailure)
    {
        Color fore;
        Color back1;
        Color back2;
        try
        {
            fore = palette.GetContentShortTextColor1(contentStyle, state);
            back1 = palette.GetBackColor1(backStyle, state);
            back2 = palette.GetBackColor2(backStyle, state);
        }
        catch (Exception ex)
        {
            result.PairsSkippedThrew++;
            RememberThrow(result, themeName, styleName, state, ex);
            return;
        }

        if (!ContrastMath.IsOpaque(fore))
        {
            result.PairsSkippedNotOpaque++;
            return;
        }

        double? worst = null;
        Color worstBack = Color.Empty;
        Consider(fore, back1, ref worst, ref worstBack);
        Consider(fore, back2, ref worst, ref worstBack);
        if (worst == null)
        {
            result.PairsSkippedNotOpaque++;
            return;
        }

        result.PairsScored++;
        double threshold = ThresholdFor(palette, contentStyle, state);
        if (worst.Value + 0.0001 >= threshold)
        {
            return;
        }

        string key = themeName + "|" + styleName + "|" + state;
        var sample = new ContrastSample(
            key,
            themeName,
            styleName,
            state.ToString(),
            worst.Value,
            threshold,
            ContrastMath.ToHex(fore),
            ContrastMath.ToHex(worstBack),
            countsAsFailure);
        result.Misses.Add(sample);
        if (countsAsFailure)
        {
            result.FailureKeys.Add(key);
        }
    }

    private static void Consider(Color fore, Color back, ref double? worst, ref Color worstBack)
    {
        if (!ContrastMath.IsOpaque(back))
        {
            return;
        }

        double ratio = ContrastMath.ContrastRatio(fore, back);
        if (worst == null || ratio < worst.Value)
        {
            worst = ratio;
            worstBack = back;
        }
    }

    private static double ThresholdFor(PaletteBase palette, PaletteContentStyle style, PaletteState state)
    {
        try
        {
            Font? font = palette.GetContentShortTextFont(style, state);
            if (font != null && ContrastMath.IsLargeText(font))
            {
                return ContrastMath.LargeTextThreshold;
            }
        }
        catch (Exception)
        {
            // This style has no short-text font. Score it as normal text.
        }

        return ContrastMath.NormalTextThreshold;
    }

    private static void RememberThrow(ContrastAuditResult result, string theme, string style, PaletteState state, Exception ex)
    {
        if (result.ThrowExamples.Count >= 20)
        {
            return;
        }

        string example = theme + "|" + style + "|" + state + "|" + ex.GetType().Name;
        if (!result.ThrowExamples.Contains(example))
        {
            result.ThrowExamples.Add(example);
        }
    }

    private static List<string> PairedStyleNames()
    {
        var contentNames = new HashSet<string>(Enum.GetNames(typeof(PaletteContentStyle)), StringComparer.Ordinal);
        return Enum.GetNames(typeof(PaletteBackStyle))
            .Where(contentNames.Contains)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToList();
    }
}
