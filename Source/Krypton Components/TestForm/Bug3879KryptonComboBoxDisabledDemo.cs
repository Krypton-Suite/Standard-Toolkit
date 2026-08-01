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
/// Reproduces issue #3879: KryptonComboBox disabled at design/startup should use theme colors.
/// </summary>
public sealed class Bug3879KryptonComboBoxDisabledDemo : KryptonForm
{
    private readonly KryptonWrapLabel _lblInfo;
    private readonly KryptonThemeComboBox _themeComboBox;
    private readonly KryptonButton _btnToggleEnabled;
    private readonly KryptonComboBox _dropDownDesignDisabled;
    private readonly KryptonComboBox _dropDownCtorDisabled;
    private readonly KryptonComboBox _dropDownListDesignDisabled;

    public Bug3879KryptonComboBoxDisabledDemo()
    {
        Text = @"Bug #3879 - KryptonComboBox Disabled Startup Colors";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(720, 420);
        MinimumSize = new Size(640, 360);

        _lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 110,
            Text =
                @"How to test issue #3879:" + Environment.NewLine +
                @"1) On load, all disabled combos below should use Krypton disabled palette colors (not system gray)." + Environment.NewLine +
                @"2) Top row uses ComboBoxStyle.DropDown (the reported scenario); bottom row uses DropDownList for comparison." + Environment.NewLine +
                @"3) Switch themes and use Toggle Enabled to confirm colors stay correct."
        };

        _themeComboBox = new KryptonThemeComboBox
        {
            Dock = DockStyle.Top,
            DropDownWidth = 260,
            IntegralHeight = false
        };

        _btnToggleEnabled = new KryptonButton
        {
            Dock = DockStyle.Top,
            Height = 34,
            Text = @"Toggle Enabled"
        };
        _btnToggleEnabled.Click += OnToggleEnabledClick;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4,
            Padding = new Padding(12)
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var dropDownCaption = CreateCaption(@"DropDown — disabled in designer");
        layout.Controls.Add(dropDownCaption, 0, 0);
        layout.SetColumnSpan(dropDownCaption, 2);

        _dropDownDesignDisabled = CreateComboBox(ComboBoxStyle.DropDown, disabledInDesigner: true);
        _dropDownCtorDisabled = CreateComboBox(ComboBoxStyle.DropDown, disabledInDesigner: false);

        layout.Controls.Add(CreateFieldPanel(@"Designer Enabled=false", _dropDownDesignDisabled), 0, 1);
        layout.Controls.Add(CreateFieldPanel(@"Ctor sets Enabled=false", _dropDownCtorDisabled), 1, 1);

        var dropDownListCaption = CreateCaption(@"DropDownList — disabled in designer");
        layout.Controls.Add(dropDownListCaption, 0, 2);
        layout.SetColumnSpan(dropDownListCaption, 2);

        _dropDownListDesignDisabled = CreateComboBox(ComboBoxStyle.DropDownList, disabledInDesigner: true);
        layout.Controls.Add(CreateFieldPanel(@"Designer Enabled=false", _dropDownListDesignDisabled), 0, 3);
        layout.SetColumnSpan(layout.Controls[layout.Controls.Count - 1], 2);

        Controls.Add(layout);
        Controls.Add(_btnToggleEnabled);
        Controls.Add(_themeComboBox);
        Controls.Add(_lblInfo);

        _dropDownCtorDisabled.Enabled = false;
        _dropDownDesignDisabled.SelectedIndex = 0;
        _dropDownCtorDisabled.SelectedIndex = 0;
        _dropDownListDesignDisabled.SelectedIndex = 0;
    }

    private static KryptonWrapLabel CreateCaption(string text) =>
        new()
        {
            AutoSize = true,
            Margin = new Padding(0, 8, 0, 4),
            Text = text
        };

    private static KryptonComboBox CreateComboBox(ComboBoxStyle style, bool disabledInDesigner)
    {
        var comboBox = new KryptonComboBox
        {
            DropDownStyle = style,
            DropDownWidth = 240,
            IntegralHeight = false,
            Size = new Size(240, 22),
            Text = style == ComboBoxStyle.DropDown ? @"Editable disabled text" : string.Empty
        };
        comboBox.Items.AddRange(["Item 1", "Item 2", "Item 3"]);

        if (disabledInDesigner)
        {
            comboBox.BeginInit();
            comboBox.Enabled = false;
            comboBox.EndInit();
        }

        return comboBox;
    }

    private static Control CreateFieldPanel(string caption, KryptonComboBox comboBox)
    {
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 0, 8, 8)
        };

        var label = new KryptonWrapLabel
        {
            AutoSize = true,
            Dock = DockStyle.Top,
            Text = caption
        };

        comboBox.Dock = DockStyle.Top;
        comboBox.Margin = new Padding(0, 4, 0, 0);

        panel.Controls.Add(comboBox);
        panel.Controls.Add(label);
        return panel;
    }

    private void OnToggleEnabledClick(object? sender, EventArgs e)
    {
        _dropDownDesignDisabled.Enabled = !_dropDownDesignDisabled.Enabled;
        _dropDownCtorDisabled.Enabled = !_dropDownCtorDisabled.Enabled;
        _dropDownListDesignDisabled.Enabled = !_dropDownListDesignDisabled.Enabled;
    }
}
