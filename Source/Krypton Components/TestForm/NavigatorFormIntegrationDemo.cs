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
    private byte[]? _savedLayout;

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
        kryptonNavigatorFormIntegrator1.TabGroupChanged += (_, _) => UpdateStatus();
        _savedLayout = null;

        // Seed a sample browser-style group so caption headers are visible immediately.
        NavigatorTabGroup sample = kryptonNavigatorFormIntegrator1.CreateGroup("Work", Color.DodgerBlue);
        if (kryptonNavigator1.Pages.Count >= 2)
        {
            kryptonNavigatorFormIntegrator1.AssignPageToGroup(kryptonNavigator1.Pages[0], sample.Id);
            kryptonNavigatorFormIntegrator1.AssignPageToGroup(kryptonNavigator1.Pages[1], sample.Id);
        }

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

    private void NudWash_ValueChanged(object? sender, EventArgs e)
    {
        NavigatorTabGroupAppearance appearance = kryptonNavigatorFormIntegrator1.TabGroupAppearance;
        int wash = (int)nudWash.Value;
        appearance.HeaderWashAlpha = wash;
        appearance.CollapsedHeaderWashAlpha = Math.Min(255, wash + 30);
        UpdateStatus();
    }

    private void ChkMemberBorder_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.TabGroupAppearance.ShowMemberBorder = chkMemberBorder.Checked;
        UpdateStatus();
    }

    private void ChkMemberUnderline_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.TabGroupAppearance.ShowMemberUnderline = chkMemberUnderline.Checked;
        UpdateStatus();
    }

    private void ChkHeaderAccent_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonNavigatorFormIntegrator1.TabGroupAppearance.ShowHeaderAccent = chkHeaderAccent.Checked;
        UpdateStatus();
    }

    private void BtnGroupSelected_Click(object? sender, EventArgs e)
    {
        KryptonPage? page = kryptonNavigator1.SelectedPage;
        if (page == null)
        {
            return;
        }

        kryptonNavigatorFormIntegrator1.CreateGroup(assignPage: page);
        UpdateStatus();
    }

    private void BtnSaveLayout_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = @"Navigator layout (*.xml)|*.xml|All files (*.*)|*.*",
            DefaultExt = "xml",
            FileName = @"navigator-tab-groups.xml",
            Title = @"Save tab/group layout"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        kryptonNavigatorFormIntegrator1.SaveLayoutToFile(dialog.FileName);
        _savedLayout = kryptonNavigatorFormIntegrator1.SaveLayoutToArray();
        UpdateStatus();
    }

    private void BtnLoadLayout_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = @"Navigator layout (*.xml)|*.xml|All files (*.*)|*.*",
            DefaultExt = "xml",
            Title = @"Load tab/group layout"
        };
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            kryptonNavigatorFormIntegrator1.LoadLayoutFromFile(dialog.FileName);
            _savedLayout = kryptonNavigatorFormIntegrator1.SaveLayoutToArray();
            UpdateStatus();
            return;
        }

        if (_savedLayout == null || _savedLayout.Length == 0)
        {
            UpdateStatus();
            return;
        }

        kryptonNavigatorFormIntegrator1.LoadLayoutFromArray(_savedLayout);
        UpdateStatus();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.T))
        {
            BtnAddPage_Click(this, EventArgs.Empty);
            return true;
        }

        if (keyData == (Keys.Control | Keys.W))
        {
            KryptonPage? page = kryptonNavigator1.SelectedPage;
            if (page != null && kryptonNavigator1.Pages.Count > 1)
            {
                kryptonNavigator1.Pages.Remove(page);
                UpdateStatus();
                return true;
            }
        }

        if (keyData == (Keys.Control | Keys.G))
        {
            BtnGroupSelected_Click(this, EventArgs.Empty);
            return true;
        }

        if (keyData == (Keys.Control | Keys.Shift | Keys.G))
        {
            KryptonPage? page = kryptonNavigator1.SelectedPage;
            if (page != null && !string.IsNullOrEmpty(page.TabGroupId))
            {
                kryptonNavigatorFormIntegrator1.ToggleGroupCollapsed(page.TabGroupId);
                UpdateStatus();
                return true;
            }
        }

        return base.ProcessCmdKey(ref msg, keyData);
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
            $"CloseEmpty={kryptonNavigatorFormIntegrator1.CloseEmptySourceWindowAfterLastTabMoved}  " +
            $"Groups={kryptonNavigatorFormIntegrator1.TabGroups.Count}  " +
            $"PageGroup={(kryptonNavigator1.SelectedPage?.TabGroupId ?? "(none)")}  " +
            $"SavedLayout={(_savedLayout == null ? "none" : $"{_savedLayout.Length} bytes")}";
    }
}
