#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.Threading.Tasks;

namespace TestForm;

/// <summary>
/// Interactive validation for <see cref="KryptonLogger"/>, <see cref="CommonHelper.LogOutput"/>,
/// optional file logging, custom <see cref="IKryptonLogger"/>, and theme-swap WM tracing (Issue #3856).
/// </summary>
public partial class KryptonLoggerDemo : KryptonForm
{
    private int _messageCounter;

    public KryptonLoggerDemo()
    {
        InitializeComponent();
        RefreshEnvironmentInfo();
        AppendOutput("Ready. Set KRYPTON_LOG or KRYPTON_LOG_PATH before launching TestForm to exercise file logging.");
    }

    private void OnWriteKryptonLoggerClick(object? sender, EventArgs e)
    {
        var message = NextMessage("KryptonLogger.Write");
        KryptonLogger.Write(message);
        AppendOutput($"Sent: {message}");
        lblStatus.Values.Text = "Last action: KryptonLogger.Write (check Debug output and log file).";
    }

    private void OnWriteLogOutputClick(object? sender, EventArgs e)
    {
        var message = NextMessage("CommonHelper.LogOutput");
        CommonHelper.LogOutput(message);
        AppendOutput($"Sent: {message}");
        lblStatus.Values.Text = "Last action: CommonHelper.LogOutput (delegates to KryptonLogger).";
    }

    private void OnParallelStressClick(object? sender, EventArgs e)
    {
        lblStatus.Values.Text = "Running parallel stress (100 writes)...";
        Application.DoEvents();

        Parallel.For(0, 100, i => KryptonLogger.Write($"stress-{i:D3}"));

        AppendOutput("Parallel stress complete: 100 messages written from Parallel.For.");
        lblStatus.Values.Text = "Last action: parallel stress — inspect log file for 100 timestamped lines.";
    }

    private void OnReadLogFileClick(object? sender, EventArgs e)
    {
        var path = ResolveExpectedLogFilePath();
        if (path == null)
        {
            AppendOutput("No log file path is active. Set KRYPTON_LOG=1 or KRYPTON_LOG_PATH before starting TestForm.");
            lblStatus.Values.Text = "File logging is not enabled for this process.";
            return;
        }

        if (!File.Exists(path))
        {
            AppendOutput($"Log file not found yet: {path}");
            lblStatus.Values.Text = "Log file does not exist — write a message first.";
            return;
        }

        try
        {
            var lines = File.ReadAllLines(path);
            var start = Math.Max(0, lines.Length - 30);
            var tail = new string[lines.Length - start];
            Array.Copy(lines, start, tail, 0, tail.Length);
            AppendOutput($"--- tail of {path} ---");
            foreach (var line in tail)
            {
                AppendOutput(line);
            }

            AppendOutput("--- end of tail ---");
            lblStatus.Values.Text = $"Reloaded last {tail.Length} line(s) from the log file.";
        }
        catch (Exception ex)
        {
            AppendOutput($"Failed to read log file: {ex.Message}");
            lblStatus.Values.Text = "Failed to read log file.";
        }
    }

    private void OnClearOutputClick(object? sender, EventArgs e)
    {
        txtOutput.Clear();
        lblStatus.Values.Text = "Output cleared.";
    }

    private void OnCustomLoggerCheckedChanged(object? sender, EventArgs e)
    {
        if (chkCustomLogger.Checked)
        {
            KryptonLogger.SetLogger(new DemoLogger(AppendOutput));
            AppendOutput("Custom IKryptonLogger enabled — messages appear here only (not Debug/file).");
            lblStatus.Values.Text = "Custom logger active.";
        }
        else
        {
            KryptonLogger.SetLogger(null);
            AppendOutput("Custom logger disabled — restored built-in default logger.");
            lblStatus.Values.Text = "Built-in logger restored.";
        }
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        AppendOutput($"Theme changed to: {kryptonThemeComboBox1.Text}");
        lblStatus.Values.Text = "Theme changed — with file logging enabled, check for [WM] lines from theme-swap tracing.";
    }

    private void RefreshEnvironmentInfo()
    {
        lblKryptonLog.Values.Text = $"KRYPTON_LOG = {FormatEnvValue("KRYPTON_LOG")}";
        lblKryptonLogPath.Values.Text = $"KRYPTON_LOG_PATH = {FormatEnvValue("KRYPTON_LOG_PATH")}";
        lblKryptonLogWm.Values.Text = $"KRYPTON_LOG_WM = {FormatEnvValue("KRYPTON_LOG_WM")}";

        var defaultPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Krypton-Suite",
            "Toolkit",
            "Krypton.log");
        lblDefaultPath.Values.Text = $"Default file when KRYPTON_LOG is enabled: {defaultPath}";

        var activePath = ResolveExpectedLogFilePath();
        lblActivePath.Values.Text = activePath == null
            ? "Active log file: (none — Debug output only)"
            : $"Active log file: {activePath}";
    }

    private static string FormatEnvValue(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        return string.IsNullOrWhiteSpace(value) ? "(not set)" : value;
    }

    private static string? ResolveExpectedLogFilePath()
    {
        var explicitPath = Environment.GetEnvironmentVariable("KRYPTON_LOG_PATH");
        if (!string.IsNullOrWhiteSpace(explicitPath))
        {
            return explicitPath;
        }

        if (!IsTruthy(Environment.GetEnvironmentVariable("KRYPTON_LOG")))
        {
            return null;
        }

        explicitPath = Environment.GetEnvironmentVariable("KRYPTON_LOG_WM");
        if (!string.IsNullOrWhiteSpace(explicitPath))
        {
            return explicitPath;
        }

        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Krypton-Suite",
            "Toolkit",
            "Krypton.log");
    }

    private static bool IsTruthy(string? value) =>
        !string.IsNullOrWhiteSpace(value)
        && (string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "on", StringComparison.OrdinalIgnoreCase));

    private string NextMessage(string source) =>
        $"{source} #{++_messageCounter} @ {DateTime.Now:HH:mm:ss.fff}";

    private void AppendOutput(string line)
    {
        if (txtOutput.TextLength > 0)
        {
            txtOutput.AppendText(Environment.NewLine);
        }

        txtOutput.AppendText(line);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (chkCustomLogger.Checked)
        {
            KryptonLogger.SetLogger(null);
        }

        base.OnFormClosing(e);
    }

    private sealed class DemoLogger : IKryptonLogger
    {
        private readonly Action<string> _sink;

        public DemoLogger(Action<string> sink) => _sink = sink;

        public void Write(string message) => _sink($"[Custom IKryptonLogger] {message}");
    }
}
