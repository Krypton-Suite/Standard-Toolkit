namespace Krypton.Toolkit
{
    internal class KryptonCheckButtonDesigner : KryptonButtonDesigner
    {
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

                    // Add the check button specific list
                    new KryptonCheckButtonActionList(this)
                };

                return actionLists;
            }
        }
        #endregion
    }
}
