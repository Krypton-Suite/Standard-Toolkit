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
/// One shared taskbar registration owner per taskbar-visible host form.
/// Merges pages from every enabled <see cref="KryptonNavigatorTaskbarThumbnails"/> on that host.
/// </summary>
internal sealed class NavigatorTaskbarHostCoordinator : IDisposable
{
    private static readonly uint s_taskbarButtonCreatedMsg = PI.RegisterWindowMessage("TaskbarButtonCreated");
    private static readonly Dictionary<Form, NavigatorTaskbarHostCoordinator> s_coordinators =
        new Dictionary<Form, NavigatorTaskbarHostCoordinator>();

    private readonly Form _hostForm;
    private readonly List<KryptonNavigatorTaskbarThumbnails> _components =
        new List<KryptonNavigatorTaskbarThumbnails>();
    private readonly Dictionary<KryptonPage, PageEntry> _entries =
        new Dictionary<KryptonPage, PageEntry>();
    private readonly Dictionary<KryptonPage, Bitmap> _snapshots =
        new Dictionary<KryptonPage, Bitmap>();
    private readonly HashSet<KryptonPage> _dirtyPages = new HashSet<KryptonPage>();
    private readonly Queue<KryptonPage> _idleCaptureQueue = new Queue<KryptonPage>();
    private readonly HashSet<KryptonPage> _idleCapturePending = new HashSet<KryptonPage>();

    private FormTaskbarListener? _formListener;
    private PI.ITaskbarList3? _taskbar;
    private PI.ITaskbarList4? _taskbar4;
    private bool _taskbarButtonCreated;
    private bool _disposed;
    private bool _syncing;
    private bool _idleCaptureScheduled;
    private System.Windows.Forms.Timer? _debounceTimer;
    private KryptonPage? _debouncePage;
    private KryptonPage? _lastActivatedPage;
    private KryptonNavigator? _lastActivatedNavigator;
    private bool _thumbButtonsAdded;

    private const uint PW_RENDERFULLCONTENT = 0x00000002;
    private const int DebounceMs = 200;

    private NavigatorTaskbarHostCoordinator(Form hostForm)
    {
        _hostForm = hostForm;
        _hostForm.HandleCreated += OnHostHandleCreated;
        _hostForm.HandleDestroyed += OnHostHandleDestroyed;
        _hostForm.Disposed += OnHostDisposed;
        if (_hostForm.IsHandleCreated)
        {
            AttachFormListener();
        }
    }

    public Form HostForm => _hostForm;

    public static NavigatorTaskbarHostCoordinator GetOrCreate(Form hostForm)
    {
        if (!s_coordinators.TryGetValue(hostForm, out NavigatorTaskbarHostCoordinator? coordinator))
        {
            coordinator = new NavigatorTaskbarHostCoordinator(hostForm);
            s_coordinators[hostForm] = coordinator;
        }

        return coordinator;
    }

    public static Form? ResolveTaskbarHost(Form? start)
    {
        Form? current = start;
        while (current != null)
        {
            if (current.ShowInTaskbar && !current.IsDisposed)
            {
                return current;
            }

            current = current.Owner;
        }

        return null;
    }

    public void Register(KryptonNavigatorTaskbarThumbnails component)
    {
        if (_disposed || _components.Contains(component))
        {
            return;
        }

        _components.Add(component);
    }

    public void Unregister(KryptonNavigatorTaskbarThumbnails component)
    {
        if (!_components.Remove(component))
        {
            return;
        }

        var remove = new List<KryptonPage>();
        foreach (KeyValuePair<KryptonPage, PageEntry> pair in _entries)
        {
            if (ReferenceEquals(pair.Value.Component, component))
            {
                remove.Add(pair.Key);
            }
        }

        foreach (KryptonPage page in remove)
        {
            UnregisterProxy(page);
        }

        if (_components.Count == 0)
        {
            Dispose();
        }
        else
        {
            Sync();
        }
    }

