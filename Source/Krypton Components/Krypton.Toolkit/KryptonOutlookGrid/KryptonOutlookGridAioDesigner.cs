namespace Krypton.Toolkit
{
    /// <summary>
    /// Provides a designer for the KryptonExtraGrid control, ensuring
    /// internal controls are designable but not directly removable.
    /// </summary>
    internal class KryptonOutlookGridAioDesigner : KryptonHeaderGroupDesigner
    {
        private KryptonOutlookGridAio? _extraGrid;

        #region Public

        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize([DisallowNull] IComponent component)
        {
            // Call the base designer's Initialize FIRST.
            // This ensures the Panel is made designable by KryptonHeaderGroupDesigner.
            base.Initialize(component);

            Debug.Assert(component != null);

            _extraGrid = component as KryptonOutlookGridAio;

            if (_extraGrid != null && _extraGrid.OutlookGrid != null)
            {
                EnableDesignMode(_extraGrid.OutlookGrid, "OutlookGrid");
            }
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents
        {
            get
            {
                ArrayList list = new(base.AssociatedComponents);

                if (_extraGrid != null)
                {
                    list.Add(_extraGrid.OutlookGrid);
                    list.Add(_extraGrid.SearchToolBar);
                    list.Add(_extraGrid.GroupBox);
                    list.Add(_extraGrid.SummaryGrid);
                }
                return list;
            }
        }

        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create a collection of action lists
                var actionLists = new DesignerActionListCollection
                {
                    // Add the header group specific list
                    new KryptonOutlookGridAioActionList(this)
                };

                return actionLists;
            }
        }

        #endregion Public

    }

}
