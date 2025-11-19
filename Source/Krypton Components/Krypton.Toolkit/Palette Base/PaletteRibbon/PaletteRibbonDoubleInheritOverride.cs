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
public class PaletteRibbonDoubleInheritOverride : PaletteRibbonDoubleInherit
{
    #region Instance Fields

    private readonly IPaletteRibbonBack _primaryBack;
    private readonly IPaletteRibbonBack _backupBack;
    private readonly IPaletteRibbonText _primaryText;
    private readonly IPaletteRibbonText _backupText;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonDoubleInheritOverride class.
    /// </summary>
    /// <param name="primaryBack">First choice inheritance background.</param>
    /// <param name="primaryText">First choice inheritance text.</param>
    /// <param name="backupBack">Backup inheritance background.</param>
    /// <param name="backupText">Backup inheritance text.</param>
    /// <param name="state">Palette state to override.</param>
    public PaletteRibbonDoubleInheritOverride([DisallowNull] IPaletteRibbonBack primaryBack,
        [DisallowNull] IPaletteRibbonText primaryText,
        [DisallowNull] IPaletteRibbonBack backupBack,
        [DisallowNull] IPaletteRibbonText backupText,
        PaletteState state) 
    {
        Debug.Assert(primaryBack is not null);
        Debug.Assert(primaryText is not null);
        Debug.Assert(backupBack is not null);
        Debug.Assert(backupText is not null);

        // Remember values
        _primaryBack = primaryBack ?? throw new ArgumentNullException(nameof(primaryBack));
        _primaryText = primaryText ?? throw new ArgumentNullException(nameof(primaryText));
        _backupBack = backupBack ?? throw new ArgumentNullException(nameof(backupBack));
        _backupText = backupText ?? throw new ArgumentNullException(nameof(backupText));

        // Default state
        Apply = false;
        Override = true;
        OverrideState = state;
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

    #region IPaletteRibbonBack
    /// <summary>
    /// Gets the method used to draw the background of a ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteRibbonBackStyle value.</returns>
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteRibbonColorStyle ret = _primaryBack.GetRibbonBackColorStyle(Override ? OverrideState : state);

            if (ret == PaletteRibbonColorStyle.Inherit)
            {
                ret = _backupBack.GetRibbonBackColorStyle(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColorStyle(state);
        }
    }

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor1(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor1(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backupBack.GetRibbonBackColor1(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor1(state);
        }
    }

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor2(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor2(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backupBack.GetRibbonBackColor2(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor2(state);
        }
    }

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor3(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor3(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backupBack.GetRibbonBackColor3(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor3(state);
        }
    }

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor4(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor4(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backupBack.GetRibbonBackColor4(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor4(state);
        }
    }

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor5(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor5(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backupBack.GetRibbonBackColor5(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor5(state);
        }
    }
    #endregion

    #region IPaletteRibbonText
    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTextColor(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryText.GetRibbonTextColor(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backupText.GetRibbonTextColor(state);
            }

            return ret;
        }
        else
        {
            return _backupText.GetRibbonTextColor(state);
        }
    }
    #endregion
}