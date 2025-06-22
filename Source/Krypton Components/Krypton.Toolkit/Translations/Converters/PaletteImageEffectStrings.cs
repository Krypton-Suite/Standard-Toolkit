#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="PaletteImageEffectConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteImageEffectStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_PALETTE_IMAGE_EFFECT_INHERIT = @"Inherit";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT = @"Light";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT_LIGHT = @"LightLight";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_NORMAL = @"Normal";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_DISABLED = @"Disabled";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_DARK = @"Dark";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_DARK_DARK = @"DarkDark";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE = @"GrayScale";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_RED = @"GrayScale - Red";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_GREEN = @"GrayScale - Green";
    private const string DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_BLUE = @"GrayScale - Blue";

    #endregion

    #region Identity

    public PaletteImageEffectStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Inherit.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_INHERIT) &&
                             Light.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT) &&
                             LightLight.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT_LIGHT) &&
                             Normal.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_NORMAL) &&
                             Disabled.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_DISABLED) &&
                             Dark.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_DARK) &&
                             DarkDark.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_DARK_DARK) &&
                             GrayScale.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE) &&
                             GrayScaleRed.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_RED) &&
                             GrayScaleGreen.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_GREEN) &&
                             GrayScaleBlue.Equals(DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_BLUE);

    public void Reset()
    {
        Inherit = DEFAULT_PALETTE_IMAGE_EFFECT_INHERIT;

        Light = DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT;

        LightLight = DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT_LIGHT;

        Normal = DEFAULT_PALETTE_IMAGE_EFFECT_NORMAL;

        Disabled = DEFAULT_PALETTE_IMAGE_EFFECT_DISABLED;

        Dark = DEFAULT_PALETTE_IMAGE_EFFECT_DARK;

        DarkDark = DEFAULT_PALETTE_IMAGE_EFFECT_DARK_DARK;

        GrayScale = DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE;

        GrayScaleRed = DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_RED;

        GrayScaleGreen = DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_GREEN;

        GrayScaleBlue = DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_BLUE;
    }

    /// <summary>Gets or sets the inherit palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The inherit palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_INHERIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Inherit { get; set; }

    /// <summary>Gets or sets the light palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The light palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Light { get; set; }

    /// <summary>Gets or sets the light light palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The light light palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT_LIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string LightLight { get; set; }

    /// <summary>Gets or sets the normal palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The normal palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_NORMAL)]
    [RefreshProperties(RefreshProperties.All)]
    public string Normal { get; set; }

    /// <summary>Gets or sets the disabled palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The disabled palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_DISABLED)]
    [RefreshProperties(RefreshProperties.All)]
    public string Disabled { get; set; }

    /// <summary>Gets or sets the dark palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The dark palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_DARK)]
    [RefreshProperties(RefreshProperties.All)]
    public string Dark { get; set; }

    /// <summary>Gets or sets the dark dark palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The dark dark palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_DARK_DARK)]
    [RefreshProperties(RefreshProperties.All)]
    public string DarkDark { get; set; }

    /// <summary>Gets or sets the grayscale palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grayscale palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE)]
    [RefreshProperties(RefreshProperties.All)]
    public string GrayScale { get; set; }

    /// <summary>Gets or sets the grayscale red palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grayscale red palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_RED)]
    [RefreshProperties(RefreshProperties.All)]
    public string GrayScaleRed { get; set; }

    /// <summary>Gets or sets the grayscale green palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grayscale green palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_GREEN)]
    [RefreshProperties(RefreshProperties.All)]
    public string GrayScaleGreen { get; set; }

    /// <summary>Gets or sets the grayscale blue palette image effect style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grayscale blue palette image effect style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_BLUE)]
    [RefreshProperties(RefreshProperties.All)]
    public string GrayScaleBlue { get; set; }

    #endregion
}