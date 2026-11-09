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
/// Also exercises issue #4685 <see cref="KryptonManager.MdiPanelStyle"/> /
/// <see cref="KryptonForm.MdiPanelStyle"/> for the MDI client background.
/// </summary>
public partial class BorderlessMdiHostDemo : KryptonForm
{
    private int _childCounter;
    private int _activateCount;
    private readonly PaletteBackStyle _originalManagerMdiPanelStyle;
    private bool _suppressManagerMdiPanelStyleApply;
    private bool _suppressFormMdiPanelStyleApply;

    /// <summary>
    /// Initializes a new instance of the <see cref="BorderlessMdiHostDemo"/> class.
    /// </summary>
    public BorderlessMdiHostDemo()
    {
        _originalManagerMdiPanelStyle = KryptonManager.MdiPanelStyle;
        InitializeComponent();
        MdiChildActivate += OnHostMdiChildActivate;
        RefreshActivateStatus();

        _suppressManagerMdiPanelStyleApply = true;
        kcmbManagerMdiPanelStyle.SelectedIndex = IndexForPanelStyle(KryptonManager.MdiPanelStyle);
        _suppressManagerMdiPanelStyleApply = false;

        _suppressFormMdiPanelStyleApply = true;
        kcmbFormMdiPanelStyle.SelectedIndex = 0; // Inherit
        ResetMdiPanelStyle();
        _suppressFormMdiPanelStyleApply = false;
    }

    /// <summary>
    /// Number of times <see cref="Form.MdiChildActivate"/> has fired on this host.
    /// </summary>
    public int MdiChildActivateCount => _activateCount;

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        KryptonManager.MdiPanelStyle = _originalManagerMdiPanelStyle;
        base.OnFormClosed(e);
    }

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
                   "The host status line should increment MdiChildActivate (do not empty-override OnHandleCreated).\r\n" +
                   "MDI client colour follows Manager/Form MdiPanelStyle when UseKryptonColorsOnForm is on (#4685)."
        };
        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        panel.Controls.Add(body);
        panel.Controls.Add(caption);
        child.Controls.Add(panel);
        child.Show();
        return child;
    }

    private void BtnOpenChild_Click(object? sender, EventArgs e) => OpenDockFillChild();

    private void kcmbManagerMdiPanelStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_suppressManagerMdiPanelStyleApply || kcmbManagerMdiPanelStyle.SelectedIndex < 0)
        {
            return;
        }

        KryptonManager.MdiPanelStyle = PanelStyleForIndex(kcmbManagerMdiPanelStyle.SelectedIndex);
        RefreshActivateStatus();
    }

    private void kcmbFormMdiPanelStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_suppressFormMdiPanelStyleApply || kcmbFormMdiPanelStyle.SelectedIndex < 0)
        {
            return;
        }

        if (kcmbFormMdiPanelStyle.SelectedIndex == 0)
        {
            ResetMdiPanelStyle();
        }
        else
        {
            MdiPanelStyle = PanelStyleForIndex(kcmbFormMdiPanelStyle.SelectedIndex - 1);
        }

        RefreshActivateStatus();
    }

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

        string local = kcmbFormMdiPanelStyle.SelectedIndex > 0 ? MdiPanelStyle.ToString() : "Inherit";
        klblStatus.Values.Text =
            $"MdiChildActivate: {_activateCount}  Children: {visibleChildren}  " +
            $"Mgr.MdiPanelStyle={KryptonManager.MdiPanelStyle}  Form.MdiPanelStyle={local} (eff {MdiPanelStyle})";
    }

    private static PaletteBackStyle PanelStyleForIndex(int index) =>
        index switch
        {
            0 => PaletteBackStyle.PanelClient,
            1 => PaletteBackStyle.PanelAlternate,
            2 => PaletteBackStyle.PanelCustom1,
            _ => PaletteBackStyle.PanelAlternate
        };

    private static int IndexForPanelStyle(PaletteBackStyle style) =>
        style switch
        {
            PaletteBackStyle.PanelClient => 0,
            PaletteBackStyle.PanelAlternate => 1,
            PaletteBackStyle.PanelCustom1 => 2,
            _ => 1
        };
}
