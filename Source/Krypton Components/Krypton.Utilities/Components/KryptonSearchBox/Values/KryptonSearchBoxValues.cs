#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Contains properties for configuring all aspects of the KryptonSearchBox control.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonSearchBoxValues : Storage
{
    #region Instance Fields
    private bool _showSearchButton;
    private bool _showClearButton;
    private bool _enableSuggestions;
    private int _suggestionMaxCount;
    private SearchSuggestionDisplayType _suggestionDisplayType;
    private int _minimumSearchLength;
    private bool _enableSearchHistory;
    private int _searchHistoryMaxCount;
    private bool _clearOnEscape;
    private string _placeholderText;
    private Func<string, IEnumerable<object>, IEnumerable<object>>? _customFilter;
    private KryptonSearchBox? _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSearchBoxValues class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    public KryptonSearchBoxValues(KryptonSearchBox? owner)
    {
        _owner = owner;
        // Button defaults
        _showSearchButton = true;
        _showClearButton = true;
        // Suggestion defaults
        _enableSuggestions = true;
        _suggestionMaxCount = 10;
        _suggestionDisplayType = SearchSuggestionDisplayType.ListBox;
        _minimumSearchLength = 0;
        // History defaults
        _enableSearchHistory = false;
        _searchHistoryMaxCount = 10;
        _clearOnEscape = true;
        _placeholderText = string.Empty;
        _customFilter = null;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    public override bool IsDefault => ShowSearchButton &&
                                      ShowClearButton &&
                                      EnableSuggestions &&
                                      SuggestionMaxCount == 10 &&
                                      SuggestionDisplayType == SearchSuggestionDisplayType.ListBox &&
                                      MinimumSearchLength == 0 &&
                                      !EnableSearchHistory &&
                                      SearchHistoryMaxCount == 10 &&
                                      ClearOnEscape &&
                                      string.IsNullOrEmpty(PlaceholderText) &&
                                      CustomFilter == null;
    #endregion

    #region Button Properties
    /// <summary>
    /// Gets or sets a value indicating whether the search button is displayed.
    /// </summary>
    [Category(@"Buttons")]
    [Description(@"Indicates whether the search button is displayed.")]
    [DefaultValue(true)]
    public bool ShowSearchButton
    {
        get => _showSearchButton;
        set
        {
            if (_showSearchButton != value)
            {
                _showSearchButton = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the clear button is displayed when text is entered.
    /// </summary>
    [Category(@"Buttons")]
    [Description(@"Indicates whether the clear button is displayed when text is entered.")]
    [DefaultValue(true)]
    public bool ShowClearButton
    {
        get => _showClearButton;
        set
        {
            if (_showClearButton != value)
            {
                _showClearButton = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }
    #endregion

    #region Suggestion Properties
    /// <summary>
    /// Gets or sets a value indicating whether custom suggestions are enabled.
    /// </summary>
    [Category(@"Suggestions")]
    [Description(@"Indicates whether custom suggestions are enabled.")]
    [DefaultValue(true)]
    public bool EnableSuggestions
    {
        get => _enableSuggestions;
        set
        {
            if (_enableSuggestions != value)
            {
                _enableSuggestions = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of suggestions to display.
    /// </summary>
    [Category(@"Suggestions")]
    [Description(@"The maximum number of suggestions to display.")]
    [DefaultValue(10)]
    public int SuggestionMaxCount
    {
        get => _suggestionMaxCount;
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            if (_suggestionMaxCount != value)
            {
                _suggestionMaxCount = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the type of control used to display suggestions.
    /// </summary>
    [Category(@"Suggestions")]
    [Description(@"The type of control used to display suggestions (ListBox or DataGridView).")]
    [DefaultValue(SearchSuggestionDisplayType.ListBox)]
    public SearchSuggestionDisplayType SuggestionDisplayType
    {
        get => _suggestionDisplayType;
        set
        {
            if (_suggestionDisplayType != value)
            {
                _suggestionDisplayType = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the minimum number of characters required before showing suggestions.
    /// </summary>
    [Category(@"Suggestions")]
    [Description(@"The minimum number of characters required before showing suggestions.")]
    [DefaultValue(0)]
    public int MinimumSearchLength
    {
        get => _minimumSearchLength;
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            if (_minimumSearchLength != value)
            {
                _minimumSearchLength = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }
    #endregion

    #region History Properties
    /// <summary>
    /// Gets or sets a value indicating whether search history is enabled.
    /// </summary>
    [Category(@"History")]
    [Description(@"Indicates whether search history is enabled.")]
    [DefaultValue(false)]
    public bool EnableSearchHistory
    {
        get => _enableSearchHistory;
        set
        {
            if (_enableSearchHistory != value)
            {
                _enableSearchHistory = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of search history items to remember.
    /// </summary>
    [Category(@"History")]
    [Description(@"The maximum number of search history items to remember.")]
    [DefaultValue(10)]
    public int SearchHistoryMaxCount
    {
        get => _searchHistoryMaxCount;
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            if (_searchHistoryMaxCount != value)
            {
                _searchHistoryMaxCount = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }
    #endregion

    #region Behavior Properties
    /// <summary>
    /// Gets or sets a value indicating whether pressing Escape clears the text.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether pressing Escape clears the text.")]
    [DefaultValue(true)]
    public bool ClearOnEscape
    {
        get => _clearOnEscape;
        set
        {
            if (_clearOnEscape != value)
            {
                _clearOnEscape = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the placeholder text (watermark) displayed when the text box is empty.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The placeholder text displayed when the text box is empty.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string PlaceholderText
    {
        get => _placeholderText;
        set
        {
            if (_placeholderText != value)
            {
                _placeholderText = value ?? string.Empty;
                _owner?.OnPlaceholderTextChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a custom filter function for suggestions.
    /// If set, this function will be used instead of the default filtering logic.
    /// The function receives the search text and the collection of suggestion objects, and returns the filtered collection.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<string, IEnumerable<object>, IEnumerable<object>>? CustomFilter
    {
        get => _customFilter;
        set => _customFilter = value;
    }
    #endregion

    #region Data Properties
    /// <summary>
    /// Gets the collection of suggestion strings.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of suggestion strings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<string> Suggestions => _owner?._suggestions ?? new List<string>();

    /// <summary>
    /// Gets the collection of search history items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of search history items.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<string> SearchHistory => _owner?._searchHistory.AsReadOnly() ?? Array.Empty<string>().ToList().AsReadOnly();

    /// <summary>
    /// Gets the collection of column definitions for DataGridView suggestion display.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Column definitions for DataGridView suggestion display. Only used when SuggestionDisplayType is DataGridView.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<SearchSuggestionColumnDefinition> DataGridViewColumns => _owner?._dataGridViewColumns ?? new List<SearchSuggestionColumnDefinition>();
    #endregion

    #region Button References
    /// <summary>
    /// Gets access to the search button specification.
    /// </summary>
    [Category(@"Buttons")]
    [Description(@"Access to the search button specification.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny? SearchButton => _owner?._searchButton;

    /// <summary>
    /// Gets access to the clear button specification.
    /// </summary>
    [Category(@"Buttons")]
    [Description(@"Access to the clear button specification.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny? ClearButton => _owner?._clearButton;
    #endregion

    #region Implementation

    internal void SetOwner(KryptonSearchBox owner) => _owner = owner;

    public void Reset()
    {
        ShowSearchButton = true;
        ShowClearButton = true;
        EnableSuggestions = true;
        SuggestionMaxCount = 10;
        SuggestionDisplayType = SearchSuggestionDisplayType.ListBox;
        MinimumSearchLength = 0;
        EnableSearchHistory = false;
        SearchHistoryMaxCount = 10;
        ClearOnEscape = true;
        PlaceholderText = string.Empty;
        CustomFilter = null;
    }

    #endregion
}