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
/// Issue #1103: compare TextRenderer / GDI+ / native GDI text performance and visual parity,
/// and exercise <see cref="AccurateText.PreferNativeGdiText"/> on live Krypton controls.
/// </summary>
public class Bug1103GdiTextPerfDemo : KryptonForm
{
    private const int IterationCount = 4000;

    private readonly KryptonWrapLabel _lblInfo;
    private readonly KryptonCheckBox _chkPreferNative;
    private readonly KryptonButton _btnRunBenchmark;
    private readonly KryptonTextBox _txtResults;
    private readonly Panel _parityHost;
    private readonly KryptonButton _btnSample;
    private readonly KryptonLabel _lblSample;
    private readonly Font _benchFont;
    private bool _savedPreferNative;

    private static readonly string[] SampleStrings =
    {
        "OK",
        "Save As...",
        "Krypton Button Caption",
        "A longer multiline label that wraps when the layout is constrained to a narrow rectangle.",
        "C:\\Users\\Example\\Documents\\Very\\Long\\Path\\FileName.krypton.xml"
    };

    public Bug1103GdiTextPerfDemo()
    {
        Text = @"Bug #1103 - Native GDI Text Performance";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(980, 720);
        MinimumSize = new Size(800, 560);

        _benchFont = new Font("Segoe UI", 9f);

        _lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 110,
            Text =
                @"Issue #1103 — native GDI text vs TextRenderer / Graphics.DrawString." + Environment.NewLine +
                @"TestForm already uses Application.SetCompatibleTextRenderingDefault(false)." + Environment.NewLine +
                @"1) Run Benchmark to time measure+draw loops (cached vs uncached HFONT)." + Environment.NewLine +
                @"2) Toggle PreferNativeGdiText and watch the sample button/label (AccurateText path)." + Environment.NewLine +
                @"3) Parity panel paints the same strings with each backend for visual comparison."
        };

