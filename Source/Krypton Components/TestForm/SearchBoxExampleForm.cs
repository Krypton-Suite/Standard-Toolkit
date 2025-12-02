using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm
{
    public partial class SearchBoxExampleForm : KryptonForm
    {
         private readonly List<string> _allItems = new();
        private DataTable? _dataTable;
        private readonly List<object> _richSuggestions = new();

        public SearchBoxExampleForm()
        {
            InitializeComponent();
            InitializeListBox();
            InitializeDataGridView();
            InitializeSearchBoxes();
        }

        private void InitializeDataGridView()
        {
            // Create a DataTable with sample fruit data
            _dataTable = new DataTable();
            _dataTable.Columns.Add("Name", typeof(string));
            _dataTable.Columns.Add("Color", typeof(string));
            _dataTable.Columns.Add("Season", typeof(string));

            // Populate with sample data
            _dataTable.Rows.Add("Apple", "Red/Green", "Fall");
            _dataTable.Rows.Add("Banana", "Yellow", "Year-round");
            _dataTable.Rows.Add("Cherry", "Red", "Summer");
            _dataTable.Rows.Add("Date", "Brown", "Fall");
            _dataTable.Rows.Add("Elderberry", "Purple", "Summer");
            _dataTable.Rows.Add("Fig", "Purple/Green", "Summer");
            _dataTable.Rows.Add("Grape", "Purple/Green", "Fall");
            _dataTable.Rows.Add("Honeydew", "Green", "Summer");
            _dataTable.Rows.Add("Kiwi", "Brown/Green", "Year-round");
            _dataTable.Rows.Add("Lemon", "Yellow", "Year-round");
            _dataTable.Rows.Add("Mango", "Orange/Yellow", "Summer");
            _dataTable.Rows.Add("Orange", "Orange", "Winter");
            _dataTable.Rows.Add("Papaya", "Orange", "Year-round");
            _dataTable.Rows.Add("Quince", "Yellow", "Fall");
            _dataTable.Rows.Add("Raspberry", "Red", "Summer");
            _dataTable.Rows.Add("Strawberry", "Red", "Spring");
            _dataTable.Rows.Add("Tangerine", "Orange", "Winter");
            _dataTable.Rows.Add("Watermelon", "Green/Red", "Summer");
            _dataTable.Rows.Add("Apricot", "Orange", "Summer");
            _dataTable.Rows.Add("Blueberry", "Blue", "Summer");

            // Bind to DataGridView
            kdgvTestData.DataSource = _dataTable;
            kdgvTestData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            kdgvTestData.ReadOnly = true;
            kdgvTestData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void InitializeSearchBoxes()
        {
            // ============================================
            // SearchBox 1: Basic ListBox suggestions with search history
            // ============================================
            ksbBasicSearchWithHistory.SearchBoxValues.PlaceholderText = "Search fruits (ListBox, with history)...";
            ksbBasicSearchWithHistory.SearchBoxValues.ShowSearchButton = true;
            ksbBasicSearchWithHistory.SearchBoxValues.ShowClearButton = true;
            ksbBasicSearchWithHistory.SearchBoxValues.ClearOnEscape = true;
            ksbBasicSearchWithHistory.SearchBoxValues.EnableSuggestions = true;
            ksbBasicSearchWithHistory.SearchBoxValues.SuggestionMaxCount = 8;
            ksbBasicSearchWithHistory.SearchBoxValues.SuggestionDisplayType = SearchSuggestionDisplayType.ListBox;
            
            // Enable search history
            ksbBasicSearchWithHistory.SearchBoxValues.EnableSearchHistory = true;
            ksbBasicSearchWithHistory.SearchBoxValues.SearchHistoryMaxCount = 10;
            
            // Set minimum search length (show suggestions after 1 character)
            ksbBasicSearchWithHistory.SearchBoxValues.MinimumSearchLength = 1;

            // Set up custom suggestions from the listbox items
            ksbBasicSearchWithHistory.SetSearchSuggestions(_allItems);

            // Handle events
            ksbBasicSearchWithHistory.TextChanged += KryptonSearchBox1_TextChanged;
            ksbBasicSearchWithHistory.Search += KryptonSearchBox_Search;
            ksbBasicSearchWithHistory.SearchCleared += KryptonSearchBox1_Cleared;
            ksbBasicSearchWithHistory.SuggestionSelected += KryptonSearchBox1_SuggestionSelected;

            // ============================================
            // SearchBox 2: DataGridView highlighting (no suggestions)
            // ============================================
            ksbDataGridViewWithHighlighting.SearchBoxValues.PlaceholderText = "Search DataGridView (highlights matches)...";
            ksbDataGridViewWithHighlighting.SearchBoxValues.ShowSearchButton = true;
            ksbDataGridViewWithHighlighting.SearchBoxValues.ShowClearButton = true;
            ksbDataGridViewWithHighlighting.SearchBoxValues.ClearOnEscape = true;
            ksbDataGridViewWithHighlighting.SearchBoxValues.EnableSuggestions = false;

            ksbDataGridViewWithHighlighting.TextChanged += KryptonSearchBox2_TextChanged;
            ksbDataGridViewWithHighlighting.Search += KryptonSearchBox2_Search;
            ksbDataGridViewWithHighlighting.SearchCleared += KryptonSearchBox2_Cleared;

            // ============================================
            // SearchBox 3: Rich suggestions with icons (ListBox)
            // ============================================
            InitializeRichSuggestions();
            ksbRichSuggestionsWithIcons.SearchBoxValues.PlaceholderText = "Rich suggestions (icons, descriptions)...";
            ksbRichSuggestionsWithIcons.SearchBoxValues.ShowSearchButton = true;
            ksbRichSuggestionsWithIcons.SearchBoxValues.ShowClearButton = true;
            ksbRichSuggestionsWithIcons.SearchBoxValues.EnableSuggestions = true;
            ksbRichSuggestionsWithIcons.SearchBoxValues.SuggestionMaxCount = 8;
            ksbRichSuggestionsWithIcons.SearchBoxValues.SuggestionDisplayType = SearchSuggestionDisplayType.ListBox;
            ksbRichSuggestionsWithIcons.SearchBoxValues.MinimumSearchLength = 1;
            
            // Set rich suggestions (with icons and descriptions)
            ksbRichSuggestionsWithIcons.SetRichSuggestions(_richSuggestions);
            
            ksbRichSuggestionsWithIcons.SuggestionSelected += KryptonSearchBox3_SuggestionSelected;

            // ============================================
            // SearchBox 4: DataGridView with multiple columns
            // ============================================
            InitializeDataGridViewSuggestions();
            ksbDataGridViewWithMultipleColumns.SearchBoxValues.PlaceholderText = "Multi-column DataGridView suggestions...";
            ksbDataGridViewWithMultipleColumns.SearchBoxValues.ShowSearchButton = true;
            ksbDataGridViewWithMultipleColumns.SearchBoxValues.ShowClearButton = true;
            ksbDataGridViewWithMultipleColumns.SearchBoxValues.EnableSuggestions = true;
            ksbDataGridViewWithMultipleColumns.SearchBoxValues.SuggestionMaxCount = 8;
            ksbDataGridViewWithMultipleColumns.SearchBoxValues.SuggestionDisplayType = SearchSuggestionDisplayType.DataGridView;
            ksbDataGridViewWithMultipleColumns.SearchBoxValues.MinimumSearchLength = 1;
            
            // Set up multi-column DataGridView
            var columns = new List<SearchSuggestionColumnDefinition>
            {
                new SearchSuggestionColumnDefinition("Name", "Name", 
                    obj => obj is FruitItem fi ? fi.Name : (obj is IContentValues cv ? cv.GetShortText() : obj?.ToString())),
                new SearchSuggestionColumnDefinition("Color", "Color", 
                    obj => obj is FruitItem fi ? fi.Color : string.Empty)
                {
                    Width = 100,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                },
                new SearchSuggestionColumnDefinition("Season", "Season", 
                    obj => obj is FruitItem fi ? fi.Season : string.Empty)
                {
                    Width = 80,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                }
            };
            ksbDataGridViewWithMultipleColumns.SetDataGridViewColumns(columns);
            
            // Set rich suggestions for DataGridView (with icons and descriptions)
            ksbDataGridViewWithMultipleColumns.SetRichSuggestions(_richSuggestions);
            
            ksbDataGridViewWithMultipleColumns.SuggestionSelected += KryptonSearchBox4_SuggestionSelected;

            // ============================================
            // SearchBox 5: Custom filtering
            // ============================================
            ksbCustomFiltering.SearchBoxValues.PlaceholderText = "Custom filter (starts with only)...";
            ksbCustomFiltering.SearchBoxValues.ShowSearchButton = true;
            ksbCustomFiltering.SearchBoxValues.ShowClearButton = true;
            ksbCustomFiltering.SearchBoxValues.EnableSuggestions = true;
            ksbCustomFiltering.SearchBoxValues.SuggestionMaxCount = 8;
            ksbCustomFiltering.SearchBoxValues.SuggestionDisplayType = SearchSuggestionDisplayType.ListBox;
            ksbCustomFiltering.SearchBoxValues.MinimumSearchLength = 1;
            
            // Set custom filter (only show items that start with search text)
            ksbCustomFiltering.CustomFilter = (searchText, suggestions) =>
            {
                var searchLower = searchText.ToLower();
                return suggestions.Where(s =>
                {
                    string? text = null;
                    if (s is IContentValues cv)
                    {
                        text = cv.GetShortText();
                    }
                    else
                    {
                        text = s?.ToString();
                    }
                    return !string.IsNullOrEmpty(text) && text.ToLower().StartsWith(searchLower);
                });
            };
            
            ksbCustomFiltering.SetSearchSuggestions(_allItems);
            ksbCustomFiltering.SuggestionSelected += KryptonSearchBox5_SuggestionSelected;
        }

        private void InitializeRichSuggestions()
        {
            // Create rich suggestions with icons and descriptions
            // Using fruit data from DataGridView
            var fruitData = new[]
            {
                new { Name = "Apple", Color = "Red/Green", Season = "Fall", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Banana", Color = "Yellow", Season = "Year-round", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Cherry", Color = "Red", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Date", Color = "Brown", Season = "Fall", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Elderberry", Color = "Purple", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Fig", Color = "Purple/Green", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Grape", Color = "Purple/Green", Season = "Fall", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Honeydew", Color = "Green", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Kiwi", Color = "Brown/Green", Season = "Year-round", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Lemon", Color = "Yellow", Season = "Year-round", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Mango", Color = "Orange/Yellow", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Orange", Color = "Orange", Season = "Winter", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Papaya", Color = "Orange", Season = "Year-round", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Quince", Color = "Yellow", Season = "Fall", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Raspberry", Color = "Red", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Strawberry", Color = "Red", Season = "Spring", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Tangerine", Color = "Orange", Season = "Winter", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Watermelon", Color = "Green/Red", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Apricot", Color = "Orange", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Blueberry", Color = "Blue", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar }
            };

            foreach (var fruit in fruitData)
            {
                try
                {
                    // Try to get an icon (using a simple approach - you can use actual fruit icons if available)
                    Image? icon = null;
                    try
                    {
                        var iconObj = GraphicsExtensions.ExtractIconFromImageres((int)fruit.Icon, IconSize.Small);
                        if (iconObj != null)
                        {
                            icon = iconObj.ToBitmap();
                            iconObj.Dispose();
                        }
                    }
                    catch
                    {
                        // Fallback if icon extraction fails
                    }

                    // Create KryptonListItem with icon and description
                    var item = new KryptonListItem(
                        fruit.Name,
                        $"{fruit.Color} - {fruit.Season}",
                        icon
                    );
                    
                    // Store additional data in Tag
                    item.Tag = new FruitItem { Name = fruit.Name, Color = fruit.Color, Season = fruit.Season };
                    
                    _richSuggestions.Add(item);
                }
                catch
                {
                    // Fallback to simple string if icon creation fails
                    _richSuggestions.Add(new FruitItem { Name = fruit.Name, Color = fruit.Color, Season = fruit.Season });
                }
            }
        }

        private void InitializeDataGridViewSuggestions()
        {
            // This is already done in InitializeRichSuggestions
            // The same rich suggestions are used for DataGridView
        }

        // Helper class for fruit data
        private class FruitItem : IContentValues
        {
            public string Name { get; set; } = string.Empty;
            public string Color { get; set; } = string.Empty;
            public string Season { get; set; } = string.Empty;

            public string GetShortText() => Name;
            public string GetLongText() => $"{Color} - {Season}";
            public Image? GetImage(PaletteState state) => null;
            public Color GetImageTransparentColor(PaletteState state) => throw new NotImplementedException();
        }

        private void InitializeListBox()
        {
            // Populate listbox with sample data
            _allItems.AddRange(new[]
            {
                "Apple", "Banana", "Cherry", "Date", "Elderberry",
                "Fig", "Grape", "Honeydew", "Kiwi", "Lemon",
                "Mango", "Orange", "Papaya", "Quince", "Raspberry",
                "Strawberry", "Tangerine", "Watermelon", "Apricot", "Blueberry",
                "Coconut", "Dragonfruit", "Grapefruit", "Lime", "Peach",
                "Pear", "Pineapple", "Plum", "Pomegranate", "Blackberry"
            });

            klbFruits.Items.AddRange(_allItems.ToArray());
            klblFruitsCount.Text = $"Total items: {_allItems.Count}";
        }

        private void KryptonSearchBox1_TextChanged(object? sender, EventArgs e)
        {
            // Real-time filtering as user types
            FilterListBox(ksbBasicSearchWithHistory.Text);
        }

        private void FilterListBox(string searchText)
        {
            klbFruits.BeginUpdate();
            klbFruits.Items.Clear();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Show all items when search is empty
                klbFruits.Items.AddRange(_allItems.ToArray());
                klblFruitsCount.Text = $"Total items: {_allItems.Count}";
            }
            else
            {
                // Filter items that contain the search text (case-insensitive)
                // Using IndexOf for compatibility with older .NET Framework versions
                var searchTextLower = searchText.ToLower();
                var filtered = _allItems.Where(item => 
                    item.ToLower().IndexOf(searchTextLower) >= 0).ToList();
                
                klbFruits.Items.AddRange(filtered.ToArray());
                klblFruitsCount.Text = $"Found {filtered.Count} of {_allItems.Count} items";
            }

            klbFruits.EndUpdate();
        }

        private void KryptonSearchBox_Search(object? sender, SearchEventArgs e)
        {
            // Example: Handle search event
            klblSearchMatches.Text = $"Searching for: {e.SearchText}";
            
            // Filter the listbox
            FilterListBox(e.SearchText);
            
            if (!string.IsNullOrEmpty(e.SearchText))
            {
                klblFoundItems.Text = $"Found {klbFruits.Items.Count} matching items";
            }
            else
            {
                klblFoundItems.Text = "Showing all items";
            }
        }

        private void KryptonSearchBox2_TextChanged(object? sender, EventArgs e)
        {
            // Real-time highlighting in DataGridView as user types
            HighlightDataGridView(ksbDataGridViewWithHighlighting.Text);
        }

        private void KryptonSearchBox2_Search(object? sender, SearchEventArgs e)
        {
            // Highlight search results in DataGridView
            HighlightDataGridView(e.SearchText);
            klblDataGridViewResults.Text = string.IsNullOrEmpty(e.SearchText) 
                ? "DataGridView search cleared" 
                : $"Highlighting: {e.SearchText}";
        }

        private void KryptonSearchBox2_Cleared(object? sender, EventArgs e)
        {
            // Clear highlighting when search is cleared
            HighlightDataGridView(string.Empty);
            klblDataGridViewResults.Text = "DataGridView search cleared";
        }

        private void HighlightDataGridView(string searchText)
        {
            if (kdgvTestData != null)
            {
                // Use KryptonDataGridView's built-in HighlightSearch method
                // Empty string clears the highlighting
                kdgvTestData.HighlightSearch(searchText);
            }
        }

        private void KryptonButton1_Click(object? sender, EventArgs e)
        {
            // Example: Programmatically trigger search
            ksbBasicSearchWithHistory.PerformSearch();
        }

        private void KryptonSearchBox1_Cleared(object? sender, EventArgs e)
        {
            // Handle clear event - update UI when search is cleared
            FilterListBox(string.Empty);
            klblSearchMatches.Text = "Search cleared";
            klblFoundItems.Text = "Showing all items";
        }

        private void KryptonButton2_Click(object? sender, EventArgs e)
        {
            // Example: Clear search programmatically
            // The Cleared event will be raised automatically
            ksbBasicSearchWithHistory.Clear();
        }

        private void KryptonSearchBox1_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            // Example: Handle suggestion selection from the dropdown
            // The text is already set in the search box, but we can perform additional actions
            klblSearchMatches.Text = $"Selected: {e.Suggestion ?? ksbBasicSearchWithHistory.Text}";
            FilterListBox(ksbBasicSearchWithHistory.Text);
            
            // Show search history count
            if (ksbBasicSearchWithHistory.SearchBoxValues.EnableSearchHistory)
            {
                klblFoundItems.Text = $"History: {ksbBasicSearchWithHistory.SearchHistory.Count} items";
            }
        }

        private void KryptonSearchBox3_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            // Handle rich suggestion selection
            string displayText = e.Suggestion ?? ksbRichSuggestionsWithIcons.Text;
            
            // Access the full object if needed
            if (e.SuggestionObject is KryptonListItem kli)
            {
                displayText = $"{kli.ShortText} - {kli.LongText}";
            }
            else if (e.SuggestionObject is FruitItem fi)
            {
                displayText = $"{fi.Name} ({fi.Color}, {fi.Season})";
            }
            
            klblRichSuggestionResults.Text = $"Rich suggestion: {displayText}";
        }

        private void KryptonSearchBox4_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            // Handle DataGridView multi-column suggestion selection
            string displayText = e.Suggestion ?? ksbDataGridViewWithMultipleColumns.Text;
            
            if (e.SuggestionObject is FruitItem fi)
            {
                displayText = $"{fi.Name} | {fi.Color} | {fi.Season}";
            }
            
            klblMultiColumnResults.Text = $"Multi-column: {displayText}";
        }

        private void KryptonSearchBox5_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            // Handle custom filter suggestion selection
            klblCustomFilterResults.Text = $"Custom filter: {e.Suggestion ?? ksbCustomFiltering.Text}";
        }
    }
}