    public void Sync()
    {
        if (_disposed || _syncing || CommonHelper.DesignMode())
        {
            return;
        }

        _syncing = true;
        try
        {
            if (!_hostForm.IsHandleCreated || !IsTaskbarApiSupported())
            {
                TearDownAll();
                return;
            }

            if (!_taskbarButtonCreated)
            {
                TearDownAll();
                return;
            }

            EnsureTaskbar();
            if (_taskbar == null)
            {
                return;
            }

            var eligible = CollectEligiblePages();
            var keep = new HashSet<KryptonPage>();
            foreach (EligiblePage item in eligible)
            {
                keep.Add(item.Page);
            }

            var remove = new List<KryptonPage>();
            foreach (KeyValuePair<KryptonPage, PageEntry> pair in _entries)
            {
                if (!keep.Contains(pair.Key))
                {
                    remove.Add(pair.Key);
                }
            }

            foreach (KryptonPage page in remove)
            {
                UnregisterProxy(page);
            }

            foreach (EligiblePage item in eligible)
            {
                if (!_entries.TryGetValue(item.Page, out PageEntry? entry))
                {
                    CreateAndRegisterProxy(item);
                }
                else
                {
                    entry.Component = item.Component;
                    entry.Navigator = item.Navigator;
                    entry.Proxy.UpdateCaption();
                    ApplyTabProperties(entry);
                    try
                    {
                        _taskbar.SetThumbnailTooltip(entry.Proxy.Handle, entry.Proxy.Text);
                    }
                    catch (Exception ex)
                    {
                        KryptonExceptionHandler.CaptureException(ex);
                    }

                    EnqueueIdleCapture(item.Page);
                }
            }

            IntPtr insertBefore = IntPtr.Zero;
            for (var i = eligible.Count - 1; i >= 0; i--)
            {
                if (_entries.TryGetValue(eligible[i].Page, out PageEntry? entry) &&
                    entry.Proxy.IsHandleCreated)
                {
                    try
                    {
                        _taskbar.SetTabOrder(entry.Proxy.Handle, insertBefore);
                        insertBefore = entry.Proxy.Handle;
                    }
                    catch (Exception ex)
                    {
                        KryptonExceptionHandler.CaptureException(ex);
                    }
                }
            }

            UpdateActiveTab();
            ApplyHostShellFromSelectedPage();
        }
        finally
        {
            _syncing = false;
        }
    }

    public void UpdateActiveTab()
    {
        if (_taskbar == null || !_hostForm.IsHandleCreated)
        {
            return;
        }

        EligiblePage? selected = FindSelectedEligiblePage();
        if (selected == null)
        {
            return;
        }

        if (!_entries.TryGetValue(selected.Value.Page, out PageEntry? entry) ||
            !entry.Proxy.IsHandleCreated)
        {
            return;
        }

        CaptureSnapshot(selected.Value.Page, force: true);
        entry.Proxy.InvalidateIconicBitmaps();
        ApplyTabProperties(entry);

        try
        {
            _taskbar.SetTabActive(entry.Proxy.Handle, _hostForm.Handle, 0);
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }

        ApplyHostShellFromSelectedPage();
    }

    public void OnSelectedPageChanged(KryptonNavigatorTaskbarThumbnails component, KryptonPage? previousPage)
    {
        if (previousPage != null && !previousPage.IsDisposed)
        {
            previousPage.Invalidated -= OnSelectedPageInvalidated;
            previousPage.Paint -= OnSelectedPagePaint;
            CaptureSnapshot(previousPage, force: true);
            if (_entries.TryGetValue(previousPage, out PageEntry? previousEntry))
            {
                previousEntry.Proxy.InvalidateIconicBitmaps();
                ApplyTabProperties(previousEntry);
            }
        }

        AttachSelectedPageInvalidate(component);
        ScheduleDebouncedInvalidate(component.Navigator?.SelectedPage);
        UpdateActiveTab();
    }

    public void InvalidatePage(KryptonPage page)
    {
        _dirtyPages.Add(page);
        CaptureSnapshot(page, force: true);
        if (_entries.TryGetValue(page, out PageEntry? entry))
        {
            entry.Proxy.UpdateCaption();
            entry.Proxy.InvalidateIconicBitmaps();
        }
    }

    public void OnProxyActivated(KryptonPage page)
    {
        if (_disposed || !_entries.TryGetValue(page, out PageEntry? entry))
        {
            return;
        }

        _lastActivatedPage = page;
        _lastActivatedNavigator = entry.Navigator;

        Control marshal = entry.Navigator.IsHandleCreated
            ? (Control)entry.Navigator
            : _hostForm;

        if (marshal.IsHandleCreated)
        {
            marshal.BeginInvoke(new Action(() => ApplyProxyActivation(page)));
        }
        else
        {
            ApplyProxyActivation(page);
        }
    }

    public void OnProxyCloseRequested(KryptonPage page)
    {
        if (_disposed || !_entries.TryGetValue(page, out PageEntry? entry))
        {
            return;
        }

        if (!entry.Component.AllowCloseFromThumbnail)
        {
            return;
        }

        if (!entry.Navigator.Pages.Contains(page))
        {
            return;
        }

        CloseButtonAction action = entry.Navigator.PerformCloseAction(page);
        // CloseButtonAction.None / cancelled leave the page registered; Sync removes only when ineligible.
        if (action == CloseButtonAction.None || entry.Navigator.Pages.Contains(page))
        {
            Sync();
            return;
        }

        Sync();
    }

