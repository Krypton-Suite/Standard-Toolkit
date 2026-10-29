#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes a custom set of strings that are used within the Krypton Toolkit, and are localisable.</summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CustomToolkitStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_COLLAPSE = @"C&ollapse"; // Accelerator key - O
    private const string DEFAULT_EXPAND = @"Ex&pand"; // Accelerator key - P
    private const string DEFAULT_SYSTEM_INFORMATION = "S&ystem Information";
    private const string DEFAULT_CURRENT_THEME = @"Current Theme";
    private const string DEFAULT_DO_NOT_SHOW_AGAIN = @"&Do not show again";
    private const string DEFAULT_TOGGLE_SWITCH_ON = @"On";
    private const string DEFAULT_TOGGLE_SWITCH_OFF = @"Off";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="CustomToolkitStrings" /> class.</summary>
    public CustomToolkitStrings()
    {
        ResetValues();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    private static CommonCommandStrings Commands => KryptonGlobalToolkitStrings.CommonToolkitStrings.Commands;

    /// <summary>
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    [Browsable(false)]
    public bool IsDefault => Commands.IsDefault &&
                             Collapse.Equals(DEFAULT_COLLAPSE) &&
                             Expand.Equals(DEFAULT_EXPAND) &&
                             DoNotShowAgain.Equals(DEFAULT_DO_NOT_SHOW_AGAIN) &&
                             SystemInformation.Equals(DEFAULT_SYSTEM_INFORMATION) &&
                             CurrentTheme.Equals(DEFAULT_CURRENT_THEME) &&
                             On.Equals(DEFAULT_TOGGLE_SWITCH_ON) &&
                             Off.Equals(DEFAULT_TOGGLE_SWITCH_OFF);

    /// <summary>Resets the values.</summary>
    public void ResetValues()
    {
        Commands.ResetValues();
        Collapse = DEFAULT_COLLAPSE;
        Expand = DEFAULT_EXPAND;
        DoNotShowAgain = DEFAULT_DO_NOT_SHOW_AGAIN;
        SystemInformation = DEFAULT_SYSTEM_INFORMATION;
        CurrentTheme = DEFAULT_CURRENT_THEME;
        On = DEFAULT_TOGGLE_SWITCH_ON;
        Off = DEFAULT_TOGGLE_SWITCH_OFF;
    }

    /// <summary>Gets or sets the collapse string used in expandable footers.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Collapse string used in expandable footers.")]
    [DefaultValue(DEFAULT_COLLAPSE)]
    public string Collapse { get; set; }

    /// <summary>Gets or sets the expand string used in expandable footers.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Expand string used in expandable footers.")]
    [DefaultValue(DEFAULT_EXPAND)]
    public string Expand { get; set; }

    /// <summary>
    /// Gets and sets the Apply string used in property dialogs.
    /// Compatibility alias for <see cref="CommonCommandStrings.Apply"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Apply string used for property dialogs.")]
    [DefaultValue(@"A&pply")]
    [ToolkitStringsCanonicalAlias]
    public string Apply
    {
        get => Commands.Apply;
        set => Commands.Apply = value;
    }

    /// <summary>
    /// Gets and sets the Back string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.Back"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Back string used for custom situations.")]
    [DefaultValue(@"Bac&k")]
    [ToolkitStringsCanonicalAlias]
    public string Back
    {
        get => Commands.Back;
        set => Commands.Back = value;
    }

    /// <summary>
    /// Gets and sets the do not show again string used in custom situations.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Do not show again string used for custom situations.")]
    [DefaultValue(DEFAULT_DO_NOT_SHOW_AGAIN)]
    public string DoNotShowAgain { get; set; }

    /// <summary>
    /// Gets and sets the Exit string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.Exit"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Exit string used for custom situations.")]
    [DefaultValue(@"E&xit")]
    [ToolkitStringsCanonicalAlias]
    public string Exit
    {
        get => Commands.Exit;
        set => Commands.Exit = value;
    }

    /// <summary>
    /// Gets and sets the Finish string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.Finish"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Finish string used for custom situations.")]
    [DefaultValue(@"&Finish")]
    [ToolkitStringsCanonicalAlias]
    public string Finish
    {
        get => Commands.Finish;
        set => Commands.Finish = value;
    }

    /// <summary>
    /// Gets and sets the Next string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.Next"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Next string used for custom situations.")]
    [DefaultValue(@"&Next")]
    [ToolkitStringsCanonicalAlias]
    public string Next
    {
        get => Commands.Next;
        set => Commands.Next = value;
    }

    /// <summary>
    /// Gets and sets the Previous string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.Previous"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Previous string used for custom situations.")]
    [DefaultValue(@"Pre&vious")]
    [ToolkitStringsCanonicalAlias]
    public string Previous
    {
        get => Commands.Previous;
        set => Commands.Previous = value;
    }

    /// <summary>
    /// Gets and sets the Cut string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.Cut"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cut string used for custom situations.")]
    [DefaultValue(@"C&ut")]
    [ToolkitStringsCanonicalAlias]
    public string Cut
    {
        get => Commands.Cut;
        set => Commands.Cut = value;
    }

    /// <summary>
    /// Gets and sets the Copy string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.Copy"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Copy string used for custom situations.")]
    [DefaultValue(@"&Copy")]
    [ToolkitStringsCanonicalAlias]
    public string Copy
    {
        get => Commands.Copy;
        set => Commands.Copy = value;
    }

    /// <summary>
    /// Gets and sets the Paste string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.Paste"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Paste string used for custom situations.")]
    [DefaultValue(@"Pas&te")]
    [ToolkitStringsCanonicalAlias]
    public string Paste
    {
        get => Commands.Paste;
        set => Commands.Paste = value;
    }

    /// <summary>
    /// Gets and sets the Select All string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.SelectAll"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Select All string used for custom situations.")]
    [DefaultValue(@"Sel&ect All")]
    [ToolkitStringsCanonicalAlias]
    public string SelectAll
    {
        get => Commands.SelectAll;
        set => Commands.SelectAll = value;
    }

    /// <summary>
    /// Gets and sets the Clear Clipboard string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.ClearClipboard"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Clear Clipboard string used for custom situations.")]
    [DefaultValue(@"Clear Clipboa&rd")]
    [ToolkitStringsCanonicalAlias]
    public string ClearClipboard
    {
        get => Commands.ClearClipboard;
        set => Commands.ClearClipboard = value;
    }

    /// <summary>
    /// Gets and sets the Yes to All string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.YesToAll"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Yes to All string used for custom situations.")]
    [DefaultValue(@"Yes &to All")]
    [ToolkitStringsCanonicalAlias]
    public string YesToAll
    {
        get => Commands.YesToAll;
        set => Commands.YesToAll = value;
    }

    /// <summary>
    /// Gets and sets the No to All string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.NoToAll"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"No to All string used for custom situations.")]
    [DefaultValue(@"No t&o All")]
    [ToolkitStringsCanonicalAlias]
    public string NoToAll
    {
        get => Commands.NoToAll;
        set => Commands.NoToAll = value;
    }

    /// <summary>
    /// Gets and sets the Ok to All string used in custom situations.
    /// Compatibility alias for <see cref="CommonCommandStrings.OkToAll"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Ok to All string used for custom situations.")]
    [DefaultValue(@"O&k to All")]
    [ToolkitStringsCanonicalAlias]
    public string OkToAll
    {
        get => Commands.OkToAll;
        set => Commands.OkToAll = value;
    }

    /// <summary>Gets or sets the reset string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Reset string used for custom situations.")]
    [DefaultValue(@"&Reset")]
    [ToolkitStringsCanonicalAlias]
    public string Reset
    {
        get => Commands.Reset;
        set => Commands.Reset = value;
    }

    /// <summary>Gets or sets the system information string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"System information string used for custom situations.")]
    [DefaultValue(DEFAULT_SYSTEM_INFORMATION)]
    public string SystemInformation { get; set; }

    /// <summary>Gets or sets the current theme string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Current theme string used for custom situations.")]
    [DefaultValue(DEFAULT_CURRENT_THEME)]
    public string CurrentTheme { get; set; }

    /// <summary>Gets or sets the on.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"'On' string used for custom situations.")]
    [DefaultValue(DEFAULT_TOGGLE_SWITCH_ON)]
    public string On { get; set; }

    /// <summary>Gets or sets the off.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"'Off' string used for custom situations.")]
    [DefaultValue(DEFAULT_TOGGLE_SWITCH_OFF)]
    public string Off { get; set; }

    #endregion
}
