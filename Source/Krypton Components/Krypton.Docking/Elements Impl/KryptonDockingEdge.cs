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

namespace Krypton.Docking;

/// <summary>
/// Provides docking functionality for a specific edge of a control.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingEdge : DockingElementClosedCollection
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockingEdge class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="control">Reference to control that is being managed.</param>
    /// <param name="edge">Docking edge being managed.</param>
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
    /// Gets the control this element is managing.
    /// </summary>
    public Control Control { get; }

    /// <summary>
    /// Gets the docking edge this element is managing.
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