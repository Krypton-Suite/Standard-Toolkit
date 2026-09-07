#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Localisable strings for <see cref="KryptonPaletteFileComboBox"/>,
/// <see cref="KryptonPaletteFileListBox"/>, and <see cref="KryptonPaletteFileTreeView"/>.
/// List items themselves use palette and file names.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonPaletteFileSelectorStrings : GlobalId
{
    private const string DefaultDuplicateDisplayNameFormat = @"{0} ({1})";

    /// <summary>
    /// Shared default instance used by <see cref="KryptonPaletteFileThemeItem.FromDirectory"/>
    /// when a format is not passed. Assign properties before listing files, or set
    /// <c>Strings</c> on a combo, list, or tree selector.
    /// </summary>
    public static KryptonPaletteFileSelectorStrings Default { get; } = new KryptonPaletteFileSelectorStrings();

    /// <summary>Initializes a new instance of the <see cref="KryptonPaletteFileSelectorStrings"/> class.</summary>
    public KryptonPaletteFileSelectorStrings() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    /// <summary>Gets a value indicating whether all values are default.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => DuplicateDisplayNameFormat == DefaultDuplicateDisplayNameFormat;

    /// <summary>Resets all strings to defaults.</summary>
    public void Reset() => DuplicateDisplayNameFormat = DefaultDuplicateDisplayNameFormat;

    /// <summary>
    /// Gets or sets the format used when two themes would share the same list caption.
    /// <c>{0}</c> is the original name, <c>{1}</c> is a unique suffix (2, 3, …).
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Format when two listed themes would have the same caption. {0}=name, {1}=suffix.")]
    [DefaultValue(DefaultDuplicateDisplayNameFormat)]
    public string DuplicateDisplayNameFormat { get; set; }
}
