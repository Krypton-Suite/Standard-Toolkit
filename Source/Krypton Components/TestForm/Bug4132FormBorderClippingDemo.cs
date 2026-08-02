#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for issue #4132: the right and bottom form borders (and the flush Close button edge)
/// were clipped away by the window region.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="KryptonForm"/> shapes itself by turning the renderer's "outside border path" into a
/// window <see cref="Control.Region"/>. When that path is a pixel short on the right and bottom,
/// the region hides the very column and row that <c>DrawBorder</c> paints into, so those two
/// borders disappear.
/// </para>
/// <para>
/// The Close button had a second, related fault. Most themes returned zero for
/// <see cref="PaletteMetricInt.HeaderButtonEdgeInsetFormRight"/>, so the control box ran flush into
/// the column the form border paints. Since the form border is drawn after its children, the
/// button's own right border was overwritten and only three of its four edges survived. Those
/// themes now read <see cref="GlobalStaticConstants.HEADER_BUTTON_EDGE_INSET_FORM_RIGHT"/>, which
/// defaults to the form border width (<c>1</c>) so the button stays flush against the border while
/// keeping its own edge. Raise it toward
/// <see cref="CommonHelper.GetFormHeaderButtonEdgeInsetRight"/> to float the control box further in.
/// </para>
/// <para>
/// The gap above the button came from a different place: <see cref="ViewLayoutCenter"/> centred the
/// button in the caption band, which left three pixels of caption above it while the right edge had
/// none. <see cref="GlobalStaticConstants.HEADER_BUTTON_EDGE_INSET_FORM_TOP"/> now drives that gap
/// instead, defaulting to <c>0</c> so the top matches the right. Set it negative to restore
/// centring.
/// </para>
/// <para>
/// The diagnostics below probe the live region rather than the view tree, so they report exactly
/// what Windows will clip.
/// </para>
/// </remarks>
public sealed class Bug4132FormBorderClippingDemo : KryptonForm
{
    private const string DemoTitle = @"Bug #4132 - Form border clipping";

    private readonly List<Bug4132BorderSampleForm> _openSamples = [];
    private readonly KryptonThemeComboBox _cmbTheme;
    private readonly KryptonWrapLabel _lblHostResult;
    private readonly KryptonWrapLabel _lblSampleResults;
    private readonly Timer _refreshTimer;

    public Bug4132FormBorderClippingDemo()
    {
        Text = DemoTitle;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(780, 520);
        MinimumSize = new Size(700, 460);

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 150,
            Padding = new Padding(12, 10, 12, 4),
            Text =
                @"How to test issue #4132:" + Environment.NewLine +
                @"1) Look at this window and any sample window: all four borders must be visible. Before the fix the right and bottom edges were missing." + Environment.NewLine +
                @"2) Hover the Close button in the caption: all four of its borders must show, sitting immediately inside the form border on both the top and the right." + Environment.NewLine +
                @"   Tune HEADER_BUTTON_EDGE_INSET_FORM_RIGHT and HEADER_BUTTON_EDGE_INSET_FORM_TOP to change those two gaps (a negative top value restores caption centring)." + Environment.NewLine +
                @"3) Switch themes below (square-cornered themes such as Microsoft 365 and Office 2013 showed the bug; rounded themes were unaffected)." + Environment.NewLine +
                @"4) Resize and maximise/restore the windows - the borders must survive every layout pass." + Environment.NewLine +
                @"5) The readouts below probe the live window region; every edge should report PASS."
        };

        var lblTheme = new KryptonLabel { Text = @"Theme:", AutoSize = true };
        _cmbTheme = new KryptonThemeComboBox { Width = 260 };

        var btnSizable = new KryptonButton { Text = @"Open Sizable sample", Width = 170 };
        var btnFixedSingle = new KryptonButton { Text = @"Open FixedSingle sample", Width = 170 };
        var btnFixedDialog = new KryptonButton { Text = @"Open FixedDialog sample", Width = 170 };
        var btnToolWindow = new KryptonButton { Text = @"Open ToolWindow sample", Width = 170 };
        var btnCloseSamples = new KryptonButton { Text = @"Close all samples", Width = 170 };

