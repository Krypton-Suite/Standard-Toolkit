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
/// Simplified designer for KryptonComboBox optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonComboBoxSimpleDesigner : ControlDesigner
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
                actionLists.Add(new KryptonComboBoxSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonComboBox optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonComboBoxSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonComboBox _comboBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonComboBoxSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonComboBoxSimpleActionList(IComponent component)
        : base(component)
    {
        _comboBox = (KryptonComboBox)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the combo box text.
    /// </summary>
    [Category("Appearance")]
    [Description("Combo box text")]
    public string Text
    {
        get => _comboBox.Text;
        set
        {
            if (_comboBox.Text != value)
            {
                _comboBox.Text = value;
                NotifyPropertyChanged("Text", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the combo box selected index.
    /// </summary>
    [Category("Behavior")]
    [Description("Selected index")]
    public int SelectedIndex
    {
        get => _comboBox.SelectedIndex;
        set
        {
            if (_comboBox.SelectedIndex != value)
            {
                _comboBox.SelectedIndex = value;
                NotifyPropertyChanged("SelectedIndex", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the combo box drop down style.
    /// </summary>
    [Category("Behavior")]
    [Description("Drop down style")]
    public ComboBoxStyle DropDownStyle
    {
        get => _comboBox.DropDownStyle;
        set
        {
            if (_comboBox.DropDownStyle != value)
            {
                _comboBox.DropDownStyle = value;
                NotifyPropertyChanged("DropDownStyle", value);
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
        get => _comboBox.PaletteMode;
        set
        {
            if (_comboBox.PaletteMode != value)
            {
                _comboBox.PaletteMode = value;
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
            var propertyDescriptor = TypeDescriptor.GetProperties(_comboBox)[propertyName];
            changeService.OnComponentChanged(_comboBox, propertyDescriptor, null, value);
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

        if (_comboBox != null)
        {
            // Appearance
            actions.Add(new DesignerActionHeaderItem("Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Combo box text"));
            
            // Behavior
            actions.Add(new DesignerActionHeaderItem("Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(SelectedIndex), "Selected Index", "Behavior", "Selected index"));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownStyle), "Drop Down Style", "Behavior", "Drop down style"));
            
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
