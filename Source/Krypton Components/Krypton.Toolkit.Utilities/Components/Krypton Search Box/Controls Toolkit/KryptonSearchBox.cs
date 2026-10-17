#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using GraphicsExtensions = Krypton.Toolkit.GraphicsExtensions;

//using SearchBoxImageResources = Krypton.Toolkit.Utilities.

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides a modern search input control with search icon and clear button.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTextBox), "ToolboxBitmaps.KryptonTextBox.bmp")]
[DefaultEvent(nameof(Search))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"Provides a modern search input control with search icon and clear button.")]
public partial class KryptonSearchBox : KryptonTextBox
{
    #region Instance Fields

    internal ButtonSpecAny? _searchButton;
    internal ButtonSpecAny? _clearButton;

    internal readonly List<string> _suggestions;

    internal readonly List<object> _richSuggestions;

    private KryptonSearchBoxValues _searchBoxValues;

    private SuggestionPopup? _suggestionPopup;

    internal readonly List<SearchSuggestionColumnDefinition> _dataGridViewColumns;

    private int _selectedSuggestionIndex;
    private bool _isNavigatingSuggestions;
    internal readonly List<string> _searchHistory;
    private System.Windows.Forms.Timer? _suggestionUpdateTimer;
    private SearchSuggestionDisplayType _popupDisplayType;
    private readonly List<object> _lastShownSuggestions;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the search is triggered (Enter key pressed or search button clicked).
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the search is triggered.")]
    public event EventHandler<SearchEventArgs>? Search;

    /// <summary>
    /// Occurs when the search text is cleared (clear button clicked, Escape key pressed, or programmatically cleared).
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the search text is cleared.")]
    public event EventHandler? SearchCleared;

    /// <summary>
    /// Occurs when a suggestion is selected from the suggestion list.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a suggestion is selected from the suggestion list.")]
    public event EventHandler<SuggestionSelectedEventArgs>? SuggestionSelected;

    /// <summary>
    /// Raises the Search event.
    /// </summary>
    /// <param name="e">A SearchEventArgs that contains the event data.</param>
    protected virtual void OnSearch(SearchEventArgs e) => Search?.Invoke(this, e);

    /// <summary>
    /// Raises the Cleared event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnSearchCleared(EventArgs e) => SearchCleared?.Invoke(this, e);

    /// <summary>
    /// Raises the SuggestionSelected event.
    /// </summary>
    /// <param name="e">A SuggestionSelectedEventArgs that contains the event data.</param>
    protected virtual void OnSuggestionSelected(SuggestionSelectedEventArgs e) => SuggestionSelected?.Invoke(this, e);

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonSearchBox class.
    /// </summary>
    public KryptonSearchBox()
    {
        // Initialize instance fields
        _suggestions = new List<string>();
        _richSuggestions = new List<object>();
        _selectedSuggestionIndex = -1;
        _searchHistory = new List<string>();
        _lastShownSuggestions = new List<object>();

        _dataGridViewColumns = new List<SearchSuggestionColumnDefinition>();

        // Default column for DataGridView (single "Suggestion" column)
        _dataGridViewColumns.Add(new SearchSuggestionColumnDefinition("Suggestion", "Suggestion",
            obj => obj is IContentValues cv ? cv.GetShortText() : obj?.ToString()));

        _selectedSuggestionIndex = -1;

        _searchBoxValues = new KryptonSearchBoxValues(this);
        _popupDisplayType = _searchBoxValues.SuggestionDisplayType;

        // Disable standard auto-complete
        base.AutoCompleteMode = AutoCompleteMode.None;
        base.AutoCompleteSource = AutoCompleteSource.None;

        // Enable button spec tooltips
        AllowButtonSpecToolTips = true;

        // Hook into text box events
        TextChanged += OnTextChangedInternal;
        KeyDown += OnKeyDownInternal;
        KeyUp += OnKeyUpInternal;
        LostFocus += OnLostFocusInternal;

        // Create search button (positioned on the right, first)
        _searchButton = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Style = PaletteButtonStyle.ButtonSpec,
            Edge = PaletteRelativeEdgeAlign.Far,
            Image = SearchBoxImageResources.Search_Button_Search_Icon_16_x_16, // GetSearchIcon(),
            ToolTipTitle = KryptonManager.Strings.SearchBoxStrings.SearchBoxSearchButtonToolTipTitle,
            ToolTipBody = KryptonManager.Strings.SearchBoxStrings.SearchBoxSearchButtonToolTipDescription
        };
        _searchButton.Click += OnSearchButtonClick;

