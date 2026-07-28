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
/// Expandable spinner configuration for <see cref="KryptonLoadingCircle"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class LoadingCircleValues : Storage
{
    #region Constants

    private const int DEFAULT_INNER_CIRCLE_RADIUS = 8;
    private const int DEFAULT_OUTER_CIRCLE_RADIUS = 10;
    private const int DEFAULT_NUMBER_OF_SPOKE = 10;
    private const int DEFAULT_SPOKE_THICKNESS = 4;
    private static readonly Color DEFAULT_COLOR = Color.DarkGray;

    #endregion

    #region Instance Fields

    private readonly KryptonLoadingCircle _owner;
    private Color _color;
    private int _outerCircleRadius;
    private int _innerCircleRadius;
    private int _numberSpoke;
    private int _spokeThickness;
    private bool _active;
    private StylePresets _stylePreset = StylePresets.Custom;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="LoadingCircleValues"/> class.
    /// </summary>
    /// <param name="owner">Owning loading circle.</param>
    public LoadingCircleValues(KryptonLoadingCircle owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _color = DEFAULT_COLOR;
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _color == DEFAULT_COLOR &&
        _outerCircleRadius is 0 or DEFAULT_OUTER_CIRCLE_RADIUS &&
        _innerCircleRadius is 0 or DEFAULT_INNER_CIRCLE_RADIUS &&
        _numberSpoke is 0 or DEFAULT_NUMBER_OF_SPOKE &&
        _spokeThickness is <= 0 or DEFAULT_SPOKE_THICKNESS &&
        !_active &&
        _stylePreset == StylePresets.Custom;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the colour of the spinning spokes.
    /// </summary>
    [Category(@"LoadingCircle")]
    [Description(@"Sets the color of spoke.")]
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            _owner.GenerateColoursPallet();
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the outer circle radius.
    /// </summary>
    [Category(@"LoadingCircle")]
    [Description(@"Gets or sets the radius of outer circle.")]
    public int OuterCircleRadius
    {
        get
        {
            if (_outerCircleRadius == 0)
            {
                _outerCircleRadius = DEFAULT_OUTER_CIRCLE_RADIUS;
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
    [Category(@"LoadingCircle")]
    [Description(@"Gets or sets the radius of inner circle.")]
    public int InnerCircleRadius
    {
        get
        {
            if (_innerCircleRadius == 0)
            {
                _innerCircleRadius = DEFAULT_INNER_CIRCLE_RADIUS;
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
    [Category(@"LoadingCircle")]
    [Description(@"Gets or sets the number of spoke.")]
    public int NumberSpoke
    {
        get
        {
            if (_numberSpoke == 0)
            {
                _numberSpoke = DEFAULT_NUMBER_OF_SPOKE;
            }

            return _numberSpoke;
        }
        set
        {
            if (_numberSpoke != value && _numberSpoke > 0)
            {
                _numberSpoke = value;
                _owner.GenerateColoursPallet();
                _owner.GetSpokesAngles();
                _owner.Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the spinner is active.
    /// </summary>
    [Category(@"LoadingCircle")]
    [Description(@"Gets or sets the number of spoke.")]
    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            _owner.ActiveTimer();
        }
    }

    /// <summary>
    /// Gets or sets the spoke thickness.
    /// </summary>
    [Category(@"LoadingCircle")]
    [Description(@"Gets or sets the thickness of a spoke.")]
    public int SpokeThickness
    {
        get
        {
            if (_spokeThickness <= 0)
            {
                _spokeThickness = DEFAULT_SPOKE_THICKNESS;
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
    [Category(@"LoadingCircle")]
    [Description(@"Gets or sets the rotation speed. Higher the slower.")]
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
    /// Quickly sets the style to one of the presets, or a custom style if desired.
    /// </summary>
    [Category(@"LoadingCircle")]
    [Description(@"Quickly sets the style to one of these presets, or a custom style if desired")]
    [DefaultValue(typeof(StylePresets), "Custom")]
    public StylePresets StylePreset
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
        _color = DEFAULT_COLOR;
        _outerCircleRadius = 0;
        _innerCircleRadius = 0;
        _numberSpoke = 0;
        _spokeThickness = 0;
        _active = false;
        _stylePreset = StylePresets.Custom;
        _owner.GenerateColoursPallet();
        _owner.Invalidate();
    }

    #endregion
}
