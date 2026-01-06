#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Dialog for selecting Font Awesome icons visually.
/// </summary>
public class FontAwesomeIconPickerDialog : KryptonForm
{
    private FontAwesomeIcon _selectedIcon;
    private ListView _iconListView;
    private TextBox _searchTextBox;
    private ComboBox _styleComboBox;
    private readonly List<FontAwesomeIcon> _allIcons = new();

    public FontAwesomeIconPickerDialog()
    {
        InitializeComponent();
        LoadIcons();
        SelectedIcon = FontAwesomeIcon.Home;
    }

    /// <summary>
    /// Gets or sets the selected Font Awesome icon.
    /// </summary>
    public FontAwesomeIcon SelectedIcon
    {
        get => _selectedIcon;
        set
        {
            _selectedIcon = value;
            UpdateSelection();
        }
    }

    private void InitializeComponent()
    {
        Text = "Font Awesome Icon Picker";
        Size = new Size(600, 500);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;

        var mainPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };

        // Search box
        var searchLabel = new KryptonLabel
        {
            Text = "Search:",
            Location = new Point(10, 10),
            Size = new Size(60, 23)
        };

        _searchTextBox = new TextBox
        {
            Location = new Point(75, 10),
            Size = new Size(300, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        _searchTextBox.TextChanged += SearchTextBox_TextChanged;

        // Style filter
        var styleLabel = new KryptonLabel
        {
            Text = "Style:",
            Location = new Point(385, 10),
            Size = new Size(50, 23)
        };

        _styleComboBox = new ComboBox
        {
            Location = new Point(440, 10),
            Size = new Size(150, 23),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        _styleComboBox.Items.AddRange(new[] { "All", "Solid", "Regular", "Brands" });
        _styleComboBox.SelectedIndex = 0;
        _styleComboBox.SelectedIndexChanged += StyleComboBox_SelectedIndexChanged;

        // Icon list view
        _iconListView = new ListView
        {
            Location = new Point(10, 40),
            Size = new Size(580, 380),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            MultiSelect = false
        };
        _iconListView.Columns.Add("Icon", 200);
        _iconListView.Columns.Add("Name", 200);
        _iconListView.Columns.Add("Unicode", 100);
        _iconListView.DoubleClick += IconListView_DoubleClick;

        // Buttons
        var okButton = new KryptonButton
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Location = new Point(420, 430),
            Size = new Size(75, 25),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right
        };
        okButton.Click += OkButton_Click;

        var cancelButton = new KryptonButton
        {
            Text = "Cancel",
            DialogResult = DialogResult.Cancel,
            Location = new Point(505, 430),
            Size = new Size(75, 25),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right
        };

        mainPanel.Controls.AddRange(new Control[]
        {
            searchLabel,
            _searchTextBox,
            styleLabel,
            _styleComboBox,
            _iconListView,
            okButton,
            cancelButton
        });

        Controls.Add(mainPanel);
    }

    private void LoadIcons()
    {
        _allIcons.Clear();

        // Store all icons from the enum
        foreach (FontAwesomeIcon icon in Enum.GetValues(typeof(FontAwesomeIcon)))
        {
            _allIcons.Add(icon);
        }

        FilterIcons();
    }

    private void SearchTextBox_TextChanged(object? sender, EventArgs e)
    {
        FilterIcons();
    }

    private void StyleComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        FilterIcons();
    }

    private void FilterIcons()
    {
        var searchText = _searchTextBox.Text.ToLowerInvariant();
        var selectedStyleText = _styleComboBox.SelectedItem?.ToString() ?? "All";
        
        FontAwesomeStyle? selectedStyle = selectedStyleText switch
        {
            "Solid" => FontAwesomeStyle.Solid,
            "Regular" => FontAwesomeStyle.Regular,
            "Brands" => FontAwesomeStyle.Brands,
            _ => null
        };

        _iconListView.Items.Clear();
        _iconListView.BeginUpdate();

        try
        {
            foreach (var icon in _allIcons)
            {
                var iconName = icon.ToString().ToLowerInvariant();
                var matchesSearch = string.IsNullOrEmpty(searchText) || iconName.Contains(searchText);
                
                var matchesStyle = selectedStyle == null;
                if (!matchesStyle)
                {
                    // If metadata is not loaded, allow all icons through style filter
                    // since we can't determine which styles are available without metadata
                    if (!FontAwesomeIconMetadataLoader.IsMetadataLoaded)
                    {
                        matchesStyle = true;
                    }
                    else
                    {
                        var availableStyles = FontAwesomeIconMetadataLoader.GetAvailableStyles(iconName);
                        matchesStyle = availableStyles.Contains(selectedStyle.Value);
                    }
                }

                if (matchesSearch && matchesStyle)
                {
                    var styleToUse = selectedStyle ?? FontAwesomeStyle.Solid;
                    var unicode = FontAwesomeHelper.GetUnicodeForIcon(iconName, styleToUse);
                    var item = new ListViewItem(icon.ToString())
                    {
                        Tag = icon,
                        SubItems =
                        {
                            icon.ToString(),
                            $"0x{unicode:X4}"
                        }
                    };

                    _iconListView.Items.Add(item);
                }
            }

            // Sort by name
            _iconListView.Sorting = SortOrder.Ascending;
            _iconListView.Sort();
        }
        finally
        {
            _iconListView.EndUpdate();
        }
    }

    private void OkButton_Click(object? sender, EventArgs e)
    {
        if (_iconListView.SelectedItems.Count > 0 &&
            _iconListView.SelectedItems[0].Tag is FontAwesomeIcon icon)
        {
            _selectedIcon = icon;
        }
    }

    private void IconListView_DoubleClick(object? sender, EventArgs e)
    {
        if (_iconListView.SelectedItems.Count > 0 &&
            _iconListView.SelectedItems[0].Tag is FontAwesomeIcon icon)
        {
            _selectedIcon = icon;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void UpdateSelection()
    {
        foreach (ListViewItem item in _iconListView.Items)
        {
            if (item.Tag is FontAwesomeIcon icon && icon == _selectedIcon)
            {
                item.Selected = true;
                item.EnsureVisible();
                break;
            }
        }
    }
}
