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
[TypeConverter(typeof(KryptonSystemMenuConverter))]
public class KryptonSystemMenu : IKryptonSystemMenu, IDisposable
{
    #region Instance Fields
    private readonly KryptonForm _form;
    private readonly KryptonContextMenu _contextMenu;
    private SystemMenuItemCollection? _designerMenuItems;
    private bool _disposed;

    // Standard menu item references for direct access
    private KryptonContextMenuItem? _menuItemRestore;
    private KryptonContextMenuItem? _menuItemMove;
    private KryptonContextMenuItem? _menuItemSize;
    private KryptonContextMenuItem? _menuItemMinimize;
    private KryptonContextMenuItem? _menuItemMaximize;
    private KryptonContextMenuItem? _menuItemClose;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSystemMenu class.
    /// </summary>
    /// <param name="form">The form to attach the themed system menu to.</param>
    public KryptonSystemMenu(KryptonForm form)
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
    public SystemMenuItemCollection? DesignerMenuItems
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

            // Get the title bar height from the form's non-client area
            var titleBarHeight = _form.RealWindowBorders.Top;
            screenLocation.Y += titleBarHeight;

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
    public void AddCustomMenuItem(string text, EventHandler? clickHandler, bool insertBeforeClose = true)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("Text cannot be null or empty.", nameof(text));
        }
        
        if (clickHandler == null)
        {
            throw new ArgumentNullException(nameof(clickHandler));
        }

        try
        {
            var customItem = new KryptonContextMenuItem(text);
            customItem.Click += clickHandler;

            if (insertBeforeClose && _menuItemClose != null)
            {
                // Find the Close item index and insert above the separator (above the Close item)
                int closeItemIndex = _contextMenu.Items.IndexOf(_menuItemClose);
                if (closeItemIndex >= 0)
                {
                    // Insert above the separator (above the Close item)
                    // First, check if there's a separator above the Close item
                    if (closeItemIndex > 0 && _contextMenu.Items[closeItemIndex - 1] is KryptonContextMenuSeparator)
                    {
                        _contextMenu.Items.Insert(closeItemIndex - 1, customItem);
                    }
                    else
                    {
                        // If no separator, add one above the Close item first
                        _contextMenu.Items.Insert(closeItemIndex, new KryptonContextMenuSeparator());
                        // Then insert the custom item above the separator
                        _contextMenu.Items.Insert(closeItemIndex, customItem);
                    }
                    return;
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
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding custom menu item: {ex.Message}");
            throw;
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

        if (insertBeforeClose && _menuItemClose != null)
        {
            // Find the Close item index and insert above the separator (above the Close item)
            int closeItemIndex = _contextMenu.Items.IndexOf(_menuItemClose);
            if (closeItemIndex >= 0)
            {
                // Insert above the separator (above the Close item)
                // First, check if there's already a separator above the Close item
                if (closeItemIndex > 0 && _contextMenu.Items[closeItemIndex - 1] is KryptonContextMenuSeparator)
                {
                    // If separator already exists, insert above it
                    _contextMenu.Items.Insert(closeItemIndex - 1, separator);
                }
                else
                {
                    // If no separator, insert directly above the Close item
                    _contextMenu.Items.Insert(closeItemIndex, separator);
                }
                return;
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
        var windowState = _form.GetWindowState();

        // Update menu items based on current state using direct field references
        if (_menuItemRestore != null) 
        {
            _menuItemRestore.Enabled = (windowState != FormWindowState.Normal);
        }
        
        // Minimize item is enabled only if MinimizeBox is true and window is not already minimized
        if (_menuItemMinimize != null) 
        {
            _menuItemMinimize.Enabled = _form.MinimizeBox && (windowState != FormWindowState.Minimized);
        }

        // Maximize item is enabled only if MaximizeBox is true and window is not already maximized
        if (_menuItemMaximize != null)
        {
            _menuItemMaximize.Enabled = _form.MaximizeBox && (windowState != FormWindowState.Maximized);
        }
        
        // Move is enabled when window is in Normal state (can be moved) or when minimized (can be restored)
        if (_menuItemMove != null)
        {
            _menuItemMove.Enabled = (windowState == FormWindowState.Normal) || (windowState == FormWindowState.Minimized);
        }
        
        // Size is enabled when window is in Normal state and form is sizable
        if (_menuItemSize != null)
        {
            _menuItemSize.Enabled = (windowState == FormWindowState.Normal) &&
                                     (_form.FormBorderStyle == FormBorderStyle.Sizable || _form.FormBorderStyle == FormBorderStyle.SizableToolWindow);
        }

        // Close is always enabled (no need to check _menuItemClose as it's always enabled)
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
    private Image? ProcessImageForTransparency(Image? originalImage)
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
    /// Determines the current theme based on the form's palette.
    /// </summary>
    /// <returns>The current theme name.</returns>
    public string GetCurrentTheme()
    {
        try
        {
            // Get the current theme from the form's palette
            var palette = _form.GetResolvedPalette();
            if (palette != null)
            {
                // Detect theme based on palette characteristics
                // This is a simplified detection - you can enhance this logic
                var headerColor = palette.GetBackColor1(PaletteBackStyle.HeaderForm, PaletteState.Normal);

                // Determine theme based on header color characteristics
                return headerColor switch
                {
                    var color when IsLightColor(color) => "Office2013",        // typically white/light gray
                    var color when IsBlueTone(color) => "Office2010",          // typically blue tones
                    var color when IsDarkBlueTone(color) => "Office2007",      // typically darker blue
                    var color when IsVibrantColor(color) => "Sparkle",         // typically vibrant colors
                    var color when IsNeutralTone(color) => "Professional",     // typically neutral tones
                    var color when IsModernColor(color) => "Microsoft365",     // typically modern colors
                    var color when IsClassicColor(color) => "Office2003",      // typically classic Windows colors
                    _ => "Office2013" // default fallback
                };
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
            // Get the color from the current palette
            var palette = _form.GetResolvedPalette();
            if (palette != null)
            {
                return palette.GetContentShortTextColor1(PaletteContentStyle.HeaderForm, PaletteState.Normal);
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
            // Get the color from the current palette
            var palette = _form.GetResolvedPalette();
            if (palette != null)
            {
                return palette.GetBackColor1(PaletteBackStyle.HeaderForm, PaletteState.Normal);
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
            // Refresh icons for standard menu items using direct field references
            if (_menuItemRestore != null)
            {
                _menuItemRestore.Image = GetSystemMenuIcon(SystemMenuIconType.Restore);
            }

            if (_menuItemMinimize != null)
            {
                _menuItemMinimize.Image = GetSystemMenuIcon(SystemMenuIconType.Minimize);
            }

            if (_menuItemMaximize != null)
            {
                _menuItemMaximize.Image = GetSystemMenuIcon(SystemMenuIconType.Maximize);
            }

            if (_menuItemClose != null)
            {
                _menuItemClose.Image = GetSystemMenuIcon(SystemMenuIconType.Close);
            }

            // Move and Size items don't typically have icons in Windows, so no need to refresh them
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
        try
        {
            // Clear existing items
            _contextMenu.Items.Clear();

            // Always use our custom menu items instead of trying to parse the native system menu
            // This ensures consistent behavior and proper separator handling
            CreateBasicMenuItems();

            // Add designer-configured menu items respecting their positioning preferences
            AddDesignerMenuItemsAboveClose();
        }
        catch (Exception ex)
        {
            // Log the error and ensure we have at least a basic menu
            Debug.WriteLine($"Error building system menu: {ex.Message}");
            
            // Ensure we have a basic menu even if there's an error
            if (_contextMenu.Items.Count == 0)
            {
                CreateBasicMenuItems();
            }
        }
    }

    private void CreateBasicMenuItems()
    {
        // Create comprehensive system menu items matching the native Windows system menu
        // Only add restore item if window is not in normal state and either minimize or maximize is enabled
        if (_form.WindowState != FormWindowState.Normal && (_form.MinimizeBox || _form.MaximizeBox))
        {
            _menuItemRestore = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Restore);
            _menuItemRestore.Image = GetSystemMenuIcon(SystemMenuIconType.Restore);
            _menuItemRestore.Click += OnRestoreItemOnClick;
            _contextMenu.Items.Add(_menuItemRestore);
        }

        // Only add move and size items if the window is resizable
        if (_form.FormBorderStyle != FormBorderStyle.FixedSingle && _form.FormBorderStyle != FormBorderStyle.Fixed3D && _form.FormBorderStyle != FormBorderStyle.FixedDialog)
        {
            _menuItemMove = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Move);
            // Move doesn't typically have an icon in Windows
            _menuItemMove.Click += (sender, e) => ExecuteMove();
            _contextMenu.Items.Add(_menuItemMove);

            _menuItemSize = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Size);
            // Size doesn't typically have an icon in Windows
            _menuItemSize.Click += (sender, e) => ExecuteSize();
            _contextMenu.Items.Add(_menuItemSize);
        }

        // Only add separator if we have items before it and either minimize or maximize is enabled
        if (_contextMenu.Items.Count > 0 && (_form.MinimizeBox || _form.MaximizeBox))
        {
            _contextMenu.Items.Add(new KryptonContextMenuSeparator());
        }

        // Always add minimize item, but it will be enabled/disabled based on MinimizeBox property and window state
        _menuItemMinimize = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Minimize);
        _menuItemMinimize.Image = GetSystemMenuIcon(SystemMenuIconType.Minimize);
        _menuItemMinimize.Click += OnMinimizeItemOnClick;
        _contextMenu.Items.Add(_menuItemMinimize);

        // Always add maximize item, but it will be enabled/disabled based on MaximizeBox property and window state
        _menuItemMaximize = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Maximize);
        _menuItemMaximize.Image = GetSystemMenuIcon(SystemMenuIconType.Maximize);
        _menuItemMaximize.Click += OnMaximizeItemOnClick;
        _contextMenu.Items.Add(_menuItemMaximize);

        // Only add separator if we have items before it
        if (_contextMenu.Items.Count > 0)
        {
            _contextMenu.Items.Add(new KryptonContextMenuSeparator());
        }

        // Only add close item if ControlBox is enabled
        if (_form.ControlBox)
        {
            _menuItemClose = new KryptonContextMenuItem($"{KryptonManager.Strings.SystemMenuStrings.Close}\tAlt+F4");
            _menuItemClose.Image = GetSystemMenuIcon(SystemMenuIconType.Close);
            _menuItemClose.Click += OnCloseItemOnClick;
            _contextMenu.Items.Add(_menuItemClose);
        }

        // Update the menu items state to enable/disable items based on form properties and current state
        UpdateMenuItemsState();
    }

    private void OnMaximizeItemOnClick(object? sender, EventArgs e) => ExecuteMaximize();

    private void OnCloseItemOnClick(object? sender, EventArgs e) => ExecuteClose();

    private void OnMinimizeItemOnClick(object? sender, EventArgs e) => ExecuteMinimize();

    private void OnRestoreItemOnClick(object? sender, EventArgs e) => ExecuteRestore();

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
            // Use the KryptonForm's close mechanism
            _form.Close();
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

        // Send the system command using KryptonForm's method
        _form.SendSysCommand(command, lParam);
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
    /// Adds designer-configured menu items respecting their InsertBeforeClose property.
    /// </summary>
    private void AddDesignerMenuItemsAboveClose()
    {
        if (_designerMenuItems == null || _designerMenuItems.Count == 0)
        {
            return;
        }

        // Separate items by their InsertBeforeClose property
        var itemsBeforeClose = new List<SystemMenuItemValues>();
        var itemsAfterClose = new List<SystemMenuItemValues>();

        foreach (var designerItem in _designerMenuItems)
        {
            if (!designerItem.Visible)
            {
                continue;
            }

            if (designerItem.InsertBeforeClose)
            {
                itemsBeforeClose.Add(designerItem);
            }
            else
            {
                itemsAfterClose.Add(designerItem);
            }
        }

        // Add items that should be inserted before Close
        if (itemsBeforeClose.Count > 0)
        {
            AddDesignerMenuItemsBeforeClose(itemsBeforeClose);
        }

        // Add items that should be inserted after Close
        if (itemsAfterClose.Count > 0)
        {
            AddDesignerMenuItemsAfterClose(itemsAfterClose);
        }
    }

    /// <summary>
    /// Adds designer menu items before the Close item.
    /// </summary>
    private void AddDesignerMenuItemsBeforeClose(List<SystemMenuItemValues> items)
    {
        // Find the Close item to insert custom items above it
        int closeItemIndex = _menuItemClose != null ? _contextMenu.Items.IndexOf(_menuItemClose) : -1;

        if (closeItemIndex >= 0)
        {
            // Always add a separator before custom items
            _contextMenu.Items.Insert(closeItemIndex, new KryptonContextMenuSeparator());

            // Insert custom items above the separator (and above the Close item)
            for (int i = items.Count - 1; i >= 0; i--)
            {
                var designerItem = items[i];
                var contextMenuItem = designerItem.CreateContextMenuItem();

                // Add click handler for designer items
                void OnContextMenuItemOnClick(object? sender, EventArgs e) => OnDesignerMenuItemClick(designerItem);

                contextMenuItem.Click += OnContextMenuItemOnClick;

                // Insert above the separator
                _contextMenu.Items.Insert(closeItemIndex, contextMenuItem);
            }
        }
        else
        {
            // Fallback: if we can't find the Close item, add at the end
            AddDesignerMenuItemsAfterClose(items);
        }
    }

    /// <summary>
    /// Adds designer menu items after the Close item.
    /// </summary>
    private void AddDesignerMenuItemsAfterClose(List<SystemMenuItemValues> items)
    {
        // Add a separator before custom items if we're adding at the end
        _contextMenu.Items.Add(new KryptonContextMenuSeparator());

        foreach (var designerItem in items)
        {
            var contextMenuItem = designerItem.CreateContextMenuItem();

            void OnContextMenuItemOnClick(object? sender, EventArgs e) => OnDesignerMenuItemClick(designerItem);

            contextMenuItem.Click += OnContextMenuItemOnClick;
            _contextMenu.Items.Add(contextMenuItem);
        }
    }

    /// <summary>
    /// Ensures there's a separator above the first custom item.
    /// This method should be called after adding custom items to maintain consistent visual separation.
    /// </summary>
    private void EnsureSeparatorAboveCustomItems()
    {
        // Find the Close item using direct reference
        if (_menuItemClose != null)
        {
            int closeItemIndex = _contextMenu.Items.IndexOf(_menuItemClose);
            if (closeItemIndex >= 0)
            {
                // Check if there's already a separator above the Close item
                if (closeItemIndex > 0 && _contextMenu.Items[closeItemIndex - 1] is KryptonContextMenuSeparator)
                {
                    // Separator already exists, no action needed
                    return;
                }
                else
                {
                    // Add a separator above the Close item
                    _contextMenu.Items.Insert(closeItemIndex, new KryptonContextMenuSeparator());
                }
            }
        }
    }

    /// <summary>
    /// Handles clicks on designer-configured menu items.
    /// </summary>
    /// <param name="designerItem">The designer menu item that was clicked.</param>
    private void OnDesignerMenuItemClick(SystemMenuItemValues designerItem)
    {
        try
        {
            // Execute the associated command if one is set
            if (designerItem.Command != null)
            {
                designerItem.Command.PerformExecute();
                return;
            }

            // This is a placeholder - in a real implementation, you might want to:
            // 1. Raise a custom event that the form can handle
            // 2. Use a callback mechanism
            // 3. Integrate with a command pattern

            // For now, we'll just log or handle it silently
            Debug.WriteLine($"Designer menu item clicked: {designerItem.Text}");
        }
        catch (Exception ex)
        {
            // Log the error and continue
            Debug.WriteLine($"Error handling designer menu item click: {ex.Message}");
        }
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
    /// Releases all resources used by the KryptonSystemMenu.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the KryptonSystemMenu and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _contextMenu.Dispose();
            }
            _disposed = true;
        }
    }

    /// <summary>
    /// Finalizer for KryptonSystemMenu.
    /// </summary>
    ~KryptonSystemMenu()
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
            throw new ObjectDisposedException(nameof(KryptonSystemMenu));
        }
    }
    #endregion
}