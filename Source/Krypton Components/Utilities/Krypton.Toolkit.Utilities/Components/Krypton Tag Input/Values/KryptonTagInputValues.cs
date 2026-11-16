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
/// Behaviour and appearance values for <see cref="KryptonTagInputControl"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonTagInputValues : Storage
{
    #region Static Fields

    private const string DefaultCueHintText = "";
    private const int DefaultInputWidth = 120;
    private const int DefaultMaxTags = 0;
    private const float DefaultChipRounding = 6f;

    #endregion

    #region Instance Fields

    private readonly KryptonTagInputControl _owner;
    private string _cueHintText;
    private int _inputWidth;
    private int _maxTags;
    private float _chipRounding;
    private bool _allowDuplicates;
    private bool _caseSensitive;
    private bool _allowCustomTags;
    private bool _commitOnEnter;
    private bool _commitOnComma;
    private bool _removeLastOnBackspace;
    private bool _showRemoveButton;
    private bool _enableSuggestions;
    private bool _clearOnEscape;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagInputValues"/> class.
    /// </summary>
    /// <param name="owner">Owning tag input control.</param>
    public KryptonTagInputValues(KryptonTagInputControl owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _cueHintText = DefaultCueHintText;
        _inputWidth = DefaultInputWidth;
        _maxTags = DefaultMaxTags;
        _chipRounding = DefaultChipRounding;
        _allowDuplicates = false;
        _caseSensitive = false;
        _allowCustomTags = true;
        _commitOnEnter = true;
        _commitOnComma = true;
        _removeLastOnBackspace = true;
        _showRemoveButton = true;
        _enableSuggestions = true;
        _clearOnEscape = true;
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        CueHintText == DefaultCueHintText &&
        InputWidth == DefaultInputWidth &&
        MaxTags == DefaultMaxTags &&
        Math.Abs(ChipRounding - DefaultChipRounding) < 0.01f &&
        !AllowDuplicates &&
        !CaseSensitive &&
        AllowCustomTags &&
        CommitOnEnter &&
        CommitOnComma &&
        RemoveLastOnBackspace &&
        ShowRemoveButton &&
        EnableSuggestions &&
        ClearOnEscape;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets cue hint text shown in the input when it is empty.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Cue hint text shown in the input when it is empty.")]
    [DefaultValue(DefaultCueHintText)]
    [Localizable(true)]
    public string CueHintText
    {
        get => _cueHintText;
        set
        {
            value ??= string.Empty;
            if (_cueHintText != value)
            {
                _cueHintText = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    private void ResetCueHintText() => CueHintText = DefaultCueHintText;
    private bool ShouldSerializeCueHintText() => CueHintText != DefaultCueHintText;

    /// <summary>
    /// Gets or sets the width of the trailing input editor, in pixels.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Width of the trailing input editor, in pixels.")]
    [DefaultValue(DefaultInputWidth)]
    public int InputWidth
    {
        get => _inputWidth;
        set
        {
            value = Math.Max(40, value);
            if (_inputWidth != value)
            {
                _inputWidth = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of tags. Use 0 for no limit.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum number of tags. Use 0 for no limit.")]
    [DefaultValue(DefaultMaxTags)]
    public int MaxTags
    {
        get => _maxTags;
        set
        {
            value = Math.Max(0, value);
            if (_maxTags != value)
            {
                _maxTags = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the corner rounding applied to each tag chip.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Corner rounding applied to each tag chip.")]
    [DefaultValue(DefaultChipRounding)]
    public float ChipRounding
    {
        get => _chipRounding;
        set
        {
            value = Math.Max(0f, value);
            if (Math.Abs(_chipRounding - value) > 0.01f)
            {
                _chipRounding = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the same tag text may be added more than once.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether the same tag text may be added more than once.")]
    [DefaultValue(false)]
    public bool AllowDuplicates
    {
        get => _allowDuplicates;
        set
        {
            if (_allowDuplicates != value)
            {
                _allowDuplicates = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether tag comparison is case-sensitive.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether tag comparison is case-sensitive.")]
    [DefaultValue(false)]
    public bool CaseSensitive
    {
        get => _caseSensitive;
        set
        {
            if (_caseSensitive != value)
            {
                _caseSensitive = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether tags that are not in the suggestion list may be added.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether tags that are not in the suggestion list may be added.")]
    [DefaultValue(true)]
    public bool AllowCustomTags
    {
        get => _allowCustomTags;
        set
        {
            if (_allowCustomTags != value)
            {
                _allowCustomTags = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Enter commits the current input as a tag.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether Enter commits the current input as a tag.")]
    [DefaultValue(true)]
    public bool CommitOnEnter
    {
        get => _commitOnEnter;
        set
        {
            if (_commitOnEnter != value)
            {
                _commitOnEnter = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether comma commits the current input as a tag.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether comma commits the current input as a tag. Tab is not used, so focus navigation is preserved.")]
    [DefaultValue(true)]
    public bool CommitOnComma
    {
        get => _commitOnComma;
        set
        {
            if (_commitOnComma != value)
            {
                _commitOnComma = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Backspace removes the last tag when the input is empty.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether Backspace removes the last tag when the input is empty.")]
    [DefaultValue(true)]
    public bool RemoveLastOnBackspace
    {
        get => _removeLastOnBackspace;
        set
        {
            if (_removeLastOnBackspace != value)
            {
                _removeLastOnBackspace = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether each chip shows a themed close button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Whether each chip shows a themed close button.")]
    [DefaultValue(true)]
    public bool ShowRemoveButton
    {
        get => _showRemoveButton;
        set
        {
            if (_showRemoveButton != value)
            {
                _showRemoveButton = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the input uses the suggestion list for auto-complete.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether the input uses the suggestion list for auto-complete.")]
    [DefaultValue(true)]
    public bool EnableSuggestions
    {
        get => _enableSuggestions;
        set
        {
            if (_enableSuggestions != value)
            {
                _enableSuggestions = value;
                _owner.OnTagInputValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Escape clears the current input text.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether Escape clears the current input text.")]
    [DefaultValue(true)]
    public bool ClearOnEscape
    {
        get => _clearOnEscape;
        set
        {
            if (_clearOnEscape != value)
            {
                _clearOnEscape = value;
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Restores every value to its factory default.
    /// </summary>
    public void Reset()
    {
        CueHintText = DefaultCueHintText;
        InputWidth = DefaultInputWidth;
        MaxTags = DefaultMaxTags;
        ChipRounding = DefaultChipRounding;
        AllowDuplicates = false;
        CaseSensitive = false;
        AllowCustomTags = true;
        CommitOnEnter = true;
        CommitOnComma = true;
        RemoveLastOnBackspace = true;
        ShowRemoveButton = true;
        EnableSuggestions = true;
        ClearOnEscape = true;
    }

    #endregion
}
