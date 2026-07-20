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
[TypeConverter(typeof(InputPulsingBorderColorValuesConverter))]
public class InputPulsingBorderColorValues : Storage
{
    #region Static Fields

    internal static readonly Color DefaultColor1 = Color.FromArgb(64, 132, 255);
    internal static readonly Color DefaultColor2 = Color.FromArgb(120, 220, 255);
    internal static readonly Color DefaultHighlightColor = Color.FromArgb(240, 248, 255);

    #endregion

    #region Instance Fields

    private Color _color1 = DefaultColor1;
    private Color _color2 = DefaultColor2;
    private Color _highlightColor = DefaultHighlightColor;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the InputGlowingBorderColorValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public InputPulsingBorderColorValues(NeedPaintHandler? needPaint) 
    {
        NeedPaint = needPaint;

        Reset();
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
        get => _color1;

        set
        {
            if (_color1 != value)
            {
                _color1 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeColor1() => _color1 != DefaultColor1;

    /// <summary>
    /// Resets the Color1 property to its default value.
    /// </summary>
    public void ResetColor1() => Color1 = DefaultColor1;

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
        get => _color2;

        set
        {
            if (_color2 != value)
            {
                _color2 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeColor2() => _color2 != DefaultColor2;

    /// <summary>
    /// Resets the Color2 property to its default value.
    /// </summary>
    public void ResetColor2() => Color2 = DefaultColor2;

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
        get => _highlightColor;

        set
        {
            if (_highlightColor != value)
            {
                _highlightColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighlightColor() => _highlightColor != DefaultHighlightColor;

    /// <summary>
    /// Resets the HighlightColor property to its default value.
    /// </summary>
    public void ResetHighlightColor() => HighlightColor = DefaultHighlightColor;

    #endregion

    #region Reset

    public void Reset()
    {
        ResetColor1();
        ResetColor2();
        ResetHighlightColor();
    }

    #endregion
}
