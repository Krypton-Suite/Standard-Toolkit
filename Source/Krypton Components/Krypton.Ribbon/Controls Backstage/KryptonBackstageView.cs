#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Provides an Office 2010-style backstage navigation surface.
/// </summary>
[ToolboxItem(true)]
[DefaultEvent(nameof(SelectedPageChanged))]
[DefaultProperty(nameof(Pages))]
[Designer(typeof(KryptonBackstageViewDesigner))]
[DesignerCategory(@"code")]
[Description(@"Office 2010-style Backstage view surface for use with KryptonRibbon File tab.")]
public class KryptonBackstageView : KryptonPanel
{
    #region Instance Fields

    private readonly KryptonBackstagePageCollection _pages;
    private readonly KryptonBackstageCommandCollection _commands;
    private KryptonBackstagePage? _selectedPage;

    private readonly KryptonPanel _navigationPanel;
    private readonly BackstageNavigationList _navigationList;
    private readonly KryptonPanel _pageContainer;

    private int _navigationWidth;
    private bool _suspendSync;
    private readonly BackStageViewColorValues _colorValues;
    private readonly BackstageCloseItem _closeItem;
    private BackstageOverlayMode _overlayMode;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the selected page changes.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Occurs when the selected page changes.")]
    public event EventHandler? SelectedPageChanged;
    
    /// <summary>
    /// Occurs when the Close button is clicked, allowing the developer to cancel the application close.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Occurs when the Close button is clicked, allowing the developer to cancel the application close.")]
    public event CancelEventHandler? CloseRequested;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonBackstageView"/> class.
    /// </summary>
    public KryptonBackstageView()
    {
        _navigationWidth = 200;
        _overlayMode = BackstageOverlayMode.FullClient;

        _pages = new KryptonBackstagePageCollection();
        _pages.Inserted += OnPagesInserted;
        _pages.Removed += OnPagesRemoved;
        _pages.Clearing += OnPagesCleared;

        _commands = new KryptonBackstageCommandCollection();
        _commands.Inserted += OnCommandsInserted;
        _commands.Removed += OnCommandsRemoved;
        _commands.Clearing += OnCommandsCleared;

        // Initialize colors object
        _colorValues = new BackStageViewColorValues(OnColorsNeedPaint);

        // Create the permanent Close item
        _closeItem = new BackstageCloseItem();

        // Left navigation area - defaults to PanelAlternate style
        _navigationPanel = new KryptonPanel
        {
            Dock = DockStyle.Left,
            Width = _navigationWidth,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };

        _navigationList = new BackstageNavigationList(this)
        {
            Dock = DockStyle.Fill
        };
        _navigationList.SelectedIndexChanged += OnNavigationSelectedIndexChanged;

        _navigationPanel.Controls.Add(_navigationList);

        // Page container area - defaults to PanelClient style
        _pageContainer = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        Controls.Add(_pageContainer);
        Controls.Add(_navigationPanel);

        // Hook into palette changes to update colors
        PaletteChanged += OnPaletteChanged;
    }

    #endregion

    #region Public
    
    /// <summary>
    /// Gets the backstage pages collection.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Collection of pages hosted by the backstage view.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(BackstagePageCollectionEditor), typeof(UITypeEditor))]
    public KryptonBackstagePageCollection Pages => _pages;

    /// <summary>
    /// Gets the backstage commands collection.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Collection of command-only items (no associated page) in the backstage view.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonBackstageCommandCollection Commands => _commands;

