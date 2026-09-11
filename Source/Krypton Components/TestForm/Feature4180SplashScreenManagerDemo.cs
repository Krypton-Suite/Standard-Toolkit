#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Threading;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demo for Issue #4180: non-blocking themed splash screen manager in Krypton.Toolkit.Utilities.
/// </summary>
public sealed class Feature4180SplashScreenManagerDemo : KryptonForm
{
    private readonly KryptonCheckBox _chkFade;
    private readonly KryptonCheckBox _chkProgress;
    private readonly KryptonCheckBox _chkCopyright;
    private readonly KryptonCheckBox _chkSemiTransparent;
    private readonly KryptonCheckBox _chkBackground;
    private readonly KryptonCheckBox _chkExceptionDialog;
    private readonly KryptonComboBox _cmbBorderAnimation;
    private readonly KryptonListBox _log;
    private readonly KryptonWrapLabel _status;
    private readonly KryptonThemeComboBox _themeCombo;

    public Feature4180SplashScreenManagerDemo()
    {
        Text = @"4180 — Splash Screen Manager";
        Size = new Size(860, 680);
        StartPosition = FormStartPosition.CenterScreen;

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 96,
            Padding = new Padding(12),
            Text =
                "Issue #4180 (Krypton.Toolkit.Utilities): non-blocking splash on a dedicated STA thread.\r\n" +
                "The splash keeps fading and painting while this form is blocked (Thread.Sleep). Distinct from the modal Toolkit Splash Screen demo.\r\n" +
                "Try blocking work, Run(steps), a failing step, background + opacity, theme changes, and Pulse/Sweep animated borders."
        };

