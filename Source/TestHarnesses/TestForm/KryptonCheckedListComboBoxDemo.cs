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
    private readonly KryptonCheckedListComboBox _tagsCombo;
    private readonly KryptonLabel _statusLabel;

    public KryptonCheckedListComboBoxDemo()
    {
        Text = @"KryptonCheckedListComboBox Demo";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(720, 380);
        Padding = new Padding(12);

        var tlp = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4,
            ColumnStyles =
            {
                new ColumnStyle(SizeType.Absolute, 200),
                new ColumnStyle(SizeType.Percent, 100)
            }
        };
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
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

        tlp.Controls.Add(new KryptonLabel { Text = @"Tags (DataSource + ValueMember):" }, 0, 2);
        var tagsSource = new BindingSource
        {
            DataSource = new List<TagDto>
            {
                new TagDto { Id = 1, Name = @"Bug" },
                new TagDto { Id = 2, Name = @"Feature" },
                new TagDto { Id = 3, Name = @"Docs" },
                new TagDto { Id = 4, Name = @"Performance" }
            }
        };
        _tagsCombo = new KryptonCheckedListComboBox { Width = 360 };
        _tagsCombo.DisplayMember = nameof(TagDto.Name);
        _tagsCombo.ValueMember = nameof(TagDto.Id);
        _tagsCombo.DataSource = tagsSource;
        _tagsCombo.CheckedItemsChanged += OnCheckedItemsChanged;
        tlp.Controls.Add(_tagsCombo, 1, 2);

        _statusLabel = new KryptonLabel
        {
            Text = @"Check items in the drop-down; the editor updates live. Press Enter to close (first combo) or click outside. Second combo keeps the popup open on Enter. Third row uses DataSource/DisplayMember/ValueMember; status shows CheckedItemList (Ids).",
            Dock = DockStyle.Top
        };
        var statusHost = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(0, 12, 0, 0) };
        statusHost.Controls.Add(_statusLabel);
        tlp.Controls.Add(statusHost, 0, 3);
        tlp.SetColumnSpan(statusHost, 2);

        Controls.Add(tlp);

        // KryptonCheckedListBox populates bound Items when the handle exists (see RefreshItems);
        // SetItemChecked in the ctor would run before Items.Count is non-zero.
        Load += OnLoadApplyInitialTagChecks;
    }

    private void OnLoadApplyInitialTagChecks(object? sender, EventArgs e)
    {
        Load -= OnLoadApplyInitialTagChecks;

        if (_tagsCombo.Items.Count == 0)
        {
            _tagsCombo.CheckedListBox.RefreshBoundItems();
        }

        if (_tagsCombo.Items.Count > 0)
        {
            _tagsCombo.SetItemChecked(0, true);
        }

        if (_tagsCombo.Items.Count > 2)
        {
            _tagsCombo.SetItemChecked(2, true);
        }

        _tagsCombo.RefreshCheckedSummary();
    }

    private sealed class TagDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    private void OnCheckedItemsChanged(object? sender, EventArgs e)
    {
        if (sender is not KryptonCheckedListComboBox combo)
        {
            return;
        }

        string which = ReferenceEquals(combo, _featuresCombo)
            ? "Features"
            : ReferenceEquals(combo, _colorsCombo)
                ? "Colors"
                : "Tags";
        object[] values = combo.GetCheckedValues();
        string extra = string.Empty;
        if (ReferenceEquals(combo, _tagsCombo))
        {
            var ids = string.Join(", ", combo.CheckedItemList);
            extra = $", value members (CheckedItemList)=[{ids}]";
        }

        _statusLabel.Text =
            $@"[{which}] Editor='{combo.Text}', checked count={values.Length}, items=[{string.Join(", ", values)}]{extra}";
    }
}