        var toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(8),
            WrapContents = true
        };

        _chkPreferNative = new KryptonCheckBox
        {
            Text = @"AccurateText.PreferNativeGdiText",
            AutoSize = true,
            Margin = new Padding(0, 6, 16, 6)
        };
        _chkPreferNative.CheckedChanged += OnPreferNativeChanged;

        _btnRunBenchmark = new KryptonButton
        {
            Text = @"Run Benchmark",
            AutoSize = true,
            Margin = new Padding(0, 0, 8, 0)
        };
        _btnRunBenchmark.Click += (_, _) => RunBenchmark();

        toolbar.Controls.Add(_chkPreferNative);
        toolbar.Controls.Add(_btnRunBenchmark);

        var samples = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(8),
            WrapContents = true
        };

        _btnSample = new KryptonButton
        {
            Text = @"Sample KryptonButton",
            AutoSize = true,
            Margin = new Padding(0, 0, 12, 0)
        };

        _lblSample = new KryptonLabel
        {
            Text = @"Sample KryptonLabel — hover themes / resize with PreferNativeGdiText on and off.",
            AutoSize = true
        };

        samples.Controls.Add(_btnSample);
        samples.Controls.Add(_lblSample);

        _txtResults = new KryptonTextBox
        {
            Dock = DockStyle.Top,
            Multiline = true,
            Height = 220,
            ReadOnly = true,
            ScrollBars = ScrollBars.Vertical,
            Text = @"Press Run Benchmark."
        };

        _parityHost = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = SystemColors.Window,
            Padding = new Padding(8)
        };
        _parityHost.Paint += OnParityPaint;

        var root = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8)
        };
        root.Controls.Add(_parityHost);
        root.Controls.Add(_txtResults);
        root.Controls.Add(samples);
        root.Controls.Add(toolbar);
        root.Controls.Add(_lblInfo);

        Controls.Add(root);

        _savedPreferNative = AccurateText.PreferNativeGdiText;
        _chkPreferNative.Checked = _savedPreferNative;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        AccurateText.PreferNativeGdiText = _savedPreferNative;
        _benchFont.Dispose();
        base.OnFormClosed(e);
    }

    private void OnPreferNativeChanged(object? sender, EventArgs e)
    {
        AccurateText.PreferNativeGdiText = _chkPreferNative.Checked;
        _btnSample.PerformLayout();
        _lblSample.PerformLayout();
        Invalidate(true);
        _parityHost.Invalidate();
    }

    private void RunBenchmark()
    {
        Cursor = Cursors.WaitCursor;
        try
        {
            using var bmp = new Bitmap(400, 120);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            var flags = TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix;
            var bounds = new Rectangle(4, 4, 380, 100);
            var proposed = new Size(380, int.MaxValue);
            var sb = new StringBuilder();
            sb.AppendLine($"Iterations per method × string: {IterationCount}");
            sb.AppendLine($"CompatibleTextRenderingDefault(false) is set in TestForm.Program.");
            sb.AppendLine();

            // Warm-up
            WarmUp(g, flags, bounds, proposed);

            AppendTimed(sb, "Graphics.MeasureString + DrawString", () =>
            {
                for (var i = 0; i < IterationCount; i++)
                {
                    foreach (var s in SampleStrings)
                    {
                        var size = g.MeasureString(s, _benchFont, proposed.Width);
                        g.DrawString(s, _benchFont, Brushes.Black, bounds);
                        _ = size;
                    }
                }
            });

            AppendTimed(sb, "TextRenderer.MeasureText + DrawText", () =>
            {
                for (var i = 0; i < IterationCount; i++)
                {
                    foreach (var s in SampleStrings)
                    {
                        var size = TextRenderer.MeasureText(g, s, _benchFont, proposed, flags);
                        TextRenderer.DrawText(g, s, _benchFont, bounds, Color.Black, flags);
                        _ = size;
                    }
                }
            });

            AppendTimed(sb, "GdiNativeText (cached HFONT)", () =>
            {
                for (var i = 0; i < IterationCount; i++)
                {
                    foreach (var s in SampleStrings)
                    {
                        var size = GdiNativeText.Measure(g, s, _benchFont, flags, proposed, useFontCache: true);
                        GdiNativeText.Draw(g, s, _benchFont, bounds, Color.Black, flags, useFontCache: true);
                        _ = size;
                    }
                }
            });

            AppendTimed(sb, "GdiNativeText (uncached HFONT)", () =>
            {
                for (var i = 0; i < IterationCount; i++)
                {
                    foreach (var s in SampleStrings)
                    {
                        var size = GdiNativeText.Measure(g, s, _benchFont, flags, proposed, useFontCache: false);
                        GdiNativeText.Draw(g, s, _benchFont, bounds, Color.Black, flags, useFontCache: false);
                        _ = size;
                    }
                }
            });

            AppendTimed(sb, "ExtTextOutW baseline (cached, no wrap)", () =>
            {
                for (var i = 0; i < IterationCount; i++)
                {
                    foreach (var s in SampleStrings)
                    {
                        GdiNativeText.ExtTextOut(g, s, _benchFont, bounds.Location, Color.Black, useFontCache: true);
                    }
                }
            });

            sb.AppendLine();
            sb.AppendLine("Lower ms is better. PreferNativeGdiText only affects AccurateText horizontal measure/draw.");
            _txtResults.Text = sb.ToString();
            _parityHost.Invalidate();
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void WarmUp(Graphics g, TextFormatFlags flags, Rectangle bounds, Size proposed)
    {
        foreach (var s in SampleStrings)
        {
            g.MeasureString(s, _benchFont);
            g.DrawString(s, _benchFont, Brushes.Black, bounds);
            TextRenderer.MeasureText(g, s, _benchFont, proposed, flags);
            TextRenderer.DrawText(g, s, _benchFont, bounds, Color.Black, flags);
            GdiNativeText.Measure(g, s, _benchFont, flags, proposed);
            GdiNativeText.Draw(g, s, _benchFont, bounds, Color.Black, flags);
            GdiNativeText.ExtTextOut(g, s, _benchFont, bounds.Location, Color.Black);
        }
    }

    private static void AppendTimed(StringBuilder sb, string label, Action action)
    {
        // Force GC quiet period so timings are less noisy across runs.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var sw = Stopwatch.StartNew();
        action();
        sw.Stop();
        sb.AppendLine($"{label,-42} {sw.ElapsedMilliseconds,8} ms");
    }

    private void OnParityPaint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.Clear(SystemColors.Window);
        var flags = TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix;
        const int colWidth = 220;
        const int rowHeight = 72;
        var headers = new[] { "DrawString", "TextRenderer", "Native DrawText", "ExtTextOut" };
        using (var headerFont = new Font(_benchFont, FontStyle.Bold))
        {
            for (var c = 0; c < headers.Length; c++)
            {
                TextRenderer.DrawText(g, headers[c], headerFont, new Point(8 + c * colWidth, 4), SystemColors.ControlText);
            }
        }

        for (var r = 0; r < SampleStrings.Length; r++)
        {
            var text = SampleStrings[r];
            var y = 28 + r * rowHeight;
            var rect0 = new Rectangle(8, y, colWidth - 12, rowHeight - 8);
            var rect1 = new Rectangle(8 + colWidth, y, colWidth - 12, rowHeight - 8);
            var rect2 = new Rectangle(8 + colWidth * 2, y, colWidth - 12, rowHeight - 8);
            var rect3 = new Rectangle(8 + colWidth * 3, y, colWidth - 12, rowHeight - 8);

            g.DrawRectangle(Pens.LightGray, rect0);
            g.DrawRectangle(Pens.LightGray, rect1);
            g.DrawRectangle(Pens.LightGray, rect2);
            g.DrawRectangle(Pens.LightGray, rect3);

            g.DrawString(text, _benchFont, Brushes.Black, rect0);
            TextRenderer.DrawText(g, text, _benchFont, rect1, Color.Black, flags);
            GdiNativeText.Draw(g, text, _benchFont, rect2, Color.Black, flags);
            GdiNativeText.ExtTextOut(g, text, _benchFont, rect3.Location, Color.Black);
        }
    }
}
