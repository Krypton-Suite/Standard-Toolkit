#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="PaletteTextTrimConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteTextTrimStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_PALETTE_TEXT_TRIM_INHERIT = @"Inherit";
    private const string DEFAULT_PALETTE_TEXT_TRIM_HIDE = @"Hide";
    private const string DEFAULT_PALETTE_TEXT_TRIM_CHARACTER = @"Character";
    private const string DEFAULT_PALETTE_TEXT_TRIM_WORD = @"Word";
    private const string DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_CHARACTER = @"Ellipsis Character";
    private const string DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_WORD = @"Ellipsis Word";
    private const string DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_PATH = @"Ellipsis Path";

    #endregion

    #region Identity

    public PaletteTextTrimStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Inherit.Equals(DEFAULT_PALETTE_TEXT_TRIM_INHERIT) &&
                             Hide.Equals(DEFAULT_PALETTE_TEXT_TRIM_HIDE) &&
                             Character.Equals(DEFAULT_PALETTE_TEXT_TRIM_CHARACTER) &&
                             Word.Equals(DEFAULT_PALETTE_TEXT_TRIM_WORD) &&
                             EllipsisCharacter.Equals(DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_CHARACTER) &&
                             EllipsisWord.Equals(DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_WORD) &&
                             EllipsisPath.Equals(DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_PATH);

    public void Reset()
    {
        Inherit = DEFAULT_PALETTE_TEXT_TRIM_INHERIT;

        Hide = DEFAULT_PALETTE_TEXT_TRIM_HIDE;

        Character = DEFAULT_PALETTE_TEXT_TRIM_CHARACTER;

        Word = DEFAULT_PALETTE_TEXT_TRIM_WORD;

        EllipsisCharacter = DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_CHARACTER;

        EllipsisWord = DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_WORD;

        EllipsisPath = DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_PATH;
    }

    /// <summary>Gets or sets the inherit palette text trim style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The inherit palette text trim style.")]
    [DefaultValue(DEFAULT_PALETTE_TEXT_TRIM_INHERIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Inherit { get; set; }

    /// <summary>Gets or sets the hide palette text trim style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The hide palette text trim style.")]
    [DefaultValue(DEFAULT_PALETTE_TEXT_TRIM_HIDE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Hide { get; set; }

    /// <summary>Gets or sets the character palette text trim style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The character palette text trim style.")]
    [DefaultValue(DEFAULT_PALETTE_TEXT_TRIM_CHARACTER)]
    [RefreshProperties(RefreshProperties.All)]
    public string Character { get; set; }

    /// <summary>Gets or sets the word palette text trim style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The word palette text trim style.")]
    [DefaultValue(DEFAULT_PALETTE_TEXT_TRIM_WORD)]
    [RefreshProperties(RefreshProperties.All)]
    public string Word { get; set; }

    /// <summary>Gets or sets the ellipsis character palette text trim style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The ellipsis character palette text trim style.")]
    [DefaultValue(DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_CHARACTER)]
    [RefreshProperties(RefreshProperties.All)]
    public string EllipsisCharacter { get; set; }

    /// <summary>Gets or sets the ellipsis word palette text trim style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The ellipsis word palette text trim style.")]
    [DefaultValue(DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_WORD)]
    [RefreshProperties(RefreshProperties.All)]
    public string EllipsisWord { get; set; }

    /// <summary>Gets or sets the ellipsis path palette text trim style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The ellipsis path palette text trim style.")]
    [DefaultValue(DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_PATH)]
    [RefreshProperties(RefreshProperties.All)]
    public string EllipsisPath { get; set; }

    #endregion
}