#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Hidden top-level proxy window registered with ITaskbarList as a composite tab-group thumbnail.
/// Must remain WS_VISIBLE (off-screen) so thumbnail clicks deliver WM_ACTIVATE to this window.
/// </summary>
internal sealed class NavigatorTaskbarGroupThumbnailProxy : Form
{
    private readonly NavigatorTaskbarHostCoordinator _coordinator;
    private readonly string _groupId;
    private bool _iconicConfigured;
    private bool _shown;

    public NavigatorTaskbarGroupThumbnailProxy(
        NavigatorTaskbarHostCoordinator coordinator,
        string groupId,
        string caption)
    {
        _coordinator = coordinator;
        _groupId = groupId ?? string.Empty;

        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        Size = new Size(1, 1);
        Location = new Point(-32000, -32000);
        Text = caption ?? string.Empty;
    }

    public string GroupId => _groupId;

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

    public void UpdateCaption(string caption)
    {
        caption ??= string.Empty;
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
        _coordinator.OnGroupProxyActivated(this);
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.CLOSE:
                // Group thumbnails never close member pages from the flyout X.
                return;

            case PI.WM_.ACTIVATE:
                if (LowWord(m.WParam) != 0)
                {
                    _coordinator.OnGroupProxyActivated(this);
                }
                break;

            case PI.WM_.DWMSENDICONICTHUMBNAIL:
            {
                var maxWidth = HighWord(m.LParam);
                var maxHeight = LowWord(m.LParam);
                if (maxWidth > 0 && maxHeight > 0)
                {
                    _coordinator.ProvideGroupIconicThumbnail(this, new Size(maxWidth, maxHeight), false);
                }
                return;
            }

            case PI.WM_.DWMSENDICONICLIVEPREVIEWBITMAP:
                _coordinator.ProvideGroupIconicThumbnail(this, _coordinator.GetGroupLivePreviewSize(), true);
                return;
        }

        base.WndProc(ref m);
    }

    private static int LowWord(IntPtr value) => unchecked((int)((long)value & 0xFFFF));

    private static int HighWord(IntPtr value) => unchecked((int)(((long)value >> 16) & 0xFFFF));
}
