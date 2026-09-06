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
/// Demo for Issue #4339: <see cref="KryptonComboBox"/> <see cref="ComboBoxStyle.Simple"/> parity with native WinForms.
/// </summary>
public sealed class Feature4339ComboBoxSimpleStyleDemo : KryptonForm
{
    private static readonly object[] SampleItems =
    {
        "Apple", "Apricot", "Banana", "Blackberry", "Blueberry",
        "Cherry", "Grape", "Kiwi", "Lemon", "Mango",
        "Orange", "Peach", "Pear", "Pineapple", "Strawberry"
    };

    private readonly KryptonLabel _statusLabel;
    private readonly KryptonComboBox _kryptonSimple;
    private readonly ComboBox _nativeSimple;

    public Feature4339ComboBoxSimpleStyleDemo()
    {
        Text = @"4339 — KryptonComboBox Simple drop-down style";
        Size = new Size(980, 640);
        MinimumSize = new Size(860, 520);
        StartPosition = FormStartPosition.CenterScreen;

        _nativeSimple = CreateNativeSimple();
        _kryptonSimple = CreateKryptonSimple();
        _nativeSimple.SelectedIndexChanged += (_, _) => UpdateStatus();
        _kryptonSimple.SelectedIndexChanged += (_, _) => UpdateStatus();
        _kryptonSimple.TextUpdate += (_, _) => UpdateStatus();

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 108,
            Padding = new Padding(12),
            Text =
                "Issue #4339: ComboBoxStyle.Simple shows an editable text box and an always-visible list (no drop-down button). " +
                "Left column is native WinForms; right column is KryptonComboBox. " +
                "Type in the edit box, click a list item, resize the Simple combos, switch DropDown / DropDownList / Simple on the live Krypton control, toggle Enabled, and change theme. " +
                "Krypton should match native Simple behaviour: list stays open, selection updates the edit text, and DropDown/DropDownList stay single-line with a drop button."
        };

        var theme = new KryptonThemeComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        var toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12, 8, 12, 4),
            WrapContents = false
        };

        var styleCombo = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 180
        };
        styleCombo.Items.AddRange(new object[]
        {
            ComboBoxStyle.Simple,
            ComboBoxStyle.DropDown,
            ComboBoxStyle.DropDownList
        });
        styleCombo.SelectedItem = ComboBoxStyle.Simple;
        styleCombo.SelectedIndexChanged += (_, _) =>
        {
            if (styleCombo.SelectedItem is ComboBoxStyle style)
            {
                _kryptonSimple.DropDownStyle = style;
                UpdateStatus();
            }
        };

        var enabledButton = new KryptonButton
        {
            Text = @"Toggle Enabled",
            AutoSize = true
        };
        enabledButton.Click += (_, _) =>
        {
            bool enabled = !_kryptonSimple.Enabled;
            _kryptonSimple.Enabled = enabled;
            _nativeSimple.Enabled = enabled;
            UpdateStatus();
        };

        toolbar.Controls.Add(new KryptonLabel { Values = { Text = @"Live Krypton style:" }, AutoSize = true, Padding = new Padding(0, 6, 8, 0) });
        toolbar.Controls.Add(styleCombo);
        toolbar.Controls.Add(enabledButton);

        _statusLabel = new KryptonLabel
        {
            Dock = DockStyle.Bottom,
            Height = 28,
            Values = { Text = @"Selection: (none)" }
        };

        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4,
            Padding = new Padding(12)
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));

        table.Controls.Add(CreateCaption(@"Native ComboBox — Simple"), 0, 0);
        table.Controls.Add(CreateCaption(@"KryptonComboBox — Simple (live style above)"), 1, 0);
        table.Controls.Add(_nativeSimple, 0, 1);
        table.Controls.Add(_kryptonSimple, 1, 1);
        table.Controls.Add(CreateCaption(@"Krypton DropDown (editable + popup)"), 0, 2);
        table.Controls.Add(CreateCaption(@"Krypton DropDownList (popup only)"), 1, 2);
        table.Controls.Add(CreateKryptonStyle(ComboBoxStyle.DropDown), 0, 3);
        table.Controls.Add(CreateKryptonStyle(ComboBoxStyle.DropDownList), 1, 3);

        Controls.Add(table);
        Controls.Add(_statusLabel);
        Controls.Add(toolbar);
        Controls.Add(theme);
        Controls.Add(instructions);

        Load += (_, _) =>
        {
            _nativeSimple.SelectedIndex = 0;
            _kryptonSimple.SelectedIndex = 0;
            UpdateStatus();
        };
    }

    private static KryptonLabel CreateCaption(string text) =>
        new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Values = { Text = text }
        };

    private static ComboBox CreateNativeSimple()
    {
        var combo = new ComboBox
        {
            Dock = DockStyle.Fill,
            DropDownStyle = ComboBoxStyle.Simple,
            IntegralHeight = true
        };
        combo.Items.AddRange(SampleItems);
        return combo;
    }

    private static KryptonComboBox CreateKryptonSimple()
    {
        var combo = new KryptonComboBox
        {
            Dock = DockStyle.Fill,
            DropDownStyle = ComboBoxStyle.Simple,
            IntegralHeight = true
        };
        combo.CueHint.CueHintText = @"Type or pick a fruit";
        combo.Items.AddRange(SampleItems);
        return combo;
    }

    private static KryptonComboBox CreateKryptonStyle(ComboBoxStyle style)
    {
        var combo = new KryptonComboBox
        {
            Dock = DockStyle.Fill,
            DropDownStyle = style,
            IntegralHeight = false
        };
        combo.Items.AddRange(SampleItems);
        combo.SelectedIndex = 0;
        return combo;
    }

    private void UpdateStatus()
    {
        string native = _nativeSimple.SelectedItem as string ?? _nativeSimple.Text;
        string krypton = _kryptonSimple.SelectedItem as string ?? _kryptonSimple.Text;
        _statusLabel.Values.Text =
            $@"Native: {native}    |    Krypton ({_kryptonSimple.DropDownStyle}, Enabled={_kryptonSimple.Enabled}): {krypton}";
    }
}
