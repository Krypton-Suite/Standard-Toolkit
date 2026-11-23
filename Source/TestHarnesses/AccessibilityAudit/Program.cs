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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccessibilityAudit;

internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        Environment.SetEnvironmentVariable("KRYPTON_HEADLESS", "1");
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        AppDomain.CurrentDomain.UnhandledException += (_, eventArgs) =>
        {
            AuditLog.Write("[FAIL] Unhandled " + eventArgs.ExceptionObject);
        };

        string? logPath = null;
        try
        {
            AuditOptions options = AuditOptions.Parse(args);
            logPath = options.ReportPath + ".log";
            using var log = new StreamWriter(new FileStream(logPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                AutoFlush = true
            };
            AuditLog.Writer = log;
            AuditLog.Write("[INFO] " + options.Suite + " audit started");
            using var dismisser = new ModalDismisser();

            if (options.Suite != AuditSuite.Accessibility)
            {
                SizeAuditResult sized = options.Suite == AuditSuite.Dpi ? DpiAudit.Run() : TouchAudit.Run();
                return FinishSizeAudit(options, sized);
            }

            ContrastMath.AssertSelfCheck();

            ContrastAuditResult contrast = ContrastAudit.Run();
            ControlAuditResult controls = ControlAudit.Run();

            BaselineFile baseline = File.Exists(options.BaselinePath)
                ? BaselineFile.Load(options.BaselinePath)
                : options.UpdateBaseline
                    ? new BaselineFile()
                    : throw new FileNotFoundException("Baseline file was not found.", options.BaselinePath);

            List<string> contrastNew = Except(contrast.FailureKeys, baseline.ContrastFailures);
            List<string> contrastStale = Except(baseline.ContrastFailures, contrast.FailureKeys);
            List<string> controlNew = Except(controls.FailureKeys, baseline.ControlFailures);
            List<string> controlStale = Except(baseline.ControlFailures, controls.FailureKeys);

            if (options.UpdateBaseline)
            {
                baseline.ContrastFailures = contrast.FailureKeys.ToList();
                baseline.ControlFailures = controls.FailureKeys.ToList();
                baseline.Save(options.BaselinePath);
            }

            bool failed = controls.HardRoleFailures.Count > 0
                || (!options.UpdateBaseline && (contrastNew.Count > 0 || controlNew.Count > 0));
            string report = BuildReport(contrast, controls, contrastNew, contrastStale, controlNew, controlStale, options.UpdateBaseline, failed);
            WriteText(options.ReportPath, report);
            WriteStepSummary(contrast, controls, contrastNew, contrastStale, controlNew, controlStale, failed);

            AuditLog.Write(failed
                ? "[FAIL] Accessibility audit found issues beyond the baseline."
                : "[PASS] Accessibility audit matched the baseline.");
            AuditLog.Write(
                $"contrast failures={contrast.FailureKeys.Count} new={contrastNew.Count} stale={contrastStale.Count}; " +
                $"control failures={controls.FailureKeys.Count} new={controlNew.Count} stale={controlStale.Count}; " +
                $"hard role failures={controls.HardRoleFailures.Count}; controls visited={controls.Visited}");

            return failed ? 1 : 0;
        }
        catch (Exception ex)
        {
            string failure = "[FAIL] " + ex + Environment.NewLine;
            AuditLog.Write(failure);
            if (!string.IsNullOrEmpty(logPath))
            {
                File.AppendAllText(logPath, failure);
            }

            return 1;
        }
    }

    private static int FinishSizeAudit(AuditOptions options, SizeAuditResult result)
    {
        KeyBaseline baseline = File.Exists(options.BaselinePath)
            ? KeyBaseline.Load(options.BaselinePath)
            : options.UpdateBaseline
                ? new KeyBaseline()
                : throw new FileNotFoundException("Baseline file was not found.", options.BaselinePath);

        List<string> fresh = Except(result.FailureKeys, baseline.Failures);
        List<string> stale = Except(baseline.Failures, result.FailureKeys);
        if (options.UpdateBaseline)
        {
            baseline.Failures = result.FailureKeys.ToList();
            baseline.Save(options.BaselinePath);
        }

        bool failed = result.HardFailures.Count > 0
            || (!options.UpdateBaseline && fresh.Count > 0);
        string report = BuildSizeReport(result, fresh, stale, options.UpdateBaseline, failed);
        WriteText(options.ReportPath, report);
        WriteSizeSummary(result, fresh, stale, failed);

        AuditLog.Write(failed
            ? "[FAIL] " + result.Title + " found issues beyond the baseline."
            : "[PASS] " + result.Title + " matched the baseline.");
        AuditLog.Write(
            $"failures={result.FailureKeys.Count} new={fresh.Count} stale={stale.Count}; " +
            $"hard failures={result.HardFailures.Count}; visited={result.Visited}; skipped={result.Skipped}");
        return failed ? 1 : 0;
    }

    private static string BuildSizeReport(
        SizeAuditResult result,
        List<string> fresh,
        List<string> stale,
        bool updatedBaseline,
        bool failed)
    {
        var report = new StringBuilder();
        report.Append("# ").AppendLine(result.Title);
        report.AppendLine();
        report.AppendLine(failed ? "Result: **FAIL**" : "Result: **PASS**");
        if (updatedBaseline)
        {
            report.AppendLine();
            report.AppendLine("Baseline file was rewritten from this run.");
        }

        report.AppendLine();
        report.AppendLine("## Summary");
        report.AppendLine();
        report.AppendLine($"- Controls visited: {result.Visited}");
        report.AppendLine($"- Skipped: {result.Skipped}");
        report.AppendLine($"- Failures: {result.FailureKeys.Count} (new {fresh.Count}, stale {stale.Count})");
        report.AppendLine($"- Hard failures: {result.HardFailures.Count}");
        report.AppendLine();
        if (result.Title.StartsWith("DPI", StringComparison.Ordinal))
        {
            report.AppendLine("Each control is scaled by 1.5. A positive size that stays below 1.2x is scale-ignored. This does not change the monitor DPI.");
        }
        else
        {
            report.AppendLine("Touch support is forced on with font scaling off and a 1.5 control scale. A runner without a digitizer is not a failure. Empty preferred sizes are skipped.");
        }

        report.AppendLine();
        AppendList(report, "Hard failures", result.HardFailures);
        AppendList(report, "New failures", fresh);
        AppendList(report, "Stale baseline keys (safe to delete)", stale);
        report.AppendLine("## Controls");
        report.AppendLine();
        foreach (string note in result.Notes)
        {
            report.AppendLine("- " + note);
        }

        report.AppendLine();
        return report.ToString();
    }

    private static void WriteSizeSummary(SizeAuditResult result, List<string> fresh, List<string> stale, bool failed)
    {
        string? summaryPath = Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY");
        if (string.IsNullOrEmpty(summaryPath))
        {
            return;
        }

        var summary = new StringBuilder();
        summary.AppendLine();
        summary.Append("### ").AppendLine(result.Title);
        summary.AppendLine();
        summary.AppendLine(failed ? "**FAIL**" : "**PASS**");
        summary.AppendLine();
        summary.AppendLine($"- Failures: {result.FailureKeys.Count} (new {fresh.Count}, stale {stale.Count})");
        summary.AppendLine($"- Hard failures: {result.HardFailures.Count}");
        summary.AppendLine($"- Controls visited: {result.Visited}");
        summary.AppendLine($"- Skipped: {result.Skipped}");
        AppendPreview(summary, "New failures", fresh);
        AppendPreview(summary, "Hard failures", result.HardFailures);
        File.AppendAllText(summaryPath, summary.ToString());
    }

    private static List<string> Except(IEnumerable<string> current, IEnumerable<string> allowed)
    {
        var allow = new HashSet<string>(allowed, StringComparer.Ordinal);
        return current.Where(key => !allow.Contains(key)).OrderBy(key => key, StringComparer.Ordinal).ToList();
    }

    private static string BuildReport(
        ContrastAuditResult contrast,
        ControlAuditResult controls,
        List<string> contrastNew,
        List<string> contrastStale,
        List<string> controlNew,
        List<string> controlStale,
        bool updatedBaseline,
        bool failed)
    {
        var report = new StringBuilder();
        report.AppendLine("# Accessibility audit");
        report.AppendLine();
        report.AppendLine(failed ? "Result: **FAIL**" : "Result: **PASS**");
        if (updatedBaseline)
        {
            report.AppendLine();
            report.AppendLine("Baseline file was rewritten from this run.");
        }

        report.AppendLine();
        report.AppendLine("## Summary");
        report.AppendLine();
        report.AppendLine($"- Themes scored: {contrast.ThemesScored} (Custom and ProfessionalSystem skipped)");
        report.AppendLine($"- Contrast pairs scored: {contrast.PairsScored}");
        report.AppendLine($"- Contrast pairs skipped (not opaque): {contrast.PairsSkippedNotOpaque}");
        report.AppendLine($"- Contrast pairs skipped (palette threw): {contrast.PairsSkippedThrew}");
        report.AppendLine($"- Contrast failures: {contrast.FailureKeys.Count} (new {contrastNew.Count}, stale {contrastStale.Count})");
        int disabledMisses = contrast.Misses.Count(sample => !sample.CountsAsFailure);
        report.AppendLine($"- Disabled pairs below threshold (reported, not failed): {disabledMisses}");
        report.AppendLine($"- Controls visited: {controls.Visited}");
        report.AppendLine($"- Control failures: {controls.FailureKeys.Count} (new {controlNew.Count}, stale {controlStale.Count})");
        report.AppendLine($"- Hard role failures: {controls.HardRoleFailures.Count}");
        report.AppendLine();
        report.AppendLine("This audit scores palette text contrast and in-process accessible objects. It does not drive Narrator, keyboard focus order, or a cross-process UI Automation tree.");
        report.AppendLine();

        AppendList(report, "Hard role failures", controls.HardRoleFailures);
        AppendList(report, "New contrast failures", contrastNew);
        AppendList(report, "New control failures", controlNew);
        AppendList(report, "Stale baseline keys (safe to delete)", contrastStale.Concat(controlStale).OrderBy(key => key, StringComparer.Ordinal));

        report.AppendLine("## Contrast misses");
        report.AppendLine();
        report.AppendLine("| Key | Ratio | Threshold | Fore | Back | Failed |");
        report.AppendLine("| --- | ---: | ---: | --- | --- | --- |");
        foreach (ContrastSample sample in contrast.Misses)
        {
            report.Append("| ").Append(sample.Key)
                .Append(" | ").Append(sample.Ratio.ToString("0.00"))
                .Append(" | ").Append(sample.Threshold.ToString("0.0"))
                .Append(" | ").Append(sample.Fore)
                .Append(" | ").Append(sample.Back)
                .Append(" | ").Append(sample.CountsAsFailure ? "yes" : "no")
                .AppendLine(" |");
        }

        report.AppendLine();
        AppendList(report, "Palette exceptions (first 20)", contrast.ThrowExamples);

        report.AppendLine("## Controls");
        report.AppendLine();
        foreach (string note in controls.Notes)
        {
            report.AppendLine("- " + note);
        }

        report.AppendLine();
        return report.ToString();
    }

    private static void AppendList(StringBuilder report, string title, IEnumerable<string> keys)
    {
        report.Append("## ").AppendLine(title);
        report.AppendLine();
        bool any = false;
        foreach (string key in keys)
        {
            any = true;
            report.Append("- ").AppendLine(key);
        }

        if (!any)
        {
            report.AppendLine("- none");
        }

        report.AppendLine();
    }

    private static void WriteStepSummary(
        ContrastAuditResult contrast,
        ControlAuditResult controls,
        List<string> contrastNew,
        List<string> contrastStale,
        List<string> controlNew,
        List<string> controlStale,
        bool failed)
    {
        string? summaryPath = Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY");
        if (string.IsNullOrEmpty(summaryPath))
        {
            return;
        }

        var summary = new StringBuilder();
        summary.AppendLine();
        summary.AppendLine("### Accessibility audit");
        summary.AppendLine();
        summary.AppendLine(failed ? "**FAIL**" : "**PASS**");
        summary.AppendLine();
        summary.AppendLine($"- Contrast failures: {contrast.FailureKeys.Count} (new {contrastNew.Count}, stale {contrastStale.Count})");
        summary.AppendLine($"- Control failures: {controls.FailureKeys.Count} (new {controlNew.Count}, stale {controlStale.Count})");
        summary.AppendLine($"- Hard role failures: {controls.HardRoleFailures.Count}");
        summary.AppendLine($"- Controls visited: {controls.Visited}");
        summary.AppendLine($"- Themes scored: {contrast.ThemesScored}");
        AppendPreview(summary, "New contrast keys", contrastNew);
        AppendPreview(summary, "New control keys", controlNew);
        AppendPreview(summary, "Hard role failures", controls.HardRoleFailures);
        File.AppendAllText(summaryPath, summary.ToString());
    }

    private static void AppendPreview(StringBuilder summary, string title, IReadOnlyList<string> keys)
    {
        if (keys.Count == 0)
        {
            return;
        }

        summary.AppendLine();
        summary.Append("#### ").AppendLine(title);
        int shown = Math.Min(keys.Count, 30);
        for (int index = 0; index < shown; index++)
        {
            summary.Append("- ").AppendLine(keys[index]);
        }

        if (keys.Count > shown)
        {
            summary.Append("- … ").Append(keys.Count - shown).AppendLine(" more");
        }
    }

    private static void WriteText(string path, string contents)
    {
        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string text = contents.Replace("\r\n", "\n").Replace("\n", "\r\n");
        File.WriteAllText(path, text, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
    }
}

