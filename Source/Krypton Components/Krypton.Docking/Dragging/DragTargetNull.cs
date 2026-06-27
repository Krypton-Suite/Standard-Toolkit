#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Placeholder drag target that never matches a drop location.
/// </summary>
public class DragTargetNull : DragTarget
{
    #region Identity
    /// <summary>
    /// Creates a non-matching target with empty screen, hot, and draw rectangles.
    /// </summary>
    public DragTargetNull()
        : base(Rectangle.Empty, Rectangle.Empty, Rectangle.Empty, DragTargetHint.None, KryptonPageFlags.All)
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Always reports a successful drop without relocating pages.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="data">Drag data for the attempted drop.</param>
    /// <returns>Always <see langword="true"/>.</returns>
    public override bool PerformDrop(Point screenPt, PageDragEndData? data) => true;

    #endregion
}
