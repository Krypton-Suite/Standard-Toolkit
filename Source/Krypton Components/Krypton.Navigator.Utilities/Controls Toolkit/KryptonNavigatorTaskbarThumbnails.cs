#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Registers <see cref="KryptonNavigator"/> pages as individual Windows taskbar thumbnails (IE-style flyout).
/// Drop this component on a form, set <see cref="Navigator"/>, and leave <see cref="Enabled"/> true.
/// Clear <see cref="KryptonPageFlags.AllowTaskbarThumbnail"/> on wizard steps that should not appear.
/// Multiple components on the same taskbar-visible host merge into one thumbnail group.
/// Optionally set <see cref="FormIntegrator"/> and <see cref="ShowTabGroupThumbnails"/> to insert
/// Explorer-like composite <c>Group | …</c> previews ahead of each catalog tab group's members.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonNavigator))]
[DefaultProperty(nameof(Navigator))]
[Designer(typeof(KryptonNavigatorTaskbarThumbnailsDesigner))]
[Description(@"Registers KryptonNavigator pages as individual Windows taskbar thumbnails.")]
public class KryptonNavigatorTaskbarThumbnails : Component
{
    #region Instance Fields

    private KryptonNavigator? _navigator;
    private KryptonNavigatorFormIntegrator? _formIntegrator;
    private bool _enabled = true;
    private bool _includeHiddenPages;
    private bool _allowCloseFromThumbnail = true;
    private bool _activeTabUsesAppPreview = true;
    private bool _useSelectedPageOverlay;
    private bool _useSelectedPageProgress;
    private bool _useSelectedPageThumbnailButtons;
    private bool _showTabGroupThumbnails;
    private int _maxThumbnails;
    private NavigatorTaskbarThumbnailManager? _manager;
    private bool _disposed;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a custom taskbar thumbnail or live-preview bitmap is requested for a page.
    /// </summary>
    [Category(@"Navigator")]
    [Description(@"Occurs when a custom taskbar thumbnail or live-preview bitmap is requested for a page.")]
    public event EventHandler<QueryTaskbarThumbnailEventArgs>? QueryThumbnail;

    /// <summary>
    /// Occurs when a custom taskbar thumbnail or live-preview bitmap is requested for a tab group.
    /// </summary>
    [Category(@"Navigator")]
    [Description(@"Occurs when a custom taskbar thumbnail or live-preview bitmap is requested for a tab group.")]
    public event EventHandler<QueryTaskbarTabGroupThumbnailEventArgs>? QueryTabGroupThumbnail;

    /// <summary>
    /// Occurs when the selected page should supply a host taskbar overlay icon.
    /// </summary>
    [Category(@"Navigator")]
    [Description(@"Occurs when the selected page should supply a host taskbar overlay icon.")]
    public event EventHandler<QueryTaskbarOverlayEventArgs>? QueryOverlay;

    /// <summary>
    /// Occurs when the selected page should supply host taskbar progress.
    /// </summary>
    [Category(@"Navigator")]
    [Description(@"Occurs when the selected page should supply host taskbar progress.")]
    public event EventHandler<QueryTaskbarProgressEventArgs>? QueryProgress;

