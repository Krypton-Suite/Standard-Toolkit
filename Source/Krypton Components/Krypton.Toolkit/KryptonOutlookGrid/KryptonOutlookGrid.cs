#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Krypton DataGridView allowing nested grouping and unlimited sorting
    /// </summary>
    /// <seealso cref="KryptonDataGridView" />
    public partial class KryptonOutlookGrid : KryptonDataGridView
    {
        #region Design Code
        private IContainer? components;

        private void InitializeComponent()
        {
            components = new Container();

        }
        #endregion

        #region Variables

        private RightToLeftLayout _rightToLeftLayout;

        private KryptonOutlookGridGroupBox? _groupBox;
        //Krypton
        private PaletteBase? _palette;
        private readonly PaletteRedirect _paletteRedirect;
        private PaletteBackInheritRedirect _paletteBack;
        private PaletteBorderInheritRedirect _paletteBorder;
        //private PaletteContentInheritRedirect _paletteContent;
        private IDisposable? _mementoBack;
        private PaletteBorder _border;

        private OutlookGridGroupCollection _groupCollection;     // List of Groups (of rows)
        private List<OutlookGridRow> _internalRows;              // List of Rows in order to keep them as is (without grouping,...)
        private HashSet<OutlookGridRow> _originalRows = default!;   // List of Rows in order to keep them as is (without grouping, filter,...)
        private readonly OutlookGridColumnCollection _internalColumns;    // List of columns in order to know if sorted, Grouped, types,...
        private int _previousGroupRowSelected = -1; //Useful to allow the selection of a group row or not when on mouse down 

        //Krypton ContextMenu for the columns header
        private KryptonContextMenu? _contextMenu;
        private KryptonContextMenuItems? _menuItems;
        private KryptonContextMenuItem _menuSortAscending = default!;
        private KryptonContextMenuItem _menuSortDescending = default!;
        private KryptonContextMenuItem _menuClearSorting = default!;
        private KryptonContextMenuSeparator _menuSeparator1 = default!;
        private KryptonContextMenuItem _menuGroupByThisColumn = default!;
        private KryptonContextMenuItem _menuUngroupByThisColumn = default!;
        private KryptonContextMenuItem _menuShowGroupBox = default!;
        private KryptonContextMenuItem _menuHideGroupBox = default!;
        private KryptonContextMenuSeparator _menuSeparator2 = default!;
        private KryptonContextMenuItem _menuBestFitColumn = default!;
        private KryptonContextMenuItem _menuBestFitAllColumns = default!;
        private KryptonContextMenuItem _menuFitColumnsToWidth = default!;
        private KryptonContextMenuSeparator _menuSeparator3 = default!;
        private KryptonContextMenuItem _menuVisibleColumns = default!;
        private KryptonContextMenuItem _menuGroupInterval = default!;
        private KryptonContextMenuItem _menuSortBySummary = default!;
        private KryptonContextMenuItem _menuExpand = default!;
        private KryptonContextMenuItem _menuCollapse = default!;
        private KryptonContextMenuSeparator _menuSeparator4 = default!;
        private KryptonContextMenuSeparator _menuSeparator5 = default!;
        private KryptonContextMenuItem _menuConditionalFormatting = default!;
        private int _colSelected = 1;         //for menu
        private const int FORMATTING_BAR_SOLID_GRADIENT_SEP_INDEX = 3;

        // Aggregation-specific menu items
        private KryptonContextMenuItem _menuAggregationNumeric = default!;
        private KryptonContextMenuItem _menuAggregationNonNumeric = default!;
        private KryptonContextMenuItem _menuFilter = default!;
        private KryptonContextMenuItem _menuClearFilter = default!;
        private KryptonContextMenuItem _menuClearAllFilter = default!;

        private KryptonContextMenuItem _menuShowSearchToolBar = default!;
        private KryptonContextMenuItem _menuHideSearchToolBar = default!;

        //For the Drag and drop of columns
        private Rectangle _dragDropRectangle;
        private int _dragDropSourceIndex;
        private int _dragDropTargetIndex;
        private int _dragDropCurrentIndex = -1;
        private int _dragDropType; //0=column, 1=row

        private bool _hideColumnOnGrouping;

        //Nodes
        private bool _showLines = true;
        internal bool InExpandCollapseMouseCapture;
        private GridFillMode _fillMode = GridFillMode.GroupsAndNodes;

        //Formatting
        private List<ConditionalFormatting> _formatConditions;

        private readonly float _factorX;
        private readonly float _factorY;

        #endregion

        #region Events

        /// <summary>
        /// Group Image Click Event
        /// </summary>
        public event EventHandler<OutlookGridGroupImageEventArgs>? GroupImageClick;
        /// <summary>
        /// Node expanding event
        /// </summary>
        public event EventHandler<ExpandingEventArgs>? NodeExpanding;
        /// <summary>
        /// Node Expanded event
        /// </summary>
        public event EventHandler<ExpandedEventArgs>? NodeExpanded;
        /// <summary>
        /// Node Collapsing Event
        /// </summary>
        public event EventHandler<CollapsingEventArgs>? NodeCollapsing;
        /// <summary>
        /// Node Collapsed event
        /// </summary>
        public event EventHandler<CollapsedEventArgs>? NodeCollapsed;

        #endregion

        #region Identity

        /// <summary>
        /// Constructor
        /// </summary>
        public KryptonOutlookGrid()
        {
            InitializeComponent();

            // very important, this indicates that a new default row class is going to be used to fill the grid
            // in this case our custom OutlookGridRow class
            base.RowTemplate = new OutlookGridRow();
            _groupCollection = new OutlookGridGroupCollection(null);
            _internalRows = [];
            //_originalRows = new List<OutlookGridRow>();
            _internalColumns = [];
            _fillMode = GridFillMode.GroupsAndNodes;

            // Cache the current global palette setting
            _palette = KryptonManager.CurrentGlobalPalette;

            // Hook into palette events
            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
            }

            // (4) We want to be notified whenever the global palette changes
            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            // Create redirection object to the base palette
            _paletteRedirect = new PaletteRedirect(_palette);

            // Create accessor objects for the back, border and content
            _paletteBack = new PaletteBackInheritRedirect(_paletteRedirect);
            _paletteBorder = new PaletteBorderInheritRedirect(_paletteRedirect);
            //_paletteContent = new PaletteContentInheritRedirect(_paletteRedirect);

            // Create storage that maps onto the inherit instances
            _border = new PaletteBorder(_paletteBorder, null);

            AllowUserToOrderColumns = false;  //we will handle it ourselves
            _hideColumnOnGrouping = false;
            _formatConditions = [];

            using (Graphics g = CreateGraphics())
            {
                _factorX = g.DpiX > 96 ? 1f * g.DpiX / 96 : 1f;
                _factorY = g.DpiY > 96 ? 1f * g.DpiY / 96 : 1f;
            }

            //Update StaticValues
            //ColumnHeadersHeight = (int)(ColumnHeadersHeight * factorY); //No need already done in KryptonDataGridView
            GlobalStaticValues.DefaultGroupRowHeight = (int)(GlobalStaticValues.DefaultGroupRowHeight * _factorY);
            GlobalStaticValues.Office2013GroupRowHeight = (int)(GlobalStaticValues.Office2013GroupRowHeight * _factorY);
            GlobalStaticValues.DefaultOffsetHeight = (int)(GlobalStaticValues.DefaultOffsetHeight * _factorY);
            GlobalStaticValues.Office2013OffsetHeight = (int)(GlobalStaticValues.DefaultOffsetHeight * _factorY);
            GlobalStaticValues.ImageOffsetWidth = (int)(GlobalStaticValues.ImageOffsetWidth * _factorX);
            GlobalStaticValues.GroupLevelMultiplier = (int)(GlobalStaticValues.GroupLevelMultiplier * _factorX);
            GlobalStaticValues.GroupImageSide = (int)(GlobalStaticValues.GroupImageSide * _factorX);

            _rightToLeftLayout = RightToLeftLayout.LeftToRight;
        }

        /// <summary>
        /// Definitely removes flickering - may not work on some systems/can cause higher CPU usage.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        #endregion OutlookGrid constructor

        #region OutlookGrid Properties

        /// <summary>
        /// Gets the RowTemplate of the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewRow RowTemplate => base.RowTemplate;

        /// <summary>
        /// Gets if the grid is grouped
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGridGrouped => !(_groupCollection.Count == 0);

        /// <summary>
        /// Gets or sets the OutlookGridGroupBox
        /// </summary>
        [Category("Behavior")]
        [Description("Associate the OutlookGridGroupBox with the grid.")]
        [DefaultValue(null)]
        public KryptonOutlookGridGroupBox? GroupBox
        {
            get => _groupBox;
            set => _groupBox = value;
        }

        /// <summary>
        /// Gets or sets the list of rows in the grid (without grouping,... for having a copy)
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<OutlookGridRow> InternalRows
        {
            get => _internalRows;
            set => _internalRows = value;
        }

        /// <summary>
        /// Gets or sets the previous selected group row
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PreviousSelectedGroupRow
        {
            get => _previousGroupRowSelected;

            set => _previousGroupRowSelected = value;
        }

        /// <summary>
        /// Gets the Krypton Palette of the OutlookGrid
        /// </summary>
        [Browsable(false)]
        public PaletteBase? GridPalette => _palette;

        /// <summary>
        /// Gets or sets the group collection.
        /// </summary>
        /// <value>OutlookGridGroupCollection.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OutlookGridGroupCollection GroupCollection
        {
            get => _groupCollection;
            set => _groupCollection = value;
        }

        /// <summary>
        /// Gets or sets the HideColumnOnGrouping property.
        /// </summary>
        /// <value>True if the column should be hidden when it is grouped, false otherwise.</value>
        [Category("Behavior")]
        [Description("Hide the column when it is grouped.")]
        [DefaultValue(false)]
        public bool HideColumnOnGrouping
        {
            get => _hideColumnOnGrouping;
            set => _hideColumnOnGrouping = value;
        }

        /// <summary>
        /// Gets or sets the conditional formatting items list.
        /// </summary>
        /// <value>
        /// The conditional formatting items list.
        /// </value>
        [Category("Behavior")]
        [Description("Conditional formatting.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<ConditionalFormatting> ConditionalFormatting
        {
            get => _formatConditions;
            set => _formatConditions = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the lines are shown between nodes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show lines]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(true)]
        public bool ShowLines
        {
            get => _showLines;
            set
            {
                if (value != _showLines)
                {
                    _showLines = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        /// <value>
        /// The fill mode.
        /// </value>
        [DefaultValue(GridFillMode.GroupsAndNodes)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public GridFillMode FillMode
        {
            get => _fillMode;
            set
            {
                if (value != _fillMode)
                {
                    _fillMode = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the right-to-left layout behavior.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RightToLeftLayout RightToLeftLayout
        {
            get => _rightToLeftLayout;

            set => _rightToLeftLayout = value;
        }

        /// <summary>
        /// Gets access to the border palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides borders settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBorder Border => _border;

        /// <summary>
        /// Gets or sets a value indicating whether the column context menu is allowed.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the context menu for columns is enabled.")]
        [DefaultValue(true)]
        public bool AllowColumnContextMenu { get; set; } = true;

        #endregion OutlookGrid property definitions

        #region OutlookGrid Overrides

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_mementoBack != null)
                {
                    _mementoBack.Dispose();
                    _mementoBack = null;
                }

                // (10) Unhook from the palette events
                if (_palette != null)
                {
                    _palette.PalettePaint -= OnPalettePaint;
                    _palette = null;
                }

                // (11) Unhook from the static events, otherwise we cannot be garbage collected
                KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

                //Unhook from specific events 
                if (_groupBox != null)
                {
                    _groupBox.ColumnGroupAdded -= ColumnGroupAddedEvent;
                    _groupBox.ColumnSortChanged -= ColumnSortChangedEvent;
                    _groupBox.ColumnGroupRemoved -= ColumnGroupRemovedEvent;
                    _groupBox.ClearGrouping -= ClearGroupingEvent;
                    _groupBox.FullCollapse -= FullCollapseEvent;
                    _groupBox.FullExpand -= FullExpandEvent;
                    _groupBox.ColumnGroupOrderChanged -= ColumnGroupIndexChangedEvent;
                    _groupBox.GroupExpand -= GridGroupExpandEvent;
                    _groupBox.GroupCollapse -= GridGroupCollapseEvent;
                    _groupBox.GroupIntervalClick -= GroupIntervalClickEvent;
                    _groupBox.SortBySummaryCount -= SortBySummaryCountEvent;
                }
            }

            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="E:CellBeginEdit" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            OutlookGridRow row = (OutlookGridRow)Rows[e.RowIndex];
            if (row.IsGroupRow || row.IsSummaryRow)
            {
                e.Cancel = true;
            }
            else
            {
                base.OnCellBeginEdit(e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:CellDoubleClick" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OutlookGridRow row = (OutlookGridRow)Rows[e.RowIndex];
                if (row.IsGroupRow)
                {
                    row.Group?.Collapsed = !row.Group.Collapsed;

                    //this is a workaround to make the grid re-calculate it's contents and background bounds
                    // so the background is updated correctly.
                    // this will also invalidate the control, so it will redraw itself
                    row.Visible = false;
                    row.Visible = true;
                    return;
                }
            }
            base.OnCellDoubleClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseUp" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // used to keep extra mouse moves from selecting more rows when collapsing
            base.OnMouseUp(e);
            InExpandCollapseMouseCapture = false;
        }

        /// <summary>
        /// Raises the <see cref="E:MouseDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //stores values for drag/drop operations if necessary
            if (AllowDrop)
            {
                if (HitTest(e.X, e.Y).ColumnIndex == -1 && HitTest(e.X, e.Y).RowIndex > -1)
                {
                    //if this is a row header cell
                    if (Rows[HitTest(e.X, e.Y).RowIndex].Selected)
                    {
                        //if this row is selected
                        _dragDropType = 1;
                        Size dragSize = SystemInformation.DragSize;
                        _dragDropRectangle = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
                        _dragDropSourceIndex = HitTest(e.X, e.Y).RowIndex;
                    }
                    else
                    {
                        _dragDropRectangle = Rectangle.Empty;
                    }
                }
                else if (HitTest(e.X, e.Y).ColumnIndex > -1 && HitTest(e.X, e.Y).RowIndex == -1)
                {
                    //if this is a column header cell
                    //if (this.Columns[this.HitTest(e.X, e.Y).ColumnIndex].Selected)
                    //{
                    _dragDropType = 0;
                    _dragDropSourceIndex = HitTest(e.X, e.Y).ColumnIndex;
                    Size dragSize = SystemInformation.DragSize;
                    _dragDropRectangle = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
                    //}
                    //else
                    //{
                    //    DragDropRectangle = Rectangle.Empty;
                    //} //end if
                }
                else
                {
                    _dragDropRectangle = Rectangle.Empty;
                }
            }
            else
            {
                _dragDropRectangle = Rectangle.Empty;
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseMove" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // while we are expanding and collapsing a node mouse moves are
            // suppressed to keep selections from being messed up.
            if (!InExpandCollapseMouseCapture)
            {
                bool dragDropDone = false;
                //handles drag/drop operations
                if (AllowDrop)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left && Cursor.Current! != Cursors.SizeWE)
                    {
                        if (_dragDropRectangle != Rectangle.Empty && !_dragDropRectangle.Contains(e.X, e.Y))
                        {
                            if (_dragDropType == 0)
                            {
                                OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_dragDropSourceIndex)!;
                                string groupInterval = "";
                                string groupType = "";
                                string? groupSortBySummaryCount = "";

                                if (col.GroupingType != null)
                                {
                                    groupType = col.GroupingType.GetType().Name;
                                    if (groupType == nameof(OutlookGridDateTimeGroup))
                                    {
                                        groupInterval = ((OutlookGridDateTimeGroup)col.GroupingType).Interval.ToString();
                                    }

                                    groupSortBySummaryCount = CommonHelper.BoolToString(col.GroupingType.SortBySummaryCount);
                                }
                                //column drag/drop
                                string info =
                                    $"{col.Name}|{col.DataGridViewColumn!.HeaderText}|{col.DataGridViewColumn.HeaderCell.SortGlyphDirection}|{col.DataGridViewColumn.SortMode}|{groupType}|{groupInterval}|{groupSortBySummaryCount}";
                                DragDropEffects dropEffect = DoDragDrop(info, DragDropEffects.Move);
                                dragDropDone = true;
                            }
                            else if (_dragDropType == 1)
                            {
                                //row drag/drop
                                DragDropEffects dropEffect = DoDragDrop(Rows[_dragDropSourceIndex], DragDropEffects.Move);
                                dragDropDone = true;
                            }
                        }
                    }
                }
                base.OnMouseMove(e);
                if (dragDropDone)
                {
                    CellOver = new Point(-2, -2);//To avoid that the column header appears in a pressed state - Modification of ToolKit
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:DragLeave" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnDragLeave(EventArgs e)
        {
            if (_dragDropCurrentIndex > -1 && _dragDropType == 0)
            {
                DataGridViewColumn col = Columns[_dragDropCurrentIndex];
                if (_groupBox != null && _groupBox.Contains(col.Name))
                {
                    _dragDropCurrentIndex = -1;
                    //this.InvalidateColumn(col.Index);
                    Invalidate();
                }
                else
                {
                    _dragDropCurrentIndex = -1;
                }
            }

            base.OnDragLeave(e);
        }

        /// <summary>
        /// Raises the <see cref="E:DragOver" /> event.
        /// </summary>
        /// <param name="drgevent">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            //runs while the drag/drop is in progress
            if (AllowDrop)
            {
                drgevent.Effect = DragDropEffects.Move;
                if (_dragDropType == 0)
                {
                    //column drag/drop
                    int curCol = HitTest(PointToClient(new Point(drgevent.X, drgevent.Y)).X, PointToClient(new Point(drgevent.X, drgevent.Y)).Y).ColumnIndex;
                    if (_dragDropCurrentIndex != curCol)
                    {
                        _dragDropCurrentIndex = curCol;
                        Invalidate(); //repaint
                    }
                }
                else if (_dragDropType == 1)
                {
                    //row drag/drop
                    int curRow = HitTest(PointToClient(new Point(drgevent.X, drgevent.Y)).X, PointToClient(new Point(drgevent.X, drgevent.Y)).Y).RowIndex;
                    if (_dragDropCurrentIndex != curRow)
                    {
                        _dragDropCurrentIndex = curRow;
                        Invalidate(); //repaint
                    }
                }
            }
            base.OnDragOver(drgevent);
        }

        /// <summary>
        /// Raises the <see cref="E:DragDrop" /> event.
        /// </summary>
        /// <param name="drgevent">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            //runs after a drag/drop operation for column/row has completed
            if (AllowDrop)
            {
                if (drgevent.Effect == DragDropEffects.Move)
                {
                    Point clientPoint = PointToClient(new Point(drgevent.X, drgevent.Y));
                    if (_dragDropType == 0)
                    {
                        //if this is a column drag/drop operation
                        _dragDropTargetIndex = HitTest(clientPoint.X, clientPoint.Y).ColumnIndex;
                        if (_dragDropTargetIndex > -1 && _dragDropCurrentIndex < ColumnCount - 1)
                        {
                            _dragDropCurrentIndex = -1;
                            //*************************************************
                            //'SourceColumn' is null after the line of code
                            //below executes... Why? This works fine for rows!!
                            if (drgevent.Data!.GetData(typeof(string)) is string r)
                            {
                                string[] res = r.Split('|');
                                DataGridViewColumn? sourceColumn = Columns[res[0]];
                                //int SourceDisplayIndex = SourceColumn.DisplayIndex;
                                DataGridViewColumn targetColumn = Columns[_dragDropTargetIndex];
                                // int TargetDisplayIndex = TargetColumn.DisplayIndex;
                                if (sourceColumn != null)
                                {
                                    sourceColumn.DisplayIndex = targetColumn.DisplayIndex;

                                    //Debug
                                    List<DataGridViewColumn> listCol = new();
                                    foreach (DataGridViewColumn col in Columns)
                                    {
                                        listCol.Add(col);
                                    }

                                    foreach (DataGridViewColumn col in listCol.OrderBy(x => x.DisplayIndex))
                                    {
                                        Debug.WriteLine($@"{col.Name} {col.DisplayIndex}");
                                    }

                                    Debug.WriteLine(@"-----------------");

                                    //*************************************************
                                    //this.Columns.RemoveAt(DragDropSourceIndex);
                                    //this.Columns.Insert(DragDropTargetIndex, SourceColumn);

                                    sourceColumn.Selected = false;
                                }

                                targetColumn.Selected = false;
                            }

                            //this.Columns[DragDropTargetIndex].Selected = true;
                            CurrentCell = this[_dragDropTargetIndex, 0];
                        } //end if
                    }
                    else if (_dragDropType == 1)
                    {
                        //if this is a row drag/drop operation
                        _dragDropTargetIndex = HitTest(clientPoint.X, clientPoint.Y).RowIndex;
                        if (_dragDropTargetIndex > -1 && _dragDropCurrentIndex < RowCount - 1)
                        {
                            _dragDropCurrentIndex = -1;
                            DataGridViewRow? sourceRow = drgevent.Data?.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                            Rows.RemoveAt(_dragDropSourceIndex);
                            if (sourceRow != null)
                            {
                                Rows.Insert(_dragDropTargetIndex, sourceRow);
                            }
                            Rows[_dragDropTargetIndex].Selected = true;
                            CurrentCell = this[0, _dragDropTargetIndex];
                        }
                    }
                }
            }
            _dragDropCurrentIndex = -1;
            Invalidate();
            base.OnDragDrop(drgevent);
        }

        /// <summary>
        /// Raises the <see cref="E:CellPainting" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellPaintingEventArgs"/> instance containing the event data.</param>
        /// <remarks>Draws a line for drag and drop</remarks>
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            //draws red drag/drop target indicator lines if necessary
            if (_dragDropCurrentIndex > -1)
            {
                if (_dragDropType == 0)
                {
                    //column drag/drop
                    if (e.ColumnIndex == _dragDropCurrentIndex)// && DragDropCurrentIndex < this.ColumnCount)
                    {
                        //if this cell is in the same column as the mouse cursor
                        using (Pen p = new(Color.Red, 1))
                        {
                            e.Graphics?.DrawLine(p, e.CellBounds.Left - 1, e.CellBounds.Top, e.CellBounds.Left - 1, e.CellBounds.Bottom);
                        }
                    } //end if
                }
                else if (_dragDropType == 1)
                {
                    //row drag/drop
                    if (e.RowIndex == _dragDropCurrentIndex && _dragDropCurrentIndex < RowCount - 1)
                    {
                        //if this cell is in the same row as the mouse cursor

                        using (Pen p = new(Color.Red, 1))
                        {
                            e.Graphics?.DrawLine(p, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
                        }
                    }
                }
            }
            base.OnCellPainting(e);
            if (_highlightSearchText && _enableSearchOnKeyPress && this.ReadOnly)
            {
                PaintSearchText(e, SearchText);
            }
        }

        /// <summary>
        /// Overrides OnCellMouseEnter
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
        {
            base.OnCellMouseEnter(e);
        }

        /// <summary>
        /// Overrides OnCellMouseDown - Check if the user has clicked on +/- of a group row
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            //base.OnCellMouseDown(e); //needed.
            if (e.RowIndex < 0)
            {
                base.OnCellMouseDown(e); // To allow column resizing
                return;
            }
            OutlookGridRow row = (OutlookGridRow)Rows[e.RowIndex];
            //DebugUtilities.WriteLine("OnCellMouseDown " + DateTime.Now.Ticks.ToString() + "IsIconHit" + row.IsIconHit(e).ToString());
            if (_previousGroupRowSelected != -1 && _previousGroupRowSelected != e.RowIndex && PreviousSelectedGroupRow < Rows.Count)
            {
                InvalidateRow(PreviousSelectedGroupRow);
            }

            PreviousSelectedGroupRow = -1;
            if (row.IsGroupRow)
            {
                PreviousSelectedGroupRow = e.RowIndex;
                ClearSelection(); //unselect
                if (row.IsIconHit(e))
                {
                    row.Group?.Collapsed = !row.Group.Collapsed;
                    //this is a workaround to make the grid re-calculate it's contents and background bounds
                    // so the background is updated correctly.
                    // this will also invalidate the control, so it will redraw itself
                    row.Visible = false;
                    row.Visible = true;
                    //When collapsing the first row still seeing it.
                    if (row.Index < FirstDisplayedScrollingRowIndex)
                    {
                        FirstDisplayedScrollingRowIndex = row.Index;
                    }
                }
                else if (row.IsGroupImageHit(e))
                {
                    OnGroupImageClick(new OutlookGridGroupImageEventArgs(row));
                }
                else
                {
                    InvalidateRow(e.RowIndex);
                    if (e.ColumnIndex > -1)
                        CurrentCell = this[e.ColumnIndex, e.RowIndex];
                }
            }
            else
            {
                base.OnCellMouseDown(e);
            }
        }

        /// <summary>
        /// Overrides the <see cref="DataGridView.OnCurrentCellChanged"/> method to manage the selection state of group rows within the grid.
        /// This method ensures that when the current cell changes, any previously selected group row is properly
        /// invalidated and its selection state is cleared. It also handles the scenario where the new current cell
        /// is a group row, preventing it from being visually selected in the standard manner.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnCurrentCellChanged(EventArgs e)
        {
            // Get the newly selected row (if any)
            DataGridViewRow? currentRow = CurrentCell?.OwningRow;
            int newRowIndex = currentRow?.Index ?? -1;

            // Check if a previous group row was selected AND
            // if the current selection is different from the previously selected group row.
            if (_previousGroupRowSelected != -1 && _previousGroupRowSelected != newRowIndex)
            {
                // Ensure the previous group row index is still valid within the Rows collection
                if (_previousGroupRowSelected < Rows.Count && Rows[_previousGroupRowSelected] is OutlookGridRow previousOutlookRow)
                {
                    // Invalidate the previously selected group row to ensure it redraws without selection.
                    // You might also need to clear specific selection states if they are being held.
                    InvalidateRow(_previousGroupRowSelected);
                }
                // Reset the tracker after invalidating the old row
                _previousGroupRowSelected = -1;
            }

            // Now, if the *new* current row is a group row, update _previousGroupRowSelected
            // and handle clearing selection if that's still desired for group rows on navigation.
            if (currentRow is OutlookGridRow currentOutlookRow && currentOutlookRow.IsGroupRow)
            {
                // If the user navigates *onto* a group row with the keyboard,
                // you might still want to clear the selection or prevent it from being selected visually.
                // This depends on desired UX.
                ClearSelection(); // Clear existing selections
                _previousGroupRowSelected = newRowIndex;
                InvalidateRow(newRowIndex); // Invalidate the new group row to ensure it's drawn correctly (e.g., without standard selection highlight)
            }
            base.OnCurrentCellChanged(e);
        }

        /// <summary>
        /// Overrides OnColumnHeaderMouseClick
        /// </summary>
        /// <param name="e">DataGridViewCellMouseEventArgs</param>
        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            _colSelected = e.ColumnIndex; //To keep a track on which column we have pressed
            //runs when the mouse is clicked over a column header cell
            if (e.ColumnIndex > -1)
            {
                //this handle when _internalColumns is null or empty grid fill without internalColumns restrict outlookGrid behavior. 
                if (_internalColumns == null || _internalColumns.Count == 0 || !AllowColumnContextMenu)
                {
                    base.OnColumnHeaderMouseClick(e);
                    return;
                }
                if (e.Button == MouseButtons.Right)
                {
                    ShowColumnHeaderContextMenu(e.ColumnIndex);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    OutlookGridColumn? col = _internalColumns.FindFromColumnIndex(e.ColumnIndex) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("col"));
                    if (col.DataGridViewColumn!.SortMode != DataGridViewColumnSortMode.NotSortable)
                    {
                        SortOrder previousSort = col.SortDirection;
                        //Reset all sorting column only if not Ctrl or Shift or the column is grouped
                        if (ModifierKeys != Keys.Shift && ModifierKeys != Keys.Control && !col.IsGrouped)
                        {
                            ResetAllSortingColumns();
                        }

                        //Remove this SortIndex
                        if (ModifierKeys == Keys.Control)
                        {
                            UnSortColumn(col);
                        }
                        //Add the first or a new SortIndex
                        else
                        {
                            if (previousSort == SortOrder.None)
                            {
                                SortColumn(col, SortOrder.Ascending);
                            }
                            else
                            {
                                SortColumn(col, previousSort == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
                            }
                        }

                        // Note Can we remove this?
                        //#if DEBUG
                        //                        internalColumns.DebugOutput();
                        //#endif

                        //Refresh the groupBox if the column is grouped
                        if (col.IsGrouped)
                        {
                            ForceRefreshGroupBox();
                        }

                        //Apply the changes
                        Fill();
                    }
                }
            }
            base.OnColumnHeaderMouseClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:CellFormatting" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            //Allows to have a picture in the first column
            if (e.DesiredType?.Name == "Image" && e.Value != null && e.Value.GetType().Name != e.DesiredType.Name && e.Value.GetType().Name != "Bitmap")
            {
                e.Value = null;
            }

            base.OnCellFormatting(e);
        }

        /// <summary>
        /// Overrides the paint event
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_palette != null)
            {
                IRenderer renderer = _palette.GetRenderer();

                using (RenderContext renderContext = new(this, e.Graphics, e.ClipRectangle, renderer))
                {
                    _paletteBorder.Style = PaletteBorderStyle.HeaderPrimary;
                    renderer.RenderStandardBorder.DrawBorder(renderContext, ClientRectangle, _border, VisualOrientation.Top, PaletteState.Normal);
                }
            }

            if (this.RowHeadersVisible)
            {
                // Get the graphics object from the PaintEventArgs
                Graphics g = e.Graphics;

                // Iterate through currently visible rows
                var visibleRows = this.Rows.Cast<OutlookGridRow>().Where(r => r.Visible && r.DataGridView != null && (r.IsGroupRow || r.IsSummaryRow));
                //var visibleRows = this.Rows.Cast<OutlookGridRow>().Where(r => r.Visible && r.DataGridView != null && r.IsGroupRow);
                if (visibleRows != null)
                {
                    foreach (DataGridViewRow row in visibleRows)
                    {
                        Rectangle rowBounds = this.GetRowDisplayRectangle(row.Index, false);
                        // If the row is not visible (e.g., scrolled out of view), skip it
                        if (rowBounds.IsEmpty || !this.ClientRectangle.IntersectsWith(rowBounds))
                        {
                            continue;
                        }

                        // Calculate the fixed row header area
                        // X is always 0 (relative to grid's client area)
                        Rectangle rowHeaderPaintArea = new(0, rowBounds.Y, this.RowHeadersWidth, rowBounds.Height);

                        // Ensure it's clipped to the visible area of the grid
                        rowHeaderPaintArea.Intersect(this.ClientRectangle);

                        // Determine selection state for the group row
                        bool isSelected = (row.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
                        if (!isSelected && this.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
                        {
                            isSelected = this.CurrentCell?.RowIndex == row.Index;
                        }
                        PaletteState rowHeaderRenderingState = isSelected ? PaletteState.CheckedNormal : PaletteState.Normal;
                        IPaletteBack rowHeaderPaletteBack = isSelected ? this.StateSelected.HeaderRow.Back : this.StateNormal.HeaderRow.Back;
                        IPaletteBorder rowHeaderPaletteBorder = isSelected ? this.StateSelected.HeaderRow.Border : this.StateNormal.HeaderRow.Border;

                        // --- Drawing logic for the row header (copied from OutlookGridRow) ---
                        using (RenderContext rhRenderContext = new(this, g, rowHeaderPaintArea, this.Renderer!))
                        {
                            using (GraphicsPath rhPath = this.Renderer!.RenderStandardBorder.GetBackPath(rhRenderContext, rowHeaderPaintArea, rowHeaderPaletteBorder, VisualOrientation.Top, rowHeaderRenderingState))
                            {
                                this.Renderer.RenderStandardBack.DrawBack(rhRenderContext, rowHeaderPaintArea, rhPath, rowHeaderPaletteBack, VisualOrientation.Top, rowHeaderRenderingState, null);
                            }
                            this.Renderer.RenderStandardBorder.DrawBorder(rhRenderContext, rowHeaderPaintArea, rowHeaderPaletteBorder, VisualOrientation.Top, rowHeaderRenderingState);
                        }
                    }
                }
            }
        }

        #endregion

        #region OutlookGrid Events

        /// <summary>
        /// Called when [palette paint].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PaletteLayoutEventArgs"/> instance containing the event data.</param>
        private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Called when [global palette changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnGlobalPaletteChanged(object? sender, EventArgs e)
        {
            // (5) Unhook events from old palette
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
            }

            // (6) Cache the new IPalette that is the global palette
            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect.Target = _palette; //!!!!!! important

            //Reflect changes for the grouped heights
            int h = GlobalStaticValues.DefaultGroupRowHeight; // default height
            if (KryptonManager.CurrentGlobalPalette != null && KryptonManager.CurrentGlobalPalette.GetRenderer() ==
                KryptonManager.RenderOffice2013)
            {
                h = GlobalStaticValues.Office2013GroupRowHeight; // special height for office 2013         
            }

            //For each OutlookGridColumn
            for (int j = 0; j < _internalColumns.Count; j++)
            {
                if (_internalColumns[j].GroupingType != null)
                {
                    var outlookGridGroup = _internalColumns[j].GroupingType;
                    outlookGridGroup?.Height = h;
                }
            }

            // (7) Hook into events for the new palette
            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
            }

            // (8) Change of palette means we should repaint to show any changes
            Invalidate();
        }

        /// <summary>
        /// Clear sorting for the column selected by the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnClearSorting(object? sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                UnSortColumn(col);
                Fill();
            }
        }

        /// <summary>
        /// Ascending sort for the column selected by the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnSortAscending(object? sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                SortColumn(col, SortOrder.Ascending);
                if (col.IsGrouped)
                {
                    ForceRefreshGroupBox();
                }
                Fill();
            }
        }

        /// <summary>
        /// Descending sort for the column selected by the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnSortDescending(object? sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                SortColumn(col, SortOrder.Descending);
                if (col.IsGrouped)
                {
                    ForceRefreshGroupBox();
                }
                Fill();
            }
        }

        /// <summary>
        /// Grouping for the column selected by the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGroupByThisColumn(object? sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                GroupColumn(col, SortOrder.Ascending, null);
                ForceRefreshGroupBox();
                Fill();
            }
        }

        /// <summary>
        /// Ungrouping for the column selected by the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnGroupByThisColumn(object? sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                UnGroupColumn(col.Name!);
                ForceRefreshGroupBox();
                Fill();
            }
        }

        private void OnGroupCollapse(object? sender, EventArgs e)
        {
            OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
            Collapse(col.Name);
        }

        private void OnGroupExpand(object? sender, EventArgs e)
        {
            OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
            Expand(col.Name);
        }

        private void OnSortBySummary(object? sender, EventArgs e)
        {

            var item = sender as KryptonContextMenuItem ?? throw new ArgumentNullException(nameof(sender));
            OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
            col.GroupingType?.SortBySummaryCount = item.Checked;
            ForceRefreshGroupBox();
            Fill();
        }

        private void OnGroupIntervalClick(object? sender, EventArgs e)
        {
            var item = sender as KryptonContextMenuItem ?? throw new ArgumentNullException(nameof(sender));
            OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
            if (col.GroupingType != null)
            {
                if (item != null)
                {
                    if (item.Tag != null)
                    {
                        ((OutlookGridDateTimeGroup)col.GroupingType).Interval =
                            (DateInterval)Enum.Parse(typeof(DateInterval), item.Tag.ToString()!);
                    }
                }
            }
            ForceRefreshGroupBox();
            Fill();
        }

        private void OnConditionalFormattingClick(object? sender, EventArgs e)
        {
            var item = sender as KryptonContextMenuImageSelect ?? throw new ArgumentNullException(nameof(sender));
            OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
            ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
            ConditionalFormatting newFormat = (item.Tag as List<ConditionalFormatting>)![item.SelectedIndex];
            if (format == null)
            {
                _formatConditions.Add(new ConditionalFormatting(col.DataGridViewColumn!.Name, newFormat.FormatType, newFormat.FormatParams));
            }
            else
            {
                format.FormatType = newFormat.FormatType;
                format.FormatParams = newFormat.FormatParams;
            }
            item.SelectedIndex = -1; //I'm unable to get only one imageSelect checked between solid and gradient, so reset the selected image
            Fill();
        }

        private void OnTwoColorsCustomClick(object? sender, EventArgs e)
        {
            if (_rightToLeftLayout == RightToLeftLayout.LeftToRight)
            {
                VisualCustomFormatRuleForm fm = new(EnumConditionalFormatType.TwoColorsRange);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newFormat = new(col.DataGridViewColumn!.Name, EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(fm.MinimumColor, fm.MaximumColor));
                        _formatConditions.Add(newFormat);
                    }
                    else
                    {
                        format.FormatType = EnumConditionalFormatType.TwoColorsRange;
                        format.FormatParams = new TwoColorsParams(fm.MinimumColor, fm.MaximumColor);
                    }
                    Fill();
                }
                fm.Dispose();
            }
            else
            {
                VisualCustomFormatRuleRtlAwareForm fm = new(EnumConditionalFormatType.TwoColorsRange);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newFormat = new(col.DataGridViewColumn!.Name, EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(fm.MinimumColor, fm.MaximumColor));
                        _formatConditions.Add(newFormat);
                    }
                    else
                    {
                        format.FormatType = EnumConditionalFormatType.TwoColorsRange;
                        format.FormatParams = new TwoColorsParams(fm.MinimumColor, fm.MaximumColor);
                    }
                    Fill();
                }
                fm.Dispose();
            }
        }


        private void OnThreeColorsCustomClick(object? sender, EventArgs e)
        {
            if (_rightToLeftLayout == RightToLeftLayout.LeftToRight)
            {
                VisualCustomFormatRuleForm fm = new(EnumConditionalFormatType.ThreeColorsRange);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newFormat = new(col.DataGridViewColumn!.Name, EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(248, 105, 107), Color.FromArgb(255, 235, 132), Color.FromArgb(99, 190, 123)));
                        _formatConditions.Add(newFormat);
                    }
                    else
                    {
                        format.FormatType = EnumConditionalFormatType.ThreeColorsRange;
                        format.FormatParams = new ThreeColorsParams(Color.FromArgb(248, 105, 107), Color.FromArgb(255, 235, 132), Color.FromArgb(99, 190, 123));
                    }
                    Fill();
                }
                fm.Dispose();
            }
            else
            {
                VisualCustomFormatRuleRtlAwareForm fm = new(EnumConditionalFormatType.ThreeColorsRange);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newFormat = new(col.DataGridViewColumn!.Name, EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(248, 105, 107), Color.FromArgb(255, 235, 132), Color.FromArgb(99, 190, 123)));
                        _formatConditions.Add(newFormat);
                    }
                    else
                    {
                        format.FormatType = EnumConditionalFormatType.ThreeColorsRange;
                        format.FormatParams = new ThreeColorsParams(Color.FromArgb(248, 105, 107), Color.FromArgb(255, 235, 132), Color.FromArgb(99, 190, 123));
                    }
                    Fill();
                }
                fm.Dispose();
            }
        }

        private void OnBarCustomClick(object? sender, EventArgs e)
        {
            if (_rightToLeftLayout == RightToLeftLayout.LeftToRight)
            {
                VisualCustomFormatRuleForm fm = new(EnumConditionalFormatType.Bar);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newFormat = new(col.DataGridViewColumn!.Name, EnumConditionalFormatType.Bar, new BarParams(fm.MinimumColor, fm.Gradient));
                        _formatConditions.Add(newFormat);
                    }
                    else
                    {
                        format.FormatType = EnumConditionalFormatType.Bar;
                        format.FormatParams = new BarParams(fm.MinimumColor, fm.Gradient);
                    }
                    Fill();
                }
                fm.Dispose();
            }
            else
            {
                VisualCustomFormatRuleRtlAwareForm fm = new(EnumConditionalFormatType.Bar);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newFormat = new(col.DataGridViewColumn!.Name, EnumConditionalFormatType.Bar, new BarParams(fm.MinimumColor, fm.Gradient));
                        _formatConditions.Add(newFormat);
                    }
                    else
                    {
                        format.FormatType = EnumConditionalFormatType.Bar;
                        format.FormatParams = new BarParams(fm.MinimumColor, fm.Gradient);
                    }
                    Fill();
                }
                fm.Dispose();
            }
        }

        private void OnClearConditionalClick(object? sender, EventArgs e)
        {
            OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnIndex(_colSelected)!;
            _formatConditions.RemoveAll(x => x.ColumnName == col.Name);
            for (int i = 0; i < _internalRows.Count; i++)
            {
                FormattingCell fCell = (FormattingCell)_internalRows[i].Cells[_colSelected];
                //fCell.FormatType = formatConditions[i].FormatType;
                fCell.FormatParams = null;
            }
            Fill();
        }


        private void OnColumnVisibleCheckedChanged(object? sender, EventArgs e)
        {
            var item = sender as KryptonContextMenuCheckBox ?? throw new ArgumentNullException(nameof(sender));
            Columns[(int)item.Tag!].Visible = item.Checked;
        }

        /// <summary>
        /// Shows the GroupBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowGroupBox(object? sender, EventArgs e)
        {
            _groupBox?.Show();
        }

        /// <summary>
        /// Hide the GroupBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHideGroupBox(object? sender, EventArgs e)
        {
            _groupBox?.Hide();
        }

        /// <summary>
        /// Resizes the selected column by the menu to best fit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBestFitColumn(object? sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                Cursor.Current = Cursors.WaitCursor;
                AutoResizeColumn(_colSelected, DataGridViewAutoSizeColumnMode.AllCells);
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Resizes all columns to best fit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBestFitAllColumns(object? sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Handles the ColumnSortChangedEvent event. Update the header (glyph) and fill the grid too.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A OutlookGridColumnEventArgs that contains the event data.</param>
        private void ColumnSortChangedEvent(object? sender, OutlookGridColumnEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives ColumnSortChangedEvent : " + e.Column.Name + @" " + e.Column.SortDirection);
#endif
            _internalColumns[e.Column.Name!]!.SortDirection = e.Column.SortDirection;
            _internalColumns[e.Column.Name!]!.DataGridViewColumn!.HeaderCell.SortGlyphDirection = e.Column.SortDirection;
            Fill();
        }

        /// <summary>
        /// Handles the ColumnGroupAddedEvent event. Fill the grid too.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A OutlookGridColumnEventArgs that contains the event data.</param>
        private void ColumnGroupAddedEvent(object? sender, OutlookGridColumnEventArgs e)
        {
            GroupColumn(e.Column.Name!, e.Column.SortDirection, null);
            //We fill again the grid with the new Grouping info
            Fill();
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives ColumnGroupAddedEvent : " + e.Column.Name);
#endif
        }

        /// <summary>
        /// Handles the ColumnGroupRemovedEvent event. Fill the grid too.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A OutlookGridColumnEventArgs that contains the event data.</param>
        private void ColumnGroupRemovedEvent(object? sender, OutlookGridColumnEventArgs e)
        {
            UnGroupColumn(e.Column.Name!);
            //We fill again the grid with the new Grouping info
            Fill();
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives ColumnGroupRemovedEvent : " + e.Column.Name);
#endif
        }

        /// <summary>
        /// Handles the ClearGroupingEvent event. Fill the grid too.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        private void ClearGroupingEvent(object? sender, EventArgs e)
        {
            ClearGroups();
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives ClearGroupingEvent");
#endif
        }

        /// <summary>
        /// Handles the FullCollapseEvent event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        private void FullCollapseEvent(object? sender, EventArgs e)
        {
            CollapseAll();
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives FullCollapseEvent");
#endif
        }

        /// <summary>
        /// Handles the FullExpandEvent event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        private void FullExpandEvent(object? sender, EventArgs e)
        {
            ExpandAll();
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives FullExpandEvent");
#endif
        }

        /// <summary>
        /// Handles the GroupExpandEvent event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridGroupExpandEvent(object? sender, OutlookGridColumnEventArgs e)
        {
            Expand(e.Column.Name);
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives GridGroupExpandEvent");
#endif
        }

        private void GridGroupCollapseEvent(object? sender, OutlookGridColumnEventArgs e)
        {
            Collapse(e.Column.Name);
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives GridGroupCollapseEvent");
#endif
        }

        private void ColumnGroupIndexChangedEvent(object? sender, OutlookGridColumnEventArgs e)
        {
            //TODO 25/01/2014
            _internalColumns.ChangeGroupIndex(e.Column);
            Fill(); //to reflect the changes
            ForceRefreshGroupBox();
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives ColumnGroupIndexChangedEvent");
#endif
        }

        private void GroupIntervalClickEvent(object? sender, OutlookGridColumnEventArgs e)
        {
            OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnName(e.Column.Name)!;
            (col.GroupingType as OutlookGridDateTimeGroup)!.Interval =
                (e.Column.GroupingType as OutlookGridDateTimeGroup)!.Interval;
            Fill();
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives GroupIntervalClickEvent");
#endif
        }

        private void SortBySummaryCountEvent(object? sender, OutlookGridColumnEventArgs e)
        {
            OutlookGridColumn col = (OutlookGridColumn)_internalColumns.FindFromColumnName(e.Column.Name)!;
            if (col.GroupingType != null)
            {
                if (e.Column.GroupingType != null)
                {
                    col.GroupingType.SortBySummaryCount = e.Column.GroupingType.SortBySummaryCount;
                }
            }
            Fill();
#if DEBUG
            Debug.WriteLine(@"OutlookGrid - Receives SortBySummaryCountEvent");
#endif
        }

        /// <summary>
        /// Raises the GroupImageClick event.
        /// </summary>
        /// <param name="e">A OutlookGridGroupImageEventArgs that contains the event data.</param>
        protected virtual void OnGroupImageClick(OutlookGridGroupImageEventArgs e)
        {
            GroupImageClick?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the NodeExpanding event.
        /// </summary>
        /// <param name="e">A ExpandingEventArgs that contains the event data.</param>
        protected virtual void OnNodeExpanding(ExpandingEventArgs e)
        {
            NodeExpanding?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the NodeExpanded event.
        /// </summary>
        /// <param name="e">A ExpandedEventArgs that contains the event data.</param>
        protected virtual void OnNodeExpanded(ExpandedEventArgs e)
        {
            NodeExpanded?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the NodeCollapsing event.
        /// </summary>
        /// <param name="e">A CollapsingEventArgs that contains the event data.</param>
        protected virtual void OnNodeCollapsing(CollapsingEventArgs e)
        {
            NodeCollapsing?.Invoke(this, e);

        }

        /// <summary>
        /// Raises the NodeCollapsed event.
        /// </summary>
        /// <param name="e">A CollapsedEventArgs that contains the event data.</param>
        protected virtual void OnNodeCollapsed(CollapsedEventArgs e)
        {
            NodeCollapsed?.Invoke(this, e);
        }

        #endregion

        #region OutlookGrid methods

        /// <summary>
        /// Add a column for internal uses of the OutlookGrid. The column must already exists in the DataGridView. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        /// <param name="comparer">The comparer if needed</param>
        public void AddInternalColumn(DataGridViewColumn col, IOutlookGridGroup group, SortOrder sortDirection, int groupIndex, int sortIndex, IComparer? comparer) =>
            AddInternalColumn(new OutlookGridColumn(col, group, sortDirection, groupIndex, sortIndex, comparer));

        /// <summary>
        /// Add a column for internal uses of the OutlookGrid. The column must already exists in the DataGridView. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        /// <param name="comparer">The comparer if needed</param>
        /// <param name="aggregationType">The type of aggregation to apply to the column.</param>
        public void AddInternalColumn(DataGridViewColumn col, IOutlookGridGroup group, SortOrder sortDirection, int groupIndex, int sortIndex, IComparer? comparer, KryptonOutlookGridAggregationType aggregationType) =>
            AddInternalColumn(new OutlookGridColumn(col, group, sortDirection, groupIndex, sortIndex, comparer, aggregationType));

        /// <summary>
        /// Add a column for internal uses of the OutlookGrid. The column must already exists in the DataGridView. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        public void AddInternalColumn(DataGridViewColumn col, IOutlookGridGroup group, SortOrder sortDirection, int groupIndex, int sortIndex) =>
            AddInternalColumn(new OutlookGridColumn(col, group, sortDirection, groupIndex, sortIndex, null));

        /// <summary>
        /// Add a column for internal uses of the OutlookGrid. The column must already exists in the DataGridView. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        /// <param name="aggregationType">The type of aggregation to apply to the column.</param>
        public void AddInternalColumn(DataGridViewColumn col, IOutlookGridGroup group, SortOrder sortDirection, int groupIndex, int sortIndex, KryptonOutlookGridAggregationType aggregationType) =>
            AddInternalColumn(new OutlookGridColumn(col, group, sortDirection, groupIndex, sortIndex, null, aggregationType));

        /// <summary>
        /// Add a column for internal uses of the OutlookGrid. The column must already exists in the DataGridView. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="col">The configured OutlookGridColumn.</param>
        public void AddInternalColumn(OutlookGridColumn col)
        {
            Debug.Assert(col != null);
            if (col != null)
            {
                _internalColumns.Add(col);
                //Already reflect the SortOrder on the column
                col.DataGridViewColumn!.HeaderCell.SortGlyphDirection = col.SortDirection;
                if (col.GroupingType != null && _hideColumnOnGrouping && col.GroupIndex > -1 && col.GroupingType.AllowHiddenWhenGrouped)
                {
                    col.DataGridViewColumn.Visible = false;
                }
            }
        }

        /// <summary>
        /// Add an array of OutlookGridColumns for internal use of OutlookGrid. The columns must already exist in the DataGridView. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="cols">The array of columns</param>
        public void AddRangeInternalColumns(params OutlookGridColumn[] cols)
        {
            Debug.Assert(cols != null);
            // All columns with DisplayIndex != -1 are put into the initialColumns array
            if (cols != null)
            {
                foreach (OutlookGridColumn col in cols)
                {
                    AddInternalColumn(col);
                }
            }
        }

        /// <summary>
        /// Add an array of OutlookGridColumns for internal use of OutlookGrid. The columns must already exist in the DataGridView. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="cols">The list of columns.</param>
        public void AddRangeInternalColumns(List<OutlookGridColumn> cols)
        {
            Debug.Assert(cols != null);
            // All columns with DisplayIndex != -1 are put into the initialColumns array
            if (cols != null)
            {
                foreach (OutlookGridColumn col in cols)
                {
                    AddInternalColumn(col);
                }
            }
        }

        /// <summary>
        /// Group a column
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <param name="sortDirection">The sort direction of the group./</param>
        /// <param name="gr">The IOutlookGridGroup object.</param>
        public void GroupColumn(string columnName, SortOrder sortDirection, IOutlookGridGroup? gr) => GroupColumn(_internalColumns[columnName]!, sortDirection, gr);

        /// <summary>
        /// Group a column
        /// </summary>
        /// <param name="col">The name of the column.</param>
        /// <param name="sortDirection">The sort direction of the group./</param>
        /// <param name="gr">The IOutlookGridGroup object.</param>
        public void GroupColumn(OutlookGridColumn col, SortOrder sortDirection, IOutlookGridGroup? gr)
        {
            if (!col.IsGrouped)
            {
                col.GroupIndex = ++_internalColumns.MaxGroupIndex;
                if (col.SortIndex > -1)
                {
                    _internalColumns.RemoveSortIndex(col);
                }

                col.SortDirection = sortDirection;
                col.DataGridViewColumn!.HeaderCell.SortGlyphDirection = sortDirection;
                if (gr != null)
                {
                    col.GroupingType = gr;
                }

                if (col.GroupingType != null && _hideColumnOnGrouping && col.GroupingType.AllowHiddenWhenGrouped)
                {
                    col.DataGridViewColumn.Visible = false;
                }
            }
        }

        /// <summary>
        /// Ungroup a column
        /// </summary>
        /// <param name="columnName">The OutlookGridColumn.</param>
        public void UnGroupColumn(string columnName)
        {
            UnGroupColumn(_internalColumns[columnName]!);
        }

        /// <summary>
        /// Ungroup a column
        /// </summary>
        /// <param name="col">The OutlookGridColumn.</param>
        public void UnGroupColumn(OutlookGridColumn col)
        {
            if (col.IsGrouped)
            {
                _internalColumns.RemoveGroupIndex(col);
                col.SortDirection = SortOrder.None;
                col.DataGridViewColumn!.HeaderCell.SortGlyphDirection = SortOrder.None;
                if (col.GroupingType != null)
                {
                    col.GroupingType.Collapsed = false;
                    if (_hideColumnOnGrouping && col.GroupingType.AllowHiddenWhenGrouped)
                    {
                        col.DataGridViewColumn.Visible = true;
                    }
                }
            }
#if DEBUG
            _internalColumns.DebugOutput();
#endif
        }

        /// <summary>
        /// Sort the column. Call Fill after to make the changes
        /// </summary>
        /// <param name="col">The outlookGridColumn</param>
        /// <param name="sort">The new SortOrder.</param>
        public void SortColumn(OutlookGridColumn col, SortOrder sort)
        {
            //Change the SortIndex and MaxSortIndex only if it is not a grouped column
            if (!col.IsGrouped && col.SortIndex == -1)
            {
                col.SortIndex = ++_internalColumns.MaxSortIndex;
            }

            //Change the order in all cases
            col.SortDirection = sort;
            col.DataGridViewColumn!.HeaderCell.SortGlyphDirection = sort;
#if DEBUG
            _internalColumns.DebugOutput();
#endif
        }

        /// <summary>
        /// UnSort the column. Call Fill after to make the changes
        /// </summary>
        /// <param name="col">The outlookGridColumn.</param>
        public void UnSortColumn(OutlookGridColumn col)
        {
            //Remove the SortIndex and rearrange the SortIndexes only if the column is not grouped
            if (!col.IsGrouped)
            {
                _internalColumns.RemoveSortIndex(col);
                col.SortDirection = SortOrder.None;
                col.DataGridViewColumn!.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
#if DEBUG
            _internalColumns.DebugOutput();
#endif
        }

        /// <summary>
        /// Collapse all groups
        /// </summary>
        public void CollapseAll()
        {
            SetGroupCollapse(true);
        }

        /// <summary>
        /// Expand all groups
        /// </summary>
        public void ExpandAll()
        {
            SetGroupCollapse(false);
        }

        /// <summary>
        /// Expand all groups associated to a specific column
        /// </summary>
        /// <param name="col">The DataGridViewColumn</param>
        public void Expand(string? col)
        {
            SetGroupCollapse(col, false);
        }

        /// <summary>
        /// Collapse all groups associated to a specific column
        /// </summary>
        /// <param name="col">The DataGridViewColumn</param>
        public void Collapse(string? col)
        {
            SetGroupCollapse(col, true);
        }

        /// <summary>
        /// Expand a group row
        /// </summary>
        /// <param name="row">Index of the row</param>
        public void Expand(int row)
        {
            SetGroupCollapse(row, false);
        }

        /// <summary>
        /// Collapse a group row
        /// </summary>
        /// <param name="row">Index of the row</param>
        public void Collapse(int row)
        {
            SetGroupCollapse(row, true);
        }

        /// <summary>
        /// Clear all groups. Performs a fill grid too.
        /// </summary>
        public void ClearGroups()
        {
            ClearGroupsWithoutFilling();
            Fill();
        }

        /// <summary>
        /// Clear all groups. No FillGrid calls.
        /// </summary>
        public void ClearGroupsWithoutFilling()
        {
            //TODO check that
            //reset groups and collapsed statuses
            _groupCollection.Clear();
            //reset groups in columns
            _internalColumns.MaxGroupIndex = -1;
            for (int i = 0; i < _internalColumns.Count; i++)
            {
                if (_internalColumns[i].IsGrouped)
                {
                    _internalColumns[i].DataGridViewColumn!.Visible = true;
                }

                _internalColumns[i].GroupIndex = -1;
            }
        }

        /// <summary>
        /// Gets the index of the previous group row if any.
        /// </summary>
        /// <param name="currentRow">Current row index</param>
        /// <returns>Index of the group row, -1 otherwise</returns>
        public int PreviousGroupRowIndex(int currentRow)
        {
            for (int i = currentRow - 1; i >= 0; i--)
            {
                OutlookGridRow row = (OutlookGridRow)Rows[i];
                if (row.IsGroupRow)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Gets all the SubRows of a GroupRow (recursive)
        /// </summary>
        /// <param name="list">The result list of OutlookGridRows</param>
        /// <param name="groupRow">The IOutlookGridGroup that contains rows to inspect.</param>
        /// <returns>A list of OutlookGridRows</returns>
        public List<OutlookGridRow> GetSubRows(ref List<OutlookGridRow> list, IOutlookGridGroup? groupRow)
        {
            list.AddRange(groupRow!.Rows);
            for (int i = 0; i < groupRow.Children.Count; i++)
            {
                if (groupRow.Children.Count > 0)
                {
                    GetSubRows(ref list, groupRow.Children[i]);
                }
            }

            return list;
        }

        /// <summary>
        /// Register for events concerning the GroupBox
        /// </summary>
        public void RegisterGroupBoxEvents()
        {
            //Register for event of the associated KryptonGroupBox
            if (GroupBox != null)
            {
                if (_groupBox != null)
                {
                    _groupBox.ColumnGroupAdded += ColumnGroupAddedEvent;
                    _groupBox.ColumnSortChanged += ColumnSortChangedEvent;
                    _groupBox.ColumnGroupRemoved += ColumnGroupRemovedEvent;
                    _groupBox.ClearGrouping += ClearGroupingEvent;
                    _groupBox.FullCollapse += FullCollapseEvent;
                    _groupBox.FullExpand += FullExpandEvent;
                    _groupBox.ColumnGroupOrderChanged += ColumnGroupIndexChangedEvent;
                    _groupBox.GroupCollapse += GridGroupCollapseEvent;
                    _groupBox.GroupExpand += GridGroupExpandEvent;
                    _groupBox.GroupIntervalClick += GroupIntervalClickEvent;
                    _groupBox.SortBySummaryCount += SortBySummaryCountEvent;
                }
            }
        }

        /// <summary>
        /// Synchronize the OutlookGrid Group Box with the current status of the grid
        /// </summary>
        public void ForceRefreshGroupBox()
        {
            _groupBox?.UpdateGroupingColumns(_internalColumns.FindGroupedColumns());
        }

        /// <summary>
        /// Show the context menu header
        /// </summary>
        /// <param name="columnIndex">The column used by the context menu.</param>
        private void ShowColumnHeaderContextMenu(int columnIndex)
        {
            // Find the OutlookGridColumn associated with the clicked DataGridViewColumn
            OutlookGridColumn? col = _internalColumns.FindFromColumnIndex(columnIndex);
            DataGridViewColumn? clickedDgvColumn = col?.DataGridViewColumn;

            // Create menu items the first time they are needed
            if (_menuItems == null)
            {
                #region Localisation (Your existing menu item creation)

                _menuSortAscending = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.SortAscending, SortingImageResources.sort_az_ascending2, OnColumnSortAscending);
                _menuSortDescending = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.SortDescending, SortingImageResources.sort_az_descending2, OnColumnSortDescending);
                _menuClearSorting = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.ClearSorting, SortingImageResources.sort_up_down_delete_16, OnColumnClearSorting);
                _menuSeparator1 = new KryptonContextMenuSeparator();
                _menuExpand = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.Expand, ElementsImageResources.element_plus_16, OnGroupExpand);
                _menuCollapse = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.Collapse, ElementsImageResources.element_minus_16, OnGroupCollapse);
                _menuSeparator4 = new KryptonContextMenuSeparator();
                _menuGroupByThisColumn = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.Group, ElementsImageResources.element, OnGroupByThisColumn);
                _menuUngroupByThisColumn = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.Ungroup, ElementsImageResources.element_delete, OnUnGroupByThisColumn);
                _menuShowGroupBox = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.ShowGroupBox, null, OnShowGroupBox);
                _menuHideGroupBox = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.HideGroupBox, null, OnHideGroupBox);
                _menuSeparator2 = new KryptonContextMenuSeparator();
                _menuBestFitColumn = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.BestFit, null, OnBestFitColumn);
                _menuBestFitAllColumns = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.BestFitAll, GenericImageResources.fit_to_size, OnBestFitAllColumns);
                _menuSeparator3 = new KryptonContextMenuSeparator();
                _menuVisibleColumns = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.Columns, GenericImageResources.table2_selection_column, null);
                _menuGroupInterval = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.GroupInterval);
                _menuSortBySummary = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.SortBySummaryCount, null, OnSortBySummary)
                {
                    CheckOnClick = true
                };
                _menuSeparator5 = new KryptonContextMenuSeparator();
                _menuConditionalFormatting = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.ConditionalFormatting, GenericImageResources.table_conditional_16, null);

                #endregion

                //Group Interval
                KryptonContextMenuItems groupIntervalItems;
                KryptonContextMenuItem? it;
                string[] names = Enum.GetNames(typeof(DateInterval));
                KryptonContextMenuItemBase[] arrayOptions = new KryptonContextMenuItemBase[names.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    it = new KryptonContextMenuItem(OutlookGridLanguageManager.Instance.GetString(names[i]))
                    {
                        Tag = names[i]
                    };
                    it.Click += OnGroupIntervalClick;
                    arrayOptions[i] = it;
                }
                groupIntervalItems = new KryptonContextMenuItems(arrayOptions);
                _menuGroupInterval.Items.Add(groupIntervalItems);

                //Visible Columns
                KryptonContextMenuCheckBox? itCheckbox;
                KryptonContextMenuItemBase?[] arrayCols = new KryptonContextMenuItemBase?[Columns.Count];
                for (int i = 0; i < Columns.Count; i++)
                {
                    itCheckbox = new KryptonContextMenuCheckBox(Columns[i].HeaderText)
                    {
                        Checked = Columns[i].Visible,
                        Tag = Columns[i].Index,
                        Visible = _internalColumns[Columns[i].Name]!.AvailableInContextMenu
                    };
                    itCheckbox.CheckedChanged += OnColumnVisibleCheckedChanged;
                    arrayCols[i] = itCheckbox;
                }
                _menuVisibleColumns.Items.AddRange(arrayCols!);

                #region Conditional formatting

                //Conditional formatting (your existing complex setup)
                ImageList imgListFormatting = new()
                {
                    ColorDepth = ColorDepth.Depth32Bit,
                    ImageSize = new Size(32, 32)
                };
                List<ConditionalFormatting> tmpTag = [];
                imgListFormatting.Images.Add(DataBarImageResources.Databar_solid_blue_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(76, 118, 255), false)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_solid_green_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(95, 173, 123), false)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_solid_red_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(248, 108, 103), false)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_solid_yellow_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(255, 185, 56), false)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_solid_violet_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(185, 56, 255), false)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_solid_pink_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(255, 56, 185), false)));

                imgListFormatting.Images.Add(DataBarImageResources.Databar_gradient_blue_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(76, 118, 255), true)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_gradient_green_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(95, 173, 123), true)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_gradient_red_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(248, 108, 103), true)));
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(255, 185, 56), true)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_gradient_violet_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(185, 56, 255), true)));
                imgListFormatting.Images.Add(DataBarImageResources.Databar_gradient_pink_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.Bar, new BarParams(Color.FromArgb(255, 56, 185), true)));

                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_white_blue_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.White, Color.FromArgb(76, 118, 255))));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_blue_white_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.FromArgb(76, 118, 255), Color.White)));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_white_green_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.White, Color.FromArgb(95, 173, 123))));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_green_white_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.FromArgb(95, 173, 123), Color.White)));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_white_red_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.White, Color.FromArgb(248, 108, 103))));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_red_white_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.FromArgb(248, 108, 103), Color.White)));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_white_yellow_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.White, Color.FromArgb(255, 185, 56))));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_yellow_white_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.FromArgb(255, 185, 56), Color.White)));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_white_violet_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.White, Color.FromArgb(185, 56, 255))));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_violet_white_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.FromArgb(185, 56, 255), Color.White)));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_white_pink_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.White, Color.FromArgb(255, 56, 185))));
                imgListFormatting.Images.Add(OutlookGridImageResources.TwoColors_pink_white_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(Color.FromArgb(255, 56, 185), Color.White)));

                imgListFormatting.Images.Add(OutlookGridImageResources.ThreeColors_green_yellow_red_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(84, 179, 112), Color.FromArgb(252, 229, 130), Color.FromArgb(243, 120, 97))));
                imgListFormatting.Images.Add(OutlookGridImageResources.ThreeColors_red_yellow_green_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(243, 120, 97), Color.FromArgb(252, 229, 130), Color.FromArgb(84, 179, 112))));
                imgListFormatting.Images.Add(OutlookGridImageResources.ThreeColors_green_white_red_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(84, 179, 112), Color.White, Color.FromArgb(243, 120, 97))));
                imgListFormatting.Images.Add(OutlookGridImageResources.ThreeColors_red_white_green_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(243, 120, 97), Color.White, Color.FromArgb(84, 179, 112))));
                imgListFormatting.Images.Add(OutlookGridImageResources.ThreeColors_blue_white_red_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(134, 166, 253), Color.White, Color.FromArgb(243, 120, 97))));
                imgListFormatting.Images.Add(OutlookGridImageResources.ThreeColors_red_white_blue_32);
                tmpTag.Add(new ConditionalFormatting(EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(243, 120, 97), Color.White, Color.FromArgb(134, 166, 253))));


                it = null;
                names = Enum.GetNames(typeof(EnumConditionalFormatType));
                arrayOptions = new KryptonContextMenuItemBase[names.Length + 2]; // +2 for separator and Clear Rules
                for (int i = 0; i < names.Length; i++)
                {
                    it = new(OutlookGridLanguageManager.Instance.GetString(names[i]))
                    {
                        Tag = names[i]
                    };

                    if (names[i] == EnumConditionalFormatType.Bar.ToString())
                    {
                        it.Image = DataBarImageResources.databar_generic_16;

                        KryptonContextMenuHeading kFormattingBarHeadingSolid = new()
                        {
                            Text = KryptonManager.Strings.OutlookGridStrings.SolidFill
                        };
                        KryptonContextMenuImageSelect kFormattingBarImgSelectSolid = new()
                        {
                            ImageList = imgListFormatting,
                            ImageIndexStart = 0,
                            ImageIndexEnd = 5,
                            LineItems = 4,
                            Tag = tmpTag
                        };
                        kFormattingBarImgSelectSolid.Click += OnConditionalFormattingClick;

                        KryptonContextMenuHeading kFormattingBarHeadingGradient = new()
                        {
                            Text = KryptonManager.Strings.OutlookGridStrings.GradientFill
                        };
                        KryptonContextMenuImageSelect kFormattingBarImgSelectGradient = new()
                        {
                            ImageList = imgListFormatting,
                            ImageIndexStart = 6,
                            ImageIndexEnd = 11,
                            LineItems = 4,
                            Tag = tmpTag
                        };
                        kFormattingBarImgSelectGradient.Click += OnConditionalFormattingClick;

                        KryptonContextMenuHeading kFormattingBarHeadingOther = new()
                        {
                            Text = KryptonManager.Strings.OutlookGridStrings.Other
                        };
                        KryptonContextMenuItem? it2;
                        it2 = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.CustomThreeDots)
                        {
                            Tag = "",
                            Image = GenericImageResources.paint_bucket_green
                        };
                        it2.Click += OnBarCustomClick;

                        KryptonContextMenuItems bars = new([it2]);

                        it.Items.AddRange([
                            kFormattingBarHeadingSolid,
                            kFormattingBarImgSelectSolid,
                            kFormattingBarHeadingGradient,
                            kFormattingBarImgSelectGradient,
                            kFormattingBarHeadingOther,
                            bars
                        ]);
                    }
                    else if (names[i] == EnumConditionalFormatType.TwoColorsRange.ToString())
                    {
                        it.Image = OutlookGridImageResources.color2scale_generic_16;

                        KryptonContextMenuItems twoColors;

                        KryptonContextMenuImageSelect kTwoColorsImgSelect = new()
                        {
                            ImageList = imgListFormatting,
                            ImageIndexStart = 12,
                            ImageIndexEnd = 23,
                            LineItems = 4,
                            Tag = tmpTag
                        };
                        kTwoColorsImgSelect.Click += OnConditionalFormattingClick;
                        it.Items.Add(kTwoColorsImgSelect);

                        KryptonContextMenuSeparator sep1 = new()
                        {
                            Tag = ""
                        };

                        KryptonContextMenuItem? it2;
                        it2 = new KryptonContextMenuItem(KryptonManager.Strings.OutlookGridStrings.CustomThreeDots)
                        {
                            Tag = "",
                            Image = GenericImageResources.paint_bucket_green
                        };
                        it2.Click += OnTwoColorsCustomClick;

                        twoColors = new KryptonContextMenuItems([sep1, it2]);
                        it.Items.Add(twoColors);
                    }
                    else if (names[i] == EnumConditionalFormatType.ThreeColorsRange.ToString())
                    {
                        it.Image = OutlookGridImageResources.color3scale_generic_16;

                        KryptonContextMenuItems threeColors;

                        KryptonContextMenuImageSelect kThreeColorsImgSelect = new()
                        {
                            ImageList = imgListFormatting,
                            ImageIndexStart = 24,
                            ImageIndexEnd = 29,
                            LineItems = 4,
                            Tag = tmpTag
                        };
                        kThreeColorsImgSelect.Click += OnConditionalFormattingClick;
                        it.Items.Add(kThreeColorsImgSelect);

                        KryptonContextMenuSeparator sep1 = new()
                        {
                            Tag = ""
                        };

                        KryptonContextMenuItem? it2 = new(KryptonManager.Strings.OutlookGridStrings.CustomThreeDots)
                        {
                            Tag = "",
                            Image = GenericImageResources.paint_bucket_green
                        };
                        it2.Click += OnThreeColorsCustomClick;

                        threeColors = new KryptonContextMenuItems([sep1, it2]);
                        it.Items.Add(threeColors);
                    }

                    arrayOptions[i] = it;
                    KryptonContextMenuSeparator sep2 = new()
                    {
                        Tag = ""
                    };
                    arrayOptions[i + 1] = sep2;
                    KryptonContextMenuItem? it3 = new(KryptonManager.Strings.OutlookGridStrings.ClearRules)
                    {
                        Image = GenericImageResources.eraser,
                        Tag = ""
                    };
                    it3.Click += OnClearConditionalClick;
                    arrayOptions[i + 2] = it3;
                }
                KryptonContextMenuItems conditionalFormattingItems = new(arrayOptions);
                _menuConditionalFormatting.Items.Add(conditionalFormattingItems);
                #endregion Conditional formatting


                _menuFitColumnsToWidth = new KryptonContextMenuItem("Fit To Width (all columns)", GenericImageResources.fit_to_size, OnFitColumnsToWidth);

                // --- NEW: AGGREGATION MENU ITEM CREATION (Done once) ---
                // Create a sub-items collection for aggregations
                KryptonContextMenuItem aggNone = new(KryptonOutlookGridAggregationType.None.ToString(), null, OnAggregationChange) { Tag = KryptonOutlookGridAggregationType.None };
                KryptonContextMenuItem aggSum = new(KryptonOutlookGridAggregationType.Sum.ToString(), null, OnAggregationChange) { Tag = KryptonOutlookGridAggregationType.Sum };
                KryptonContextMenuItem aggCount = new(KryptonOutlookGridAggregationType.Count.ToString(), null, OnAggregationChange) { Tag = KryptonOutlookGridAggregationType.Count };
                KryptonContextMenuItem aggAverage = new(KryptonOutlookGridAggregationType.Average.ToString(), null, OnAggregationChange) { Tag = KryptonOutlookGridAggregationType.Average };
                KryptonContextMenuItem aggMin = new(KryptonOutlookGridAggregationType.Min.ToString(), null, OnAggregationChange) { Tag = KryptonOutlookGridAggregationType.Min };
                KryptonContextMenuItem aggMax = new(KryptonOutlookGridAggregationType.Max.ToString(), null, OnAggregationChange) { Tag = KryptonOutlookGridAggregationType.Max };
                KryptonContextMenuItem aggMinMax = new(KryptonOutlookGridAggregationType.MinMax.ToString(), null, OnAggregationChange) { Tag = KryptonOutlookGridAggregationType.MinMax };

                _menuAggregationNumeric = new KryptonContextMenuItem("Aggregation", null, null);
                _menuAggregationNumeric.Items.AddRange([aggNone, aggSum, aggCount, aggAverage, aggMin, aggMax, aggMinMax]);
                _menuAggregationNumeric.Visible = (ShowSubTotal || ShowGrandTotal) && clickedDgvColumn.IsNumericColumn();

                _menuAggregationNonNumeric = new KryptonContextMenuItem("Aggregation", null, null);
                _menuAggregationNonNumeric.Items.AddRange([aggNone, aggCount, aggMin, aggMax, aggMinMax]);
                _menuAggregationNonNumeric.Visible = (ShowSubTotal || ShowGrandTotal) && !_menuAggregationNumeric.Visible;

                //var filterMenu = ShowColumnFilterContextMenu();
                _menuFilter = new KryptonContextMenuItem("Filter", null, OnFilterClick) { Visible = ShowColumnFilter };
                _menuClearFilter = new KryptonContextMenuItem("Clear Filter", null, OnClearFilterClick) { Visible = col!.Filters != null };
                _menuClearAllFilter = new KryptonContextMenuItem("Clear All Filter", null, OnClearAllFilterClick) { Visible = _internalColumns.Any(c => c.Filters != null) };

                _menuShowSearchToolBar = new KryptonContextMenuItem("Show SearchToolBar", null, OnShowSearchToolBar);
                _menuHideSearchToolBar = new KryptonContextMenuItem("Hide SearchToolBar", null, OnHideSearchToolBar);

                //Add items inside an items collection (apart from separator1 which is only added if required)
                _menuItems = new KryptonContextMenuItems([
                    _menuSortAscending,
                    _menuSortDescending,
                    _menuSortBySummary,
                    _menuClearSorting,
                    _menuSeparator1,
                    _menuExpand,
                    _menuCollapse,
                    _menuSeparator4,
                    _menuGroupByThisColumn,
                    _menuGroupInterval,
                    _menuUngroupByThisColumn,
                    _menuShowGroupBox,
                    _menuHideGroupBox,
                    _menuSeparator2,
                    _menuBestFitColumn,
                    _menuBestFitAllColumns,
                    _menuFitColumnsToWidth,
                    _menuSeparator3,
                    _menuVisibleColumns,
                    _menuSeparator5,
                    _menuConditionalFormatting,
                    // Add aggregation items here at the end of the main collection
                    //new KryptonContextMenuSeparator(),
                    _menuAggregationNumeric,
                    _menuAggregationNonNumeric,
                    _menuFilter,
                    _menuClearFilter,
                    _menuClearAllFilter,
                    _menuShowSearchToolBar,
                    _menuHideSearchToolBar
                ]);
            }

            // Ensure we have a krypton context menu if not already present
            _contextMenu ??= new KryptonContextMenu();

            // --- Update the individual menu options based on the clicked column ---
            // This part happens every time the context menu is shown, adjusting visibility and checked states.
            if (col != null && clickedDgvColumn != null)
            {
                _menuSortAscending!.Visible = clickedDgvColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuSortAscending.Checked = col.SortDirection == SortOrder.Ascending;
                _menuSortDescending!.Checked = col.SortDirection == SortOrder.Descending;
                _menuSortDescending.Visible = clickedDgvColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuSortBySummary!.Visible = col.IsGrouped && col.GroupingType != null;
                if (_menuSortBySummary.Visible)
                {
                    _menuSortBySummary.Checked = col.GroupingType!.SortBySummaryCount;
                }

                _menuClearSorting!.Enabled = col.SortDirection != SortOrder.None && !col.IsGrouped;
                _menuClearSorting.Visible = clickedDgvColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuSeparator1!.Visible = _menuSortAscending.Visible || _menuSortDescending.Visible || _menuClearSorting.Visible;
                _menuExpand!.Visible = col.IsGrouped;
                _menuCollapse!.Visible = col.IsGrouped;
                _menuSeparator4!.Visible = _menuExpand.Visible || _menuCollapse.Visible;
                _menuGroupByThisColumn!.Visible = !col.IsGrouped && clickedDgvColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuGroupInterval!.Visible = col.IsGrouped && clickedDgvColumn.SortMode != DataGridViewColumnSortMode.NotSortable && col.GroupingType?.GetType() == typeof(OutlookGridDateTimeGroup);
                if (_menuGroupInterval.Visible)
                {
                    string? currentInterval = Enum.GetName(typeof(DateInterval), (col.GroupingType as OutlookGridDateTimeGroup)!.Interval);
                    foreach (KryptonContextMenuItem item in ((KryptonContextMenuItems)_menuGroupInterval.Items[0]).Items.Cast<KryptonContextMenuItem>())
                    {
                        item.Checked = item.Tag!.ToString() == currentInterval;
                    }
                }
                _menuUngroupByThisColumn!.Visible = col.IsGrouped && clickedDgvColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuShowGroupBox!.Visible = _groupBox != null && !_groupBox.Visible;
                _menuHideGroupBox!.Visible = _groupBox != null && _groupBox.Visible;
                _menuSeparator2!.Visible = _menuGroupByThisColumn.Visible || _menuUngroupByThisColumn.Visible || _menuShowGroupBox.Visible || _menuHideGroupBox.Visible;
                _menuBestFitColumn!.Visible = true;
                if (clickedDgvColumn.GetType() == typeof(KryptonDataGridViewFormattingColumn))
                {
                    _menuSeparator5!.Visible = true;
                    _menuConditionalFormatting!.Visible = true;

                    //Get the format condition
                    ConditionalFormatting format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name)!;

                    for (int i = 0; i < _menuConditionalFormatting.Items[0].ItemChildCount; i++)
                    {
                        if (format != null
                            && (_menuConditionalFormatting.Items[0] as KryptonContextMenuItems)!.Items[i].Tag!.ToString()!.Equals(format.FormatType.ToString()))
                        {
                            ((KryptonContextMenuItem)((KryptonContextMenuItems)_menuConditionalFormatting.Items[0]).Items[i]).Checked = true;
                        }
                        else
                        {
                            if (((KryptonContextMenuItems)_menuConditionalFormatting.Items[0]).Items[i].GetType() != typeof(KryptonContextMenuSeparator))
                            {
                                ((KryptonContextMenuItem)((KryptonContextMenuItems)_menuConditionalFormatting.Items[0]).Items[i]).Checked = false;
                            }
                        }
                    }
                }
                else
                {
                    _menuSeparator5!.Visible = false;
                    _menuConditionalFormatting!.Visible = false;
                }

                foreach (KryptonContextMenuCheckBox item in _menuVisibleColumns.Items.Cast<KryptonContextMenuCheckBox>())
                {
                    item.Visible = _internalColumns[item.Tag.ToInteger()]!.AvailableInContextMenu;
                }

                _menuAggregationNumeric.Visible = (ShowSubTotal || ShowGrandTotal) && clickedDgvColumn.IsNumericColumn();
                _menuAggregationNonNumeric.Visible = (ShowSubTotal || ShowGrandTotal) && !_menuAggregationNumeric.Visible;
                if (_menuAggregationNumeric.Visible)
                {
                    foreach (KryptonContextMenuItem item in _menuAggregationNumeric.Items.Cast<KryptonContextMenuItem>())
                    {
                        item.Checked = item.Tag!.ToString() == col.AggregationType.ToString();
                    }
                }
                if (_menuAggregationNonNumeric.Visible)
                {
                    foreach (KryptonContextMenuItem item in _menuAggregationNonNumeric.Items.Cast<KryptonContextMenuItem>())
                    {
                        item.Checked = item.Tag!.ToString() == col.AggregationType.ToString();
                    }
                }
                _menuFilter.Visible = ShowColumnFilter;
                _menuClearFilter.Visible = col.Filters != null;
                _menuClearAllFilter.Visible = _internalColumns.Any(c => c.Filters != null);
                _menuShowSearchToolBar.Visible = _searchToolBar != null && !_searchToolBar.Visible;
                _menuHideSearchToolBar.Visible = _searchToolBar != null && _searchToolBar.Visible;
            }
            else // If col is null (shouldn't happen if columnIndex is valid but GetOutlookGridColumn returns null)
            {
                _menuSortAscending!.Visible = false;
                _menuSortDescending!.Visible = false;
                _menuSortBySummary!.Visible = false;
                _menuClearSorting!.Visible = false;
                _menuSeparator1!.Visible = false;
                _menuExpand!.Visible = false;
                _menuCollapse!.Visible = false;
                _menuSeparator4!.Visible = false;
                _menuGroupByThisColumn!.Visible = false;
                _menuGroupInterval!.Visible = false;
                _menuUngroupByThisColumn!.Visible = false;
                _menuShowGroupBox!.Visible = _groupBox != null && !_groupBox.Visible;
                _menuHideGroupBox!.Visible = _groupBox != null && _groupBox.Visible;
                _menuSeparator2!.Visible = _menuGroupByThisColumn.Visible || _menuUngroupByThisColumn.Visible || _menuShowGroupBox.Visible || _menuHideGroupBox.Visible;
                _menuBestFitColumn!.Visible = false;
                _menuSeparator5!.Visible = false;
                _menuConditionalFormatting!.Visible = false;
                _menuFilter.Visible = false;
                _menuClearFilter.Visible = false;
                _menuClearAllFilter.Visible = false;
                _menuShowSearchToolBar.Visible = _searchToolBar != null && !_searchToolBar.Visible;
                _menuHideSearchToolBar.Visible = _searchToolBar != null && _searchToolBar.Visible;

                // Also hide aggregation options if column is invalid
                _menuAggregationNumeric.Visible = false;
                _menuAggregationNonNumeric.Visible = false;
                //((KryptonContextMenuSeparator)_menuItems!.Items[(_menuItems.Items.Count - 3)]).Visible = false; // Hide the separator
            }

            // Ensure we add the main collection of menu items to the context menu if not already present
            if (!_contextMenu!.Items.Contains(_menuItems))
            {
                _contextMenu.Items.Add(_menuItems);
            }

            // Show the menu!
            _contextMenu.Show(this);
        }

        /// <summary>
        /// Clear all sorting columns only (not the grouped ones)
        /// </summary>
        public void ResetAllSortingColumns()
        {
            _internalColumns.MaxSortIndex = -1;
            foreach (OutlookGridColumn col in _internalColumns)
            {
                if (!col.IsGrouped && col.SortDirection != SortOrder.None)
                {
                    col.DataGridViewColumn!.HeaderCell.SortGlyphDirection = SortOrder.None;
                    col.SortDirection = SortOrder.None;
                    col.SortIndex = -1;
                }
            }
#if DEBUG
            _internalColumns.DebugOutput();
#endif
        }

        ///// <summary>
        ///// Sort the grid
        ///// </summary>
        ///// <param name="comparer">The IComparer object.</param>
        //public void Sort(IComparer<OutlookGridRow> comparer)
        //{
        //    Fill();
        //}

        /// <summary>
        /// Clears the internal rows.
        /// </summary>
        public void ClearInternalRows()
        {
            _internalRows.Clear();
            _originalRows?.Clear();
        }

        /// <summary>
        /// Assign the rows to the internal list.
        /// </summary>
        /// <param name="l">List of OutlookGridRows</param>
        public void AssignRows(List<OutlookGridRow> l)
        {
            _internalRows = l;
            _originalRows = new HashSet<OutlookGridRow>(l);
        }

        /// <summary>
        /// Assign the rows to the internal list.
        /// </summary>
        /// <param name="rows">DataGridViewRowCollection</param>
        public void AssignRows(DataGridViewRowCollection rows)
        {
            _internalRows = rows.Cast<OutlookGridRow>().ToList();
            _originalRows = new HashSet<OutlookGridRow>(rows.Cast<OutlookGridRow>());
        }

        /// <summary>
        /// Collapse/Expand all group rows
        /// </summary>
        /// <param name="collapsed">True if collapsed, false if expanded</param>
        private void SetGroupCollapse(bool collapsed)
        {
            if (!IsGridGrouped || _internalRows.Count == 0)
            {
                return;
            }

            //// loop through all rows to find the GroupRows
            //for (int i = 0; i < this.Rows.Count; i++)
            //{
            //    if (((OutlookGridRow)this.Rows[i]).IsGroupRow)
            //        ((OutlookGridRow)this.Rows[i]).Group.Collapsed = collapsed;
            //}
            RecursiveSetGroupCollapse(_groupCollection, collapsed);

            // workaround, make the grid refresh properly
            Rows[0].Visible = !Rows[0].Visible;
            Rows[0].Visible = !Rows[0].Visible;

            //When collapsing the first row still seeing it.
            if (Rows[0].Index < FirstDisplayedScrollingRowIndex)
            {
                FirstDisplayedScrollingRowIndex = Rows[0].Index;
            }
        }

        private void RecursiveSetGroupCollapse(OutlookGridGroupCollection col, bool collapsed)
        {
            for (int i = 0; i < col.Count; i++)
            {
                col[i]!.Collapsed = collapsed;
                RecursiveSetGroupCollapse(col[i]!.Children, collapsed);
            }
        }

        private void SetGroupCollapse(string? c, bool collapsed)
        {
            if (!IsGridGrouped || _internalRows.Count == 0)
            {
                return;
            }

            // loop through all rows to find the GroupRows
            //for (int i = 0; i < this.Rows.Count; i++)
            //{
            //    if (((OutlookGridRow)this.Rows[i]).IsGroupRow && ((OutlookGridRow)this.Rows[i]).Group.Column.DataGridViewColumn.Name == c.Name)
            //        ((OutlookGridRow)this.Rows[i]).Group.Collapsed = collapsed;
            //}
            RecursiveSetGroupCollapse(c, _groupCollection, collapsed);

            // workaround, make the grid refresh properly
            Rows[0].Visible = !Rows[0].Visible;
            Rows[0].Visible = !Rows[0].Visible;

            //When collapsing the first row still seeing it.
            if (Rows[0].Index < FirstDisplayedScrollingRowIndex)
            {
                FirstDisplayedScrollingRowIndex = Rows[0].Index;
            }
        }

        private void RecursiveSetGroupCollapse(string? c, OutlookGridGroupCollection col, bool collapsed)
        {
            for (int i = 0; i < col.Count; i++)
            {
                if (col[i]!.Column.Name == c)
                {
                    col[i]!.Collapsed = collapsed;
                }

                RecursiveSetGroupCollapse(c, col[i]!.Children, collapsed);
            }
        }

        /// <summary>
        /// Collapse/Expand a group row
        /// </summary>
        /// <param name="rowIndex">The index of the group row.</param>
        /// <param name="collapsed">True if collapsed, false if expanded.</param>
        private void SetGroupCollapse(int rowIndex, bool collapsed)
        {
            if (!IsGridGrouped || _internalRows.Count == 0 || rowIndex < 0)
            {
                return;
            }

            OutlookGridRow row = (OutlookGridRow)Rows[rowIndex];
            if (row.IsGroupRow)
            {
                row.Group?.Collapsed = collapsed;

                //this is a workaround to make the grid re-calculate it's contents and background bounds
                // so the background is updated correctly.
                // this will also invalidate the control, so it will redraw itself
                row.Visible = false;
                row.Visible = true;

                //When collapsing the first row still seeing it.
                if (row.Index < FirstDisplayedScrollingRowIndex)
                {
                    FirstDisplayedScrollingRowIndex = row.Index;
                }
            }
        }

        /// <summary>
        /// Expand all nodes
        /// </summary>
        public void ExpandAllNodes()
        {
            if (Rows.Count > 0)
            {
                foreach (OutlookGridRow r in Rows)
                {
                    RecursiveDescendantSetNodeCollapse(r, false);
                }
                Rows[0].Visible = !Rows[0].Visible;
                Rows[0].Visible = !Rows[0].Visible;

                //When collapsing the first row still seeing it.
                if (Rows[0].Index < FirstDisplayedScrollingRowIndex)
                {
                    FirstDisplayedScrollingRowIndex = Rows[0].Index;
                }
            }
        }

        /// <summary>
        /// Collapse all nodes
        /// </summary>
        public void CollapseAllNodes()
        {
            if (Rows.Count > 0)
            {
                foreach (OutlookGridRow r in Rows)
                {
                    RecursiveDescendantSetNodeCollapse(r, true);
                }
                Rows[0].Visible = !Rows[0].Visible;
                Rows[0].Visible = !Rows[0].Visible;

                //When collapsing the first row still seeing it.
                if (Rows[0].Index < FirstDisplayedScrollingRowIndex)
                {
                    FirstDisplayedScrollingRowIndex = Rows[0].Index;
                }
            }
        }

        private void RecursiveDescendantSetNodeCollapse(OutlookGridRow r, bool collapsed)
        {
            //No events - for speed
            if (r.HasChildren)
            {
                r.Collapsed = collapsed;
                foreach (OutlookGridRow r2 in r.Nodes.Nodes)
                {
                    RecursiveDescendantSetNodeCollapse(r2, collapsed);
                }
            }
        }

        //private void RecursiveUpwardSetNodeCollapse(OutlookGridRow r, bool collapsed)
        //{
        //    //No events - for speed
        //    if (r.ParentNode != null)
        //    {
        //        if (r.ParentNode.Collapsed)
        //        {
        //            r.ParentNode.Collapsed = collapsed;
        //            RecursiveUpwardSetNodeCollapse(r.ParentNode, collapsed);
        //        }
        //    }
        //    //sw.Stop();
        //    //Debug.WriteLine(sw.ElapsedMilliseconds.ToString() + " ms" + r.ToString());

        //}

        private void RecursiveUpwardSetNodeCollapse(OutlookGridRow? r, bool collapsed)
        {
            //No events - for speed
            if (r != null && r.ParentNode != null)
            {
                r.ParentNode.Collapsed = collapsed;
                RecursiveUpwardSetNodeCollapse(r.ParentNode, collapsed);
            }
        }


        /// <summary>
        /// Ensure the node is visible (all parents expanded)
        /// </summary>
        /// <param name="r">The OutlookGridRow which needs to be visible.</param>
        public void EnsureVisibleNode(OutlookGridRow r) => RecursiveUpwardSetNodeCollapse(r, false);

        /// <summary>
        /// Collapses the node.
        /// </summary>
        /// <param name="node">The OutlookGridRow node.</param>
        /// <returns></returns>
        public bool CollapseNode(OutlookGridRow node)
        {
            if (!node.Collapsed)
            {
                CollapsingEventArgs exp = new(node);
                OnNodeCollapsing(exp);

                if (!exp.Cancel)
                {
                    node.SetNodeCollapse(true);

                    CollapsedEventArgs expend = new(node);
                    OnNodeCollapsed(expend);
                }

                return !exp.Cancel;
            }
            else
            {
                // row isn't expanded, so we didn't do anything.				
                return false;
            }
        }

        /// <summary>
        /// Expands the node.
        /// </summary>
        /// <param name="node">The OutlookGridRow node.</param>
        /// <returns></returns>
        public bool ExpandNode(OutlookGridRow node)
        {
            if (node.Collapsed)
            {
                ExpandingEventArgs exp = new(node);
                OnNodeExpanding(exp);

                if (!exp.Cancel)
                {
                    node.SetNodeCollapse(false);

                    ExpandedEventArgs expend = new(node);
                    OnNodeExpanded(expend);
                }

                return !exp.Cancel;
            }
            else
            {
                // row isn't expanded, so we didn't do anything.				
                return false;
            }
        }

        /// <summary>
        /// Expand Node and all its SubNodes (without events)
        /// </summary>
        public void ExpandNodeAndSubNodes(OutlookGridRow r)
        {
            RecursiveDescendantSetNodeCollapse(r, false);
            r.Visible = !r.Visible;
            r.Visible = !r.Visible;

            ////When collapsing the first row still seeing it.
            //if (this.Rows[0].Index < this.FirstDisplayedScrollingRowIndex)
            //    this.FirstDisplayedScrollingRowIndex = this.Rows[0].Index;
        }

        /// <summary>
        /// Collapse Node and all its SubNodes (without events)
        /// </summary>
        public void CollapseNodeAndSubNodes(OutlookGridRow r)
        {
            RecursiveDescendantSetNodeCollapse(r, true);
            r.Visible = !r.Visible;
            r.Visible = !r.Visible;

            ////When collapsing the first row still seeing it.
            //if (this.Rows[0].Index < this.FirstDisplayedScrollingRowIndex)
            //    this.FirstDisplayedScrollingRowIndex = this.Rows[0].Index;
        }

        #region Grid Fill functions


        private void NonGroupedRecursiveFillOutlookGridRows(List<OutlookGridRow> l, List<OutlookGridRow> tmp)
        {
            for (int i = 0; i < l.Count; i++)
            {
                tmp.Add(l[i]);

                //Recursive call
                if (l[i].HasChildren)
                {
                    NonGroupedRecursiveFillOutlookGridRows(l[i].Nodes.Nodes, tmp);
                }
            }
        }

        private void FillMinMaxFormatConditions(Type? typeColumn, int j, List<OutlookGridRow> list, int formatColumn)
        {
            if (typeColumn == typeof(TimeSpan))
            {
                for (var i = 0; i < list.Count; i++)
                {
                    if (list[i].Cells[formatColumn].Value != null)
                    {

                        if (((TimeSpan)list[i].Cells[formatColumn].Value!).TotalMinutes < _formatConditions[j].MinValue)
                        {
                            _formatConditions[j].MinValue = ((TimeSpan)list[i].Cells[formatColumn].Value!).TotalMinutes;
                        }

                        if (((TimeSpan)list[i].Cells[formatColumn].Value!).TotalMinutes > _formatConditions[j].MaxValue)
                        {
                            _formatConditions[j].MaxValue = ((TimeSpan)list[i].Cells[formatColumn].Value!).TotalMinutes;
                        }
                    }
                    if (list[i].HasChildren)
                    {
                        FillMinMaxFormatConditions(typeColumn, j, list[i].Nodes.Nodes, formatColumn);
                    }
                }
            }
            else if (typeColumn == typeof(decimal))
            {
                for (var i = 0; i < list.Count; i++)
                {
                    if (list[i].Cells[formatColumn].Value != null)
                    {
                        if (Convert.ToDouble(list[i].Cells[formatColumn].Value) < _formatConditions[j].MinValue)
                        {
                            _formatConditions[j].MinValue = Convert.ToDouble(list[i].Cells[formatColumn].Value);
                        }

                        if (Convert.ToDouble(list[i].Cells[formatColumn].Value) > _formatConditions[j].MaxValue)
                        {
                            _formatConditions[j].MaxValue = Convert.ToDouble(list[i].Cells[formatColumn].Value);
                        }
                    }
                    if (list[i].HasChildren)
                    {
                        FillMinMaxFormatConditions(typeColumn, j, list[i].Nodes.Nodes, formatColumn);
                    }
                }
            }
            else
            {
                for (var i = 0; i < list.Count; i++)
                {
                    if (list[i].Cells[formatColumn].Value != null)
                    {
                        if (Convert.ToDouble(list[i].Cells[formatColumn].Value) < _formatConditions[j].MinValue)
                        {
                            _formatConditions[j].MinValue = (double)list[i].Cells[formatColumn].Value!;
                        }

                        if (Convert.ToDouble(list[i].Cells[formatColumn].Value) > _formatConditions[j].MaxValue)
                        {
                            _formatConditions[j].MaxValue = (double)list[i].Cells[formatColumn].Value!;
                        }
                    }
                    if (list[i].HasChildren)
                    {
                        FillMinMaxFormatConditions(typeColumn, j, list[i].Nodes.Nodes, formatColumn);
                    }
                }
            }
        }

        private void FillValueFormatConditions(int formatColumn, Type? typeColumn, List<OutlookGridRow> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = 0; j < _formatConditions.Count; j++)
                {
                    formatColumn = Columns[_formatConditions[j].ColumnName]!.Index;
                    if (list[i].Cells[formatColumn].Value != null)
                    {
                        typeColumn = Columns[_formatConditions[j].ColumnName]!.ValueType;
                        FormattingCell fCell = new(list[i].Cells[formatColumn])
                        {
                            FormatType = _formatConditions[j].FormatType,
                            FormatParams = _formatConditions[j].FormatParams?.Clone() as IFormatParams
                        };

                        switch (_formatConditions[j].FormatType)
                        {
                            case EnumConditionalFormatType.Bar:
                                if (typeColumn == typeof(TimeSpan))
                                {
                                    (fCell.FormatParams as BarParams)!.ProportionValue =
                                        ColorFormatting.ConvertBar(
                                            ((TimeSpan)list[i].Cells[formatColumn].Value!).TotalMinutes,
                                            _formatConditions[j].MinValue, _formatConditions[j].MaxValue);
                                }
                                else if (typeColumn == typeof(decimal))
                                {
                                    (fCell.FormatParams as BarParams)!.ProportionValue = ColorFormatting.ConvertBar(Convert.ToDouble(list[i].Cells[formatColumn].Value), _formatConditions[j].MinValue, _formatConditions[j].MaxValue);
                                }
                                else
                                {
                                    (fCell.FormatParams as BarParams)!.ProportionValue = ColorFormatting.ConvertBar((double)list[i].Cells[formatColumn].Value!, _formatConditions[j].MinValue, _formatConditions[j].MaxValue);
                                }
                                break;
                            case EnumConditionalFormatType.TwoColorsRange:
                                if (typeColumn == typeof(TimeSpan))
                                {
                                    (fCell.FormatParams as TwoColorsParams)!.ValueColor = ColorFormatting.ConvertTwoRange(((TimeSpan)list[i].Cells[formatColumn].Value!).TotalMinutes, _formatConditions[j].MinValue, _formatConditions[j].MaxValue, (TwoColorsParams)_formatConditions[j].FormatParams!);
                                }
                                else if (typeColumn == typeof(decimal))
                                {
                                    (fCell.FormatParams as TwoColorsParams)!.ValueColor = ColorFormatting.ConvertTwoRange(Convert.ToDouble(list[i].Cells[formatColumn].Value), _formatConditions[j].MinValue, _formatConditions[j].MaxValue, (TwoColorsParams)_formatConditions[j].FormatParams!);
                                }
                                else
                                {
                                    (fCell.FormatParams as TwoColorsParams)!.ValueColor =
                                        ColorFormatting.ConvertTwoRange(
                                            Convert.ToDouble(list[i].Cells[formatColumn].Value), _formatConditions[j].MinValue, _formatConditions[j].MaxValue, (TwoColorsParams)_formatConditions[j].FormatParams!);
                                }
                                //list[i].Cells[formatColumn].Style.SelectionBackColor = list[i].Cells[formatColumn].Style.BackColor;
                                break;
                            case EnumConditionalFormatType.ThreeColorsRange:
                                if (typeColumn == typeof(TimeSpan))
                                {
                                    (fCell.FormatParams as ThreeColorsParams)!.ValueColor = ColorFormatting.ConvertThreeRange(((TimeSpan)list[i].Cells[formatColumn].Value!).TotalMinutes, _formatConditions[j].MinValue, _formatConditions[j].MaxValue, _formatConditions[j].FormatParams as ThreeColorsParams);
                                }
                                else if (typeColumn == typeof(decimal))
                                {
                                    (fCell.FormatParams as ThreeColorsParams)!.ValueColor = ColorFormatting.ConvertThreeRange(Convert.ToDouble(list[i].Cells[formatColumn].Value), _formatConditions[j].MinValue, _formatConditions[j].MaxValue, _formatConditions[j].FormatParams as ThreeColorsParams);
                                }
                                else
                                {
                                    (fCell.FormatParams as ThreeColorsParams)!.ValueColor = ColorFormatting.ConvertThreeRange(Convert.ToDouble(list[i].Cells[formatColumn].Value), _formatConditions[j].MinValue, _formatConditions[j].MaxValue, _formatConditions[j].FormatParams as ThreeColorsParams);
                                }
                                //list[i].Cells[formatColumn].Style.SelectionBackColor = list[i].Cells[formatColumn].Style.BackColor;
                                break;
                        }
                    }
                }

                if (list[i].HasChildren)
                {
                    FillValueFormatConditions(formatColumn, typeColumn, list[i].Nodes.Nodes);
                }
            }
        }


        /// <summary>
        /// Fill the grid (grouping, sorting,...)
        /// </summary>
        public void Fill()
        {
            Cursor.Current = Cursors.WaitCursor;
#if (DEBUG)
            Stopwatch azer = new();
            azer.Start();
#endif
            List<OutlookGridRow> list;
            List<OutlookGridRow> tmp = null!; // = new List<OutlookGridRow>();
            IOutlookGridGroup? grParent = null;
            Rows.Clear();
            _groupCollection.Clear();

            if (_internalRows.Count == 0)
            {
                return;
            }

            list = _internalRows;


            //Apply Formatting
            int formatColumn = 0;
            Type? typeColumn = null;

            //Determine mix and max value
            for (int j = 0; j < _formatConditions.Count; j++)
            {
                formatColumn = Columns[_formatConditions[j].ColumnName]!.Index;
                typeColumn = Columns[_formatConditions[j].ColumnName]!.ValueType;
                _formatConditions[j].MinValue = double.MaxValue;
                _formatConditions[j].MaxValue = double.MinValue;
                FillMinMaxFormatConditions(typeColumn, j, list, formatColumn);
            }

            //Passing the necessary information to cells 
            FillValueFormatConditions(formatColumn, typeColumn, list);

            //End of Formatting
#if DEBUG
            azer.Stop();
            Debug.WriteLine(@"Formatting : " + azer.ElapsedMilliseconds + @" ms");
            azer.Start();
#endif
            // this block is used of grouping is turned off
            // this will simply list all attributes of each object in the list
            if (_internalColumns.CountGrouped() == 0)
            {
                //Applying sort
                //list.Sort(new OutlookGridRowComparer2(_internalColumns.GetIndexAndSortSortedOnlyColumns()));
                var orders = _internalColumns.GetIndexAndSortSortedOnlyColumns();
                if (orders.Count > 0)
                    list.Sort(new OutlookGridRowComparer2(orders));

                //Add rows to underlying DataGridView
                if (_fillMode == GridFillMode.GroupsOnly)
                {
                    tmp = list;
                    /*if (list != null)
                    {
                        Rows.AddRange(list.ToArray());
                    }*/
                }
                else
                {
                    tmp = [];
                    NonGroupedRecursiveFillOutlookGridRows(list, tmp);

                    //Add all the rows to the grid
                    /*if (tmp != null)
                    {
                        Rows.AddRange(tmp.ToArray());
                    }*/
                }

            }
            // this block is used when grouping is used
            // items in the list must be sorted, and then they will automatically be grouped
            else
            {
                //Group part of the job
                //We get the columns that are grouped
                List<OutlookGridColumn>? groupedColumns = _internalColumns.FindGroupedColumns();

                //For each OutlookGridRow in the grid
                for (int j = 0; j < list.Count; j++)
                {
                    //Reload the groups collection for each rows !!
                    OutlookGridGroupCollection children = _groupCollection;

                    //For each grouped column (ordered by GroupIndex)
                    if (groupedColumns != null)
                    {
                        for (int i = 0; i < groupedColumns.Count; i++)
                        {
                            if (i == 0)
                            {
                                grParent = null;
                            }

                            //Gets the stored value
                            object? value = list[j].Cells[groupedColumns[i].DataGridViewColumn!.Index].Value;
                            object? formattedValue;

                            //We get the formatting value according to the type of group (Alphabetic, DateTime,...)
                            var outlookGridGroup = groupedColumns[i].GroupingType;
                            if (outlookGridGroup != null)
                            {
                                outlookGridGroup.Value = value;
                                formattedValue = outlookGridGroup.Value;

                                //We search the corresponding group.
                                IOutlookGridGroup? gr = children.FindGroup(formattedValue);
                                if (gr == null)
                                {
                                    gr = (IOutlookGridGroup)outlookGridGroup.Clone();
                                    gr.ParentGroup = grParent;
                                    gr.Column = groupedColumns[i];
                                    gr.Value = value;
                                    gr.FormatStyle =
                                        groupedColumns[i].DataGridViewColumn!.DefaultCellStyle
                                            .Format; //We can the formatting applied to the cell to the group
                                    if (value is TextAndImage)
                                    {
                                        gr.GroupImage = (value as TextAndImage)?.Image;
                                    }
                                    else if (value is Token token)
                                    {
                                        Bitmap bmp = new(13, 13);
                                        using (Graphics gfx = Graphics.FromImage(bmp))
                                        {
                                            using (SolidBrush brush = new(token.BackColor))
                                            {
                                                gfx.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                                            }
                                        }

                                        gr.GroupImage = bmp;
                                    }
                                    else if (value is Bitmap bitmap)
                                    {
                                        gr.GroupImage = bitmap;
                                    }
                                    //else if (groupedColumns[i].DataGridViewColumn.GetType() == typeof(KryptonDataGridViewRatingColumn))
                                    //{
                                    //    gr.GroupImage = (Image)Resources.OutlookGridImageResources.ResourceManager.GetObject("star" + value.ToString());
                                    //}

                                    gr.Level = i;
                                    children.Add(gr);
                                }

                                //Go deeper in the groups, set current group as parent
                                grParent = gr;
                                children = gr.Children;

                                //if we have browsed all the groups we are sure to be in the right group: add rows and update counters !
                                if (i == groupedColumns.Count - 1)
                                {
                                    list[j].Group = gr;
                                    gr.Rows.Add(list[j]);
                                    RecursiveIncrementParentGroupCounters(gr);
                                }
                            }
                        }
                    }
                }

                //reset local variable for GC
                groupedColumns = null;

                //Sorting part : sort the groups between them and sort the rows inside the groups
                List<Tuple<int, SortOrder, IComparer>> sortList = _internalColumns.GetIndexAndSortSortedOnlyColumns();
                if (sortList.Count > 0)
                {
                    RecursiveSort(_groupCollection, sortList);
                }
                else
                {
                    RecursiveSort(_groupCollection, _internalColumns.GetIndexAndSortGroupedColumns());
                }

                //ReInit!
                tmp = [];
                //Get a list of rows (GroupRow and Non-GroupRow)
                RecursiveFillOutlookGridRows(_groupCollection, tmp);

                //Finally add the rows to underlying DataGridView after all the magic !
                //Rows.AddRange(tmp.ToArray());
            }

            if (tmp != null)
            {
                var firstColumn = this.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
                int firstColumnIndex = firstColumn == null ? -1 : firstColumn.Index;
                bool _requireSummary = _fillMode != GridFillMode.GroupsOnly && _internalColumns.Any(c => c.AggregationType != KryptonOutlookGridAggregationType.None);
                if (_requireSummary && _showGrandTotal)
                {
                    //Create the summary row
                    var row = CreateSummaryRow(null, tmp.Where(r => !r.IsGroupRow && !r.IsSummaryRow).ToList(), firstColumnIndex);
                    if (_summaryGrid != null)
                    {
                        ResetSummaryGrid();
                        var sRow = (OutlookGridRow)row.Clone()!;
                        sRow.IsSummaryRow = true;
                        sRow.CreateCells(_summaryGrid!, row.Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray()!);
                        _summaryGrid.Rows.Add(sRow);
                        AdjustSummaryGridSize();
                    }
                    else
                    {
                        tmp.Add(row);
                    }
                }
                else
                {
                    _summaryGrid?.Visible = false;
                }
                Rows.AddRange(tmp.ToArray());
            }

            Cursor.Current = Cursors.Default;
#if DEBUG
            azer.Stop();
            Debug.WriteLine(@"FillGrid : " + azer.ElapsedMilliseconds + @" ms");
#endif
        }

        /// <summary>
        /// Sort recursively the OutlookGridRows within groups
        /// </summary>
        /// <param name="groupCollection">The OutlookGridGroupCollection.</param>
        /// <param name="sortList">The list of sorted columns</param>
        private void RecursiveSort(OutlookGridGroupCollection groupCollection, List<Tuple<int, SortOrder, IComparer>> sortList)
        {
            //We sort the groups
            if (groupCollection.Count > 0)
            {
                if (groupCollection[0]!.Column.GroupingType!.SortBySummaryCount)
                {
                    groupCollection.Sort(new OutlookGridGroupCountComparer());
                }
                else
                {
                    groupCollection.Sort();
                }
            }

            //Sort the rows inside each group
            for (int i = 0; i < groupCollection.Count; i++)
            {
                //If there is no child group then we have only rows...
                if (groupCollection[i]!.Children.Count == 0 && sortList.Count > 0)
                {
                    //We sort the rows according to the sorted only columns
                    groupCollection[i]!.Rows.Sort(new OutlookGridRowComparer2(sortList));
                }
                //else
                //{
                //    Debug.WriteLine("groupCollection[i].Rows" + groupCollection[i].Rows.Count.ToString());
                //    //We sort the rows according to the group sort (useful for alphabetic for example)
                //    groupCollection[i].Rows.Sort(new OutlookGridRowComparer(groupCollection[i].Column.DataGridViewColumn.Index, internalColumns[groupCollection[i].Column.DataGridViewColumn.Name].SortDirection));
                //}

                //Recursive call for children
                RecursiveSort(groupCollection[i]!.Children, sortList);
            }
        }

        /// <summary>
        /// Update all the parents counters of a group
        /// </summary>
        /// <param name="l">The group which</param>
        private void RecursiveIncrementParentGroupCounters(IOutlookGridGroup? l)
        {
            //Add +1 to the counter
            if (l != null)
            {
                l.ItemCount++;
                if (l.ParentGroup != null)
                {
                    //Recursive call for parent
                    RecursiveIncrementParentGroupCounters(l.ParentGroup);
                }
            }
        }

        /// <summary>
        /// Transform a group in a list of OutlookGridRows. Recursive call
        /// </summary>
        /// <param name="l">The OutlookGridGroupCollection that contains the groups and associated rows.</param>
        /// <param name="tmp">A List of OutlookGridRow</param>
        private void RecursiveFillOutlookGridRows(OutlookGridGroupCollection l, List<OutlookGridRow> tmp)
        {
            OutlookGridRow? gridRow;
            IOutlookGridGroup? gridGroup;
            //OutlookGridRow? grSummaryRow;
            var firstColumn = this.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
            int firstColumnIndex = firstColumn == null ? -1 : firstColumn.Index;
            bool _requireSummary = _fillMode != GridFillMode.GroupsOnly && _internalColumns.Any(c => c.AggregationType != KryptonOutlookGridAggregationType.None);

            //for each group
            for (var i = 0; i < l.List.Count; i++)
            {
                gridGroup = l.List[i];

                //Create the group row
                gridRow = RowTemplate.Clone() as OutlookGridRow;
                gridRow!.Group = gridGroup;
                gridRow.IsGroupRow = true;
                if (gridGroup != null)
                {
                    gridRow.Height = gridGroup.Height;
                    gridRow.MinimumHeight = gridRow.Height; //To handle auto resize rows correctly on high dpi
                    gridRow.CreateCells(this, gridGroup.Value!);
                    tmp.Add(gridRow);

                    //Recursive call
                    if (gridGroup.Children.Count > 0)
                    {
                        RecursiveFillOutlookGridRows(gridGroup.Children, tmp);
                        if (_requireSummary && _showSubTotal)
                        {
                            //var dataRows = gridGroup.Children.List.SelectMany(c => c!.Rows).ToList();
                            var dataRows = GetAllRowsRecursive(gridGroup);
                            //Create the summary row
                            var row = CreateSummaryRow(gridGroup, dataRows, firstColumnIndex);
                            tmp.Add(row);
                        }
                    }

                    //We add the rows associated with the current group
                    if (_fillMode == GridFillMode.GroupsOnly)
                    {
                        tmp.AddRange(gridGroup.Rows);
                    }
                    else
                    {
                        NonGroupedRecursiveFillOutlookGridRows(gridGroup.Rows, tmp);
                    }
                    if (gridGroup.Children.Count == 0 && _requireSummary && _showSubTotal)
                    {
                        //Create the summary row
                        var row = CreateSummaryRow(gridGroup, gridGroup.Rows, firstColumnIndex);
                        tmp.Add(row);
                    }
                }
            }
        }

        /// <summary>
        /// Recursively retrieves all data rows (non-group and non-summary rows) belonging to the current group
        /// and all its descendant child groups. This method uses a pure LINQ approach for recursion.
        /// </summary>
        /// <param name="currentGroup">The current <see cref="IOutlookGridGroup"/> to start collecting rows from.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="OutlookGridRow"/> objects that are data rows within the specified group hierarchy.</returns>
        public static List<OutlookGridRow> GetAllRowsRecursive(IOutlookGridGroup? currentGroup)
        {
            if (currentGroup == null)
            {
                return [];
            }

            // Get current group's direct data rows, filtering out any group or summary rows.
            var directDataRows = (currentGroup.Rows ?? Enumerable.Empty<OutlookGridRow>())
                                 .Where(r => !r.IsGroupRow && !r.IsSummaryRow);

            // Get all data rows from all descendants by recursively flattening the child groups.
            // The Where clause ensures that only actual data rows are collected from the recursive calls.
            var descendantDataRows = currentGroup.Children.List?
                                         .SelectMany(child => GetAllRowsRecursive(child))
                                         .Where(r => !r.IsGroupRow && !r.IsSummaryRow) // Ensure filtering at each level
                                         ?? [];

            // Concatenate the direct data rows with all descendant data rows and convert to a list.
            return directDataRows.Concat(descendantDataRows).ToList();
        }

        #endregion Grid Fill functions

        /// <summary>
        /// Persist the configuration of the KryptonOutlookGrid
        /// </summary>
        /// <param name="path">The path where the .xml file will be saved.</param>
        /// <param name="version">The version of the config file.</param>
        public void PersistConfiguration(string path, string version)
        {
            OutlookGridColumn? col;
            using (XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("OutlookGrid");
                writer.WriteAttributeString("V", version);
                writer.WriteElementString("GroupBox", (_groupBox != null && _groupBox.Visible).ToString());
                writer.WriteElementString("HideColumnOnGrouping", CommonHelper.BoolToString(HideColumnOnGrouping));
                writer.WriteStartElement("Columns");
                for (int i = 0; i < _internalColumns.Count; i++)
                {
                    col = _internalColumns[i];
                    writer.WriteStartElement("Column");
                    writer.WriteElementString("Name", col.Name);
                    writer.WriteStartElement("GroupingType");
                    if (col.GroupingType != null)
                    {
                        writer.WriteElementString("Name", col.GroupingType.GetType().AssemblyQualifiedName); //.GetType().Name.ToString());
                        writer.WriteElementString("OneItemText", col.GroupingType.OneItemText);
                        writer.WriteElementString("XXXItemsText", col.GroupingType.XxxItemsText);
                        writer.WriteElementString("SortBySummaryCount", CommonHelper.BoolToString(col.GroupingType.SortBySummaryCount));
                        //writer.WriteElementString("ItemsComparer", (col.GroupingType.ItemsComparer == null) ? "" : col.GroupingType.ItemsComparer.GetType().AssemblyQualifiedName);
                        if (col.GroupingType.GetType() == typeof(OutlookGridDateTimeGroup))
                        {
                            writer.WriteElementString("GroupDateInterval", ((OutlookGridDateTimeGroup)col.GroupingType).Interval.ToString());
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteElementString("SortDirection", col.SortDirection.ToString());
                    writer.WriteElementString("GroupIndex", col.GroupIndex.ToString());
                    writer.WriteElementString("SortIndex", col.SortIndex.ToString());
                    writer.WriteElementString("Visible", col.DataGridViewColumn!.Visible.ToString());
                    writer.WriteElementString("Width", col.DataGridViewColumn.Width.ToString());
                    writer.WriteElementString("Index", col.DataGridViewColumn.Index.ToString());
                    writer.WriteElementString("DisplayIndex", col.DataGridViewColumn.DisplayIndex.ToString());
                    writer.WriteElementString("RowsComparer", col != null && col.RowsComparer == null ? "" : col?.RowsComparer!.GetType().AssemblyQualifiedName);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                //Conditional formatting
                writer.WriteStartElement("ConditionalFormatting");
                for (int i = 0; i < _formatConditions.Count; i++)
                {
                    _formatConditions[i].Persist(writer);
                }
                writer.WriteEndElement(); // End ConditionalFormatting
                writer.WriteEndElement(); //End OutlookGrid
                writer.WriteEndDocument();
                writer.Flush();
            }
        }

        /// <summary>
        /// Clears everything in the OutlookGrid (groups, rows, columns, DataGridViewColumns). Ready for a completely new start.
        /// </summary>
        public void ClearEverything()
        {
            _groupCollection.Clear();
            _internalRows.Clear();
            _originalRows?.Clear();
            _internalColumns.Clear();
            Columns.Clear();
            ConditionalFormatting.Clear();
            _menuItems = null; // Reset for columns context menu reset the columns list
            _contextMenu = null;
            DataSource = null;
            //Snif everything is gone ! Be Ready for a new start !
        }

        /// <summary>
        /// Finds the column from its name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public OutlookGridColumn? FindFromColumnName(string name) => _internalColumns.FindFromColumnName(name);

        /// <summary>
        /// Finds the column from its index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public OutlookGridColumn? FindFromColumnIndex(int index) => _internalColumns.FindFromColumnIndex(index);

        #endregion OutlookGrid methods


        #region Set DataSource

        /// <summary>
        /// Occurs when a grid column is about to be created, allowing external customization.
        /// </summary>
        /// <remarks>
        /// This event is raised just before a <see cref="DataGridViewColumn"/> is fully initialized and added to the grid.
        /// Subscribers can use this event to apply custom styling, formatting, or behavior to the column.
        /// The `sender` parameter of the event handler will be the <see cref="DataGridViewColumn"/> being created.
        /// </remarks>
        public event EventHandler? OnGridColumnCreating;

        /// <summary>
        /// Occurs when an internal <see cref="OutlookGridColumn"/> is about to be created, allowing external customization of its properties.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This event is raised just before an <see cref="OutlookGridColumn"/> is fully initialized after being derived from a <see cref="DataGridViewColumn"/>.
        /// Subscribers can use this event to apply custom properties, such as <see cref="OutlookGridColumn.AggregationType"/>,
        /// <see cref="OutlookGridColumn.GroupingType"/>, <see cref="OutlookGridColumn.RowsComparer"/>, or <see cref="OutlookGridColumn.AvailableInContextMenu"/>.
        /// </para>
        /// <para>
        /// The `sender` parameter of the event handler will be the <see cref="OutlookGridColumn"/> being customized.
        /// </para>
        /// </remarks>
        public event EventHandler? OnInternalColumnCreating;

        /// <summary>
        /// Gets or sets a value indicating whether internal columns are automatically generated when use SetDataSource method.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if internal columns are automatically generated; otherwise, <see langword="false"/>.
        ///   The default value is <see langword="true"/>.
        /// </value>
        [DefaultValue(true)]
        public bool AutoGenerateInternalColumns { get; set; } = true;

        /// <summary>
        /// Sets the <see cref="System.Data.DataTable"/> as the data source for the KryptonOutlookGrid.
        /// </summary>
        /// <param name="dataTable">The <see cref="System.Data.DataTable"/> to be used as the data source.</param>
        public void SetDataSource([DisallowNull] System.Data.DataTable dataTable)
        {
            ClearEverythingOnSetDataSource();
            SuspendLayout();
            AutoGenerateKryptonColumns = false;

            if (AutoGenerateColumns)
            {
                foreach (System.Data.DataColumn column in dataTable.Columns)
                {
                    DataGridViewColumn col = CreateGridColumn(column.ColumnName, column.DataType);
                    Columns.Add(col);
                    if (AutoGenerateInternalColumns)
                        AddInternalColumn(CreateInternalColumn(col));
                }
            }

            List<OutlookGridRow> l = [];
            if (AutoGenerateColumns)
            {
                foreach (System.Data.DataRow item in dataTable.Rows)
                {
                    if (AutoGenerateColumns)
                    {
                        OutlookGridRow row = new();
                        row.CreateCells(this, item.ItemArray!);
                        l.Add(row);
                    }
                }
            }
            else
            {
                // Cache column mappings once
                Dictionary<int, int> gridColumnToIndexMap = []; // GridColIndex -> DataTableColIndex
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    DataGridViewColumn gridColumn = this.Columns[i];
                    if (!string.IsNullOrEmpty(gridColumn.DataPropertyName) && dataTable.Columns.Contains(gridColumn.DataPropertyName))
                    {
                        gridColumnToIndexMap[i] = dataTable.Columns.IndexOf(gridColumn.DataPropertyName);
                    }
                    else if (!string.IsNullOrEmpty(gridColumn.Name) && dataTable.Columns.Contains(gridColumn.Name))
                    {
                        gridColumnToIndexMap[i] = dataTable.Columns.IndexOf(gridColumn.Name);
                    }
                }
                foreach (System.Data.DataRow item in dataTable.Rows)
                {
                    object[] cellValues = new object[this.Columns.Count];
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        if (gridColumnToIndexMap.TryGetValue(i, out int dataTableColumnIndex))
                        {
                            cellValues[i] = item[dataTableColumnIndex];
                        }
                        else
                        {
                            cellValues[i] = DBNull.Value; // Or appropriate default
                        }
                    }
                    OutlookGridRow row = new();
                    row.CreateCells(this, cellValues);
                    l.Add(row);
                }
            }

            ResumeLayout();
            AssignRows(l);
            ForceRefreshGroupBox();
            Fill();
        }

        /// <summary>
        /// Sets a generic <see cref="System.Collections.Generic.List{T}"/> of model objects
        /// as the data source for the KryptonDataGridView, by manually populating its rows.
        /// </summary>
        /// <typeparam name="T">The type of the model object in the list.</typeparam>
        /// <param name="data">The <see cref="System.Collections.Generic.List{T}"/> of model objects to be used as the data source.</param>
        /// <remarks>
        /// This method explicitly iterates through the provided list, creates <see cref="OutlookGridRow"/>
        /// instances, and populates their cells based on the model's properties and the grid's
        /// column definitions (either auto-generated or pre-defined). This provides granular control
        /// over row and cell creation, adhering to a manual population strategy.
        /// </remarks>
        public void SetDataSource<T>([DisallowNull] List<T> data) where T : class
        {
            ClearEverythingOnSetDataSource();
            SuspendLayout();
            AutoGenerateKryptonColumns = false;

            List<OutlookGridRow> l = [];
            try
            {
                if (AutoGenerateColumns)
                {
                    // Get properties of T to create columns
                    PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in properties)
                    {
                        // Exclude properties that are not readable or indexed
                        if (!prop.CanRead || prop.GetIndexParameters().Length > 0)
                        {
                            continue;
                        }

                        // Create grid column for each public property
                        DataGridViewColumn col = CreateGridColumn(prop.Name, prop.PropertyType);
                        Columns.Add(col);
                        // Apply internal column logic if enabled
                        if (AutoGenerateInternalColumns)
                            AddInternalColumn(CreateInternalColumn(col));
                    }
                }

                // Cache property info and column mappings for performance
                // This avoids repeated reflection lookups inside the row loop
                Dictionary<int, PropertyInfo> gridColIndexToPropertyMap = [];
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    DataGridViewColumn gridColumn = this.Columns[i];
                    string propertyName = gridColumn.DataPropertyName; // Prefer DataPropertyName
                    if (string.IsNullOrEmpty(propertyName))
                    {
                        propertyName = gridColumn.Name; // Fallback to Name
                    }

                    if (!string.IsNullOrEmpty(propertyName))
                    {
                        PropertyInfo? prop = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if (prop != null && prop.CanRead)
                        {
                            gridColIndexToPropertyMap[i] = prop;
                        }
                    }
                }

                foreach (T item in data)
                {
                    OutlookGridRow row = new();
                    object?[] cellValues = new object?[this.Columns.Count];

                    // Populate cellValues array based on column mappings
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        if (gridColIndexToPropertyMap.TryGetValue(i, out PropertyInfo? propInfo))
                        {
                            try
                            {
                                cellValues[i] = propInfo.GetValue(item);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Error getting property '{propInfo.Name}' for item: {ex.Message}");
                                cellValues[i] = DBNull.Value;
                            }
                        }
                        else
                        {
                            // No mapping found for this grid column
                            cellValues[i] = DBNull.Value; // Default value for unmapped columns
                        }
                    }

                    row.CreateCells(this, cellValues!); // Create cells for the row
                    l.Add(row); // Add to the temporary list
                }
            }
            finally
            {
                ResumeLayout(true);
            }

            AssignRows(l);
            ForceRefreshGroupBox();
            Fill();
        }

        /// <summary>
        /// Sets a <see cref="System.Collections.Generic.List{T}"/> of object arrays as the data source for the KryptonDataGridView,
        /// where each inner object array represents a row of cell values.
        /// </summary>
        /// <param name="data">The <see cref="System.Collections.Generic.List{T}"/> of object arrays to be used as the data source.</param>
        /// <remarks>
        /// This method manually populates the grid's rows by iterating through the provided list.
        /// Each inner object array is treated as the direct cell values for a new <see cref="OutlookGridRow"/>.
        /// If <see cref="P:System.Windows.Forms.DataGridView.AutoGenerateColumns"/> is <see langword="true"/>, column types
        /// are inferred by examining values across all rows for each respective column index.
        /// It's crucial that the order of values within each <see langword="object[]"/>
        /// precisely matches the order and expected types of the columns defined in the grid.
        /// </remarks>
        public void SetDataSource([DisallowNull] List<object[]> data)
        {
            ClearEverythingOnSetDataSource();
            SuspendLayout();
            AutoGenerateKryptonColumns = false;

            List<OutlookGridRow> l = [];
            try
            {
                if (AutoGenerateColumns)
                {
                    if (data != null && data.Count > 0)
                    {
                        // Determine the maximum number of columns found in any row
                        // This handles cases where rows might have different lengths,
                        // ensuring we create enough columns for the widest row.
                        int numberOfColumns = data.Max(rowArray => rowArray?.Length ?? 0);

                        // Array to store the inferred type for each column index
                        Type[] inferredColumnTypes = new Type[numberOfColumns];

                        // Initialize all inferred types to string (default fallback)
                        for (int i = 0; i < numberOfColumns; i++)
                        {
                            inferredColumnTypes[i] = typeof(string);
                        }

                        // Iterate through all rows and all columns to infer types
                        for (int colIndex = 0; colIndex < numberOfColumns; colIndex++)
                        {
                            List<Type> typesForThisColumn = [];
                            foreach (object[] rowArray in data)
                            {
                                if (rowArray != null && colIndex < rowArray.Length && rowArray[colIndex] != null)
                                {
                                    typesForThisColumn.Add(rowArray[colIndex].GetType());
                                }
                            }

                            if (typesForThisColumn.Count > 0)
                            {
                                // Logic to find the "best" common type for this column index
                                // Prioritize numeric types over string, widest numeric type if mixed (e.g., double > int)
                                if (typesForThisColumn.All(t => t.IsInteger()))
                                {
                                    inferredColumnTypes[colIndex] = typeof(int);
                                }
                                else if (typesForThisColumn.All(t => t.IsInteger() || t.IsDouble()))
                                {
                                    // If any floating point or decimal, use the widest
                                    inferredColumnTypes[colIndex] = typeof(double); // Default for mixed numeric
                                }
                                else if (typesForThisColumn.All(t => t == typeof(bool)))
                                {
                                    inferredColumnTypes[colIndex] = typeof(bool);
                                }
                                else if (typesForThisColumn.All(t => t == typeof(DateTime)))
                                {
                                    inferredColumnTypes[colIndex] = typeof(DateTime);
                                }
                                // Fallback to string if a mix of incompatible types, or if string is present
                                else if (typesForThisColumn.Any(t => t == typeof(string)))
                                {
                                    inferredColumnTypes[colIndex] = typeof(string);
                                }
                                else // Try to find a common base type for custom objects
                                {
                                    Type commonBase = typesForThisColumn.First();
                                    foreach (var type in typesForThisColumn.Skip(1))
                                    {
                                        while (commonBase != null && !commonBase.IsAssignableFrom(type))
                                        {
                                            commonBase = commonBase.BaseType!;
                                        }
                                        if (commonBase == null) break;
                                    }
                                    inferredColumnTypes[colIndex] = commonBase ?? typeof(string);
                                }
                            }
                            // Else, it remains typeof(string) as initialized
                        }

                        // Now, create the actual DataGridViewColumns based on inferred types
                        for (int i = 0; i < numberOfColumns; i++)
                        {
                            DataGridViewColumn col = CreateGridColumn($"Column{i}", inferredColumnTypes[i]);
                            Columns.Add(col);

                            if (AutoGenerateInternalColumns)
                                AddInternalColumn(CreateInternalColumn(col));
                        }
                    }
                }

                // Populate rows using the provided data
                if (data != null)
                {
                    foreach (object[] itemArray in data)
                    {
                        OutlookGridRow row = new();
                        // Ensure that the number of cells created matches the number of actual columns in the grid.
                        // If itemArray is shorter, the extra cells will be default. If longer, extra values ignored.
                        object[] valuesToUse = new object[this.Columns.Count];
                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            if (itemArray != null && i < itemArray.Length)
                            {
                                valuesToUse[i] = itemArray[i];
                            }
                            else
                            {
                                valuesToUse[i] = DBNull.Value; // Fill missing data with DBNull.Value
                            }
                        }

                        row.CreateCells(this, valuesToUse);
                        l.Add(row);
                    }
                }
            }
            finally
            {
                ResumeLayout(true);
            }

            AssignRows(l);
            ForceRefreshGroupBox();
            Fill();
        }

        /// <summary>
        /// Sets a <see cref="System.Collections.Generic.List{T}"/> of dictionaries as the data source for the KryptonDataGridView,
        /// where each dictionary represents a row with string keys mapping to column names.
        /// </summary>
        /// <param name="data">The <see cref="System.Collections.Generic.List{T}"/> to be used as the data source.</param>
        /// <remarks>
        /// This method manually populates the grid's rows by iterating through the provided list of dictionaries.
        /// It maps the dictionary keys to the grid's column names (first by <see cref="P:System.Windows.Forms.DataGridViewColumn.DataPropertyName"/>
        /// then by <see cref="P:System.Windows.Forms.DataGridViewColumn.Name"/>) to populate each cell.
        /// Column data types are inferred by examining values across all rows for each respective key.
        /// </remarks>
        public void SetDataSource([DisallowNull] List<Dictionary<string, object>> data)
        {
            ClearEverythingOnSetDataSource();
            SuspendLayout();
            AutoGenerateKryptonColumns = false;

            List<OutlookGridRow> l = [];
            try
            {
                if (AutoGenerateColumns)
                {
                    if (data != null && data.Count > 0)
                    {
                        // Get all unique keys from all rows to determine all potential columns
                        // This is the equivalent of 'numberOfColumns' for dictionaries.
                        HashSet<string> allUniqueKeys = [];
                        foreach (var rowDict in data)
                        {
                            if (rowDict != null)
                            {
                                foreach (var key in rowDict.Keys)
                                {
                                    allUniqueKeys.Add(key);
                                }
                            }
                        }

                        Dictionary<string, Type> inferredColumnTypes = [];
                        // Initialize all inferred types to string (default fallback) and iterate through keys
                        foreach (string key in allUniqueKeys)
                        {
                            inferredColumnTypes[key] = typeof(string); // Default type
                        }

                        // Iterate through all unique keys (columns) to infer types based on all rows.
                        foreach (string key in allUniqueKeys)
                        {
                            List<Type> typesForThisColumn = []; // Collect all non-null types for the current key
                            foreach (var rowDict in data)
                            {
                                if (rowDict != null && rowDict.TryGetValue(key, out object? value) && value != null)
                                {
                                    typesForThisColumn.Add(value.GetType());
                                }
                            }

                            if (typesForThisColumn.Count > 0)
                            {
                                // Logic to find the "best" common type for this column key
                                // (Same inference logic as provided previously for Dictionary data)
                                if (typesForThisColumn.All(t => t.IsInteger()))
                                {
                                    inferredColumnTypes[key] = typeof(int);
                                }
                                else if (typesForThisColumn.All(t => t.IsInteger() || t.IsDouble()))
                                {
                                    inferredColumnTypes[key] = typeof(double);
                                }
                                else if (typesForThisColumn.All(t => t == typeof(bool)))
                                {
                                    inferredColumnTypes[key] = typeof(bool);
                                }
                                else if (typesForThisColumn.All(t => t == typeof(DateTime)))
                                {
                                    inferredColumnTypes[key] = typeof(DateTime);
                                }
                                else if (typesForThisColumn.Any(t => t == typeof(string)))
                                {
                                    inferredColumnTypes[key] = typeof(string);
                                }
                                else
                                {
                                    Type commonBase = typesForThisColumn.First();
                                    foreach (var type in typesForThisColumn.Skip(1))
                                    {
                                        while (commonBase != null && !commonBase.IsAssignableFrom(type))
                                        {
                                            commonBase = commonBase.BaseType!;
                                        }
                                        if (commonBase == null) break;
                                    }
                                    inferredColumnTypes[key] = commonBase ?? typeof(string);
                                }
                            }
                            // Else, it remains typeof(string) as initialized
                        }

                        // Now, create the actual DataGridViewColumns based on inferred types
                        // Order them alphabetically by key or by some other preferred order if desired
                        foreach (string key in allUniqueKeys)
                        {
                            DataGridViewColumn col = CreateGridColumn(key, inferredColumnTypes[key]);
                            Columns.Add(col);

                            if (AutoGenerateInternalColumns)
                                AddInternalColumn(CreateInternalColumn(col));
                        }
                    }
                }

                // Cache grid column index to dictionary key mapping for performance
                Dictionary<int, string> gridColIndexToDictKeyMap = [];
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    DataGridViewColumn gridColumn = this.Columns[i];
                    string key = gridColumn.DataPropertyName; // Prefer DataPropertyName
                    if (string.IsNullOrEmpty(key))
                    {
                        key = gridColumn.Name; // Fallback to Name
                    }

                    if (!string.IsNullOrEmpty(key))
                    {
                        gridColIndexToDictKeyMap[i] = key;
                    }
                }

                if (data != null)
                {
                    foreach (Dictionary<string, object> rowDictionary in data)
                    {
                        OutlookGridRow row = new();
                        object[] cellValues = new object[this.Columns.Count];

                        // Populate cellValues array based on grid column order and dictionary keys
                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            if (gridColIndexToDictKeyMap.TryGetValue(i, out string? key))
                            {
                                if (rowDictionary.TryGetValue(key, out object? value))
                                {
                                    cellValues[i] = value;
                                }
                                else
                                {
                                    cellValues[i] = DBNull.Value; // Default value for missing data
                                }
                            }
                            else
                            {
                                // No mapping found for this grid column
                                cellValues[i] = DBNull.Value; // Default value for unmapped columns
                            }
                        }
                        row.CreateCells(this, cellValues);
                        l.Add(row);
                    }
                }
            }
            finally
            {
                ResumeLayout(true);
            }

            AssignRows(l);
            ForceRefreshGroupBox();
            Fill();
        }

        /// <summary>
        /// Sets the data source for the KryptonOutlookGrid, manually generating OutlookGridRow instances
        /// from various supported data source types. This method consolidates all specific SetDataSource overloads.
        /// </summary>
        /// <param name="dataSource">The data source (e.g., DataTable, List&lt;T&gt;, BindingSource, Array, List&lt;object[]&gt;, List&lt;Dictionary&lt;string, object&gt;&gt;).</param>
        /// <remarks>
        /// This method manages column auto-generation and manual row population, providing granular control
        /// over the grid's display while supporting common .NET data collection types. It also triggers
        /// <see cref="OnGridColumnCreating"/> and <see cref="OnInternalColumnCreating"/> events for customization.
        /// </remarks>
        public void SetDataSource(object? dataSource)
        {
            ClearEverythingOnSetDataSource();
            SuspendLayout();
            AutoGenerateKryptonColumns = false;

            List<OutlookGridRow> generatedRows = [];

            try
            {
                IEnumerable? dataItems = null;
                Type? itemType = null; // The type of a single element in the data collection

                // --- 1. Identify the actual enumerable data and infer item type ---
                if (dataSource == null)
                {
                    return;
                }
                else if (dataSource is BindingSource bindingSource)
                {
                    // First, try to get the most specific list from the BindingSource
                    dataItems = bindingSource.List;

                    if (bindingSource.DataSource != null)
                    {
                        Type bsSourceType = bindingSource.DataSource.GetType();

                        // Specific handling for DataSet with DataMember
                        if (bindingSource.DataSource is System.Data.DataSet dataSet && !string.IsNullOrEmpty(bindingSource.DataMember))
                        {
                            if (dataSet.Tables.Contains(bindingSource.DataMember))
                            {
                                // If it's a specific table from a DataSet, the item type is DataRow
                                itemType = typeof(System.Data.DataRow);
                                // Also, directly set dataItems to the DataView for that table for consistency
                                dataItems = dataSet.Tables[bindingSource.DataMember]!.DefaultView;
                            }
                            else if (dataSet.Relations.Contains(bindingSource.DataMember))
                            {
                                // If it's a relation, the item type would typically be DataRowView from the child table
                                // This scenario is more complex for direct row population and usually requires nested grids.
                                // For a flat grid, we assume it's a table name.
                                Debug.WriteLine($"Warning: BindingSource.DataMember '{bindingSource.DataMember}' refers to a DataRelation, which is not directly supported for flat grid population.");
                                itemType = typeof(System.Data.DataRowView); // Still infer DataRowView if possible from the list
                            }
                        }
                        // Direct DataTable, DataView, List<T>, Array, etc.
                        else if (bsSourceType == typeof(System.Data.DataTable))
                        {
                            itemType = typeof(System.Data.DataRow);
                        }
                        else if (bsSourceType == typeof(System.Data.DataView))
                        {
                            itemType = typeof(System.Data.DataRowView);
                        }
                        else if (bsSourceType.IsGenericType && bsSourceType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            itemType = bsSourceType.GetGenericArguments()[0];
                        }
                        else if (bsSourceType.IsArray)
                        {
                            itemType = bsSourceType.GetElementType();
                        }
                        else
                        {
                            // Fallback: Infer type from the first item in the BindingSource's list
                            itemType = bindingSource.List.Cast<object>().FirstOrDefault()?.GetType();
                        }
                    }
                    // If DataMember was not used or didn't resolve to a table, and list is not empty,
                    // try to infer from the list's first item.
                    if (itemType == null && bindingSource.List != null && bindingSource.List.Count > 0)
                    {
                        itemType = bindingSource.List[0]?.GetType();
                    }
                }
                else if (dataSource is System.Data.DataTable dataTable)
                {
                    dataItems = dataTable.Rows;
                    itemType = typeof(System.Data.DataRow);
                }
                else if (dataSource is System.Data.DataView dataView) // Directly binding a DataView
                {
                    dataItems = dataView;
                    itemType = typeof(System.Data.DataRowView);
                }

                if (dataSource is System.Data.DataSet dataSetDirect)
                {
                    // Check if DataGridView.DataMember is specified and refers to a valid table
                    if (!string.IsNullOrEmpty(this.DataMember) && dataSetDirect.Tables.Contains(this.DataMember))
                    {
                        dataItems = dataSetDirect.Tables[this.DataMember]!.DefaultView;
                        itemType = typeof(System.Data.DataRowView);
                    }
                    // Check if DataGridView.DataMember is specified and refers to a valid relation (handle as a warning/debug)
                    else if (!string.IsNullOrEmpty(this.DataMember) && dataSetDirect.Relations.Contains(this.DataMember))
                    {
                        Debug.WriteLine($"Warning: DataGridView.DataMember '{this.DataMember}' refers to a DataRelation. This is not directly supported for flat grid population; consider setting DataMember to a specific table name.");
                        // Fallback to the first table if a relation is specified, as flat grids don't directly show relations.
                        if (dataSetDirect.Tables.Count > 0)
                        {
                            dataItems = dataSetDirect.Tables[0].DefaultView;
                            itemType = typeof(System.Data.DataRowView);
                            Debug.WriteLine("Warning: Falling back to displaying the first table as DataMember referred to a relation.");
                        }
                        else
                        {
                            Debug.WriteLine("Info: DataSet is empty, no tables to display.");
                            return; // Or handle as appropriate if no data
                        }
                    }
                    // If DataGridView.DataMember is not specified, or doesn't match a table/relation,
                    // then we default to the first table in the DataSet.
                    else if (dataSetDirect.Tables.Count > 0)
                    {
                        dataItems = dataSetDirect.Tables[0].DefaultView; // Default to first table's default view
                        itemType = typeof(System.Data.DataRowView);
                        Debug.WriteLine("Warning: DataGridView.DataMember was not specified or did not match a table/relation. Displaying the first table.");
                    }
                    else
                    {
                        Debug.WriteLine("Info: DataSet is empty, no tables to display.");
                        return; // Or handle as appropriate if no data
                    }
                }
                else if (dataSource is IListSource listSource) // E.g., DataSet implements IListSource
                {
                    dataItems = listSource.GetList();
                    if (dataItems is System.Data.DataView dvFromListSource)
                    {
                        itemType = typeof(System.Data.DataRowView);
                    }
                    else if (dataItems is IBindingList blist && blist.Count > 0)
                    {
                        itemType = blist[0]?.GetType();
                    }
                    else if (dataItems is IList list && list.Count > 0) // <--- This handles non-generic IList as well
                    {
                        itemType = list[0]?.GetType();
                    }
                }
                else if (dataSource is IBindingList bindingList) // E.g., BindingList<T>
                {
                    dataItems = bindingList;
                    if (bindingList.GetType().IsGenericType && bindingList.GetType().GetGenericTypeDefinition() == typeof(BindingList<>))
                    {
                        itemType = bindingList.GetType().GetGenericArguments()[0];
                    }
                    else if (bindingList.Count > 0)
                    {
                        itemType = bindingList[0]?.GetType();
                    }
                }
                else if (dataSource is IList list) // E.g., List<T>, Array (T[] also implements IList)
                {
                    dataItems = list;
                    if (list.GetType().IsGenericType && list.GetType().GetGenericTypeDefinition() == typeof(List<>))
                    {
                        itemType = list.GetType().GetGenericArguments()[0];
                    }
                    else if (list.GetType().IsArray)
                    {
                        itemType = list.GetType().GetElementType();
                    }
                    else if (list.Count > 0) // <--- This is the crucial check for non-generic IList or unknown generic types
                    {
                        itemType = list[0]?.GetType();
                    }
                }
                else if (dataSource is IEnumerable enumerable) // General IEnumerable, last resort
                {
                    dataItems = enumerable;
                    itemType = enumerable.Cast<object>().FirstOrDefault()?.GetType();
                }
                else
                {
                    Debug.WriteLine($"KryptonOutlookGrid.SetDataSource: Unsupported DataSource type: {dataSource.GetType().Name}");
                    return;
                }

                // --- 2. Column Generation (if AutoGenerateColumns is enabled and no columns exist) ---
                if (AutoGenerateColumns && Columns.Count == 0 && dataItems != null)
                {
                    System.Data.DataTable? schemaTable = null;

                    // Priority 1: Directly from DataTable/DataView
                    if (dataSource is System.Data.DataTable dtDirect)
                    {
                        schemaTable = dtDirect;
                    }
                    else if (dataSource is System.Data.DataView dvDirect)
                    {
                        schemaTable = dvDirect.Table;
                    }
                    // Priority 2: From BindingSource, respecting DataMember for DataSet
                    else if (dataSource is BindingSource bindingSourceForSchema)
                    {
                        if (bindingSourceForSchema.DataSource is System.Data.DataSet dataSetForSchema && !string.IsNullOrEmpty(bindingSourceForSchema.DataMember))
                        {
                            if (dataSetForSchema.Tables.Contains(bindingSourceForSchema.DataMember))
                            {
                                schemaTable = dataSetForSchema.Tables[bindingSourceForSchema.DataMember];
                            }
                        }
                        else if (bindingSourceForSchema.DataSource is System.Data.DataTable dataTableFromBS)
                        {
                            schemaTable = dataTableFromBS;
                        }
                        else if (bindingSourceForSchema.DataSource is System.Data.DataView dataViewFromBS)
                        {
                            schemaTable = dataViewFromBS.Table;
                        }
                        // Fallback: If BindingSource's list items are DataRowView, get table from current item
                        else if (bindingSourceForSchema.Current is System.Data.DataRowView drvFromBS)
                        {
                            schemaTable = drvFromBS.DataView.Table;
                        }
                    }
                    // Priority 3: From the first item of the inferred dataItems if it's DataRow/DataRowView
                    else if (itemType == typeof(System.Data.DataRow) && dataItems?.Cast<System.Data.DataRow>().FirstOrDefault()?.Table is System.Data.DataTable firstRowTable)
                    {
                        schemaTable = firstRowTable;
                    }
                    else if (itemType == typeof(System.Data.DataRowView) && dataItems?.Cast<System.Data.DataRowView>().FirstOrDefault()?.DataView.Table is System.Data.DataTable firstRowViewTable)
                    {
                        schemaTable = firstRowViewTable;
                    }


                    if (schemaTable != null)
                    {
                        foreach (System.Data.DataColumn column in schemaTable.Columns)
                        {
                            DataGridViewColumn col = CreateGridColumn(column.ColumnName, column.DataType);
                            Columns.Add(col);
                            if (AutoGenerateInternalColumns)
                                AddInternalColumn(CreateInternalColumn(col));
                        }
                    }
                    else if (itemType == typeof(object[]))
                    {
                        object[]? firstItemArray = dataItems!.Cast<object[]>().FirstOrDefault();
                        if (firstItemArray != null)
                        {
                            int numberOfColumns = firstItemArray.Length;
                            Type[] inferredColumnTypes = new Type[numberOfColumns];

                            for (int colIndex = 0; colIndex < numberOfColumns; colIndex++)
                            {
                                List<Type> typesForThisColumn = new();
                                foreach (object[] rowArray in dataItems!.Cast<object[]>())
                                {
                                    if (rowArray != null && colIndex < rowArray.Length && rowArray[colIndex] != null)
                                    {
                                        typesForThisColumn.Add(rowArray[colIndex].GetType());
                                    }
                                }
                                inferredColumnTypes[colIndex] = typesForThisColumn.InferCommonType();

                                DataGridViewColumn col = CreateGridColumn($"Column{colIndex}", inferredColumnTypes[colIndex]);
                                Columns.Add(col);
                                if (AutoGenerateInternalColumns)
                                    AddInternalColumn(CreateInternalColumn(col));
                            }
                        }
                    }
                    else if (itemType == typeof(Dictionary<string, object>))
                    {
                        HashSet<string> allUniqueKeys = [];
                        foreach (var rowDict in dataItems!.Cast<Dictionary<string, object>>())
                        {
                            if (rowDict != null) allUniqueKeys.UnionWith(rowDict.Keys);
                        }

                        //List<string> sortedKeys = allUniqueKeys.OrderBy(k => k).ToList();

                        Dictionary<string, Type> inferredColumnTypesMap = [];
                        //foreach (string key in sortedKeys)
                        foreach (string key in allUniqueKeys)
                        {
                            List<Type> typesForThisColumn = [];
                            foreach (var rowDict in dataItems!.Cast<Dictionary<string, object>>())
                            {
                                if (rowDict != null && rowDict.TryGetValue(key, out object? value) && value != null)
                                {
                                    typesForThisColumn.Add(value.GetType());
                                }
                            }
                            inferredColumnTypesMap[key] = typesForThisColumn.InferCommonType();

                            DataGridViewColumn col = CreateGridColumn(key, inferredColumnTypesMap[key]);
                            Columns.Add(col);
                            if (AutoGenerateInternalColumns)
                                AddInternalColumn(CreateInternalColumn(col));
                        }
                    }
                    else if (itemType != null && itemType != typeof(object)) // For List<T>, BindingList<T>, Arrays of T, custom objects
                    {
                        PropertyInfo[] properties = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        foreach (PropertyInfo prop in properties)
                        {
                            if (!prop.CanRead || prop.GetIndexParameters().Length > 0)
                            {
                                continue;
                            }
                            DataGridViewColumn col = CreateGridColumn(prop.Name, prop.PropertyType);
                            Columns.Add(col);
                            if (AutoGenerateInternalColumns)
                                AddInternalColumn(CreateInternalColumn(col));
                        }
                    }
                }

                // --- 3. Cache column mappings for efficient cell population ---
                Dictionary<int, PropertyInfo> gridColIndexToPropertyMap = [];
                Dictionary<int, int> gridColIndexToDataTableColIndexMap = [];
                Dictionary<int, string> gridColIndexToDictKeyMap = [];

                if (itemType != null)
                {
                    System.Data.DataTable? currentSchemaTable = null;
                    // Consistent schema table retrieval logic
                    if (dataSource is System.Data.DataTable dtMap) currentSchemaTable = dtMap;
                    else if (dataSource is System.Data.DataView dvMap) currentSchemaTable = dvMap.Table;
                    else if (dataSource is BindingSource bsMap)
                    {
                        if (bsMap.DataSource is System.Data.DataSet dsMap && !string.IsNullOrEmpty(bsMap.DataMember))
                        {
                            if (dsMap.Tables.Contains(bsMap.DataMember)) currentSchemaTable = dsMap.Tables[bsMap.DataMember];
                        }
                        else if (bsMap.DataSource is System.Data.DataTable bsDtMap) currentSchemaTable = bsDtMap;
                        else if (bsMap.DataSource is System.Data.DataView bsDvMap) currentSchemaTable = bsDvMap.Table;
                        else if (bsMap.Current is System.Data.DataRowView drvMap) currentSchemaTable = drvMap.DataView.Table;
                    }
                    // It's safe to keep these checks if dataItems can genuinely contain DataRow/DataRowView.
                    // However, given the itemType is determined earlier, you should rely on that.
                    else if (itemType == typeof(System.Data.DataRow) && dataItems?.Cast<System.Data.DataRow>().FirstOrDefault()?.Table is System.Data.DataTable firstRowSchemaTable)
                    {
                        currentSchemaTable = firstRowSchemaTable;
                    }
                    else if (itemType == typeof(System.Data.DataRowView) && dataItems?.Cast<System.Data.DataRowView>().FirstOrDefault()?.DataView.Table is System.Data.DataTable firstRowViewSchemaTable)
                    {
                        currentSchemaTable = firstRowViewSchemaTable;
                    }

                    for (int i = 0; i < Columns.Count; i++)
                    {
                        DataGridViewColumn gridColumn = Columns[i];
                        string dataMappingName = string.IsNullOrEmpty(gridColumn.DataPropertyName) ? gridColumn.Name : gridColumn.DataPropertyName;

                        if (!string.IsNullOrEmpty(dataMappingName))
                        {
                            if (itemType == typeof(System.Data.DataRow) || itemType == typeof(System.Data.DataRowView))
                            {
                                if (currentSchemaTable != null && currentSchemaTable.Columns.Contains(dataMappingName))
                                {
                                    gridColIndexToDataTableColIndexMap[i] = currentSchemaTable.Columns.IndexOf(dataMappingName);
                                }
                            }
                            else if (itemType == typeof(Dictionary<string, object>))
                            {
                                gridColIndexToDictKeyMap[i] = dataMappingName;
                            }
                            // Added handling for IList<T> or Array where T is a custom type
                            else if (itemType != typeof(object[]) && itemType != typeof(object) && itemType != null)
                            {
                                // This now correctly covers custom types in List<T>, BindingList<T>, and Arrays
                                // as well as non-generic IList where itemType was inferred.
                                PropertyInfo? prop = itemType.GetProperty(dataMappingName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                                if (prop != null && prop.CanRead)
                                {
                                    gridColIndexToPropertyMap[i] = prop;
                                }
                            }
                            // For object[] case, mapping is implicit by index, so no specific map needed here.
                        }
                    }
                }


                // --- 4. Populate rows (using the optimized delegate approach) ---
                Action<OutlookGridRow, object, object?[]>? processItemFunction = null;

                // Determine the data item type and select the appropriate processing function *once*
                if (itemType == typeof(System.Data.DataRow))
                {
                    processItemFunction = (row, item, cellValues) =>
                    {
                        System.Data.DataRow dataRow = (System.Data.DataRow)item;
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            if (gridColIndexToDataTableColIndexMap.TryGetValue(i, out int dataTableColIndex))
                            {
                                cellValues[i] = dataRow[dataTableColIndex];
                            }
                            else
                            {
                                cellValues[i] = DBNull.Value;
                            }
                        }
                    };
                }
                else if (itemType == typeof(System.Data.DataRowView))
                {
                    processItemFunction = (row, item, cellValues) =>
                    {
                        System.Data.DataRowView dataRowView = (System.Data.DataRowView)item;
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            if (gridColIndexToDataTableColIndexMap.TryGetValue(i, out int dataTableColIndex))
                            {
                                cellValues[i] = dataRowView.Row[dataTableColIndex];
                            }
                            else
                            {
                                cellValues[i] = DBNull.Value;
                            }
                        }
                    };
                }
                else if (itemType == typeof(object[]))
                {
                    processItemFunction = (row, item, cellValues) =>
                    {
                        object[] itemArray = (object[])item;
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            if (i < itemArray.Length)
                            {
                                cellValues[i] = itemArray[i];
                            }
                            else
                            {
                                cellValues[i] = DBNull.Value;
                            }
                        }
                    };
                }
                else if (itemType == typeof(Dictionary<string, object>))
                {
                    processItemFunction = (row, item, cellValues) =>
                    {
                        Dictionary<string, object> rowDictionary = (Dictionary<string, object>)item;
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            if (gridColIndexToDictKeyMap.TryGetValue(i, out string? key))
                            {
                                if (rowDictionary.TryGetValue(key, out object? value))
                                {
                                    cellValues[i] = value;
                                }
                                else
                                {
                                    cellValues[i] = DBNull.Value;
                                }
                            }
                            else
                            {
                                cellValues[i] = DBNull.Value;
                            }
                        }
                    };
                }
                // THIS IS THE ENHANCED BLOCK FOR GENERIC/CUSTOM OBJECTS (including those from IList<T>, Array)
                else if (itemType != null && itemType != typeof(object))
                {
                    processItemFunction = (row, item, cellValues) =>
                    {
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            if (gridColIndexToPropertyMap.TryGetValue(i, out PropertyInfo? propInfo))
                            {
                                try
                                {
                                    cellValues[i] = propInfo.GetValue(item);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"Error getting property '{propInfo.Name}' for item: {ex.Message}");
                                    cellValues[i] = DBNull.Value;
                                }
                            }
                            else
                            {
                                cellValues[i] = DBNull.Value;
                            }
                        }
                    };
                }

                if (dataItems != null && processItemFunction != null)
                {
                    foreach (var item in dataItems)
                    {
                        if (item == null) continue;

                        OutlookGridRow row = new();
                        object?[] cellValues = new object?[Columns.Count];

                        // Call the pre-selected function
                        processItemFunction(row, item, cellValues);

                        row.CreateCells(this, cellValues!);
                        generatedRows.Add(row);
                    }
                }
                else if (dataItems != null) // Fallback if no specific processor was found (e.g., itemType was typeof(object))
                {
                    // This case might mean auto-generation failed or data is too generic.
                    // Consider logging a warning or throwing an exception if this state indicates an error.
                    // For now, it will simply populate with DBNull.Value if no specific mapping is found.
                    foreach (var item in dataItems)
                    {
                        if (item == null) continue;

                        OutlookGridRow row = new();
                        object?[] cellValues = new object?[Columns.Count];
                        // If no specific processor, cells will remain DBNull.Value unless explicitly set here.
                        // This scenario typically indicates either no columns were generated or a generic type
                        // that couldn't be mapped to properties/columns.
                        row.CreateCells(this, cellValues!); // Creates empty cells
                        generatedRows.Add(row);
                    }
                }

            }
            finally
            {
                ResumeLayout(true);
            }

            // --- 5. Finalization ---
            AssignRows(generatedRows);
            ForceRefreshGroupBox();
            Fill();
        }

        /// <summary>
        /// Clears everything in the OutlookGrid (groups, rows, columns, DataGridViewColumns). Ready for a completely new start.
        /// </summary>
        public void ClearEverythingOnSetDataSource()
        {
            _groupCollection.Clear();
            _internalRows.Clear();
            _originalRows?.Clear();
            if (AutoGenerateColumns)
                Columns.Clear();
            if (AutoGenerateColumns && AutoGenerateInternalColumns)
                _internalColumns.Clear();

            ConditionalFormatting.Clear();
            _menuItems = null; // Reset for columns context menu reset the columns list
            _contextMenu = null;
            DataSource = null;
        }

        /// <summary>
        /// Clears the internal columns.
        /// </summary>
        public void ClearInternalColumns()
        {
            _internalColumns?.Clear();
        }

        /// <summary>
        /// Creates and configures a <see cref="DataGridViewColumn"/> based on the provided column name and data type.
        /// This method acts as a factory for different column types (e.g., checkbox, image, or text)
        /// and sets essential properties like Name, HeaderText, and DataPropertyName.
        /// Please ensure data is loaded using `SetDataSource()`.
        /// </summary>
        /// <param name="columnName">The programmatic name and header text for the column. This will also be used as the <see cref="P:System.Windows.Forms.DataGridViewColumn.DataPropertyName"/>.</param>
        /// <param name="dataType">The <see cref="Type"/> of data this column is intended to display, which helps determine the specific <see cref="DataGridViewColumn"/> type (e.g., <see cref="bool"/> for <see cref="KryptonDataGridViewCheckBoxColumn"/>).</param>
        /// <returns>A new instance of a <see cref="DataGridViewColumn"/> (e.g., <see cref="KryptonDataGridViewTextBoxColumn"/>, <see cref="KryptonDataGridViewCheckBoxColumn"/>, <see cref="KryptonDataGridViewImageColumn"/>) configured for the specified data.</returns>
        protected virtual DataGridViewColumn CreateGridColumn(string columnName, Type dataType)
        {
            if (dataType == typeof(bool))
            {
                KryptonDataGridViewCheckBoxColumn col = new()
                {
                    Name = columnName,
                    HeaderText = columnName,
                    DataPropertyName = columnName,
                    ValueType = dataType,
                    SortMode = DataGridViewColumnSortMode.Programmatic,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                CustomizeGridColumn(col);
                return col;
            }
            else if (dataType == typeof(Image) || dataType == typeof(Bitmap))
            {
                KryptonDataGridViewImageColumn col = new()
                {
                    Name = columnName,
                    HeaderText = columnName,
                    DataPropertyName = columnName,
                    ValueType = dataType,
                    SortMode = DataGridViewColumnSortMode.Programmatic,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                CustomizeGridColumn(col);
                return col;
            }
            else // Default to text column
            {
                KryptonDataGridViewTextBoxColumn col = new()
                {
                    Name = columnName,
                    HeaderText = columnName,
                    DataPropertyName = columnName,
                    ValueType = dataType,
                    SortMode = DataGridViewColumnSortMode.Programmatic,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                CustomizeGridColumn(col);
                return col;
            }
        }

        /// <summary>
        /// Allows to customize the style and properties of a newly created <see cref="DataGridViewColumn"/>.
        /// This method also raises the <see cref="OnGridColumnCreating"/> event, providing an external
        /// point for customization.
        /// </summary>
        /// <param name="column">The <see cref="DataGridViewColumn"/> to customize.</param>
        /// <remarks>
        /// <para>
        /// This method applies base common styling (if any) and then raises the <see cref="OnGridColumnCreating"/> event.
        /// You can subscribe to this event to apply additional or override default styling.
        /// </para>
        /// <para>
        /// **Important Note on Data Loading:**
        /// Please ensure data is loaded into the grid using one of the following methods for columns to be properly generated and accessible for customization:
        /// <list type="bullet">
        /// <item><see cref="SetDataSource(System.Data.DataTable)"/></item>
        /// <item><see cref="SetDataSource{T}(List{T})"/></item>
        /// <item><see cref="SetDataSource(List{object[]})"/></item>
        /// <item><see cref="SetDataSource(List{Dictionary{string, object}})"/></item>
        /// <item><see cref="SetDataSource(object?)"/></item>
        /// </list>
        /// </para>
        /// <para>Here are some examples of how you might customize a column, either directly within an override of this method (if this method is virtual)
        /// or more commonly within an event handler subscribed to <see cref="OnGridColumnCreating"/>:</para>
        /// <list type="bullet">
        /// <item>
        /// <term>Formatting numeric columns with two decimal places and right alignment:</term>
        /// <description>
        /// <code language="csharp">
        /// if (column.ValueType == typeof(decimal) || column.ValueType == typeof(double) || column.ValueType == typeof(float))
        /// {
        ///     column.DefaultCellStyle.Format = "N2";
        ///     column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        /// }
        /// </code>
        /// </description>
        /// </item>
        /// <item>
        /// <term>Setting a background color for specific column types, e.g., a "Status" column:</term>
        /// <description>
        /// <code language="csharp">
        /// if (column.Name == "StatusColumn")
        /// {
        ///     column.DefaultCellStyle.BackColor = Color.LightBlue;
        /// }
        /// </code>
        /// </description>
        /// </item>
        /// <item>
        /// <term>Adjusting header text and making a column read-only:</term>
        /// <description>
        /// <code language="csharp">
        /// if (column.Name == "ID")
        /// {
        ///     column.HeaderText = "Unique Identifier";
        ///     column.ReadOnly = true;
        /// }
        /// </code>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        protected virtual void CustomizeGridColumn(DataGridViewColumn column)
        {
            OnGridColumnCreating?.Invoke(column, EventArgs.Empty);
        }

        /// <summary>
        /// Creates an internal <see cref="OutlookGridColumn"/> from a <see cref="DataGridViewColumn"/>,
        /// determining the appropriate grouping based on the <see cref="DataGridViewColumn.ValueType"/>.
        /// It then calls <see cref="CustomizeInternalColumn(OutlookGridColumn)"/> to allow for further customization
        /// and to raise the <see cref="OnInternalColumnCreating"/> event.
        /// </summary>
        /// <param name="column">The <see cref="DataGridViewColumn"/> from which to create the internal <see cref="OutlookGridColumn"/>.</param>
        /// <returns>A newly created and potentially customized <see cref="OutlookGridColumn"/> instance.</returns>
        protected virtual OutlookGridColumn CreateInternalColumn(DataGridViewColumn column)
        {
            IOutlookGridGroup group = new OutlookGridDefaultGroup(null);
            if (column.ValueType != null && column.ValueType == typeof(DateTime))
                group = new OutlookGridDateTimeGroup(null);

            var col = new OutlookGridColumn(column, group, SortOrder.None, -1, -1);
            CustomizeInternalColumn(col);
            return col;
        }

        /// <summary>
        /// Allows to customize properties of a newly created internal <see cref="OutlookGridColumn"/>.
        /// This method also raises the <see cref="OnInternalColumnCreating"/> event, providing an external
        /// point for customization.
        /// </summary>
        /// <param name="column">The <see cref="OutlookGridColumn"/> to customize.</param>
        /// <remarks>
        /// <para>
        /// This method applies base common styling (if any) and then raises the <see cref="OnInternalColumnCreating"/> event.
        /// You can subscribe to this event to apply additional or override default properties.
        /// </para>
        /// <para>
        /// **Important Note on Data Loading:**
        /// Please ensure data is loaded into the grid using one of the following methods for columns to be properly generated and accessible for customization:
        /// <list type="bullet">
        /// <item><see cref="SetDataSource(System.Data.DataTable)"/></item>
        /// <item><see cref="SetDataSource{T}(List{T})"/></item>
        /// <item><see cref="SetDataSource(List{object[]})"/></item>
        /// <item><see cref="SetDataSource(List{Dictionary{string, object}})"/></item>
        /// <item><see cref="SetDataSource(object?)"/></item>
        /// </list>
        /// </para>
        /// <para>Here are some examples of how you might customize an <see cref="OutlookGridColumn"/>, either directly within an override of this method (if this method is virtual)
        /// or more commonly within an event handler subscribed to <see cref="OnInternalColumnCreating"/>:</para>
        /// <list type="bullet">
        /// <item>
        /// <term>Setting an <see cref="KryptonOutlookGridAggregationType"/> for a specific column:</term>
        /// <description>
        /// <code language="csharp">
        /// if (column.Name == "QuantityColumn")
        /// {
        ///     column.AggregationType = AggregationType.Sum;
        ///     column.AggregationFormatString = "N0"; // Format as number with no decimals
        /// }
        /// </code>
        /// </description>
        /// </item>
        /// <item>
        /// <term>Customizing the grouping behavior for a date column:</term>
        /// <description>
        /// <code language="csharp">
        /// if (column.DataGridViewColumn != null &amp;&amp; column.DataGridViewColumn.ValueType == typeof(DateTime))
        /// {
        ///     column.GroupingType = new OutlookGridDateTimeGroup(null); // Ensure date grouping
        /// }
        /// </code>
        /// </description>
        /// </item>
        /// <item>
        /// <term>Preventing a column from appearing in the context menu:</term>
        /// <description>
        /// <code language="csharp">
        /// if (column.Name == "InternalID")
        /// {
        ///     column.AvailableInContextMenu = false;
        /// }
        /// </code>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        protected virtual void CustomizeInternalColumn(OutlookGridColumn column)
        {
            OnInternalColumnCreating?.Invoke(column, EventArgs.Empty);
        }

        /// <summary>
        /// Automatically creates internal columns for all existing columns in the KryptonOutlookGrid.
        /// </summary>
        public void AutoCreateInternalColumns()
        {
            DataGridViewColumn currentColumn;
            _internalColumns.Clear();
            for (int i = 0; i < ColumnCount; i++)
            {
                currentColumn = Columns[i];
                AddInternalColumn(CreateInternalColumn(currentColumn));
            }
        }

        #endregion Set DataSource

        #region Aggregation

        private KryptonOutlookGrid? _summaryGrid = null;

        /// <summary>
        /// Gets or sets the SummaryGrid associated with this KryptonOutlookGrid.
        /// </summary>
        [Category("Behavior")]
        [Description("Associate the KryptonOutlookGrid for summary with the grid.")]
        [DefaultValue(null)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonOutlookGrid? SummaryGrid
        {
            get => _summaryGrid;
            set
            {
                if (_summaryGrid != value)
                {
                    if (_summaryGrid != null && value == null)
                    {
                        this.ColumnWidthChanged -= KryptonOutlookGrid_ColumnWidthChanged;
                        this.ColumnDisplayIndexChanged -= KryptonOutlookGrid_ColumnDisplayIndexChanged;
                        this.Scroll -= KryptonOutlookGrid_Scroll;
                        this.RowHeadersWidthChanged -= KryptonOutlookGrid_RowHeadersWidthChanged;
                        this.Layout -= KryptonOutlookGrid_Layout;
                    }
                    _summaryGrid = value;
                    if (_summaryGrid != null)
                    {
                        ConfigSummaryGrid();
                        this.ColumnWidthChanged += KryptonOutlookGrid_ColumnWidthChanged;
                        this.ColumnDisplayIndexChanged += KryptonOutlookGrid_ColumnDisplayIndexChanged;
                        this.Scroll += KryptonOutlookGrid_Scroll;
                        this.RowHeadersWidthChanged += KryptonOutlookGrid_RowHeadersWidthChanged;
                        this.Layout += KryptonOutlookGrid_Layout;
                    }
                }
            }
        }

        private bool _showSubTotal = false;
        /// <summary>
        /// Gets or sets a value indicating whether sub-totals should be displayed for grouped columns.
        /// Setting this property to <c>true</c> will show sub-totals for each group,
        /// while setting it to <c>false</c> will hide them.
        /// </summary>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowSubTotal
        {
            get => _showSubTotal;
            set
            {
                if (_showSubTotal != value)
                {
                    _showSubTotal = value;
                    Fill();
                }
            }
        }

        private bool _showGrandTotal = false;
        /// <summary>
        /// Gets or sets a value indicating whether a grand total should be displayed for the entire grid.
        /// Setting this property to <c>true</c> will show a grand total row at the bottom,
        /// while setting it to <c>false</c> will hide it.
        /// </summary>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowGrandTotal
        {
            get => _showGrandTotal;
            set
            {
                if (_showGrandTotal != value)
                {
                    _showGrandTotal = value;
                    Fill();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the column that contains row headers is displayed.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new bool RowHeadersVisible
        {
            get => base.RowHeadersVisible;
            set
            {
                base.RowHeadersVisible = value;
                _summaryGrid?.RowHeadersVisible = base.RowHeadersVisible;
            }
        }

        /// <summary>
        /// Handles the change event for aggregation options in the context menu.
        /// When an aggregation type is selected from the menu, this method updates the
        /// <see cref="OutlookGridColumn.AggregationType"/> for the selected column
        /// and triggers a grid refresh to apply the new aggregation.
        /// </summary>
        /// <param name="sender">The source of the event, expected to be a <see cref="KryptonContextMenuItem"/>.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sender"/> is null.</exception>
        private void OnAggregationChange(object? sender, EventArgs e)
        {
            var item = sender as KryptonContextMenuItem ?? throw new ArgumentNullException(nameof(sender));
            OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected)!;
            if (item != null)
            {
                if (item.Tag != null)
                {
                    var aggregationType = (KryptonOutlookGridAggregationType)Enum.Parse(typeof(KryptonOutlookGridAggregationType), item.Tag.ToString()!);
                    if (col!.AggregationType != aggregationType)
                    {
                        col!.AggregationType = aggregationType;
                        Fill();
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the aggregated value for a given column within a list of rows based on the column's <see cref="KryptonOutlookGridAggregationType"/>.
        /// Supports Sum, Average, Min, Max, and Count.
        /// </summary>
        /// <param name="rows">The list of <see cref="OutlookGridRow"/> objects to aggregate.</param>
        /// <param name="column">The <see cref="OutlookGridColumn"/> for which to perform aggregation,
        /// which also defines the <see cref="KryptonOutlookGridAggregationType"/>.</param>
        /// <returns>
        /// The calculated aggregated value as an <see cref="object"/> (e.g., double for Sum/Average,
        /// object for Min/Max, int for Count), or <c>null</c> if no aggregation is specified
        /// (<see cref="KryptonOutlookGridAggregationType.None"/>) or if an error occurs during calculation (e.g., non-numeric data for numeric aggregations).
        /// </returns>
        private object? CalculateAggregation(List<OutlookGridRow> rows, OutlookGridColumn column)
        {
            if (column.AggregationType == KryptonOutlookGridAggregationType.None)
            {
                return null;
            }

            try
            {
                int columnIndex = column.DataGridViewColumn!.Index;

                switch (column.AggregationType)
                {
                    case KryptonOutlookGridAggregationType.Sum:
                        return rows.Cast<OutlookGridRow>().Sum(r => Convert.ToDouble(r.Cells[columnIndex].Value));
                    case KryptonOutlookGridAggregationType.Count:
                        return rows.Cast<OutlookGridRow>().Count(r => r.Cells[columnIndex].Value != null);
                    case KryptonOutlookGridAggregationType.Average:
                        return rows.Cast<OutlookGridRow>().Average(r => Convert.ToDouble(r.Cells[columnIndex].Value));
                    case KryptonOutlookGridAggregationType.Min:
                        return rows.Cast<OutlookGridRow>().Min(r => r.Cells[columnIndex].Value);
                    case KryptonOutlookGridAggregationType.Max:
                        return rows.Cast<OutlookGridRow>().Max(r => r.Cells[columnIndex].Value);
                    case KryptonOutlookGridAggregationType.MinMax:
                        var minValue = rows.Cast<OutlookGridRow>().Min(r => r.Cells[columnIndex].Value);
                        var maxValue = rows.Cast<OutlookGridRow>().Max(r => r.Cells[columnIndex].Value);
                        string format = column.DataGridViewColumn.DefaultCellStyle.Format;
                        var formattedMinValue = !string.IsNullOrEmpty(format) ? string.Format("{0:" + format + "}", minValue) : minValue;
                        var formattedMaxValue = !string.IsNullOrEmpty(format) ? string.Format("{0:" + format + "}", maxValue) : maxValue;
                        return $"{formattedMinValue} - {formattedMaxValue}";
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                // Return null if there's an issue with conversion or aggregation (e.g., non-numeric data for sum/average)
                return null;
            }
        }

        /// <summary>
        /// Creates a summary row for a given group or a grand total for the entire grid.
        /// </summary>
        /// <param name="gridGroup">
        /// The <see cref="IOutlookGridGroup"/> for which the summary row is being created.
        /// If <c>null</c>, this method generates a 'Grand Total' summary row.
        /// </param>
        /// <param name="rows">
        /// The list of <see cref="OutlookGridRow"/> instances that belong to the specified group (or all rows for grand total).
        /// These rows are used to calculate the aggregated values.
        /// </param>
        /// <param name="firstColumnIndex">
        /// The index of the column where the group's descriptive text (e.g., "Total for [GroupValue]" or "Grand Total")
        /// should be displayed. If -1, no specific column is designated for this text.
        /// </param>
        /// <returns>
        /// An <see cref="OutlookGridRow"/> instance configured as a summary row, containing aggregated values
        /// formatted according to column-specific aggregation types and format strings.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method clones the grid's <see cref="System.Windows.Forms.DataGridView.RowTemplate"/> to initialize the summary row.
        /// It sets properties like <see cref="OutlookGridRow.Group"/> and <see cref="OutlookGridRow.IsSummaryRow"/>.
        /// The row's height is adjusted to match the group's height (if applicable) for correct rendering, especially on high DPI.
        /// </para>
        /// <para>
        /// Cells are populated based on the <see cref="KryptonOutlookGridAggregationType"/> of each column in `_internalColumns`:
        /// <list type="bullet">
        ///     <item>
        ///         For columns with <see cref="KryptonOutlookGridAggregationType.None"/>: If the column is the <paramref name="firstColumnIndex"/>,
        ///         it displays the "Grand Total" or "Total for [GroupValue]" text; otherwise, the cell value is <c>null</c>.
        ///     </item>
        ///     <item>
        ///         For columns with other <see cref="KryptonOutlookGridAggregationType"/>s: The method calls `CalculateAggregation` to compute
        ///         the raw aggregated value. This value is then formatted:
        ///         <list type="bullet">
        ///             <item>First, using the <see cref="System.Windows.Forms.DataGridViewCellStyle.Format"/>
        ///                   of the associated <see cref="System.Windows.Forms.DataGridViewColumn"/>.</item>
        ///             <item>Then, if <see cref="OutlookGridColumn.AggregationFormatString"/> is provided for the column,
        ///                   it formats the entire display string using the group text, aggregation type, and the
        ///                   previously formatted aggregated value (as arguments {0}, {1}, and {2} respectively).</item>
        ///             <item>If <see cref="OutlookGridColumn.AggregationFormatString"/> is not set or fails, it defaults
        ///                   to "[AggregationType]: [FormattedValue]".</item>
        ///             <item>For <see cref="KryptonOutlookGridAggregationType.MinMax"/>, `CalculateAggregation` is assumed to return a string
        ///                   that is used directly.</item>
        ///         </list>
        ///     </item>
        /// </list>
        /// </para>
        /// <para>
        /// A clone of the `grSummaryRow` (named `grSummaryRowForGroup`) is created and stored in
        /// <see cref="IOutlookGridGroup.SummaryRow"/>. This allows for a raw aggregated value to be stored directly on the group,
        /// potentially for internal calculations, while the primary `grSummaryRow` holds the formatted display values.
        /// </para>
        /// </remarks>
        private OutlookGridRow CreateSummaryRow(IOutlookGridGroup? gridGroup, List<OutlookGridRow> rows, int firstColumnIndex)
        {
            string grandTotalText = "Grand Total";
            OutlookGridRow? grSummaryRowForGroup;
            OutlookGridRow? grSummaryRow;
            grSummaryRow = RowTemplate.Clone() as OutlookGridRow;
            grSummaryRow!.Group = gridGroup;
            grSummaryRow.IsSummaryRow = true;

            if (gridGroup != null)
                grSummaryRow.Height = gridGroup.Height;
            grSummaryRow.MinimumHeight = grSummaryRow.Height;

            grSummaryRowForGroup = grSummaryRow.Clone() as OutlookGridRow;

            // Define the group text once
            object groupText = gridGroup == null ? grandTotalText : gridGroup.Value!; // Use gridGroup.Value directly

            // Create cells for the row. This needs to be robust for all columns.
            grSummaryRow.CreateCells(this, new object[_internalColumns.Count]); // Create enough cells for all columns
            grSummaryRowForGroup!.CreateCells(this, new object[_internalColumns.Count]);
            for (int j = 0; j < _internalColumns.Count; j++)
            {
                var col = _internalColumns[j];
                object? aggregatedValue;
                string? displayValue = null;

                if (col.AggregationType == KryptonOutlookGridAggregationType.None)
                {
                    if (firstColumnIndex > -1 && j == firstColumnIndex)
                    {
                        // This is the cell for the group name/grand total in the first column
                        grSummaryRow.Cells[j].Value = groupText;
                    }
                    else
                    {
                        grSummaryRow.Cells[j].Value = null;
                    }
                    continue;
                }

                // Calculate the raw aggregated value
                aggregatedValue = CalculateAggregation(rows, col);

                if (aggregatedValue != null)
                {
                    string formattedAggregatedValueForCell;
                    grSummaryRowForGroup.Cells[j].Value = aggregatedValue;
                    if (col.AggregationType == KryptonOutlookGridAggregationType.MinMax)
                    {
                        // MinMax is special: CalculateAggregation already returns a formatted string
                        formattedAggregatedValueForCell = aggregatedValue.ToString()!;
                    }
                    else
                    {
                        // Apply DefaultCellStyle.Format to the aggregated value
                        string cellValueFormat = col.DataGridViewColumn.DefaultCellStyle.Format;
                        if (!string.IsNullOrEmpty(cellValueFormat))
                        {
                            try
                            {
                                formattedAggregatedValueForCell = string.Format("{0:" + cellValueFormat + "}", aggregatedValue);
                            }
                            catch (FormatException)
                            {
                                formattedAggregatedValueForCell = aggregatedValue.ToString()!;
                            }
                        }
                        else
                        {
                            formattedAggregatedValueForCell = aggregatedValue.ToString()!;
                        }
                    }

                    // Now, use OutlookGridColumn.AggregationFormatString for the *overall summary cell string*
                    // Pass all three potential arguments: GroupText, AggregationType, FormattedValue
                    if (!string.IsNullOrEmpty(col.AggregationFormatString))
                    {
                        try
                        {
                            displayValue = string.Format(
                                col.AggregationFormatString,
                                groupText.ToStringNull() == grandTotalText ? "" : groupText,    // {0}
                                col.AggregationType.ToString(),                                 // {1}
                                formattedAggregatedValueForCell                                 // {2}
                            );
                        }
                        catch (FormatException)
                        {
                            // Fallback if AggregationFormatString is malformed or expects different args
                            displayValue = $"{col.AggregationType}: {formattedAggregatedValueForCell}";
                        }
                    }
                    else
                    {
                        // Default if AggregationFormatString is not set
                        displayValue = $"{col.AggregationType}: {formattedAggregatedValueForCell}";
                    }
                }
                // If aggregatedValue is null, displayValue remains null

                grSummaryRow.Cells[j].Value = displayValue;
            }
            gridGroup?.SummaryRow = grSummaryRowForGroup;
            return grSummaryRow;
        }

        #region Set Total Grid

        /// <summary>
        /// Handles the ColumnDisplayIndexChanged event for the <see cref="KryptonOutlookGrid"/>.
        /// Synchronizes the display order of columns in the summary grid with the main grid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="DataGridViewColumnEventArgs"/> that contains the event data.</param>
        private void KryptonOutlookGrid_ColumnDisplayIndexChanged(object? sender, DataGridViewColumnEventArgs e)
        {
            if (_summaryGrid != null && _summaryGrid.Columns.Count > 0)
            {
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    if (_summaryGrid.ColumnCount > i)
                        _summaryGrid.Columns[i].DisplayIndex = this.Columns[i].DisplayIndex;
                }
            }
        }

        /// <summary>
        /// Handles the Scroll event for the <see cref="KryptonOutlookGrid"/>.
        /// Synchronizes the horizontal scroll offset of the summary grid with the main grid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="ScrollEventArgs"/> that contains the event data.</param>
        private void KryptonOutlookGrid_Scroll(object? sender, ScrollEventArgs e)
        {
            if (_summaryGrid != null && e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                _summaryGrid?.HorizontalScrollingOffset = this.HorizontalScrollingOffset;
        }

        /// <summary>
        /// Handles the ColumnWidthChanged event for the <see cref="KryptonOutlookGrid"/>.
        /// Synchronizes the width of a column in the summary grid with the main grid and adjusts the summary grid's height.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="DataGridViewColumnEventArgs"/> that contains the event data.</param>
        private void KryptonOutlookGrid_ColumnWidthChanged(object? sender, DataGridViewColumnEventArgs e)
        {
            if (_summaryGrid != null && _summaryGrid.ColumnCount > 0 && e.Column.Index < _summaryGrid.ColumnCount)
            {
                _summaryGrid.Columns[e.Column.Index].Width = this.Columns[e.Column.Index].Width;
                AdjustSummaryGridSize();
            }
        }

        /// <summary>
        /// Handles the <see cref="System.Windows.Forms.DataGridView.RowHeadersWidthChanged"/> event.
        /// </summary>
        /// <remarks>
        /// This method synchronizes the <see cref="System.Windows.Forms.DataGridView.RowHeadersWidth"/> property
        /// of the internal <c>_summaryGrid</c> with the current instance's
        /// <see cref="System.Windows.Forms.DataGridView.RowHeadersWidth"/>. This ensures that the row header width
        /// of the summary grid updates whenever the main grid's row header width changes,
        /// maintaining a consistent appearance.
        /// </remarks>
        /// <param name="sender">The source of the event, typically the <see cref="System.Windows.Forms.DataGridView"/> instance that raised the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> object that contains the event data.</param>
        private void KryptonOutlookGrid_RowHeadersWidthChanged(object? sender, EventArgs e)
        {
            _summaryGrid?.RowHeadersWidth = this.RowHeadersWidth;
            AdjustSummaryGridSize();
        }

        private void KryptonOutlookGrid_Layout(object? sender, LayoutEventArgs e)
        {
            AdjustSummaryGridSize();
        }

        /// <summary>
        /// Configures the appearance and properties of the internal summary grid (<see cref="_summaryGrid"/>).
        /// </summary>
        private void ConfigSummaryGrid()
        {
            if (_summaryGrid == null) return;
            _summaryGrid.SuspendLayout();
            // Configure totalGrid appearance and properties
            _summaryGrid.AllowUserToAddRows = false;
            _summaryGrid.AllowUserToDeleteRows = false;
            _summaryGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            _summaryGrid.AutoGenerateColumns = false;
            _summaryGrid.AutoGenerateKryptonColumns = false;
            _summaryGrid.ColumnHeadersVisible = false;
            _summaryGrid.Enabled = false;
            _summaryGrid.ReadOnly = true;
            _summaryGrid.RowHeadersVisible = this.RowHeadersVisible;
            _summaryGrid.RowHeadersWidth = this.RowHeadersWidth;
            _summaryGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _summaryGrid.ScrollBars = ScrollBars.None;
            _summaryGrid.TabStop = false;
            _summaryGrid.Rows.Clear();
            _summaryGrid.Columns.Clear();
            //ConfigSummaryGridState();
            _summaryGrid.ResumeLayout();
        }

        private void ConfigSummaryGridState()
        {
            if (_summaryGrid == null) return;
            PaletteState state = PaletteState.Normal;
            _summaryGrid.RowHeadersDefaultCellStyle.Font = StateCommon.HeaderRow.Content.Font ?? StateCommon.HeaderRow.Content.GetContentShortTextFont(state);
            _summaryGrid.RowHeadersDefaultCellStyle.Padding = StateCommon.HeaderRow.Content.GetBorderContentPadding(null, state);
            _summaryGrid.RowHeadersDefaultCellStyle.BackColor = StateNormal.HeaderRow.Back.GetBackColor1(state);
            _summaryGrid.RowHeadersDefaultCellStyle.ForeColor = StateNormal.HeaderRow.Content.GetContentShortTextColor1(state);
        }

        /// <summary>
        /// Resets the summary grid by clearing its rows and columns, and then populating its columns
        /// based on the columns of the main <see cref="KryptonOutlookGrid"/>.
        /// This method ensures the summary grid's column structure mirrors the main grid, including width,
        /// display index, visibility, value type, alignment, and format.
        /// </summary>
        private void ResetSummaryGrid()
        {
            if (_summaryGrid == null) return;
            _summaryGrid.SuspendLayout();
            _summaryGrid.Rows.Clear();
            _summaryGrid.Columns.Clear();
            // Add columns to totalGrid
            foreach (DataGridViewColumn parentCol in this.Columns)
            {
                try
                {
                    var index = _summaryGrid.Columns.Add(parentCol.Name, "");
                    _summaryGrid.Columns[index].Width = parentCol.Width;
                    _summaryGrid.Columns[index].DisplayIndex = parentCol.DisplayIndex;
                    _summaryGrid.Columns[index].Visible = parentCol.Visible;
                    _summaryGrid.Columns[index].ValueType = parentCol.ValueType;
                    _summaryGrid.Columns[index].DefaultCellStyle.Alignment = parentCol.DefaultCellStyle.Alignment;
                    _summaryGrid.Columns[index].DefaultCellStyle.Format = parentCol.DefaultCellStyle.Format;
                    _summaryGrid.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                catch (Exception) { /* Handle exceptions gracefully */ }
            }
            _summaryGrid.Visible = true;
            _summaryGrid.ResumeLayout();
        }

        /// <summary>
        /// Adjusts the height of the summary grid to fit its content.
        /// It calculates the preferred height of the first row and sets the summary grid's height accordingly.
        /// </summary>
        private void AdjustSummaryGridSize()
        {
            // The previous _summaryGrid (now this.SummaryGrid)
            if (_summaryGrid != null && _summaryGrid.Rows.Count > 0)
            {
                /*if (_summaryGrid.Columns.Contains("ColScroll"))
                    _summaryGrid.Columns.Remove("ColScroll");*/
                _summaryGrid.Height = _summaryGrid.Rows[0].Height;
                //var sHeight = (_summaryGrid.Rows[0] as OutlookGridRow)!.GetPreferredHeight(0, DataGridViewAutoSizeRowMode.AllCells, false);
                //_summaryGrid.Height = sHeight;
                int mainGridScrollbarWidth = this.VerticalScrollBar.Visible ? this.VerticalScrollBar.Width : 0;
                if (mainGridScrollbarWidth > 0)
                {
                    if (!_summaryGrid.Columns.Contains("ColScroll"))
                        _summaryGrid.Columns.Add("ColScroll", "");
                    int totalVisibleWidth = this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Sum(c => c.Width);
                    int rowHeaderWidth = this.RowHeadersVisible ? this.RowHeadersWidth : 0;
                    //int availableWidth = this.Width - (rowHeaderWidth + totalVisibleWidth + mainGridScrollbarWidth);
                    int availableWidth = this.Width - (rowHeaderWidth + totalVisibleWidth);
                    //_summaryGrid.Columns["ColScroll"]?.Width = mainGridScrollbarWidth + availableWidth;
                    _summaryGrid.Columns["ColScroll"]?.Width = Math.Max(mainGridScrollbarWidth, availableWidth);
                    _summaryGrid.HorizontalScrollingOffset = this.HorizontalScrollingOffset;
                    if (availableWidth - mainGridScrollbarWidth > 0)
                        _summaryGrid.Columns["ColScroll"]?.Visible = false;
                    else
                        _summaryGrid.Columns["ColScroll"]?.Visible = true;
                }
                else
                {
                    _summaryGrid.Columns["ColScroll"]?.Visible = false;
                }
                _summaryGrid.ClearSelection();
            }
        }

        #endregion Set Total Grid

        #endregion Aggregation

        #region Filter

        /// <summary>
        /// Gets or sets a value indicating whether a column filter should be displayed for the column context menu.
        /// Setting this property to <c>true</c> will show a filter option in column context menu,
        /// while setting it to <c>false</c> will hide it.
        /// </summary>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowColumnFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the column filter should apply in real-time as the user input changes.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if the column filter applies immediately as input changes;
        ///   <see langword="false"/> if the filter requires an explicit action (ok button) to take effect.
        ///   The default value for this property is <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// This property is typically used in conjunction with <see cref="ShowColumnFilter"/>.
        /// When set to <see langword="true"/>, the grid will filter its data dynamically based on the user's input in the column filter UI,
        /// providing an interactive and immediate filtering experience.
        /// <para/>
        /// **Performance Note:** Enabling <see cref="LiveColumnFilter"/> may lead to lower performance when dealing with a very large number of records,
        /// as filtering operations are executed continuously with each user input. Consider setting this to <see langword="false"/>
        /// for improved responsiveness in such scenarios, requiring an explicit action to apply the filter.
        /// <para/>
        /// </remarks>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool LiveColumnFilter { get; set; }

        /// <summary>
        /// Handles the click event for the filter button, opening a filter dialog for the selected column.
        /// </summary>
        /// <param name="sender">The source of the event, typically the filter button.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void OnFilterClick(object? sender, EventArgs e)
        {
            KryptonOutlookGridFilter? filterBuilder = null;
            try
            {
                var col = _internalColumns.FindFromColumnIndex(_colSelected)!;
                var gridColumn = col.DataGridViewColumn;
                var valueType = gridColumn.ValueType ?? typeof(string);
                KryptonOutlookGridFilterSourceColumn column = new(gridColumn.Name, gridColumn.HeaderText, valueType.Name, gridColumn.DefaultCellStyle.Format);
                filterBuilder = new(column, col.Filters)
                {
                    Text = $"Filter for {col.DataGridViewColumn.HeaderText}"
                };
                if (LiveColumnFilter)
                    filterBuilder.FilterChanged += FilterBuilder_FilterChanged;
                var result = filterBuilder.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var filterData = filterBuilder.FilterData;
                    if (filterData == null || filterData.Count == 0)
                        col.Filters = null;
                    else
                        col.Filters = filterData;
                    ApplyFilter();
                }
            }
            finally
            {
                if (filterBuilder != null && LiveColumnFilter)
                    filterBuilder.FilterChanged -= FilterBuilder_FilterChanged;
            }
        }

        /// <summary>
        /// Handles the <see cref="KryptonOutlookGridFilter.FilterChanged"/> event from the filter builder dialog,
        /// applying the filter to the data when the filter criteria change.
        /// </summary>
        /// <param name="sender">The source of the event, which is the <see cref="KryptonOutlookGridFilter"/> instance.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void FilterBuilder_FilterChanged(object sender, EventArgs e)
        {
            if (sender == null)
            {
                ApplyFilter();
                return;
            }
            KryptonOutlookGridFilter currentFilter = (KryptonOutlookGridFilter)sender;
            var fData = currentFilter.FilterData;
            if (fData != null && fData.Count == 0)
                fData = null;
            //if (fData != null)
            ApplyFilter(fData);
        }

        /// <summary>
        /// Handles the click event for the clear filter button, clear a filter for the selected column.
        /// </summary>
        /// <param name="sender">The source of the event, typically the clear filter button.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void OnClearFilterClick(object? sender, EventArgs e)
        {
            var col = _internalColumns.FindFromColumnIndex(_colSelected)!;
            ClearColumnFilter(col.Name!);
        }

        /// <summary>
        /// Handles the click event for the clear all filter button, clear a filter for the all columns.
        /// </summary>
        /// <param name="sender">The source of the event, typically the clear all filter button.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void OnClearAllFilterClick(object? sender, EventArgs e)
        {
            ClearAllColumnFilter();
        }

        /// <summary>
        /// Applies the current set of filters to the grid's data, updating the displayed rows.
        /// </summary>
        /// <param name="currentFilter">An optional list of <see cref="KryptonOutlookGridFilterField"/> objects representing
        /// filters to be applied immediately. If null, existing column filters are used.</param>
        public void ApplyFilter(List<KryptonOutlookGridFilterField>? currentFilter = null)
        {
            int grpInfo = 1;
            List<KryptonOutlookGridFilterField> allFilters = [];
            List<OutlookGridColumn> filteredColumns = [];
            var list = _originalRows;
            //_internalRows = _originalRows.ToList();

            if (currentFilter != null)
                filteredColumns = _internalColumns.Where(col => col.Filters != null && col.DataGridViewColumn.Index != _colSelected).ToList();
            else
                filteredColumns = _internalColumns.Where(col => col.Filters != null).ToList();

            if (ToolBarFilters != null && ToolBarFilters.Count > 0)
            {
                var fExpression = ToolBarFilters.ToExpression<OutlookGridRow>(Columns);
                // compile and use expression
                Func<OutlookGridRow, bool> cFilter = fExpression.Compile();
                list = new HashSet<OutlookGridRow>(_originalRows.Where(cFilter));
            }

            if (filteredColumns.Count == 0 && currentFilter == null)
            {
                _internalRows = list.ToList();
                Fill();
                return;
            }
            foreach (var col in filteredColumns)
            {
                var filters = col.Filters!;
                for (int i = 0; i < filters.Count; i++)
                {
                    if (string.IsNullOrEmpty(filters[i].GroupInfo) || filters[i].IsGroupInfoTemp)
                    {
                        filters[i].GroupInfo = grpInfo.ToString();
                        filters[i].GroupConjunction = "AND";
                        filters[i].IsGroupInfoTemp = true;
                    }
                }
                allFilters.AddRange(filters);
                grpInfo++;
            }
            if (currentFilter != null)
            {
                if (currentFilter.Count == 0 && allFilters.Count == 0) return;
                for (int i = 0; i < currentFilter.Count; i++)
                {
                    if (string.IsNullOrEmpty(currentFilter[i].GroupInfo) || currentFilter[i].IsGroupInfoTemp)
                    {
                        currentFilter[i].GroupInfo = grpInfo.ToString();
                        currentFilter[i].GroupConjunction = "AND";
                        currentFilter[i].IsGroupInfoTemp = true;
                    }
                }
                allFilters.AddRange(currentFilter);
            }

            var filterExpression = allFilters.ToExpression<OutlookGridRow>(Columns);
            // compile and use expression
            Func<OutlookGridRow, bool> compiledFilter = filterExpression.Compile();
            _internalRows = list.Where(compiledFilter).ToList();
            /*var filteredResults = _internalRows.Where(compiledFilter).ToList();
            _internalRows = filteredResults;*/

            Fill();
        }

        /// <summary>
        /// Clears the filter for a specific column and reapplies the overall grid filter.
        /// </summary>
        /// <param name="columnName">The name of the column for which to clear the filter.</param>
        /// <remarks>
        /// This method looks up the column by its <paramref name="columnName"/>.
        /// If a filter is currently applied to that column (i.e., its <c>Filters</c> property is not <c>null</c>),
        /// the filter is removed by setting <c>Filters</c> to <c>null</c>.
        /// After clearing the column's filter, <see cref="ApplyFilter"/> is invoked
        /// to refresh the grid's display based on the updated filter criteria across all columns.
        /// </remarks>
        public void ClearColumnFilter(string columnName)
        {
            var col = _internalColumns[columnName]!;
            if (col.Filters != null)
            {
                col.Filters = null;
                ApplyFilter();
            }
        }

        /// <summary>
        /// Clears filters from all columns that currently have them applied and reapplies the overall grid filter.
        /// </summary>
        /// <remarks>
        /// This method iterates through all internal columns to identify those with active filters (where <c>Filters</c> is not <c>null</c>).
        /// For each identified column, its filter is cleared by setting its <c>Filters</c> property to <c>null</c>.
        /// After all column filters have been cleared, <see cref="ApplyFilter"/> is called
        /// to update the grid's display, showing all data unconstrained by column-specific filters.
        /// </remarks>
        public void ClearAllColumnFilter()
        {
            var filteredColumns = _internalColumns.Where(col => col.Filters != null).ToList();
            if (filteredColumns != null && filteredColumns.Count > 0)
            {
                for (int i = 0; i < filteredColumns.Count; i++)
                {
                    filteredColumns[i].Filters = null;
                }
                ApplyFilter();
            }
        }

        #endregion Filter

        #region Search

        /// <summary>
        /// Occurs when a search operation has completed.
        /// </summary>
        /// <remarks>
        /// This event is typically raised after an asynchronous search process finishes,
        /// allowing subscribers to perform actions based on the search results or completion status.
        /// The event handler will receive the sender of the event and an <see cref="System.EventArgs"/> object.
        /// </remarks>
        public event EventHandler? OnSearchCompleted;

        private bool _enableSearchOnKeyPress = false;
        /// <summary>
        /// Gets or sets a value indicating whether search functionality is enabled on key press.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if search on key press is enabled; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// This custom search functionality is specifically designed to work **only when the associated KryptonOutlookGrid's <see cref="DataGridView.ReadOnly"/> property is set to <see langword="true"/>.**
        /// When <see cref="EnableSearchOnKeyPress"/> is <see langword="true"/> and the KryptonOutlookGrid is read-only, typing will trigger a search.
        /// If the KryptonOutlookGrid is **not** read-only (<see cref="DataGridView.ReadOnly"/> is <see langword="false"/>), this search on key press feature will be **inactive**, regardless of this property's setting.
        /// </remarks>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool EnableSearchOnKeyPress
        {
            get => _enableSearchOnKeyPress;
            set
            {
                if (_enableSearchOnKeyPress != value)
                {
                    _enableSearchOnKeyPress = value;
                    if (_enableSearchOnKeyPress)
                    {
                        this.KeyPress += KryptonOutlookGrid_KeyPress;
                    }
                    else
                    {
                        this.KeyPress -= KryptonOutlookGrid_KeyPress;
                    }
                }
            }
        }

        private bool _highlightSearchText = false;
        /// <summary>
        /// Gets or sets a value indicating whether search text should be highlighted in the cells.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if search text should be highlighted; otherwise, <see langword="false"/>.
        /// The default is <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// For this property to take effect, the <see cref="EnableSearchOnKeyPress"/> property must also be set to <see langword="true"/>.
        /// </remarks>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool HighlightSearchText
        {
            get => _highlightSearchText;
            set
            {
                if (_highlightSearchText != value)
                {
                    _highlightSearchText = value;
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="System.Windows.Forms.Control.KeyPress"/> event for the KryptonOutlookGrid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="KeyPressEventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// This method implements a "search-as-you-type" functionality within the grid.
        /// If <c>EnableSearchOnKeyPress</c> is true and the grid is not read-only,
        /// it appends typed characters to a <c>SearchText</c> property and initiates a search.
        /// Pressing the Backspace key (ASCII 8) will remove the last character from the search text.
        /// Non-printable characters (ASCII &lt; 32) and extended characters (ASCII > 128) are ignored.
        /// The <see cref="OnSearchCompleted"/> event is raised after each search operation.
        /// </remarks>
        public virtual void KryptonOutlookGrid_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!EnableSearchOnKeyPress || !this.ReadOnly)
            {
                return;
            }
            // Handle Backspace key if SearchText is not empty
            if (e.KeyChar == (char)8 && SearchText.Length > 0)
            {
                SearchText = SearchText.Substring(0, SearchText.Length - 1);
                SearchText = Search(SearchText);
            }
            else if (e.KeyChar >= (char)32 && e.KeyChar <= (char)128) // Handle printable ASCII characters (space to tilde)
            {
                SearchText += e.KeyChar;
                SearchText = Search(SearchText);
            }
            OnSearchCompleted?.Invoke(this, e);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the formatted cell value should be used for searching.
        /// When set to <c>true</c>, the value displayed in the cell (after applying formatting like currency, dates, etc.)
        /// will be used for search operations. When <c>false</c>, the underlying cell value (e.g., the raw number or date)
        /// will be used for searching, which might not match the user's visual representation.
        /// The default value is <c>true</c>.
        /// </summary>
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool UseFormattedValueForSearch { get; set; } = true;

        /// <summary>
        /// Stores the current search text entered by the user.
        /// </summary>
        /// <remarks>
        /// This field is updated as the user types, typically in conjunction with a "search-as-you-type" feature.
        /// It is used by the search logic to filter or locate items within the grid.
        /// </remarks>
        public string SearchText = string.Empty;

        /// <summary>
        /// Performs a search operation within the KryptonOutlookGrid based on the provided text.
        /// </summary>
        /// <param name="text">The text to search for.</param>
        /// <param name="searchColumnIndex">
        /// The index of the column to search within. If -1 (default), the search will consider
        /// the current cell's column, or the first visible column if no cell is current.
        /// </param>
        /// <returns>
        /// The actual text that was matched or used for the search, which might be a processed version of the input 'text'.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the grid's data source is not loaded or is in an unsupported format for searching.
        /// This method supports searching within `OutlookGridRow` collections, `System.Data.DataTable`,
        /// or directly populated rows when no `DataSource` is assigned.
        /// </exception>
        /// <remarks>
        /// <para>
        /// After the search, if any rows remain, it attempts to set the <see cref="System.Windows.Forms.DataGridView.CurrentCell"/>
        /// to the first visible cell of the first visible row, skipping group rows if present.
        /// </para>
        /// </remarks>
        public string Search(string text, int searchColumnIndex = -1)
        {
            int currentCellIndex = searchColumnIndex < 0 ? this.CurrentCell?.ColumnIndex ?? -1 : searchColumnIndex;
            int currentRowIndex;
            string matchText;
            if (_originalRows != null)
            {
                matchText = SearchInOutlookGridRows(text, searchColumnIndex);
            }
            else if (this.DataSource != null)
            {
                var dataTable = SharedDataFunctions.GetSourceTable(this);
                if (dataTable == null)
                {
                    throw new InvalidOperationException(
                        "Search cannot be performed. The grid's data source is either not loaded or is in an unsupported format for searching. " +
                        "Please ensure data is loaded using `AssignRows()`, `SetDataSource()`, by assigning a `DataTable` to the `DataSource` property, " +
                        "or by directly populating grid rows when `DataSource` is `null`."
                    );
                }
                matchText = SearchInDataTable(text, dataTable);
            }
            else if (this.DataSource == null)
            {
                matchText = SearchWithoutSource(text, searchColumnIndex);
            }
            else
            {
                // This 'else' block means:
                // - _originalRows is null or empty.
                // - DataSource is NOT a DataTable.
                // - DataSource is NOT null (meaning DataSource is set to some *other* object type that is not handled for search).
                // In this specific scenario, the grid's data source is set to an unrecognized or unsupported type for searching.
                throw new InvalidOperationException(
                    "Search cannot be performed. The grid's data source is either not loaded or is in an unsupported format for searching. " +
                    "Please ensure data is loaded using `AssignRows()`, `SetDataSource()`, by assigning a `DataTable` to the `DataSource` property, " +
                    "or by directly populating grid rows when `DataSource` is `null`."
                );
            }

            if (Rows.Count > 0)
            {
                if (currentCellIndex == -1)
                    currentCellIndex = this.Columns.GetFirstColumn(DataGridViewElementStates.Visible)!.Index;
                currentRowIndex = this.Rows.GetFirstRow(DataGridViewElementStates.Visible);
                if (this.Rows[currentRowIndex] is OutlookGridRow oRow && oRow.IsGroupRow)
                {
                    currentRowIndex++;
                }
                this.CurrentCell = this[currentCellIndex, currentRowIndex];
            }
            return matchText;
        }

        /// <summary>
        /// Searches the rows of the OutlookGrid based on the provided text, either in a specific column or all visible columns.
        /// The filtered rows are then applied to the grid via the internal `Fill()` method.
        /// </summary>
        /// <param name="textToSearch">The text to search for within the grid rows.</param>
        /// <param name="searchColumnIndex">The zero-based index of the column to search. If -1, all visible columns are searched.</param>
        /// <returns>The <paramref name="textToSearch"/> if at least one row matches the search criteria; otherwise, returns an empty string.</returns>
        /// <remarks>
        /// This method directly filters an internal collection of OutlookGridRow objects (`_originalRows`)
        /// and updates the grid's displayed rows (`_internalRows`) by calling `Fill()`.
        /// This approach is used when filtering by `BindingSource.Filter` is not applicable or desired.
        /// It performs a case-insensitive substring search.
        /// </remarks>
        private string SearchInOutlookGridRows(string textToSearch, int searchColumnIndex = -1)
        {
            List<OutlookGridRow> filteredRows = new();

            if (string.IsNullOrEmpty(textToSearch))
            {
                _internalRows = _originalRows.ToList();
                Fill();
                return string.Empty;
            }
            Func<OutlookGridRow, bool> rowFilterPredicate;
            if (searchColumnIndex >= 0)
            {
                // Search in a specific column
                rowFilterPredicate = row =>
                {
                    // Validate column index to prevent ArgumentOutOfRangeException
                    if (searchColumnIndex >= row.Cells.Count)
                    {
                        return false; // Column index out of bounds for this row
                    }

                    DataGridViewCell cell = row.Cells[searchColumnIndex];
                    string cellValue = UseFormattedValueForSearch ?
                                       cell.FormattedValue.ToStringNull() :
                                       cell.Value.ToStringNull();
                    return cellValue.Contains(textToSearch, StringComparison.OrdinalIgnoreCase);
                };
            }
            else // searchColumnIndex is -1: Search all visible columns
            {
                rowFilterPredicate = row => row.Cells.Cast<DataGridViewCell>()
                                                   .Any(cell =>
                                                   {
                                                       // Only search visible columns
                                                       if (cell.OwningColumn == null || !cell.OwningColumn.Visible)
                                                       {
                                                           return false;
                                                       }

                                                       string cellValue = UseFormattedValueForSearch ?
                                                                          cell.FormattedValue.ToStringNull() :
                                                                          cell.Value.ToStringNull();
                                                       return cellValue.Contains(textToSearch, StringComparison.OrdinalIgnoreCase);// .ToUpper().Contains(searchTextUpper);
                                                   });
            }

            filteredRows = _originalRows.Where(row =>
            {
                return rowFilterPredicate(row);
            }).ToList();

            if (filteredRows.Count <= 0)
            {
                return textToSearch.Substring(0, textToSearch.Length - 1);
            }
            _internalRows = filteredRows;
            Fill();
            return textToSearch;
        }

        /// <summary>
        /// Searches the grid's <see cref="System.Data.DataTable"/> data source by applying a `RowFilter`.
        /// The search can be performed across all visible columns or restricted to a specific column.
        /// </summary>
        /// <param name="textToSearch">The text to search for within the specified column(s).</param>
        /// <param name="dt">The <see cref="System.Data.DataTable"/> associated with the grid's data source.</param>
        /// <param name="searchColumnIndex">
        /// The zero-based index of the column to search within. If set to -1 (default),
        /// the search will be performed across all currently visible columns of the grid.
        /// If a specific column index is provided but is invalid or refers to an invisible column,
        /// the filter will be cleared.
        /// </param>
        /// <returns>
        /// The text that was successfully used for filtering (i.e., <paramref name="textToSearch"/>).
        /// Returns an empty string if:
        /// <list type="bullet">
        ///     <item><paramref name="textToSearch"/> is empty or contains only whitespace.</item>
        ///     <item>The specified <paramref name="searchColumnIndex"/> is out of bounds or refers to a non-visible column.</item>
        ///     <item>An error occurs during the filter application process.</item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// This method dynamically builds a `RowFilter` expression for either the <see cref="System.Data.DataTable.DefaultView"/>
        /// or the <see cref="System.Windows.Forms.BindingSource.Filter"/> property, depending on the grid's data source.
        /// <para/>
        /// **Filter Construction Logic:**
        /// <list type="bullet">
        ///     <item>If <paramref name="searchColumnIndex"/> is -1, it constructs "OR" conditions across all currently visible columns of the grid.</item>
        ///     <item>If <paramref name="searchColumnIndex"/> is a valid index, it constructs a filter for only that specific column.</item>
        /// </list>
        /// The filter attempts to match values based on the column's <see cref="System.Data.DataColumn.DataType"/>:
        /// <list type="bullet">
        ///     <item>String columns are filtered using `LIKE '%{textToSearch}%'`.</item>
        ///     <item>Numeric columns are filtered using `=` if <paramref name="textToSearch"/> can be parsed as a number.</item>
        ///     <item>DateTime columns are filtered using `=` if <paramref name="textToSearch"/> is a valid date, formatted as `#yyyy-MM-dd HH:mm:ss#`.</item>
        ///     <item>Boolean columns are filtered using `=` for "true" or "false" string matches.</item>
        ///     <item>Guid columns are filtered using `=` if <paramref name="textToSearch"/> is a valid GUID string.</item>
        ///     <item>TimeSpan columns are converted to string (`CONVERT({columnName}, 'System.String')`) and then filtered using `LIKE '%{textToSearch}%'`.</item>
        /// </list>
        /// Errors encountered during the filter application (e.g., malformed filter strings) are caught,
        /// and the filter is cleared to prevent the grid from entering a stuck state.
        /// </remarks>
        private string SearchInDataTable(string textToSearch, System.Data.DataTable dt, int searchColumnIndex = -1)
        {
            var grid = this;

            object? gridActualDataSource = grid.DataSource;
            // Try to get the BindingSource if the grid is bound to one.
            // We'll prioritize using the BindingSource.Filter if available.
            BindingSource? bindingSource = gridActualDataSource as BindingSource;
            System.Data.DataView? dv = null;
            try
            {
                dv = dt.DefaultView;
                if (string.IsNullOrWhiteSpace(textToSearch))
                {
                    if (bindingSource != null)
                        bindingSource.Filter = string.Empty;
                    else
                        dv.RowFilter = string.Empty;

                    // Ensure grid display is updated when filter is cleared
                    grid.ClearSelection();
                    grid.Refresh();
                    return string.Empty;
                }

                var filterBuilder = new StringBuilder();
                string escapedTextToSearch = textToSearch.Replace("'", "''"); // Escape single quotes for SQL-like strings

                // --- Logic to determine which columns to search based on searchColumnIndex ---
                IEnumerable<int> columnIndicesToProcess;

                if (searchColumnIndex != -1)
                {
                    // If a specific column is requested, only consider that one
                    // First, validate the index and visibility
                    if (searchColumnIndex < 0 || searchColumnIndex >= grid.ColumnCount || !grid.Columns[searchColumnIndex].Visible)
                    {
                        // If the specified column is invalid or not visible, clear filter and return empty search text
                        if (bindingSource != null)
                            bindingSource.Filter = string.Empty;
                        else
                            dv.RowFilter = string.Empty;
                        return string.Empty; // Invalid column, so effectively no search happened
                    }
                    columnIndicesToProcess = new int[] { searchColumnIndex };
                }
                else
                {
                    // Otherwise, iterate through all visible columns
                    // THIS IS THE MODIFIED LINE: Explicitly cast the lambda to Func<int, bool>
                    columnIndicesToProcess = Enumerable.Range(0, grid.ColumnCount)
                                                       .Where((Func<int, bool>)(idx => grid.Columns[idx].Visible));
                }
                // --- End of searchColumnIndex logic ---


                foreach (int i in columnIndicesToProcess)
                {
                    string columnName = grid.Columns[i].DataPropertyName;
                    if (string.IsNullOrEmpty(columnName))
                        columnName = grid.Columns[i].Name;

                    if (string.IsNullOrEmpty(columnName) || !dt.Columns.Contains(columnName))
                        continue; // Skip if column is not found in DataTable or is invalid

                    Type? columnType = dt.Columns[columnName]?.DataType;

                    if (columnType == null) continue;

                    // Build filter conditions based on column type
                    if (columnType == typeof(string))
                    {
                        AppendFilter(filterBuilder, $"{columnName} LIKE '%{escapedTextToSearch}%'");
                    }
                    else if (columnType.IsNumeric() && textToSearch.IsNumeric())
                    {
                        AppendFilter(filterBuilder, $"{columnName} = {escapedTextToSearch}");
                    }
                    else if (columnType == typeof(DateTime) && textToSearch.IsDate())
                    {
                        AppendFilter(filterBuilder, $"{columnName} = #{textToSearch.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss")}#");
                    }
                    else if (columnType == typeof(bool) && (textToSearch.Equals("true", StringComparison.OrdinalIgnoreCase) || textToSearch.Equals("false", StringComparison.OrdinalIgnoreCase)))
                    {
                        AppendFilter(filterBuilder, $"{columnName} = {textToSearch}");
                    }
                    else if (columnType == typeof(Guid) && Guid.TryParse(textToSearch, out _))
                    {
                        AppendFilter(filterBuilder, $"{columnName} = '{escapedTextToSearch}'");
                    }
                    else if (columnType == typeof(TimeSpan))
                    {
                        AppendFilter(filterBuilder, $"CONVERT({columnName}, 'System.String') LIKE '%{escapedTextToSearch}%'");
                    }
                }

                // Apply the constructed filter string
                if (bindingSource != null)
                {
                    if (filterBuilder.Length > 0)
                        bindingSource.Filter = filterBuilder.ToString();
                    else
                        bindingSource.Filter = string.Empty;
                }
                else
                {
                    if (filterBuilder.Length > 0)
                        dv.RowFilter = filterBuilder.ToString();
                    else
                        dv.RowFilter = string.Empty;
                }

                // Ensure grid display is updated after filter is applied
                grid.ClearSelection();
                grid.Refresh();

                return textToSearch;
            }
            catch (Exception)
            {
                // If an error occurs during filter application, clear the filter to avoid a stuck state
                if (bindingSource != null)
                    bindingSource.Filter = string.Empty;
                else
                    dv?.RowFilter = string.Empty;

                grid.ClearSelection();
                grid.Refresh(); // Refresh after clearing error filter
                return string.Empty;
            }
        }

        /// <summary>
        /// Appends a filter condition to a <see cref="StringBuilder"/> with a specified conjunction.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
        /// <param name="condition">The filter condition to add.</param>
        /// <param name="conjunction">The logical operator to use (e.g., "OR", "AND"). Defaults to "OR".</param>
        private void AppendFilter(StringBuilder builder, string condition, string conjunction = "OR")
        {
            if (builder.Length > 0)
                builder.Append($" {conjunction} ");
            builder.Append(condition);
        }

        /// <summary>
        /// Searches the currently displayed rows of the grid when no external data source
        /// (<see cref="DataGridView.DataSource"/> or `_originalRows`) is explicitly used for searching.
        /// </summary>
        /// <param name="textToSearch">The text to search for.</param>
        /// <param name="searchColumnIndex">
        /// The optional index of the column to search within. If -1, all visible cells in a row are searched.
        /// </param>
        /// <returns>
        /// An empty string if a match is found for the given `textToSearch`.
        /// If no match is found, it recursively calls itself with a truncated `textToSearch` (last character removed).
        /// </returns>
        /// <remarks>
        /// This method iterates through each <see cref="DataGridViewRow"/> and sets its <see cref="DataGridViewRow.Visible"/>
        /// property based on whether its cells contain the `textToSearch` (case-insensitive).
        /// If `textToSearch` is empty or whitespace, all rows are made visible.
        /// </remarks>
        private string SearchWithoutSource(string textToSearch, int searchColumnIndex = -1)
        {
            if (string.IsNullOrEmpty(textToSearch))
            {
                this.SuspendLayout(); // Suspend layout for the grid
                try
                {
                    foreach (DataGridViewRow row in this.Rows)
                    {
                        if (row.IsNewRow) continue;
                        row.Visible = true;
                    }
                }
                finally
                {
                    this.ResumeLayout(); // Resume layout for the grid
                }
                this.Refresh(); // Ensure UI updates
                return string.Empty;
            }

            bool foundMatch = false;

            this.SuspendLayout(); // Suspend layout for the grid
            try
            {

                foreach (DataGridViewRow row in this.Rows)
                {
                    if (row.IsNewRow) continue; // Always skip the new row placeholder
                    bool rowMatches = false;

                    if (searchColumnIndex >= 0)
                    {
                        string cellValue = row.Cells[searchColumnIndex].Value?.ToString() ?? string.Empty;
                        rowMatches = cellValue.Contains(textToSearch, StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            string cellValue = cell.Value?.ToString() ?? string.Empty;
                            if (cellValue.Contains(textToSearch, StringComparison.OrdinalIgnoreCase))
                            {
                                rowMatches = true;
                                break;
                            }
                        }
                    }
                    row.Visible = rowMatches;
                    foundMatch |= rowMatches;
                }
            }
            finally
            {
                this.ResumeLayout(); // Resume layout for the grid
            }
            this.Refresh(); // Ensure UI updates
            return textToSearch;
            //return foundMatch ? textToSearch : SearchWithoutSource(textToSearch.Substring(0, textToSearch.Length - 1), searchColumnIndex);
        }

        #region Paint Search Text

        /// <summary>
        /// Paints the search text within a DataGridView cell with a highlight color.
        /// It finds the occurrence of the searchText within the cell's formatted value
        /// and draws a colored rectangle behind it.
        /// </summary>
        /// <param name="e">The DataGridViewCellPaintingEventArgs.</param>
        /// <param name="searchText">The text to highlight within the cell.</param>
        public void PaintSearchText(DataGridViewCellPaintingEventArgs e, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText) || e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Graphics == null)
                return;

            // Prepare formatted text
            const string zeroWidth = "|";
            string formattedValue;
            if (UseFormattedValueForSearch)
                formattedValue = e.FormattedValue.ToStringNull()?.Replace(" ", zeroWidth) ?? string.Empty;
            else
                formattedValue = e.Value.ToStringNull()?.Replace(" ", zeroWidth) ?? string.Empty;

            if (this.Rows[e.RowIndex] is OutlookGridRow row && row.IsSummaryRow)
                formattedValue = e.FormattedValue.ToStringNull()?.Replace(" ", zeroWidth) ?? string.Empty;

            var formattedSearchText = searchText.Replace(" ", zeroWidth);

            // Find the search text within the cell value
            var searchIndex = formattedValue.IndexOf(formattedSearchText, StringComparison.InvariantCultureIgnoreCase);
            if (searchIndex < 0) return;

            //e.Handled = true; // Prevent default painting
            var g = e.Graphics;

            using var stringFormat = ToStringFormat(e.CellStyle!.Alignment);

            //var font = e.CellStyle.Font!;
            Font font = this.GridPalette?.GetContentShortTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal) ??
                                 new Font(this.DefaultCellStyle.Font!, FontStyle.Bold);

            // Measure text dimensions
            var zeroWidthSize = g.MeasureString(zeroWidth, font, e.CellBounds.Width, stringFormat).Width;
            var totalValueWidth = g.MeasureString(formattedValue, font, e.CellBounds.Width, stringFormat).Width;
            var highlightedTextStartWidth = g.MeasureString(formattedValue.Substring(0, searchIndex), font, e.CellBounds.Width, stringFormat).Width;
            var highlightedTextWidth = g.MeasureString(formattedValue.Substring(searchIndex, formattedSearchText.Length), font, e.CellBounds.Width, stringFormat).Width;

            // Calculate horizontal offset for alignment
            var xOffset = CalculateHorizontalOffset(e.CellStyle.Alignment, e.CellBounds.Width, totalValueWidth, zeroWidthSize, highlightedTextStartWidth);

            var pad = e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft ? e.CellStyle.Padding.Left : 0;
            // Draw highlight rectangle
            var highlightRectangle = new RectangleF(
                e.CellBounds.X + xOffset + pad,
                e.CellBounds.Y + 3,
                highlightedTextWidth,
                e.CellBounds.Height - 7
            );

            e.PaintBackground(e.CellBounds, true); // Paint background
            g.FillRectangle(Brushes.Orange, highlightRectangle); // Draw highlight
            e.PaintContent(e.CellBounds); // Paint content
        }

        /// <summary>
        /// Creates a StringFormat object based on the DataGridViewContentAlignment to properly align the painted text.
        /// </summary>
        /// <param name="alignment">The DataGridViewContentAlignment of the cell.</param>
        /// <returns>A StringFormat object configured with the specified alignment.</returns>
        private StringFormat ToStringFormat(DataGridViewContentAlignment alignment)
        {
            var stringFormat = StringFormat.GenericTypographic;

            switch (alignment)
            {
                case DataGridViewContentAlignment.MiddleCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case DataGridViewContentAlignment.BottomCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
                case DataGridViewContentAlignment.TopLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case DataGridViewContentAlignment.TopRight:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
            }

            stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            return stringFormat;
        }

        /// <summary>
        /// Calculates the horizontal offset needed to properly position the highlight rectangle based on the cell's alignment.
        /// </summary>
        /// <param name="alignment">The DataGridViewContentAlignment of the cell.</param>
        /// <param name="cellWidth">The width of the cell.</param>
        /// <param name="totalValueWidth">The total width of the cell's text.</param>
        /// <param name="zeroWidthSize">The width of the zero-width character used for spacing.</param>
        /// <param name="highlightedTextStartWidth">The width of the text before the highlighted part.</param>
        /// <returns>The horizontal offset for the highlight rectangle.</returns>
        private float CalculateHorizontalOffset(DataGridViewContentAlignment alignment, float cellWidth, float totalValueWidth, float zeroWidthSize, float highlightedTextStartWidth)
        {
            return alignment switch
            {
                DataGridViewContentAlignment.MiddleCenter or DataGridViewContentAlignment.BottomCenter or DataGridViewContentAlignment.TopCenter => (cellWidth - totalValueWidth) / 2 + highlightedTextStartWidth - zeroWidthSize / 2,
                DataGridViewContentAlignment.MiddleRight or DataGridViewContentAlignment.BottomRight or DataGridViewContentAlignment.TopRight => cellWidth - totalValueWidth + highlightedTextStartWidth - zeroWidthSize * 1.5f,
                _ => highlightedTextStartWidth + zeroWidthSize / 2,
            };
        }

        #endregion Paint Search Text

        #endregion Search

        #region Find ToolBar

        private KryptonOutlookGridSearchToolBar? _searchToolBar;

        /// <summary>
        /// Gets the list of <see cref="KryptonOutlookGridFilterField"/> objects representing the current filter configuration.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<KryptonOutlookGridFilterField>? ToolBarFilters { get; set; } = null;

        /// <summary>
        /// Gets or sets the OutlookGridGroupBox
        /// </summary>
        [Category("Behavior")]
        [Description("Associate the OutlookGridGroupBox with the grid.")]
        [DefaultValue(null)]
        public KryptonOutlookGridSearchToolBar? SearchToolBar
        {
            get => _searchToolBar;
            set
            {
                if (_searchToolBar != value)
                {
                    if (_searchToolBar != null && value == null)
                    {
                        this.ColumnAdded -= KryptonOutlookGrid_ColumnAddedOrRemoved;
                        this.ColumnRemoved -= KryptonOutlookGrid_ColumnAddedOrRemoved;
                        _searchToolBar.Search -= SearchToolBar_Search;
                        _searchToolBar.OnFilter -= SearchToolBar_OnFilter;
                    }
                    _searchToolBar = value;
                    if (_searchToolBar != null)
                    {
                        _searchToolBar.Search += SearchToolBar_Search;
                        _searchToolBar.OnFilter += SearchToolBar_OnFilter;
                        this.ColumnAdded += KryptonOutlookGrid_ColumnAddedOrRemoved;
                        this.ColumnRemoved += KryptonOutlookGrid_ColumnAddedOrRemoved;
                    }
                }
            }
        }

        private void SearchToolBar_OnFilter(object? sender, EventArgs e)
        {
            KryptonOutlookGridFilter? filterBuilder = null;
            try
            {
                var cols = this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => new KryptonOutlookGridFilterSourceColumn(c.Name, c.HeaderText, c.ValueType?.Name ?? string.Empty, c.DefaultCellStyle.Format)).ToList();
                if (cols == null || cols.Count == 0)
                {
                    KryptonMessageBox.Show("No visible columns found to filter.", "No Columns");
                    return;
                }
                filterBuilder = new(cols, ToolBarFilters)
                {
                    Text = $"Filter"
                };
                filterBuilder.FilterChanged += FilterBuilder_FilterChanged;
                var result = filterBuilder.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var filterData = filterBuilder.FilterData;
                    if (filterData == null || filterData.Count == 0)
                        ToolBarFilters = null;
                    else
                        ToolBarFilters = filterData;
                    ApplyFilter();
                }
            }
            finally
            {
                if (filterBuilder != null)
                    filterBuilder.FilterChanged -= FilterBuilder_FilterChanged;
            }
        }

        /// <summary>
        /// Handles the <see cref="System.Windows.Forms.DataGridView.ColumnStateChanged"/> event for the KryptonOutlookGrid.
        /// </summary>
        /// <param name="e">A <see cref="DataGridViewColumnStateChangedEventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// This method ensures that the associated search toolbar (`_searchToolBar`) is updated with the latest column information
        /// whenever a column's state (e.g., visibility, sort mode) changes. This keeps the search toolbar synchronized with the grid's columns.
        /// </remarks>
        protected override void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Visible)
            {
                _searchToolBar?.SetColumns(this.Columns);
                if (_summaryGrid?.Columns.Count > e.Column.Index)
                    _summaryGrid?.Columns[e.Column.Index]?.Visible = this.Columns[e.Column.Index].Visible;
                AdjustSummaryGridSize();
            }
            base.OnColumnStateChanged(e);
        }

        /// <summary>
        /// Handles the <see cref="System.Windows.Forms.DataGridView.ColumnAdded"/> or <see cref="System.Windows.Forms.DataGridView.ColumnRemoved"/>
        /// event for the KryptonOutlookGrid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="DataGridViewColumnEventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// This method ensures that the associated search toolbar (`_searchToolBar`) is updated with the latest column information
        /// whenever a column is added to or removed from the grid. This keeps the search toolbar synchronized with the grid's columns.
        /// </remarks>
        private void KryptonOutlookGrid_ColumnAddedOrRemoved(object? sender, DataGridViewColumnEventArgs e)
        {
            _searchToolBar?.SetColumns(this.Columns);
        }

        /// <summary>
        /// Handles the event to show the search toolbar.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// This method invokes the <c>Show</c> method on the internal <c>_searchToolBar</c> instance,
        /// making the search interface visible to the user.
        /// </remarks>
        private void OnShowSearchToolBar(object? sender, EventArgs e)
        {
            _searchToolBar?.Show();
        }

        /// <summary>
        /// Handles the event to hide the search toolbar.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// This method invokes the <c>Hide</c> method on the internal <c>_searchToolBar</c> instance,
        /// making the search interface invisible to the user.
        /// </remarks>
        private void OnHideSearchToolBar(object? sender, EventArgs e)
        {
            _searchToolBar?.Hide();
        }

        /// <summary>
        /// Handles the custom <c>Search</c> event from the <c>SearchToolBar</c> to perform a grid search.
        /// </summary>
        /// <param name="sender">The source of the event, typically the search toolbar.</param>
        /// <param name="e">A <see cref="KryptonOutlookGridSearchToolBarSearchEventArgs"/> that contains the search parameters.</param>
        /// <remarks>
        /// <para>
        /// This method orchestrates the finding of a cell matching the search criteria provided by the toolbar.
        /// It determines the starting point for the search within the grid based on whether the search should
        /// start from the beginning or continue from the current cell.
        /// </para>
        /// <para>
        /// It calls the <c>FindCell</c> method with parameters such as the value to search, the target column,
        /// and options for whole word or case-sensitive matching.
        /// </para>
        /// <para>
        /// If an initial search (from the current position) does not find a match and <c>restartSearch</c> is true,
        /// it performs a second search starting from the beginning of the grid (row 0, column 0) to wrap around.
        /// </para>
        /// <para>
        /// If a matching cell is found, the grid's <see cref="System.Windows.Forms.DataGridView.CurrentCell"/> is set to that cell.
        /// </para>
        /// </remarks>
        private void SearchToolBar_Search(object sender, KryptonOutlookGridSearchToolBarSearchEventArgs e)
        {
            if (CurrentCell == null) return; // If no current cell, do not proceed with search
            bool restartSearch = true; // This variable seems to be hardcoded true, consider its actual purpose or make it dynamic
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin) // If search is not from the beginning, calculate starting point
            {
                // Check if at the end of the current row/column
                bool endCol = CurrentCell!.ColumnIndex + 1 >= ColumnCount;
                bool endRow = CurrentCell!.RowIndex + 1 >= RowCount;

                if (endCol && endRow) // If at the very last cell
                {
                    startColumn = CurrentCell!.ColumnIndex; // Stay at current cell to potentially restart from here if no wrap
                    startRow = CurrentCell.RowIndex;
                }
                else // Move to the next cell in sequence
                {
                    startColumn = endCol ? 0 : CurrentCell!.ColumnIndex + 1; // If at end of column, go to first column of next row
                    startRow = CurrentCell!.RowIndex + (endCol ? 1 : 0);      // Increment row if moving to next row
                }
            }
            DataGridViewCell? c = FindCell(
                e.ValueToSearch,
                e.ColumnToSearch?.Name,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);

            // If no cell found from the current position, and restartSearch is enabled,
            // try searching from the beginning of the grid (wrap around).
            if (c == null && restartSearch)
            {
                c = FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch?.Name,
                    0, // Start from row 0
                    0, // Start from column 0
                    e.WholeWord,
                    e.CaseSensitive);
            }

            if (c != null)
            {
                CurrentCell = c; // Set the found cell as the current cell
            }
        }

        /// <summary>
        /// Finds the first cell in the KryptonOutlookGrid that contains or matches the specified value,
        /// starting from a given row and column index.
        /// </summary>
        /// <param name="valueToFind">The string value to search for within the cells.</param>
        /// <param name="columnName">
        /// The optional name of a specific column to search within. If <c>null</c> or empty,
        /// the search will span across all visible columns.
        /// </param>
        /// <param name="rowIndex">The starting row index for the search. The search will begin from this row (inclusive).</param>
        /// <param name="columnIndex">The starting column index for the search. The search will begin from this column (inclusive) within the <paramref name="rowIndex"/>.</param>
        /// <param name="isWholeWordSearch">
        /// A <see cref="bool"/> indicating whether the search should only match whole words (<c>true</c>)
        /// or if it should match any occurrence of <paramref name="valueToFind"/> as a substring (<c>false</c>).
        /// </param>
        /// <param name="isCaseSensitive">
        /// A <see cref="bool"/> indicating whether the search should be case-sensitive (<c>true</c>) or case-insensitive (<c>false</c>).
        /// </param>
        /// <returns>
        /// The first <see cref="DataGridViewCell"/> that matches the criteria, or <c>null</c> if no matching cell is found.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The method first validates input parameters and then proceeds with the search based on whether a specific
        /// column name is provided.
        /// </para>
        /// <para>
        /// If <paramref name="columnName"/> is specified:
        /// The search is confined to that particular column, starting from the given <paramref name="rowIndex"/>
        /// (adjusting the start row if <paramref name="columnIndex"/> is already past the target column in the initial row).
        /// </para>
        /// <para>
        /// If <paramref name="columnName"/> is not specified:
        /// The search iterates through all rows starting from <paramref name="rowIndex"/>, and within each row,
        /// it iterates through all visible columns starting from <paramref name="columnIndex"/> (which resets to 0 for subsequent rows).
        /// </para>
        /// <para>
        /// Cell values are converted to strings using <see cref="System.Windows.Forms.DataGridViewCell.FormattedValue"/>
        /// and then processed for case sensitivity and whole word matching as per the input flags.
        /// </para>
        /// </remarks>
        public DataGridViewCell? FindCell(string valueToFind, string? columnName, int rowIndex, int columnIndex, bool isWholeWordSearch, bool isCaseSensitive)
        {
            // Pre-condition check: Ensure valueToFind is not null and grid has rows/columns.
            // Also, if a column name is provided, ensure it exists and is visible.
            if (valueToFind != null && RowCount > 0 && ColumnCount > 0 && (columnName == null || (Columns.Contains(columnName) && Columns[columnName]!.Visible)))
            {
                rowIndex = Math.Max(0, rowIndex); // Ensure rowIndex is not negative

                if (!isCaseSensitive)
                {
                    valueToFind = valueToFind.ToLower(); // Convert search value to lower case for case-insensitive comparison
                }

                if (columnName != null) // Search within a specific column
                {
                    int c = Columns[columnName]!.Index; // Get the actual index of the named column

                    // If starting columnIndex is beyond the target column in the current row,
                    // start search from the next row at the target column.
                    if (columnIndex > c)
                    {
                        rowIndex++;
                    }

                    // Iterate through rows from the starting rowIndex
                    for (int r = rowIndex; r < RowCount; r++)
                    {
                        // Get the formatted value of the cell in the target column
                        string? value = Rows[r].Cells[c].FormattedValue!.ToString();
                        if (!isCaseSensitive)
                        {
                            value = value?.ToLower(); // Convert cell value to lower case for comparison
                        }

                        // Perform the comparison based on whole word or contains
                        if ((!isWholeWordSearch && value!.Contains(valueToFind)) || value!.Equals(valueToFind))
                        {
                            return Rows[r].Cells[c]; // Return the matching cell
                        }
                    }
                }
                else // Search across all visible columns
                {
                    columnIndex = Math.Max(0, columnIndex); // Ensure columnIndex is not negative

                    // Iterate through rows from the starting rowIndex
                    for (int r = rowIndex; r < RowCount; r++)
                    {
                        // Iterate through columns from the starting columnIndex for the current row
                        for (int c = columnIndex; c < ColumnCount; c++)
                        {
                            // Skip invisible cells/columns
                            if (!Rows[r].Cells[c].Visible)
                            {
                                continue;
                            }

                            // Get the formatted value of the cell
                            string? value = Rows[r].Cells[c].FormattedValue!.ToString();
                            if (!isCaseSensitive)
                            {
                                value = value?.ToLower(); // Convert cell value to lower case for comparison
                            }

                            // Perform the comparison based on whole word or contains
                            if ((!isWholeWordSearch && value!.Contains(valueToFind)) || value!.Equals(valueToFind))
                            {
                                return Rows[r].Cells[c]; // Return the matching cell
                            }
                        }
                        columnIndex = 0; // After the first row, reset columnIndex to 0 for subsequent rows to search from the beginning of each row
                    }
                }
            }

            return null; // No matching cell found
        }


        /// <summary>
        /// Clears the search toolbar filter and applies the updated filter.
        /// </summary>
        /// <remarks>
        /// If a filter is currently active in the search toolbar, this method will
        /// reset it by setting the <see cref="ToolBarFilters"/> property to <c>null</c>.
        /// After clearing the filter, <see cref="ApplyFilter"/> is called to refresh the
        /// displayed data based on the cleared filter criteria.
        /// </remarks>
        public void ClearSearchToolBarFilter()
        {
            if (ToolBarFilters != null)
            {
                ToolBarFilters = null;
                ApplyFilter();
            }
        }

        #endregion Find ToolBar

        #region Adjust Columns Width

        /// <summary>
        /// Handles the event that triggers resizing all columns to proportionally fill the available width of the DataGridView.
        /// </summary>
        /// <param name="sender">The source of the event, typically the control or object that raised it.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        /// <remarks>
        /// This method simply calls the <see cref="FitColumnsToWidth(bool, int)"/> method with <c>allowToDecrease</c> set to <c>true</c>,
        /// ensuring that columns are adjusted to fit the available space, even if it means shrinking them.
        /// </remarks>
        private void OnFitColumnsToWidth(object? sender, EventArgs e)
        {
            FitColumnsToWidth(true);
        }

        /// <summary>
        /// Adjusts the width of the visible columns in the DataGridView to proportionally fit the control's client width.
        /// </summary>
        /// <param name="allowToDecrease">
        /// If <c>true</c>, the adjustment is forced, meaning columns will be resized to fill the available space
        /// even if their current combined width is already greater than the available width (allowing them to decrease).
        /// If <c>false</c> (default), columns will only be adjusted to increase their width if they are
        /// currently narrower than the available space.
        /// </param>
        /// <param name="additionalPadding">
        /// Additional padding (in pixels) to reserve at the end of the columns, beyond the calculated scrollbar width.
        /// This ensures the last column does not touch the right edge of the grid.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the additional padding is negative.</exception>
        /// <remarks>
        /// <para>
        /// This method dynamically determines the width of the vertical scrollbar (if visible)
        /// and incorporates it into the available width calculation, along with any <paramref name="additionalPadding"/>.
        /// </para>
        /// <para>
        /// It calculates the total width of all visible columns and the available space within the grid
        /// (considering row headers, dynamic scrollbar width, and <paramref name="additionalPadding"/>).
        /// </para>
        /// <para>
        /// If the total visible width is less than the available space, columns are expanded proportionally.
        /// If <paramref name="allowToDecrease"/> is <c>true</c>, and the total visible width is greater than the available space,
        /// columns will be shrunk proportionally to fit.
        /// </para>
        /// <para>
        /// The method suppresses layout updates during the resizing process for better performance.
        /// </para>
        /// </remarks>
        public void FitColumnsToWidth(bool allowToDecrease = false, int additionalPadding = 0)
        {
            this.Refresh();
            if (additionalPadding < 0) throw new ArgumentOutOfRangeException(nameof(additionalPadding), "Additional padding must be non-negative.");

            // Dynamically determine the vertical scrollbar width
            int actualVScrollbarWidth = 0;//  10;
            actualVScrollbarWidth += this.VerticalScrollBar.Visible ? this.VerticalScrollBar.Width : 0;

            // Calculate total width of all visible columns
            float totalVisibleWidth = this.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .Sum(c => c.Width);

            // Include row header width if visible
            int rowHeaderWidth = this.RowHeadersVisible ? this.RowHeadersWidth : 0;

            // Determine the available width for columns, now including dynamic scrollbar width
            //int availableWidth = this.Width - (actualVScrollbarWidth + additionalPadding + rowHeaderWidth);
            int availableWidth = this.ClientRectangle.Width - (actualVScrollbarWidth + additionalPadding + rowHeaderWidth);

            // Determine whether adjustment is required
            if (totalVisibleWidth > 0 &&
               (totalVisibleWidth < availableWidth || allowToDecrease))
            {
                float adjustmentRatio = (float)availableWidth / totalVisibleWidth;
                this.SuspendLayout();
                _summaryGrid?.SuspendLayout();
                if (_summaryGrid != null)
                    this.ColumnWidthChanged -= KryptonOutlookGrid_ColumnWidthChanged;
                // Adjust each visible column's width proportionally
                foreach (var column in this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible))
                {
                    column.Width = Convert.ToInt32(Math.Floor(column.Width * adjustmentRatio));
                    if (_summaryGrid?.ColumnCount > column.Index)
                        _summaryGrid?.Columns[column.Index]?.Width = column.Width; // Adjust summary grid column width if it exists
                }
                if (_summaryGrid != null)
                    this.ColumnWidthChanged += KryptonOutlookGrid_ColumnWidthChanged;
                this.ResumeLayout();
                _summaryGrid?.ResumeLayout();
                AdjustSummaryGridSize();
            }
        }

        #endregion Adjust Columns Width

        /*/// <summary>
        /// Gets distinct values from the column as filter options.
        /// </summary>
        public void GetDistinctValuesForFilter(KryptonContextMenuItems parent)
        {
            var distinctValues = _originalRows.Select(row => row.Cells[_colSelected].Value).Distinct().ToList();
            // Sort the distinct values alphabetically for better UX
            var sortedValues = distinctValues.OrderBy(v => v).ToList();
        }*/

    }
}