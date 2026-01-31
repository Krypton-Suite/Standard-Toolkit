#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class SplashScreenStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_COPYRIGHT = @"Copyright";

    private const string DEFAULT_VERSION = @"Version";

    #endregion

    #region Identity

    public SplashScreenStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Copyright.Equals(DEFAULT_COPYRIGHT) &&
                             Version.Equals(DEFAULT_VERSION);

    public void Reset()
    {
        Copyright = DEFAULT_COPYRIGHT;

        Version = DEFAULT_VERSION;
    }

    /// <summary>Gets or sets the copyright string.</summary>
    [Category(@"Visuals")]
    [Description(@"The copyright string.")]
    [DefaultValue(DEFAULT_COPYRIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Copyright { get; set; }

    /// <summary>Gets or sets the version string.</summary>
    [Category(@"Visuals")]
    [Description(@"The version string.")]
    [DefaultValue(DEFAULT_VERSION)]
    [RefreshProperties(RefreshProperties.All)]
    public string Version { get; set; }

    #endregion
}