#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Display text and images with the styling features of the Krypton Toolkit
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonDataGridView), "ToolboxBitmaps.KryptonDataGridView.bmp")]
[DesignerCategory(@"Code")]
//[Designer(typeof(KryptonDataGridViewDesigner))] do not use for now. use the the winforms editor
[Designer($"System.Windows.Forms.Design.DataGridViewDesigner")]
[DefaultEvent(nameof(CellContentClick))]
[ComplexBindingProperties(nameof(DataSource), nameof(DataMember))]
[Docking(DockingBehavior.Ask)]
[Description(@"Display rows and columns of data of a grid you can customize.")]
public class KryptonDataGridView : DataGridView
{
    #region Type Declaractions
    private class ColumnHeaderCache : Dictionary<int, bool>;
    private class RowHeaderCache : Dictionary<int, Rectangle>;
    #endregion

    #region Classes
    private class ToolTipContent : IContentValues
    {
        #region Instance Fields
        private readonly string _toolTipText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ToolTipContent class.
        /// </summary>
        /// <param name="toolTipText">Text to show as a tooltip.</param>
        public ToolTipContent(string toolTipText) => _toolTipText = toolTipText;

        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image? GetImage(PaletteState state) => null;

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText() => _toolTipText;

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText() => string.Empty;

        #endregion
    }
    #endregion

    #region Static Fields
    private static readonly Point _nullCell = new Point(-2, -2);

    // Cached access to private parent values
    private static PropertyInfo _piRTL;
    private static PropertyInfo _piCML;
    private static MethodInfo _miPTB;
    private static MethodInfo _miGCI;
    private static MethodInfo _miGTTT;
    private static MethodInfo _miGET;
    private static MethodInfo _miATT;
    private static MethodInfo _miGPW;
    private static MethodInfo _miGPH;
    #endregion

    #region Instance Fields
    // Standard Krypton layout/rendering fields
    private bool _refresh;
    private bool _refreshAll;
    private bool _layoutDirty;
    private bool _paintTransparent;
    private bool _evalTransparent;
    private Size _lastLayoutSize;
    private PaletteBase? _localPalette;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private ViewDrawPanel _drawPanel;
    private SimpleCall _refreshCall;

    // States and redirector

    // Cached values for determining cell style overrides
    private Font? _columnFont;
    private Font? _rowFont;
    private Font? _dataCellFont;
    private Padding _columnPadding;
    private Padding _rowPadding;
    private Padding _dataCellPadding;
    private DataGridViewContentAlignment _columnAlign;
    private DataGridViewContentAlignment _rowAlign;
    private DataGridViewContentAlignment _dataCellAlign;
    private Color _columnBackColor, _rowBackColor, _dataCellBackColor;
    private Color _columnForeColor, _rowForeColor, _dataCellForeColor;
    private Color _columnSelBackColor, _rowSelBackColor, _dataCellSelBackColor;
    private Color _columnSelForeColor, _rowSelForeColor, _dataCellSelForeColor;

    // Implementation fields
    private ShortTextValue _shortTextValue;
    private VisualPopupToolTip? _visualPopupToolTip;
    private PaletteBorderInheritForced _borderForced;
    private PaletteDataGridViewBackInherit _backInherit;
    private PaletteDataGridViewContentInherit _contentInherit;
    private ColumnHeaderCache _columnCache;
    private RowHeaderCache _rowCache;
    private Point _cellOver;
    private Point _cellDown;
    private System.Windows.Forms.Timer? _showTimer;
    private bool _hideOuterBorders;
    private string _toolTipText;
    private byte _oldLocation;
    private DataGridViewCell? _oldCell;
    private KryptonContextMenu? _kryptonContextMenu;

