#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Integrates a <see cref="KryptonNavigator"/> with a <see cref="KryptonForm"/> for
/// browser / Windows Explorer-style tabbed chrome (issue #925).
/// </summary>
/// <remarks>
/// <para>
/// Drop this component on a <see cref="KryptonForm"/> that hosts a
/// <see cref="KryptonNavigator"/>. Assign <see cref="Form"/> and <see cref="Navigator"/>,
/// then set <see cref="Enabled"/> to activate.
/// </para>
/// <para>
/// <see cref="NavigatorFormIntegrationMode.CaptionIntegrated"/> injects a tab strip into the
/// form caption (Ribbon-style <see cref="KryptonForm.InjectViewElement"/>), switches the
/// navigator to <see cref="NavigatorMode.Panel"/> for content only, and keeps the form control box.
/// </para>
/// <para>
/// <see cref="NavigatorFormIntegrationMode.ClientChrome"/> hides the form control box and
/// shows form min/max/close button specs on the navigator.
/// <see cref="NavigatorFormIntegrationMode.CaptionAdjacent"/> keeps the form caption buttons
/// and optionally syncs the selected page text into the form title.
/// </para>
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonNavigator))]
[Designer(typeof(KryptonNavigatorFormIntegratorDesigner))]
[DefaultProperty(nameof(Form))]
[DefaultEvent(nameof(IntegrationChanged))]
[Description(@"Integrates KryptonNavigator with KryptonForm for browser-style tabbed chrome.")]
public class KryptonNavigatorFormIntegrator : Component, IDragTargetProvider
{
    #region Instance Fields

    private static readonly HashSet<KryptonNavigatorFormIntegrator> _registeredIntegrators = [];

    private KryptonForm? _form;
    private KryptonNavigator? _navigator;
    private bool _enabled = true;
    private bool _syncFormTitle;
    private bool _suppressFormTitleWhenClientChrome = true;
    private NavigatorFormIntegrationMode _mode = NavigatorFormIntegrationMode.CaptionIntegrated;
    private bool _applied;
    private bool _disposed;
    private bool _captionInjected;

    private ViewLayoutNavigatorCaptionTabs? _captionTabs;
    private NavigatorCaptionDragPageNotify? _captionDragNotify;

    private bool _allowTearOut = true;
    private bool _closeEmptySourceWindowAfterLastTabMoved = true;
    private bool _useBuiltInTabContextMenu = true;
    private bool _includeFormSystemMenuInTabContextMenu = true;
    private bool _showNewTabButton;
    private bool _allowTabGroups = true;
    private readonly NavigatorTabGroupCollection _tabGroups = new();
    private ContextMenuStrip? _builtInTabContextMenu;
    private KryptonPage? _contextMenuPage;
    private Krypton.Workspace.KryptonWorkspace? _workspace;
    private ViewLayoutCaptionDocumentGroups? _captionDocumentGroups;
    private bool _workspacePersistenceHooked;

    // Restored when integration is detached
    private bool _savedControlBox = true;
    private bool _savedControlKryptonFormFeatures;
    private bool _savedAllowIconDisplay = true;
    private string? _savedFormText;
    private KryptonForm? _savedOwner;
    private NavigatorMode _savedNavigatorMode = NavigatorMode.BarTabOnly;
    private IDragPageNotify? _savedDragPageNotify;
    private bool _savedAllowPageDrag;
    private bool _haveSavedState;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when integration is applied or revoked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when navigator/form integration is applied or revoked.")]
    public event EventHandler? IntegrationChanged;

    /// <summary>
    /// Occurs before the built-in caption-tab context menu is shown.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Allows customization of the built-in caption-tab context menu before it is shown.")]
    public event EventHandler<NavigatorTabContextMenuEventArgs>? TabContextMenuOpening;

    /// <summary>
    /// Occurs when the optional caption new-tab ('+') button is clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the caption new-tab button is clicked. Host apps should create and add a page.")]
    public event EventHandler? NewTabButtonClick;

    /// <summary>
    /// Occurs when browser-style tab group membership or catalog metadata changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when tab groups are created, assigned, collapsed, or otherwise changed.")]
    public event EventHandler? TabGroupChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonNavigatorFormIntegrator"/> class.
    /// </summary>
    public KryptonNavigatorFormIntegrator()
    {
        _tabGroups.Inserted += OnTabGroupsCollectionChanged;
        _tabGroups.Removed += OnTabGroupsCollectionChanged;
        _tabGroups.Cleared += OnTabGroupsCollectionCleared;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonNavigatorFormIntegrator"/> class.
    /// </summary>
    /// <param name="container">Container that owns the component.</param>
    public KryptonNavigatorFormIntegrator(IContainer container)
        : this()
    {
        container?.Add(this);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the form to integrate with.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"KryptonForm that hosts the integrated navigator.")]
    public KryptonForm? Form
    {
        get => _form;
        set
        {
            if (ReferenceEquals(_form, value))
            {
                return;
            }

            UnhookFormChromeEvents(_form);
            Detach();
            _form = value;
            HookFormChromeEvents(_form);
            TryApply();
        }
    }

    /// <summary>
    /// Gets or sets the navigator that provides the tab strip / page content.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"KryptonNavigator that provides pages and (except CaptionIntegrated) the client tab strip.")]
    public KryptonNavigator? Navigator
    {
        get => _navigator;
        set
        {
            if (ReferenceEquals(_navigator, value))
            {
                return;
            }

            Detach();
            _navigator = value;
            TryApply();
        }
    }

    /// <summary>
    /// Gets or sets whether integration is active.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"When true, applies the selected integration mode to Form and Navigator.")]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled == value)
            {
                return;
            }

