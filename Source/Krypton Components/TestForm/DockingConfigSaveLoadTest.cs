#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved. 
 *  
 */
#endregion

using Krypton.Docking;
using Krypton.Navigator;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive demo form to test SaveConfigToArray and LoadConfigFromArray functionality,
/// specifically testing the fix for issue #2516 where pages docked to workspace edges
/// were not being saved correctly.
/// </summary>
public partial class DockingConfigSaveLoadTest : KryptonForm
{
    private byte[]? _savedConfig;
    private int _pageCounter;

    public DockingConfigSaveLoadTest()
    {
        InitializeComponent();
        InitializeDocking();
        UpdateStatus("Ready. Create pages and dock them to test save/load functionality.");
    }

    private void InitializeDocking()
    {
        // Manage control for docking - this automatically sets up all four edges (Left, Right, Top, Bottom)
        var dockingControl = kryptonDockingManager1.ManageControl("Control", kryptonPanel1);

        // Manage workspace for filler pages
        kryptonDockingManager1.ManageWorkspace("Workspace", kryptonDockableWorkspace1);

        // Manage floating window capability
        kryptonDockingManager1.ManageFloating("Floating", this);

        // Edges are automatically available when you manage a control
        // We don't need to create empty dockspaces - AddDockspace will create them when needed
    }

    private KryptonPage CreatePage(string name, Color backColor)
    {
        var page = new KryptonPage
        {
            Name = $"Page_{_pageCounter++}",
            Text = name,
            TextTitle = name,
            TextDescription = $"This is {name}",
            UniqueName = $"Unique_{name}_{Guid.NewGuid():N}",
            MinimumSize = new Size(200, 200)
        };

        page.SetFlags(KryptonPageFlags.AllowConfigSave | KryptonPageFlags.DockingAllowDocked |
                      KryptonPageFlags.DockingAllowFloating | KryptonPageFlags.DockingAllowAutoHidden);

        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            BackColor = backColor,
            StateCommon = {  Color1 = backColor } 
        };

        var label = new KryptonLabel
        {
            Text = name,
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel,
            StateCommon = { LongText = { Font = new Font("Segoe UI", 14F, FontStyle.Bold), TextH = PaletteRelativeAlign.Center, TextV = PaletteRelativeAlign.Center } }
        };

        panel.Controls.Add(label);
        page.Controls.Add(panel);
        page.Size = new Size(400, 300);

