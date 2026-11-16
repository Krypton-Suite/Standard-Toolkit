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
/// Provides a PrintDocument with Krypton theming support for printing with palette colors.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(PrintDocument), "ToolboxBitmaps.KryptonPrintDocument.bmp")]
[DefaultProperty(nameof(DocumentName))]
[DesignerCategory(@"code")]
[Description(@"Represents a document to be printed with Krypton theming support.")]
public class KryptonPrintDocument : PrintDocument
{
    #region Instance Fields

    private PaletteBase? _palette;
    private PaletteMode _paletteMode = PaletteMode.Global;
    private PaletteBase? _localPalette;
    private PaletteContentStyle _textStyle = PaletteContentStyle.LabelNormalPanel;
    private PaletteBackStyle _backgroundStyle = PaletteBackStyle.PanelClient;
    private bool _usePaletteColors = true;

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
    /// Initialize a new instance of the KryptonPrintDocument class.
    /// </summary>
    public KryptonPrintDocument()
    {
        // Set palette to global default
        SetPalette(KryptonManager.CurrentGlobalPalette);
        _paletteMode = PaletteMode.Global;

        // Subscribe to global palette changes
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
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the palette to be applied.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Palette applied to printing.")]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;
        set
        {
            if (_paletteMode != value)
            {
                _paletteMode = value;
                _localPalette = null;

                if (value == PaletteMode.Custom)
                {
                    // Do nothing, you must assign a palette to the 'Palette' property
                }
                else
                {
                    SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));
                    OnPaletteChanged(EventArgs.Empty);
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
    [Description(@"Custom palette applied to printing.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
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
                }

                // If real change has occurred
                if (old != _localPalette)
                {
                    // Raise the change event
                    OnPaletteChanged(EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use palette colors when printing.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether to use palette colors when printing.")]
    [DefaultValue(true)]
    public bool UsePaletteColors
    {
        get => _usePaletteColors;
        set => _usePaletteColors = value;
    }

    /// <summary>
    /// Gets or sets the text style used for retrieving text colors from the palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text style used for retrieving text colors from the palette.")]
    [DefaultValue(PaletteContentStyle.LabelNormalPanel)]
    public PaletteContentStyle TextStyle
    {
        get => _textStyle;
        set => _textStyle = value;
    }

    /// <summary>
    /// Gets or sets the background style used for retrieving background colors from the palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background style used for retrieving background colors from the palette.")]
    [DefaultValue(PaletteBackStyle.PanelClient)]
    public PaletteBackStyle BackgroundStyle
    {
        get => _backgroundStyle;
        set => _backgroundStyle = value;
    }

    #endregion

    #region Themed Printing Helpers

    /// <summary>
    /// Gets the text color from the current palette.
    /// </summary>
    /// <param name="state">The palette state to use.</param>
    /// <returns>The text color from the palette, or Black if palette is not available.</returns>
    public Color GetTextColor(PaletteState state = PaletteState.Normal)
    {
        if (!_usePaletteColors || _palette == null)
        {
            return Color.Black;
        }

        try
        {
            return _palette.GetContentShortTextColor1(_textStyle, state);
        }
        catch
        {
            return Color.Black;
        }
    }

    /// <summary>
    /// Gets the background color from the current palette.
    /// </summary>
    /// <param name="state">The palette state to use.</param>
    /// <returns>The background color from the palette, or White if palette is not available.</returns>
    public Color GetBackgroundColor(PaletteState state = PaletteState.Normal)
    {
        if (!_usePaletteColors || _palette == null)
        {
            return Color.White;
        }

        try
        {
            return _palette.GetBackColor1(_backgroundStyle, state);
        }
        catch
        {
            return Color.White;
        }
    }

    /// <summary>
    /// Gets the border color from the current palette.
    /// </summary>
    /// <param name="state">The palette state to use.</param>
    /// <returns>The border color from the palette, or Black if palette is not available.</returns>
    public Color GetBorderColor(PaletteState state = PaletteState.Normal)
    {
        if (!_usePaletteColors || _palette == null)
        {
            return Color.Black;
        }

        try
        {
            return _palette.GetBorderColor1(PaletteBorderStyle.ControlClient, state);
        }
        catch
        {
            return Color.Black;
        }
    }

    /// <summary>
    /// Gets the font from the current palette.
    /// </summary>
    /// <param name="state">The palette state to use.</param>
    /// <returns>The font from the palette, or a default font if palette is not available.</returns>
    public Font GetFont(PaletteState state = PaletteState.Normal)
    {
        if (!_usePaletteColors || _palette == null)
        {
            return new Font("Arial", 12);
        }

        try
        {
            return _palette.GetContentShortTextFont(_textStyle, state) ?? new Font("Arial", 12);
        }
        catch
        {
            return new Font("Arial", 12);
        }
    }

    /// <summary>
    /// Draws text using palette colors.
    /// </summary>
    /// <param name="graphics">The Graphics object to draw on.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="font">The font to use (if null, uses palette font).</param>
    /// <param name="bounds">The bounding rectangle for the text.</param>
    /// <param name="format">The StringFormat to use.</param>
    /// <param name="state">The palette state to use.</param>
    public void DrawThemedText(Graphics graphics, string text, Font? font, Rectangle bounds,
        StringFormat? format = null, PaletteState state = PaletteState.Normal)
    {
        if (graphics == null || string.IsNullOrEmpty(text))
        {
            return;
        }

        var textColor = GetTextColor(state);
        var textFont = font ?? GetFont(state);

        using var brush = new SolidBrush(textColor);
        graphics.DrawString(text, textFont, brush, bounds, format ?? StringFormat.GenericDefault);
    }

    /// <summary>
    /// Draws a rectangle with themed background and border.
    /// </summary>
    /// <param name="graphics">The Graphics object to draw on.</param>
    /// <param name="bounds">The bounding rectangle.</param>
    /// <param name="state">The palette state to use.</param>
    public void DrawThemedRectangle(Graphics graphics, Rectangle bounds, PaletteState state = PaletteState.Normal)
    {
        if (graphics == null)
        {
            return;
        }

        var backColor = GetBackgroundColor(state);
        var borderColor = GetBorderColor(state);

        using (var brush = new SolidBrush(backColor))
        {
            graphics.FillRectangle(brush, bounds);
        }

        using (var pen = new Pen(borderColor))
        {
            graphics.DrawRectangle(pen, bounds);
        }
    }

    /// <summary>
    /// Draws a line using the border color from the palette.
    /// </summary>
    /// <param name="graphics">The Graphics object to draw on.</param>
    /// <param name="x1">The x-coordinate of the first point.</param>
    /// <param name="y1">The y-coordinate of the first point.</param>
    /// <param name="x2">The x-coordinate of the second point.</param>
    /// <param name="y2">The y-coordinate of the second point.</param>
    /// <param name="state">The palette state to use.</param>
    public void DrawThemedLine(Graphics graphics, int x1, int y1, int x2, int y2,
        PaletteState state = PaletteState.Normal)
    {
        if (graphics == null)
        {
            return;
        }

        var lineColor = GetBorderColor(state);
        using var pen = new Pen(lineColor);
        graphics.DrawLine(pen, x1, y1, x2, y2);
    }

    /// <summary>
    /// Gets a SolidBrush with the text color from the palette.
    /// </summary>
    /// <param name="state">The palette state to use.</param>
    /// <returns>A SolidBrush with the palette text color.</returns>
    public Brush GetTextBrush(PaletteState state = PaletteState.Normal)
    {
        return new SolidBrush(GetTextColor(state));
    }

    /// <summary>
    /// Gets a SolidBrush with the background color from the palette.
    /// </summary>
    /// <param name="state">The palette state to use.</param>
    /// <returns>A SolidBrush with the palette background color.</returns>
    public Brush GetBackgroundBrush(PaletteState state = PaletteState.Normal)
    {
        return new SolidBrush(GetBackgroundColor(state));
    }

    /// <summary>
    /// Gets a Pen with the border color from the palette.
    /// </summary>
    /// <param name="state">The palette state to use.</param>
    /// <param name="width">The pen width.</param>
    /// <returns>A Pen with the palette border color.</returns>
    public Pen GetBorderPen(PaletteState state = PaletteState.Normal, float width = 1.0f)
    {
        return new Pen(GetBorderColor(state), width);
    }

    #endregion

    #region Protected

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnPaletteChanged(EventArgs e) => PaletteChanged?.Invoke(this, e);

    #endregion

    #region Private

    /// <summary>
    /// Sets the palette to the specified value.
    /// </summary>
    /// <param name="palette">The palette to set.</param>
    private void SetPalette(PaletteBase? palette)
    {
        _palette = palette;
    }

    /// <summary>
    /// Handles the global palette changed event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            SetPalette(KryptonManager.CurrentGlobalPalette);
            OnPaletteChanged(EventArgs.Empty);
        }
    }

    #endregion
}
