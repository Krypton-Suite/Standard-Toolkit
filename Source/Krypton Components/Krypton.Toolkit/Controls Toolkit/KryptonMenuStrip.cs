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
[ToolboxBitmap(typeof(MenuStrip), "ToolboxBitmaps.KryptonMenuBar.bmp")]
[Description(@"A Krypton based menu strip.")]
[DesignerCategory(@"code")]
[ToolboxItem(true)]
public class KryptonMenuStrip : MenuStrip,
    IFocusLostMenuItem
{
    #region Instance Fields
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private PaletteBackInheritMenuStrip? _inherit;
    private readonly PaletteBack _stateCommon;
    private readonly PaletteBack _stateDisabled;
    private readonly PaletteBack _stateNormal;
    private ToolStripFontSyncHelper? _fontSync;
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

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        _fontSync = new ToolStripFontSyncHelper(this, ToolStripFontKind.MenuStrip, GetCurrentPalette);

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

            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            _fontSync?.Dispose();
            _fontSync = null;

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
                        UpdateInherit();
                        UpdateAppearance();
                        _fontSync?.OnPaletteSourceChanged(_paletteMode == PaletteMode.Global);
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

                UpdateInherit();
                UpdateAppearance();
                _fontSync?.OnPaletteSourceChanged(_paletteMode == PaletteMode.Global);
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

    private PaletteBase? GetCurrentPalette() => _palette ?? KryptonManager.CurrentGlobalPalette;

    private void UpdateInherit()
    {
        var currentPalette = GetCurrentPalette();
        if (_inherit != null)
        {
            _inherit.SetPalette(currentPalette);
        }
        else
        {
            _inherit = new PaletteBackInheritMenuStrip(currentPalette);
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
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateInherit();
            UpdateAppearance();
        }

        _fontSync?.OnPaletteSourceChanged(_paletteMode == PaletteMode.Global);
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
