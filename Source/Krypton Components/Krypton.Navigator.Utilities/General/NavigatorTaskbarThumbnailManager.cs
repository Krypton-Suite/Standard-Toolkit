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
/// Owns proxy HWNDs and ITaskbarList3 registration for navigator page taskbar thumbnails.
/// </summary>
internal sealed class NavigatorTaskbarThumbnailManager : IDisposable
{
    private static readonly uint s_taskbarButtonCreatedMsg = PI.RegisterWindowMessage("TaskbarButtonCreated");

    private readonly KryptonNavigatorTaskbarThumbnails _owner;
    private readonly Dictionary<KryptonPage, NavigatorTaskbarThumbnailProxy> _proxies =
        new Dictionary<KryptonPage, NavigatorTaskbarThumbnailProxy>();
    private readonly Dictionary<KryptonPage, Bitmap> _snapshots =
        new Dictionary<KryptonPage, Bitmap>();
    private FormTaskbarListener? _formListener;
    private Form? _hostForm;
    private PI.ITaskbarList3? _taskbar;
    private bool _taskbarButtonCreated;
    private bool _disposed;
    private bool _syncing;

    private const uint PW_RENDERFULLCONTENT = 0x00000002;

    public NavigatorTaskbarThumbnailManager(KryptonNavigatorTaskbarThumbnails owner) =>
        _owner = owner;

    public Form? HostForm => _hostForm;

    public void Sync()
    {
        KryptonNavigator? navigator = _owner.Navigator;
        if (_disposed || _syncing || CommonHelper.DesignMode() ||
            navigator == null || navigator.IsDisposed)
        {
            return;
        }

        _syncing = true;
        try
        {
            if (!_owner.Enabled || !IsTaskbarApiSupported())
            {
                TearDownAll();
                DetachHostForm();
                return;
            }

            AttachHostForm(navigator.FindForm());
            if (_hostForm == null || !_hostForm.IsHandleCreated || !_taskbarButtonCreated)
            {
                TearDownAll();
                return;
            }

            EnsureTaskbar();
            if (_taskbar == null)
            {
                return;
            }

            var eligible = GetEligiblePages(navigator);
            var keep = new HashSet<KryptonPage>(eligible);

            var remove = new List<KryptonPage>();
            foreach (KeyValuePair<KryptonPage, NavigatorTaskbarThumbnailProxy> pair in _proxies)
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

            foreach (KryptonPage page in eligible)
            {
                if (!_proxies.TryGetValue(page, out NavigatorTaskbarThumbnailProxy? proxy))
                {
                    CreateAndRegisterProxy(page);
                }
                else
                {
                    proxy.UpdateCaption();
                    try
                    {
                        _taskbar.SetTabOrder(proxy.Handle, IntPtr.Zero);
                    }
                    catch (Exception ex)
                    {
                        KryptonExceptionHandler.CaptureException(ex);
                    }
                }
            }

            IntPtr insertBefore = IntPtr.Zero;
            for (var i = eligible.Count - 1; i >= 0; i--)
            {
                if (_proxies.TryGetValue(eligible[i], out NavigatorTaskbarThumbnailProxy? proxy) &&
                    proxy.IsHandleCreated)
                {
                    try
                    {
                        _taskbar.SetTabOrder(proxy.Handle, insertBefore);
                        insertBefore = proxy.Handle;
                    }
                    catch (Exception ex)
                    {
                        KryptonExceptionHandler.CaptureException(ex);
                    }
                }
            }

            UpdateActiveTab();
        }
        finally
        {
            _syncing = false;
        }
    }

    public void UpdateActiveTab()
    {
        KryptonNavigator? navigator = _owner.Navigator;
        if (_taskbar == null || _hostForm == null || !_hostForm.IsHandleCreated || navigator == null)
        {
            return;
        }

        KryptonPage? selected = navigator.SelectedPage;
        if (selected != null &&
            _proxies.TryGetValue(selected, out NavigatorTaskbarThumbnailProxy? proxy) &&
            proxy.IsHandleCreated)
        {
            CaptureSnapshot(selected, force: true);
            proxy.InvalidateIconicBitmaps();

            try
            {
                _taskbar.SetTabActive(proxy.Handle, _hostForm.Handle, 0);
            }
            catch (Exception ex)
            {
                KryptonExceptionHandler.CaptureException(ex);
            }
        }
    }

    public void InvalidatePage(KryptonPage page)
    {
        CaptureSnapshot(page, force: true);
        if (_proxies.TryGetValue(page, out NavigatorTaskbarThumbnailProxy? proxy))
        {
            proxy.UpdateCaption();
            proxy.InvalidateIconicBitmaps();
        }
    }

