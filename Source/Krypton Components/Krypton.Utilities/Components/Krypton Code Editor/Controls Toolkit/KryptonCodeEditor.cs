#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Utilities;

/// <summary>
/// Provides a native code editor control with syntax highlighting, line numbering, and advanced editing features.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRichTextBox), "ToolboxBitmaps.KryptonRichTextBox.bmp")]
[DefaultEvent(nameof(TextChanged))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[Designer(typeof(KryptonCodeEditorDesigner))]
[DesignerCategory(@"code")]
[Description(@"Provides a native code editor with syntax highlighting, line numbering, code folding, and Krypton theming.")]
public class KryptonCodeEditor : VisualPanel,
    IContainedInputControl
{
    #region Instance Fields

    private readonly KryptonRichTextBox _richTextBox;
    private readonly LineNumberMargin _lineNumberMargin;
    private readonly Panel _marginPanel;
    private readonly FoldingMargin _foldingMargin;
    private bool _showLineNumbers = true;
    internal bool _enableCodeFolding = false;
    private Language _language = Language.None;
    private Font _editorFont;
    private Dictionary<string, Color> _keywordColors;
    internal List<FoldBlock> _foldBlocks;
    private bool _isApplyingSyntaxHighlighting;
    private readonly Timer _highlightTimer;
    private int _lineNumberMarginWidth = 50;
    private int _lastHighlightedLine = -1;
    private readonly int _foldingMarginWidth = 20;
    private VisualAutoCompleteForm? _autoCompleteForm;
    private List<string> _autoCompleteKeywords;
    private bool _autoCompleteEnabled = true;
    private EditorTheme _theme;
    private EditorThemeType _themeType = EditorThemeType.Light;

    #endregion

    #region Instance Fields - State Properties

    // Line number margin state properties (using Triple for Back + Content)
    private readonly PaletteTripleRedirect _lineNumberMarginStateCommon;
    private readonly PaletteTriple _lineNumberMarginStateDisabled;
    private readonly PaletteTriple _lineNumberMarginStateNormal;

    // Folding margin state properties (using Triple for Back + Content)
    private readonly PaletteTripleRedirect _foldingMarginStateCommon;
    private readonly PaletteTriple _foldingMarginStateDisabled;
    private readonly PaletteTriple _foldingMarginStateNormal;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonCodeEditor class.
    /// </summary>
    public KryptonCodeEditor()
    {
        // Default properties
        _editorFont = new Font("Consolas", 10F);
        _foldBlocks = new List<FoldBlock>();

        // Create line number margin palette storage (Back + Content for text color)
        _lineNumberMarginStateCommon = new PaletteTripleRedirect(Redirector!, PaletteBackStyle.PanelAlternate, PaletteBorderStyle.ControlClient, PaletteContentStyle.LabelNormalPanel, NeedPaintDelegate);
        _lineNumberMarginStateDisabled = new PaletteTriple(_lineNumberMarginStateCommon, NeedPaintDelegate);
        _lineNumberMarginStateNormal = new PaletteTriple(_lineNumberMarginStateCommon, NeedPaintDelegate);

        // Create folding margin palette storage (Back + Content for indicator colors)
        _foldingMarginStateCommon = new PaletteTripleRedirect(Redirector!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, PaletteContentStyle.LabelNormalPanel, NeedPaintDelegate);
        _foldingMarginStateDisabled = new PaletteTriple(_foldingMarginStateCommon, NeedPaintDelegate);
        _foldingMarginStateNormal = new PaletteTriple(_foldingMarginStateCommon, NeedPaintDelegate);

        // Initialize theme
        _theme = new EditorTheme(_themeType);

        // Initialize keyword colors (legacy support)
        InitializeKeywordColors();

        // Create folding margin panel
        _foldingMargin = new FoldingMargin(this)
        {
            Dock = DockStyle.Left,
            Width = _foldingMarginWidth,
            BackColor = SystemColors.Control,
            Visible = false
        };

        // Create margin panel for line numbers
        _marginPanel = new Panel
        {
            Dock = DockStyle.Left,
            Width = _lineNumberMarginWidth,
            BackColor = SystemColors.Control
        };

        // Create line number margin
        _lineNumberMargin = new LineNumberMargin(this)
        {
            Dock = DockStyle.Fill
        };
        _marginPanel.Controls.Add(_lineNumberMargin);

        // Initialize auto-complete keywords
        InitializeAutoCompleteKeywords();

        // Create rich text box
        _richTextBox = new KryptonRichTextBox
        {
            Dock = DockStyle.Fill,
            Font = _editorFont,
            WordWrap = false,
            AcceptsTab = true,
            DetectUrls = false
        };

        // Wire up events
        _richTextBox.TextChanged += OnRichTextBoxTextChanged;
        _richTextBox.VScroll += OnRichTextBoxVScroll;
        _richTextBox.SelectionChanged += OnRichTextBoxSelectionChanged;
        _richTextBox.KeyDown += OnRichTextBoxKeyDown;
        _richTextBox.KeyPress += OnRichTextBoxKeyPress;

        // Add controls (order matters - right to left)
        Controls.Add(_richTextBox);
        Controls.Add(_marginPanel);
        Controls.Add(_foldingMargin);

        // Set up syntax highlighting timer (debounce)
        _highlightTimer = new Timer { Interval = 300 };
        _highlightTimer.Tick += (s, e) =>
        {
            _highlightTimer.Stop();
            ApplySyntaxHighlighting();
        };

        // Set default size
        Size = new Size(300, 200);

        // Initialize background color from palette
        var palette = GetResolvedPalette();
        if (palette != null)
        {
            base.BackColor = palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
        }

        // Update line number margin visibility
        UpdateLineNumberMargin();
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets and sets the code text content.
    /// </summary>
    [Bindable(true)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue("")]
    [Localizable(true)]
    public override string Text
    {
        get => _richTextBox.Text;
        set
        {
            _richTextBox.Text = value;
            ApplySyntaxHighlighting();
            UpdateLineNumberMargin();
        }
    }

    /// <summary>
    /// Gets and sets whether line numbers are displayed.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description(@"Indicates whether line numbers are displayed in the margin.")]
    public bool ShowLineNumbers
    {
        get => _showLineNumbers;
        set
        {
            if (_showLineNumbers != value)
            {
                _showLineNumbers = value;
                UpdateLineNumberMargin();
            }
        }
    }

    /// <summary>
    /// Gets and sets the width of the line number margin.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(50)]
    [Description(@"The width of the line number margin in pixels.")]
    public int LineNumberMarginWidth
    {
        get => _lineNumberMarginWidth;
        set
        {
            if (_lineNumberMarginWidth != value)
            {
                _lineNumberMarginWidth = Math.Max(30, value);
                if (_showLineNumbers)
                {
                    _marginPanel.Width = _lineNumberMarginWidth;
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the programming language for syntax highlighting.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(Language.None)]
    [Description(@"The programming language for syntax highlighting.")]
    public Language Language
    {
        get => _language;
        set
        {
            if (_language != value)
            {
                _language = value;
                UpdateAutoCompleteKeywords();
                ApplySyntaxHighlighting();
            }
        }
    }

    /// <summary>
    /// Gets and sets whether code folding is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Indicates whether code folding is enabled.")]
    public bool EnableCodeFolding
    {
        get => _enableCodeFolding;
        set
        {
            if (_enableCodeFolding != value)
            {
                _enableCodeFolding = value;
                _foldingMargin.Visible = value;
                if (value)
                {
                    UpdateFoldBlocks();
                    _foldingMargin.Invalidate();
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets whether auto-completion is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Indicates whether auto-completion is enabled.")]
    public bool AutoCompleteEnabled
    {
        get => _autoCompleteEnabled;
        set => _autoCompleteEnabled = value;
    }

    /// <summary>
    /// Gets and sets the editor theme for syntax highlighting.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(EditorThemeType.Light)]
    [Description(@"The theme used for syntax highlighting.")]
    public EditorThemeType Theme
    {
        get => _themeType;
        set
        {
            if (_themeType != value)
            {
                _themeType = value;
                _theme.ApplyPredefinedTheme(value);
                ApplySyntaxHighlighting();
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets the current editor theme instance for custom color customization.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public EditorTheme ThemeInstance => _theme;

    /// <summary>
    /// Gets and sets the font used for editing.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Font), "Consolas, 10pt")]
    [Description(@"The font used for editing code.")]
    public Font EditorFont
    {
        get => _editorFont;
        set
        {
            if (_editorFont != value)
            {
                _editorFont?.Dispose();
                _editorFont = value;
                if (_richTextBox != null)
                {
                    _richTextBox.Font = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets the underlying KryptonRichTextBox control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRichTextBox RichTextBox => _richTextBox;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control ContainedControl => RichTextBox;

    /// <summary>
    /// Gets access to the common RichTextBox appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common RichTextBox appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleRedirect RichTextBoxStateCommon => _richTextBox.StateCommon;

    private bool ShouldSerializeRichTextBoxStateCommon() => !_richTextBox.StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled RichTextBox appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled RichTextBox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates RichTextBoxStateDisabled => _richTextBox.StateDisabled;

    private bool ShouldSerializeRichTextBoxStateDisabled() => !_richTextBox.StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal RichTextBox appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal RichTextBox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates RichTextBoxStateNormal => _richTextBox.StateNormal;

    private bool ShouldSerializeRichTextBoxStateNormal() => !_richTextBox.StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the active RichTextBox appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active RichTextBox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates RichTextBoxStateActive => _richTextBox.StateActive;

    private bool ShouldSerializeRichTextBoxStateActive() => !_richTextBox.StateActive.IsDefault;

    /// <summary>
    /// Gets or sets the selected text.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedText
    {
        get => _richTextBox.SelectedText;
        set => _richTextBox.SelectedText = value;
    }

    /// <summary>
    /// Gets or sets the starting point of text selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => _richTextBox.SelectionStart;
        set => _richTextBox.SelectionStart = value;
    }

    /// <summary>
    /// Gets or sets the number of characters selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => _richTextBox.SelectionLength;
        set => _richTextBox.SelectionLength = value;
    }

    /// <summary>
    /// Gets or sets the background color of the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get
        {
            var palette = GetResolvedPalette();
            return palette?.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal) ?? base.BackColor;
        }
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets access to the common line number margin appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common line number margin appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect LineNumberMarginStateCommon => _lineNumberMarginStateCommon;

    private bool ShouldSerializeLineNumberMarginStateCommon() => !_lineNumberMarginStateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled line number margin appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled line number margin appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple LineNumberMarginStateDisabled => _lineNumberMarginStateDisabled;

    private bool ShouldSerializeLineNumberMarginStateDisabled() => !_lineNumberMarginStateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal line number margin appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal line number margin appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple LineNumberMarginStateNormal => _lineNumberMarginStateNormal;

    private bool ShouldSerializeLineNumberMarginStateNormal() => !_lineNumberMarginStateNormal.IsDefault;

    /// <summary>
    /// Gets access to the common folding margin appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common folding margin appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect FoldingMarginStateCommon => _foldingMarginStateCommon;

    private bool ShouldSerializeFoldingMarginStateCommon() => !_foldingMarginStateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled folding margin appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled folding margin appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple FoldingMarginStateDisabled => _foldingMarginStateDisabled;

    private bool ShouldSerializeFoldingMarginStateDisabled() => !_foldingMarginStateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal folding margin appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal folding margin appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple FoldingMarginStateNormal => _foldingMarginStateNormal;

    private bool ShouldSerializeFoldingMarginStateNormal() => !_foldingMarginStateNormal.IsDefault;

    #endregion

    #region Protected

    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnTextChanged(EventArgs e)
    {
        TextChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        base.OnPaletteChanged(e);

        // Update the background color from palette
        var palette = GetResolvedPalette();
        if (palette != null)
        {
            base.BackColor = palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
        }

        // Invalidate line number margin and folding margin to refresh their colors
        _lineNumberMargin?.Invalidate();
        _foldingMargin?.Invalidate();

        Invalidate();
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);

        // Invalidate line number margin and folding margin to refresh their colors based on enabled state
        _lineNumberMargin?.Invalidate();
        _foldingMargin?.Invalidate();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _highlightTimer?.Stop();
            _highlightTimer?.Dispose();
            _editorFont?.Dispose();
            _lineNumberMargin?.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion

    #region Private Methods

    internal void GetLineNumberPaletteColors(out Color backColor, out Color textColor)
    {
        var state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        var stateObject = Enabled ? _lineNumberMarginStateNormal : _lineNumberMarginStateDisabled;

        backColor = stateObject.Back.GetBackColor1(state);
        textColor = stateObject.Content.GetContentShortTextColor1(state);
    }

    internal void GetFoldingMarginPaletteColors(out Color backColor, out Color indicatorFillColor, out Color indicatorBorderColor, out Color indicatorTextColor)
    {
        var state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        var stateObject = Enabled ? _foldingMarginStateNormal : _foldingMarginStateDisabled;
        var palette = GetResolvedPalette();

        backColor = stateObject.Back.GetBackColor1(state);
        var textColor = stateObject.Content.GetContentShortTextColor1(state);

        // For indicator fill, use ControlClient style for a button-like appearance
        // For border/text, use the text color from content
        // For the "..." indicator, use a semi-transparent version of text color
        indicatorFillColor = palette?.GetBackColor1(PaletteBackStyle.ControlClient, state) ?? SystemColors.ControlDark;
        indicatorBorderColor = textColor;
        indicatorTextColor = Color.FromArgb(150, textColor);
    }

    #region Win32 Redraw Suppression

    private static class Win32
    {
        internal const uint WM_SETREDRAW = 0x000B;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    }

    private static void BeginRedraw(Control control)
    {
        if (control != null && control.IsHandleCreated)
        {
            Win32.SendMessage(control.Handle, Win32.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }
    }

    private static void EndRedraw(Control control)
    {
        if (control != null && control.IsHandleCreated)
        {
            Win32.SendMessage(control.Handle, Win32.WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            control.Invalidate();
        }
    }

    #endregion

    private void InitializeKeywordColors()
    {
        var palette = GetResolvedPalette();

        _keywordColors = new Dictionary<string, Color>
        {
            ["keyword"] = palette?.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.LinkNotVisitedOverride) ?? Color.Blue,
            ["string"] = Color.FromArgb(0, 128, 0), // Green
            ["comment"] = Color.FromArgb(128, 128, 128), // Gray
            ["number"] = Color.FromArgb(0, 0, 255), // Blue
            ["operator"] = Color.Black,
            ["preprocessor"] = Color.FromArgb(128, 64, 0) // Brown
        };
    }

    private void UpdateLineNumberMargin()
    {
        if (_showLineNumbers)
        {
            _marginPanel.Width = _lineNumberMarginWidth;
            _marginPanel.Visible = true;
            _lineNumberMargin.Invalidate();
        }
        else
        {
            _marginPanel.Visible = false;
        }
    }

    private void OnRichTextBoxTextChanged(object? sender, EventArgs e)
    {
        // Debounce syntax highlighting
        _highlightTimer.Stop();
        _highlightTimer.Start();

        UpdateLineNumberMargin();

        if (_enableCodeFolding)
        {
            UpdateFoldBlocks();
            _foldingMargin.Invalidate();
        }

        OnTextChanged(e);
    }

    private void OnRichTextBoxVScroll(object? sender, EventArgs e)
    {
        _lineNumberMargin.Invalidate();
        if (_enableCodeFolding)
        {
            _foldingMargin.Invalidate();
        }
    }

    private void OnRichTextBoxSelectionChanged(object? sender, EventArgs e)
    {
        // Highlight matching braces
        HighlightMatchingBraces();
    }

    private void OnRichTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        // Handle Tab key for indentation
        if (e.KeyCode == Keys.Tab && !e.Control && !e.Alt)
        {
            if (_richTextBox.SelectionLength > 0)
            {
                // Indent/unindent selected lines
                if (e.Shift)
                {
                    UnindentSelection();
                }
                else
                {
                    IndentSelection();
                }
                e.Handled = true;
            }
        }
        // Handle Enter for auto-indentation
        else if (e.KeyCode == Keys.Enter)
        {
            AutoIndent();
        }
    }

    private void ApplySyntaxHighlighting()
    {
        if (_isApplyingSyntaxHighlighting || _language == Language.None)
        {
            return;
        }

        _isApplyingSyntaxHighlighting = true;

        try
        {
            var text = _richTextBox.Text;
            if (string.IsNullOrEmpty(text))
            {
                _isApplyingSyntaxHighlighting = false;
                return;
            }

            // Save selection
            var selectionStart = _richTextBox.SelectionStart;
            var selectionLength = _richTextBox.SelectionLength;

            // Get tokens
            var tokens = Tokenize(text);

            // Apply formatting (suppress redraw to avoid flashing)
            BeginRedraw(_richTextBox);
            try
            {
                _richTextBox.SelectAll();
                _richTextBox.SelectionColor = GetColorForTokenType(TokenType.Normal);
                _richTextBox.SelectionFont = _editorFont;

                foreach (var token in tokens)
                {
                    if (token.Type != TokenType.Normal)
                    {
                        _richTextBox.Select(token.StartIndex, token.Length);
                        _richTextBox.SelectionColor = GetColorForTokenType(token.Type);
                        _richTextBox.SelectionFont = GetFontForTokenType(token.Type);
                    }
                }
            }
            finally
            {
                EndRedraw(_richTextBox);
            }

            // Restore selection
            _richTextBox.SelectionStart = selectionStart;
            _richTextBox.SelectionLength = selectionLength;
        }
        finally
        {
            _isApplyingSyntaxHighlighting = false;
        }
    }

    private List<CodeToken> Tokenize(string text)
    {
        var tokens = new List<CodeToken>();
        var patterns = GetTokenPatterns();

        if (patterns.Count == 0)
        {
            tokens.Add(new CodeToken(TokenType.Normal, 0, text.Length));
            return tokens;
        }

        // Create a combined regex pattern with named groups
        var patternBuilder = new StringBuilder();
        patternBuilder.Append("(?<");

        var groupNames = new List<string>();
        for (int i = 0; i < patterns.Count; i++)
        {
            var groupName = $"type{i}";
            groupNames.Add(groupName);
            if (i > 0)
            {
                patternBuilder.Append("|");
            }

            patternBuilder.Append($"{groupName}>{patterns[i].Pattern})");
        }

        var combinedPattern = patternBuilder.ToString();

        // Tokenize using a state machine approach
        int currentIndex = 0;
        var processedRanges = new List<(int Start, int End, TokenType Type)>();

        // Find all matches for each pattern type
        for (int i = 0; i < patterns.Count; i++)
        {
            var pattern = patterns[i];
            var matches = Regex.Matches(text, pattern.Pattern, RegexOptions.Multiline | RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                // Check if this range overlaps with already processed ranges
                bool overlaps = processedRanges.Any(r =>
                    (match.Index >= r.Start && match.Index < r.End) ||
                    (match.Index + match.Length > r.Start && match.Index + match.Length <= r.End) ||
                    (match.Index <= r.Start && match.Index + match.Length >= r.End));

                if (!overlaps)
                {
                    processedRanges.Add((match.Index, match.Index + match.Length, pattern.Type));
                }
            }
        }

        // Sort ranges by start position
        processedRanges = processedRanges.OrderBy(r => r.Start).ToList();

        // Remove overlapping ranges (keep first/longest)
        var finalRanges = new List<(int Start, int End, TokenType Type)>();
        foreach (var range in processedRanges)
        {
            bool shouldAdd = true;
            for (int i = finalRanges.Count - 1; i >= 0; i--)
            {
                var existing = finalRanges[i];
                if (range.Start >= existing.Start && range.End <= existing.End)
                {
                    // Range is completely inside existing - skip
                    shouldAdd = false;
                    break;
                }
                if (range.Start < existing.End && range.End > existing.Start)
                {
                    // Overlaps - remove shorter one
                    if (range.End - range.Start > existing.End - existing.Start)
                    {
                        finalRanges.RemoveAt(i);
                    }
                    else
                    {
                        shouldAdd = false;
                        break;
                    }
                }
            }
            if (shouldAdd)
            {
                finalRanges.Add(range);
            }
        }

        // Sort again after cleanup
        finalRanges = finalRanges.OrderBy(r => r.Start).ToList();

        // Build token list
        int lastIndex = 0;
        foreach (var range in finalRanges)
        {
            // Add normal text before this token
            if (range.Start > lastIndex)
            {
                tokens.Add(new CodeToken(TokenType.Normal, lastIndex, range.Start - lastIndex));
            }

            // Add the token - index-based, no string duplication
            tokens.Add(new CodeToken(range.Type, range.Start, range.End - range.Start));

            lastIndex = range.End;
        }

        // Add remaining text
        if (lastIndex < text.Length)
        {
            tokens.Add(new CodeToken(TokenType.Normal, lastIndex, text.Length - lastIndex));
        }

        return tokens;
    }

    private List<(string Pattern, TokenType Type)> GetTokenPatterns()
    {
        return _language switch
        {
            Language.CSharp => GetCSharpPatterns(),
            Language.Cpp => GetCppPatterns(),
            Language.VbNet => GetVbNetPatterns(),
            Language.Xml => GetXmlPatterns(),
            Language.Html => GetHtmlPatterns(),
            Language.Css => GetCssPatterns(),
            Language.JavaScript => GetJavaScriptPatterns(),
            Language.Python => GetPythonPatterns(),
            Language.Sql => GetSqlPatterns(),
            Language.Json => GetJsonPatterns(),
            Language.Markdown => GetMarkdownPatterns(),
            Language.Rust => GetRustPatterns(),
            Language.Go => GetGoPatterns(),
            Language.Java => GetJavaPatterns(),
            Language.TypeScript => GetTypeScriptPatterns(),
            Language.Php => GetPhpPatterns(),
            Language.Ruby => GetRubyPatterns(),
            Language.Swift => GetSwiftPatterns(),
            Language.Kotlin => GetKotlinPatterns(),
            Language.Yaml => GetYamlPatterns(),
            Language.Toml => GetTomlPatterns(),
            Language.Batch => GetBatchPatterns(),
            Language.PowerShell => GetPowerShellPatterns(),
            _ => new List<(string, TokenType)>()
        };
    }

    private List<(string Pattern, TokenType Type)> GetCSharpPatterns()
    {
        var keywords = @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|async|await|get|set|value|yield|var|let|from|select|where|orderby|group|by|into|join|on|equals|ascending|descending)\b";
        var types = @"\b(bool|byte|char|decimal|double|float|int|long|object|sbyte|short|string|uint|ulong|ushort|void|dynamic|var)\b";
        var classes = @"\b(class|struct|interface|enum|namespace)\s+(\w+)";
        var functions = @"\b(\w+)\s*\([^)]*\)\s*\{";
        var strings = @"""[^""]*""|'[^']*'|@""[^""]*""";
        var verbatimStrings = @"@""(?:[^""]|"""")*""";
        var comments = @"//.*?$|/\*[\s\S]*?\*/";
        var docComments = @"///.*?$";
        var numbers = @"\b\d+\.?\d*[fFdDmMlL]?\b|0x[0-9a-fA-F]+";
        var preprocessor = @"#(if|else|elif|endif|define|undef|warning|error|line|region|endregion|pragma)\b";
        var attributes = @"\[[\w\s,=()]+\]";
        var constants = @"\b(true|false|null)\b";
        var operators = @"(==|!=|<=|>=|&&|\|\||\+\+|--|=>|::|->)";

        return new List<(string, TokenType)>
        {
            (docComments, TokenType.Comment),
            (comments, TokenType.Comment),
            (verbatimStrings, TokenType.String),
            (strings, TokenType.String),
            (preprocessor, TokenType.Preprocessor),
            (attributes, TokenType.Attribute),
            (constants, TokenType.Constant),
            (types, TokenType.Type),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number),
            (operators, TokenType.Operator)
        };
    }

    private List<(string Pattern, TokenType Type)> GetCppPatterns()
    {
        var keywords = @"\b(alignas|alignof|and|and_eq|asm|auto|bitand|bitor|bool|break|case|catch|char|char16_t|char32_t|class|compl|const|constexpr|const_cast|continue|decltype|default|delete|do|double|dynamic_cast|else|enum|explicit|export|extern|false|float|for|friend|goto|if|inline|int|long|mutable|namespace|new|noexcept|not|not_eq|nullptr|operator|or|or_eq|private|protected|public|register|reinterpret_cast|return|short|signed|sizeof|static|static_assert|static_cast|struct|switch|template|this|thread_local|throw|true|try|typedef|typeid|typename|union|unsigned|using|virtual|void|volatile|wchar_t|while|xor|xor_eq)\b";
        var strings = @"""[^""]*""|'[^']*'";
        var comments = @"//.*?$|/\*.*?\*/";
        var numbers = @"\b\d+\.?\d*\b";
        var preprocessor = @"#\w+";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (preprocessor, TokenType.Preprocessor),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetVbNetPatterns()
    {
        var keywords = @"\b(AddHandler|AddressOf|Alias|And|AndAlso|As|Boolean|ByRef|Byte|ByVal|Call|Case|Catch|CBool|CByte|CChar|CDate|CDbl|CDec|Char|CInt|Class|CLng|CObj|Const|Continue|CSByte|CShort|CSng|CStr|CType|CUInt|CULng|CUShort|Date|Decimal|Declare|Default|Delegate|Dim|DirectCast|Do|Double|Each|Else|ElseIf|End|EndIf|Enum|Erase|Error|Event|Exit|False|Finally|For|Friend|Function|Get|GetType|GetXMLNamespace|Global|GoSub|GoTo|Handles|If|Implements|Imports|In|Inherits|Integer|Interface|Is|IsNot|Let|Lib|Like|Long|Loop|Me|Mod|Module|MustInherit|MustOverride|MyBase|MyClass|Namespace|Narrowing|New|Next|Not|Nothing|NotInheritable|NotOverridable|Object|Of|On|Operator|Option|Optional|Or|OrElse|Overloads|Overridable|Overrides|ParamArray|Partial|Private|Property|Protected|Public|RaiseEvent|ReadOnly|ReDim|REM|RemoveHandler|Resume|Return|SByte|Select|Set|Shadows|Shared|Short|Single|Static|Step|Stop|String|Structure|Sub|SyncLock|Then|Throw|To|True|Try|TryCast|TypeOf|UInteger|ULong|UShort|Using|Variant|Wend|When|While|Widening|With|WithEvents|WriteOnly|Xor)\b";
        var strings = @"""[^""]*""";
        var comments = @"'.*?$|REM\s+.*?$";
        var numbers = @"\b\d+\.?\d*\b";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetXmlPatterns()
    {
        var tags = @"</?\w+[^>]*>";
        var attributes = @"\w+\s*=";
        var strings = @"""[^""]*""|'[^']*'";
        var comments = @"<!--.*?-->";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (tags, TokenType.Keyword),
            (attributes, TokenType.Identifier)
        };
    }

    private List<(string Pattern, TokenType Type)> GetHtmlPatterns()
    {
        var tags = @"</?\w+[^>]*>";
        var attributes = @"\w+\s*=";
        var strings = @"""[^""]*""|'[^']*'";
        var comments = @"<!--.*?-->";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (tags, TokenType.Keyword),
            (attributes, TokenType.Identifier)
        };
    }

    private List<(string Pattern, TokenType Type)> GetCssPatterns()
    {
        var selectors = @"[.#]?\w+\s*\{";
        var properties = @"\w+\s*:";
        var values = @":\s*[^;]+";
        var comments = @"/\*.*?\*/";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (selectors, TokenType.Keyword),
            (properties, TokenType.Identifier),
            (values, TokenType.String)
        };
    }

    private List<(string Pattern, TokenType Type)> GetJavaScriptPatterns()
    {
        var keywords = @"\b(break|case|catch|class|const|continue|debugger|default|delete|do|else|export|extends|finally|for|function|if|import|in|instanceof|new|return|super|switch|this|throw|try|typeof|var|let|void|while|with|yield)\b";
        var strings = @"""[^""]*""|'[^']*'|`[^`]*`";
        var comments = @"//.*?$|/\*.*?\*/";
        var numbers = @"\b\d+\.?\d*\b";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetPythonPatterns()
    {
        var keywords = @"\b(and|as|assert|break|class|continue|def|del|elif|else|except|exec|finally|for|from|global|if|import|in|is|lambda|not|or|pass|print|raise|return|try|while|with|yield)\b";
        var strings = "\"\"\"[^\"]*\"\"\"|\"[^\"]*\"|'[^']*'";
        var comments = @"#.*?$";
        var numbers = @"\b\d+\.?\d*\b";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetSqlPatterns()
    {
        var keywords = @"\b(SELECT|FROM|WHERE|INSERT|UPDATE|DELETE|CREATE|ALTER|DROP|TABLE|INDEX|VIEW|PROCEDURE|FUNCTION|TRIGGER|DATABASE|SCHEMA|GRANT|REVOKE|COMMIT|ROLLBACK|BEGIN|END|IF|ELSE|WHILE|FOR|LOOP|CASE|WHEN|THEN|ELSE|END|DECLARE|SET|EXEC|EXECUTE|UNION|JOIN|INNER|LEFT|RIGHT|FULL|OUTER|ON|GROUP|BY|ORDER|HAVING|DISTINCT|TOP|LIMIT|OFFSET|AS|AND|OR|NOT|IN|EXISTS|LIKE|BETWEEN|IS|NULL|NULL|TRUE|FALSE)\b";
        var strings = @"'[^']*'";
        var comments = @"--.*?$|/\*.*?\*/";
        var numbers = @"\b\d+\.?\d*\b";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetJsonPatterns()
    {
        var keys = @"""\w+""\s*:";
        var strings = @"""[^""]*""";
        var numbers = @"\b\d+\.?\d*\b";
        var booleans = @"\b(true|false|null)\b";

        return new List<(string, TokenType)>
        {
            (strings, TokenType.String),
            (keys, TokenType.Keyword),
            (booleans, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetMarkdownPatterns()
    {
        var headers = @"^#{1,6}\s+.*$";
        var bold = @"\*\*.*?\*\*|__.*?__";
        var italic = @"\*.*?\*|_.*?_";
        var code = @"`[^`]+`";
        var codeBlocks = @"```[\s\S]*?```";
        var links = @"\[.*?\]\(.*?\)";

        return new List<(string, TokenType)>
        {
            (codeBlocks, TokenType.String),
            (headers, TokenType.Keyword),
            (bold, TokenType.Keyword),
            (italic, TokenType.Identifier),
            (code, TokenType.String),
            (links, TokenType.String)
        };
    }

    private List<(string Pattern, TokenType Type)> GetRustPatterns()
    {
        var keywords = @"\b(as|break|const|continue|crate|dyn|else|enum|extern|false|fn|for|if|impl|in|let|loop|match|mod|move|mut|pub|ref|return|self|Self|static|struct|super|trait|true|type|unsafe|use|where|while|async|await|dyn)\b";
        var types = @"\b(i8|i16|i32|i64|i128|isize|u8|u16|u32|u64|u128|usize|f32|f64|bool|char|str|String|Vec|Option|Result|Box|Rc|Arc|RefCell|Mutex|RwLock)\b";
        var strings = @"""[^""]*""|'[^']*'|r#*""[^""]*""#*";
        var comments = @"//.*?$|/\*[\s\S]*?\*/";
        var docComments = @"///.*?$|//!.*?$";
        var numbers = @"\b\d+(_\d+)*\.?\d*[fF]?\d*\b|0x[0-9a-fA-F_]+|0b[01_]+|0o[0-7_]+";
        var lifetimes = @"'\w+";
        var macros = @"\w+!";
        var attributes = @"#\[[\w\s,=()]+\]";

        return new List<(string, TokenType)>
        {
            (docComments, TokenType.Comment),
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (attributes, TokenType.Attribute),
            (macros, TokenType.Function),
            (lifetimes, TokenType.Type),
            (types, TokenType.Type),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetGoPatterns()
    {
        var keywords = @"\b(break|case|chan|const|continue|default|defer|else|fallthrough|for|func|go|goto|if|import|interface|map|package|range|return|select|struct|switch|type|var)\b";
        var types = @"\b(bool|byte|complex64|complex128|error|float32|float64|int|int8|int16|int32|int64|rune|string|uint|uint8|uint16|uint32|uint64|uintptr)\b";
        var builtins = @"\b(append|cap|close|complex|copy|delete|imag|len|make|new|panic|print|println|real|recover)\b";
        var strings = @"`[^`]*`|""[^""]*""";
        var comments = @"//.*?$|/\*[\s\S]*?\*/";
        var numbers = @"\b\d+\.?\d*\b|0x[0-9a-fA-F]+|0o[0-7]+";
        var functions = @"\bfunc\s+(\w+)";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (builtins, TokenType.Function),
            (types, TokenType.Type),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetJavaPatterns()
    {
        var keywords = @"\b(abstract|assert|boolean|break|byte|case|catch|char|class|const|continue|default|do|double|else|enum|extends|final|finally|float|for|goto|if|implements|import|instanceof|int|interface|long|native|new|package|private|protected|public|return|short|static|strictfp|super|switch|synchronized|this|throw|throws|transient|try|void|volatile|while)\b";
        var types = @"\b(boolean|byte|char|double|float|int|long|short|void|String|Object|Integer|Double|Float|Boolean|Character|Long|Short|Byte)\b";
        var annotations = @"@\w+";
        var strings = @"""[^""]*""|'[^']*'";
        var comments = @"//.*?$|/\*[\s\S]*?\*/";
        var docComments = @"/\*\*[\s\S]*?\*/";
        var numbers = @"\b\d+\.?\d*[fFdDlL]?\b|0x[0-9a-fA-F]+";

        return new List<(string, TokenType)>
        {
            (docComments, TokenType.Comment),
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (annotations, TokenType.Attribute),
            (types, TokenType.Type),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetTypeScriptPatterns()
    {
        var keywords = @"\b(break|case|catch|class|const|continue|debugger|default|delete|do|else|export|extends|finally|for|function|if|import|in|instanceof|new|return|super|switch|this|throw|try|typeof|var|let|void|while|with|yield|as|async|await|enum|implements|interface|namespace|private|protected|public|static|readonly|abstract|declare|module|type|keyof|typeof|is|infer)\b";
        var types = @"\b(string|number|boolean|object|undefined|null|void|any|unknown|never|bigint|symbol)\b";
        var strings = @"""[^""]*""|'[^']*'|`[^`]*`";
        var comments = @"//.*?$|/\*[\s\S]*?\*/";
        var docComments = @"/\*\*[\s\S]*?\*/";
        var numbers = @"\b\d+\.?\d*\b|0x[0-9a-fA-F]+|0b[01]+|0o[0-7]+";
        var decorators = @"@\w+";

        return new List<(string, TokenType)>
        {
            (docComments, TokenType.Comment),
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (decorators, TokenType.Attribute),
            (types, TokenType.Type),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetPhpPatterns()
    {
        var keywords = @"\b(abstract|and|array|as|break|callable|case|catch|class|clone|const|continue|declare|default|do|else|elseif|enddeclare|endfor|endforeach|endif|endswitch|endwhile|extends|final|finally|for|foreach|function|global|goto|if|implements|include|include_once|instanceof|insteadof|interface|isset|list|namespace|new|or|private|protected|public|require|require_once|return|static|switch|throw|trait|try|unset|use|var|while|xor|yield|__CLASS__|__DIR__|__FILE__|__FUNCTION__|__LINE__|__METHOD__|__NAMESPACE__|__TRAIT__)\b";
        var strings = @"[""'][^""']*[""']|<<<[\w]+\n[\s\S]*?\n\w+;";
        var comments = @"//.*?$|#.*?$|/\*[\s\S]*?\*/";
        var docComments = @"/\*\*[\s\S]*?\*/";
        var numbers = @"\b\d+\.?\d*\b";
        var variables = @"\$[\w]+";
        var functions = @"\bfunction\s+(\w+)";

        return new List<(string, TokenType)>
        {
            (docComments, TokenType.Comment),
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (variables, TokenType.Variable),
            (functions, TokenType.Function),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetRubyPatterns()
    {
        var keywords = @"\b(alias|and|begin|break|case|class|def|defined\?|do|else|elsif|end|ensure|false|for|if|in|module|next|nil|not|or|redo|rescue|retry|return|self|super|then|true|undef|unless|until|when|while|yield|__FILE__|__LINE__)\b";
        var strings = @"""[^""]*""|'[^']*'|`[^`]*`|%[qQrwx]?[\(\[\{<][^\)\]\}\>]*[\)\]\}\>]";
        var comments = @"#.*?$|=begin[\s\S]*?=end";
        var numbers = @"\b\d+\.?\d*\b|0x[0-9a-fA-F]+|0b[01]+|0o[0-7]+";
        var symbols = @":[\w]+";
        var instanceVars = @"@[\w]+";
        var classVars = @"@@[\w]+";
        var globals = @"\$[\w]+";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (symbols, TokenType.Constant),
            (instanceVars, TokenType.Variable),
            (classVars, TokenType.Variable),
            (globals, TokenType.Variable),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetSwiftPatterns()
    {
        var keywords = @"\b(associatedtype|break|case|catch|class|continue|default|defer|deinit|do|else|enum|extension|fallthrough|fileprivate|for|func|guard|if|import|in|init|inout|internal|is|let|lazy|mutating|nil|nonmutating|open|operator|private|protocol|public|repeat|rethrows|return|self|Self|static|struct|subscript|super|switch|throw|throws|try|typealias|var|where|while)\b";
        var types = @"\b(Bool|Int|Int8|Int16|Int32|Int64|UInt|UInt8|UInt16|UInt32|UInt64|Float|Double|String|Character|Array|Dictionary|Set|Optional|Any|AnyObject)\b";
        var strings = @"""[^""]*""|'[^']*'";
        var comments = @"//.*?$|/\*[\s\S]*?\*/";
        var docComments = @"///.*?$";
        var numbers = @"\b\d+\.?\d*\b";
        var attributes = @"@[\w]+";

        return new List<(string, TokenType)>
        {
            (docComments, TokenType.Comment),
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (attributes, TokenType.Attribute),
            (types, TokenType.Type),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetKotlinPatterns()
    {
        var keywords = @"\b(abstract|actual|annotation|as|break|by|catch|class|companion|const|constructor|continue|crossinline|data|do|dynamic|else|enum|expect|external|final|finally|for|fun|get|if|import|in|infix|init|inline|inner|interface|internal|is|lateinit|noinline|null|object|open|operator|out|override|package|private|protected|public|reified|return|sealed|set|super|suspend|tailrec|this|throw|try|typealias|typeof|val|var|vararg|when|where|while)\b";
        var types = @"\b(Any|Boolean|Byte|Char|Double|Float|Int|Long|Short|String|Unit|Nothing|Number|Comparable)\b";
        var strings = @"""[^""]*""|'[^']*'|""""""[\s\S]*?""""""";
        var comments = @"//.*?$|/\*[\s\S]*?\*/";
        var docComments = @"/\*\*[\s\S]*?\*/";
        var numbers = @"\b\d+\.?\d*[fFdDlL]?\b|0x[0-9a-fA-F]+|0b[01]+";
        var annotations = @"@[\w]+";

        return new List<(string, TokenType)>
        {
            (docComments, TokenType.Comment),
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (annotations, TokenType.Attribute),
            (types, TokenType.Type),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetYamlPatterns()
    {
        var keys = @"^\s*[\w\-]+\s*:";
        var strings = @"""[^""]*""|'[^']*'";
        var comments = @"#.*?$";
        var numbers = @"\b\d+\.?\d*\b";
        var booleans = @"\b(true|false|yes|no|on|off|null)\b";
        var anchors = @"&[\w]+|\*[\w]+";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (anchors, TokenType.Constant),
            (booleans, TokenType.Constant),
            (strings, TokenType.String),
            (keys, TokenType.Meta),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetTomlPatterns()
    {
        var keys = @"^\s*\[[\w\.]+\]";
        var strings = @"""[^""]*""|'[^']*'";
        var comments = @"#.*?$";
        var numbers = @"\b\d+\.?\d*\b";
        var booleans = @"\b(true|false)\b";
        var dates = @"\d{4}-\d{2}-\d{2}(T\d{2}:\d{2}:\d{2}(Z|[\+\-]\d{2}:\d{2})?)?";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (dates, TokenType.Constant),
            (booleans, TokenType.Constant),
            (strings, TokenType.String),
            (keys, TokenType.Meta),
            (numbers, TokenType.Number)
        };
    }

    private List<(string Pattern, TokenType Type)> GetBatchPatterns()
    {
        var keywords = @"\b(echo|set|if|else|for|goto|call|start|exit|pause|rem|@echo|off|on|errorlevel|cd|dir|del|copy|move|ren|md|rd|type|more|find|findstr|sort|cls|title|color|prompt|path|chcp|ver|vol|label|attrib|fc|comp|diskcomp|diskcopy|format|keyb|mode|print|recover|replace|restore|time|tree|xcopy)\b";
        var variables = @"%[\w]+%|![\w]+!";
        var strings = @"""[^""]*""";
        var comments = @"^::.*?$|^rem\s+.*?$";
        var labels = @"^:[\w]+";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (variables, TokenType.Variable),
            (labels, TokenType.Constant),
            (keywords, TokenType.Keyword)
        };
    }

    private List<(string Pattern, TokenType Type)> GetPowerShellPatterns()
    {
        var keywords = @"\b(if|else|elseif|switch|for|foreach|while|do|until|break|continue|return|function|filter|workflow|class|enum|namespace|using|module|param|begin|process|end|try|catch|finally|throw|trap|data|inlinescript|parallel|sequence|flowcontrol|configuration|node|import-dscresource)\b";
        var cmdlets = @"\b(Get-|Set-|New-|Remove-|Add-|Clear-|Copy-|Export-|Import-|Move-|Out-|Pop-|Push-|Rename-|Resolve-|Search-|Select-|Send-|Sort-|Split-|Start-|Stop-|Suspend-|Test-|Trace-|Update-|Wait-|Write-|ConvertFrom-|ConvertTo-|Disable-|Enable-|Format-|Group-|Hide-|Join-|Limit-|Lock-|Measure-|Mount-|Open-|Optimize-|Publish-|Read-|Receive-|Register-|Restore-|Save-|Show-|Skip-|Step-|Switch-|Sync-|Tab-|Tee-|Unblock-|Unlock-|Unpublish-|Unregister-|Use-|Watch-)\w+\b";
        var variables = @"\$[\w]+|\$\{[\w]+\}";
        var strings = @"""[^""]*""|'[^']*'|" + @"`""[^`]*`"""; // PowerShell backtick-escaped strings
        var comments = @"#.*?$|<#[\s\S]*?#>";
        var numbers = @"\b\d+\.?\d*\b";
        var operators = @"(-eq|-ne|-gt|-ge|-lt|-le|-like|-notlike|-match|-notmatch|-contains|-notcontains|-in|-notin|-replace|-split|-join|-and|-or|-not|-xor|-band|-bor|-bxor|-shl|-shr)\b";

        return new List<(string, TokenType)>
        {
            (comments, TokenType.Comment),
            (strings, TokenType.String),
            (variables, TokenType.Variable),
            (cmdlets, TokenType.Function),
            (operators, TokenType.Operator),
            (keywords, TokenType.Keyword),
            (numbers, TokenType.Number)
        };
    }

    private Color GetColorForTokenType(TokenType type)
    {
        // Use theme colors if available, otherwise fall back to legacy keyword colors
        if (_theme != null)
        {
            return _theme.GetTokenColor(type);
        }

        var palette = GetResolvedPalette();
        var defaultForeColor = palette?.GetContentShortTextColor1(PaletteContentStyle.InputControlStandalone, PaletteState.Normal) ?? Color.Black;

        return type switch
        {
            TokenType.Keyword => _keywordColors.TryGetValue("keyword", out var kw) ? kw : defaultForeColor,
            TokenType.String => _keywordColors.TryGetValue("string", out var str) ? str : defaultForeColor,
            TokenType.Comment => _keywordColors.TryGetValue("comment", out var cmt) ? cmt : defaultForeColor,
            TokenType.Number => _keywordColors.TryGetValue("number", out var num) ? num : defaultForeColor,
            TokenType.Preprocessor => _keywordColors.TryGetValue("preprocessor", out var prep) ? prep : defaultForeColor,
            _ => defaultForeColor
        };
    }

    private Font GetFontForTokenType(TokenType type)
    {
        return type == TokenType.Keyword
            ? new Font(_editorFont, FontStyle.Bold)
            : _editorFont;
    }

    private void UpdateFoldBlocks()
    {
        _foldBlocks.Clear();

        if (_language == Language.None)
        {
            return;
        }

        var lines = _richTextBox.Lines;
        var indentStack = new Stack<(int Line, int Indent)>();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var indent = GetIndentLevel(line);

            // Check for block start
            if (IsBlockStart(line))
            {
                indentStack.Push((i, indent));
            }
            // Check for block end
            else if (IsBlockEnd(line) && indentStack.Count > 0)
            {
                var start = indentStack.Pop();
                if (start.Indent < indent)
                {
                    _foldBlocks.Add(new FoldBlock
                    {
                        StartLine = start.Line,
                        EndLine = i,
                        IndentLevel = start.Indent
                    });
                }
            }
        }
    }

    private int GetIndentLevel(string line)
    {
        int indent = 0;
        foreach (char c in line)
        {
            if (c == ' ' || c == '\t')
            {
                indent++;
            }
            else
            {
                break;
            }
        }
        return indent;
    }

    private bool IsBlockStart(string line)
    {
        var trimmed = line.Trim();
        return _language switch
        {
            Language.CSharp or Language.Cpp => trimmed.EndsWith("{") || trimmed.StartsWith("namespace") || trimmed.StartsWith("class") || trimmed.StartsWith("struct") || trimmed.StartsWith("if") || trimmed.StartsWith("for") || trimmed.StartsWith("while"),
            Language.VbNet => trimmed.StartsWith("Sub") || trimmed.StartsWith("Function") || trimmed.StartsWith("If") || trimmed.StartsWith("For") || trimmed.StartsWith("While"),
            Language.Python => trimmed.EndsWith(":") && (trimmed.StartsWith("def") || trimmed.StartsWith("class") || trimmed.StartsWith("if") || trimmed.StartsWith("for") || trimmed.StartsWith("while")),
            _ => false
        };
    }

    private bool IsBlockEnd(string line)
    {
        var trimmed = line.Trim();
        return _language switch
        {
            Language.CSharp or Language.Cpp => trimmed == "}",
            Language.VbNet => trimmed == "End Sub" || trimmed == "End Function" || trimmed == "End If" || trimmed == "End For" || trimmed == "End While",
            Language.Python => GetIndentLevel(line) == 0 && !string.IsNullOrWhiteSpace(line),
            _ => false
        };
    }

    private void HighlightMatchingBraces()
    {
        // Simple brace matching - highlight matching braces
        var text = _richTextBox.Text;
        var pos = _richTextBox.SelectionStart;

        if (pos >= text.Length)
        {
            return;
        }

        char charAtPos = text[pos];
        char? matchingChar = null;
        bool forward = false;

        switch (charAtPos)
        {
            case '{':
                matchingChar = '}';
                forward = true;
                break;
            case '}':
                matchingChar = '{';
                forward = false;
                break;
            case '(':
                matchingChar = ')';
                forward = true;
                break;
            case ')':
                matchingChar = '(';
                forward = false;
                break;
            case '[':
                matchingChar = ']';
                forward = true;
                break;
            case ']':
                matchingChar = '[';
                forward = false;
                break;
        }

        if (matchingChar.HasValue)
        {
            // Find matching brace (simplified - doesn't handle nested braces perfectly)
            // This is a basic implementation
        }
    }

    private void IndentSelection()
    {
        var lines = _richTextBox.Lines;
        var startLine = _richTextBox.GetLineFromCharIndex(_richTextBox.SelectionStart);
        var endLine = _richTextBox.GetLineFromCharIndex(_richTextBox.SelectionStart + _richTextBox.SelectionLength);

        var sb = new StringBuilder();
        for (int i = startLine; i <= endLine && i < lines.Length; i++)
        {
            sb.AppendLine("    " + lines[i]);
        }

        var startIndex = _richTextBox.GetFirstCharIndexFromLine(startLine);
        var length = _richTextBox.GetFirstCharIndexFromLine(endLine + 1) - startIndex;

        _richTextBox.Select(startIndex, length);
        _richTextBox.SelectedText = sb.ToString();
    }

    private void UnindentSelection()
    {
        var lines = _richTextBox.Lines;
        var startLine = _richTextBox.GetLineFromCharIndex(_richTextBox.SelectionStart);
        var endLine = _richTextBox.GetLineFromCharIndex(_richTextBox.SelectionStart + _richTextBox.SelectionLength);

        var sb = new StringBuilder();
        for (int i = startLine; i <= endLine && i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.StartsWith("    "))
            {
                sb.AppendLine(line.Substring(4));
            }
            else if (line.StartsWith("\t"))
            {
                sb.AppendLine(line.Substring(1));
            }
            else
            {
                sb.AppendLine(line);
            }
        }

        var startIndex = _richTextBox.GetFirstCharIndexFromLine(startLine);
        var length = _richTextBox.GetFirstCharIndexFromLine(endLine + 1) - startIndex;

        _richTextBox.Select(startIndex, length);
        _richTextBox.SelectedText = sb.ToString();
    }

    private void AutoIndent()
    {
        var currentLine = _richTextBox.GetLineFromCharIndex(_richTextBox.SelectionStart);
        if (currentLine > 0)
        {
            var previousLine = _richTextBox.Lines[currentLine - 1];
            var indent = GetIndentLevel(previousLine);

            if (IsBlockStart(previousLine))
            {
                indent += 4; // Add one level of indentation
            }

            _richTextBox.SelectedText = new string(' ', indent);
        }
    }

    private void OnRichTextBoxKeyPress(object? sender, KeyPressEventArgs e)
    {
        if (_autoCompleteEnabled && _language != Language.None)
        {
            // If the auto-complete popup is already visible, update it in-place.
            // Re-running ShowAutoComplete() on every keypress causes noticeable flashing.
            if (_autoCompleteForm != null && _autoCompleteForm.Visible)
            {
                _autoCompleteForm.UpdateFilter(e.KeyChar);
                return;
            }

            // Show auto-complete on certain characters
            if (char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '_')
            {
                ShowAutoComplete();
            }
            else if (e.KeyChar == '.' || e.KeyChar == '(')
            {
                ShowAutoComplete();
            }
        }
    }

    private void InitializeAutoCompleteKeywords()
    {
        _autoCompleteKeywords = new List<string>();

        // Language-specific keywords will be set when language changes
        UpdateAutoCompleteKeywords();
    }

    private void UpdateAutoCompleteKeywords()
    {
        _autoCompleteKeywords.Clear();

        _autoCompleteKeywords.AddRange(_language switch
        {
            Language.CSharp => new[] { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "async", "await", "Console", "WriteLine", "ReadLine", "Math", "String", "Int32", "Double", "Boolean" },
            Language.Cpp => new[] { "auto", "bool", "break", "case", "catch", "char", "class", "const", "continue", "default", "delete", "do", "double", "else", "enum", "extern", "float", "for", "goto", "if", "int", "long", "namespace", "new", "operator", "private", "protected", "public", "return", "short", "signed", "sizeof", "static", "struct", "switch", "template", "this", "throw", "try", "typedef", "union", "unsigned", "using", "virtual", "void", "volatile", "while", "cout", "cin", "endl", "string", "vector", "map", "set" },
            Language.VbNet => new[] { "AddHandler", "AddressOf", "Alias", "And", "AndAlso", "As", "Boolean", "ByRef", "Byte", "ByVal", "Call", "Case", "Catch", "Char", "Class", "Const", "Continue", "Date", "Decimal", "Declare", "Default", "Delegate", "Dim", "Do", "Double", "Each", "Else", "ElseIf", "End", "Enum", "Erase", "Error", "Event", "Exit", "False", "Finally", "For", "Friend", "Function", "Get", "GetType", "Global", "GoSub", "GoTo", "Handles", "If", "Implements", "Imports", "In", "Inherits", "Integer", "Interface", "Is", "IsNot", "Let", "Lib", "Like", "Long", "Loop", "Me", "Mod", "Module", "MustInherit", "MustOverride", "MyBase", "MyClass", "Namespace", "Narrowing", "New", "Next", "Not", "Nothing", "NotInheritable", "NotOverridable", "Object", "Of", "On", "Operator", "Option", "Optional", "Or", "OrElse", "Overloads", "Overridable", "Overrides", "ParamArray", "Partial", "Private", "Property", "Protected", "Public", "RaiseEvent", "ReadOnly", "ReDim", "REM", "RemoveHandler", "Resume", "Return", "SByte", "Select", "Set", "Shadows", "Shared", "Short", "Single", "Static", "Step", "Stop", "String", "Structure", "Sub", "SyncLock", "Then", "Throw", "To", "True", "Try", "TryCast", "TypeOf", "UInteger", "ULong", "UShort", "Using", "Variant", "Wend", "When", "While", "Widening", "With", "WithEvents", "WriteOnly", "Xor", "Console", "WriteLine", "ReadLine", "Math", "String", "Integer", "Double", "Boolean" },
            Language.JavaScript => new[] { "break", "case", "catch", "class", "const", "continue", "debugger", "default", "delete", "do", "else", "export", "extends", "finally", "for", "function", "if", "import", "in", "instanceof", "new", "return", "super", "switch", "this", "throw", "try", "typeof", "var", "let", "void", "while", "with", "yield", "console", "log", "alert", "document", "window", "Array", "Object", "String", "Number", "Boolean", "Date", "Math", "JSON", "Promise", "async", "await" },
            Language.TypeScript => new[] { "break", "case", "catch", "class", "const", "continue", "debugger", "default", "delete", "do", "else", "export", "extends", "finally", "for", "function", "if", "import", "in", "instanceof", "new", "return", "super", "switch", "this", "throw", "try", "typeof", "var", "let", "void", "while", "with", "yield", "as", "async", "await", "enum", "implements", "interface", "namespace", "private", "protected", "public", "static", "readonly", "abstract", "declare", "module", "type", "keyof", "typeof", "is", "infer", "string", "number", "boolean", "object", "undefined", "null", "void", "any", "unknown", "never", "bigint", "symbol" },
            Language.Python => new[] { "and", "as", "assert", "break", "class", "continue", "def", "del", "elif", "else", "except", "exec", "finally", "for", "from", "global", "if", "import", "in", "is", "lambda", "not", "or", "pass", "print", "raise", "return", "try", "while", "with", "yield", "True", "False", "None", "abs", "all", "any", "bin", "bool", "chr", "dict", "dir", "divmod", "enumerate", "eval", "exec", "filter", "float", "format", "frozenset", "getattr", "hasattr", "hash", "help", "hex", "id", "input", "int", "isinstance", "issubclass", "iter", "len", "list", "map", "max", "min", "next", "oct", "open", "ord", "pow", "print", "range", "repr", "reversed", "round", "set", "slice", "sorted", "str", "sum", "tuple", "type", "vars", "zip" },
            Language.Rust => new[] { "as", "break", "const", "continue", "crate", "dyn", "else", "enum", "extern", "false", "fn", "for", "if", "impl", "in", "let", "loop", "match", "mod", "move", "mut", "pub", "ref", "return", "self", "Self", "static", "struct", "super", "trait", "true", "type", "unsafe", "use", "where", "while", "async", "await", "dyn", "i8", "i16", "i32", "i64", "i128", "isize", "u8", "u16", "u32", "u64", "u128", "usize", "f32", "f64", "bool", "char", "str", "String", "Vec", "Option", "Result", "Box", "Rc", "Arc", "RefCell", "Mutex", "RwLock" },
            Language.Go => new[] { "break", "case", "chan", "const", "continue", "default", "defer", "else", "fallthrough", "for", "func", "go", "goto", "if", "import", "interface", "map", "package", "range", "return", "select", "struct", "switch", "type", "var", "bool", "byte", "complex64", "complex128", "error", "float32", "float64", "int", "int8", "int16", "int32", "int64", "rune", "string", "uint", "uint8", "uint16", "uint32", "uint64", "uintptr", "append", "cap", "close", "complex", "copy", "delete", "imag", "len", "make", "new", "panic", "print", "println", "real", "recover" },
            Language.Java => new[] { "abstract", "assert", "boolean", "break", "byte", "case", "catch", "char", "class", "const", "continue", "default", "do", "double", "else", "enum", "extends", "final", "finally", "float", "for", "goto", "if", "implements", "import", "instanceof", "int", "interface", "long", "native", "new", "package", "private", "protected", "public", "return", "short", "static", "strictfp", "super", "switch", "synchronized", "this", "throw", "throws", "transient", "try", "void", "volatile", "while", "String", "Object", "Integer", "Double", "Float", "Boolean", "Character", "Long", "Short", "Byte" },
            Language.Php => new[] { "abstract", "and", "array", "as", "break", "callable", "case", "catch", "class", "clone", "const", "continue", "declare", "default", "do", "else", "elseif", "enddeclare", "endfor", "endforeach", "endif", "endswitch", "endwhile", "extends", "final", "finally", "for", "foreach", "function", "global", "goto", "if", "implements", "include", "include_once", "instanceof", "insteadof", "interface", "isset", "list", "namespace", "new", "or", "private", "protected", "public", "require", "require_once", "return", "static", "switch", "throw", "trait", "try", "unset", "use", "var", "while", "xor", "yield" },
            Language.Ruby => new[] { "alias", "and", "begin", "break", "case", "class", "def", "defined?", "do", "else", "elsif", "end", "ensure", "false", "for", "if", "in", "module", "next", "nil", "not", "or", "redo", "rescue", "retry", "return", "self", "super", "then", "true", "undef", "unless", "until", "when", "while", "yield" },
            Language.Swift => new[] { "associatedtype", "break", "case", "catch", "class", "continue", "default", "defer", "deinit", "do", "else", "enum", "extension", "fallthrough", "fileprivate", "for", "func", "guard", "if", "import", "in", "init", "inout", "internal", "is", "let", "lazy", "mutating", "nil", "nonmutating", "open", "operator", "private", "protocol", "public", "repeat", "rethrows", "return", "self", "Self", "static", "struct", "subscript", "super", "switch", "throw", "throws", "try", "typealias", "var", "where", "while", "Bool", "Int", "Int8", "Int16", "Int32", "Int64", "UInt", "UInt8", "UInt16", "UInt32", "UInt64", "Float", "Double", "String", "Character", "Array", "Dictionary", "Set", "Optional", "Any", "AnyObject" },
            Language.Kotlin => new[] { "abstract", "actual", "annotation", "as", "break", "by", "catch", "class", "companion", "const", "constructor", "continue", "crossinline", "data", "do", "dynamic", "else", "enum", "expect", "external", "final", "finally", "for", "fun", "get", "if", "import", "in", "infix", "init", "inline", "inner", "interface", "internal", "is", "lateinit", "noinline", "null", "object", "open", "operator", "out", "override", "package", "private", "protected", "public", "reified", "return", "sealed", "set", "super", "suspend", "tailrec", "this", "throw", "try", "typealias", "typeof", "val", "var", "vararg", "when", "where", "while", "Any", "Boolean", "Byte", "Char", "Double", "Float", "Int", "Long", "Short", "String", "Unit", "Nothing", "Number", "Comparable" },
            Language.Sql => new[] { "SELECT", "FROM", "WHERE", "INSERT", "UPDATE", "DELETE", "CREATE", "ALTER", "DROP", "TABLE", "INDEX", "VIEW", "PROCEDURE", "FUNCTION", "TRIGGER", "DATABASE", "SCHEMA", "GRANT", "REVOKE", "COMMIT", "ROLLBACK", "BEGIN", "END", "IF", "ELSE", "WHILE", "FOR", "LOOP", "CASE", "WHEN", "THEN", "ELSE", "END", "DECLARE", "SET", "EXEC", "EXECUTE", "UNION", "JOIN", "INNER", "LEFT", "RIGHT", "FULL", "OUTER", "ON", "GROUP", "BY", "ORDER", "HAVING", "DISTINCT", "TOP", "LIMIT", "OFFSET", "AS", "AND", "OR", "NOT", "IN", "EXISTS", "LIKE", "BETWEEN", "IS", "NULL", "TRUE", "FALSE", "COUNT", "SUM", "AVG", "MAX", "MIN", "CAST", "CONVERT", "GETDATE", "GETUTCDATE", "YEAR", "MONTH", "DAY", "DATEPART", "DATEADD", "DATEDIFF" },
            Language.PowerShell => new[] { "if", "else", "elseif", "switch", "for", "foreach", "while", "do", "until", "break", "continue", "return", "function", "filter", "workflow", "class", "enum", "namespace", "using", "module", "param", "begin", "process", "end", "try", "catch", "finally", "throw", "trap", "Get-", "Set-", "New-", "Remove-", "Add-", "Clear-", "Copy-", "Export-", "Import-", "Move-", "Out-", "Pop-", "Push-", "Rename-", "Resolve-", "Search-", "Select-", "Send-", "Sort-", "Split-", "Start-", "Stop-", "Suspend-", "Test-", "Trace-", "Update-", "Wait-", "Write-" },
            _ => new List<string>()
        });
    }

    private void ShowAutoComplete()
    {
        if (_autoCompleteForm == null)
        {
            _autoCompleteForm = new VisualAutoCompleteForm(this);
        }

        var currentWord = GetCurrentWord();
        // Only set source items once per open; filtering happens on keypress while open.
        if (!_autoCompleteForm.Visible)
        {
            _autoCompleteForm.SetItems(_autoCompleteKeywords);
        }

        _autoCompleteForm.SetCurrentWordPrefix(currentWord);
        _autoCompleteForm.FilterItems(currentWord);

        if (_autoCompleteForm.ItemCount > 0)
        {
            var pos = _richTextBox.GetPositionFromCharIndex(_richTextBox.SelectionStart);
            var screenPos = PointToScreen(pos);
            _autoCompleteForm.Location = new Point(screenPos.X, screenPos.Y + _richTextBox.Font.Height);
            if (!_autoCompleteForm.Visible)
            {
                _autoCompleteForm.Show();
            }
        }
        else
        {
            _autoCompleteForm.Hide();
        }
    }

    private string GetCurrentWord()
    {
        var text = _richTextBox.Text;
        var pos = _richTextBox.SelectionStart;

        int start = pos;
        while (start > 0 && (char.IsLetterOrDigit(text[start - 1]) || text[start - 1] == '_'))
        {
            start--;
        }

        return text.Substring(start, pos - start);
    }

    internal void ToggleFoldBlock(FoldBlock block)
    {
        // Suppress redraw to avoid flashing while we change formatting
        BeginRedraw(_richTextBox);
        try
        {
            if (block.IsFolded)
            {
                // Hide lines
                var startIndex = _richTextBox.GetFirstCharIndexFromLine(block.StartLine + 1);
                var endIndex = _richTextBox.GetFirstCharIndexFromLine(block.EndLine);

                if (endIndex > startIndex)
                {
                    _richTextBox.Select(startIndex, endIndex - startIndex);
                    // Store the hidden text in a tag or marker
                    // For now, we'll just collapse visually by setting font size to 0
                    _richTextBox.SelectionFont = new Font(_richTextBox.Font.FontFamily, 0.1f);
                    _richTextBox.SelectionLength = 0;
                }
            }
            else
            {
                // Show lines - restore font
                var startIndex = _richTextBox.GetFirstCharIndexFromLine(block.StartLine + 1);
                var endIndex = _richTextBox.GetFirstCharIndexFromLine(block.EndLine);

                if (endIndex > startIndex)
                {
                    _richTextBox.Select(startIndex, endIndex - startIndex);
                    _richTextBox.SelectionFont = _editorFont;
                    _richTextBox.SelectionLength = 0;
                }
            }
        }
        finally
        {
            EndRedraw(_richTextBox);
        }

        _foldingMargin.Invalidate();
    }

    /// <summary>
    /// Gets whether a line is collapsed (folded).
    /// </summary>
    internal bool IsLineCollapsed(int lineNumber)
    {
        return _foldBlocks.Any(block =>
            block.IsFolded &&
            lineNumber > block.StartLine &&
            lineNumber < block.EndLine);
    }

    #endregion

    #region Designer Support

    /// <summary>
    /// Gets the ViewManager instance for designer support.
    /// </summary>
    /// <returns>ViewManager instance or null.</returns>
    public ViewManager? GetViewManager() => ViewManager;

    /// <summary>
    /// Gets a component from the given point.
    /// </summary>
    /// <param name="pt">Point in client coordinates.</param>
    /// <returns>Component at the point, or null if none.</returns>
    public Component? DesignerComponentFromPoint(Point pt) =>
        // Ignore call as view builder is already destructed
        IsDisposed ? null : ViewManager?.ComponentFromPoint(pt);

    /// <summary>
    /// Simulates the mouse leaving the control for designer support.
    /// </summary>
    public void DesignerMouseLeave() =>
        // Simulate the mouse leaving the control so that the tracking
        // element that thinks it has the focus is informed it does not
        OnMouseLeave(EventArgs.Empty);

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the Text property value changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the Text property value changes.")]
    public new event EventHandler? TextChanged;

    #endregion
}

