#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion



namespace TestForm;

/// <summary>
/// Comprehensive demo form for KryptonAutoTextSuggestProvider.
/// </summary>
public partial class TextSuggestionDemo : KryptonForm
{
    /// <summary>
    /// Initializes a new instance of the TextSuggestionDemo class.
    /// </summary>
    public TextSuggestionDemo()
    {
        InitializeComponent();
        SetupSuggestions();
        SetupEventHandlers();
    }

    private void SetupSuggestions()
    {
        string[] fruits =
        {
            "Apple", "Apricot", "Avocado",
            "Banana", "Blackberry", "Blueberry",
            "Cherry", "Coconut", "Cranberry",
            "Date",
            "Elderberry",
            "Fig",
            "Grape", "Grapefruit", "Guava",
            "Kiwi",
            "Lemon", "Lime", "Lychee",
            "Mango", "Melon",
            "Orange",
            "Papaya", "Peach", "Pear", "Pineapple", "Plum",
            "Raspberry",
            "Strawberry",
            "Tangerine",
            "Watermelon"
        };

        foreach (string fruit in fruits)
        {
            _suggestionProvider1.Suggestions.Add(new KryptonAutoTextSuggestItem(fruit));
        }

        string[] countries =
        {
            "United States", "United Kingdom", "Canada",
            "Australia", "Austria",
            "Brazil", "Belgium", "Bulgaria",
            "China", "Chile", "Colombia",
            "Denmark",
            "Egypt", "Estonia",
            "France", "Finland",
            "Germany", "Greece",
            "India", "Italy", "Ireland", "Iceland", "Indonesia",
            "Japan",
            "Kenya",
            "Mexico", "Malaysia",
            "Netherlands", "Norway", "New Zealand",
            "Poland", "Portugal",
            "Russia",
            "Spain", "Sweden", "Switzerland", "South Africa", "South Korea",
            "Thailand", "Turkey",
            "Ukraine",
            "Vietnam"
        };

        foreach (string country in countries)
        {
            _suggestionProvider2.Suggestions.Add(new KryptonAutoTextSuggestItem(country));
        }

        var languages = new[]
        {
            new KryptonAutoTextSuggestItem("C#", "C# (.NET)", "Microsoft's object-oriented programming language"),
            new KryptonAutoTextSuggestItem("JavaScript", "JavaScript (JS)", "Dynamic scripting language for web"),
            new KryptonAutoTextSuggestItem("Python", "Python", "High-level interpreted programming language"),
            new KryptonAutoTextSuggestItem("Java", "Java", "Object-oriented programming language"),
            new KryptonAutoTextSuggestItem("C++", "C++", "General-purpose programming language"),
            new KryptonAutoTextSuggestItem("TypeScript", "TypeScript (TS)", "Typed superset of JavaScript"),
            new KryptonAutoTextSuggestItem("Go", "Go (Golang)", "Google's compiled programming language"),
            new KryptonAutoTextSuggestItem("Rust", "Rust", "Systems programming language"),
            new KryptonAutoTextSuggestItem("Swift", "Swift", "Apple's programming language"),
            new KryptonAutoTextSuggestItem("Kotlin", "Kotlin", "JVM-based programming language"),
            new KryptonAutoTextSuggestItem("PHP", "PHP", "Server-side scripting language"),
            new KryptonAutoTextSuggestItem("Ruby", "Ruby", "Dynamic object-oriented language"),
            new KryptonAutoTextSuggestItem("R", "R", "Statistical computing language"),
            new KryptonAutoTextSuggestItem("Scala", "Scala", "Functional programming language"),
            new KryptonAutoTextSuggestItem("Dart", "Dart", "Google's programming language")
        };

        foreach (var lang in languages)
        {
            _suggestionProvider3.Suggestions.Add(lang);
        }
    }

