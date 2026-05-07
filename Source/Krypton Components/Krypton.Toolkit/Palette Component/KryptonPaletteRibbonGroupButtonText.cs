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
/// Storage for palette ribbon group button text states.
/// </summary>
public class KryptonPaletteRibbonGroupButtonText : KryptonPaletteRibbonGroupBaseText
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteRibbonGroupButtonText class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteRibbonGroupButtonText(PaletteRedirect redirect,
        NeedPaintHandler needPaint)
        : base(redirect, PaletteRibbonTextStyle.RibbonGroupButtonText, needPaint)
    {
    }
    #endregion
}