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

        #endregion

        #region Private
        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if ((_dateTimePicker != null) && (e.Component == _dateTimePicker))
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            }
        }
        #endregion
    }
}
