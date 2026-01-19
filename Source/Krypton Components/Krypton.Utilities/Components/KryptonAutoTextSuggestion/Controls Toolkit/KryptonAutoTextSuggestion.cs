#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides automatic text suggestion functionality for text controls.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTextBox), "ToolboxBitmaps.KryptonTextBox.bmp")]
[DefaultEvent(nameof(SuggestionSelected))]
[DefaultProperty(nameof(Suggestions))]
[Description("Provides automatic text suggestion functionality for text controls.")]
public class KryptonAutoTextSuggestion : Component
{
    #region Instance Fields

    private Control? _attachedControl;
    private VisualAutoTextSuggestPopup? _popup;
    private readonly System.Windows.Forms.Timer _showTimer;
    private readonly List<KryptonAutoTextSuggestItem> _suggestions = new List<KryptonAutoTextSuggestItem>();
    private string _lastText = string.Empty;
    private int _lastSelectionStart;
    private bool _isApplyingSuggestion;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a suggestion is selected and applied.
    /// </summary>
    [Category("Action")]
    [Description("Occurs when a suggestion is selected and applied.")]
    public event EventHandler<KryptonAutoTextSuggestEventArgs>? SuggestionSelected;

    /// <summary>
    /// Occurs when suggestions are being filtered.
    /// </summary>
    [Category("Action")]
    [Description("Occurs when suggestions are being filtered. Allows custom filtering logic.")]
    public event EventHandler<KryptonAutoTextSuggestFilterEventArgs>? Filtering;
    
    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the KryptonAutoTextSuggestion class.
    /// </summary>
    public KryptonAutoTextSuggestion()
    {
        MinCharsToShow = 1;
        ShowDelay = 300;
        MaxVisibleItems = 8;
        PopupWidth = 250;
        CaseSensitive = false;
        MatchMode = KryptonAutoTextSuggestMatchMode.StartsWith;
        _showTimer = new System.Windows.Forms.Timer();
        _showTimer.Tick += ShowTimer_Tick;
    }

    /// <summary>
    /// Initializes a new instance of the KryptonAutoTextSuggestion class.
    /// </summary>
    /// <param name="container">The container.</param>
    public KryptonAutoTextSuggestion(IContainer container)
        : this()
    {
        container.Add(this);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Detach();
            _showTimer.Dispose();
            _popup?.Dispose();
        }