    //Seb
    private string _searchString;
    private List<int> _restrictColumnsSearch;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the palette changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the Palette property is changed.")]
    public event EventHandler? PaletteChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridView class.
    /// </summary>
    public KryptonDataGridView()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint, true);

        // We need to allow a transparent background
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        // We need to repaint entire control whenever resized
        SetStyle(ControlStyles.ResizeRedraw, true);

        // Yes, we want to be drawn double buffered by default
        DoubleBuffered = true;

        //Seb : for DPi Correction  
        using (Graphics g = CreateGraphics())
        {
            //float factorX = g.DpiX > 96 ? (1f * g.DpiX / 96) : 1f;  
            var factorY = g.DpiY > 96 ? (1f * g.DpiY / 96) : 1f;
            ColumnHeadersHeight = (int)(ColumnHeadersHeight * factorY);
        }

        SetupVisuals();
        SetupViewAndStates();
        SetupDefaults();
        SetupSyncCellStyles();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Must remove the timer so we can dispose ourself
            if (_showTimer != null)
            {
                _showTimer.Stop();
                _showTimer.Tick -= OnTimerTick;
                _showTimer.Dispose();
                _showTimer = null;
            }

            // Must unhook from the palette paint event
            if (_palette != null)
            {
                _palette.PalettePaint -= OnNeedResyncPaint;
                _palette.ButtonSpecChanged -= OnButtonSpecChanged;
            }

            // Unhook from global events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;

            // Dispose of view manager related resources
            ViewManager?.Dispose();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public New
    /// <inheritdoc/>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool DoubleBuffered 
    {
        get => base.DoubleBuffered;
        set
        {
            if (base.DoubleBuffered != value)
            {
                base.DoubleBuffered = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the number of columns displayed in the KryptonDataGridView.
    /// </summary>
    /// <returns>The number of columns displayed in the KryptonDataGridView.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0.</exception>
    /// <exception cref="System.InvalidOperationException">When setting this property, the System.Windows.Forms.DataGridView.DataSource property has been set.</exception>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(0)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public new int ColumnCount 
    {
        // base.ColumnCount is a non virtual property.
        get => base.ColumnCount;

        set
        {
            // Let the base do its work
            base.ColumnCount = value;

            // If there is a count and AutoGenerate is enabled convert them to Krypton columns
            if (base.ColumnCount > 0
                && AutoGenerateColumns
                && AutoGenerateKryptonColumns)
            {
                ReplaceDefaultColumsWithKryptonColumns(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the background color of the DataGridView.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color BackgroundColor {
        get => base.BackgroundColor;
        set => base.BackgroundColor = value;
    }

    ///// <summary>
    ///// Gets or sets the border style for the DataGridView.
    ///// </summary>
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // Disable as part of https://github.com/Krypton-Suite/Standard-Toolkit/issues/989
    //public new BorderStyle BorderStyle
    //{
    //    get => base.BorderStyle;
    //    set { /* Do nothing, we do not allow a border style change! */ }
    //}

    /// <summary>
    /// Gets the cell border style for the DataGridView.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellBorderStyle CellBorderStyle 
    {
        get => base.CellBorderStyle;
        set => base.CellBorderStyle = value;
    }

    /// <summary>
    /// Gets the border style applied to the column headers.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle 
    {
        get => base.ColumnHeadersBorderStyle;
        set => base.ColumnHeadersBorderStyle = value;
    }

    /// <summary>
    /// Gets or sets the default column header style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellStyle ColumnHeadersDefaultCellStyle 
    {
        get => base.ColumnHeadersDefaultCellStyle;
        set => base.ColumnHeadersDefaultCellStyle = value;
    }

    /// <summary>
    /// Gets or sets the default cell style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellStyle DefaultCellStyle 
    {
        get => base.DefaultCellStyle;
        set => base.DefaultCellStyle = value;
    }

    /// <summary>
    /// Gets or sets the use of visual styles for headers.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool EnableHeadersVisualStyles 
    {
        get => base.EnableHeadersVisualStyles;
        set => base.EnableHeadersVisualStyles = value;
    }

    /// <summary>
    /// Gets or sets the color of the grid lines separating the cells of the DataGridView. 
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color GridColor 
    {
        get => base.GridColor;
        set => base.GridColor = value;
    }

    /// <summary>
    /// Gets or sets the border style of the row header cells.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewHeaderBorderStyle RowHeadersBorderStyle 
    {
        get => base.RowHeadersBorderStyle;
        set => base.RowHeadersBorderStyle = value;
    }

    /// <summary>
    /// Gets or sets the default row header style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellStyle RowHeadersDefaultCellStyle 
    {
        get => base.RowHeadersDefaultCellStyle;
        set => base.RowHeadersDefaultCellStyle = value;
    }

    /// <summary>
    /// Indicates if tool tips are Displayed when the mouse hovers over the cell.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool ShowCellToolTips { get; set; }

    #endregion

    #region Public
    [Browsable(true)]
    [Category(@"Behavior")]
    [Description(@"When true the KryptonDataGridView will, upon connecting a data source, convert WinForms column types to Krypton column types, when false the standard WinForms column types.")]
    [DefaultValue(true)]
    public bool AutoGenerateKryptonColumns 
    {
        get; set;
    } = true;

    /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.</summary>
    /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or <see langword="null" /> if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is <see langword="null" />.</returns>
    [Category(@"Behavior")]
    [Description(@"Consider using KryptonContextMenu within the behaviors section.\nThe Winforms shortcut menu to show when the user right-clicks the page.\nNote: The ContextMenu will be rendered.")]
    [DefaultValue(null)]
    public override ContextMenuStrip? ContextMenuStrip 
    {
        [DebuggerStepThrough]
        get => base.ContextMenuStrip;

        set
        {
            // Unhook from any current menu strip
            if (base.ContextMenuStrip != null)
            {
                base.ContextMenuStrip.Opening -= OnContextMenuStripOpening;
                base.ContextMenuStrip.Closed -= OnContextMenuClosed;
            }

            // Let parent handle actual storage
            base.ContextMenuStrip = value;

            // Hook into the strip being shown (so we can set the correct renderer)
            if (base.ContextMenuStrip != null)
            {
                base.ContextMenuStrip.Opening += OnContextMenuStripOpening;
                base.ContextMenuStrip.Closed += OnContextMenuClosed;
            }
        }
    }


    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The KryptonContextMenu to show when the user right-clicks the Control.")]
    [DefaultValue(null)]
    public virtual KryptonContextMenu? KryptonContextMenu 
    {
        get => _kryptonContextMenu;

        set
        {
            if (_kryptonContextMenu != value)
            {
                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Closed -= OnContextMenuClosed;
                    _kryptonContextMenu.Disposed -= OnKryptonContextMenuDisposed;
                }

                _kryptonContextMenu = value;

                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Closed += OnContextMenuClosed;
                    _kryptonContextMenu.Disposed += OnKryptonContextMenuDisposed;
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets a value determining if the outer borders of the grid cells are drawn.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determine if the outer borders of the grid cells are drawn.")]
    [DefaultValue(false)]
    public bool HideOuterBorders 
    {
        get => _hideOuterBorders;

        set
        {
            if (value != _hideOuterBorders)
            {
                _hideOuterBorders = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the palette to be applied.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Palette applied to drawing.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode 
    {
        [DebuggerStepThrough]
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action despends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must assign a palette to the 
                        // 'Palette' property in order to get the custom mode
                        break;
                    default:
                        // Use the new value
                        _paletteMode = value;

                        // Get a reference to the standard palette from its name
                        _localPalette = null;
                        SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));

                        // Must raise event to change palette in redirector
                        OnPaletteChanged(EventArgs.Empty);

                        // Need to layout again use new palette
                        PerformLayout();
                        break;
                }
            }
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    /// <summary>
    /// Resets the PaletteMode property to its default value.
    /// </summary>
    public void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette implementation.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Custom palette applied to drawing.")]
    [DefaultValue(null)]
    public PaletteBase? Palette 
    {
        [DebuggerStepThrough]
        get => _localPalette;

        set
        {
            // Only interested in changes of value
            if (_localPalette != value)
            {
                // Remember the starting palette
                PaletteBase? old = _localPalette;

                // Use the provided palette value
                SetPalette(value);

                // If no custom palette is required
                if (value == null)
                {
                    // No custom palette, so revert back to the global setting
                    _paletteMode = PaletteMode.Global;

                    // Get the appropriate palette for the global mode
                    _localPalette = null;
                    SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));
                }
                else
                {
                    // No longer using a standard palette
                    _localPalette = value;
                    _paletteMode = PaletteMode.Custom;
                    SetPalette(_localPalette);
                }

                // If real change has occurred
                if (old != _localPalette)
                {
                    // Raise the change event
                    OnPaletteChanged(EventArgs.Empty);

                    // Need to layout again use new palette
                    PerformLayout();
                }
            }
        }
    }

    /// <summary>
    /// Resets the Palette property to its default value.
    /// </summary>
    public void ResetPalette() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets access to the current renderer.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IRenderer? Renderer 
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    /// <summary>
    /// Gets access to the common data grid view appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common data grid view appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDataGridViewRedirect StateCommon { get; private set; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled data grid view appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled data grid view appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDataGridViewAll StateDisabled { get; private set; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal data grid view appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal data grid view appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDataGridViewAll StateNormal { get; private set; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the tracking data grid view appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking data grid view appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDataGridViewHeaders StateTracking { get; private set; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed data grid view appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed data grid view appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDataGridViewHeaders StatePressed { get; private set; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the selected data grid view appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining selected data grid view appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDataGridViewCells StateSelected { get; private set; }

    private bool ShouldSerializeStateSelected() => !StateSelected.IsDefault;

    /// <summary>
    /// Gets access to the grid styles.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Set of grid styles.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public DataGridViewStyles GridStyles { get; private set; }

    /// <summary>
    /// Fires the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    public void PerformNeedPaint(bool needLayout) => OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));

    /// <summary>
    /// Recovers the back/border/content palettes to use for drawing the provided cell.
    /// </summary>
    /// <param name="state">State of the cell.</param>
    /// <param name="rowIndex">Row index of cell (-1 for row headers).</param>
    /// <param name="columnIndex">Column index of cell (-1 for cell headers).</param>
    /// <param name="paletteBack">IPaletteBack to be used for cell drawing.</param>
    /// <param name="paletteBorder">IPaletteBorder to be used for cell drawing.</param>
    /// <param name="paletteContent">IPaletteContent to be used for cell drawing.</param>
    /// <returns></returns>
    public virtual PaletteState GetCellTriple(DataGridViewElementStates state,
        int rowIndex,
        int columnIndex,
        out IPaletteBack paletteBack,
        out IPaletteBorder paletteBorder,
        out IPaletteContent paletteContent)
    {
        PaletteState retState;

        // If control is disabled, then draw cell as disabled
        if (!Enabled)
        {
            retState = PaletteState.Disabled;
        }
        else
        {
            retState = PaletteState.Normal;

            // If the cell is selected, then use the checked state
            if ((state & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                retState = PaletteState.CheckedNormal;
            }
            else
            {
                // A data cell cannot become tracking or pressed
                if ((rowIndex < 0) || (columnIndex < 0))
                {
                    var cellIndex = new Point(columnIndex, rowIndex);

                    // If the user has pressed down on this cell
                    if (cellIndex.Equals(_cellDown))
                    {
                        // ..and the mouse is still over the cell
                        if (cellIndex.Equals(_cellOver))
                        {
                            // ...then Displayed as pressed
                            retState = PaletteState.Pressed;
                        }
                    }
                    else
                    {
                        // Cell not pressed, but if mouse over the cell anyway
                        if (cellIndex.Equals(_cellOver))
                        {
                            retState = PaletteState.Tracking;
                        }
                    }
                }
            }
        }

        switch (rowIndex)
        {
            // Is this a data cell?
            case >= 0 when (columnIndex >= 0):
                switch (retState)
                {
                    default:
                    case PaletteState.Normal:
                        paletteBack = StateNormal.DataCell.Back;
                        paletteBorder = StateNormal.DataCell.Border;
                        paletteContent = StateNormal.DataCell.Content;
                        break;
                    case PaletteState.Disabled:
                        paletteBack = StateDisabled.DataCell.Back;
                        paletteBorder = StateDisabled.DataCell.Border;
                        paletteContent = StateDisabled.DataCell.Content;
                        break;
                    case PaletteState.CheckedNormal:
                        paletteBack = StateSelected.DataCell.Back;
                        paletteBorder = StateSelected.DataCell.Border;
                        paletteContent = StateSelected.DataCell.Content;
                        break;
                }

                break;
            case < 0:
                // Negative row index means it is a header cell
                switch (retState)
                {
                    default:
                    case PaletteState.Normal:
                        paletteBack = StateNormal.HeaderColumn.Back;
                        paletteBorder = StateNormal.HeaderColumn.Border;
                        paletteContent = StateNormal.HeaderColumn.Content;
                        break;
                    case PaletteState.Disabled:
                        paletteBack = StateDisabled.HeaderColumn.Back;
                        paletteBorder = StateDisabled.HeaderColumn.Border;
                        paletteContent = StateDisabled.HeaderColumn.Content;
                        break;
                    case PaletteState.Tracking:
                        paletteBack = StateTracking.HeaderColumn.Back;
                        paletteBorder = StateTracking.HeaderColumn.Border;
                        paletteContent = StateTracking.HeaderColumn.Content;
                        break;
                    case PaletteState.Pressed:
                        paletteBack = StatePressed.HeaderColumn.Back;
                        paletteBorder = StatePressed.HeaderColumn.Border;
                        paletteContent = StatePressed.HeaderColumn.Content;
                        break;
                    case PaletteState.CheckedNormal:
                        paletteBack = StateSelected.HeaderColumn.Back;
                        paletteBorder = StateSelected.HeaderColumn.Border;
                        paletteContent = StateSelected.HeaderColumn.Content;
                        break;
                }

                break;
            default:
                // Negative column index means it is a row cell
                switch (retState)
                {
                    default:
                    case PaletteState.Normal:
                        paletteBack = StateNormal.HeaderRow.Back;
                        paletteBorder = StateNormal.HeaderRow.Border;
                        paletteContent = StateNormal.HeaderRow.Content;
                        break;
                    case PaletteState.Disabled:
                        paletteBack = StateDisabled.HeaderRow.Back;
                        paletteBorder = StateDisabled.HeaderRow.Border;
                        paletteContent = StateDisabled.HeaderRow.Content;
                        break;
                    case PaletteState.Tracking:
                        paletteBack = StateTracking.HeaderRow.Back;
                        paletteBorder = StateTracking.HeaderRow.Border;
                        paletteContent = StateTracking.HeaderRow.Content;
                        break;
                    case PaletteState.Pressed:
                        paletteBack = StatePressed.HeaderRow.Back;
                        paletteBorder = StatePressed.HeaderRow.Border;
                        paletteContent = StatePressed.HeaderRow.Content;
                        break;
                    case PaletteState.CheckedNormal:
                        paletteBack = StateSelected.HeaderRow.Back;
                        paletteBorder = StateSelected.HeaderRow.Border;
                        paletteContent = StateSelected.HeaderRow.Content;
                        break;
                }

                break;
        }

        return retState;
    }

    /// <summary>
    /// Gets the ViewManager instance.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ViewManager? GetViewManager() => ViewManager;

    /// <summary>
    /// Gets the resolved palette to actually use when drawing.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteBase? GetResolvedPalette() => _palette;

    /// <summary>
    /// Gets or Sets the internal KryptonDataGridView CellOver
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Point CellOver 
    {
        get => _cellOver;
        set => _cellOver = value;
    }

    //seb
    /// <summary>
    /// Highlight search strings in the DataGridView 
    /// </summary>
    /// <param name="s">The string to search.</param>
    public void HighlightSearch(string s) => HighlightSearch(s, []);

    /// <summary>
    /// Highlight search strings in the DataGridView 
    /// </summary>
    /// <param name="s">The string to search.</param>
    /// <param name="columnsIndex">The columns where highlighting is possible, empty list means all columns.</param>
    public void HighlightSearch(string s, List<int> columnsIndex)
    {
        _searchString = s;
        _restrictColumnsSearch = columnsIndex;
        Invalidate();
    }

    #region ToolTipShadow

    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Button tooltip Shadow.")]
    [DefaultValue(true)]
    public bool ToolTipShadow { get; set; } = true;

    private bool ShouldSerializeToolTipShadow() => !ToolTipShadow;

    private void ResetToolTipShadow() => ToolTipShadow = true;
    #endregion

    #endregion

    #region Protected
    /// <summary>
    /// Gets and sets the ViewManager instance.
    /// </summary>
    protected ViewManager? ViewManager {
        [DebuggerStepThrough]
        get;
        set;
    }

    /// <summary>
    /// Gets access to the need paint delegate.
    /// </summary>
    protected NeedPaintHandler NeedPaintDelegate {
        [DebuggerStepThrough]
        get;
        private set;
    }

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected void OnNeedResyncPaint(object? sender, NeedLayoutEventArgs e)
    {
        // Ensure the current cell style values are in sync with the new 
        // palette setting and any state overrides that are defined
        SyncCellStylesWithPalette();

        // Continue with usual painting logic
        OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected void OnNeedPaint(object? sender, [DisallowNull] NeedLayoutEventArgs e)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // Change in setting means we need to evaluate transparent painting
        _evalTransparent = true;

        // If required, layout the control
        if (e.NeedLayout)
        {
            _layoutDirty = true;
        }

        if (IsHandleCreated && (!_refreshAll || !e.InvalidRect.IsEmpty))
        {
            // Always request the repaint immediately
            if (e.InvalidRect.IsEmpty)
            {
                _refreshAll = true;
                Invalidate();
            }
            else
            {
                Invalidate(e.InvalidRect);
            }

            // Do we need to use an Invoke to force repaint?
            if (!_refresh && EvalInvokePaint)
            {
                BeginInvoke(_refreshCall);
            }

            // A refresh is outstanding
            _refresh = true;
        }
    }

    /// <summary>
    /// Gets a value indicating if transparent paint is needed
    /// </summary>
    protected bool NeedTransparentPaint {
        get
        {
            // Do we need to evaluate the need for a tranparent paint
            if (_evalTransparent)
            {
                _paintTransparent = EvalTransparentPaint();

                // Answer is cached until paint values are changed
                _evalTransparent = false;
            }

            return _paintTransparent;
        }
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnPaletteChanged(EventArgs e)
    {
        // Update the redirector with latest palette
        Redirector.Target = _palette;

        SyncCellStylesWithPalette();

        // A new palette source means we need to layout and redraw
        OnNeedPaint(_palette, new NeedLayoutEventArgs(true));

        PaletteChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Work out if this control needs to paint transparent areas.
    /// </summary>
    /// <returns>True if paint required; otherwise false.</returns>
    protected virtual bool EvalTransparentPaint() =>
        // Do we have a manager to use for asking about painting?
        ViewManager != null && ViewManager.EvalTransparentPaint(Renderer!);

    /// <summary>
    /// Work out if this control needs to use Invoke to force a repaint.
    /// </summary>
    protected virtual bool EvalInvokePaint => false;

    /// <summary>
    /// Gets the control reference that is the parent for transparent drawing.
    /// </summary>
    protected virtual Control? TransparentParent => Parent;

    /// <summary>
    /// Processes a notification from palette storage of a button spec change.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected virtual void OnButtonSpecChanged(object? sender, [DisallowNull] EventArgs e)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }
    }
    #endregion


    #region Protected Override
    /// <inheritdoc/>
    protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
    {
        base.OnDataBindingComplete(e);

        if (AutoGenerateKryptonColumns && DataSource is not null)
        {
            ReplaceDefaultColumsWithKryptonColumns();
        }
    }

    /// <summary>
    /// Raises the PaintBackground event.  
    /// </summary>
    /// <param name="pevent">An PaintEventArgs that contains the event data.</param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        // Do nothing
    }

    /// <summary>
    /// Raises the CellMouseEnter event. 
    /// </summary>
    /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
    protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
    {
        _cellOver = new Point(e.ColumnIndex, e.RowIndex);
        base.OnCellMouseEnter(e);
    }

    /// <summary>
    /// Raises the CellMouseMove event. 
    /// </summary>
    /// <param name="e">A DataGridViewCellMouseEventArgs that contains the event data.</param>
    protected override void OnCellMouseMove(DataGridViewCellMouseEventArgs e)
    {
        // Cache mouse location before calling base class
        DataGridViewCell? cell = GetCellInternal(e.ColumnIndex, e.RowIndex);

        var oldLocation = CurrentMouseLocation(cell);
        if ((cell is DataGridViewRowHeaderCell) && (_oldCell == cell))
        {
            oldLocation = _oldLocation;
        }

        base.OnCellMouseMove(e);

        var newLocation = UpdateLocationForRowErrors(e, cell, CurrentMouseLocation(cell));
        if (cell is DataGridViewRowHeaderCell)
        {
            _oldLocation = newLocation;
            _oldCell = cell;
        }
        else
        {
            _oldCell = null;
        }

        // Use the cached value from before the call to base class
        switch (oldLocation)
        {
            case 0:
                if (newLocation != 1)
                {
                    CellErrorAreaMouseEnterInternal(cell);
                }

                CellDataAreaMouseEnterInternal(cell);
                break;
            case 1:
                if (newLocation == 2)
                {
                    CellAreaMouseLeaveInternal();
                    CellErrorAreaMouseEnterInternal(cell);
                }
                break;
            case 2:
                if (newLocation == 1)
                {
                    CellAreaMouseLeaveInternal();
                    CellDataAreaMouseEnterInternal(cell);
                }
                break;
        }
    }

    /// <summary>
    /// Raises the CellMouseLeave event. 
    /// </summary>
    /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
    protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
    {
        switch (CurrentMouseLocation(GetCellInternal(e.ColumnIndex, e.RowIndex)))
        {
            case 1:
            case 2:
                CellAreaMouseLeaveInternal();
                break;
        }

        _cellOver = _nullCell;
        base.OnCellMouseLeave(e);
    }

    /// <summary>
    /// Raises the CellMouseDown event. 
    /// </summary>
    /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
    protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
    {
        _cellDown = new Point(e.ColumnIndex, e.RowIndex);

        // If mouse down over the columns or row headers then turn off double buffering,
        // because if the user is using the mouse to resize a header item then it will use
        // an XOR painting technique to draw the resizing bar and double buffering causes
        // the painting to fail.
        if ((_cellDown.X == -1) || (_cellDown.Y == -1))
        {
            DoubleBuffered = false;
        }

        base.OnCellMouseDown(e);
    }

    /// <summary>
    /// Raises the CellMouseUp event. 
    /// </summary>
    /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
    protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
    {
        _cellDown = _nullCell;

        // Put back double buffered if it was turned off in the OnCellMouseDown
        if (!DoubleBuffered)
        {
            DoubleBuffered = true;
        }

        base.OnCellMouseUp(e);
    }

    /// <summary>
    /// Raises the EditingControlShowing event.
    /// </summary>
    /// <param name="e">A DataGridViewEditingControlShowingEventArgs that contains information about the editing control.</param>
    protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
    {
        // Prevent a tooltip from showing while the editing control is showing
        CellAreaMouseLeaveInternal();
        base.OnEditingControlShowing(e);
    }

    /// <summary>
    /// Raises the CellPainting event.
    /// </summary>
    /// <param name="e">A DataGridViewCellPaintingEventArgs that contains the event data.</param>
    protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
    {
        if (e is null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // Get the palette and state values for this cell
        PaletteState state = GetCellTriple(e.State,
            e.RowIndex,
            e.ColumnIndex,
            out IPaletteBack paletteBack,
            out IPaletteBorder paletteBorder,
            out IPaletteContent paletteContent);

        try
        {
            // If the font we get from the base palette is a system font that is invalid this will throw exception
            var hContent = _contentInherit.GetContentShortTextFont(state)!.Height;
        }
        catch
        {
            // Get the latest font from the base palette that will have been updated to be valid
            SyncCellStylesWithPalette();
        }

        var rtl = RightToLeftInternal;

        // Use an offscreen bitmap to draw onto before blitting it to the screen
        var tempCellBounds = e.CellBounds with { X = 0, Y = 0 };
        using (var tempBitmap = new Bitmap(e.CellBounds.Width, e.CellBounds.Height, e.Graphics!))
        {
            using (Graphics tempG = Graphics.FromImage(tempBitmap))
            {
                using (var renderContext = new RenderContext(this, tempG, tempCellBounds, Renderer!))
                {
                    // Force the border to have a specified maximum border edge
                    _borderForced.SetInherit(paletteBorder);
                    _borderForced.MaxBorderEdges = GetCellMaxBorderEdges(e.CellBounds, e.ColumnIndex, e.RowIndex);

                    // Get the padding used to decide how to draw the background
                    Padding borderPadding = Renderer!.RenderStandardBorder.GetBorderRawPadding(_borderForced, state, VisualOrientation.Top);

                    // Get the border path used to limit drawing of the background
                    GraphicsPath borderPath = Renderer.RenderStandardBorder.GetBackPath(renderContext, tempCellBounds, _borderForced, VisualOrientation.Top, state);

                    // Reduce background drawing rect by the raw padding
                    Rectangle tempCellBackBounds = CommonHelper.ApplyPadding(VisualOrientation.Top, tempCellBounds, borderPadding);

                    // Update the back interceptor class
                    _backInherit.SetInherit(paletteBack, e.CellStyle!);
                    using var gh = new GraphicsHint(renderContext.Graphics, _borderForced.GetBorderGraphicsHint(PaletteState.Normal));
                    IDisposable? unused = Renderer.RenderStandardBack.DrawBack(renderContext, tempCellBackBounds, borderPath, _backInherit, VisualOrientation.Top, state, null);

                    // We never save the memento for reuse later
                    unused?.Dispose();

                    Renderer.RenderStandardBorder.DrawBorder(renderContext, tempCellBounds, _borderForced, VisualOrientation.Top, state);

                    // Must remember to release resources!
                    gh.Dispose();
                    borderPath.Dispose();

                    switch (e)
                    {
                        // If this is a column header cell
                        case { RowIndex: -1, ColumnIndex: >= 0 }:
                        {
                            // If this column needs a sort glyph drawn
                            if (Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection != SortOrder.None)
                            {
                                // Draw the sort glyph and update the remainder cell bounds left over
                                tempCellBounds = Renderer.RenderGlyph.DrawGridSortGlyph(renderContext, Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection, tempCellBounds, paletteContent, state, rtl);
                            }

                            // If this column supports icons, see if it has any.
                            if (Columns[e.ColumnIndex] is IIconCell iconColumn)
                            {
                                foreach (IconSpec spec in iconColumn.IconSpecs)
                                {
                                    if (spec.Icon == null)
                                    {
                                        continue;
                                    }
                                    // Draw icon and update the remainder cell bounds left over
                                    var iconWidth = spec.Icon.Width + 5;
                                    var width = tempCellBounds.Width - iconWidth;
                                    var iconBounds = new Rectangle(
                                        tempCellBounds.X + (spec.Alignment == IconSpec.IconAlignment.Left
                                            ? 5
                                            : width), tempCellBounds.Y + 3, spec.Icon.Width, spec.Icon.Height);
                                    renderContext.Graphics.DrawImage(spec.Icon, iconBounds);
                                    tempCellBounds = tempCellBounds with
                                    {
                                        X = tempCellBounds.X +
                                            (spec.Alignment == IconSpec.IconAlignment.Left ? iconWidth : 0),
                                        Width = width
                                    };
                                }
                            }

                            break;
                        }
                        // If this is a row header cell
                        case { RowIndex: >= 0, ColumnIndex: -1 }:
                        {
                            // By default, there is no glyph needed for the row
                            var glyph = GridRowGlyph.None;

                            // Find the correct glyph that should be drawn
                            if (CurrentCellAddress.Y == e.RowIndex)
                            {
                                if (VirtualMode)
                                {
                                    if (IsCurrentRowDirty && ShowEditingIcon)
                                    {
                                        glyph = GridRowGlyph.Pencil;
                                    }
                                    else if (NewRowIndex == e.RowIndex)
                                    {
                                        glyph = GridRowGlyph.ArrowStar;
                                    }
                                    else
                                    {
                                        glyph = GridRowGlyph.Arrow;
                                    }
                                }
                                else if (IsCurrentCellDirty && ShowEditingIcon)
                                {
                                    glyph = GridRowGlyph.Pencil;
                                }
                                else if (NewRowIndex == e.RowIndex)
                                {
                                    glyph = GridRowGlyph.ArrowStar;
                                }
                                else
                                {
                                    glyph = GridRowGlyph.Arrow;
                                }
                            }
                            else if (NewRowIndex == e.RowIndex)
                            {
                                glyph = GridRowGlyph.Star;
                            }

                            // Do we need to draw an image?
                            if (glyph != GridRowGlyph.None)
                            {
                                // Draw the row glyph and update the remainder cell bounds left over
                                tempCellBounds = Renderer.RenderGlyph.DrawGridRowGlyph(renderContext, glyph, tempCellBounds, paletteContent, state, rtl);
                            }

                            // Is there an error icon associated with the row that needs showing
                            if (ShowRowErrors && !string.IsNullOrEmpty(Rows[e.RowIndex].ErrorText))
                            {
                                // Draw error icon and update the remainder cell bounds left over
                                Rectangle beforeCellBounds = tempCellBounds;
                                tempCellBounds = Renderer.RenderGlyph.DrawGridErrorGlyph(renderContext, tempCellBounds, state, rtl);

                                // Calculate the icon rectangle
                                var iconBounds = new Rectangle(tempCellBounds.Right + 1, tempCellBounds.Top,
                                    beforeCellBounds.Width - tempCellBounds.Width, tempCellBounds.Height);

                                // Cache the icon area
                                if (_rowCache.ContainsKey(e.RowIndex))
                                {
                                    _rowCache[e.RowIndex] = iconBounds;
                                }
                                else
                                {
                                    _rowCache.Add(e.RowIndex, iconBounds);
                                }
                            }
                            else
                            {
                                // Remove any cache entry
                                if (_rowCache.ContainsKey(e.RowIndex))
                                {
                                    _rowCache.Remove(e.RowIndex);
                                }
                            }

                            break;
                        }
                        // Is this a data cell
                        case { RowIndex: >= 0, ColumnIndex: >= 0 }:
                        {
                            // If this cell supports icons, see if it has any.
                            if (Rows[e.RowIndex].Cells[e.ColumnIndex] is IIconCell iconColumn)
                            {
                                foreach (IconSpec spec in iconColumn.IconSpecs)
                                {
                                    if (spec.Icon == null)
                                    {
                                        continue;
                                    }

                                    // Draw icon and update the remainder cell bounds left over
                                    var iconWidth = spec.Icon.Width + 5;
                                    var width = tempCellBounds.Width - iconWidth;
                                    var iconBounds = new Rectangle(
                                        tempCellBounds.X + (spec.Alignment == IconSpec.IconAlignment.Left
                                            ? 5
                                            : width), tempCellBounds.Y + 3, spec.Icon.Width, spec.Icon.Height);
                                    renderContext.Graphics.DrawImage(spec.Icon, iconBounds);
                                    tempCellBounds = tempCellBounds with
                                    {
                                        X = tempCellBounds.X +
                                            (spec.Alignment == IconSpec.IconAlignment.Left ? iconWidth : 0),
                                        Width = width
                                    };
                                }
                            }

                            // Is there an error icon associated with the cell that needs showing
                            if (ShowCellErrors && !string.IsNullOrEmpty(Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText))
                            {
                                // Draw error icon and update the remainder cell bounds left over
                                tempCellBounds = Renderer.RenderGlyph.DrawGridErrorGlyph(renderContext, tempCellBounds, state, rtl);
                            }

                            break;
                        }
                    }

                    if (((e.PaintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground) ||
                        ((e.PaintParts & DataGridViewPaintParts.ContentBackground) == DataGridViewPaintParts.ContentBackground))
                    {
                        // Only consider drawing content for the data cells
                        if (e is { ColumnIndex: >= 0, RowIndex: >= 0 })
                        {
                            // Blit the image onto the screen
                            e.Graphics?.DrawImage(tempBitmap, e.CellBounds.Location);

                            //Seb Search highlight 
                            //Empty _restrictColumnsSearch means highlight everywhere
                            if (!string.IsNullOrEmpty(_searchString)
                                && (_restrictColumnsSearch.Count == 0 || (_restrictColumnsSearch.Count != 0
                                                                          && _restrictColumnsSearch.Contains(e.ColumnIndex)))
                                && e.FormattedValue!.GetType().Name != nameof(Bitmap))
                            {
                                var val = (string)e.FormattedValue;
                                var sindx = val.ToLower().IndexOf(_searchString.ToLower());
                                var sCount = 1;
                                while (sindx >= 0)
                                {
                                    var hl_rect = new Rectangle
                                    {
                                        Y = e.CellBounds.Y + 2,
                                        Height = e.CellBounds.Height - 5
                                    };

                                    var sBefore = val.Substring(0, sindx);
                                    var sWord = val.Substring(sindx, _searchString.Length);
                                    Size s1 = TextRenderer.MeasureText(e.Graphics!, sBefore, e.CellStyle!.Font, e.CellBounds.Size);
                                    Size s2 = TextRenderer.MeasureText(e.Graphics!, sWord, e.CellStyle.Font, e.CellBounds.Size);

                                    if (s1.Width > 5)
                                    {
                                        hl_rect.X = e.CellBounds.X + e.CellStyle.Padding.Left + s1.Width - 4;
                                        hl_rect.Width = s2.Width - 7;
                                    }
                                    else
                                    {
                                        hl_rect.X = e.CellBounds.X + 2 + e.CellStyle.Padding.Left;
                                        hl_rect.Width = s2.Width - 6;
                                    }

                                    //Original
                                    //if (s1.Width > 5)
                                    //{
                                    //    hl_rect.X = e.CellBounds.X + s1.Width - 5;
                                    //    hl_rect.Width = s2.Width - 6;
                                    //}
                                    //else
                                    //{
                                    //    hl_rect.X = e.CellBounds.X + 2;
                                    //    hl_rect.Width = s2.Width - 6;
                                    //}

                                    var hl_brush =
                                        (e.State & DataGridViewElementStates.Selected) !=
                                        DataGridViewElementStates.None
                                            ? new SolidBrush(Color.DarkGoldenrod)
                                            : new SolidBrush(Color.Yellow);

                                    e.Graphics!.FillRectangle(hl_brush, hl_rect);

                                    hl_brush.Dispose();
                                    sindx = val.ToLower().IndexOf(_searchString.ToLower(), sCount++);
                                }
                            }
                            // Let column do the painting
                            e.Paint(e.ClipBounds, e.PaintParts & (DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ContentBackground));
                        }
                        else
                        {
                            // Update the content interceptor class
                            _contentInherit.SetInherit(paletteContent!, e.CellStyle!);

                            // Is there any text to be Displayed?
                            if (e.FormattedValue != null)
                            {
                                // Use the display value of the header cell
                                _shortTextValue.ShortText = e.FormattedValue.ToString();

                                using var layoutContext = new ViewLayoutContext(this, Renderer);
                                // If a column header cell...
                                if ((e.RowIndex == -1) && (e.ColumnIndex != -1))
                                {
                                    // Find size needed to show header text fully
                                    Size prefSize = Renderer.RenderStandardContent.GetContentPreferredSize(
                                        layoutContext, _contentInherit, _shortTextValue,
                                        VisualOrientation.Top, state);

                                    var contentsFit = (prefSize.Width <= tempCellBounds.Width) &&
                                                      (prefSize.Height <= tempCellBounds.Height);

                                    // Cache if the column cell can display all the content
                                    if (_columnCache.ContainsKey(e.ColumnIndex))
                                    {
                                        _columnCache[e.ColumnIndex] = contentsFit;
                                    }
                                    else
                                    {
                                        _columnCache.Add(e.ColumnIndex, contentsFit);
                                    }
                                }

                                // Find the correct layout for the header content
                                using IDisposable memento = Renderer.RenderStandardContent.LayoutContent(
                                    layoutContext, tempCellBounds,
                                    _contentInherit, _shortTextValue,
                                    VisualOrientation.Top, state);
                                // Perform actual drawing of the content
                                Renderer.RenderStandardContent.DrawContent(renderContext, tempCellBounds,
                                    _contentInherit, memento,
                                    VisualOrientation.Top,
                                    state, true);
                            }

                            // Blit the image onto the screen
                            e.Graphics?.DrawImage(tempBitmap, e.CellBounds.Location);
                        }
                    }
                    else
                    {
                        // Blit the image onto the screen
                        e.Graphics?.DrawImage(tempBitmap, e.CellBounds.Location);
                    }
                }
            }
        }

        if (e != null && (e.PaintParts & DataGridViewPaintParts.Focus) == DataGridViewPaintParts.Focus)
        {
            // Only consider drawing the focus rectangle if the control has focus wants to show the cues
            if (ShowFocusCues && Focused)
            {
                // Only consider drawing focus for data cells
                if (e is { ColumnIndex: >= 0, RowIndex: >= 0 })
                {
                    // Is the cell being drawn the current cell
                    if ((CurrentCellAddress.X == e.ColumnIndex) &&
                        (CurrentCellAddress.Y == e.RowIndex))
                    {
                        Rectangle focusCellBounds = e.CellBounds;
                        focusCellBounds.Width--;
                        focusCellBounds.Height--;

                        // If RTL then need to shift from left edge instead of right
                        if (rtl)
                        {
                            focusCellBounds.X++;
                        }

                        ControlPaint.DrawFocusRectangle(e.Graphics!, focusCellBounds, GlobalStaticValues.EMPTY_COLOR, paletteContent!.GetContentShortTextColor1(state));
                    }
                }
            }
        }

        // Prevent base class from doing the standard drawing
        e!.Handled = true;

        base.OnCellPainting(e);
    }

    /// <summary>
    /// Paints the background of the DataGridView.
    /// </summary>
    /// <param name="graphics">The Graphics used to paint the background.</param>
    /// <param name="clipBounds">A Rectangle that represents the area of the DataGridView that needs to be painted.</param>
    /// <param name="gridBounds">A Rectangle that represents the area in which cells are drawn.</param>
    protected override void PaintBackground(Graphics graphics,
        Rectangle clipBounds,
        Rectangle gridBounds)
    {
        if (!IsDisposed)
        {
            // Do we have a manager to use for painting?
            if (ViewManager != null)
            {
                // If the layout is dirty, or the size of the control has changed 
                // without a layout being performed, then perform a layout now
                if (_layoutDirty && (!Size.Equals(_lastLayoutSize)))
                {
                    ViewManagerLayout();
                }

                // Do not currently clip because it causes issues when the scroll bars are not showing and the user
                // scrolls by using the keyboard or by sorting the columns. So it does cause a little flicker
                //   using (Clipping clip = new Clipping(graphics, GetBackgroundClipRect(), true))
                {
                    // Draw the background as transparent, by drawing parent
                    PaintTransparentBackground(graphics, clipBounds);

                    // Use the view manager to paint the view panel that fills the entire areas as the background
                    using var context = new RenderContext(this, graphics, clipBounds, Renderer!);
                    ViewManager.Paint(context);
                }

                // Request for a refresh has been serviced
                _refresh = false;
                _refreshAll = false;
            }
        }
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        _drawPanel.SetPalettes(Enabled ? StateNormal.Background : StateDisabled.Background);

        // Update with latest enabled state
        _drawPanel.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        OnNeedResyncPaint(this, new NeedLayoutEventArgs(true));

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        // Get the view manager to relayout its elements
        ViewManagerLayout();

        // Let base class layout child controls
        base.OnLayout(levent);
    }
    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal PaletteRedirect Redirector {
        [DebuggerStepThrough]
        get;
        private set;
    }

    internal void SyncStyles()
    {
        // Update with individual grid styles required
        StateCommon.SetGridStyles(GridStyles.StyleColumn,
            GridStyles.StyleRow,
            GridStyles.StyleDataCells);

        // Update the background separately
        StateCommon.BackStyle = GridStyles.StyleBackground;

        SyncCellStylesWithPalette();
    }

    internal bool RightToLeftInternal 
    {
        get
        {
            // Only need to cache reflection info the first time around
            if (_piRTL == null)
            {
                // Cache access to the internal get property 'RightToLeftInternal'
                _piRTL = typeof(DataGridView).GetProperty(nameof(RightToLeftInternal), BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.GetField)!;

            }

            // Grab the internal calculated value of the right to left setting
            return (bool)_piRTL.GetValue(this, null)!;
        }
    }
    #endregion

    #region Implementation

    /// <summary>
    /// Handles the auto generation of Krypton columns<br/>
    /// </summary>
    /// <param name="convertOnEmptyDataPropertyName">When true, even if the DataPropertyName has not been set columns will be converted. 
    /// When true the DataPropertyName is mandatory.</param>
    private void ReplaceDefaultColumsWithKryptonColumns(bool convertOnEmptyDataPropertyName = false)
    {
        DataGridViewColumn currentColumn;
        int index;
        IComponentChangeService? changeService = null;
        IDesignerHost? designerHost = null;

        if (this.DesignMode)
        {
            changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            designerHost = this.Site!.GetService(typeof(IDesignerHost)) as IDesignerHost;
            changeService?.OnComponentChanging(this, null);
        }

        for (int i = 0 ; i < ColumnCount ; i++)
        {
            currentColumn = Columns[i];

            /*
             * Auto generated columns are always of DataGridViewTextBoxColumn, DataGridViewCheckBoxBoxColumn or DataGridViewImageColumn
             */
            if (currentColumn.DataPropertyName.Length > 0 || convertOnEmptyDataPropertyName)
            {
                index = currentColumn.Index;

                if (currentColumn is DataGridViewTextBoxColumn)
                {
                    var newColumn = this.DesignMode
                        ? designerHost?.CreateComponent(typeof(KryptonDataGridViewTextBoxColumn)) as KryptonDataGridViewTextBoxColumn
                        : new KryptonDataGridViewTextBoxColumn();

                    newColumn!.Name = currentColumn.Name;
                    newColumn.DataPropertyName = currentColumn.DataPropertyName;
                    newColumn.HeaderText = currentColumn.HeaderText;
                    newColumn.Width = currentColumn.Width;
                    newColumn.AutoSizeMode = currentColumn.AutoSizeMode;
                    newColumn.DefaultCellStyle.Format = currentColumn.DefaultCellStyle.Format;
                    newColumn.DefaultCellStyle.Alignment = currentColumn.DefaultCellStyle.Alignment;
                    newColumn.Visible = currentColumn.Visible;
                    newColumn.HeaderCell.Style.Alignment= currentColumn.HeaderCell.Style.Alignment;

                    Columns.RemoveAt(index);
                    Columns.Insert(index, newColumn);

                    designerHost?.DestroyComponent(currentColumn);
                }
                else if (currentColumn is DataGridViewCheckBoxColumn)
                {
                    var newColumn = this.DesignMode
                        ? designerHost?.CreateComponent(typeof(KryptonDataGridViewCheckBoxColumn)) as KryptonDataGridViewCheckBoxColumn
                        : new KryptonDataGridViewCheckBoxColumn();

                    newColumn!.Name = currentColumn.Name;
                    newColumn.DataPropertyName = currentColumn.DataPropertyName;
                    newColumn.HeaderText = currentColumn.HeaderText;
                    newColumn.Width = currentColumn.Width;
                    newColumn.AutoSizeMode = currentColumn.AutoSizeMode;
                    newColumn.DefaultCellStyle.Format = currentColumn.DefaultCellStyle.Format;
                    newColumn.DefaultCellStyle.Alignment = currentColumn.DefaultCellStyle.Alignment;
                    newColumn.Visible = currentColumn.Visible;
                    newColumn.HeaderCell.Style.Alignment= currentColumn.HeaderCell.Style.Alignment;

                    Columns.RemoveAt(index);
                    Columns.Insert(index, newColumn);

                    designerHost?.DestroyComponent(currentColumn);
                }
                else if (currentColumn is DataGridViewImageColumn)
                {
                    var newColumn = this.DesignMode
                        ? designerHost?.CreateComponent(typeof(KryptonDataGridViewImageColumn)) as KryptonDataGridViewImageColumn
                        : new KryptonDataGridViewImageColumn();

                    newColumn!.Name = currentColumn.Name;
                    newColumn.DataPropertyName = currentColumn.DataPropertyName;
                    newColumn.HeaderText = currentColumn.HeaderText;
                    newColumn.Width = currentColumn.Width;
                    newColumn.ImageLayout = (currentColumn as DataGridViewImageColumn)!.ImageLayout;
                    newColumn.AutoSizeMode = currentColumn.AutoSizeMode;
                    newColumn.DefaultCellStyle.Format = currentColumn.DefaultCellStyle.Format;
                    newColumn.DefaultCellStyle.Alignment = currentColumn.DefaultCellStyle.Alignment;
                    newColumn.Visible = currentColumn.Visible;
                    newColumn.HeaderCell.Style.Alignment= currentColumn.HeaderCell.Style.Alignment;

                    Columns.RemoveAt(index);
                    Columns.Insert(index, newColumn);

                    designerHost?.DestroyComponent(currentColumn);
                }
            }
        }

        changeService?.OnComponentChanged(this, null, null, null);
    }

    private void SetupVisuals()
    {
        // Setup the invoke used to refresh display
        _refreshCall = OnPerformRefresh;

        // Setup the need paint delegate
        NeedPaintDelegate = OnNeedResyncPaint;

        // Must layout before first draw attempt
        _layoutDirty = true;
        _evalTransparent = true;
        _lastLayoutSize = Size.Empty;

        // Set the palette to the defaults as specified by the PaletteMode property
        _localPalette = null;
        // start off with global mode as default
        _paletteMode = PaletteMode.Global;
        SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));

        // Create constant target for resolving palette delegates
        Redirector = new PaletteRedirect(_palette);

        // Hook into global palette changing events
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // We need to notice when system color settings change
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
    }

    private void SetupViewAndStates()
    {
        // Create the state storage objects
        StateCommon = new PaletteDataGridViewRedirect(Redirector, NeedPaintDelegate);
        StateDisabled = new PaletteDataGridViewAll(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteDataGridViewAll(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteDataGridViewHeaders(StateCommon, NeedPaintDelegate);
        StatePressed = new PaletteDataGridViewHeaders(StateCommon, NeedPaintDelegate);
        StateSelected = new PaletteDataGridViewCells(StateCommon, NeedPaintDelegate);

        // Our view contains just a simple canvas that is the background
        _drawPanel = new ViewDrawPanel(StateNormal.Background);

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawPanel);
    }

    private void SetupDefaults()
    {
        // Create internal objects
        _shortTextValue = new ShortTextValue();
        _borderForced = new PaletteBorderInheritForced(null);
        _backInherit = new PaletteDataGridViewBackInherit();
        _contentInherit = new PaletteDataGridViewContentInherit();
        GridStyles = new DataGridViewStyles(this);
        _columnCache = new ColumnHeaderCache();
        _rowCache = new RowHeaderCache();
        _showTimer = new System.Windows.Forms.Timer
        {
            Interval = 500
        };
        _showTimer.Tick += OnTimerTick;

        // Default internal fields
        _cellDown = _nullCell;
        _cellOver = _nullCell;
        _hideOuterBorders = false;
        ShowCellToolTips = true;

        // Remove border from being drawn, border is drawn according to system settings
        // and we do not want that appearance. So set to 'None' and override the 
        // BorderStyle property so it cannot be set to anything else.
        BorderStyle = BorderStyle.None;

        // Always turn off the base functionality as we do it instead.
        base.ShowCellToolTips = false;

        //Seb
        _searchString = string.Empty;
    }

    private void SetupSyncCellStyles()
    {
        // Grab the default font (etc...) values as the starting remembered values. 
        // Then when we test for changes it will not look as if the user has changed
        // them. This ensures the call to SyncCellStylesWithPalette updated them.
        _columnFont = ColumnHeadersDefaultCellStyle.Font;
        _rowFont = RowHeadersDefaultCellStyle.Font;
        _dataCellFont = DefaultCellStyle.Font;
        _columnPadding = ColumnHeadersDefaultCellStyle.Padding;
        _rowPadding = RowHeadersDefaultCellStyle.Padding;
        _dataCellPadding = DefaultCellStyle.Padding;
        _columnAlign = ColumnHeadersDefaultCellStyle.Alignment;
        _rowAlign = RowHeadersDefaultCellStyle.Alignment;
        _dataCellAlign = DefaultCellStyle.Alignment;
        _columnBackColor = ColumnHeadersDefaultCellStyle.BackColor;
        _columnForeColor = ColumnHeadersDefaultCellStyle.ForeColor;
        _columnSelBackColor = ColumnHeadersDefaultCellStyle.SelectionBackColor;
        _columnSelForeColor = ColumnHeadersDefaultCellStyle.SelectionForeColor;
        _rowBackColor = RowHeadersDefaultCellStyle.BackColor;
        _rowForeColor = RowHeadersDefaultCellStyle.ForeColor;
        _rowSelBackColor = RowHeadersDefaultCellStyle.SelectionBackColor;
        _rowSelForeColor = RowHeadersDefaultCellStyle.SelectionForeColor;
        _dataCellBackColor = DefaultCellStyle.BackColor;
        _dataCellForeColor = DefaultCellStyle.ForeColor;
        _dataCellSelBackColor = DefaultCellStyle.SelectionBackColor;
        _dataCellSelForeColor = DefaultCellStyle.SelectionForeColor;

        // Ensure the current cell style values are in sync with the new palette 
        // setting and any state overrides that are defined.
        SyncCellStylesWithPalette();

        // We need to know when the common values we sync are changed
        StateCommon.HeaderColumn.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateCommon.HeaderRow.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateCommon.DataCell.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateNormal.HeaderColumn.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateNormal.HeaderRow.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateNormal.DataCell.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateSelected.HeaderColumn.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateSelected.HeaderRow.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateSelected.DataCell.Content.SyncPropertyChanged += OnSyncPropertyChanged;
        StateNormal.HeaderColumn.Back.PropertyChanged += OnSyncBackPropertyChanged;
        StateNormal.HeaderRow.Back.PropertyChanged += OnSyncBackPropertyChanged;
        StateNormal.DataCell.Back.PropertyChanged += OnSyncBackPropertyChanged;
        StateSelected.HeaderColumn.Back.PropertyChanged += OnSyncBackPropertyChanged;
        StateSelected.HeaderRow.Back.PropertyChanged += OnSyncBackPropertyChanged;
        StateSelected.DataCell.Back.PropertyChanged += OnSyncBackPropertyChanged;
    }

    private void SyncCellStylesWithPalette()
    {
        if (StateCommon != null)
        {
            SyncFontCellStylesWithPalette();
            SyncPaddingCellStylesWithPalette();
            SyncAlignmentCellStylesWithPalette();
            SyncBackColorCellStylesWithPalette();
            SyncSelBackColorCellStylesWithPalette();
            SyncForeColorCellStylesWithPalette();
            SyncSelForeColorCellStylesWithPalette();
        }
    }

    private void SyncFontCellStylesWithPalette()
    {
        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;

        // If the column headers default font is null or if the same as when we last
        // set the value then we do need to update with the latest value. Otherwise
        // the programmer has modified the value and so leave it alone as over-ridden.
        if ((ColumnHeadersDefaultCellStyle.Font == null) ||
            ColumnHeadersDefaultCellStyle.Font.Equals(_columnFont))
        {
            // Get the overriden value from the stat common
            _columnFont = StateCommon.HeaderColumn.Content.Font ?? StateCommon.HeaderColumn.Content.GetContentShortTextFont(state);

            // If not found, get it from the inheritance palette

            ColumnHeadersDefaultCellStyle.Font = _columnFont;
        }

        if ((RowHeadersDefaultCellStyle.Font == null) ||
            RowHeadersDefaultCellStyle.Font.Equals(_rowFont))
        {
            _rowFont = StateCommon.HeaderRow.Content.Font ?? StateCommon.HeaderRow.Content.GetContentShortTextFont(state);

            RowHeadersDefaultCellStyle.Font = _rowFont;
        }

        if ((DefaultCellStyle.Font == null) ||
            DefaultCellStyle.Font.Equals(_dataCellFont))
        {
            _dataCellFont = StateCommon.DataCell.Content.Font ?? StateCommon.DataCell.Content.GetContentShortTextFont(state);

            DefaultCellStyle.Font = _dataCellFont;
        }
    }

    private void SyncPaddingCellStylesWithPalette()
    {
        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;

        if (ColumnHeadersDefaultCellStyle.Padding.Equals(_columnPadding))
        {
            _columnPadding = StateCommon.HeaderColumn.Content.Padding;
            if (_columnPadding.Equals(CommonHelper.InheritPadding))
            {
                _columnPadding = StateCommon.HeaderColumn.Content.GetBorderContentPadding(null, state);
            }

            ColumnHeadersDefaultCellStyle.Padding = _columnPadding;
        }

        if (RowHeadersDefaultCellStyle.Padding.Equals(_rowPadding))
        {
            _rowPadding = StateCommon.HeaderRow.Content.Padding;
            if (_rowPadding.Equals(CommonHelper.InheritPadding))
            {
                _rowPadding = StateCommon.HeaderRow.Content.GetBorderContentPadding(null, state);
            }

            RowHeadersDefaultCellStyle.Padding = _rowPadding;
        }

        if (DefaultCellStyle.Padding.Equals(_dataCellPadding))
        {
            _dataCellPadding = StateCommon.DataCell.Content.Padding;
            if (_dataCellPadding.Equals(CommonHelper.InheritPadding))
            {
                _dataCellPadding = StateCommon.DataCell.Content.GetBorderContentPadding(null, state);
            }

            DefaultCellStyle.Padding = _dataCellPadding;
        }
    }

    private void SyncAlignmentCellStylesWithPalette()
    {
        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;

        if (ColumnHeadersDefaultCellStyle.Alignment == _columnAlign)
        {
            PaletteRelativeAlign textH = StateCommon.HeaderColumn.Content.TextH;
            PaletteRelativeAlign textV = StateCommon.HeaderColumn.Content.TextV;

            if (textH == PaletteRelativeAlign.Inherit)
            {
                textH = StateCommon.HeaderColumn.Content.GetContentShortTextH(state);
            }

            if (textV == PaletteRelativeAlign.Inherit)
            {
                textV = StateCommon.HeaderColumn.Content.GetContentShortTextV(state);
            }

            _columnAlign = RelativeToAlign(textH, textV);
            ColumnHeadersDefaultCellStyle.Alignment = _columnAlign;
        }

        if (RowHeadersDefaultCellStyle.Alignment == _rowAlign)
        {
            PaletteRelativeAlign textH = StateCommon.HeaderRow.Content.TextH;
            PaletteRelativeAlign textV = StateCommon.HeaderRow.Content.TextV;

            if (textH == PaletteRelativeAlign.Inherit)
            {
                textH = StateCommon.HeaderRow.Content.GetContentShortTextH(state);
            }

            if (textV == PaletteRelativeAlign.Inherit)
            {
                textV = StateCommon.HeaderRow.Content.GetContentShortTextV(state);
            }

            _rowAlign = RelativeToAlign(textH, textV);
            RowHeadersDefaultCellStyle.Alignment = _rowAlign;
        }

        if (DefaultCellStyle.Alignment == _dataCellAlign)
        {
            PaletteRelativeAlign textH = StateCommon.DataCell.Content.TextH;
            PaletteRelativeAlign textV = StateCommon.DataCell.Content.TextV;

            if (textH == PaletteRelativeAlign.Inherit)
            {
                textH = StateCommon.DataCell.Content.GetContentShortTextH(state);
            }

            if (textV == PaletteRelativeAlign.Inherit)
            {
                textV = StateCommon.DataCell.Content.GetContentShortTextV(state);
            }

            _dataCellAlign = RelativeToAlign(textH, textV);
            DefaultCellStyle.Alignment = _dataCellAlign;
        }
    }

    private void SyncBackColorCellStylesWithPalette()
    {
        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;

        if ((ColumnHeadersDefaultCellStyle.BackColor == GlobalStaticValues.EMPTY_COLOR) ||
            (ColumnHeadersDefaultCellStyle.BackColor == _columnBackColor))
        {
            _columnBackColor = StateNormal.HeaderColumn.Back.Color1;

            if (_columnBackColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _columnBackColor = StateNormal.HeaderColumn.Back.GetBackColor1(state);
            }

            ColumnHeadersDefaultCellStyle.BackColor = _columnBackColor;
        }

        if ((RowHeadersDefaultCellStyle.BackColor == GlobalStaticValues.EMPTY_COLOR) ||
            (RowHeadersDefaultCellStyle.BackColor == _rowBackColor))
        {
            _rowBackColor = StateNormal.HeaderRow.Back.Color1;

            if (_rowBackColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _rowBackColor = StateNormal.HeaderRow.Back.GetBackColor1(state);
            }

            RowHeadersDefaultCellStyle.BackColor = _rowBackColor;
        }

        if ((DefaultCellStyle.BackColor == GlobalStaticValues.EMPTY_COLOR) ||
            (DefaultCellStyle.BackColor == _dataCellBackColor))
        {
            _dataCellBackColor = StateNormal.DataCell.Back.Color1;

            if (_dataCellBackColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _dataCellBackColor = StateNormal.DataCell.Back.GetBackColor1(state);
            }

            DefaultCellStyle.BackColor = _dataCellBackColor;
        }
    }

    private void SyncSelBackColorCellStylesWithPalette()
    {
        PaletteState state = Enabled ? PaletteState.CheckedNormal : PaletteState.Disabled;

        if ((ColumnHeadersDefaultCellStyle.SelectionBackColor == GlobalStaticValues.EMPTY_COLOR) ||
            (ColumnHeadersDefaultCellStyle.SelectionBackColor == _columnSelBackColor))
        {
            _columnSelBackColor = StateSelected.HeaderColumn.Back.Color1;

            if (_columnSelBackColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _columnSelBackColor = StateSelected.HeaderColumn.Back.GetBackColor1(state);
            }

            ColumnHeadersDefaultCellStyle.SelectionBackColor = _columnSelBackColor;
        }

        if ((RowHeadersDefaultCellStyle.SelectionBackColor == GlobalStaticValues.EMPTY_COLOR) ||
            (RowHeadersDefaultCellStyle.SelectionBackColor == _rowSelBackColor))
        {
            _rowSelBackColor = StateSelected.HeaderRow.Back.Color1;

            if (_rowSelBackColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _rowSelBackColor = StateSelected.HeaderRow.Back.GetBackColor1(state);
            }

            RowHeadersDefaultCellStyle.SelectionBackColor = _rowSelBackColor;
        }

        if ((DefaultCellStyle.SelectionBackColor == GlobalStaticValues.EMPTY_COLOR) ||
            (DefaultCellStyle.SelectionBackColor == _dataCellSelBackColor))
        {
            _dataCellSelBackColor = StateSelected.DataCell.Back.Color1;

            if (_dataCellSelBackColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _dataCellSelBackColor = StateSelected.DataCell.Back.GetBackColor1(state);
            }

            DefaultCellStyle.SelectionBackColor = _dataCellSelBackColor;
        }
    }

    private void SyncForeColorCellStylesWithPalette()
    {
        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;

        if ((ColumnHeadersDefaultCellStyle.ForeColor == GlobalStaticValues.EMPTY_COLOR) ||
            (ColumnHeadersDefaultCellStyle.ForeColor == _columnForeColor))
        {
            _columnForeColor = StateNormal.HeaderColumn.Content.Color1;

            if (_columnForeColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _columnForeColor = StateNormal.HeaderColumn.Content.GetContentShortTextColor1(state);
            }

            ColumnHeadersDefaultCellStyle.ForeColor = _columnForeColor;
        }

        if ((RowHeadersDefaultCellStyle.ForeColor == GlobalStaticValues.EMPTY_COLOR) ||
            (RowHeadersDefaultCellStyle.ForeColor == _rowForeColor))
        {
            _rowForeColor = StateNormal.HeaderRow.Content.Color1;

            if (_rowForeColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _rowForeColor = StateNormal.HeaderRow.Content.GetContentShortTextColor1(state);
            }

            RowHeadersDefaultCellStyle.ForeColor = _rowForeColor;
        }

        if ((DefaultCellStyle.ForeColor == GlobalStaticValues.EMPTY_COLOR) ||
            (DefaultCellStyle.ForeColor == _dataCellForeColor))
        {
            _dataCellForeColor = StateNormal.DataCell.Content.Color1;

            if (_dataCellForeColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _dataCellForeColor = StateNormal.DataCell.Content.GetContentShortTextColor1(state);
            }

            DefaultCellStyle.ForeColor = _dataCellForeColor;
        }
    }

    private void SyncSelForeColorCellStylesWithPalette()
    {
        PaletteState state = Enabled ? PaletteState.CheckedNormal : PaletteState.Disabled;

        if ((ColumnHeadersDefaultCellStyle.SelectionForeColor == GlobalStaticValues.EMPTY_COLOR) ||
            (ColumnHeadersDefaultCellStyle.SelectionForeColor == _columnSelForeColor))
        {
            _columnSelForeColor = StateSelected.HeaderColumn.Content.Color1;

            if (_columnSelForeColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _columnSelForeColor = StateSelected.HeaderColumn.Content.GetContentShortTextColor1(state);
            }

            ColumnHeadersDefaultCellStyle.SelectionForeColor = _columnSelForeColor;
        }

        if ((RowHeadersDefaultCellStyle.SelectionForeColor == GlobalStaticValues.EMPTY_COLOR) ||
            (RowHeadersDefaultCellStyle.SelectionForeColor == _rowSelForeColor))
        {
            _rowSelForeColor = StateSelected.HeaderRow.Content.Color1;

            if (_rowSelForeColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _rowSelForeColor = StateSelected.HeaderRow.Content.GetContentShortTextColor1(state);
            }

            RowHeadersDefaultCellStyle.SelectionForeColor = _rowSelForeColor;
        }

        if ((DefaultCellStyle.SelectionForeColor == GlobalStaticValues.EMPTY_COLOR) ||
            (DefaultCellStyle.SelectionForeColor == _dataCellSelForeColor))
        {
            _dataCellSelForeColor = StateSelected.DataCell.Content.Color1;

            if (_dataCellSelForeColor == GlobalStaticValues.EMPTY_COLOR)
            {
                _dataCellSelForeColor = StateSelected.DataCell.Content.GetContentShortTextColor1(state);
            }

            DefaultCellStyle.SelectionForeColor = _dataCellSelForeColor;
        }
    }

    private byte UpdateLocationForRowErrors(DataGridViewCellMouseEventArgs e,
        DataGridViewCell? cell,
        byte location)
    {
        // If over the main area of a row header cell...
        if ((cell is DataGridViewRowHeaderCell) && (location == 1))
        {
            // Check is really over the error icon area
            if (_rowCache.ContainsKey(e.RowIndex))
            {
                // Mark as location=2 which is over icon bounds
                if (_rowCache[e.RowIndex].Contains(new Point(e.X, e.Y)))
                {
                    location = 2;
                }
            }
        }

        return location;
    }

    private DataGridViewContentAlignment RelativeToAlign(PaletteRelativeAlign textH,
        PaletteRelativeAlign textV)
    {
        switch (textH)
        {
            case PaletteRelativeAlign.Near:
                switch (textV)
                {
                    case PaletteRelativeAlign.Near:
                        return DataGridViewContentAlignment.TopLeft;
                    case PaletteRelativeAlign.Center:
                        return DataGridViewContentAlignment.MiddleLeft;
                    case PaletteRelativeAlign.Far:
                        return DataGridViewContentAlignment.BottomLeft;
                }
                break;
            case PaletteRelativeAlign.Center:
                switch (textV)
                {
                    case PaletteRelativeAlign.Near:
                        return DataGridViewContentAlignment.TopCenter;
                    case PaletteRelativeAlign.Center:
                        return DataGridViewContentAlignment.MiddleCenter;
                    case PaletteRelativeAlign.Far:
                        return DataGridViewContentAlignment.BottomCenter;
                }
                break;
            case PaletteRelativeAlign.Far:
                switch (textV)
                {
                    case PaletteRelativeAlign.Near:
                        return DataGridViewContentAlignment.TopRight;
                    case PaletteRelativeAlign.Center:
                        return DataGridViewContentAlignment.MiddleRight;
                    case PaletteRelativeAlign.Far:
                        return DataGridViewContentAlignment.BottomRight;
                }
                break;
        }

        // Should never happen!
        Debug.Assert(false);
        DebugTools.NotImplemented(textH.ToString());
        return DataGridViewContentAlignment.MiddleLeft;
    }

    private PaletteDrawBorders GetCellMaxBorderEdges(Rectangle cellBounds,
        int column,
        int row)
    {
        // We always draw the bottom border and left/right depending on RTL setting
        PaletteDrawBorders maxBorders = PaletteDrawBorders.Bottom |
                                        (RightToLeftInternal ? PaletteDrawBorders.Left :
                                            PaletteDrawBorders.Right);

        // Do we need a top border
        if (!HideOuterBorders && ((row == -1) || ((row == 0) && !ColumnHeadersVisible)))
        {
            maxBorders |= PaletteDrawBorders.Top;
        }

        // Do we need a left/right border
        if (!HideOuterBorders && ((column == -1) || ((column == 0) && !RowHeadersVisible)))
        {
            maxBorders |= RightToLeftInternal ? PaletteDrawBorders.Right :
                PaletteDrawBorders.Left;
        }

        // Check if the cell is hard against the far or bottom edges, if so do not need to draw 
        // border that is hard against the edge as it will then look like it has double borders
        if (HideOuterBorders)
        {
            // With RTL we check the left border
            if (RightToLeftInternal)
            {
                if (cellBounds.Left == 0)
                {
                    maxBorders &= ~PaletteDrawBorders.Left;
                }
            }
            else
            {
                // Check the right border
                if (cellBounds.Right == Width)
                {
                    maxBorders &= ~PaletteDrawBorders.Right;
                }
            }

            // Check the bottom border
            if (cellBounds.Bottom == Height)
            {
                maxBorders &= ~PaletteDrawBorders.Bottom;
            }
        }

        return maxBorders;
    }

    private void ViewManagerLayout()
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager to use for laying out?
            if (ViewManager != null)
            {
                // Prevent infinite loop by looping a maximum number of times
                var max = 5;

                do
                {
                    // Layout cannot now be dirty
                    _layoutDirty = false;

                    // Ask the view to perform a layout
                    ViewManager.Layout(Renderer!);

                } while (_layoutDirty && (max-- > 0));

                // Remember size when last layout was performed
                _lastLayoutSize = Size;
            }
        }
    }

    private void CellDataAreaMouseEnterInternal(DataGridViewCell? cell)
    {
        Point currentCellAddress = CurrentCellAddress;

        if (!(cell is { RowIndex: >= 0, ColumnIndex: -1 }))
        {
            // Are we allowed to show a tooltip?
            if (ShowCellToolTips &&
                ((currentCellAddress.X == -1) || (currentCellAddress.X != cell!.ColumnIndex) ||
                 (currentCellAddress.Y != cell.RowIndex) || (EditingControl == null)))
            {
                // Grab the correct tooltip text for the cell
                _toolTipText = GetToolTipText(cell, cell!.RowIndex);

                // No explicit text provided?
                if (string.IsNullOrEmpty(_toolTipText))
                {
                    // Only interested in string values
                    if (cell.FormattedValueType == typeof(string))
                    {
                        // If for a data row and NOT the header
                        if ((cell.RowIndex != -1) && (cell.OwningColumn != null))
                        {
                            if ((cell.OwningColumn.Width < GetCellPreferredWidth(cell)) ||
                                (cell.OwningRow!.Height < GetCellPreferredHeight(cell)))
                            {
                                var editedValue = cell.GetEditedFormattedValue(cell.RowIndex, DataGridViewDataErrorContexts.Display) as string;
                                if (!string.IsNullOrEmpty(editedValue))
                                {
                                    _toolTipText = TruncateToolTipText(editedValue ?? string.Empty);
                                }
                            }
                        }
                        else if ((cell.RowIndex == -1) && (cell.ColumnIndex != -1) && _columnCache.ContainsKey(cell.ColumnIndex))
                        {
                            // If for a column cell and the contents do not fit...
                            if (!_columnCache[cell.ColumnIndex])
                            {
                                try
                                {
                                    var editedValue = cell.GetEditedFormattedValue(cell.RowIndex, DataGridViewDataErrorContexts.Display) as string;
                                    if (!string.IsNullOrEmpty(editedValue))
                                    {
                                        _toolTipText = TruncateToolTipText(editedValue ?? string.Empty);
                                    }
                                }
                                catch
                                {
                                    // ignored
                                }
                            }
                        }
                    }
                }

                // Restart the timer for showing the tooltip
                if (_showTimer != null)
                {
                    _showTimer.Stop();
                    _showTimer.Start();
                }
            }
            else
            {
                CellAreaMouseLeaveInternal();
            }
        }
    }

    private void CellErrorAreaMouseEnterInternal(DataGridViewCell? cell)
    {
        // Grab the correct error text for the cell
        _toolTipText = GetErrorText(cell, cell!.RowIndex);

        // Restart the timer for showing the error tooltip
        if (_showTimer != null)
        {
            _showTimer.Stop();
            _showTimer.Start();
        }
    }

    private void CellAreaMouseLeaveInternal()
    {
        // Stop the timer from showing a tooltip
        _showTimer?.Stop();

        // If there is a popup tooltip showing
        if (_visualPopupToolTip != null)
        {
            VisualPopupManager.Singleton.EndPopupTracking(_visualPopupToolTip);
        }
    }

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page any more
        _visualPopupToolTip = null;
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        // Only need a one tick timer
        if (_showTimer != null)
        {
            _showTimer.Stop();

            if (!string.IsNullOrEmpty(_toolTipText))
            {
                // Prevent the base class from showing a tooltip itself
                DismissBaseToolTips();

                // Remove any currently showing tooltip
                _visualPopupToolTip?.Dispose();

                // Create the actual tooltip popup object
                _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                    new ToolTipContent(_toolTipText),
                    Renderer!,
                    PaletteBackStyle.ControlToolTip,
                    PaletteBorderStyle.ControlToolTip,
                    PaletteContentStyle.LabelToolTip,
                    ToolTipShadow);

                _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;

                // Show relative to the provided screen point
                _visualPopupToolTip.ShowCalculatingSize(MousePosition);
            }
        }
    }

    private DataGridViewCell? GetCellInternal(int column, int row)
    {
        // Only need to cache reflection info the first time around
        if (_miGCI == null)
        {
            // Cache access to the internal method 'GetCellInternal'
            _miGCI = typeof(DataGridView).GetMethod(nameof(GetCellInternal), BindingFlags.Instance |
                                                                             BindingFlags.NonPublic |
                                                                             BindingFlags.GetField)!;
        }

        return _miGCI.Invoke(this, [column, row]) as DataGridViewCell;
    }

    private string GetToolTipText(DataGridViewCell? cell, int row)
    {
        // Only need to cache reflection info the first time around
        if (_miGTTT == null)
        {
            // Cache access to the internal get property 'GetToolTipText'
            _miGTTT = typeof(DataGridViewCell).GetMethod(nameof(GetToolTipText), BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.GetField)!;
        }

        try
        {
            return _miGTTT.Invoke(cell, [row]) as string ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    private string GetErrorText(DataGridViewCell? cell, int row)
    {
        // Only need to cache reflection info the first time around
        if (_miGET == null)
        {
            // Cache access to the internal get property 'GetErrorText'
            _miGET = typeof(DataGridViewCell).GetMethod(nameof(GetErrorText), BindingFlags.Instance |
                                                                              BindingFlags.NonPublic |
                                                                              BindingFlags.GetField)!;
        }

        try
        {
            return _miGET.Invoke(cell, [row]) as string ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    private byte CurrentMouseLocation(DataGridViewCell? cell)
    {
        // Only need to cache reflection info the first time around
        if (_piCML == null)
        {
            // Cache access to the internal get property 'CurrentMouseLocation'
            _piCML = typeof(DataGridViewCell).GetProperty(nameof(CurrentMouseLocation), BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.GetField)!;
        }

        // Grab the internal calculated value of the right to left setting
        return (byte)_piCML.GetValue(cell, null)!;
    }

    private int GetCellPreferredWidth([DisallowNull] DataGridViewCell? cell)
    {
        if (cell is null)
        {
            throw new ArgumentNullException(nameof(cell));
        }

        // Only need to cache reflection info the first time around
        if (_miGPW == null)
        {
            // Cache access to the internal method 'GetPreferredWidth' of cells
            _miGPW = typeof(DataGridViewCell).GetMethod(@"GetPreferredWidth", BindingFlags.Instance |
                                                                              BindingFlags.NonPublic |
                                                                              BindingFlags.GetField)!;
        }

        return (int)_miGPW.Invoke(cell, [cell.RowIndex!, cell.OwningRow!.Height!])!;
    }

    private int GetCellPreferredHeight(DataGridViewCell? cell)
    {
        if (cell is null)
        {
            throw new ArgumentNullException(nameof(cell));
        }

        // Only need to cache reflection info the first time around
        if (_miGPH == null)
        {
            // Cache access to the internal method 'GetPreferredHeight' of cells
            _miGPH = typeof(DataGridViewCell).GetMethod(@"GetPreferredHeight", BindingFlags.Instance |
                                                                               BindingFlags.NonPublic |
                                                                               BindingFlags.GetField)!;
        }

        return (int)_miGPH.Invoke(cell, [cell.RowIndex, cell.OwningColumn!.Width])!;
    }

    private string DismissBaseToolTips()
    {
        // Only need to cache reflection info the first time around
        if (_miATT == null)
        {
            // Cache access to the internal get property 'ActivateToolTip'
            _miATT = typeof(DataGridView).GetMethod(@"ActivateToolTip", BindingFlags.Instance |
                                                                        BindingFlags.NonPublic |
                                                                        BindingFlags.GetField)!;
        }

        return _miATT.Invoke(this, [false, string.Empty, -1, -1]) as string ?? string.Empty;
    }

    private string TruncateToolTipText(string toolTipText)
    {
        if (toolTipText.Length > 0x120)
        {
            var builder = new StringBuilder(toolTipText.Substring(0, 0x100), 0x103);
            builder.Append(@"...");
            return builder.ToString();
        }
        return toolTipText;
    }

    private void SetPalette(PaletteBase? palette)
    {
        if (palette != _palette)
        {
            // Unhook from current palette events
            if (_palette != null)
            {
                _palette.PalettePaint -= OnNeedResyncPaint;
                _palette.ButtonSpecChanged -= OnButtonSpecChanged;
            }

            // Remember the new palette
            _palette = palette;

            // Get the renderer associated with the palette
            Renderer = _palette?.GetRenderer();

            // Hook to new palette events
            if (_palette != null)
            {
                _palette.PalettePaint += OnNeedResyncPaint;
                _palette.ButtonSpecChanged += OnButtonSpecChanged;
            }

            // Ensure the current cell style values are in sync with the new 
            // palette setting and any state overrides that are defined
            SyncCellStylesWithPalette();
        }
    }

    private void PaintTransparentBackground(Graphics g, Rectangle clipRect)
    {
        // Get the parent control for transparent drawing purposes
        Control? parent = TransparentParent;

        // Do we have a parent control and we need to paint background?
        if ((parent != null) && NeedTransparentPaint)
        {
            // Only grab the required reference once
            if (_miPTB == null)
            {
                // Use reflection so we can call the Windows Forms internal method for painting parent background
                _miPTB = typeof(Control).GetMethod(nameof(PaintTransparentBackground),
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                    null, CallingConventions.HasThis,
                    [typeof(PaintEventArgs), typeof(Rectangle), typeof(Region)],
                    null)!;
            }

            _miPTB.Invoke(this, [new PaintEventArgs(g, clipRect), ClientRectangle, null!]);
        }
    }

    private void OnPerformRefresh()
    {
        // If we still need to perform the refresh
        if (_refresh)
        {
            // Perform the requested paint of the control
            Refresh();

            // If the layout is still dirty after the refresh
            if (_layoutDirty)
            {
                // Then non of the control is visible, so perform manual request
                // for a layout to ensure that child controls can be resized
                PerformLayout();

                // Need another repaint to take the layout change into account
                Refresh();
            }

            // Refresh request has been serviced
            _refresh = false;
            _refreshAll = false;
        }
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // We only care if we are using the global palette
        if (PaletteMode == PaletteMode.Global)
        {
            // Update ourself with the new global palette
            _localPalette = null;
            SetPalette(KryptonManager.CurrentGlobalPalette);
            Redirector.Target = _palette;
            SyncCellStylesWithPalette();

            // A new palette source means we need to layout and redraw
            OnNeedPaint(Palette!, new NeedLayoutEventArgs(true));
        }
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e) => OnNeedResyncPaint(Palette!, new NeedLayoutEventArgs(true));

    private void OnSyncPropertyChanged(object? sender, EventArgs e) =>
        // Ensure the current cell style values are in sync with the new palette 
        // setting and any state overrides that are defined.
        SyncCellStylesWithPalette();

    private void OnSyncBackPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Only interested in the first color from the background palettes
        if (e.PropertyName == "Color1")
        {
            // Ensure the current cell style values are in sync with the new palette 
            // setting and any state overrides that are defined.
            SyncCellStylesWithPalette();
        }
    }
    #endregion

    #region Menus
    private void OnContextMenuStripOpening(object? sender, CancelEventArgs e)
    {
        // Get the actual strip instance
        ContextMenuStrip? cms = base.ContextMenuStrip;

        // Make sure it has the correct renderer
        if (cms != null)
        {
            cms.Renderer = CreateToolStripRenderer();
        }
    }

    /// <summary>
    /// Create a tool strip renderer appropriate for the current renderer/palette pair.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ToolStripRenderer CreateToolStripRenderer() => Renderer?.RenderToolStrip(GetResolvedPalette()!)!;

    private void OnKryptonContextMenuDisposed(object? sender, EventArgs e) =>
        // When the current krypton context menu is disposed, we should remove 
        // it to prevent it being used again, as that would just throw an exception 
        // because it has been disposed.
        KryptonContextMenu = null;

    private void OnContextMenuClosed(object? sender, ToolStripDropDownClosedEventArgs e) => ContextMenuClosed();

    /// <summary>
    /// Called when a context menu has just been closed.
    /// </summary>
    protected virtual void ContextMenuClosed()
    {
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        // We need to snoop the need to show a context menu
        if (m.Msg == PI.WM_.CONTEXTMENU)
        {
            // Only interested in overriding the behaviour when we have a krypton context menu...
            if (KryptonContextMenu != null)
            {
                // Extract the screen mouse position (if might not actually be provided)
                var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                // If keyboard activated, the menu position is centered
                if (((int)(long)m.LParam) == -1)
                {
                    mousePt = new Point(Width / 2, Height / 2);
                }
                else
                {
                    mousePt = PointToClient(mousePt);

                    // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                    // of the showing context menu just like it happens for a ContextMenuStrip.
                    mousePt.X -= 1;
                    mousePt.Y -= 1;
                }

                // If the mouse position is within our client area
                if (ClientRectangle.Contains(mousePt))
                {
                    // Show the context menu
                    KryptonContextMenu.Show(this, PointToScreen(mousePt));

                    // We eat the message!
                    return;
                }
            }
        }

        base.WndProc(ref m);
    }
    #endregion menus
      
}