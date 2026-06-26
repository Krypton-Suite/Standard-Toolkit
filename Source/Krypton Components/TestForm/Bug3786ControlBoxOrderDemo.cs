#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demo for Issue #3786: KryptonForm control box button order and RTL placement.
/// </summary>
/// <remarks>
/// <para>
/// Exercises three cooperating mechanisms in <see cref="KryptonForm"/>:
/// </para>
/// <list type="number">
/// <item><description>Collection order — <c>_buttonSpecsFixed</c> sequence (min/max/close vs traffic-light).</description></item>
/// <item><description>Edge placement — <see cref="KryptonForm.FormTrafficLightEdge"/> and palette Near/Far, with RTL Far→Near remap.</description></item>
/// <item><description>Runtime RTL — <see cref="Control.RightToLeft"/> + <see cref="Control.RightToLeftLayout"/>.</description></item>
/// </list>
/// <para>
/// Live diagnostics use non-client <see cref="KryptonForm.HitTestCloseButton"/> hit tests along the
/// caption to infer visual order and side without accessing internal view rectangles.
/// </para>
/// </remarks>
public partial class Bug3786ControlBoxOrderDemo : KryptonForm
{
    private readonly List<Bug3786ControlBoxSampleForm> _openSamples = [];
    private readonly Timer _refreshTimer;