        // Create clear button (positioned on the right, after search button)
        _clearButton = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Style = PaletteButtonStyle.ButtonSpec,
            Edge = PaletteRelativeEdgeAlign.Far,
            Image = SearchBoxImageResources.Search_Button_Close_Icon_16_x_16, // GetClearIcon(),
            ToolTipTitle = KryptonManager.Strings.SearchBoxStrings.ClearButtonToolTipTitle,
            ToolTipBody = KryptonManager.Strings.SearchBoxStrings.ClearSearchBoxToolTipDescription,
            Visible = false
        };
        _clearButton.Click += OnClearButtonClick;

        // Add buttons to text box
        ButtonSpecs.Add(_searchButton);
        ButtonSpecs.Add(_clearButton);

        // Update clear button visibility
        UpdateClearButtonVisibility();

        // Initialize placeholder text
        OnPlaceholderTextChanged();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            TextChanged -= OnTextChangedInternal;
            KeyDown -= OnKeyDownInternal;
            KeyUp -= OnKeyUpInternal;
            LostFocus -= OnLostFocusInternal;

            if (_searchButton != null)
            {
                _searchButton.Click -= OnSearchButtonClick;
            }

            if (_clearButton != null)
            {
                _clearButton.Click -= OnClearButtonClick;
            }

            StopSuggestionUpdateTimer();
            if (_suggestionUpdateTimer != null)
            {
                _suggestionUpdateTimer.Tick -= OnSuggestionUpdateTimerTick;
                _suggestionUpdateTimer.Dispose();
                _suggestionUpdateTimer = null;
            }

            HideSuggestions();

            if (_suggestionPopup != null)
            {
                _suggestionPopup.SuggestionSelected -= OnSuggestionPopupSelected;
                _suggestionPopup.Dispose();
                _suggestionPopup = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public

    /// <summary>
    /// Gets access to the search box configuration values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Search box configuration values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonSearchBoxValues SearchBoxValues => _searchBoxValues;

    public bool ShouldSerializeSearchBoxValues() => !SearchBoxValues.IsDefault;

    private void ResetSearchBoxValues() => SearchBoxValues.Reset();

    /// <summary>
    /// Gets the collection of suggestion strings.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of suggestion strings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<string> Suggestions => _suggestions;

    /// <summary>
    /// Gets the collection of search history items.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<string> SearchHistory => _searchHistory.AsReadOnly();

    /// <summary>
    /// Gets or sets a custom filter function for suggestions.
    /// If set, this function will be used instead of the default filtering logic.
    /// The function receives the search text and the collection of suggestion objects, and returns the filtered collection.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<string, IEnumerable<object>, IEnumerable<object>>? CustomFilter
    {
        get => _searchBoxValues.CustomFilter;
        set => _searchBoxValues.CustomFilter = value;
    }

    /// <summary>
    /// Gets the collection of column definitions for DataGridView suggestion display.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Column definitions for DataGridView suggestion display. Only used when SuggestionDisplayType is DataGridView.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<SearchSuggestionColumnDefinition> DataGridViewColumns => _dataGridViewColumns;

    /// <summary>
    /// Sets the column definitions for DataGridView suggestion display.
    /// </summary>
    /// <param name="columns">The column definitions.</param>
    public void SetDataGridViewColumns(IEnumerable<SearchSuggestionColumnDefinition> columns)
    {
        if (columns == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(columns));
        }

        _dataGridViewColumns.Clear();
        _dataGridViewColumns.AddRange(columns);

        // Recreate popup if it exists to apply new columns
        if (_suggestionPopup != null)
        {
            _suggestionPopup.Dispose();
            _suggestionPopup = null;
        }
    }

    /// <summary>
    /// Gets access to the search button specification.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny? SearchButton => _searchButton;

    /// <summary>
    /// Gets access to the clear button specification.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny? ClearButton => _clearButton;

    /// <summary>
    /// Clears the search text.
    /// </summary>
    public new void Clear()
    {
        StopSuggestionUpdateTimer();
        base.Clear();
        Focus();
        UpdateClearButtonVisibility();
        HideSuggestions();
        OnSearchCleared(EventArgs.Empty);
    }

    /// <summary>
    /// Triggers the search event.
    /// </summary>
    public void PerformSearch()
    {
        if (!string.IsNullOrEmpty(Text))
        {
            // Add to search history if enabled
            if (_searchBoxValues.EnableSearchHistory)
            {
                AddToSearchHistory(Text);
            }
        }

        HideSuggestions();
        OnSearch(new SearchEventArgs(Text));
    }

    /// <summary>
    /// Sets the search suggestions from a collection of strings.
    /// </summary>
    /// <param name="suggestions">The collection of suggestion strings.</param>
    public void SetSearchSuggestions(IEnumerable<string> suggestions)
    {
        if (suggestions == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(suggestions));
        }

        _suggestions.Clear();
        _suggestions.AddRange(suggestions);
    }

    /// <summary>
    /// Adds a search term to the search history.
    /// </summary>
    /// <param name="searchText">The search text to add.</param>
    public void AddToSearchHistory(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return;
        }

        // Remove if already exists (to move to top)
        _searchHistory.Remove(searchText);

        // Add to beginning
        _searchHistory.Insert(0, searchText);

        // Trim to max count
        while (_searchHistory.Count > _searchBoxValues.SearchHistoryMaxCount)
        {
            _searchHistory.RemoveAt(_searchHistory.Count - 1);
        }
    }

    /// <summary>
    /// Clears the search history.
    /// </summary>
    public void ClearSearchHistory()
    {
        _searchHistory.Clear();
    }

    /// <summary>
    /// Sets rich suggestions that support IContentValues (icons, descriptions, etc.).
    /// </summary>
    /// <param name="suggestions">Collection of suggestion objects (can be strings or IContentValues).</param>
    public void SetRichSuggestions(IEnumerable<object> suggestions)
    {
        if (suggestions == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(suggestions));
        }

        _richSuggestions.Clear();
        _richSuggestions.AddRange(suggestions);
    }

    /// <summary>
    /// Adds a rich suggestion item.
    /// </summary>
    /// <param name="suggestion">The suggestion object (string or IContentValues).</param>
    public void AddRichSuggestion(object suggestion)
    {
        if (suggestion != null)
        {
            _richSuggestions.Add(suggestion);
        }
    }

    /// <summary>
    /// Clears all rich suggestions.
    /// </summary>
    public void ClearRichSuggestions()
    {
        _richSuggestions.Clear();
    }

    #endregion

    #region Implementation
    private void OnTextChangedInternal(object? sender, EventArgs e)
    {
        UpdateClearButtonVisibility();

        if (!_searchBoxValues.EnableSuggestions || _isNavigatingSuggestions)
        {
            return;
        }

        // Empty / below minimum length: hide immediately (do not wait for debounce).
        if (string.IsNullOrEmpty(Text) || Text.Length < _searchBoxValues.MinimumSearchLength)
        {
            StopSuggestionUpdateTimer();
            HideSuggestions();
            return;
        }

        ScheduleSuggestionUpdate();
    }

    private void OnKeyDownInternal(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (IsSuggestionPopupShowing() && _selectedSuggestionIndex >= 0)
            {
                // Select the highlighted suggestion
                SelectSuggestion(_selectedSuggestionIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                // Perform search
                e.Handled = true;
                e.SuppressKeyPress = true;
                PerformSearch();
            }
        }
        else if (e.KeyCode == Keys.Escape)
        {
            if (IsSuggestionPopupShowing())
            {
                // Hide suggestions
                HideSuggestions();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (_searchBoxValues.ClearOnEscape)
            {
                // Clear text
                e.Handled = true;
                e.SuppressKeyPress = true;
                Clear();
            }
        }
        else if (e.KeyCode == Keys.Down)
        {
            if (IsSuggestionPopupShowing())
            {
                NavigateSuggestions(1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        else if (e.KeyCode == Keys.Up)
        {
            if (IsSuggestionPopupShowing())
            {
                NavigateSuggestions(-1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }

    private void OnKeyUpInternal(object? sender, KeyEventArgs e)
    {
        _isNavigatingSuggestions = false;
    }

    private void OnLostFocusInternal(object? sender, EventArgs e)
    {
        // Hide suggestions when focus is lost (with a small delay to allow clicking on suggestions)
        if (!IsSuggestionPopupShowing())
        {
            return;
        }

        // Check if focus is going to the popup or its child controls
        if (_suggestionPopup!.HasFocus())
        {
            // Focus is going to the popup, don't hide
            return;
        }

        // Use a timer to delay hiding, allowing click events to process
        var timer = new System.Windows.Forms.Timer { Interval = 200 };
        timer.Tick += (s, args) =>
        {
            timer.Stop();
            timer.Dispose();

            // Double-check that focus hasn't moved to the popup
            if (_suggestionPopup != null && !_suggestionPopup.HasFocus())
            {
                HideSuggestions();
            }
        };
        timer.Start();
    }

    private void OnSearchButtonClick(object? sender, EventArgs e)
    {
        PerformSearch();
    }

    private void OnClearButtonClick(object? sender, EventArgs e)
    {
        Clear();
    }

    private void UpdateClearButtonVisibility()
    {
        if (_clearButton != null)
        {
            _clearButton.Visible = _searchBoxValues.ShowClearButton && !string.IsNullOrEmpty(Text);
        }
    }

    internal void OnSearchBoxValuesChanged()
    {
        // Update button visibility
        if (_searchButton != null)
        {
            _searchButton.Visible = _searchBoxValues.ShowSearchButton;
        }
        UpdateClearButtonVisibility();

        // Handle suggestion changes
        if (!_searchBoxValues.EnableSuggestions)
        {
            StopSuggestionUpdateTimer();
            HideSuggestions();
        }

        // Dispose existing popup only when display type changes
        if (_suggestionPopup != null && _popupDisplayType != _searchBoxValues.SuggestionDisplayType)
        {
            _suggestionPopup.SuggestionSelected -= OnSuggestionPopupSelected;
            _suggestionPopup.Dispose();
            _suggestionPopup = null;
            _lastShownSuggestions.Clear();
        }

        _popupDisplayType = _searchBoxValues.SuggestionDisplayType;

        // Trim history if max count changed
        while (_searchHistory.Count > _searchBoxValues.SearchHistoryMaxCount)
        {
            _searchHistory.RemoveAt(_searchHistory.Count - 1);
        }
    }

    internal void OnSuggestionUpdateDelayChanged()
    {
        if (_suggestionUpdateTimer != null)
        {
            _suggestionUpdateTimer.Interval = Math.Max(1, _searchBoxValues.SuggestionUpdateDelay);
        }
    }

    internal void OnPlaceholderTextChanged()
    {
        CueHint.CueHintText = _searchBoxValues.PlaceholderText;
    }

    private void ScheduleSuggestionUpdate()
    {
        var delay = _searchBoxValues.SuggestionUpdateDelay;
        if (delay <= 0)
        {
            StopSuggestionUpdateTimer();
            UpdateSuggestions();
            return;
        }

        EnsureSuggestionUpdateTimer();
        _suggestionUpdateTimer!.Stop();
        _suggestionUpdateTimer.Interval = delay;
        _suggestionUpdateTimer.Start();
    }

    private void EnsureSuggestionUpdateTimer()
    {
        if (_suggestionUpdateTimer != null)
        {
            return;
        }

        _suggestionUpdateTimer = new System.Windows.Forms.Timer();
        _suggestionUpdateTimer.Tick += OnSuggestionUpdateTimerTick;
    }

    private void OnSuggestionUpdateTimerTick(object? sender, EventArgs e)
    {
        StopSuggestionUpdateTimer();
        UpdateSuggestions();
    }

    private void StopSuggestionUpdateTimer()
    {
        if (_suggestionUpdateTimer != null)
        {
            _suggestionUpdateTimer.Stop();
        }
    }

    private bool IsSuggestionPopupShowing() =>
        _suggestionPopup != null && _suggestionPopup.IsShowing;

    private void UpdateSuggestions()
    {
        if (!_searchBoxValues.EnableSuggestions || string.IsNullOrEmpty(Text))
        {
            HideSuggestions();
            return;
        }

        // Check minimum search length
        if (Text.Length < _searchBoxValues.MinimumSearchLength)
        {
            HideSuggestions();
            return;
        }

        var searchText = Text;
        var maxCount = _searchBoxValues.SuggestionMaxCount;
        var filtered = new List<object>(Math.Min(maxCount, 16));

        // Prefer Values.CustomFilter (also set via the control CustomFilter property).
        var customFilter = _searchBoxValues.CustomFilter;
        if (customFilter != null)
        {
            foreach (var item in customFilter(searchText, EnumerateAllSuggestions()))
            {
                filtered.Add(item);
                if (filtered.Count >= maxCount)
                {
                    break;
                }
            }
        }
        else
        {
            // Single-pass string suggestions
            for (var i = 0; i < _suggestions.Count && filtered.Count < maxCount; i++)
            {
                var suggestion = _suggestions[i];
                if (!string.IsNullOrEmpty(suggestion) &&
                    suggestion.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filtered.Add(suggestion);
                }
            }

            // Then rich suggestions until the cap
            for (var i = 0; i < _richSuggestions.Count && filtered.Count < maxCount; i++)
            {
                var item = _richSuggestions[i];
                if (MatchesSuggestion(item, searchText))
                {
                    filtered.Add(item);
                }
            }
        }

        // Add search history if enabled and no other suggestions
        if (filtered.Count == 0 && _searchBoxValues.EnableSearchHistory && _searchHistory.Count > 0)
        {
            for (var i = 0; i < _searchHistory.Count && filtered.Count < maxCount; i++)
            {
                var historyItem = _searchHistory[i];
                if (!string.IsNullOrEmpty(historyItem) &&
                    historyItem.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filtered.Add(historyItem);
                }
            }
        }

        if (filtered.Count == 0)
        {
            HideSuggestions();
            return;
        }

        // Skip rebuild/show when the filtered set is unchanged
        if (AreSuggestionSequencesEqual(_lastShownSuggestions, filtered) && IsSuggestionPopupShowing())
        {
            return;
        }

        ShowSuggestions(filtered);
    }

    private IEnumerable<object> EnumerateAllSuggestions()
    {
        for (var i = 0; i < _suggestions.Count; i++)
        {
            yield return _suggestions[i];
        }

        for (var i = 0; i < _richSuggestions.Count; i++)
        {
            yield return _richSuggestions[i];
        }
    }

    private static bool MatchesSuggestion(object item, string searchText)
    {
        if (item is IContentValues contentValues)
        {
            var shortText = contentValues.GetShortText() ?? string.Empty;
            if (shortText.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            var longText = contentValues.GetLongText() ?? string.Empty;
            return longText.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        var text = item.ToString();
        return !string.IsNullOrEmpty(text) &&
               text!.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static bool AreSuggestionSequencesEqual(List<object> left, List<object> right)
    {
        if (left.Count != right.Count)
        {
            return false;
        }

        for (var i = 0; i < left.Count; i++)
        {
            if (!ReferenceEquals(left[i], right[i]) &&
                !(left[i] is string leftString && right[i] is string rightString &&
                  string.Equals(leftString, rightString, StringComparison.Ordinal)))
            {
                return false;
            }
        }

        return true;
    }

    private void ShowSuggestions(List<object> suggestions)
    {
        if (_suggestionPopup == null)
        {
            _suggestionPopup = new SuggestionPopup(this);
            _suggestionPopup.SuggestionSelected += OnSuggestionPopupSelected;
            _popupDisplayType = _searchBoxValues.SuggestionDisplayType;
        }

        var wasShowing = _suggestionPopup.IsShowing;
        _suggestionPopup.SetSearchSuggestions(suggestions);
        _selectedSuggestionIndex = -1;

        _lastShownSuggestions.Clear();
        _lastShownSuggestions.AddRange(suggestions);

        // Position the popup below the search box
        var screenPoint = PointToScreen(new Point(0, Height));
        _suggestionPopup.Show(screenPoint, Width);

        // Restore focus only when the popup was just shown (not every keystroke)
        if (!wasShowing)
        {
            BeginInvoke(new Action(() =>
            {
                if (!Focused && CanFocus)
                {
                    Focus();
                }
            }));
        }
    }

    private void HideSuggestions()
    {
        StopSuggestionUpdateTimer();

        if (_suggestionPopup != null)
        {
            _suggestionPopup.Hide();
            _selectedSuggestionIndex = -1;
        }

        _lastShownSuggestions.Clear();
    }

    private void NavigateSuggestions(int direction)
    {
        if (!IsSuggestionPopupShowing())
        {
            return;
        }

        _isNavigatingSuggestions = true;
        var popup = _suggestionPopup!;
        var count = popup.SuggestionCount;

        if (count == 0)
        {
            return;
        }

        _selectedSuggestionIndex += direction;

        if (_selectedSuggestionIndex < 0)
        {
            _selectedSuggestionIndex = count - 1;
        }
        else if (_selectedSuggestionIndex >= count)
        {
            _selectedSuggestionIndex = 0;
        }

        popup.HighlightIndex(_selectedSuggestionIndex);
    }

    private void SelectSuggestion(int index)
    {
        if (_suggestionPopup == null || index < 0)
        {
            return;
        }

        var suggestionObject = _suggestionPopup.GetSuggestion(index);
        var suggestionText = _suggestionPopup.GetSuggestionText(index);

        if (!string.IsNullOrEmpty(suggestionText))
        {
            _isNavigatingSuggestions = true;
            Text = suggestionText;
            SelectionStart = Text.Length;
            SelectionLength = 0;
            _isNavigatingSuggestions = false;

            HideSuggestions();
            var args = new SuggestionSelectedEventArgs(index)
            {
                Suggestion = suggestionText,
                SuggestionObject = suggestionObject
            };
            OnSuggestionSelected(args);
        }
    }

    private void OnSuggestionPopupSelected(object? sender, SuggestionSelectedEventArgs e)
    {
        if (e.Suggestion != null)
        {
            _isNavigatingSuggestions = true;
            Text = e.Suggestion;
            SelectionStart = Text.Length;
            SelectionLength = 0;
            _isNavigatingSuggestions = false;

            HideSuggestions();
            OnSuggestionSelected(e);
        }
    }

    private Image? GetSearchIcon()
    {
        try
        {
            var icon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.ApplicationCalendar, IconSize.Small);
            return icon?.ToBitmap();
        }
        catch
        {
            // Fallback to a simple search icon or null
            return null;
        }
    }

    private Image? GetClearIcon()
    {
        try
        {
            var icon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.ActionClear, IconSize.Small);
            return icon?.ToBitmap();
        }
        catch
        {
            // Fallback to a simple clear icon or null
            return null;
        }
    }

    #endregion
}