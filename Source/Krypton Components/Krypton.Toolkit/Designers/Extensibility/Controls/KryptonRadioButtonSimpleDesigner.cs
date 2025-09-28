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
/// Simplified designer for KryptonRadioButton optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonRadioButtonSimpleDesigner : ControlDesigner
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
                actionLists.Add(new KryptonRadioButtonSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonRadioButton optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonRadioButtonSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonRadioButton _radioButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRadioButtonSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonRadioButtonSimpleActionList(IComponent component)
        : base(component)
    {
        _radioButton = (KryptonRadioButton)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the radio button text.
    /// </summary>
    [Category("Appearance")]
    [Description("Radio button text")]
    public string Text
    {
        get => _radioButton.Text;
        set
        {
            if (_radioButton.Text != value)
            {
                _radioButton.Text = value;
                NotifyPropertyChanged("Text", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the radio button checked state.
    /// </summary>
    [Category("Behavior")]
    [Description("Radio button checked state")]
    public bool Checked
    {
        get => _radioButton.Checked;
        set
        {
            if (_radioButton.Checked != value)
            {
                _radioButton.Checked = value;
                NotifyPropertyChanged("Checked", value);
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
        get => _radioButton.PaletteMode;
        set
        {
            if (_radioButton.PaletteMode != value)
            {
                _radioButton.PaletteMode = value;
                NotifyPropertyChanged("PaletteMode", value);
            }
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Notify that a property has changed.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    /// <param name="value">New value of the property.</param>
    private void NotifyPropertyChanged(string propertyName, object? value)
    {
        var changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        if (changeService != null)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(_radioButton)[propertyName];
            changeService.OnComponentChanged(_radioButton, propertyDescriptor, null, value);
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

        if (_radioButton != null)
        {
            // Appearance
            actions.Add(new DesignerActionHeaderItem("Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Radio button text"));
            
            // Behavior
            actions.Add(new DesignerActionHeaderItem("Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), "Checked", "Behavior", "Radio button checked state"));
            
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
