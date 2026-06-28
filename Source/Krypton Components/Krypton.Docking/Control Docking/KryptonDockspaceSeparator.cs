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
/// Low-profile separator placed between dockspace regions on a control edge.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockspaceSeparator : KryptonSeparator
{
    #region Identity
    /// <summary>
    /// Docks and orients the separator for the given edge; when opposite is true, docks on the far side of the edge.
    /// </summary>
    /// <param name="edge">Docking edge the separator sits against.</param>
    /// <param name="opposite">When true, docks the separator on the opposite side of the edge.</param>
    public KryptonDockspaceSeparator(DockingEdge edge, bool opposite)
    {
        // Setup docking specific settings for the separator
        Dock = DockingHelper.DockStyleFromDockEdge(edge, opposite); 
        Orientation = DockingHelper.OrientationFromDockEdge(edge);
        SeparatorStyle = SeparatorStyle.LowProfile;
    }

    /// <summary>
    /// Returns a diagnostic label that includes dock and orientation values.
    /// </summary>
    /// <returns>Label identifying this separator, dock, and orientation.</returns>
    public override string ToString() => $@"KryptonDockspaceSeparator {Dock} {Orientation}";

    #endregion
}