    public void OnProxyActivated(KryptonPage page)
    {
        KryptonNavigator? navigator = _owner.Navigator;
        if (_disposed || navigator == null || navigator.IsDisposed || !navigator.Pages.Contains(page))
        {
            return;
        }

        // Defer past the proxy's activation so restoring the host form does not fight Shell focus.
        if (navigator.IsHandleCreated)
        {
            navigator.BeginInvoke(new Action(() => ApplyProxyActivation(page)));
        }
        else
        {
            ApplyProxyActivation(page);
        }
    }

    private void ApplyProxyActivation(KryptonPage page)
    {
        KryptonNavigator? navigator = _owner.Navigator;
        if (_disposed || navigator == null || navigator.IsDisposed || !navigator.Pages.Contains(page))
        {
            return;
        }

        if (navigator.SelectedPage != page)
        {
            navigator.SelectedPage = page;
        }

        Form? form = _hostForm ?? navigator.FindForm();
        if (form != null && form.IsHandleCreated)
        {
            if (form.WindowState == FormWindowState.Minimized)
            {
                form.WindowState = FormWindowState.Normal;
            }

            form.Activate();
            form.BringToFront();
        }

        UpdateActiveTab();
    }

    public bool OnProxyCloseRequested(KryptonPage page)
    {
        KryptonNavigator? navigator = _owner.Navigator;
        if (_disposed || !_owner.AllowCloseFromThumbnail || navigator == null)
        {
            return false;
        }

        if (navigator.Pages.Contains(page))
        {
            navigator.PerformCloseAction(page);
        }

        return true;
    }

    public void ProvideIconicThumbnail(NavigatorTaskbarThumbnailProxy proxy, Size maxSize, bool livePreview)
    {
        if (!proxy.IsHandleCreated || maxSize.Width <= 0 || maxSize.Height <= 0)
        {
            return;
        }

        using Bitmap bitmap = CreatePageBitmap(proxy.Page, maxSize, livePreview);
        // DWM requires a 32-bit HBITMAP and rejects thumbnails larger than maxSize.
        IntPtr hBitmap = bitmap.GetHbitmap();
        try
        {
            if (livePreview)
            {
                PI.Dwm.DwmSetIconicLivePreviewBitmap(proxy.Handle, hBitmap, IntPtr.Zero, PI.Dwm.DWM_SIT.DisplayFrame);
            }
            else
            {
                PI.Dwm.DwmSetIconicThumbnail(proxy.Handle, hBitmap, PI.Dwm.DWM_SIT.None);
            }
        }
        finally
        {
            PI.DeleteObject(hBitmap);
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        TearDownAll();
        ClearSnapshots();
        DetachHostForm();
        ReleaseTaskbar();
    }

    private List<KryptonPage> GetEligiblePages(KryptonNavigator navigator)
    {
        var result = new List<KryptonPage>();
        int max = _owner.MaxThumbnails;
        bool includeHidden = _owner.IncludeHiddenPages;

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

            if (page.FindForm() is { } pageForm &&
                pageForm != _hostForm &&
                pageForm.ShowInTaskbar)
            {
                continue;
            }

            result.Add(page);
            if (max > 0 && result.Count >= max)
            {
                break;
            }
        }

        return result;
    }

