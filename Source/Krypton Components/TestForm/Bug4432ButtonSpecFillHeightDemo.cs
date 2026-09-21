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
/// Demo for issue #4432: <see cref="ButtonSpec.FillHeight"/> and
/// <see cref="ButtonSpecEdgeArrange.StackAlongEdge"/> on tall input hosts.
/// </summary>
public sealed class Bug4432ButtonSpecFillHeightDemo : KryptonForm
{
    private readonly KryptonCheckBox _chkFillHeight;
    private readonly KryptonCheckBox _chkStackAlongEdge;
    private readonly List<ButtonSpecAny> _toggleSpecs = new();
    private readonly List<Control> _hosts = new();

    public Bug4432ButtonSpecFillHeightDemo()
    {
        Text = @"Bug #4432 - ButtonSpec FillHeight / Edge Arrange";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(820, 720);
        MinimumSize = new Size(640, 560);

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 112,
            Text =
                @"How to test issue #4432:" + Environment.NewLine +
                @"1) Tall hosts show a centred Close ButtonSpec next to a FillHeight Next ButtonSpec (side-by-side)." + Environment.NewLine +
                @"2) Toggle ""Apply FillHeight to all specs"" to stretch every ButtonSpec." + Environment.NewLine +
                @"3) Toggle ""StackAlongEdge"" to stack same-edge ButtonSpecs vertically (host ButtonSpecEdgeArrange)." + Environment.NewLine +
                @"4) Form chrome / headers stay side-by-side unless a host opts in."
        };

        _chkFillHeight = new KryptonCheckBox
        {
            Text = @"Apply FillHeight to all specs",
            Checked = false,
            AutoSize = true
        };
        _chkFillHeight.CheckedChanged += (_, _) => ApplyFillHeight(_chkFillHeight.Checked);

        _chkStackAlongEdge = new KryptonCheckBox
        {
            Text = @"StackAlongEdge (vertical stack on Far edge)",
            Checked = false,
            AutoSize = true
        };
        _chkStackAlongEdge.CheckedChanged += (_, _) =>
            ApplyEdgeArrange(_chkStackAlongEdge.Checked
                ? ButtonSpecEdgeArrange.StackAlongEdge
                : ButtonSpecEdgeArrange.SideBySide);

        var toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12, 4, 12, 4),
            WrapContents = true
        };
        toolbar.Controls.Add(_chkFillHeight);
        toolbar.Controls.Add(_chkStackAlongEdge);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            ColumnCount = 1,
            RowCount = 8,
            AutoScroll = true
        };
        for (var i = 0; i < 8; i++)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        AddHostRow(layout, 0, @"KryptonTextBox Multiline (Height 96)", CreateTextBox(96));
        AddHostRow(layout, 2, @"KryptonComboBox Simple (Height 120)", CreateComboBox(120));
        AddHostRow(layout, 4, @"KryptonDateTimePicker (larger font → taller preferred height)", CreateDateTimePicker());
        AddHostRow(layout, 6, @"KryptonMaskedTextBox AutoSize=false (Height 80)", CreateMaskedTextBox(80));

        Controls.Add(layout);
        Controls.Add(toolbar);
        Controls.Add(lblInfo);
    }

    private void AddHostRow(TableLayoutPanel layout, int row, string caption, Control host)
    {
        var label = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            Values = { Text = caption }
        };
        layout.Controls.Add(label, 0, row);
        layout.Controls.Add(host, 0, row + 1);
        _hosts.Add(host);
    }

    private KryptonTextBox CreateTextBox(int height)
    {
        var control = new KryptonTextBox
        {
            Dock = DockStyle.Top,
            Multiline = true,
            AutoSize = false,
            Height = height,
            Text = @"Centred Close vs FillHeight Next"
        };
        AddPair(control.ButtonSpecs);
        return control;
    }

    private KryptonComboBox CreateComboBox(int height)
    {
        var control = new KryptonComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.Simple,
            Height = height
        };
        control.Items.AddRange(new object[] { @"Alpha", @"Beta", @"Gamma" });
        control.SelectedIndex = 0;
        AddPair(control.ButtonSpecs);
        return control;
    }

    private KryptonDateTimePicker CreateDateTimePicker()
    {
        // DateTimePicker is FixedHeight; enlarge the font so PreferredHeight grows and FillHeight is visible.
        var control = new KryptonDateTimePicker
        {
            Dock = DockStyle.Top,
            Format = DateTimePickerFormat.Long,
            Font = new Font(@"Segoe UI", 18f)
        };
        AddPair(control.ButtonSpecs);
        return control;
    }

    private KryptonMaskedTextBox CreateMaskedTextBox(int height)
    {
        var control = new KryptonMaskedTextBox
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = height,
            Mask = @"(000) 000-0000",
            Text = @"5551234567"
        };
        AddPair(control.ButtonSpecs);
        return control;
    }

    private void AddPair(ButtonSpecCollection<ButtonSpecAny> buttonSpecs)
    {
        var centred = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            ToolTipTitle = @"Centred (default)",
            ToolTipBody = @"FillHeight = false",
            FillHeight = false
        };
        _toggleSpecs.Add(centred);
        buttonSpecs.Add(centred);

        var filled = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Next,
            ToolTipTitle = @"FillHeight",
            ToolTipBody = @"FillHeight = true",
            FillHeight = true
        };
        _toggleSpecs.Add(filled);
        buttonSpecs.Add(filled);
    }

    private void ApplyFillHeight(bool fillHeight)
    {
        foreach (ButtonSpecAny spec in _toggleSpecs)
        {
            spec.FillHeight = fillHeight;
        }
    }

    private void ApplyEdgeArrange(ButtonSpecEdgeArrange arrange)
    {
        foreach (Control host in _hosts)
        {
            switch (host)
            {
                case KryptonTextBox textBox:
                    textBox.ButtonSpecEdgeArrange = arrange;
                    break;
                case KryptonComboBox comboBox:
                    comboBox.ButtonSpecEdgeArrange = arrange;
                    break;
                case KryptonDateTimePicker dateTimePicker:
                    dateTimePicker.ButtonSpecEdgeArrange = arrange;
                    break;
                case KryptonMaskedTextBox maskedTextBox:
                    maskedTextBox.ButtonSpecEdgeArrange = arrange;
                    break;
            }
        }
    }
}