            _enabled = value;
            if (_enabled)
            {
                TryApply();
            }
            else
            {
                Detach();
            }
        }
    }

    /// <summary>
    /// Gets or sets how the navigator and form share caption / control-box duties.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(NavigatorFormIntegrationMode.CaptionIntegrated)]
    [Description(@"CaptionIntegrated: tabs in form caption. ClientChrome: navigator hosts form buttons. CaptionAdjacent: form keeps caption buttons.")]
    public NavigatorFormIntegrationMode Mode
    {
        get => _mode;
        set
        {
            if (_mode == value)
            {
                return;
            }

            Detach();
            _mode = value;
            TryApply();
        }
    }

    /// <summary>
    /// Gets or sets whether the selected page text is copied to <see cref="System.Windows.Forms.Form.Text"/>.
    /// </summary>
    /// <remarks>
    /// Ignored in <see cref="NavigatorFormIntegrationMode.CaptionIntegrated"/> — that mode always clears
    /// <c>Form.Text</c> so the caption is not crowded by both tabs and title text.
    /// </remarks>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"When true, Form.Text tracks the selected KryptonPage.Text (ignored in CaptionIntegrated).")]
    public bool SyncFormTitle
    {
        get => _syncFormTitle;
        set
        {
            if (_syncFormTitle == value)
            {
                return;
            }

            _syncFormTitle = value;
            if (_applied)
            {
                UpdateFormTitle();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether <see cref="System.Windows.Forms.Form.Text"/> is cleared in
    /// ClientChrome / CaptionIntegrated when not syncing, so the caption does not compete with tabs.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Clear the form title when SyncFormTitle is false in ClientChrome or CaptionIntegrated.")]
    public bool SuppressFormTitleWhenClientChrome
    {
        get => _suppressFormTitleWhenClientChrome;
        set
        {
            if (_suppressFormTitleWhenClientChrome == value)
            {
                return;
            }

            _suppressFormTitleWhenClientChrome = value;
            if (_applied)
            {
                UpdateFormTitle();
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether integration is currently applied.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsIntegrated => _applied;

    /// <summary>
    /// Applies integration immediately when Form and Navigator are assigned.
    /// </summary>
    public void Apply() => TryApply(force: true);

    /// <summary>
    /// Removes integration and restores the previous form/navigator chrome settings.
    /// </summary>
    public void Revoke() => Detach();

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            UnhookFormChromeEvents(_form);
            UnhookWorkspacePersistence();
            Detach();
            _captionDragNotify?.Dispose();
            _captionDragNotify = null;
            _builtInTabContextMenu?.Dispose();
            _builtInTabContextMenu = null;
            _disposed = true;
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Raises the <see cref="IntegrationChanged"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnIntegrationChanged(EventArgs e) => IntegrationChanged?.Invoke(this, e);

    #endregion

    #region Implementation

    /// <summary>
    /// Gets or sets if dragging a caption tab outside any registered target should create
    /// a new window and move the pages into it.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"When false, dragging outside other navigators will not tear out tabs into a new window.")]
    public bool AllowTearOut
    {
        get => _allowTearOut;
        set
        {
            if (_allowTearOut == value)
            {
                return;
            }

            _allowTearOut = value;
            // No immediate state change needed for the drag pipeline.
        }
    }

    /// <summary>
    /// Gets or sets if an empty source window should be closed after the last tab is moved
    /// (browser-like behavior).
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"When true, closes the source KryptonForm after its last tab is moved out.")]
    public bool CloseEmptySourceWindowAfterLastTabMoved
    {
        get => _closeEmptySourceWindowAfterLastTabMoved;
        set
        {
            if (_closeEmptySourceWindowAfterLastTabMoved == value)
            {
                return;
            }

            _closeEmptySourceWindowAfterLastTabMoved = value;
        }
    }

    /// <summary>
    /// Gets or sets whether caption tabs use the built-in default context menu when the page
    /// itself does not provide a menu.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"When true, caption tabs use the built-in default context menu if the page does not already provide one.")]
    public bool UseBuiltInTabContextMenu
    {
        get => _useBuiltInTabContextMenu;
        set
        {
            if (_useBuiltInTabContextMenu == value)
            {
                return;
            }

            _useBuiltInTabContextMenu = value;
        }
    }

    /// <summary>
    /// Gets or sets whether the built-in caption-tab context menu also includes the form
    /// system-menu commands (Restore, Move, Size, Minimize, Maximize, Close).
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"When true, the built-in caption-tab menu includes form system-menu commands alongside tab commands.")]
    public bool IncludeFormSystemMenuInTabContextMenu
    {
        get => _includeFormSystemMenuInTabContextMenu;
        set
        {
            if (_includeFormSystemMenuInTabContextMenu == value)
            {
                return;
            }

            _includeFormSystemMenuInTabContextMenu = value;
        }
    }

    /// <summary>
    /// Gets or sets whether a '+' button is shown to the right of the last caption tab
    /// (CaptionIntegrated mode only). Handle <see cref="NewTabButtonClick"/> to create pages.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description(@"When true, CaptionIntegrated mode shows a '+' button after the last caption tab.")]
    public bool ShowNewTabButton
    {
        get => _showNewTabButton;
        set
        {
            if (_showNewTabButton == value)
            {
                return;
            }

            _showNewTabButton = value;
            if (_captionTabs != null)
            {
                _captionTabs.ShowNewTabButton = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets whether browser-style colored tab groups are enabled for CaptionIntegrated.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"When true, CaptionIntegrated renders group headers, accents, and collapse for pages with TabGroupId.")]
    public bool AllowTabGroups
    {
        get => _allowTabGroups;
        set
        {
            if (_allowTabGroups == value)
            {
                return;
            }

            _allowTabGroups = value;
            if (_captionTabs != null)
            {
                _captionTabs.AllowTabGroups = value;
            }
        }
    }

    /// <summary>
    /// Gets the catalog of browser-style tab groups (title, color, collapsed).
    /// </summary>
    [Category(@"Data")]
    [Description(@"Catalog of browser-style tab groups referenced by KryptonPage.TabGroupId.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public NavigatorTabGroupCollection TabGroups => _tabGroups;

    /// <summary>
    /// Gets or sets an optional workspace used for multi-strip caption document groups (Phase 3).
    /// </summary>
    /// <remarks>
    /// When set in CaptionIntegrated mode, the caption hosts one tab strip per workspace cell
    /// instead of a single navigator strip. <see cref="Navigator"/> should typically be null
    /// or unused in that configuration.
    /// </remarks>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"Optional KryptonWorkspace for multi-strip caption document groups.")]
    public Krypton.Workspace.KryptonWorkspace? Workspace
    {
        get => _workspace;
        set
        {
            if (ReferenceEquals(_workspace, value))
            {
                return;
            }

            Detach();
            UnhookWorkspacePersistence();
            _workspace = value;
            HookWorkspacePersistence();
            TryApply();
        }
    }

    /// <summary>
    /// Saves browser-style tab groups and (when a navigator is bound) page order/membership.
    /// </summary>
    /// <remarks>
    /// When <see cref="Workspace"/> is set, prefer workspace layout save/load APIs —
    /// the integrator embeds the group catalog in workspace <c>GlobalSaving</c>, and page
    /// <c>TabGroupId</c> is persisted as the <c>TG</c> attribute.
    /// </remarks>
    public void SaveLayoutToFile(string filename) => SaveLayoutToFile(filename, Encoding.Unicode);

    /// <summary>
    /// Saves layout information to a file with the specified encoding.
    /// </summary>
    public void SaveLayoutToFile(string filename, Encoding encoding)
    {
        using var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
        SaveLayoutToStream(stream, encoding);
    }

    /// <summary>
    /// Saves layout information to a stream.
    /// </summary>
    public void SaveLayoutToStream(Stream stream, Encoding encoding)
    {
        var settings = new XmlWriterSettings
        {
            Encoding = encoding,
            Indent = true,
            CloseOutput = false
        };
        using XmlWriter xmlWriter = XmlWriter.Create(stream, settings);
        xmlWriter.WriteStartDocument();
        SaveLayoutToXml(xmlWriter);
        xmlWriter.WriteEndDocument();
        xmlWriter.Flush();
    }

    /// <summary>
    /// Saves layout information using the provided XML writer.
    /// </summary>
    public void SaveLayoutToXml(XmlWriter xmlWriter)
    {
        if (_workspace != null)
        {
            _workspace.SaveLayoutToXml(xmlWriter);
            return;
        }

        if (_navigator == null)
        {
            throw new InvalidOperationException(@"SaveLayout requires Navigator or Workspace to be assigned.");
        }

        NavigatorTabGroupLayoutSerializer.SaveNavigatorLayout(xmlWriter, _navigator, _tabGroups);
    }

    /// <summary>
    /// Saves layout information to a byte array.
    /// </summary>
    public byte[] SaveLayoutToArray() => SaveLayoutToArray(Encoding.Unicode);

    /// <summary>
    /// Saves layout information to a byte array with the specified encoding.
    /// </summary>
    public byte[] SaveLayoutToArray(Encoding encoding)
    {
        using var ms = new MemoryStream();
        SaveLayoutToStream(ms, encoding);
        return ms.ToArray();
    }

    /// <summary>
    /// Loads browser-style tab groups and (when a navigator is bound) page order/membership.
    /// </summary>
    public void LoadLayoutFromFile(string filename)
    {
        using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        LoadLayoutFromStream(stream);
    }

    /// <summary>
    /// Loads layout information from a stream.
    /// </summary>
    public void LoadLayoutFromStream(Stream stream)
    {
        using var xmlReader = new XmlTextReader(stream)
        {
            WhitespaceHandling = WhitespaceHandling.None
        };
        xmlReader.MoveToContent();
        LoadLayoutFromXml(xmlReader);
    }

    /// <summary>
    /// Loads layout information from a byte array.
    /// </summary>
    public void LoadLayoutFromArray(byte[] buffer)
    {
        using var ms = new MemoryStream(buffer);
        LoadLayoutFromStream(ms);
    }

    /// <summary>
    /// Loads layout information using the provided XML reader.
    /// </summary>
    public void LoadLayoutFromXml(XmlReader xmlReader)
    {
        if (_workspace != null)
        {
            _workspace.LoadLayoutFromXml(xmlReader);
            _captionDocumentGroups?.RebuildStrips();
            OnTabGroupChanged(EventArgs.Empty);
            return;
        }

        if (_navigator == null)
        {
            throw new InvalidOperationException(@"LoadLayout requires Navigator or Workspace to be assigned.");
        }

        NavigatorTabGroupLayoutSerializer.LoadNavigatorLayout(xmlReader, _navigator, _tabGroups);
        _captionTabs?.RebuildTabs();
        OnTabGroupChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Creates a new tab group and optionally assigns a page to it.
    /// </summary>
    public NavigatorTabGroup CreateGroup(string? title = null, Color? color = null, KryptonPage? assignPage = null)
    {
        var group = new NavigatorTabGroup(
            Guid.NewGuid().ToString("N"),
            string.IsNullOrEmpty(title)
                ? $"{KryptonManager.Strings.NavigatorIntegrationStrings.DefaultGroupTitle} {_tabGroups.Count + 1}"
                : title!,
            color ?? PickNextGroupColor());
        _tabGroups.Add(group);

        if (assignPage != null)
        {
            AssignPageToGroup(assignPage, group.Id);
        }

        OnTabGroupChanged(EventArgs.Empty);
        return group;
    }

    /// <summary>
    /// Assigns a page to a group id and clusters it with sibling members.
    /// </summary>
    public void AssignPageToGroup(KryptonPage page, string groupId)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        if (string.IsNullOrEmpty(groupId))
        {
            UngroupPage(page);
            return;
        }

        if (_tabGroups[groupId] == null)
        {
            throw new ArgumentException(@"Group id was not found in TabGroups.", nameof(groupId));
        }

        page.TabGroupId = groupId;
        ClusterPageWithGroup(page, groupId);
        NavigatorTabGroup? group = _tabGroups[groupId];
        NavigatorTabGroupBarAccent.Apply(page, group);
        if (_navigator != null)
        {
            NavigatorTabGroupBarAccent.SyncNavigator(_navigator, _tabGroups);
        }

        OnTabGroupChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Removes a page from any tab group.
    /// </summary>
    public void UngroupPage(KryptonPage page)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        if (string.IsNullOrEmpty(page.TabGroupId))
        {
            return;
        }

        page.TabGroupId = string.Empty;
        NavigatorTabGroupBarAccent.Clear(page);
        OnTabGroupChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Merges group catalog entries from another integrator (cross-window catalog sync).
    /// </summary>
    public void MergeTabGroupsFrom(NavigatorTabGroupCollection source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        _tabGroups.CopyFrom(source);
        if (_navigator != null)
        {
            NavigatorTabGroupBarAccent.SyncNavigator(_navigator, _tabGroups);
        }

        OnTabGroupChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Toggles the collapsed state of a tab group.
    /// </summary>
    public void ToggleGroupCollapsed(string groupId)
    {
        NavigatorTabGroup? group = _tabGroups[groupId];
        if (group == null)
        {
            return;
        }

        group.Collapsed = !group.Collapsed;
        OnTabGroupChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Raises <see cref="TabGroupChanged"/>.
    /// </summary>
    protected virtual void OnTabGroupChanged(EventArgs e)
    {
        if (_navigator != null)
        {
            NavigatorTabGroupBarAccent.SyncNavigator(_navigator, _tabGroups);
        }

        if (_workspace != null)
        {
            NavigatorTabGroupBarAccent.SyncWorkspace(_workspace, _tabGroups);
        }

        // Single-strip caption rebuild; multi-strip strips already listen to TabGroups PropertyChanged.
        _captionTabs?.RebuildTabs();
        TabGroupChanged?.Invoke(this, e);
    }

    private void TryApply(bool force = false)
    {
        if (_disposed || (!_enabled && !force))
        {
            return;
        }

        // Multi-strip caption uses Workspace; classic modes require Navigator.
        if (_form == null || (_workspace == null && _navigator == null))
        {
            return;
        }

        if (_applied)
        {
            DetachCore(raiseEvent: false);
        }

        SaveState();
        ApplyCore();
        _applied = true;
        OnIntegrationChanged(EventArgs.Empty);
    }

    private void Detach()
    {
        if (!_applied)
        {
            return;
        }

        DetachCore(raiseEvent: true);
    }

    private void SaveState()
    {
        Debug.Assert(_form != null);

        _savedControlBox = _form!.ControlBox;
        _savedFormText = _form.Text;
        _savedAllowIconDisplay = _form.AllowIconDisplay;
        if (_navigator != null)
        {
            _savedOwner = _navigator.Owner;
            _savedControlKryptonFormFeatures = _navigator.ControlKryptonFormFeatures;
            _savedNavigatorMode = _navigator.NavigatorMode;
            _savedDragPageNotify = _navigator.DragPageNotify;
            _savedAllowPageDrag = _navigator.AllowPageDrag;
        }

        _haveSavedState = true;
    }

    private void ApplyCore()
    {
        Debug.Assert(_form != null);

        if (_mode == NavigatorFormIntegrationMode.CaptionIntegrated && _workspace != null)
        {
            ApplyCaptionDocumentGroups();
            return;
        }

        if (_navigator == null)
        {
            return;
        }

        _navigator.Owner = _form;
        _navigator.SelectedPageChanged += OnNavigatorSelectedPageChanged;

        switch (_mode)
        {
            case NavigatorFormIntegrationMode.CaptionIntegrated:
                ApplyCaptionIntegrated();
                break;

            case NavigatorFormIntegrationMode.ClientChrome:
                _navigator.ControlKryptonFormFeatures = false;
                _form!.ControlBox = false;
                break;

            case NavigatorFormIntegrationMode.CaptionAdjacent:
            default:
                _navigator.ControlKryptonFormFeatures = true;
                break;
        }

        UpdateFormTitle();
    }

    private void ApplyCaptionIntegrated()
    {
        Debug.Assert(_form != null && _navigator != null);

        // Form keeps its control box; navigator does not show form button specs.
        _navigator!.ControlKryptonFormFeatures = true;
        _form!.ControlBox = _savedControlBox;

        // Content only in the client area — tabs live in the caption.
        _navigator.NavigatorMode = NavigatorMode.Panel;

        // Caption tabs replace the title strip; keep Form.Text empty so SyncFormTitle
        // cannot reprint the selected page next to the tabs.
        _form.Text = string.Empty;
        _form.AllowIconDisplay = false;
        _navigator.AllowPageDrag = true;
        _captionDragNotify ??= new NavigatorCaptionDragPageNotify(this);
        _navigator.DragPageNotify = _captionDragNotify;
        _registeredIntegrators.Add(this);

        InjectCaptionTabs();
    }

    private void ApplyCaptionDocumentGroups()
    {
        Debug.Assert(_form != null && _workspace != null);

        _form!.ControlBox = _savedControlBox;
        _form.Text = string.Empty;
        _form.AllowIconDisplay = false;
        _registeredIntegrators.Add(this);
        InjectCaptionDocumentGroups();
    }

    private void InjectCaptionTabs()
    {
        if (_form == null || _navigator == null || _captionInjected)
        {
            return;
        }

        _captionTabs = new ViewLayoutNavigatorCaptionTabs(_navigator, OnCaptionNeedPaint, ShowTabContextMenu, OnNewTabButtonClick)
        {
            ShowNewTabButton = _showNewTabButton,
            AllowTabGroups = _allowTabGroups,
            TabGroups = _tabGroups
        };
        _form.InjectViewElement(_captionTabs, ViewDockStyle.Left);
        _captionInjected = true;
        _form.PerformNeedPaint(true);
        _form.InvalidateNonClient();
    }

    private void InjectCaptionDocumentGroups()
    {
        if (_form == null || _workspace == null || _captionInjected)
        {
            return;
        }

        _captionDocumentGroups = new ViewLayoutCaptionDocumentGroups(
            _workspace,
            OnCaptionNeedPaint,
            ShowTabContextMenu,
            _tabGroups,
            _allowTabGroups,
            _showNewTabButton,
            OnNewTabButtonClick);
        _form.InjectViewElement(_captionDocumentGroups, ViewDockStyle.Left);
        _captionInjected = true;
        _form.PerformNeedPaint(true);
        _form.InvalidateNonClient();
    }

    private void OnNewTabButtonClick() => NewTabButtonClick?.Invoke(this, EventArgs.Empty);

    private void RevokeCaptionTabs()
    {
        if (_form == null || !_captionInjected)
        {
            return;
        }

        if (_captionDocumentGroups != null)
        {
            _form.RevokeViewElement(_captionDocumentGroups, ViewDockStyle.Left);
            _captionDocumentGroups.Dispose();
            _captionDocumentGroups = null;
        }

        if (_captionTabs != null)
        {
            _form.RevokeViewElement(_captionTabs, ViewDockStyle.Left);
            _captionTabs.Dispose();
            _captionTabs = null;
        }

        _captionInjected = false;
        _form.CustomCaptionArea = Rectangle.Empty;
        _form.CustomCaptionAreas = Array.Empty<Rectangle>();
        _form.PerformNeedPaint(true);
        _form.InvalidateNonClient();
    }

    private void OnCaptionNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (_form is { IsDisposed: false })
        {
            _form.PerformNeedPaint(e.NeedLayout);
            _form.InvalidateNonClient();
        }
    }

    private void DetachCore(bool raiseEvent)
    {
        if (_navigator != null)
        {
            _navigator.SelectedPageChanged -= OnNavigatorSelectedPageChanged;
        }

        RevokeCaptionTabs();
        _registeredIntegrators.Remove(this);

        if (_haveSavedState && _form != null)
        {
            _form.ControlBox = _savedControlBox;
            _form.AllowIconDisplay = _savedAllowIconDisplay;
            if (_savedFormText != null)
            {
                _form.Text = _savedFormText;
            }

            if (_navigator != null)
            {
                _navigator.Owner = _savedOwner;
                _navigator.ControlKryptonFormFeatures = _savedControlKryptonFormFeatures;
                _navigator.NavigatorMode = _savedNavigatorMode;
                _navigator.DragPageNotify = _savedDragPageNotify;
                _navigator.AllowPageDrag = _savedAllowPageDrag;
            }
        }

        _applied = false;
        _haveSavedState = false;

        if (raiseEvent)
        {
            OnIntegrationChanged(EventArgs.Empty);
        }
    }

    private void OnNavigatorSelectedPageChanged(object? sender, EventArgs e) => UpdateFormTitle();

    private void UpdateFormTitle()
    {
        if (_form == null || _navigator == null || !_applied)
        {
            return;
        }

        // CaptionIntegrated chrome is the tab strip — never paint Form.Text beside it.
        if (_mode == NavigatorFormIntegrationMode.CaptionIntegrated)
        {
            _form.Text = string.Empty;
            return;
        }

        if (_syncFormTitle)
        {
            var page = _navigator.SelectedPage;
            if (page != null && !string.IsNullOrEmpty(page.Text))
            {
                _form.Text = page.Text;
                return;
            }
        }

        if (_suppressFormTitleWhenClientChrome
            && !_syncFormTitle
            && _mode == NavigatorFormIntegrationMode.ClientChrome)
        {
            _form.Text = string.Empty;
        }
        else if (_haveSavedState && _savedFormText != null && !_syncFormTitle)
        {
            _form.Text = _savedFormText;
        }
    }

    private void HookFormChromeEvents(KryptonForm? form)
    {
        if (form == null)
        {
            return;
        }

        form.HandleCreated += OnFormChromeReady;
        form.ApplyUseThemeFormChromeBorderWidthChanged += OnFormChromeReady;
        form.DpiChanged += OnFormDpiChanged;
    }

    private void UnhookFormChromeEvents(KryptonForm? form)
    {
        if (form == null)
        {
            return;
        }

        form.HandleCreated -= OnFormChromeReady;
        form.ApplyUseThemeFormChromeBorderWidthChanged -= OnFormChromeReady;
        form.DpiChanged -= OnFormDpiChanged;
    }

    private void OnFormDpiChanged(object? sender, DpiChangedEventArgs e)
    {
        if (!_applied || _mode != NavigatorFormIntegrationMode.CaptionIntegrated)
        {
            return;
        }

        _captionTabs?.RebuildTabs();
        _captionDocumentGroups?.RebuildStrips();
        if (_form is { IsDisposed: false })
        {
            _form.PerformNeedPaint(true);
            _form.InvalidateNonClient();
        }
    }

    private void HookWorkspacePersistence()
    {
        if (_workspace == null || _workspacePersistenceHooked)
        {
            return;
        }

        _workspace.GlobalSaving += OnWorkspaceGlobalSaving;
        _workspace.GlobalLoading += OnWorkspaceGlobalLoading;
        _workspacePersistenceHooked = true;
    }

    private void UnhookWorkspacePersistence()
    {
        if (_workspace == null || !_workspacePersistenceHooked)
        {
            return;
        }

        _workspace.GlobalSaving -= OnWorkspaceGlobalSaving;
        _workspace.GlobalLoading -= OnWorkspaceGlobalLoading;
        _workspacePersistenceHooked = false;
    }

    private void OnWorkspaceGlobalSaving(object? sender, XmlSavingEventArgs e) =>
        NavigatorTabGroupLayoutSerializer.WriteGroups(e.XmlWriter, _tabGroups);

    private void OnWorkspaceGlobalLoading(object? sender, XmlLoadingEventArgs e)
    {
        // Reader is positioned on CGD; scan for optional NTG child.
        if (e.XmlReader.IsEmptyElement)
        {
            return;
        }

        while (e.XmlReader.Read())
        {
            if (e.XmlReader.NodeType == XmlNodeType.EndElement && e.XmlReader.Name == @"CGD")
            {
                // Workspace loader also consumes until CGD end — avoid double-consuming by
                // only reading while still inside custom data. Break before the EndElement
                // so the workspace loop can finish cleanly.
                break;
            }

            if (e.XmlReader.NodeType == XmlNodeType.Element && e.XmlReader.Name == @"NTG")
            {
                NavigatorTabGroupLayoutSerializer.ReadGroups(e.XmlReader, _tabGroups);
            }
        }
    }

    private void OnFormChromeReady(object? sender, EventArgs e)
    {
        if (!_applied || _mode != NavigatorFormIntegrationMode.CaptionIntegrated || _captionInjected)
        {
            return;
        }

        if (_workspace != null)
        {
            InjectCaptionDocumentGroups();
        }
        else
        {
            InjectCaptionTabs();
        }
    }

    internal void ShowTabContextMenu(KryptonPage page, Point screenPoint)
    {
        KryptonNavigator? hostNavigator = ResolveNavigatorForPage(page);

        if (CommonHelper.ValidKryptonContextMenu(page.KryptonContextMenu))
        {
            page.KryptonContextMenu!.Show((Control?)hostNavigator ?? _form!, screenPoint);
            return;
        }

        if (CommonHelper.ValidContextMenuStrip(page.ContextMenuStrip))
        {
            page.ContextMenuStrip!.Show(screenPoint);
            return;
        }

        if (!_useBuiltInTabContextMenu || hostNavigator == null)
        {
            return;
        }

        EnsureBuiltInTabContextMenu();
        _contextMenuPage = page;
        UpdateBuiltInTabContextMenuState(page, hostNavigator);

        var args = new NavigatorTabContextMenuEventArgs(page, _builtInTabContextMenu!)
        {
            Cancel = false
        };
        TabContextMenuOpening?.Invoke(this, args);
        if (!args.Cancel)
        {
            args.ContextMenuStrip.Show(screenPoint);
        }
    }

    private void EnsureBuiltInTabContextMenu()
    {
        if (_builtInTabContextMenu != null)
        {
            return;
        }

        NavigatorFormIntegrationStrings strings = KryptonManager.Strings.NavigatorIntegrationStrings;
        SystemMenuStrings systemStrings = KryptonManager.Strings.SystemMenuStrings;

        _builtInTabContextMenu = new ContextMenuStrip();
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.MoveToNewWindow, null, (_, _) => MovePageToNewWindow(_contextMenuPage))
        {
            Name = "MoveToNewWindow"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripSeparator
        {
            Name = "TearOutSeparator"
        });

        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.AddToGroup)
        {
            Name = "AddToGroup"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.NewGroup, null, (_, _) => CreateGroupForContextPage())
        {
            Name = "NewGroup"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.Ungroup, null, (_, _) =>
        {
            if (_contextMenuPage != null)
            {
                UngroupPage(_contextMenuPage);
            }
        })
        {
            Name = "Ungroup"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.RenameGroup, null, (_, _) => RenameContextGroup())
        {
            Name = "RenameGroup"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.RecolorGroup, null, (_, _) => RecolorContextGroup())
        {
            Name = "RecolorGroup"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.CollapseGroup, null, (_, _) => ToggleContextGroupCollapsed())
        {
            Name = "CollapseExpandGroup"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripSeparator
        {
            Name = "GroupSeparator"
        });

        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.CloseTab, null, (_, _) => ClosePage(_contextMenuPage))
        {
            Name = "CloseTab",
            ShortcutKeys = Keys.Control | Keys.W
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.CloseOtherTabs, null, (_, _) => CloseOtherPages(_contextMenuPage))
        {
            Name = "CloseOtherTabs"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(strings.CloseTabsToTheRight, null, (_, _) => ClosePagesToRight(_contextMenuPage))
        {
            Name = "CloseTabsToTheRight"
        });

        // Form system-menu commands (same set as the native caption menu).
        _builtInTabContextMenu.Items.Add(new ToolStripSeparator
        {
            Name = "SystemMenuSeparator"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(systemStrings.Restore, null, (_, _) => ExecuteFormSystemCommand(FormSystemCommand.Restore))
        {
            Name = "SystemRestore"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(systemStrings.Move, null, (_, _) => ExecuteFormSystemCommand(FormSystemCommand.Move))
        {
            Name = "SystemMove"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(systemStrings.Size, null, (_, _) => ExecuteFormSystemCommand(FormSystemCommand.Size))
        {
            Name = "SystemSize"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(systemStrings.Minimize, null, (_, _) => ExecuteFormSystemCommand(FormSystemCommand.Minimize))
        {
            Name = "SystemMinimize"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(systemStrings.Maximize, null, (_, _) => ExecuteFormSystemCommand(FormSystemCommand.Maximize))
        {
            Name = "SystemMaximize"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripSeparator
        {
            Name = "SystemCloseSeparator"
        });
        _builtInTabContextMenu.Items.Add(new ToolStripMenuItem(systemStrings.Close, null, (_, _) => ExecuteFormSystemCommand(FormSystemCommand.Close))
        {
            Name = "SystemClose"
        });
    }

    private void UpdateBuiltInTabContextMenuState(KryptonPage page) =>
        UpdateBuiltInTabContextMenuState(page, ResolveNavigatorForPage(page));

    private void UpdateBuiltInTabContextMenuState(KryptonPage page, KryptonNavigator? navigator)
    {
        if (_builtInTabContextMenu == null || navigator == null)
        {
            return;
        }

        var hasPage = navigator.Pages.Contains(page);
        var pageCount = navigator.Pages.Count;
        var pageIndex = navigator.Pages.IndexOf(page);
        NavigatorTabGroup? pageGroup = string.IsNullOrEmpty(page.TabGroupId) ? null : _tabGroups[page.TabGroupId];
        NavigatorFormIntegrationStrings strings = KryptonManager.Strings.NavigatorIntegrationStrings;
        SystemMenuStrings systemStrings = KryptonManager.Strings.SystemMenuStrings;

        if (_builtInTabContextMenu.Items["MoveToNewWindow"] is ToolStripMenuItem move)
        {
            move.Text = strings.MoveToNewWindow;
            move.Enabled = _allowTearOut && hasPage;
        }

        if (_builtInTabContextMenu.Items["TearOutSeparator"] is ToolStripSeparator sep)
        {
            sep.Visible = _allowTearOut;
        }

        UpdateGroupMenuItems(page, pageGroup, strings);

        if (_builtInTabContextMenu.Items["CloseTab"] is ToolStripMenuItem close)
        {
            close.Text = strings.CloseTab;
            close.Enabled = hasPage && pageCount > 1;
        }

        if (_builtInTabContextMenu.Items["CloseOtherTabs"] is ToolStripMenuItem closeOther)
        {
            closeOther.Text = strings.CloseOtherTabs;
            closeOther.Enabled = hasPage && pageCount > 1;
        }

        if (_builtInTabContextMenu.Items["CloseTabsToTheRight"] is ToolStripMenuItem closeRight)
        {
            closeRight.Text = strings.CloseTabsToTheRight;
            closeRight.Enabled = hasPage && pageIndex >= 0 && pageIndex < pageCount - 1;
        }

        UpdateBuiltInSystemMenuState(systemStrings);
    }

    private KryptonNavigator? ResolveNavigatorForPage(KryptonPage page)
    {
        if (_navigator != null && _navigator.Pages.Contains(page))
        {
            return _navigator;
        }

        if (page.KryptonParentContainer is KryptonNavigator host)
        {
            return host;
        }

        return _navigator;
    }

    private void UpdateGroupMenuItems(KryptonPage page, NavigatorTabGroup? pageGroup, NavigatorFormIntegrationStrings strings)
    {
        if (_builtInTabContextMenu == null)
        {
            return;
        }

        bool showGroups = _allowTabGroups && _mode == NavigatorFormIntegrationMode.CaptionIntegrated;
        string[] groupNames =
        {
            "AddToGroup", "NewGroup", "Ungroup", "RenameGroup", "RecolorGroup", "CollapseExpandGroup", "GroupSeparator"
        };

        foreach (string name in groupNames)
        {
            if (_builtInTabContextMenu.Items[name] is ToolStripItem item)
            {
                item.Visible = showGroups;
            }
        }

        if (!showGroups)
        {
            return;
        }

        if (_builtInTabContextMenu.Items["AddToGroup"] is ToolStripMenuItem addToGroup)
        {
            addToGroup.Text = strings.AddToGroup;
            addToGroup.DropDownItems.Clear();
            foreach (NavigatorTabGroup group in _tabGroups)
            {
                NavigatorTabGroup localGroup = group;
                string title = string.IsNullOrEmpty(localGroup.Title)
                    ? (string.IsNullOrEmpty(localGroup.Id) ? strings.DefaultGroupTitle : localGroup.Id)
                    : localGroup.Title;
                var item = new ToolStripMenuItem(title)
                {
                    Checked = pageGroup != null && string.Equals(pageGroup.Id, localGroup.Id, StringComparison.Ordinal),
                    Tag = localGroup.Id
                };
                item.Click += (_, _) => AssignPageToGroup(page, localGroup.Id);
                addToGroup.DropDownItems.Add(item);
            }

            addToGroup.Enabled = _tabGroups.Count > 0;
        }

        if (_builtInTabContextMenu.Items["NewGroup"] is ToolStripMenuItem newGroup)
        {
            newGroup.Text = strings.NewGroup;
        }

        if (_builtInTabContextMenu.Items["Ungroup"] is ToolStripMenuItem ungroup)
        {
            ungroup.Text = strings.Ungroup;
            ungroup.Enabled = pageGroup != null;
        }

        if (_builtInTabContextMenu.Items["RenameGroup"] is ToolStripMenuItem rename)
        {
            rename.Text = strings.RenameGroup;
            rename.Enabled = pageGroup != null;
        }

        if (_builtInTabContextMenu.Items["RecolorGroup"] is ToolStripMenuItem recolor)
        {
            recolor.Text = strings.RecolorGroup;
            recolor.Enabled = pageGroup != null;
        }

        if (_builtInTabContextMenu.Items["CollapseExpandGroup"] is ToolStripMenuItem collapseExpand)
        {
            collapseExpand.Enabled = pageGroup != null;
            collapseExpand.Text = pageGroup is { Collapsed: true } ? strings.ExpandGroup : strings.CollapseGroup;
        }
    }

    private void UpdateBuiltInSystemMenuState(SystemMenuStrings systemStrings)
    {
        if (_builtInTabContextMenu == null)
        {
            return;
        }

        var showSystem = _includeFormSystemMenuInTabContextMenu && _form is { IsDisposed: false, ControlBox: true };
        string[] systemItemNames =
        {
            "SystemMenuSeparator",
            "SystemRestore",
            "SystemMove",
            "SystemSize",
            "SystemMinimize",
            "SystemMaximize",
            "SystemCloseSeparator",
            "SystemClose"
        };

        foreach (string name in systemItemNames)
        {
            if (_builtInTabContextMenu.Items[name] is ToolStripItem item)
            {
                item.Visible = showSystem;
            }
        }

        if (!showSystem || _form == null)
        {
            return;
        }

        FormWindowState windowState = _form.GetWindowState();

        if (_builtInTabContextMenu.Items["SystemRestore"] is ToolStripMenuItem restore)
        {
            restore.Text = systemStrings.Restore;
            restore.Enabled = windowState != FormWindowState.Normal;
        }

        if (_builtInTabContextMenu.Items["SystemMove"] is ToolStripMenuItem systemMove)
        {
            systemMove.Text = systemStrings.Move;
            systemMove.Enabled = windowState is FormWindowState.Normal or FormWindowState.Minimized;
        }

        if (_builtInTabContextMenu.Items["SystemSize"] is ToolStripMenuItem systemSize)
        {
            systemSize.Text = systemStrings.Size;
            systemSize.Enabled = windowState == FormWindowState.Normal
                && _form.FormBorderStyle is FormBorderStyle.Sizable or FormBorderStyle.SizableToolWindow;
        }

        if (_builtInTabContextMenu.Items["SystemMinimize"] is ToolStripMenuItem minimize)
        {
            minimize.Text = systemStrings.Minimize;
            minimize.Enabled = _form.MinimizeBox && windowState != FormWindowState.Minimized;
        }

        if (_builtInTabContextMenu.Items["SystemMaximize"] is ToolStripMenuItem maximize)
        {
            maximize.Text = systemStrings.Maximize;
            maximize.Enabled = _form.MaximizeBox && windowState != FormWindowState.Maximized;
        }

        if (_builtInTabContextMenu.Items["SystemClose"] is ToolStripMenuItem systemClose)
        {
            systemClose.Text = systemStrings.Close;
            systemClose.Enabled = true;
        }
    }

    private enum FormSystemCommand
    {
        Restore,
        Move,
        Size,
        Minimize,
        Maximize,
        Close
    }

    private void ExecuteFormSystemCommand(FormSystemCommand command)
    {
        if (_form is not { IsDisposed: false })
        {
            return;
        }

        switch (command)
        {
            case FormSystemCommand.Restore:
                if (_form.WindowState != FormWindowState.Normal)
                {
                    _form.WindowState = FormWindowState.Normal;
                }
                break;

            case FormSystemCommand.Move:
                // Enter Windows size/move loop (same as the native system menu).
                NativeMethods.SendSysCommand(_form.Handle, NativeMethods.SC_MOVE);
                break;

            case FormSystemCommand.Size:
                NativeMethods.SendSysCommand(_form.Handle, NativeMethods.SC_SIZE);
                break;

            case FormSystemCommand.Minimize:
                if (_form.WindowState != FormWindowState.Minimized)
                {
                    _form.WindowState = FormWindowState.Minimized;
                }
                break;

            case FormSystemCommand.Maximize:
                if (_form.WindowState != FormWindowState.Maximized)
                {
                    _form.WindowState = FormWindowState.Maximized;
                }
                break;

            case FormSystemCommand.Close:
                _form.Close();
                break;
        }
    }

    private void MovePageToNewWindow(KryptonPage? page)
    {
        KryptonNavigator? navigator = page == null ? null : ResolveNavigatorForPage(page);
        if (navigator == null || page == null || !navigator.Pages.Contains(page))
        {
            return;
        }

        var pages = new KryptonPageCollection
        {
            page
        };
        if (TryTearOutPages(navigator, pages, Cursor.Position))
        {
            navigator.Pages.Remove(page);
        }
    }

    private void ClosePage(KryptonPage? page)
    {
        KryptonNavigator? navigator = page == null ? null : ResolveNavigatorForPage(page);
        if (navigator == null || page == null || !navigator.Pages.Contains(page) || navigator.Pages.Count <= 1)
        {
            return;
        }

        navigator.Pages.Remove(page);
    }

    private void CloseOtherPages(KryptonPage? page)
    {
        KryptonNavigator? navigator = page == null ? null : ResolveNavigatorForPage(page);
        if (navigator == null || page == null || !navigator.Pages.Contains(page))
        {
            return;
        }

        for (var i = navigator.Pages.Count - 1; i >= 0; i--)
        {
            KryptonPage current = navigator.Pages[i];
            if (!ReferenceEquals(current, page))
            {
                navigator.Pages.Remove(current);
            }
        }

        navigator.SelectedPage = page;
    }

    private void ClosePagesToRight(KryptonPage? page)
    {
        KryptonNavigator? navigator = page == null ? null : ResolveNavigatorForPage(page);
        if (navigator == null || page == null)
        {
            return;
        }

        var startIndex = navigator.Pages.IndexOf(page);
        if (startIndex < 0)
        {
            return;
        }

        for (var i = navigator.Pages.Count - 1; i > startIndex; i--)
        {
            navigator.Pages.RemoveAt(i);
        }

        navigator.SelectedPage = page;
    }

    public DragTargetList GenerateDragTargets(PageDragEndData? dragEndData)
    {
        var targets = new DragTargetList();
        if (!IsIntegrated)
        {
            return targets;
        }

        if (_workspace is { IsDisposed: false })
        {
            targets.AddRange(_workspace.GenerateDragTargets(dragEndData));
        }

        if (_navigator is { IsDisposed: false })
        {
            if (_mode != NavigatorFormIntegrationMode.CaptionIntegrated)
            {
                targets.AddRange(_navigator.GenerateDragTargets(dragEndData));
            }
            else
            {
                Rectangle screenRect = GetNavigatorDropScreenRectangle();
                if (!screenRect.IsEmpty)
                {
                    targets.Add(new DragTargetNavigatorTransfer(screenRect, _navigator, KryptonPageFlags.All));
                }
            }
        }

        return targets;
    }

    internal IReadOnlyList<KryptonNavigatorFormIntegrator> GetRegisteredIntegrators()
    {
        var targets = new List<KryptonNavigatorFormIntegrator>();
        foreach (KryptonNavigatorFormIntegrator integrator in _registeredIntegrators)
        {
            if (integrator.IsIntegrated && integrator._navigator is { IsDisposed: false })
            {
                targets.Add(integrator);
            }
        }

        return targets;
    }

    internal bool ContainsDropTarget(Point screenPoint)
    {
        Rectangle screenRect = GetNavigatorDropScreenRectangle();
        return !screenRect.IsEmpty && screenRect.Contains(screenPoint);
    }

    private Rectangle GetNavigatorDropScreenRectangle()
    {
        if (_navigator is not { IsDisposed: false })
        {
            return Rectangle.Empty;
        }

        Rectangle screenRect = _navigator.RectangleToScreen(_navigator.ClientRectangle);

        if (_mode == NavigatorFormIntegrationMode.CaptionIntegrated
            && _captionTabs?.OwningControl is KryptonForm captionForm)
        {
            // Caption view rectangles are relative to the window rather than the client area.
            Padding borders = captionForm.RealWindowBorders;
            Point clientOrigin = captionForm.PointToScreen(Point.Empty);
            Rectangle captionRect = _captionTabs.ClientRectangle;
            captionRect.Offset(clientOrigin.X - borders.Left, clientOrigin.Y - borders.Top);

            if (!captionRect.IsEmpty)
            {
                screenRect = Rectangle.Union(screenRect, captionRect);
            }
        }

        return screenRect;
    }

    internal bool TryTearOutPages(KryptonNavigator sourceNavigator, KryptonPageCollection pages, Point dropScreenPoint)
    {
        if (_disposed || !_allowTearOut || pages.Count == 0)
        {
            return false;
        }

        var targetForm = new KryptonForm
        {
            StartPosition = FormStartPosition.Manual,
            Location = new Point(Math.Max(0, dropScreenPoint.X - 40), Math.Max(0, dropScreenPoint.Y - 16)),
            Size = _form?.Size ?? new Size(900, 600),
            Text = sourceNavigator.SelectedPage?.Text ?? string.Empty
        };

        var targetNavigator = new KryptonNavigator
        {
            Dock = DockStyle.Fill,
            NavigatorMode = NavigatorMode.BarTabOnly,
            AllowPageDrag = true,
            AllowPageReorder = true
        };
        targetForm.Controls.Add(targetNavigator);

        var integrator = new KryptonNavigatorFormIntegrator
        {
            Form = targetForm,
            Navigator = targetNavigator,
            Mode = Mode,
            SyncFormTitle = SyncFormTitle,
            SuppressFormTitleWhenClientChrome = SuppressFormTitleWhenClientChrome,
            AllowTabGroups = AllowTabGroups,
            Enabled = true
        };
        CopyReferencedGroups(integrator, pages);
        // Cross-window catalog sync: also push our groups into the new window and keep local catalog.
        integrator.MergeTabGroupsFrom(_tabGroups);

        foreach (KryptonPage page in pages)
        {
            targetNavigator.Pages.Add(page);
            NavigatorTabGroup? group = string.IsNullOrEmpty(page.TabGroupId) ? null : integrator.TabGroups[page.TabGroupId];
            NavigatorTabGroupBarAccent.Apply(page, group);
        }

        if (targetNavigator.AllowTabSelect && targetNavigator.Pages.Count > 0)
        {
            targetNavigator.SelectedPage = targetNavigator.Pages[targetNavigator.Pages.Count - 1];
        }

        targetForm.Show();
        _ = integrator;
        return true;
    }

    private void CopyReferencedGroups(KryptonNavigatorFormIntegrator target, KryptonPageCollection pages)
    {
        foreach (KryptonPage page in pages)
        {
            if (string.IsNullOrEmpty(page.TabGroupId))
            {
                continue;
            }

            NavigatorTabGroup? sourceGroup = _tabGroups[page.TabGroupId];
            if (sourceGroup == null)
            {
                continue;
            }

            if (target.TabGroups[sourceGroup.Id] == null)
            {
                target.TabGroups.Add(sourceGroup.Clone());
            }
        }
    }

    private void ClusterPageWithGroup(KryptonPage page, string groupId)
    {
        KryptonNavigator? navigator = page.KryptonParentContainer as KryptonNavigator ?? _navigator;
        if (navigator == null || !navigator.Pages.Contains(page))
        {
            return;
        }

        int insertIndex = -1;
        for (var i = 0; i < navigator.Pages.Count; i++)
        {
            KryptonPage candidate = navigator.Pages[i];
            if (!ReferenceEquals(candidate, page) &&
                string.Equals(candidate.TabGroupId, groupId, StringComparison.Ordinal))
            {
                insertIndex = i + 1;
            }
        }

        if (insertIndex < 0)
        {
            return;
        }

        int currentIndex = navigator.Pages.IndexOf(page);
        if (currentIndex < 0 || currentIndex == insertIndex || currentIndex + 1 == insertIndex)
        {
            return;
        }

        navigator.Pages.Remove(page);
        if (insertIndex > currentIndex)
        {
            insertIndex--;
        }

        insertIndex = Math.Max(0, Math.Min(insertIndex, navigator.Pages.Count));
        navigator.Pages.Insert(insertIndex, page);
    }

    private static Color PickNextGroupColor()
    {
        Color[] palette =
        {
            Color.DodgerBlue,
            Color.MediumSeaGreen,
            Color.Orange,
            Color.MediumOrchid,
            Color.Tomato,
            Color.CadetBlue,
            Color.Goldenrod
        };
        return palette[Environment.TickCount % palette.Length];
    }

    private void CreateGroupForContextPage()
    {
        if (_contextMenuPage == null)
        {
            return;
        }

        CreateGroup(assignPage: _contextMenuPage);
    }

    private void RenameContextGroup()
    {
        if (_contextMenuPage == null || string.IsNullOrEmpty(_contextMenuPage.TabGroupId))
        {
            return;
        }

        NavigatorTabGroup? group = _tabGroups[_contextMenuPage.TabGroupId];
        if (group == null || _form == null)
        {
            return;
        }

        NavigatorFormIntegrationStrings strings = KryptonManager.Strings.NavigatorIntegrationStrings;
        string title = KryptonInputBox.Show(new KryptonInputBoxData
        {
            Owner = _form,
            Caption = strings.RenameGroup,
            Prompt = strings.RenameGroupPrompt,
            DefaultResponse = group.Title,
            CueText = strings.GroupNameCue
        });

        if (!string.IsNullOrWhiteSpace(title) && !string.Equals(title, group.Title, StringComparison.Ordinal))
        {
            group.Title = title.Trim();
            OnTabGroupChanged(EventArgs.Empty);
        }
    }

    private void RecolorContextGroup()
    {
        if (_contextMenuPage == null || string.IsNullOrEmpty(_contextMenuPage.TabGroupId))
        {
            return;
        }

        NavigatorTabGroup? group = _tabGroups[_contextMenuPage.TabGroupId];
        if (group == null)
        {
            return;
        }

        using var dialog = new KryptonColorDialog
        {
            Color = group.Color,
            FullOpen = true
        };
        if (dialog.ShowDialog(_form) == DialogResult.OK)
        {
            group.Color = dialog.Color;
            if (_navigator != null)
            {
                NavigatorTabGroupBarAccent.SyncNavigator(_navigator, _tabGroups);
            }

            if (_workspace != null)
            {
                NavigatorTabGroupBarAccent.SyncWorkspace(_workspace, _tabGroups);
            }

            OnTabGroupChanged(EventArgs.Empty);
        }
    }

    private void ToggleContextGroupCollapsed()
    {
        if (_contextMenuPage == null || string.IsNullOrEmpty(_contextMenuPage.TabGroupId))
        {
            return;
        }

        ToggleGroupCollapsed(_contextMenuPage.TabGroupId);
    }

    private void OnTabGroupsCollectionChanged(object sender, TypedCollectionEventArgs<NavigatorTabGroup> e) =>
        OnTabGroupChanged(EventArgs.Empty);

    private void OnTabGroupsCollectionCleared(object? sender, EventArgs e) =>
        OnTabGroupChanged(EventArgs.Empty);

    private static class NativeMethods
    {
        internal const int WM_SYSCOMMAND = 0x0112;
        internal const int SC_SIZE = 0xF000;
        internal const int SC_MOVE = 0xF010;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        internal static void SendSysCommand(IntPtr handle, int sysCommand) =>
            SendMessage(handle, WM_SYSCOMMAND, new IntPtr(sysCommand), IntPtr.Zero);
    }

    #endregion
}
