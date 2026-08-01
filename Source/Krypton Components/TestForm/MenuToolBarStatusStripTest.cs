#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class MenuToolBarStatusStripTest : KryptonForm
{
    private readonly System.Windows.Forms.Timer _statusStripTimer = new System.Windows.Forms.Timer();
    private Color _baseStatusStripColor;
    private readonly Color _targetStatusStripColor = Color.DarkOrange;
    private int _animStep;
    private int _animDir = 1;
    private readonly ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
    private Font? _savedBaseFont;
    private int _baseFontFamilyIndex;

    private static readonly string[] BaseFontFamilies =
    [
        "Segoe UI",
        "Tahoma",
        "Arial",
        "Calibri",
        "Verdana",
        "Times New Roman",
        "Courier New"
    ];

    public MenuToolBarStatusStripTest()
    {
        InitializeComponent();
        _statusStripTimer.Interval = 50;
        _statusStripTimer.Tick += StatusStripTimer_Tick;
        KryptonManager.GlobalPaletteChanged += KryptonManager_GlobalPaletteChanged;

        InitializeBaseFontDemo();
    }

    private void InitializeBaseFontDemo()
    {
        Text = "Menu/Tool/Status Strip (#1297 BaseFont demo)";

        _contextMenuStrip.Items.Add(new ToolStripMenuItem("Context menu item 1"));
        _contextMenuStrip.Items.Add(new ToolStripMenuItem("Context menu item 2"));
        kryptonPanel1.ContextMenuStrip = _contextMenuStrip;

        var increaseBaseFontMenuItem = new ToolStripMenuItem("Increase Base Font (+2 pt)")
        {
            Name = "increaseBaseFontToolStripMenuItem"
        };
        increaseBaseFontMenuItem.Click += IncreaseBaseFontToolStripMenuItem_Click;

        var decreaseBaseFontMenuItem = new ToolStripMenuItem("Decrease Base Font (-2 pt)")
        {
            Name = "decreaseBaseFontToolStripMenuItem"
        };
        decreaseBaseFontMenuItem.Click += DecreaseBaseFontToolStripMenuItem_Click;

        var resetBaseFontMenuItem = new ToolStripMenuItem("Reset Base Font")
        {
            Name = "resetBaseFontToolStripMenuItem"
        };
        resetBaseFontMenuItem.Click += ResetBaseFontToolStripMenuItem_Click;

        var toggleBoldBaseFontMenuItem = new ToolStripMenuItem("Toggle Base Font Bold")
        {
            Name = "toggleBoldBaseFontToolStripMenuItem"
        };
        toggleBoldBaseFontMenuItem.Click += ToggleBoldBaseFontToolStripMenuItem_Click;

        var changeBaseFontFamilyMenuItem = new ToolStripMenuItem("Change Base Font Family")
        {
            Name = "changeBaseFontFamilyToolStripMenuItem"
        };
        changeBaseFontFamilyMenuItem.Click += ChangeBaseFontFamilyToolStripMenuItem_Click;

        toolsToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        toolsToolStripMenuItem.DropDownItems.Add(increaseBaseFontMenuItem);
        toolsToolStripMenuItem.DropDownItems.Add(decreaseBaseFontMenuItem);
        toolsToolStripMenuItem.DropDownItems.Add(changeBaseFontFamilyMenuItem);
        toolsToolStripMenuItem.DropDownItems.Add(resetBaseFontMenuItem);
        toolsToolStripMenuItem.DropDownItems.Add(toggleBoldBaseFontMenuItem);

        _savedBaseFont = KryptonManager.CurrentGlobalPalette.BaseFont;
        _baseFontFamilyIndex = Array.FindIndex(BaseFontFamilies, family =>
            string.Equals(family, _savedBaseFont.Name, StringComparison.OrdinalIgnoreCase));
        if (_baseFontFamilyIndex < 0)
        {
            _baseFontFamilyIndex = 0;
        }

        UpdateStripFontStatus();
    }

    private void IncreaseBaseFontToolStripMenuItem_Click(object? sender, EventArgs e) =>
        ApplyBaseFontSizeDelta(2f);

    private void DecreaseBaseFontToolStripMenuItem_Click(object? sender, EventArgs e) =>
        ApplyBaseFontSizeDelta(-2f);

    private void ToggleBoldBaseFontToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        var currentFont = KryptonManager.CurrentGlobalPalette.BaseFont;
        var style = currentFont.Bold ? FontStyle.Regular : FontStyle.Bold;
        KryptonManager.CurrentGlobalPalette.BaseFont =
            new Font(currentFont.FontFamily, currentFont.SizeInPoints, style);
        UpdateStripFontStatus();
    }

    private void ChangeBaseFontFamilyToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        _baseFontFamilyIndex = (_baseFontFamilyIndex + 1) % BaseFontFamilies.Length;
        ApplyBaseFontFamily(BaseFontFamilies[_baseFontFamilyIndex]);
    }

    private void ApplyBaseFontFamily(string familyName)
    {
        var currentFont = KryptonManager.CurrentGlobalPalette.BaseFont;
        KryptonManager.CurrentGlobalPalette.BaseFont =
            new Font(familyName, currentFont.SizeInPoints, currentFont.Style);
        UpdateStripFontStatus();
    }

    private void ResetBaseFontToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        if (_savedBaseFont != null)
        {
            KryptonManager.CurrentGlobalPalette.BaseFont = _savedBaseFont;
            _baseFontFamilyIndex = Array.FindIndex(BaseFontFamilies, family =>
                string.Equals(family, _savedBaseFont.Name, StringComparison.OrdinalIgnoreCase));
            if (_baseFontFamilyIndex < 0)
            {
                _baseFontFamilyIndex = 0;
            }
        }

        UpdateStripFontStatus();
    }

    private void ApplyBaseFontSizeDelta(float delta)
    {
        var currentFont = KryptonManager.CurrentGlobalPalette.BaseFont;
        KryptonManager.CurrentGlobalPalette.BaseFont =
            new Font(currentFont.FontFamily, currentFont.SizeInPoints + delta, currentFont.Style);
        UpdateStripFontStatus();
    }

    private void UpdateStripFontStatus()
    {
        var baseFont = KryptonManager.CurrentGlobalPalette.BaseFont;
        var nextFamily = BaseFontFamilies[(_baseFontFamilyIndex + 1) % BaseFontFamilies.Length];
        toolStripStatusLabel1.Text =
            $"BaseFont: {baseFont.Name} {baseFont.SizeInPoints:0.#}pt ({baseFont.Style}) | " +
            $"Menu: {menuStrip1.Font.Name} | ToolStrip: {toolStrip1.Font.Name} | " +
            $"Status: {statusStrip1.Font.Name} | Context: {_contextMenuStrip.Font.Name} | " +
            $"Next family (Tools): {nextFamily}";
    }

    private void animateStatusStripToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        if (animateStatusStripToolStripMenuItem.Checked)
        {
            _animStep = 0;
            _animDir = 1;
            _baseStatusStripColor = GetCurrentStatusStripBaseColor();
            _statusStripTimer.Start();
        }
        else
        {
            _statusStripTimer.Stop();
            // Reset to palette defaults when manually stopped
            if (statusStrip1 is Krypton.Toolkit.KryptonStatusStrip kss)
            {
                kss.StateCommon.Color1 = GlobalStaticVariables.EMPTY_COLOR;
                kss.StateCommon.Color2 = GlobalStaticVariables.EMPTY_COLOR;
                kss.StateCommon.ColorStyle = PaletteColorStyle.Inherit;
                kss.StateCommon.ColorAngle = -1f;
                kss.Invalidate();
            }
            else
            {
                statusStrip1.BackColor = SystemColors.Control;
            }
        }
    }

    private void StatusStripTimer_Tick(object? sender, EventArgs e)
    {
        // Fast ping-pong between base color and dark orange
        const int maxSteps = 14; // smaller is faster
        _animStep += _animDir;
        if (_animStep >= maxSteps || _animStep <= 0)
        {
            _animDir *= -1;
            if (_animStep < 0)
            {
                _animStep = 0;
            }
            else if (_animStep > maxSteps)
            {
                _animStep = maxSteps;
            }
        }
        float t = _animStep / (float)maxSteps;
        var color = LerpColor(_baseStatusStripColor, _targetStatusStripColor, t);

        if (statusStrip1 is Krypton.Toolkit.KryptonStatusStrip kss)
        {
            // Use per-control override path
            kss.StateCommon.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kss.StateCommon.Color1 = color;
            kss.StateCommon.Color2 = GlobalStaticVariables.EMPTY_COLOR;
            kss.StateCommon.ColorAngle = -1f;
        }
        else
        {
            statusStrip1.BackColor = color;
        }
    }

    private void KryptonManager_GlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Stop animation and reset to palette defaults
        _statusStripTimer.Stop();
        if (animateStatusStripToolStripMenuItem.Checked)
        {
            animateStatusStripToolStripMenuItem.Checked = false;
        }

        if (statusStrip1 is Krypton.Toolkit.KryptonStatusStrip kss)
        {
            // Clear per-control overrides to fall back to palette
            kss.StateCommon.Color1 = GlobalStaticVariables.EMPTY_COLOR;
            kss.StateCommon.Color2 = GlobalStaticVariables.EMPTY_COLOR;
            kss.StateCommon.ColorStyle = PaletteColorStyle.Inherit;
            kss.StateCommon.ColorAngle = -1f;
            kss.Invalidate();
        }

        UpdateStripFontStatus();
    }

    /*
    “Lerp” = linear interpolation. LerpColor blends between two colors by a fraction t in [0,1], per channel:
    - t=0 returns the start color
    - t=1 returns the end color
    - 0<t<1 returns a mix

    Example (conceptually): result.R = a.R + (b.R - a.R) * t, same for G and B.
    */
    private static Color LerpColor(Color a, Color b, float t)
    {
        t = Math.Max(0f, Math.Min(1f, t));
        int r = (int)Math.Round(a.R + (b.R - a.R) * t);
        int g = (int)Math.Round(a.G + (b.G - a.G) * t);
        int bch = (int)Math.Round(a.B + (b.B - a.B) * t);
        return Color.FromArgb(255, r, g, bch);
    }

    private Color GetCurrentStatusStripBaseColor()
    {
        // Prefer palette ColorTable values for the current theme
        var ct = KryptonManager.CurrentGlobalPalette?.ColorTable;
        if (ct is not null)
        {
            if (ct.StatusStripGradientEnd != GlobalStaticVariables.EMPTY_COLOR)
            {
                return ct.StatusStripGradientEnd;
            }
            if (ct.StatusStripGradientBegin != GlobalStaticVariables.EMPTY_COLOR)
            {
                return ct.StatusStripGradientBegin;
            }
        }

        // Fallbacks
        if (statusStrip1 is Krypton.Toolkit.KryptonStatusStrip kss)
        {
            // Use inherited default if available
            var c1 = kss.StateCommon.GetBackColor1(Krypton.Toolkit.PaletteState.Normal);
            if (c1 != GlobalStaticVariables.EMPTY_COLOR && !c1.IsEmpty)
            {
                return c1;
            }
        }
        return statusStrip1.BackColor.IsEmpty
            ? SystemColors.Control
            : statusStrip1.BackColor;
    }
}