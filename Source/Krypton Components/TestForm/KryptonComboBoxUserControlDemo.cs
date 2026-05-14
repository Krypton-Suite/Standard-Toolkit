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
/// Demonstrates <see cref="KryptonComboBoxUserControl"/> &#8211; a ComboBox-style control whose
/// drop-down hosts any <see cref="UserControl"/>. Implements feature request #3443.
/// </summary>
public sealed class KryptonComboBoxUserControlDemo : KryptonForm
{
    private readonly KryptonComboBoxUserControl _treePickerCombo;
    private readonly KryptonComboBoxUserControl _gridPickerCombo;
    private readonly KryptonComboBoxUserControl _plainCombo;
    private readonly KryptonComboBoxUserControl _filterCombo;
    private readonly KryptonLabel _statusLabel;

    public KryptonComboBoxUserControlDemo()
    {
        Text = @"KryptonComboBoxUserControl Demo (Issue #3443)";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(720, 410);
        Padding = new Padding(12);

        var tlp = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 5,
            ColumnStyles = { new ColumnStyle(SizeType.Absolute, 220), new ColumnStyle(SizeType.Percent, 100) },
        };
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        // 1) Tree-picker drop-down (implements IKryptonDropDownUserControl)
        tlp.Controls.Add(new KryptonLabel { Text = @"Tree picker:" }, 0, 0);
        _treePickerCombo = new KryptonComboBoxUserControl
        {
            Width = 320,
            DropContent = new TreePickerControl(),
            DropDownResizable = true,
            DropDownWidth = 280,
            DropDownHeight = 240
        };
        _treePickerCombo.ValueCommitted += OnValueCommitted;
        tlp.Controls.Add(_treePickerCombo, 1, 0);

        // 2) Grid-picker drop-down (also implements the contract; multi-column data)
        tlp.Controls.Add(new KryptonLabel { Text = @"Grid picker:" }, 0, 1);
        _gridPickerCombo = new KryptonComboBoxUserControl
        {
            Width = 320,
            ReadOnlyEditor = true,
            DropContent = new GridPickerControl(),
            DropDownResizable = true,
            DropDownWidth = 380,
            DropDownHeight = 220,
            DropDownAlign = LeftRightAlignment.Right
        };
        _gridPickerCombo.ValueCommitted += OnValueCommitted;
        tlp.Controls.Add(_gridPickerCombo, 1, 1);

        // 3) Plain UserControl drop-down (no contract; demonstrates the fallback path)
        tlp.Controls.Add(new KryptonLabel { Text = @"Plain control:" }, 0, 2);
        _plainCombo = new KryptonComboBoxUserControl
        {
            Width = 320,
            DropContent = BuildPlainColorPalette(),
            DropDownWidth = 240,
            DropDownHeight = 160
        };
        _plainCombo.ValueCommitted += OnValueCommitted;
        tlp.Controls.Add(_plainCombo, 1, 2);

        // 4) Filter-as-you-type city picker (implements both contracts; AutoOpenOnType)
        tlp.Controls.Add(new KryptonLabel { Text = @"Filter-as-you-type:" }, 0, 3);
        _filterCombo = new KryptonComboBoxUserControl
        {
            Width = 320,
            DropContent = new CityFilterControl(),
            AutoOpenOnType = true,
            MinFilterLength = 1,
            DropDownWidth = 280,
            DropDownHeight = 200
        };
        _filterCombo.CueHint.CueHintText = @"Start typing a city name…";
        _filterCombo.ValueCommitted += OnValueCommitted;
        tlp.Controls.Add(_filterCombo, 1, 3);

