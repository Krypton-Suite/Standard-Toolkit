#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual repro for issue #3367: ButtonSpec hover flicker on KryptonTextBox / KryptonMaskedTextBox.
/// </summary>
public class Bug3367KryptonTextBoxButtonSpecHoverDemo : KryptonForm
{
    public Bug3367KryptonTextBoxButtonSpecHoverDemo()
    {
        Text = @"Bug #3367 - TextBox ButtonSpec Hover Flicker Demo";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(720, 420);
        MinimumSize = new Size(560, 360);

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 88,
            Text =
                @"How to test issue #3367:" + Environment.NewLine +
                @"1) Slowly move the pointer over each ButtonSpec (right edge of the controls below)." + Environment.NewLine +
                @"2) Move between the text area and the ButtonSpecs, then leave the control entirely." + Environment.NewLine +
                @"3) Before the fix, ButtonSpecs flickered on hover; tracking/active chrome should now stay stable."
        };

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            ColumnCount = 1,
            RowCount = 4
        };
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));

        var lblTextBox = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            Values = { Text = @"KryptonTextBox (Generic + Close ButtonSpecs)" }
        };

        var ktbDemo = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            Text = @"Hover the ButtonSpecs on the right"
        };
        AddButtonSpecs(ktbDemo.ButtonSpecs);

        var lblMasked = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            Values = { Text = @"KryptonMaskedTextBox (Generic + Close ButtonSpecs)" }
        };

        var kmtbDemo = new KryptonMaskedTextBox
        {
            Dock = DockStyle.Fill,
            Mask = @"(000) 000-0000",
            Text = @"1234567890"
        };
        AddButtonSpecs(kmtbDemo.ButtonSpecs);

        layout.Controls.Add(lblTextBox, 0, 0);
        layout.Controls.Add(ktbDemo, 0, 1);
        layout.Controls.Add(lblMasked, 0, 2);
        layout.Controls.Add(kmtbDemo, 0, 3);

        Controls.Add(layout);
        Controls.Add(lblInfo);
    }

    private static void AddButtonSpecs(ButtonSpecCollection<ButtonSpecAny> buttonSpecs)
    {
        buttonSpecs.Add(new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            ToolTipBody = @"Generic",
            ToolTipTitle = @"ButtonSpec"
        });
        buttonSpecs.Add(new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            ToolTipBody = @"Clear",
            ToolTipTitle = @"ButtonSpec"
        });
    }
}
