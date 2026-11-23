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
/// Demo for issue #638: ToolMenuStatus ImageMargin gradient colours must paint the
/// drop-down image column on every theme, not only System and Professional.
/// </summary>
public sealed class Bug638ImageMarginDemo : KryptonForm
{
    private const string DemoTitle = @"Bug #638 - ImageMargin gradients";

    private readonly KryptonManager _manager = new();
    private readonly KryptonThemeComboBox _themeCombo;
    private readonly KryptonColorButton _btnBegin;
    private readonly KryptonColorButton _btnMiddle;
    private readonly KryptonColorButton _btnEnd;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonContextMenu _kryptonMenu;
    private readonly Bitmap _itemImage;
    private PaletteMode _savedPaletteMode;
    private PaletteMode? _overrideBaseMode;
    private KryptonCustomPaletteBase? _savedCustomPalette;
    private KryptonCustomPaletteBase? _overridePalette;

    public Bug638ImageMarginDemo()
    {
        Text = DemoTitle;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(920, 560);
        MinimumSize = new Size(800, 480);

        _savedPaletteMode = KryptonManager.CurrentGlobalPaletteMode;
        _savedCustomPalette = _manager.GlobalCustomPalette;
        _itemImage = CreateSwatch(Color.SteelBlue);

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 118,
            Text =
                @"How to test issue #638:" + Environment.NewLine +
                @"1) Pick an Office 2007 / 2010 / 365, Sparkle, Visual Studio, or Material theme." + Environment.NewLine +
                @"2) Set Begin / Middle / End to three obvious colours (default: red, lime, blue)." + Environment.NewLine +
                @"3) Click Apply ImageMargin colours, then open File on each menu and the KryptonContextMenu." + Environment.NewLine +
                @"The icon column must show that left-to-right gradient. Reset restores the theme default (usually solid)."
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 36,
            Text = @"ColorTable ImageMargin: (not applied)"
        };

