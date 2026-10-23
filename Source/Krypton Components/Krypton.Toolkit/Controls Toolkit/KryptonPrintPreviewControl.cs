#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, Lesandro, tobitege et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a PrintPreviewControl with Krypton styling applied.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPrintPreviewControl), "ToolboxBitmaps.KryptonPrintPreview.bmp")]
[DefaultEvent(nameof(StartPageChanged))]
[DefaultProperty(nameof(Document))]
[DesignerCategory(@"code")]
[Description(@"Displays a PrintDocument in a preview format with Krypton theming.")]
public class KryptonPrintPreviewControl : VisualControlBase
{
    #region Internal Classes

    private class InternalPrintPreviewControl : PrintPreviewControl
    {
        #region Instance Fields
    
        private readonly KryptonPrintPreviewControl _kryptonPrintPreviewControl;
        
        #endregion

        #region Identity
   
        /// <summary>
        /// Initialize a new instance of the InternalPrintPreviewControl class.
        /// </summary>
        /// <param name="kryptonPrintPreviewControl">Reference to owning control.</param>
        public InternalPrintPreviewControl(KryptonPrintPreviewControl kryptonPrintPreviewControl)
        {
            _kryptonPrintPreviewControl = kryptonPrintPreviewControl;

            // Remove border as we provide it via Krypton theming
            AutoZoom = false;
        }
        
        #endregion

        #region Protected
        
        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_.NCHITTEST:
                    if (_kryptonPrintPreviewControl.DesignMode)
                    {
                        m.Result = (IntPtr)PI.HT.TRANSPARENT;
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion
    }

    #endregion

    #region Instance Fields

    private readonly ViewLayoutDocker _drawDockerInner;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly InternalPrintPreviewControl _previewControl;
    private readonly PaletteDoubleRedirect _stateCommon;
    private readonly PaletteDouble? _stateDisabled;
    private readonly PaletteDouble? _stateNormal;

    private PaletteBackStyle _panelBackStyle;

    private bool _disposed;

    // Expose state fields to internal class
    internal PaletteDouble? _stateNormalInternal => _stateNormal;
    internal PaletteDouble? _stateDisabledInternal => _stateDisabled;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the starting page changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the starting page changes.")]
    public event EventHandler? StartPageChanged
    {
        add => _previewControl.StartPageChanged += value;
        remove => _previewControl.StartPageChanged -= value;
    }

#pragma warning disable CS0067 // Event is never used - hidden from designer to avoid confusion with base control
    /// <summary>
    /// Occurs when the value of the BackColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackColorChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImage property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImageLayout property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageLayoutChanged;

    /// <summary>
    /// Occurs when the value of the ForeColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? ForeColorChanged;
#pragma warning restore CS0067

    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the KryptonPrintPreviewControl class.
    /// </summary>
    public KryptonPrintPreviewControl()
    {
        // Cannot select this control, only the child PrintPreviewControl
        SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);

        _panelBackStyle = PaletteBackStyle.PanelAlternate;

        // Create the palette storage
        _stateCommon = new PaletteDoubleRedirect(Redirector!, _panelBackStyle, PaletteBorderStyle.ControlClient, NeedPaintDelegate);
        _stateDisabled = new PaletteDouble(_stateCommon, NeedPaintDelegate);
        _stateNormal = new PaletteDouble(_stateCommon, NeedPaintDelegate);

        // Create the internal print preview control
        _previewControl = new InternalPrintPreviewControl(this);

        // Set initial background color from palette
        UpdatePreviewControlBackColor();

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(_previewControl);

        // Create inner view for placing inside the drawing docker
        _drawDockerInner = new ViewLayoutDocker
        {
            { _layoutFill, ViewDockStyle.Fill }
        };

        // Create view for the control border and background
        _drawDockerOuter = new ViewDrawDocker(_stateNormal!.Back, _stateNormal!.Border)
        {
            { _drawDockerInner, ViewDockStyle.Fill }
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDockerOuter);

