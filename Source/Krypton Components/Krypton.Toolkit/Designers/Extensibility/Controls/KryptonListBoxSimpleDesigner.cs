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
/// Simplified designer for KryptonListBox optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonListBoxSimpleDesigner : ControlDesigner
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
                actionLists.Add(new KryptonListBoxSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonListBox optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonListBoxSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonListBox _listBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonListBoxSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonListBoxSimpleActionList(IComponent component)
        : base(component)
    {
        _listBox = (KryptonListBox)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the list box selected index.
    /// </summary>
    [Category("Behavior")]
    [Description("Selected index")]
    public int SelectedIndex
    {
        get => _listBox.SelectedIndex;
        set
        {
            if (_listBox.SelectedIndex != value)
            {
                _listBox.SelectedIndex = value;
                NotifyPropertyChanged("SelectedIndex", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the list box selection mode.
    /// </summary>
    [Category("Behavior")]
    [Description("Selection mode")]
    public SelectionMode SelectionMode
    {
        get => _listBox.SelectionMode;
        set
        {
            if (_listBox.SelectionMode != value)
            {
                _listBox.SelectionMode = value;
                NotifyPropertyChanged("SelectionMode", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the list box sorted setting.
    /// </summary>
    [Category("Behavior")]
    [Description("Sorted")]
    public bool Sorted
    {
        get => _listBox.Sorted;
        set
        {
            if (_listBox.Sorted != value)
            {
                _listBox.Sorted = value;
                NotifyPropertyChanged("Sorted", value);
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
        get => _listBox.PaletteMode;
        set
        {
            if (_listBox.PaletteMode != value)
            {
                _listBox.PaletteMode = value;
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
            var propertyDescriptor = TypeDescriptor.GetProperties(_listBox)[propertyName];
            changeService.OnComponentChanged(_listBox, propertyDescriptor, null, value);
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

        if (_listBox != null)
        {
            // Behavior
            actions.Add(new DesignerActionHeaderItem("Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(SelectedIndex), "Selected Index", "Behavior", "Selected index"));
            actions.Add(new DesignerActionPropertyItem(nameof(SelectionMode), "Selection Mode", "Behavior", "Selection mode"));
            actions.Add(new DesignerActionPropertyItem(nameof(Sorted), "Sorted", "Behavior", "Sorted"));
            
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
