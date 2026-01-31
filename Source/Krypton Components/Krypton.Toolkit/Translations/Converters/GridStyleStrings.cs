#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="GridStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class GridStyleStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_GRID_STYLE_CUSTOM_ONE = @"Custom 1";
    private const string DEFAULT_GRID_STYLE_CUSTOM_TWO = @"Custom 2";
    private const string DEFAULT_GRID_STYLE_CUSTOM_THREE = @"Custom 3";
    private const string DEFAULT_GRID_STYLE_LIST = @"List";
    private const string DEFAULT_GRID_STYLE_SHEET = @"Sheet";

    #endregion

    #region Identity

    public GridStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => CustomOne.Equals(DEFAULT_GRID_STYLE_CUSTOM_ONE) &&
                             CustomTwo.Equals(DEFAULT_GRID_STYLE_CUSTOM_TWO) &&
                             CustomThree.Equals(DEFAULT_GRID_STYLE_CUSTOM_THREE) &&
                             List.Equals(DEFAULT_GRID_STYLE_LIST) &&
                             Sheet.Equals(DEFAULT_GRID_STYLE_SHEET);

    public void Reset()
    {
        CustomOne = DEFAULT_GRID_STYLE_CUSTOM_ONE;

        CustomTwo = DEFAULT_GRID_STYLE_CUSTOM_TWO;

        CustomThree = DEFAULT_GRID_STYLE_CUSTOM_THREE;

        List = DEFAULT_GRID_STYLE_LIST;

        Sheet = DEFAULT_GRID_STYLE_SHEET;
    }

    /// <summary>Gets or sets the custom 1 grid style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 grid style.")]
    [DefaultValue(DEFAULT_GRID_STYLE_CUSTOM_ONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomOne { get; set; }

    /// <summary>Gets or sets the custom 2 grid style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 grid style.")]
    [DefaultValue(DEFAULT_GRID_STYLE_CUSTOM_TWO)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomTwo { get; set; }

    /// <summary>Gets or sets the custom 3 grid style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 grid style.")]
    [DefaultValue(DEFAULT_GRID_STYLE_CUSTOM_THREE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomThree { get; set; }

    /// <summary>Gets or sets the list grid style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The list grid style.")]
    [DefaultValue(DEFAULT_GRID_STYLE_LIST)]
    [RefreshProperties(RefreshProperties.All)]
    public string List { get; set; }

    /// <summary>Gets or sets the sheet grid style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The sheet grid style.")]
    [DefaultValue(DEFAULT_GRID_STYLE_SHEET)]
    [RefreshProperties(RefreshProperties.All)]
    public string Sheet { get; set; }

    #endregion'
}