    /// <summary>
    /// Gets and sets the selected page.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Selected backstage page.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonBackstagePage? SelectedPage
    {
        get => _selectedPage;
        set
        {
            if (!ReferenceEquals(_selectedPage, value))
            {
                SelectPage(value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the width of the left navigation panel.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Width of the left navigation panel.")]
    [DefaultValue(200)]
    public int NavigationWidth
    {
        get => _navigationWidth;
        set
        {
            if (_navigationWidth != value)
            {
                _navigationWidth = value;
                _navigationPanel.Width = value;
                PerformLayout();
            }
        }
    }

    /// <summary>
    /// Gets access to the internal page container (used by the designer).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonPanel PageContainer => _pageContainer;

    /// <summary>
    /// Gets access to the backstage colors.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Groups backstage view color properties.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BackStageViewColorValues BackStageViewColors => _colorValues;

    private bool ShouldSerializeBackStageViewColors() => !_colorValues.IsDefault;

    /// <summary>
    /// Gets and sets the overlay coverage mode.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Determines whether the overlay covers the full client area or only the area below the ribbon.")]
    [DefaultValue(BackstageOverlayMode.FullClient)]
    public BackstageOverlayMode OverlayMode
    {
        get => _overlayMode;
        set
        {
            if (_overlayMode != value)
            {
                _overlayMode = value;
                PerformNeedPaint(false);
            }
        }
    }

    #endregion

    #region Implementation

    private void SelectPage(KryptonBackstagePage? page)
    {
        _suspendSync = true;
        try
        {
            if (_selectedPage != null)
            {
                _selectedPage.Visible = false;
            }

            _selectedPage = page;

            if (_selectedPage != null)
            {
                _selectedPage.Dock = DockStyle.Fill;
                _selectedPage.Visible = true;
                _selectedPage.BringToFront();

                if (!ReferenceEquals(_navigationList.SelectedItem, _selectedPage))
                {
                    _navigationList.SelectedItem = _selectedPage;
                }
            }
            else
            {
                _navigationList.ClearSelected();
            }
        }
        finally
        {
            _suspendSync = false;
        }

        SelectedPageChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnNavigationSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_suspendSync)
        {
            return;
        }

        var selectedItem = _navigationList.SelectedItem;

        // Check if the Close item was clicked
        if (selectedItem is BackstageCloseItem)
        {
            // Raise the CloseRequested event to allow developers to cancel or handle the close
            var cancelEventArgs = new CancelEventArgs(false);
            OnCloseRequested(cancelEventArgs);

            // If the event was canceled, don't close the application
            if (cancelEventArgs.Cancel)
            {
                // Clear the selection so the Close button doesn't appear selected
                _navigationList.ClearSelected();
                if (_selectedPage != null)
                {
                    _navigationList.SelectedItem = _selectedPage;
                }
                return;
            }

            // Close the main form (which will close the application)
            CloseApplication();
            return;
        }

        // Check if a command was clicked
        if (selectedItem is KryptonBackstageCommand command)
        {
            // Execute the command
            command.PerformClick();
            // Clear selection after command execution
            _navigationList.ClearSelected();
            if (_selectedPage != null)
            {
                _navigationList.SelectedItem = _selectedPage;
            }
            return;
        }

        var page = selectedItem as KryptonBackstagePage;
        if (!ReferenceEquals(_selectedPage, page))
        {
            SelectPage(page);
        }
    }

    /// <summary>
    /// Raises the <see cref="CloseRequested"/> event.
    /// </summary>
    /// <param name="e">A <see cref="CancelEventArgs"/> containing the event data.</param>
    protected virtual void OnCloseRequested(CancelEventArgs e) => CloseRequested?.Invoke(this, e);

    /// <summary>
    /// Closes the main application form.
    /// </summary>
    private void CloseApplication()
    {
        // Find the main form (the form that owns the ribbon)
        var form = FindMainForm();
        if (form != null && !form.IsDisposed)
        {
            form.Close();
        }
        else
        {
            // Fallback: if we can't find the form, use Application.Exit()
            Application.Exit();
        }
    }

    /// <summary>
    /// Finds the main form that contains the ribbon.
    /// </summary>
    private Form? FindMainForm()
    {
        // First, try to find the form that owns this control
        var form = FindForm();
        if (form != null)
        {
            // If this form is an overlay form, get its owner
            if (form.Owner != null)
            {
                return form.Owner;
            }
            return form;
        }

        // Fallback: try to find the ribbon and then its form
        var ribbon = FindKryptonRibbon();
        return ribbon?.FindForm();
    }

    /// <summary>
    /// Finds the parent KryptonRibbon control by walking up the control hierarchy.
    /// </summary>
    private KryptonRibbon? FindKryptonRibbon()
    {
        // First, try walking up the parent chain
        var control = Parent;
        while (control != null)
        {
            if (control is KryptonRibbon ribbon)
            {
                return ribbon;
            }
            control = control.Parent;
        }

        // If not found, check the form that contains this control
        var form = FindForm();
        if (form != null)
        {
            // Check if this form is an overlay form (owned by another form)
            if (form.Owner != null)
            {
                // Search in the owner form's controls
                var found = FindKryptonRibbonInControls(form.Owner.Controls);
                if (found != null)
                {
                    return found;
                }
            }

            // Also search in the current form's controls
            var foundInForm = FindKryptonRibbonInControls(form.Controls);
            if (foundInForm != null)
            {
                return foundInForm;
            }
        }

        return null;
    }

    /// <summary>
    /// Recursively searches for KryptonRibbon in a control collection.
    /// </summary>
    private static KryptonRibbon? FindKryptonRibbonInControls(ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is KryptonRibbon ribbon)
            {
                return ribbon;
            }

            if (control.HasChildren)
            {
                var found = FindKryptonRibbonInControls(control.Controls);
                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }

    private void OnPagesInserted(object? sender, TypedCollectionEventArgs<KryptonBackstagePage> e)
    {
        KryptonBackstagePage? page = e.Item;
        if (page == null)
        {
            return;
        }

        // Ensure page is hosted inside our internal page container
        if (page.Parent != _pageContainer)
        {
            _pageContainer.Controls.Add(page);
        }

        page.Dock = DockStyle.Fill;
        page.Visible = false;
        page.NavigationPropertyChanged += OnPageNavigationPropertyChanged;

        RebuildNavigationList();

        // Auto-select first page
        if (_selectedPage == null)
        {
            SelectPage(page);
        }
    }

    private void OnPagesRemoved(object? sender, TypedCollectionEventArgs<KryptonBackstagePage> e)
    {
        KryptonBackstagePage? page = e.Item;
        if (page == null)
        {
            return;
        }

        page.NavigationPropertyChanged -= OnPageNavigationPropertyChanged;

        if (page.Parent == _pageContainer)
        {
            _pageContainer.Controls.Remove(page);
        }

        RebuildNavigationList();

        // If we removed the selected page, select another
        if (ReferenceEquals(_selectedPage, page))
        {
            SelectPage(_pages.Count > 0 ? _pages[0] : null);
        }
    }

    private void OnPagesCleared(object? sender, EventArgs e)
    {
        foreach (KryptonBackstagePage page in _pages.ToArray())
        {
            page.NavigationPropertyChanged -= OnPageNavigationPropertyChanged;
        }

        _pageContainer.Controls.Clear();
        _navigationList.Items.Clear();
        SelectPage(null);
    }

    private void OnPageNavigationPropertyChanged(object? sender, EventArgs e) => RebuildNavigationList();

    private void UpdateNavigationColors()
    {
        if (_colorValues.NavigationBackgroundColor.HasValue)
        {
            _navigationPanel.StateCommon.Color1 = _colorValues.NavigationBackgroundColor.Value;
            _navigationPanel.StateCommon.ColorStyle = PaletteColorStyle.Solid;
        }
        else
        {
            // Reset to use PanelAlternate palette
            _navigationPanel.StateCommon.ColorStyle = PaletteColorStyle.Inherit;
            _navigationPanel.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        }
        _navigationPanel.Invalidate();
    }

    private void UpdateContentColors()
    {
        if (_colorValues.ContentBackgroundColor.HasValue)
        {
            _pageContainer.StateCommon.Color1 = _colorValues.ContentBackgroundColor.Value;
            _pageContainer.StateCommon.ColorStyle = PaletteColorStyle.Solid;
        }
        else
        {
            // Reset to use PanelClient palette
            _pageContainer.StateCommon.ColorStyle = PaletteColorStyle.Inherit;
            _pageContainer.PanelBackStyle = PaletteBackStyle.PanelClient;
        }
        _pageContainer.Invalidate();
    }

    private void OnColorsNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        UpdateNavigationColors();
        UpdateContentColors();
        _navigationList.Invalidate();
        PerformNeedPaint(e.NeedLayout);
    }

    internal Color GetNavigationBackgroundColor()
    {
        if (_colorValues.NavigationBackgroundColor.HasValue)
        {
            return _colorValues.NavigationBackgroundColor.Value;
        }

        // Theme-aware defaults
        var palette = GetPalette();
        if (IsOffice2013Theme(palette))
        {
            // Office 2013: Dark blue navigation panel
            return Color.FromArgb(31, 78, 121);
        }

        // Office 2010 and others: Use PanelAlternate palette color
        return palette?.GetBackColor1(PaletteBackStyle.PanelAlternate, PaletteState.Normal) ?? Color.FromArgb(240, 240, 240);
    }

    internal Color GetContentBackgroundColor()
    {
        if (_colorValues.ContentBackgroundColor.HasValue)
        {
            return _colorValues.ContentBackgroundColor.Value;
        }

        // Get color from PanelClient palette
        var palette = GetPalette();
        return palette?.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal) ?? Color.White;
    }

    internal Color GetSelectedItemHighlightColor()
    {
        if (_colorValues.SelectedItemHighlightColor.HasValue)
        {
            return _colorValues.SelectedItemHighlightColor.Value;
        }

        // Theme-aware defaults
        var palette = GetPalette();

        if (palette != null)
        {
            try
            {
                /*// Try to get the highlight color from the palette
                var highlightColor = palette.GetBackColor1(PaletteBackStyle.ButtonNavigatorStack, PaletteState.Tracking);
                if (highlightColor.ToArgb() != Color.Empty.ToArgb())
                {
                    return highlightColor;
                }*/

                return palette.GetArrayColor(SchemeTrackingColors.MenuItemSelectedBegin);
            }
            catch
            {
                if (IsOffice2013Theme(palette) || IsMicrosoft365Theme(palette))
                {
                    // Office 2013 / Microsoft 365: Lighter blue highlight for selected items
                    return Color.FromArgb(68, 114, 196);
                }
            }
        }

        // Office 2010: Orange highlight
        return Color.FromArgb(242, 155, 57);
    }

    private static bool IsOffice2013Theme(PaletteBase? palette)
    {
        if (palette == null)
        {
            return false;
        }

        // Check if the palette is an Office 2013 palette
        var paletteType = palette.GetType();
        var name = paletteType.Name;
        var nameSpace = paletteType.Namespace;

        return (name != null && name.IndexOf("Office2013", StringComparison.OrdinalIgnoreCase) >= 0) ||
               (nameSpace != null && nameSpace.IndexOf("Office 2013", StringComparison.OrdinalIgnoreCase) >= 0);
    }

    private static bool IsMicrosoft365Theme(PaletteBase? palette)
    {
        if (palette == null)
        {
            return false;
        }

        // Check if the palette is a Microsoft 365 palette by checking the renderer type
        var renderer = palette.GetRenderer();
        if (renderer != null)
        {
            var rendererType = renderer.GetType();
            var rendererName = rendererType.Name;
            if (rendererName != null && rendererName.IndexOf("Microsoft365", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
        }

        // Also check the palette type name and namespace as a fallback
        var paletteType = palette.GetType();
        var name = paletteType.Name;
        var nameSpace = paletteType.Namespace;

        return (name != null && name.IndexOf("Microsoft365", StringComparison.OrdinalIgnoreCase) >= 0) ||
               (nameSpace != null && nameSpace.IndexOf("Microsoft 365", StringComparison.OrdinalIgnoreCase) >= 0);
    }

    private PaletteBase? GetPalette()
    {
        // Use our own resolved palette (inherits from VisualPanel)
        return GetResolvedPalette();
    }

    private void RebuildNavigationList()
    {
        if (_suspendSync)
        {
            return;
        }

        _suspendSync = true;
        try
        {
            _navigationList.BeginUpdate();
            _navigationList.Items.Clear();

            // Add all visible pages
            foreach (KryptonBackstagePage page in _pages)
            {
                if (page.VisibleInNavigation)
                {
                    _navigationList.Items.Add(page);
                }
            }

            // Add all visible commands
            foreach (KryptonBackstageCommand command in _commands)
            {
                if (command.VisibleInNavigation)
                {
                    _navigationList.Items.Add(command);
                }
            }

            // Always add the Close button as the last item
            _navigationList.Items.Add(_closeItem);

            if (_selectedPage != null && _selectedPage.VisibleInNavigation)
            {
                _navigationList.SelectedItem = _selectedPage;
            }
        }
        finally
        {
            _navigationList.EndUpdate();
            _suspendSync = false;
        }
    }

    private void OnCommandsInserted(object? sender, TypedCollectionEventArgs<KryptonBackstageCommand> e)
    {
        KryptonBackstageCommand? command = e.Item;
        if (command == null)
        {
            return;
        }

        command.NavigationPropertyChanged += OnCommandNavigationPropertyChanged;
        RebuildNavigationList();
    }

    private void OnCommandsRemoved(object? sender, TypedCollectionEventArgs<KryptonBackstageCommand> e)
    {
        KryptonBackstageCommand? command = e.Item;
        if (command == null)
        {
            return;
        }

        command.NavigationPropertyChanged -= OnCommandNavigationPropertyChanged;
        RebuildNavigationList();
    }

    private void OnCommandsCleared(object? sender, EventArgs e)
    {
        foreach (KryptonBackstageCommand command in _commands.ToArray())
        {
            command.NavigationPropertyChanged -= OnCommandNavigationPropertyChanged;
        }

        RebuildNavigationList();
    }

    private void OnCommandNavigationPropertyChanged(object? sender, EventArgs e) => RebuildNavigationList();

    private void OnPaletteChanged(object? sender, EventArgs e)
    {
        UpdateNavigationColors();
        UpdateContentColors();
        _navigationList.Invalidate();
    }
    #endregion
}
