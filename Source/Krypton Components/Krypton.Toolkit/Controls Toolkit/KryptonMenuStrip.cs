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
/// A Krypton based menu strip.
/// </summary>
[ToolboxBitmap(typeof(MenuStrip))]
[Description(@"A Krypton based menu strip.")]
[DesignerCategory(@"code")]
[ToolboxItem(true)]
public class KryptonMenuStrip : MenuStrip,
    IFocusLostMenuItem
{
    #region Instance Fields
    private PaletteBase? _palette;
    private PaletteBase? _hookedPalette;
    private PaletteBase? _hookedGlobalPalette;
    private PaletteMode _paletteMode;
    private PaletteBackInheritMenuStrip? _inherit;
    private readonly PaletteBack _stateCommon;
    private readonly PaletteBack _stateDisabled;
    private readonly PaletteBack _stateNormal;
    private bool _disposed;
    #endregion

    #region Constructor
    /// <summary>
    /// Initialize a new instance of the KryptonMenuStrip class.
    /// </summary>
    public KryptonMenuStrip()
    {
        // Use Krypton
        RenderMode = ToolStripRenderMode.ManagerRenderMode;

        // Set initial palette mode
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create palette storage for per-control overrides, inheriting defaults from current palette ColorTable
        _inherit = new PaletteBackInheritMenuStrip(_palette);
        _stateCommon = new PaletteBack(_inherit, OnNeedPaint);
        _stateDisabled = new PaletteBack(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteBack(_stateCommon, OnNeedPaint);

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Hook into palette paint events to update font
        HookPaletteEvents();

        // Always listen to global palette for BaseFont changes
        HookGlobalPaletteEvents();

        // Set initial font from palette
        UpdateFont();

        // Register with the FocusLostMenuHelper
        Register(this);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            // Deregister from the FocusLostMenuHelper
            Deregister(this);

            // Unhook from events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            // Unhook from palette events
            if (_hookedPalette != null)
            {
                _hookedPalette.PalettePaintInternal -= OnPalettePaint;
            }

            // Unhook from global palette events
            if (_hookedGlobalPalette != null)
            {
                _hookedGlobalPalette.PalettePaintInternal -= OnGlobalPalettePaint;
            }

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Overrides
    
    /// <summary>
    /// Raises the RendererChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnRendererChanged(EventArgs e)
    {
        base.OnRendererChanged(e);
        Invalidate();
    }

    /*/// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [AmbientValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value;
    }*/
    
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
                        HookPaletteEvents();
                        UpdateInherit();
                        UpdateAppearance();
                        UpdateFont();
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

                HookPaletteEvents();
                UpdateInherit();
                UpdateAppearance();
                UpdateFont();
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && _palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        _palette = null;
    }
    
    #endregion

    #region Visual States
    
    /// <summary>
    /// Gets access to the common menu strip appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common menu strip appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon;

    private bool ShouldSerializeStateCommon() => !_stateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled menu strip appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled menu strip appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal menu strip appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal menu strip appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal;

    private bool ShouldSerializeStateNormal() => !_stateNormal.IsDefault;

    #endregion

    #region Implementation

    private void UpdateInherit()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        if (_inherit != null)
        {
            _inherit.SetPalette(currentPalette);
        }
        else
        {
            _inherit = new PaletteBackInheritMenuStrip(currentPalette);
        }
    }

    private void HookPaletteEvents()
    {
        // Get current palette
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;

        // Only unhook/hook if palette has changed
        if (_hookedPalette != currentPalette)
        {
            // Unhook from old palette events (if any)
            if (_hookedPalette != null)
            {
                _hookedPalette.PalettePaintInternal -= OnPalettePaint;
            }

            // Hook into new palette events
            if (currentPalette != null)
            {
                currentPalette.PalettePaintInternal += OnPalettePaint;
            }

            // Remember the currently hooked palette
            _hookedPalette = currentPalette;
        }
    }

    private void HookGlobalPaletteEvents()
    {
        // Always listen to CurrentGlobalPalette for BaseFont changes
        var currentGlobalPalette = KryptonManager.CurrentGlobalPalette;

        // Only unhook/hook if global palette has changed
        if (_hookedGlobalPalette != currentGlobalPalette)
        {
            // Unhook from old global palette events (if any)
            if (_hookedGlobalPalette != null)
            {
                _hookedGlobalPalette.PalettePaintInternal -= OnGlobalPalettePaint;
            }

            // Hook into new global palette events
            if (currentGlobalPalette != null)
            {
                currentGlobalPalette.PalettePaintInternal += OnGlobalPalettePaint;
            }

            // Remember the currently hooked global palette
            _hookedGlobalPalette = currentGlobalPalette;
        }
    }

    private void UpdateAppearance()
    {
        // Force repaint when palette changes
        Invalidate();
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (!IsDisposed)
        {
            if (e != null && e.NeedLayout)
            {
                PerformLayout();
            }
            Invalidate();
        }
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Re-hook global palette events in case the global palette instance changed
        HookGlobalPaletteEvents();

        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            HookPaletteEvents();
            UpdateInherit();
            UpdateAppearance();
            UpdateFont();
        }
        else
        {
            // Even if not using global palette, BaseFont changes might affect us
            // Update font to ensure it reflects any BaseFont changes
            UpdateFont();
        }
    }

    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        // Update font when palette changes (e.g., BaseFont changes)
        if (!IsDisposed)
        {
            UpdateFont();
        }
    }

    private void OnGlobalPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        // Update font immediately when global palette BaseFont changes
        if (!IsDisposed)
        {
            UpdateFont();
        }
    }

    private void UpdateFont()
    {
        if (!IsDisposed)
        {
            try
            {
                var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
                if (currentPalette != null)
                {
                    // Force ColorTable refresh to ensure ToolStripFont is up to date
                    var colorTable = currentPalette.ColorTable;
                    var toolStripFont = colorTable.ToolStripFont;
                    if (Font != toolStripFont)
                    {
                        Font = toolStripFont;
                    }
                }
            }
            catch
            {
                // Ignore errors accessing ColorTable (may not be initialized yet)
            }
        }
    }
    #endregion

    #region IFocusLostMenuItem
    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ProcessItem()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] is ToolStripMenuItem dropDownItem
                && dropDownItem.DropDown.Visible)
            {
                dropDownItem.DropDown.Close(ToolStripDropDownCloseReason.AppFocusChange);
                return;
            }
        }
    }

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Register(IFocusLostMenuItem item)
    {
        FocusLostMenuHelper.Register(item);
    }

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deregister(IFocusLostMenuItem item)
    {
        FocusLostMenuHelper.Deregister(item);
    }
    #endregion

}
