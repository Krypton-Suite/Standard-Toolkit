#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

using Krypton.Navigator;
using Krypton.Navigator.Utilities;
using Krypton.Workspace;

namespace NavigatorTabGroupLayoutSmoke;

/// <summary>
/// Headless-ish smoke: save → mutate → load round-trip for navigator tab groups and workspace layout.
/// Avoids <c>KryptonForm</c> so the harness does not depend on VisualForm static Win32 init.
/// Exit code 0 = pass; non-zero = fail.
/// </summary>
internal static class Program
{
    [STAThread]
    private static int Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        try
        {
            RunNavigatorRoundTrip();
            RunWorkspaceRoundTrip();
            Console.WriteLine("NavigatorTabGroupLayoutSmoke: PASS");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("NavigatorTabGroupLayoutSmoke: FAIL");
            Console.Error.WriteLine(ex);
            return 1;
        }
    }

    private static void RunNavigatorRoundTrip()
    {
        using var host = new Form { Size = new Size(800, 600) };
        var navigator = new KryptonNavigator { Dock = DockStyle.Fill };
        host.Controls.Add(navigator);

        var page1 = new KryptonPage { Text = "A", UniqueName = "A" };
        var page2 = new KryptonPage { Text = "B", UniqueName = "B" };
        var page3 = new KryptonPage { Text = "C", UniqueName = "C" };
        navigator.Pages.AddRange(new[] { page1, page2, page3 });

        using var integrator = new KryptonNavigatorFormIntegrator
        {
            Navigator = navigator
        };

        NavigatorTabGroup group = integrator.CreateGroup("Work", Color.DodgerBlue, page1);
        integrator.AssignPageToGroup(page2, group.Id);
        group.Collapsed = true;

        byte[] blob = integrator.SaveLayoutToArray();
        if (blob.Length == 0)
        {
            throw new InvalidOperationException("Navigator save produced empty buffer.");
        }

        integrator.UngroupPage(page1);
        integrator.UngroupPage(page2);
        integrator.TabGroups.Clear();
        navigator.Pages.Clear();
        navigator.Pages.AddRange(new[] { page3, page2, page1 });

        integrator.LoadLayoutFromArray(blob);

        if (integrator.TabGroups.Count != 1)
        {
            throw new InvalidOperationException($"Expected 1 group after load, got {integrator.TabGroups.Count}.");
        }

        if (!string.Equals(page1.TabGroupId, group.Id, StringComparison.Ordinal)
            || !string.Equals(page2.TabGroupId, group.Id, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("TabGroupId membership was not restored.");
        }

        if (!integrator.TabGroups[group.Id]!.Collapsed)
        {
            throw new InvalidOperationException("Collapsed flag was not restored.");
        }

        if (navigator.Pages.IndexOf(page1) > navigator.Pages.IndexOf(page2))
        {
            throw new InvalidOperationException("Page order was not restored.");
        }
    }

    private static void RunWorkspaceRoundTrip()
    {
        using var host = new Form { Size = new Size(900, 600) };
        var workspace = new KryptonWorkspace { Dock = DockStyle.Fill };
        host.Controls.Add(workspace);

        var cell = new KryptonWorkspaceCell { NavigatorMode = NavigatorMode.Panel };
        var page1 = new KryptonPage { Text = "L1", UniqueName = "L1" };
        var page2 = new KryptonPage { Text = "L2", UniqueName = "L2" };
        cell.Pages.AddRange(new[] { page1, page2 });
        workspace.Root.Children!.Add(cell);
        workspace.ActiveCell = cell;

        using var integrator = new KryptonNavigatorFormIntegrator
        {
            Workspace = workspace
        };

        NavigatorTabGroup group = integrator.CreateGroup("Docs", Color.Orange, page1);
        page2.TabGroupId = group.Id;

        KryptonDocumentGroupHelper.SplitActiveCell(workspace, Orientation.Horizontal);
        byte[] blob = integrator.SaveLayoutToArray();

        integrator.TabGroups.Clear();
        page1.TabGroupId = string.Empty;
        page2.TabGroupId = string.Empty;
        workspace.ApplySingleCell();

        integrator.LoadLayoutFromArray(blob);

        if (integrator.TabGroups[group.Id] == null)
        {
            throw new InvalidOperationException("Workspace GlobalSaving did not restore NTG catalog.");
        }

        // After workspace load, pages may be remapped; resolve by UniqueName.
        KryptonPage? restored1 = FindPage(workspace, "L1");
        KryptonPage? restored2 = FindPage(workspace, "L2");
        if (restored1 == null || restored2 == null)
        {
            throw new InvalidOperationException("Workspace pages were not restored by UniqueName.");
        }

        if (string.IsNullOrEmpty(restored1.TabGroupId) && string.IsNullOrEmpty(restored2.TabGroupId))
        {
            throw new InvalidOperationException("Workspace TG attributes were not restored.");
        }
    }

    private static KryptonPage? FindPage(KryptonWorkspace workspace, string uniqueName)
    {
        for (KryptonWorkspaceCell? cell = workspace.FirstCell();
             cell != null;
             cell = workspace.NextCell(cell))
        {
            foreach (KryptonPage page in cell.Pages)
            {
                if (string.Equals(page.UniqueName, uniqueName, StringComparison.Ordinal))
                {
                    return page;
                }
            }
        }

        return null;
    }
}
