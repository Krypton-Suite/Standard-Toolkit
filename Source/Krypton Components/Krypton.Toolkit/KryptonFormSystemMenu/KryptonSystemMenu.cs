#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using System.Windows.Forms;

namespace Krypton.Toolkit;

public class KryptonSystemMenu : IDisposable
{
    #region Fields
    private KryptonForm _form;
    private ViewDrawContent _drawContent;
    private KryptonSystemMenuListener _listener;
    private KryptonContextMenu _contextMenu;
    private bool _disposed;
    #endregion

    #region Identity
    public KryptonSystemMenu(KryptonForm kryptonForm, ViewDrawContent drawContent, KryptonContextMenu contextMenu)
    {
        _form = kryptonForm;
        _contextMenu = contextMenu;
        _drawContent = drawContent;

        // Instantiate the listener
        _listener = new(_form, _drawContent);

        // Subscribe to property changed events
        _form.SystemMenuValues.PropertyChanged += OnMenuValuesPropertyChanged;

        _listener.NCRightMouseButtonDown += OnListenerNCRightMouseButtonDown;
        _listener.NCLeftMouseButtonDown += OnListenerNCLeftMouseButtonDown;
        _listener.KeyAltSpaceDown += OnListenerKeyAltSpaceDown;
    }
    #endregion

    #region Public
    /// <summary>
    /// Stop listening for mouse and keyboard events that trigger the system menu.
    /// </summary>
    public void DisableListener()
    {
        _listener.DisableListener();
    }

    /// <summary>
    /// Enable listening for mouse and keyboard events that trigger the system menu.
    /// </summary>
    public void EnableListener()
    {
        _listener.EnableListener();
    }
    #endregion

    #region Private
    private void OnListenerKeyAltSpaceDown(Point screenPoint)
    {
        if (_form.ControlBox)
        {
            _form.UpdateSystemMenuItemState();
            _contextMenu.Show(_form, screenPoint);
        }
    }

    private void OnListenerNCLeftMouseButtonDown(Point screenPoint)
    {
        if (_form.ControlBox)
        {
            _form.UpdateSystemMenuItemState();
            _contextMenu.Show(_form, screenPoint);
        }
    }

    private void OnListenerNCRightMouseButtonDown(Point screenPoint)
    {
        if (_form.ControlBox)
        {
            _form.UpdateSystemMenuItemState();
            _contextMenu.Show(_form, screenPoint);
        }
    }

    private void OnMenuValuesPropertyChanged(object? sender, PropertyChangedEventArgs eventArgs)
    {
        if (eventArgs.PropertyName == nameof(_form.SystemMenuValues.Enabled))
        {
            if (_form.SystemMenuValues.Enabled)
            {
                EnableListener();
            }
            else
            {
                DisableListener();
            }
        }
    }
    #endregion

    #region IDisposable
    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        { 
            _form.SystemMenuValues.PropertyChanged -= OnMenuValuesPropertyChanged;
            
            _disposed = true;
        }
    }

    /// <summary>
    /// Cleanup and dispose the instance.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
