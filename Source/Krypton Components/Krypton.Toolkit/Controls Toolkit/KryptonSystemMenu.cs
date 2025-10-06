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
public class KryptonSystemMenu : IKryptonSystemMenu, IDisposable
{
    #region Instance Fields
    private readonly KryptonForm _form;
    private readonly KryptonContextMenu _contextMenu;
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
            // The native system menu appears at the very top-left corner of the form window
            var formLocation = _form.Location;
            var screenLocation = new Point(formLocation.X, formLocation.Y);

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

        // Handle Alt+Space for showing the menu (only if enabled)
        if (keyData == (Keys.Alt | Keys.Space) && ShowOnAltSpace)
        {
            ShowAtFormTopLeft();
            return true;
        }

        return false;
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
            var currentTheme = GetCurrentTheme();
            var icon = GetThemeIcon(currentTheme, iconType);
            if (icon != null)
            {
                var processedIcon = ProcessImageForTransparency(icon);
                if (processedIcon != null)
                {
                    return processedIcon;
                }
            }

            return GetDrawnIcon(iconType);
        }
        catch
        {
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
            if (originalImage.PixelFormat == PixelFormat.Format32bppArgb)
            {
                return originalImage;
            }

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
            Debug.WriteLine($"Failed to process image transparency: {ex.Message}");
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
            var palette = _form.GetResolvedPalette();
            if (palette != null)
            {
                var headerColor = palette.GetBackColor1(PaletteBackStyle.HeaderForm, PaletteState.Normal);

                return headerColor switch
                {
                    var color when IsLightColor(color) => "Office2013",
                    var color when IsBlueTone(color) => "Office2010",
                    var color when IsDarkBlueTone(color) => "Office2007",
                    var color when IsVibrantColor(color) => "Sparkle",
                    var color when IsNeutralTone(color) => "Professional",
                    var color when IsModernColor(color) => "Microsoft365",
                    var color when IsClassicColor(color) => "Office2003",
                    _ => "Office2013"
                };
            }
        }
        catch
        {
            // Fallback to default theme
        }

        return "Office2013";
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
            const int iconSize = 16;
            var bitmap = new Bitmap(iconSize, iconSize);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(Color.Transparent);

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
            ThemeType.Black => "Office2013",
            ThemeType.Blue => "Office2010",
            ThemeType.Silver => "Office2013",
            ThemeType.DarkBlue => "Office2010",
            ThemeType.LightBlue => "Office2010",
            ThemeType.WarmSilver => "Office2013",
            ThemeType.ClassicSilver => "Office2007",
            _ => "Office2013"
        };

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
            _contextMenu.Items.Clear();
            CreateBasicMenuItems();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error building system menu: {ex.Message}");
            
            if (_contextMenu.Items.Count == 0)
            {
                CreateBasicMenuItems();
            }
        }
    }

    private void CreateBasicMenuItems()
    {
        if (_form.WindowState != FormWindowState.Normal && (_form.MinimizeBox || _form.MaximizeBox))
        {
            _menuItemRestore = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Restore);
            _menuItemRestore.Image = GetSystemMenuIcon(SystemMenuIconType.Restore);
            _menuItemRestore.Click += OnRestoreItemOnClick;
            _contextMenu.Items.Add(_menuItemRestore);
        }

        if (_form.FormBorderStyle != FormBorderStyle.FixedSingle && _form.FormBorderStyle != FormBorderStyle.Fixed3D && _form.FormBorderStyle != FormBorderStyle.FixedDialog)
        {
            _menuItemMove = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Move);
            _menuItemMove.Click += (sender, e) => ExecuteMove();
            _contextMenu.Items.Add(_menuItemMove);

            _menuItemSize = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Size);
            _menuItemSize.Click += (sender, e) => ExecuteSize();
            _contextMenu.Items.Add(_menuItemSize);
        }

        if (_contextMenu.Items.Count > 0 && (_form.MinimizeBox || _form.MaximizeBox))
        {
            _contextMenu.Items.Add(new KryptonContextMenuSeparator());
        }

        _menuItemMinimize = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Minimize);
        _menuItemMinimize.Image = GetSystemMenuIcon(SystemMenuIconType.Minimize);
        _menuItemMinimize.Click += OnMinimizeItemOnClick;
        _contextMenu.Items.Add(_menuItemMinimize);

        _menuItemMaximize = new KryptonContextMenuItem(KryptonManager.Strings.SystemMenuStrings.Maximize);
        _menuItemMaximize.Image = GetSystemMenuIcon(SystemMenuIconType.Maximize);
        _menuItemMaximize.Click += OnMaximizeItemOnClick;
        _contextMenu.Items.Add(_menuItemMaximize);

        if (_contextMenu.Items.Count > 0)
        {
            _contextMenu.Items.Add(new KryptonContextMenuSeparator());
        }

        if (_form.ControlBox)
        {
            _menuItemClose = new KryptonContextMenuItem($"{KryptonManager.Strings.SystemMenuStrings.Close}\tAlt+F4");
            _menuItemClose.Image = GetSystemMenuIcon(SystemMenuIconType.Close);
            _menuItemClose.Click += OnCloseItemOnClick;
            _contextMenu.Items.Add(_menuItemClose);
        }

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
                SendSysCommand(PI.SC_.RESTORE);
            }
        }
        catch
        {
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
            SendSysCommand(PI.SC_.MOVE);
        }
        catch
        {
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
            SendSysCommand(PI.SC_.SIZE);
        }
        catch
        {
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
                SendSysCommand(PI.SC_.MINIMIZE);
            }
        }
        catch
        {
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
                SendSysCommand(PI.SC_.MAXIMIZE);
            }
        }
        catch
        {
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
            _form.Close();
        }
        catch
        {
            SendSysCommand(PI.SC_.CLOSE);
        }
    }

    #endregion

    private void SendSysCommand(PI.SC_ command)
    {
        var screenPos = Control.MousePosition;
        var lParam = (IntPtr)(PI.MAKELOWORD(screenPos.X) | PI.MAKEHIWORD(screenPos.Y));
        _form.SendSysCommand(command, lParam);
    }

    /// <summary>
    /// Adjusts the menu position to ensure it's fully visible on screen.
    /// </summary>
    /// <param name="originalLocation">The original screen location.</param>
    /// <returns>The adjusted screen location.</returns>
    private Point AdjustMenuPosition(Point originalLocation)
    {
        var screenBounds = Screen.FromControl(_form).Bounds;
        var estimatedMenuWidth = 200;
        var estimatedMenuHeight = _contextMenu.Items.Count * 25;

        if (originalLocation.X + estimatedMenuWidth > screenBounds.Right)
        {
            originalLocation.X = screenBounds.Right - estimatedMenuWidth;
        }

        if (originalLocation.Y + estimatedMenuHeight > screenBounds.Bottom)
        {
            originalLocation.Y = screenBounds.Bottom - estimatedMenuHeight;
        }

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

    #region Color Detection Helper Methods
    /// <summary>
    /// Determines if a color is light (suitable for Office 2013 theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is light.</returns>
    private bool IsLightColor(Color color)
    {
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
        return Math.Abs(color.R - color.G) < 30 && color.B > Math.Max(color.R, color.G);
    }

    /// <summary>
    /// Determines if a color is classic (suitable for Office 2003 theme).
    /// </summary>
    /// <param name="color">The color to test.</param>
    /// <returns>True if the color is classic.</returns>
    private bool IsClassicColor(Color color)
    {
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