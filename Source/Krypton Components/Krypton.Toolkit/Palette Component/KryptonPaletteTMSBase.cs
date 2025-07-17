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
/// Base class for the palette TMS storage classes to derive from.
/// </summary>
public abstract class KryptonPaletteTMSBase : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteKCTBase class.
    /// </summary>
    /// <param name="internalKCT">Reference to inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    internal KryptonPaletteTMSBase([DisallowNull] KryptonInternalKCT internalKCT,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(internalKCT != null);

        InternalKCT = internalKCT!;

        // Store the provided paint notification delegate
        NeedPaint = needPaint;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets access to the internal class used to inherit values.
    /// </summary>
    internal KryptonInternalKCT InternalKCT { get; }

    #endregion
}