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
/// Allow the knob palette to be overridden optionally.
/// </summary>
public class PaletteKnobStatesOverride : GlobalId
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteKnobStatesOverride class.
    /// </summary>
    /// <param name="normalStates">Normal state values.</param>
    /// <param name="overrideStates">Override state values.</param>
    /// <param name="overrideState">State to override.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public PaletteKnobStatesOverride([DisallowNull] PaletteKnobRedirect normalStates,
        [DisallowNull] PaletteKnobStates overrideStates,
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

        Back = normalStates.Back;
        Face = new PaletteElementColorInheritOverride(normalStates.Face, overrideStates.Face);
        Tick = new PaletteElementColorInheritOverride(normalStates.Tick, overrideStates.Tick);
        Indicator = new PaletteElementColorInheritOverride(normalStates.Indicator, overrideStates.Indicator);

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
        PaletteKnobStates overrideStates)
    {
        Face.SetPalettes(normalStates.Face, overrideStates.Face);
        Tick.SetPalettes(normalStates.Tick, overrideStates.Tick);
        Indicator.SetPalettes(normalStates.Indicator, overrideStates.Indicator);
    }
    #endregion

    #region Apply
    /// <summary>
    /// Gets and sets a value indicating if override should be applied.
    /// </summary>
    public bool Apply
    {
        get => Face.Apply;
        set
        {
            Face.Apply = value;
            Tick.Apply = value;
            Indicator.Apply = value;
        }
    }
    #endregion

    #region Override
    /// <summary>
    /// Gets and sets a value indicating if override state should be applied.
    /// </summary>
    public bool Override
    {
        get => Face.Override;
        set
        {
            Face.Override = value;
            Tick.Override = value;
            Indicator.Override = value;
        }
    }
    #endregion

    #region OverrideState
    /// <summary>
    /// Gets and sets the override palette state to use.
    /// </summary>
    public PaletteState OverrideState
    {
        get => Face.OverrideState;
        set
        {
            Face.OverrideState = value;
            Tick.OverrideState = value;
            Indicator.OverrideState = value;
        }
    }
    #endregion

    #region Back
    /// <summary>
    /// Gets access to the back appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack Back { get; }

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

    #region Tick
    /// <summary>
    /// Gets access to the scale tick appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining scale tick appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColorInheritOverride Tick { get; }

    #endregion

    #region Indicator
    /// <summary>
    /// Gets access to the value indicator appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining value indicator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColorInheritOverride Indicator { get; }

    #endregion
}
