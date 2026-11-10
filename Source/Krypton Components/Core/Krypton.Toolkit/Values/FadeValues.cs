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
/// Designer-serializable fade in/out settings for a <see cref="VisualForm"/> (including <see cref="KryptonForm"/>).
/// Disabled by default so existing forms are unchanged.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public class FadeValues : Storage
{
    #region Static Fields
    private const bool DEFAULT_FADING_ENABLED = false;
    private const bool DEFAULT_FADE_IN = true;
    private const bool DEFAULT_FADE_OUT = true;
    private const FadeSpeedChoice DEFAULT_FADE_SPEED = FadeSpeedChoice.Normal;
    private const float DEFAULT_CUSTOM_FADE_SPEED = KryptonFormFadeSpeed.DEFAULT_NORMAL;
    #endregion

    #region Instance Fields
    private bool _fadingEnabled;
    private bool _fadeIn;
    private bool _fadeOut;
    private FadeSpeedChoice _fadeSpeed;
    private float _customFadeSpeed;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="FadeValues"/> class.
    /// </summary>
    public FadeValues() => Reset();

    /// <summary>
    /// Resets all fade values to their defaults.
    /// </summary>
    public void Reset()
    {
        ResetFadingEnabled();
        ResetFadeIn();
        ResetFadeOut();
        ResetFadeSpeed();
        ResetCustomFadeSpeed();
    }
    #endregion

    #region FadingEnabled
    /// <summary>
    /// Gets or sets whether the form automatically fades in when shown and out when closed.
    /// </summary>
    /// <remarks>
    /// Default is <c>false</c>. When enabled, <see cref="FadeIn"/> and <see cref="FadeOut"/> control each direction.
    /// Manual <see cref="VisualForm.FadeIn()"/> / <see cref="VisualForm.FadeOut()"/> still work when this is <c>false</c>.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Automatically fade the form in on show and out on close.")]
    [DefaultValue(DEFAULT_FADING_ENABLED)]
    public bool FadingEnabled
    {
        get => _fadingEnabled;
        set => _fadingEnabled = value;
    }

    private bool ShouldSerializeFadingEnabled() => FadingEnabled != DEFAULT_FADING_ENABLED;

    /// <summary>
    /// Resets <see cref="FadingEnabled"/> to its default.
    /// </summary>
    public void ResetFadingEnabled() => FadingEnabled = DEFAULT_FADING_ENABLED;
    #endregion

    #region FadeIn
    /// <summary>
    /// Gets or sets whether a fade-in runs when the form is first shown.
    /// </summary>
    /// <remarks>
    /// Used only when <see cref="FadingEnabled"/> is <c>true</c>. Default is <c>true</c>.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Fade in from transparent when the form is shown.")]
    [DefaultValue(DEFAULT_FADE_IN)]
    public bool FadeIn
    {
        get => _fadeIn;
        set => _fadeIn = value;
    }

    private bool ShouldSerializeFadeIn() => FadeIn != DEFAULT_FADE_IN;

    /// <summary>
    /// Resets <see cref="FadeIn"/> to its default.
    /// </summary>
    public void ResetFadeIn() => FadeIn = DEFAULT_FADE_IN;
    #endregion

    #region FadeOut
    /// <summary>
    /// Gets or sets whether a fade-out runs when the form is closing.
    /// </summary>
    /// <remarks>
    /// Used only when <see cref="FadingEnabled"/> is <c>true</c>. Default is <c>true</c>.
    /// Shutdown, task-manager, and application-exit close reasons skip the fade so the process can exit promptly.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Fade out to transparent when the form is closing.")]
    [DefaultValue(DEFAULT_FADE_OUT)]
    public bool FadeOut
    {
        get => _fadeOut;
        set => _fadeOut = value;
    }

    private bool ShouldSerializeFadeOut() => FadeOut != DEFAULT_FADE_OUT;

    /// <summary>
    /// Resets <see cref="FadeOut"/> to its default.
    /// </summary>
    public void ResetFadeOut() => FadeOut = DEFAULT_FADE_OUT;
    #endregion

    #region FadeSpeed
    /// <summary>
    /// Gets or sets the fade speed preset.
    /// </summary>
    /// <remarks>
    /// Presets map to built-in fade-speed units (opacity step scaled by 1000 on a 10 ms timer).
    /// Use <see cref="FadeSpeedChoice.Custom"/> with <see cref="CustomFadeSpeed"/> to supply your own units (typically 1–100).
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Preset fade speed. Use Custom with CustomFadeSpeed for a specific step.")]
    [DefaultValue(DEFAULT_FADE_SPEED)]
    public FadeSpeedChoice FadeSpeed
    {
        get => _fadeSpeed;
        set => _fadeSpeed = value;
    }

    private bool ShouldSerializeFadeSpeed() => FadeSpeed != DEFAULT_FADE_SPEED;

    /// <summary>
    /// Resets <see cref="FadeSpeed"/> to its default.
    /// </summary>
    public void ResetFadeSpeed() => FadeSpeed = DEFAULT_FADE_SPEED;
    #endregion

    #region CustomFadeSpeed
    /// <summary>
    /// Gets or sets the custom fade-speed units used when <see cref="FadeSpeed"/> is <see cref="FadeSpeedChoice.Custom"/>.
    /// </summary>
    /// <remarks>
    /// Same units as the built-in fade-speed presets. Values of <c>1</c> (slowest) through <c>100</c> (fastest) are typical.
    /// Values of <c>0</c> or less fall back to <see cref="FadeSpeedChoice.Normal"/>.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Custom fade-speed units when FadeSpeed is Custom. Typical range is 1 (slow) to 100 (fast).")]
    [DefaultValue(DEFAULT_CUSTOM_FADE_SPEED)]
    public float CustomFadeSpeed
    {
        get => _customFadeSpeed;
        set => _customFadeSpeed = value;
    }

    private bool ShouldSerializeCustomFadeSpeed() => Math.Abs(CustomFadeSpeed - DEFAULT_CUSTOM_FADE_SPEED) > 0.001f;

    /// <summary>
    /// Resets <see cref="CustomFadeSpeed"/> to its default.
    /// </summary>
    public void ResetCustomFadeSpeed() => CustomFadeSpeed = DEFAULT_CUSTOM_FADE_SPEED;
    #endregion

    #region Default Values
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeFadingEnabled()
                                      && !ShouldSerializeFadeIn()
                                      && !ShouldSerializeFadeOut()
                                      && !ShouldSerializeFadeSpeed()
                                      && !ShouldSerializeCustomFadeSpeed();
    #endregion
}
