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
    IDisposable
{
    #region Fields
    private bool _disposed = false;
    private KryptonPanel _panel;
    private bool _panelVisible;
    #endregion

    #region Events
    public event Action VisibleChanged;
    #endregion

    #region Indentity
    /// <summary>
    /// Default constructor
    /// </summary>
    public KryptonTaskDialogElementBase()
    {
        _panel = new()
        {
            Margin = new Padding(0),
            Padding = new Padding(0),
            Visible = false
        };
        _panelVisible = _panel.Visible;
        
    }
    #endregion

    #region Internal
    /// <summary>
    /// Krypton panel that hosts the Element's controls and used for themed background coloring.
    /// </summary>
    internal KryptonPanel Panel => _panel;
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
    public override string ToString()
    {
        return string.Empty;
    }
    #endregion

    #region Public
    /// <summary>
    /// The height of the element.
    /// </summary>
    public int Height => Panel.Height;
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

    public void OnVisibleChanged()
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
