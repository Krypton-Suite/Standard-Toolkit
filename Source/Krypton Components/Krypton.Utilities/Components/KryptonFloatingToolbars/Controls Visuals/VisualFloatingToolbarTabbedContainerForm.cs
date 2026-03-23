#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Container form for tabbed floating toolbars.
/// </summary>
public partial class VisualFloatingToolbarTabbedContainerForm : KryptonForm
{
    #region Instance Fields

    private FloatingToolbarGroup? _group;
    private KryptonNavigator? _navigator;
    private readonly Dictionary<KryptonFloatableToolStrip, KryptonPage> _toolbarPages = [];
    private readonly Dictionary<KryptonFloatableMenuStrip, KryptonPage> _menuStripPages = [];
    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the toolbar group associated with this container.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FloatingToolbarGroup? Group
    {
        get => _group;
        set
        {
            _group = value;
            UpdateTabs();
        }
    }

    /// <summary>
    /// Gets the navigator control hosting the tabs.
    /// </summary>
    public KryptonNavigator? Navigator => _navigator;

    #endregion

    #region Identity

    public VisualFloatingToolbarTabbedContainerForm()
    {
        InitializeComponent();
        InitializeNavigator();
    }

    private void InitializeNavigator()
    {
        _navigator = new KryptonNavigator
        {
            Dock = DockStyle.Fill,
            NavigatorMode = NavigatorMode.HeaderGroupTab,
            // TODO: Header.HeaderPositionPrimary = HeaderGroupPositionPrimary.Top,
            // TODO: Header.HeaderPositionSecondary = HeaderGroupPositionSecondary.Bottom,
            AllowDrop = true
        };
        
        // Enable drag-and-drop
        _navigator.DragEnter += Navigator_DragEnter;
        _navigator.DragOver += Navigator_DragOver;
        _navigator.DragLeave += Navigator_DragLeave;
        _navigator.DragDrop += Navigator_DragDrop;
        
        Controls.Add(_navigator);
    }

