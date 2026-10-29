#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Owns the canonical nested toolkit string collections used for localization export/import.
/// </summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CommonStrings : GlobalId
{
    #region Instance Fields

    private readonly GeneralToolkitStrings _general;
    private readonly ControlBoxStrings _controlBox;
    private readonly SystemMenuStrings _systemMenu;
    private readonly CommonCommandStrings _commands;
    private readonly KryptonFileSystemListViewStrings _fileSystem;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="CommonStrings"/> class.</summary>
    public CommonStrings()
    {
        _general = new GeneralToolkitStrings();
        _controlBox = new ControlBoxStrings();
        _systemMenu = new SystemMenuStrings();
        _commands = new CommonCommandStrings();
        _fileSystem = new KryptonFileSystemListViewStrings();
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>
    /// Gets a value indicating if all nested string collections are default values.
    /// </summary>
    [Browsable(false)]
    public bool IsDefault => General.IsDefault &&
                             ControlBox.IsDefault &&
                             SystemMenu.IsDefault &&
                             Commands.IsDefault &&
                             FileSystem.IsDefault;

    /// <summary>
    /// Reset all nested string collections to default values.
    /// </summary>
    public void Reset()
    {
        General.Reset();
        ControlBox.Reset();
        SystemMenu.ResetValues();
        Commands.ResetValues();
        FileSystem.Reset();
    }

    /// <summary>
    /// Gets or sets a value indicating whether nested providers prefer Windows language-pack (MUI) text.
    /// Aggregates and forwards to <see cref="GeneralToolkitStrings.UseOSStrings"/>,
    /// <see cref="ControlBoxStrings.UseOSStrings"/>, and <see cref="KryptonFileSystemListViewStrings.UseOSStrings"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"When true, dialog, control-box, and file-system strings prefer Windows language-pack (MUI) text.")]
    [DefaultValue(false)]
    public bool UseOSStrings
    {
        get => General.UseOSStrings &&
               ControlBox.UseOSStrings &&
               FileSystem.UseOSStrings;
        set
        {
            General.UseOSStrings = value;
            ControlBox.UseOSStrings = value;
            FileSystem.UseOSStrings = value;
        }
    }

    /// <summary>Gets the general (dialog) toolkit strings.</summary>
    [Category(@"Visuals")]
    [Description(@"Collection of general dialog and toolkit strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public GeneralToolkitStrings General => _general;

    /// <summary>Gets the control-box caption-button tooltip strings.</summary>
    [Category(@"Visuals")]
    [Description(@"Collection of control-box caption-button tooltip strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public ControlBoxStrings ControlBox => _controlBox;

    /// <summary>Gets the system menu strings.</summary>
    [Category(@"Visuals")]
    [Description(@"Collection of system menu strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public SystemMenuStrings SystemMenu => _systemMenu;

    /// <summary>Gets the common command strings.</summary>
    [Category(@"Visuals")]
    [Description(@"Collection of common command strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public CommonCommandStrings Commands => _commands;

    /// <summary>Gets the file system list view strings.</summary>
    [Category(@"Visuals")]
    [Description(@"Collection of file system list view strings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonFileSystemListViewStrings FileSystem => _fileSystem;

    #endregion
}