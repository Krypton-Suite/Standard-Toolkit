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

internal class KryptonRichTextBoxDesigner : ControlDesigner
{
    #region Instance Fields
    private KryptonRichTextBox? _richTextBox;
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
        _richTextBox = component as KryptonRichTextBox;

        if (_richTextBox != null)
        {
            // Hook into rich textbox events
            _richTextBox.GetViewManager()!.MouseUpProcessed += OnTextBoxMouseUp;
            _richTextBox.GetViewManager()!.DoubleClickProcessed += OnTextBoxDoubleClick;
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
            SelectionRules rules = base.SelectionRules;

            // Get access to the actual control instance
            var richTextBox = (KryptonRichTextBox)Component;

            // With multiline and auto-size we prevent the user changing the height
            if (richTextBox is { Multiline: false, AutoSize: true })
            {
                rules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
            }

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
                // Add the label specific list
                new KryptonRichTextBoxActionList(this)
            };

            return actionLists;
        }
    }

    /// <summary>
    /// Receives a call when the mouse leaves the control. 
    /// </summary>
    protected override void OnMouseLeave()
    {
        _richTextBox?.DesignerMouseLeave();

        base.OnMouseLeave();
    }
    #endregion

    #region Implementation
    private void OnTextBoxMouseUp(object? sender, MouseEventArgs e)
    {
        if ((_richTextBox != null) && (e.Button == MouseButtons.Left))
        {
            // Get any component associated with the current mouse position
            Component? component = _richTextBox.DesignerComponentFromPoint(new Point(e.X, e.Y));

            if (component != null)
            {
                // Force the layout to be updated for any change in selection
                _richTextBox.PerformLayout();

                // Select the component
                var selectionList = new ArrayList
                {
                    component
                };
                _selectionService?.SetSelectedComponents(selectionList, SelectionTypes.Auto);
            }
        }
    }

    private void OnTextBoxDoubleClick(object sender, Point pt)
    {
        // Get any component associated with the current mouse position
        Component? component = _richTextBox?.DesignerComponentFromPoint(pt);

        if (component != null)
        {
            // Get the designer for the component
            IDesigner? designer = _designerHost?.GetDesigner(component);

            // Request code for the default event be generated
            designer?.DoDefaultAction();
        }
    }
    #endregion
}