    public Bug3786ControlBoxOrderDemo()
    {
        InitializeComponent();

        _refreshTimer = new Timer { Interval = 400 };
        _refreshTimer.Tick += (_, _) => RefreshDiagnostics();

        Load += Bug3786ControlBoxOrderDemo_Load;
        FormClosed += (_, _) =>
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
            CloseAllSamples();
        };
    }

    private void Bug3786ControlBoxOrderDemo_Load(object? sender, EventArgs e)
    {
        cmbTrafficLightEdge.SelectedIndex = 0;
        WireEvents();
        ApplyHostSettings();
        _refreshTimer.Start();
        RefreshDiagnostics();
    }

    private void WireEvents()
    {
        kryptonThemeComboBox1.SelectedIndexChanged += (_, _) => OnHostSettingChanged();
        chkRtl.CheckedChanged += (_, _) => OnHostSettingChanged();
        chkMinimizeBox.CheckedChanged += (_, _) => OnHostSettingChanged();
        chkMaximizeBox.CheckedChanged += (_, _) => OnHostSettingChanged();
        cmbTrafficLightEdge.SelectedIndexChanged += (_, _) => OnHostSettingChanged();

        btnOpenStandardLtr.Click += (_, _) => OpenSample(Bug3786ControlBoxSampleForm.Scenario.StandardLtr);
        btnOpenStandardRtl.Click += (_, _) => OpenSample(Bug3786ControlBoxSampleForm.Scenario.StandardRtl);
        btnOpenMacTrafficLights.Click += (_, _) => OpenSample(Bug3786ControlBoxSampleForm.Scenario.MacTrafficLights);
        btnOpenMacWindowsLayout.Click += (_, _) => OpenSample(Bug3786ControlBoxSampleForm.Scenario.MacWindowsLayout);
        btnOpenAquaTrafficLights.Click += (_, _) => OpenSample(Bug3786ControlBoxSampleForm.Scenario.AquaTrafficLights);
        btnOpenAllScenarios.Click += (_, _) => OpenAllScenarios();
        btnCloseSamples.Click += (_, _) => CloseAllSamples();
        btnApplyToOpenSamples.Click += (_, _) => ApplySettingsToOpenSamples();
    }

    private void OnHostSettingChanged()
    {
        ApplyHostSettings();
        ApplySettingsToOpenSamples();
        RefreshDiagnostics();
    }

    private void ApplyHostSettings()
    {
        MinimizeBox = chkMinimizeBox.Checked;
        MaximizeBox = chkMaximizeBox.Checked;
        FormTrafficLightEdge = GetSelectedTrafficLightEdge();

        if (chkRtl.Checked)
        {
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
        }
        else
        {
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
        }

        RecreateMinMaxCloseButtons();
    }

    private PaletteRelativeEdgeAlign GetSelectedTrafficLightEdge() =>
        cmbTrafficLightEdge.SelectedIndex switch
        {
            1 => PaletteRelativeEdgeAlign.Near,
            2 => PaletteRelativeEdgeAlign.Far,
            _ => PaletteRelativeEdgeAlign.Inherit
        };

    private void OpenSample(Bug3786ControlBoxSampleForm.Scenario scenario)
    {
        var sample = new Bug3786ControlBoxSampleForm(scenario);
        sample.FormClosed += (_, _) => _openSamples.Remove(sample);
        _openSamples.Add(sample);
        sample.Show(this);
        sample.BringToFront();
    }

    private void OpenAllScenarios()
    {
        CloseAllSamples();

        var scenarios = new[]
        {
            Bug3786ControlBoxSampleForm.Scenario.StandardLtr,
            Bug3786ControlBoxSampleForm.Scenario.StandardRtl,
            Bug3786ControlBoxSampleForm.Scenario.MacTrafficLights,
            Bug3786ControlBoxSampleForm.Scenario.MacWindowsLayout
        };

        var screen = Screen.FromControl(this).WorkingArea;
        int width = Math.Min(520, (screen.Width - 40) / 2);
        int height = Math.Min(280, (screen.Height - 60) / 2);
        Point[] origins =
        [
            new Point(screen.Left + 10, screen.Top + 10),
            new Point(screen.Left + 20 + width, screen.Top + 10),
            new Point(screen.Left + 10, screen.Top + 20 + height),
            new Point(screen.Left + 20 + width, screen.Top + 20 + height)
        ];

        for (var i = 0; i < scenarios.Length; i++)
        {
            var sample = new Bug3786ControlBoxSampleForm(scenarios[i])
            {
                StartPosition = FormStartPosition.Manual,
                Location = origins[i],
                Size = new Size(width, height)
            };
            sample.FormClosed += (_, _) => _openSamples.Remove(sample);
            _openSamples.Add(sample);
            sample.Show(this);
        }
    }

    private void ApplySettingsToOpenSamples()
    {
        foreach (var sample in _openSamples.ToArray())
        {
            if (!sample.IsDisposed)
            {
                sample.RefreshFromHost(this);
            }
        }

        RefreshDiagnostics();
    }

    private void CloseAllSamples()
    {
        foreach (var sample in _openSamples.ToArray())
        {
            sample.Close();
        }

        _openSamples.Clear();
    }

    private void RefreshDiagnostics()
    {
        var mode = PaletteMode == PaletteMode.Global
            ? KryptonManager.CurrentGlobalPaletteMode
            : PaletteMode;
        bool trafficLights = Bug3786ControlBoxDiagnostics.IsTrafficLightPalette(mode);
        bool rtl = RightToLeftLayout && RightToLeft == RightToLeft.Yes;
        bool macWindowsLayout = trafficLights && FormTrafficLightEdge == PaletteRelativeEdgeAlign.Far;

        string expectedOrder = Bug3786ControlBoxDiagnostics.GetExpectedOrder(trafficLights, rtl, macWindowsLayout);
        string expectedSide = Bug3786ControlBoxDiagnostics.GetExpectedSide(trafficLights, rtl, macWindowsLayout);
        string measuredOrder = Bug3786ControlBoxDiagnostics.MeasureButtonOrder(this);
        string measuredSide = Bug3786ControlBoxDiagnostics.MeasureButtonSide(this);
        bool orderOk = string.Equals(expectedOrder, measuredOrder, StringComparison.Ordinal);
        bool sideOk = string.Equals(expectedSide, measuredSide, StringComparison.OrdinalIgnoreCase);

        lblHostPalette.Values.Text = $"Global palette: {mode}";
        lblHostRtl.Values.Text = $"RTL: {(rtl ? "Yes (RightToLeftLayout + RightToLeft.Yes)" : "No")}";
        lblHostTrafficEdge.Values.Text = $"FormTrafficLightEdge: {FormTrafficLightEdge}";
        lblExpected.Values.Text = $"Expected: {expectedSide} — {expectedOrder}";
        lblMeasured.Values.Text = $"Measured (this form): {measuredSide} — {measuredOrder}";
        lblResult.Values.Text = orderOk && sideOk
            ? "Result: PASS"
            : $"Result: FAIL (order={(orderOk ? "OK" : "wrong")}, side={(sideOk ? "OK" : "wrong")})";
        lblResult.StateCommon.ShortText.Color1 = orderOk && sideOk ? Color.DarkGreen : Color.DarkRed;

        lblOpenSamples.Values.Text = $"Open sample windows: {_openSamples.Count}";
    }
}

/// <summary>
/// Sample child form for a single control box scenario.
/// </summary>
public sealed class Bug3786ControlBoxSampleForm : KryptonForm
{
    public enum Scenario
    {
        StandardLtr,
        StandardRtl,
        MacTrafficLights,
        MacWindowsLayout,
        AquaTrafficLights,
        Custom
    }

    private readonly Scenario _scenario;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly Timer _refreshTimer;

