namespace Krypton.Toolkit
{
    internal class KryptonScrollBarDesigner : ControlDesigner
    {
        #region Identity
        /// <summary>Initializes a new instance of the <see cref="KryptonScrollBarDesigner" /> class.</summary>
        public KryptonScrollBarDesigner() =>
            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;

        #endregion

        #region Public Overrides

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection actionList = new()
                {
                    new KryptonScrollBarActionList(this)
                };

                return actionList;
            }
        }

        #endregion
    }
}