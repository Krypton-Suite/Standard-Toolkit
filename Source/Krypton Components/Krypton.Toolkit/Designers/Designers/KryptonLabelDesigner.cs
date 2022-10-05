namespace Krypton.Toolkit
{
    internal class KryptonLabelDesigner : ControlDesigner
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonLabelDesigner class.
        /// </summary>
        public KryptonLabelDesigner() =>
            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;

        #endregion

        #region Public Overrides
        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create a collection of action lists
                DesignerActionListCollection actionLists = new()
                {

                    // Add the label specific list
                    new KryptonLabelActionList(this)
                };

                return actionLists;
            }
        }
        #endregion
    }
}
