#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a container for tool strips with Krypton theming support.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(ToolStripContainer))]
[DefaultEvent(nameof(ContentPanel))]
[DefaultProperty(nameof(ContentPanel))]
[DesignerCategory(@"code")]
[Description(@"Provides a container for tool strips with Krypton theming support.")]
public class KryptonToolStripContainer : ToolStripContainer
{
    #region Instance Fields

    private PaletteRedirect? _redirector;
    private PaletteDoubleRedirect? _stateCommon;
    private PaletteDouble? _stateDisabled;
    private PaletteDouble? _stateNormal;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private bool _contentPanelPainted;
    
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the KryptonToolStripContainer class.
    /// </summary>
    public KryptonToolStripContainer()
    {
        // Create redirector to access the global palette
        _redirector = new PaletteRedirect(KryptonManager.CurrentGlobalPalette);

        // Create the palette storage
        _stateCommon = new PaletteDoubleRedirect(_redirector, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, OnNeedPaint);
        _stateDisabled = new PaletteDouble(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteDouble(_stateCommon, OnNeedPaint);

        // Set initial palette mode
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Hook into ContentPanel paint to apply Krypton theming
        ContentPanel.Paint += OnContentPanelPaint;

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
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
            ContentPanel.Paint -= OnContentPanelPaint;
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
                        break;
                }

                // Update redirector to point at new palette
                if (_redirector != null)
                {
                    _redirector.Target = _palette;
                }

                // Need to repaint to show change
                Invalidate();
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

                // Update redirector to point at new palette
                if (_redirector != null)
                {
                    _redirector.Target = _palette;
                }

                // Need to repaint to show change
                Invalidate();
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
    /// Gets and sets the container background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Container background style.")]
    public PaletteBackStyle ContainerBackStyle
    {
        get => _stateCommon?.BackStyle ?? PaletteBackStyle.PanelClient;

        set
        {
            if (_stateCommon != null && _stateCommon.BackStyle != value)
            {
                _stateCommon.BackStyle = value;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeContainerBackStyle() => ContainerBackStyle != PaletteBackStyle.PanelClient;

    private void ResetContainerBackStyle() => ContainerBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets access to the common container appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common container appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon?.Back ?? throw new ObjectDisposedException(nameof(KryptonToolStripContainer));

    private bool ShouldSerializeStateCommon() => _stateCommon != null && !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled container appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled container appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled?.Back ?? throw new ObjectDisposedException(nameof(KryptonToolStripContainer));

    private bool ShouldSerializeStateDisabled() => _stateDisabled != null && !_stateDisabled.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal container appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal container appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal?.Back ?? throw new ObjectDisposedException(nameof(KryptonToolStripContainer));

    private bool ShouldSerializeStateNormal() => _stateNormal != null && !_stateNormal.Back.IsDefault;

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        // Not implemented for ToolStripContainer
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
        // Need to repaint ContentPanel when enabled state changes
        ContentPanel.Invalidate();
        base.OnEnabledChanged(e);
    }
    
    #endregion

    #region Implementation
    
    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        // Need to repaint ContentPanel when palette changes
        ContentPanel.Invalidate();
    }

    private void OnContentPanelPaint(object? sender, PaintEventArgs e)
    {
        if (_contentPanelPainted || _stateNormal == null || _stateDisabled == null)
        {
            return;
        }

        _contentPanelPainted = true;

        try
        {
            // Get the appropriate palette state based on enabled state
            var paletteState = Enabled ? PaletteState.Normal : PaletteState.Disabled;
            var paletteBack = Enabled ? _stateNormal.Back : _stateDisabled.Back;

            // Check if we should draw the background
            if (paletteBack.GetBackDraw(paletteState) == InheritBool.True)
            {
                // Get background colors from palette
                var backColor1 = paletteBack.GetBackColor1(paletteState);
                var backColor2 = paletteBack.GetBackColor2(paletteState);

                // Draw the background based on style
                if (backColor1 != Color.Empty && backColor2 != Color.Empty && backColor1 != backColor2)
                {
                    // Gradient background
                    using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        ContentPanel.ClientRectangle,
                        backColor1,
                        backColor2,
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    e.Graphics.FillRectangle(brush, ContentPanel.ClientRectangle);
                }
                else if (backColor1 != Color.Empty)
                {
                    // Solid background
                    using var brush = new SolidBrush(backColor1);
                    e.Graphics.FillRectangle(brush, ContentPanel.ClientRectangle);
                }
            }
        }
        finally
        {
            _contentPanelPainted = false;
        }
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            if (_redirector != null)
            {
                _redirector.Target = _palette;
            }

            Invalidate();
        }
    }

    #endregion
}