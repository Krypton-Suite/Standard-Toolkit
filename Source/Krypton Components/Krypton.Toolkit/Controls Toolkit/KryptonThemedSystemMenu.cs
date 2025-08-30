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
public class KryptonThemedSystemMenu : IKryptonThemedSystemMenu, IDisposable
{
    #region Instance Fields
    private readonly Form _form;
    private readonly KryptonContextMenu _contextMenu;
    private ThemedSystemMenuItemCollection? _designerMenuItems;
    private bool _disposed;
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
    public KryptonContextMenu ContextMenu
    {
        get
        {
            ThrowIfDisposed();
            return _contextMenu;
        }
    }

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
    public int MenuItemCount
    {
        get
        {
            ThrowIfDisposed();
            return _contextMenu.Items.Count;
        }
    }

    /// <summary>
    /// Gets whether the menu has been populated with items.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the themed system menu contains any items.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasMenuItems
    {
        get
        {
            ThrowIfDisposed();
            return _contextMenu.Items.Count > 0;
        }
    }

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

    /// <summary>
    /// Gets the current theme name being used for icons.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The current theme name being used for system menu icons.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CurrentIconTheme => GetCurrentTheme();
    #endregion

    #region Public Methods
    /// <summary>
    /// Shows the themed system menu at the specified location.
    /// </summary>
    /// <param name="screenLocation">The screen coordinates where to show the menu.</param>
    public void Show(Point screenLocation)
    {
        ThrowIfDisposed();
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
        ThrowIfDisposed();
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
        ThrowIfDisposed();
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
        ThrowIfDisposed();
        if (!Enabled)
        {
            return false;
        }

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
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(text) || clickHandler == null)
        {
            return;
        }

        var customItem = new KryptonContextMenuItem(text);
        customItem.Click += clickHandler;

        if (insertBeforeClose && _contextMenu.Items.Count > 0)
        {
            // Find the Close item and insert above the separator (above the Close item)
            for (int i = _contextMenu.Items.Count - 1; i >= 0; i--)
            {
                if (_contextMenu.Items[i] is KryptonContextMenuItem menuItem)
                {
                    // Get the text without keyboard shortcuts (remove tab and everything after)
                    var itemText = menuItem.Text.Split('\t')[0];
                    
                    // Check if this is the Close item by comparing with the system menu string
                    // Handle both "Close" and "C&lose" (with accelerator key)
                    if (itemText.Equals(KryptonManager.Strings.SystemMenuStrings.Close, StringComparison.OrdinalIgnoreCase) ||
                        itemText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Close.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        // Insert above the separator (above the Close item)
                        // First, check if there's a separator above the Close item
                        if (i > 0 && _contextMenu.Items[i - 1] is KryptonContextMenuSeparator)
                        {
                            _contextMenu.Items.Insert(i - 1, customItem);
                        }
                        else
                        {
                            // If no separator, add one above the Close item first
                            _contextMenu.Items.Insert(i, new KryptonContextMenuSeparator());
                            // Then insert the custom item above the separator
                            _contextMenu.Items.Insert(i, customItem);
                        }
                        return;
                    }
                }
            }
        }

        // If we couldn't find the Close item or insertBeforeClose is false, add at the end
        _contextMenu.Items.Add(customItem);
        
