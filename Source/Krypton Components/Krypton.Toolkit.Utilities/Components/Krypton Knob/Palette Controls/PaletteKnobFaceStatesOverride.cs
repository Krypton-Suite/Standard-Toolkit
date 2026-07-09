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
/// Allow the knob face palette to be overridden optionally.
/// </summary>
public class PaletteKnobFaceStatesOverride : GlobalId
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteKnobFaceStatesOverride class.
    /// </summary>
    /// <param name="normalStates">Normal state values.</param>
    /// <param name="overrideStates">Override state values.</param>
    /// <param name="overrideState">State to override.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public PaletteKnobFaceStatesOverride([DisallowNull] PaletteKnobRedirect normalStates,
        [DisallowNull] PaletteKnobFaceStates overrideStates,
        PaletteState overrideState)
    {
        Debug.Assert(normalStates != null);
        Debug.Assert(overrideStates != null);

        if (normalStates == null)
        {
            throw new ArgumentNullException(nameof(normalStates));
        }

        if (overrideStates == null)
        {
            throw new ArgumentNullException(nameof(overrideStates));
        }

        Face = new PaletteElementColorInheritOverride(normalStates.Face, overrideStates.Face);

        Apply = false;
        Override = true;
        OverrideState = overrideState;
    }
    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the normal and override palettes.
    /// </summary>
    /// <param name="normalStates">New normal palette.</param>
    /// <param name="overrideStates">New override palette.</param>
    public void SetPalettes(PaletteKnobRedirect normalStates,
        PaletteKnobFaceStates overrideStates) =>
        Face.SetPalettes(normalStates.Face, overrideStates.Face);

    #endregion

    #region Apply
    /// <summary>
    /// Gets and sets a value indicating if override should be applied.
    /// </summary>
    public bool Apply
    {
        get => Face.Apply;
        set => Face.Apply = value;
    }
    #endregion

    #region Override
    /// <summary>
    /// Gets and sets a value indicating if override state should be applied.
    /// </summary>
    public bool Override
    {
        get => Face.Override;
        set => Face.Override = value;
    }
    #endregion

    #region OverrideState
    /// <summary>
    /// Gets and sets the override palette state to use.
    /// </summary>
    public PaletteState OverrideState
    {
        get => Face.OverrideState;
        set => Face.OverrideState = value;
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
    public PaletteElementColorInheritOverride Face { get; }

    #endregion
}
