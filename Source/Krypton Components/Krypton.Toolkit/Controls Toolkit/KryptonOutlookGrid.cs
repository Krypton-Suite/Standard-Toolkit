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

        private OutlookGridGroupCollection _groupCollection;     // List of Groups (of rows)
        private List<OutlookGridRow> _internalRows;              // List of Rows in order to keep them as is (without grouping,...)
        private readonly OutlookGridColumnCollection _internalColumns;    // List of columns in order to know if sorted, Grouped, types,...
        private int _previousGroupRowSelected = -1; //Useful to allow the selection of a group row or not when on mouse down 

        //Krypton ContextMenu for the columns header
        private KryptonContextMenu? _contextMenu;
        private KryptonContextMenuItems? _menuItems;
        private KryptonContextMenuItem _menuSortAscending;
        private KryptonContextMenuItem _menuSortDescending;
        private KryptonContextMenuItem _menuClearSorting;
        private KryptonContextMenuSeparator _menuSeparator1;
        private KryptonContextMenuItem _menuGroupByThisColumn;
        private KryptonContextMenuItem _menuUngroupByThisColumn;
        private KryptonContextMenuItem _menuShowGroupBox;
        private KryptonContextMenuItem _menuHideGroupBox;
        private KryptonContextMenuSeparator _menuSeparator2;
        private KryptonContextMenuItem _menuBestFitColumn;
        private KryptonContextMenuItem _menuBestFitAllColumns;
        private KryptonContextMenuSeparator _menuSeparator3;
        private KryptonContextMenuItem _menuVisibleColumns;
        private KryptonContextMenuItem _menuGroupInterval;
        private KryptonContextMenuItem _menuSortBySummary;
        private KryptonContextMenuItem _menuExpand;
        private KryptonContextMenuItem _menuCollapse;
        private KryptonContextMenuSeparator _menuSeparator4;
        private KryptonContextMenuSeparator _menuSeparator5;
        private KryptonContextMenuItem _menuConditionalFormatting;
        private int _colSelected = 1;         //for menu
        private const int FORMATTING_BAR_SOLID_GRADIENT_SEP_INDEX = 3;

        //For the Drag and drop of columns
        private Rectangle _dragDropRectangle;
        private int _dragDropSourceIndex;
        private int _dragDropTargetIndex;
        private int _dragDropCurrentIndex = -1;
        private int _dragDropType; //0=column, 1=row

        private bool _hideColumnOnGrouping;

        //Nodes
        private bool _showLines;
        internal bool InExpandCollapseMouseCapture;
        private GridFillMode _fillMode;

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
            _internalRows = new List<OutlookGridRow>();
            _internalColumns = new OutlookGridColumnCollection();
            _fillMode = GridFillMode.GroupsOnly;

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

            AllowUserToOrderColumns = false;  //we will handle it ourselves
            _hideColumnOnGrouping = false;
            _formatConditions = new List<ConditionalFormatting>();

            using (Graphics g = CreateGraphics())
            {
                _factorX = g.DpiX > 96 ? 1f * g.DpiX / 96 : 1f;
                _factorY = g.DpiY > 96 ? 1f * g.DpiY / 96 : 1f;
            }

            //Update StaticValues
            //ColumnHeadersHeight = (int)(ColumnHeadersHeight * factorY); //No need already done in KryptonDataGridView
            GlobalStaticValues.DefaultGroupRowHeight = (int)(GlobalStaticValues.DefaultGroupRowHeight * _factorY);
            GlobalStaticValues._2013GroupRowHeight = (int)(GlobalStaticValues._2013GroupRowHeight * _factorY);
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

        public RightToLeftLayout RightToLeftLayout
        {
            get => _rightToLeftLayout;

            set => _rightToLeftLayout = value;
        }

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
            if (row.IsGroupRow)
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
                    if (row.Group != null)
                    {
                        row.Group.Collapsed = !row.Group.Collapsed;
                    }

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
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left && Cursor.Current != Cursors.SizeWE)
                    {
                        if (_dragDropRectangle != Rectangle.Empty && !_dragDropRectangle.Contains(e.X, e.Y))
                        {
                            if (_dragDropType == 0)
                            {
                                OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_dragDropSourceIndex);
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
                                    $"{col.Name}|{col.DataGridViewColumn.HeaderText}|{col.DataGridViewColumn.HeaderCell.SortGlyphDirection}|{col.DataGridViewColumn.SortMode.ToString()}|{groupType}|{groupInterval}|{groupSortBySummaryCount}";
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
                            if (drgevent.Data.GetData(typeof(string)) is string r)
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
                                    List<DataGridViewColumn> listcol = new();
                                    foreach (DataGridViewColumn col in Columns)
                                    {
                                        listcol.Add(col);
                                    }

                                    foreach (DataGridViewColumn col in listcol.OrderBy(x => x.DisplayIndex))
                                    {
                                        Console.WriteLine($@"{col.Name} {col.DisplayIndex}");
                                    }

                                    Console.WriteLine(@"-----------------");

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
            if (_previousGroupRowSelected != -1 && _previousGroupRowSelected != e.RowIndex)
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
                    if (row.Group != null)
                    {
                        row.Group.Collapsed = !row.Group.Collapsed;
                    }

                    //this is a workaround to make the grid re-calculate it's contents and backgroun bounds
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
                }
            }
            else
            {
                base.OnCellMouseDown(e);
            }
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
                if (e.Button == MouseButtons.Right)
                {
                    ShowColumnHeaderContextMenu(e.ColumnIndex);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    OutlookGridColumn col = _internalColumns.FindFromColumnIndex(e.ColumnIndex);
                    if (col.DataGridViewColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
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

        //protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        //{
        //    var header = new FilterColumnHeaderCell();
        //   // header.FilterButtonClicked += new EventHandler<ColumnFilterClickedEventArg>(header_FilterButtonClicked);
        //    e.Column.HeaderCell = header;


        //    base.OnColumnAdded(e);
        //}



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

        #endregion

        #region OutlookGrid Events

        /// <summary>
        /// Called when [palette paint].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PaletteLayoutEventArgs"/> instance containing the event data.</param>
        private void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Called when [global palette changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnGlobalPaletteChanged(object sender, EventArgs e)
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
                h = GlobalStaticValues._2013GroupRowHeight; // special height for office 2013         
            }

            //For each outlookgridcolumn
            for (int j = 0; j < _internalColumns.Count; j++)
            {
                if (_internalColumns[j].GroupingType != null)
                {
                    var outlookGridGroup = _internalColumns[j].GroupingType;
                    if (outlookGridGroup != null)
                    {
                        outlookGridGroup.Height = h;
                    }
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
        private void OnColumnClearSorting(object sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
                UnSortColumn(col);
                Fill();
            }
        }

        /// <summary>
        /// Ascending sort for the column selected by the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnSortAscending(object sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
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
        private void OnColumnSortDescending(object sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
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
        private void OnGroupByThisColumn(object sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
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
        private void OnUnGroupByThisColumn(object sender, EventArgs e)
        {
            if (_colSelected > -1)
            {
                OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
                UnGroupColumn(col.Name);
                ForceRefreshGroupBox();
                Fill();
            }
        }

        private void OnGroupCollapse(object sender, EventArgs e)
        {
            OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
            Collapse(col.Name);
        }

        private void OnGroupExpand(object sender, EventArgs e)
        {
            OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
            Expand(col.Name);
        }

        private void OnSortBySummary(object sender, EventArgs e)
        {

            KryptonContextMenuItem item = (KryptonContextMenuItem)sender;
            OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
            if (col.GroupingType != null)
            {
                col.GroupingType.SortBySummaryCount = item.Checked;
            }
            ForceRefreshGroupBox();
            Fill();
        }

        private void OnGroupIntervalClick(object sender, EventArgs e)
        {
            KryptonContextMenuItem? item = (KryptonContextMenuItem)sender;
            OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
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

        private void OnConditionalFormattingClick(object sender, EventArgs e)
        {
            KryptonContextMenuImageSelect item = (KryptonContextMenuImageSelect)sender;
            OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
            ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
            ConditionalFormatting newformat = (item.Tag as List<ConditionalFormatting>)![item.SelectedIndex];
            if (format == null)
            {
                _formatConditions.Add(new ConditionalFormatting(col.DataGridViewColumn.Name, newformat.FormatType, newformat.FormatParams));
            }
            else
            {
                format.FormatType = newformat.FormatType;
                format.FormatParams = newformat.FormatParams;
            }
            ((KryptonContextMenuImageSelect)sender).SelectedIndex = -1; //I'm unable to get only one imageselect checked between solid and gradient, so reset the selected image
            Fill();
        }

        private void OnTwoColorsCustomClick(object sender, EventArgs e)
        {
            if (_rightToLeftLayout == RightToLeftLayout.LeftToRight)
            {
                VisualCustomFormatRuleForm fm = new VisualCustomFormatRuleForm(EnumConditionalFormatType.TwoColorsRange);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newformat = new(col.DataGridViewColumn.Name, EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(fm.MinimumColor, fm.MaximumColor));
                        _formatConditions.Add(newformat);
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
                VisualCustomFormatRuleRtlAwareForm fm = new VisualCustomFormatRuleRtlAwareForm(EnumConditionalFormatType.TwoColorsRange);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newformat = new(col.DataGridViewColumn.Name, EnumConditionalFormatType.TwoColorsRange, new TwoColorsParams(fm.MinimumColor, fm.MaximumColor));
                        _formatConditions.Add(newformat);
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


        private void OnThreeColorsCustomClick(object sender, EventArgs e)
        {
            if (_rightToLeftLayout == RightToLeftLayout.LeftToRight)
            {
                VisualCustomFormatRuleForm fm = new VisualCustomFormatRuleForm(EnumConditionalFormatType.ThreeColorsRange);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newformat = new(col.DataGridViewColumn.Name, EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(248, 105, 107), Color.FromArgb(255, 235, 132), Color.FromArgb(99, 190, 123)));
                        _formatConditions.Add(newformat);
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
                VisualCustomFormatRuleRtlAwareForm fm = new VisualCustomFormatRuleRtlAwareForm(EnumConditionalFormatType.ThreeColorsRange);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newformat = new(col.DataGridViewColumn.Name, EnumConditionalFormatType.ThreeColorsRange, new ThreeColorsParams(Color.FromArgb(248, 105, 107), Color.FromArgb(255, 235, 132), Color.FromArgb(99, 190, 123)));
                        _formatConditions.Add(newformat);
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

        private void OnBarCustomClick(object sender, EventArgs e)
        {
            if (_rightToLeftLayout == RightToLeftLayout.LeftToRight)
            {
                VisualCustomFormatRuleForm fm = new VisualCustomFormatRuleForm(EnumConditionalFormatType.Bar);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newformat = new(col.DataGridViewColumn.Name, EnumConditionalFormatType.Bar, new BarParams(fm.MinimumColor, fm.Gradient));
                        _formatConditions.Add(newformat);
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
                VisualCustomFormatRuleRtlAwareForm fm = new VisualCustomFormatRuleRtlAwareForm(EnumConditionalFormatType.Bar);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
                    ConditionalFormatting? format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);
                    if (format == null)
                    {
                        ConditionalFormatting newformat = new(col.DataGridViewColumn.Name, EnumConditionalFormatType.Bar, new BarParams(fm.MinimumColor, fm.Gradient));
                        _formatConditions.Add(newformat);
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

        private void OnClearConditionalClick(object sender, EventArgs e)
        {
            OutlookGridColumn col = _internalColumns.FindFromColumnIndex(_colSelected);
            _formatConditions.RemoveAll(x => x.ColumnName == col.Name);
            for (int i = 0; i < _internalRows.Count; i++)
            {
                FormattingCell fCell = (FormattingCell)_internalRows[i].Cells[_colSelected];
                //fCell.FormatType = formatConditions[i].FormatType;
                fCell.FormatParams = null;
            }
            Fill();
        }


        private void OnColumnVisibleCheckedChanged(object sender, EventArgs e)
        {
            KryptonContextMenuCheckBox item = (KryptonContextMenuCheckBox)sender;
            Columns[(int)item.Tag!].Visible = item.Checked;
        }

        /// <summary>
        /// Shows the groupbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowGroupBox(object sender, EventArgs e)
        {
            if (_groupBox != null)
            {
                _groupBox.Show();
            }
        }

        /// <summary>
        /// Hide the groupbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHideGroupBox(object sender, EventArgs e)
        {
            if (_groupBox != null)
            {
                _groupBox.Hide();
            }
        }

        /// <summary>
        /// Resizes the selected column by the menu to best fit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBestFitColumn(object sender, EventArgs e)
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
        private void OnBestFitAllColumns(object sender, EventArgs e)
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
        private void ColumnSortChangedEvent(object sender, OutlookGridColumnEventArgs e)
        {
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives ColumnSortChangedEvent : " + e.Column.Name + @" " + e.Column.SortDirection);
#endif
            _internalColumns[e.Column.Name].SortDirection = e.Column.SortDirection;
            _internalColumns[e.Column.Name].DataGridViewColumn.HeaderCell.SortGlyphDirection = e.Column.SortDirection;
            Fill();
        }

        /// <summary>
        /// Handles the ColumnGroupAddedEvent event. Fill the grid too.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A OutlookGridColumnEventArgs that contains the event data.</param>
        private void ColumnGroupAddedEvent(object sender, OutlookGridColumnEventArgs e)
        {
            GroupColumn(e.Column.Name, e.Column.SortDirection, null);
            //We fill again the grid with the new Grouping info
            Fill();
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives ColumnGroupAddedEvent : " + e.Column.Name);
#endif
        }

        /// <summary>
        /// Handles the ColumnGroupRemovedEvent event. Fill the grid too.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A OutlookGridColumnEventArgs that contains the event data.</param>
        private void ColumnGroupRemovedEvent(object sender, OutlookGridColumnEventArgs e)
        {
            UnGroupColumn(e.Column.Name);
            //We fill again the grid with the new Grouping info
            Fill();
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives ColumnGroupRemovedEvent : " + e.Column.Name);
#endif
        }

        /// <summary>
        /// Handles the ClearGroupingEvent event. Fill the grid too.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        private void ClearGroupingEvent(object sender, EventArgs e)
        {
            ClearGroups();
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives ClearGroupingEvent");
#endif
        }

        /// <summary>
        /// Handles the FullCollapseEvent event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        private void FullCollapseEvent(object sender, EventArgs e)
        {
            CollapseAll();
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives FullCollapseEvent");
#endif
        }

        /// <summary>
        /// Handles the FullExpandEvent event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        private void FullExpandEvent(object sender, EventArgs e)
        {
            ExpandAll();
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives FullExpandEvent");
#endif
        }

        /// <summary>
        /// Handles the GroupExpandEvent event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridGroupExpandEvent(object sender, OutlookGridColumnEventArgs e)
        {
            Expand(e.Column.Name);
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives GridGroupExpandEvent");
#endif
        }

        private void GridGroupCollapseEvent(object sender, OutlookGridColumnEventArgs e)
        {
            Collapse(e.Column.Name);
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives GridGroupCollapseEvent");
#endif
        }

        private void ColumnGroupIndexChangedEvent(object sender, OutlookGridColumnEventArgs e)
        {
            //TODO 25/01/2014
            _internalColumns.ChangeGroupIndex(e.Column);
            Fill(); //to reflect the changes
            ForceRefreshGroupBox();
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives ColumnGroupIndexChangedEvent");
#endif
        }

        private void GroupIntervalClickEvent(object sender, OutlookGridColumnEventArgs e)
        {
            OutlookGridColumn col = _internalColumns.FindFromColumnName(e.Column.Name);
            (col.GroupingType as OutlookGridDateTimeGroup)!.Interval =
                (e.Column.GroupingType as OutlookGridDateTimeGroup)!.Interval;
            Fill();
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives GroupIntervalClickEvent");
#endif
        }

        private void SortBySummaryCountEvent(object sender, OutlookGridColumnEventArgs e)
        {
            OutlookGridColumn col = _internalColumns.FindFromColumnName(e.Column.Name);
            if (col.GroupingType != null)
            {
                if (e.Column.GroupingType != null)
                {
                    col.GroupingType.SortBySummaryCount = e.Column.GroupingType.SortBySummaryCount;
                }
            }
            Fill();
#if DEBUG
            Console.WriteLine(@"OutlookGrid - Receives SortBySummaryCountEvent");
#endif
        }

        /// <summary>
        /// Raises the GroupImageClick event.
        /// </summary>
        /// <param name="e">A OutlookGridGroupImageEventArgs that contains the event data.</param>
        protected virtual void OnGroupImageClick(OutlookGridGroupImageEventArgs e)
        {
            if (GroupImageClick != null)
            {
                GroupImageClick(this, e);
            }
        }

        /// <summary>
        /// Raises the NodeExpanding event.
        /// </summary>
        /// <param name="e">A ExpandingEventArgs that contains the event data.</param>
        protected virtual void OnNodeExpanding(ExpandingEventArgs e)
        {
            if (NodeExpanding != null)
            {
                NodeExpanding(this, e);
            }
        }

        /// <summary>
        /// Raises the NodeExpanded event.
        /// </summary>
        /// <param name="e">A ExpandedEventArgs that contains the event data.</param>
        protected virtual void OnNodeExpanded(ExpandedEventArgs e)
        {
            if (NodeExpanded != null)
            {
                NodeExpanded(this, e);
            }
        }

        /// <summary>
        /// Raises the NodeCollapsing event.
        /// </summary>
        /// <param name="e">A CollapsingEventArgs that contains the event data.</param>
        protected virtual void OnNodeCollapsing(CollapsingEventArgs e)
        {
            if (NodeCollapsing != null)
            {
                NodeCollapsing(this, e);
            }

        }

        /// <summary>
        /// Raises the NodeCollapsed event.
        /// </summary>
        /// <param name="e">A CollapsedEventArgs that contains the event data.</param>
        protected virtual void OnNodeCollapsed(CollapsedEventArgs e)
        {
            if (NodeCollapsed != null)
            {
                NodeCollapsed(this, e);
            }
        }

        #endregion

        #region OutlookGrid methods

        /// <summary>
        /// Add a column for internal uses of the OutlookGrid. The column must already exists in the datagridview. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        /// <param name="comparer">The comparer if needed</param>
        public void AddInternalColumn(DataGridViewColumn? col, IOutlookGridGroup group, SortOrder sortDirection, int groupIndex, int sortIndex, IComparer? comparer)
        {
            AddInternalColumn(new OutlookGridColumn(col, group, sortDirection, groupIndex, sortIndex, comparer));
            //internalColumns.Add(new OutlookGridColumn(col, group, sortDirection, groupIndex, sortIndex));
            ////Already reflect the SortOrder on the column
            //col.HeaderCell.SortGlyphDirection = sortDirection;
            //if (this._hideColumnOnGrouping && groupIndex > -1 && group.AllowHiddenWhenGrouped)
            //    col.Visible = false;
        }

        /// <summary>
        /// Add a column for internal uses of the OutlookGrid. The column must already exists in the datagridview. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        public void AddInternalColumn(DataGridViewColumn? col, IOutlookGridGroup group, SortOrder sortDirection,
            int groupIndex, int sortIndex) =>
            AddInternalColumn(new OutlookGridColumn(col, group, sortDirection, groupIndex, sortIndex, null));

        /// <summary>
        /// Add a column for internal uses of the OutlookGrid. The column must already exists in the datagridview. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
        /// </summary>
        /// <param name="col">The configured OutlookGridColumn.</param>
        public void AddInternalColumn(OutlookGridColumn col)
        {
            Debug.Assert(col != null);
            if (col != null)
            {
                _internalColumns.Add(col);
                //Already reflect the SortOrder on the column
                col.DataGridViewColumn.HeaderCell.SortGlyphDirection = col.SortDirection;
                if (col.GroupingType != null && _hideColumnOnGrouping && col.GroupIndex > -1 && col.GroupingType.AllowHiddenWhenGrouped)
                {
                    col.DataGridViewColumn.Visible = false;
                }
            }
        }

        /// <summary>
        /// Add an array of OutlookGridColumns for internal use of OutlookGrid. The columns must already exist in the datagridview. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
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
        /// Add an array of OutlookGridColumns for internal use of OutlookGrid. The columns must already exist in the datagridview. Do this *BEFORE* using the grid (sorting and grouping, filling,...)
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
        public void GroupColumn(string? columnName, SortOrder sortDirection, IOutlookGridGroup? gr) => GroupColumn(_internalColumns[columnName], sortDirection, gr);

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
                col.DataGridViewColumn.HeaderCell.SortGlyphDirection = sortDirection;
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
        public void UnGroupColumn(string? columnName)
        {
            UnGroupColumn(_internalColumns[columnName]);
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
                col.DataGridViewColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
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
            col.DataGridViewColumn.HeaderCell.SortGlyphDirection = sort;
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
                col.DataGridViewColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
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
                    _internalColumns[i].DataGridViewColumn.Visible = true;
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
        /// Gets all the subrows of a grouprow (recursive)
        /// </summary>
        /// <param name="list">The result list of OutlookGridRows</param>
        /// <param name="groupRow">The IOutlookGridGroup that contains rows to inspect.</param>
        /// <returns>A list of OutlookGridRows</returns>
        public List<OutlookGridRow> GetSubRows(ref List<OutlookGridRow> list, IOutlookGridGroup? groupRow)
        {
            list.AddRange(groupRow.Rows);
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
        /// Register for events concerning the groupbox
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
            if (_groupBox != null)
            {
                _groupBox.UpdateGroupingColumns(_internalColumns.FindGroupedColumns());
            }
        }

        /// <summary>
        /// Show the context menu header
        /// </summary>
        /// <param name="columnIndex">The column used by the context menu.</param>
        private void ShowColumnHeaderContextMenu(int columnIndex)
        {
            OutlookGridColumn? col = _internalColumns.FindFromColumnIndex(columnIndex);
            // Create menu items the first time they are needed
            if (_menuItems == null)
            {
                // Create individual items
                /*_menuSortAscending = new KryptonContextMenuItem(LanguageManager.Instance.GetString("SORTASCENDING"), Resources.OutlookGridImageResources.sort_az_ascending2, OnColumnSortAscending);
                _menuSortDescending = new KryptonContextMenuItem(LanguageManager.Instance.GetString("SORTDESCENDING"), Resources.OutlookGridImageResources.sort_az_descending2, new EventHandler(OnColumnSortDescending));
                _menuClearSorting = new KryptonContextMenuItem(LanguageManager.Instance.GetString("CLEARSORTING"), Resources.OutlookGridImageResources.sort_up_down_delete_16, new EventHandler(OnColumnClearSorting));
                _menuSeparator1 = new KryptonContextMenuSeparator();
                _menuExpand = new KryptonContextMenuItem(LanguageManager.Instance.GetString("EXPAND"), Resources.OutlookGridImageResources.element_plus_16, new EventHandler(OnGroupExpand));
                _menuCollapse = new KryptonContextMenuItem(LanguageManager.Instance.GetString("COLLAPSE"), Resources.OutlookGridImageResources.element_minus_16, new EventHandler(OnGroupCollapse));
                _menuSeparator4 = new KryptonContextMenuSeparator();
                _menuGroupByThisColumn = new KryptonContextMenuItem(LanguageManager.Instance.GetString("GROUP"), Resources.OutlookGridImageResources.element, new EventHandler(OnGroupByThisColumn));
                _menuUngroupByThisColumn = new KryptonContextMenuItem(LanguageManager.Instance.GetString("UNGROUP"), Resources.OutlookGridImageResources.element_delete, new EventHandler(OnUnGroupByThisColumn));
                _menuShowGroupBox = new KryptonContextMenuItem(LanguageManager.Instance.GetString("SHOWGROUPBOX"), null, new EventHandler(OnShowGroupBox));
                _menuHideGroupBox = new KryptonContextMenuItem(LanguageManager.Instance.GetString("HIDEGROUPBOX"), null, new EventHandler(OnHideGroupBox));
                _menuSeparator2 = new KryptonContextMenuSeparator();
                _menuBestFitColumn = new KryptonContextMenuItem(LanguageManager.Instance.GetString("BESTFIT"), null, new EventHandler(OnBestFitColumn));
                _menuBestFitAllColumns = new KryptonContextMenuItem(LanguageManager.Instance.GetString("BESTFITALL"), Resources.OutlookGridImageResources.fit_to_size, new EventHandler(OnBestFitAllColumns));
                _menuSeparator3 = new KryptonContextMenuSeparator();
                _menuVisibleColumns = new KryptonContextMenuItem(LanguageManager.Instance.GetString("COLUMNS"), Resources.OutlookGridImageResources.table2_selection_column, null);
                _menuGroupInterval = new KryptonContextMenuItem(LanguageManager.Instance.GetString("GROUPINTERVAL"));
                _menuSortBySummary = new KryptonContextMenuItem(LanguageManager.Instance.GetString("SORTBYSUMMARYCOUNT"), null, new EventHandler(OnSortBySummary));
                _menuSortBySummary.CheckOnClick = true;
                _menuSeparator5 = new KryptonContextMenuSeparator();
                _menuConditionalFormatting = new KryptonContextMenuItem(LanguageManager.Instance.GetString("CONDITIONALFORMATTING"), Resources.OutlookGridImageResources.table_conditional_16, null);*/

                #region Localisation

                _menuSortAscending = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.SortAscending, SortingImageResources.sort_az_ascending2, OnColumnSortAscending);
                _menuSortDescending = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.SortDescending, SortingImageResources.sort_az_descending2, OnColumnSortDescending);
                _menuClearSorting = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.ClearSorting, SortingImageResources.sort_up_down_delete_16, OnColumnClearSorting);
                _menuSeparator1 = new KryptonContextMenuSeparator();
                _menuExpand = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.Expand, ElementsImageResources.element_plus_16, OnGroupExpand);
                _menuCollapse = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.Collapse, ElementsImageResources.element_minus_16, OnGroupCollapse);
                _menuSeparator4 = new KryptonContextMenuSeparator();
                _menuGroupByThisColumn = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.Group, ElementsImageResources.element, OnGroupByThisColumn);
                _menuUngroupByThisColumn = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.Ungroup, ElementsImageResources.element_delete, OnUnGroupByThisColumn);
                _menuShowGroupBox = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.ShowGroupBox, null, OnShowGroupBox);
                _menuHideGroupBox = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.HideGroupBox, null, OnHideGroupBox);
                _menuSeparator2 = new KryptonContextMenuSeparator();
                _menuBestFitColumn = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.BestFit, null, OnBestFitColumn);
                _menuBestFitAllColumns = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.BestFitAll, GenericImageResources.fit_to_size, OnBestFitAllColumns);
                _menuSeparator3 = new KryptonContextMenuSeparator();
                _menuVisibleColumns = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.Columns, GenericImageResources.table2_selection_column, null);
                _menuGroupInterval = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.GroupInterval);
                _menuSortBySummary = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.SortBySummaryCount, null, OnSortBySummary);
                _menuSortBySummary.CheckOnClick = true;
                _menuSeparator5 = new KryptonContextMenuSeparator();
                _menuConditionalFormatting = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.ConditionalFormatting, GenericImageResources.table_conditional_16, null);

                #endregion

                //Group Interval
                KryptonContextMenuItems groupIntervalItems;
                KryptonContextMenuItem? it;
                string[] names = Enum.GetNames(typeof(DateInterval));
                KryptonContextMenuItemBase[] arrayOptions = new KryptonContextMenuItemBase[names.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    it = new KryptonContextMenuItem(OutlookGridLanguageManager.Instance.GetString(names[i]));
                    it.Tag = names[i];
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
                    itCheckbox = new KryptonContextMenuCheckBox(Columns[i].HeaderText);
                    itCheckbox.Checked = Columns[i].Visible;
                    itCheckbox.Tag = Columns[i].Index;
                    itCheckbox.CheckedChanged += OnColumnVisibleCheckedChanged;
                    arrayCols[i] = itCheckbox;
                }
                _menuVisibleColumns.Items.AddRange(arrayCols!);

                //Conditional formatting
                ImageList imgListFormatting = new();
                imgListFormatting.ColorDepth = ColorDepth.Depth32Bit;
                imgListFormatting.ImageSize = new Size(32, 32);
                List<ConditionalFormatting> tmpTag = new();
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
                imgListFormatting.Images.Add(DataBarImageResources.Databar_gradient_yellow_32);
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
                arrayOptions = new KryptonContextMenuItemBase[names.Length + 2];
                for (int i = 0; i < names.Length; i++)
                {
                    it = new KryptonContextMenuItem(OutlookGridLanguageManager.Instance.GetString(names[i]));
                    it.Tag = names[i];

                    if (names[i] == EnumConditionalFormatType.Bar.ToString())
                    {
                        it.Image = DataBarImageResources.databar_generic_16;

                        //Solid
                        KryptonContextMenuHeading kFormattingBarHeadingSolid = new();
                        kFormattingBarHeadingSolid.Text = KryptonManager.Strings.KryptonOutlookGridStrings.SolidFill;
                        KryptonContextMenuImageSelect kFormattingBarImgSelectSolid = new();
                        kFormattingBarImgSelectSolid.ImageList = imgListFormatting;
                        kFormattingBarImgSelectSolid.ImageIndexStart = 0;
                        kFormattingBarImgSelectSolid.ImageIndexEnd = 5;
                        kFormattingBarImgSelectSolid.LineItems = 4;
                        kFormattingBarImgSelectSolid.Tag = tmpTag;
                        kFormattingBarImgSelectSolid.Click += OnConditionalFormattingClick;

                        //Gradient
                        KryptonContextMenuHeading kFormattingBarHeadingGradient = new();
                        kFormattingBarHeadingGradient.Text = KryptonManager.Strings.KryptonOutlookGridStrings.GradientFill;
                        KryptonContextMenuImageSelect kFormattingBarImgSelectGradient = new();
                        kFormattingBarImgSelectGradient.ImageList = imgListFormatting;
                        kFormattingBarImgSelectGradient.ImageIndexStart = 6;
                        kFormattingBarImgSelectGradient.ImageIndexEnd = 11;
                        kFormattingBarImgSelectGradient.LineItems = 4;
                        kFormattingBarImgSelectGradient.Tag = tmpTag;
                        kFormattingBarImgSelectGradient.Click += OnConditionalFormattingClick;

                        //Custom
                        KryptonContextMenuHeading kFormattingBarHeadingOther = new();
                        kFormattingBarHeadingOther.Text = KryptonManager.Strings.KryptonOutlookGridStrings.Other;
                        KryptonContextMenuItem? it2;
                        it2 = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.CustomThreeDots);
                        it2.Tag = "";
                        it2.Image = GenericImageResources.paint_bucket_green;
                        it2.Click += OnBarCustomClick;

                        KryptonContextMenuItems bars = new(new KryptonContextMenuItemBase[] { it2 });

                        //Menu construction
                        it.Items.AddRange(new KryptonContextMenuItemBase[] {
                        kFormattingBarHeadingSolid,
                        kFormattingBarImgSelectSolid,
                        kFormattingBarHeadingGradient,
                        kFormattingBarImgSelectGradient,
                        kFormattingBarHeadingOther,
                        bars
                        });
                    }
                    else if (names[i] == EnumConditionalFormatType.TwoColorsRange.ToString())
                    {
                        it.Image = OutlookGridImageResources.color2scale_generic_16;

                        KryptonContextMenuItems twoColors;

                        KryptonContextMenuImageSelect kTwoColorsImgSelect = new();
                        kTwoColorsImgSelect.ImageList = imgListFormatting;
                        kTwoColorsImgSelect.ImageIndexStart = 12;
                        kTwoColorsImgSelect.ImageIndexEnd = 23;
                        kTwoColorsImgSelect.LineItems = 4;
                        kTwoColorsImgSelect.Tag = tmpTag;
                        kTwoColorsImgSelect.Click += OnConditionalFormattingClick;
                        it.Items.Add(kTwoColorsImgSelect);

                        KryptonContextMenuSeparator sep1 = new();
                        sep1.Tag = "";

                        KryptonContextMenuItem? it2;
                        it2 = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.CustomThreeDots);
                        it2.Tag = "";
                        it2.Image = GenericImageResources.paint_bucket_green;
                        it2.Click += OnTwoColorsCustomClick;

                        twoColors = new KryptonContextMenuItems(new KryptonContextMenuItemBase[] { sep1, it2 });
                        it.Items.Add(twoColors);
                    }
                    else if (names[i] == EnumConditionalFormatType.ThreeColorsRange.ToString())
                    {
                        it.Image = OutlookGridImageResources.color3scale_generic_16;

                        KryptonContextMenuItems threeColors;

                        KryptonContextMenuImageSelect kThreeColorsImgSelect = new();
                        kThreeColorsImgSelect.ImageList = imgListFormatting;
                        kThreeColorsImgSelect.ImageIndexStart = 24;
                        kThreeColorsImgSelect.ImageIndexEnd = 29;
                        kThreeColorsImgSelect.LineItems = 4;
                        kThreeColorsImgSelect.Tag = tmpTag;
                        kThreeColorsImgSelect.Click += OnConditionalFormattingClick;
                        it.Items.Add(kThreeColorsImgSelect);

                        KryptonContextMenuSeparator sep1 = new();
                        sep1.Tag = "";

                        KryptonContextMenuItem? it2;
                        it2 = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.CustomThreeDots);
                        it2.Tag = "";
                        it2.Image = GenericImageResources.paint_bucket_green;
                        it2.Click += OnThreeColorsCustomClick;

                        threeColors = new KryptonContextMenuItems(new KryptonContextMenuItemBase[] { sep1, it2 });
                        it.Items.Add(threeColors);
                    }

                    arrayOptions[i] = it;
                    KryptonContextMenuSeparator sep2 = new();
                    sep2.Tag = "";
                    arrayOptions[i + 1] = sep2;
                    KryptonContextMenuItem? it3;
                    it3 = new KryptonContextMenuItem(KryptonManager.Strings.KryptonOutlookGridStrings.ClearRules);
                    it3.Image = GenericImageResources.eraser;
                    it3.Tag = "";
                    it3.Click += OnClearConditionalClick;
                    arrayOptions[i + 2] = it3;
                }
                KryptonContextMenuItems conditionalFormattingItems = new(arrayOptions);
                _menuConditionalFormatting.Items.Add(conditionalFormattingItems);

                //Add items inside an items collection (apart from separator1 which is only added if required)
                _menuItems = new KryptonContextMenuItems(new KryptonContextMenuItemBase[] { _menuSortAscending,
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
                                                                                            _menuSeparator3,
                                                                                            _menuVisibleColumns,
                                                                                            _menuSeparator5,
                                                                                            _menuConditionalFormatting});
            }

            // Ensure we have a krypton context menu if not already present
            _contextMenu ??= new KryptonContextMenu();

            // Update the individual menu options
            if (col != null)
            {
                _menuSortAscending.Visible = col.DataGridViewColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuSortAscending.Checked = col.SortDirection == SortOrder.Ascending ? true : false;
                _menuSortDescending.Checked = col.SortDirection == SortOrder.Descending ? true : false;
                _menuSortDescending.Visible = col.DataGridViewColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuSortBySummary.Visible = col.IsGrouped && col.GroupingType != null;
                if (_menuSortBySummary.Visible)
                {
                    _menuSortBySummary.Checked = col.GroupingType!.SortBySummaryCount;
                }

                _menuClearSorting.Enabled = col.SortDirection != SortOrder.None && !col.IsGrouped;
                _menuClearSorting.Visible = col.DataGridViewColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuSeparator1.Visible = _menuSortAscending.Visible || _menuSortDescending.Visible || _menuClearSorting.Visible;
                _menuExpand.Visible = col.IsGrouped;
                _menuCollapse.Visible = col.IsGrouped;
                _menuSeparator4.Visible = _menuExpand.Visible || _menuCollapse.Visible;
                _menuGroupByThisColumn.Visible = !col.IsGrouped && col.DataGridViewColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuGroupInterval.Visible = col.IsGrouped && col.DataGridViewColumn.SortMode != DataGridViewColumnSortMode.NotSortable && col.GroupingType?.GetType() == typeof(OutlookGridDateTimeGroup);
                if (_menuGroupInterval.Visible)
                {
                    string? currentInterval = Enum.GetName(typeof(DateInterval), ((col.GroupingType as OutlookGridDateTimeGroup)!).Interval);
                    foreach (KryptonContextMenuItem item in ((KryptonContextMenuItems)_menuGroupInterval.Items[0]).Items)
                    {
                        item.Checked = item.Tag.ToString() == currentInterval;
                    }
                }
                _menuUngroupByThisColumn.Visible = col.IsGrouped && col.DataGridViewColumn.SortMode != DataGridViewColumnSortMode.NotSortable;
                _menuShowGroupBox.Visible = _groupBox != null && !_groupBox.Visible;
                _menuHideGroupBox.Visible = _groupBox != null && _groupBox.Visible;
                _menuSeparator2.Visible = _menuGroupByThisColumn.Visible || _menuUngroupByThisColumn.Visible || _menuShowGroupBox.Visible || _menuHideGroupBox.Visible;
                _menuBestFitColumn.Visible = true;
                if (col.DataGridViewColumn.GetType() == typeof(KryptonDataGridViewFormattingColumn))
                {
                    _menuSeparator5.Visible = true;
                    _menuConditionalFormatting.Visible = true;

                    //Get the format condition
                    ConditionalFormatting format = _formatConditions.FirstOrDefault(x => x.ColumnName == col.Name);

                    for (int i = 0; i < _menuConditionalFormatting.Items[0].ItemChildCount; i++)
                    {
                        if (format != null && ((KryptonContextMenuItems)_menuConditionalFormatting.Items[0]).Items[i].Tag.ToString().Equals(format.FormatType.ToString()))
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
                    _menuSeparator5.Visible = false;
                    _menuConditionalFormatting.Visible = false;
                }
            }
            else
            {
                _menuSortAscending.Visible = false;
                _menuSortDescending.Visible = false;
                _menuSortBySummary.Visible = false;
                _menuClearSorting.Visible = false;
                _menuSeparator1.Visible = _menuSortAscending.Visible || _menuSortDescending.Visible || _menuClearSorting.Visible;
                _menuExpand.Visible = false;
                _menuCollapse.Visible = false;
                _menuSeparator4.Visible = _menuExpand.Visible || _menuCollapse.Visible;
                _menuGroupByThisColumn.Visible = false;
                _menuGroupInterval.Visible = false;
                _menuUngroupByThisColumn.Visible = false;
                _menuShowGroupBox.Visible = _groupBox != null && !_groupBox.Visible;
                _menuHideGroupBox.Visible = _groupBox != null && _groupBox.Visible;
                _menuSeparator2.Visible = _menuGroupByThisColumn.Visible || _menuUngroupByThisColumn.Visible || _menuShowGroupBox.Visible || _menuHideGroupBox.Visible;
                _menuBestFitColumn.Visible = false;
                _menuSeparator5.Visible = false;
                _menuConditionalFormatting.Visible = false;

            }

            if (!_contextMenu.Items.Contains(_menuItems))
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
                    col.DataGridViewColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
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
        }

        /// <summary>
        /// Assign the rows to the internal list.
        /// </summary>
        /// <param name="l">List of OutlookGridRows</param>
        public void AssignRows(List<OutlookGridRow> l)
        {
            _internalRows = l;
        }

        /// <summary>
        /// Assign the rows to the internal list.
        /// </summary>
        /// <param name="col">DataGridViewRowCollection</param>
        public void AssignRows(DataGridViewRowCollection col)
        {
            //dataSource.Rows = col.Cast<OutlookGridRow>().ToList();
            _internalRows = col.Cast<OutlookGridRow>().ToList();
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
        /// <param name="rowindex">The index of the group row.</param>
        /// <param name="collapsed">True if collapsed, false if expanded.</param>
        private void SetGroupCollapse(int rowindex, bool collapsed)
        {
            if (!IsGridGrouped || _internalRows.Count == 0 || rowindex < 0)
            {
                return;
            }

            OutlookGridRow row = (OutlookGridRow)Rows[rowindex];
            if (row.IsGroupRow)
            {
                if (row.Group != null)
                {
                    row.Group.Collapsed = collapsed;
                }

                //this is a workaround to make the grid re-calculate it's contents and backgroun bounds
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

        //private void RecursiveUpwardSetNodeCollaspse(OutlookGridRow r, bool collasped)
        //{
        //    //No events - for speed
        //    if (r.ParentNode != null)
        //    {
        //        if (r.ParentNode.Collapsed)
        //        {
        //            r.ParentNode.Collapsed = collasped;
        //            RecursiveUpwardSetNodeCollaspse(r.ParentNode, collapsed);
        //        }
        //    }
        //    //sw.Stop();
        //    //Console.WriteLine(sw.ElapsedMilliseconds.ToString() + " ms" + r.ToString());

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
        /// Ensure the node is visible (all parents exanded)
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

                    CollapsedEventArgs exped = new(node);
                    OnNodeCollapsed(exped);
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

                    ExpandedEventArgs exped = new(node);
                    OnNodeExpanded(exped);
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
        /// Expand Node and all its subnodes (without events)
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
        /// Collapse Node and all its subnodes (without events)
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

                //Recusive call
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
                                    (fCell.FormatParams as TwoColorsParams)!.ValueColor = ColorFormatting.ConvertTwoRange(((TimeSpan)list[i].Cells[formatColumn].Value!).TotalMinutes, _formatConditions[j].MinValue, _formatConditions[j].MaxValue, _formatConditions[j].FormatParams as TwoColorsParams);
                                }
                                else if (typeColumn == typeof(decimal))
                                {
                                    (fCell.FormatParams as TwoColorsParams)!.ValueColor = ColorFormatting.ConvertTwoRange(Convert.ToDouble(list[i].Cells[formatColumn].Value), _formatConditions[j].MinValue, _formatConditions[j].MaxValue, _formatConditions[j].FormatParams as TwoColorsParams);
                                }
                                else
                                {
                                    (fCell.FormatParams as TwoColorsParams)!.ValueColor =
                                        ColorFormatting.ConvertTwoRange(
                                            Convert.ToDouble(list[i].Cells[formatColumn].Value), _formatConditions[j].MinValue, _formatConditions[j].MaxValue, _formatConditions[j].FormatParams as TwoColorsParams);
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
            Stopwatch azer = new Stopwatch();
            azer.Start();
#endif
            List<OutlookGridRow> list;
            List<OutlookGridRow> tmp; // = new List<OutlookGridRow>();
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
            Console.WriteLine(@"Formatting : " + azer.ElapsedMilliseconds + @" ms");
            azer.Start();
#endif
            // this block is used of grouping is turned off
            // this will simply list all attributes of each object in the list
            if (_internalColumns.CountGrouped() == 0)
            {
                //Applying sort
                //try
                //{
                list.Sort(new OutlookGridRowComparer2(_internalColumns.GetIndexAndSortSortedOnlyColumns()));
                //}
                //catch (Exception e)
                //{
                //    MessageBox.Show("Failed to sort.\n\n Error:" + e.Message,
                //                   "Grid Sorting",
                //                   MessageBoxButtons.OK,
                //                   MessageBoxIcon.Error);
                // }

                //Add rows to underlying DataGridView
                if (_fillMode == GridFillMode.GroupsOnly)
                {
                    if (list != null)
                    {
                        Rows.AddRange(list.ToArray());
                    }
                }
                else
                {
                    tmp = new List<OutlookGridRow>();
                    NonGroupedRecursiveFillOutlookGridRows(list, tmp);

                    //Add all the rows to the grid
                    if (tmp != null)
                    {
                        Rows.AddRange(tmp.ToArray());
                    }
                }

            }
            // this block is used when grouping is used
            // items in the list must be sorted, and then they will automatically be grouped
            else
            {
                //Group part of the job
                //try
                //{
                //We get the columns that are grouped
                List<OutlookGridColumn>? groupedColumns = _internalColumns.FindGroupedColumns();

                //For each outlookgrid row in the grid
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
                            object? value = list[j].Cells[groupedColumns[i].DataGridViewColumn.Index].Value;
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
                                        groupedColumns[i].DataGridViewColumn.DefaultCellStyle
                                            .Format; //We can the formatting applied to the cell to the group
                                    if (value is TextAndImage)
                                    {
                                        gr.GroupImage = (value as TextAndImage)?.Image;
                                    }
                                    else if (value is Token)
                                    {
                                        Bitmap bmp = new(13, 13);
                                        using (Graphics gfx = Graphics.FromImage(bmp))
                                        {
                                            using (SolidBrush brush = new(((Token)value).BackColor))
                                            {
                                                gfx.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                                            }
                                        }

                                        gr.GroupImage = bmp;
                                    }
                                    else if (value is Bitmap)
                                    {
                                        gr.GroupImage = (Bitmap)value;
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

                                //if we have browsed all the groups we are sure to be in the righr group: add rows and update counters !
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
                //}
                //catch (Exception e)
                //{
                //    MessageBox.Show("Failed to add rows.\n\n Error:" + e.Message,
                //                   "Grid Filling",
                //                   MessageBoxButtons.OK,
                //                   MessageBoxIcon.Error);
                //}

                //Sorting part : sort the groups between them and sort the rows inside the groups
                //try
                //{
                //int index = internalColumns.FindSortedColumnNotgrouped();
                //RecursiveSort(this.groupCollection, index, (index == -1) ? SortOrder.None : internalColumns.FindFromColumnIndex(index).SortDirection);
                List<Tuple<int, SortOrder, IComparer>> sortList = _internalColumns.GetIndexAndSortSortedOnlyColumns();
                if (sortList.Count > 0)
                {
                    RecursiveSort(_groupCollection, sortList);
                }
                else
                {
                    RecursiveSort(_groupCollection, _internalColumns.GetIndexAndSortGroupedColumns());
                }
                //}
                //catch (Exception e)
                //{
                //    MessageBox.Show("Failed to sort.\n\n Error:" + e.Message,
                //                   "Grid Sorting",
                //                   MessageBoxButtons.OK,
                //                   MessageBoxIcon.Error);
                //}

                //Reinit!
                tmp = new List<OutlookGridRow>();
                //Get a list of rows (grouprow and non-grouprow)
                RecursiveFillOutlookGridRows(_groupCollection, tmp);

                //Finally add the rows to underlying datagridview after all the magic !
                Rows.AddRange(tmp.ToArray());
            }
            Cursor.Current = Cursors.Default;
#if DEBUG
            azer.Stop();
            Console.WriteLine(@"FillGrid : " + azer.ElapsedMilliseconds + @" ms");
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
                //    Console.WriteLine("groupCollection[i].Rows" + groupCollection[i].Rows.Count.ToString());
                //    //We sort the rows according to the group sort (useful for alphbetics for example)
                //    groupCollection[i].Rows.Sort(new OutlookGridRowComparer(groupCollection[i].Column.DataGridViewColumn.Index, internalColumns[groupCollection[i].Column.DataGridViewColumn.Name].SortDirection));
                //}

                //Recursive call for children
                RecursiveSort(groupCollection[i]!.Children, sortList);
            }
        }

        /// <summary>
        /// Update all the parents counters of a group
        /// </summary>
        /// <param name="l">The group whic</param>
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
                }
            }
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
                    writer.WriteElementString("Visible", col.DataGridViewColumn.Visible.ToString());
                    writer.WriteElementString("Width", col.DataGridViewColumn.Width.ToString());
                    writer.WriteElementString("Index", col.DataGridViewColumn.Index.ToString());
                    writer.WriteElementString("DisplayIndex", col.DataGridViewColumn.DisplayIndex.ToString());
                    writer.WriteElementString("RowsComparer", col != null && col.RowsComparer == null ? "" : col?.RowsComparer.GetType().AssemblyQualifiedName);
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
            _internalColumns.Clear();
            Columns.Clear();
            ConditionalFormatting.Clear();
            //Snif everything is gone ! Be Ready for a new start !
        }

        /// <summary>
        /// Finds the column from its name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public OutlookGridColumn FindFromColumnName(string? name)
        => _internalColumns.FindFromColumnName(name);

        /// <summary>
        /// Finds the column from its index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public OutlookGridColumn FindFromColumnIndex(int index) => _internalColumns.FindFromColumnIndex(index);

        #endregion OutlookGrid methods
    }
}