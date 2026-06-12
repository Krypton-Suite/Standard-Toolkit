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
/// Comprehensive demo for docking undock/redock behaviour (Issue #2933).
/// Demonstrates that when a document is undocked then redocked into the workspace,
/// no empty floating window is left behind.
/// </summary>
public partial class DockingRedockDemo : KryptonForm
{
    private int _pageCounter;

    public DockingRedockDemo()
    {
        InitializeComponent();
        InitializeDocking();
        UpdateStatus("Ready. Add a document, then undock (Float) and redock by dragging back.");
    }

    private void InitializeDocking()
    {
        kryptonDockingManager1.ManageControl("Control", kryptonPanel1);
        kryptonDockingManager1.ManageWorkspace("Workspace", kryptonDockableWorkspace1);
        kryptonDockingManager1.ManageFloating("Floating", this);
    }

    private KryptonPage CreatePage(string name, Color backColor)
    {
        var page = new KryptonPage
        {
            Name = $"Page_{_pageCounter++}",
            Text = name,
            TextTitle = name,
            TextDescription = $"Document: {name}",
            UniqueName = $"Unique_{name}_{Guid.NewGuid():N}",
            MinimumSize = new Size(200, 200)
        };

        page.SetFlags(KryptonPageFlags.AllowConfigSave | KryptonPageFlags.DockingAllowDocked |
                      KryptonPageFlags.DockingAllowFloating | KryptonPageFlags.DockingAllowAutoHidden |
                      KryptonPageFlags.DockingAllowWorkspace);

        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            BackColor = backColor,
            StateCommon = { Color1 = backColor }
        };

        var label = new KryptonLabel
        {
            Text = $"{name}\n\n• Right-click the tab → Float to undock.\n• Drag the floating window back onto the workspace to redock.\n• After redocking, no empty window should remain (Issue #2933).",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel,
            StateCommon = { LongText = { Font = new Font("Segoe UI", 10F), TextH = PaletteRelativeAlign.Center, TextV = PaletteRelativeAlign.Center } }
        };

        panel.Controls.Add(label);
        page.Controls.Add(panel);
        page.Size = new Size(400, 300);

        return page;
    }

    private void BtnAddDocument_Click(object? sender, EventArgs e)
    {
        var names = new[] { "Document A", "Document B", "Document C", "Log", "Output" };
        var colors = new[] { Color.LightBlue, Color.LightGreen, Color.LightYellow, Color.Lavender, Color.PaleTurquoise };
        var index = Math.Min(_pageCounter % names.Length, colors.Length - 1);
        var page = CreatePage(names[index], colors[index]);
        kryptonDockingManager1.AddToWorkspace("Workspace", new[] { page });
        UpdateStatus($"Added '{names[index]}'. Undock via tab context menu (Float), then drag back to redock.");
    }

    private void BtnAddMultiple_Click(object? sender, EventArgs e)
    {
        var page1 = CreatePage("Document 1", Color.LightBlue);
        var page2 = CreatePage("Document 2", Color.LightGreen);
        var page3 = CreatePage("Document 3", Color.LightYellow);
        kryptonDockingManager1.AddToWorkspace("Workspace", new[] { page1, page2, page3 });
        UpdateStatus("Added 3 documents. Undock any tab (Float), then redock by dragging; no empty window should remain.");
    }

    private void BtnClearAll_Click(object? sender, EventArgs e)
    {
        var pages = kryptonDockingManager1.Pages.ToArray();
        foreach (var page in pages)
        {
            kryptonDockingManager1.RemovePage(page, true);
        }
        _pageCounter = 0;
        UpdateStatus("All documents cleared. Add documents and try undock/redock again.");
    }

    private void UpdateStatus(string message)
    {
        var timestamp = DateTime.Now.ToString("HH:mm:ss");
        kryptonTextBoxStatus.AppendText($"[{timestamp}] {message}\r\n");
        kryptonTextBoxStatus.SelectionStart = kryptonTextBoxStatus.Text.Length;
        kryptonTextBoxStatus.ScrollToCaret();
    }
}
