#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Data;
using Krypton.Toolkit;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates <see cref="KryptonMultiColumnComboBox"/> — a ComboBox-style control whose
/// drop-down hosts a multi-column <see cref="KryptonDataGridView"/>. Implements feature request #4237.
/// </summary>
public sealed class KryptonMultiColumnComboBoxDemo : KryptonForm
{
    private readonly KryptonMultiColumnComboBox _countriesCombo;
    private readonly KryptonComboBox _countriesKryptonCombo;
    private readonly KryptonMultiColumnComboBox _productsCombo;
    private readonly KryptonMultiColumnComboBox _citiesCombo;
    private readonly KryptonLabel _statusLabel;

    public KryptonMultiColumnComboBoxDemo()
    {
        Text = @"KryptonMultiColumnComboBox Demo (Issue #4237)";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(920, 460);
        Padding = new Padding(12);

        var tlp = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 5
        };
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var instructions = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Text = @"Open a drop-down (F4 / Alt+Down or the arrow). Click a row or press Enter to select; Escape cancels. " +
                   @"The closed editor is a Krypton combo (ButtonSpecs, rounded corners). Native WinForms ComboBox has no multi-column list, " +
                   @"so the first row compares against KryptonComboBox chrome instead. Header clicks do not commit. The third combo filters as you type."
        };
        tlp.Controls.Add(instructions, 0, 0);
        tlp.SetColumnSpan(instructions, 3);

        tlp.Controls.Add(new KryptonLabel { Text = @"Countries (explicit columns):", Dock = DockStyle.Fill }, 0, 1);

        List<CountryDto> countries = CreateCountries();
        _countriesCombo = new KryptonMultiColumnComboBox
        {
            Dock = DockStyle.Fill,
            DropDownWidth = 420,
            DropDownHeight = 220,
            DisplayMember = nameof(CountryDto.Name),
            ValueMember = nameof(CountryDto.Code)
        };
        _countriesCombo.Columns.Add(new KryptonMultiColumnComboBoxColumn(nameof(CountryDto.Code), @"Code", 60));
        _countriesCombo.Columns.Add(new KryptonMultiColumnComboBoxColumn(nameof(CountryDto.Name), @"Country", 180));
        _countriesCombo.Columns.Add(new KryptonMultiColumnComboBoxColumn(nameof(CountryDto.Currency), @"Currency", 80));
        _countriesCombo.DataSource = countries;
        _countriesCombo.SelectedValue = @"UK";
        _countriesCombo.SelectedIndexChanged += OnSelectionChanged;

        ButtonSpecAny clearSpec = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close
        };
        clearSpec.Click += (_, _) => _countriesCombo.SelectedIndex = -1;
        _countriesCombo.ButtonSpecs.Add(clearSpec);

        tlp.Controls.Add(_countriesCombo, 1, 1);

        _countriesKryptonCombo = new KryptonComboBox
        {
            Dock = DockStyle.Fill,
            DropDownStyle = ComboBoxStyle.DropDownList,
            DisplayMember = nameof(CountryDto.Name),
            ValueMember = nameof(CountryDto.Code),
            DataSource = new List<CountryDto>(countries)
        };
        tlp.Controls.Add(_countriesKryptonCombo, 2, 1);

        tlp.Controls.Add(new KryptonLabel { Text = @"Products (auto columns / DataTable):", Dock = DockStyle.Fill }, 0, 2);
        _productsCombo = new KryptonMultiColumnComboBox
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = true,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            DisplayMember = @"Name",
            ValueMember = @"Id",
            DropDownWidth = 480,
            DropDownHeight = 240
        };
        _productsCombo.DataSource = CreateProductsTable();
        _productsCombo.SelectedIndexChanged += OnSelectionChanged;
        tlp.Controls.Add(_productsCombo, 1, 2);
        tlp.SetColumnSpan(_productsCombo, 2);

        tlp.Controls.Add(new KryptonLabel { Text = @"Cities (type to filter):", Dock = DockStyle.Fill }, 0, 3);
        _citiesCombo = new KryptonMultiColumnComboBox
        {
            Dock = DockStyle.Fill,
            ReadOnlyEditor = false,
            AutoOpenOnType = true,
            MinFilterLength = 1,
            DisplayMember = nameof(CityDto.Name),
            ValueMember = nameof(CityDto.Name),
            DropDownWidth = 420,
            DropDownHeight = 240
        };
        _citiesCombo.Columns.Add(new KryptonMultiColumnComboBoxColumn(nameof(CityDto.Name), @"City", 140));
        _citiesCombo.Columns.Add(new KryptonMultiColumnComboBoxColumn(nameof(CityDto.Country), @"Country", 140));
        _citiesCombo.Columns.Add(new KryptonMultiColumnComboBoxColumn(nameof(CityDto.Population), @"Population", 90)
        {
            Alignment = DataGridViewContentAlignment.MiddleRight,
            Format = @"N0"
        });
        _citiesCombo.DataSource = CreateCities();
        _citiesCombo.CueHint.CueHintText = @"Start typing a city name…";
        _citiesCombo.SelectedIndexChanged += OnSelectionChanged;
        tlp.Controls.Add(_citiesCombo, 1, 3);
        tlp.SetColumnSpan(_citiesCombo, 2);

        _statusLabel = new KryptonLabel
        {
            Text = @"Pick a country, product, or city. Extra close ButtonSpec on the first combo clears the selection. Switch themes from the main TestForm to verify palette.",
            Dock = DockStyle.Top
        };
        var statusHost = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(0, 12, 0, 0) };
        statusHost.Controls.Add(_statusLabel);
        tlp.Controls.Add(statusHost, 0, 4);
        tlp.SetColumnSpan(statusHost, 3);

        Controls.Add(tlp);

        Load += OnLoadApplyInitialCountry;
    }

    private void OnLoadApplyInitialCountry(object? sender, EventArgs e)
    {
        Load -= OnLoadApplyInitialCountry;

        if (_countriesCombo.SelectedIndex < 0)
        {
            _countriesCombo.SelectedValue = @"UK";
        }
    }

    private void OnSelectionChanged(object? sender, EventArgs e)
    {
        if (sender is not KryptonMultiColumnComboBox combo)
        {
            return;
        }

        string which = ReferenceEquals(combo, _countriesCombo)
            ? "Countries"
            : ReferenceEquals(combo, _productsCombo)
                ? "Products"
                : "Cities";

        _statusLabel.Text =
            $@"[{which}] Text='{combo.Text}', SelectedIndex={combo.SelectedIndex}, SelectedValue='{combo.SelectedValue}', SelectedItem='{combo.SelectedItem}'";
    }

    private static List<CountryDto> CreateCountries() =>
    [
        new CountryDto { Code = @"UK", Name = @"United Kingdom", Currency = @"GBP" },
        new CountryDto { Code = @"US", Name = @"United States", Currency = @"USD" },
        new CountryDto { Code = @"DE", Name = @"Germany", Currency = @"EUR" },
        new CountryDto { Code = @"FR", Name = @"France", Currency = @"EUR" },
        new CountryDto { Code = @"JP", Name = @"Japan", Currency = @"JPY" },
        new CountryDto { Code = @"AU", Name = @"Australia", Currency = @"AUD" }
    ];

    private static DataTable CreateProductsTable()
    {
        var table = new DataTable(@"Products");
        table.Columns.Add(@"Id", typeof(int));
        table.Columns.Add(@"Name", typeof(string));
        table.Columns.Add(@"Category", typeof(string));
        table.Columns.Add(@"Price", typeof(decimal));
        table.Rows.Add(1, @"Docking", @"Layout", 0m);
        table.Rows.Add(2, @"Navigator", @"Layout", 0m);
        table.Rows.Add(3, @"Ribbon", @"Command", 0m);
        table.Rows.Add(4, @"Workspace", @"Layout", 0m);
        table.Rows.Add(5, @"Toolkit", @"Core", 0m);
        return table;
    }

    private static List<CityDto> CreateCities() =>
    [
        new CityDto { Name = @"London", Country = @"United Kingdom", Population = 9000000 },
        new CityDto { Name = @"Manchester", Country = @"United Kingdom", Population = 550000 },
        new CityDto { Name = @"New York", Country = @"United States", Population = 8400000 },
        new CityDto { Name = @"Berlin", Country = @"Germany", Population = 3700000 },
        new CityDto { Name = @"Paris", Country = @"France", Population = 2100000 },
        new CityDto { Name = @"Tokyo", Country = @"Japan", Population = 14000000 },
        new CityDto { Name = @"Sydney", Country = @"Australia", Population = 5300000 },
        new CityDto { Name = @"Lisbon", Country = @"Portugal", Population = 550000 }
    ];

    private sealed class CountryDto
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Currency { get; set; } = string.Empty;
    }

    private sealed class CityDto
    {
        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public int Population { get; set; }

        public override string ToString() => Name;
    }
}
