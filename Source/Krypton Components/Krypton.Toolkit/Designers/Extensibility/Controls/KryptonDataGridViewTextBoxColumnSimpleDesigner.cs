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
/// Simplified designer for KryptonDataGridViewTextBoxColumn optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonDataGridViewTextBoxColumnSimpleDesigner : ControlDesigner
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
                actionLists.Add(new KryptonDataGridViewTextBoxColumnSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonDataGridViewTextBoxColumn optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonDataGridViewTextBoxColumnSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonDataGridViewTextBoxColumn _kryptonDataGridViewTextBoxColumn;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewTextBoxColumnSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonDataGridViewTextBoxColumnSimpleActionList(IComponent component)
        : base(component)
    {
        _kryptonDataGridViewTextBoxColumn = (KryptonDataGridViewTextBoxColumn)component;
    }
    #endregion

    #region Public Properties
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
            var propertyDescriptor = TypeDescriptor.GetProperties(_kryptonDataGridViewTextBoxColumn)[propertyName];
            changeService.OnComponentChanged(_kryptonDataGridViewTextBoxColumn, propertyDescriptor, null, value);
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

        if (_kryptonDataGridViewTextBoxColumn != null)
        {
        }

        return actions;
    }
    #endregion
}