    public Bug3786ControlBoxSampleForm(Scenario scenario)
    {
        _scenario = scenario;

        Text = GetScenarioTitle(scenario);
        Size = new Size(520, 280);
        StartPosition = FormStartPosition.CenterParent;
        MinimizeBox = true;
        MaximizeBox = true;
        ControlBox = true;

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            Height = 72,
            Padding = new Padding(12, 8, 12, 8),
            Text = "Measuring…"
        };

        var body = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            Text = GetScenarioInstructions(scenario)
        };

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        panel.Controls.Add(body);
        panel.Controls.Add(_lblStatus);
        Controls.Add(panel);

        _refreshTimer = new Timer { Interval = 400 };
        _refreshTimer.Tick += (_, _) => RefreshStatus();

        Load += (_, _) =>
        {
            ApplyScenarioDefaults();
            _refreshTimer.Start();
            RefreshStatus();
        };

        FormClosed += (_, _) =>
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
        };
    }

    public void RefreshFromHost(Bug3786ControlBoxOrderDemo host)
    {
        PaletteMode = host.PaletteMode;
        MinimizeBox = host.MinimizeBox;
        MaximizeBox = host.MaximizeBox;
        FormTrafficLightEdge = host.FormTrafficLightEdge;
        RightToLeftLayout = host.RightToLeftLayout;
        RightToLeft = host.RightToLeft;
        RefreshStatus();
    }

    private void ApplyScenarioDefaults()
    {
        switch (_scenario)
        {
            case Scenario.StandardLtr:
                PaletteMode = PaletteMode.Microsoft365Blue;
                RightToLeftLayout = false;
                RightToLeft = RightToLeft.No;
                FormTrafficLightEdge = PaletteRelativeEdgeAlign.Inherit;
                break;
            case Scenario.StandardRtl:
                PaletteMode = PaletteMode.Microsoft365Blue;
                RightToLeftLayout = true;
                RightToLeft = RightToLeft.Yes;
                FormTrafficLightEdge = PaletteRelativeEdgeAlign.Inherit;
                break;
            case Scenario.MacTrafficLights:
                PaletteMode = PaletteMode.MacOSLight;
                RightToLeftLayout = false;
                RightToLeft = RightToLeft.No;
                FormTrafficLightEdge = PaletteRelativeEdgeAlign.Inherit;
                break;
            case Scenario.MacWindowsLayout:
                PaletteMode = PaletteMode.MacOSLight;
                RightToLeftLayout = false;
                RightToLeft = RightToLeft.No;
                FormTrafficLightEdge = PaletteRelativeEdgeAlign.Far;
                break;
            case Scenario.AquaTrafficLights:
                PaletteMode = PaletteMode.MacOSXAqua;
                RightToLeftLayout = false;
                RightToLeft = RightToLeft.No;
                FormTrafficLightEdge = PaletteRelativeEdgeAlign.Inherit;
                break;
        }
    }

    private void RefreshStatus()
    {
        var mode = PaletteMode == PaletteMode.Global
            ? KryptonManager.CurrentGlobalPaletteMode
            : PaletteMode;
        bool trafficLights = Bug3786ControlBoxDiagnostics.IsTrafficLightPalette(mode);
        bool rtl = RightToLeftLayout && RightToLeft == RightToLeft.Yes;
        bool macWindowsLayout = trafficLights && FormTrafficLightEdge == PaletteRelativeEdgeAlign.Far;

        string expectedOrder = Bug3786ControlBoxDiagnostics.GetExpectedOrder(trafficLights, rtl, macWindowsLayout);
        string expectedSide = Bug3786ControlBoxDiagnostics.GetExpectedSide(trafficLights, rtl, macWindowsLayout);
        string measuredOrder = Bug3786ControlBoxDiagnostics.MeasureButtonOrder(this);
        string measuredSide = Bug3786ControlBoxDiagnostics.MeasureButtonSide(this);
        bool pass = string.Equals(expectedOrder, measuredOrder, StringComparison.Ordinal)
                    && string.Equals(expectedSide, measuredSide, StringComparison.OrdinalIgnoreCase);

        _lblStatus.Text =
            $"Palette: {mode} | RTL: {rtl} | Traffic edge: {FormTrafficLightEdge}{Environment.NewLine}" +
            $"Expected: {expectedSide} — {expectedOrder}{Environment.NewLine}" +
            $"Measured: {measuredSide} — {measuredOrder} | {(pass ? "PASS" : "FAIL")}";
        _lblStatus.StateNormal.TextColor = pass ? Color.DarkGreen : Color.DarkRed;
    }

    private static string GetScenarioTitle(Scenario scenario) =>
        scenario switch
        {
            Scenario.StandardLtr => "3786 — Standard LTR",
            Scenario.StandardRtl => "3786 — Standard RTL",
            Scenario.MacTrafficLights => "3786 — macOS traffic lights",
            Scenario.MacWindowsLayout => "3786 — macOS Windows layout",
            Scenario.AquaTrafficLights => "3786 — OS X Aqua traffic lights",
            _ => "3786 — Control box sample"
        };

    private static string GetScenarioInstructions(Scenario scenario) =>
        scenario switch
        {
            Scenario.StandardLtr =>
                "Standard palette, LTR. Control box on the right: Minimize → Maximize/Restore → Close.",
            Scenario.StandardRtl =>
                "Standard palette, RTL. Control box on the left: Close → Maximize/Restore → Minimize.",
            Scenario.MacTrafficLights =>
                "macOS palette. Traffic lights on the left: Close (red) → Minimize (yellow) → Maximize/Restore (green).",
            Scenario.MacWindowsLayout =>
                "macOS palette with FormTrafficLightEdge = Far. Windows order on the right: Minimize → Maximize/Restore → Close.",
            Scenario.AquaTrafficLights =>
                "OS X Aqua palette. Traffic lights on the left in red → yellow → green order.",
            _ => "Adjust settings from the host demo to explore other combinations."
        };
}