internal static class AuditLog
{
    public static TextWriter? Writer { get; set; }

    public static void Write(string message)
    {
        try
        {
            Console.WriteLine(message);
        }
        catch (Exception)
        {
            // WinExe has no console unless a parent attached one.
        }

        try
        {
            Writer?.WriteLine(message);
        }
        catch (Exception)
        {
            // The progress log is best-effort.
        }
    }
}

internal sealed class AuditOptions
{
    private AuditOptions(string baselinePath, string reportPath, bool updateBaseline, AuditSuite suite)
    {
        BaselinePath = baselinePath;
        ReportPath = reportPath;
        UpdateBaseline = updateBaseline;
        Suite = suite;
    }

    public string BaselinePath { get; }

    public string ReportPath { get; }

    public bool UpdateBaseline { get; }

    public AuditSuite Suite { get; }

    public static AuditOptions Parse(string[] args)
    {
        string? baseline = null;
        string? report = null;
        bool update = false;
        AuditSuite suite = AuditSuite.Accessibility;
        const string usage = "Usage: AccessibilityAudit --baseline <path> --report <path> [--suite accessibility|dpi|touch] [--update-baseline]";

        for (int index = 0; index < args.Length; index++)
        {
            string arg = args[index];
            if (string.Equals(arg, "--update-baseline", StringComparison.Ordinal))
            {
                update = true;
                continue;
            }

            if (string.Equals(arg, "--baseline", StringComparison.Ordinal) && index + 1 < args.Length)
            {
                baseline = args[++index];
                continue;
            }

            if (string.Equals(arg, "--report", StringComparison.Ordinal) && index + 1 < args.Length)
            {
                report = args[++index];
                continue;
            }

            if (string.Equals(arg, "--suite", StringComparison.Ordinal) && index + 1 < args.Length)
            {
                suite = ParseSuite(args[++index], usage);
                continue;
            }

            throw new ArgumentException(usage);
        }

        if (string.IsNullOrWhiteSpace(baseline) || string.IsNullOrWhiteSpace(report))
        {
            throw new ArgumentException(usage);
        }

        return new AuditOptions(baseline, report, update, suite);
    }

    private static AuditSuite ParseSuite(string value, string usage)
    {
        if (string.Equals(value, "accessibility", StringComparison.OrdinalIgnoreCase))
        {
            return AuditSuite.Accessibility;
        }

        if (string.Equals(value, "dpi", StringComparison.OrdinalIgnoreCase))
        {
            return AuditSuite.Dpi;
        }

        if (string.Equals(value, "touch", StringComparison.OrdinalIgnoreCase))
        {
            return AuditSuite.Touch;
        }

        throw new ArgumentException(usage);
    }
}

internal enum AuditSuite
{
    Accessibility,
    Dpi,
    Touch
}
