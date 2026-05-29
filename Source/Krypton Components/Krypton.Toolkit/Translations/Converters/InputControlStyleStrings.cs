#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="InputControlStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class InputControlStyleStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_ONE = @"Custom 1";
    private const string DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_TWO = @"Custom 2";
    private const string DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_THREE = @"Custom 3";
    private const string DEFAULT_INPUT_CONTROL_STYLE_RIBBON = @"Ribbon";
    private const string DEFAULT_INPUT_CONTROL_STYLE_PANEL_ALTERNATE = @"Panel Alternate";
    private const string DEFAULT_INPUT_CONTROL_STYLE_PANEL_CLIENT = @"Panel Client";
    private const string DEFAULT_INPUT_CONTROL_STYLE_STANDALONE = @"Standalone";

    #endregion

    #region Identity

    public InputControlStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => CustomOne.Equals(DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_ONE) &&
                             CustomTwo.Equals(DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_TWO) &&
                             CustomThree.Equals(DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_THREE) &&
                             Ribbon.Equals(DEFAULT_INPUT_CONTROL_STYLE_RIBBON) &&
                             PanelAlternate.Equals(DEFAULT_INPUT_CONTROL_STYLE_PANEL_ALTERNATE) &&
                             PanelClient.Equals(DEFAULT_INPUT_CONTROL_STYLE_PANEL_CLIENT) &&
                             Standalone.Equals(DEFAULT_INPUT_CONTROL_STYLE_STANDALONE);

    public void Reset()
    {
        CustomOne = DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_ONE;

        CustomTwo = DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_TWO;

        CustomThree = DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_THREE;

        Ribbon = DEFAULT_INPUT_CONTROL_STYLE_RIBBON;

        PanelAlternate = DEFAULT_INPUT_CONTROL_STYLE_PANEL_ALTERNATE;

        PanelClient = DEFAULT_INPUT_CONTROL_STYLE_PANEL_CLIENT;

        Standalone = DEFAULT_INPUT_CONTROL_STYLE_STANDALONE;
    }

    /// <summary>Gets or sets the custom 1 input control style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 input control style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_ONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomOne { get; set; }

    /// <summary>Gets or sets the custom 2 input control style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 input control style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_TWO)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomTwo { get; set; }

    /// <summary>Gets or sets the custom 3 input control style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 input control style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_THREE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomThree { get; set; }

    /// <summary>Gets or sets the ribbon input control style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The ribbon input control style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_STYLE_RIBBON)]
    [RefreshProperties(RefreshProperties.All)]
    public string Ribbon { get; set; }

    /// <summary>Gets or sets the panel alternate input control style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The panel alternate input control style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_STYLE_PANEL_ALTERNATE)]
    [RefreshProperties(RefreshProperties.All)]
    public string PanelAlternate { get; set; }

    /// <summary>Gets or sets the panel client input control style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The panel client input control style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_STYLE_PANEL_CLIENT)]
    [RefreshProperties(RefreshProperties.All)]
    public string PanelClient { get; set; }

    /// <summary>Gets or sets the standalone input control style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The standalone input control style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_STYLE_STANDALONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Standalone { get; set; }

    #endregion
}