    /// <summary>
    /// Occurs when the selected page should supply host taskbar thumbnail toolbar buttons.
    /// </summary>
    [Category(@"Navigator")]
    [Description(@"Occurs when the selected page should supply host taskbar thumbnail toolbar buttons.")]
    public event EventHandler<QueryTaskbarThumbnailButtonsEventArgs>? QueryThumbnailButtons;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonNavigatorTaskbarThumbnails"/> class.
    /// </summary>
    public KryptonNavigatorTaskbarThumbnails()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonNavigatorTaskbarThumbnails"/> class with the specified container.
    /// </summary>
    /// <param name="container">The container for the component.</param>
    public KryptonNavigatorTaskbarThumbnails(IContainer container)
        : this()
    {
        container?.Add(this);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            DetachNavigator();
            DetachFormIntegrator();
            if (_manager != null)
            {
                _manager.Dispose();
                _manager = null;
            }
            _disposed = true;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the navigator whose pages are registered as taskbar thumbnails.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Navigator whose pages are registered as taskbar thumbnails.")]
    [DefaultValue(null)]
    public KryptonNavigator? Navigator
    {
        get => _navigator;
        set
        {
            if (ReferenceEquals(_navigator, value))
            {
                return;
            }

            DetachNavigator();
            _navigator = value;
            AttachNavigator();
            Sync();
        }
    }

    /// <summary>
    /// Gets and sets the form integrator that supplies the caption tab-group catalog for composite thumbnails.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Form integrator whose TabGroups catalog drives composite Group | … taskbar thumbnails.")]
    [DefaultValue(null)]
    public KryptonNavigatorFormIntegrator? FormIntegrator
    {
        get => _formIntegrator;
        set
        {
            if (ReferenceEquals(_formIntegrator, value))
            {
                return;
            }

            DetachFormIntegrator();
            _formIntegrator = value;
            AttachFormIntegrator();
            Sync();
        }
    }

    /// <summary>
    /// Gets and sets whether catalog tab groups appear as composite <c>Group | …</c> thumbnails ahead of their members.
    /// Requires <see cref="FormIntegrator"/> with <see cref="KryptonNavigatorFormIntegrator.AllowTabGroups"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Insert composite Group | … taskbar thumbnails for FormIntegrator tab groups.")]
    [DefaultValue(false)]
    public bool ShowTabGroupThumbnails
    {
        get => _showTabGroupThumbnails;
        set
        {
            if (_showTabGroupThumbnails != value)
            {
                _showTabGroupThumbnails = value;
                Sync();
            }
        }
    }

    /// <summary>
    /// Gets and sets whether pages are registered as individual Windows taskbar thumbnails.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Register each eligible page as an individual Windows taskbar thumbnail.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                Sync();
            }
        }
    }

    /// <summary>
    /// Gets and sets whether hidden pages are included in the taskbar thumbnail flyout.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Include hidden pages in the taskbar thumbnail flyout.")]
    [DefaultValue(false)]
    public bool IncludeHiddenPages
    {
        get => _includeHiddenPages;
        set
        {
            if (_includeHiddenPages != value)
            {
                _includeHiddenPages = value;
                Sync();
            }
        }
    }

    /// <summary>
    /// Gets and sets whether closing a thumbnail (X on the flyout) closes the related page via the navigator close action.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Closing a taskbar thumbnail closes the related navigator page.")]
    [DefaultValue(true)]
    public bool AllowCloseFromThumbnail
    {
        get => _allowCloseFromThumbnail;
        set => _allowCloseFromThumbnail = value;
    }

    /// <summary>
    /// Gets and sets whether the active tab uses the host window for thumbnail and Peek when active.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, the active tab uses the application window for thumbnail and Peek previews.")]
    [DefaultValue(true)]
    public bool ActiveTabUsesAppPreview
    {
        get => _activeTabUsesAppPreview;
        set
        {
            if (_activeTabUsesAppPreview != value)
            {
                _activeTabUsesAppPreview = value;
                Sync();
            }
        }
    }

    /// <summary>
    /// Gets and sets whether the host taskbar overlay icon is driven by the selected page via <see cref="QueryOverlay"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Apply a host taskbar overlay icon from the selected page.")]
    [DefaultValue(false)]
    public bool UseSelectedPageOverlay
    {
        get => _useSelectedPageOverlay;
        set
        {
            if (_useSelectedPageOverlay != value)
            {
                _useSelectedPageOverlay = value;
                Sync();
            }
        }
    }

    /// <summary>
    /// Gets and sets whether host taskbar progress is driven by the selected page via <see cref="QueryProgress"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Apply host taskbar progress from the selected page.")]
    [DefaultValue(false)]
    public bool UseSelectedPageProgress
    {
        get => _useSelectedPageProgress;
        set
        {
            if (_useSelectedPageProgress != value)
            {
                _useSelectedPageProgress = value;
                Sync();
            }
        }
    }

    /// <summary>
    /// Gets and sets whether host thumbnail toolbar buttons are driven by the selected page via <see cref="QueryThumbnailButtons"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Apply host taskbar thumbnail toolbar buttons from the selected page.")]
    [DefaultValue(false)]
    public bool UseSelectedPageThumbnailButtons
    {
        get => _useSelectedPageThumbnailButtons;
        set
        {
            if (_useSelectedPageThumbnailButtons != value)
            {
                _useSelectedPageThumbnailButtons = value;
                Sync();
            }
        }
    }

    /// <summary>
    /// Gets and sets the maximum number of registered taskbar tabs from this component (group slots plus page slots).
    /// Zero means unlimited.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum registered taskbar tabs from this component (groups + pages). Zero means unlimited.")]
    [DefaultValue(0)]
    public int MaxThumbnails
    {
        get => _maxThumbnails;
        set
        {
            value = Math.Max(0, value);
            if (_maxThumbnails != value)
            {
                _maxThumbnails = value;
                Sync();
            }
        }
    }

    /// <summary>
    /// Refresh taskbar tab registration to match the current navigator pages and settings.
    /// </summary>
    public void RefreshThumbnails() => Sync();

    #endregion

    #region Internal

    internal void RaiseQueryThumbnail(QueryTaskbarThumbnailEventArgs e) => QueryThumbnail?.Invoke(this, e);

    internal void RaiseQueryTabGroupThumbnail(QueryTaskbarTabGroupThumbnailEventArgs e) =>
        QueryTabGroupThumbnail?.Invoke(this, e);

    internal void RaiseQueryOverlay(QueryTaskbarOverlayEventArgs e) => QueryOverlay?.Invoke(this, e);

    internal void RaiseQueryProgress(QueryTaskbarProgressEventArgs e) => QueryProgress?.Invoke(this, e);

    internal void RaiseQueryThumbnailButtons(QueryTaskbarThumbnailButtonsEventArgs e) =>
        QueryThumbnailButtons?.Invoke(this, e);

    /// <summary>
    /// Resolves whether composite tab-group thumbnails are active for the current wiring.
    /// </summary>
    internal bool AreTabGroupThumbnailsActive()
    {
        if (!_showTabGroupThumbnails || _formIntegrator == null || !_formIntegrator.AllowTabGroups)
        {
            return false;
        }

        return _navigator != null &&
               !_navigator.IsDisposed &&
               ReferenceEquals(_formIntegrator.Navigator, _navigator);
    }

    internal void Sync()
    {
        if (_disposed || CommonHelper.DesignMode())
        {
            return;
        }

        if (_enabled && _navigator != null && !_navigator.IsDisposed)
        {
            _manager ??= new NavigatorTaskbarThumbnailManager(this);
            _manager.Sync();
        }
        else if (_manager != null)
        {
            _manager.Sync();
        }
    }

    #endregion

    #region Implementation

    private void AttachNavigator()
    {
        if (_navigator == null)
        {
            return;
        }

        _navigator.SelectedPageChanged += OnNavigatorSelectedPageChanged;
        _navigator.Pages.Inserted += OnNavigatorPagesChanged;
        _navigator.Pages.Removed += OnNavigatorPagesChanged;
        _navigator.Pages.Cleared += OnNavigatorPagesCleared;
        _navigator.ParentChanged += OnNavigatorParentChanged;
        _navigator.HandleCreated += OnNavigatorHandleCreated;
        _navigator.VisibleChanged += OnNavigatorVisibleChanged;
    }

    private void DetachNavigator()
    {
        if (_navigator == null)
        {
            return;
        }

        _navigator.SelectedPageChanged -= OnNavigatorSelectedPageChanged;
        _navigator.Pages.Inserted -= OnNavigatorPagesChanged;
        _navigator.Pages.Removed -= OnNavigatorPagesChanged;
        _navigator.Pages.Cleared -= OnNavigatorPagesCleared;
        _navigator.ParentChanged -= OnNavigatorParentChanged;
        _navigator.HandleCreated -= OnNavigatorHandleCreated;
        _navigator.VisibleChanged -= OnNavigatorVisibleChanged;
    }

    private void AttachFormIntegrator()
    {
        if (_formIntegrator == null)
        {
            return;
        }

        _formIntegrator.TabGroupChanged += OnFormIntegratorTabGroupChanged;
    }

    private void DetachFormIntegrator()
    {
        if (_formIntegrator == null)
        {
            return;
        }

        _formIntegrator.TabGroupChanged -= OnFormIntegratorTabGroupChanged;
    }

    private void OnNavigatorSelectedPageChanged(object? sender, EventArgs e) =>
        _manager?.UpdateActiveTab();

    private void OnNavigatorPagesChanged(object? sender, TypedCollectionEventArgs<KryptonPage> e) =>
        Sync();

    private void OnNavigatorPagesCleared(object? sender, EventArgs e) => Sync();

    private void OnNavigatorParentChanged(object? sender, EventArgs e) => Sync();

    private void OnNavigatorHandleCreated(object? sender, EventArgs e) => Sync();

    private void OnNavigatorVisibleChanged(object? sender, EventArgs e)
    {
        if (_navigator is { Visible: true })
        {
            Sync();
        }
    }

    private void OnFormIntegratorTabGroupChanged(object? sender, EventArgs e) => Sync();

    #endregion
}
