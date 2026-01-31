using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace ThemeSwapRepro;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var envLog = Environment.GetEnvironmentVariable("THEME_SWAP_LOG");
        var logPath = string.IsNullOrWhiteSpace(envLog)
            ? Path.Combine(Path.GetTempPath(), "ThemeSwapRepro.log")
            : envLog!;
        Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "logs"));
        if (string.IsNullOrWhiteSpace(envLog))
        {
            logPath = Path.Combine(AppContext.BaseDirectory, "logs", "ThemeSwapRepro.log");
        }
        using var log = new StreamWriter(new FileStream(logPath, FileMode.Create, FileAccess.Write, FileShare.Read)) { AutoFlush = true };
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            try
            {
                log.WriteLine($"Unhandled: {e.ExceptionObject}");
            }
            catch { /* ignore */ }
        };

        // Create 3 forms with a theme selector each
        var forms = Enumerable.Range(0, 3).Select(i => CreateForm(i, log)).ToArray();
        foreach (var f in forms)
        {
            f.Show();
        }

        // Start automated toggling across forms
        _ = RunAutomation(forms, log);
        log.WriteLine($"Log: {logPath}");

        Application.Run(forms[0]);
    }

    private static Form CreateForm(int index, StreamWriter log)
    {
        var form = new Form
        {
            Text = $"Repro Form {index + 1}",
            StartPosition = FormStartPosition.Manual,
            Width = 640,
            Height = 400
        };
        // Cascade forms so they are visible instead of stacked
        form.Location = new System.Drawing.Point(100 + (index * 60), 100 + (index * 40));

        var combo = new KryptonThemeComboBox
        {
            Dock = DockStyle.Top
        };
        form.Controls.Add(combo);

        // Add a grid of KryptonButtons to stress Material button painting
        var container = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            AutoScroll = true,
            Padding = new Padding(10)
        };
        form.Controls.Add(container);

        for (int iBtn = 0; iBtn < 12; iBtn++)
        {
            var kb = new KryptonButton
            {
                Text = $"Button {iBtn + 1}",
                Width = 140,
                Height = 40,
                Margin = new Padding(6)
            };
            kb.Click += (_, __) =>
            {
                try { log.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] {form.Text} Click -> {kb.Text}"); }
                catch { /* ignore */ }
            };
            container.Controls.Add(kb);
        }

        combo.SelectedIndexChanged += (_, __) =>
        {
            try
            {
                var msg = $"[{DateTime.Now:HH:mm:ss.fff}] {form.Text} SelectedIndexChanged -> {combo.Text}";
                log.WriteLine(msg);
            }
            catch { /* ignore */ }
        };

        return form;
    }

    private static async Task RunAutomation(Form[] forms, StreamWriter log)
    {
        // Small delay to let forms finish handle creation
        await Task.Delay(1000);

        var combos = forms.Select(f => f.Controls.OfType<KryptonThemeComboBox>().First()).ToArray();
        // Ensure all forms start with a known theme and exercise buttons to stress painting
        foreach (var combo in combos)
        {
            try { combo.Text = "Material - Dark"; } catch { /* ignore */ }
        }
        var paletteNames = combos[0].Items.Cast<object>().Select(o => o.ToString()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        string[] interesting = new[] { "Microsoft 365 - Black (Dark Mode)", "Material - Dark", "Material - Light", "Microsoft 365 - Dark", "Office 2010 - Blue" };
        var cycle = paletteNames.Where(p => interesting.Contains(p!)).DefaultIfEmpty(paletteNames.First()).ToArray();

        int cyclesLimit = 30; // default cycles if not specified
        var cyclesEnv = Environment.GetEnvironmentVariable("TSR_CYCLES");
        if (int.TryParse(cyclesEnv, out var tmp) && tmp > 0)
        {
            cyclesLimit = tmp;
        }

        int i = 0;
        while (forms.All(f => !f.IsDisposed) && (cyclesLimit == 0 || i < cyclesLimit))
        {
            for (int f = 0; f < forms.Length; f++)
            {
                var combo = combos[f];
                var target = cycle[(i + f) % cycle.Length];
                try
                {
                    forms[f].BeginInvoke((MethodInvoker)(() =>
                    {
                        combo.Text = target;
                        // Also click a few buttons to trigger press/paint under the current theme
                        var buttons = forms[f].Controls.OfType<FlowLayoutPanel>().First().Controls.OfType<KryptonButton>().ToArray();
                        foreach (var b in buttons.Take(2))
                        {
                            try { b.PerformClick(); } catch { /* ignore */ }
                        }
                    }));
                    log.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Toggle {forms[f].Text} -> {target}");
                }
                catch (Exception ex)
                {
                    log.WriteLine($"Automation error: {ex}");
                }
                await Task.Delay(500);
            }
            i++;
        }

        try
        {
            foreach (var form in forms)
            {
                if (form.IsHandleCreated && !form.IsDisposed)
                {
                    form.BeginInvoke((MethodInvoker)(() => form.Close()));
                }
            }
        }
        catch { /* ignore */ }
    }
}
