using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonDataGridViewDesigner : ControlDesigner
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonWrapLabelDesigner class.
        /// </summary>
        public KryptonDataGridViewDesigner()
        {
            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;
        }
        #endregion

        #region Public Overrides
        ///// <summary>
        /////  Gets the design-time action lists supported by the component associated with the designer.
        ///// </summary>
        //public override DesignerActionListCollection ActionLists
        //{
        //    get
        //    {
        //        // Create a collection of action lists
        //        DesignerActionListCollection actionLists = new DesignerActionListCollection();

        //        // Add the wrap label specific list
        //        actionLists.Add(new KryptonWrapLabelActionList(this));

        //        return actionLists;
        //    }
        //}
        #endregion
    }
}
