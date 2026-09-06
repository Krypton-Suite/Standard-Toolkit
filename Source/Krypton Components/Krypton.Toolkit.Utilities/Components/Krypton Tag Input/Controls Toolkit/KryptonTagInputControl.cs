#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// A wrap-capable editor that displays tags as themed chips with a trailing <see cref="KryptonTextBox"/>.
/// Commit with Enter or comma; Backspace removes the last tag when the input is empty. Tab is left for focus navigation.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTextBox), "ToolboxBitmaps.KryptonTextBox.bmp")]
[DefaultEvent(nameof(TagAdded))]
[DefaultProperty(nameof(Tags))]
[Designer(typeof(KryptonTagInputControlDesigner))]
[DesignerCategory(@"code")]
[DisplayName(@"Krypton Tag Input")]
[Description(@"Wrap-capable tag editor with themed chips, suggestions, and optional category colours.")]
[Docking(DockingBehavior.Ask)]
public class KryptonTagInputControl : KryptonPanel
{
    #region Instance Fields

    private readonly FlowLayoutPanel _flow;
    private readonly KryptonTextBox _inputBox;
    private readonly Dictionary<string, Color> _categoryColors;
    private readonly AutoCompleteStringCollection _suggestions;
    private bool _readOnly;
    private bool _committing;
    private bool _suspendInputEvents;

    #endregion

    #region Events

    /// <summary>
    /// Occurs before a tag is added. Set <see cref="CancelEventArgs.Cancel"/> to reject it.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs before a tag is added. Set Cancel to reject it.")]
    public event EventHandler<KryptonTagCancelEventArgs>? TagAdding;

    /// <summary>
    /// Occurs after a tag has been added.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs after a tag has been added.")]
    public event EventHandler<KryptonTagEventArgs>? TagAdded;

    /// <summary>
    /// Occurs after a tag has been removed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs after a tag has been removed.")]
    public event EventHandler<KryptonTagEventArgs>? TagRemoved;

    /// <summary>
    /// Occurs after the tag collection has changed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs after the tag collection has changed.")]
    public event EventHandler? TagsChanged;

    /// <summary>
    /// Raises the <see cref="TagAdding"/> event.
    /// </summary>
    /// <param name="e">Event arguments describing the proposed tag.</param>
    protected virtual void OnTagAdding(KryptonTagCancelEventArgs e) => TagAdding?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="TagAdded"/> event.
    /// </summary>
    /// <param name="e">Event arguments describing the added tag.</param>
    protected virtual void OnTagAdded(KryptonTagEventArgs e) => TagAdded?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="TagRemoved"/> event.
    /// </summary>
    /// <param name="e">Event arguments describing the removed tag.</param>
    protected virtual void OnTagRemoved(KryptonTagEventArgs e) => TagRemoved?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="TagsChanged"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnTagsChanged(EventArgs e) => TagsChanged?.Invoke(this, e);

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagInputControl"/> class.
    /// </summary>
    public KryptonTagInputControl()
    {
        Values = new KryptonTagInputValues(this);
        Tags = new KryptonTagCollection(this);
        _suggestions = new AutoCompleteStringCollection();
        _categoryColors = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);

