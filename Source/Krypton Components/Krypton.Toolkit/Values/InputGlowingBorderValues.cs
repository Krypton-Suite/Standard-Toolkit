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
/// Storage for optional input control glowing border settings.
/// </summary>
[TypeConverter(typeof(InputGlowingBorderValuesConverter))]
public class InputGlowingBorderValues : Storage
{
    #region Instance Fields

    private bool _enable;
    private bool _animate = true;
    private InputGlowingBorderShowWhen _showWhen = InputGlowingBorderShowWhen.Focused;
    private InputGlowingBorderStyle _style = InputGlowingBorderStyle.Bottom;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the InputGlowingBorderValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public InputGlowingBorderValues(NeedPaintHandler? needPaint)
    {
        NeedPaint = needPaint;
        Colors = new InputGlowingBorderColorValues(needPaint);
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault => !ShouldSerializeEnable()
                                      && !ShouldSerializeAnimate()
                                      && !ShouldSerializeShowWhen()
                                      && !ShouldSerializeStyle()
                                      && Colors.IsDefault;

    #endregion

    #region Enable

    /// <summary>
    /// Gets and sets whether the glowing bottom border is drawn on the control.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Gets and sets whether the glowing border is drawn on the control.")]
    [DefaultValue(false)]
    public bool Enable
    {
        get => _enable;

        set
        {
            if (_enable != value)
            {
                _enable = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeEnable() => _enable;

    /// <summary>
    /// Resets the Enable property to its default value.
    /// </summary>
    public void ResetEnable() => Enable = false;

    #endregion

    #region Animate

    /// <summary>
    /// Gets and sets whether the glowing border animates while visible.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Gets and sets whether the glowing border animates while visible.")]
    [DefaultValue(true)]
    public bool Animate
    {
        get => _animate;

        set
        {
            if (_animate != value)
            {
                _animate = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeAnimate() => !_animate;

    /// <summary>
    /// Resets the Animate property to its default value.
    /// </summary>
    public void ResetAnimate() => Animate = true;

    #endregion

    #region ShowWhen

    /// <summary>
    /// Gets and sets when the glowing border is shown.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Gets and sets when the glowing border is shown.")]
    [DefaultValue(InputGlowingBorderShowWhen.Focused)]
    public InputGlowingBorderShowWhen ShowWhen
    {
        get => _showWhen;

        set
        {
            if (_showWhen != value)
            {
                _showWhen = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeShowWhen() => _showWhen != InputGlowingBorderShowWhen.Focused;

    /// <summary>
    /// Resets the ShowWhen property to its default value.
    /// </summary>
    public void ResetShowWhen() => ShowWhen = InputGlowingBorderShowWhen.Focused;

    #endregion

    #region Style

    /// <summary>
    /// Gets and sets whether the glow follows the bottom edge only or the entire border.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Gets and sets whether the glow follows the bottom edge only or the entire border.")]
    [DefaultValue(InputGlowingBorderStyle.Bottom)]
    public InputGlowingBorderStyle Style
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeStyle() => _style != InputGlowingBorderStyle.Bottom;

    /// <summary>
    /// Resets the Style property to its default value.
    /// </summary>
    public void ResetStyle() => Style = InputGlowingBorderStyle.Bottom;

    #endregion

    #region Colors

    /// <summary>
    /// Gets access to the glowing border color values.
    /// </summary>
    [Category(@"Glowing Border")]
    [Description(@"Colors used to render the glowing border.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputGlowingBorderColorValues Colors { get; }

    private bool ShouldSerializeColors() => !Colors.IsDefault;

    #endregion
}
