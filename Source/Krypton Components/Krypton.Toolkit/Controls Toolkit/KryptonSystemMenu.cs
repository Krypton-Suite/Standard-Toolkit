#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonSystemMenu : IDisposable
{
    #region Fields
    private KryptonForm _form;
    private ViewDrawContent _drawContent;
    private KryptonSystemMenuListener _listener;
    private KryptonContextMenu _contextMenu;
    private KryptonContextMenuItem _systemMenuContextMenuItemRestore;
    private KryptonContextMenuItem _systemMenuContextMenuItemMove;
    private KryptonContextMenuItem _systemMenuContextMenuItemSize;
    private KryptonContextMenuItem _systemMenuContextMenuItemMinimize;
    private KryptonContextMenuItem _systemMenuContextMenuItemMaximize;
    private KryptonContextMenuItem _systemMenuContextMenuItemClose;
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

        SetupContextMenu();
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
    private void SetupContextMenu()
    {
        KryptonContextMenuItems items = new();

        _systemMenuContextMenuItemRestore = new(KryptonManager.Strings.SystemMenuStrings.Restore);
        _systemMenuContextMenuItemRestore.Click += OnSystemMenuContextMenuItemRestoreClick;

        _systemMenuContextMenuItemMove = new(KryptonManager.Strings.SystemMenuStrings.Move);
        _systemMenuContextMenuItemMove.Click += OnSystemMenuContextMenuItemMoveClick;

        _systemMenuContextMenuItemSize = new(KryptonManager.Strings.SystemMenuStrings.Size);
        _systemMenuContextMenuItemSize.Click += OnSystemMenuContextMenuItemSizeClick;

        _systemMenuContextMenuItemMinimize = new(KryptonManager.Strings.SystemMenuStrings.Minimize);
        _systemMenuContextMenuItemMinimize.Click += OnSystemMenuContextMenuItemMinimizeClick;

        _systemMenuContextMenuItemMaximize = new(KryptonManager.Strings.SystemMenuStrings.Maximize);
        _systemMenuContextMenuItemMaximize.Click += OnSystemMenuContextMenuItemMaximizeClick;

        _systemMenuContextMenuItemClose = new(KryptonManager.Strings.SystemMenuStrings.Close);
        _systemMenuContextMenuItemClose.Click += OnSystemMenuContextMenuItemCloseClick;

        // Add the items in the order of the default system menu
        items.Items.Add(_systemMenuContextMenuItemRestore);
        items.Items.Add(_systemMenuContextMenuItemMove);
        items.Items.Add(_systemMenuContextMenuItemSize);
        items.Items.Add(_systemMenuContextMenuItemMinimize);
        items.Items.Add(_systemMenuContextMenuItemMaximize);
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(_systemMenuContextMenuItemClose);
        _contextMenu.Items.Insert(0, items);
    }

    private void UpdateSystemMenuItemState()
    {
        var windowState = _form.GetWindowState();

        // Update menu items based on current state using direct field references
        _systemMenuContextMenuItemRestore.Enabled = (windowState != FormWindowState.Normal);

        // Minimize item is enabled only if MinimizeBox is true and window is not already minimized
        _systemMenuContextMenuItemMinimize.Enabled = _form.MinimizeBox && (windowState != FormWindowState.Minimized);

        // Maximize item is enabled only if MaximizeBox is true and window is not already maximized
        _systemMenuContextMenuItemMaximize.Enabled = _form.MaximizeBox && (windowState != FormWindowState.Maximized);

        // Move is enabled when window is in Normal state (can be moved) or when minimized (can be restored)
        _systemMenuContextMenuItemMove.Enabled = (windowState == FormWindowState.Normal) || (windowState == FormWindowState.Minimized);

        // Size is enabled when the window is in Normal state and form is sizable
        _systemMenuContextMenuItemSize.Enabled = (windowState == FormWindowState.Normal)
            && _form.FormBorderStyle is FormBorderStyle.Sizable or FormBorderStyle.SizableToolWindow;
    }

    private void OnSystemMenuContextMenuItemRestoreClick(object? sender, EventArgs e)
    {
        if (_form.WindowState != FormWindowState.Normal)
        {
            _form.WindowState = FormWindowState.Normal;
        }
    }

    private void OnSystemMenuContextMenuItemMoveClick(object? sender, EventArgs e)
    {
        _form.SendSysCommand(PI.SC_.MOVE);
    }

    private void OnSystemMenuContextMenuItemSizeClick(object? sender, EventArgs e)
    {
        _form.SendSysCommand(PI.SC_.SIZE);
    }

    private void OnSystemMenuContextMenuItemMinimizeClick(object? sender, EventArgs e)
    {
        if (_form.WindowState != FormWindowState.Minimized)
        {
            _form.WindowState = FormWindowState.Minimized;
        }
    }
    private void OnSystemMenuContextMenuItemMaximizeClick(object? sender, EventArgs e)
    {
        if (_form.WindowState != FormWindowState.Maximized)
        {
            _form.WindowState = FormWindowState.Maximized;
        }
    }

    private void OnSystemMenuContextMenuItemCloseClick(object? sender, EventArgs e)
    {
        _form.Close();
    }

    private void OnListenerKeyAltSpaceDown(Point screenPoint)
    {
        if (_form.ControlBox)
        {
            UpdateSystemMenuItemState();
            _contextMenu.Show(_form, screenPoint);
        }
    }

    private void OnListenerNCLeftMouseButtonDown(Point screenPoint)
    {
        if (_form.ControlBox)
        {
            UpdateSystemMenuItemState();
            _contextMenu.Show(_form, screenPoint);
        }
    }

    private void OnListenerNCRightMouseButtonDown(Point screenPoint)
    {
        if (_form.ControlBox)
        {
            UpdateSystemMenuItemState();
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

            // Stop the listener which also will release the assigned handle
            _listener.DisableListener();

            // System menu
            _systemMenuContextMenuItemRestore.Click -= OnSystemMenuContextMenuItemRestoreClick;
            _systemMenuContextMenuItemMove.Click -= OnSystemMenuContextMenuItemMoveClick;
            _systemMenuContextMenuItemSize.Click -= OnSystemMenuContextMenuItemSizeClick;
            _systemMenuContextMenuItemMinimize.Click -= OnSystemMenuContextMenuItemMinimizeClick;
            _systemMenuContextMenuItemMaximize.Click -= OnSystemMenuContextMenuItemMaximizeClick;
            _systemMenuContextMenuItemClose.Click -= OnSystemMenuContextMenuItemCloseClick;

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
