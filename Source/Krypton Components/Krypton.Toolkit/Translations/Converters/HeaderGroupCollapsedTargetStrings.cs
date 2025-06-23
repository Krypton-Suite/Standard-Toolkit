#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="HeaderGroupCollapsedTargetConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class HeaderGroupCollapsedTargetStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_BOTH = @"Collapse to Both Headers";
    private const string DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_PRIMARY = @"Collapse to Primary Header";
    private const string DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_SECONDARY = @"Collapse to Secondary Header";

    #endregion

    #region Identity

    public HeaderGroupCollapsedTargetStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => CollapsedToBoth.Equals(DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_BOTH) &&
                             CollapsedToPrimary.Equals(DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_PRIMARY) &&
                             CollapsedToSecondary.Equals(DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_SECONDARY);

    public void Reset()
    {
        CollapsedToBoth = DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_BOTH;

        CollapsedToPrimary = DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_PRIMARY;

        CollapsedToSecondary = DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_SECONDARY;
    }

    /// <summary>Gets or sets the collapsed to both header group string.</summary>
    [Category(@"Visuals")]
    [Description(@"The collapsed to both header group.")]
    [DefaultValue(DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_BOTH)]
    [RefreshProperties(RefreshProperties.All)]
    public string CollapsedToBoth { get; set; }

    /// <summary>Gets or sets the collapsed to primary header group string.</summary>
    [Category(@"Visuals")]
    [Description(@"The collapsed to both primary group.")]
    [DefaultValue(DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_PRIMARY)]
    [RefreshProperties(RefreshProperties.All)]
    public string CollapsedToPrimary { get; set; }

    /// <summary>Gets or sets the collapsed to secondary header group string.</summary>
    [Category(@"Visuals")]
    [Description(@"The collapsed to secondary header group.")]
    [DefaultValue(DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_SECONDARY)]
    [RefreshProperties(RefreshProperties.All)]
    public string CollapsedToSecondary { get; set; }

    #endregion
}