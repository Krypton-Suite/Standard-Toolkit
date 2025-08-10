namespace Krypton.Toolkit
{
    /// <summary>
    /// Represents a composite control that integrates a <see cref="KryptonOutlookGrid"/>,
    /// a <see cref="KryptonOutlookGridGroupBox"/> for grouping, and a <see cref="KryptonOutlookGridSearchToolBar"/>
    /// for search functionality, all within a <see cref="KryptonHeaderGroup"/>.
    /// </summary>
    /// <remarks>
    /// This control acts as a container for enhanced data display and interaction,
    /// providing out-of-the-box grouping and searching capabilities for the wrapped grid.
    /// It manages the layout and initial configuration of its child controls.
    /// </remarks>
    [Designer(typeof(KryptonOutlookGridAioDesigner))]
    public class KryptonOutlookGridAio : KryptonHeaderGroup
    {

        #region Private Variables

        private bool _showGrandTotalAtBottom = false;

        /// <summary>
        /// Gets or sets the <see cref="KryptonOutlookGrid"/> instance used to display summary or aggregate information.
        /// </summary>
        /// <remarks>
        /// This private grid is intended for displaying a single row of data, such as a grand total,
        /// at the bottom of the main grid. Its visibility and use are managed internally by the control,
        /// and it is linked to the <c>ShowGrandTotalAtBottom</c> property. It is not intended for direct
        /// access or serialization by the designer.
        /// </remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal KryptonOutlookGrid SummaryGrid { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <see cref="KryptonOutlookGridGroupBox"/> associated with this control.
        /// </summary>
        /// <remarks>
        /// This property is marked with <see cref="DesignerSerializationVisibility.Content"/>,
        /// meaning that the properties of the <see cref="KryptonOutlookGridGroupBox"/> instance itself
        /// will be serialized by the designer, allowing its customizable settings to be saved and loaded.
        /// This group box typically provides grouping functionality for the grid.
        /// </remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal KryptonOutlookGridGroupBox GroupBox { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <see cref="KryptonOutlookGridSearchToolBar"/> associated with this control.
        /// </summary>
        /// <remarks>
        /// This property is marked with <see cref="DesignerSerializationVisibility.Content"/>,
        /// meaning that the properties of the <see cref="KryptonOutlookGridSearchToolBar"/> instance itself
        /// will be serialized by the designer, allowing its customizable settings to be saved and loaded.
        /// This toolbar provides search capabilities for the grid.
        /// </remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal KryptonOutlookGridSearchToolBar SearchToolBar { get; set; } = default!;

        #endregion Private Variables

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the associated <see cref="KryptonOutlookGridGroupBox"/> is visible.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if the <see cref="KryptonOutlookGridGroupBox"/> is currently visible;
        ///   otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// Setting this property directly controls the <see cref="System.Windows.Forms.Control.Visible"/>
        /// state of the associated GroupBox.
        /// </remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowGroupBox
        {
            get => GroupBox.Visible;
            set => GroupBox.Visible = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the search toolbar is visible.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if the <see cref="KryptonOutlookGridSearchToolBar"/> is currently visible;
        ///   otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// Setting this property directly controls the <see cref="System.Windows.Forms.Control.Visible"/>
        /// state of the associated search toolbar.
        /// </remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowSearchToolBar
        {
            get => SearchToolBar.Visible;
            set => SearchToolBar.Visible = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="KryptonOutlookGrid"/> instance managed by this control.
        /// </summary>
        /// <remarks>
        /// This property is marked with <see cref="DesignerSerializationVisibility.Content"/>,
        /// meaning that the properties of the <see cref="KryptonOutlookGrid"/> instance itself
        /// will be serialized by the designer, allowing its customizable settings to be saved and loaded.
        /// This is the primary grid control whose appearance and behavior are being customized.
        /// </remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonOutlookGrid OutlookGrid { get; set; } = default!;

        /// <summary>
        /// Gets or sets a value indicating whether the grand total row should be displayed at the bottom of the grid.
        /// </summary>
        /// <remarks>
        /// Setting this property to <c>true</c> will make the grand total row visible at the bottom of the <see cref="OutlookGrid"/>.
        /// Setting it to <c>false</c> will hide the grand total row.
        /// When visible, the <see cref="SummaryGrid"/> is used to display the grand total.
        /// </remarks>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowGrandTotalAtBottom
        {
            get { return _showGrandTotalAtBottom; }
            set
            {
                _showGrandTotalAtBottom = value;
                if (_showGrandTotalAtBottom)
                    OutlookGrid.SummaryGrid = SummaryGrid;
                else
                    OutlookGrid.SummaryGrid = null;
            }
        }

        /// <summary>
        /// Gets the number of columns currently in the <see cref="KryptonOutlookGrid"/> control.
        /// </summary>
        /// <remarks>
        /// This property provides the total count of columns displayed in the <see cref="KryptonOutlookGrid"/>.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ColumnCount => OutlookGrid.ColumnCount;

        /// <summary>
        /// Gets the collection of columns in the <see cref="DataGridView"/> control.
        /// </summary>
        /// <remarks>
        /// This property provides access to the <see cref="DataGridViewColumnCollection"/> object
        /// that contains all the columns currently displayed in the <see cref="KryptonOutlookGrid"/>.
        /// Changes to this collection directly affect the columns visible in the grid.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewColumnCollection Columns => OutlookGrid.Columns;

        /// <summary>
        /// Gets the collection of selected columns in the <see cref="KryptonOutlookGrid"/> control.
        /// </summary>
        /// <remarks>
        /// This property provides access to the <see cref="DataGridViewSelectedColumnCollection"/> object
        /// that contains all the columns currently selected in the <see cref="KryptonOutlookGrid"/>.
        /// Note that direct column selection (by clicking column headers) might behave differently
        /// depending on the <see cref="DataGridView.SelectionMode"/> property of the underlying grid.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewSelectedColumnCollection SelectedColumns => OutlookGrid.SelectedColumns;

        /// <summary>
        /// Gets the number of rows currently in the <see cref="KryptonOutlookGrid"/> control.
        /// </summary>
        /// <remarks>
        /// This property provides the total count of rows, including any new row for data entry if enabled,
        /// in the <see cref="KryptonOutlookGrid"/>.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RowCount => OutlookGrid.RowCount;

        /// <summary>
        /// Gets the collection of rows in the <see cref="DataGridView"/> control.
        /// </summary>
        /// <remarks>
        /// This property provides access to the <see cref="DataGridViewRowCollection"/> object
        /// that contains all the rows currently in the <see cref="KryptonOutlookGrid"/>.
        /// You can add, remove, or manipulate rows through this collection.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewRowCollection Rows => OutlookGrid.Rows;

        /// <summary>
        /// Gets the collection of selected rows in the <see cref="KryptonOutlookGrid"/> control.
        /// </summary>
        /// <remarks>
        /// This property provides access to a collection of all rows currently selected in the grid.
        /// You can iterate through this collection to access individual selected rows.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewSelectedRowCollection SelectedRows => OutlookGrid.SelectedRows;

        /// <summary>
        /// Gets the current row in the <see cref="KryptonOutlookGrid"/> control.
        /// </summary>
        /// <remarks>
        /// This property provides direct access to the currently selected or active row
        /// within the <see cref="KryptonOutlookGrid"/>. If no row is current, this property will be null.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewRow CurrentRow => OutlookGrid.CurrentRow!;

        /// <summary>
        /// Gets the collection of selected cells in the <see cref="KryptonOutlookGrid"/> control.
        /// </summary>
        /// <remarks>
        /// This property provides access to a collection of all cells currently selected in the grid.
        /// You can iterate through this collection to access individual selected cells.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewSelectedCellCollection SelectedCells => OutlookGrid.SelectedCells;

        /// <summary>
        /// Gets or sets the current cell in the <see cref="KryptonOutlookGrid"/> control.
        /// </summary>
        /// <remarks>
        /// This property allows you to get or set the currently selected or active cell
        /// within the <see cref="KryptonOutlookGrid"/>. Setting this property will navigate to the specified cell.
        /// If no cell is current, this property will be null.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewCell CurrentCell
        {
            get => OutlookGrid.CurrentCell!;
            set => OutlookGrid.CurrentCell = value;
        }

        #endregion Public Properties

        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="KryptonOutlookGridAio"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor sets up the initial state and layout of the composite control.
        /// It initializes the <see cref="GroupBox"/>, <see cref="SearchToolBar"/>, and <see cref="OutlookGrid"/>
        /// child controls, adding them to the control's panel.
        /// It also establishes event subscriptions, such as `OnSearchCompleted` from the <see cref="OutlookGrid"/>
        /// to update the header based on search text.
        /// </remarks>
        public KryptonOutlookGridAio()
        {
            this.ValuesPrimary.Image = null;
            this.HeaderVisibleSecondary = false;
            this.ValuesSecondary.Heading = string.Empty;

            SearchToolBar = new KryptonOutlookGridSearchToolBar();
            GroupBox = new KryptonOutlookGridGroupBox();
            SummaryGrid = new KryptonOutlookGrid();
            OutlookGrid = new KryptonOutlookGrid();

            // Initialize GroupBox control
            GroupBox.Name = "GroupBox";
            GroupBox.Dock = DockStyle.Top;
            GroupBox.AllowDrop = true;
            GroupBox.Visible = false;

            // Initialize SearchToolBar control
            SearchToolBar.Name = "SearchToolBar";
            SearchToolBar.Dock = DockStyle.Top;
            SearchToolBar.AllowMerge = false;
            SearchToolBar.GripStyle = ToolStripGripStyle.Hidden;
            SearchToolBar.Visible = false;

            // Initialize OutlookGrid for summary
            SummaryGrid.Name = "SummaryGrid";
            SummaryGrid.Dock = DockStyle.Bottom;
            SummaryGrid.ColumnHeadersVisible = false;
            SummaryGrid.Enabled = false;
            SummaryGrid.Visible = false;
            SummaryGrid.Height = 25;

            // Initialize OutlookGrid control
            OutlookGrid.Name = "OutlookGrid";
            OutlookGrid.Dock = DockStyle.Fill;
            OutlookGrid.AllowDrop = true;
            OutlookGrid.GroupBox = GroupBox;
            OutlookGrid.ShowColumnFilter = true;
            OutlookGrid.SearchToolBar = SearchToolBar;
            OutlookGrid.EnableSearchOnKeyPress = true;

            // Add controls to the Panel of the KryptonHeaderGroup in desired Z-order
            this.Panel.Controls.Add(OutlookGrid);
            this.Panel.Controls.Add(SearchToolBar);
            this.Panel.Controls.Add(GroupBox);
            this.Panel.Controls.Add(SummaryGrid);

            // Register events after controls are set up
            OutlookGrid.RegisterGroupBoxEvents();
            OutlookGrid.OnSearchCompleted += OutlookGrid_OnSearchCompleted;
            SummaryGrid.RowHeightChanged += SummaryGrid_RowHeightChanged;
        }

        #endregion Identity

        #region Methods

        /// <summary>
        /// Handles the <see cref="KryptonOutlookGrid.OnSearchCompleted"/> event to update the secondary header.
        /// </summary>
        /// <param name="sender">The source of the event, typically the <see cref="OutlookGrid"/> instance.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// When a search operation in the <see cref="OutlookGrid"/> completes, this method updates the visibility
        /// and text of the <see cref="P:Krypton.Toolkit.HeaderGroupValues.Heading"/> based on the grid's
        /// <see cref="KryptonOutlookGrid.SearchText"/>. If there's active search text, the secondary header
        /// becomes visible and displays that text, providing feedback to the user.
        /// </remarks>
        private void OutlookGrid_OnSearchCompleted(object? sender, EventArgs e)
        {
            // Show secondary header if search text is not empty, otherwise hide it
            this.HeaderVisibleSecondary = !string.IsNullOrEmpty(OutlookGrid.SearchText);
            // Set the secondary header's heading to the current search text
            this.ValuesSecondary.Heading = OutlookGrid.SearchText;
        }

        /// <summary>
        /// Handles the <see cref="DataGridView.RowHeightChanged"/> event for the <see cref="SummaryGrid"/>.
        /// This method adjusts the height of the <see cref="SummaryGrid"/> to match the height of its first row.
        /// It also suspends and resumes layout for the grid, its parent panel, and the form
        /// to prevent flickering during the height adjustment.
        /// </summary>
        /// <param name="sender">The source of the event, typically the <see cref="SummaryGrid"/>.</param>
        /// <param name="e">A <see cref="DataGridViewRowEventArgs"/> that contains the event data,
        /// providing information about the row whose height has changed.</param>
        private void SummaryGrid_RowHeightChanged(object? sender, DataGridViewRowEventArgs e)
        {
            this.SummaryGrid.SuspendLayout();
            this.Panel.SuspendLayout();
            this.SuspendLayout();

            // Set the height of the entire DataGridView to match the height of its first row.
            SummaryGrid.Height = SummaryGrid.Rows[0].Height;

            this.SummaryGrid.ResumeLayout();
            this.Panel.ResumeLayout();
            this.ResumeLayout();
        }

        /// <summary>
        /// Sets the focus to the underlying OutlookGrid control.
        /// </summary>
        public new void Focus()
        {
            OutlookGrid.Focus();
        }

        #endregion Methods

    }
}