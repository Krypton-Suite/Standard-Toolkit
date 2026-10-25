#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive demo for issue #2935: Maximized MDI window form border drawn on the correct monitor.
/// When an MDI child (KryptonForm) is maximized on a secondary monitor, the form border should be drawn
/// on the same monitor as the form content, not on the primary monitor.
/// </summary>
public partial class Bug2935MdiMultiMonitorDemo : KryptonForm
{
    private int _childCounter;

    public Bug2935MdiMultiMonitorDemo()
    {
        InitializeComponent();
        UpdateInstructions();
        UpdateMoveToSecondaryButton();
    }

    private void UpdateInstructions()
    {
        bool multiMonitor = Screen.AllScreens.Length > 1;
        lblInstructions.Text = multiMonitor
            ? "Issue #2935 – MDI multi-monitor border demo\r\n\r\n" +
              "1. Click \"Move to secondary monitor\" to place this window on the second display (or drag it there).\r\n" +
              "2. Click \"New MDI Child\" to open a KryptonForm MDI child.\r\n" +
              "3. Maximize the MDI child (maximize button or double-click title bar).\r\n" +
              "4. Verify: The form border/frame is drawn on the SAME monitor as the form content (secondary), not on the primary monitor.\r\n\r\n" +
              "Expected: No border drawn on the primary (left) monitor when the maximized child is on the secondary (right) monitor."
            : "Issue #2935 – MDI multi-monitor border demo\r\n\r\n" +
              "This demo requires two monitors to reproduce the bug.\r\n" +
              "With one monitor, you can still open MDI children and maximize them to test basic behaviour.\r\n\r\n" +
              "Steps with two monitors:\r\n" +
              "1. Move this window to the second display.\r\n" +
              "2. Open an MDI child and maximize it.\r\n" +
              "3. The border should appear on the same monitor as the form.";
    }

    private void UpdateMoveToSecondaryButton()
    {
        btnMoveToSecondary.Enabled = Screen.AllScreens.Length > 1;
    }

    private void BtnNewMdiChild_Click(object? sender, EventArgs e)
    {
        _childCounter++;
        var child = new KryptonForm
        {
            Text = $"MDI Child #{_childCounter}",
            MdiParent = this,
            WindowState = FormWindowState.Normal
        };

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        var label = new KryptonLabel
        {
            Text = $"Child #{_childCounter}\r\n\r\nMaximize this window (max button or double-click title bar).\r\nOn a second monitor, the border should stay on this monitor.",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel,
            StateCommon = { LongText = { TextH = PaletteRelativeAlign.Center, TextV = PaletteRelativeAlign.Center } }
        };
        panel.Controls.Add(label);
        child.Controls.Add(panel);

        child.Show();
    }

    private void BtnMoveToSecondary_Click(object? sender, EventArgs e)
    {
        Screen[] screens = Screen.AllScreens;
        if (screens.Length < 2)
        {
            return;
        }

        Screen secondary = screens[0].Primary ? screens[1] : screens[0];
        Rectangle area = secondary.WorkingArea;
        Location = new Point(area.Left + 20, area.Top + 20);
        Size = new Size(Math.Min(800, area.Width - 40), Math.Min(600, area.Height - 40));
    }

    private void BtnTileHorizontal_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.TileHorizontal);

    private void BtnTileVertical_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.TileVertical);

    private void BtnCascade_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.Cascade);

    private void BtnArrangeIcons_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.ArrangeIcons);
}
