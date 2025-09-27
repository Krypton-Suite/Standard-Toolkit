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
/// Action list for the KryptonPropertyGrid control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonPropertyGridExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonPropertyGrid? _propertyGrid;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPropertyGridExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonPropertyGridExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _propertyGrid = (KryptonPropertyGrid?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the selected object.
    /// </summary>
    public object? SelectedObject
    {
        get => _propertyGrid?.SelectedObject;
        set => SetPropertyValue(_propertyGrid!, nameof(SelectedObject), SelectedObject, value, v => _propertyGrid!.SelectedObject = v);
    }

    /// <summary>
    /// Gets and sets the selected objects.
    /// </summary>
    public object[]? SelectedObjects
    {
        get => _propertyGrid?.SelectedObjects;
        set => SetPropertyValue(_propertyGrid!, nameof(SelectedObjects), SelectedObjects, value, v => _propertyGrid!.SelectedObjects = value);
    }

    /// <summary>
    /// Gets and sets the property sort order.
    /// </summary>
    public PropertySort PropertySort
    {
        get => _propertyGrid?.PropertySort ?? PropertySort.CategorizedAlphabetical;
        set => SetPropertyValue(_propertyGrid!, nameof(PropertySort), PropertySort, value, v => _propertyGrid!.PropertySort = v);
    }

    /// <summary>
    /// Gets and sets whether the property grid shows help.
    /// </summary>
    public bool HelpVisible
    {
        get => _propertyGrid?.HelpVisible ?? true;
        set => SetPropertyValue(_propertyGrid!, nameof(HelpVisible), HelpVisible, value, v => _propertyGrid!.HelpVisible = v);
    }

    /// <summary>
    /// Gets and sets whether the property grid shows tooltips.
    /// </summary>
    public bool ToolbarVisible
    {
        get => _propertyGrid?.ToolbarVisible ?? true;
        set => SetPropertyValue(_propertyGrid!, nameof(ToolbarVisible), ToolbarVisible, value, v => _propertyGrid!.ToolbarVisible = v);
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

        // This can be null when deleting a control instance at design time
        if (_propertyGrid != null)
        {
            // Add the list of PropertyGrid specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(PropertySort), @"Property Sort", @"Appearance", @"Property sort order"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(HelpVisible), @"Help Visible", @"Behavior", @"Show help"));
            actions.Add(new DesignerActionPropertyItem(nameof(ToolbarVisible), @"Toolbar Visible", @"Behavior", @"Show toolbar"));
        }

        return actions;
    }
    #endregion
}
