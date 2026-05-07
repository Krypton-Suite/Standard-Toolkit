#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="DataGridViewStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class DataGridViewStyleStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_ONE = @"Custom 1";
    private const string DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_TWO = @"Custom 2";
    private const string DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_THREE = @"Custom 3";
    private const string DEFAULT_DATA_GRID_VIEW_STYLE_MIXED = @"Mixed";
    private const string DEFAULT_DATA_GRID_VIEW_STYLE_LIST = @"List";
    private const string DEFAULT_DATA_GRID_VIEW_STYLE_SHEET = @"Sheet";

    #endregion

    #region Identity

    public DataGridViewStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => CustomOne.Equals(DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_ONE) &&
                             CustomTwo.Equals(DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_TWO) &&
                             CustomThree.Equals(DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_THREE) &&
                             Mixed.Equals(DEFAULT_DATA_GRID_VIEW_STYLE_MIXED) &&
                             List.Equals(DEFAULT_DATA_GRID_VIEW_STYLE_LIST) &&
                             Sheet.Equals(DEFAULT_DATA_GRID_VIEW_STYLE_SHEET);

    public void Reset()
    {
        CustomOne = DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_ONE;

        CustomTwo = DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_TWO;

        CustomThree = DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_THREE;

        Mixed = DEFAULT_DATA_GRID_VIEW_STYLE_MIXED;

        List = DEFAULT_DATA_GRID_VIEW_STYLE_LIST;

        Sheet = DEFAULT_DATA_GRID_VIEW_STYLE_SHEET;
    }

    /// <summary>Gets or sets the custom 1 data grid view style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 data grid view style.")]
    [DefaultValue(DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_ONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomOne { get; set; }

    /// <summary>Gets or sets the custom 2 data grid view style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 data grid view style.")]
    [DefaultValue(DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_TWO)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomTwo { get; set; }

    /// <summary>Gets or sets the custom 3 data grid view style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 data grid view style.")]
    [DefaultValue(DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_THREE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomThree { get; set; }

    /// <summary>Gets or sets the mixed data grid view style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The mixed data grid view style.")]
    [DefaultValue(DEFAULT_DATA_GRID_VIEW_STYLE_MIXED)]
    [RefreshProperties(RefreshProperties.All)]
    public string Mixed { get; set; }

    /// <summary>Gets or sets the list data grid view style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The list data grid view style.")]
    [DefaultValue(DEFAULT_DATA_GRID_VIEW_STYLE_LIST)]
    [RefreshProperties(RefreshProperties.All)]
    public string List { get; set; }

    /// <summary>Gets or sets the sheet data grid view style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The sheet data grid view style.")]
    [DefaultValue(DEFAULT_DATA_GRID_VIEW_STYLE_SHEET)]
    [RefreshProperties(RefreshProperties.All)]
    public string Sheet { get; set; }

    #endregion
}