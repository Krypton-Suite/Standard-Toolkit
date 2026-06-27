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
/// Docking element for one edge of a control; creates docked and auto-hidden child collections during construction.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingEdge : DockingElementClosedCollection
{
    #region Identity
    /// <summary>
    /// Creates auto-hidden and docked child elements for <paramref name="edge"/> of <paramref name="control"/>.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="control">Control whose edge is configured for docking.</param>
    /// <param name="edge">Edge of <paramref name="control"/> represented by this element.</param>
    /// <exception cref="ArgumentNullException"><paramref name="control"/> is <see langword="null"/>.</exception>
    public KryptonDockingEdge(string name, Control control, DockingEdge edge)
        : base(name)
    {
        Control = control ?? throw new ArgumentNullException(nameof(control));
        Edge = edge;

        // Auto create elements for handling standard docked content and auto hidden content
        InternalAdd(new KryptonDockingEdgeAutoHidden(@"AutoHidden", control, edge));
        InternalAdd(new KryptonDockingEdgeDocked(@"Docked", control, edge));
    }
    #endregion

    #region Public
    /// <summary>
    /// Control whose edge this element represents in the docking hierarchy.
    /// </summary>
    public Control Control { get; }

    /// <summary>
    /// Edge of <see cref="Control"/> represented by this element and its child collections.
    /// </summary>
    public DockingEdge Edge { get; }

    #endregion

    #region Protected
    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DE";

    #endregion
}