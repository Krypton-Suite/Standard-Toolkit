#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="HeaderStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class HeaderStyleStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_HEADER_STYLE_CALENDAR = nameof(System.Globalization.Calendar);
    private const string DEFAULT_HEADER_STYLE_CUSTOM_ONE = @"Custom 1";
    private const string DEFAULT_HEADER_STYLE_CUSTOM_TWO = @"Custom 2";
    private const string DEFAULT_HEADER_STYLE_CUSTOM_THREE = @"Custom 3";
    private const string DEFAULT_HEADER_STYLE_DOCK_ACTIVE = @"Dock - Active";
    private const string DEFAULT_HEADER_STYLE_DOCK_INACTIVE = @"Dock - Inactive";
    private const string DEFAULT_HEADER_STYLE_FORM = nameof(System.Windows.Forms.Form);
    private const string DEFAULT_HEADER_STYLE_PRIMARY = @"Primary";
    private const string DEFAULT_HEADER_STYLE_SECONDARY = @"Secondary";

    #endregion

    #region Identity

    public HeaderStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Calendar.Equals(DEFAULT_HEADER_STYLE_CALENDAR) &&
                             CustomOne.Equals(DEFAULT_HEADER_STYLE_CUSTOM_ONE) &&
                             CustomTwo.Equals(DEFAULT_HEADER_STYLE_CUSTOM_TWO) &&
                             CustomThree.Equals(DEFAULT_HEADER_STYLE_CUSTOM_THREE) &&
                             DockActive.Equals(DEFAULT_HEADER_STYLE_DOCK_ACTIVE) &&
                             DockInactive.Equals(DEFAULT_HEADER_STYLE_DOCK_INACTIVE) &&
                             Form.Equals(DEFAULT_HEADER_STYLE_FORM) &&
                             Primary.Equals(DEFAULT_HEADER_STYLE_PRIMARY) &&
                             Secondary.Equals(DEFAULT_HEADER_STYLE_SECONDARY);

    public void Reset()
    {
        Calendar = DEFAULT_HEADER_STYLE_CALENDAR;

        CustomOne = DEFAULT_HEADER_STYLE_CUSTOM_ONE;

        CustomTwo = DEFAULT_HEADER_STYLE_CUSTOM_TWO;

        CustomThree = DEFAULT_HEADER_STYLE_CUSTOM_THREE;

        DockActive = DEFAULT_HEADER_STYLE_DOCK_ACTIVE;

        DockInactive = DEFAULT_HEADER_STYLE_DOCK_INACTIVE;

        Form = DEFAULT_HEADER_STYLE_FORM;

        Primary = DEFAULT_HEADER_STYLE_PRIMARY;

        Secondary = DEFAULT_HEADER_STYLE_SECONDARY;
    }

    /// <summary>Gets or sets the calendar header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The calendar header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_CALENDAR)]
    [RefreshProperties(RefreshProperties.All)]
    public string Calendar { get; set; }

    /// <summary>Gets or sets the custom 1 header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_CUSTOM_ONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomOne { get; set; }

    /// <summary>Gets or sets the custom 2 header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_CUSTOM_TWO)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomTwo { get; set; }

    /// <summary>Gets or sets the custom 3 header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_CUSTOM_THREE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomThree { get; set; }

    /// <summary>Gets or sets the dock active header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The dock active header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_DOCK_ACTIVE)]
    [RefreshProperties(RefreshProperties.All)]
    public string DockActive { get; set; }

    /// <summary>Gets or sets the dock inactive header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The dock inactive header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_DOCK_INACTIVE)]
    [RefreshProperties(RefreshProperties.All)]
    public string DockInactive { get; set; }

    /// <summary>Gets or sets the form header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_FORM)]
    [RefreshProperties(RefreshProperties.All)]
    public string Form { get; set; }

    /// <summary>Gets or sets the primary header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The primary header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_PRIMARY)]
    [RefreshProperties(RefreshProperties.All)]
    public string Primary { get; set; }

    /// <summary>Gets or sets the secondary header style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The secondary header style.")]
    [DefaultValue(DEFAULT_HEADER_STYLE_SECONDARY)]
    [RefreshProperties(RefreshProperties.All)]
    public string Secondary { get; set; }

    #endregion
}