        _flow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            WrapContents = true,
            AutoScroll = true,
            BackColor = Color.Transparent,
            Margin = Padding.Empty,
            Padding = Padding.Empty,
            TabStop = false
        };

        _inputBox = new KryptonTextBox
        {
            AutoSize = false,
            Width = Values.InputWidth,
            MinimumSize = new Size(40, 0),
            Margin = new Padding(2),
            TabIndex = 0,
            AutoCompleteMode = AutoCompleteMode.SuggestAppend,
            AutoCompleteSource = AutoCompleteSource.CustomSource,
            AutoCompleteCustomSource = _suggestions
        };
        _inputBox.KeyDown += OnInputKeyDown;
        _inputBox.KeyPress += OnInputKeyPress;
        _inputBox.TextChanged += OnInputTextChanged;

        _flow.Controls.Add(_inputBox);
        base.Controls.Add(_flow);

        ApplyValues();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _inputBox.KeyDown -= OnInputKeyDown;
            _inputBox.KeyPress -= OnInputKeyPress;
            _inputBox.TextChanged -= OnInputTextChanged;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the collection of current tags.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of current tags.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    public KryptonTagCollection Tags { get; }

    private bool ShouldSerializeTags() => Tags.Count > 0;
    private void ResetTags() => ClearTags();

    /// <summary>
    /// Gets the suggestion list used for auto-complete.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Suggestion strings offered by the input auto-complete.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(false)]
    public AutoCompleteStringCollection Suggestions => _suggestions;

    private bool ShouldSerializeSuggestions() => _suggestions.Count > 0;
    private void ResetSuggestions() => _suggestions.Clear();

    /// <summary>
    /// Gets the behaviour and appearance values for this control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Behaviour and appearance values for the tag input.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTagInputValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;
    private void ResetValues() => Values.Reset();

    /// <summary>
    /// Gets or sets a value indicating whether tags can be added or removed from the UI.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether tags can be added or removed from the UI.")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get => _readOnly;
        set
        {
            if (_readOnly != value)
            {
                _readOnly = value;
                ApplyValues();
            }
        }
    }

    /// <summary>
    /// Child controls are owned by the tag input and are not serialized by the designer.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Control.ControlCollection Controls => base.Controls;

    /// <summary>
    /// Adds <paramref name="tag"/> if it passes validation and is not cancelled.
    /// </summary>
    /// <param name="tag">Tag text to add.</param>
    /// <returns>true if the tag was added; otherwise, false.</returns>
    public bool AddTag(string tag)
    {
        var trimmed = (tag ?? string.Empty).Trim();
        if (trimmed.Length == 0)
        {
            return false;
        }

        if (!CanAcceptTag(trimmed))
        {
            return false;
        }

        Tags.SuspendOwnerNotify = true;
        Tags.Add(trimmed);
        Tags.SuspendOwnerNotify = false;

        NotifyTagInserted(trimmed);
        return true;
    }

    /// <summary>
    /// Removes the first tag that matches <paramref name="tag"/> using the current comparison.
    /// </summary>
    /// <param name="tag">Tag text to remove.</param>
    /// <returns>true if a tag was removed; otherwise, false.</returns>
    public bool RemoveTag(string tag)
    {
        var existing = FindExisting(tag);
        if (existing == null)
        {
            return false;
        }

        Tags.SuspendOwnerNotify = true;
        Tags.Remove(existing);
        Tags.SuspendOwnerNotify = false;

        NotifyTagRemoved(existing);
        return true;
    }

    /// <summary>
    /// Removes every tag.
    /// </summary>
    public void ClearTags()
    {
        if (Tags.Count == 0)
        {
            return;
        }

        var snapshot = new string[Tags.Count];
        Tags.CopyTo(snapshot, 0);

        Tags.SuspendOwnerNotify = true;
        Tags.Clear();
        Tags.SuspendOwnerNotify = false;

        NotifyTagsCleared(snapshot);
    }

    /// <summary>
    /// Replaces the suggestion list used for auto-complete.
    /// </summary>
    /// <param name="items">Suggestion strings.</param>
    public void SetSuggestions(IEnumerable<string> items)
    {
        _suggestions.Clear();
        if (items == null)
        {
            return;
        }

        foreach (var item in items)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                _suggestions.Add(item.Trim());
            }
        }
    }

    /// <summary>
    /// Assigns a fill colour used when a tag matches <paramref name="category"/>.
    /// </summary>
    /// <param name="category">Tag or category name.</param>
    /// <param name="color">Chip fill colour.</param>
    public void SetCategoryColor(string category, Color color)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            return;
        }

        _categoryColors[category.Trim()] = color;
        ApplyChipAppearances();
    }

    /// <summary>
    /// Tries to get the category colour assigned to <paramref name="category"/>.
    /// </summary>
    /// <param name="category">Tag or category name.</param>
    /// <param name="color">Assigned colour when the method returns true.</param>
    /// <returns>true if a colour is assigned; otherwise, false.</returns>
    public bool TryGetCategoryColor(string category, out Color color)
    {
        if (!string.IsNullOrWhiteSpace(category) && _categoryColors.TryGetValue(category.Trim(), out color))
        {
            return true;
        }

        color = Color.Empty;
        return false;
    }

    /// <summary>
    /// Removes the category colour assigned to <paramref name="category"/>.
    /// </summary>
    /// <param name="category">Tag or category name.</param>
    /// <returns>true if a colour was removed; otherwise, false.</returns>
    public bool RemoveCategoryColor(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            return false;
        }

        var removed = _categoryColors.Remove(category.Trim());
        if (removed)
        {
            ApplyChipAppearances();
        }

        return removed;
    }

    /// <summary>
    /// Clears every category colour override.
    /// </summary>
    public void ClearCategoryColors()
    {
        if (_categoryColors.Count == 0)
        {
            return;
        }

        _categoryColors.Clear();
        ApplyChipAppearances();
    }

    #endregion

    #region Internal

    /// <summary>
    /// Returns true if <paramref name="tag"/> may be inserted (static rules plus <see cref="TagAdding"/>).
    /// </summary>
    internal bool CanAcceptTag(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            return false;
        }

        if (Values.MaxTags > 0 && Tags.Count >= Values.MaxTags)
        {
            return false;
        }

        if (!Values.AllowDuplicates && FindExisting(tag) != null)
        {
            return false;
        }

        if (!Values.AllowCustomTags && !IsSuggested(tag))
        {
            return false;
        }

        var args = new KryptonTagCancelEventArgs(tag);
        OnTagAdding(args);
        return !args.Cancel;
    }

    /// <summary>
    /// Completes a collection insert by creating the chip and raising add events.
    /// </summary>
    internal void NotifyTagInserted(string tag)
    {
        AddChip(tag);
        OnTagAdded(new KryptonTagEventArgs(tag));
        OnTagsChanged(EventArgs.Empty);
        ApplyInputVisibility();
    }

    /// <summary>
    /// Completes a collection remove by disposing the chip and raising remove events.
    /// </summary>
    internal void NotifyTagRemoved(string tag)
    {
        RemoveChip(tag);
        OnTagRemoved(new KryptonTagEventArgs(tag));
        OnTagsChanged(EventArgs.Empty);
        ApplyInputVisibility();
    }

    /// <summary>
    /// Completes a collection clear.
    /// </summary>
    internal void NotifyTagsCleared(IReadOnlyList<string> tags)
    {
        RemoveAllChips();
        foreach (var tag in tags)
        {
            OnTagRemoved(new KryptonTagEventArgs(tag));
        }

        OnTagsChanged(EventArgs.Empty);
        ApplyInputVisibility();
    }

    /// <summary>
    /// Applies live values from <see cref="Values"/> to the input and chips.
    /// </summary>
    internal void OnTagInputValuesChanged() => ApplyValues();

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(280, 36);

    /// <inheritdoc />
    protected override Padding DefaultPadding => new Padding(4);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        ApplyValues();
    }

    #endregion

    #region Implementation

    private StringComparison Comparison =>
        Values.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

    private string? FindExisting(string tag)
    {
        foreach (var existing in Tags)
        {
            if (string.Equals(existing, tag, Comparison))
            {
                return existing;
            }
        }

        return null;
    }

    private bool IsSuggested(string tag)
    {
        foreach (string suggestion in _suggestions)
        {
            if (string.Equals(suggestion, tag, Comparison))
            {
                return true;
            }
        }

        return false;
    }

    private void AddChip(string tag)
    {
        var chip = new KryptonTagChip(this, tag);
        ApplyChipAppearance(chip);
        _flow.Controls.Add(chip);
        _flow.Controls.SetChildIndex(_inputBox, _flow.Controls.Count - 1);
    }

    private void RemoveChip(string tag)
    {
        var chip = FindChip(tag);
        if (chip == null)
        {
            return;
        }

        _flow.Controls.Remove(chip);
        chip.Dispose();
    }

    private void RemoveAllChips()
    {
        var chips = _flow.Controls.OfType<KryptonTagChip>().ToArray();
        foreach (var chip in chips)
        {
            _flow.Controls.Remove(chip);
            chip.Dispose();
        }
    }

    private KryptonTagChip? FindChip(string tag) =>
        _flow.Controls.OfType<KryptonTagChip>()
            .FirstOrDefault(chip => string.Equals(chip.TagText, tag, Comparison));

    private void ApplyChipAppearance(KryptonTagChip chip)
    {
        _categoryColors.TryGetValue(chip.TagText, out var color);
        var interactive = Enabled && !_readOnly;
        chip.ApplyAppearance(color, Values.ChipRounding, Values.ShowRemoveButton, interactive);
    }

    private void ApplyChipAppearances()
    {
        foreach (var chip in _flow.Controls.OfType<KryptonTagChip>())
        {
            ApplyChipAppearance(chip);
        }
    }

    private void ApplyValues()
    {
        _inputBox.Width = Values.InputWidth;
        _inputBox.CueHint.CueHintText = Values.CueHintText;
        _inputBox.ReadOnly = _readOnly;
        _inputBox.AutoCompleteMode = Values.EnableSuggestions && !_readOnly
            ? AutoCompleteMode.SuggestAppend
            : AutoCompleteMode.None;
        _inputBox.AutoCompleteSource = Values.EnableSuggestions
            ? AutoCompleteSource.CustomSource
            : AutoCompleteSource.None;
        ApplyInputVisibility();
        ApplyChipAppearances();
    }

    private void ApplyInputVisibility()
    {
        var atMax = Values.MaxTags > 0 && Tags.Count >= Values.MaxTags;
        _inputBox.Visible = !_readOnly && !atMax;
    }

    private void OnInputKeyDown(object? sender, KeyEventArgs e)
    {
        if (_readOnly)
        {
            return;
        }

        if (e.KeyCode == Keys.Enter && Values.CommitOnEnter)
        {
            e.SuppressKeyPress = true;
            e.Handled = true;
            CommitCurrentInput();
        }
        else if (e.KeyCode == Keys.Back &&
                 Values.RemoveLastOnBackspace &&
                 string.IsNullOrWhiteSpace(_inputBox.Text) &&
                 Tags.Count > 0)
        {
            e.SuppressKeyPress = true;
            e.Handled = true;
            RemoveTag(Tags[Tags.Count - 1]);
        }
        else if (e.KeyCode == Keys.Escape && Values.ClearOnEscape)
        {
            e.SuppressKeyPress = true;
            e.Handled = true;
            ClearInputText();
        }
    }

    private void OnInputKeyPress(object? sender, KeyPressEventArgs e)
    {
        if (_readOnly || !Values.CommitOnComma || e.KeyChar != ',')
        {
            return;
        }

        e.Handled = true;
        CommitCurrentInput();
    }

    private void OnInputTextChanged(object? sender, EventArgs e)
    {
        if (_suspendInputEvents || _committing || _readOnly || !Values.CommitOnComma)
        {
            return;
        }

        if (_inputBox.Text.IndexOf(',') >= 0)
        {
            CommitDelimitedText(_inputBox.Text);
        }
    }

    private void CommitCurrentInput()
    {
        var text = _inputBox.Text.Trim();
        if (text.Length == 0)
        {
            return;
        }

        if (Values.CommitOnComma && text.IndexOf(',') >= 0)
        {
            CommitDelimitedText(text);
            return;
        }

        if (AddTag(text))
        {
            ClearInputText();
        }
    }

    private void CommitDelimitedText(string text)
    {
        if (_committing)
        {
            return;
        }

        _committing = true;
        try
        {
            var parts = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                AddTag(part.Trim());
            }

            ClearInputText();
        }
        finally
        {
            _committing = false;
        }
    }

    private void ClearInputText()
    {
        _suspendInputEvents = true;
        try
        {
            _inputBox.Text = string.Empty;
        }
        finally
        {
            _suspendInputEvents = false;
        }
    }

    #endregion
}
