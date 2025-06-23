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

/// <summary>Exposes the set of <see cref="LabelStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class LabelStyleStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_LABEL_STYLE_BOLD_CONTROL = @"Bold (Control)";
    private const string DEFAULT_LABEL_STYLE_BOLD_PANEL = @"Bold (Panel)";
    private const string DEFAULT_LABEL_STYLE_CUSTOM_ONE = @"Custom 1";
    private const string DEFAULT_LABEL_STYLE_CUSTOM_TWO = @"Custom 2";
    private const string DEFAULT_LABEL_STYLE_CUSTOM_THREE = @"Custom 3";
    private const string DEFAULT_LABEL_STYLE_GROUP_BOX_CAPTION = @"Caption (Panel)";
    private const string DEFAULT_LABEL_STYLE_NORMAL_CONTROL = @"Normal (Control)";
    internal const string DEFAULT_LABEL_STYLE_NORMAL_PANEL = @"Normal (Panel)";
    private const string DEFAULT_LABEL_STYLE_TITLE_CONTROL = @"Title (Control)";
    private const string DEFAULT_LABEL_STYLE_TITLE_PANEL = @"Title (Panel)";
    private const string DEFAULT_LABEL_STYLE_ITALIC_CONTROL = @"Italic (Control)";
    private const string DEFAULT_LABEL_STYLE_ITALIC_PANEL = @"Italic (Panel)";
    internal const string DEFAULT_LABEL_STYLE_TOOL_TIP = @"ToolTip";
    internal const string DEFAULT_LABEL_STYLE_SUPER_TIP = @"SuperTip";
    private const string DEFAULT_LABEL_STYLE_KEY_TIP = @"KeyTip";

    #endregion

    #region Identity

    public LabelStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => BoldControl.Equals(DEFAULT_LABEL_STYLE_BOLD_CONTROL) &&
                             BoldPanel.Equals(DEFAULT_LABEL_STYLE_BOLD_PANEL) &&
                             CustomOne.Equals(DEFAULT_LABEL_STYLE_CUSTOM_ONE) &&
                             CustomTwo.Equals(DEFAULT_LABEL_STYLE_CUSTOM_TWO) &&
                             CustomThree.Equals(DEFAULT_LABEL_STYLE_CUSTOM_THREE) &&
                             GroupBoxCaption.Equals(DEFAULT_LABEL_STYLE_GROUP_BOX_CAPTION) &&
                             NormalControl.Equals(DEFAULT_LABEL_STYLE_NORMAL_CONTROL) &&
                             NormalPanel.Equals(DEFAULT_LABEL_STYLE_NORMAL_PANEL) &&
                             TitleControl.Equals(DEFAULT_LABEL_STYLE_TITLE_CONTROL) &&
                             TitlePanel.Equals(DEFAULT_LABEL_STYLE_TITLE_PANEL) &&
                             ItalicControl.Equals(DEFAULT_LABEL_STYLE_ITALIC_CONTROL) &&
                             ItalicPanel.Equals(DEFAULT_LABEL_STYLE_ITALIC_PANEL) &&
                             ToolTip.Equals(DEFAULT_LABEL_STYLE_TOOL_TIP) &&
                             SuperTip.Equals(DEFAULT_LABEL_STYLE_SUPER_TIP) &&
                             KeyTip.Equals(DEFAULT_LABEL_STYLE_KEY_TIP);

    public void Reset()
    {
        BoldControl = DEFAULT_LABEL_STYLE_BOLD_CONTROL;

        BoldPanel = DEFAULT_LABEL_STYLE_BOLD_PANEL;

        CustomOne = DEFAULT_LABEL_STYLE_CUSTOM_ONE;

        CustomTwo = DEFAULT_LABEL_STYLE_CUSTOM_TWO;

        CustomThree = DEFAULT_LABEL_STYLE_CUSTOM_THREE;

        GroupBoxCaption = DEFAULT_LABEL_STYLE_GROUP_BOX_CAPTION;

        NormalControl = DEFAULT_LABEL_STYLE_NORMAL_CONTROL;

        NormalPanel = DEFAULT_LABEL_STYLE_NORMAL_PANEL;

        TitleControl = DEFAULT_LABEL_STYLE_TITLE_CONTROL;

        TitlePanel = DEFAULT_LABEL_STYLE_TITLE_PANEL;

        ItalicControl = DEFAULT_LABEL_STYLE_ITALIC_CONTROL;

        ItalicPanel = DEFAULT_LABEL_STYLE_ITALIC_PANEL;

        ToolTip = DEFAULT_LABEL_STYLE_TOOL_TIP;

        SuperTip = DEFAULT_LABEL_STYLE_SUPER_TIP;

        KeyTip = DEFAULT_LABEL_STYLE_KEY_TIP;
    }

    // <summary>Gets or sets the bold control label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The bold control label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_BOLD_CONTROL)]
    [RefreshProperties(RefreshProperties.All)]
    public string BoldControl { get; set; }

    // <summary>Gets or sets the bold panel label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The bold panel label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_BOLD_PANEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string BoldPanel { get; set; }

    // <summary>Gets or sets the custom 1 label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_CUSTOM_ONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomOne { get; set; }

    /// <summary>Gets or sets the custom 2 label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_CUSTOM_TWO)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomTwo { get; set; }

    /// <summary>Gets or sets the custom 3 label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_CUSTOM_THREE)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomThree { get; set; }

    // <summary>Gets or sets the group box caption label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The group box caption label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_GROUP_BOX_CAPTION)]
    [RefreshProperties(RefreshProperties.All)]
    public string GroupBoxCaption { get; set; }

    // <summary>Gets or sets the normal control label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The normal control label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_NORMAL_CONTROL)]
    [RefreshProperties(RefreshProperties.All)]
    public string NormalControl { get; set; }

    // <summary>Gets or sets the normal panel label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The normal panel label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_NORMAL_PANEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string NormalPanel { get; set; }

    // <summary>Gets or sets the title control label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The title control label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_TITLE_CONTROL)]
    [RefreshProperties(RefreshProperties.All)]
    public string TitleControl { get; set; }

    // <summary>Gets or sets the title panel label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The title panel label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_TITLE_PANEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string TitlePanel { get; set; }

    // <summary>Gets or sets the italic control label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The italic control label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_ITALIC_CONTROL)]
    [RefreshProperties(RefreshProperties.All)]
    public string ItalicControl { get; set; }

    // <summary>Gets or sets the italic panel label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The italic panel label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_ITALIC_PANEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string ItalicPanel { get; set; }

    // <summary>Gets or sets the tool tip label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tool tip label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_TOOL_TIP)]
    [RefreshProperties(RefreshProperties.All)]
    public string ToolTip { get; set; }

    // <summary>Gets or sets the super tip label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The super tip label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_SUPER_TIP)]
    [RefreshProperties(RefreshProperties.All)]
    public string SuperTip { get; set; }

    // <summary>Gets or sets the key tip label style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The key tip label style.")]
    [DefaultValue(DEFAULT_LABEL_STYLE_KEY_TIP)]
    [RefreshProperties(RefreshProperties.All)]
    public string KeyTip { get; set; }

    #endregion
}