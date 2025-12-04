#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Abstract base class where all KryptonTaskDialogElementData components must be derived from.
/// Derived elements can be extended through the use of IKryptonTaskDialogElement interfaces to 
/// guarantee correct and consistent definitions and naming of properties and methods.
/// 
/// The IDisposable pattern has been implemented by default so derived classes can override and implement if required.
/// </summary>
public abstract class KryptonTaskDialogElementBase : 
    IKryptonTaskDialogElementBase,
    IKryptonTaskDialogElementEventSizeChanged,
    IDisposable
{
    #region Fields
    private bool _disposed;
    private KryptonTaskDialogKryptonPanel _panel;
    private bool _panelVisible;
    private KryptonTaskDialogDefaults _taskDialogDefaults;
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

        // Although OnGlobalPaletteChanged synchronizes palette changes with the elements,
        // The initialisation is done here
        Palette = KryptonManager.CurrentGlobalPalette;
        // From there the event handler will take over.
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        _panel = new(_taskDialogDefaults)
        {
            Margin = _taskDialogDefaults.NullPadding,
            Padding = _taskDialogDefaults.NullMargin,
            Visible = false
        };

        _panelVisible =  false;
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
        get => _panel.ShowSeparator;
        set => _panel.ShowSeparator = value;
    }

    /// <summary>
    /// Returns if the element's layout needs a refresh when visible or before the dialog will be displayed.
    /// </summary>
    internal bool LayoutDirty { get; set; }

    /// <summary>
    /// Returns the height of the element when visible.<br/>
    /// When not visible Height returns zero.
    /// </summary>
    public int Height => _panelVisible ? Panel.Height : 0;

    /// <inheritdoc/>
    internal KryptonTaskDialogDefaults Defaults => _taskDialogDefaults;
    #endregion

    #region Private
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

    #region IDispose
    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

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

