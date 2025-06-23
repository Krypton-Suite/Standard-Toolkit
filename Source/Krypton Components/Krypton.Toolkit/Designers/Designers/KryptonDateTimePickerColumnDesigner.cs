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

internal class KryptonDateTimePickerColumnDesigner : ComponentDesigner
{
    #region Instance Fields
    private KryptonDataGridViewDateTimePickerColumn? _dateTimePicker;
    private IComponentChangeService? _changeService;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // Cast to correct type
        _dateTimePicker = component as KryptonDataGridViewDateTimePickerColumn;

        // Get access to the design services
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Private
    private void OnComponentRemoving(object sender, ComponentEventArgs e)
    {
        // If our control is being removed
        if ((_dateTimePicker != null) && (Equals(e.Component, _dateTimePicker)))
        {
            // Need access to host in order to delete a component
            var host = GetService(typeof(IDesignerHost)) as IDesignerHost;
        }
    }
    #endregion
}