#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Utilities;

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
        // Suggestions are already set up in InitializeComponent
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
