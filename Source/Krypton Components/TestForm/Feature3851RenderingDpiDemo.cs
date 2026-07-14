#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Interactive demo for issue #3851 (rendering, DPI and performance).
/// <list type="bullet">
/// <item>4.1 / 4.2 - the ribbon Quick Access Toolbar overflow / context-arrow glyph is exercised across
/// every ribbon shape (theme) so it can be eyeballed at 100%, 150% and 200% DPI.</item>
/// <item>4.4 - the multiline editor button now lives in an internal fixed collection, so it never appears
/// in the public <see cref="KryptonTextBox.ButtonSpecs"/> collection (the count stays at zero).</item>
/// </list>
/// </summary>
public partial class Feature3851RenderingDpiDemo : KryptonForm
{
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonTextBox _multilineTextBox;
    private readonly KryptonCheckBox _multilineToggle;
    private readonly KryptonLabel _dpiLabel;
    private readonly KryptonLabel _shapeLabel;
    private readonly KryptonLabel _buttonSpecLabel;

    public Feature3851RenderingDpiDemo()
    {
        InitializeComponent();

        _ribbon = BuildRibbon();

        var themeCombo = new KryptonThemeComboBox
        {
            Dock = DockStyle.Top,
            Margin = new Padding(0, 0, 0, 8)
        };
        themeCombo.SelectedIndexChanged += (_, _) => RefreshDiagnostics();

        _dpiLabel = new KryptonLabel { Dock = DockStyle.Top };
        _shapeLabel = new KryptonLabel { Dock = DockStyle.Top };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            LabelStyle = LabelStyle.NormalPanel,
            Text = "Switch themes to change the ribbon shape and watch the Quick Access Toolbar overflow / "
                 + "context-arrow glyph (top-left of the ribbon). Re-open this form on monitors at 100%, 150% "
                 + "and 200% scaling to confirm the glyph stays aligned (issue #3851, points 4.1/4.2).\r\n\r\n"
                 + "The text box below enables the multiline string editor button. That button is now an internal "
                 + "fixed button spec, so the public ButtonSpecs count stays at 0 - it can no longer be removed, "
                 + "reordered, or serialized by the designer (point 4.4)."
        };

        _multilineTextBox = new KryptonTextBox
        {
            Dock = DockStyle.Top,
            Multiline = true,
            Height = 90,
            Text = "Line one\r\nLine two\r\nToggle the multiline editor to show the editor button.",
            Margin = new Padding(0, 8, 0, 0)
        };

        _multilineToggle = new KryptonCheckBox
        {
            Dock = DockStyle.Top,
            Text = "MultilineStringEditor (adds the internal editor button)"
        };
        _multilineToggle.CheckedChanged += (_, _) =>
        {
            _multilineTextBox.MultilineStringEditor = _multilineToggle.Checked;
            RefreshDiagnostics();
        };

        _buttonSpecLabel = new KryptonLabel { Dock = DockStyle.Top };

        var refreshButton = new KryptonButton
        {
            Dock = DockStyle.Top,
            Text = "Refresh diagnostics",
            Margin = new Padding(0, 8, 0, 0)
        };
        refreshButton.Click += (_, _) => RefreshDiagnostics();

        var panel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };

        // Docked children are added bottom-first so the first added ends up at the bottom of the stack.
        panel.Controls.Add(refreshButton);
        panel.Controls.Add(_multilineTextBox);
        panel.Controls.Add(_multilineToggle);
        panel.Controls.Add(_buttonSpecLabel);
        panel.Controls.Add(instructions);
        panel.Controls.Add(_shapeLabel);
        panel.Controls.Add(_dpiLabel);
        panel.Controls.Add(themeCombo);

        Controls.Add(panel);
        Controls.Add(_ribbon);

        Load += (_, _) => RefreshDiagnostics();
        DpiChanged += (_, _) => RefreshDiagnostics();
    }

    private KryptonRibbon BuildRibbon()
    {
        var ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            QATLocation = QATLocation.Above
        };

        ((ISupportInitialize)ribbon).BeginInit();

        // Add enough Quick Access Toolbar buttons that they overflow the available caption width,
        // which is what makes the overflow / context-arrow extra button (and its glyph) visible.
        for (var i = 1; i <= 14; i++)
        {
            ribbon.QATButtons.Add(new KryptonRibbonQATButton
            {
                Image = CreateSwatch(i),
                Text = $"QAT {i}"
            });
        }

        var tab = new KryptonRibbonTab { Text = "Home" };
        var group = new KryptonRibbonGroup { TextLine1 = "Sample" };
        var triple = new KryptonRibbonGroupTriple();
        triple.Items!.Add(new KryptonRibbonGroupButton { TextLine1 = "One" });
        triple.Items.Add(new KryptonRibbonGroupButton { TextLine1 = "Two" });
        triple.Items.Add(new KryptonRibbonGroupButton { TextLine1 = "Three" });
        group.Items.Add(triple);
        tab.Groups.Add(group);
        ribbon.RibbonTabs.Add(tab);
        ribbon.SelectedTab = tab;

        ((ISupportInitialize)ribbon).EndInit();

        return ribbon;
    }

    private static Bitmap CreateSwatch(int seed)
    {
        var bitmap = new Bitmap(16, 16);
        using Graphics g = Graphics.FromImage(bitmap);
        Color color = Color.FromArgb(60 + (seed * 12) % 180, 90, 200 - (seed * 9) % 150);
        using var brush = new SolidBrush(color);
        g.FillRectangle(brush, 2, 2, 12, 12);
        g.DrawRectangle(Pens.DimGray, 2, 2, 11, 11);
        return bitmap;
    }

    private void RefreshDiagnostics()
    {
        var factor = DeviceDpi / 96f;
        _dpiLabel.Text = $"Device DPI: {DeviceDpi} (scale factor {factor:0.00}x)";
        _shapeLabel.Text = $"Active ribbon shape: {_ribbon.StateCommon.RibbonGeneral.GetRibbonShape()}";
        _buttonSpecLabel.Text = $"Public ButtonSpecs count: {_multilineTextBox.ButtonSpecs.Count} "
                              + "(expected 0 - the editor button is internal)";
    }
}
