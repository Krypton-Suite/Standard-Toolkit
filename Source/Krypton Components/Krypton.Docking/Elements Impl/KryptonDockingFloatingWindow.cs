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

// ReSharper disable RedundantNullableFlowAttribute
// ReSharper disable VirtualMemberCallInConstructor
namespace Krypton.Docking;

/// <summary>
/// Provides docking functionality for a floating window that contains just a dockspace.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingFloatingWindow : DockingElementClosedCollection
{
    #region Instance Fields

    private readonly ObscureControl _obscure;
    private int _updateCount;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockingFloatingWindow class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="owner">Reference to form that owns the floating windows.</param>
    /// <param name="floatspace">Reference to form that will own all the floating window.</param>
    /// <param name="useMinimiseBox">Allow window to be minimised.</param>
    public KryptonDockingFloatingWindow(string? name, [DisallowNull] Form owner, [DisallowNull] KryptonDockingFloatspace floatspace, bool useMinimiseBox)
        : base(name)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        FloatspaceElement = floatspace ?? throw new ArgumentNullException(nameof(floatspace));
        FloatspaceElement.Disposed += OnDockingFloatspaceDisposed;

        // Create the actual window control and hook into events
        FloatingWindow = new KryptonFloatingWindow(owner, floatspace.FloatspaceControl, useMinimiseBox);
        FloatingWindow.WindowCloseClicked += OnFloatingWindowCloseClicked;
        FloatingWindow.WindowCaptionDragging += OnFloatingWindowCaptionDragging;
        FloatingWindow.Disposed += OnFloatingWindowDisposed;

        // Create and add a control we use to obscure the floating window client area during multi-part operations
        _obscure = new ObscureControl
        {
            Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom),
            Visible = false
        };
        FloatingWindow.Controls.Add(_obscure);

        // Add the floatspace as the only child of this collection
        InternalAdd(floatspace);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets access to the parent docking element.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IDockingElement? Parent
    {
        set
        {
            // Let base class perform standard processing
            base.Parent = value;

            // Grab the strings from the docking manager
            FloatspaceElement.UpdateStrings();
        }
    }

    /// <summary>
    /// Gets the window this element is managing.
    /// </summary>
    public KryptonFloatingWindow FloatingWindow { get; }

    /// <summary>
    /// Gets the floatspace element contained by the floating window.
    /// </summary>
    public KryptonDockingFloatspace FloatspaceElement { get; }

    /// <summary>
    /// Propagates an action request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="action">Action that is requested to be performed.</param>
    /// <param name="uniqueNames">Array of unique names of the pages the action relates to.</param>
    public override void PropogateAction(DockingPropogateAction action, string[]? uniqueNames)
    {
        switch (action)
        {
            case DockingPropogateAction.StartUpdate:
                // Only the first of several 'StartUpdate' actions needs actioning
                if (_updateCount++ == 0)
                {
                    // Do not layout the floatspace until all changes have been made
                    FloatingWindow.FloatspaceControl?.SuspendWorkspaceLayout();

                    // Place the obscuring control at the top of the z-order
                    FloatingWindow.Controls.SetChildIndex(_obscure, 0);

                    // Set obscuring control to take up entire client area and be made visible, this prevents
                    // the drawing of any control underneath it and so prevents any drawing artifacts being seen
                    // until the end of all operations resulting from the request action.
                    _obscure.SetBounds(0, 0, FloatingWindow.ClientSize.Width, FloatingWindow.ClientSize.Height);
                    _obscure.Visible = true;
                }
                break;

            case DockingPropogateAction.EndUpdate:
                // Only final matching 'EndUpdate' needs to reverse start action
                if ((_updateCount > 0) && (_updateCount-- == 1))
                {
                    FloatingWindow.FloatspaceControl?.ResumeWorkspaceLayout();
                    _obscure.Visible = false;
                }
                break;

            default:
                // Let base class perform actual requested actions
                base.PropogateAction(action, uniqueNames);
                break;
        }
    }

    /// <summary>
    /// Propagates a request for drag targets down the hierarchy of docking elements.
    /// </summary>
    /// <param name="floatingWindow">Reference to window being dragged.</param>
    /// <param name="dragData">Set of pages being dragged.</param>
    /// <param name="targets">Collection of drag targets.</param>
    public override void PropogateDragTargets(KryptonFloatingWindow? floatingWindow,
        PageDragEndData? dragData,
        DragTargetList targets)
    {
        // Can only generate targets for a floating window that is actually visible and not the one being dragged
        if (FloatingWindow is { Visible: true } && (floatingWindow != FloatingWindow))
        {
            base.PropogateDragTargets(floatingWindow, dragData, targets);
        }
    }

    /// <summary>
    /// Return the workspace cell that contains the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name for search.</param>
    /// <returns>Reference to KryptonWorkspaceCell if match found; otherwise null.</returns>
    public KryptonWorkspaceCell? CellForPage(string uniqueName) => FloatspaceElement.CellForPage(uniqueName);

    /// <summary>
    /// Ensure the provided page is selected within the cell that contains it.
    /// </summary>
    /// <param name="uniqueName">Unique name to be selected.</param>
    public void SelectPage(string uniqueName)
    {
        // Find the cell that contains the target named paged
        KryptonWorkspaceCell? cell = CellForPage(uniqueName);
        // Check that the pages collection contains the named paged
        KryptonPage? page = cell?.Pages[uniqueName];
        if (page != null)
        {
            cell!.SelectedPage = page;
        }
    }

    /// <summary>
    /// Saves docking configuration information using a provider xml writer.
    /// </summary>
    /// <param name="xmlWriter">Xml writer object.</param>
    public override void SaveElementToXml(XmlWriter xmlWriter)
    {
        // Output floating window docking element
        xmlWriter.WriteStartElement(XmlElementName);
        xmlWriter.WriteAttributeString(@"N", Name);
        xmlWriter.WriteAttributeString(@"C", Count.ToString());
        xmlWriter.WriteAttributeString(@"L", CommonHelper.PointToString(FloatingWindow.Location));
        xmlWriter.WriteAttributeString(@"S", CommonHelper.SizeToString(FloatingWindow.ClientSize));

        // Output an element per child
        foreach (IDockingElement child in this)
        {
            child.SaveElementToXml(xmlWriter);
        }

        // Terminate the workspace element
        xmlWriter.WriteFullEndElement();
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DFW";

    /// <summary>
    /// Perform docking element specific actions based on the loading xml.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages.</param>
    protected override void LoadDockingElement(XmlReader xmlReader, KryptonPageCollection pages)
    {
        // Grab the requested size and location
        Point location = CommonHelper.StringToPoint(xmlReader.GetAttribute(@"L"));
        Size clientSize = CommonHelper.StringToSize(xmlReader.GetAttribute(@"S"));

        // Find the size of the floating window borders
        var hBorders = FloatingWindow.Width - FloatingWindow.ClientSize.Width;
        var vBorders = FloatingWindow.Height - FloatingWindow.ClientSize.Height;

        // Find the monitor that has the window
        Rectangle workingArea = Screen.GetWorkingArea(new Rectangle(location, clientSize));

        // Limit client size to that which will fit inside the working area
        if (clientSize.Width > (workingArea.Width - hBorders))
        {
            clientSize.Width = workingArea.Width - hBorders;
        }

        if (clientSize.Height > (workingArea.Height - vBorders))
        {
            clientSize.Height = workingArea.Height - vBorders;
        }

        // Ensure floating window is positioned inside the working area
        if (location.X < workingArea.X)
        {
            location.X = workingArea.X;
        }
        else if ((location.X + clientSize.Width + hBorders) > workingArea.Right)
        {
            location.X = workingArea.Right - clientSize.Width - hBorders;
        }

        if (location.Y < workingArea.Y)
        {
            location.Y = workingArea.Y;
        }
        else if ((location.Y + clientSize.Height + vBorders) > workingArea.Bottom)
        {
            location.Y = workingArea.Bottom - clientSize.Height - vBorders;
        }

        // Update floating window with loaded size/position
        FloatingWindow.Location = location;
        FloatingWindow.ClientSize = clientSize;
    }
    #endregion

    #region Implementation
    private void OnFloatingWindowCloseClicked(object? sender, UniqueNamesEventArgs e)
    {
        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.CloseRequest(e.UniqueNames);
    }

    private void OnFloatingWindowCaptionDragging(object? sender, ScreenAndOffsetEventArgs e)
    {
        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.DoDragDrop(e.ScreenPoint, e.ElementOffset, null, this);
    }

    private void OnDockingFloatspaceDisposed(object? sender, EventArgs e)
    {
        // Cast to correct type and unhook event handlers so garbage collection can occur
        var floatspaceElement = sender as KryptonDockingFloatspace ?? throw new ArgumentNullException(nameof(sender));
        floatspaceElement.Disposed -= OnDockingFloatspaceDisposed;

        // Kill the floatspace window
        if (!FloatingWindow.IsDisposed)
        {
            FloatingWindow.Dispose();
        }
    }

    private void OnFloatingWindowDisposed(object? sender, EventArgs e)
    {
        // Unhook from events so the control can be garbage collected
        FloatingWindow.Disposed -= OnFloatingWindowDisposed;

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Generate event so the floating window customization can be reversed.
            var floatingWindowArgs = new FloatingWindowEventArgs(FloatingWindow, this);
            dockingManager.RaiseFloatingWindowRemoved(floatingWindowArgs);
        }

        // Remove the child floatspace control as it is no longer required
        InternalRemove(FloatspaceElement);

        // Generate event so interested parties know this element and associated window have been disposed
        Dispose();
    }
    #endregion
}