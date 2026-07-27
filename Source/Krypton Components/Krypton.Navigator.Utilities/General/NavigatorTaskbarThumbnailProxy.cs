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
/// Hidden top-level proxy window registered with ITaskbarList3 as a tab thumbnail for a <see cref="KryptonPage"/>.
/// Must remain WS_VISIBLE (off-screen) so thumbnail clicks deliver WM_ACTIVATE to this window.
/// </summary>
internal sealed class NavigatorTaskbarThumbnailProxy : Form
{
    private readonly NavigatorTaskbarThumbnailManager _manager;
    private readonly KryptonPage _page;
    private bool _iconicConfigured;
    private bool _shown;

    public NavigatorTaskbarThumbnailProxy(NavigatorTaskbarThumbnailManager manager, KryptonPage page)
    {
        _manager = manager;
        _page = page;

        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        Size = new Size(1, 1);
        Location = new Point(-32000, -32000);
        Text = GetPageCaption(page);
    }

    public KryptonPage Page => _page;

    /// <summary>
    /// Avoid stealing focus when the proxy is first shown off-screen.
    /// </summary>
    protected override bool ShowWithoutActivation => true;

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            // WS_EX_TOOLWINDOW keeps the proxy out of Alt+Tab while still allowing DWM thumbnails.
            cp.ExStyle |= unchecked((int)0x00000080);
            return cp;
        }
    }

    /// <summary>
    /// Ensure the proxy is WS_VISIBLE so the taskbar can activate it when its thumbnail is clicked.
    /// </summary>
    /// <param name="owner">Host form that owns the navigator.</param>
    public void ShowProxy(Form? owner)
    {
        if (_shown && IsHandleCreated && Visible)
        {
            return;
        }

        if (owner != null && !owner.IsDisposed && owner.IsHandleCreated)
        {
            Owner = owner;
        }

        // Show() with ShowWithoutActivation keeps the window visible but off-screen.
        Show();
        _shown = true;
        EnsureIconicAttributes();
    }

    public void EnsureIconicAttributes()
    {
        if (_iconicConfigured || !IsHandleCreated)
        {
            return;
        }

        var enabled = 1;
        PI.Dwm.DwmSetWindowAttribute(Handle, PI.Dwm.DWMWINDOWATTRIBUTE.ForceIconicRepresentation,
            ref enabled, sizeof(int));
        PI.Dwm.DwmSetWindowAttribute(Handle, PI.Dwm.DWMWINDOWATTRIBUTE.HasIconicBitmap,
            ref enabled, sizeof(int));
        _iconicConfigured = true;
    }

    public void InvalidateIconicBitmaps()
    {
        if (IsHandleCreated)
        {
            PI.Dwm.DwmInvalidateIconicBitmaps(Handle);
        }
    }

    public void UpdateCaption()
    {
        var caption = GetPageCaption(_page);
        if (!string.Equals(Text, caption, StringComparison.Ordinal))
        {
            Text = caption;
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        EnsureIconicAttributes();
    }

    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        // Thumbnail click activates this proxy; switch the real navigator page on the UI thread after activation settles.
        _manager.OnProxyActivated(_page);
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.CLOSE:
                if (_manager.OnProxyCloseRequested(_page))
                {
                    return;
                }
                break;

            case PI.WM_.DWMSENDICONICTHUMBNAIL:
            {
                // HIWORD = max width, LOWORD = max height (see WM_DWMSENDICONICTHUMBNAIL docs).
                var maxWidth = HighWord(m.LParam);
                var maxHeight = LowWord(m.LParam);
                if (maxWidth > 0 && maxHeight > 0)
                {
                    _manager.ProvideIconicThumbnail(this, new Size(maxWidth, maxHeight), false);
                }
                return;
            }

            case PI.WM_.DWMSENDICONICLIVEPREVIEWBITMAP:
                _manager.ProvideIconicThumbnail(this, GetLivePreviewSize(), true);
                return;
        }

        base.WndProc(ref m);
    }

    private Size GetLivePreviewSize()
    {
        if (_page.IsHandleCreated && _page.Width > 0 && _page.Height > 0)
        {
            return _page.Size;
        }

        Form? form = _manager.HostForm;
        if (form != null && form.ClientSize.Width > 0 && form.ClientSize.Height > 0)
        {
            return form.ClientSize;
        }

        return new Size(200, 150);
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

    private static int LowWord(IntPtr value) => unchecked((int)((long)value & 0xFFFF));

    private static int HighWord(IntPtr value) => unchecked((int)(((long)value >> 16) & 0xFFFF));
}
