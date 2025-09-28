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
/// Simplified designer for KryptonSplitContainer optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonSplitContainerSimpleDesigner : ControlDesigner
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
                actionLists.Add(new KryptonSplitContainerSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonSplitContainer optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonSplitContainerSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonSplitContainer _kryptonSplitContainer;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSplitContainerSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonSplitContainerSimpleActionList(IComponent component)
        : base(component)
    {
        _kryptonSplitContainer = (KryptonSplitContainer)component;
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
        get => _kryptonSplitContainer.PaletteMode;
        set
        {
            if (_kryptonSplitContainer.PaletteMode != value)
            {
                _kryptonSplitContainer.PaletteMode = value;
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
            var propertyDescriptor = TypeDescriptor.GetProperties(_kryptonSplitContainer)[propertyName];
            changeService.OnComponentChanged(_kryptonSplitContainer, propertyDescriptor, null, value);
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

        if (_kryptonSplitContainer != null)
        {
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}