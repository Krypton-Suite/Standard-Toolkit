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
/// Simplified designer for KryptonCheckBox optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonCheckBoxSimpleDesigner : ControlDesigner
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
                actionLists.Add(new KryptonCheckBoxSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonCheckBox optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonCheckBoxSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonCheckBox _checkBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckBoxSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonCheckBoxSimpleActionList(IComponent component)
        : base(component)
    {
        _checkBox = (KryptonCheckBox)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the check box text.
    /// </summary>
    [Category("Appearance")]
    [Description("Check box text")]
    public string Text
    {
        get => _checkBox.Text;
        set
        {
            if (_checkBox.Text != value)
            {
                _checkBox.Text = value;
                NotifyPropertyChanged("Text", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the check box checked state.
    /// </summary>
    [Category("Behavior")]
    [Description("Check box checked state")]
    public bool Checked
    {
        get => _checkBox.Checked;
        set
        {
            if (_checkBox.Checked != value)
            {
                _checkBox.Checked = value;
                NotifyPropertyChanged("Checked", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the check box three state setting.
    /// </summary>
    [Category("Behavior")]
    [Description("Three state")]
    public bool ThreeState
    {
        get => _checkBox.ThreeState;
        set
        {
            if (_checkBox.ThreeState != value)
            {
                _checkBox.ThreeState = value;
                NotifyPropertyChanged("ThreeState", value);
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
        get => _checkBox.PaletteMode;
        set
        {
            if (_checkBox.PaletteMode != value)
            {
                _checkBox.PaletteMode = value;
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
            var propertyDescriptor = TypeDescriptor.GetProperties(_checkBox)[propertyName];
            changeService.OnComponentChanged(_checkBox, propertyDescriptor, null, value);
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

        if (_checkBox != null)
        {
            // Appearance
            actions.Add(new DesignerActionHeaderItem("Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Check box text"));
            
            // Behavior
            actions.Add(new DesignerActionHeaderItem("Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), "Checked", "Behavior", "Check box checked state"));
            actions.Add(new DesignerActionPropertyItem(nameof(ThreeState), "Three State", "Behavior", "Three state"));
            
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
