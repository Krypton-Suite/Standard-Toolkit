#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonPropertyGridActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonPropertyGrid _propertyGrid;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    public KryptonPropertyGridActionList(KryptonPropertyGridDesigner owner) : base(owner.Component)
    {
        _propertyGrid = (owner.Component as KryptonPropertyGrid)!;

        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    public object SelectedObject
    {
        get => _propertyGrid.SelectedObject!;

        set
        {
            if (_propertyGrid.SelectedObject != value)
            {
                _service?.OnComponentChanged(_propertyGrid, null, _propertyGrid.SelectedObject, value);

                _propertyGrid.SelectedObject = value;
            }
        }
    }

    public object[] SelectedObjects
    {
        get => _propertyGrid.SelectedObjects;

        set
        {
            if (_propertyGrid.SelectedObjects != value)
            {
                _service?.OnComponentChanged(_propertyGrid, null, _propertyGrid.SelectedObjects, value);

                _propertyGrid.SelectedObjects = value;
            }
        }
    }

    public PropertySort PropertySort
    {
        get => _propertyGrid.PropertySort;

        set
        {
            if (_propertyGrid.PropertySort != value)
            {
                _service?.OnComponentChanged(_propertyGrid, null, _propertyGrid.PropertySort, value);

                _propertyGrid.PropertySort = value;
            }
        }
    }
    #endregion

    #region Public Override

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_propertyGrid != null)
        {
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(PropertySort), @"Property Sort", nameof(Appearance), @"Sort properties by."));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(@"SelectedObject", @"Selected Object", @"Values", @"The object to alter."));
            //actions.Add(new DesignerActionPropertyItem(@"SelectedObjects", @"Selected Objects", @"Values", @"The objects to alter."));
        }

        return actions;
    }

    #endregion
}