#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Implement storage for palette border and background.
/// </summary>
public class PaletteFormDoubleRedirect : PaletteDoubleRedirect
{
    #region Identity
    ///// <summary>
    ///// Initialize a new instance of the PaletteDoubleRedirect class.
    ///// </summary>
    ///// <param name="redirect">inheritance redirection instance.</param>
    ///// <param name="backStyle">Initial background style.</param>
    ///// <param name="borderStyle">Initial border style.</param>
    //public PaletteFormDoubleRedirect(PaletteRedirect redirect,
    //                             PaletteBackStyle backStyle,
    //                             PaletteBorderStyle borderStyle)
    //    : this(redirect, backStyle, borderStyle, null)
    //{
    //}

    /// <summary>
    /// Initialize a new instance of the PaletteDoubleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="ownerForm"></param>
    public PaletteFormDoubleRedirect(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        NeedPaintHandler? needPaint,
        VisualForm ownerForm)
    {
        // Store the inherit instances
        var backInherit = new PaletteBackInheritRedirect(redirect, backStyle);
        var borderInherit = new PaletteBorderInheritRedirect(redirect, borderStyle);

        // Create storage that maps onto the inherit instances
        var back = new PaletteBack(backInherit, needPaint);
        var border = new PaletteFormBorder(borderInherit, needPaint, ownerForm);

        Construct(redirect, back, backInherit, border, borderInherit, needPaint);
    }

    /// <summary>
    /// Initialize a new instance of the PaletteDoubleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="back">Storage for back values.</param>
    /// <param name="backInherit">inheritance for back values.</param>
    /// <param name="border">Storage for border values.</param>
    /// <param name="borderInherit">inheritance for border values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteFormDoubleRedirect(PaletteRedirect redirect,
        PaletteBack back,
        PaletteBackInheritRedirect backInherit,
        PaletteFormBorder border,
        PaletteBorderInheritRedirect borderInherit,
        NeedPaintHandler needPaint)
    {
        Construct(redirect, back, backInherit, border, borderInherit, needPaint);
    }
    #endregion

}