    private NavigatorTaskbarThumbnailProxy? CreateAndRegisterProxy(KryptonPage page)
    {
        if (_taskbar == null || _hostForm == null || !_hostForm.IsHandleCreated)
        {
            return null;
        }

        var proxy = new NavigatorTaskbarThumbnailProxy(this, page);
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

            page.AppearancePropertyChanged += OnPageAppearancePropertyChanged;
            page.Disposed += OnPageDisposed;
            page.VisibleChanged += OnPageVisibleChanged;
            page.FlagsChanged += OnPageFlagsChanged;

            _proxies[page] = proxy;
            CaptureSnapshot(page, force: true);
            proxy.InvalidateIconicBitmaps();
            return proxy;
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
            proxy.Dispose();
            return null;
        }
    }

    private void UnregisterProxy(KryptonPage page)
    {
        if (!_proxies.TryGetValue(page, out NavigatorTaskbarThumbnailProxy? proxy))
        {
            return;
        }

        _proxies.Remove(page);
        page.AppearancePropertyChanged -= OnPageAppearancePropertyChanged;
        page.Disposed -= OnPageDisposed;
        page.VisibleChanged -= OnPageVisibleChanged;
        page.FlagsChanged -= OnPageFlagsChanged;
        RemoveSnapshot(page);

        try
        {
            if (_taskbar != null && proxy.IsHandleCreated)
            {
                _taskbar.UnregisterTab(proxy.Handle);
            }
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }

        proxy.Dispose();
    }

    private void TearDownAll()
    {
        var pages = new List<KryptonPage>(_proxies.Keys);
        foreach (KryptonPage page in pages)
        {
            UnregisterProxy(page);
        }
    }

    private void AttachHostForm(Form? form)
    {
        if (ReferenceEquals(_hostForm, form))
        {
            if (_hostForm != null && _formListener == null && _hostForm.IsHandleCreated)
            {
                AttachFormListener();
            }
            return;
        }

        DetachHostForm();
        _hostForm = form;
        if (_hostForm == null)
        {
            return;
        }

        _hostForm.HandleCreated += OnHostHandleCreated;
        _hostForm.HandleDestroyed += OnHostHandleDestroyed;
        if (_hostForm.IsHandleCreated)
        {
            AttachFormListener();
            if (_hostForm.Visible)
            {
                _taskbarButtonCreated = true;
            }
        }
    }

    private void DetachHostForm()
    {
        if (_formListener != null)
        {
            _formListener.ReleaseHandle();
            _formListener = null;
        }

        if (_hostForm != null)
        {
            _hostForm.HandleCreated -= OnHostHandleCreated;
            _hostForm.HandleDestroyed -= OnHostHandleDestroyed;
            _hostForm = null;
        }

        _taskbarButtonCreated = false;
        ReleaseTaskbar();
    }

    private void AttachFormListener()
    {
        if (_hostForm == null || !_hostForm.IsHandleCreated)
        {
            return;
        }

        _formListener?.ReleaseHandle();
        _formListener = new FormTaskbarListener(this);
        _formListener.AssignHandle(_hostForm.Handle);
    }

    private void OnHostHandleCreated(object? sender, EventArgs e)
    {
        AttachFormListener();
        if (_hostForm is { Visible: true })
        {
            _taskbarButtonCreated = true;
        }
        Sync();
    }

    private void OnHostHandleDestroyed(object? sender, EventArgs e)
    {
        _taskbarButtonCreated = false;
        _formListener?.ReleaseHandle();
        _formListener = null;
        TearDownAll();
        ReleaseTaskbar();
    }

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
            _taskbar = (PI.ITaskbarList3)new PI.TaskbarList();
            _taskbar.HrInit();
        }
        catch (Exception ex)
        {
            _taskbar = null;
            KryptonExceptionHandler.CaptureException(ex);
        }
    }

    private void ReleaseTaskbar()
    {
        if (_taskbar != null && Marshal.IsComObject(_taskbar))
        {
            Marshal.ReleaseComObject(_taskbar);
        }

        _taskbar = null;
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
        if (sender is KryptonPage { Visible: true } page)
        {
            CaptureSnapshot(page, force: true);
            if (_proxies.TryGetValue(page, out NavigatorTaskbarThumbnailProxy? proxy))
            {
                proxy.InvalidateIconicBitmaps();
            }
        }

        Sync();
    }

    private void OnPageFlagsChanged(object? sender, KryptonPageFlagsEventArgs e)
    {
        if ((e.Flags & KryptonPageFlags.AllowTaskbarThumbnail) != 0)
        {
            Sync();
        }
    }

    private Bitmap CreatePageBitmap(KryptonPage page, Size maxSize, bool livePreview)
    {
        var args = new QueryTaskbarThumbnailEventArgs(page, maxSize, livePreview);
        _owner.RaiseQueryThumbnail(args);

        if (args.Thumbnail != null)
        {
            return FitBitmap(args.Thumbnail, maxSize);
        }

        // Prefer a live capture of the visible page; otherwise reuse the last snapshot.
        CaptureSnapshot(page, force: page.Visible);
        if (_snapshots.TryGetValue(page, out Bitmap? snapshot) && snapshot != null)
        {
            return FitBitmap(snapshot, maxSize);
        }

        return CreatePlaceholderBitmap(page, maxSize);
    }

    private void CaptureSnapshot(KryptonPage page, bool force)
    {
        if (page.IsDisposed)
        {
            return;
        }

        if (!force && _snapshots.ContainsKey(page))
        {
            return;
        }

        // Hidden/non-selected pages usually cannot be captured meaningfully.
        if (!page.IsHandleCreated || page.Width <= 0 || page.Height <= 0)
        {
            return;
        }

        if (!page.Visible && _owner.Navigator?.SelectedPage != page)
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
    }

    private static Bitmap? TryCaptureControl(Control control)
    {
        try
        {
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
        private readonly NavigatorTaskbarThumbnailManager _manager;

        public FormTaskbarListener(NavigatorTaskbarThumbnailManager manager) =>
            _manager = manager;

        protected override void WndProc(ref Message m)
        {
            if (s_taskbarButtonCreatedMsg != 0 && m.Msg == (int)s_taskbarButtonCreatedMsg)
            {
                _manager.OnTaskbarButtonCreated();
            }

            base.WndProc(ref m);
        }
    }
}