        // Status label
        _statusLabel = new KryptonLabel
        {
            Text = @"Pick a value. Try F4 / Alt+Down to open. The bottom combo opens automatically as you type and Up/Down/Enter navigate the list.",
            Dock = DockStyle.Top
        };
        var statusHost = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(0, 12, 0, 0) };
        statusHost.Controls.Add(_statusLabel);
        tlp.Controls.Add(statusHost, 0, 4);
        tlp.SetColumnSpan(statusHost, 2);

        Controls.Add(tlp);
    }

    private void OnValueCommitted(object? sender, KryptonDropDownCommitEventArgs e)
    {
        var which = ReferenceEquals(sender, _treePickerCombo) ? "Tree"
                  : ReferenceEquals(sender, _gridPickerCombo) ? "Grid"
                  : ReferenceEquals(sender, _filterCombo) ? "Filter"
                  : "Plain";
        _statusLabel.Text = $@"[{which}] DisplayText='{e.DisplayText}', Value='{e.Value}'";
    }

    private static UserControl BuildPlainColorPalette()
    {
        // A user control without IKryptonDropDownUserControl - the popup just shows it
        var panel = new UserControl { BackColor = Color.WhiteSmoke };
        var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(6) };
        Color[] colors = { Color.Tomato, Color.Goldenrod, Color.MediumSeaGreen, Color.SteelBlue, Color.MediumOrchid, Color.SlateGray };
        foreach (var c in colors)
        {
            var btn = new KryptonButton
            {
                Width = 80,
                Height = 28,
                Margin = new Padding(4)
            };
            btn.Values.Text = c.Name;
            btn.StateCommon.Back.Color1 = c;
            btn.StateCommon.Back.Color2 = c;
            flow.Controls.Add(btn);
        }
        panel.Controls.Add(flow);
        return panel;
    }

    #region Tree picker drop-down

    private sealed class TreePickerControl : UserControl, IKryptonDropDownUserControl
    {
        private readonly KryptonTreeView _tree;

        public TreePickerControl()
        {
            _tree = new KryptonTreeView { Dock = DockStyle.Fill };
            _tree.Nodes.Add(BuildNode("Continents",
                BuildNode("Europe", BuildNode("United Kingdom"), BuildNode("Germany"), BuildNode("France")),
                BuildNode("North America", BuildNode("USA"), BuildNode("Canada"), BuildNode("Mexico")),
                BuildNode("Asia", BuildNode("Japan"), BuildNode("South Korea"), BuildNode("India"))));
            foreach (TreeNode root in _tree.Nodes)
            {
                root.Expand();
                foreach (TreeNode child in root.Nodes)
                {
                    child.Expand();
                }
            }
            _tree.NodeMouseDoubleClick += OnNodeDoubleClick;
            _tree.KeyDown += OnTreeKeyDown;
            Controls.Add(_tree);
        }

        public Size GetPreferredDropSize(Size proposedSize) => new Size(260, 240);

        public void OnDropDownOpening(object owner) { }

        public void OnDropDownOpened(object owner) => _tree.Focus();

        public void OnDropDownClosing(object owner, ref bool cancel) { }

        public void OnDropDownClosed(object owner) { }

        public event EventHandler<KryptonDropDownCommitEventArgs>? CommitValue;
        public event EventHandler? RequestClose;

        private static TreeNode BuildNode(string text, params TreeNode[] children)
        {
            var node = new TreeNode(text);
            node.Nodes.AddRange(children);
            return node;
        }

        private void OnNodeDoubleClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 0)
            {
                CommitValue?.Invoke(this, new KryptonDropDownCommitEventArgs(e.Node.FullPath, e.Node.Text));
            }
        }

        private void OnTreeKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _tree.SelectedNode is { Nodes.Count: 0 } selected)
            {
                CommitValue?.Invoke(this, new KryptonDropDownCommitEventArgs(selected.FullPath, selected.Text));
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }

    #endregion

    #region Grid picker drop-down

    private sealed class GridPickerControl : UserControl, IKryptonDropDownUserControl
    {
        private readonly KryptonDataGridView _grid;

        public GridPickerControl()
        {
            _grid = new KryptonDataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _grid.Columns.Add("Code", "Code");
            _grid.Columns.Add("Name", "Name");
            _grid.Columns.Add("Currency", "Currency");
            _grid.Rows.Add("UK", "United Kingdom", "GBP");
            _grid.Rows.Add("US", "United States", "USD");
            _grid.Rows.Add("DE", "Germany", "EUR");
            _grid.Rows.Add("FR", "France", "EUR");
            _grid.Rows.Add("JP", "Japan", "JPY");
            _grid.Rows.Add("AU", "Australia", "AUD");
            _grid.Columns[0].Width = 60;
            _grid.CellDoubleClick += OnGridCellDoubleClick;
            _grid.KeyDown += OnGridKeyDown;
            Controls.Add(_grid);
        }

        public Size GetPreferredDropSize(Size proposedSize) => new Size(360, 200);

        public void OnDropDownOpening(object owner) { }

        public void OnDropDownOpened(object owner) => _grid.Focus();

        public void OnDropDownClosing(object owner, ref bool cancel) { }

        public void OnDropDownClosed(object owner) { }

        public event EventHandler<KryptonDropDownCommitEventArgs>? CommitValue;
        public event EventHandler? RequestClose;

        private void OnGridCellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            CommitFromRow(_grid.Rows[e.RowIndex]);
        }

        private void OnGridKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _grid.CurrentRow != null)
            {
                CommitFromRow(_grid.CurrentRow);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void CommitFromRow(DataGridViewRow row)
        {
            var code = row.Cells[0].Value?.ToString() ?? string.Empty;
            var name = row.Cells[1].Value?.ToString() ?? string.Empty;
            var currency = row.Cells[2].Value?.ToString() ?? string.Empty;
            CommitValue?.Invoke(this, new KryptonDropDownCommitEventArgs(
                value: new { Code = code, Name = name, Currency = currency },
                displayText: $"{name} ({currency})"));
        }
    }

    #endregion

    #region Filter-as-you-type drop-down

    private sealed class CityFilterControl : UserControl, IKryptonDropDownUserControl, IKryptonDropDownFilterable
    {
        private static readonly string[] AllCities =
        {
            "Amsterdam", "Athens", "Auckland", "Bangkok", "Barcelona", "Beijing", "Berlin",
            "Brisbane", "Brussels", "Budapest", "Buenos Aires", "Cairo", "Cape Town", "Chicago",
            "Copenhagen", "Dubai", "Dublin", "Edinburgh", "Florence", "Geneva", "Helsinki",
            "Hong Kong", "Honolulu", "Istanbul", "Jakarta", "Johannesburg", "Kuala Lumpur",
            "Lisbon", "London", "Los Angeles", "Madrid", "Manila", "Melbourne", "Mexico City",
            "Milan", "Montreal", "Moscow", "Mumbai", "Nairobi", "Naples", "New York", "Oslo",
            "Paris", "Prague", "Rio de Janeiro", "Rome", "San Francisco", "Santiago", "Sao Paulo",
            "Seoul", "Shanghai", "Singapore", "Stockholm", "Sydney", "Taipei", "Tokyo", "Toronto",
            "Vancouver", "Venice", "Vienna", "Warsaw", "Wellington", "Zurich"
        };

        private readonly KryptonListBox _list;

        public CityFilterControl()
        {
            _list = new KryptonListBox { Dock = DockStyle.Fill };
            _list.DoubleClick += OnListDoubleClick;
            Controls.Add(_list);
            ApplyFilter(string.Empty);
        }

        public Size GetPreferredDropSize(Size proposedSize) => new Size(260, 220);

        public void OnDropDownOpening(object owner) { }

        public void OnDropDownOpened(object owner)
        {
            // Don't steal focus - the host wants the editor to keep focus while the user types
        }

        public void OnDropDownClosing(object owner, ref bool cancel) { }

        public void OnDropDownClosed(object owner) { }

        public event EventHandler<KryptonDropDownCommitEventArgs>? CommitValue;
#pragma warning disable CS0067 // Event is part of the IKryptonDropDownUserControl contract; this scenario doesn't request close from inside
        public event EventHandler? RequestClose;
#pragma warning restore CS0067

        public bool ApplyFilter(string text)
        {
            _list.BeginUpdate();
            try
            {
                _list.Items.Clear();
                IEnumerable<string> matches = string.IsNullOrEmpty(text)
                    ? AllCities
                    : AllCities.Where(c => c.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);

                foreach (string city in matches)
                {
                    _list.Items.Add(city);
                }

                if (_list.Items.Count > 0)
                {
                    _list.SelectedIndex = 0;
                }
            }
            finally
            {
                _list.EndUpdate();
            }

            return _list.Items.Count > 0;
        }

        public void NavigateSelection(int direction)
        {
            if (_list.Items.Count == 0)
            {
                return;
            }

            int idx = _list.SelectedIndex + direction;
            if (idx < 0)
            {
                idx = _list.Items.Count - 1;
            }
            else if (idx >= _list.Items.Count)
            {
                idx = 0;
            }

            _list.SelectedIndex = idx;
            _list.TopIndex = Math.Max(0, idx - 4);
        }

        public bool CommitSelection()
        {
            if (_list.SelectedItem is string city)
            {
                CommitValue?.Invoke(this, new KryptonDropDownCommitEventArgs(city, city));
                return true;
            }

            return false;
        }

        private void OnListDoubleClick(object? sender, EventArgs e) => CommitSelection();
    }

    #endregion
}
