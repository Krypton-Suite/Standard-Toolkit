namespace Krypton.Toolkit
{

    #region Delegates

    /// <summary>
    /// Represents the method that will handle the <c>GroupSelectedAndOr</c> event,
    /// providing data for changes in menu button selections related to group AND/OR operations.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterGroupSelectedAndOrEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <c>GroupSelectedEnd</c> event,
    /// providing data for changes in menu button selections when a group selection ends.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterGroupSelectedEndEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <c>GroupSelectedDelete</c> event,
    /// providing data for changes in menu button selections when a group is about to be deleted.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterGroupSelectedDeleteEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <c>GroupSelectedInsert</c> event,
    /// providing data for changes in menu button selections when a group is about to be inserted.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterGroupSelectedInsertEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <c>GroupSelectedMakeSubGroup</c> event,
    /// providing data for changes in menu button selections when a group is about to be made into a subgroup.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterGroupSelectedMakeSubGroupEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e);


    /// <summary>
    /// Represents the method that will handle events related to AND/OR operations on a selected item.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterSelectedAndOrEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e);

    /// <summary>
    /// Represents the method that will handle events when the selection ends for an item.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterSelectedEndEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e);

    /// <summary>
    /// Represents the method that will handle events when a selected item is about to be deleted.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterSelectedDeleteEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e);

    /// <summary>
    /// Represents the method that will handle events when an item is about to be inserted after a selection.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterSelectedInsertEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e);

    /// <summary>
    /// Represents the method that will handle events when a selected item is about to be made into a subgroup.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterSelectedMakeSubgroupEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e);

    /// <summary>
    /// Represents the method that will handle events when a filter has changed.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterFilterChangedEventHandler(object sender, EventArgs e);


    /// <summary>
    /// Represents the method that will handle the <c>SelectionChanged</c> event,
    /// providing data for changes in menu button selections.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterSelectionChangedEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <c>SelectionChanging</c> event,
    /// providing data for menu button selections that are about to change.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs"/> that contains the event data.</param>
    public delegate void KryptonOutlookGridFilterSelectionChangingEventHandler(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e);

    #endregion Delegates

    #region Event Args Classes

    /// <summary>
    ///   The event args when the menu button selection is changed
    /// </summary>
    /// <remarks>Exposes the index of the selected value</remarks>
    public class KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs : EventArgs
    {

        #region Private Variables

        private int _NewSelectedIndex; // The index of the newly selected item
        private int _OldSelectedIndex; // The index of the item selected before the most recent one

        #endregion Private Variables

        #region Public Properties

        /// <summary>
        ///   The index of the newly selected menu item
        /// </summary>
        public int NewSelectedIndex
        {
            get
            {
                return _NewSelectedIndex;
            }
            set
            {
                this._NewSelectedIndex = value;
            }
        }

        /// <summary>
        ///   The index of the old selected menu item
        /// </summary>
        public int OldSelectedIndex
        {
            get
            {
                return _OldSelectedIndex;
            }
            set
            {
                this._OldSelectedIndex = value;
            }
        }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name="newSelectedIndex">Sets the NewSelectedIndex Property</param>
        /// <param name="oldSelectedINdex">Sets the OldSelectedIndex Property</param>
        public KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs(int newSelectedIndex, int oldSelectedINdex)
        {
            this.NewSelectedIndex = newSelectedIndex;
            this.OldSelectedIndex = oldSelectedINdex;
        }

        #endregion Constructor

    }

    /// <summary>
    ///   The event args when the menu button selection is changed
    /// </summary>
    /// <remarks>Exposes the index of the selected value</remarks>
    public class KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs : EventArgs
    {

        #region Private Variables

        private int _NewSelectedIndex; // The index of the newly selected item
        private int _OldSelectedIndex; // The index of the previously selected item
        private bool _Cancel; // Indicates whether the selection has been canceled

        #endregion Private Variables

        #region Public Properties

        /// <summary>
        ///   The index of the newly selected menu item
        /// </summary>
        public int NewSelectedIndex
        {
            get { return _NewSelectedIndex; }
            set { this._NewSelectedIndex = value; }
        }

        /// <summary>
        ///   The index of the old selected menu item
        /// </summary>
        public int OldSelectedIndex
        {
            get { return _OldSelectedIndex; }
            set { this._OldSelectedIndex = value; }
        }

        /// <summary>
        ///   Whether to cancel the selection changing event
        /// </summary>
        public bool Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs(int newSelectedIndex, int oldSelectedINdex)
        {
            this.NewSelectedIndex = newSelectedIndex;
            this.OldSelectedIndex = oldSelectedINdex;
            this.Cancel = false;
        }

        #endregion Constructor

    }

    #endregion Event Args Classes

}
