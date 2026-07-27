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
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonNavigator))]
[DefaultProperty(nameof(Navigator))]
[Description(@"Registers KryptonNavigator pages as individual Windows taskbar thumbnails.")]
public class KryptonNavigatorTaskbarThumbnails : Component
{
    #region Instance Fields

    private KryptonNavigator? _navigator;
    private bool _enabled = true;
    private bool _includeHiddenPages;
    private bool _allowCloseFromThumbnail = true;
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
    /// Gets and sets the maximum number of page thumbnails to register. Zero means unlimited.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum number of page thumbnails to register. Zero means unlimited.")]
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

    #endregion
}
