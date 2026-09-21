#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Navigator.Utilities;
using Krypton.Workspace;

namespace TestForm;

/// <summary>
/// Demo for IDE-style document groups (Workspace cells) and multi-strip CaptionIntegrated chrome.
/// </summary>
public partial class DocumentGroupsDemo : KryptonForm
{
    private int _pageCounter;
    private byte[]? _savedLayout;

    public DocumentGroupsDemo()
    {
        InitializeComponent();
        SeedWorkspace();

        kryptonNavigatorFormIntegrator1.Form = this;
        kryptonNavigatorFormIntegrator1.Workspace = kryptonWorkspace1;
        kryptonNavigatorFormIntegrator1.Mode = NavigatorFormIntegrationMode.CaptionIntegrated;
        kryptonNavigatorFormIntegrator1.Enabled = true;
        kryptonNavigatorFormIntegrator1.ShowNewTabButton = true;
        kryptonNavigatorFormIntegrator1.NewTabButtonClick += (_, _) => AddPageToActiveCell();
        UpdateStatus();
    }

    private void SeedWorkspace()
    {
        kryptonWorkspace1.Root.Children!.Clear();
        var cell = new KryptonWorkspaceCell
        {
            NavigatorMode = NavigatorMode.Panel
        };
        cell.Pages.Add(CreatePage("Left 1"));
        cell.Pages.Add(CreatePage("Left 2"));
        kryptonWorkspace1.Root.Children.Add(cell);
        kryptonWorkspace1.ActiveCell = cell;
    }

    private KryptonPage CreatePage(string title)
    {
        _pageCounter++;
        var page = new KryptonPage
        {
            Text = title,
            TextTitle = title,
            UniqueName = $"DocGroup{_pageCounter}"
        };
        page.Controls.Add(new KryptonLabel
        {
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.NormalControl,
            Values =
            {
                Text = $"{title}\r\n\r\nUse Split / Move page to exercise KryptonDocumentGroupHelper.\r\nCaption strips map 1:1 to workspace cells."
            }
        });
        return page;
    }

    private void AddPageToActiveCell()
    {
        KryptonWorkspaceCell? cell = kryptonWorkspace1.ActiveCell ?? kryptonWorkspace1.FirstCell();
        if (cell == null)
        {
            return;
        }

        KryptonPage page = CreatePage($"Document {_pageCounter + 1}");
        cell.Pages.Add(page);
        cell.SelectedPage = page;
        UpdateStatus();
    }

    private void BtnSplitHorizontal_Click(object? sender, EventArgs e)
    {
        KryptonDocumentGroupHelper.SplitActiveCell(kryptonWorkspace1, Orientation.Horizontal);
        UpdateStatus();
    }

    private void BtnSplitVertical_Click(object? sender, EventArgs e)
    {
        KryptonDocumentGroupHelper.SplitActiveCell(kryptonWorkspace1, Orientation.Vertical);
        UpdateStatus();
    }

    private void BtnMovePage_Click(object? sender, EventArgs e)
    {
        KryptonPage? page = kryptonWorkspace1.ActivePage;
        if (page == null)
        {
            return;
        }

        KryptonDocumentGroupHelper.MovePageToNewCell(kryptonWorkspace1, page, Orientation.Horizontal);
        UpdateStatus();
    }

    private void BtnCloseEmpty_Click(object? sender, EventArgs e)
    {
        KryptonDocumentGroupHelper.CloseEmptyCells(kryptonWorkspace1);
        UpdateStatus();
    }

    private void BtnSaveLayout_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = @"Workspace layout (*.xml)|*.xml|All files (*.*)|*.*",
            DefaultExt = "xml",
            FileName = @"document-groups.xml",
            Title = @"Save document group layout"
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
            Filter = @"Workspace layout (*.xml)|*.xml|All files (*.*)|*.*",
            DefaultExt = "xml",
            Title = @"Load document group layout"
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
            AddPageToActiveCell();
            return true;
        }

        if (keyData == (Keys.Control | Keys.Shift | Keys.Right))
        {
            KryptonDocumentGroupHelper.SplitActiveCell(kryptonWorkspace1, Orientation.Horizontal);
            UpdateStatus();
            return true;
        }

        if (keyData == (Keys.Control | Keys.Shift | Keys.Down))
        {
            KryptonDocumentGroupHelper.SplitActiveCell(kryptonWorkspace1, Orientation.Vertical);
            UpdateStatus();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void BtnAddPage_Click(object? sender, EventArgs e) => AddPageToActiveCell();

    private void UpdateStatus()
    {
        klblStatus.Text =
            $"Cells={(CountCells())}  ActiveCell={(kryptonWorkspace1.ActiveCell?.Name ?? "(none)")}  " +
            $"ActivePage={(kryptonWorkspace1.ActivePage?.Text ?? "(none)")}  " +
            $"Integrated={kryptonNavigatorFormIntegrator1.IsIntegrated}  " +
            $"Groups={kryptonNavigatorFormIntegrator1.TabGroups.Count}  " +
            $"SavedLayout={(_savedLayout == null ? "none" : $"{_savedLayout.Length} bytes")}";
    }

    private int CountCells()
    {
        var cells = new List<KryptonWorkspaceCell>();
        Collect(kryptonWorkspace1.Root, cells);
        return cells.Count;
    }

    private static void Collect(KryptonWorkspaceSequence sequence, List<KryptonWorkspaceCell> cells)
    {
        if (sequence.Children == null)
        {
            return;
        }

        foreach (Component child in sequence.Children)
        {
            switch (child)
            {
                case KryptonWorkspaceCell cell:
                    cells.Add(cell);
                    break;
                case KryptonWorkspaceSequence nested:
                    Collect(nested, cells);
                    break;
            }
        }
    }
}
