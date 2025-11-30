#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Simplified designer for KryptonButton optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonButtonSimpleDesigner : ControlDesigner
{
    #region Public Overrides
    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            
            if (Component != null)
            {
                actionLists.Add(new KryptonButtonSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonButton optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonButtonSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonButton _button;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonButtonSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonButtonSimpleActionList(IComponent component)
        : base(component)
    {
        _button = (KryptonButton)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the button text.
    /// </summary>
    [Category("Appearance")]
    [Description("Button text")]
    public string Text
    {
        get => _button.Values.Text;
        set
        {
            if (_button.Values.Text != value)
            {
                _button.Values.Text = value;
                
                // Simple property change notification
                var changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (changeService != null)
                {
                    var propertyDescriptor = TypeDescriptor.GetProperties(_button)["Text"];
                    changeService.OnComponentChanged(_button, propertyDescriptor, null, value);
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    [Category("Appearance")]
    [Description("Button style")]
    public ButtonStyle ButtonStyle
    {
        get => _button.ButtonStyle;
        set
        {
            if (_button.ButtonStyle != value)
            {
                var oldValue = _button.ButtonStyle;
                _button.ButtonStyle = value;
                
                // Simple property change notification
                var changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (changeService != null)
                {
                    var propertyDescriptor = TypeDescriptor.GetProperties(_button)["ButtonStyle"];
                    changeService.OnComponentChanged(_button, propertyDescriptor, oldValue, value);
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category("Visuals")]
    [Description("Palette applied to drawing")]
    public PaletteMode PaletteMode
    {
        get => _button.PaletteMode;
        set
        {
            if (_button.PaletteMode != value)
            {
                var oldValue = _button.PaletteMode;
                _button.PaletteMode = value;
                
                // Simple property change notification
                var changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (changeService != null)
                {
                    var propertyDescriptor = TypeDescriptor.GetProperties(_button)["PaletteMode"];
                    changeService.OnComponentChanged(_button, propertyDescriptor, oldValue, value);
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
        var actions = new DesignerActionItemCollection();

        if (_button != null)
        {
            // Appearance
            actions.Add(new DesignerActionHeaderItem("Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Button text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonStyle), "Style", "Appearance", "Button style"));
            
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
