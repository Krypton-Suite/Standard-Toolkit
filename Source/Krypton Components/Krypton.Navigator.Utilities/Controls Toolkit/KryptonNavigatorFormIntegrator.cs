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
    private ContextMenuStrip? _builtInTabContextMenu;
    private KryptonPage? _contextMenuPage;

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

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonNavigatorFormIntegrator"/> class.
    /// </summary>
    public KryptonNavigatorFormIntegrator()
    {
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

    private void TryApply(bool force = false)
    {
        if (_disposed || (!_enabled && !force))
        {
            return;
        }

        if (_form == null || _navigator == null)
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
        Debug.Assert(_form != null && _navigator != null);

        _savedControlBox = _form!.ControlBox;
        _savedFormText = _form.Text;
        _savedAllowIconDisplay = _form.AllowIconDisplay;
        _savedOwner = _navigator!.Owner;
        _savedControlKryptonFormFeatures = _navigator.ControlKryptonFormFeatures;
        _savedNavigatorMode = _navigator.NavigatorMode;
        _savedDragPageNotify = _navigator.DragPageNotify;
        _savedAllowPageDrag = _navigator.AllowPageDrag;
        _haveSavedState = true;
    }

    private void ApplyCore()
    {
        Debug.Assert(_form != null && _navigator != null);

        _navigator!.Owner = _form;
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

    private void InjectCaptionTabs()
    {
        if (_form == null || _navigator == null || _captionInjected)
        {
            return;
        }

        _captionTabs = new ViewLayoutNavigatorCaptionTabs(_navigator, OnCaptionNeedPaint, ShowTabContextMenu, OnNewTabButtonClick);
        _captionTabs.ShowNewTabButton = _showNewTabButton;
        _form.InjectViewElement(_captionTabs, ViewDockStyle.Left);
        _captionInjected = true;
        _form.PerformNeedPaint(true);
        _form.InvalidateNonClient();
    }

    private void OnNewTabButtonClick() => NewTabButtonClick?.Invoke(this, EventArgs.Empty);

    private void RevokeCaptionTabs()
    {
        if (_form == null || !_captionInjected || _captionTabs == null)
        {
            return;
        }

        _form.RevokeViewElement(_captionTabs, ViewDockStyle.Left);
        _captionTabs.Dispose();
        _captionTabs = null;
        _captionInjected = false;
        _form.CustomCaptionArea = Rectangle.Empty;
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

        if (_haveSavedState && _form != null && _navigator != null)
        {
            _form.ControlBox = _savedControlBox;
            _form.AllowIconDisplay = _savedAllowIconDisplay;
            if (_savedFormText != null)
            {
                _form.Text = _savedFormText;
            }

            _navigator.Owner = _savedOwner;
            _navigator.ControlKryptonFormFeatures = _savedControlKryptonFormFeatures;
            _navigator.NavigatorMode = _savedNavigatorMode;
            _navigator.DragPageNotify = _savedDragPageNotify;
            _navigator.AllowPageDrag = _savedAllowPageDrag;
        }

        _haveSavedState = false;
        _applied = false;

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
    }

    private void UnhookFormChromeEvents(KryptonForm? form)
    {
        if (form == null)
        {
            return;
        }

        form.HandleCreated -= OnFormChromeReady;
        form.ApplyUseThemeFormChromeBorderWidthChanged -= OnFormChromeReady;
    }

    private void OnFormChromeReady(object? sender, EventArgs e)
    {
        if (!_applied || _mode != NavigatorFormIntegrationMode.CaptionIntegrated || _captionInjected)
        {
            return;
        }

        InjectCaptionTabs();
    }

    internal void ShowTabContextMenu(KryptonPage page, Point screenPoint)
    {
        if (CommonHelper.ValidKryptonContextMenu(page.KryptonContextMenu))
        {
            page.KryptonContextMenu!.Show(_navigator!, screenPoint);
            return;
        }

        if (CommonHelper.ValidContextMenuStrip(page.ContextMenuStrip))
        {
            page.ContextMenuStrip!.Show(screenPoint);
            return;
        }

        if (!_useBuiltInTabContextMenu || _navigator == null)
        {
            return;
        }

        EnsureBuiltInTabContextMenu();
        _contextMenuPage = page;
        UpdateBuiltInTabContextMenuState(page);

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

    private void UpdateBuiltInTabContextMenuState(KryptonPage page)
    {
        if (_builtInTabContextMenu == null || _navigator == null)
        {
            return;
        }

        var hasPage = _navigator.Pages.Contains(page);
        var pageCount = _navigator.Pages.Count;
        var pageIndex = _navigator.Pages.IndexOf(page);

        if (_builtInTabContextMenu.Items["MoveToNewWindow"] is ToolStripMenuItem move)
        {
            move.Enabled = _allowTearOut && hasPage;
        }

        if (_builtInTabContextMenu.Items["TearOutSeparator"] is ToolStripSeparator sep)
        {
            sep.Visible = _allowTearOut;
        }

        if (_builtInTabContextMenu.Items["CloseTab"] is ToolStripMenuItem close)
        {
            close.Enabled = hasPage && pageCount > 1;
        }

        if (_builtInTabContextMenu.Items["CloseOtherTabs"] is ToolStripMenuItem closeOther)
        {
            closeOther.Enabled = hasPage && pageCount > 1;
        }

        if (_builtInTabContextMenu.Items["CloseTabsToTheRight"] is ToolStripMenuItem closeRight)
        {
            closeRight.Enabled = hasPage && pageIndex >= 0 && pageIndex < pageCount - 1;
        }

        UpdateBuiltInSystemMenuState();
    }

    private void UpdateBuiltInSystemMenuState()
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
            restore.Enabled = windowState != FormWindowState.Normal;
        }

        if (_builtInTabContextMenu.Items["SystemMove"] is ToolStripMenuItem systemMove)
        {
            systemMove.Enabled = windowState is FormWindowState.Normal or FormWindowState.Minimized;
        }

        if (_builtInTabContextMenu.Items["SystemSize"] is ToolStripMenuItem systemSize)
        {
            systemSize.Enabled = windowState == FormWindowState.Normal
                && _form.FormBorderStyle is FormBorderStyle.Sizable or FormBorderStyle.SizableToolWindow;
        }

        if (_builtInTabContextMenu.Items["SystemMinimize"] is ToolStripMenuItem minimize)
        {
            minimize.Enabled = _form.MinimizeBox && windowState != FormWindowState.Minimized;
        }

        if (_builtInTabContextMenu.Items["SystemMaximize"] is ToolStripMenuItem maximize)
        {
            maximize.Enabled = _form.MaximizeBox && windowState != FormWindowState.Maximized;
        }

        if (_builtInTabContextMenu.Items["SystemClose"] is ToolStripMenuItem systemClose)
        {
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
        if (_navigator == null || page == null || !_navigator.Pages.Contains(page))
        {
            return;
        }

        var pages = new KryptonPageCollection
        {
            page
        };
        if (TryTearOutPages(_navigator, pages, Cursor.Position))
        {
            _navigator.Pages.Remove(page);
        }
    }

    private void ClosePage(KryptonPage? page)
    {
        if (_navigator == null || page == null || !_navigator.Pages.Contains(page) || _navigator.Pages.Count <= 1)
        {
            return;
        }

        _navigator.Pages.Remove(page);
    }

    private void CloseOtherPages(KryptonPage? page)
    {
        if (_navigator == null || page == null || !_navigator.Pages.Contains(page))
        {
            return;
        }

        for (var i = _navigator.Pages.Count - 1; i >= 0; i--)
        {
            KryptonPage current = _navigator.Pages[i];
            if (!ReferenceEquals(current, page))
            {
                _navigator.Pages.Remove(current);
            }
        }

        _navigator.SelectedPage = page;
    }

    private void ClosePagesToRight(KryptonPage? page)
    {
        if (_navigator == null || page == null)
        {
            return;
        }

        var startIndex = _navigator.Pages.IndexOf(page);
        if (startIndex < 0)
        {
            return;
        }

        for (var i = _navigator.Pages.Count - 1; i > startIndex; i--)
        {
            _navigator.Pages.RemoveAt(i);
        }

        _navigator.SelectedPage = page;
    }

    public DragTargetList GenerateDragTargets(PageDragEndData? dragEndData)
    {
        var targets = new DragTargetList();
        if (!IsIntegrated || _navigator is not { IsDisposed: false })
        {
            return targets;
        }

        if (_mode != NavigatorFormIntegrationMode.CaptionIntegrated)
        {
            targets.AddRange(_navigator.GenerateDragTargets(dragEndData));
            return targets;
        }

        Rectangle screenRect = GetNavigatorDropScreenRectangle();
        if (!screenRect.IsEmpty)
        {
            targets.Add(new DragTargetNavigatorTransfer(screenRect, _navigator, KryptonPageFlags.All));
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
            && _captionTabs?.OwningControl != null)
        {
            Rectangle captionRect = _captionTabs.OwningControl.RectangleToScreen(_captionTabs.ClientRectangle);
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
            Enabled = true
        };

        foreach (KryptonPage page in pages)
        {
            targetNavigator.Pages.Add(page);
        }

        if (targetNavigator.AllowTabSelect && targetNavigator.Pages.Count > 0)
        {
            targetNavigator.SelectedPage = targetNavigator.Pages[targetNavigator.Pages.Count - 1];
        }

        targetForm.Show();
        _ = integrator;
        return true;
    }

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