        // Add print preview control to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_previewControl);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the panel style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Panel style.")]
    [DefaultValue(PaletteBackStyle.PanelAlternate)]
    public PaletteBackStyle PanelBackStyle
    {
        get => _panelBackStyle;
        set
        {
            if (_panelBackStyle != value)
            {
                _panelBackStyle = value;
                _stateCommon.BackStyle = value;
                UpdatePreviewControlBackColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializePanelBackStyle() => PanelBackStyle != PaletteBackStyle.PanelAlternate;

    private void ResetPanelBackStyle() => PanelBackStyle = PaletteBackStyle.PanelAlternate;

    /// <summary>
    /// Gets access to the common print preview control appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common print preview control appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon.Back;

    private bool ShouldSerializeStateCommon() => !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled print preview control appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled print preview control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled!.Back;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled!.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal print preview control appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal print preview control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal!.Back;

    private bool ShouldSerializeStateNormal() => !_stateNormal!.Back.IsDefault;

    /// <summary>
    /// Gets access to the contained PrintPreviewControl instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public PrintPreviewControl PrintPreviewControl => _previewControl;

    /// <summary>
    /// Gets or sets the PrintDocument to preview.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The PrintDocument to preview.")]
    [DefaultValue(null)]
    public PrintDocument? Document
    {
        get => _previewControl.Document;
        set => _previewControl.Document = value;
    }

    /// <summary>
    /// Gets or sets the number of pages displayed horizontally across the page.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The number of pages displayed horizontally across the page.")]
    [DefaultValue(1)]
    public int Columns
    {
        get => _previewControl.Columns;
        set => _previewControl.Columns = value;
    }

    /// <summary>
    /// Gets or sets the number of pages displayed vertically down the page.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The number of pages displayed vertically down the page.")]
    [DefaultValue(1)]
    public int Rows
    {
        get => _previewControl.Rows;
        set => _previewControl.Rows = value;
    }

    /// <summary>
    /// Gets or sets the zoom level of the pages.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The zoom level of the pages.")]
    [DefaultValue(0.3)]
    public double Zoom
    {
        get => _previewControl.Zoom;
        set => _previewControl.Zoom = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control automatically resizes to fit its contents.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control automatically resizes to fit its contents.")]
    [DefaultValue(false)]
    public bool AutoZoom
    {
        get => _previewControl.AutoZoom;
        set => _previewControl.AutoZoom = value;
    }

    /// <summary>
    /// Gets or sets the starting page number.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The starting page number.")]
    [DefaultValue(0)]
    public int StartPage
    {
        get => _previewControl.StartPage;
        set => _previewControl.StartPage = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether anti-aliasing is used when rendering the page.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether anti-aliasing is used when rendering the page.")]
    [DefaultValue(true)]
    public bool UseAntiAlias
    {
        get => _previewControl.UseAntiAlias;
        set => _previewControl.UseAntiAlias = value;
    }

    /// <summary>
    /// Gets and sets if the control is in the tab chain.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool TabStop
    {
        get => _previewControl.TabStop;
        set => _previewControl.TabStop = value;
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets the background image displayed in the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Image? BackgroundImage
    {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
    }

    /// <summary>
    /// Gets or sets the background image layout as defined in the ImageLayout enumeration.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ImageLayout BackgroundImageLayout
    {
        get => base.BackgroundImageLayout;
        set => base.BackgroundImageLayout = value;
    }

    #endregion

    #region Protected

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        var state = Enabled ? _stateNormal! : _stateDisabled!;
        _drawDockerOuter.SetPalettes(state.Back, state.Border);

        // Update with latest enabled state
        _drawDockerOuter.Enabled = Enabled;
        _previewControl.Enabled = Enabled;

        // Update background color to match new state
        UpdatePreviewControlBackColor();

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        _stateCommon.SetRedirector(Redirector);
        UpdatePreviewControlBackColor();
        base.OnPaletteChanged(e);
    }

    /// <inheritdoc />
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);

        if (!IsDisposed && !Disposing)
        {
            // Only use layout logic if control is fully initialized or if being forced
            // to allow a relayout or if in design mode.
            if (IsHandleCreated || DesignMode)
            {
                Rectangle fillRect = _layoutFill.FillRect;

                // Position the internal print preview control to fill the available space
                if (fillRect.Width > 0 && fillRect.Height > 0)
                {
                    _previewControl.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
                }
            }
        }
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Updates the internal PrintPreviewControl's background color to match the themed background.
    /// </summary>
    private void UpdatePreviewControlBackColor()
    {
        if (_previewControl != null && !IsDisposed && !Disposing)
        {
            var backColor = Enabled
                ? _stateNormal!.Back.GetBackColor1(PaletteState.Normal)
                : _stateDisabled!.Back.GetBackColor1(PaletteState.Disabled);

            _previewControl.BackColor = backColor;
        }
    }

    #endregion

    #region Disposal

    /// <inheritdoc />
    protected override void Dispose(bool isDisposing)
    {
        if (!_disposed)
        {
            if (isDisposing)
            {
                _previewControl?.Dispose();
            }

            _disposed = true;
        }
    }

    ~KryptonPrintPreviewControl() => Dispose(false);

    public new void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    #endregion
}
