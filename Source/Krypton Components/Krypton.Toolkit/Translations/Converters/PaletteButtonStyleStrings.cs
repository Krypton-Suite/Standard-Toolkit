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

/// <summary>Exposes the set of <see cref="PaletteButtonStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteButtonStyleStrings : GlobalId
{
    #region Static Fields

    internal const string DEFAULT_PALETTE_BUTTON_STYLE_INHERIT = @"Inherit";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_STANDALONE = @"Standalone";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_ALTERNATE = @"Alternate";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_LOW_PROFILE = @"Low Profile";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_BREAD_CRUMB = @"BreadCrumb";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_CLUSTER = @"Cluster";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_STACK = @"Navigator Stack";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_OVERFLOW = @"Navigator Overflow";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_MINI = @"Navigator Mini";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_INPUT_CONTROL = @"Input Control";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_LIST_ITEM = @"List Item";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_FORM = nameof(System.Windows.Forms.Form);
    private const string DEFAULT_PALETTE_BUTTON_STYLE_FORM_CLOSE = @"Form Close";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_BUTTON_SPEC = nameof(Toolkit.ButtonSpec);
    private const string DEFAULT_PALETTE_BUTTON_STYLE_COMMAND = @"Command";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM1 = @"Custom 1";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM2 = @"Custom 2";
    private const string DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM3 = @"Custom 3";

    #endregion

    #region Identity

    public PaletteButtonStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Alternate.Equals(DEFAULT_PALETTE_BUTTON_STYLE_ALTERNATE) &&
                             BreadCrumb.Equals(DEFAULT_PALETTE_BUTTON_STYLE_BREAD_CRUMB) &&
                             ButtonSpec.Equals(DEFAULT_PALETTE_BUTTON_STYLE_BUTTON_SPEC) &&
                             Cluster.Equals(DEFAULT_PALETTE_BUTTON_STYLE_CLUSTER) &&
                             Command.Equals(DEFAULT_PALETTE_BUTTON_STYLE_COMMAND) &&
                             Custom1.Equals(DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM1) &&
                             Custom2.Equals(DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM2) &&
                             Custom3.Equals(DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM3) &&
                             Form.Equals(DEFAULT_PALETTE_BUTTON_STYLE_FORM) &&
                             FormClose.Equals(DEFAULT_PALETTE_BUTTON_STYLE_FORM_CLOSE) &&
                             Inherit.Equals(DEFAULT_PALETTE_BUTTON_STYLE_INHERIT) &&
                             InputControl.Equals(DEFAULT_PALETTE_BUTTON_STYLE_INPUT_CONTROL) &&
                             Standalone.Equals(DEFAULT_PALETTE_BUTTON_STYLE_STANDALONE) &&
                             LowProfile.Equals(DEFAULT_PALETTE_BUTTON_STYLE_LOW_PROFILE) &&
                             ListItem.Equals(DEFAULT_PALETTE_BUTTON_STYLE_LIST_ITEM) &&
                             NavigatorMini.Equals(DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_MINI) &&
                             NavigatorOverflow.Equals(DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_OVERFLOW) &&
                             NavigatorStack.Equals(DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_STACK);

    public void Reset()
    {
        Alternate = DEFAULT_PALETTE_BUTTON_STYLE_ALTERNATE;

        BreadCrumb = DEFAULT_PALETTE_BUTTON_STYLE_BREAD_CRUMB;

        ButtonSpec = DEFAULT_PALETTE_BUTTON_STYLE_BUTTON_SPEC;

        Cluster = DEFAULT_PALETTE_BUTTON_STYLE_CLUSTER;

        Command = DEFAULT_PALETTE_BUTTON_STYLE_COMMAND;

        Custom1 = DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM1;

        Custom2 = DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM2;

        Custom3 = DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM3;

        FormClose = DEFAULT_PALETTE_BUTTON_STYLE_FORM_CLOSE;

        Form = DEFAULT_PALETTE_BUTTON_STYLE_FORM;

        Inherit = DEFAULT_PALETTE_BUTTON_STYLE_INHERIT;

        InputControl = DEFAULT_PALETTE_BUTTON_STYLE_INPUT_CONTROL;

        Standalone = DEFAULT_PALETTE_BUTTON_STYLE_STANDALONE;

        LowProfile = DEFAULT_PALETTE_BUTTON_STYLE_LOW_PROFILE;

        ListItem = DEFAULT_PALETTE_BUTTON_STYLE_LIST_ITEM;

        NavigatorMini = DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_MINI;

        NavigatorOverflow = DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_OVERFLOW;

        NavigatorStack = DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_STACK;
    }

    /// <summary>Gets or sets the alternate palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The alternate palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_ALTERNATE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Alternate { get; set; }

    /// <summary>Gets or sets the breadcrumb palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The breadcrumb palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_BREAD_CRUMB)]
    [RefreshProperties(RefreshProperties.All)]
    public string BreadCrumb { get; set; }

    /// <summary>Gets or sets the buttonspec palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The buttonspec palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_BUTTON_SPEC)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonSpec { get; set; }

    /// <summary>Gets or sets the cluster palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The cluster palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_CLUSTER)]
    [RefreshProperties(RefreshProperties.All)]
    public string Cluster { get; set; }

    /// <summary>Gets or sets the command palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The command palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_COMMAND)]
    [RefreshProperties(RefreshProperties.All)]
    public string Command { get; set; }

    /// <summary>Gets or sets the custom 1 palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM1)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom1 { get; set; }

    /// <summary>Gets or sets the custom 2 palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM2)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom2 { get; set; }

    /// <summary>Gets or sets the custom 3 palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM3)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom3 { get; set; }

    /// <summary>Gets or sets the form palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_FORM)]
    [RefreshProperties(RefreshProperties.All)]
    public string Form { get; set; }

    /// <summary>Gets or sets the form close palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form close palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_FORM_CLOSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string FormClose { get; set; }

    /// <summary>Gets or sets the inherit palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The inherit palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_INHERIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Inherit { get; set; }

    /// <summary>Gets or sets the input control palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The input control palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_INPUT_CONTROL)]
    [RefreshProperties(RefreshProperties.All)]
    public string InputControl { get; set; }

    /// <summary>Gets or sets the standalone palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The standalone palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_STANDALONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Standalone { get; set; }

    /// <summary>Gets or sets the low profile palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The low profile palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_LOW_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string LowProfile { get; set; }

    /// <summary>Gets or sets the list item palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The list item palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_LIST_ITEM)]
    [RefreshProperties(RefreshProperties.All)]
    public string ListItem { get; set; }

    /// <summary>Gets or sets the navigator stack palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The navigator stack palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_STACK)]
    [RefreshProperties(RefreshProperties.All)]
    public string NavigatorStack { get; set; }

    /// <summary>Gets or sets the navigator mini palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The navigator mini palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_MINI)]
    [RefreshProperties(RefreshProperties.All)]
    public string NavigatorMini { get; set; }

    /// <summary>Gets or sets the navigator overflow palette button style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The navigator overflow palette button style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_OVERFLOW)]
    [RefreshProperties(RefreshProperties.All)]
    public string NavigatorOverflow { get; set; }

    #endregion
}