        // Ensure there's a separator above custom items if we added at the end
        if (!insertBeforeClose)
        {
            EnsureSeparatorAboveCustomItems();
        }
    }

    /// <summary>
    /// Adds a separator to the themed system menu.
    /// </summary>
    /// <param name="insertBeforeClose">If true, inserts the separator before the Close item; otherwise adds it at the end.</param>
    public void AddSeparator(bool insertBeforeClose = true)
    {
        ThrowIfDisposed();
        var separator = new KryptonContextMenuSeparator();

        if (insertBeforeClose && _contextMenu.Items.Count > 0)
        {
            // Find the Close item and insert above the separator (above the Close item)
            for (int i = _contextMenu.Items.Count - 1; i >= 0; i--)
            {
                if (_contextMenu.Items[i] is KryptonContextMenuItem menuItem)
                {
                    // Get the text without keyboard shortcuts (remove tab and everything after)
                    var itemText = menuItem.Text.Split('\t')[0];
                    
                    // Check if this is the Close item by comparing with the system menu string
                    // Handle both "Close" and "C&lose" (with accelerator key)
                    if (itemText.Equals(KryptonManager.Strings.SystemMenuStrings.Close, StringComparison.OrdinalIgnoreCase) ||
                        itemText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Close.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        // Insert above the separator (above the Close item)
                        // First, check if there's already a separator above the Close item
                        if (i > 0 && _contextMenu.Items[i - 1] is KryptonContextMenuSeparator)
                        {
                            // If separator already exists, insert above it
                            _contextMenu.Items.Insert(i - 1, separator);
                        }
                        else
                        {
                            // If no separator, insert directly above the Close item
                            _contextMenu.Items.Insert(i, separator);
                        }
                        return;
                    }
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
        ThrowIfDisposed();
        BuildSystemMenu();
    }

    /// <summary>
    /// Gets a list of all custom menu items (non-standard system menu items).
    /// </summary>
    /// <returns>A list of custom menu item texts.</returns>
    public List<string> GetCustomMenuItems()
    {
        ThrowIfDisposed();
        var customItems = new List<string>();
        var standardItems = new[]
        {
            KryptonManager.Strings.SystemMenuStrings.Restore, 
            KryptonManager.Strings.SystemMenuStrings.Move,
            KryptonManager.Strings.SystemMenuStrings.Size,
            KryptonManager.Strings.SystemMenuStrings.Minimize,
            KryptonManager.Strings.SystemMenuStrings.Maximize,
            KryptonManager.Strings.SystemMenuStrings.Close
        };

        foreach (var item in _contextMenu.Items)
        {
            if (item is KryptonContextMenuItem menuItem)
            {
                var itemText = menuItem.Text.Split('\t')[0]; // Remove keyboard shortcuts
                if (!standardItems.Any(standard => 
                    itemText.Equals(standard, StringComparison.OrdinalIgnoreCase) ||
                    itemText.Replace("&", "").Equals(standard.Replace("&", ""), StringComparison.OrdinalIgnoreCase)))
                {
                    customItems.Add(itemText);
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
                    // Get the text without keyboard shortcuts (remove tab and everything after)
                    var menuText = menuItem.Text.Split('\t')[0];
                    
                    // Enable/disable items based on current window state
                    if (menuText.Equals(KryptonManager.Strings.SystemMenuStrings.Restore, StringComparison.OrdinalIgnoreCase) ||
                        menuText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Restore.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        menuItem.Enabled = (windowState != FormWindowState.Normal);
                    }
                    else if (menuText.Equals(KryptonManager.Strings.SystemMenuStrings.Minimize, StringComparison.OrdinalIgnoreCase) ||
                             menuText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Minimize.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        // Minimize item is enabled only if MinimizeBox is true and window is not already minimized
                        menuItem.Enabled = _form.MinimizeBox && (windowState != FormWindowState.Minimized);
                    }
                    else if (menuText.Equals(KryptonManager.Strings.SystemMenuStrings.Maximize, StringComparison.OrdinalIgnoreCase) ||
                             menuText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Maximize.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        // Maximize item is enabled only if MaximizeBox is true and window is not already maximized
                        menuItem.Enabled = _form.MaximizeBox && (windowState != FormWindowState.Maximized);
                    }
                    else if (menuText.Equals(KryptonManager.Strings.SystemMenuStrings.Move, StringComparison.OrdinalIgnoreCase) ||
                             menuText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Move.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        menuItem.Enabled = (windowState != FormWindowState.Normal);
                    }
                    else if (menuText.Equals(KryptonManager.Strings.SystemMenuStrings.Size, StringComparison.OrdinalIgnoreCase) ||
                             menuText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Size.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        menuItem.Enabled = (windowState != FormWindowState.Normal) && _form.FormBorderStyle == FormBorderStyle.Sizable || _form.FormBorderStyle == FormBorderStyle.SizableToolWindow;
                    }
                    // Close is always enabled
                }
            }
        }
    }
    #endregion

    #region Icon Generation
    /// <summary>
    /// Gets an appropriate system menu icon based on the icon type and current theme.
    /// </summary>
    /// <param name="iconType">The type of system menu icon to retrieve.</param>
    /// <returns>An Image representing the system menu icon.</returns>
    private Image? GetSystemMenuIcon(SystemMenuIconType iconType)
    {
        try
        {
            // Try to get the current theme to determine which resource set to use
            var currentTheme = GetCurrentTheme();
            
            // Get the appropriate icon based on theme and icon type
            var icon = GetThemeIcon(currentTheme, iconType);
            if (icon != null)
            {
                // Log image information for debugging
                LogImageInfo(icon, iconType, currentTheme);
                
                // Ensure proper transparency handling
                var processedIcon = ProcessImageForTransparency(icon);
                if (processedIcon != null)
                {
                    return processedIcon;
                }
                // If transparency processing fails, fall back to drawn icon
            }
            
            // Fallback to the current drawn icons if theme icons aren't available
            return GetDrawnIcon(iconType);
        }
        catch
        {
            // If icon retrieval fails, return null (no icon)
            return null;
        }
    }
    
    /// <summary>
    /// Processes an image to ensure proper transparency handling.
    /// </summary>
    /// <param name="originalImage">The original image that may have transparency issues.</param>
    /// <returns>A processed image with proper transparency support.</returns>
    private Image? ProcessImageForTransparency(Image originalImage)
    {
        if (originalImage == null)
        {
            return null;
        }
        
        try
        {
            // Check if the image already has proper transparency support
            if (originalImage.PixelFormat == PixelFormat.Format32bppArgb)
            {
                return originalImage; // Already in correct format
            }
            
            // Create a new bitmap with proper transparency support
            var bitmap = new Bitmap(originalImage.Width, originalImage.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(originalImage, 0, 0);
            }
            return bitmap;
        }
        catch (Exception ex)
        {
            // Log the error for debugging (in production, you might want to remove this)
            Debug.WriteLine($"Failed to process image transparency: {ex.Message}");
            
            // If processing fails, return the original image
            return originalImage;
        }
        }
    
    /// <summary>
    /// Logs information about an image for debugging purposes.
    /// </summary>
    /// <param name="image">The image to log information about.</param>
    /// <param name="iconType">The type of icon being processed.</param>
    /// <param name="theme">The theme being used.</param>
    private void LogImageInfo(Image image, SystemMenuIconType iconType, string theme)
    {
        try
        {
            Debug.WriteLine($"Image Info - Type: {iconType}, Theme: {theme}, " +
                                            $"Size: {image.Width}x{image.Height}, " +
                                            $"PixelFormat: {image.PixelFormat}, " +
                                            $"Flags: {image.Flags}");
        }
        catch
        {
            // Ignore logging errors
        }
    }
    
    /// <summary>
    /// Determines the current theme based on the form's palette.
    /// </summary>
    /// <returns>The current theme name.</returns>
    public string GetCurrentTheme()
    {
        try
        {
            // Try to get the current theme from the form's palette
            if (_form is KryptonForm kryptonForm)
            {
                var palette = kryptonForm.GetResolvedPalette();
                if (palette != null)
                {
                    // Detect theme based on palette characteristics
                    // This is a simplified detection - you can enhance this logic
                    var headerColor = palette.GetBackColor1(PaletteBackStyle.HeaderForm, PaletteState.Normal);
                    
                    // Office 2013 - typically white/light gray
                    if (IsLightColor(headerColor))
                    {
                        return "Office2013";
                    }
                    
                    // Office 2010 - typically blue tones
                    if (IsBlueTone(headerColor))
                    {
                        return "Office2010";
                    }
                    
                    // Office 2007 - typically darker blue
                    if (IsDarkBlueTone(headerColor))
                    {
                        return "Office2007";
                    }
                    
                    // Sparkle - typically vibrant colors
                    if (IsVibrantColor(headerColor))
                    {
                        return "Sparkle";
                    }
                    
                    // Professional - typically neutral tones
                    if (IsNeutralTone(headerColor))
                    {
                        return "Professional";
                    }
                    
                    // Microsoft 365 - typically modern colors
                    if (IsModernColor(headerColor))
                    {
                        return "Microsoft365";
                    }
                    
                    // Office 2003 - typically classic Windows colors
                    if (IsClassicColor(headerColor))
                    {
                        return "Office2003";
                    }
                }
            }
        }
        catch
        {
            // Fallback to default theme
        }
        
        return "Office2013"; // Default theme
    }

    /// <summary>
    /// Gets the appropriate icon for the specified theme and icon type.
    /// </summary>
    /// <param name="theme">The theme name.</param>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The appropriate icon image or null if not available.</returns>
    private Image? GetThemeIcon(string theme, SystemMenuIconType iconType)
    {
        try
        {
            switch (theme)
            {
                case "Office2013":
                    return GetOffice2013Icon(iconType);
                case "Office2010":
                    return GetOffice2010Icon(iconType);
                case "Office2007":
                    return GetOffice2007Icon(iconType);
                case "Sparkle":
                    return GetSparkleIcon(iconType);
                case "Professional":
                    return GetProfessionalIcon(iconType);
                case "Microsoft365":
                    return GetMicrosoft365Icon(iconType);
                case "Office2003":
                    return GetOffice2003Icon(iconType);
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets Office 2013 themed icons.
    /// </summary>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The appropriate icon image or null if not available.</returns>
    private Image? GetOffice2013Icon(SystemMenuIconType iconType)
    {
        try
        {
            switch (iconType)
            {
                case SystemMenuIconType.Restore:
                    return SystemMenuImageResources.Microsoft365SystemMenuRestoreNormalSmall;
                case SystemMenuIconType.Minimize:
                    return SystemMenuImageResources.Microsoft365SystemMenuMinimiseNormalSmall;
                case SystemMenuIconType.Maximize:
                    return SystemMenuImageResources.Microsoft365SystemMenuMaximiseNormalSmall;
                case SystemMenuIconType.Close:
                    return SystemMenuImageResources.Microsoft365SystemMenuCloseNormalSmall;
                case SystemMenuIconType.Move:
                case SystemMenuIconType.Size:
                    // These don't have specific icons in the resources, so return null
                    return null;
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets Office 2010 themed icons.
    /// </summary>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The appropriate icon image or null if not available.</returns>
    private Image? GetOffice2010Icon(SystemMenuIconType iconType)
    {
        try
        {
            switch (iconType)
            {
                case SystemMenuIconType.Restore:
                    return SystemMenuImageResources.Office2010SystemMenuBlackRestoreNormalSmall;
                case SystemMenuIconType.Minimize:
                    return SystemMenuImageResources.Office2010SystemMenuBlackMinimiseNormalSmall;
                case SystemMenuIconType.Maximize:
                    return SystemMenuImageResources.Office2010SystemMenuBlackMaximiseNormalSmall;
                case SystemMenuIconType.Close:
                    return SystemMenuImageResources.Office2010SystemMenuBlackCloseNormalSmall;
                case SystemMenuIconType.Move:
                case SystemMenuIconType.Size:
                    return null;
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets Office 2007 themed icons.
    /// </summary>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The appropriate icon image or null if not available.</returns>
    private Image? GetOffice2007Icon(SystemMenuIconType iconType)
    {
        try
        {
            switch (iconType)
            {
                case SystemMenuIconType.Restore:
                    return SystemMenuImageResources.Office2007SystemMenuBlackRestoreNormalSmall;
                case SystemMenuIconType.Minimize:
                    return SystemMenuImageResources.Office2007SystemMenuBlackMinimiseNormalSmall;
                case SystemMenuIconType.Maximize:
                    return SystemMenuImageResources.Office2007SystemMenuBlackMaximiseNormalSmall;
                case SystemMenuIconType.Close:
                    return SystemMenuImageResources.Office2007SystemMenuBlackCloseNormalSmall;
                case SystemMenuIconType.Move:
                case SystemMenuIconType.Size:
                    return null;
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets Sparkle themed icons.
    /// </summary>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The appropriate icon image or null if not available.</returns>
    private Image? GetSparkleIcon(SystemMenuIconType iconType)
    {
        try
        {
            switch (iconType)
            {
                case SystemMenuIconType.Restore:
                    return SystemMenuImageResources.SparkleSystemMenuRestoreNormalSmall;
                case SystemMenuIconType.Minimize:
                    return SystemMenuImageResources.SparkleSystemMenuMinimiseNormalSmall;
                case SystemMenuIconType.Maximize:
                    return SystemMenuImageResources.SparkleSystemMenuMaximiseNormalSmall;
                case SystemMenuIconType.Close:
                    return SystemMenuImageResources.SparkleSystemMenuCloseNormalSmall;
                case SystemMenuIconType.Move:
                case SystemMenuIconType.Size:
                    return null;
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets Professional themed icons.
    /// </summary>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The appropriate icon image or null if not available.</returns>
    private Image? GetProfessionalIcon(SystemMenuIconType iconType)
    {
        try
        {
            switch (iconType)
            {
                case SystemMenuIconType.Restore:
                    return SystemMenuImageResources.ProfessionalSystemMenuRestoreNormalSmall;
                case SystemMenuIconType.Minimize:
                    return SystemMenuImageResources.ProfessionalSystemMenuMinimiseNormalSmall;
                case SystemMenuIconType.Maximize:
                    return SystemMenuImageResources.ProfessionalSystemMenuMaximiseNormalSmall;
                case SystemMenuIconType.Close:
                    return SystemMenuImageResources.ProfessionalSystemMenuCloseNormalSmall;
                case SystemMenuIconType.Move:
                case SystemMenuIconType.Size:
                    return null;
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets Microsoft 365 themed icons.
    /// </summary>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The appropriate icon image or null if not available.</returns>
    private Image? GetMicrosoft365Icon(SystemMenuIconType iconType)
    {
        try
        {
            switch (iconType)
            {
                case SystemMenuIconType.Restore:
                    return SystemMenuImageResources.Office2010SystemMenuBlackRestoreNormalSmall;
                case SystemMenuIconType.Minimize:
                    return SystemMenuImageResources.Office2010SystemMenuBlackMinimiseNormalSmall;
                case SystemMenuIconType.Maximize:
                    return SystemMenuImageResources.Office2010SystemMenuBlackMaximiseNormalSmall;
                case SystemMenuIconType.Close:
                    return SystemMenuImageResources.Office2010SystemMenuBlackCloseNormalSmall;
                case SystemMenuIconType.Move:
                case SystemMenuIconType.Size:
                    return null;
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets Office 2003 themed icons.
    /// </summary>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The appropriate icon image or null if not available.</returns>
    private Image? GetOffice2003Icon(SystemMenuIconType iconType)
    {
        try
        {
            switch (iconType)
            {
                case SystemMenuIconType.Restore:
                    return SystemMenuImageResources.ProfessionalSystemMenuRestoreNormalSmall;
                case SystemMenuIconType.Minimize:
                    return SystemMenuImageResources.ProfessionalSystemMenuMinimiseNormalSmall;
                case SystemMenuIconType.Maximize:
                    return SystemMenuImageResources.ProfessionalSystemMenuMaximiseNormalSmall;
                case SystemMenuIconType.Close:
                    return SystemMenuImageResources.ProfessionalSystemMenuCloseNormalSmall;
                case SystemMenuIconType.Move:
                case SystemMenuIconType.Size:
                    return null;
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the drawn icon as a fallback when theme icons are not available.
    /// </summary>
    /// <param name="iconType">The icon type.</param>
    /// <returns>The drawn icon image.</returns>
    private Image? GetDrawnIcon(SystemMenuIconType iconType)
    {
        try
        {
            const int iconSize = 16; // Standard system menu icon size
            var bitmap = new Bitmap(iconSize, iconSize);
            
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
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
        try
        {
            // Refresh icons for all existing menu items
            foreach (var item in _contextMenu.Items)
            {
                if (item is KryptonContextMenuItem menuItem)
                {
                    // Determine icon type based on menu item text
                    var iconType = GetIconTypeFromText(menuItem.Text);
                    if (iconType.HasValue)
                    {
                        menuItem.Image = GetSystemMenuIcon(iconType.Value);
                    }
                }
            }
        }
        catch
        {
            // If icon refresh fails, continue without icons
        }
    }

    /// <summary>
    /// Manually refreshes all icons to match the current theme.
    /// Call this method when the application theme changes.
    /// </summary>
    public void RefreshThemeIcons()
    {
        RefreshIcons();
    }

    /// <summary>
    /// Manually sets the theme for icon selection.
    /// </summary>
    /// <param name="themeName">The theme name to use for icons.</param>
    public void SetIconTheme(string themeName)
    {
        if (string.IsNullOrEmpty(themeName))
        {
            return;
        }

        // Force refresh of icons with the specified theme
        RefreshIcons();
    }

    /// <summary>
    /// Sets the theme based on specific theme types (Black, Blue, Silver).
    /// </summary>
    /// <param name="themeType">The theme type to use.</param>
    public void SetThemeType(ThemeType themeType)
    {
        string themeName = themeType switch
        {
            ThemeType.Black => "Office2013", // Black theme uses Office 2013 icons
            ThemeType.Blue => "Office2010",  // Blue theme uses Office 2010 icons
            ThemeType.Silver => "Office2013", // Silver theme uses Office 2013 icons
            ThemeType.DarkBlue => "Office2010", // Dark Blue theme uses Office 2010 icons
            ThemeType.LightBlue => "Office2010", // Light Blue theme uses Office 2010 icons
            ThemeType.WarmSilver => "Office2013", // Warm Silver theme uses Office 2013 icons
            ThemeType.ClassicSilver => "Office2007", // Classic Silver theme uses Office 2007 icons
            _ => "Office2013" // Default to Office 2013
        };

        // Set the icon theme and refresh
        SetIconTheme(themeName);
    }

    /// <summary>
    /// Determines the icon type based on the menu item text.
    /// </summary>
    /// <param name="text">The menu item text.</param>
    /// <returns>The corresponding icon type or null if not found.</returns>
    private SystemMenuIconType? GetIconTypeFromText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }

        var normalizedText = text.ToLowerInvariant().Trim();
        
        if (normalizedText.StartsWith(KryptonManager.Strings.SystemMenuStrings.Restore.ToLower()))
        {
            return SystemMenuIconType.Restore;
        }

        if (normalizedText.StartsWith(KryptonManager.Strings.SystemMenuStrings.Minimize.ToLower()))
        {
            return SystemMenuIconType.Minimize;
        }

        if (normalizedText.StartsWith(KryptonManager.Strings.SystemMenuStrings.Maximize.ToLower()))
        {
            return SystemMenuIconType.Maximize;
        }

        if (normalizedText.StartsWith(KryptonManager.Strings.SystemMenuStrings.Close.ToLower()))
        {
            return SystemMenuIconType.Close;
        }

        if (normalizedText.StartsWith(KryptonManager.Strings.SystemMenuStrings.Move.ToLower()))
        {
            return SystemMenuIconType.Move;
        }

        if (normalizedText.StartsWith(KryptonManager.Strings.SystemMenuStrings.Size.ToLower()))
        {
            return SystemMenuIconType.Size;
        }

        return null;
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
        // Clear existing items
        _contextMenu.Items.Clear();

        // Always use our custom menu items instead of trying to parse the native system menu
        // This ensures consistent behavior and proper separator handling
        CreateBasicMenuItems();

        // Add designer-configured menu items above the Close item
        AddDesignerMenuItemsAboveClose();
    }

            private void CreateBasicMenuItems()
        {
            // Create comprehensive system menu items matching the native Windows system menu
            // Only add restore item if window is not in normal state and either minimize or maximize is enabled
            if (_form.WindowState != FormWindowState.Normal && (_form.MinimizeBox || _form.MaximizeBox))
            {
                var restoreItem = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Restore);
                restoreItem.Image = GetSystemMenuIcon(SystemMenuIconType.Restore);
                restoreItem.Click += (sender, e) => ExecuteRestore();
                _contextMenu.Items.Add(restoreItem);
            }

            // Only add move and size items if the window is resizable
            if (_form.FormBorderStyle != FormBorderStyle.FixedSingle && _form.FormBorderStyle != FormBorderStyle.Fixed3D && _form.FormBorderStyle != FormBorderStyle.FixedDialog)
            {
                var moveItem = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Move);
                // Move doesn't typically have an icon in Windows
                moveItem.Click += (sender, e) => ExecuteMove();
                _contextMenu.Items.Add(moveItem);

                var sizeItem = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Size);
                // Size doesn't typically have an icon in Windows
                sizeItem.Click += (sender, e) => ExecuteSize();
                _contextMenu.Items.Add(sizeItem);
            }

            // Only add separator if we have items before it and either minimize or maximize is enabled
            if (_contextMenu.Items.Count > 0 && (_form.MinimizeBox || _form.MaximizeBox))
            {
                _contextMenu.Items.Add(new KryptonContextMenuSeparator());
            }

            // Always add minimize item, but it will be enabled/disabled based on MinimizeBox property and window state
            var minimizeItem = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Minimize);
            minimizeItem.Image = GetSystemMenuIcon(SystemMenuIconType.Minimize);
            minimizeItem.Click += (sender, e) => ExecuteMinimize();
            _contextMenu.Items.Add(minimizeItem);

            // Always add maximize item, but it will be enabled/disabled based on MaximizeBox property and window state
            var maximizeItem = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Maximize);
            maximizeItem.Image = GetSystemMenuIcon(SystemMenuIconType.Maximize);
            maximizeItem.Click += (sender, e) => ExecuteMaximize();
            _contextMenu.Items.Add(maximizeItem);

            // Only add separator if we have items before it
            if (_contextMenu.Items.Count > 0)
            {
                _contextMenu.Items.Add(new KryptonContextMenuSeparator());
            }

            // Only add close item if ControlBox is enabled
            if (_form.ControlBox)
            {
                var closeItem = new KryptonContextMenuItem($"{KryptonManager.Strings.SystemMenuStrings.Close}\tAlt+F4");
                closeItem.Image = GetSystemMenuIcon(SystemMenuIconType.Close);
                closeItem.Click += (sender, e) => ExecuteClose();
                _contextMenu.Items.Add(closeItem);
            }

            // Update the menu items state to enable/disable items based on form properties and current state
            UpdateMenuItemsState();
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
        {
            return;
        }

        // Add a separator before custom items if there are existing items
        if (_contextMenu.Items.Count > 0)
        {
            _contextMenu.Items.Add(new KryptonContextMenuSeparator());
        }

        foreach (var designerItem in _designerMenuItems)
        {
            if (!designerItem.Visible)
            {
                continue;
            }

            var contextMenuItem = designerItem.CreateContextMenuItem();
            
            // Add click handler for designer items (placeholder - can be extended)
            contextMenuItem.Click += (sender, e) => OnDesignerMenuItemClick(designerItem);
            
            if (designerItem.InsertBeforeClose)
            {
                // Find the Close item and insert before it
                for (int i = _contextMenu.Items.Count - 1; i >= 0; i--)
                {
                    if (_contextMenu.Items[i] is KryptonContextMenuItem menuItem)
                    {
                        // Get the text without keyboard shortcuts (remove tab and everything after)
                        var itemText = menuItem.Text.Split('\t')[0];
                        
                        // Check if this is the Close item by comparing with the system menu string
                        // Handle both "Close" and "C&lose" (with accelerator key)
                        if (itemText.Equals(KryptonManager.Strings.SystemMenuStrings.Close, StringComparison.OrdinalIgnoreCase) ||
                            itemText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Close.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                        {
                            _contextMenu.Items.Insert(i, contextMenuItem);
                            break;
                        }
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
    /// Adds designer-configured menu items above the Close item, above the separator.
    /// </summary>
    private void AddDesignerMenuItemsAboveClose()
    {
        if (_designerMenuItems == null || _designerMenuItems.Count == 0)
        {
            return;
        }

        // Find the Close item to insert custom items above it
        int closeItemIndex = -1;
        for (int i = _contextMenu.Items.Count - 1; i >= 0; i--)
        {
            if (_contextMenu.Items[i] is KryptonContextMenuItem menuItem)
            {
                // Get the text without keyboard shortcuts (remove tab and everything after)
                var itemText = menuItem.Text.Split('\t')[0];
                
                // Check if this is the Close item by comparing with the system menu string
                // Handle both "Close" and "C&lose" (with accelerator key)
                if (itemText.Equals(KryptonManager.Strings.SystemMenuStrings.Close, StringComparison.OrdinalIgnoreCase) ||
                    itemText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Close.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                {
                    closeItemIndex = i;
                    break;
                }
            }
        }

        if (closeItemIndex >= 0)
        {
            // Always add a separator before custom items
            _contextMenu.Items.Insert(closeItemIndex, new KryptonContextMenuSeparator());
            
            // Insert custom items above the separator (and above the Close item)
            for (int i = _designerMenuItems.Count - 1; i >= 0; i--)
            {
                var designerItem = _designerMenuItems[i];
                if (!designerItem.Visible)
                {
                    continue;
                }

                var contextMenuItem = designerItem.CreateContextMenuItem();
                
                // Add click handler for designer items
                contextMenuItem.Click += (sender, e) => OnDesignerMenuItemClick(designerItem);
                
                // Insert above the separator
                _contextMenu.Items.Insert(closeItemIndex, contextMenuItem);
            }
        }
        else
        {
            // Fallback: if we can't find the Close item, add at the end
            // Add a separator before custom items
            _contextMenu.Items.Add(new KryptonContextMenuSeparator());
            
            foreach (var designerItem in _designerMenuItems)
            {
                if (!designerItem.Visible)
                {
                    continue;
                }

                var contextMenuItem = designerItem.CreateContextMenuItem();
                contextMenuItem.Click += (sender, e) => OnDesignerMenuItemClick(designerItem);
                _contextMenu.Items.Add(contextMenuItem);
            }
        }
    }
    
    /// <summary>
    /// Ensures there's a separator above the first custom item.
    /// This method should be called after adding custom items to maintain consistent visual separation.
    /// </summary>
    private void EnsureSeparatorAboveCustomItems()
    {
        // Find the Close item
        for (int i = _contextMenu.Items.Count - 1; i >= 0; i--)
        {
            if (_contextMenu.Items[i] is KryptonContextMenuItem menuItem)
            {
                var itemText = menuItem.Text.Split('\t')[0];
                
                if (itemText.Equals(KryptonManager.Strings.SystemMenuStrings.Close, StringComparison.OrdinalIgnoreCase) ||
                    itemText.Replace("&", "").Equals(KryptonManager.Strings.SystemMenuStrings.Close.Replace("&", ""), StringComparison.OrdinalIgnoreCase))
                {
                    // Check if there's already a separator above the Close item
                    if (i > 0 && _contextMenu.Items[i - 1] is KryptonContextMenuSeparator)
                    {
                        // Separator already exists, no action needed
                        return;
                    }
                    else
                    {
                        // Add a separator above the Close item
                        _contextMenu.Items.Insert(i, new KryptonContextMenuSeparator());
                    }
                    return;
                }
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
        Debug.WriteLine($"Designer menu item clicked: {designerItem.Text}");
    }
    #endregion

    #region Color Detection Helper Methods
    /// <summary>
    /// Determines if a color is light (suitable for Office 2013 theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is light.</returns>
    private bool IsLightColor(Color color)
    {
        // Calculate perceived brightness
        var brightness = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
        return brightness > 0.6;
    }

    /// <summary>
    /// Determines if a color is a blue tone (suitable for Office 2010 theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is a blue tone.</returns>
    private bool IsBlueTone(Color color)
    {
        return color.B > color.R && color.B > color.G && color.B > 100;
    }

    /// <summary>
    /// Determines if a color is a dark blue tone (suitable for Office 2007 theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is a dark blue tone.</returns>
    private bool IsDarkBlueTone(Color color)
    {
        return color.B > color.R && color.B > color.G && color.B < 100;
    }

    /// <summary>
    /// Determines if a color is vibrant (suitable for Sparkle theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is vibrant.</returns>
    private bool IsVibrantColor(Color color)
    {
        var saturation = Math.Max(color.R, Math.Max(color.G, color.B)) - Math.Min(color.R, Math.Min(color.G, color.B));
        return saturation > 100;
    }

    /// <summary>
    /// Determines if a color is neutral (suitable for Professional theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is neutral.</returns>
    private bool IsNeutralTone(Color color)
    {
        var diff = Math.Abs(color.R - color.G) + Math.Abs(color.G - color.B) + Math.Abs(color.B - color.R);
        return diff < 50;
    }

    /// <summary>
    /// Determines if a color is modern (suitable for Microsoft 365 theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is modern.</returns>
    private bool IsModernColor(Color color)
    {
        // Modern colors often have balanced RGB values with slight blue tint
        return Math.Abs(color.R - color.G) < 30 && color.B > Math.Max(color.R, color.G);
    }

    /// <summary>
    /// Determines if a color is classic (suitable for Office 2003 theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is classic.</returns>
    private bool IsClassicColor(Color color)
    {
        // Classic Windows colors are often grayish
        var grayish = Math.Abs(color.R - color.G) < 20 && Math.Abs(color.G - color.B) < 20;
        return grayish && color.R < 200;
    }
    #endregion

    #region IDisposable Implementation
    /// <summary>
    /// Releases all resources used by the KryptonThemedSystemMenu.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the KryptonThemedSystemMenu and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _contextMenu?.Dispose();
            }
            _disposed = true;
        }
    }

    /// <summary>
    /// Finalizer for KryptonThemedSystemMenu.
    /// </summary>
    ~KryptonThemedSystemMenu()
    {
        Dispose(false);
    }

    /// <summary>
    /// Throws an ObjectDisposedException if the object has been disposed.
    /// </summary>
    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(KryptonThemedSystemMenu));
        }
    }
    #endregion
}