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
/// Implement storage for a knob palette state.
/// </summary>
public class PaletteKnobStates : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteKnobStates class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteKnobStates(PaletteKnobRedirect redirect,
        NeedPaintHandler needPaint)
        : this(redirect.Face, redirect.Tick, redirect.Indicator, needPaint)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteKnobStates class.
    /// </summary>
    /// <param name="inheritFace">Source for inheriting face values.</param>
    /// <param name="inheritTick">Source for inheriting tick values.</param>
    /// <param name="inheritIndicator">Source for inheriting indicator values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteKnobStates([DisallowNull] IPaletteElementColor inheritFace,
        [DisallowNull] IPaletteElementColor inheritTick,
        [DisallowNull] IPaletteElementColor inheritIndicator,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inheritFace != null);
        Debug.Assert(inheritTick != null);
        Debug.Assert(inheritIndicator != null);

        NeedPaint = needPaint;

        Face = new PaletteElementColor(inheritFace!, needPaint);
        Tick = new PaletteElementColor(inheritTick!, needPaint);
        Indicator = new PaletteElementColor(inheritIndicator!, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Face.IsDefault &&
                                      Tick.IsDefault &&
                                      Indicator.IsDefault;

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    /// <param name="inheritFace">Source for inheriting face values.</param>
    /// <param name="inheritTick">Source for inheriting tick values.</param>
    /// <param name="inheritIndicator">Source for inheriting indicator values.</param>
    public void SetInherit(IPaletteElementColor inheritFace,
        IPaletteElementColor inheritTick,
        IPaletteElementColor inheritIndicator)
    {
        Face.SetInherit(inheritFace);
        Tick.SetInherit(inheritTick);
        Indicator.SetInherit(inheritIndicator);
    }
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        Face.PopulateFromBase(state);
        Indicator.PopulateFromBase(state);
    }
    #endregion

    #region Face
    /// <summary>
    /// Gets access to the knob face appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining knob face appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Face { get; }

    private bool ShouldSerializeFace() => !Face.IsDefault;

    #endregion

    #region Tick
    /// <summary>
    /// Gets access to the scale tick appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining scale tick and graduation label colour. Defaults to the palette control label text colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Tick { get; }

    private bool ShouldSerializeTick() => !Tick.IsDefault;

    #endregion

    #region Indicator
    /// <summary>
    /// Gets access to the value indicator appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining value indicator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Indicator { get; }

    private bool ShouldSerializeIndicator() => !Indicator.IsDefault;

    #endregion
}
