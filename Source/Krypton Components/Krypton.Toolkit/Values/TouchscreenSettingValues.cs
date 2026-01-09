#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Access 'Global' Krypton touchscreen support settings.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TouchscreenSettingValues : GlobalId
{
    #region Identity

    /// <summary>Initializes a new instance of the <see cref="TouchscreenSettingValues"/> class.</summary>
    public TouchscreenSettingValues()
    {
        
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether touchscreen support should be automatically detected and enabled.
    /// When set to true, the system will automatically check for touchscreen capability and enable/disable touchscreen support accordingly.
    /// </summary>
    [Category(@"Detection")]
    [Description(@"When enabled, automatically detects touchscreen capability and enables/disables touchscreen support accordingly.")]
    [DefaultValue(false)]
    public bool AutomaticallyDetectTouchscreen
    {
        get => KryptonManager.AutomaticallyDetectTouchscreen;
        set => KryptonManager.AutomaticallyDetectTouchscreen = value;
    }
    private bool ShouldSerializeAutomaticallyDetectTouchscreen() => AutomaticallyDetectTouchscreen;
    private void ResetAutomaticallyDetectTouchscreen() => AutomaticallyDetectTouchscreen = false;

    /// <summary>
    /// Gets or sets a value indicating if touchscreen support is enabled, making controls larger based on the scale factor.
    /// Note: If AutomaticallyDetectTouchscreen is true, this property will be automatically updated based on detection.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should touchscreen support be enabled, making controls larger for easier touch interaction. Note: If AutomaticallyDetectTouchscreen is true, this property will be automatically updated based on detection.")]
    [DefaultValue(false)]
    public bool TouchscreenModeEnabled
    {
        get => KryptonManager.UseTouchscreenSupport;
        set => KryptonManager.UseTouchscreenSupport = value;
    }
    private bool ShouldSerializeTouchscreenModeEnabled() => TouchscreenModeEnabled;
    private void ResetTouchscreenModeEnabled() => TouchscreenModeEnabled = false;

    /// <summary>
    /// Gets or sets the scale factor applied to controls when touchscreen support is enabled.
    /// </summary>
    /// <remarks>
    /// A value of 1.25 means controls will be 25% larger. Must be greater than 0.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"The scale factor applied to controls when touchscreen support is enabled. Default is 1.25 (25% larger).")]
    [DefaultValue(1.25f)]
    public float ControlScaleFactor
    {
        get => KryptonManager.TouchscreenScaleFactorValue;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, @"Scale factor must be greater than 0.");
            }
            KryptonManager.TouchscreenScaleFactorValue = value;
        }
    }
    private bool ShouldSerializeControlScaleFactor() => Math.Abs(ControlScaleFactor - 1.25f) > 0.001f;
    private void ResetControlScaleFactor() => ControlScaleFactor = 1.25f;

    /// <summary>
    /// Gets or sets a value indicating if font scaling is enabled when touchscreen support is enabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should fonts be scaled when touchscreen support is enabled. When enabled, fonts will be scaled by the FontScaleFactor.")]
    [DefaultValue(true)]
    public bool FontScalingEnabled
    {
        get => KryptonManager.UseTouchscreenFontScaling;
        set => KryptonManager.UseTouchscreenFontScaling = value;
    }
    private bool ShouldSerializeFontScalingEnabled() => !FontScalingEnabled;
    private void ResetFontScalingEnabled() => FontScalingEnabled = true;

    /// <summary>
    /// Gets or sets the scale factor applied to fonts when font scaling is enabled.
    /// </summary>
    /// <remarks>
    /// A value of 1.25 means fonts will be 25% larger. Must be greater than 0.
    /// By default, this uses the same value as ControlScaleFactor, but can be set independently.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"The scale factor applied to fonts when font scaling is enabled. Default is 1.25 (25% larger).")]
    [DefaultValue(1.25f)]
    public float FontScaleFactor
    {
        get => KryptonManager.TouchscreenFontScaleFactorValue;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, @"Font scale factor must be greater than 0.");
            }
            KryptonManager.TouchscreenFontScaleFactorValue = value;
        }
    }
    private bool ShouldSerializeFontScaleFactor() => Math.Abs(FontScaleFactor - 1.25f) > 0.001f;
    private void ResetFontScaleFactor() => FontScaleFactor = 1.25f;

    /// <summary>
    /// Gets the maximum number of simultaneous touch contacts supported by the system.
    /// Returns 0 if no touchscreen is available or the API is not supported.
    /// </summary>
    [Category(@"Detection")]
    [Description(@"The maximum number of simultaneous touch contacts supported by the system. Returns 0 if not available.")]
    [Browsable(true)]
    [ReadOnly(true)]
    public int MaximumTouchContacts => KryptonManager.GetMaximumTouchContacts();

    /// <summary>
    /// Gets or sets the interval (in milliseconds) for periodic touchscreen detection polling.
    /// This is used when AutomaticallyDetectTouchscreen is enabled to detect hot-plug scenarios.
    /// Default is 2000ms (2 seconds). Minimum value is 500ms.
    /// </summary>
    [Category(@"Detection")]
    [Description(@"The interval in milliseconds for periodic touchscreen detection polling. Used for hot-plug detection. Minimum is 500ms.")]
    [DefaultValue(2000)]
    public int DetectionInterval
    {
        get => KryptonManager.TouchscreenDetectionInterval;
        set => KryptonManager.TouchscreenDetectionInterval = value;
    }
    private bool ShouldSerializeDetectionInterval() => DetectionInterval != 2000;
    private void ResetDetectionInterval() => DetectionInterval = 2000;

    #endregion

    #region Overrides

    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => !(ShouldSerializeAutomaticallyDetectTouchscreen() ||
                               ShouldSerializeTouchscreenModeEnabled() ||
                               ShouldSerializeControlScaleFactor() ||
                               ShouldSerializeFontScalingEnabled() ||
                               ShouldSerializeFontScaleFactor() ||
                               ShouldSerializeDetectionInterval());

    #endregion

    #region Reset

    public void Reset()
    {
        ResetAutomaticallyDetectTouchscreen();
        ResetTouchscreenModeEnabled();
        ResetControlScaleFactor();
        ResetFontScalingEnabled();
        ResetFontScaleFactor();
        ResetDetectionInterval();
    }

    #endregion
}