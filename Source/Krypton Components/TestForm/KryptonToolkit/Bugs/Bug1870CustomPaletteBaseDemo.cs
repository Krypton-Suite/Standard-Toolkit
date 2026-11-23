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
/// Demo for issue #1870: changing <see cref="KryptonCustomPaletteBase.BasePaletteMode"/>
/// must inherit that theme's colours (including <see cref="PaletteBase.ColorTable"/>)
/// instead of remaining stuck on Microsoft 365 Blue.
/// </summary>
public sealed class Bug1870CustomPaletteBaseDemo : KryptonForm
{
    private const string DemoTitle = @"Bug #1870 - Custom palette base colour table";

    private readonly KryptonManager _manager = new();
    private readonly KryptonCustomPaletteBase _palette = new();
    private readonly KryptonComboBox _cboBaseMode;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonPropertyGrid _propertyGrid;
    private readonly PaletteMode _savedPaletteMode;
    private readonly KryptonCustomPaletteBase? _savedCustomPalette;
    private readonly PaletteModeConverter _modeConverter = new();

    public Bug1870CustomPaletteBaseDemo()
    {
        Text = DemoTitle;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1100, 640);
        MinimumSize = new Size(900, 520);

        _savedPaletteMode = KryptonManager.CurrentGlobalPaletteMode;
        _savedCustomPalette = _manager.GlobalCustomPalette;

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 108,
            Text =
                @"How to test issue #1870:" + Environment.NewLine +
                @"1) Pick Office 2010 - Silver (or Office 2007 - Silver) in BasePaletteMode." + Environment.NewLine +
                @"2) The header, button, menu, and status strip must follow that theme — not Microsoft 365 Blue." + Environment.NewLine +
                @"3) Expand ColorTable in the property grid; ToolStripGradientBegin / StatusStripGradientBegin must change." + Environment.NewLine +
                @"4) Click Override status strip to force a lime colour; Reset override returns to the inherited table."
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 48,
            Text = @"ColorTable: (pending)"
        };

        _cboBaseMode = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 280
        };
        PopulateBaseModeCombo();
        _cboBaseMode.SelectedIndexChanged += (_, _) => ApplySelectedBaseMode();

        var btnOverride = new KryptonButton { Text = @"Override status strip", AutoSize = true };
        btnOverride.Click += (_, _) => ApplyStatusStripOverride();

        var btnReset = new KryptonButton { Text = @"Reset override", AutoSize = true };
        btnReset.Click += (_, _) => ResetStatusStripOverride();

        var sampleHeader = new KryptonHeaderGroup
        {
            Dock = DockStyle.Fill
        };
        sampleHeader.ValuesPrimary.Heading = @"Sample header (inherits from BasePaletteMode)";

        var sampleButton = new KryptonButton
        {
            Text = @"Sample button",
            AutoSize = true,
            Location = new Point(12, 12)
        };
        sampleHeader.Panel.Controls.Add(sampleButton);

        var menu = new MenuStrip();
        menu.Items.Add(new ToolStripMenuItem(@"File", null,
            new ToolStripMenuItem(@"New"),
            new ToolStripMenuItem(@"Open"),
            new ToolStripSeparator(),
            new ToolStripMenuItem(@"Exit")));
        var status = new StatusStrip();
        status.Items.Add(new ToolStripStatusLabel(@"Status strip uses ColorTable"));

        MainMenuStrip = menu;
        Controls.Add(menu);
        Controls.Add(status);

        _propertyGrid = new KryptonPropertyGrid
        {
            Dock = DockStyle.Fill,
            SelectedObject = _palette
        };

        var left = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(12)
        };
        left.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        left.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        left.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        var toolbar = new FlowLayoutPanel
        {
            AutoSize = true,
            WrapContents = false,
            Dock = DockStyle.Fill
        };
        toolbar.Controls.Add(new KryptonLabel { Text = @"BasePaletteMode:", AutoSize = true, Padding = new Padding(0, 6, 8, 0) });
        toolbar.Controls.Add(_cboBaseMode);
        toolbar.Controls.Add(btnOverride);
        toolbar.Controls.Add(btnReset);

        left.Controls.Add(toolbar, 0, 0);
        left.Controls.Add(_lblStatus, 0, 1);
        left.Controls.Add(sampleHeader, 0, 2);

        var split = new KryptonSplitContainer
        {
            Dock = DockStyle.Fill,
            SplitterDistance = 560
        };
        split.Panel1.Controls.Add(left);
        split.Panel2.Controls.Add(_propertyGrid);

        Controls.Add(split);
        Controls.Add(lblInfo);

        _manager.GlobalCustomPalette = _palette;
        SelectModeInCombo(PaletteMode.Office2010Silver);
        ApplySelectedBaseMode();

        FormClosed += OnFormClosed;
    }

    private void PopulateBaseModeCombo()
    {
        var values = _modeConverter.GetStandardValues(null);
        if (values == null)
        {
            return;
        }

        foreach (PaletteMode mode in values)
        {
            if (mode is PaletteMode.Custom or PaletteMode.Global)
            {
                continue;
            }

            var display = _modeConverter.ConvertToString(mode);
            if (!string.IsNullOrEmpty(display))
            {
                _cboBaseMode.Items.Add(new ModeItem(mode, display!));
            }
        }

        _cboBaseMode.DisplayMember = nameof(ModeItem.Display);
    }

    private void SelectModeInCombo(PaletteMode mode)
    {
        for (var i = 0; i < _cboBaseMode.Items.Count; i++)
        {
            if (_cboBaseMode.Items[i] is ModeItem item && item.Mode == mode)
            {
                _cboBaseMode.SelectedIndex = i;
                return;
            }
        }
    }

    private void ApplySelectedBaseMode()
    {
        if (_cboBaseMode.SelectedItem is not ModeItem item)
        {
            return;
        }

        _palette.BasePaletteMode = item.Mode;
        RefreshPropertyGrid();
        UpdateStatus();
    }

    private void ApplyStatusStripOverride()
    {
        _palette.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = Color.Lime;
        _palette.ToolMenuStatus.StatusStrip.StatusStripGradientEnd = Color.LimeGreen;
        RefreshPropertyGrid();
        UpdateStatus();
    }

    private void ResetStatusStripOverride()
    {
        _palette.ToolMenuStatus.StatusStrip.ResetStatusStripGradientBegin();
        _palette.ToolMenuStatus.StatusStrip.ResetStatusStripGradientEnd();
        RefreshPropertyGrid();
        UpdateStatus();
    }

    private void RefreshPropertyGrid()
    {
        _propertyGrid.SelectedObject = null;
        _propertyGrid.SelectedObject = _palette;
    }

    private void UpdateStatus()
    {
        var table = _palette.ColorTable;
        var builtin = KryptonManager.GetPaletteForMode(_palette.BasePaletteMode).ColorTable;
        _lblStatus.Text =
            $"BasePaletteMode={_palette.BasePaletteMode}; BasePalette={_palette.BasePalette?.GetType().Name}; " +
            $"ColorTable ToolStripBegin={FormatColor(table.ToolStripGradientBegin)} " +
            $"(builtin {FormatColor(builtin.ToolStripGradientBegin)}); " +
            $"StatusStripBegin={FormatColor(table.StatusStripGradientBegin)}";
    }

    private static string FormatColor(Color color) =>
        color.IsNamedColor ? color.Name : $"#{color.R:X2}{color.G:X2}{color.B:X2}";

    private void OnFormClosed(object? sender, FormClosedEventArgs e)
    {
        if (_savedCustomPalette != null)
        {
            _manager.GlobalCustomPalette = _savedCustomPalette;
        }
        else
        {
            _manager.GlobalCustomPalette = null;
            if (_savedPaletteMode != PaletteMode.Custom)
            {
                _manager.GlobalPaletteMode = _savedPaletteMode;
            }
        }

        _palette.Dispose();
    }

    private sealed class ModeItem
    {
        public ModeItem(PaletteMode mode, string display)
        {
            Mode = mode;
            Display = display;
        }

        public PaletteMode Mode { get; }

        public string Display { get; }
    }
}
