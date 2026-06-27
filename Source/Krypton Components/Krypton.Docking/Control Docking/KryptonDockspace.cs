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
/// Workspace hosted on a control docking edge with a default minimum size of 150×150.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockspace : KryptonSpace
{
    #region Identity
    /// <summary>
    /// Creates a dockspace space named "Docked" with a 150×150 minimum size when the embedded control does not specify one.
    /// </summary>
    /// <remarks>
    /// If minimum size is not set on the embedded control, defaults to 150×150.
    /// </remarks>
    public KryptonDockspace()
        : base(@"Docked") =>
        // Define a sensible default minimum size
        base.MinimumSize = new Size(150, 150);

    /// <summary>
    /// Returns a diagnostic label that includes the current dock assignment.
    /// </summary>
    /// <returns>Label identifying this dockspace and its dock value.</returns>
    public override string ToString() => $@"KryptonDockspace {Dock}";

    #endregion
}
