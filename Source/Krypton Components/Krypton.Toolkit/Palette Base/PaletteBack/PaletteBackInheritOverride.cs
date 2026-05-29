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
public class PaletteBackInheritOverride : PaletteBackInherit
{
    #region Instance Fields

    private IPaletteBack _primary;
    private IPaletteBack _backup;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBackInheritOverride class.
    /// </summary>
    /// <param name="primary">First choice inheritance.</param>
    /// <param name="backup">Backup inheritance.</param>
    public PaletteBackInheritOverride([DisallowNull] IPaletteBack primary,
        [DisallowNull] IPaletteBack backup)
    {
        Debug.Assert(primary != null);
        Debug.Assert(backup != null);

        // Store incoming alternatives
        _primary = primary ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(primary)));
        _backup = backup ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(backup)));

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
    public void SetPalettes(IPaletteBack primary,
        IPaletteBack backup)
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

    #region IPaletteBack
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBackDraw(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primary.GetBackDraw(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backup.GetBackDraw(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackDraw(state);
        }
    }

    /// <summary>
    /// Gets the graphics drawing hint.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
    {
        if (Apply)
        {
            PaletteGraphicsHint ret = _primary.GetBackGraphicsHint(Override ? OverrideState : state);

            if (ret == PaletteGraphicsHint.Inherit)
            {
                ret = _backup.GetBackGraphicsHint(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackGraphicsHint(state);
        }
    }

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetBackColor1(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetBackColor1(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackColor1(state);
        }
    }

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetBackColor2(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetBackColor2(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackColor2(state);
        }
    }

    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteColorStyle ret = _primary.GetBackColorStyle(Override ? OverrideState : state);

            if (ret == PaletteColorStyle.Inherit)
            {
                ret = _backup.GetBackColorStyle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackColorStyle(state);
        }
    }

    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primary.GetBackColorAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backup.GetBackColorAlign(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackColorAlign(state);
        }
    }

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteState state)
    {
        if (Apply)
        {
            var ret = _primary.GetBackColorAngle(Override ? OverrideState : state);

            if (ret == -1)
            {
                ret = _backup.GetBackColorAngle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackColorAngle(state);
        }
    }

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteState state)
    {
        if (Apply)
        {
            Image ret = _primary.GetBackImage(Override ? OverrideState : state) ?? _backup.GetBackImage(state)!;

            return ret;
        }
        else
        {
            return _backup.GetBackImage(state);
        }
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteImageStyle ret = _primary.GetBackImageStyle(Override ? OverrideState : state);

            if (ret == PaletteImageStyle.Inherit)
            {
                ret = _backup.GetBackImageStyle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackImageStyle(state);
        }
    }

    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primary.GetBackImageAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backup.GetBackImageAlign(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBackImageAlign(state);
        }
    }
    #endregion
}