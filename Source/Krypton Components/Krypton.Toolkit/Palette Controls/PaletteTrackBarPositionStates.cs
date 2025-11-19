#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Implement storage for a track bar position only states.
/// </summary>
public class PaletteTrackBarPositionStates : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteTrackBarPositionStates class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteTrackBarPositionStates(PaletteTrackBarRedirect redirect,
        NeedPaintHandler needPaint)
        : this(redirect.Position, needPaint)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteTrackBarPositionStates class.
    /// </summary>
    /// <param name="inheritPosition">Source for inheriting position values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteTrackBarPositionStates([DisallowNull] IPaletteElementColor inheritPosition,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inheritPosition != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create storage that maps onto the inherit instances
        Position = new PaletteElementColor(inheritPosition!, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Position.IsDefault;

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    /// <param name="inheritPosition">Source for inheriting position values.</param>
    public void SetInherit(IPaletteElementColor inheritPosition) => Position.SetInherit(inheritPosition);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state) => Position.PopulateFromBase(state);

    #endregion

    #region Position
    /// <summary>
    /// Gets access to the position appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining position appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Position { get; }

    private bool ShouldSerializePosition() => !Position.IsDefault;

    #endregion
}