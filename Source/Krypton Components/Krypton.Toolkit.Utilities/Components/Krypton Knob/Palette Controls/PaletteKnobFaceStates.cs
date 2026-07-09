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
/// Implement storage for a knob face-only palette state.
/// </summary>
public class PaletteKnobFaceStates : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteKnobFaceStates class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteKnobFaceStates(PaletteKnobRedirect redirect,
        NeedPaintHandler needPaint)
        : this(redirect.Face, needPaint)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteKnobFaceStates class.
    /// </summary>
    /// <param name="inheritFace">Source for inheriting face values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteKnobFaceStates([DisallowNull] IPaletteElementColor inheritFace,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inheritFace != null);

        NeedPaint = needPaint;
        Face = new PaletteElementColor(inheritFace!, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Face.IsDefault;

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    /// <param name="inheritFace">Source for inheriting face values.</param>
    public void SetInherit(IPaletteElementColor inheritFace) => Face.SetInherit(inheritFace);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state) => Face.PopulateFromBase(state);

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
}
