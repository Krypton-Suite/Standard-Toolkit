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
/// Storage for input control pulsing border colors.
/// </summary>
/// <remarks>
/// Control-owned instances inherit unset colors from
/// <see cref="KryptonManager.PulsingBorderValues"/>.Colors. Assigning a color stores a local
/// override; <c>Reset*</c> (and <see cref="Reset"/>) clears the override so the global value is
/// used again. The manager instance does not inherit.
/// </remarks>
[TypeConverter(typeof(InputPulsingBorderColorValuesConverter))]
public class InputPulsingBorderColorValues : Storage
{
    #region Static Fields

    internal static readonly Color DefaultColor1 = Color.FromArgb(64, 132, 255);
    internal static readonly Color DefaultColor2 = Color.FromArgb(120, 220, 255);
    internal static readonly Color DefaultHighlightColor = Color.FromArgb(240, 248, 255);

    #endregion

    #region Instance Fields

    private readonly bool _inheritFromGlobal;
    private Color? _color1;
    private Color? _color2;
    private Color? _highlightColor;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="InputPulsingBorderColorValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public InputPulsingBorderColorValues(NeedPaintHandler? needPaint)
        : this(needPaint, inheritFromGlobal: true)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="InputPulsingBorderColorValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="inheritFromGlobal">
    /// When <see langword="true"/>, unset colors read from
    /// <see cref="KryptonManager.PulsingBorderValues"/>.Colors. Pass <see langword="false"/> for
    /// the manager's own global instance.
    /// </param>
    internal InputPulsingBorderColorValues(NeedPaintHandler? needPaint, bool inheritFromGlobal)
    {
        NeedPaint = needPaint;
        _inheritFromGlobal = inheritFromGlobal;

        if (!inheritFromGlobal)
        {
            Reset();
        }
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault => !ShouldSerializeColor1()
                                      && !ShouldSerializeColor2()
                                      && !ShouldSerializeHighlightColor();

    #endregion

    #region Color1

    /// <summary>
    /// Gets and sets the first edge color used for the pulsing border gradient.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"The first edge color used for the pulsing border gradient.")]
    [TypeConverter(typeof(ColorConverter))]
    public Color Color1
    {
        get => GetInherited(_color1, colors => colors.Color1, DefaultColor1);

        set => SetLocal(ref _color1, value);
    }

    private bool ShouldSerializeColor1() => ShouldSerializeLocal(_color1, Color1, DefaultColor1);

    /// <summary>
    /// Resets the Color1 property to its default value.
    /// </summary>
    public void ResetColor1() => ResetLocal(ref _color1, DefaultColor1);

    #endregion

    #region Color2

    /// <summary>
    /// Gets and sets the second edge color used for the pulsing border gradient.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"The second edge color used for the pulsing border gradient.")]
    [TypeConverter(typeof(ColorConverter))]
    public Color Color2
    {
        get => GetInherited(_color2, colors => colors.Color2, DefaultColor2);

        set => SetLocal(ref _color2, value);
    }

    private bool ShouldSerializeColor2() => ShouldSerializeLocal(_color2, Color2, DefaultColor2);

    /// <summary>
    /// Resets the Color2 property to its default value.
    /// </summary>
    public void ResetColor2() => ResetLocal(ref _color2, DefaultColor2);

    #endregion

    #region HighlightColor

    /// <summary>
    /// Gets and sets the highlight color used at the center of the pulsing border.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"The highlight color used at the center of the pulsing border.")]
    [TypeConverter(typeof(ColorConverter))]
    public Color HighlightColor
    {
        get => GetInherited(_highlightColor, colors => colors.HighlightColor, DefaultHighlightColor);

        set => SetLocal(ref _highlightColor, value);
    }

    private bool ShouldSerializeHighlightColor() =>
        ShouldSerializeLocal(_highlightColor, HighlightColor, DefaultHighlightColor);

    /// <summary>
    /// Resets the HighlightColor property to its default value.
    /// </summary>
    public void ResetHighlightColor() => ResetLocal(ref _highlightColor, DefaultHighlightColor);

    #endregion

    #region Reset

    /// <summary>
    /// Resets all colors. Control instances clear local overrides and inherit from
    /// <see cref="KryptonManager.PulsingBorderValues"/> again; the global instance restores factory defaults.
    /// </summary>
    public void Reset()
    {
        ResetColor1();
        ResetColor2();
        ResetHighlightColor();
    }

    #endregion

    #region Implementation

    private InputPulsingBorderColorValues? InheritSource =>
        _inheritFromGlobal && !ReferenceEquals(this, KryptonManager.PulsingBorderValues.Colors)
            ? KryptonManager.PulsingBorderValues.Colors
            : null;

    private Color GetInherited(Color? local, Func<InputPulsingBorderColorValues, Color> read, Color factory)
    {
        if (local.HasValue)
        {
            return local.Value;
        }

        InputPulsingBorderColorValues? source = InheritSource;
        return source != null ? read(source) : factory;
    }

    private bool ShouldSerializeLocal(Color? local, Color effective, Color factory) =>
        _inheritFromGlobal ? local.HasValue : effective != factory;

    private void SetLocal(ref Color? field, Color value)
    {
        if (!field.HasValue || field.Value != value)
        {
            field = value;
            PerformNeedPaint(true);
        }
    }

    private void ResetLocal(ref Color? field, Color factory)
    {
        if (_inheritFromGlobal)
        {
            if (field.HasValue)
            {
                field = null;
                PerformNeedPaint(true);
            }

            return;
        }

        SetLocal(ref field, factory);
    }

    #endregion
}
