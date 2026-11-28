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
/// Storage for one <see cref="KryptonRating"/> palette state (fill and empty glyph colours).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteRatingStates : Storage
{
    #region Instance Fields

    private PaletteRatingStates? _inherit;
    private Color _fill;
    private Color _empty;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteRatingStates"/> class.
    /// </summary>
    /// <param name="inherit">State to inherit from when a colour is <see cref="Color.Empty"/>. Typically <c>StateCommon</c>.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRatingStates(PaletteRatingStates? inherit, NeedPaintHandler? needPaint)
    {
        _inherit = inherit;
        NeedPaint = needPaint;
        Reset();
    }

    /// <inheritdoc />
    public override bool IsDefault => !ShouldSerializeFill() && !ShouldSerializeEmpty();

    /// <summary>
    /// Restore fill and empty colours to inherit.
    /// </summary>
    public void Reset()
    {
        ResetFill();
        ResetEmpty();
    }

    #endregion

    #region SetInherit

    /// <summary>
    /// Sets the inheritance parent used when a colour is <see cref="Color.Empty"/>.
    /// </summary>
    /// <param name="inherit">State to inherit from.</param>
    public void SetInherit(PaletteRatingStates? inherit) => _inherit = inherit;

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the filled glyph colour. Empty inherits from <c>StateCommon</c>, then the built-in gold.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Filled glyph colour. Empty inherits from StateCommon, then the built-in gold.")]
    [KryptonDefaultColor]
    public Color Fill
    {
        get => _fill;
        set
        {
            if (_fill != value)
            {
                _fill = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeFill() => !_fill.IsEmpty;

    /// <summary>
    /// Resets the Fill property to its default value.
    /// </summary>
    public void ResetFill() => Fill = Color.Empty;

    /// <summary>
    /// Gets and sets the empty glyph colour. Empty inherits from <c>StateCommon</c>, then a palette-based outline.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Empty glyph colour. Empty inherits from StateCommon, then a palette-based outline.")]
    [KryptonDefaultColor]
    public Color Empty
    {
        get => _empty;
        set
        {
            if (_empty != value)
            {
                _empty = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeEmpty() => !_empty.IsEmpty;

    /// <summary>
    /// Resets the Empty property to its default value.
    /// </summary>
    public void ResetEmpty() => Empty = Color.Empty;

    #endregion

    #region Internal

    /// <summary>
    /// Resolve the fill colour, walking inherit then <paramref name="fallback"/>.
    /// </summary>
    /// <param name="fallback">Colour used when this state and its inherit chain are empty.</param>
    /// <returns>A concrete colour.</returns>
    internal Color GetResolvedFill(Color fallback) => !_fill.IsEmpty
        ? _fill
        : _inherit?.GetResolvedFill(fallback) ?? fallback;

    /// <summary>
    /// Resolve the empty colour, walking inherit then <paramref name="fallback"/>.
    /// </summary>
    /// <param name="fallback">Colour used when this state and its inherit chain are empty.</param>
    /// <returns>A concrete colour.</returns>
    internal Color GetResolvedEmpty(Color fallback) => !_empty.IsEmpty
        ? _empty
        : _inherit?.GetResolvedEmpty(fallback) ?? fallback;

    #endregion
}
