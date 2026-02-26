#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Exposes the set of strings used by <see cref="KryptonFormTitleBar"/> Insert Standard Items,
/// localizable via <see cref="KryptonManager.Strings"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class FormTitleBarStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_FILE = @"File";
    private const string DEFAULT_EDIT = @"Edit";
    private const string DEFAULT_TOOLS = @"Tools";
    private const string DEFAULT_HELP = @"Help";
    private const string DEFAULT_EXIT = @"Exit";
    private const string DEFAULT_SELECT_ALL = @"Select All";
    private const string DEFAULT_CUSTOMIZE = @"Customize";
    private const string DEFAULT_OPTIONS = @"Options";
    private const string DEFAULT_CONTENTS = @"Contents";
    private const string DEFAULT_INDEX = @"Index";
    private const string DEFAULT_ABOUT = @"About";

    #endregion

    #region Identity

    public FormTitleBarStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => File.Equals(DEFAULT_FILE) &&
                             Edit.Equals(DEFAULT_EDIT) &&
                             Tools.Equals(DEFAULT_TOOLS) &&
                             Help.Equals(DEFAULT_HELP) &&
                             Exit.Equals(DEFAULT_EXIT) &&
                             SelectAll.Equals(DEFAULT_SELECT_ALL) &&
                             Customize.Equals(DEFAULT_CUSTOMIZE) &&
                             Options.Equals(DEFAULT_OPTIONS) &&
                             Contents.Equals(DEFAULT_CONTENTS) &&
                             Index.Equals(DEFAULT_INDEX) &&
                             About.Equals(DEFAULT_ABOUT);

    public void Reset()
    {
        File = DEFAULT_FILE;
        Edit = DEFAULT_EDIT;
        Tools = DEFAULT_TOOLS;
        Help = DEFAULT_HELP;
        Exit = DEFAULT_EXIT;
        SelectAll = DEFAULT_SELECT_ALL;
        Customize = DEFAULT_CUSTOMIZE;
        Options = DEFAULT_OPTIONS;
        Contents = DEFAULT_CONTENTS;
        Index = DEFAULT_INDEX;
        About = DEFAULT_ABOUT;
    }

    /// <summary>Gets or sets the File menu caption.</summary>
    [Category(@"Visuals")]
    [Description(@"The File top-level menu caption.")]
    [DefaultValue(DEFAULT_FILE)]
    [Localizable(true)]
    public string File { get; set; }

    /// <summary>Gets or sets the Edit menu caption.</summary>
    [Category(@"Visuals")]
    [Description(@"The Edit top-level menu caption.")]
    [DefaultValue(DEFAULT_EDIT)]
    [Localizable(true)]
    public string Edit { get; set; }

    /// <summary>Gets or sets the Tools menu caption.</summary>
    [Category(@"Visuals")]
    [Description(@"The Tools top-level menu caption.")]
    [DefaultValue(DEFAULT_TOOLS)]
    [Localizable(true)]
    public string Tools { get; set; }

    /// <summary>Gets or sets the Help menu caption.</summary>
    [Category(@"Visuals")]
    [Description(@"The Help top-level menu caption.")]
    [DefaultValue(DEFAULT_HELP)]
    [Localizable(true)]
    public string Help { get; set; }

    /// <summary>Gets or sets the Exit menu item text.</summary>
    [Category(@"Visuals")]
    [Description(@"The Exit menu item text.")]
    [DefaultValue(DEFAULT_EXIT)]
    [Localizable(true)]
    public string Exit { get; set; }

    /// <summary>Gets or sets the Select All menu item text.</summary>
    [Category(@"Visuals")]
    [Description(@"The Select All menu item text.")]
    [DefaultValue(DEFAULT_SELECT_ALL)]
    [Localizable(true)]
    public string SelectAll { get; set; }

    /// <summary>Gets or sets the Customize menu item text.</summary>
    [Category(@"Visuals")]
    [Description(@"The Customize menu item text.")]
    [DefaultValue(DEFAULT_CUSTOMIZE)]
    [Localizable(true)]
    public string Customize { get; set; }

    /// <summary>Gets or sets the Options menu item text.</summary>
    [Category(@"Visuals")]
    [Description(@"The Options menu item text.")]
    [DefaultValue(DEFAULT_OPTIONS)]
    [Localizable(true)]
    public string Options { get; set; }

    /// <summary>Gets or sets the Contents menu item text.</summary>
    [Category(@"Visuals")]
    [Description(@"The Contents menu item text.")]
    [DefaultValue(DEFAULT_CONTENTS)]
    [Localizable(true)]
    public string Contents { get; set; }

    /// <summary>Gets or sets the Index menu item text.</summary>
    [Category(@"Visuals")]
    [Description(@"The Index menu item text.")]
    [DefaultValue(DEFAULT_INDEX)]
    [Localizable(true)]
    public string Index { get; set; }

    /// <summary>Gets or sets the About menu item text.</summary>
    [Category(@"Visuals")]
    [Description(@"The About menu item text.")]
    [DefaultValue(DEFAULT_ABOUT)]
    [Localizable(true)]
    public string About { get; set; }

    #endregion
}
