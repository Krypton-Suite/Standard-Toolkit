#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

using System.Diagnostics;

namespace Krypton.Toolkit;

/// <summary>
/// Diagnostic designer for KryptonButton to troubleshoot .NET 8+ designer issues.
/// </summary>
internal class KryptonButtonDiagnosticDesigner : ControlDesigner
{
    #region Instance Fields
    private KryptonButton? _button;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] ActionLists called for {Component?.GetType().Name}");
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Component is null: {Component == null}");
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Button is null: {_button == null}");

            try
            {
                // Create a collection of action lists
                var actionLists = new DesignerActionListCollection();
                
                if (Component != null)
                {
                    Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Creating action list for {Component.GetType().Name}");
                    // Add the button specific list
                    actionLists.Add(new KryptonButtonDiagnosticActionList(this));
                    Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Action list added, count: {actionLists.Count}");
                }
                else
                {
                    Debug.WriteLine("[KryptonButtonDiagnosticDesigner] Component is null, cannot create action list");
                }

                return actionLists;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Exception in ActionLists: {ex.Message}");
                Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Exception stack trace: {ex.StackTrace}");
                return new DesignerActionListCollection();
            }
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The component to initialize the designer with.</param>
    public override void Initialize(IComponent component)
    {
        Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Initialize called with {component?.GetType().Name}");
        
        try
        {
            base.Initialize(component);
            
            // Remember the actual control being designed
            _button = component as KryptonButton;
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Button assigned: {_button != null}");

            if (_button != null)
            {
                Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Button text: {_button.Values.Text}");
                // Hook into button events if needed
                // _button.SomeEvent += OnSomeEvent;
            }
            
            // Check services
            var changeService = GetService(typeof(IComponentChangeService));
            var selectionService = GetService(typeof(ISelectionService));
            var designerHost = GetService(typeof(IDesignerHost));
            
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] IComponentChangeService available: {changeService != null}");
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] ISelectionService available: {selectionService != null}");
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] IDesignerHost available: {designerHost != null}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Exception in Initialize: {ex.Message}");
            Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Exception stack trace: {ex.StackTrace}");
        }
    }

    /// <summary>
    /// Handles component disposal.
    /// </summary>
    /// <param name="disposing">true if disposing managed resources</param>
    protected override void Dispose(bool disposing)
    {
        Debug.WriteLine($"[KryptonButtonDiagnosticDesigner] Dispose called with disposing={disposing}");
        
        if (disposing && _button != null)
        {
            // Unhook from button events if needed
            // _button.SomeEvent -= OnSomeEvent;
        }

        base.Dispose(disposing);
    }
    #endregion
}

/// <summary>
/// Diagnostic action list for KryptonButton to troubleshoot .NET 8+ designer issues.
/// </summary>
internal class KryptonButtonDiagnosticActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonButton? _button;
    private readonly IComponentChangeService? _changeService;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonButtonDiagnosticActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonButtonDiagnosticActionList(ComponentDesigner owner)
        : base(owner.Component)
    {
        Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Constructor called with {owner.Component?.GetType().Name}");
        
        try
        {
            // Remember the button instance
            _button = owner.Component as KryptonButton;
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Button assigned: {_button != null}");
            
            // Get change service
            _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] IComponentChangeService available: {_changeService != null}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Exception in constructor: {ex.Message}");
        }
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the button text.
    /// </summary>
    public string Text
    {
        get 
        {
            var text = _button?.Values.Text ?? "";
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Text getter called, returning: '{text}'");
            return text;
        }
        set 
        {
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Text setter called with: '{value}'");
            
            if (_button != null && _button.Values.Text != value)
            {
                try
                {
                    var oldValue = _button.Values.Text;
                    _button.Values.Text = value;
                    
                    // Notify of change
                    if (_changeService != null)
                    {
                        var propertyDescriptor = TypeDescriptor.GetProperties(_button)["Text"];
                        _changeService.OnComponentChanged(_button, propertyDescriptor, oldValue, value);
                        Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Change notification sent");
                    }
                    else
                    {
                        Debug.WriteLine($"[KryptonButtonDiagnosticActionList] No change service available");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Exception in Text setter: {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    public ButtonStyle ButtonStyle
    {
        get 
        {
            var style = _button?.ButtonStyle ?? ButtonStyle.Standalone;
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] ButtonStyle getter called, returning: {style}");
            return style;
        }
        set 
        {
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] ButtonStyle setter called with: {value}");
            
            if (_button != null && _button.ButtonStyle != value)
            {
                try
                {
                    var oldValue = _button.ButtonStyle;
                    _button.ButtonStyle = value;
                    
                    // Notify of change
                    if (_changeService != null)
                    {
                        var propertyDescriptor = TypeDescriptor.GetProperties(_button)["ButtonStyle"];
                        _changeService.OnComponentChanged(_button, propertyDescriptor, oldValue, value);
                        Debug.WriteLine($"[KryptonButtonDiagnosticActionList] ButtonStyle change notification sent");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Exception in ButtonStyle setter: {ex.Message}");
                }
            }
        }
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        Debug.WriteLine($"[KryptonButtonDiagnosticActionList] GetSortedActionItems called");
        
        try
        {
            // Create a new collection for holding the single item we want to create
            var actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_button != null)
            {
                Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Adding action items for button");
                
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Button text"));
                actions.Add(new DesignerActionPropertyItem(nameof(ButtonStyle), "Button Style", "Appearance", "Button style"));
                
                Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Added {actions.Count} action items");
            }
            else
            {
                Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Button is null, no action items added");
            }

            return actions;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Exception in GetSortedActionItems: {ex.Message}");
            Debug.WriteLine($"[KryptonButtonDiagnosticActionList] Exception stack trace: {ex.StackTrace}");
            return new DesignerActionItemCollection();
        }
    }
    #endregion
}
