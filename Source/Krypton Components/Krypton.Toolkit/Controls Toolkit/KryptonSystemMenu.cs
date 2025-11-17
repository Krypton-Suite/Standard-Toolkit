#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
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
    private KryptonContextMenuItem _contextMenuItemRestore;
    private KryptonContextMenuItem _contextMenuItemMove;
    private KryptonContextMenuItem _contextMenuItemSize;
    private KryptonContextMenuItem _contextMenuItemMinimize;
    private KryptonContextMenuItem _cntextMenuItemMaximize;
    private KryptonContextMenuItem _contextMenuItemClose;
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

        _contextMenuItemRestore = new(KryptonManager.Strings.SystemMenuStrings.Restore);
        _contextMenuItemRestore.Image = SystemMenuImageResources.Microsoft365SystemMenuRestoreNormalSmall;
        _contextMenuItemRestore.Click += OnContextMenuItemRestoreClick;

        _contextMenuItemMove = new(KryptonManager.Strings.SystemMenuStrings.Move);
        _contextMenuItemMove.Image = ResourceFiles.Toolbars.Office2013ToolbarImageResources.Office2013ToolbarCopyNormal;
        _contextMenuItemMove.Click += OnContextMenuItemMoveClick;

        _contextMenuItemSize = new(KryptonManager.Strings.SystemMenuStrings.Size);
        _contextMenuItemSize.Image = ResourceFiles.Toolbars.Office2013ToolbarImageResources.Office2013ToolbarPageSetupNormal;
        _contextMenuItemSize.Click += OnContextMenuItemSizeClick;

        _contextMenuItemMinimize = new(KryptonManager.Strings.SystemMenuStrings.Minimize);
        _contextMenuItemMinimize.Image = SystemMenuImageResources.Microsoft365SystemMenuMinimiseNormalSmall;
        _contextMenuItemMinimize.Click += OnContextMenuItemMinimizeClick;

        _cntextMenuItemMaximize = new(KryptonManager.Strings.SystemMenuStrings.Maximize);
        _cntextMenuItemMaximize.Image = SystemMenuImageResources.Microsoft365SystemMenuMaximiseNormalSmall;
        _cntextMenuItemMaximize.Click += OnContextMenuItemMaximizeClick;

        _contextMenuItemClose = new(KryptonManager.Strings.SystemMenuStrings.Close);
        _contextMenuItemClose.Image = SystemMenuImageResources.Microsoft365SystemMenuCloseNormalSmall;
        _contextMenuItemClose.Click += OnContextMenuItemCloseClick;

        // Add the items in the order of the default system menu
        items.Items.Add(_contextMenuItemRestore);
        items.Items.Add(_contextMenuItemMove);
        items.Items.Add(_contextMenuItemSize);
        items.Items.Add(_contextMenuItemMinimize);
        items.Items.Add(_cntextMenuItemMaximize);
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(_contextMenuItemClose);
        _contextMenu.Items.Insert(0, items);
    }

    private void UpdateSystemMenuItemState()
    {
        var windowState = _form.GetWindowState();

        // Update menu items based on current state using direct field references
        _contextMenuItemRestore.Enabled = (windowState != FormWindowState.Normal);

        // Minimize item is enabled only if MinimizeBox is true and window is not already minimized
        _contextMenuItemMinimize.Enabled = _form.MinimizeBox && (windowState != FormWindowState.Minimized);

        // Maximize item is enabled only if MaximizeBox is true and window is not already maximized
        _cntextMenuItemMaximize.Enabled = _form.MaximizeBox && (windowState != FormWindowState.Maximized);

        // Move is enabled when window is in Normal state (can be moved) or when minimized (can be restored)
        _contextMenuItemMove.Enabled = (windowState == FormWindowState.Normal) || (windowState == FormWindowState.Minimized);

        // Size is enabled when the window is in Normal state and form is sizable
        _contextMenuItemSize.Enabled = (windowState == FormWindowState.Normal)
            && _form.FormBorderStyle is FormBorderStyle.Sizable or FormBorderStyle.SizableToolWindow;
    }

    private void OnContextMenuItemRestoreClick(object? sender, EventArgs e)
    {
        if (_form.WindowState != FormWindowState.Normal)
        {
            _form.WindowState = FormWindowState.Normal;
        }
    }

    private void OnContextMenuItemMoveClick(object? sender, EventArgs e)
    {
        _form.SendSysCommand(PI.SC_.MOVE);
    }

    private void OnContextMenuItemSizeClick(object? sender, EventArgs e)
    {
        _form.SendSysCommand(PI.SC_.SIZE);
    }

    private void OnContextMenuItemMinimizeClick(object? sender, EventArgs e)
    {
        if (_form.WindowState != FormWindowState.Minimized)
        {
            _form.WindowState = FormWindowState.Minimized;
        }
    }
    private void OnContextMenuItemMaximizeClick(object? sender, EventArgs e)
    {
        if (_form.WindowState != FormWindowState.Maximized)
        {
            _form.WindowState = FormWindowState.Maximized;
        }
    }

    private void OnContextMenuItemCloseClick(object? sender, EventArgs e)
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
            _contextMenuItemRestore.Click -= OnContextMenuItemRestoreClick;
            _contextMenuItemMove.Click -= OnContextMenuItemMoveClick;
            _contextMenuItemSize.Click -= OnContextMenuItemSizeClick;
            _contextMenuItemMinimize.Click -= OnContextMenuItemMinimizeClick;
            _cntextMenuItemMaximize.Click -= OnContextMenuItemMaximizeClick;
            _contextMenuItemClose.Click -= OnContextMenuItemCloseClick;

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
