#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for optional pulsing border settings.
/// </summary>
/// <remarks>
/// Control-owned instances inherit unset properties from the matching
/// <see cref="KryptonManager.PulsingBorderValues"/> group. Assigning a property stores a local
/// override; <c>Reset*</c> (and <see cref="Reset"/>) clears the override so the group value is
/// used again. Manager group instances do not inherit.
/// </remarks>
[TypeConverter(typeof(InputPulsingBorderValuesConverter))]
public class InputPulsingBorderValues : Storage
{
    #region Static Fields

    private const bool DefaultEnable = false;
    private const bool DefaultAnimate = true;
    private const float DefaultAnimationSpeed = 1f;

    #endregion

    #region Instance Fields

    private readonly InputPulsingBorderCategory? _inheritCategory;
    private readonly InputPulsingBorderStyle _defaultStyle;
    private readonly InputPulsingBorderShowWhen _defaultShowWhen;
    private bool? _enable;
    private bool? _animate;
    private float? _animationSpeed;
    private InputPulsingBorderShowWhen? _showWhen;
    private InputPulsingBorderStyle? _style;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="InputPulsingBorderValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public InputPulsingBorderValues(NeedPaintHandler? needPaint)
        : this(needPaint, InputPulsingBorderCategory.Inputs)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="InputPulsingBorderValues"/> class that inherits from a manager group.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="inheritCategory">Manager group to inherit unset properties from.</param>
    internal InputPulsingBorderValues(NeedPaintHandler? needPaint, InputPulsingBorderCategory inheritCategory)
        : this(needPaint, inheritCategory, InputPulsingBorderStyle.Bottom, InputPulsingBorderShowWhen.Focused)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="InputPulsingBorderValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="inheritCategory">
    /// Manager group to inherit from, or <see langword="null"/> for a manager group instance.
    /// </param>
    /// <param name="defaultStyle">Factory style used when this instance does not inherit.</param>
    /// <param name="defaultShowWhen">Factory show-when used when this instance does not inherit.</param>
    internal InputPulsingBorderValues(NeedPaintHandler? needPaint,
        InputPulsingBorderCategory? inheritCategory,
        InputPulsingBorderStyle defaultStyle,
        InputPulsingBorderShowWhen defaultShowWhen)
    {
        NeedPaint = needPaint;
        _inheritCategory = inheritCategory;
        _defaultStyle = defaultStyle;
        _defaultShowWhen = defaultShowWhen;
        Colors = new InputPulsingBorderColorValues(needPaint, inheritCategory);

        if (!inheritCategory.HasValue)
        {
            Reset();
        }
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault => !ShouldSerializeEnable()
                                      && !ShouldSerializeAnimate()
                                      && !ShouldSerializeAnimationSpeed()
                                      && !ShouldSerializeShowWhen()
                                      && !ShouldSerializeStyle()
                                      && Colors.IsDefault;

    #endregion

    #region Enable

    /// <summary>
    /// Gets and sets whether the pulsing border is drawn on the control.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Gets and sets whether the pulsing border is drawn on the control. Unset control values inherit from the matching KryptonManager.PulsingBorderValues group.")]
    [DefaultValue(DefaultEnable)]
    public bool Enable
    {
        get => GetInherited(_enable, values => values.Enable, DefaultEnable);

        set => SetLocal(ref _enable, value, needLayout: true);
    }

    private bool ShouldSerializeEnable() => ShouldSerializeLocal(_enable, Enable, DefaultEnable);

    /// <summary>
    /// Resets the Enable property to its default value.
    /// </summary>
    public void ResetEnable() => ResetLocal(ref _enable, DefaultEnable, needLayout: true);

    #endregion

    #region Animate

    /// <summary>
    /// Gets and sets whether the pulsing border animates while visible.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Gets and sets whether the pulsing border animates while visible.")]
    [DefaultValue(DefaultAnimate)]
    public bool Animate
    {
        get => GetInherited(_animate, values => values.Animate, DefaultAnimate);

        set => SetLocal(ref _animate, value, needLayout: true);
    }

    private bool ShouldSerializeAnimate() => ShouldSerializeLocal(_animate, Animate, DefaultAnimate);

    /// <summary>
    /// Resets the Animate property to its default value.
    /// </summary>
    public void ResetAnimate() => ResetLocal(ref _animate, DefaultAnimate, needLayout: true);

    #endregion

    #region AnimationSpeed

    /// <summary>
    /// Gets and sets the pulsing border animation speed multiplier.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Animation speed multiplier. 1 is the default speed; values greater than 1 animate faster and values less than 1 animate slower.")]
    [DefaultValue(DefaultAnimationSpeed)]
    public float AnimationSpeed
    {
        get => GetInherited(_animationSpeed, values => values.AnimationSpeed, DefaultAnimationSpeed);

        set
        {
            float speed = Math.Max(0.1f, Math.Min(10f, value));
            if (!_animationSpeed.HasValue || Math.Abs(_animationSpeed.Value - speed) > float.Epsilon)
            {
                _animationSpeed = speed;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeAnimationSpeed() =>
        ShouldSerializeLocal(_animationSpeed, AnimationSpeed, DefaultAnimationSpeed);

    /// <summary>
    /// Resets the AnimationSpeed property to its default value.
    /// </summary>
    public void ResetAnimationSpeed() => ResetLocal(ref _animationSpeed, DefaultAnimationSpeed, needLayout: false);

    #endregion

    #region ShowWhen

    /// <summary>
    /// Gets and sets when the pulsing border is shown.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Gets and sets when the pulsing border is shown.")]
    [DefaultValue(InputPulsingBorderShowWhen.Focused)]
    public InputPulsingBorderShowWhen ShowWhen
    {
        get => GetInherited(_showWhen, values => values.ShowWhen, _defaultShowWhen);

        set => SetLocal(ref _showWhen, value, needLayout: true);
    }

    private bool ShouldSerializeShowWhen() => ShouldSerializeLocal(_showWhen, ShowWhen, _defaultShowWhen);

    /// <summary>
    /// Resets the ShowWhen property to its default value.
    /// </summary>
    public void ResetShowWhen() => ResetLocal(ref _showWhen, _defaultShowWhen, needLayout: true);

    #endregion

    #region Style

    /// <summary>
    /// Gets and sets whether the glow follows the bottom edge only or the entire border.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Gets and sets whether the glow follows the bottom edge only or the entire border.")]
    [DefaultValue(InputPulsingBorderStyle.Bottom)]
    public InputPulsingBorderStyle Style
    {
        get => GetInherited(_style, values => values.Style, _defaultStyle);

        set => SetLocal(ref _style, value, needLayout: true);
    }

    private bool ShouldSerializeStyle() => ShouldSerializeLocal(_style, Style, _defaultStyle);

    /// <summary>
    /// Resets the Style property to its default value.
    /// </summary>
    public void ResetStyle() => ResetLocal(ref _style, _defaultStyle, needLayout: true);

    #endregion

    #region Colors

    /// <summary>
    /// Gets access to the pulsing border color values.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Colors used to render the pulsing border.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputPulsingBorderColorValues Colors { get; }

    private bool ShouldSerializeColors() => !Colors.IsDefault;

    #endregion

    #region Reset

    /// <summary>
    /// Resets all properties. Control instances clear local overrides and inherit from the
    /// matching <see cref="KryptonManager.PulsingBorderValues"/> group again; a manager group
    /// instance restores its factory defaults.
    /// </summary>
    public void Reset()
    {
        ResetEnable();
        ResetAnimate();
        ResetAnimationSpeed();
        ResetShowWhen();
        ResetStyle();
        Colors.Reset();
    }

    #endregion

    #region Implementation

    private bool InheritFromGlobal => _inheritCategory.HasValue;

    private InputPulsingBorderValues? InheritSource =>
        _inheritCategory.HasValue
            ? KryptonManager.PulsingBorderValues.Get(_inheritCategory.Value)
            : null;

    private T GetInherited<T>(T? local, Func<InputPulsingBorderValues, T> read, T factory)
        where T : struct
    {
        if (local.HasValue)
        {
            return local.Value;
        }

        InputPulsingBorderValues? source = InheritSource;
        return source != null ? read(source) : factory;
    }

    private bool ShouldSerializeLocal<T>(T? local, T effective, T factory)
        where T : struct =>
        InheritFromGlobal ? local.HasValue : !EqualityComparer<T>.Default.Equals(effective, factory);

    private void SetLocal<T>(ref T? field, T value, bool needLayout)
        where T : struct
    {
        if (!field.HasValue || !EqualityComparer<T>.Default.Equals(field.Value, value))
        {
            field = value;
            PerformNeedPaint(needLayout);
        }
    }

    private void ResetLocal<T>(ref T? field, T factory, bool needLayout)
        where T : struct
    {
        if (InheritFromGlobal)
        {
            if (field.HasValue)
            {
                field = null;
                PerformNeedPaint(needLayout);
            }

            return;
        }

        SetLocal(ref field, factory, needLayout);
    }

    #endregion
}