        return page;
    }

    private void BtnCreateChartPage_Click(object? sender, EventArgs e)
    {
        var page = CreatePage("Chart", Color.LightBlue);
        kryptonDockingManager1.AddToWorkspace("Workspace", new[] { page });
        UpdateStatus($"Created Chart page. Current page count: {GetTotalPageCount()}");
    }

    private void BtnCreateLoggingPage_Click(object? sender, EventArgs e)
    {
        var page = CreatePage("Logging", Color.LightGreen);
        kryptonDockingManager1.AddToWorkspace("Workspace", new[] { page });
        UpdateStatus($"Created Logging page. Current page count: {GetTotalPageCount()}");
    }

    private void BtnCreateDeviceLogPage_Click(object? sender, EventArgs e)
    {
        var page = CreatePage("DeviceLog", Color.LightYellow);
        kryptonDockingManager1.AddToWorkspace("Workspace", new[] { page });
        UpdateStatus($"Created DeviceLog page. Current page count: {GetTotalPageCount()}");
    }

    private void BtnCreateGenericPage_Click(object? sender, EventArgs e)
    {
        var page = CreatePage($"Page{_pageCounter}", Color.LightCoral);
        kryptonDockingManager1.AddToWorkspace("Workspace", new[] { page });
        UpdateStatus($"Created generic page. Current page count: {GetTotalPageCount()}");
    }

    private void BtnDockToLeft_Click(object? sender, EventArgs e)
    {
        var page = CreatePage("LeftDocked", Color.LightPink);
        try
        {
            kryptonDockingManager1.AddDockspace("Control", DockingEdge.Left, new[] { page });
            UpdateStatus($"Docked page to LEFT edge. Current page count: {GetTotalPageCount()}");
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error docking to left: {ex.Message}");
        }
    }

    private void BtnDockToRight_Click(object? sender, EventArgs e)
    {
        var page = CreatePage("RightDocked", Color.LightSalmon);
        try
        {
            kryptonDockingManager1.AddDockspace("Control", DockingEdge.Right, new[] { page });
            UpdateStatus($"Docked page to RIGHT edge. Current page count: {GetTotalPageCount()}");
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error docking to right: {ex.Message}");
        }
    }

    private void BtnDockToTop_Click(object? sender, EventArgs e)
    {
        var page = CreatePage("TopDocked", Color.Lavender);
        try
        {
            kryptonDockingManager1.AddDockspace("Control", DockingEdge.Top, new[] { page });
            UpdateStatus($"Docked page to TOP edge. Current page count: {GetTotalPageCount()}");
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error docking to top: {ex.Message}");
        }
    }

    private void BtnDockToBottom_Click(object? sender, EventArgs e)
    {
        var page = CreatePage("BottomDocked", Color.PaleTurquoise);
        try
        {
            kryptonDockingManager1.AddDockspace("Control", DockingEdge.Bottom, new[] { page });
            UpdateStatus($"Docked page to BOTTOM edge. Current page count: {GetTotalPageCount()}");
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error docking to bottom: {ex.Message}");
        }
    }

    private void BtnSaveConfig_Click(object? sender, EventArgs e)
    {
        try
        {
            var pageCountBefore = GetTotalPageCount();
            _savedConfig = kryptonDockingManager1.SaveConfigToArray();
            var pageCountAfter = GetTotalPageCount();

            UpdateStatus(pageCountBefore == pageCountAfter
                ? $"Configuration saved successfully! {pageCountBefore} pages saved. Size: {_savedConfig.Length} bytes"
                : $"WARNING: Page count changed during save! Before: {pageCountBefore}, After: {pageCountAfter}");

            btnLoadConfig.Enabled = _savedConfig != null && _savedConfig.Length > 0;
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error saving configuration: {ex.Message}");
            btnLoadConfig.Enabled = false;
        }
    }

    private void BtnLoadConfig_Click(object? sender, EventArgs e)
    {
        if (_savedConfig == null || _savedConfig.Length == 0)
        {
            UpdateStatus("No saved configuration available!");
            return;
        }

        try
        {
            var pageCountBefore = GetTotalPageCount();

            // Clear existing pages
            var allPages = kryptonDockingManager1.Pages.ToArray();
            foreach (var page in allPages)
            {
                kryptonDockingManager1.RemovePage(page, false);
            }

            // Load configuration
            kryptonDockingManager1.LoadConfigFromArray(_savedConfig);
            var pageCountAfter = GetTotalPageCount();

            if (pageCountAfter == 0)
            {
                UpdateStatus($"ERROR: Configuration loaded but NO pages found! Expected pages from saved config.");
            }
            else
            {
                UpdateStatus($"Configuration loaded successfully! Pages before: {pageCountBefore}, Pages after: {pageCountAfter}");

                // Report which pages were loaded
                var loadedPageNames = string.Join(", ", kryptonDockingManager1.Pages.Select(p => p.TextTitle));
                UpdateStatus($"Loaded pages: {loadedPageNames}");
            }
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error loading configuration: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private void BtnClearAll_Click(object? sender, EventArgs e)
    {
        try
        {
            var pages = kryptonDockingManager1.Pages.ToArray();
            foreach (var page in pages)
            {
                kryptonDockingManager1.RemovePage(page, true);
            }
            _savedConfig = null;
            btnLoadConfig.Enabled = false;
            _pageCounter = 0;
            UpdateStatus("All pages cleared. Ready for new test.");
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error clearing pages: {ex.Message}");
        }
    }

    private void BtnTestIssue2516_Click(object? sender, EventArgs e)
    {
        // This replicates the exact scenario from issue #2516
        UpdateStatus("Testing Issue #2516 scenario...");

        try
        {
            // Clear everything first
            BtnClearAll_Click(sender, e);

            // Create Chart page
            var chartPage = CreatePage("Chart", Color.LightBlue);
            kryptonDockingManager1.AddToWorkspace("Workspace", new[] { chartPage });

            // Create Logging page  
            var loggingPage = CreatePage("Logging", Color.LightGreen);
            kryptonDockingManager1.AddToWorkspace("Workspace", new[] { loggingPage });

            // Create DeviceLog page and dock to RIGHT edge of Logging window
            // This is the problematic scenario - docking directly to an edge
            var deviceLogPage = CreatePage("DeviceLog", Color.LightYellow);

            // First, we need to find the workspace and dock to its edge
            // For this test, we'll dock to the right edge of the control
            kryptonDockingManager1.AddDockspace("Control", DockingEdge.Right, new[] { deviceLogPage });

            UpdateStatus("Issue #2516 scenario setup complete. Pages: Chart, Logging, DeviceLog (right edge). Now try Save/Load.");

            // Automatically save after a short delay to test
            var timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                timer.Dispose();
                BtnSaveConfig_Click(sender, e);
            };
            timer.Start();
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error in Issue #2516 test: {ex.Message}");
        }
    }

    private int GetTotalPageCount() => kryptonDockingManager1.Pages.Count(p => !(p is KryptonStorePage));

    private void UpdateStatus(string message)
    {
        var timestamp = DateTime.Now.ToString("HH:mm:ss");
        kryptonTextBoxStatus.AppendText($"[{timestamp}] {message}\r\n");
        kryptonTextBoxStatus.SelectionStart = kryptonTextBoxStatus.Text.Length;
        kryptonTextBoxStatus.ScrollToCaret();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);

        // Clean up
        if (_savedConfig != null)
        {
            _savedConfig = null;
        }
    }
}

