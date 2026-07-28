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
/// Expandable configuration for <see cref="KryptonSliderButton"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SliderButtonValues : Storage
{
    #region Constants

    private const bool DEFAULT_SINGLE_CLICK = false;
    private const VisualOrientation DEFAULT_ORIENTATION = VisualOrientation.Top;
    private const KryptonSliderButton.ButtonStyles DEFAULT_BUTTON_STYLE = KryptonSliderButton.ButtonStyles.MinusButton;
    private const PaletteBackStyle DEFAULT_VISUAL_LOOK = PaletteBackStyle.ButtonStandalone;
    private const int DEFAULT_EVENT_FIRE_RATE = 1000;

    #endregion

    #region Instance Fields

    private readonly KryptonSliderButton _owner;
    private bool _singleClick = DEFAULT_SINGLE_CLICK;
    private VisualOrientation _orientation = DEFAULT_ORIENTATION;
    private KryptonSliderButton.ButtonStyles _buttonStyle = DEFAULT_BUTTON_STYLE;
    private PaletteBackStyle _visualLook = DEFAULT_VISUAL_LOOK;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="SliderButtonValues"/> class.
    /// </summary>
    /// <param name="owner">Owning slider button.</param>
    public SliderButtonValues(KryptonSliderButton owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _singleClick == DEFAULT_SINGLE_CLICK &&
        _orientation == DEFAULT_ORIENTATION &&
        _buttonStyle == DEFAULT_BUTTON_STYLE &&
        _visualLook == DEFAULT_VISUAL_LOOK &&
        _owner.FireTimer.Interval == DEFAULT_EVENT_FIRE_RATE;

    #endregion

    #region Public

    /// <summary>
    /// Determines if the button is single click or machine gun fire.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_SINGLE_CLICK)]
    [Description(@"Determines if the button is single click or machine gun fire.")]
    public bool SingleClick
    {
        get => _singleClick;
        set { _singleClick = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Determines the orientation of the button.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_ORIENTATION)]
    [Description(@"Determines the orientation of the button.")]
    public VisualOrientation Orientation
    {
        get => _orientation;
        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                _owner.PerformLayout();
                _owner.Invalidate();
            }
        }
    }

    /// <summary>
    /// Determines the style of the button.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_BUTTON_STYLE)]
    [Description(@"Determines the style of the button.")]
    public KryptonSliderButton.ButtonStyles ButtonStyle
    {
        get => _buttonStyle;
        set { _buttonStyle = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Determines the visual look of the button.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_VISUAL_LOOK)]
    [Description(@"Determines the visual look of the button.")]
    public PaletteBackStyle VisualLook
    {
        get => _visualLook;
        set
        {
            if (_visualLook != value)
            {
                _visualLook = value;
                _owner.Invalidate();
            }
        }
    }

    /// <summary>
    /// Determines the rate at which the button fires events.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_EVENT_FIRE_RATE)]
    [Description(@"Determines the rate at which the button fires events.")]
    public int EventFireRate
    {
        get => _owner.FireTimer.Interval;
        set => _owner.SetEventFireRateCore(value);
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _singleClick = DEFAULT_SINGLE_CLICK;
        _orientation = DEFAULT_ORIENTATION;
        _buttonStyle = DEFAULT_BUTTON_STYLE;
        _visualLook = DEFAULT_VISUAL_LOOK;
        _owner.SetEventFireRateCore(DEFAULT_EVENT_FIRE_RATE);
        _owner.Invalidate();
    }

    #endregion
}
