#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="KryptonContextMenu"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonContextMenuStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_OVERFLOW_SCROLL_UP = @"Scroll Up";
    private const string DEFAULT_OVERFLOW_SCROLL_DOWN = @"Scroll Down";
    private const string DEFAULT_OVERFLOW_SCROLL_UP_ARROW = "\u25B2";
    private const string DEFAULT_OVERFLOW_SCROLL_DOWN_ARROW = "\u25BC";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonContextMenuStrings" /> class.</summary>
    public KryptonContextMenuStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    [Browsable(false)]
    public bool IsDefault => OverflowScrollUp.Equals(DEFAULT_OVERFLOW_SCROLL_UP) &&
                             OverflowScrollDown.Equals(DEFAULT_OVERFLOW_SCROLL_DOWN) &&
                             !OverflowScrollUseArrows &&
                             OverflowScrollUpArrow.Equals(DEFAULT_OVERFLOW_SCROLL_UP_ARROW) &&
                             OverflowScrollDownArrow.Equals(DEFAULT_OVERFLOW_SCROLL_DOWN_ARROW);

    /// <summary>Resets this instance.</summary>
    public void Reset()
    {
        OverflowScrollUp = DEFAULT_OVERFLOW_SCROLL_UP;
        OverflowScrollDown = DEFAULT_OVERFLOW_SCROLL_DOWN;
        OverflowScrollUseArrows = false;
        OverflowScrollUpArrow = DEFAULT_OVERFLOW_SCROLL_UP_ARROW;
        OverflowScrollDownArrow = DEFAULT_OVERFLOW_SCROLL_DOWN_ARROW;
    }

    /// <summary>Gets or sets the overflow scroll up row label when <see cref="OverflowScrollUseArrows"/> is false.</summary>
    [Category(@"Visuals")]
    [Description(@"The overflow scroll up row label when OverflowScrollUseArrows is false.")]
    [DefaultValue(DEFAULT_OVERFLOW_SCROLL_UP)]
    [RefreshProperties(RefreshProperties.All)]
    public string OverflowScrollUp { get; set; }

    /// <summary>Gets or sets the overflow scroll down row label when <see cref="OverflowScrollUseArrows"/> is false.</summary>
    [Category(@"Visuals")]
    [Description(@"The overflow scroll down row label when OverflowScrollUseArrows is false.")]
    [DefaultValue(DEFAULT_OVERFLOW_SCROLL_DOWN)]
    [RefreshProperties(RefreshProperties.All)]
    public string OverflowScrollDown { get; set; }

    /// <summary>Gets or sets whether overflow scroll rows use arrow glyphs instead of text labels.</summary>
    [Category(@"Visuals")]
    [Description(@"When true, overflow scroll rows show arrow glyphs instead of OverflowScrollUp/OverflowScrollDown text.")]
    [DefaultValue(false)]
    [RefreshProperties(RefreshProperties.All)]
    public bool OverflowScrollUseArrows { get; set; }

    /// <summary>Gets or sets the glyph shown on the overflow scroll up row when <see cref="OverflowScrollUseArrows"/> is true.</summary>
    [Category(@"Visuals")]
    [Description(@"The glyph shown on the overflow scroll up row when OverflowScrollUseArrows is true.")]
    [DefaultValue(DEFAULT_OVERFLOW_SCROLL_UP_ARROW)]
    [RefreshProperties(RefreshProperties.All)]
    public string OverflowScrollUpArrow { get; set; }

    /// <summary>Gets or sets the glyph shown on the overflow scroll down row when <see cref="OverflowScrollUseArrows"/> is true.</summary>
    [Category(@"Visuals")]
    [Description(@"The glyph shown on the overflow scroll down row when OverflowScrollUseArrows is true.")]
    [DefaultValue(DEFAULT_OVERFLOW_SCROLL_DOWN_ARROW)]
    [RefreshProperties(RefreshProperties.All)]
    public string OverflowScrollDownArrow { get; set; }

    #endregion

    #region Internal

    /// <summary>
    /// Gets the label for an overflow scroll row.
    /// </summary>
    /// <param name="scrollUp">True for scroll up; otherwise scroll down.</param>
    /// <param name="useArrows">True to use arrow glyphs; otherwise use text labels.</param>
    /// <returns>Display text for the overflow scroll row.</returns>
    internal string GetOverflowScrollText(bool scrollUp, bool useArrows) =>
        useArrows
            ? scrollUp ? OverflowScrollUpArrow : OverflowScrollDownArrow
            : scrollUp ? OverflowScrollUp : OverflowScrollDown;

    #endregion
}