    public void ProvideIconicThumbnail(NavigatorTaskbarThumbnailProxy proxy, Size maxSize, bool livePreview)
    {
        if (!proxy.IsHandleCreated || maxSize.Width <= 0 || maxSize.Height <= 0)
        {
            return;
        }

        if (!_entries.TryGetValue(proxy.Page, out PageEntry? entry))
        {
            return;
        }

        using Bitmap bitmap = CreatePageBitmap(entry, maxSize, livePreview);
        IntPtr hBitmap = bitmap.GetHbitmap();
        IntPtr pptClient = IntPtr.Zero;
        try
        {
            if (livePreview)
            {
                // Offset of the live-preview bitmap within the proxy client area (top-left).
                var point = new PI.POINT(0, 0);
                pptClient = Marshal.AllocHGlobal(Marshal.SizeOf(point));
                Marshal.StructureToPtr(point, pptClient, false);
                PI.Dwm.DwmSetIconicLivePreviewBitmap(proxy.Handle, hBitmap, pptClient, PI.Dwm.DWM_SIT.DisplayFrame);
            }
            else
            {
                PI.Dwm.DwmSetIconicThumbnail(proxy.Handle, hBitmap, PI.Dwm.DWM_SIT.None);
            }
        }
        finally
        {
            if (pptClient != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(pptClient);
            }

            PI.DeleteObject(hBitmap);
        }
    }

    public Size GetLivePreviewSize(KryptonPage page)
    {
        if (_hostForm.ClientSize.Width > 0 && _hostForm.ClientSize.Height > 0)
        {
            return _hostForm.ClientSize;
        }

        if (page.IsHandleCreated && page.Width > 0 && page.Height > 0)
        {
            return page.Size;
        }

        return new Size(200, 150);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        s_coordinators.Remove(_hostForm);

        _debounceTimer?.Stop();
        _debounceTimer?.Dispose();
        _debounceTimer = null;

        foreach (KryptonNavigatorTaskbarThumbnails component in _components.ToArray())
        {
            DetachSelectedPageInvalidate(component);
        }

        TearDownAll();
        ClearSnapshots();
        DetachFormListener();
        ReleaseTaskbar();
        ClearHostOverlay();

        _hostForm.HandleCreated -= OnHostHandleCreated;
        _hostForm.HandleDestroyed -= OnHostHandleDestroyed;
        _hostForm.Disposed -= OnHostDisposed;
        _components.Clear();
    }

    private void ApplyProxyActivation(KryptonPage page)
    {
        if (_disposed || !_entries.TryGetValue(page, out PageEntry? entry))
        {
            return;
        }

        if (!entry.Navigator.Pages.Contains(page))
        {
            return;
        }

        if (entry.Navigator.SelectedPage != page)
        {
            entry.Navigator.SelectedPage = page;
        }

        if (_hostForm.IsHandleCreated)
        {
            if (_hostForm.WindowState == FormWindowState.Minimized)
            {
                _hostForm.WindowState = FormWindowState.Normal;
            }

            _hostForm.Activate();
            _hostForm.BringToFront();
        }

        UpdateActiveTab();
    }

    private List<EligiblePage> CollectEligiblePages()
    {
        var result = new List<EligiblePage>();
        foreach (KryptonNavigatorTaskbarThumbnails component in _components)
        {
            if (!component.Enabled || component.Navigator == null || component.Navigator.IsDisposed)
            {
                continue;
            }

            KryptonNavigator navigator = component.Navigator;
            Form? pageHost = ResolveTaskbarHost(navigator.FindForm());
            if (!ReferenceEquals(pageHost, _hostForm))
            {
                continue;
            }

            int max = component.MaxThumbnails;
            int added = 0;
            bool includeHidden = component.IncludeHiddenPages;

            foreach (KryptonPage page in navigator.Pages)
            {
                if (!page.AreFlagsSet(KryptonPageFlags.AllowTaskbarThumbnail))
                {
                    continue;
                }

                if (!includeHidden && !page.LastVisibleSet)
                {
                    continue;
                }

                Form? owningForm = page.FindForm();
                Form? owningHost = ResolveTaskbarHost(owningForm);
                if (!ReferenceEquals(owningHost, _hostForm))
                {
                    // Floated onto a non-taskbar window (or another host): skip.
                    continue;
                }

                result.Add(new EligiblePage(component, navigator, page));
                added++;
                if (max > 0 && added >= max)
                {
                    break;
                }
            }
        }

        return result;
    }

