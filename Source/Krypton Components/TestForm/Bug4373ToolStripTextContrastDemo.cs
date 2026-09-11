#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Repro for issue #4373: ToolStrip item text must stay readable when cycling themes,
/// including Office White, Office 2007 Black, and Visual Studio 2010 variations.
/// </summary>
public sealed class Bug4373ToolStripTextContrastDemo : KryptonForm
{
    private const string DemoTitle = @"Bug #4373 - ToolStrip text contrast";

    private readonly KryptonManager _manager = new();
    private readonly KryptonThemeComboBox _themeCombo;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonToolStrip _kryptonStrip;
    private readonly ToolStrip _nativeStrip;
    private readonly PaletteMode _savedPaletteMode;

    public Bug4373ToolStripTextContrastDemo()
    {
        Text = DemoTitle;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(980, 420);
        MinimumSize = new Size(760, 360);
        _savedPaletteMode = KryptonManager.CurrentGlobalPaletteMode;

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 88,
            Text =
                @"How to test issue #4373:" + Environment.NewLine +
                @"1) Cycle themes, or jump to a previously unreadable one with the buttons below." + Environment.NewLine +
                @"2) ToolStripBtn1, label1, Label2, Button1, Button2, and ButtonWithIcon must stay readable on both strips." + Environment.NewLine +
                @"Office 2010/2013 White, Office 2007 Black, and Visual Studio 2010 (2010/2013) used to paint light text on a light strip (or dark on dark)."
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 40,
            Text = @"ColorTable: (pending)"
        };

        _themeCombo = new KryptonThemeComboBox
        {
            Width = 320,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _themeCombo.SelectedIndexChanged += (_, _) => RefreshReadout();

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(8, 4, 8, 4),
            WrapContents = true
        };
        buttons.Controls.Add(_themeCombo);
        buttons.Controls.Add(CreateThemeButton(@"Office 2010 White", PaletteMode.Office2010White));
        buttons.Controls.Add(CreateThemeButton(@"Office 2013 White", PaletteMode.Office2013White));
        buttons.Controls.Add(CreateThemeButton(@"Office 2007 Black", PaletteMode.Office2007Black));
        buttons.Controls.Add(CreateThemeButton(@"Office 2010 Black", PaletteMode.Office2010Black));
        buttons.Controls.Add(CreateThemeButton(@"VS2010 (2010)", PaletteMode.VisualStudio2010Render2010));
        buttons.Controls.Add(CreateThemeButton(@"VS2010 (2013)", PaletteMode.VisualStudio2010Render2013));
        buttons.Controls.Add(CreateThemeButton(@"VS2010 (365)", PaletteMode.VisualStudio2010Render365));

        var kryptonHost = new Panel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(8, 4, 8, 0) };
        _kryptonStrip = CreateKryptonStrip();
        kryptonHost.Controls.Add(_kryptonStrip);

        var nativeHost = new Panel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(8, 4, 8, 0) };
        _nativeStrip = CreateNativeStrip();
        nativeHost.Controls.Add(_nativeStrip);

        var kryptonCaption = new KryptonLabel { Dock = DockStyle.Top, Text = @"KryptonToolStrip" };
        var nativeCaption = new KryptonLabel { Dock = DockStyle.Top, Text = @"Native ToolStrip (same ColorTable)" };

        Controls.Add(nativeHost);
        Controls.Add(nativeCaption);
        Controls.Add(kryptonHost);
        Controls.Add(kryptonCaption);
        Controls.Add(buttons);
        Controls.Add(_lblStatus);
        Controls.Add(lblInfo);

        Load += (_, _) => RefreshReadout();
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        FormClosed += (_, _) =>
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            _manager.GlobalPaletteMode = _savedPaletteMode;
        };
    }

    private KryptonButton CreateThemeButton(string text, PaletteMode mode)
    {
        var button = new KryptonButton { Text = text, AutoSize = true };
        button.Click += (_, _) =>
        {
            _manager.GlobalPaletteMode = mode;
            RefreshReadout();
        };
        return button;
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e) => RefreshReadout();

    private static KryptonToolStrip CreateKryptonStrip()
    {
        var strip = new KryptonToolStrip { Dock = DockStyle.Fill, GripStyle = ToolStripGripStyle.Visible };
        PopulateStrip(strip.Items);
        return strip;
    }

    private static ToolStrip CreateNativeStrip()
    {
        var strip = new ToolStrip { Dock = DockStyle.Fill, GripStyle = ToolStripGripStyle.Visible };
        PopulateStrip(strip.Items);
        return strip;
    }

    private static void PopulateStrip(ToolStripItemCollection items)
    {
        var dropDown = new ToolStripDropDownButton(@"ToolstripBtn1");
        dropDown.DropDownItems.Add(new ToolStripMenuItem(@"Office 2010 - Black"));
        items.Add(dropDown);
        items.Add(new ToolStripLabel(@"label1"));
        items.Add(new ToolStripLabel(@"Label2"));
        items.Add(new ToolStripButton(@"Button1"));
        items.Add(new ToolStripButton(@"Button2"));
        items.Add(new ToolStripButton(@"ButtonWithIcon")
        {
            Image = SystemIcons.Information.ToBitmap(),
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        });
    }

    private void RefreshReadout()
    {
        var table = KryptonManager.CurrentGlobalPalette?.ColorTable;
        if (table is null)
        {
            _lblStatus.Text = @"ColorTable: (none)";
            return;
        }

        var text = table.ToolStripText;
        var back = table.ToolStripGradientBegin;
        var ratio = CommonHelper.ColorContrastRatio(text, back);
        var pass = CommonHelper.HasReadableContrast(text, back);
        _lblStatus.Text =
            $"Theme: {KryptonManager.CurrentGlobalPaletteMode}. " +
            $"ToolStripText={FormatColor(text)}  ToolStripBegin={FormatColor(back)}  " +
            $"contrast {ratio:0.00}:1 {(pass ? "PASS" : "FAIL")} (WCAG AA {CommonHelper.ReadableContrastRatio:0.0}:1).";

        _kryptonStrip.Invalidate();
        _nativeStrip.Invalidate();
    }

    private static string FormatColor(Color color) =>
        color.IsEmpty ? "(empty)" : $"#{color.R:X2}{color.G:X2}{color.B:X2}";
}
