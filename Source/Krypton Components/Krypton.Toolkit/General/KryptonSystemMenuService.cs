#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Service class that manages the system menu functionality for forms.
/// Implements IDisposable to ensure proper cleanup of resources.
/// </summary>
public class KryptonSystemMenuService : IDisposable
{
    #region Instance Fields
    private readonly Form _form;
    private readonly KryptonSystemMenu _systemMenu;
    private bool _disposed = false;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSystemMenuService class.
    /// </summary>
    /// <param name="form">The form to attach the themed system menu to.</param>
    public KryptonSystemMenuService(Form form)
    {
        _form = form ?? throw new ArgumentNullException(nameof(form));
        _systemMenu = new KryptonSystemMenu(form);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets whether the themed system menu is enabled.
    /// </summary>
    public bool UseSystemMenu
    {
        get
        {
            ThrowIfDisposed();
            return _systemMenu.Enabled;
        }
        set
        {
            ThrowIfDisposed();
            _systemMenu.Enabled = value;
        }
    }

    /// <summary>
    /// Gets or sets whether to show the themed system menu on left click.
    /// </summary>
    public bool ShowSystemMenuOnLeftClick
    {
        get
        {
            ThrowIfDisposed();
            return _systemMenu.ShowOnLeftClick;
        }
        set
        {
            ThrowIfDisposed();
            _systemMenu.ShowOnLeftClick = value;
        }
    }

    /// <summary>
    /// Gets or sets whether to show the themed system menu on right click.
    /// </summary>
    public bool ShowSystemMenuOnRightClick
    {
        get
        {
            ThrowIfDisposed();
            return _systemMenu.ShowOnRightClick;
        }
        set
        {
            ThrowIfDisposed();
            _systemMenu.ShowOnRightClick = value;
        }
    }

    /// <summary>
    /// Gets or sets whether to show the themed system menu on Alt+Space.
    /// </summary>
    public bool ShowSystemMenuOnAltSpace
    {
        get
        {
            ThrowIfDisposed();
            return _systemMenu.ShowOnAltSpace;
        }
        set
        {
            ThrowIfDisposed();
            _systemMenu.ShowOnAltSpace = value;
        }
    }

    /// <summary>
    /// Gets the underlying themed system menu instance.
    /// </summary>
    public KryptonSystemMenu SystemMenu
    {
        get
        {
            ThrowIfDisposed();
            return _systemMenu;
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Handles keyboard shortcuts for system menu actions.
    /// </summary>
    /// <param name="keyData">The key combination pressed.</param>
    /// <returns>True if the shortcut was handled; otherwise false.</returns>
    public bool HandleKeyboardShortcut(Keys keyData)
    {
        ThrowIfDisposed();
        return _systemMenu.HandleKeyboardShortcut(keyData);
    }

    /// <summary>
    /// Handles right-click events for showing the themed system menu.
    /// </summary>
    /// <param name="screenPoint">The screen coordinates of the click.</param>
    /// <param name="isInTitleBarArea">Whether the click is in the title bar area.</param>
    /// <param name="isOnControlButtons">Whether the click is on control buttons.</param>
    /// <returns>True if the event was handled; otherwise false.</returns>
    public bool HandleRightClick(Point screenPoint, bool isInTitleBarArea, bool isOnControlButtons)
    {
        ThrowIfDisposed();
            
        if (UseSystemMenu && ShowSystemMenuOnRightClick && isInTitleBarArea && !isOnControlButtons)
        {
            _systemMenu.Show(screenPoint);
            return true;
        }
            
        return false;
    }

    /// <summary>
    /// Handles left-click events for showing the themed system menu.
    /// </summary>
    /// <param name="screenPoint">The screen coordinates of the click.</param>
    /// <param name="isInTitleBarArea">Whether the click is in the title bar area.</param>
    /// <param name="isOnControlButtons">Whether the click is on control buttons.</param>
    /// <returns>True if the event was handled; otherwise false.</returns>
    public bool HandleLeftClick(Point screenPoint, bool isInTitleBarArea, bool isOnControlButtons)
    {
        ThrowIfDisposed();
            
        if (UseSystemMenu && ShowSystemMenuOnLeftClick && isInTitleBarArea && !isOnControlButtons)
        {
            _systemMenu.Show(screenPoint);
            return true;
        }
            
        return false;
    }

    /// <summary>
    /// Refreshes the themed system menu.
    /// </summary>
    public void Refresh()
    {
        ThrowIfDisposed();
        _systemMenu.Refresh();
    }
    #endregion

    #region IDisposable Implementation
    /// <summary>
    /// Throws an ObjectDisposedException if the object has been disposed.
    /// </summary>
    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(KryptonSystemMenuService));
        }
    }

    /// <summary>
    /// Releases all resources used by the KryptonSystemMenuService.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the KryptonSystemMenuService and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                _systemMenu?.Dispose();
            }

            _disposed = true;
        }
    }

    /// <summary>
    /// Finalizer for KryptonSystemMenuService.
    /// </summary>
    ~KryptonSystemMenuService()
    {
        Dispose(false);
    }
    #endregion
}