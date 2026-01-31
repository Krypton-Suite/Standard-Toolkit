#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="KryptonLinkBehaviorConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonLinkBehaviorStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_LINK_BEHAVIOR_ALWAYS_UNDERLINE = @"Always Underline";
    private const string DEFAULT_LINK_BEHAVIOR_HOVER_UNDERLINE = @"Hover Underline";
    private const string DEFAULT_LINK_BEHAVIOR_NEVER_UNDERLINE = @"Never Underline";

    #endregion

    #region Identity

    public KryptonLinkBehaviorStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => AlwaysUnderline.Equals(DEFAULT_LINK_BEHAVIOR_ALWAYS_UNDERLINE) &&
                             HoverUnderline.Equals(DEFAULT_LINK_BEHAVIOR_HOVER_UNDERLINE) &&
                             NeverUnderline.Equals(DEFAULT_LINK_BEHAVIOR_NEVER_UNDERLINE);

    public void Reset()
    {
        AlwaysUnderline = DEFAULT_LINK_BEHAVIOR_ALWAYS_UNDERLINE;

        HoverUnderline = DEFAULT_LINK_BEHAVIOR_HOVER_UNDERLINE;

        NeverUnderline = DEFAULT_LINK_BEHAVIOR_NEVER_UNDERLINE;
    }

    /// <summary>Gets or sets the always underline link behavior style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The always underline link behavior style.")]
    [DefaultValue(DEFAULT_LINK_BEHAVIOR_ALWAYS_UNDERLINE)]
    [RefreshProperties(RefreshProperties.All)]
    public string AlwaysUnderline { get; set; }

    /// <summary>Gets or sets the hover underline link behavior style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The hover underline link behavior style.")]
    [DefaultValue(DEFAULT_LINK_BEHAVIOR_HOVER_UNDERLINE)]
    [RefreshProperties(RefreshProperties.All)]
    public string HoverUnderline { get; set; }

    /// <summary>Gets or sets the never underline link behavior style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The never underline link behavior style.")]
    [DefaultValue(DEFAULT_LINK_BEHAVIOR_NEVER_UNDERLINE)]
    [RefreshProperties(RefreshProperties.All)]
    public string NeverUnderline { get; set; }

    #endregion
}