        btnSizable.Click += (_, _) => OpenSample(FormBorderStyle.Sizable);
        btnFixedSingle.Click += (_, _) => OpenSample(FormBorderStyle.FixedSingle);
        btnFixedDialog.Click += (_, _) => OpenSample(FormBorderStyle.FixedDialog);
        btnToolWindow.Click += (_, _) => OpenSample(FormBorderStyle.SizableToolWindow);
        btnCloseSamples.Click += (_, _) => CloseAllSamples();

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12, 4, 12, 4)
        };
        buttons.Controls.Add(lblTheme);
        buttons.Controls.Add(_cmbTheme);
        buttons.Controls.Add(btnSizable);
        buttons.Controls.Add(btnFixedSingle);
        buttons.Controls.Add(btnFixedDialog);
        buttons.Controls.Add(btnToolWindow);
        buttons.Controls.Add(btnCloseSamples);

        _lblHostResult = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 90,
            Padding = new Padding(12, 8, 12, 4),
            Text = @"Measuring…"
        };

        _lblSampleResults = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            Padding = new Padding(12, 4, 12, 12),
            Text = @"No sample windows open."
        };

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        panel.Controls.Add(_lblSampleResults);
        panel.Controls.Add(_lblHostResult);
        panel.Controls.Add(buttons);
        panel.Controls.Add(lblInfo);
        Controls.Add(panel);

        _refreshTimer = new Timer { Interval = 400 };
        _refreshTimer.Tick += (_, _) => RefreshDiagnostics();

        Load += (_, _) =>
        {
            _refreshTimer.Start();
            RefreshDiagnostics();
        };

        FormClosed += (_, _) =>
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
            CloseAllSamples();
        };
    }

    private void OpenSample(FormBorderStyle borderStyle)
    {
        var sample = new Bug4132BorderSampleForm(borderStyle);
        sample.FormClosed += (_, _) => _openSamples.Remove(sample);
        _openSamples.Add(sample);
        sample.Show(this);
        sample.BringToFront();
    }

    private void CloseAllSamples()
    {
        foreach (Bug4132BorderSampleForm sample in _openSamples.ToArray())
        {
            sample.Close();
        }

        _openSamples.Clear();
    }

    private void RefreshDiagnostics()
    {
        _lblHostResult.Text = @"This window: " + Bug4132RegionDiagnostics.Describe(this);
        _lblHostResult.StateNormal.TextColor = Bug4132RegionDiagnostics.EdgesVisible(this)
            ? Color.DarkGreen
            : Color.DarkRed;

        if (_openSamples.Count == 0)
        {
            _lblSampleResults.Text = @"No sample windows open.";
            return;
        }

        var lines = new List<string>();
        var allPass = true;

        foreach (Bug4132BorderSampleForm sample in _openSamples.ToArray())
        {
            if (sample.IsDisposed)
            {
                continue;
            }

            lines.Add(sample.Text + @": " + Bug4132RegionDiagnostics.Describe(sample));
            allPass &= Bug4132RegionDiagnostics.EdgesVisible(sample);
        }

        _lblSampleResults.Text = string.Join(Environment.NewLine, lines);
        _lblSampleResults.StateNormal.TextColor = allPass ? Color.DarkGreen : Color.DarkRed;
    }
}

/// <summary>
/// Sample window for a single <see cref="FormBorderStyle"/>, used to eyeball all four borders.
/// </summary>
public sealed class Bug4132BorderSampleForm : KryptonForm
{
    private readonly KryptonWrapLabel _lblStatus;
    private readonly Timer _refreshTimer;

    public Bug4132BorderSampleForm(FormBorderStyle borderStyle)
    {
        Text = @"4132 - " + borderStyle;
        FormBorderStyle = borderStyle;
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(460, 260);

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            Padding = new Padding(12),
            Text = @"Measuring…"
        };

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        panel.Controls.Add(_lblStatus);
        Controls.Add(panel);

        _refreshTimer = new Timer { Interval = 400 };
        _refreshTimer.Tick += (_, _) => RefreshStatus();

        Load += (_, _) =>
        {
            _refreshTimer.Start();
            RefreshStatus();
        };

        FormClosed += (_, _) =>
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
        };
    }

    private void RefreshStatus() =>
        _lblStatus.Text =
            @"Compare all four borders of this window." + Environment.NewLine + Environment.NewLine +
            Bug4132RegionDiagnostics.Describe(this);
}

/// <summary>
/// Probes the live window region so the demo reports what Windows actually clips.
/// </summary>
internal static class Bug4132RegionDiagnostics
{
    /// <summary>
    /// Returns true when the rightmost column and bottom row of the window survive the region.
    /// </summary>
    public static bool EdgesVisible(Form form)
    {
        if (!form.IsHandleCreated)
        {
            return true;
        }

        // A null region means the whole window is used, so nothing can be clipped
        Region? region = form.Region;
        if (region == null)
        {
            return true;
        }

        using Graphics g = form.CreateGraphics();
        return region.IsVisible(new Point(form.Width - 1, form.Height / 2), g)
               && region.IsVisible(new Point(form.Width / 2, form.Height - 1), g);
    }

    public static string Describe(Form form)
    {
        if (!form.IsHandleCreated)
        {
            return @"(pending)";
        }

        Region? region = form.Region;
        if (region == null)
        {
            return $@"{form.Width}x{form.Height}, no window region - all edges PASS";
        }

        using Graphics g = form.CreateGraphics();
        bool left = region.IsVisible(new Point(0, form.Height / 2), g);
        bool top = region.IsVisible(new Point(form.Width / 2, 0), g);
        bool right = region.IsVisible(new Point(form.Width - 1, form.Height / 2), g);
        bool bottom = region.IsVisible(new Point(form.Width / 2, form.Height - 1), g);

        return $@"{form.Width}x{form.Height} - left {Result(left)}, top {Result(top)}, right {Result(right)}, bottom {Result(bottom)}";
    }

    private static string Result(bool visible) => visible ? @"PASS" : @"CLIPPED";
}
