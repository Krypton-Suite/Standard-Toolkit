#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Utilities;

internal class KryptonCodeEditorDesigner : ControlDesigner
{
    #region Instance Fields

    private KryptonCodeEditor? _codeEditor;
    private IDesignerHost? _designerHost;
    private ISelectionService? _selectionService;

    #endregion

    #region Public Overrides
    
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

        // Cast to correct type
        _codeEditor = component as KryptonCodeEditor;

        // Hook into code editor events
        var viewManager = _codeEditor?.GetViewManager();
        if (viewManager != null)
        {
            viewManager.MouseUpProcessed += OnCodeEditorMouseUp;
            viewManager.DoubleClickProcessed += OnCodeEditorDoubleClick;
        }

        // Get access to the design services
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
        _selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
    }

    /// <summary>
    /// Gets the selection rules that indicate the movement capabilities of a component.
    /// </summary>
    public override SelectionRules SelectionRules
    {
        get
        {
            // Start with all edges being sizeable
            var rules = base.SelectionRules;

            // Code editor is always resizable
            return rules;
        }
    }

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the code editor specific list
                new KryptonCodeEditorActionList(this)
            };

            return actionLists;
        }
    }

    /// <summary>
    /// Receives a call when the mouse leaves the control. 
    /// </summary>
    protected override void OnMouseLeave()
    {
        _codeEditor?.DesignerMouseLeave();

        base.OnMouseLeave();
    }
    
    #endregion

    #region Implementation
    
    private void OnCodeEditorMouseUp(object? sender, MouseEventArgs e)
    {
        if ((_codeEditor != null) && (e.Button == MouseButtons.Left))
        {
            // Get any component associated with the current mouse position
            var component = _codeEditor.DesignerComponentFromPoint(new Point(e.X, e.Y));

            if (component != null)
            {
                // Force the layout to be updated for any change in selection
                _codeEditor.PerformLayout();

                // Select the component
                var selectionList = new ArrayList
                {
                    component
                };
                _selectionService?.SetSelectedComponents(selectionList, SelectionTypes.Auto);
            }
        }
    }

    private void OnCodeEditorDoubleClick(object sender, Point pt)
    {
        // Get any component associated with the current mouse position
        var component = _codeEditor?.DesignerComponentFromPoint(pt);

        if (component != null)
        {
            // Get the designer for the component
            var designer = _designerHost?.GetDesigner(component);

            // Request code for the default event be generated
            designer?.DoDefaultAction();
        }
    }
    
    #endregion
}