    private void Navigator_DragEnter(object? sender, DragEventArgs e)
    {
        if (IsValidDragData(e.Data))
        {
            e.Effect = DragDropEffects.Move;
            Invalidate();
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void Navigator_DragOver(object? sender, DragEventArgs e)
    {
        if (IsValidDragData(e.Data))
        {
            e.Effect = DragDropEffects.Move;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void Navigator_DragLeave(object? sender, EventArgs e)
    {
        Invalidate();
    }

    private void Navigator_DragDrop(object? sender, DragEventArgs e)
    {
        Invalidate();

        if (!IsValidDragData(e.Data))
        {
            return;
        }

        // Handle toolbar drop
        if (e.Data!.GetData(typeof(KryptonFloatableToolStrip)) is KryptonFloatableToolStrip toolbar)
        {
            AddToolbar(toolbar);
        }

        // Handle menu strip drop
        if (e.Data.GetData(typeof(KryptonFloatableMenuStrip)) is KryptonFloatableMenuStrip menuStrip)
        {
            AddMenuStrip(menuStrip);
        }
    }

    private bool IsValidDragData(IDataObject? data)
    {
        return data != null && (
            data.GetDataPresent(typeof(KryptonFloatableToolStrip)) ||
            data.GetDataPresent(typeof(KryptonFloatableMenuStrip))
        );
    }

    #endregion

    #region Implementation

    private void UpdateTabs()
    {
        if (_group == null || _navigator == null)
        {
            return;
        }

        _navigator.Pages.Clear();
        _toolbarPages.Clear();
        _menuStripPages.Clear();

        // Add toolbar tabs
        foreach (var toolbar in _group.Toolbars)
        {
            var page = new KryptonPage
            {
                Text = toolbar.FloatingToolBarWindowText,
                TextTitle = toolbar.FloatingToolBarWindowText,
                TextDescription = $@"Toolbar: {toolbar.Name}"
            };

            var panel = new KryptonPanel
            {
                Dock = DockStyle.Fill
            };

            // Remove toolbar from its current parent
            toolbar.Parent?.Controls.Remove(toolbar);
            
            // Set dock style
            ((Control)toolbar).Dock = DockStyle.Top;
            toolbar.LayoutStyle = ToolStripLayoutStyle.Flow;

            panel.Controls.Add(toolbar);
            // TODO: page.Panel.Controls.Add(panel);
            _navigator.Pages.Add(page);
            _toolbarPages[toolbar] = page;
        }

        // Add menu strip tabs
        foreach (var menuStrip in _group.MenuStrips)
        {
            var page = new KryptonPage
            {
                Text = menuStrip.FloatingWindowText,
                TextTitle = menuStrip.FloatingWindowText,
                TextDescription = $@"Menu Strip: {menuStrip.Name}"
            };

            var panel = new KryptonPanel
            {
                Dock = DockStyle.Fill
            };

            // Remove menu strip from its current parent
            menuStrip.Parent?.Controls.Remove(menuStrip);
            
            // Set dock style
            ((Control)menuStrip).Dock = DockStyle.Top;
            menuStrip.LayoutStyle = ToolStripLayoutStyle.Flow;

            panel.Controls.Add(menuStrip);
            // TODO: page.Panel.Controls.Add(panel);
            _navigator.Pages.Add(page);
            _menuStripPages[menuStrip] = page;
        }

        // Set active tab
        if (_navigator.Pages.Count > 0 && _group.ActiveTabIndex >= 0 && _group.ActiveTabIndex < _navigator.Pages.Count)
        {
            _navigator.SelectedIndex = _group.ActiveTabIndex;
        }

        // Handle tab selection change
        _navigator.SelectedPageChanged += Navigator_SelectedPageChanged;
    }

    private void Navigator_SelectedPageChanged(object? sender, EventArgs e)
    {
        if (_group != null && _navigator != null && _navigator.SelectedIndex >= 0)
        {
            _group.ActiveTabIndex = _navigator.SelectedIndex;
        }
    }

    /// <summary>
    /// Adds a toolbar to the tabbed container.
    /// </summary>
    public void AddToolbar(KryptonFloatableToolStrip toolbar)
    {
        if (_group == null || _navigator == null || _toolbarPages.ContainsKey(toolbar))
        {
            return;
        }

        var page = new KryptonPage
        {
            Text = toolbar.FloatingToolBarWindowText,
            TextTitle = toolbar.FloatingToolBarWindowText,
            TextDescription = $@"Toolbar: {toolbar.Name}"
        };

        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill
        };

        toolbar.Parent?.Controls.Remove(toolbar);
        ((Control)toolbar).Dock = DockStyle.Top;
        toolbar.LayoutStyle = ToolStripLayoutStyle.Flow;

        panel.Controls.Add(toolbar);
        // TODO: page.Panel.Controls.Add(panel);
        _navigator.Pages.Add(page);
        _toolbarPages[toolbar] = page;
        _group.AddToolbar(toolbar);
    }

    /// <summary>
    /// Adds a menu strip to the tabbed container.
    /// </summary>
    public void AddMenuStrip(KryptonFloatableMenuStrip menuStrip)
    {
        if (_group == null || _navigator == null || _menuStripPages.ContainsKey(menuStrip))
        {
            return;
        }

        var page = new KryptonPage
        {
            Text = menuStrip.FloatingWindowText,
            TextTitle = menuStrip.FloatingWindowText,
            TextDescription = $@"Menu Strip: {menuStrip.Name}"
        };

        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill
        };

        menuStrip.Parent?.Controls.Remove(menuStrip);
        ((Control)menuStrip).Dock = DockStyle.Top;
        menuStrip.LayoutStyle = ToolStripLayoutStyle.Flow;

        panel.Controls.Add(menuStrip);
        // TODO: page.Panel.Controls.Add(panel);
        _navigator.Pages.Add(page);
        _menuStripPages[menuStrip] = page;
        _group.AddMenuStrip(menuStrip);
    }

    /// <summary>
    /// Removes a toolbar from the tabbed container.
    /// </summary>
    public void RemoveToolbar(KryptonFloatableToolStrip toolbar)
    {
        if (_navigator == null || !_toolbarPages.TryGetValue(toolbar, out var page))
        {
            return;
        }

        _navigator.Pages.Remove(page);
        _toolbarPages.Remove(toolbar);
        _group?.RemoveToolbar(toolbar);
    }

    /// <summary>
    /// Removes a menu strip from the tabbed container.
    /// </summary>
    public void RemoveMenuStrip(KryptonFloatableMenuStrip menuStrip)
    {
        if (_navigator == null || !_menuStripPages.TryGetValue(menuStrip, out var page))
        {
            return;
        }

        _navigator.Pages.Remove(page);
        _menuStripPages.Remove(menuStrip);
        _group?.RemoveMenuStrip(menuStrip);
    }

    #endregion
}
