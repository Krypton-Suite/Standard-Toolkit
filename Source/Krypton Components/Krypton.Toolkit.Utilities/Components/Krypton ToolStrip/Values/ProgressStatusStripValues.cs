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
/// Expandable progress-bar configuration for <see cref="KryptonProgressStatusStrip"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ProgressStatusStripValues : Storage
{
    #region Constants

    // NOTE: The legacy control left these numeric fields unset (all zero at runtime) even though the
    // property [DefaultValue] hints below advertised 100/0; that mismatch is preserved here so behavior
    // is unchanged. IsDefault/Reset track the real (zero) runtime defaults, not the designer hints.
    private const float DEFAULT_CURRENT_VALUE = 0.0f;
    private const float DEFAULT_MAXIMUM_VALUE = 0.0f;
    private const float DEFAULT_MINIMUM_VALUE = 0.0f;
    private static readonly Color DEFAULT_BAR_COLOUR = Color.Empty;
    private static readonly Color DEFAULT_BAR_SHADE = Color.Empty;

    #endregion

    #region Instance Fields

    private readonly KryptonProgressStatusStrip _owner;
    private bool _useAsProgressBar;
    private Color _barColour = DEFAULT_BAR_COLOUR;
    private Color _barShade = DEFAULT_BAR_SHADE;
    private float _currentValue = DEFAULT_CURRENT_VALUE;
    private float _minimumValue = DEFAULT_MINIMUM_VALUE;
    private float _maximumValue = DEFAULT_MAXIMUM_VALUE;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ProgressStatusStripValues"/> class.
    /// </summary>
    /// <param name="owner">Owning progress status strip.</param>
    public ProgressStatusStripValues(KryptonProgressStatusStrip owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        !_useAsProgressBar &&
        _barColour == DEFAULT_BAR_COLOUR &&
        _barShade == DEFAULT_BAR_SHADE &&
        _currentValue == DEFAULT_CURRENT_VALUE &&
        _minimumValue == DEFAULT_MINIMUM_VALUE &&
        _maximumValue == DEFAULT_MAXIMUM_VALUE;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether to use the status strip as a progress bar.
    /// </summary>
    [Category(@"Progress Bar")]
    [DefaultValue(false)]
    [Description(@"Use the status strip as a progress bar.")]
    public bool UseAsProgressBar
    {
        get => _useAsProgressBar;
        set { _useAsProgressBar = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Gets or sets the colour of the progress bar.
    /// </summary>
    [Category(@"Progress Bar")]
    [DefaultValue(typeof(Color), "")]
    [Description(@"The colour of the progress bar.")]
    public Color BarColour
    {
        get => _barColour;
        set { _barColour = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Gets or sets the shade colour of the progress bar.
    /// </summary>
    [Category(@"Progress Bar")]
    [DefaultValue(typeof(Color), "")]
    [Description(@"The shade colour of the progress bar.")]
    public Color BarShade
    {
        get => _barShade;
        set { _barShade = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Gets or sets the current value for the progress bar, in the range specified by <see cref="MinimumValue"/> and <see cref="MaximumValue"/>.
    /// </summary>
    [Category(@"Progress Bar")]
    [DefaultValue(DEFAULT_CURRENT_VALUE)]
    [Description(@"The current value for the progress bar, in the range specified by the minimum and maximum values.")]
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            _currentValue = value;

            if (_currentValue < _minimumValue)
            {
                _currentValue = _minimumValue;
            }

            if (_currentValue > _maximumValue)
            {
                _currentValue = _maximumValue;
            }

            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the upper bound of the range the progress bar is working with.
    /// </summary>
    [Category(@"Progress Bar")]
    [DefaultValue(DEFAULT_MAXIMUM_VALUE)]
    [Description(@"The upper bound of the range the progress bar is working with. The legacy default is zero; set both Minimum and Maximum explicitly before enabling UseAsProgressBar.")]
    public float MaximumValue
    {
        get => _maximumValue;
        set
        {
            _maximumValue = value;

            if (_maximumValue < _minimumValue)
            {
                _minimumValue = _maximumValue;
            }

            if (_maximumValue < _currentValue)
            {
                _currentValue = _maximumValue;
            }

            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the lower bound of the range the progress bar is working with.
    /// </summary>
    [Category(@"Progress Bar")]
    [DefaultValue(DEFAULT_MINIMUM_VALUE)]
    [Description(@"The lower bound of the range the progress bar is working with.")]
    public float MinimumValue
    {
        get => _minimumValue;
        set
        {
            _minimumValue = value;

            if (_minimumValue > _maximumValue)
            {
                _maximumValue = _minimumValue;
            }

            if (_minimumValue > _currentValue)
            {
                _currentValue = _minimumValue;
            }

            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _useAsProgressBar = false;
        _barColour = DEFAULT_BAR_COLOUR;
        _barShade = DEFAULT_BAR_SHADE;
        _currentValue = DEFAULT_CURRENT_VALUE;
        _minimumValue = DEFAULT_MINIMUM_VALUE;
        _maximumValue = DEFAULT_MAXIMUM_VALUE;
        _owner.Invalidate();
    }

    #endregion
}