        _themeCombo = new KryptonThemeComboBox
        {
            Width = 280,
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        _btnBegin = CreateColorButton(@"Begin", Color.Red);
        _btnMiddle = CreateColorButton(@"Middle", Color.Lime);
        _btnEnd = CreateColorButton(@"End", Color.Blue);

        var btnApply = new KryptonButton { Text = @"Apply ImageMargin colours", AutoSize = true };
        btnApply.Click += (_, _) => ApplyImageMarginOverride();

        var btnReset = new KryptonButton { Text = @"Reset to theme", AutoSize = true };
        btnReset.Click += (_, _) => ResetOverride();

        var btnContext = new KryptonButton { Text = @"Show KryptonContextMenu", AutoSize = true };
        btnContext.Click += OnShowKryptonContextMenu;

        var controls = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12, 8, 12, 8),
            WrapContents = true
        };
        controls.Controls.Add(new KryptonLabel { Text = @"Theme:", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
        controls.Controls.Add(_themeCombo);
        controls.Controls.Add(_btnBegin);
        controls.Controls.Add(_btnMiddle);
        controls.Controls.Add(_btnEnd);
        controls.Controls.Add(btnApply);
        controls.Controls.Add(btnReset);
        controls.Controls.Add(btnContext);

        var comparison = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2,
            Padding = new Padding(12)
        };
        comparison.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        comparison.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        comparison.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        comparison.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        comparison.Controls.Add(new KryptonLabel
        {
            Text = @"KryptonMenuStrip",
            LabelStyle = LabelStyle.BoldControl,
            Dock = DockStyle.Fill
        }, 0, 0);
        comparison.Controls.Add(new KryptonLabel
        {
            Text = @"Native MenuStrip",
            LabelStyle = LabelStyle.BoldControl,
            Dock = DockStyle.Fill
        }, 1, 0);

        var kryptonMenuStrip = CreateKryptonMenuStrip();
        MainMenuStrip = kryptonMenuStrip;
        var kryptonHost = new KryptonPanel { Dock = DockStyle.Fill };
        var nativeHost = new KryptonPanel { Dock = DockStyle.Fill };
        kryptonHost.Controls.Add(kryptonMenuStrip);
        nativeHost.Controls.Add(CreateNativeMenuStrip());
        comparison.Controls.Add(kryptonHost, 0, 1);
        comparison.Controls.Add(nativeHost, 1, 1);

        _kryptonMenu = CreateKryptonContextMenu();

        Controls.Add(comparison);
        Controls.Add(controls);
        Controls.Add(_lblStatus);
        Controls.Add(lblInfo);

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        FormClosed += OnFormClosed;
        UpdateStatus();
    }

    private static KryptonColorButton CreateColorButton(string text, Color color) =>
        new()
        {
            Values = { Text = text },
            SelectedColor = color,
            Width = 110,
            Height = 32
        };

    private static Bitmap CreateSwatch(Color color)
    {
        var bitmap = new Bitmap(16, 16);
        using var g = Graphics.FromImage(bitmap);
        g.Clear(color);
        using var pen = new Pen(Color.Black);
        g.DrawRectangle(pen, 0, 0, 15, 15);
        return bitmap;
    }

    private KryptonMenuStrip CreateKryptonMenuStrip()
    {
        var menu = new KryptonMenuStrip
        {
            Dock = DockStyle.Top,
            GripStyle = ToolStripGripStyle.Hidden
        };
        menu.Items.Add(CreateDropDown(@"&File"));
        menu.Items.Add(CreateDropDown(@"&Edit"));
        return menu;
    }

    private MenuStrip CreateNativeMenuStrip()
    {
        var menu = new MenuStrip
        {
            Dock = DockStyle.Top,
            GripStyle = ToolStripGripStyle.Hidden
        };
        menu.Items.Add(CreateDropDown(@"Native &File"));
        menu.Items.Add(CreateDropDown(@"Native &Edit"));
        return menu;
    }

    private ToolStripMenuItem CreateDropDown(string text)
    {
        var root = new ToolStripMenuItem(text);
        root.DropDownItems.Add(new ToolStripMenuItem(@"New", _itemImage));
        root.DropDownItems.Add(new ToolStripMenuItem(@"Open", _itemImage));
        root.DropDownItems.Add(new ToolStripSeparator());
        root.DropDownItems.Add(new ToolStripMenuItem(@"Save", _itemImage));
        root.DropDownItems.Add(new ToolStripMenuItem(@"Exit", _itemImage));
        return root;
    }

    private KryptonContextMenu CreateKryptonContextMenu()
    {
        var menu = new KryptonContextMenu();
        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem(@"New", _itemImage, null));
        items.Items.Add(new KryptonContextMenuItem(@"Open", _itemImage, null));
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(new KryptonContextMenuItem(@"Save", _itemImage, null));
        items.Items.Add(new KryptonContextMenuItem(@"Exit", _itemImage, null));
        menu.Items.Add(items);
        return menu;
    }

    private void ApplyImageMarginOverride()
    {
        var baseMode = KryptonManager.CurrentGlobalPaletteMode;
        if (baseMode == PaletteMode.Custom)
        {
            baseMode = _overrideBaseMode ?? PaletteMode.Office2007Blue;
        }

        _overrideBaseMode = baseMode;
        _manager.GlobalCustomPalette = null;
        _overridePalette?.Dispose();
        _overridePalette = new KryptonCustomPaletteBase { BasePaletteMode = baseMode };
        _overridePalette.SetPaletteName(@"ImageMargin overlay");
        _overridePalette.ToolMenuStatus.Menu.ImageMarginGradientBegin = _btnBegin.SelectedColor;
        _overridePalette.ToolMenuStatus.Menu.ImageMarginGradientMiddle = _btnMiddle.SelectedColor;
        _overridePalette.ToolMenuStatus.Menu.ImageMarginGradientEnd = _btnEnd.SelectedColor;
        _overridePalette.ToolMenuStatus.Menu.ImageMarginRevealedGradientBegin = _btnBegin.SelectedColor;
        _overridePalette.ToolMenuStatus.Menu.ImageMarginRevealedGradientMiddle = _btnMiddle.SelectedColor;
        _overridePalette.ToolMenuStatus.Menu.ImageMarginRevealedGradientEnd = _btnEnd.SelectedColor;
        _manager.GlobalCustomPalette = _overridePalette;
        UpdateStatus();
    }

    private void ResetOverride()
    {
        if (_overridePalette == null)
        {
            return;
        }

        var restoreMode = _overrideBaseMode ?? _savedPaletteMode;
        _manager.GlobalCustomPalette = null;
        _overridePalette.Dispose();
        _overridePalette = null;
        if (restoreMode != PaletteMode.Custom)
        {
            _manager.GlobalPaletteMode = restoreMode;
        }

        UpdateStatus();
    }

    private void OnShowKryptonContextMenu(object? sender, EventArgs e)
    {
        var button = (Control)sender!;
        _kryptonMenu.Show(button, button.PointToScreen(new Point(0, button.Height)));
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e) => UpdateStatus();

    private void UpdateStatus()
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        var table = palette?.ColorTable;
        if (table == null)
        {
            _lblStatus.Text = @"ColorTable ImageMargin: (none)";
            return;
        }

        _lblStatus.Text =
            $"Mode={KryptonManager.CurrentGlobalPaletteMode}; palette={palette!.GetType().Name}; " +
            $"ImageMargin Begin={FormatColor(table.ImageMarginGradientBegin)}, " +
            $"Middle={FormatColor(table.ImageMarginGradientMiddle)}, " +
            $"End={FormatColor(table.ImageMarginGradientEnd)}";
    }

    private static string FormatColor(Color color) =>
        color.IsNamedColor ? color.Name : $"#{color.R:X2}{color.G:X2}{color.B:X2}";

    private void OnFormClosed(object? sender, FormClosedEventArgs e)
    {
        KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        if (_savedCustomPalette != null)
        {
            _manager.GlobalCustomPalette = _savedCustomPalette;
        }
        else
        {
            _manager.GlobalCustomPalette = null;
            _manager.GlobalPaletteMode = _savedPaletteMode;
        }

        _overridePalette?.Dispose();
        _overridePalette = null;
        _itemImage.Dispose();
        _kryptonMenu.Dispose();
    }
}
