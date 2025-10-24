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
/// Provides edge docking functionality for a control using child dockspace control instances.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingEdgeDocked : DockingElementClosedCollection
{
    #region Type Declaractions
    private class SeparatorToDockspace : Dictionary<KryptonDockspaceSeparator, KryptonDockingDockspace> { };
    private class DockspaceToSeparator : Dictionary<KryptonDockingDockspace, KryptonDockspaceSeparator> { };
    #endregion

    #region Static Fields
    private static readonly Size _defaultDockspaceSize = new Size(200, 200);
    #endregion

    #region Instance Fields

    private readonly SeparatorToDockspace _lookupSeparator;
    private readonly DockspaceToSeparator _lookupDockspace;
    private bool _update;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockingEdgeDocked class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="control">Reference to control that is being managed.</param>
    /// <param name="edge">Docking edge being managed.</param>
    public KryptonDockingEdgeDocked(string name, Control control, DockingEdge edge)
        : base(name)
    {
        Control = control ?? throw new ArgumentNullException(nameof(control));
        Edge = edge;
        _lookupSeparator = new SeparatorToDockspace();
        _lookupDockspace = new DockspaceToSeparator();
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

    /// <summary>
    /// Create and add a new dockspace instance to the correct edge of the owning control.
    /// </summary>
    /// <returns>Reference to docking element that handles the new dockspace.</returns>
    public KryptonDockingDockspace AppendDockspace() =>
        // Generate a unique string by creating a GUID
        AppendDockspace(CommonHelper.UniqueString);

    /// <summary>
    /// Create and add a new dockspace instance to the correct edge of the owning control.
    /// </summary>
    /// <param name="name">Initial name of the dockspace element.</param>
    /// <returns>Reference to docking element that handles the new dockspace.</returns>
    public KryptonDockingDockspace AppendDockspace(string name) => AppendDockspace(name, new Size(200, 200));

    /// <summary>
    /// Create and add a new dockspace instance to the correct edge of the owning control.
    /// </summary>
    /// <param name="name">Initial name of the dockspace element.</param>
    /// <param name="size">Initial size of the dockspace control.</param>
    /// <returns>Reference to docking element that handles the new dockspace.</returns>
    public KryptonDockingDockspace AppendDockspace(string name, Size size) => CreateAndInsertDockspace(Count, name, size);

    /// <summary>
    /// Create and insert a new dockspace instance to the correct edge of the owning control.
    /// </summary>
    /// <param name="index">Insertion index.</param>
    /// <returns>Reference to docking element that handles the new dockspace.</returns>
    public KryptonDockingDockspace InsertDockspace(int index) =>
        // Generate a unique string by creating a GUID
        InsertDockspace(index, CommonHelper.UniqueString);

    /// <summary>
    /// Create and insert a new dockspace instance to the correct edge of the owning control.
    /// </summary>
    /// <param name="index">Insertion index.</param>
    /// <param name="name">Initial name of the dockspace element.</param>
    /// <returns>Reference to docking element that handles the new dockspace.</returns>
    public KryptonDockingDockspace InsertDockspace(int index, string name) => InsertDockspace(index, name, new Size(200, 200));

    /// <summary>
    /// Create and insert a new dockspace instance to the correct edge of the owning control.
    /// </summary>
    /// <param name="index">Insertion index.</param>
    /// <param name="name">Initial name of the dockspace element.</param>
    /// <param name="size">Initial size of the dockspace control.</param>
    /// <returns>Reference to docking element that handles the new dockspace.</returns>
    public KryptonDockingDockspace InsertDockspace(int index, string name, Size size) => CreateAndInsertDockspace(index, name, size);

    /// <summary>
    /// Find a edge docked element by searching the hierarchy.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable docking edge element is required.</param>
    /// <returns>KryptonDockingEdgeDocked reference if found; otherwise false.</returns>
    public override KryptonDockingEdgeDocked FindDockingEdgeDocked(string uniqueName) => this;

    #endregion

    #region Protected
    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DED";

    /// <summary>
    /// Perform docking element specific actions for loading a child xml.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages.</param>
    /// <param name="child">Optional reference to existing child docking element.</param>
    protected override void LoadChildDockingElement(XmlReader xmlReader,
        KryptonPageCollection pages,
        IDockingElement? child)
    {
        if (child != null)
        {
            child.LoadElementFromXml(xmlReader, pages);
        }
        else
        {
            Size dockspaceSize = _defaultDockspaceSize;
            var elementSize = xmlReader.GetAttribute(@"S");

            // Cache the loading size
            if (!string.IsNullOrEmpty(elementSize))
            {
                dockspaceSize = CommonHelper.StringToSize(elementSize);
            }

            // Create a new dockspace and then reload it
            KryptonDockingDockspace dockspace = AppendDockspace(xmlReader.GetAttribute(@"N") ?? string.Empty, dockspaceSize);
            dockspace.LoadElementFromXml(xmlReader, pages);
        }
    }
    #endregion

    #region Implementation
    private KryptonDockingDockspace CreateAndInsertDockspace(int index, string name, Size size)
    {
        // Create a dockspace separator do the dockspace can be resized
        var separatorControl = new KryptonDockspaceSeparator(Edge, false);
        separatorControl.SplitterMoveRect += OnDockspaceSeparatorMoveRect;
        separatorControl.SplitterMoved += OnDockspaceSeparatorMoved;
        separatorControl.SplitterNotMoved += OnDockspaceSeparatorNotMoved;
        separatorControl.Disposed += OnDockspaceSeparatorDisposed;

        // Create and add the dockspace to the collection
        var dockspaceElement = new KryptonDockingDockspace(name, Edge, size);
        dockspaceElement.HasVisibleCells += OnDockingDockspaceHasVisibleCells;
        dockspaceElement.HasNoVisibleCells += OnDockingDockspaceHasNoVisibleCells;
        dockspaceElement.Disposed += OnDockingDockspaceDisposed;
        InternalInsert(index, dockspaceElement);

        // Create lookup associations
        _lookupSeparator.Add(separatorControl, dockspaceElement);
        _lookupDockspace.Add(dockspaceElement, separatorControl);

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Allow the dockspace and dockspace separator to be customized by event handlers
            var spaceArgs = new DockspaceEventArgs(dockspaceElement.DockspaceControl, dockspaceElement);
            var separatorArgs = new DockspaceSeparatorEventArgs(separatorControl, dockspaceElement);
            dockingManager.RaiseDockspaceAdding(spaceArgs);
            dockingManager.RaiseDockspaceSeparatorAdding(separatorArgs);
        }

        if (index == 0)
        {
            InsertAtOuterMost(separatorControl);
            InsertAtOuterMost(dockspaceElement.DockspaceControl);
        }
        else if (index == (Count - 1))
        {
            InsertAtInnerMost(dockspaceElement.DockspaceControl);
            InsertAtInnerMost(separatorControl);
        }
        else
        {
            if (this[index + 1] is KryptonDockingDockspace target)
            {
                InsertAfter(dockspaceElement.DockspaceControl, target.DockspaceControl);
                InsertAfter(separatorControl, target.DockspaceControl);
            }
        }

        return dockspaceElement;
    }

    private void OnDockingDockspaceHasNoVisibleCells(object? sender, EventArgs e)
    {
        // Cast to correct type and grab associated separator control
        var dockspaceElement = sender as KryptonDockingDockspace ?? throw new ArgumentNullException(nameof(sender));
        KryptonDockspaceSeparator separatorControl = _lookupDockspace[dockspaceElement];

        // No more visible cells so we hide the controls
        dockspaceElement.DockspaceControl.Visible = false;
        separatorControl.Visible = false;
    }

    private void OnDockspaceSeparatorMoveRect(object? sender, SplitterMoveRectMenuArgs e)
    {
        // Cast to correct type and grab associated dockspace element
        var separatorControl = sender as KryptonDockspaceSeparator ?? throw new ArgumentNullException(nameof(sender));
        KryptonDockingDockspace dockspaceElement = _lookupSeparator[separatorControl];

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Allow the movement rectangle to be modified by event handlers
            var dockspaceResizeRectArgs = new DockspaceSeparatorResizeEventArgs(separatorControl, dockspaceElement,
                FindMovementRect(dockspaceElement, e.MoveRect));
            dockingManager.RaiseDockspaceSeparatorResize(dockspaceResizeRectArgs);
            e.MoveRect = dockspaceResizeRectArgs.ResizeRect;
        }

        if (GetParentType(typeof(KryptonDockingControl)) is KryptonDockingControl c)
        {
            // Inform our owning control that an update is starting, this will prevent drawing of the control area
            c.PropogateAction(DockingPropogateAction.StartUpdate, null as string[]);
            _update = true;
        }
    }

    private void OnDockspaceSeparatorMoved(object? sender, SplitterEventArgs e)
    {
        // Cast to correct type and grab associated dockspace element
        var separatorControl = sender as KryptonDockspaceSeparator ?? throw new ArgumentNullException(nameof(sender));
        KryptonDockingDockspace dockspaceElement = _lookupSeparator[separatorControl];

        // Update with delta change
        switch (Edge)
        {
            case DockingEdge.Left:
                dockspaceElement.DockspaceControl.Width += e.SplitX;
                break;
            case DockingEdge.Right:
                dockspaceElement.DockspaceControl.Width -= e.SplitX;
                break;
            case DockingEdge.Top:
                dockspaceElement.DockspaceControl.Height += e.SplitY;
                break;
            case DockingEdge.Bottom:
                dockspaceElement.DockspaceControl.Height -= e.SplitY;
                break;
        }

        if (_update)
        {
            // Inform our owning control that the update has ended, allowing the client area to be drawn
            var c = GetParentType(typeof(KryptonDockingControl)) as KryptonDockingControl;
            c?.PropogateAction(DockingPropogateAction.EndUpdate, null as string[]);
            _update = false;
        }
    }

    private void OnDockspaceSeparatorNotMoved(object? sender, EventArgs e)
    {
        if (_update)
        {
            // Inform our owning control that the update has ended, allowing the client area to be drawn
            var c = GetParentType(typeof(KryptonDockingControl)) as KryptonDockingControl;
            c?.PropogateAction(DockingPropogateAction.EndUpdate, null as string[]);
            _update = false;
        }
    }

    private void OnDockingDockspaceHasVisibleCells(object? sender, EventArgs e)
    {
        // Cast to correct type and grab associated separator control
        var dockspaceElement = sender as KryptonDockingDockspace ?? throw new ArgumentNullException(nameof(sender));
        KryptonDockspaceSeparator separatorControl = _lookupDockspace[dockspaceElement];

        // Now have a visible cell so we show the controls
        dockspaceElement.DockspaceControl.Visible = true;
        separatorControl.Visible = true;
    }

    private void OnDockingDockspaceDisposed(object? sender, EventArgs e)
    {
        // Cast to correct type and unhook event handlers so garbage collection can occur
        var dockspaceElement = sender as KryptonDockingDockspace ?? throw new ArgumentNullException(nameof(sender));
        dockspaceElement.HasVisibleCells -= OnDockingDockspaceHasVisibleCells;
        dockspaceElement.HasNoVisibleCells -= OnDockingDockspaceHasNoVisibleCells;
        dockspaceElement.Disposed -= OnDockingDockspaceDisposed;

        // Remove the element from our child collection as it is no longer valid
        InternalRemove(dockspaceElement);

        // Ensure the matching separator is also disposed
        KryptonDockspaceSeparator separatorControl = _lookupDockspace[dockspaceElement];
        if (!separatorControl.IsDisposed)
        {
            separatorControl.Dispose();
        }

        // Remove lookup association
        _lookupDockspace.Remove(dockspaceElement);
    }

    private void OnDockspaceSeparatorDisposed(object? sender, EventArgs e)
    {
        // Unhook from events so the control can be garbage collected
        var separatorControl = sender as KryptonDockspaceSeparator ?? throw new ArgumentNullException(nameof(sender));
        separatorControl.SplitterMoveRect -= OnDockspaceSeparatorMoveRect;
        separatorControl.SplitterMoved -= OnDockspaceSeparatorMoved;
        separatorControl.SplitterNotMoved -= OnDockspaceSeparatorNotMoved;
        separatorControl.Disposed -= OnDockspaceSeparatorDisposed;

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Allow the dockspace and dockspace separator to be customized by event handlers
            var separatorArgs = new DockspaceSeparatorEventArgs(separatorControl, _lookupSeparator[separatorControl]);
            dockingManager.RaiseDockspaceSeparatorRemoved(separatorArgs);
        }

        // Remove lookup association
        _lookupSeparator.Remove(separatorControl);
    }

    private Rectangle FindMovementRect(KryptonDockingDockspace dockspaceElement, Rectangle moveRect)
    {
        // Find the available inner rectangle of our containing control
        Rectangle innerRect = DockingHelper.InnerRectangle(Control);

        // How much can we reduce the width/height of the dockspace to reach the minimum
        Size dockspaceMinimum = dockspaceElement.DockspaceControl.MinimumSize;
        var reduceWidth = Math.Max(dockspaceElement.DockspaceControl.Width - dockspaceMinimum.Width, 0);
        var reduceHeight = Math.Max(dockspaceElement.DockspaceControl.Height - dockspaceMinimum.Height, 0);

        // Get the minimum size requested for the inner area of the control
        var innerMinimum = Size.Empty;
        if (GetParentType(typeof(KryptonDockingControl)) is KryptonDockingControl dockingControl)
        {
            innerMinimum = dockingControl.InnerMinimum;
        }

        // How much can we expand the width/height of the dockspace to reach the inner minimum
        var expandWidth = Math.Max(innerRect.Width - innerMinimum.Width, 0);
        var expandHeight = Math.Max(innerRect.Height - innerMinimum.Height, 0);

        // Limit check we are not growing bigger than the maximum allowed
        Size dockspaceMaximum = dockspaceElement.DockspaceControl.MaximumSize;
        if (dockspaceMaximum.Width > 0)
        {
            expandWidth = Math.Min(expandWidth, dockspaceMaximum.Width);
        }

        if (dockspaceMaximum.Height > 0)
        {
            expandHeight = Math.Min(expandHeight, dockspaceMaximum.Height);
        }

        // Allow movement rectangle to extend inwards according to inner rectangle and outwards according to dockspace size
        var retRect = Rectangle.Empty;
        switch (Edge)
        {
            case DockingEdge.Left:
                retRect = new Rectangle(moveRect.X - reduceWidth, moveRect.Y, moveRect.Width + reduceWidth + expandWidth, moveRect.Height);
                break;
            case DockingEdge.Right:
                retRect = new Rectangle(moveRect.X - expandWidth, moveRect.Y, moveRect.Width + reduceWidth + expandWidth, moveRect.Height);
                break;
            case DockingEdge.Top:
                retRect = new Rectangle(moveRect.X, moveRect.Y - reduceHeight, moveRect.Width, moveRect.Height + reduceHeight + expandHeight);
                break;
            case DockingEdge.Bottom:
                retRect = new Rectangle(moveRect.X, moveRect.Y - expandHeight, moveRect.Width, moveRect.Height + reduceHeight + expandHeight);
                break;
        }

        // We do not allow negative width/height
        retRect.Width = Math.Max(retRect.Width, 0);
        retRect.Height = Math.Max(retRect.Height, 0);

        return retRect;
    }

    private void InsertAtInnerMost(Control c)
    {
        // Find control that we should always insert ourself before
        var insertIndex = Control.Controls.Count;
        for (var i = 0; i < Control.Controls.Count; i++)
        {
            Control test = Control.Controls[i];
            if ((test is KryptonDockspaceSeparator or KryptonDockspace or KryptonAutoHiddenPanel) 
                || ((test is KryptonAutoHiddenSlidePanel) && !test.Visible)
               )
            {
                insertIndex = i;
                break;
            }
        }

        Control.Controls.Add(c);
        Control.Controls.SetChildIndex(c, insertIndex);
        Control.Controls.SetChildIndex(c, insertIndex);
    }

    private void InsertAtOuterMost(Control c)
    {
        // Find control that we should always insert ourself after
        var insertIndex = Control.Controls.Count;
        for (var i = 0; i < Control.Controls.Count; i++)
        {
            Control test = Control.Controls[i];
            if (test is KryptonDockspace)
            {
                insertIndex = i + 1;
            }

            if ((test is KryptonAutoHiddenPanel) 
                || ((test is KryptonAutoHiddenSlidePanel) && !test.Visible)
               )
            {
                insertIndex = i;
                break;
            }
        }

        Control.Controls.Add(c);
        Control.Controls.SetChildIndex(c, insertIndex);
        Control.Controls.SetChildIndex(c, insertIndex);
    }

    private void InsertAfter(Control c, Control after)
    {
        // Find control that we should always insert ourself after
        var insertIndex = Control.Controls.Count;
        for (var i = 0; i < Control.Controls.Count; i++)
        {
            if (Control.Controls[i] == after)
            {
                insertIndex = i + 1;
                break;
            }
        }

        Control.Controls.Add(c);
        Control.Controls.SetChildIndex(c, insertIndex);
        Control.Controls.SetChildIndex(c, insertIndex);
    }
    #endregion
}