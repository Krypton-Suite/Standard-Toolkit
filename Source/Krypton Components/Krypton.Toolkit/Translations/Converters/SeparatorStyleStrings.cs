#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="SeparatorStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SeparatorStyleStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_SEPARATOR_STYLE_LOW_PROFILE = @"Low Profile";
    private const string DEFAULT_SEPARATOR_STYLE_HIGH_PROFILE = @"High Profile";
    private const string DEFAULT_SEPARATOR_STYLE_HIGH_INTERNAL_PROFILE = @"High Internal Profile";
    private const string DEFAULT_SEPARATOR_STYLE_CUSTOM1 = @"Custom 1";
    private const string DEFAULT_SEPARATOR_STYLE_CUSTOM2 = @"Custom 2";
    private const string DEFAULT_SEPARATOR_STYLE_CUSTOM3 = @"Custom 3";

    #endregion

    #region Identity

    public SeparatorStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => LowProfile.Equals(DEFAULT_SEPARATOR_STYLE_LOW_PROFILE) &&
                             HighProfile.Equals(DEFAULT_SEPARATOR_STYLE_HIGH_PROFILE) &&
                             HighInternalProfile.Equals(DEFAULT_SEPARATOR_STYLE_HIGH_INTERNAL_PROFILE) &&
                             Custom1.Equals(DEFAULT_SEPARATOR_STYLE_CUSTOM1) &&
                             Custom2.Equals(DEFAULT_SEPARATOR_STYLE_CUSTOM2) &&
                             Custom3.Equals(DEFAULT_SEPARATOR_STYLE_CUSTOM3);

    public void Reset()
    {
        LowProfile = DEFAULT_SEPARATOR_STYLE_LOW_PROFILE;

        HighProfile = DEFAULT_SEPARATOR_STYLE_HIGH_PROFILE;

        HighInternalProfile = DEFAULT_SEPARATOR_STYLE_HIGH_INTERNAL_PROFILE;

        Custom1 = DEFAULT_SEPARATOR_STYLE_CUSTOM1;

        Custom2 = DEFAULT_SEPARATOR_STYLE_CUSTOM2;

        Custom3 = DEFAULT_SEPARATOR_STYLE_CUSTOM3;
    }

    /// <summary>Gets or sets the low profile separator style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The low profile separator style.")]
    [DefaultValue(DEFAULT_SEPARATOR_STYLE_LOW_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string LowProfile { get; set; }

    /// <summary>Gets or sets the high profile separator style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The high profile separator style.")]
    [DefaultValue(DEFAULT_SEPARATOR_STYLE_HIGH_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string HighProfile { get; set; }

    /// <summary>Gets or sets the high internal profile separator style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The high internal profile separator style.")]
    [DefaultValue(DEFAULT_SEPARATOR_STYLE_HIGH_INTERNAL_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string HighInternalProfile { get; set; }

    /// <summary>Gets or sets the custom 1 separator style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 seperator style.")]
    [DefaultValue(DEFAULT_SEPARATOR_STYLE_CUSTOM1)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom1 { get; set; }

    /// <summary>Gets or sets the custom 2 separator style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 seperator style.")]
    [DefaultValue(DEFAULT_SEPARATOR_STYLE_CUSTOM2)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom2 { get; set; }

    /// <summary>Gets or sets the custom 3 separator style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 seperator style.")]
    [DefaultValue(DEFAULT_SEPARATOR_STYLE_CUSTOM3)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom3 { get; set; }

    #endregion
}