        base.Dispose(disposing);
    }
    
    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the control to attach suggestions to.
    /// </summary>
    [Category("Behavior")]
    [Description("The control to attach suggestions to.")]
    [DefaultValue(null)]
    public Control? AttachedControl
    {
        get => _attachedControl;
        set
        {
            if (_attachedControl != value)
            {
                Detach();
                _attachedControl = value;
                Attach();
            }
        }
    }

    /// <summary>
    /// Gets the collection of suggestions.
    /// </summary>
    [Category("Data")]
    [Description("The collection of text suggestions.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<KryptonAutoTextSuggestItem> Suggestions => _suggestions;

    /// <summary>
    /// Gets or sets the minimum number of characters before showing suggestions.
    /// </summary>
    [Category("Behavior")]
    [Description("Minimum number of characters before showing suggestions.")]
    [DefaultValue(1)]
    public int MinCharsToShow { get; set; }

    /// <summary>
    /// Gets or sets the delay in milliseconds before showing suggestions.
    /// </summary>
    [Category("Behavior")]
    [Description("Delay in milliseconds before showing suggestions.")]
    [DefaultValue(300)]
    public int ShowDelay { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of visible items in the popup.
    /// </summary>
    [Category("Appearance")]
    [Description("Maximum number of visible items in the popup.")]
    [DefaultValue(8)]
    public int MaxVisibleItems { get; set; }

    /// <summary>
    /// Gets or sets the width of the popup.
    /// </summary>
    [Category("Appearance")]
    [Description("Width of the suggestion popup.")]
    [DefaultValue(250)]
    public int PopupWidth { get; set; }

    /// <summary>
    /// Gets or sets whether matching is case-sensitive.
    /// </summary>
    [Category("Behavior")]
    [Description("Whether matching is case sensitive.")]
    [DefaultValue(false)]
    public bool CaseSensitive { get; set; }

    /// <summary>
    /// Gets or sets the match mode for filtering suggestions.
    /// </summary>
    [Category("Behavior")]
    [Description("The match mode for filtering suggestions.")]
    [DefaultValue(KryptonAutoTextSuggestMatchMode.StartsWith)]
    public KryptonAutoTextSuggestMatchMode MatchMode { get; set; }

    /// <summary>
    /// Gets whether the popup is currently visible.
    /// </summary>
    [Browsable(false)]
    public bool IsPopupVisible => _popup is { IsHandleCreated: true, IsDisposed: false, Visible: true };

    /// <summary>
    /// Gets or sets whether suggestions are enabled.
    /// </summary>
    [Category("Behavior")]
    [Description("Whether suggestions are enabled.")]
    [DefaultValue(true)]
    public bool Enabled { get; set; } = true;

    #endregion

    #region Public Methods

    /// <summary>
    /// Shows the suggestion popup immediately.
    /// </summary>
    public void ShowSuggestions()
    {
        if (!Enabled || _attachedControl == null)
        {
            return;
        }

        UpdateSuggestions();
    }

    /// <summary>
    /// Hides the suggestion popup.
    /// </summary>
    public void HideSuggestions()
    {
        _popup?.Close();
        _popup = null;
    }

    /// <summary>
    /// Applies the specified suggestion to the attached control.
    /// </summary>
    /// <param name="item">The suggestion item to apply.</param>
    internal void ApplySuggestion(KryptonAutoTextSuggestItem item)
    {
        if (_attachedControl == null || _isApplyingSuggestion)
        {
            return;
        }

        _isApplyingSuggestion = true;

        try
        {
            var args = new KryptonAutoTextSuggestEventArgs(item, _attachedControl);
            SuggestionSelected?.Invoke(this, args);

            if (!args.Handled)
            {
                ApplyTextToControl(item.InsertText);
            }

            HideSuggestions();
        }
        finally
        {
            _isApplyingSuggestion = false;
        }
    }
    
    #endregion

    #region Implementation

    private void Attach()
    {
        if (_attachedControl == null)
        {
            return;
        }

        if (_attachedControl is TextBox textBox)
        {
            textBox.TextChanged += Control_TextChanged;
            textBox.KeyDown += Control_KeyDown;
            textBox.LostFocus += Control_LostFocus;
        }
        else if (_attachedControl is KryptonTextBox kryptonTextBox)
        {
            kryptonTextBox.TextChanged += Control_TextChanged;
            kryptonTextBox.KeyDown += Control_KeyDown;
            kryptonTextBox.LostFocus += Control_LostFocus;
        }
        else if (_attachedControl is RichTextBox richTextBox)
        {
            richTextBox.TextChanged += Control_TextChanged;
            richTextBox.KeyDown += Control_KeyDown;
            richTextBox.LostFocus += Control_LostFocus;
        }
    }

    private void Detach()
    {
        if (_attachedControl == null)
        {
            return;
        }

        HideSuggestions();

        if (_attachedControl is TextBox textBox)
        {
            textBox.TextChanged -= Control_TextChanged;
            textBox.KeyDown -= Control_KeyDown;
            textBox.LostFocus -= Control_LostFocus;
        }
        else if (_attachedControl is KryptonTextBox kryptonTextBox)
        {
            kryptonTextBox.TextChanged -= Control_TextChanged;
            kryptonTextBox.KeyDown -= Control_KeyDown;
            kryptonTextBox.LostFocus -= Control_LostFocus;
        }
        else if (_attachedControl is RichTextBox richTextBox)
        {
            richTextBox.TextChanged -= Control_TextChanged;
            richTextBox.KeyDown -= Control_KeyDown;
            richTextBox.LostFocus -= Control_LostFocus;
        }

        _attachedControl = null;
    }

    private void Control_TextChanged(object? sender, EventArgs e)
    {
        if (_isApplyingSuggestion || !Enabled)
        {
            return;
        }

        string currentText = GetControlText();
        int currentSelection = GetControlSelectionStart();

        // Only update if text or selection actually changed
        if (currentText != _lastText || currentSelection != _lastSelectionStart)
        {
            _lastText = currentText;
            _lastSelectionStart = currentSelection;

            // Reset and start timer
            _showTimer.Stop();
            _showTimer.Interval = ShowDelay;
            _showTimer.Start();
        }
    }

    private void Control_KeyDown(object? sender, KeyEventArgs e)
    {
        if (!IsPopupVisible)
        {
            return;
        }

        switch (e.KeyCode)
        {
            case Keys.Down:
                _popup?.SelectNext();
                e.Handled = true;
                break;
            case Keys.Up:
                _popup?.SelectPrevious();
                e.Handled = true;
                break;
            case Keys.Enter:
                var selected = _popup?.GetSelectedItem();
                if (selected != null)
                {
                    ApplySuggestion(selected);
                    e.Handled = true;
                }
                break;
            case Keys.Escape:
                HideSuggestions();
                e.Handled = true;
                break;
        }
    }

    private void Control_LostFocus(object? sender, EventArgs e)
    {
        // Don't hide if focus moved to popup
        if (_popup != null && _popup.ContainsFocus)
        {
            return;
        }

        HideSuggestions();
    }

    private void ShowTimer_Tick(object? sender, EventArgs e)
    {
        _showTimer.Stop();
        UpdateSuggestions();
    }

    private void UpdateSuggestions()
    {
        if (_attachedControl == null || !Enabled)
        {
            return;
        }

        string currentText = GetControlText();
        int selectionStart = GetControlSelectionStart();

        // Get word at cursor position
        string wordAtCursor = GetWordAtPosition(currentText, selectionStart);

        if (wordAtCursor.Length < MinCharsToShow)
        {
            HideSuggestions();
            return;
        }

        // Filter suggestions
        var filtered = FilterSuggestions(wordAtCursor);

        if (filtered.Count == 0)
        {
            HideSuggestions();
            return;
        }

        // Show popup
        ShowPopup(filtered);
    }

    private string GetWordAtPosition(string text, int position)
    {
        if (string.IsNullOrEmpty(text) || position < 0 || position > text.Length)
        {
            return string.Empty;
        }

        // Find word boundaries
        int start = position;
        while (start > 0 && IsWordChar(text[start - 1]))
        {
            start--;
        }

        int end = position;
        while (end < text.Length && IsWordChar(text[end]))
        {
            end++;
        }

        return text.Substring(start, end - start);
    }

    private bool IsWordChar(char c)
    {
        return char.IsLetterOrDigit(c) || c == '_' || c == '.';
    }

    private List<KryptonAutoTextSuggestItem> FilterSuggestions(string filterText)
    {
        var results = new List<KryptonAutoTextSuggestItem>();

        string compareText = CaseSensitive ? filterText : filterText.ToLowerInvariant();

        foreach (var item in _suggestions)
        {
            string itemText = CaseSensitive ? item.DisplayText : item.DisplayText.ToLowerInvariant();

            bool matches = MatchMode switch
            {
                KryptonAutoTextSuggestMatchMode.StartsWith => itemText.StartsWith(compareText),
                KryptonAutoTextSuggestMatchMode.Contains => itemText.Contains(compareText),
                KryptonAutoTextSuggestMatchMode.Fuzzy => FuzzyMatch(itemText, compareText),
                _ => itemText.StartsWith(compareText)
            };

            if (matches)
            {
                results.Add(item);
            }
        }

        // Allow custom filtering
        var args = new KryptonAutoTextSuggestFilterEventArgs(filterText, results);
        Filtering?.Invoke(this, args);

        return args.Suggestions;
    }

    private bool FuzzyMatch(string text, string pattern)
    {
        // Simple fuzzy match: pattern characters appear in order in text
        int patternIndex = 0;
        foreach (char c in text)
        {
            if (patternIndex < pattern.Length && c == pattern[patternIndex])
            {
                patternIndex++;
            }
        }

        return patternIndex == pattern.Length;
    }

    private void ShowPopup(List<KryptonAutoTextSuggestItem> suggestions)
    {
        if (_attachedControl is not { IsHandleCreated: true })
        {
            return;
        }

        if (_popup == null || _popup.IsDisposed)
        {
            var palette = KryptonManager.CurrentGlobalPalette;
            var renderer = palette.GetRenderer();
            _popup = new VisualAutoTextSuggestPopup(this, renderer);
        }

        _popup.UpdateSuggestions(suggestions);
        _popup.ShowPopup(_attachedControl);
    }

    private string GetControlText()
    {
        return _attachedControl switch
        {
            TextBox tb => tb.Text,
            KryptonTextBox ktb => ktb.Text,
            RichTextBox rtb => rtb.Text,
            _ => string.Empty
        };
    }

    private int GetControlSelectionStart()
    {
        return _attachedControl switch
        {
            TextBox tb => tb.SelectionStart,
            KryptonTextBox ktb => ktb.SelectionStart,
            RichTextBox rtb => rtb.SelectionStart,
            _ => 0
        };
    }

    private void ApplyTextToControl(string text)
    {
        if (_attachedControl == null)
        {
            return;
        }

        string currentText = GetControlText();
        int selectionStart = GetControlSelectionStart();
        string wordAtCursor = GetWordAtPosition(currentText, selectionStart);

        if (string.IsNullOrEmpty(wordAtCursor))
        {
            return;
        }

        // Find the word start position
        int wordStart = selectionStart;
        while (wordStart > 0 && IsWordChar(currentText[wordStart - 1]))
        {
            wordStart--;
        }

        // Replace the word with the suggestion
        string newText = currentText.Substring(0, wordStart) + text + currentText.Substring(selectionStart);
        int newSelectionStart = wordStart + text.Length;

        if (_attachedControl is TextBox tb)
        {
            tb.Text = newText;
            tb.SelectionStart = newSelectionStart;
            tb.SelectionLength = 0;
        }
        else if (_attachedControl is KryptonTextBox ktb)
        {
            ktb.Text = newText;
            ktb.SelectionStart = newSelectionStart;
            ktb.SelectionLength = 0;
        }
        else if (_attachedControl is RichTextBox rtb)
        {
            rtb.Text = newText;
            rtb.SelectionStart = newSelectionStart;
            rtb.SelectionLength = 0;
        }
    }
    
    #endregion
}
