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

namespace Krypton.Toolkit;

internal class KryptonSplitContainerDesigner : ParentControlDesigner
{
    #region Instance Fields
    private KryptonSplitContainer? _splitContainer;
    private IDesignerHost? _designerHost;
    private ISelectionService? _selectionService;
    private BehaviorService? _behaviorService;
    private Adorner _adorner;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate with the designer.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

        // Acquire service interfaces
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
        _selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
        _behaviorService = GetService(typeof(BehaviorService)) as BehaviorService;

        // Remember the actual control being designed
        _splitContainer = component as KryptonSplitContainer;

        // Create a new adorner and add our splitter glyph
        _adorner = new Adorner();
        _adorner.Glyphs.Add(new KryptonSplitContainerGlyph(_selectionService!, _behaviorService!, _adorner, this));
        _behaviorService?.Adorners.Add(_adorner);

        // Let the two panels in the container be designable
        if (_splitContainer != null)
        {
            EnableDesignMode(_splitContainer.Panel1, "Panel1");
            EnableDesignMode(_splitContainer.Panel2, "Panel2");
        }
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing)
            {
                // Remove adorners
                _behaviorService?.Adorners.Remove(_adorner);
            }
        }
        finally
        {
            // Ensure base class is always disposed
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Indicates whether the specified control can be a child of the control managed by a designer.
    /// </summary>
    /// <param name="control">The Control to test.</param>
    /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
    public override bool CanParent(Control control) =>
        // We never allow anything to be added to the split container
        false;

    /// <summary>
    /// Returns the internal control designer with the specified index in the ControlDesigner.
    /// </summary>
    /// <param name="internalControlIndex">A specified index to select the internal control designer. This index is zero-based.</param>
    /// <returns>A ControlDesigner at the specified index.</returns>
    public override ControlDesigner? InternalControlDesigner(int internalControlIndex)
    {
        if (_splitContainer != null)
        {
            switch (internalControlIndex)
            {
                // Get the control designer for the requested indexed child control
                case 0:
                    return _designerHost?.GetDesigner(_splitContainer.Panel1) as ControlDesigner;
                case 1:
                    return _designerHost?.GetDesigner(_splitContainer.Panel2) as ControlDesigner;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns the number of internal control designers in the ControlDesigner.
    /// </summary>
    /// <returns>The number of internal control designers in the ControlDesigner.</returns>
    public override int NumberOfInternalControlDesigners() => _splitContainer != null ? 2 : 0;

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            DesignerActionListCollection actionList = new DesignerActionListCollection();

            actionList.Add(new KryptonSplitContainerActionList(this));

            return actionList;

            /*DesignerActionListCollection actionLists = [new KryptonSplitContainerActionList(this)];

            return actionLists;*/
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the DragEnter event.
    /// </summary>
    /// <param name="de">A DragEventArgs that contains the event data.</param>
    protected override void OnDragEnter(DragEventArgs de) =>
        // Prevent user dragging a toolbox control onto the control
        de.Effect = DragDropEffects.None;
    #endregion
}