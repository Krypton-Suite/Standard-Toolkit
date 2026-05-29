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
/// Provide inheritance of palette element colors.
/// </summary>
public abstract class PaletteElementColorInherit : GlobalId,
    IPaletteElementColor
{
    #region IPaletteElementColor
    /// <summary>
    /// Gets the first color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor1(PaletteState state);

    /// <summary>
    /// Gets the second color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor2(PaletteState state);

    /// <summary>
    /// Gets the third color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor3(PaletteState state);

    /// <summary>
    /// Gets the fourth color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor4(PaletteState state);

    /// <summary>
    /// Gets the fifth color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor5(PaletteState state);
    #endregion
}