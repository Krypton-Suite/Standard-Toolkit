#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Docking;
using Krypton.Navigator;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Demo for Issue #4129 Part 2: Windows 11 Snap Group eligibility for peer KryptonForm windows
/// and optional docking floats that show on the taskbar.
/// </summary>
public partial class Feature4129SnapGroupsDemo : KryptonForm
{
    private KryptonForm? _peerForm;
    private KryptonDockingFloating? _floating;
    private int _pageCounter;

    public Feature4129SnapGroupsDemo()
    {
        InitializeComponent();
    }

    private void Feature4129SnapGroupsDemo_Load(object? sender, EventArgs e)
    {
        // Process AUMID is already set in Program.Main; keep JumpList.AppId aligned for this form.
        JumpList.AppId = "KryptonToolkit.JumpListTest";

        kryptonDockingManager1.ManageControl("Control", kryptonPanelContent);
        kryptonDockingManager1.ManageWorkspace("Workspace", kryptonDockableWorkspace1);
        _floating = kryptonDockingManager1.ManageFloating("Floating", this);
        if (_floating != null)
        {
            _floating.ShowFloatingWindowsInTaskbar = kchkFloatsInTaskbar.Checked;
        }

        AddDocument("Main document", Color.AliceBlue);
        UpdateStatus();
    }

    private void kbtnOpenPeer_Click(object? sender, EventArgs e)
    {
        if (_peerForm == null || _peerForm.IsDisposed)
        {
            _peerForm = new KryptonForm
            {
                Text = "Peer window (Snap Group)",
                Size = new Size(480, 360),
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X + Width + 20, Location.Y),
                // Unowned peer with ShowInTaskbar so Windows can form a Snap Group.
                ShowInTaskbar = true,
                Owner = null
            };
            _peerForm.JumpList.AppId = JumpList.AppId;
            var label = new KryptonLabel
            {
                Dock = DockStyle.Fill,
                Values =
                {
                    Text = "Peer KryptonForm\r\n\r\nSnap this beside the main demo window (Win+Left / Win+Right),\r\nthen hover either taskbar button for the OS Snap Group preview."
                },
                LabelStyle = LabelStyle.NormalPanel,
                StateCommon =
                {
                    ShortText =
                    {
                        TextH = PaletteRelativeAlign.Center,
                        TextV = PaletteRelativeAlign.Center
                    }
                }
            };
            _peerForm.Controls.Add(label);
            _peerForm.FormClosed += (_, _) =>
            {
                _peerForm = null;
                UpdateStatus();
            };
        }

        _peerForm.Show();
        _peerForm.Activate();
        UpdateStatus();
    }

    private void kchkFloatsInTaskbar_CheckedChanged(object? sender, EventArgs e)
    {
        if (_floating != null)
        {
            _floating.ShowFloatingWindowsInTaskbar = kchkFloatsInTaskbar.Checked;
        }

        UpdateStatus();
    }

    private void kbtnAddAndFloat_Click(object? sender, EventArgs e)
    {
        KryptonPage page = AddDocument($"Float candidate {_pageCounter}", Color.Honeydew);
        kryptonDockingManager1.MakeFloatingRequest(page.UniqueName);
        UpdateStatus();
    }

    private KryptonPage AddDocument(string title, Color color)
    {
        _pageCounter++;
        var page = new KryptonPage
        {
            Text = title,
            TextTitle = title,
            UniqueName = $"SnapDemo_{_pageCounter}_{Guid.NewGuid():N}",
            MinimumSize = new Size(160, 120)
        };
        page.SetFlags(KryptonPageFlags.AllowConfigSave | KryptonPageFlags.DockingAllowDocked |
                      KryptonPageFlags.DockingAllowFloating | KryptonPageFlags.DockingAllowAutoHidden |
                      KryptonPageFlags.DockingAllowWorkspace);

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        panel.StateCommon.Color1 = color;
        panel.Controls.Add(new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Values = { Text = title + "\r\n\r\nRight-click tab → Float, or use Add + float." },
            LabelStyle = LabelStyle.NormalPanel,
            StateCommon =
            {
                ShortText =
                {
                    TextH = PaletteRelativeAlign.Center,
                    TextV = PaletteRelativeAlign.Center
                }
            }
        });
        page.Controls.Add(panel);
        kryptonDockingManager1.AddToWorkspace("Workspace", new[] { page });
        return page;
    }

    private void UpdateStatus()
    {
        klblStatus.Values.Text =
            $"Peer open: {_peerForm != null && !_peerForm.IsDisposed} | " +
            $"Floats in taskbar (new floats): {_floating?.ShowFloatingWindowsInTaskbar == true} | " +
            $"AUMID: {JumpList.AppId}";
    }
}
