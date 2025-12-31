#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// 
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class AcrylicTrackingValues : Storage
{
    #region Instance Fields
    private bool _enabled = true;
    private float _intensity = 1.0f;
    private Color? _lightColor;
    private Color? _darkColor;
    private AcrylicTrackingQuality _quality = AcrylicTrackingQuality.Balanced;
    private bool _enableAnimation = false;
    private int _animationDuration = 200;
    #endregion

    #region Identity

    public AcrylicTrackingValues()
    {
        Reset();
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => IsDefault ? string.Empty : @"Modified";
   
    #endregion

    #region Enabled
    /// <summary>
    /// Gets or sets whether Acrylic Tracking effects are enabled.
    /// </summary>
    [Category(@"Acrylic Tracking")]
    [Description(@"Enable or disable Acrylic hover effects globally.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;
        set => _enabled = value;
    }

    private bool ShouldSerializeEnabled() => !_enabled;

    /// <summary>
    /// Resets the Enabled property to its default value.
    /// </summary>
    public void ResetEnabled() => _enabled = true;
    #endregion

    #region Intensity
    /// <summary>
    /// Gets or sets the intensity of the Acrylic hover effect (0.0 to 2.0).
    /// </summary>
    [Category(@"Acrylic Tracking")]
    [Description(@"Intensity of the Acrylic hover effect. Range: 0.0 (disabled) to 2.0 (maximum).")]
    [DefaultValue(1.0f)]
    public float Intensity
    {
        get => _intensity;
        set => _intensity = Math.Max(0.0f, Math.Min(2.0f, value));
    }

    private bool ShouldSerializeIntensity() => Math.Abs(_intensity - 1.0f) > 0.001f;

    /// <summary>
    /// Resets the Intensity property to its default value.
    /// </summary>
    public void ResetIntensity() => _intensity = 1.0f;
    #endregion

    #region LightColor
    /// <summary>
    /// Gets or sets a custom light color for the center of the hover effect.
    /// </summary>
    [Category(@"Acrylic Tracking")]
    [Description(@"Custom light color for the center of the hover effect. Null uses automatic calculation.")]
    [DefaultValue(null)]
    public Color? LightColor
    {
        get => _lightColor;
        set => _lightColor = value;
    }

    private bool ShouldSerializeLightColor() => _lightColor.HasValue;

    /// <summary>
    /// Resets the LightColor property to its default value.
    /// </summary>
    public void ResetLightColor() => _lightColor = null;
    #endregion

    #region DarkColor
    /// <summary>
    /// Gets or sets a custom dark color for the edges of the hover effect.
    /// </summary>
    [Category(@"Acrylic Tracking")]
    [Description(@"Custom dark color for the edges of the hover effect. Null uses automatic calculation.")]
    [DefaultValue(null)]
    public Color? DarkColor
    {
        get => _darkColor;
        set => _darkColor = value;
    }

    private bool ShouldSerializeDarkColor() => _darkColor.HasValue;

    /// <summary>
    /// Resets the DarkColor property to its default value.
    /// </summary>
    public void ResetDarkColor() => _darkColor = null;
    #endregion

    #region Quality
    /// <summary>
    /// Gets or sets the quality profile for Acrylic hover effects.
    /// </summary>
    [Category(@"Acrylic Tracking")]
    [Description(@"Quality profile for Acrylic hover effects. Higher quality uses more resources.")]
    [DefaultValue(AcrylicTrackingQuality.Balanced)]
    public AcrylicTrackingQuality TrackingQuality
    {
        get => _quality;
        set => _quality = value;
    }

    private bool ShouldSerializeQuality() => _quality != AcrylicTrackingQuality.Balanced;

    /// <summary>
    /// Resets the Quality property to its default value.
    /// </summary>
    public void ResetQuality() => _quality = AcrylicTrackingQuality.Balanced;
    #endregion

    #region EnableAnimation
    /// <summary>
    /// Gets or sets whether smooth animation transitions are enabled.
    /// </summary>
    [Category(@"Acrylic Tracking")]
    [Description(@"Enable smooth animation transitions when entering/exiting hover state.")]
    [DefaultValue(false)]
    public bool EnableAnimation
    {
        get => _enableAnimation;
        set => _enableAnimation = value;
    }

    private bool ShouldSerializeEnableAnimation() => _enableAnimation;

    /// <summary>
    /// Resets the EnableAnimation property to its default value.
    /// </summary>
    public void ResetEnableAnimation() => _enableAnimation = false;
    #endregion

    #region AnimationDuration
    /// <summary>
    /// Gets or sets the duration of animation transitions in milliseconds.
    /// </summary>
    [Category(@"Acrylic Tracking")]
    [Description(@"Duration of animation transitions in milliseconds. Only used when EnableAnimation is true.")]
    [DefaultValue(200)]
    public int AnimationDuration
    {
        get => _animationDuration;
        set => _animationDuration = Math.Max(0, Math.Min(1000, value));
    }

    private bool ShouldSerializeAnimationDuration() => _animationDuration != 200;

    /// <summary>
    /// Resets the AnimationDuration property to its default value.
    /// </summary>
    public void ResetAnimationDuration() => _animationDuration = 200;
    
    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => _enabled == true &&
                                      Math.Abs(_intensity - 1.0f) < 0.001f &&
                                      !_lightColor.HasValue &&
                                      !_darkColor.HasValue &&
                                      _quality == AcrylicTrackingQuality.Balanced &&
                                      !_enableAnimation && _animationDuration == 200;


    /// <summary>
    /// Resets all settings to their default values.
    /// </summary>
    public void Reset()
    {
        ResetEnabled();
        ResetIntensity();
        ResetLightColor();
        ResetDarkColor();
        ResetQuality();
        ResetEnableAnimation();
        ResetAnimationDuration();
    }
    
    #endregion
}