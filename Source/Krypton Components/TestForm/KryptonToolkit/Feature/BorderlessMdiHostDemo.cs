#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Issue #2922: MDI parent that opens a <see cref="FormBorderStyle.None"/> child with
/// <see cref="DockStyle.Fill"/>. The child must appear without a system title-bar flash,
/// and <see cref="Form.MdiChildActivate"/> must still fire.
/// </summary>
public partial class BorderlessMdiHostDemo : KryptonForm
{
    private int _childCounter;
    private int _activateCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="BorderlessMdiHostDemo"/> class.
    /// </summary>
    public BorderlessMdiHostDemo()
    {
        InitializeComponent();
        MdiChildActivate += OnHostMdiChildActivate;
        RefreshActivateStatus();
    }

    /// <summary>
    /// Number of times <see cref="Form.MdiChildActivate"/> has fired on this host.
    /// </summary>
    public int MdiChildActivateCount => _activateCount;

    /// <summary>
    /// Opens a borderless, dock-filled MDI child using the same sequence as issue #2922.
    /// </summary>
    /// <returns>The shown child form.</returns>
    public KryptonForm OpenDockFillChild()
    {
        _childCounter++;
        var child = new KryptonForm();
        child.FormBorderStyle = FormBorderStyle.None;
        child.Text = $"Borderless MDI child #{_childCounter}";
        child.ShowIcon = false;
        child.ShowInTaskbar = false;
        child.MdiParent = this;
        child.Dock = DockStyle.Fill;

        var caption = new KryptonLabel
        {
            Dock = DockStyle.Top,
            LabelStyle = LabelStyle.TitleControl,
            Values = { Text = child.Text + "  (custom title bar — no system caption)" }
        };
        var body = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Text = "Expected: this child fills the MDI client with no flash of the Windows title bar or system border on open.\r\n" +
                   "The host status line should increment MdiChildActivate (do not empty-override OnHandleCreated)."
        };
        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        panel.Controls.Add(body);
        panel.Controls.Add(caption);
        child.Controls.Add(panel);
        child.Show();
        return child;
    }

    private void BtnOpenChild_Click(object? sender, EventArgs e) => OpenDockFillChild();

    private void OnHostMdiChildActivate(object? sender, EventArgs e)
    {
        _activateCount++;
        RefreshActivateStatus();
    }

    private void RefreshActivateStatus()
    {
        int visibleChildren = 0;
        foreach (Form child in MdiChildren)
        {
            if (child.Visible)
            {
                visibleChildren++;
            }
        }

        klblStatus.Values.Text =
            $"MdiChildActivate count: {_activateCount}    Visible MDI children: {visibleChildren}";
    }
}
