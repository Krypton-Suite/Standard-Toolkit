#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Borderless TopMost host form that owns the <see cref="VisualScreenColorPickerKryptonFlyout"/> control
/// and moves it independently of the transparent overlay to avoid erase artifacts.
/// Mouse hits pass through so the overlay can confirm or cancel the pick.
/// </summary>
internal sealed partial class VisualScreenColorPickerKryptonFlyoutForm : Form
{
    private const int WsExNoActivate = 0x08000000;
    private const int WsExToolWindow = 0x00000080;
    private const int WsExTopMost = 0x00000008;
    private const int WmNcHitTest = 0x0084;
    private const int HtTransparent = -1;

    private readonly VisualScreenColorPickerKryptonFlyout _flyout;

    internal VisualScreenColorPickerKryptonFlyoutForm(KryptonCustomPaletteBase? palette,
        KryptonScreenColorPickerColorFormat visibleFormats)
    {
        InitializeComponent();
        SetStyle(ControlStyles.Selectable, false);

        _flyout = new VisualScreenColorPickerKryptonFlyout(visibleFormats);
        _flyout.ApplyPalette(palette);
        _flyout.Dock = DockStyle.Fill;
        Controls.Add(_flyout);
        Controls.Add(_flyout.ReadoutPanel);
        ClientSize = _flyout.PreferredFlyoutSize;
    }

    /// <summary>Gets the pixel size of the flyout control.</summary>
    internal Size FlyoutSize => ClientSize;

    internal void UpdateSample(Bitmap screenshot, Point samplePoint, Color color, int magnifierSize, int zoom)
    {
        Size nextSize = VisualScreenColorPickerKryptonFlyout.CalculateSize(magnifierSize, zoom, _flyout.VisibleFormats);
        _flyout.UpdateSample(screenshot, samplePoint, color, magnifierSize, zoom);
        if (ClientSize != nextSize)
        {
            ClientSize = nextSize;
        }
    }

    protected override bool ShowWithoutActivation => true;

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= WsExNoActivate | WsExToolWindow | WsExTopMost;
            return cp;
        }
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WmNcHitTest)
        {
            // Hits pass through to the overlay so left-click still samples.
            m.Result = (IntPtr)HtTransparent;
            return;
        }

        base.WndProc(ref m);
    }
}
