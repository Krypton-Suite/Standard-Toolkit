#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

using static Krypton.Utilities.KryptonAdvancedDataGridViewSearchToolBar;

namespace Krypton.Utilities;

[DesignerCategory("code")]
internal partial class MenuStrip : ContextMenuStrip
{
    #region Designer Code

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer? components;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this._sortAscMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._sortDescMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._cancelSortMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._toolStripSeparator1MenuItem = new System.Windows.Forms.ToolStripSeparator();
        this._cancelFilterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._customFilterLastFiltersListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._customFilterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._toolStripSeparator2MenuItem = new System.Windows.Forms.ToolStripSeparator();
        this._customFilterLastFilter1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._customFilterLastFilter2MenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._customFilterLastFilter3MenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._customFilterLastFilter4MenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._customFilterLastFilter5MenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this._toolStripSeparator3MenuItem = new System.Windows.Forms.ToolStripSeparator();
        this._checkList = new System.Windows.Forms.TreeView();
        this._buttonFilter = new System.Windows.Forms.Button();
        this._buttonUndofilter = new System.Windows.Forms.Button();
        this._checkFilterListPanel = new System.Windows.Forms.Panel();
        this._checkFilterListButtonsPanel = new System.Windows.Forms.Panel();
        this._checkFilterListButtonsControlHost = new System.Windows.Forms.ToolStripControlHost(_checkFilterListButtonsPanel);
        this._checkFilterListControlHost = new System.Windows.Forms.ToolStripControlHost(_checkFilterListPanel);
        this._checkTextFilter = new System.Windows.Forms.TextBox();
        this._checkTextFilterControlHost = new System.Windows.Forms.ToolStripControlHost(_checkTextFilter);
        this._resizeBoxControlHost = new System.Windows.Forms.ToolStripControlHost(new System.Windows.Forms.Control());
        this.SuspendLayout();
        //
        // MenuStrip
        //
        this.BackColor = System.Drawing.SystemColors.ControlLightLight;
        this.AutoSize = false;
        this.Padding = new System.Windows.Forms.Padding(0);
        this.Margin = new System.Windows.Forms.Padding(0);
        this.Size = new System.Drawing.Size(287, 370);
        this.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(MenuStrip_Closed);
        this.LostFocus += new System.EventHandler(MenuStrip_LostFocus);
        this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            _sortAscMenuItem,
            _sortDescMenuItem,
            _cancelSortMenuItem,
            _toolStripSeparator1MenuItem,
            _cancelFilterMenuItem,
            _customFilterLastFiltersListMenuItem,
            _toolStripSeparator3MenuItem,
            _checkTextFilterControlHost,
            _checkFilterListControlHost,
            _checkFilterListButtonsControlHost,
            _resizeBoxControlHost});
        //
        // sortASCMenuItem
        //
        this._sortAscMenuItem.Name = "_sortAscMenuItem";
        this._sortAscMenuItem.AutoSize = false;
        this._sortAscMenuItem.Size = new System.Drawing.Size(Width - 1, 22);
        this._sortAscMenuItem.Click += new System.EventHandler(SortASCMenuItem_Click);
        this._sortAscMenuItem.MouseEnter += new System.EventHandler(SortASCMenuItem_MouseEnter);
        this._sortAscMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        //
        // sortDESCMenuItem
        //
        this._sortDescMenuItem.Name = "_sortDescMenuItem";
        this._sortDescMenuItem.AutoSize = false;
        this._sortDescMenuItem.Size = new System.Drawing.Size(Width - 1, 22);
        this._sortDescMenuItem.Click += new System.EventHandler(SortDESCMenuItem_Click);
        this._sortDescMenuItem.MouseEnter += new System.EventHandler(SortDESCMenuItem_MouseEnter);
        this._sortDescMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        //
        // cancelSortMenuItem
        //
        this._cancelSortMenuItem.Name = "_cancelSortMenuItem";
        this._cancelSortMenuItem.Enabled = false;
        this._cancelSortMenuItem.AutoSize = false;
        this._cancelSortMenuItem.Size = new System.Drawing.Size(Width - 1, 22);
        this._cancelSortMenuItem.Text = "Clear Sort";
        this._cancelSortMenuItem.Click += new System.EventHandler(CancelSortMenuItem_Click);
        this._cancelSortMenuItem.MouseEnter += new System.EventHandler(CancelSortMenuItem_MouseEnter);
        //
        // toolStripSeparator1MenuItem
        //
        this._toolStripSeparator1MenuItem.Name = "_toolStripSeparator1MenuItem";
        this._toolStripSeparator1MenuItem.Size = new System.Drawing.Size(Width - 4, 6);
        //
        // cancelFilterMenuItem
        //
        this._cancelFilterMenuItem.Name = "_cancelFilterMenuItem";
        this._cancelFilterMenuItem.Enabled = false;
        this._cancelFilterMenuItem.AutoSize = false;
        this._cancelFilterMenuItem.Size = new System.Drawing.Size(Width - 1, 22);
        this._cancelFilterMenuItem.Text = "Clear Filter";
        this._cancelFilterMenuItem.Click += new System.EventHandler(CancelFilterMenuItem_Click);
        this._cancelFilterMenuItem.MouseEnter += new System.EventHandler(CancelFilterMenuItem_MouseEnter);
        //
        // toolStripMenuItem2
        //
        this._toolStripSeparator2MenuItem.Name = "_toolStripSeparator2MenuItem";
        this._toolStripSeparator2MenuItem.Size = new System.Drawing.Size(149, 6);
        this._toolStripSeparator2MenuItem.Visible = false;
        //
        // customFilterMenuItem
        //
        this._customFilterMenuItem.Name = "_customFilterMenuItem";
        this._customFilterMenuItem.Size = new System.Drawing.Size(152, 22);
        this._customFilterMenuItem.Text = "Add a Custom Filter";
        this._customFilterMenuItem.Click += new System.EventHandler(CustomFilterMenuItem_Click);
        //
        // customFilterLastFilter1MenuItem
        //
        this._customFilterLastFilter1MenuItem.Name = "_customFilterLastFilter1MenuItem";
        this._customFilterLastFilter1MenuItem.Size = new System.Drawing.Size(152, 22);
        this._customFilterLastFilter1MenuItem.Tag = "0";
        this._customFilterLastFilter1MenuItem.Text = null;
        this._customFilterLastFilter1MenuItem.Visible = false;
        this._customFilterLastFilter1MenuItem.VisibleChanged += new System.EventHandler(CustomFilterLastFilter1MenuItem_VisibleChanged);
        this._customFilterLastFilter1MenuItem.Click += new System.EventHandler(CustomFilterLastFilterMenuItem_Click);
        this._customFilterLastFilter1MenuItem.TextChanged += new System.EventHandler(CustomFilterLastFilterMenuItem_TextChanged);
        //
        // customFilterLastFilter2MenuItem
        //
        this._customFilterLastFilter2MenuItem.Name = "_customFilterLastFilter2MenuItem";
        this._customFilterLastFilter2MenuItem.Size = new System.Drawing.Size(152, 22);
        this._customFilterLastFilter2MenuItem.Tag = "1";
        this._customFilterLastFilter2MenuItem.Text = null;
        this._customFilterLastFilter2MenuItem.Visible = false;
        this._customFilterLastFilter2MenuItem.Click += new System.EventHandler(CustomFilterLastFilterMenuItem_Click);
        this._customFilterLastFilter2MenuItem.TextChanged += new System.EventHandler(CustomFilterLastFilterMenuItem_TextChanged);
        //
        // customFilterLastFilter3MenuItem
        //
        this._customFilterLastFilter3MenuItem.Name = "_customFilterLastFilter3MenuItem";
        this._customFilterLastFilter3MenuItem.Size = new System.Drawing.Size(152, 22);
        this._customFilterLastFilter3MenuItem.Tag = "2";
        this._customFilterLastFilter3MenuItem.Text = null;
        this._customFilterLastFilter3MenuItem.Visible = false;
        this._customFilterLastFilter3MenuItem.Click += new System.EventHandler(CustomFilterLastFilterMenuItem_Click);
        this._customFilterLastFilter3MenuItem.TextChanged += new System.EventHandler(CustomFilterLastFilterMenuItem_TextChanged);
        //
        // customFilterLastFilter3MenuItem
        //
        this._customFilterLastFilter4MenuItem.Name = "lastfilter4MenuItem";
        this._customFilterLastFilter4MenuItem.Size = new System.Drawing.Size(152, 22);
        this._customFilterLastFilter4MenuItem.Tag = "3";
        this._customFilterLastFilter4MenuItem.Text = null;
        this._customFilterLastFilter4MenuItem.Visible = false;
        this._customFilterLastFilter4MenuItem.Click += new System.EventHandler(CustomFilterLastFilterMenuItem_Click);
        this._customFilterLastFilter4MenuItem.TextChanged += new System.EventHandler(CustomFilterLastFilterMenuItem_TextChanged);
        //
        // customFilterLastFilter5MenuItem
        //
        this._customFilterLastFilter5MenuItem.Name = "_customFilterLastFilter5MenuItem";
        this._customFilterLastFilter5MenuItem.Size = new System.Drawing.Size(152, 22);
        this._customFilterLastFilter5MenuItem.Tag = "4";
        this._customFilterLastFilter5MenuItem.Text = null;
        this._customFilterLastFilter5MenuItem.Visible = false;
        this._customFilterLastFilter5MenuItem.Click += new System.EventHandler(CustomFilterLastFilterMenuItem_Click);
        this._customFilterLastFilter5MenuItem.TextChanged += new System.EventHandler(CustomFilterLastFilterMenuItem_TextChanged);
        //
        // customFilterLastFiltersListMenuItem
        //
        this._customFilterLastFiltersListMenuItem.Name = "_customFilterLastFiltersListMenuItem";
        this._customFilterLastFiltersListMenuItem.AutoSize = false;
        this._customFilterLastFiltersListMenuItem.Size = new System.Drawing.Size(Width - 1, 22);
        this._customFilterLastFiltersListMenuItem.Image = Properties.Resources.ColumnHeader_Filtered;
        this._customFilterLastFiltersListMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        this._customFilterLastFiltersListMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            _customFilterMenuItem,
            _toolStripSeparator2MenuItem,
            _customFilterLastFilter1MenuItem,
            _customFilterLastFilter2MenuItem,
            _customFilterLastFilter3MenuItem,
            _customFilterLastFilter4MenuItem,
            _customFilterLastFilter5MenuItem});
        this._customFilterLastFiltersListMenuItem.MouseEnter += new System.EventHandler(CustomFilterLastFiltersListMenuItem_MouseEnter);
        this._customFilterLastFiltersListMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(CustomFilterLastFiltersListMenuItem_Paint);
        //
        // toolStripMenuItem3
        //
        this._toolStripSeparator3MenuItem.Name = "_toolStripSeparator3MenuItem";
        this._toolStripSeparator3MenuItem.Size = new System.Drawing.Size(Width - 4, 6);
        //
        // button_filter
        //
        this._buttonFilter.Name = "_buttonFilter";
        this._buttonFilter.BackColor = System.Windows.Forms.Button.DefaultBackColor;
        this._buttonFilter.UseVisualStyleBackColor = true;
        this._buttonFilter.Margin = new System.Windows.Forms.Padding(0);
        this._buttonFilter.Size = new System.Drawing.Size(75, 23);
        this._buttonFilter.Text = "Filter";
        this._buttonFilter.Click += new System.EventHandler(Button_ok_Click);
        this._buttonFilter.Location = new System.Drawing.Point(this._checkFilterListButtonsPanel.Width - 164, 0);
        //
        // button_undofilter
        //
        this._buttonUndofilter.Name = "_buttonUndofilter";
        this._buttonUndofilter.BackColor = System.Windows.Forms.Button.DefaultBackColor;
        this._buttonUndofilter.UseVisualStyleBackColor = true;
        this._buttonUndofilter.Margin = new System.Windows.Forms.Padding(0);
        this._buttonUndofilter.Size = new System.Drawing.Size(75, 23);
        this._buttonUndofilter.Text = "Cancel";
        this._buttonUndofilter.Click += new System.EventHandler(Button_cancel_Click);
        this._buttonUndofilter.Location = new System.Drawing.Point(this._checkFilterListButtonsPanel.Width - 79, 0);
        //
        // resizeBoxControlHost
        //
        this._resizeBoxControlHost.Name = "_resizeBoxControlHost";
        this._resizeBoxControlHost.Control.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
        this._resizeBoxControlHost.AutoSize = false;
        this._resizeBoxControlHost.Padding = new System.Windows.Forms.Padding(0);
        this._resizeBoxControlHost.Margin = new System.Windows.Forms.Padding(Width - 45, 0, 0, 0);
        this._resizeBoxControlHost.Size = new System.Drawing.Size(10, 10);
        this._resizeBoxControlHost.Paint += new System.Windows.Forms.PaintEventHandler(ResizeBoxControlHost_Paint);
        this._resizeBoxControlHost.MouseDown += new System.Windows.Forms.MouseEventHandler(ResizeBoxControlHost_MouseDown);
        this._resizeBoxControlHost.MouseUp += new System.Windows.Forms.MouseEventHandler(ResizeBoxControlHost_MouseUp);
        this._resizeBoxControlHost.MouseMove += new System.Windows.Forms.MouseEventHandler(ResizeBoxControlHost_MouseMove);
        //
        // checkFilterListControlHost
        //
        this._checkFilterListControlHost.Name = "_checkFilterListControlHost";
        this._checkFilterListControlHost.AutoSize = false;
        this._checkFilterListControlHost.Size = new System.Drawing.Size(Width - 35, 194);
        this._checkFilterListControlHost.Padding = new System.Windows.Forms.Padding(0);
        this._checkFilterListControlHost.Margin = new System.Windows.Forms.Padding(0);
        //
        // checkTextFilterControlHost
        //
        this._checkTextFilterControlHost.Name = "_checkTextFilterControlHost";
        this._checkTextFilterControlHost.AutoSize = false;
        this._checkTextFilterControlHost.Size = new System.Drawing.Size(Width - 35, 20);
        this._checkTextFilterControlHost.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
        this._checkTextFilterControlHost.Margin = new System.Windows.Forms.Padding(0);
        //
        // checkFilterListButtonsControlHost
        //
        this._checkFilterListButtonsControlHost.Name = "_checkFilterListButtonsControlHost";
        this._checkFilterListButtonsControlHost.AutoSize = false;
        this._checkFilterListButtonsControlHost.Size = new System.Drawing.Size(Width - 35, 24);
        this._checkFilterListButtonsControlHost.Padding = new System.Windows.Forms.Padding(0);
        this._checkFilterListButtonsControlHost.Margin = new System.Windows.Forms.Padding(0);
        //
        // checkFilterListPanel
        //
        this._checkFilterListPanel.Name = "_checkFilterListPanel";
        this._checkFilterListPanel.AutoSize = false;
        this._checkFilterListPanel.Size = _checkFilterListControlHost.Size;
        this._checkFilterListPanel.Padding = new System.Windows.Forms.Padding(0);
        this._checkFilterListPanel.Margin = new System.Windows.Forms.Padding(0);
        this._checkFilterListPanel.BackColor = BackColor;
        this._checkFilterListPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this._checkFilterListPanel.Controls.Add(_checkList);
        //
        // checkList
        //
        this._checkList.Name = "_checkList";
        this._checkList.AutoSize = false;
        this._checkList.Padding = new System.Windows.Forms.Padding(0);
        this._checkList.Margin = new System.Windows.Forms.Padding(0);
        this._checkList.Bounds = new System.Drawing.Rectangle(4, 4, this._checkFilterListPanel.Width - 8, this._checkFilterListPanel.Height - 8);
        this._checkList.StateImageList = GetCheckListStateImages();
        this._checkList.CheckBoxes = false;
        this._checkList.MouseLeave += new System.EventHandler(CheckList_MouseLeave);
        this._checkList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(CheckList_NodeMouseClick);
        this._checkList.KeyDown += new System.Windows.Forms.KeyEventHandler(CheckList_KeyDown);
        this._checkList.MouseEnter += CheckList_MouseEnter;
        this._checkList.NodeMouseDoubleClick += CheckList_NodeMouseDoubleClick;
        //
        // checkTextFilter
        //
        this._checkTextFilter.Name = "_checkTextFilter";
        this._checkTextFilter.Padding = new System.Windows.Forms.Padding(0);
        this._checkTextFilter.Margin = new System.Windows.Forms.Padding(0);
        this._checkTextFilter.Size = _checkTextFilterControlHost.Size;
        this._checkTextFilter.Dock = System.Windows.Forms.DockStyle.Fill;
        this._checkTextFilter.TextChanged += new System.EventHandler(CheckTextFilter_TextChanged);
        //
        // checkFilterListButtonsPanel
        //
        this._checkFilterListButtonsPanel.Name = "_checkFilterListButtonsPanel";
        this._checkFilterListButtonsPanel.AutoSize = false;
        this._checkFilterListButtonsPanel.Size = _checkFilterListButtonsControlHost.Size;
        this._checkFilterListButtonsPanel.Padding = new System.Windows.Forms.Padding(0);
        this._checkFilterListButtonsPanel.Margin = new System.Windows.Forms.Padding(0);
        this._checkFilterListButtonsPanel.BackColor = BackColor;
        this._checkFilterListButtonsPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this._checkFilterListButtonsPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
            _buttonFilter,
            _buttonUndofilter
        });
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private ToolStripMenuItem _sortAscMenuItem;
    private ToolStripMenuItem _sortDescMenuItem;
    private ToolStripMenuItem _cancelSortMenuItem;
    private ToolStripSeparator _toolStripSeparator1MenuItem;
    private ToolStripSeparator _toolStripSeparator2MenuItem;
    private ToolStripSeparator _toolStripSeparator3MenuItem;
    private ToolStripMenuItem _cancelFilterMenuItem;
    private ToolStripMenuItem _customFilterLastFiltersListMenuItem;
    private ToolStripMenuItem _customFilterMenuItem;
    private ToolStripMenuItem _customFilterLastFilter1MenuItem;
    private ToolStripMenuItem _customFilterLastFilter2MenuItem;
    private ToolStripMenuItem _customFilterLastFilter3MenuItem;
    private ToolStripMenuItem _customFilterLastFilter4MenuItem;
    private ToolStripMenuItem _customFilterLastFilter5MenuItem;
    private TreeView _checkList;
    private Button _buttonFilter;
    private Button _buttonUndofilter;
    private ToolStripControlHost _checkFilterListControlHost;
    private ToolStripControlHost _checkFilterListButtonsControlHost;
    private ToolStripControlHost _resizeBoxControlHost;
    private Panel _checkFilterListPanel;
    private Panel _checkFilterListButtonsPanel;
    private TextBox _checkTextFilter;
    private ToolStripControlHost _checkTextFilterControlHost;

    #endregion

    #region public enum

    /// <summary>
    /// MenuStrip Filter type
    /// </summary>
    public enum FilterType : byte
    {
        None = 0,
        Custom = 1,
        CheckList = 2,
        Loaded = 3
    }


    /// <summary>
    /// MenuStrip Sort type
    /// </summary>
    public enum SortType : byte
    {
        None = 0,
        Asc = 1,
        Desc = 2
    }

    #endregion


    #region public constants

    /// <summary>
    /// Default checklist filter node behaviour 
    /// </summary>
    public const bool DefaultCheckTextFilterRemoveNodesOnSearch = true;

    /// <summary>
    /// Default max filter checklist max nodes
    /// </summary>
    public const int DefaultMaxChecklistNodes = 10000;

    /// <summary>
    /// Default number of nodes to enable the TextChanged delay on text filter
    /// </summary>
    public const int DefaultTextFilterTextChangedDelayNodes = 1000;

    /// <summary>
    /// Number of nodes to disable the text filter TextChanged delay
    /// </summary>
    public const int TextFilterTextChangedDelayNodesDisabled = -1;

    /// <summary>
    /// Default delay milliseconds for TextChanged delay on text filter
    /// </summary>
    public const int DefaultTextFilterTextChangedDelayMs = 300;

    #endregion


    #region class properties

    private FilterType _activeFilterType = FilterType.None;
    private SortType _activeSortType = SortType.None;
    private TreeNodeItemSelector?[] _loadedNodes = [];
    private TreeNodeItemSelector?[] _startingNodes = [];
    private TreeNodeItemSelector?[] _removedNodes = [];
    private TreeNodeItemSelector?[] _removedSessionNodes = [];
    private string? _sortString;
    private string? _filterString;
    private static readonly Point _resizeStartPoint = new Point(1, 1);
    private Point _resizeEndPoint = new Point(-1, -1);
    private bool _checkTextFilterChangedEnabled = true;
    private bool _checkTextFilterRemoveNodesOnSearch = DefaultCheckTextFilterRemoveNodesOnSearch;
    private int _maxChecklistNodes = DefaultMaxChecklistNodes;
    private bool _filterClick;
    private Timer? _textFilterTextChangedTimer;
    private int _textFilterTextChangedDelayNodes = DefaultTextFilterTextChangedDelayNodes;
    private int _textFilterTextChangedDelayMs = DefaultTextFilterTextChangedDelayMs;

    #endregion


    #region Identity

    /// <summary>
    /// MenuStrip constructor
    /// </summary>
    /// <param name="dataType"></param>
    public MenuStrip(Type dataType)
        : base()
    {
        //initialize components
        InitializeComponent();

        //set component translations
        _cancelSortMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewClearSort)];
        _cancelFilterMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewClearFilter)];
        _customFilterMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewAddCustomFilter)];
        _buttonFilter!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewButtonFilter)];
        _buttonUndofilter!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewButtonUndoFilter)];

        //set type
        DataType = dataType;

        //set components values
        if (DataType == typeof(DateTime) || DataType == typeof(TimeSpan))
        {
            _customFilterLastFiltersListMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewCustomFilter)];
            _sortAscMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewSortDateTimeAscending)];
            _sortDescMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewSortDateTimeDescending)];
            _sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCnum;
            _sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCnum;
        }
        else if (DataType == typeof(bool))
        {
            _customFilterLastFiltersListMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewCustomFilter)];
            _sortAscMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewSortBoolAscending)];
            _sortDescMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewSortBoolDescending)];
            _sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCbool;
            _sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCbool;
        }
        else if (DataType == typeof(Int32) || DataType == typeof(Int64) || DataType == typeof(Int16) ||
                 DataType == typeof(UInt32) || DataType == typeof(UInt64) || DataType == typeof(UInt16) ||
                 DataType == typeof(Byte) || DataType == typeof(SByte) || DataType == typeof(Decimal) ||
                 DataType == typeof(Single) || DataType == typeof(Double))
        {
            _customFilterLastFiltersListMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewCustomFilter)];
            _sortAscMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewSortNumAscending)];
            _sortDescMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewSortNumDescending)];
            _sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCnum;
            _sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCnum;
        }
        else
        {
            _customFilterLastFiltersListMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewCustomFilter)];
            _sortAscMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewSortTextAscending)];
            _sortDescMenuItem!.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewSortTextDescending)];
            _sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCtxt;
            _sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCtxt;
        }

        //set check filter textbox
        if (DataType == typeof(DateTime) || DataType == typeof(TimeSpan) || DataType == typeof(bool))
        {
            _checkTextFilter!.Enabled = false;
        }

        //set default NOT IN logic
        IsFilterNotinLogicEnabled = false;

        //sent enablers default
        IsSortEnabled = true;
        IsFilterEnabled = true;
        IsFilterChecklistEnabled = true;
        IsFilterDateAndTimeEnabled = true;

        //set default components
        _customFilterLastFiltersListMenuItem.Enabled = DataType != typeof(bool);
        _customFilterLastFiltersListMenuItem.Checked = ActiveFilterType == FilterType.Custom;

        //resize before hitting ResizeBox so the grip works correctly
        float scalingFactor = GetScalingFactor();
        MinimumSize = new Size(Scale(PreferredSize.Width, scalingFactor), Scale(PreferredSize.Height, scalingFactor));
        //once the size is set resize the ones that won't change      
        _resizeBoxControlHost!.Height = Scale(_resizeBoxControlHost.Height, scalingFactor);
        _resizeBoxControlHost.Width = Scale(_resizeBoxControlHost.Width, scalingFactor);
        _toolStripSeparator1MenuItem!.Height = Scale(_toolStripSeparator1MenuItem.Height, scalingFactor);
        _toolStripSeparator2MenuItem!.Height = Scale(_toolStripSeparator2MenuItem.Height, scalingFactor);
        _toolStripSeparator3MenuItem!.Height = Scale(_toolStripSeparator3MenuItem.Height, scalingFactor);
        _sortAscMenuItem.Height = Scale(_sortAscMenuItem.Height, scalingFactor);
        _sortDescMenuItem.Height = Scale(_sortDescMenuItem.Height, scalingFactor);
        _cancelSortMenuItem.Height = Scale(_cancelSortMenuItem.Height, scalingFactor);
        _cancelFilterMenuItem.Height = Scale(_cancelFilterMenuItem.Height, scalingFactor);
        _customFilterMenuItem.Height = Scale(_customFilterMenuItem.Height, scalingFactor);
        _customFilterLastFiltersListMenuItem.Height = Scale(_customFilterLastFiltersListMenuItem.Height, scalingFactor);
        _checkTextFilterControlHost!.Height = Scale(_checkTextFilterControlHost.Height, scalingFactor);
        _buttonFilter.Width = Scale(_buttonFilter.Width, scalingFactor);
        _buttonFilter.Height = Scale(_buttonFilter.Height, scalingFactor);
        _buttonUndofilter.Width = Scale(_buttonUndofilter.Width, scalingFactor);
        _buttonUndofilter.Height = Scale(_buttonUndofilter.Height, scalingFactor);
        //resize
        ResizeBox(MinimumSize.Width, MinimumSize.Height);

        _textFilterTextChangedTimer = new Timer();
        _textFilterTextChangedTimer.Interval = _textFilterTextChangedDelayMs;
        _textFilterTextChangedTimer.Tick += new EventHandler(CheckTextFilterTextChangedTimer_Tick);

        RenderMode = ToolStripRenderMode.ManagerRenderMode;
    }

    /// <summary>
    /// Closed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuStrip_Closed(object? sender, EventArgs e)
    {
        ResizeClean();

        if (_checkTextFilterRemoveNodesOnSearch && !_filterClick)
        {
            _loadedNodes = DuplicateNodes(_startingNodes);
        }

        _startingNodes = [];

        _checkTextFilterChangedEnabled = false;
        _checkTextFilter.Text = "";
        _checkTextFilterChangedEnabled = true;
    }

    /// <summary>
    /// LostFocus event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuStrip_LostFocus(object? sender, EventArgs e)
    {
        if (!ContainsFocus)
        {
            Close();
        }
    }

    /// <summary>
    /// Control removed event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnControlRemoved(ControlEventArgs e)
    {
        _loadedNodes = [];
        _startingNodes = [];
        _removedNodes = [];
        _removedSessionNodes = [];
        _textFilterTextChangedTimer?.Stop();

        base.OnControlRemoved(e);
    }

    /// <summary>
    /// Get all images for checkList
    /// </summary>
    /// <returns></returns>
    private static ImageList GetCheckListStateImages()
    {
        ImageList images = new ImageList();
        Bitmap unCheckImg = new Bitmap(16, 16);
        Bitmap checkImg = new Bitmap(16, 16);
        Bitmap mixedImg = new Bitmap(16, 16);

        using (Bitmap img = new Bitmap(16, 16))
        {
            using (Graphics g = Graphics.FromImage(img))
            {
                CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.UncheckedNormal);
                unCheckImg = (Bitmap)img.Clone();
                CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.CheckedNormal);
                checkImg = (Bitmap)img.Clone();
                CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.MixedNormal);
                mixedImg = (Bitmap)img.Clone();
            }
        }

        images.Images.Add("uncheck", unCheckImg);
        images.Images.Add("check", checkImg);
        images.Images.Add("mixed", mixedImg);

        return images;
    }

    #endregion


    #region public events

    /// <summary>
    /// The current Sorting in changed
    /// </summary>
    public event EventHandler? SortChanged;

    /// <summary>
    /// The current Filter is changed
    /// </summary>
    public event EventHandler? FilterChanged;

    #endregion


    #region public getter and setters

    /// <summary>
    /// Set the max checklist nodes
    /// </summary>
    [DefaultValue(DefaultMaxChecklistNodes)]
    public int MaxChecklistNodes
    {
        get => _maxChecklistNodes;
        set => _maxChecklistNodes = value;
    }

    /// <summary>
    /// Get the current MenuStripSortType type
    /// </summary>
    public SortType ActiveSortType => _activeSortType;

    /// <summary>
    /// Get the current MenuStripFilterType type
    /// </summary>
    public FilterType ActiveFilterType => _activeFilterType;

    /// <summary>
    /// Get the DataType for the MenuStrip Filter
    /// </summary>
    public Type DataType { get; private set; }

    /// <summary>
    /// Get or Set the Filter Sort enabled
    /// </summary>
    [DefaultValue(false)]
    public bool IsSortEnabled { get; set; }

    /// <summary>
    /// Get or Set the Filter enabled
    /// </summary>
    [DefaultValue(false)]
    public bool IsFilterEnabled { get; set; }

    /// <summary>
    /// Get or Set the Filter Checklist enabled
    /// </summary>
    [DefaultValue(false)]
    public bool IsFilterChecklistEnabled { get; set; }

    /// <summary>
    /// Get or Set the Filter Custom enabled
    /// </summary>
    [DefaultValue(false)]
    public bool IsFilterCustomEnabled { get; set; }

    /// <summary>
    /// Get or Set the Filter DateAndTime enabled
    /// </summary>
    [DefaultValue(false)]
    public bool IsFilterDateAndTimeEnabled { get; set; }

    /// <summary>
    /// Get or Set the NOT IN logic for Filter
    /// </summary>
    [DefaultValue(false)]
    public bool IsFilterNotinLogicEnabled { get; set; }

    /// <summary>
    /// Set the text filter search nodes behaviour
    /// </summary>
    [DefaultValue(DefaultCheckTextFilterRemoveNodesOnSearch)]
    public bool DoesTextFilterRemoveNodesOnSearch
    {
        get => _checkTextFilterRemoveNodesOnSearch;
        set => _checkTextFilterRemoveNodesOnSearch = value;
    }

    /// <summary>
    /// Number of nodes to enable the TextChanged delay on text filter
    /// </summary>
    [DefaultValue(DefaultTextFilterTextChangedDelayNodes)]
    public int TextFilterTextChangedDelayNodes
    {
        get => _textFilterTextChangedDelayNodes;
        set => _textFilterTextChangedDelayNodes = value;
    }

    /// <summary>
    /// Delay milliseconds for TextChanged delay on text filter
    /// </summary>
    [DefaultValue(DefaultTextFilterTextChangedDelayMs)]
    public int TextFilterTextChangedDelayMs
    {
        get => _textFilterTextChangedDelayMs;
        set => _textFilterTextChangedDelayMs = value;
    }

    #endregion


    #region public enablers

    /// <summary>
    /// Enabled or disable Sorting capabilities
    /// </summary>
    /// <param name="enabled"></param>
    public void SetSortEnabled(bool enabled)
    {
        IsSortEnabled = enabled;

        _sortAscMenuItem.Enabled = enabled;
        _sortDescMenuItem.Enabled = enabled;
        _cancelSortMenuItem.Enabled = enabled;
    }

    /// <summary>
    /// Enable or disable Filter capabilities
    /// </summary>
    /// <param name="enabled"></param>
    public void SetFilterEnabled(bool enabled)
    {
        IsFilterEnabled = enabled;

        _cancelFilterMenuItem.Enabled = enabled;
        _customFilterLastFiltersListMenuItem.Enabled = enabled && DataType != typeof(bool);
        _buttonFilter.Enabled = enabled;
        _buttonUndofilter.Enabled = enabled;
        _checkList.Enabled = enabled;
        _checkTextFilter.Enabled = enabled;
    }

    /// <summary>
    /// Enable or disable Filter checklist capabilities
    /// </summary>
    /// <param name="enabled"></param>
    public void SetFilterChecklistEnabled(bool enabled)
    {
        if (!IsFilterEnabled)
        {
            enabled = false;
        }

        IsFilterChecklistEnabled = enabled;
        _checkList.Enabled = enabled;
        _checkTextFilter.ReadOnly = !enabled;

        if (!IsFilterChecklistEnabled)
        {
            ChecklistClearNodes();
            TreeNodeItemSelector disabledNode = TreeNodeItemSelector.CreateNode(
                $"{KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewFilterChecklistDisable)]}            ", null, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.SelectAll);
            disabledNode.NodeFont = new Font(_checkList.Font, FontStyle.Bold);
            ChecklistAddNode(disabledNode);
            ChecklistReloadNodes();
        }
    }

    /// <summary>
    /// Enable or disable Filter custom capabilities
    /// </summary>
    /// <param name="enabled"></param>
    public void SetFilterCustomEnabled(bool enabled)
    {
        if (!IsFilterEnabled)
        {
            enabled = false;
        }

        IsFilterCustomEnabled = enabled;
        _customFilterMenuItem.Enabled = enabled;
        _customFilterLastFiltersListMenuItem.Enabled = enabled;

        if (!IsFilterCustomEnabled)
        {
            UnCheckCustomFilters();
        }
    }

    /// <summary>
    /// Disable text filter TextChanged delay
    /// </summary>
    public void SetTextFilterTextChangedDelayNodesDisabled()
    {
        _textFilterTextChangedDelayNodes = TextFilterTextChangedDelayNodesDisabled;
    }

    #endregion


    #region preset loader

    public void SetLoadedMode(bool enabled)
    {
        _customFilterMenuItem.Enabled = !enabled;
        _cancelFilterMenuItem.Enabled = enabled;
        if (enabled)
        {
            _activeFilterType = FilterType.Loaded;
            _sortString = null;
            _filterString = null;
            _customFilterLastFiltersListMenuItem.Checked = false;
            for (int i = 2; i < _customFilterLastFiltersListMenuItem.DropDownItems.Count - 1; i++)
            {
                ((_customFilterLastFiltersListMenuItem.DropDownItems[i] as ToolStripMenuItem)!).Checked = false;
            }

            ChecklistClearNodes();
            TreeNodeItemSelector allNode = TreeNodeItemSelector.CreateNode(
                $"{KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectAll)]}            ", null, CheckState.Indeterminate, TreeNodeItemSelector.CustomNodeType.SelectAll);
            allNode.NodeFont = new Font(_checkList.Font, FontStyle.Bold);
            ChecklistAddNode(allNode);
            ChecklistReloadNodes();

            SetSortEnabled(false);
            SetFilterEnabled(false);
        }
        else
        {
            _activeFilterType = FilterType.None;

            SetSortEnabled(true);
            SetFilterEnabled(true);
        }
    }

    #endregion


    #region public show methods

    /// <summary>
    /// Show the menuStrip
    /// </summary>
    /// <param name="control"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="vals"></param>
    public void Show(Control control, int x, int y, IEnumerable<DataGridViewCell> vals)
    {
        _removedNodes = [];
        _removedSessionNodes = [];

        //add nodes
        BuildNodes(vals);
        //set the starting nodes
        _startingNodes = DuplicateNodes(_loadedNodes);

        if (_activeFilterType == FilterType.Custom)
        {
            SetNodesCheckState(_loadedNodes, false);
        }

        base.Show(control, x, y);

        _filterClick = false;

        _checkTextFilterChangedEnabled = false;
        _checkTextFilter.Text = "";
        _checkTextFilterChangedEnabled = true;
    }

    /// <summary>
    /// Show the menuStrip
    /// </summary>
    /// <param name="control"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="restoreFilter"></param>
    public void Show(Control control, int x, int y, bool restoreFilter)
    {
        _checkTextFilterChangedEnabled = false;
        _checkTextFilter.Text = "";
        _checkTextFilterChangedEnabled = true;
        if (restoreFilter || _checkTextFilterRemoveNodesOnSearch)
        {
            //reset the starting nodes
            _startingNodes = DuplicateNodes(_loadedNodes);
        }
        //reset removed nodes
        if (_checkTextFilterRemoveNodesOnSearch)
        {
            _removedNodes = _loadedNodes.Where(n => n!.CheckState == CheckState.Unchecked && n.NodeType == TreeNodeItemSelector.CustomNodeType.Default).ToArray();
            _removedSessionNodes = _removedNodes;
        }

        ChecklistReloadNodes();

        base.Show(control, x, y);

        _filterClick = false;
    }

    /// <summary>
    /// Get values used for Show method
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static IEnumerable<DataGridViewCell> GetValuesForFilter(DataGridView grid, string columnName)
    {
        var vals =
            from DataGridViewRow nulls in grid.Rows
            select nulls.Cells[columnName];

        return vals;
    }

    #endregion


    #region public sort methods

    /// <summary>
    /// Sort ASC
    /// </summary>
    public void SortAsc()
    {
        SortASCMenuItem_Click(this, null);
    }

    /// <summary>
    /// Sort DESC
    /// </summary>
    public void SortDesc()
    {
        SortDESCMenuItem_Click(this, null);
    }

    /// <summary>
    /// Get the Sorting String
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SortString
    {
        get => !String.IsNullOrEmpty(_sortString) ? _sortString : "";
        private set
        {
            _cancelSortMenuItem.Enabled = value is { Length: > 0 };
            _sortString = value;
        }
    }

    /// <summary>
    /// Clean the Sorting
    /// </summary>
    public void CleanSort()
    {
        _sortAscMenuItem.Checked = false;
        _sortDescMenuItem.Checked = false;
        _activeSortType = SortType.None;
        SortString = null;
    }

    #endregion


    #region public filter methods

    /// <summary>
    /// Get the Filter string
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? FilterString
    {
        get => !String.IsNullOrEmpty(_filterString) ? _filterString : "";
        private set
        {
            _cancelFilterMenuItem.Enabled = value is { Length: > 0 };
            _filterString = value;
        }
    }

    /// <summary>
    /// Clean the Filter
    /// </summary>
    public void CleanFilter()
    {
        if (_checkTextFilterRemoveNodesOnSearch)
        {
            _removedNodes = [];
            _removedSessionNodes = [];
        }

        for (int i = 2; i < _customFilterLastFiltersListMenuItem.DropDownItems.Count - 1; i++)
        {
            ((_customFilterLastFiltersListMenuItem.DropDownItems[i] as ToolStripMenuItem)!).Checked = false;
        }
        _activeFilterType = FilterType.None;
        SetNodesCheckState(_loadedNodes, true);
        FilterString = null;
        _customFilterLastFiltersListMenuItem.Checked = false;
        _buttonFilter.Enabled = true;
    }

    /// <summary>
    /// Set the text filter on checklist remove node mode
    /// </summary>
    /// <param name="enabled"></param>
    public void SetChecklistTextFilterRemoveNodesOnSearchMode(bool enabled)
    {
        if (_checkTextFilterRemoveNodesOnSearch != enabled)
        {
            _checkTextFilterRemoveNodesOnSearch = enabled;
            CleanFilter();
        }
    }

    #endregion


    #region checklist filter methods

    /// <summary>
    /// Clear checklist loaded nodes
    /// </summary>
    private void ChecklistClearNodes()
    {
        _loadedNodes = [];
    }

    /// <summary>
    /// Add a node to checklist nodes
    /// </summary>
    /// <param name="node"></param>
    private void ChecklistAddNode(TreeNodeItemSelector node)
    {
        _loadedNodes = _loadedNodes.Concat([node]).ToArray();
    }

    /// <summary>
    /// Load checklist nodes
    /// </summary>
    private void ChecklistReloadNodes()
    {
        _checkList.BeginUpdate();
        _checkList.Nodes.Clear();
        int nodeCount = 0;
        foreach (TreeNodeItemSelector? node in _loadedNodes)
        {
            if (node!.NodeType == TreeNodeItemSelector.CustomNodeType.Default)
            {
                if (_maxChecklistNodes == 0)
                {
                    if (!_removedNodes.Contains(node))
                    {
                        _checkList.Nodes.Add(node);
                    }
                }
                else
                {
                    if (nodeCount < _maxChecklistNodes && !_removedNodes.Contains(node))
                    {
                        _checkList.Nodes.Add(node);
                    }
                    else if (nodeCount == _maxChecklistNodes)
                    {
                        _checkList.Nodes.Add("...");
                    }

                    if (!_removedNodes.Contains(node) || nodeCount == _maxChecklistNodes)
                    {
                        nodeCount++;
                    }
                }
            }
            else
            {
                _checkList.Nodes.Add(node);
            }

        }
        _checkList.EndUpdate();
    }

    /// <summary>
    /// Get checklist nodes
    /// </summary>
    /// <returns></returns>
    private TreeNodeCollection ChecklistNodes() => _checkList.Nodes;

    /// <summary>
    /// Set the Filter String using checkList selected Nodes
    /// </summary>
    private void SetCheckListFilter()
    {
        UnCheckCustomFilters();

        TreeNodeItemSelector? selectAllNode = GetSelectAllNode();
        _customFilterLastFiltersListMenuItem.Checked = false;

        if (selectAllNode is { Checked: true } && string.IsNullOrEmpty(_checkTextFilter.Text))
        {
            CancelFilterMenuItem_Click(null, EventArgs.Empty);
        }
        else
        {
            string? oldFilter = FilterString;
            FilterString = "";
            _activeFilterType = FilterType.CheckList;

            if (_loadedNodes.Length > 1)
            {
                selectAllNode = GetSelectEmptyNode();
                if (selectAllNode is { Checked: true })
                {
                    FilterString = "[{0}] IS NULL";
                }

                if (_loadedNodes.Length > 2 || selectAllNode == null)
                {
                    string filter = BuildNodesFilterString(
                        IsFilterNotinLogicEnabled && DataType != typeof(DateTime) && DataType != typeof(TimeSpan) && DataType != typeof(bool) ?
                            _loadedNodes.AsParallel().Cast<TreeNodeItemSelector>().Where(
                                n => n.NodeType != TreeNodeItemSelector.CustomNodeType.SelectAll
                                     && n.NodeType != TreeNodeItemSelector.CustomNodeType.SelectEmpty
                                     && n.CheckState == CheckState.Unchecked
                            ) :
                            _loadedNodes.AsParallel().Cast<TreeNodeItemSelector>().Where(
                                n => n.NodeType != TreeNodeItemSelector.CustomNodeType.SelectAll
                                     && n.NodeType != TreeNodeItemSelector.CustomNodeType.SelectEmpty
                                     && n.CheckState != CheckState.Unchecked
                            )
                    );

                    if (filter.Length > 0)
                    {
                        if (FilterString.Length > 0)
                        {
                            FilterString += " OR ";
                        }

                        if (DataType == typeof(bool))
                        {
                            FilterString += $"[{{0}}] ={filter}";
                        }
                        else if (DataType == typeof(int) || DataType == typeof(long) || DataType == typeof(short) ||
                                 DataType == typeof(uint) || DataType == typeof(ulong) || DataType == typeof(ushort) ||
                                 DataType == typeof(decimal) ||
                                 DataType == typeof(byte) || DataType == typeof(sbyte) || DataType == typeof(string))
                        {
                            if (IsFilterNotinLogicEnabled)
                            {
                                FilterString += $"[{{0}}] NOT IN ({filter})";
                            }
                            else
                            {
                                FilterString += $"[{{0}}] IN ({filter})";
                            }
                        }
                        else if (DataType == typeof(Bitmap))
                        { }
                        else
                        {
                            if (IsFilterNotinLogicEnabled)
                            {
                                FilterString += $"Convert([{{0}}],System.String) NOT IN ({filter})";
                            }
                            else
                            {
                                FilterString += $"Convert([{{0}}],System.String) IN ({filter})";
                            }
                        }
                    }
                }
            }

            if (oldFilter != FilterString && FilterChanged != null)
            {
                FilterChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Build a Filter string based on selected Nodes
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    private string BuildNodesFilterString(IEnumerable<TreeNodeItemSelector>? nodes)
    {
        StringBuilder sb = new StringBuilder("");

        string appx = ", ";

        var treeNodeItemSelectors = nodes as TreeNodeItemSelector[] ?? nodes?.ToArray();
        if (nodes != null && treeNodeItemSelectors!.Any())
        {
            if (DataType == typeof(DateTime))
            {
                if (treeNodeItemSelectors != null)
                {
                    foreach (TreeNodeItemSelector n in treeNodeItemSelectors)
                    {
                        if (n.Checked && !n.Nodes.AsParallel().Cast<TreeNodeItemSelector>()
                                .Any(sn => sn.CheckState != CheckState.Unchecked))
                        {
                            DateTime dt = (DateTime)n.Value!;
                            sb.Append(
                                $"'{Convert.ToString(IsFilterDateAndTimeEnabled ? dt : dt.Date, CultureInfo.CurrentCulture)}'{appx}");
                        }
                        else if (n.CheckState != CheckState.Unchecked && n.Nodes.Count > 0)
                        {
                            string subnode = BuildNodesFilterString(n.Nodes.AsParallel().Cast<TreeNodeItemSelector>()
                                .Where(sn => sn.CheckState != CheckState.Unchecked));
                            if (subnode.Length > 0)
                            {
                                sb.Append(subnode + appx);
                            }
                        }
                    }
                }
            }
            else if (DataType == typeof(TimeSpan))
            {
                if (treeNodeItemSelectors != null)
                {
                    foreach (TreeNodeItemSelector n in treeNodeItemSelectors)
                    {
                        if (n.Checked && !n.Nodes.AsParallel().Cast<TreeNodeItemSelector>()
                                .Any(sn => sn.CheckState != CheckState.Unchecked))
                        {
                            TimeSpan ts = (TimeSpan)n.Value!;
                            sb.Append(
                                $"'P{(ts.Days > 0 ? $"{ts.Days}D" : "")}{(ts.TotalHours > 0 ? "T" : "")}{(ts.Hours > 0 ? $"{ts.Hours}H" : "")}{(ts.Minutes > 0 ? $"{ts.Minutes}M" : "")}{(ts.Seconds > 0 ? $"{ts.Seconds}S" : "")}'{appx}");
                        }
                        else if (n.CheckState != CheckState.Unchecked && n.Nodes.Count > 0)
                        {
                            string subnode = BuildNodesFilterString(n.Nodes.AsParallel().Cast<TreeNodeItemSelector>()
                                .Where(sn => sn.CheckState != CheckState.Unchecked));
                            if (subnode.Length > 0)
                            {
                                sb.Append(subnode + appx);
                            }
                        }
                    }
                }
            }
            else if (DataType == typeof(bool))
            {
                if (treeNodeItemSelectors != null)
                {
                    foreach (TreeNodeItemSelector n in treeNodeItemSelectors)
                    {
                        sb.Append(n.Value);
                        break;
                    }
                }
            }
            else if (DataType == typeof(int) || DataType == typeof(long) || DataType == typeof(short) ||
                     DataType == typeof(uint) || DataType == typeof(ulong) || DataType == typeof(ushort) ||
                     DataType == typeof(byte) || DataType == typeof(sbyte))
            {
                if (treeNodeItemSelectors != null)
                {
                    foreach (TreeNodeItemSelector n in treeNodeItemSelectors)
                    {
                        sb.Append(n.Value + appx);
                    }
                }
            }
            else if (DataType == typeof(float) || DataType == typeof(double) || DataType == typeof(decimal))
            {
                if (treeNodeItemSelectors != null)
                {
                    foreach (TreeNodeItemSelector n in treeNodeItemSelectors)
                    {
                        sb.Append((n.Value?.ToString() ?? string.Empty).Replace(",", ".") + appx);
                    }
                }
            }
            else if (DataType == typeof(Bitmap))
            { }
            else
            {
                if (treeNodeItemSelectors != null)
                {
                    foreach (TreeNodeItemSelector n in treeNodeItemSelectors)
                    {
                        sb.Append($"'{FormatFilterString(n.Value?.ToString()!)}'{appx}");
                    }
                }
            }
        }

        if (sb.Length > appx.Length && DataType != typeof(bool))
        {
            sb.Remove(sb.Length - appx.Length, appx.Length);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Format a text Filter string
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string FormatFilterString(string text)
    {
        return text.Replace("'", "''");
    }

    /// <summary>
    /// Add nodes to checkList
    /// </summary>
    /// <param name="vals"></param>
    private void BuildNodes(IEnumerable<DataGridViewCell>? vals)
    {
        if (!IsFilterChecklistEnabled)
        {
            return;
        }

        ChecklistClearNodes();

        if (vals != null)
        {
            //add select all node
            TreeNodeItemSelector allNode = TreeNodeItemSelector.CreateNode(
                $"{KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectAll)]}            ", null, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.SelectAll);
            allNode.NodeFont = new Font(_checkList.Font, FontStyle.Bold);
            ChecklistAddNode(allNode);

            if (vals.Any())
            {
                var noNulls = vals.Where<DataGridViewCell>(c => c.Value != null && c.Value != DBNull.Value);

                //add select empty node
                IEnumerable<DataGridViewCell> dataGridViewCells = noNulls as DataGridViewCell[] ?? noNulls.ToArray();
                if (vals.Count() != dataGridViewCells.Count())
                {
                    TreeNodeItemSelector nullNode = TreeNodeItemSelector.CreateNode(
                        $"{KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectEmpty)]}               ", null, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.SelectEmpty);
                    nullNode.NodeFont = new Font(_checkList.Font, FontStyle.Bold);
                    ChecklistAddNode(nullNode);
                }

                //add datetime nodes
                if (DataType == typeof(DateTime))
                {
                    var years =
                        from year in dataGridViewCells
                        group year by ((DateTime)year.Value!).Year into cy
                        orderby cy.Key ascending
                        select cy;

                    foreach (var year in years)
                    {
                        TreeNodeItemSelector yearNode = TreeNodeItemSelector.CreateNode(year.Key.ToString(), year.Key, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.DateTimeNode);
                        ChecklistAddNode(yearNode);

                        var months =
                            from month in year
                            group month by ((DateTime)month.Value!).Month into cm
                            orderby cm.Key ascending
                            select cm;

                        foreach (var month in months)
                        {
                            TreeNodeItemSelector? monthNode = yearNode.CreateChildNode(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Key), month.Key);

                            var days =
                                from day in month
                                group day by ((DateTime)day.Value!).Day into cd
                                orderby cd.Key ascending
                                select cd;

                            foreach (var day in days)
                            {
                                TreeNodeItemSelector? daysNode;

                                if (!IsFilterDateAndTimeEnabled)
                                {
                                    daysNode = monthNode?.CreateChildNode(day.Key.ToString("D2"), day.First().Value);
                                }
                                else
                                {
                                    daysNode = monthNode?.CreateChildNode(day.Key.ToString("D2"), day.Key);

                                    var hours =
                                        from hour in day
                                        group hour by ((DateTime)hour.Value!).Hour into ch
                                        orderby ch.Key ascending
                                        select ch;

                                    foreach (var hour in hours)
                                    {
                                        TreeNodeItemSelector? hoursNode = daysNode?.CreateChildNode(
                                            $"{hour.Key:D2} h", hour.Key);

                                        var mins =
                                            from min in hour
                                            group min by ((DateTime)min.Value!).Minute into cmin
                                            orderby cmin.Key ascending
                                            select cmin;

                                        foreach (var min in mins)
                                        {
                                            TreeNodeItemSelector? minsNode = hoursNode?.CreateChildNode(
                                                $"{min.Key:D2} m", min.Key);

                                            var secs =
                                                from sec in min
                                                group sec by ((DateTime)sec.Value!).Second into cs
                                                orderby cs.Key ascending
                                                select cs;

                                            foreach (var sec in secs)
                                            {
                                                TreeNodeItemSelector? secsNode = minsNode?.CreateChildNode(
                                                    $"{sec.Key:D2} s", sec.First().Value);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //add timespan nodes
                else if (DataType == typeof(TimeSpan))
                {
                    var days =
                        from day in dataGridViewCells
                        group day by ((TimeSpan)day.Value!).Days into cd
                        orderby cd.Key ascending
                        select cd;

                    foreach (var day in days)
                    {
                        TreeNodeItemSelector daysNode = TreeNodeItemSelector.CreateNode(day.Key.ToString("D2"), day.Key, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.DateTimeNode);
                        ChecklistAddNode(daysNode);

                        var hours =
                            from hour in day
                            group hour by ((TimeSpan)hour.Value!).Hours into ch
                            orderby ch.Key ascending
                            select ch;

                        foreach (var hour in hours)
                        {
                            TreeNodeItemSelector? hoursNode = daysNode.CreateChildNode($"{hour.Key:D2} h", hour.Key);

                            var mins =
                                from min in hour
                                group min by ((TimeSpan)min.Value!).Minutes into cmin
                                orderby cmin.Key ascending
                                select cmin;

                            foreach (var min in mins)
                            {
                                TreeNodeItemSelector? minsNode = hoursNode?.CreateChildNode($"{min.Key:D2} m", min.Key);

                                var secs =
                                    from sec in min
                                    group sec by ((TimeSpan)sec.Value!).Seconds into cs
                                    orderby cs.Key ascending
                                    select cs;

                                foreach (var sec in secs)
                                {
                                    TreeNodeItemSelector? secsNode = minsNode?.CreateChildNode($"{sec.Key:D2} s", sec.First().Value);
                                }
                            }
                        }
                    }
                }

                //add boolean nodes
                else if (DataType == typeof(bool))
                {
                    var values = dataGridViewCells.Where<DataGridViewCell>(c => c.Value is true);

                    var gridViewCells = values as DataGridViewCell[] ?? values.ToArray();
                    if (gridViewCells.Count() != dataGridViewCells.Count())
                    {
                        TreeNodeItemSelector node = TreeNodeItemSelector.CreateNode(KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectFalse)], false, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.Default);
                        ChecklistAddNode(node);
                    }

                    if (gridViewCells.Any())
                    {
                        TreeNodeItemSelector node = TreeNodeItemSelector.CreateNode(KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectTrue)], true, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.Default);
                        ChecklistAddNode(node);
                    }
                }

                //ignore image nodes
                else if (DataType == typeof(Bitmap))
                { }

                //add string nodes
                else
                {
                    foreach (var v in dataGridViewCells.GroupBy(c => c.Value).OrderBy(g => g.Key))
                    {
                        TreeNodeItemSelector node = TreeNodeItemSelector.CreateNode(v.First().FormattedValue?.ToString(), v.Key, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.Default);
                        ChecklistAddNode(node);
                    }
                }
            }
        }

        ChecklistReloadNodes();
    }

    /// <summary>
    /// Check if filter buttons needs to be enabled
    /// </summary>
    private void CheckFilterButtonEnabled()
    {
        _buttonFilter.Enabled = HasNodesChecked(_loadedNodes);
    }

    /// <summary>
    /// Check if selected nodes exists
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    private bool HasNodesChecked(TreeNodeItemSelector?[] nodes)
    {
        bool state = false;
        state = !string.IsNullOrEmpty(_checkTextFilter.Text) ? nodes.Any(n => n!.CheckState == CheckState.Checked && n.Text.ToLower().Contains(_checkTextFilter.Text.ToLower())) : nodes.Any(n => n!.CheckState == CheckState.Checked);

        if (state)
        {
            return state;
        }

        foreach (TreeNodeItemSelector? node in nodes)
        {
            foreach (TreeNodeItemSelector nodesel in node!.Nodes)
            {
                state = HasNodesChecked([nodesel]);
                if (state)
                {
                    break;
                }
            }
            if (state)
            {
                break;
            }
        }

        return state;
    }

    /// <summary>
    /// Check
    /// </summary>
    /// <param name="node"></param>
    private void NodeCheckChange(TreeNodeItemSelector? node)
    {
        if (node != null)
        {
            node.CheckState = node.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;

            if (node.NodeType == TreeNodeItemSelector.CustomNodeType.SelectAll)
            {
                SetNodesCheckState(_loadedNodes, node.Checked);
            }
            else
            {
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNodeItemSelector subnode in node.Nodes)
                    {
                        SetNodesCheckState([subnode], node.Checked);
                    }
                }

                //refresh nodes
                CheckState state = UpdateNodesCheckState(ChecklistNodes());
                GetSelectAllNode()!.CheckState = state;
            }
        }
    }

    /// <summary>
    /// Set Nodes CheckState
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="isChecked"></param>
    private void SetNodesCheckState(TreeNodeItemSelector?[] nodes, bool isChecked)
    {
        foreach (TreeNodeItemSelector? node in nodes)
        {
            node!.Checked = isChecked;
            if (node.Nodes is { Count: > 0 })
            {
                foreach (TreeNodeItemSelector subnode in node.Nodes)
                {
                    SetNodesCheckState([subnode], isChecked);
                }
            }

        }
    }

    /// <summary>
    /// Update Nodes CheckState recursively
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    private CheckState UpdateNodesCheckState(TreeNodeCollection nodes)
    {
        CheckState result = CheckState.Unchecked;
        bool isFirstNode = true;
        bool isAllNodesSomeCheckState = true;

        foreach (TreeNodeItemSelector n in nodes.OfType<TreeNodeItemSelector>())
        {
            if (n.NodeType == TreeNodeItemSelector.CustomNodeType.SelectAll)
            {
                continue;
            }

            if (n.Nodes.Count > 0)
            {
                n.CheckState = UpdateNodesCheckState(n.Nodes);
            }

            if (isFirstNode)
            {
                result = n.CheckState;
                isFirstNode = false;
            }
            else
            {
                if (result != n.CheckState)
                {
                    isAllNodesSomeCheckState = false;
                }
            }
        }

        return isAllNodesSomeCheckState ? result : CheckState.Indeterminate;
    }

    /// <summary>
    /// Get the SelectAll Node
    /// </summary>
    /// <returns></returns>
    private TreeNodeItemSelector? GetSelectAllNode()
    {
        TreeNodeItemSelector? result = null;
        int i = 0;
        foreach (TreeNodeItemSelector? n in ChecklistNodes().OfType<TreeNodeItemSelector>())
        {
            if (n.NodeType == TreeNodeItemSelector.CustomNodeType.SelectAll)
            {
                result = n;
                break;
            }
            else if (i > 2)
            {
                break;
            }
            else
            {
                i++;
            }
        }

        return result;
    }

    /// <summary>
    /// Get the SelectEmpty Node
    /// </summary>
    /// <returns></returns>
    private TreeNodeItemSelector? GetSelectEmptyNode()
    {
        TreeNodeItemSelector? result = null;
        int i = 0;
        foreach (TreeNodeItemSelector? n in ChecklistNodes().OfType<TreeNodeItemSelector>())
        {
            if (n.NodeType == TreeNodeItemSelector.CustomNodeType.SelectEmpty)
            {
                result = n;
                break;
            }
            else if (i > 2)
            {
                break;
            }
            else
            {
                i++;
            }
        }

        return result;
    }

    /// <summary>
    /// Duplicate Nodes
    /// </summary>
    private static TreeNodeItemSelector?[] DuplicateNodes(TreeNodeItemSelector?[] nodes)
    {
        TreeNodeItemSelector?[] ret = new TreeNodeItemSelector[nodes.Length];
        int i = 0;
        foreach (TreeNodeItemSelector? n in nodes)
        {
            ret[i] = n?.Clone();
            i++;
        }
        return ret;
    }

    #endregion


    #region checklist filter events

    /// <summary>
    /// CheckList NodeMouseClick event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckList_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
    {
        TreeViewHitTestInfo hitTestInfo = _checkList.HitTest(e.X, e.Y);
        if (hitTestInfo is { Location: TreeViewHitTestLocations.StateImage })
        {
            //check the node check status
            NodeCheckChange(e.Node as TreeNodeItemSelector);
            //set filter button enabled
            CheckFilterButtonEnabled();
        }
    }

    /// <summary>
    /// CheckList KeyDown event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckList_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            //check the node check status
            NodeCheckChange(_checkList.SelectedNode as TreeNodeItemSelector);
            //set filter button enabled
            CheckFilterButtonEnabled();
        }
    }

    /// <summary>
    /// CheckList NodeMouseDoubleClick event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckList_NodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e)
    {
        TreeNodeItemSelector? n = e.Node as TreeNodeItemSelector;
        //set the new node check status
        SetNodesCheckState(_loadedNodes, false);
        n!.CheckState = CheckState.Unchecked;
        NodeCheckChange(n);
        //set filter button enabled
        CheckFilterButtonEnabled();
        //do Filter by checkList
        Button_ok_Click(this, EventArgs.Empty);
    }

    /// <summary>
    /// CheckList MouseEnter event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckList_MouseEnter(object? sender, EventArgs e)
    {
        _checkList.Focus();
    }

    /// <summary>
    /// CheckList MouseLeave event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckList_MouseLeave(object? sender, EventArgs e)
    {
        Focus();
    }

    /// <summary>
    /// Set the Filter by checkList
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_ok_Click(object? sender, EventArgs e)
    {
        _filterClick = true;

        SetCheckListFilter();
        Close();
    }

    /// <summary>
    /// Undo changed by checkList 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_cancel_Click(object? sender, EventArgs e)
    {
        _loadedNodes = DuplicateNodes(_startingNodes);
        Close();
    }

    #endregion


    #region filter methods

    /// <summary>
    /// UnCheck all Custom Filter presets
    /// </summary>
    private void UnCheckCustomFilters()
    {
        for (int i = 2; i < _customFilterLastFiltersListMenuItem.DropDownItems.Count; i++)
        {
            ((_customFilterLastFiltersListMenuItem.DropDownItems[i] as ToolStripMenuItem)!).Checked = false;
        }
    }

    /// <summary>
    /// Set a Custom Filter
    /// </summary>
    /// <param name="filtersMenuItemIndex"></param>
    private void SetCustomFilter(int filtersMenuItemIndex)
    {
        if (_activeFilterType == FilterType.CheckList)
        {
            SetNodesCheckState(_loadedNodes, false);
        }

        ToolStripItem presetItem = _customFilterLastFiltersListMenuItem.DropDownItems[filtersMenuItemIndex];
        string filterString = presetItem.Tag?.ToString() ?? string.Empty;
        string viewFilterString = presetItem.Text ?? string.Empty;

        //do preset jobs
        if (filtersMenuItemIndex != 2)
        {
            for (var i = filtersMenuItemIndex; i > 2; i--)
            {
                _customFilterLastFiltersListMenuItem.DropDownItems[i].Text = _customFilterLastFiltersListMenuItem.DropDownItems[i - 1].Text;
                _customFilterLastFiltersListMenuItem.DropDownItems[i].Tag = _customFilterLastFiltersListMenuItem.DropDownItems[i - 1].Tag;
            }

            _customFilterLastFiltersListMenuItem.DropDownItems[2].Text = viewFilterString;
            _customFilterLastFiltersListMenuItem.DropDownItems[2].Tag = filterString;
        }

        // uncheck other preset
        for (var i = 3; i < _customFilterLastFiltersListMenuItem.DropDownItems.Count; i++)
        {
            ((_customFilterLastFiltersListMenuItem.DropDownItems[i] as ToolStripMenuItem)!).Checked = false;
        }

        ((_customFilterLastFiltersListMenuItem.DropDownItems[2] as ToolStripMenuItem)!).Checked = true;
        _activeFilterType = FilterType.Custom;

        //get Filter string
        string? oldFilter = FilterString;
        FilterString = filterString;

        //set CheckList nodes
        SetNodesCheckState(_loadedNodes, false);

        _customFilterLastFiltersListMenuItem.Checked = true;
        _buttonFilter.Enabled = false;

        //fire Filter changed
        if (oldFilter != FilterString && FilterChanged != null)
        {
            FilterChanged(this, EventArgs.Empty);
        }
    }

    #endregion


    #region filter events

    /// <summary>
    /// Cancel Filter Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelFilterMenuItem_Click(object? sender, EventArgs e)
    {
        string? oldFilter = FilterString;

        //clean Filter
        CleanFilter();

        //fire Filter changed
        if (oldFilter != FilterString && FilterChanged != null)
        {
            FilterChanged(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Cancel Filter MouseEnter event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelFilterMenuItem_MouseEnter(object? sender, EventArgs e)
    {
        if (((sender as ToolStripMenuItem)!).Enabled)
        {
            ((sender as ToolStripMenuItem)!).Select();
        }
    }

    /// <summary>
    /// Custom Filter Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomFilterMenuItem_Click(object? sender, EventArgs e)
    {
        //ignore image nodes
        if (DataType == typeof(Bitmap))
        {
            return;
        }

        //open a new Custom filter window
        VisualCustomFilterForm flt = new VisualCustomFilterForm(DataType, IsFilterDateAndTimeEnabled);

        if (flt.ShowDialog() == DialogResult.OK)
        {
            //add the new Filter presets

            string? filterString = flt.FilterString;
            string? viewFilterString = flt.FilterStringDescription;

            int index = -1;

            for (int i = 2; i < _customFilterLastFiltersListMenuItem.DropDownItems.Count; i++)
            {
                if (_customFilterLastFiltersListMenuItem.DropDown.Items[i].Available)
                {
                    ToolStripItem presetRow = _customFilterLastFiltersListMenuItem.DropDownItems[i];
                    if (string.Equals(presetRow.Text, viewFilterString, StringComparison.Ordinal)
                        && string.Equals(presetRow.Tag?.ToString(), filterString, StringComparison.Ordinal))
                    {
                        index = i;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (index < 2)
            {
                for (int i = _customFilterLastFiltersListMenuItem.DropDownItems.Count - 2; i > 1; i--)
                {
                    if (_customFilterLastFiltersListMenuItem.DropDownItems[i].Available)
                    {
                        _customFilterLastFiltersListMenuItem.DropDownItems[i + 1].Text = _customFilterLastFiltersListMenuItem.DropDownItems[i].Text;
                        _customFilterLastFiltersListMenuItem.DropDownItems[i + 1].Tag = _customFilterLastFiltersListMenuItem.DropDownItems[i].Tag;
                    }
                }
                index = 2;

                _customFilterLastFiltersListMenuItem.DropDownItems[2].Text = viewFilterString;
                _customFilterLastFiltersListMenuItem.DropDownItems[2].Tag = filterString;
            }

            //set the Custom Filter
            SetCustomFilter(index);
        }
    }

    /// <summary>
    /// Custom Filter preset MouseEnter event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomFilterLastFiltersListMenuItem_MouseEnter(object? sender, EventArgs e)
    {
        if (((sender as ToolStripMenuItem)!).Enabled)
        {
            ((sender as ToolStripMenuItem)!).Select();
        }
    }

    /// <summary>
    /// Custom Filter preset MouseEnter event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomFilterLastFiltersListMenuItem_Paint(object? sender, PaintEventArgs e)
    {
        Rectangle rect = new Rectangle(_customFilterLastFiltersListMenuItem.Width - 12, 7, 10, 10);
        ControlPaint.DrawMenuGlyph(e.Graphics, rect, MenuGlyph.Arrow, Color.Black, Color.Transparent);
    }

    /// <summary>
    /// Custom Filter preset 1 Visibility changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomFilterLastFilter1MenuItem_VisibleChanged(object? sender, EventArgs e)
    {
        _toolStripSeparator2MenuItem.Visible = !_customFilterLastFilter1MenuItem.Visible;
        ((sender as ToolStripMenuItem)!).VisibleChanged -= CustomFilterLastFilter1MenuItem_VisibleChanged;
    }

    /// <summary>
    /// Custom Filter preset Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomFilterLastFilterMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is not ToolStripMenuItem menuitem)
        {
            return;
        }

        for (int i = 2; i < _customFilterLastFiltersListMenuItem.DropDownItems.Count; i++)
        {
            ToolStripItem presetRow = _customFilterLastFiltersListMenuItem.DropDownItems[i];
            if (string.Equals(presetRow.Text, menuitem.Text, StringComparison.Ordinal)
                && string.Equals(presetRow.Tag?.ToString(), menuitem.Tag?.ToString(), StringComparison.Ordinal))
            {
                //set current filter preset as active
                SetCustomFilter(i);
                break;
            }
        }
    }

    /// <summary>
    /// Custom Filter preset TextChanged event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomFilterLastFilterMenuItem_TextChanged(object? sender, EventArgs e)
    {
        ((sender as ToolStripMenuItem)!).Available = true;
        ((sender as ToolStripMenuItem)!).TextChanged -= CustomFilterLastFilterMenuItem_TextChanged;
    }

    /// <summary>
    /// Text changed timer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckTextFilterTextChangedTimer_Tick(object? sender, EventArgs e)
    {
        if (sender is not Timer timer)
        {
            return;
        }

        CheckTextFilterHandleTextChanged(timer.Tag?.ToString() ?? string.Empty);

        timer.Stop();
    }

    /// <summary>
    /// Check list filter changer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckTextFilter_TextChanged(object? sender, EventArgs e)
    {
        if (!_checkTextFilterChangedEnabled)
        {
            return;
        }

        if (_textFilterTextChangedDelayNodes != TextFilterTextChangedDelayNodesDisabled && _loadedNodes.Length > _textFilterTextChangedDelayNodes)
        {
            if (_textFilterTextChangedTimer == null)
            {
                _textFilterTextChangedTimer = new Timer();
                _textFilterTextChangedTimer.Tick += new EventHandler(CheckTextFilterTextChangedTimer_Tick);
            }
            _textFilterTextChangedTimer.Stop();
            _textFilterTextChangedTimer.Interval = _textFilterTextChangedDelayMs;
            _textFilterTextChangedTimer.Tag = _checkTextFilter.Text.ToLower();
            _textFilterTextChangedTimer.Start();
        }
        else
        {
            CheckTextFilterHandleTextChanged(_checkTextFilter.Text.ToLower());
        }
    }

    /// <summary>
    /// Handle check filter text changed
    /// </summary>
    /// <param name="text"></param>
    private void CheckTextFilterHandleTextChanged(string text)
    {
        TreeNodeItemSelector allNode = TreeNodeItemSelector.CreateNode(
            $"{KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectAll)]}            ", null, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.SelectAll);
        TreeNodeItemSelector nullNode = TreeNodeItemSelector.CreateNode(
            $"{KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectEmpty)]}               ", null, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.SelectEmpty);
        string?[] removedNodesText = [];
        if (_checkTextFilterRemoveNodesOnSearch)
        {
            removedNodesText = _removedSessionNodes.Where(r => !string.IsNullOrEmpty(r?.Text)).Select(r => r?.Text.ToLower()).Distinct().ToArray();
        }
        for (int i = _loadedNodes.Length - 1; i >= 0; i--)
        {
            TreeNodeItemSelector? node = _loadedNodes[i];
            if (node?.Text == allNode.Text)
            {
                node.CheckState = CheckState.Indeterminate;
            }
            else if (node?.Text == nullNode.Text)
            {
                node.CheckState = CheckState.Unchecked;
            }
            else
            {
                if (node != null)
                {
                    node.CheckState = node.Text.ToLower().Contains(text)
                        ? CheckState.Unchecked
                        : CheckState.Checked;

                    if (removedNodesText.Contains(node.Text.ToLower()))
                    {
                        node.CheckState = CheckState.Checked;
                    }

                    NodeCheckChange(node);
                }
            }
        }
        //set filter button enabled
        CheckFilterButtonEnabled();
        _removedNodes = _removedSessionNodes;
        if (_checkTextFilterRemoveNodesOnSearch)
        {
            for (int i = _loadedNodes.Length - 1; i >= 0; i--)
            {
                TreeNodeItemSelector? node = _loadedNodes[i];
                if (!(node?.Text == allNode.Text || node?.Text == nullNode.Text))
                {
                    if (!node!.Text.ToLower().Contains(text))
                    {
                        _removedNodes = _removedNodes.Concat([node]).ToArray();
                    }
                }
            }
            ChecklistReloadNodes();
        }
    }

    #endregion


    #region sort events

    /// <summary>
    /// Sort ASC Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SortASCMenuItem_Click(object? sender, EventArgs? e)
    {
        //ignore image nodes
        if (DataType == typeof(Bitmap))
        {
            return;
        }

        _sortAscMenuItem.Checked = true;
        _sortDescMenuItem.Checked = false;
        _activeSortType = SortType.Asc;

        //get Sort String
        string? oldSort = SortString;
        SortString = "[{0}] ASC";

        //fire Sort Changed
        if (oldSort != SortString && SortChanged != null)
        {
            SortChanged(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Sort ASC MouseEnter event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SortASCMenuItem_MouseEnter(object? sender, EventArgs e)
    {
        if (((sender as ToolStripMenuItem)!).Enabled)
        {
            ((ToolStripMenuItem)sender).Select();
        }
    }

    /// <summary>
    /// Sort DESC Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SortDESCMenuItem_Click(object? sender, EventArgs? e)
    {
        //ignore image nodes
        if (DataType == typeof(Bitmap))
        {
            return;
        }

        _sortAscMenuItem.Checked = false;
        _sortDescMenuItem.Checked = true;
        _activeSortType = SortType.Desc;

        //get Sort String
        string? oldSort = SortString;
        SortString = "[{0}] DESC";

        //fire Sort Changed
        if (oldSort != SortString && SortChanged != null)
        {
            SortChanged(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Sort DESC MouseEnter event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SortDESCMenuItem_MouseEnter(object? sender, EventArgs e)
    {
        if (((sender as ToolStripMenuItem)!).Enabled)
        {
            ((ToolStripMenuItem)sender).Select();
        }
    }

    /// <summary>
    /// Cancel Sort Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelSortMenuItem_Click(object? sender, EventArgs e)
    {
        string? oldSort = SortString;
        //clean Sort
        CleanSort();
        //fire Sort changed
        if (oldSort != SortString && SortChanged != null)
        {
            SortChanged(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Cancel Sort MouseEnter event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelSortMenuItem_MouseEnter(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menuItem && menuItem.Enabled)
        {
            menuItem.Select();
        }
    }

    #endregion


    #region resize methods

    /// <summary>
    /// Get the scaling factor
    /// </summary>
    /// <returns></returns>
    private float GetScalingFactor()
    {
        float ret = 1;
        using (Graphics gScale = CreateGraphics())
        {
            try
            {
                ret = gScale.DpiX / 96.0F;
            }
            catch (Exception e)
            {
                KryptonExceptionHandler.CaptureException(e);
            }
        }
        return ret;
    }

    /// <summary>
    /// Scale an item
    /// </summary>
    /// <param name="dimension"></param>
    /// <param name="factor"></param>
    /// <returns></returns>
    private static int Scale(int dimension, float factor)
    {
        return (int)Math.Floor(dimension * factor);
    }

    /// <summary>
    /// Resize the box
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// 
    private void ResizeBox(int w, int h)
    {
        _sortAscMenuItem.Width = w - 1;
        _sortDescMenuItem.Width = w - 1;
        _cancelSortMenuItem.Width = w - 1;
        _cancelFilterMenuItem.Width = w - 1;
        _customFilterMenuItem.Width = w - 1;
        _customFilterLastFiltersListMenuItem.Width = w - 1;
        _checkTextFilterControlHost.Width = w - 35;

        //scale objects using original width and height
        float scalingFactor = GetScalingFactor();
        int w2 = (int)Math.Round(w / scalingFactor, 0);
        int h2 = (int)Math.Round(h / scalingFactor, 0);
        _checkFilterListControlHost.Size = new Size(Scale(w2 - 35, scalingFactor), Scale(h2 - 160 - 25, scalingFactor));
        _checkFilterListPanel.Size = _checkFilterListControlHost.Size;
        _checkList.Bounds = new Rectangle(Scale(4, scalingFactor), Scale(4, scalingFactor), Scale(w2 - 35 - 8, scalingFactor), Scale(h2 - 160 - 25 - 8, scalingFactor));
        _checkFilterListButtonsControlHost.Size = new Size(Scale(w2 - 35, scalingFactor), Scale(24, scalingFactor));
        _buttonFilter.Location = new Point(Scale(w2 - 35 - 164, scalingFactor), 0);
        _buttonUndofilter.Location = new Point(Scale(w2 - 35 - 79, scalingFactor), 0);
        _resizeBoxControlHost.Margin = new Padding(Scale(w2 - 46, scalingFactor), 0, 0, 0);

        //get all objects height to make sure we have room for the grip
        int finalHeight =
            _sortAscMenuItem.Height +
            _sortDescMenuItem.Height +
            _cancelSortMenuItem.Height +
            _cancelFilterMenuItem.Height +
            _toolStripSeparator1MenuItem.Height +
            _toolStripSeparator2MenuItem.Height +
            _customFilterLastFiltersListMenuItem.Height +
            _toolStripSeparator3MenuItem.Height +
            _checkFilterListControlHost.Height +
            _checkTextFilterControlHost.Height +
            _checkFilterListButtonsControlHost.Height +
            _resizeBoxControlHost.Height;

        // apply the needed height only when scaled
        Size = Math.Abs(scalingFactor - 1) < 1 ? new Size(w, h) : new Size(w, h + (finalHeight - h < 0 ? 0 : finalHeight - h));
    }

    /// <summary>
    /// Clean box for Resize
    /// </summary>
    private void ResizeClean()
    {
        if (_resizeEndPoint.X != -1)
        {
            Point startPoint = PointToScreen(_resizeStartPoint);

            Rectangle rc = new Rectangle(startPoint.X, startPoint.Y, _resizeEndPoint.X, _resizeEndPoint.Y)
            {
                X = Math.Min(startPoint.X, _resizeEndPoint.X),
                Width = Math.Abs(startPoint.X - _resizeEndPoint.X),

                Y = Math.Min(startPoint.Y, _resizeEndPoint.Y),
                Height = Math.Abs(startPoint.Y - _resizeEndPoint.Y)
            };

            ControlPaint.DrawReversibleFrame(rc, Color.Black, FrameStyle.Dashed);

            _resizeEndPoint.X = -1;
        }
    }

    #endregion


    #region resize events

    /// <summary>
    /// Resize MouseDown event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResizeBoxControlHost_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ResizeClean();
        }
    }

    /// <summary>
    /// Resize MouseMove event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResizeBoxControlHost_MouseMove(object? sender, MouseEventArgs e)
    {
        if (Visible)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y;

                ResizeClean();

                x += Width - _resizeBoxControlHost.Width;
                y += Height - _resizeBoxControlHost.Height;

                x = Math.Max(x, MinimumSize.Width - 1);
                y = Math.Max(y, MinimumSize.Height - 1);

                Point startPoint = PointToScreen(_resizeStartPoint);
                Point endPoint = PointToScreen(new Point(x, y));

                Rectangle rc = new Rectangle
                {
                    X = Math.Min(startPoint.X, endPoint.X),
                    Width = Math.Abs(startPoint.X - endPoint.X),

                    Y = Math.Min(startPoint.Y, endPoint.Y),
                    Height = Math.Abs(startPoint.Y - endPoint.Y)
                };

                ControlPaint.DrawReversibleFrame(rc, Color.Black, FrameStyle.Dashed);

                _resizeEndPoint.X = endPoint.X;
                _resizeEndPoint.Y = endPoint.Y;
            }
        }
    }

    /// <summary>
    /// Resize MouseUp event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResizeBoxControlHost_MouseUp(object? sender, MouseEventArgs e)
    {
        if (_resizeEndPoint.X != -1)
        {
            ResizeClean();

            if (Visible)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int newWidth = e.X + Width - _resizeBoxControlHost.Width;
                    int newHeight = e.Y + Height - _resizeBoxControlHost.Height;

                    newWidth = Math.Max(newWidth, MinimumSize.Width);
                    newHeight = Math.Max(newHeight, MinimumSize.Height);

                    ResizeBox(newWidth, newHeight);
                }
            }
        }
    }

    /// <summary>
    /// Resize Paint event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResizeBoxControlHost_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.DrawImage(Properties.Resources.MenuStrip_ResizeGrip, 0, 0);
    }

    #endregion

}
