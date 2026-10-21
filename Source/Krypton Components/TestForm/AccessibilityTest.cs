#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive accessibility demo for UIA Provider implementation (Issue #762).
/// Demonstrates and tests all 10 controls that wrap standard Windows Forms controls.
/// </summary>
public partial class AccessibilityTest : KryptonForm
{
    #region Instance Fields
    // Test Controls (created dynamically, not in Designer)
    private KryptonTextBox _testTextBox;
    private KryptonRichTextBox _testRichTextBox;
    private KryptonComboBox _testComboBox;
    private KryptonListBox _testListBox;
    private KryptonCheckedListBox _testCheckedListBox;
    private KryptonMaskedTextBox _testMaskedTextBox;
    private KryptonNumericUpDown _testNumericUpDown;
    private KryptonDomainUpDown _testDomainUpDown;
    private KryptonLinkWrapLabel _testLinkWrapLabel;
    private KryptonListView _testListView;
    #endregion

    #region Identity
    public AccessibilityTest()
    {
        InitializeComponent();
        SetupAccessibilityProperties();
        DisplayFrameworkInfo();
        // Subscribe to Load event to create controls after form is fully initialized
        Load += AccessibilityTest_Load;
    }

    private void AccessibilityTest_Load(object? sender, EventArgs e)
    {
        // Create and add the test controls panel to Panel1
        // Do this in Load event to avoid recursion issues during construction
        try
        {
            // Suspend layout to prevent recursion during control addition
            _splitContainer.Panel1.SuspendLayout();
            _splitContainer.Panel1.Controls.Clear();
            var controlsPanel = CreateControlsPanel();
            _splitContainer.Panel1.Controls.Add(controlsPanel);
            _splitContainer.Panel1.ResumeLayout(true);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error creating controls panel: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion

    #region Implementation

    private Panel CreateControlsPanel()
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(10)
        };

        var tableLayout = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 10,
            AutoSize = true,
            Padding = new Padding(5),
            Location = new Point(0, 0)
        };

        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

        // Suspend layout to prevent recursion while adding controls
        tableLayout.SuspendLayout();
        panel.SuspendLayout();

        try
        {
            int row = 0;
            AddControlToTable(tableLayout, "KryptonTextBox", CreateTextBox(), ref row);
            AddControlToTable(tableLayout, "KryptonRichTextBox", CreateRichTextBox(), ref row);
            AddControlToTable(tableLayout, "KryptonComboBox", CreateComboBox(), ref row);
            AddControlToTable(tableLayout, "KryptonListBox", CreateListBox(), ref row);
            AddControlToTable(tableLayout, "KryptonCheckedListBox", CreateCheckedListBox(), ref row);
            AddControlToTable(tableLayout, "KryptonMaskedTextBox", CreateMaskedTextBox(), ref row);
            AddControlToTable(tableLayout, "KryptonNumericUpDown", CreateNumericUpDown(), ref row);
            AddControlToTable(tableLayout, "KryptonDomainUpDown", CreateDomainUpDown(), ref row);
            AddControlToTable(tableLayout, "KryptonLinkWrapLabel", CreateLinkWrapLabel(), ref row);
            AddControlToTable(tableLayout, "KryptonListView", CreateListView(), ref row);

            panel.Controls.Add(tableLayout);
        }
        finally
        {
            tableLayout.ResumeLayout(false);
            panel.ResumeLayout(false);
        }

