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
/// Resize grip between stacked <see cref="KryptonDockspace"/> controls on one edge.
/// Dock/orientation are derived from the parent edge and whether the grip faces inward.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockspaceSeparator : KryptonSeparator
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockspaceSeparator class.
    /// </summary>
    /// <param name="edge">Docking edge the separator is against.</param>
    /// <param name="opposite">Should the separator be docked against the opposite edge.</param>
    public KryptonDockspaceSeparator(DockingEdge edge, bool opposite)
    {
        // Setup docking specific settings for the separator
        Dock = DockingHelper.DockStyleFromDockEdge(edge, opposite); 
        Orientation = DockingHelper.OrientationFromDockEdge(edge);
        SeparatorStyle = SeparatorStyle.LowProfile;
    }

    /// <summary>
    /// Gets a string representation of the class.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"KryptonDockspaceSeparator {Dock} {Orientation}";

    #endregion
}