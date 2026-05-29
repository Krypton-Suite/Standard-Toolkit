#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="ButtonStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ButtonStyleStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_BUTTON_SPEC_STYLE_ALTERNATE = @"Alternate";
    public const string DEFAULT_BUTTON_SPEC_STYLE_STANDALONE = @"Standalone";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_LOW_PROFILE = @"Low Profile";
    private const string DEFAULT_BUTTON_SPEC_STYLE_BUTTON_SPEC = @"ButtonSpec";
    private const string DEFAULT_BUTTON_SPEC_STYLE_BREAD_CRUMB = @"Bread Crumb";
    private const string DEFAULT_BUTTON_SPEC_STYLE_CALENDAR_DAY = @"Calendar Day";
    private const string DEFAULT_BUTTON_SPEC_STYLE_CLUSTER = @"Cluster";
    private const string DEFAULT_BUTTON_SPEC_STYLE_GALLERY = @"Gallery";
    private const string DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_STACK = @"Navigator Stack";
    private const string DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_OVERFLOW = @"Navigator Overflow";
    private const string DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_MINI = @"Navigator Mini";
    private const string DEFAULT_BUTTON_SPEC_STYLE_INPUT_CONTROL = @"Input Control";
    private const string DEFAULT_BUTTON_SPEC_STYLE_LIST_ITEM = @"List Item";
    private const string DEFAULT_BUTTON_SPEC_STYLE_FORM = @"Form";
    private const string DEFAULT_BUTTON_SPEC_STYLE_FORM_CLOSE = @"Form Close";
    private const string DEFAULT_BUTTON_SPEC_STYLE_COMMAND = @"Command";
    private const string DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_ONE = @"Custom 1";
    private const string DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_TWO = @"Custom 2";
    private const string DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_THREE = @"Custom 3";

    #endregion

    #region Identity

    public ButtonStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Alternate.Equals(DEFAULT_BUTTON_SPEC_STYLE_ALTERNATE) &&
                             BreadCrumb.Equals(DEFAULT_BUTTON_SPEC_STYLE_BREAD_CRUMB) &&
                             ButtonSpec.Equals(DEFAULT_BUTTON_SPEC_STYLE_BUTTON_SPEC) &&
                             CalendarDay.Equals(DEFAULT_BUTTON_SPEC_STYLE_CALENDAR_DAY) &&
                             Cluster.Equals(DEFAULT_BUTTON_SPEC_STYLE_CLUSTER) &&
                             Command.Equals(DEFAULT_BUTTON_SPEC_STYLE_COMMAND) &&
                             CustomOne.Equals(DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_ONE) &&
                             CustomTwo.Equals(DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_TWO) &&
                             CustomThree.Equals(DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_THREE) &&
                             FormClose.Equals(DEFAULT_BUTTON_SPEC_STYLE_FORM_CLOSE) &&
                             Form.Equals(DEFAULT_BUTTON_SPEC_STYLE_FORM) &&
                             Gallery.Equals(DEFAULT_BUTTON_SPEC_STYLE_GALLERY) &&
                             InputControl.Equals(DEFAULT_BUTTON_SPEC_STYLE_INPUT_CONTROL) &&
                             ListItem.Equals(DEFAULT_BUTTON_SPEC_STYLE_LIST_ITEM) &&
                             LowProfile.Equals(DEFAULT_BUTTON_SPEC_STYLE_LOW_PROFILE) &&
                             NavigatorStack.Equals(DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_STACK) &&
                             NavigatorOverflow.Equals(DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_OVERFLOW) &&
                             NavigatorMini.Equals(DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_MINI) &&
                             Standalone.Equals(DEFAULT_BUTTON_SPEC_STYLE_STANDALONE);

    public void Reset()
    {
        Alternate = DEFAULT_BUTTON_SPEC_STYLE_ALTERNATE;

        BreadCrumb = DEFAULT_BUTTON_SPEC_STYLE_BREAD_CRUMB;

        ButtonSpec = DEFAULT_BUTTON_SPEC_STYLE_BUTTON_SPEC;

        CalendarDay = DEFAULT_BUTTON_SPEC_STYLE_CALENDAR_DAY;

        Cluster = DEFAULT_BUTTON_SPEC_STYLE_CLUSTER;

        Command = DEFAULT_BUTTON_SPEC_STYLE_COMMAND;

        CustomOne = DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_ONE;

        CustomTwo = DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_TWO;

        CustomThree = DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_THREE;

        FormClose = DEFAULT_BUTTON_SPEC_STYLE_FORM_CLOSE;

        Form = DEFAULT_BUTTON_SPEC_STYLE_FORM;

        Gallery = DEFAULT_BUTTON_SPEC_STYLE_GALLERY;

        InputControl = DEFAULT_BUTTON_SPEC_STYLE_INPUT_CONTROL;

        ListItem = DEFAULT_BUTTON_SPEC_STYLE_LIST_ITEM;

        LowProfile = DEFAULT_BUTTON_SPEC_STYLE_LOW_PROFILE;

        NavigatorMini = DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_MINI;

        NavigatorOverflow = DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_OVERFLOW;

        NavigatorStack = DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_STACK;

        Standalone = DEFAULT_BUTTON_SPEC_STYLE_STANDALONE;
    }

    /// <summary>Gets or sets the alternate button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The alternate button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_ALTERNATE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Alternate { get; set; }

    /// <summary>Gets or sets the bread crumb button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The bread crumb button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_BREAD_CRUMB)]
    [RefreshProperties(RefreshProperties.All)]
    public string BreadCrumb { get; set; }

    /// <summary>Gets or sets the buttonspec button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The buttonspec button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_BUTTON_SPEC)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonSpec { get; set; }

    /// <summary>Gets or sets the calendar day button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The calendar day button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_CALENDAR_DAY)]
    [RefreshProperties(RefreshProperties.All)]
    public string CalendarDay { get; set; }

    /// <summary>Gets or sets the cluster button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The cluster button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_CLUSTER)]
    [RefreshProperties(RefreshProperties.All)]
    public string Cluster { get; set; }

    /// <summary>Gets or sets the command button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The command button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_COMMAND)]
    [RefreshProperties(RefreshProperties.All)]
    public string Command { get; set; }

    /// <summary>Gets or sets the custom 1 button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_ONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomOne { get; set; }

    /// <summary>Gets or sets the custom 2 button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_TWO)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomTwo { get; set; }

    /// <summary>Gets or sets the custom 3 button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_THREE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomThree { get; set; }

    /// <summary>Gets or sets the form close button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form close button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_FORM_CLOSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string FormClose { get; set; }

    /// <summary>Gets or sets the form button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_FORM)]
    [RefreshProperties(RefreshProperties.All)]
    public string Form { get; set; }

    /// <summary>Gets or sets the gallery button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The gallery button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_GALLERY)]
    [RefreshProperties(RefreshProperties.All)]
    public string Gallery { get; set; }

    /// <summary>Gets or sets the input control button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The input control button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_INPUT_CONTROL)]
    [RefreshProperties(RefreshProperties.All)]
    public string InputControl { get; set; }

    /// <summary>Gets or sets the list item button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The list item button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_LIST_ITEM)]
    [RefreshProperties(RefreshProperties.All)]
    public string ListItem { get; set; }

    /// <summary>Gets or sets the low profile button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The low profile button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_LOW_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string LowProfile { get; set; }

    /// <summary>Gets or sets the navigator stack button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The navigator stack button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_STACK)]
    [RefreshProperties(RefreshProperties.All)]
    public string NavigatorStack { get; set; }

    /// <summary>Gets or sets the navigator overflow button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The navigator overflow button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_OVERFLOW)]
    [RefreshProperties(RefreshProperties.All)]
    public string NavigatorOverflow { get; set; }

    /// <summary>Gets or sets the navigator mini button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The navigator mini button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_MINI)]
    [RefreshProperties(RefreshProperties.All)]
    public string NavigatorMini { get; set; }

    /// <summary>Gets or sets the standalone button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The standalone button spec style.")]
    [DefaultValue(DEFAULT_BUTTON_SPEC_STYLE_STANDALONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Standalone { get; set; }

    #endregion
}