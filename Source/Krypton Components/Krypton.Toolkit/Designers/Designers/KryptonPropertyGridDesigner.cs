namespace Krypton.Toolkit
{
    internal class KryptonPropertyGridDesigner : ControlDesigner
    {
        #region Identity
        /// <summary>Initializes a new instance of the <see cref="KryptonPropertyGridDesigner" /> class.</summary>
        public KryptonPropertyGridDesigner() => AutoResizeHandles = true;

        #endregion

        #region Public Override
        /// <summary>Gets the design-time action lists supported by the component associated with the designer.</summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection actionList = new()
                {
                    new KryptonPropertyGridActionList(this)
                };

                return actionList;
            }
        }
        #endregion
    }
}