namespace Krypton.Toolkit
{
    internal class KryptonNumericUpDownColumnDesigner : ComponentDesigner
    {
        #region Instance Fields
        private KryptonDataGridViewNumericUpDownColumn _numericUpDown;
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
            _numericUpDown = component as KryptonDataGridViewNumericUpDownColumn;

            // Get access to the design services
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }

        #endregion

    }
}