        _status = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 36,
            Padding = new Padding(12, 6, 12, 6),
            Text = @"Ready."
        };

        _log = new KryptonListBox
        {
            Dock = DockStyle.Bottom,
            Height = 160
        };

        var options = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12, 4, 12, 4),
            WrapContents = true
        };

        _themeCombo = new KryptonThemeComboBox
        {
            Width = 280,
            Margin = new Padding(4)
        };
        options.Controls.Add(_themeCombo);

        _chkFade = CreateCheck("Fade in/out", true);
        _chkProgress = CreateCheck("Progress bar", true);
        _chkCopyright = CreateCheck("Copyright", false);
        _chkSemiTransparent = CreateCheck("Semi-transparent (70%)", false);
        _chkBackground = CreateCheck("Background image", false);
        _chkExceptionDialog = CreateCheck("Show exception dialog", true);
        _cmbBorderAnimation = new KryptonComboBox
        {
            Width = 180,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Margin = new Padding(8, 6, 8, 6)
        };
        _cmbBorderAnimation.Items.Add(KryptonSplashBorderAnimation.None);
        _cmbBorderAnimation.Items.Add(KryptonSplashBorderAnimation.Pulse);
        _cmbBorderAnimation.Items.Add(KryptonSplashBorderAnimation.Sweep);
        _cmbBorderAnimation.SelectedIndex = 0;
        options.Controls.Add(_chkFade);
        options.Controls.Add(_chkProgress);
        options.Controls.Add(_chkCopyright);
        options.Controls.Add(_chkSemiTransparent);
        options.Controls.Add(_chkBackground);
        options.Controls.Add(_chkExceptionDialog);
        options.Controls.Add(_cmbBorderAnimation);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            WrapContents = true,
            AutoScroll = true
        };

        buttons.Controls.Add(CreateButton("Blocking work (live status)", OnBlockingWork));
        buttons.Controls.Add(CreateButton("Run(steps) auto-progress", OnRunSteps));
        buttons.Controls.Add(CreateButton("Throw in a step", OnThrowInStep));
        buttons.Controls.Add(CreateButton("Background + opacity", OnBackgroundOpacity));
        buttons.Controls.Add(CreateButton("Explicit SetProgress", OnExplicitProgress));
        buttons.Controls.Add(CreateButton("Clear log", (_, _) =>
        {
            _log.Items.Clear();
            SetStatus("Log cleared.");
        }));

        Controls.Add(buttons);
        Controls.Add(options);
        Controls.Add(_log);
        Controls.Add(_status);
        Controls.Add(instructions);
    }

    private void OnBlockingWork(object? sender, EventArgs e)
    {
        KryptonSplashScreenManagerData data = CreateData();
        data.ExpectedStepCount = 5;
        data.MinimumDisplayMilliseconds = 400;
        using var splash = KryptonSplashScreenManager.Show(data);
        for (int i = 1; i <= 5; i++)
        {
            splash.SetStatus($"Loading module {i} of 5…");
            Thread.Sleep(700);
        }

        SetStatus("Blocking work finished — splash closed. This form was frozen; the splash kept painting.");
    }

    private void OnRunSteps(object? sender, EventArgs e)
    {
        KryptonSplashScreenManagerData data = CreateData();
        data.MinimumDisplayMilliseconds = 400;
        var steps = new List<KryptonSplashStep>
        {
            new("Reading configuration…", () => Thread.Sleep(500)),
            new("Connecting to services…", () => Thread.Sleep(500)),
            new("Loading plugins…", () => Thread.Sleep(500)),
            new("Preparing main window…", () => Thread.Sleep(500))
        };
        KryptonSplashScreenManager.Run(data, steps);
        SetStatus("Run(steps) completed. Progress advanced from the step count.");
    }

    private void OnThrowInStep(object? sender, EventArgs e)
    {
        KryptonSplashScreenManagerData data = CreateData();
        data.MinimumDisplayMilliseconds = 200;
        var steps = new List<KryptonSplashStep>
        {
            new("Connecting…", () => Thread.Sleep(400)),
            new("This step fails", () => throw new InvalidOperationException("Simulated startup failure (#4180).")),
            new("Never reached", () => { })
        };
        KryptonSplashScreenManager.Run(data, steps);
        SetStatus("Startup exception was reported; splash closed before the exception dialog.");
    }

    private void OnBackgroundOpacity(object? sender, EventArgs e)
    {
        KryptonSplashScreenManagerData data = CreateData();
        data.Opacity = 0.82;
        data.BackgroundImage = CreateBackgroundImage();
        data.BackgroundImageLayout = ImageLayout.Stretch;
        data.ExpectedStepCount = 3;
        data.MinimumDisplayMilliseconds = 500;
        using var splash = KryptonSplashScreenManager.Show(data);
        splash.SetStatus("Applying themed background…");
        Thread.Sleep(600);
        splash.SetStatus("Fading semi-transparent chrome…");
        Thread.Sleep(600);
        splash.SetStatus("Ready.");
        Thread.Sleep(400);
        SetStatus("Background image + opacity splash closed.");
    }

    private void OnExplicitProgress(object? sender, EventArgs e)
    {
        KryptonSplashScreenManagerData data = CreateData();
        data.ExpectedStepCount = null;
        data.ShowProgressBar = true;
        data.MinimumDisplayMilliseconds = 300;
        using var splash = KryptonSplashScreenManager.Show(data);
        splash.SetStatus("Downloading…");
        for (int value = 0; value <= 100; value += 10)
        {
            splash.SetProgress(value);
            Thread.Sleep(180);
        }

        SetStatus("Explicit SetProgress finished.");
    }

    private KryptonSplashScreenManagerData CreateData()
    {
        var data = new KryptonSplashScreenManagerData
        {
            Title = @"Krypton TestForm",
            Status = @"Starting…",
            Assembly = typeof(Feature4180SplashScreenManagerDemo).Assembly,
            ShowApplicationName = true,
            ShowVersion = true,
            ShowCopyright = _chkCopyright.Checked,
            ShowProgressBar = _chkProgress.Checked,
            ShowExceptionDialog = _chkExceptionDialog.Checked,
            FadeIn = _chkFade.Checked,
            FadeOut = _chkFade.Checked,
            FadeSpeed = FadeSpeedChoice.Fast,
            Opacity = _chkSemiTransparent.Checked ? 0.7 : 1.0,
            BorderAnimation = _cmbBorderAnimation.SelectedIndex switch
            {
                1 => KryptonSplashBorderAnimation.Pulse,
                2 => KryptonSplashBorderAnimation.Sweep,
                _ => KryptonSplashBorderAnimation.None
            },
            PaletteMode = KryptonManager.CurrentGlobalPaletteMode,
            Logger = new DemoSplashLogger(message => OnLog("[logger] " + message)),
            LogCallback = message => OnLog("[callback] " + message)
        };

        if (_chkBackground.Checked)
        {
            data.BackgroundImage = CreateBackgroundImage();
        }

        return data;
    }

    private void OnLog(string message)
    {
        if (IsDisposed)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(new Action<string>(OnLog), message);
            return;
        }

        _log.Items.Add($"[{DateTime.Now:HH:mm:ss.fff}] {message}");
        _log.SelectedIndex = _log.Items.Count - 1;
    }

    private void SetStatus(string text) => _status.Text = text;

    private static KryptonCheckBox CreateCheck(string text, bool isChecked) =>
        new()
        {
            Text = text,
            Checked = isChecked,
            Margin = new Padding(8, 6, 8, 6),
            AutoSize = true
        };

    private static KryptonButton CreateButton(string text, EventHandler onClick)
    {
        var button = new KryptonButton
        {
            Text = text,
            Size = new Size(240, 40),
            Margin = new Padding(4)
        };
        button.Click += onClick;
        return button;
    }

    private static Image CreateBackgroundImage()
    {
        var bitmap = new Bitmap(520, 320);
        using (var graphics = Graphics.FromImage(bitmap))
        using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                   new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                   Color.FromArgb(40, 80, 140),
                   Color.FromArgb(20, 40, 70),
                   90f))
        {
            graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
        }

        return bitmap;
    }

    private sealed class DemoSplashLogger : IKryptonLogger
    {
        private readonly Action<string> _sink;

        public DemoSplashLogger(Action<string> sink) => _sink = sink;

        public void Write(string message) => _sink(message);
    }
}
