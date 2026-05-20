#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates <see cref="KryptonCheckedListComboBox"/> — multi-select combo with a
/// <see cref="KryptonCheckedListBox"/> drop-down on the <see cref="KryptonComboBoxUserControl"/> stack.
/// </summary>
public sealed class KryptonCheckedListComboBoxDemo : KryptonForm
{
    private readonly KryptonCheckedListComboBox _featuresCombo;
    private readonly KryptonCheckedListComboBox _colorsCombo;
    private readonly KryptonLabel _statusLabel;

    public KryptonCheckedListComboBoxDemo()
    {
        Text = @"KryptonCheckedListComboBox Demo";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(720, 320);
        Padding = new Padding(12);

        var tlp = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3,
            ColumnStyles =
            {
                new ColumnStyle(SizeType.Absolute, 200),
                new ColumnStyle(SizeType.Percent, 100)
            }
        };
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        tlp.Controls.Add(new KryptonLabel { Text = @"Features:" }, 0, 0);
        _featuresCombo = new KryptonCheckedListComboBox { Width = 360 };
        _featuresCombo.Items.AddRange(new object[]
        {
            "Docking", "Navigator", "Ribbon", "Workspace", "Utilities", "Code Editor", "Toast", "Search Box"
        });
        _featuresCombo.SetItemChecked(0, true);
        _featuresCombo.SetItemChecked(2, true);
        _featuresCombo.RefreshCheckedSummary();
        _featuresCombo.CheckedItemsChanged += OnCheckedItemsChanged;
        tlp.Controls.Add(_featuresCombo, 1, 0);

        tlp.Controls.Add(new KryptonLabel { Text = @"Colors ( | sep):" }, 0, 1);
        _colorsCombo = new KryptonCheckedListComboBox
        {
            Width = 360,
            ValueSeparator = @" | ",
            EmptySelectionText = @"(pick colors)",
            CloseDropDownOnEnter = false
        };
        _colorsCombo.Items.AddRange(new object[] { "Tomato", "Goldenrod", "Sea Green", "Steel Blue", "Medium Orchid" });
        _colorsCombo.CheckedItemsChanged += OnCheckedItemsChanged;
        tlp.Controls.Add(_colorsCombo, 1, 1);

        _statusLabel = new KryptonLabel
        {
            Text = @"Check items in the drop-down; the editor updates live. Press Enter to close (first combo) or click outside. Second combo keeps the popup open on Enter.",
            Dock = DockStyle.Top
        };
        var statusHost = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(0, 12, 0, 0) };
        statusHost.Controls.Add(_statusLabel);
        tlp.Controls.Add(statusHost, 0, 2);
        tlp.SetColumnSpan(statusHost, 2);

        Controls.Add(tlp);
    }

    private void OnCheckedItemsChanged(object? sender, EventArgs e)
    {
        if (sender is not KryptonCheckedListComboBox combo)
        {
            return;
        }

        string which = ReferenceEquals(combo, _featuresCombo) ? "Features" : "Colors";
        object[] values = combo.GetCheckedValues();
        _statusLabel.Text = $@"[{which}] Editor='{combo.Text}', checked count={values.Length}, values=[{string.Join(", ", values)}]";
    }
}
