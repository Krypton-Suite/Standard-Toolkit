#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Represents a single tab page in a KryptonTabControl.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(TabPage))]
[DesignerCategory(@"code")]
[Description(@"Represents a single tab page in a KryptonTabControl.")]
public class KryptonTabPage : TabPage
{
    #region Instance Fields
    
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private PaletteRedirect? _redirector;
    private PaletteDoubleRedirect? _stateCommon;
    private PaletteDouble? _stateDisabled;
    private PaletteDouble? _stateNormal;
  
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the KryptonTabPage class.
    /// </summary>
    public KryptonTabPage()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);

        // Set initial palette mode
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create redirector to access the global palette
        _redirector = new PaletteRedirect(_palette);

        // Create the palette storage
        _stateCommon = new PaletteDoubleRedirect(_redirector, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, OnNeedPaint);
        _stateDisabled = new PaletteDouble(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteDouble(_stateCommon, OnNeedPaint);

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Update background from palette
        UpdateBackColor();
    }

    /// <summary>
    /// Initialize a new instance of the KryptonTabPage class with text.
    /// </summary>
    /// <param name="text">The text to display on the tab page.</param>
    public KryptonTabPage(string text)
        : this()
    {
        Text = text;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            // Clean up palette objects
            _stateNormal = null;
            _stateDisabled = null;
            _stateCommon = null;
            _redirector = null;
            _palette = null;
        }

        base.Dispose(disposing);
    }
    
    #endregion

    #region Public
    
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action depends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        // Use the one of the built in palettes
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        UpdateRedirector();
                        UpdateBackColor();
                        break;
                }
            }
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;

        set
        {
            // Only interested in changes of value
            if (_palette != value)
            {
                // Remember new palette
                _palette = value;

                // If no custom palette provided, then must be using a built in palette
                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    // No longer using a built in palette
                    _paletteMode = PaletteMode.Custom;
                }

                UpdateRedirector();
                UpdateBackColor();
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && _palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        _palette = null;
    }

    /// <summary>
    /// Gets and sets the tab page background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab page background style.")]
    public PaletteBackStyle TabPageBackStyle
    {
        get => _stateCommon?.BackStyle ?? PaletteBackStyle.PanelClient;

        set
        {
            if (_stateCommon != null && _stateCommon.BackStyle != value)
            {
                _stateCommon.BackStyle = value;
                UpdateBackColor();
            }
        }
    }

    private bool ShouldSerializeTabPageBackStyle() => TabPageBackStyle != PaletteBackStyle.PanelClient;

    private void ResetTabPageBackStyle() => TabPageBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets access to the common tab page appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common tab page appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabPage));

    private bool ShouldSerializeStateCommon() => _stateCommon != null && !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled tab page appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled tab page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabPage));

    private bool ShouldSerializeStateDisabled() => _stateDisabled != null && !_stateDisabled.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal tab page appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal tab page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabPage));

    private bool ShouldSerializeStateNormal() => _stateNormal != null && !_stateNormal.Back.IsDefault;

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        // Not implemented for TabPage
        // This method is provided for API consistency with other Krypton controls
    }
    
    #endregion

    #region Protected Overrides
   
    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        UpdateBackColor();
    }
  
    #endregion

    #region Implementation
    
    private void UpdateRedirector()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        if (_redirector != null)
        {
            _redirector.Target = currentPalette;
        }
        else
        {
            _redirector = new PaletteRedirect(currentPalette);
            if (_stateCommon != null)
            {
                _stateCommon.SetRedirector(_redirector);
            }
        }
    }

    private void UpdateBackColor()
    {
        if (_stateNormal == null || _stateDisabled == null)
        {
            return;
        }

        var paletteState = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        var paletteBack = Enabled ? _stateNormal.Back : _stateDisabled.Back;

        // Get background color from palette
        if (paletteBack.GetBackDraw(paletteState) == InheritBool.True)
        {
            var backColor1 = paletteBack.GetBackColor1(paletteState);
            if (backColor1 != Color.Empty)
            {
                base.BackColor = backColor1;
            }
        }
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        UpdateBackColor();
        Invalidate();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateRedirector();
            UpdateBackColor();
        }
    }

    #endregion
}