    private EligiblePage? FindSelectedEligiblePage()
    {
        if (_lastActivatedPage != null &&
            _entries.TryGetValue(_lastActivatedPage, out PageEntry? lastEntry) &&
            ReferenceEquals(lastEntry.Navigator.SelectedPage, _lastActivatedPage))
        {
            return new EligiblePage(lastEntry.Component, lastEntry.Navigator, _lastActivatedPage);
        }

        if (_lastActivatedNavigator != null &&
            !_lastActivatedNavigator.IsDisposed &&
            _lastActivatedNavigator.SelectedPage != null &&
            _entries.ContainsKey(_lastActivatedNavigator.SelectedPage))
        {
            PageEntry entry = _entries[_lastActivatedNavigator.SelectedPage];
            return new EligiblePage(entry.Component, entry.Navigator, _lastActivatedNavigator.SelectedPage);
        }

        foreach (KryptonNavigatorTaskbarThumbnails component in _components)
        {
            KryptonPage? selected = component.Navigator?.SelectedPage;
            if (selected != null && _entries.TryGetValue(selected, out PageEntry? entry))
            {
                return new EligiblePage(entry.Component, entry.Navigator, selected);
            }
        }

        return null;
    }

    private void CreateAndRegisterProxy(EligiblePage item)
    {
        if (_taskbar == null || !_hostForm.IsHandleCreated)
        {
            return;
        }

        var proxy = new NavigatorTaskbarThumbnailProxy(this, item.Page);
        try
        {
            proxy.ShowProxy(_hostForm);
            proxy.EnsureIconicAttributes();
            proxy.UpdateCaption();

            _taskbar.RegisterTab(proxy.Handle, _hostForm.Handle);
            _taskbar.SetTabOrder(proxy.Handle, IntPtr.Zero);
            try
            {
                _taskbar.SetThumbnailTooltip(proxy.Handle, proxy.Text);
            }
            catch (Exception ex)
            {
                KryptonExceptionHandler.CaptureException(ex);
            }

            item.Page.AppearancePropertyChanged += OnPageAppearancePropertyChanged;
            item.Page.Disposed += OnPageDisposed;
            item.Page.VisibleChanged += OnPageVisibleChanged;
            item.Page.FlagsChanged += OnPageFlagsChanged;
            item.Page.ParentChanged += OnPageParentChanged;

            var entry = new PageEntry(item.Component, item.Navigator, proxy);
            _entries[item.Page] = entry;
            ApplyTabProperties(entry);
            CaptureSnapshot(item.Page, force: true);
            EnqueueIdleCapture(item.Page);
            proxy.InvalidateIconicBitmaps();
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
            proxy.Dispose();
        }
    }

    private void UnregisterProxy(KryptonPage page)
    {
        if (!_entries.TryGetValue(page, out PageEntry? entry))
        {
            return;
        }

        _entries.Remove(page);
        _dirtyPages.Remove(page);
        _idleCapturePending.Remove(page);
        page.AppearancePropertyChanged -= OnPageAppearancePropertyChanged;
        page.Disposed -= OnPageDisposed;
        page.VisibleChanged -= OnPageVisibleChanged;
        page.FlagsChanged -= OnPageFlagsChanged;
        page.ParentChanged -= OnPageParentChanged;
        RemoveSnapshot(page);

        try
        {
            if (_taskbar != null && entry.Proxy.IsHandleCreated)
            {
                _taskbar.UnregisterTab(entry.Proxy.Handle);
            }
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }

        entry.Proxy.Dispose();
    }

    private void TearDownAll()
    {
        var pages = new List<KryptonPage>(_entries.Keys);
        foreach (KryptonPage page in pages)
        {
            UnregisterProxy(page);
        }
    }

    private void ApplyTabProperties(PageEntry entry)
    {
        if (_taskbar4 == null || !entry.Proxy.IsHandleCreated)
        {
            return;
        }

        PI.STPFLAG flags = PI.STPFLAG.STPF_NONE;
        bool isActive = ReferenceEquals(entry.Navigator.SelectedPage, entry.Proxy.Page);
        if (isActive && entry.Component.ActiveTabUsesAppPreview)
        {
            flags = PI.STPFLAG.STPF_USEAPPTHUMBNAILWHENACTIVE | PI.STPFLAG.STPF_USEAPPPEEKWHENACTIVE;
        }

        try
        {
            _taskbar4.SetTabProperties(entry.Proxy.Handle, flags);
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }
    }

