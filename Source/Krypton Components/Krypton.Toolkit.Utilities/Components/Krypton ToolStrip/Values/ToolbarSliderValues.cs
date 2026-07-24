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
/// Expandable configuration for <see cref="KryptonToolbarSlider"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToolbarSliderValues : Storage
{
    #region Constants

    private const int DEFAULT_RANGE = 100;
    private const int DEFAULT_VALUE = 0;
    private const int DEFAULT_STEPS = 2;
    private const int DEFAULT_FIRE_INTERVAL = 200;
    private const bool DEFAULT_SINGLE_CLICK = false;

    #endregion

    #region Instance Fields

    private readonly KryptonToolbarSlider _owner;
    private int _range = DEFAULT_RANGE;
    private int _value = DEFAULT_VALUE;
    private int _steps = DEFAULT_STEPS;
    private int _fireInterval = DEFAULT_FIRE_INTERVAL;
    private bool _singleClick = DEFAULT_SINGLE_CLICK;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ToolbarSliderValues"/> class.
    /// </summary>
    /// <param name="owner">Owning toolbar slider.</param>
    public ToolbarSliderValues(KryptonToolbarSlider owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _range == DEFAULT_RANGE &&
        _value == DEFAULT_VALUE &&
        _steps == DEFAULT_STEPS &&
        _fireInterval == DEFAULT_FIRE_INTERVAL &&
        _singleClick == DEFAULT_SINGLE_CLICK;

    #endregion

    #region Public

    /// <summary>
    /// The current value of the slider.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_VALUE)]
    [Description(@"The current value of the slider.")]
    public int Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                _owner.Invalidate();
            }
        }
    }

    /// <summary>
    /// Determines if the slider buttons are single click or machine gun fire.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_SINGLE_CLICK)]
    [Description(@"Determines if the slider buttons are single click or machine gun fire.")]
    public bool SingleClick
    {
        get => _singleClick;
        set
        {
            _singleClick = value;
            _owner.kplus.SingleClick = value;
            _owner.kminus.SingleClick = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// The interval at which the slider buttons fire events.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_FIRE_INTERVAL)]
    [Description(@"The interval at which the slider buttons fire events.")]
    public int FireInterval
    {
        get => _fireInterval;
        set
        {
            _fireInterval = value;
            _owner.kplus.EventFireRate = value;
            _owner.kminus.EventFireRate = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// The range of the slider.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_RANGE)]
    [Description(@"The range of the slider.")]
    public int Range
    {
        get => _range;
        set
        {
            if (_range != value)
            {
                _range = value;
                _owner.Invalidate();
            }
        }
    }

    /// <summary>
    /// The step size of the slider.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_STEPS)]
    [Description(@"The step size of the slider.")]
    public int Steps
    {
        get => _steps;
        set
        {
            if (_steps != value)
            {
                _steps = value;
                _owner.Invalidate();
            }
        }
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _range = DEFAULT_RANGE;
        _value = DEFAULT_VALUE;
        _steps = DEFAULT_STEPS;
        _fireInterval = DEFAULT_FIRE_INTERVAL;
        _singleClick = DEFAULT_SINGLE_CLICK;
        _owner.kplus.SingleClick = DEFAULT_SINGLE_CLICK;
        _owner.kminus.SingleClick = DEFAULT_SINGLE_CLICK;
        _owner.kplus.EventFireRate = DEFAULT_FIRE_INTERVAL;
        _owner.kminus.EventFireRate = DEFAULT_FIRE_INTERVAL;
        _owner.Invalidate();
    }

    #endregion
}
