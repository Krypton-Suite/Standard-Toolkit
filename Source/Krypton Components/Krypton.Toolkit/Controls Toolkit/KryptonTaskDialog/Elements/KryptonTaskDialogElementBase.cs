#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 * © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Abstract base class where all KryptonTaskDialogElementData components must be derived from.
/// Derived elements can be extended through the use of IKryptonTaskDialogElement interfaces to 
/// guarantee correct and consistent definitions and naming of properties and methods.
/// 
/// The IDisposable pattern has been implemented by default so derived classes can override and implement if required
/// </summary>
public abstract class KryptonTaskDialogElementBase : 
    IKryptonTaskDialogElementBase,
    IKryptonTaskDialogElementEventSizeChanged,
    IDisposable
{
    #region Fields
    private bool _disposed;
    private KryptonPanel _panel;
    private bool _panelVisible;
    private KryptonTaskDialogDefaults _taskDialogDefaults;
    private Bitmap _separator;
    private (int FontMinValue, int FontMaxValue, int Adjust75, int Adjust50, int Adjust25) _separatorValues;
    #endregion

    #region Events
    /// <summary>
    /// Subscribers will be notified when the visibility of the element has changed.
    /// </summary>
    public event Action VisibleChanged;

    /// <summary>
    /// Subscribers will be notified when size of the element has changed.
    /// </summary>
    public event Action SizeChanged;
    #endregion

    #region Indentity
    /// <summary>
    /// Default constructor
    /// </summary>
    public KryptonTaskDialogElementBase(KryptonTaskDialogDefaults taskDialogDefaults)
    {
        // Set the data
        _taskDialogDefaults = taskDialogDefaults;
        _disposed = false;

        _separatorValues.FontMinValue = 50;
        _separatorValues.FontMaxValue = 255 - 50;
        _separatorValues.Adjust75 = 75;
        _separatorValues.Adjust50 = 50;
        _separatorValues.Adjust25 = 25;

        // Although OnGlobalPaletteChanged synchronizes palette changes with the elements,
        // The initialisation is done here.
        Palette = KryptonManager.CurrentGlobalPalette;
        // From there the event handler will take over.
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        _panel = new()
        {
            Margin = _taskDialogDefaults.NullPadding,
            Padding = _taskDialogDefaults.NullMargin,
            Visible = false

        };

        _panelVisible =  false;
        _separator    =  CreateSeparator();
        _panel.Paint += OnPanelPaint;
        Palette.PalettePaint += OnPalettePaint;

        LayoutDirty = false;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Virtual method that invokes the SizeChanged event and notifies subscribers.
    /// </summary>
    /// <param name="performLayout">If a layout needs to be performed regardless of the element's visible state.</param>
    protected virtual void OnSizeChanged(bool performLayout = false)
    {
        if (!performLayout)
        {
            SizeChanged?.Invoke();
        }
    }
    #endregion

    #region Protected (virtual)
    /// <summary>
    /// Will be connented to and fired from the active palette.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event data.</param>
    protected virtual void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
    }

    /// <summary>
    /// Acts on Krypton Manager palette changes.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event data.</param>
    protected virtual void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (Palette is not null)
        {
            Palette.PalettePaint -= OnPalettePaint;
        }

        Palette = KryptonManager.CurrentGlobalPalette;

        if (Palette is not null)
        {
            Palette.PalettePaint += OnPalettePaint;
            _separator = CreateSeparator();
        }
    }
    #endregion

    #region Internal (virtual)
    /// <summary>
    /// Requests a layout of the element.<br/>
    /// Practically this is used to update the element before it is shown.<br/>
    /// </summary>
    internal virtual void PerformLayout()
    {
    }

    /// <summary>
    /// Krypton panel that hosts the Element's controls and used for themed background coloring.
    /// </summary>
    internal KryptonPanel Panel => _panel;

    /// <summary>
    /// Access to the active global palette.
    /// </summary>
    internal PaletteBase Palette { get; private set; }
    #endregion

    #region Public virtual
    /// <inheritdoc/>
    public virtual Color BackColor1 
    {
        get => Panel.StateCommon.Color1;
        set
        {
            Panel.StateCommon.Color1 = value;
            OnBackColorsChanged();
        }
    }

    /// <inheritdoc/>
    public virtual Color BackColor2 
    {
        get => Panel.StateCommon.Color2;
        set
        {
            Panel.StateCommon.Color2 = value;
            OnBackColorsChanged();
        }
    }

    /// <inheritdoc/>
    public virtual bool Visible 
    {
        get => _panelVisible;
        set
        {  
            if (_panelVisible != value)
            {
                _panelVisible = value;
                _panel.Visible = _panelVisible;
                OnVisibleChanged();
            }
        }
    }
    #endregion

    #region public override
    /// <summary>
    /// Not implemented
    /// </summary>
    /// <returns>String.Empty</returns>
    public sealed override string ToString()
    {
        return string.Empty;
    }
    #endregion

    #region Public
    /// <summary>
    /// Displays a separator line at the top of the element.
    /// </summary>
    public bool ShowSeparator
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                _panel.Invalidate();
            }
        }
    }

    /// <summary>
    /// Returns if the element's layout needs a refresh when visible or before the dialog will be displayed.
    /// </summary>
    internal bool LayoutDirty { get; set; }

    /// <summary>
    /// Return the height of the element when visible.<br/>
    /// When not visible Height returns zero.
    /// </summary>
    public int Height => _panelVisible ? Panel.Height : 0;

    /// <inheritdoc/>
    internal KryptonTaskDialogDefaults Defaults => _taskDialogDefaults;
    #endregion

    #region Private
    private void OnPanelPaint(object? sender, PaintEventArgs e)
    {
        PaintSeparator();
    }

    private void OnBackColorsChanged()
    {
        if (BackColor1 != Color.Empty && BackColor2 != Color.Empty)
        {
            //When both colors are assigned, the linear gradient is applied.
            Panel.StateCommon.ColorStyle = PaletteColorStyle.Linear25;
            Panel.StateCommon.ColorAngle = 1f;
        }
        else
        {
            // in all other cases the gradient is turned off
            Panel.StateCommon.ColorStyle = PaletteColorStyle.Inherit;
            Panel.StateCommon.ColorAngle = -1;
        }
    }

    protected virtual void OnVisibleChanged()
    {
        VisibleChanged?.Invoke();
    }
    #endregion

    #region Private Separator
    private Bitmap CreateSeparator()
    {
        (Color color1, Color color2) colors = GetSeparatorColors();

        int width = Defaults.ClientWidth - Defaults.PanelLeft - Defaults.PanelRight;
        Rectangle rectangle = new Rectangle(0, 0, width, 2);
        Rectangle rectangleTop = new Rectangle(0, 0, width, 1);
        Rectangle rectangleBottom = new Rectangle(0, 1, width, 1);

        using Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
        using Brush brush1 = new SolidBrush(colors.color1);
        using Brush brush2 = new SolidBrush(colors.color2);
        using Graphics graphics = Graphics.FromImage(bitmap);

        graphics.FillRectangle(brush1, rectangleTop);
        graphics.FillRectangle(brush2, rectangleBottom);

        return bitmap.Clone(rectangle, bitmap.PixelFormat);
    }

    private void PaintSeparator()
    {
        if (ShowSeparator)
        {
            using Graphics graphics = Panel.CreateGraphics();
            graphics.DrawImage(_separator, Defaults.PanelLeft, 0);
        }
    }

    private (Color, Color) GetSeparatorColors()
    {
        Color color1;
        Color color2;

        // If BackColor1 has been set use this for the separator, otherwise fall back to theme colours.
        Color tmp = this.BackColor1 != Color.Empty
            ? this.BackColor1
            : Panel.GetResolvedPalette().GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);

        if (tmp.R > _separatorValues.FontMaxValue && tmp.G > _separatorValues.FontMaxValue && tmp.B > _separatorValues.FontMaxValue)
        {
            // If the colour reaches an upper bound it can't be made lighter.
            color1 = Color.FromArgb(tmp.R - _separatorValues.Adjust75, tmp.G - _separatorValues.Adjust75, tmp.B - _separatorValues.Adjust75);
            color2 = Color.White;
        }
        else if (tmp.R < _separatorValues.FontMinValue && tmp.G < _separatorValues.FontMinValue && tmp.B < _separatorValues.FontMinValue)
        {
            // If the colour reaches a lower bound, it can't be made darker.
            color2 = ControlPaint.Light(tmp);
            color1 = ControlPaint.Light(Color.LightSlateGray);
        }
        else if (tmp.R > 150 && tmp.G > 150 && tmp.B > 150)
        {
            // Colours that are somewhere in the middle need different handling
            int r = Math.Min(tmp.R - _separatorValues.Adjust50, 255);
            int g = Math.Min(tmp.G - _separatorValues.Adjust50, 255);
            int b = Math.Min(tmp.B - _separatorValues.Adjust50, 255);

            color1 = Color.FromArgb(r, g, b);
            color2 = Color.LightGray;
        }
        else
        {
            color1 = ControlPaint.Dark(tmp);
            color2 = ControlPaint.Light(tmp);
        }

        return (color1, color2);
    }
    #endregion

    #region IDispose
    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            _panel.Paint -= OnPanelPaint;

            if (Palette is not null)
            {
                Palette.PalettePaint -= OnPalettePaint;
                Palette = null!;
            }

            _disposed = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