        return panel;
    }

    private Panel CreateTextInputPanel()
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(10)
        };

        var tableLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 2,
            RowCount = 3,
            AutoSize = true,
            Padding = new Padding(5)
        };

        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

        int row = 0;
        AddControlToTable(tableLayout, "KryptonTextBox", CreateTextBox(), ref row);
        AddControlToTable(tableLayout, "KryptonRichTextBox", CreateRichTextBox(), ref row);
        AddControlToTable(tableLayout, "KryptonMaskedTextBox", CreateMaskedTextBox(), ref row);

        panel.Controls.Add(tableLayout);
        return panel;
    }

    private Panel CreateSelectionPanel()
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(10)
        };

        var tableLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 2,
            RowCount = 4,
            AutoSize = true,
            Padding = new Padding(5)
        };

        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

        int row = 0;
        AddControlToTable(tableLayout, "KryptonComboBox", CreateComboBox(), ref row);
        AddControlToTable(tableLayout, "KryptonListBox", CreateListBox(), ref row);
        AddControlToTable(tableLayout, "KryptonCheckedListBox", CreateCheckedListBox(), ref row);
        AddControlToTable(tableLayout, "KryptonListView", CreateListView(), ref row);

        panel.Controls.Add(tableLayout);
        return panel;
    }

    private Panel CreateNumericPanel()
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(10)
        };

        var tableLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 2,
            RowCount = 2,
            AutoSize = true,
            Padding = new Padding(5)
        };

        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

        int row = 0;
        AddControlToTable(tableLayout, "KryptonNumericUpDown", CreateNumericUpDown(), ref row);
        AddControlToTable(tableLayout, "KryptonDomainUpDown", CreateDomainUpDown(), ref row);

        panel.Controls.Add(tableLayout);
        return panel;
    }

    private Panel CreateOtherPanel()
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(10)
        };

        var tableLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 2,
            RowCount = 1,
            AutoSize = true,
            Padding = new Padding(5)
        };

        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

        int row = 0;
        AddControlToTable(tableLayout, "KryptonLinkWrapLabel", CreateLinkWrapLabel(), ref row);

        panel.Controls.Add(tableLayout);
        return panel;
    }

    private void AddControlToTable(TableLayoutPanel table, string labelText, Control control, ref int row)
    {
        var label = new KryptonLabel
        {
            Text = labelText + ":",
            //TextAlign = ContentAlignment.MiddleLeft,
            Margin = new Padding(5)
        };
        table.Controls.Add(label, 0, row);
        table.Controls.Add(control, 1, row++);
    }

    #region Control Creation
    private KryptonTextBox CreateTextBox()
    {
        _testTextBox = new KryptonTextBox
        {
            Text = "Type your name here",
            AccessibleName = "User Name Input",
            AccessibleDescription = "Enter your full name in this text box",
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        return _testTextBox;
    }

    private KryptonRichTextBox CreateRichTextBox()
    {
        _testRichTextBox = new KryptonRichTextBox
        {
            Text = "This is a RichTextBox.\nIt supports rich text formatting.\nTry typing and formatting text!",
            AccessibleName = "Rich Text Editor",
            AccessibleDescription = "A multi-line rich text editor with formatting capabilities",
            Height = 100,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        return _testRichTextBox;
    }

    private KryptonComboBox CreateComboBox()
    {
        _testComboBox = new KryptonComboBox
        {
            AccessibleName = "Country Selection",
            AccessibleDescription = "Select your country from the dropdown list",
            DropDownStyle = ComboBoxStyle.DropDownList,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        _testComboBox.Items.AddRange(new[] { "United States", "United Kingdom", "Canada", "Australia", "Germany", "France" });
        _testComboBox.SelectedIndex = 0;
        return _testComboBox;
    }

    private KryptonListBox CreateListBox()
    {
        _testListBox = new KryptonListBox
        {
            AccessibleName = "Item List",
            AccessibleDescription = "Select one or more items from this list",
            Height = 120,
            SelectionMode = SelectionMode.MultiExtended,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        _testListBox.Items.AddRange(new[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6" });
        return _testListBox;
    }

    private KryptonCheckedListBox CreateCheckedListBox()
    {
        _testCheckedListBox = new KryptonCheckedListBox
        {
            AccessibleName = "Checklist",
            AccessibleDescription = "Select multiple items by checking the boxes",
            Height = 120,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        _testCheckedListBox.Items.AddRange(new[] { "Option A", "Option B", "Option C", "Option D", "Option E" });
        _testCheckedListBox.SetItemChecked(0, true);
        _testCheckedListBox.SetItemChecked(2, true);
        return _testCheckedListBox;
    }

    private KryptonMaskedTextBox CreateMaskedTextBox()
    {
        _testMaskedTextBox = new KryptonMaskedTextBox
        {
            Mask = "00/00/0000",
            Text = "01/01/2024",
            AccessibleName = "Date Input",
            AccessibleDescription = "Enter a date in MM/DD/YYYY format",
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        return _testMaskedTextBox;
    }

    private KryptonNumericUpDown CreateNumericUpDown()
    {
        _testNumericUpDown = new KryptonNumericUpDown
        {
            Minimum = 0,
            Maximum = 100,
            Value = 50,
            Increment = 5,
            AccessibleName = "Quantity Selector",
            AccessibleDescription = "Use the up and down arrows to adjust the quantity",
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        return _testNumericUpDown;
    }

    private KryptonDomainUpDown CreateDomainUpDown()
    {
        _testDomainUpDown = new KryptonDomainUpDown
        {
            AccessibleName = "Value Selector",
            AccessibleDescription = "Use the up and down arrows to cycle through values",
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        _testDomainUpDown.Items.AddRange(new[] { "Small", "Medium", "Large", "Extra Large" });
        _testDomainUpDown.SelectedIndex = 1;
        return _testDomainUpDown;
    }

    private KryptonLinkWrapLabel CreateLinkWrapLabel()
    {
        _testLinkWrapLabel = new KryptonLinkWrapLabel
        {
            Text = "Visit our website for more information. Click here to learn more!",
            AccessibleName = "Website Link",
            AccessibleDescription = "Click this link to visit our website",
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        _testLinkWrapLabel.LinkClicked += (s, e) =>
            MessageBox.Show("Link clicked! This demonstrates that the link is accessible and functional.",
                "Accessibility Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return _testLinkWrapLabel;
    }

    private KryptonListView CreateListView()
    {
        _testListView = new KryptonListView
        {
            AccessibleName = "Data Grid",
            AccessibleDescription = "A list view displaying data in columns",
            Height = 150,
            View = View.Details,
            FullRowSelect = true,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };
        _testListView.Columns.Add("Product", 150);
        _testListView.Columns.Add("Price", 100);
        _testListView.Columns.Add("Stock", 80);
        _testListView.Items.Add(new ListViewItem(new[] { "Widget A", "$19.99", "45" }));
        _testListView.Items.Add(new ListViewItem(new[] { "Widget B", "$29.99", "23" }));
        _testListView.Items.Add(new ListViewItem(new[] { "Widget C", "$39.99", "12" }));
        _testListView.Items.Add(new ListViewItem(new[] { "Widget D", "$49.99", "8" }));
        return _testListView;
    }
    #endregion

    private KryptonLabel CreateLabel(string text)
    {
        return new KryptonLabel
        {
            Text = text, 
            // TextAlign = ContentAlignment.MiddleLeft,
            Margin = new Padding(5)
        };
    }

        // Tab 2: Text Input Controls

    private void SetupAccessibilityProperties()
    {
        // All accessibility properties are set during control creation
    }

    private void DisplayFrameworkInfo()
    {
        var framework = Environment.Version;
        var runtime = RuntimeInformation.FrameworkDescription;
        _lblFramework.Text = $"Framework: {runtime} | .NET Version: {framework}";
    }

    private void BtnRunTests_Click(object? sender, EventArgs e)
    {
        _lblStatus.Text = "Running tests...";
        _lblStatus.StateCommon.ShortText.Color1 = Color.Orange;
        Application.DoEvents();

        var results = new StringBuilder();
        results.AppendLine("═══════════════════════════════════════════════════════════════");
        results.AppendLine("  UIA Provider Accessibility Test Results");
        results.AppendLine("  Issue #762 - Krypton Toolkit UIA Provider Implementation");
        results.AppendLine("═══════════════════════════════════════════════════════════════");
        results.AppendLine();
        results.AppendLine($"Test Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        results.AppendLine($"Framework: {RuntimeInformation.FrameworkDescription}");
        results.AppendLine($"OS Version: {Environment.OSVersion}");
        results.AppendLine();

        var testResults = new List<TestResult>();

        // Test each control
        testResults.Add(TestControl("KryptonTextBox", _testTextBox));
        testResults.Add(TestControl("KryptonRichTextBox", _testRichTextBox));
        testResults.Add(TestControl("KryptonComboBox", _testComboBox));
        testResults.Add(TestControl("KryptonListBox", _testListBox));
        testResults.Add(TestControl("KryptonCheckedListBox", _testCheckedListBox));
        testResults.Add(TestControl("KryptonMaskedTextBox", _testMaskedTextBox));
        testResults.Add(TestControl("KryptonNumericUpDown", _testNumericUpDown));
        testResults.Add(TestControl("KryptonDomainUpDown", _testDomainUpDown));
        testResults.Add(TestControl("KryptonLinkWrapLabel", _testLinkWrapLabel));
        testResults.Add(TestControl("KryptonListView", _testListView));

        // Format results
        foreach (var result in testResults)
        {
            results.AppendLine($"┌─ {result.ControlName} ─────────────────────────────────────────────");
            results.AppendLine($"│  Status: {(result.Success ? "✓ PASS" : "✗ FAIL")}");
            results.AppendLine($"│  AccessibleObject: {(result.AccessibleObjectCreated ? "✓ Created" : "✗ NULL")}");
            results.AppendLine($"│  Name: {result.Name ?? "(null)"}");
            results.AppendLine($"│  Description: {result.Description ?? "(null)"}");
            results.AppendLine($"│  Role: {result.Role}");
            results.AppendLine($"│  State: {result.State}");
            results.AppendLine($"│  Value: {result.Value ?? "(null)"}");
            results.AppendLine($"│  Child Count: {result.ChildCount}");
            results.AppendLine($"│  Navigation: {(result.NavigationWorks ? "✓" : "✗")}");
            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                results.AppendLine($"│  ERROR: {result.ErrorMessage}");
            }
            results.AppendLine("└───────────────────────────────────────────────────────────");
            results.AppendLine();
        }

        // Summary
        var passed = testResults.Count(r => r.Success);
        var failed = testResults.Count - passed;
        results.AppendLine("═══════════════════════════════════════════════════════════════");
        results.AppendLine("  Test Summary");
        results.AppendLine("═══════════════════════════════════════════════════════════════");
        results.AppendLine($"Total Controls Tested: {testResults.Count}");
        results.AppendLine($"Passed: {passed} ✓");
        results.AppendLine($"Failed: {failed} {(failed > 0 ? "✗" : "")}");
        results.AppendLine();
        results.AppendLine("═══════════════════════════════════════════════════════════════");
        results.AppendLine("  Next Steps for Manual Testing");
        results.AppendLine("═══════════════════════════════════════════════════════════════");
        results.AppendLine("1. Enable Windows Narrator: Win + Ctrl + Enter");
        results.AppendLine("2. Use Tab key to navigate between controls");
        results.AppendLine("3. Listen to what Narrator announces for each control");
        results.AppendLine("4. Verify:");
        results.AppendLine("   - Control name is announced correctly");
        results.AppendLine("   - Control role is announced correctly");
        results.AppendLine("   - Control value is announced (if applicable)");
        results.AppendLine("   - Controls are interactive");
        results.AppendLine();
        results.AppendLine("5. Test with UI Automation tools:");
        results.AppendLine("   - Inspect.exe (Windows SDK)");
        results.AppendLine("   - Accessibility Insights for Windows");
        results.AppendLine("   - UI Automation Spy");
        results.AppendLine();
        results.AppendLine("6. Verify across all target frameworks:");
        results.AppendLine("   - net472, net48, net481 (Legacy)");
        results.AppendLine("   - net8.0-windows, net9.0-windows, net10.0-windows (Modern)");

        _txtResults.Text = results.ToString();
        _txtResults.SelectionStart = 0;
        _txtResults.ScrollToCaret();

        _lblStatus.Text = $"Tests completed: {passed}/{testResults.Count} passed";
        _lblStatus.StateCommon.ShortText.Color1 = failed == 0 ? Color.Green : Color.Red;
    }

    private TestResult TestControl(string controlName, Control? control)
    {
        var result = new TestResult { ControlName = controlName };

        if (control == null)
        {
            result.ErrorMessage = "Control is null";
            return result;
        }

        try
        {
            var accessible = control.AccessibilityObject;
            result.AccessibleObjectCreated = accessible != null;

            if (accessible != null)
            {
                result.Name = accessible.Name;
                result.Description = accessible.Description;
                result.Role = accessible.Role.ToString();
                result.State = accessible.State.ToString();
                result.Value = accessible.Value;
                result.ChildCount = accessible.GetChildCount();

                // Test navigation
                try
                {
                    var parent = accessible.Navigate(AccessibleNavigation.FirstChild);
                    result.NavigationWorks = true;
                }
                catch
                {
                    result.NavigationWorks = false;
                }

                result.Success = true;
            }
            else
            {
                result.ErrorMessage = "AccessibilityObject is null";
            }
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
            result.Success = false;
        }

        return result;
    }

    private void BtnClearResults_Click(object? sender, EventArgs e)
    {
        _txtResults.Text = string.Empty;
        _lblStatus.Text = "Ready";
        _lblStatus.StateCommon.ShortText.Color1 = Color.Black;
    }

    private void BtnExportResults_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_txtResults.Text))
        {
            MessageBox.Show("No results to export. Please run tests first.", "Export Results",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var saveDialog = new SaveFileDialog
        {
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            FileName = $"AccessibilityTestResults_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
            Title = "Export Test Results"
        };

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                System.IO.File.WriteAllText(saveDialog.FileName, _txtResults.Text);
                MessageBox.Show($"Results exported successfully to:\n{saveDialog.FileName}",
                    "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting results:\n{ex.Message}",
                    "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private class TestResult
    {
        public string ControlName { get; set; } = string.Empty;
        public bool Success { get; set; }
        public bool AccessibleObjectCreated { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Role { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string? Value { get; set; }
        public int ChildCount { get; set; }
        public bool NavigationWorks { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
    #endregion
}
