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
/// Inherit properties from primary source in preference to the backup source.
/// </summary>
public class PaletteElementColorInheritOverride : PaletteElementColorInherit
{
    #region Instance Fields

    private IPaletteElementColor _primary;
    private IPaletteElementColor _backup;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteElementColorInheritOverride class.
    /// </summary>
    /// <param name="primary">First choice inheritance.</param>
    /// <param name="backup">Backup inheritance.</param>
    public PaletteElementColorInheritOverride([DisallowNull] IPaletteElementColor primary,
        [DisallowNull] IPaletteElementColor backup)
    {
        Debug.Assert(primary != null);
        Debug.Assert(backup != null);

        // Store incoming alternatives
        _primary = primary ?? throw new ArgumentNullException(nameof(primary));
        _backup = backup ?? throw new ArgumentNullException(nameof(backup));

        // Default other state
        Apply = true;
        Override = true;
        OverrideState = PaletteState.Normal;
    }
    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the the primary and backup palettes.
    /// </summary>
    /// <param name="primary">New primary palette.</param>
    /// <param name="backup">New backup palette.</param>
    public void SetPalettes(IPaletteElementColor primary,
        IPaletteElementColor backup)
    {
        // Store incoming alternatives
        _primary = primary;
        _backup = backup;
    }
    #endregion

    #region Apply
    /// <summary>
    /// Gets and sets a value indicating if override should be applied.
    /// </summary>
    public bool Apply { get; set; }

    #endregion

    #region Override
    /// <summary>
    /// Gets and sets a value indicating if override state should be applied.
    /// </summary>
    public bool Override { get; set; }

    #endregion

    #region OverrideState
    /// <summary>
    /// Gets and sets the override palette state to use.
    /// </summary>
    public PaletteState OverrideState { get; set; }

    #endregion    

    #region IPaletteElementColor
    /// <summary>
    /// Gets the first color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor1(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetElementColor1(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetElementColor1(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetElementColor1(state);
        }
    }

    /// <summary>
    /// Gets the second color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor2(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetElementColor2(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetElementColor2(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetElementColor2(state);
        }
    }

    /// <summary>
    /// Gets the third color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor3(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetElementColor3(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetElementColor3(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetElementColor3(state);
        }
    }

    /// <summary>
    /// Gets the fourth color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor4(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetElementColor4(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetElementColor4(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetElementColor4(state);
        }
    }

    /// <summary>
    /// Gets the fifth color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor5(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetElementColor5(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetElementColor5(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetElementColor5(state);
        }
    }
    #endregion
}