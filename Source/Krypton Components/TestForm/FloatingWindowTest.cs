#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2024 - 2026. All rights reserved. 
 *  
 */
#endregion

using Krypton.Docking;
using Krypton.Navigator;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive test form for floating window bug fix (Issue #2721).
/// Tests that floating windows display content correctly after SetInheritedControlOverride is called.
/// </summary>
public partial class FloatingWindowTest : KryptonForm
{
    private int _pageCounter;
    private readonly List<KryptonDockingFloatingWindow> _floatingWindows = new();

    public FloatingWindowTest()
    {
        InitializeComponent();
        InitializeDocking();
        UpdateStatus("Ready. Use buttons to test floating window functionality.");
    }

    private void InitializeDocking()
    {
        // Manage control for docking
        kryptonDockingManager1.ManageControl("Control", kryptonPanel1);

        // Manage workspace for filler pages
        kryptonDockingManager1.ManageWorkspace("Workspace", kryptonDockableWorkspace1);

        // Manage floating window capability - this is critical for the test
        kryptonDockingManager1.ManageFloating("Floating", this);
    }

    private KryptonPage CreatePage(string name, Color backColor)
    {
        var page = new KryptonPage
        {
            Name = $"Page_{_pageCounter++}",
            Text = name,
            TextTitle = name,
            TextDescription = $"This is {name} - testing floating window display",
            UniqueName = $"Unique_{name}_{Guid.NewGuid():N}",
            MinimumSize = new Size(200, 200)
        };

        page.SetFlags(KryptonPageFlags.AllowConfigSave | KryptonPageFlags.DockingAllowDocked |
                      KryptonPageFlags.DockingAllowFloating | KryptonPageFlags.DockingAllowAutoHidden);

        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            BackColor = backColor,
            StateCommon = { Color1 = backColor }
        };

        var label = new KryptonLabel
        {
            Text = $"{name}\n\nThis content should be visible in the floating window.\nIf you see this text, the fix is working!",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel,
            StateCommon = { LongText = { Font = new Font("Segoe UI", 12F, FontStyle.Regular), TextH = PaletteRelativeAlign.Center, TextV = PaletteRelativeAlign.Center } }
        };

        panel.Controls.Add(label);
        page.Controls.Add(panel);
        page.Size = new Size(400, 300);

        return page;
    }

    private void BtnTestSingleFloatingWindow_Click(object? sender, EventArgs e)
    {
        try
        {
            UpdateStatus("Testing: Create single floating window with one page...");

            var page = CreatePage("TestPage", Color.LightBlue);
            var floatingWindow = kryptonDockingManager1.AddFloatingWindow("Floating", new[] { page },
                new Point(Location.X + Width + 20, Location.Y),
                new Size(450, 350));

            _floatingWindows.Add(floatingWindow);
            floatingWindow.FloatingWindow.Show();

            // Verify the floating window has content
            VerifyFloatingWindowContent(floatingWindow, "Single floating window test");

            UpdateStatus("✓ Single floating window created successfully. Verify content is visible.");
        }
        catch (Exception ex)
        {
            UpdateStatus($"✗ Error creating single floating window: {ex.Message}");
        }
    }

    private void BtnTestMultipleFloatingWindows_Click(object? sender, EventArgs e)
    {
        try
        {
            UpdateStatus("Testing: Create multiple floating windows...");

            var page1 = CreatePage("Page1", Color.LightBlue);
            var page2 = CreatePage("Page2", Color.LightGreen);
            var page3 = CreatePage("Page3", Color.LightYellow);

            var floatingWindow1 = kryptonDockingManager1.AddFloatingWindow("Floating", new[] { page1 },
                new Point(Location.X + Width + 20, Location.Y),
                new Size(450, 350));

            var floatingWindow2 = kryptonDockingManager1.AddFloatingWindow("Floating", new[] { page2 },
                new Point(Location.X + Width + 20, Location.Y + 370),
                new Size(450, 350));

            var floatingWindow3 = kryptonDockingManager1.AddFloatingWindow("Floating", new[] { page3 },
                new Point(Location.X + Width + 20, Location.Y + 740),
                new Size(450, 350));

            _floatingWindows.Add(floatingWindow1);
            _floatingWindows.Add(floatingWindow2);
            _floatingWindows.Add(floatingWindow3);

            floatingWindow1.FloatingWindow.Show();
            floatingWindow2.FloatingWindow.Show();
            floatingWindow3.FloatingWindow.Show();

            // Verify all floating windows have content
            VerifyFloatingWindowContent(floatingWindow1, "Multiple floating windows test - Window 1");
            VerifyFloatingWindowContent(floatingWindow2, "Multiple floating windows test - Window 2");
            VerifyFloatingWindowContent(floatingWindow3, "Multiple floating windows test - Window 3");

            UpdateStatus("✓ Multiple floating windows created successfully. Verify all windows show content.");
        }
        catch (Exception ex)
        {
            UpdateStatus($"✗ Error creating multiple floating windows: {ex.Message}");
        }
    }

    private void BtnTestFloatingWindowWithMultiplePages_Click(object? sender, EventArgs e)
    {
        try
        {
            UpdateStatus("Testing: Create floating window with multiple pages...");

            var page1 = CreatePage("PageA", Color.LightBlue);
            var page2 = CreatePage("PageB", Color.LightGreen);
            var page3 = CreatePage("PageC", Color.LightYellow);

            var floatingWindow = kryptonDockingManager1.AddFloatingWindow("Floating", new[] { page1, page2, page3 },
                new Point(Location.X + Width + 20, Location.Y),
                new Size(500, 400));

            _floatingWindows.Add(floatingWindow);
            floatingWindow.FloatingWindow.Show();

            VerifyFloatingWindowContent(floatingWindow, "Multiple pages in floating window test");

            UpdateStatus("✓ Floating window with multiple pages created. Verify tabs are visible and content displays.");
        }
        catch (Exception ex)
        {
            UpdateStatus($"✗ Error creating floating window with multiple pages: {ex.Message}");
        }
    }

    private void BtnTestIssue2721Scenario_Click(object? sender, EventArgs e)
    {
        try
        {
            UpdateStatus("Testing: Issue #2721 scenario - replicate the bug conditions...");

            // First create a page in workspace
            var workspacePage = CreatePage("WorkspacePage", Color.LightPink);
            kryptonDockingManager1.AddToWorkspace("Workspace", new[] { workspacePage });

            // Then create a floating window - this is the scenario from the bug report
            var floatingPage = CreatePage("FloatingPage", Color.LightCoral);
            var floatingWindow = kryptonDockingManager1.AddFloatingWindow("Floating", new[] { floatingPage },
                new Point(Location.X + Width + 20, Location.Y),
                new Size(450, 350));

            _floatingWindows.Add(floatingWindow);
            floatingWindow.FloatingWindow.Show();

            VerifyFloatingWindowContent(floatingWindow, "Issue #2721 scenario test");

            UpdateStatus("✓ Issue #2721 scenario test completed. The floating window should NOT be empty.");
        }
        catch (Exception ex)
        {
            UpdateStatus($"✗ Error in Issue #2721 scenario test: {ex.Message}");
        }
    }

    private void BtnVerifyFloatspaceControl_Click(object? sender, EventArgs e)
    {
        try
        {
            UpdateStatus("Testing: Verify FloatspaceControl is in Controls collection...");

            if (_floatingWindows.Count == 0)
            {
                UpdateStatus("✗ No floating windows created yet. Create one first.");
                return;
            }

            var verifiedCount = 0;
            foreach (var floatingWindowElement in _floatingWindows)
            {
                var floatingWindow = floatingWindowElement.FloatingWindow;

                // Check that FloatspaceControl exists
                if (floatingWindow.FloatspaceControl == null)
                {
                    UpdateStatus($"✗ FloatingWindow {verifiedCount + 1}: FloatspaceControl is NULL");
                    continue;
                }

                // Check that FloatspaceControl is in the Controls collection
                if (!floatingWindow.Controls.Contains(floatingWindow.FloatspaceControl))
                {
                    UpdateStatus($"✗ FloatingWindow {verifiedCount + 1}: FloatspaceControl is NOT in Controls collection");
                }
                else
                {
                    UpdateStatus($"✓ FloatingWindow {verifiedCount + 1}: FloatspaceControl is correctly in Controls collection");
                    verifiedCount++;
                }

                // Additional verification: check that FloatspaceControl is visible
                if (floatingWindow.FloatspaceControl.Visible)
                {
                    UpdateStatus($"✓ FloatingWindow {verifiedCount}: FloatspaceControl is visible");
                }
                else
                {
                    UpdateStatus($"⚠ FloatingWindow {verifiedCount}: FloatspaceControl exists but is not visible");
                }
            }

            UpdateStatus($"Verification complete: {verifiedCount}/{_floatingWindows.Count} floating windows verified.");
        }
        catch (Exception ex)
        {
            UpdateStatus($"✗ Error verifying FloatspaceControl: {ex.Message}");
        }
    }

    private void BtnClearAll_Click(object? sender, EventArgs e)
    {
        try
        {
            UpdateStatus("Clearing all floating windows and pages...");

            // Close all floating windows
            foreach (var floatingWindowElement in _floatingWindows)
            {
                if (!floatingWindowElement.FloatingWindow.IsDisposed)
                {
                    floatingWindowElement.FloatingWindow.Close();
                }
            }
            _floatingWindows.Clear();

            // Clear all pages from docking manager
            var pages = kryptonDockingManager1.Pages.ToArray();
            foreach (var page in pages)
            {
                kryptonDockingManager1.RemovePage(page, true);
            }

            _pageCounter = 0;
            UpdateStatus("All floating windows and pages cleared. Ready for new tests.");
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error clearing: {ex.Message}");
        }
    }

    private void VerifyFloatingWindowContent(KryptonDockingFloatingWindow floatingWindowElement, string testName)
    {
        try
        {
            var floatingWindow = floatingWindowElement.FloatingWindow;

            // Verify FloatspaceControl exists
            if (floatingWindow.FloatspaceControl == null)
            {
                UpdateStatus($"✗ {testName}: FloatspaceControl is NULL");
                return;
            }

            // Verify FloatspaceControl is in Controls collection (this is what the fix addresses)
            if (!floatingWindow.Controls.Contains(floatingWindow.FloatspaceControl))
            {
                UpdateStatus($"✗ {testName}: FloatspaceControl is NOT in Controls collection - BUG DETECTED!");
                return;
            }

            // Verify the floating window has cells
            if (floatingWindow.FloatspaceControl.CellCount == 0)
            {
                UpdateStatus($"⚠ {testName}: FloatspaceControl exists but has no cells");
                return;
            }

            UpdateStatus($"✓ {testName}: FloatspaceControl verified in Controls collection with {floatingWindow.FloatspaceControl.CellCount} cell(s)");
        }
        catch (Exception ex)
        {
            UpdateStatus($"✗ {testName}: Verification error - {ex.Message}");
        }
    }

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

        // Clean up floating windows
        foreach (var floatingWindowElement in _floatingWindows)
        {
            if (!floatingWindowElement.FloatingWindow.IsDisposed)
            {
                floatingWindowElement.FloatingWindow.Close();
            }
        }
        _floatingWindows.Clear();
    }
}