    private void ApplyHostShellFromSelectedPage()
    {
        if (_taskbar == null || !_hostForm.IsHandleCreated)
        {
            return;
        }

        EligiblePage? selected = FindSelectedEligiblePage();
        if (selected == null)
        {
            ClearHostOverlay();
            try
            {
                _taskbar.SetProgressState(_hostForm.Handle, PI.TBPFLAG.TBPF_NOPROGRESS);
            }
            catch (Exception ex)
            {
                KryptonExceptionHandler.CaptureException(ex);
            }
            return;
        }

        KryptonNavigatorTaskbarThumbnails component = selected.Value.Component;
        KryptonPage page = selected.Value.Page;

        if (component.UseSelectedPageOverlay)
        {
            var overlayArgs = new QueryTaskbarOverlayEventArgs(page);
            component.RaiseQueryOverlay(overlayArgs);
            SetHostOverlay(overlayArgs.Icon, overlayArgs.Description);
        }
        else
        {
            ClearHostOverlay();
        }

        if (component.UseSelectedPageProgress)
        {
            var progressArgs = new QueryTaskbarProgressEventArgs(page);
            component.RaiseQueryProgress(progressArgs);
            try
            {
                PI.TBPFLAG nativeState = (PI.TBPFLAG)(int)progressArgs.State;
                _taskbar.SetProgressState(_hostForm.Handle, nativeState);
                if (progressArgs.State != TaskbarProgressState.NoProgress &&
                    progressArgs.State != TaskbarProgressState.Indeterminate)
                {
                    _taskbar.SetProgressValue(_hostForm.Handle, progressArgs.Completed,
                        progressArgs.Total == 0 ? 1UL : progressArgs.Total);
                }
            }
            catch (Exception ex)
            {
                KryptonExceptionHandler.CaptureException(ex);
            }
        }

        if (component.UseSelectedPageThumbnailButtons)
        {
            var buttonArgs = new QueryTaskbarThumbnailButtonsEventArgs(page);
            component.RaiseQueryThumbnailButtons(buttonArgs);
            ApplyThumbnailButtons(buttonArgs.Buttons);
        }
    }

    private void SetHostOverlay(Icon? icon, string? description)
    {
        if (_taskbar == null || !_hostForm.IsHandleCreated)
        {
            return;
        }

        try
        {
            // Icon ownership remains with the event handler.
            _taskbar.SetOverlayIcon(_hostForm.Handle, icon?.Handle ?? IntPtr.Zero, description ?? string.Empty);
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }
    }

    private void ClearHostOverlay()
    {
        if (_taskbar != null && _hostForm.IsHandleCreated)
        {
            try
            {
                _taskbar.SetOverlayIcon(_hostForm.Handle, IntPtr.Zero, string.Empty);
            }
            catch (Exception ex)
            {
                KryptonExceptionHandler.CaptureException(ex);
            }
        }
    }

