namespace Krypton.Toolkit
{
    internal class KryptonCommandActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonCommand _command;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCommandActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonCommandActionList(KryptonCommandDesigner owner)
            : base(owner.Component) =>
            // Remember the panel instance
            _command = owner.Component as KryptonCommand;

        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new();

            // This can be null when deleting a component instance at design time
            if (_command != null)
            {
            }

            return actions;
        }
        #endregion
    }
}
