#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2017 - 2026. All rights reserved.
 */
#endregion

using System.Drawing;
using System.Windows.Forms;
using Krypton.Ribbon;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive demo for Issue #2921: Ribbon + MDI form.
/// Verifies: no double ribbon tabs when opening/closing maximized MDI children,
/// close/minimize/maximize and QAT click areas aligned with visuals.
/// </summary>
public partial class RibbonMdiDemo : KryptonForm
{
    private KryptonRibbon? _ribbon;
    private int _childCounter;

    public RibbonMdiDemo()
    {
        InitializeComponent();
        Load += OnLoad;
    }

    private void OnLoad(object? sender, System.EventArgs e)
    {
        InitializeRibbon();
    }

    private void InitializeRibbon()
    {
        _ribbon = new KryptonRibbon
        {
            Name = "kryptonRibbon",
            Dock = DockStyle.Top,
            QATLocation = QATLocation.Above
        };
        _ribbon!.RibbonFileAppButton.AppButtonVisible = true;

        // File tab (minimal for demo)
        var fileTab = new KryptonRibbonTab { Text = "File" };
        var fileGroup = new KryptonRibbonGroup { TextLine1 = "File" };
        var newBtn = new KryptonRibbonGroupButton { TextLine1 = "New" };
        newBtn.Click += (_, _) => AddChild(resizable: true, maximized: false);
        var triple1 = new KryptonRibbonGroupTriple();
        triple1!.Items.Add(newBtn);
        fileGroup.Items.Add(triple1);
        fileTab.Groups.Add(fileGroup);

        // Home tab
        var homeTab = new KryptonRibbonTab { Text = "Home" };
        var homeGroup = new KryptonRibbonGroup { TextLine1 = "Actions" };
        var openMaxBtn = new KryptonRibbonGroupButton { TextLine1 = "Open Maximized" };
        openMaxBtn.Click += (_, _) => AddChild(resizable: true, maximized: true);
        var triple2 = new KryptonRibbonGroupTriple();
        triple2!.Items.Add(openMaxBtn);
        homeGroup.Items.Add(triple2);
        homeTab.Groups.Add(homeGroup);

        _ribbon!.RibbonTabs.Add(fileTab);
        _ribbon.RibbonTabs.Add(homeTab);
        _ribbon.SelectedTab = fileTab;

        // Ribbon must be first control (at top) for caption integration.
        Controls.Add(_ribbon);
        Controls.SetChildIndex(_ribbon, 0);
    }

    private void BtnAddResizable_Click(object? sender, System.EventArgs e) =>
        AddChild(resizable: true, maximized: false);

    private void BtnAddNoResize_Click(object? sender, System.EventArgs e) =>
        AddChild(resizable: false, maximized: false);

    private void BtnOpenMaximized_Click(object? sender, System.EventArgs e) =>
        AddChild(resizable: true, maximized: true);

    private void BtnCloseAll_Click(object? sender, System.EventArgs e)
    {
        foreach (Form child in MdiChildren)
        {
            child.Close();
        }
    }

    private void BtnTileHorizontal_Click(object? sender, System.EventArgs e) => LayoutMdi(MdiLayout.TileHorizontal);

    private void BtnTileVertical_Click(object? sender, System.EventArgs e) => LayoutMdi(MdiLayout.TileVertical);

    private void BtnCascade_Click(object? sender, System.EventArgs e) => LayoutMdi(MdiLayout.Cascade);

    private void AddChild(bool resizable, bool maximized)
    {
        _childCounter++;
        var title = resizable ? "Resize Test Window" : "No Resize";
        var child = new RibbonMdiChildForm(resizable)
        {
            Text = $"{title} #{_childCounter}",
            MdiParent = this,
            WindowState = maximized ? FormWindowState.Maximized : FormWindowState.Normal
        };
        child.Show();
    }
}
