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
/// Provides a themed system menu that replaces the native Windows system menu with a KryptonContextMenu.
/// </summary>
[TypeConverter(typeof(KryptonThemedSystemMenuConverter))]
public class KryptonThemedSystemMenu : IKryptonThemedSystemMenu
{
    #region Instance Fields
    private readonly Form _form;
    private readonly KryptonContextMenu _contextMenu;
    private readonly List<SystemMenuItem> _menuItems;
    private ThemedSystemMenuItemCollection? _designerMenuItems;
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
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonContextMenu ContextMenu => _contextMenu;

    /// <summary>
    /// Gets or sets the designer-configured menu items.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ThemedSystemMenuItemCollection? DesignerMenuItems
    {
        get => _designerMenuItems;
        set
        {
            if (_designerMenuItems != value)
            {
                _designerMenuItems = value;
                Refresh();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the themed system menu is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Enables or disables the themed system menu.")]
    [DefaultValue(true)]
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets the number of items in the themed system menu.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The number of items currently in the themed system menu.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MenuItemCount => _contextMenu.Items.Count;

    /// <summary>
    /// Gets whether the menu has been populated with items.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the themed system menu contains any items.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasMenuItems => _contextMenu.Items.Count > 0;

    /// <summary>
    /// Gets or sets whether left-click on title bar shows the themed system menu.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowOnLeftClick { get; set; } = true;

    /// <summary>
    /// Gets or sets whether right-click on title bar shows the themed system menu.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowOnRightClick { get; set; } = true;

    /// <summary>
    /// Gets or sets whether Alt+Space shows the themed system menu.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowOnAltSpace { get; set; } = true;
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
        RefreshIcons();
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

    #region Icon Generation
    /// <summary>
    /// Generates a system menu icon based on the specified type.
    /// </summary>
    /// <param name="iconType">The type of icon to generate.</param>
    /// <returns>An Image representing the system menu icon.</returns>
    private Image? GetSystemMenuIcon(SystemMenuIconType iconType)
    {
        try
        {
            const int iconSize = 16; // Standard system menu icon size
            var bitmap = new Bitmap(iconSize, iconSize);
            
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.Clear(Color.Transparent);

                // Get theme-appropriate colors
                var foregroundColor = GetThemeForegroundColor();
                var backgroundColor = GetThemeBackgroundColor();

                switch (iconType)
                {
                    case SystemMenuIconType.Restore:
                        DrawRestoreIcon(graphics, iconSize, foregroundColor, backgroundColor);
                        break;
                    case SystemMenuIconType.Minimize:
                        DrawMinimizeIcon(graphics, iconSize, foregroundColor);
                        break;
                    case SystemMenuIconType.Maximize:
                        DrawMaximizeIcon(graphics, iconSize, foregroundColor);
                        break;
                    case SystemMenuIconType.Close:
                        DrawCloseIcon(graphics, iconSize, foregroundColor);
                        break;
                }
            }

            return bitmap;
        }
        catch
        {
            // If icon generation fails, return null (no icon)
            return null;
        }
    }

    /// <summary>
    /// Gets the foreground color appropriate for the current theme.
    /// </summary>
    /// <returns>The foreground color for icons.</returns>
    private Color GetThemeForegroundColor()
    {
        try
        {
            // Try to get the color from the current palette
            if (_form is KryptonForm kryptonForm)
            {
                var palette = kryptonForm.GetResolvedPalette();
                if (palette != null)
                {
                    return palette.GetContentShortTextColor1(PaletteContentStyle.HeaderForm, PaletteState.Normal);
                }
            }
        }
        catch
        {
            // Fallback to default color
        }
        
        return Color.Black;
    }

    /// <summary>
    /// Gets the background color appropriate for the current theme.
    /// </summary>
    /// <returns>The background color for icons.</returns>
    private Color GetThemeBackgroundColor()
    {
        try
        {
            // Try to get the color from the current palette
            if (_form is KryptonForm kryptonForm)
            {
                var palette = kryptonForm.GetResolvedPalette();
                if (palette != null)
                {
                    return palette.GetBackColor1(PaletteBackStyle.HeaderForm, PaletteState.Normal);
                }
            }
        }
        catch
        {
            // Fallback to default color
        }
        
        return Color.White;
    }

    /// <summary>
    /// Refreshes the icons for all menu items to match the current theme.
    /// </summary>
    private void RefreshIcons()
    {
        foreach (var item in _contextMenu.Items)
        {
            if (item is KryptonContextMenuItem menuItem)
            {
                var text = menuItem.Text.Split('\t')[0]; // Remove keyboard shortcuts
                
                if (text.StartsWith("Restore"))
                {
                    menuItem.Image = GetSystemMenuIcon(SystemMenuIconType.Restore);
                }
                else if (text.StartsWith("Minimize"))
                {
                    menuItem.Image = GetSystemMenuIcon(SystemMenuIconType.Minimize);
                }
                else if (text.StartsWith("Maximize"))
                {
                    menuItem.Image = GetSystemMenuIcon(SystemMenuIconType.Maximize);
                }
                else if (text.StartsWith("Close"))
                {
                    menuItem.Image = GetSystemMenuIcon(SystemMenuIconType.Close);
                }
            }
        }
    }

    /// <summary>
    /// Draws a restore icon (square with arrow).
    /// </summary>
    private void DrawRestoreIcon(Graphics graphics, int size, Color foregroundColor, Color backgroundColor)
    {
        var pen = new Pen(foregroundColor, 1);
        var brush = new SolidBrush(backgroundColor);
        
        // Draw the main square
        var rect = new Rectangle(2, 2, size - 4, size - 4);
        graphics.FillRectangle(brush, rect);
        graphics.DrawRectangle(pen, rect);
        
        // Draw the arrow pointing to the square
        var arrowPoints = new Point[]
        {
            new Point(size - 6, 4),
            new Point(size - 6, size - 6),
            new Point(4, size - 6)
        };
        graphics.FillPolygon(brush, arrowPoints);
        graphics.DrawPolygon(pen, arrowPoints);
        
        pen.Dispose();
        brush.Dispose();
    }

    /// <summary>
    /// Draws a minimize icon (horizontal line).
    /// </summary>
    private void DrawMinimizeIcon(Graphics graphics, int size, Color foregroundColor)
    {
        var pen = new Pen(foregroundColor, 2);
        
        // Draw horizontal line
        var y = size / 2;
        graphics.DrawLine(pen, 3, y, size - 3, y);
        
        pen.Dispose();
    }

    /// <summary>
    /// Draws a maximize icon (square outline).
    /// </summary>
    private void DrawMaximizeIcon(Graphics graphics, int size, Color foregroundColor)
    {
        var pen = new Pen(foregroundColor, 1);
        
        // Draw square outline
        var rect = new Rectangle(2, 2, size - 4, size - 4);
        graphics.DrawRectangle(pen, rect);
        
        pen.Dispose();
    }

    /// <summary>
    /// Draws a close icon (X).
    /// </summary>
    private void DrawCloseIcon(Graphics graphics, int size, Color foregroundColor)
    {
        var pen = new Pen(foregroundColor, 2);
        
        // Draw X
        graphics.DrawLine(pen, 3, 3, size - 3, size - 3);
        graphics.DrawLine(pen, 3, size - 3, size - 3, 3);
        
        pen.Dispose();
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

        // Add designer-configured menu items
        AddDesignerMenuItems();
    }

    private SystemMenuItem? CreateMenuItem(IntPtr hMenu, int position)
    {
        try
        {
            var mii = new PI.MENUITEMINFO();
            mii.fMask = (uint)(PI.MIIM_.ID | PI.MIIM_.ID | PI.MIIM_.ID | PI.MIIM_.TYPE);

            if (PI.GetMenuItemInfo(hMenu, (uint)position, true, ref mii))
            {
                // Get the menu item text
                var text = GetMenuItemText(hMenu, position);

                // Create the context menu item
                var contextMenuItem = new KryptonContextMenuItem(text);

                // Set the enabled state
                contextMenuItem.Enabled = (mii.fState & (uint)PI.MFS_.DISABLED) == 0;

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
            restoreItem.Image = GetSystemMenuIcon(SystemMenuIconType.Restore);
            restoreItem.Click += (sender, e) => ExecuteRestore();
            _contextMenu.Items.Add(restoreItem);

            var moveItem = new KryptonContextMenuItem("Move");
            // Move doesn't typically have an icon in Windows
            moveItem.Click += (sender, e) => ExecuteMove();
            _contextMenu.Items.Add(moveItem);

            var sizeItem = new KryptonContextMenuItem("Size");
            // Size doesn't typically have an icon in Windows
            sizeItem.Click += (sender, e) => ExecuteSize();
            _contextMenu.Items.Add(sizeItem);

            _contextMenu.Items.Add(new KryptonContextMenuSeparator());

            var minimizeItem = new KryptonContextMenuItem("Minimize");
            minimizeItem.Image = GetSystemMenuIcon(SystemMenuIconType.Minimize);
            minimizeItem.Click += (sender, e) => ExecuteMinimize();
            _contextMenu.Items.Add(minimizeItem);

            var maximizeItem = new KryptonContextMenuItem("Maximize");
            maximizeItem.Image = GetSystemMenuIcon(SystemMenuIconType.Maximize);
            maximizeItem.Click += (sender, e) => ExecuteMaximize();
            _contextMenu.Items.Add(maximizeItem);

            _contextMenu.Items.Add(new KryptonContextMenuSeparator());

            var closeItem = new KryptonContextMenuItem("Close\tAlt+F4");
            closeItem.Image = GetSystemMenuIcon(SystemMenuIconType.Close);
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
            PI.PostMessage(_form.Handle, (uint)PI.WM_.SYSCOMMAND, (IntPtr)(uint)command, lParam);
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

    /// <summary>
    /// Adds designer-configured menu items to the context menu.
    /// </summary>
    private void AddDesignerMenuItems()
    {
        if (_designerMenuItems == null || _designerMenuItems.Count == 0)
            return;

        // Add a separator before custom items if there are existing items
        if (_contextMenu.Items.Count > 0)
        {
            _contextMenu.Items.Add(new KryptonContextMenuSeparator());
        }

        foreach (var designerItem in _designerMenuItems)
        {
            if (!designerItem.Visible)
                continue;

            var contextMenuItem = designerItem.CreateContextMenuItem();
            
            // Add click handler for designer items (placeholder - can be extended)
            contextMenuItem.Click += (sender, e) => OnDesignerMenuItemClick(designerItem);
            
            if (designerItem.InsertBeforeClose)
            {
                // Find the Close item and insert before it
                for (int i = _contextMenu.Items.Count - 1; i >= 0; i--)
                {
                    if (_contextMenu.Items[i] is KryptonContextMenuItem menuItem &&
                        menuItem.Text.StartsWith("Close"))
                    {
                        _contextMenu.Items.Insert(i, contextMenuItem);
                        break;
                    }
                }
            }
            else
            {
                _contextMenu.Items.Add(contextMenuItem);
            }
        }
    }

    /// <summary>
    /// Handles clicks on designer-configured menu items.
    /// </summary>
    /// <param name="designerItem">The designer menu item that was clicked.</param>
    private void OnDesignerMenuItemClick(ThemedSystemMenuItem designerItem)
    {
        // This is a placeholder - in a real implementation, you might want to:
        // 1. Raise a custom event that the form can handle
        // 2. Use a callback mechanism
        // 3. Integrate with a command pattern
        
        // For now, we'll just log or handle it silently
        System.Diagnostics.Debug.WriteLine($"Designer menu item clicked: {designerItem.Text}");
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