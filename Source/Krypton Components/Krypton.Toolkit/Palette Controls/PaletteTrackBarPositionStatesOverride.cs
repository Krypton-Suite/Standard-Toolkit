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
public class PaletteTrackBarPositionStatesOverride : GlobalId
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the PaletteTrackBarPositionStatesOverride class.
    /// </summary>
    /// <param name="normalStates">Normal state values.</param>
    /// <param name="overrideStates">Override state values.</param>
    /// <param name="overrideState">State to override.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public PaletteTrackBarPositionStatesOverride([DisallowNull] PaletteTrackBarRedirect normalStates,
        [DisallowNull] PaletteTrackBarPositionStates overrideStates,
        PaletteState overrideState)
    {
        Debug.Assert(normalStates != null);
        Debug.Assert(overrideStates != null);

        // Validate incoming references
        if (normalStates == null)
        {
            throw new ArgumentNullException(nameof(normalStates));
        }

        if (overrideStates == null)
        {
            throw new ArgumentNullException(nameof(overrideStates));
        }

        // Create the override instance
        Position = new PaletteElementColorInheritOverride(normalStates.Position, overrideStates.Position);

        // Do not apply an override by default
        Apply = false;

        // Always override the state
        Override = true;
        OverrideState = overrideState;
    }
    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the the normal and override palettes.
    /// </summary>
    /// <param name="normalStates">New normal palette.</param>
    /// <param name="overrideStates">New override palette.</param>
    public void SetPalettes(PaletteTrackBarRedirect normalStates,
        PaletteTrackBarPositionStates overrideStates) =>
        Position.SetPalettes(normalStates.Position, overrideStates.Position);

    #endregion

    #region Apply
    /// <summary>
    /// Gets and sets a value indicating if override should be applied.
    /// </summary>
    public bool Apply
    {
        get => Position.Apply;
        set => Position.Apply = value;
    }
    #endregion

    #region Override
    /// <summary>
    /// Gets and sets a value indicating if override state should be applied.
    /// </summary>
    public bool Override
    {
        get => Position.Override;
        set => Position.Override = value;
    }
    #endregion

    #region OverrideState
    /// <summary>
    /// Gets and sets the override palette state to use.
    /// </summary>
    public PaletteState OverrideState
    {
        get => Position.OverrideState;
        set => Position.OverrideState = value;
    }
    #endregion

    #region Position
    /// <summary>
    /// Gets access to the position appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining position appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColorInheritOverride Position { get; }

    #endregion
}