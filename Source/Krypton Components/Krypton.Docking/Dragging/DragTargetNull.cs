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
/// Placeholder drag target that never changes layout but satisfies the requirement for at least one target.
/// </summary>
public class DragTargetNull : DragTarget
{
    #region Identity
    /// <summary>
    /// Creates a non-interactive target with empty geometry and all page flags allowed.
    /// </summary>
    public DragTargetNull()
        : base(Rectangle.Empty, Rectangle.Empty, Rectangle.Empty, DragTargetHint.None, KryptonPageFlags.All)
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Always returns true without changing layout.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="data">Data to pass to the target to process drop.</param>
    /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
    public override bool PerformDrop(Point screenPt, PageDragEndData? data) => true;

    #endregion
}