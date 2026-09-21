#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Configuration values for <see cref="InternalKryptonLoadingCircle"/>.
/// </summary>
internal class InternalLoadingCircleValues : Storage
{
    #region Constants

    private const int DefaultInnerCircleRadius = 8;
    private const int DefaultOuterCircleRadius = 10;
    private const int DefaultNumberOfSpoke = 10;
    private const int DefaultSpokeThickness = 4;

    #endregion

    #region Instance Fields

    private readonly InternalKryptonLoadingCircle _owner;
    private Color _color = Color.Empty;
    private int _outerCircleRadius;
    private int _innerCircleRadius;
    private int _numberSpoke;
    private int _spokeThickness;
    private bool _active;
    private InternalLoadingCircleStylePresets _stylePreset = InternalLoadingCircleStylePresets.Custom;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="InternalLoadingCircleValues"/> class.
    /// </summary>
    /// <param name="owner">Owning spinner.</param>
    public InternalLoadingCircleValues(InternalKryptonLoadingCircle owner) =>
        _owner = owner ?? ThrowHelper.ThrowArgumentNullException(owner);

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _color.IsEmpty &&
        _outerCircleRadius is 0 or DefaultOuterCircleRadius &&
        _innerCircleRadius is 0 or DefaultInnerCircleRadius &&
        _numberSpoke is 0 or DefaultNumberOfSpoke &&
        _spokeThickness is <= 0 or DefaultSpokeThickness &&
        !_active &&
        _stylePreset == InternalLoadingCircleStylePresets.Custom;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the spoke colour. <see cref="Color.Empty"/> uses the active palette.
    /// </summary>
    [DefaultValue(typeof(Color), "Empty")]
    public Color Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                _color = value;
                _owner.GenerateColoursPallet();
                _owner.Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the outer circle radius.
    /// </summary>
    public int OuterCircleRadius
    {
        get
        {
            if (_outerCircleRadius == 0)
            {
                _outerCircleRadius = DefaultOuterCircleRadius;
            }

            return _outerCircleRadius;
        }
        set
        {
            _outerCircleRadius = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the inner circle radius.
    /// </summary>
    public int InnerCircleRadius
    {
        get
        {
            if (_innerCircleRadius == 0)
            {
                _innerCircleRadius = DefaultInnerCircleRadius;
            }

            return _innerCircleRadius;
        }
        set
        {
            _innerCircleRadius = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the number of spokes.
    /// </summary>
    public int NumberSpoke
    {
        get
        {
            if (_numberSpoke == 0)
            {
                _numberSpoke = DefaultNumberOfSpoke;
            }

            return _numberSpoke;
        }
        set
        {
            if (_numberSpoke != value && value > 0)
            {
                _numberSpoke = value;
                _owner.GenerateColoursPallet();
                _owner.GetSpokesAngles();
                _owner.Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the spinner animation is active.
    /// </summary>
    [DefaultValue(false)]
    public bool Active
    {
        get => _active;
        set
        {
            if (_active != value)
            {
                _active = value;
                _owner.ActiveTimer();
            }
        }
    }

    /// <summary>
    /// Gets or sets the spoke thickness.
    /// </summary>
    public int SpokeThickness
    {
        get
        {
            if (_spokeThickness <= 0)
            {
                _spokeThickness = DefaultSpokeThickness;
            }

            return _spokeThickness;
        }
        set
        {
            _spokeThickness = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the rotation speed. Higher is slower.
    /// </summary>
    public int RotationSpeed
    {
        get => _owner.TimerInterval;
        set
        {
            if (value > 0)
            {
                _owner.TimerInterval = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a geometry style preset.
    /// </summary>
    [DefaultValue(typeof(InternalLoadingCircleStylePresets), "Custom")]
    public InternalLoadingCircleStylePresets StylePreset
    {
        get => _stylePreset;
        set
        {
            _stylePreset = value;
            _owner.ApplyStylePreset(value);
        }
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _color = Color.Empty;
        _outerCircleRadius = 0;
        _innerCircleRadius = 0;
        _numberSpoke = 0;
        _spokeThickness = 0;
        _active = false;
        _stylePreset = InternalLoadingCircleStylePresets.Custom;
        _owner.GenerateColoursPallet();
        _owner.GetSpokesAngles();
        _owner.Invalidate();
    }

    #endregion
}
