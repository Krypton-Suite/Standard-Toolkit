#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provides a themed system menu that replaces the native Windows system menu with a KryptonContextMenu.
    /// </summary>
    public class KryptonThemedSystemMenu
    {
        #region Instance Fields
        private readonly Form _form;
        private readonly KryptonContextMenu _contextMenu;
        private readonly List<SystemMenuItem> _menuItems;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonThemedSystemMenu class.
        /// </summary>
        /// <param name="form">The form to attach the themed system menu to.</param>
        public KryptonThemedSystemMenu(Form form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form));
            _contextMenu = new KryptonContextMenu();
            _menuItems = new List<SystemMenuItem>();
            
            BuildSystemMenu();
            
            // Ensure we always have a complete menu
            if (_contextMenu.Items.Count == 0)
            {
                CreateBasicMenuItems();
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the themed context menu.
        /// </summary>
        public KryptonContextMenu ContextMenu => _contextMenu;

        /// <summary>
        /// Gets or sets whether the themed system menu is enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets the number of items in the themed system menu.
        /// </summary>
        public int MenuItemCount => _contextMenu.Items.Count;

        /// <summary>
        /// Gets whether the menu has been populated with items.
        /// </summary>
        public bool HasMenuItems => _contextMenu.Items.Count > 0;
        #endregion

        #region Public Methods
        /// <summary>
        /// Shows the themed system menu at the specified location.
        /// </summary>
        /// <param name="screenLocation">The screen coordinates where to show the menu.</param>
        public void Show(Point screenLocation)
        {
            if (Enabled && _contextMenu.Items.Count > 0)
            {
                // Adjust the position to ensure the menu is fully visible
                var adjustedLocation = AdjustMenuPosition(screenLocation);
                _contextMenu.Show(_form, adjustedLocation);
            }
        }

        /// <summary>
        /// Shows the themed system menu at the top-left corner of the form.
        /// </summary>
        public void ShowAtFormTopLeft()
        {
            if (Enabled && _contextMenu.Items.Count > 0)
            {
                // Position at the top-left corner of the form, just like the native system menu
                var screenLocation = _form.PointToScreen(new Point(0, 0));
                
                // Adjust for the title bar height to position it properly
                if (_form is KryptonForm kryptonForm)
                {
                    // Get the title bar height from the form's non-client area
                    var titleBarHeight = kryptonForm.RealWindowBorders.Top;
                    screenLocation.Y += titleBarHeight;
                }
                
                _contextMenu.Show(_form, screenLocation);
            }
        }

        /// <summary>
        /// Refreshes the system menu items based on current form state.
        /// </summary>
        public void Refresh()
        {
            BuildSystemMenu();
            UpdateMenuItemsState();
        }

        /// <summary>
        /// Handles keyboard shortcuts for system menu actions.
        /// </summary>
        /// <param name="keyData">The key combination pressed.</param>
        /// <returns>True if the shortcut was handled; otherwise false.</returns>
        public bool HandleKeyboardShortcut(Keys keyData)
        {
            if (!Enabled) return false;

            // Handle Alt+F4 for Close
            if (keyData == (Keys.Alt | Keys.F4))
            {
                ExecuteClose();
                return true;
            }

            // Handle Alt+Space for showing the menu
            if (keyData == (Keys.Alt | Keys.Space))
            {
                ShowAtFormTopLeft();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a custom menu item to the system menu.
        /// </summary>
        /// <param name="text">The text to display for the menu item.</param>
        /// <param name="clickHandler">The action to execute when the item is clicked.</param>
        /// <param name="insertBeforeClose">If true, inserts the item before the Close item; otherwise adds it at the end.</param>
        public void AddCustomMenuItem(string text, EventHandler clickHandler, bool insertBeforeClose = true)
        {
            if (string.IsNullOrEmpty(text) || clickHandler == null) return;

            var customItem = new KryptonContextMenuItem(text);
            customItem.Click += clickHandler;

            if (insertBeforeClose && _contextMenu.Items.Count > 0)
            {
                // Find the Close item and insert before it
                for (int i = _contextMenu.Items.Count - 1; i >= 0; i--)
                {
                    if (_contextMenu.Items[i] is KryptonContextMenuItem menuItem && 
                        menuItem.Text.StartsWith("Close"))
                    {
                        _contextMenu.Items.Insert(i, customItem);
                        return;
                    }
                }
            }

            // If we couldn't find the Close item or insertBeforeClose is false, add at the end
            _contextMenu.Items.Add(customItem);
        }

        /// <summary>
        /// Adds a separator to the system menu.
        /// </summary>
        /// <param name="insertBeforeClose">If true, inserts the separator before the Close item; otherwise adds it at the end.</param>
        public void AddSeparator(bool insertBeforeClose = true)
        {
            var separator = new KryptonContextMenuSeparator();

            if (insertBeforeClose && _contextMenu.Items.Count > 0)
            {
                // Find the Close item and insert before it
                for (int i = _contextMenu.Items.Count - 1; i >= 0; i--)
                {
                    if (_contextMenu.Items[i] is KryptonContextMenuItem menuItem && 
                        menuItem.Text.StartsWith("Close"))
                    {
                        _contextMenu.Items.Insert(i, separator);
                        return;
                    }
                }
            }

            // If we couldn't find the Close item or insertBeforeClose is false, add at the end
            _contextMenu.Items.Add(separator);
        }

        /// <summary>
        /// Clears all custom menu items and restores the default system menu.
        /// </summary>
        public void ClearCustomItems()
        {
            BuildSystemMenu();
        }

        /// <summary>
        /// Gets a list of all custom menu items (non-standard system menu items).
        /// </summary>
        /// <returns>A list of custom menu item texts.</returns>
        public List<string> GetCustomMenuItems()
        {
            var customItems = new List<string>();
            var standardItems = new[] { "Restore", "Move", "Size", "Minimize", "Maximize", "Close" };

            foreach (var item in _contextMenu.Items)
            {
                if (item is KryptonContextMenuItem menuItem)
                {
                    var text = menuItem.Text.Split('\t')[0]; // Remove keyboard shortcuts
                    if (!standardItems.Any(standard => text.StartsWith(standard)))
                    {
                        customItems.Add(text);
                    }
                }
            }

            return customItems;
        }

        /// <summary>
        /// Updates the enabled state of menu items based on current form state.
        /// </summary>
        private void UpdateMenuItemsState()
        {
            if (_form is KryptonForm kryptonForm)
            {
                var windowState = kryptonForm.GetWindowState();
                
                // Update menu items based on current state
                foreach (var item in _contextMenu.Items)
                {
                    if (item is KryptonContextMenuItem menuItem)
                    {
                        // Enable/disable items based on current window state
                        if (menuItem.Text.StartsWith("Restore"))
                        {
                            menuItem.Enabled = (windowState != FormWindowState.Normal);
                        }
                        else if (menuItem.Text.StartsWith("Minimize"))
                        {
                            menuItem.Enabled = (windowState != FormWindowState.Minimized);
                        }
                        else if (menuItem.Text.StartsWith("Maximize"))
                        {
                            menuItem.Enabled = (windowState != FormWindowState.Maximized);
                        }
                        else if (menuItem.Text.StartsWith("Move"))
                        {
                            menuItem.Enabled = (windowState != FormWindowState.Normal);
                        }
                        else if (menuItem.Text.StartsWith("Size"))
                        {
                            menuItem.Enabled = (windowState == FormWindowState.Normal);
                        }
                        // Close is always enabled
                    }
                }
            }
        }
        #endregion

        #region Implementation
        private void BuildSystemMenu()
        {
            _contextMenu.Items.Clear();
            _menuItems.Clear();

            // Get the system menu handle
            var hSystemMenu = PI.GetSystemMenu(_form.Handle, false);
            if (hSystemMenu == IntPtr.Zero)
            {
                // If we can't get the system menu, create the basic menu
                CreateBasicMenuItems();
                return;
            }

            try
            {
                // Get the number of menu items
                var itemCount = PI.GetMenuItemCount(hSystemMenu);
                
                if (itemCount > 0)
                {
                    // Try to parse the native system menu
                    for (int i = 0; i < itemCount; i++)
                    {
                        var menuItem = CreateMenuItem(hSystemMenu, i);
                        if (menuItem != null)
                        {
                            _menuItems.Add(menuItem);
                            _contextMenu.Items.Add(menuItem.ContextMenuItem);
                        }
                    }
                    
                    // If we didn't get any items, fall back to basic menu
                    if (_contextMenu.Items.Count == 0)
                    {
                        CreateBasicMenuItems();
                    }
                }
                else
                {
                    // No items in native menu, create basic menu
                    CreateBasicMenuItems();
                }
            }
            catch (Exception)
            {
                // If anything goes wrong, fall back to basic menu items
                CreateBasicMenuItems();
            }
        }

        private SystemMenuItem? CreateMenuItem(IntPtr hMenu, int position)
        {
            try
            {
                var mii = new PI.MENUITEMINFO();
                mii.fMask = PI.MIIM_ID | PI.MIIM_STRING | PI.MIIM_STATE | PI.MIIM_TYPE;
                
                if (PI.GetMenuItemInfo(hMenu, (uint)position, true, ref mii))
                {
                    // Get the menu item text
                    var text = GetMenuItemText(hMenu, position);
                    
                    // Create the context menu item
                    var contextMenuItem = new KryptonContextMenuItem(text);
                    
                    // Set the enabled state
                    contextMenuItem.Enabled = (mii.fState & PI.MFS_DISABLED) == 0;
                    
                    // Handle click events
                    contextMenuItem.Click += (sender, e) => HandleMenuItemClick(mii.wID);
                    
                    return new SystemMenuItem
                    {
                        CommandId = mii.wID,
                        Text = text,
                        ContextMenuItem = contextMenuItem,
                        Enabled = contextMenuItem.Enabled
                    };
                }
            }
            catch (Exception)
            {
                // Ignore individual menu item errors
            }
            
            return null;
        }

        private string GetMenuItemText(IntPtr hMenu, int position)
        {
            try
            {
                var buffer = new StringBuilder(256);
                var result = PI.GetMenuString(hMenu, (uint)position, buffer, buffer.Capacity, PI.MF_.BYPOSITION);
                
                if (result > 0)
                {
                    return buffer.ToString();
                }
            }
            catch (Exception)
            {
                // Ignore text retrieval errors
            }
            
            return "Menu Item";
        }

        private void CreateBasicMenuItems()
        {
            // Create comprehensive system menu items matching the native Windows system menu
            var restoreItem = new KryptonContextMenuItem("Restore");
            restoreItem.Click += (sender, e) => ExecuteRestore();
            _contextMenu.Items.Add(restoreItem);

            var moveItem = new KryptonContextMenuItem("Move");
            moveItem.Click += (sender, e) => ExecuteMove();
            _contextMenu.Items.Add(moveItem);

            var sizeItem = new KryptonContextMenuItem("Size");
            sizeItem.Click += (sender, e) => ExecuteSize();
            _contextMenu.Items.Add(sizeItem);

            _contextMenu.Items.Add(new KryptonContextMenuSeparator());

            var minimizeItem = new KryptonContextMenuItem("Minimize");
            minimizeItem.Click += (sender, e) => ExecuteMinimize();
            _contextMenu.Items.Add(minimizeItem);

            var maximizeItem = new KryptonContextMenuItem("Maximize");
            maximizeItem.Click += (sender, e) => ExecuteMaximize();
            _contextMenu.Items.Add(maximizeItem);

            _contextMenu.Items.Add(new KryptonContextMenuSeparator());

            var closeItem = new KryptonContextMenuItem("Close\tAlt+F4");
            closeItem.Click += (sender, e) => ExecuteClose();
            _contextMenu.Items.Add(closeItem);
        }

        private void HandleMenuItemClick(uint commandId)
        {
            SendSysCommand((PI.SC_)commandId);
        }

        #region Action Execution Methods

        /// <summary>
        /// Executes the Restore action to restore the window to normal size.
        /// </summary>
        private void ExecuteRestore()
        {
            try
            {
                if (_form.WindowState != FormWindowState.Normal)
                {
                    _form.WindowState = FormWindowState.Normal;
                }
                else
                {
                    // Fallback to system command if direct property change doesn't work
                    SendSysCommand(PI.SC_.RESTORE);
                }
            }
            catch
            {
                // Fallback to system command
                SendSysCommand(PI.SC_.RESTORE);
            }
        }

        /// <summary>
        /// Executes the Move action to allow window dragging.
        /// </summary>
        private void ExecuteMove()
        {
            try
            {
                // For Move, we need to use the system command as it's a special Windows behavior
                SendSysCommand(PI.SC_.MOVE);
            }
            catch
            {
                // Fallback to system command
                SendSysCommand(PI.SC_.MOVE);
            }
        }

        /// <summary>
        /// Executes the Size action to allow window resizing.
        /// </summary>
        private void ExecuteSize()
        {
            try
            {
                // For Size, we need to use the system command as it's a special Windows behavior
                SendSysCommand(PI.SC_.SIZE);
            }
            catch
            {
                // Fallback to system command
                SendSysCommand(PI.SC_.SIZE);
            }
        }

        /// <summary>
        /// Executes the Minimize action to minimize the window.
        /// </summary>
        private void ExecuteMinimize()
        {
            try
            {
                if (_form.WindowState != FormWindowState.Minimized)
                {
                    _form.WindowState = FormWindowState.Minimized;
                }
                else
                {
                    // Fallback to system command if direct property change doesn't work
                    SendSysCommand(PI.SC_.MINIMIZE);
                }
            }
            catch
            {
                // Fallback to system command
                SendSysCommand(PI.SC_.MINIMIZE);
            }
        }

        /// <summary>
        /// Executes the Maximize action to maximize the window.
        /// </summary>
        private void ExecuteMaximize()
        {
            try
            {
                if (_form.WindowState != FormWindowState.Maximized)
                {
                    _form.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    // Fallback to system command if direct property change doesn't work
                    SendSysCommand(PI.SC_.MAXIMIZE);
                }
            }
            catch
            {
                // Fallback to system command
                SendSysCommand(PI.SC_.MAXIMIZE);
            }
        }

        /// <summary>
        /// Executes the Close action to close the window.
        /// </summary>
        private void ExecuteClose()
        {
            try
            {
                // Try to close the form gracefully first
                if (_form is KryptonForm kryptonForm)
                {
                    // Use the KryptonForm's close mechanism
                    kryptonForm.Close();
                }
                else
                {
                    // Fallback to standard form close
                    _form.Close();
                }
            }
            catch
            {
                // Fallback to system command if close fails
                SendSysCommand(PI.SC_.CLOSE);
            }
        }

        #endregion

        private void SendSysCommand(PI.SC_ command)
        {
            // Convert screen position to LPARAM format of WM_SYSCOMMAND message
            var screenPos = Control.MousePosition;
            var lParam = (IntPtr)(PI.MAKELOWORD(screenPos.X) | PI.MAKEHIWORD(screenPos.Y));
            
            // Send the system command - try KryptonForm first, fallback to Win32 API
            if (_form is KryptonForm kryptonForm)
            {
                kryptonForm.SendSysCommand(command, lParam);
            }
            else
            {
                // Fallback for non-KryptonForm forms using Win32 API
                PI.PostMessage(_form.Handle, PI.WM_.SYSCOMMAND, (IntPtr)command, lParam);
            }
        }

        /// <summary>
        /// Adjusts the menu position to ensure it's fully visible on screen.
        /// </summary>
        /// <param name="originalLocation">The original screen location.</param>
        /// <returns>The adjusted screen location.</returns>
        private Point AdjustMenuPosition(Point originalLocation)
        {
            // Get the screen bounds
            var screenBounds = Screen.FromControl(_form).Bounds;
            
            // Estimate menu size (this is approximate)
            var estimatedMenuWidth = 200;
            var estimatedMenuHeight = _contextMenu.Items.Count * 25; // Approximate height per item
            
            // Check if menu would go off the right edge
            if (originalLocation.X + estimatedMenuWidth > screenBounds.Right)
            {
                originalLocation.X = screenBounds.Right - estimatedMenuWidth;
            }
            
            // Check if menu would go off the bottom edge
            if (originalLocation.Y + estimatedMenuHeight > screenBounds.Bottom)
            {
                originalLocation.Y = screenBounds.Bottom - estimatedMenuHeight;
            }
            
            // Ensure menu doesn't go off the left or top edges
            if (originalLocation.X < screenBounds.Left)
            {
                originalLocation.X = screenBounds.Left;
            }
            
            if (originalLocation.Y < screenBounds.Top)
            {
                originalLocation.Y = screenBounds.Top;
            }
            
            return originalLocation;
        }
        #endregion

        #region Nested Classes
        /// <summary>
        /// Represents a system menu item with its associated context menu item.
        /// </summary>
        private class SystemMenuItem
        {
            public uint CommandId { get; set; }
            public string Text { get; set; } = string.Empty;
            public KryptonContextMenuItem ContextMenuItem { get; set; } = null!;
            public bool Enabled { get; set; }
        }
        #endregion
    }
}