internal static class Bug3786ControlBoxDiagnostics
{
    // Window-coordinate Y inside the caption band used when probing HitTest* along the title bar.
    private const int ProbeY = 12;

    public static bool IsTrafficLightPalette(PaletteMode mode) =>
        mode is PaletteMode.MacOSLight or PaletteMode.MacOSDark or PaletteMode.MacOSXAqua;

    /// <summary>Expected left-to-right button labels for the active scenario.</summary>
    public static string GetExpectedOrder(bool trafficLights, bool rtl, bool macWindowsLayout)
    {
        if (trafficLights && !macWindowsLayout)
        {
            return "Close → Min → Max";
        }

        return rtl ? "Close → Max → Min" : "Min → Max → Close";
    }

    /// <summary>Expected physical side of the control box cluster (Left or Right).</summary>
    public static string GetExpectedSide(bool trafficLights, bool rtl, bool macWindowsLayout)
    {
        if (trafficLights && !macWindowsLayout)
        {
            return "Left";
        }

        return rtl ? "Left" : "Right";
    }

    /// <summary>
    /// Scans the caption at <see cref="ProbeY"/> and returns the sequence of button hits along X.
    /// </summary>
    /// <remarks>
    /// Uses window coordinates (same space as <see cref="KryptonForm.HitTestCloseButton"/>).
    /// A new label is recorded each time the hit target changes while sweeping left→right.
    /// </remarks>
    public static string MeasureButtonOrder(KryptonForm form)
    {
        if (!form.IsHandleCreated || !form.ControlBox)
        {
            return "(pending)";
        }

        var seen = new List<string>();
        string last = string.Empty;
        int width = Math.Max(form.Width, 1);

        for (var x = 0; x < width; x++)
        {
            var pt = new Point(x, ProbeY);
            string current = GetButtonAt(form, pt);
            if (!string.IsNullOrEmpty(current) && !string.Equals(current, last, StringComparison.Ordinal))
            {
                seen.Add(current);
                last = current;
            }
            else if (string.IsNullOrEmpty(current))
            {
                last = string.Empty;
            }
        }

        return seen.Count == 0 ? "(no buttons)" : string.Join(" → ", seen);
    }

    /// <summary>
    /// Returns Left or Right based on the median X of all caption hit-test samples.
    /// </summary>
    public static string MeasureButtonSide(KryptonForm form)
    {
        if (!form.IsHandleCreated || !form.ControlBox)
        {
            return "(pending)";
        }

        var xs = new List<int>();
        int width = Math.Max(form.Width, 1);

        for (var x = 0; x < width; x++)
        {
            var pt = new Point(x, ProbeY);
            if (!string.IsNullOrEmpty(GetButtonAt(form, pt)))
            {
                xs.Add(x);
            }
        }

        if (xs.Count == 0)
        {
            return "(unknown)";
        }

        int mid = xs[xs.Count / 2];
        return mid < width / 2 ? "Left" : "Right";
    }

    private static string GetButtonAt(KryptonForm form, Point windowPoint)
    {
        if (form.HitTestCloseButton(windowPoint))
        {
            return "Close";
        }

        if (form.HitTestMaxButton(windowPoint))
        {
            return "Max";
        }

        if (form.HitTestMinButton(windowPoint))
        {
            return "Min";
        }

        return string.Empty;
    }
}