    private void SetupEventHandlers()
    {
        _suggestionProvider1.SuggestionSelected += SuggestionProvider_SuggestionSelected;
        _suggestionProvider2.SuggestionSelected += SuggestionProvider_SuggestionSelected;
        _suggestionProvider3.SuggestionSelected += SuggestionProvider_SuggestionSelected;

        _checkBoxEnabled.CheckedChanged += CheckBoxEnabled_CheckedChanged;
        _checkBoxCaseSensitive.CheckedChanged += CheckBoxCaseSensitive_CheckedChanged;
        _comboBoxMatchMode.SelectedIndexChanged += ComboBoxMatchMode_SelectedIndexChanged;
        _numericUpDownMinChars.ValueChanged += NumericUpDownMinChars_ValueChanged;
        _numericUpDownDelay.ValueChanged += NumericUpDownDelay_ValueChanged;
        _numericUpDownMaxItems.ValueChanged += NumericUpDownMaxItems_ValueChanged;
        _numericUpDownWidth.ValueChanged += NumericUpDownWidth_ValueChanged;
        _buttonClearLog.Click += ButtonClearLog_Click;
    }

    private void SuggestionProvider_SuggestionSelected(object? sender, KryptonAutoTextSuggestEventArgs e)
    {
        string message = $"[{DateTime.Now:HH:mm:ss.fff}] Suggestion selected: '{e.Item.DisplayText}' -> '{e.Item.InsertText}' in {e.Control.Name}";
        _listBoxLog.Items.Insert(0, message);
        if (_listBoxLog.Items.Count > 100)
        {
            _listBoxLog.Items.RemoveAt(_listBoxLog.Items.Count - 1);
        }
    }

    private void CheckBoxEnabled_CheckedChanged(object? sender, EventArgs e)
    {
        bool enabled = _checkBoxEnabled.Checked;
        _suggestionProvider1.Enabled = enabled;
        _suggestionProvider2.Enabled = enabled;
        _suggestionProvider3.Enabled = enabled;
        LogMessage($"Suggestions {(enabled ? "enabled" : "disabled")}");
    }

    private void CheckBoxCaseSensitive_CheckedChanged(object? sender, EventArgs e)
    {
        bool caseSensitive = _checkBoxCaseSensitive.Checked;
        _suggestionProvider1.CaseSensitive = caseSensitive;
        _suggestionProvider2.CaseSensitive = caseSensitive;
        _suggestionProvider3.CaseSensitive = caseSensitive;
        LogMessage($"Case sensitive matching {(caseSensitive ? "enabled" : "disabled")}");
    }

    private void ComboBoxMatchMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var mode = (KryptonAutoTextSuggestMatchMode)_comboBoxMatchMode.SelectedIndex;
        _suggestionProvider1.MatchMode = mode;
        _suggestionProvider2.MatchMode = mode;
        _suggestionProvider3.MatchMode = mode;
        LogMessage($"Match mode changed to: {mode}");
    }

    private void NumericUpDownMinChars_ValueChanged(object? sender, EventArgs e)
    {
        int value = (int)_numericUpDownMinChars.Value;
        _suggestionProvider1.MinCharsToShow = value;
        _suggestionProvider2.MinCharsToShow = value;
        _suggestionProvider3.MinCharsToShow = value;
        LogMessage($"Min characters changed to: {value}");
    }

    private void NumericUpDownDelay_ValueChanged(object? sender, EventArgs e)
    {
        int value = (int)_numericUpDownDelay.Value;
        _suggestionProvider1.ShowDelay = value;
        _suggestionProvider2.ShowDelay = value;
        _suggestionProvider3.ShowDelay = value;
        LogMessage($"Show delay changed to: {value}ms");
    }

    private void NumericUpDownMaxItems_ValueChanged(object? sender, EventArgs e)
    {
        int value = (int)_numericUpDownMaxItems.Value;
        _suggestionProvider1.MaxVisibleItems = value;
        _suggestionProvider2.MaxVisibleItems = value;
        _suggestionProvider3.MaxVisibleItems = value;
        LogMessage($"Max visible items changed to: {value}");
    }

    private void NumericUpDownWidth_ValueChanged(object? sender, EventArgs e)
    {
        int value = (int)_numericUpDownWidth.Value;
        _suggestionProvider1.PopupWidth = value;
        _suggestionProvider2.PopupWidth = value;
        _suggestionProvider3.PopupWidth = value;
        LogMessage($"Popup width changed to: {value}");
    }

    private void ButtonClearLog_Click(object? sender, EventArgs e)
    {
        _listBoxLog.Items.Clear();
    }

    private void LogMessage(string message)
    {
        _listBoxLog.Items.Insert(0, $"[{DateTime.Now:HH:mm:ss.fff}] {message}");
        if (_listBoxLog.Items.Count > 100)
        {
            _listBoxLog.Items.RemoveAt(_listBoxLog.Items.Count - 1);
        }
    }
}