    private void ApplyThumbnailButtons(IList<TaskbarThumbnailButton>? buttons)
    {
        if (_taskbar == null || !_hostForm.IsHandleCreated || buttons == null || buttons.Count == 0)
        {
            return;
        }

        int count = Math.Min(7, buttons.Count);
        var native = new PI.THUMBBUTTON[count];
        for (var i = 0; i < count; i++)
        {
            TaskbarThumbnailButton button = buttons[i];
            native[i] = new PI.THUMBBUTTON
            {
                dwMask = PI.THUMBBUTTONMASK.THB_FLAGS | PI.THUMBBUTTONMASK.THB_TOOLTIP,
                iId = button.Id,
                szTip = button.Tooltip ?? string.Empty,
                dwFlags = (PI.THUMBBUTTONFLAGS)(int)button.Flags,
                hIcon = button.Icon?.Handle ?? IntPtr.Zero
            };
            if (button.Icon != null)
            {
                native[i].dwMask |= PI.THUMBBUTTONMASK.THB_ICON;
            }
        }

        IntPtr buffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PI.THUMBBUTTON)) * count);
        try
        {
            for (var i = 0; i < count; i++)
            {
                IntPtr element = IntPtr.Add(buffer, i * Marshal.SizeOf(typeof(PI.THUMBBUTTON)));
                Marshal.StructureToPtr(native[i], element, false);
            }

            if (!_thumbButtonsAdded)
            {
                _taskbar.ThumbBarAddButtons(_hostForm.Handle, (uint)count, buffer);
                _thumbButtonsAdded = true;
            }
            else
            {
                _taskbar.ThumbBarUpdateButtons(_hostForm.Handle, (uint)count, buffer);
            }
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    private Bitmap CreatePageBitmap(PageEntry entry, Size maxSize, bool livePreview)
    {
        var args = new QueryTaskbarThumbnailEventArgs(entry.Proxy.Page, maxSize, livePreview);
        entry.Component.RaiseQueryThumbnail(args);

        if (args.Thumbnail != null)
        {
            return FitBitmap(args.Thumbnail, maxSize);
        }

        if (livePreview && entry.Component.ActiveTabUsesAppPreview &&
            ReferenceEquals(entry.Navigator.SelectedPage, entry.Proxy.Page))
        {
            Bitmap? hostCapture = TryCaptureControl(_hostForm);
            if (hostCapture != null)
            {
                using (hostCapture)
                {
                    return FitBitmap(hostCapture, maxSize);
                }
            }
        }

        CaptureSnapshot(entry.Proxy.Page, force: entry.Proxy.Page.Visible || _dirtyPages.Contains(entry.Proxy.Page));
        if (_snapshots.TryGetValue(entry.Proxy.Page, out Bitmap? snapshot) && snapshot != null)
        {
            return FitBitmap(snapshot, maxSize);
        }

        return CreatePlaceholderBitmap(entry.Proxy.Page, maxSize);
    }

    private void CaptureSnapshot(KryptonPage page, bool force)
    {
        if (page.IsDisposed)
        {
            return;
        }

        if (!force && _snapshots.ContainsKey(page) && !_dirtyPages.Contains(page))
        {
            return;
        }

        EnsurePageCapturable(page);

        if (!page.IsHandleCreated || page.Width <= 0 || page.Height <= 0)
        {
            return;
        }

        Bitmap? captured = TryCaptureControl(page);
        if (captured == null)
        {
            return;
        }

        if (_snapshots.TryGetValue(page, out Bitmap? old))
        {
            old.Dispose();
        }

        _snapshots[page] = captured;
        _dirtyPages.Remove(page);
    }

    private static void EnsurePageCapturable(KryptonPage page)
    {
        if (page.IsDisposed)
        {
            return;
        }

        if (!page.IsHandleCreated)
        {
            try
            {
                _ = page.Handle;
            }
            catch (Exception ex)
            {
                KryptonExceptionHandler.CaptureException(ex);
            }
        }

        if (page.Width <= 0 || page.Height <= 0)
        {
            Size parentSize = page.Parent?.ClientSize ?? new Size(400, 300);
            if (parentSize.Width > 0 && parentSize.Height > 0)
            {
                page.SetBounds(page.Left, page.Top, parentSize.Width, parentSize.Height);
            }
        }
    }

    private void EnqueueIdleCapture(KryptonPage page)
    {
        if (_idleCapturePending.Contains(page) || page.IsDisposed)
        {
            return;
        }

        _idleCapturePending.Add(page);
        _idleCaptureQueue.Enqueue(page);
        ScheduleIdleCapture();
    }

    private void ScheduleIdleCapture()
    {
        if (_idleCaptureScheduled || !_hostForm.IsHandleCreated)
        {
            return;
        }

        _idleCaptureScheduled = true;
        _hostForm.BeginInvoke(new Action(ProcessIdleCaptureQueue));
    }

    private void ProcessIdleCaptureQueue()
    {
        _idleCaptureScheduled = false;
        if (_disposed)
        {
            return;
        }

        const int maxPerPulse = 2;
        for (var i = 0; i < maxPerPulse && _idleCaptureQueue.Count > 0; i++)
        {
            KryptonPage page = _idleCaptureQueue.Dequeue();
            _idleCapturePending.Remove(page);
            if (!_entries.ContainsKey(page) || page.IsDisposed)
            {
                continue;
            }

            CaptureSnapshot(page, force: true);
            if (_entries.TryGetValue(page, out PageEntry? entry))
            {
                entry.Proxy.InvalidateIconicBitmaps();
            }
        }

        if (_idleCaptureQueue.Count > 0)
        {
            ScheduleIdleCapture();
        }
    }

    private void AttachSelectedPageInvalidate(KryptonNavigatorTaskbarThumbnails component)
    {
        KryptonPage? page = component.Navigator?.SelectedPage;
        if (page == null || page.IsDisposed)
        {
            return;
        }

        page.Invalidated += OnSelectedPageInvalidated;
        page.Paint += OnSelectedPagePaint;
    }

    private void DetachSelectedPageInvalidate(KryptonNavigatorTaskbarThumbnails component)
    {
        KryptonPage? page = component.Navigator?.SelectedPage;
        if (page == null)
        {
            // Still try known entries for this component.
            foreach (KeyValuePair<KryptonPage, PageEntry> pair in _entries)
            {
                if (ReferenceEquals(pair.Value.Component, component))
                {
                    pair.Key.Invalidated -= OnSelectedPageInvalidated;
                    pair.Key.Paint -= OnSelectedPagePaint;
                }
            }
            return;
        }

        page.Invalidated -= OnSelectedPageInvalidated;
        page.Paint -= OnSelectedPagePaint;
    }

    private void OnSelectedPageInvalidated(object? sender, InvalidateEventArgs e)
    {
        if (sender is KryptonPage page)
        {
            ScheduleDebouncedInvalidate(page);
        }
    }

    private void OnSelectedPagePaint(object? sender, PaintEventArgs e)
    {
        if (sender is KryptonPage page)
        {
            ScheduleDebouncedInvalidate(page);
        }
    }

    private void ScheduleDebouncedInvalidate(KryptonPage? page)
    {
        if (page == null || page.IsDisposed)
        {
            return;
        }

        _debouncePage = page;
        _dirtyPages.Add(page);
        _debounceTimer ??= new System.Windows.Forms.Timer { Interval = DebounceMs };
        _debounceTimer.Tick -= OnDebounceTick;
        _debounceTimer.Tick += OnDebounceTick;
        _debounceTimer.Stop();
        _debounceTimer.Start();
    }

    private void OnDebounceTick(object? sender, EventArgs e)
    {
        _debounceTimer?.Stop();
        KryptonPage? page = _debouncePage;
        if (page != null && !page.IsDisposed)
        {
            InvalidatePage(page);
        }
    }

    private void AttachFormListener()
    {
        if (!_hostForm.IsHandleCreated)
        {
            return;
        }

        DetachFormListener();
        _formListener = new FormTaskbarListener(this);
        _formListener.AssignHandle(_hostForm.Handle);

        // Late bind: TaskbarButtonCreated may already have been posted before this listener existed.
        if (_hostForm.Visible)
        {
            _taskbarButtonCreated = true;
        }
    }

    private void DetachFormListener()
    {
        if (_formListener != null)
        {
            _formListener.ReleaseHandle();
            _formListener = null;
        }
    }

    private void OnHostHandleCreated(object? sender, EventArgs e)
    {
        AttachFormListener();
        Sync();
    }

    private void OnHostHandleDestroyed(object? sender, EventArgs e)
    {
        _taskbarButtonCreated = false;
        DetachFormListener();
        TearDownAll();
        ReleaseTaskbar();
    }

    private void OnHostDisposed(object? sender, EventArgs e) => Dispose();

    private void OnTaskbarButtonCreated()
    {
        _taskbarButtonCreated = true;
        Sync();
    }

    private void EnsureTaskbar()
    {
        if (_taskbar != null)
        {
            return;
        }

        try
        {
            object com = new PI.TaskbarList();
            _taskbar = (PI.ITaskbarList3)com;
            _taskbar4 = com as PI.ITaskbarList4;
            _taskbar.HrInit();
        }
        catch (Exception ex)
        {
            _taskbar = null;
            _taskbar4 = null;
            KryptonExceptionHandler.CaptureException(ex);
        }
    }

    private void ReleaseTaskbar()
    {
        object? com = null;
        if (_taskbar4 != null)
        {
            com = _taskbar4;
        }
        else if (_taskbar != null)
        {
            com = _taskbar;
        }

        if (com != null && Marshal.IsComObject(com))
        {
            Marshal.ReleaseComObject(com);
        }

        _taskbar = null;
        _taskbar4 = null;
        _thumbButtonsAdded = false;
    }

    private void OnPageAppearancePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is KryptonPage page)
        {
            InvalidatePage(page);
        }
    }

    private void OnPageDisposed(object? sender, EventArgs e)
    {
        if (sender is KryptonPage page)
        {
            UnregisterProxy(page);
        }
    }

    private void OnPageVisibleChanged(object? sender, EventArgs e)
    {
        if (sender is KryptonPage page)
        {
            if (page.Visible)
            {
                CaptureSnapshot(page, force: true);
                if (_entries.TryGetValue(page, out PageEntry? entry))
                {
                    entry.Proxy.InvalidateIconicBitmaps();
                }
            }

            Sync();
        }
    }

    private void OnPageFlagsChanged(object? sender, KryptonPageFlagsEventArgs e)
    {
        if ((e.Flags & KryptonPageFlags.AllowTaskbarThumbnail) != 0)
        {
            Sync();
        }
    }

    private void OnPageParentChanged(object? sender, EventArgs e) => Sync();

    private static Bitmap? TryCaptureControl(Control control)
    {
        try
        {
            if (!control.IsHandleCreated || control.Width <= 0 || control.Height <= 0)
            {
                return null;
            }

            var bmp = new Bitmap(control.Width, control.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                IntPtr hdc = g.GetHdc();
                try
                {
                    if (!PI.PrintWindow(control.Handle, hdc, PW_RENDERFULLCONTENT))
                    {
                        g.ReleaseHdc(hdc);
                        hdc = IntPtr.Zero;
                        control.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                    }
                }
                finally
                {
                    if (hdc != IntPtr.Zero)
                    {
                        g.ReleaseHdc(hdc);
                    }
                }
            }

            return bmp;
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
            return null;
        }
    }

    private Bitmap CreatePlaceholderBitmap(KryptonPage page, Size maxSize)
    {
        Size size = FitSize(new Size(240, 160), maxSize);
        var result = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using (Graphics g = Graphics.FromImage(result))
        {
            g.Clear(SystemColors.Window);
            using (var border = new Pen(SystemColors.ActiveBorder))
            {
                g.DrawRectangle(border, 0, 0, size.Width - 1, size.Height - 1);
            }

            Image? image = page.ImageSmall ?? page.ImageMedium;
            var textBounds = new Rectangle(8, 8, size.Width - 16, size.Height - 16);
            if (image != null)
            {
                int icon = Math.Min(32, Math.Min(size.Width, size.Height) / 3);
                g.DrawImage(image, new Rectangle(8, 8, icon, icon));
                textBounds = new Rectangle(8, 8 + icon + 6, size.Width - 16, size.Height - (16 + icon + 6));
            }

            TextRenderer.DrawText(g, GetPageCaption(page), SystemFonts.DefaultFont, textBounds,
                SystemColors.WindowText,
                TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis);
        }

        return result;
    }

    private static Bitmap FitBitmap(Image source, Size maxSize)
    {
        Size fitted = FitSize(source.Size, maxSize);
        var result = new Bitmap(fitted.Width, fitted.Height, PixelFormat.Format32bppArgb);
        using (Graphics g = Graphics.FromImage(result))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.Clear(Color.White);
            g.DrawImage(source, new Rectangle(Point.Empty, fitted));
        }

        return result;
    }

    private static Size FitSize(Size source, Size maxSize)
    {
        if (source.Width <= 0 || source.Height <= 0)
        {
            return new Size(Math.Max(1, maxSize.Width), Math.Max(1, maxSize.Height));
        }

        if (source.Width <= maxSize.Width && source.Height <= maxSize.Height)
        {
            return source;
        }

        double scale = Math.Min((double)maxSize.Width / source.Width, (double)maxSize.Height / source.Height);
        return new Size(
            Math.Max(1, (int)Math.Round(source.Width * scale)),
            Math.Max(1, (int)Math.Round(source.Height * scale)));
    }

    private void RemoveSnapshot(KryptonPage page)
    {
        if (_snapshots.TryGetValue(page, out Bitmap? bmp))
        {
            _snapshots.Remove(page);
            bmp.Dispose();
        }
    }

    private void ClearSnapshots()
    {
        foreach (Bitmap bmp in _snapshots.Values)
        {
            bmp.Dispose();
        }

        _snapshots.Clear();
        _dirtyPages.Clear();
        _idleCaptureQueue.Clear();
        _idleCapturePending.Clear();
    }

    private static string GetPageCaption(KryptonPage page)
    {
        if (!string.IsNullOrEmpty(page.TextTitle))
        {
            return page.TextTitle!;
        }

        if (!string.IsNullOrEmpty(page.Text))
        {
            return page.Text;
        }

        return page.UniqueName;
    }

    private static bool IsTaskbarApiSupported()
    {
        Version version = Environment.OSVersion.Version;
        return version.Major > 6 || (version.Major == 6 && version.Minor >= 1);
    }

    private sealed class FormTaskbarListener : NativeWindow
    {
        private readonly NavigatorTaskbarHostCoordinator _coordinator;

        public FormTaskbarListener(NavigatorTaskbarHostCoordinator coordinator) =>
            _coordinator = coordinator;

        protected override void WndProc(ref Message m)
        {
            if (s_taskbarButtonCreatedMsg != 0 && m.Msg == (int)s_taskbarButtonCreatedMsg)
            {
                _coordinator.OnTaskbarButtonCreated();
            }

            base.WndProc(ref m);
        }
    }

    private sealed class PageEntry
    {
        public PageEntry(KryptonNavigatorTaskbarThumbnails component, KryptonNavigator navigator,
            NavigatorTaskbarThumbnailProxy proxy)
        {
            Component = component;
            Navigator = navigator;
            Proxy = proxy;
        }

        public KryptonNavigatorTaskbarThumbnails Component { get; set; }
        public KryptonNavigator Navigator { get; set; }
        public NavigatorTaskbarThumbnailProxy Proxy { get; }
    }

    private readonly struct EligiblePage
    {
        public EligiblePage(KryptonNavigatorTaskbarThumbnails component, KryptonNavigator navigator, KryptonPage page)
        {
            Component = component;
            Navigator = navigator;
            Page = page;
        }

        public KryptonNavigatorTaskbarThumbnails Component { get; }
        public KryptonNavigator Navigator { get; }
        public KryptonPage Page { get; }
    }
}
