#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.Threading.Tasks;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Interactive validation for the native <see cref="KryptonLog"/> stack (Issue #4223).
/// Distinct from <see cref="KryptonLoggerDemo"/> which exercises the toolkit <c>IKryptonLogger</c> hook (#3856).
/// </summary>
public sealed class Feature4223KryptonLogDemo : KryptonForm
{
    private readonly string _logFilePath;
    private readonly KryptonListBox _output;
    private readonly KryptonWrapLabel _status;
    private readonly KryptonTextBox _txtCategory;
    private readonly KryptonCheckBox _chkToolkitLogger;
    private readonly KryptonThemeComboBox _themeCombo;
    private int _counter;

    public Feature4223KryptonLogDemo()
    {
        Text = @"4223 — Native logging (KryptonLog)";
        Size = new Size(920, 720);
        StartPosition = FormStartPosition.CenterScreen;

        _logFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Krypton-Suite",
            "Toolkit",
            "Krypton-4223-demo.log");

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 88,
            Padding = new Padding(12),
            Text =
                "Issue #4223 (Krypton.Toolkit.Utilities): native KryptonLog — levels, named categories, rolling file, memory viewer.\r\n" +
                "Configure is opt-in. InstallAsToolkitLogger sends CommonHelper.LogOutput and theme-swap [WM] lines through this pipeline.\r\n" +
                "Open the viewer, write at each level, run parallel stress, then show an exception dialog with a log excerpt."
        };

        _status = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 36,
            Padding = new Padding(12, 6, 12, 6),
            Text = @"Ready."
        };

        _output = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };

        var filters = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12, 4, 12, 4),
            WrapContents = true
        };

        _themeCombo = new KryptonThemeComboBox
        {
            Width = 260,
            Margin = new Padding(4)
        };
        _themeCombo.SelectedIndexChanged += (_, _) =>
            Append($"Theme: {_themeCombo.Text}");
        filters.Controls.Add(_themeCombo);

        filters.Controls.Add(new KryptonWrapLabel
        {
            AutoSize = true,
            Text = @"Category:",
            Margin = new Padding(12, 8, 4, 4)
        });

        _txtCategory = new KryptonTextBox
        {
            Text = @"MyApp.Demo",
            Width = 180,
            Margin = new Padding(4)
        };
        filters.Controls.Add(_txtCategory);

        _chkToolkitLogger = new KryptonCheckBox
        {
            Checked = true,
            Margin = new Padding(12, 8, 4, 4)
        };
        _chkToolkitLogger.Values.Text = @"InstallAsToolkitLogger";
        _chkToolkitLogger.CheckedChanged += OnToolkitLoggerCheckedChanged;
        filters.Controls.Add(_chkToolkitLogger);

        var actions = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12, 4, 12, 8),
            WrapContents = true
        };

        actions.Controls.Add(CreateButton("Trace", (_, _) => Write(KryptonLogLevel.Trace)));
        actions.Controls.Add(CreateButton("Debug", (_, _) => Write(KryptonLogLevel.Debug)));
        actions.Controls.Add(CreateButton("Information", (_, _) => Write(KryptonLogLevel.Information)));
        actions.Controls.Add(CreateButton("Warning", (_, _) => Write(KryptonLogLevel.Warning)));
        actions.Controls.Add(CreateButton("Error + exception", (_, _) => WriteError()));
        actions.Controls.Add(CreateButton("Fatal", (_, _) => Write(KryptonLogLevel.Fatal)));
        actions.Controls.Add(CreateButton("Parallel stress", OnParallelStress));
        actions.Controls.Add(CreateButton("Open viewer", (_, _) => KryptonLogViewer.Show(this)));
        actions.Controls.Add(CreateButton("Exception dialog", OnExceptionDialog));
        actions.Controls.Add(CreateButton("CommonHelper.LogOutput", OnToolkitLogOutput));
        actions.Controls.Add(CreateButton("Clear output", (_, _) => _output.Items.Clear()));

        Controls.Add(_output);
        Controls.Add(actions);
        Controls.Add(filters);
        Controls.Add(instructions);
        Controls.Add(_status);

        ConfigurePipeline();
        if (_chkToolkitLogger.Checked)
        {
            KryptonLog.InstallAsToolkitLogger();
        }

        Append($"File: {_logFilePath}");
        _status.Text = @"Pipeline configured (Debug + file + memory + callback).";
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        KryptonLog.UninstallToolkitLogger();
        KryptonLog.CloseAndFlush();
        base.OnFormClosed(e);
    }

    private void ConfigurePipeline()
    {
        KryptonLog.Configure(cfg => cfg
            .MinimumLevel(KryptonLogLevel.Trace)
            .Override("Krypton.Toolkit", KryptonLogLevel.Debug)
            .WriteTo.Debug()
            .WriteTo.File(_logFilePath, rollOnSizeBytes: 1_000_000, retainedFileCount: 3)
            .WriteTo.Memory(2000)
            .WriteTo.Callback(evt =>
            {
                if (IsDisposed || !IsHandleCreated)
                {
                    return;
                }

                BeginInvoke(new Action(() =>
                    Append($"[{evt.Level}] {evt.Category} {evt.Message}")));
            })
            .Enrich.WithThreadId()
            .Async());
    }

    private IKryptonContextualLogger CurrentLogger() =>
        KryptonLog.ForContext(string.IsNullOrWhiteSpace(_txtCategory.Text) ? "MyApp.Demo" : _txtCategory.Text.Trim());

    private void Write(KryptonLogLevel level)
    {
        var n = ++_counter;
        CurrentLogger().Write(level, "Demo event {Number} at {Level}", n, level);
        _status.Text = $"Wrote {level} #{n}.";
    }

    private void WriteError()
    {
        var n = ++_counter;
        try
        {
            throw new InvalidOperationException($"Demo failure #{n}");
        }
        catch (Exception ex)
        {
            CurrentLogger().Error(ex, "Handled demo error {Number}", n);
            _status.Text = $"Wrote Error #{n} with exception.";
        }
    }

    private void OnParallelStress(object? sender, EventArgs e)
    {
        _status.Text = @"Running parallel stress (100 writes)…";
        Application.DoEvents();
        var log = CurrentLogger();
        Parallel.For(0, 100, i => log.Debug("stress-{Index:D3}", i));
        _status.Text = @"Parallel stress complete — open the viewer or inspect the log file.";
    }

    private void OnExceptionDialog(object? sender, EventArgs e)
    {
        try
        {
            throw new ApplicationException("Demo exception for KryptonExceptionDialog + recent log.");
        }
        catch (Exception ex)
        {
            KryptonExceptionDialog.Show(ex, new KryptonExceptionDialogOptions
            {
                ShowCopyButton = true,
                ShowSearchBox = true,
                IncludeRecentLog = true,
                ShowViewLogButton = true
            });
            _status.Text = @"Exception dialog closed.";
        }
    }

    private void OnToolkitLogOutput(object? sender, EventArgs e)
    {
        CommonHelper.LogOutput($"CommonHelper.LogOutput #{++_counter}");
        _status.Text = @"Sent CommonHelper.LogOutput (requires InstallAsToolkitLogger).";
    }

    private void OnToolkitLoggerCheckedChanged(object? sender, EventArgs e)
    {
        if (_chkToolkitLogger.Checked)
        {
            KryptonLog.InstallAsToolkitLogger();
            Append("InstallAsToolkitLogger enabled.");
        }
        else
        {
            KryptonLog.UninstallToolkitLogger();
            Append("Toolkit logger restored.");
        }
    }

    private static KryptonButton CreateButton(string text, EventHandler onClick)
    {
        var button = new KryptonButton
        {
            AutoSize = true,
            Margin = new Padding(4)
        };
        button.Values.Text = text;
        button.Click += onClick;
        return button;
    }

    private void Append(string line)
    {
        _output.Items.Add(line);
        _output.SelectedIndex = _output.Items.Count - 1;
    }
}
