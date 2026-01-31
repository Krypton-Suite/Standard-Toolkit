#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="TabStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TabStyleStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_TAB_STYLE_HIGH_PROFILE = @"High Profile";
    private const string DEFAULT_TAB_STYLE_STANDARD_PROFILE = @"Standard Profile";
    private const string DEFAULT_TAB_STYLE_LOW_PROFILE = @"Low Profile";
    private const string DEFAULT_TAB_STYLE_ONE_NOTE = @"OneNote";
    private const string DEFAULT_TAB_STYLE_DOCK = @"Dock";
    private const string DEFAULT_TAB_STYLE_DOCK_AUTO_HIDDEN = @"Dock AutoHidden";
    private const string DEFAULT_TAB_STYLE_CUSTOM1 = @"Custom 1";
    private const string DEFAULT_TAB_STYLE_CUSTOM2 = @"Custom 2";
    private const string DEFAULT_TAB_STYLE_CUSTOM3 = @"Custom 3";

    #endregion

    #region Identity

    public TabStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Custom1.Equals(DEFAULT_TAB_STYLE_CUSTOM1) &&
                             Custom2.Equals(DEFAULT_TAB_STYLE_CUSTOM2) &&
                             Custom3.Equals(DEFAULT_TAB_STYLE_CUSTOM3) &&
                             DockAutoHidden.Equals(DEFAULT_TAB_STYLE_DOCK_AUTO_HIDDEN) &&
                             Dock.Equals(DEFAULT_TAB_STYLE_DOCK) &&
                             HighProfile.Equals(DEFAULT_TAB_STYLE_HIGH_PROFILE) &&
                             StandardProfile.Equals(DEFAULT_TAB_STYLE_STANDARD_PROFILE) &&
                             LowProfile.Equals(DEFAULT_TAB_STYLE_LOW_PROFILE) &&
                             OneNote.Equals(DEFAULT_TAB_STYLE_ONE_NOTE);

    public void Reset()
    {
        Custom1 = DEFAULT_TAB_STYLE_CUSTOM1;

        Custom2 = DEFAULT_TAB_STYLE_CUSTOM2;

        Custom3 = DEFAULT_TAB_STYLE_CUSTOM3;

        DockAutoHidden = DEFAULT_TAB_STYLE_DOCK_AUTO_HIDDEN;

        Dock = DEFAULT_TAB_STYLE_DOCK;

        HighProfile = DEFAULT_TAB_STYLE_HIGH_PROFILE;

        StandardProfile = DEFAULT_TAB_STYLE_STANDARD_PROFILE;

        LowProfile = DEFAULT_TAB_STYLE_LOW_PROFILE;

        OneNote = DEFAULT_TAB_STYLE_ONE_NOTE;
    }

    /// <summary>Gets or sets the custom 1 tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 1 tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_CUSTOM1)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom1 { get; set; }

    /// <summary>Gets or sets the custom 2 tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 2 tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_CUSTOM2)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom2 { get; set; }

    /// <summary>Gets or sets the custom 3 tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The custom 3 tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_CUSTOM3)]
    [RefreshProperties(RefreshProperties.All)]
    public string Custom3 { get; set; }

    /// <summary>Gets or sets the dock auto hidden tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The dock auto hidden tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_DOCK_AUTO_HIDDEN)]
    [RefreshProperties(RefreshProperties.All)]
    public string DockAutoHidden { get; set; }

    /// <summary>Gets or sets the dock tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The dock tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_DOCK)]
    [RefreshProperties(RefreshProperties.All)]
    public string Dock { get; set; }

    /// <summary>Gets or sets the high profile tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The high profile tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_HIGH_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string HighProfile { get; set; }

    /// <summary>Gets or sets the standard profile tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The standard profile tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_STANDARD_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string StandardProfile { get; set; }

    /// <summary>Gets or sets the low profile tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The low profile tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_LOW_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string LowProfile { get; set; }

    /// <summary>Gets or sets the OneNote tab style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The OneNote tab style.")]
    [DefaultValue(DEFAULT_TAB_STYLE_ONE_NOTE)]
    [RefreshProperties(RefreshProperties.All)]
    public string OneNote { get; set; }

    #endregion
}