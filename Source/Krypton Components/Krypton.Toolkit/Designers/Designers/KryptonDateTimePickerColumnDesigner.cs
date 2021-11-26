#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    internal class KryptonDateTimePickerColumnDesigner : ComponentDesigner
    {
        #region Instance Fields
        private KryptonDataGridViewDateTimePickerColumn _dateTimePicker;
        private IComponentChangeService _changeService;
        #endregion

        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            // Let base class do standard stuff
            base.Initialize(component);

            Debug.Assert(component != null);

            // Cast to correct type
            _dateTimePicker = component as KryptonDataGridViewDateTimePickerColumn;

            // Get access to the design services
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents =>
            _dateTimePicker != null ? _dateTimePicker.ButtonSpecs : base.AssociatedComponents;

        #endregion

        #region Private
        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if ((_dateTimePicker != null) && (e.Component == _dateTimePicker))
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all the button spec instances
                for (var i = _dateTimePicker.ButtonSpecs.Count - 1; i >= 0; i--)
                {
                    // Get access to the indexed button spec
                    ButtonSpec spec = _dateTimePicker.ButtonSpecs[i];

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanging(_dateTimePicker, null);

                    // Perform actual removal of button spec from textbox
                    _dateTimePicker.ButtonSpecs.Remove(spec);

                    // Get host to remove it from design time
                    host.DestroyComponent(spec);

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanged(_dateTimePicker, null, null, null);
                }
            }
        }
        #endregion
    }
}
