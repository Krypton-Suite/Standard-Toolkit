#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Navigator.Utilities;

namespace TestForm;

/// <summary>
/// Demo for Issue #925 / #927: KryptonNavigator integrated with KryptonForm
/// (browser / Explorer-style tabbed chrome and form min/max/close button specs).
/// </summary>
public partial class NavigatorFormIntegrationDemo : KryptonForm
{
    private int _pageCounter = 3;

    public NavigatorFormIntegrationDemo()
    {
        InitializeComponent();

        kryptonNavigatorFormIntegrator1.Form = this;
        kryptonNavigatorFormIntegrator1.Navigator = kryptonNavigator1;
        kryptonNavigatorFormIntegrator1.Mode = NavigatorFormIntegrationMode.CaptionIntegrated;
        kryptonNavigatorFormIntegrator1.SyncFormTitle = false;
        kryptonNavigatorFormIntegrator1.Enabled = true;
        kryptonNavigatorFormIntegrator1.AllowTearOut = chkTearOutEnabled.Checked;
        kryptonNavigatorFormIntegrator1.CloseEmptySourceWindowAfterLastTabMoved = chkCloseEmptySourceWindow.Checked;
        kryptonNavigatorFormIntegrator1.ShowNewTabButton = chkShowNewTabButton.Checked;
        kryptonNavigatorFormIntegrator1.NewTabButtonClick += (_, _) => BtnAddPage_Click(this, EventArgs.Empty);
        kryptonNavigatorFormIntegrator1.TabContextMenuOpening += OnTabContextMenuOpening;

        cmbMode.SelectedIndex = 0; // CaptionIntegrated
        chkSyncTitle.Checked = false;
        UpdateStatus();
        kryptonNavigatorFormIntegrator1.IntegrationChanged += (_, _) => UpdateStatus();
        kryptonNavigator1.SelectedPageChanged += (_, _) => UpdateStatus();
    }

    private void CmbMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.Mode = cmbMode.SelectedIndex switch
        {
            1 => NavigatorFormIntegrationMode.ClientChrome,
            2 => NavigatorFormIntegrationMode.CaptionAdjacent,
            _ => NavigatorFormIntegrationMode.CaptionIntegrated
        };
        UpdateStatus();
    }

    private void ChkEnabled_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.Enabled = chkEnabled.Checked;
        UpdateStatus();
    }

    private void ChkSyncTitle_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.SyncFormTitle = chkSyncTitle.Checked;
        UpdateStatus();
    }

    private void ChkTearOutEnabled_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.AllowTearOut = chkTearOutEnabled.Checked;
        UpdateStatus();
    }

    private void ChkCloseEmptySourceWindow_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.CloseEmptySourceWindowAfterLastTabMoved = chkCloseEmptySourceWindow.Checked;
        UpdateStatus();
    }

    private void ChkShowNewTabButton_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.ShowNewTabButton = chkShowNewTabButton.Checked;
        UpdateStatus();
    }

    private void BtnAddPage_Click(object? sender, EventArgs e)
    {
        _pageCounter++;
        var page = new KryptonPage
        {
            Text = $"Document {_pageCounter}",
            TextTitle = $"Document {_pageCounter}",
            UniqueName = $"Document{_pageCounter}"
        };
        var label = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Text = $"Content for Document {_pageCounter}\r\n\r\nUse the mode combo to switch ClientChrome vs CaptionAdjacent.\r\nIn ClientChrome, use the navigator form buttons to minimize, maximize, or close.",
            LabelStyle = LabelStyle.NormalControl
        };
        page.Controls.Add(label);
        kryptonNavigator1.Pages.Add(page);
        kryptonNavigator1.SelectedPage = page;
    }

    private void OnTabContextMenuOpening(object? sender, NavigatorTabContextMenuEventArgs e)
    {
        if (e.ContextMenuStrip.Items["DemoNewTab"] == null)
        {
            e.ContextMenuStrip.Items.Insert(0, new ToolStripMenuItem(KryptonManager.Strings.NavigatorIntegrationStrings.NewTab, null, (_, _) => BtnAddPage_Click(this, EventArgs.Empty))
            {
                Name = "DemoNewTab",
                ShortcutKeys = Keys.Control | Keys.T
            });
            e.ContextMenuStrip.Items.Insert(1, new ToolStripSeparator
            {
                Name = "DemoNewTabSeparator"
            });
        }
    }

    private void UpdateStatus()
    {
        klblStatus.Text =
            $"Integrated={kryptonNavigatorFormIntegrator1.IsIntegrated}  " +
            $"Mode={kryptonNavigatorFormIntegrator1.Mode}  " +
            $"Owner={(kryptonNavigator1.Owner == null ? "null" : "this")}  " +
            $"ControlKryptonFormFeatures={kryptonNavigator1.ControlKryptonFormFeatures}  " +
            $"Form.ControlBox={ControlBox}  " +
            $"Selected={(kryptonNavigator1.SelectedPage?.Text ?? "(none)")}  " +
            $"TearOut={kryptonNavigatorFormIntegrator1.AllowTearOut}  " +
            $"CloseEmpty={kryptonNavigatorFormIntegrator1.CloseEmptySourceWindowAfterLastTabMoved}";
    }
}
