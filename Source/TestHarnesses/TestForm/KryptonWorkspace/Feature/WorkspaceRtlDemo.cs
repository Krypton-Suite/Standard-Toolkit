#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Workspace;

namespace TestForm;

/// <summary>
/// Demonstrates logical RTL packing for <see cref="KryptonWorkspace"/> (Issue #2383).
/// </summary>
public partial class WorkspaceRtlDemo : KryptonForm
{
    internal const string UniqueCellA = @"RtlCellA";
    internal const string UniqueCellTop = @"RtlCellTop";
    internal const string UniqueCellBottom = @"RtlCellBottom";
    internal const string UniqueCellC = @"RtlCellC";

    private byte[]? _savedLayout;

    public WorkspaceRtlDemo()
    {
        InitializeComponent();
        BuildWorkspace();
        UpdateStatus();
    }

    private void BuildWorkspace()
    {
        kryptonWorkspace.SuspendWorkspaceLayout();
        kryptonWorkspace.Root.Children!.Clear();
        kryptonWorkspace.Root.Orientation = Orientation.Horizontal;

        var cellA = CreateCell(UniqueCellA, @"A");
        var cellTop = CreateCell(UniqueCellTop, @"Top");
        var cellBottom = CreateCell(UniqueCellBottom, @"Bottom");
        var cellC = CreateCell(UniqueCellC, @"C");

        var stacked = new KryptonWorkspaceSequence(Orientation.Vertical);
        stacked.Children!.Add(cellTop);
        stacked.Children.Add(cellBottom);

        kryptonWorkspace.Root.Children.Add(cellA);
        kryptonWorkspace.Root.Children.Add(stacked);
        kryptonWorkspace.Root.Children.Add(cellC);
        kryptonWorkspace.ResumeWorkspaceLayout();
    }

    private static KryptonWorkspaceCell CreateCell(string uniqueName, string title)
    {
        var cell = new KryptonWorkspaceCell
        {
            UniqueName = uniqueName,
            NavigatorMode = NavigatorMode.BarTabGroup,
            StarSize = @"50*,50*"
        };

        var page = new KryptonPage
        {
            Text = title,
            TextTitle = title,
            UniqueName = uniqueName + @"Page"
        };
        page.Controls.Add(new KryptonLabel
        {
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel,
            Values =
            {
                Text = title + @" — drag this page to a cell edge or workspace edge."
            }
        });
        cell.Pages.Add(page);

        var spare = new KryptonPage
        {
            Text = title + @" 2",
            TextTitle = title + @" 2",
            UniqueName = uniqueName + @"Page2"
        };
        spare.Controls.Add(new KryptonLabel
        {
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.NormalControl,
            Values = { Text = @"Second page in " + title + @"." }
        });
        cell.Pages.Add(spare);
        return cell;
    }

    private void OnRtlCheckedChanged(object? sender, EventArgs e)
    {
        var rtl = chkRtl.Checked;
        RightToLeft = rtl ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = rtl;
        kryptonWorkspace.PerformLayout();
        UpdateStatus();
    }

    private void OnSaveLayout(object? sender, EventArgs e)
    {
        _savedLayout = kryptonWorkspace.SaveLayoutToArray();
        UpdateStatus(@"Saved XML layout (" + _savedLayout.Length + @" bytes).");
    }

    private void OnLoadLayout(object? sender, EventArgs e)
    {
        if (_savedLayout == null)
        {
            UpdateStatus(@"Nothing saved yet — click Save layout first.");
            return;
        }

        kryptonWorkspace.LoadLayoutFromArray(_savedLayout);
        kryptonWorkspace.PerformLayout();
        UpdateStatus(@"Loaded XML layout. Children order is unchanged.");
    }

    private void OnMaximizeToggle(object? sender, EventArgs e)
    {
        if (kryptonWorkspace.MaximizedCell != null)
        {
            kryptonWorkspace.MaximizedCell = null;
        }
        else
        {
            kryptonWorkspace.MaximizedCell = kryptonWorkspace.ActiveCell;
        }

        kryptonWorkspace.PerformLayout();
        UpdateStatus();
    }

    private void UpdateStatus(string? extra = null)
    {
        KryptonWorkspaceCell? cellA = FindCell(UniqueCellA);
        KryptonWorkspaceCell? cellC = FindCell(UniqueCellC);
        KryptonWorkspaceCell? cellTop = FindCell(UniqueCellTop);
        KryptonWorkspaceCell? cellBottom = FindCell(UniqueCellBottom);

        var order = DescribeChildren(kryptonWorkspace.Root);
        var visual = @"A.Left=" + (cellA?.Left ?? -1) + @", C.Left=" + (cellC?.Left ?? -1) +
                     @", Top.Y=" + (cellTop?.Top ?? -1) + @", Bottom.Y=" + (cellBottom?.Top ?? -1);
        var flags = @"RightToLeft=" + RightToLeft + @", RightToLeftLayout=" + RightToLeftLayout +
                    @", workspace.RightToLeftLayout=" + kryptonWorkspace.RightToLeftLayout;
        kwlblStatus.Text = flags + Environment.NewLine + @"Children: " + order + Environment.NewLine +
                           visual + (string.IsNullOrEmpty(extra) ? string.Empty : Environment.NewLine + extra);
    }

    private KryptonWorkspaceCell? FindCell(string uniqueName)
    {
        KryptonWorkspaceCell? cell = kryptonWorkspace.FirstCell();
        while (cell != null)
        {
            if (cell.UniqueName == uniqueName)
            {
                return cell;
            }

            cell = kryptonWorkspace.NextCell(cell);
        }

        return null;
    }

    private static string DescribeChildren(KryptonWorkspaceSequence sequence)
    {
        var parts = new List<string>();
        foreach (Component child in sequence.Children!)
        {
            switch (child)
            {
                case KryptonWorkspaceCell cell:
                    parts.Add(cell.UniqueName);
                    break;
                case KryptonWorkspaceSequence nested:
                    parts.Add(@"[" + DescribeChildren(nested) + @"]");
                    break;
            }
        }

        return string.Join(@", ", parts);
    }
}
