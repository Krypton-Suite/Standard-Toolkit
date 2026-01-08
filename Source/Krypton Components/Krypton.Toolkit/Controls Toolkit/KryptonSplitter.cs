#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Splitter = System.Windows.Forms.Splitter;

namespace Krypton.Toolkit;

/// <summary>
/// Enables the user to resize docked controls with Krypton integration.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(Splitter))]
[DefaultEvent(nameof(SplitterMoved))]
[DefaultProperty(nameof(Dock))]
[DesignerCategory(@"code")]
[Description(@"Enables the user to resize docked controls.")]
public class KryptonSplitter : Splitter
{
    #region Instance Fields
    
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the KryptonSplitter class.
    /// </summary>
    public KryptonSplitter()
    {
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

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
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        }

        base.Dispose(disposing);
    }
    
    #endregion

    #region Public
    
    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;
        set
        {
            if (_paletteMode != value)
            {
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        Invalidate();
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;
        set
        {
            if (_palette != value)
            {
                _palette = value;

                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    _paletteMode = PaletteMode.Custom;
                }

                Invalidate();
            }
        }
    }
    
    #endregion

    #region Implementation
    
    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            Invalidate();
        }
    }

    #endregion
}
