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
/// Simplified designer for KryptonDataGridViewTextBoxEditingControl optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonDataGridViewTextBoxEditingControlSimpleDesigner : ControlDesigner
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
                actionLists.Add(new KryptonDataGridViewTextBoxEditingControlSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonDataGridViewTextBoxEditingControl optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonDataGridViewTextBoxEditingControlSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonDataGridViewTextBoxEditingControl _kryptonDataGridViewTextBoxEditingControl;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewTextBoxEditingControlSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonDataGridViewTextBoxEditingControlSimpleActionList(IComponent component)
        : base(component)
    {
        _kryptonDataGridViewTextBoxEditingControl = (KryptonDataGridViewTextBoxEditingControl)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category("Visuals")]
    [Description("Palette applied to drawing")]
    public PaletteMode PaletteMode
    {
        get => _kryptonDataGridViewTextBoxEditingControl.PaletteMode;
        set
        {
            if (_kryptonDataGridViewTextBoxEditingControl.PaletteMode != value)
            {
                _kryptonDataGridViewTextBoxEditingControl.PaletteMode = value;
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
            var propertyDescriptor = TypeDescriptor.GetProperties(_kryptonDataGridViewTextBoxEditingControl)[propertyName];
            changeService.OnComponentChanged(_kryptonDataGridViewTextBoxEditingControl, propertyDescriptor, null, value);
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

        if (_kryptonDataGridViewTextBoxEditingControl != null)
        {
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}