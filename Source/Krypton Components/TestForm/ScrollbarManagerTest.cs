#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KryptonScrollbarManager with container mode, native wrapper mode, and integration examples.
/// </summary>
public partial class ScrollbarManagerTest : KryptonForm
{
    #region Instance Fields

    private KryptonScrollbarManager? _containerManager;
    private KryptonScrollbarManager? _textBoxManager;
    private KryptonScrollbarManager? _richTextBoxManager;
    private int _contentCounter;
    private string _textBoxScenarioName = "Both axes overflow";
    private string _textBoxScenarioExpectation = "Expected H/V: Visible/Visible";
    private string _richTextBoxScenarioName = "Vertical overflow only";
    private string _richTextBoxScenarioExpectation = "Expected H/V: Hidden/Visible";

    #endregion

    #region Identity

    public ScrollbarManagerTest()
    {
        InitializeComponent();
        InitializeDemo();
    }

    #endregion

    #region Implementation

    private void InitializeDemo()
    {
        // Example 1: Container Mode with KryptonPanel
        SetupContainerModeExample();

        // Example 2: Native Wrapper Mode with KryptonTextBox
        SetupNativeWrapperTextBoxExample();

        // Example 3: Native Wrapper Mode with KryptonRichTextBox
        SetupNativeWrapperRichTextBoxExample();

        // Example 4: Dynamic Content
        SetupDynamicContentExample();

        // Example 5: Scrollbar Properties
        SetupScrollbarPropertiesExample();
    }

    private void SetupContainerModeExample()
    {
        lblContainerExample.Text = "Example 1: Container Mode - KryptonPanel with Krypton Scrollbars";
        btnAddContentContainer.Text = "Add Content";
        btnClearContentContainer.Text = "Clear";
        btnToggleContainerManager.Text = "Toggle Manager";

        // Create scrollbar manager for container panel
        _containerManager = new KryptonScrollbarManager(panelContainer, ScrollbarManagerMode.Container)
        {
            Enabled = true
        };

        _containerManager.ScrollbarsChanged += (s, e) =>
        {
            UpdateContainerStatus();
        };

        btnAddContentContainer.Click += (s, e) =>
        {
            AddContentToPanel(panelContainer);
        };

        btnClearContentContainer.Click += (s, e) =>
        {
            ClearPanelContent(panelContainer);
        };

        btnToggleContainerManager.Click += (s, e) =>
        {
            if (_containerManager != null)
            {
                _containerManager.Enabled = !_containerManager.Enabled;
                btnToggleContainerManager.Text = _containerManager.Enabled ? "Disable Manager" : "Enable Manager";
                UpdateContainerStatus();
            }
        };

        // Add initial content
        for (int i = 0; i < 5; i++)
        {
            AddContentToPanel(panelContainer);
        }

        UpdateContainerStatus();
    }

    private void SetupNativeWrapperTextBoxExample()
    {
        lblTextBoxExample.Text = "Example 2: Native Wrapper Mode - KryptonTextBox regression harness";
        btnToggleTextBoxManager.Text = "Toggle Manager";

        // Create scrollbar manager for textbox (native wrapper mode)
        _textBoxManager = new KryptonScrollbarManager(textBoxNative, ScrollbarManagerMode.NativeWrapper)
        {
            Enabled = true
        };

        _textBoxManager.ScrollbarsChanged += (s, e) =>
        {
            UpdateTextBoxStatus();
        };

        btnToggleTextBoxManager.Click += (s, e) =>
        {
            if (_textBoxManager != null)
            {
                _textBoxManager.Enabled = !_textBoxManager.Enabled;
                btnToggleTextBoxManager.Text = _textBoxManager.Enabled ? "Disable Manager" : "Enable Manager";
                UpdateTextBoxStatus();
            }
        };

        textBoxNative.Multiline = true;
        textBoxNative.WordWrap = false; // Enable horizontal scrolling
        textBoxNative.ScrollBars = ScrollBars.Both;
        CreateNativeTextBoxScenarioButtons();
        ApplyTextBoxScenarioBothOverflow();

        UpdateTextBoxStatus();
    }

    private void SetupNativeWrapperRichTextBoxExample()
    {
        lblRichTextBoxExample.Text = "Example 3: Native Wrapper Mode - KryptonRichTextBox regression harness";
        btnToggleRichTextBoxManager.Text = "Toggle Manager";

        // Create scrollbar manager for richtextbox (native wrapper mode)
        _richTextBoxManager = new KryptonScrollbarManager(richTextBoxNative, ScrollbarManagerMode.NativeWrapper)
        {
            Enabled = true
        };

        _richTextBoxManager.ScrollbarsChanged += (s, e) =>
        {
            UpdateRichTextBoxStatus();
        };

        btnToggleRichTextBoxManager.Click += (s, e) =>
        {
            if (_richTextBoxManager != null)
            {
                _richTextBoxManager.Enabled = !_richTextBoxManager.Enabled;
                btnToggleRichTextBoxManager.Text = _richTextBoxManager.Enabled ? "Disable Manager" : "Enable Manager";
                UpdateRichTextBoxStatus();
            }
        };

        richTextBoxNative.Multiline = true;
        richTextBoxNative.WordWrap = true;
        CreateNativeRichTextBoxScenarioButtons();
        ApplyRichTextBoxScenarioVerticalOverflow();

        UpdateRichTextBoxStatus();
    }

    private void CreateNativeTextBoxScenarioButtons()
    {
        KryptonButton btnNoOverflow = new()
        {
            Location = new Point(120, 191),
            Size = new Size(90, 22),
            Text = "No Overflow"
        };
        btnNoOverflow.Click += (s, e) => ApplyTextBoxScenarioNoOverflow();
        kpgTextBox.Panel.Controls.Add(btnNoOverflow);

        KryptonButton btnVerticalOnly = new()
        {
            Location = new Point(215, 191),
            Size = new Size(92, 22),
            Text = "Vertical"
        };
        btnVerticalOnly.Click += (s, e) => ApplyTextBoxScenarioVerticalOverflow();
        kpgTextBox.Panel.Controls.Add(btnVerticalOnly);

        KryptonButton btnBoth = new()
        {
            Location = new Point(312, 191),
            Size = new Size(92, 22),
            Text = "Both Axes"
        };
        btnBoth.Click += (s, e) => ApplyTextBoxScenarioBothOverflow();
        kpgTextBox.Panel.Controls.Add(btnBoth);
    }

    private void CreateNativeRichTextBoxScenarioButtons()
    {
        KryptonButton btnNoOverflow = new()
        {
            Location = new Point(120, 191),
            Size = new Size(90, 22),
            Text = "No Overflow"
        };
        btnNoOverflow.Click += (s, e) => ApplyRichTextBoxScenarioNoOverflow();
        kpgRichTextBox.Panel.Controls.Add(btnNoOverflow);

        KryptonButton btnVerticalOnly = new()
        {
            Location = new Point(215, 191),
            Size = new Size(92, 22),
            Text = "Vertical"
        };
        btnVerticalOnly.Click += (s, e) => ApplyRichTextBoxScenarioVerticalOverflow();
        kpgRichTextBox.Panel.Controls.Add(btnVerticalOnly);

        KryptonButton btnBoth = new()
        {
            Location = new Point(312, 191),
            Size = new Size(92, 22),
            Text = "Both Axes"
        };
        btnBoth.Click += (s, e) => ApplyRichTextBoxScenarioBothOverflow();
        kpgRichTextBox.Panel.Controls.Add(btnBoth);
    }

    private void ApplyTextBoxScenarioNoOverflow()
    {
        _textBoxScenarioName = "No overflow";
        _textBoxScenarioExpectation = "Expected H/V: Hidden/Hidden";
        textBoxNative.WordWrap = false;
        textBoxNative.Text = "Short line";
        _textBoxManager?.UpdateScrollbars();
        UpdateTextBoxStatus();
    }

    private void ApplyTextBoxScenarioVerticalOverflow()
    {
        _textBoxScenarioName = "Vertical overflow only";
        _textBoxScenarioExpectation = "Expected H/V: Hidden/Visible";
        textBoxNative.WordWrap = true;
        textBoxNative.Text = "Line 1 short text.\r\n" +
                             "Line 2 short text.\r\n" +
                             "Line 3 short text.\r\n" +
                             "Line 4 short text.\r\n" +
                             "Line 5 short text.\r\n" +
                             "Line 6 short text.\r\n" +
                             "Line 7 short text.\r\n" +
                             "Line 8 short text.\r\n" +
                             "Line 9 short text.\r\n" +
                             "Line 10 short text.\r\n" +
                             "Line 11 short text.\r\n" +
                             "Line 12 short text.";
        _textBoxManager?.UpdateScrollbars();
        UpdateTextBoxStatus();
    }

    private void ApplyTextBoxScenarioBothOverflow()
    {
        _textBoxScenarioName = "Both axes overflow";
        _textBoxScenarioExpectation = "Expected H/V: Visible/Visible";
        textBoxNative.WordWrap = false;
        textBoxNative.Text = "This is an intentionally very long line that should exceed the width of the native textbox and require a horizontal scrollbar.\r\n" +
                             "Line 2 extends content vertically.\r\n" +
                             "Line 3 extends content vertically.\r\n" +
                             "Line 4 extends content vertically.\r\n" +
                             "Line 5 extends content vertically.\r\n" +
                             "Line 6 extends content vertically.\r\n" +
                             "Line 7 extends content vertically.\r\n" +
                             "Line 8 extends content vertically.\r\n" +
                             "Line 9 extends content vertically.\r\n" +
                             "Line 10 extends content vertically.";
        _textBoxManager?.UpdateScrollbars();
        UpdateTextBoxStatus();
    }

    private void ApplyRichTextBoxScenarioNoOverflow()
    {
        _richTextBoxScenarioName = "No overflow";
        _richTextBoxScenarioExpectation = "Expected H/V: Hidden/Hidden";
        richTextBoxNative.WordWrap = true;
        richTextBoxNative.Text = "Short content";
        _richTextBoxManager?.UpdateScrollbars();
        UpdateRichTextBoxStatus();
    }

    private void ApplyRichTextBoxScenarioVerticalOverflow()
    {
        _richTextBoxScenarioName = "Vertical overflow only";
        _richTextBoxScenarioExpectation = "Expected H/V: Hidden/Visible";
        richTextBoxNative.WordWrap = true;
        richTextBoxNative.Text = "Rich Text Box Example\r\n\r\n" +
                                 "Line 1 short text.\r\n" +
                                 "Line 2 short text.\r\n" +
                                 "Line 3 short text.\r\n" +
                                 "Line 4 short text.\r\n" +
                                 "Line 5 short text.\r\n" +
                                 "Line 6 short text.\r\n" +
                                 "Line 7 short text.\r\n" +
                                 "Line 8 short text.\r\n" +
                                 "Line 9 short text.\r\n" +
                                 "Line 10 short text.\r\n" +
                                 "Line 11 short text.\r\n" +
                                 "Line 12 short text.";
        _richTextBoxManager?.UpdateScrollbars();
        UpdateRichTextBoxStatus();
    }

    private void ApplyRichTextBoxScenarioBothOverflow()
    {
        _richTextBoxScenarioName = "Both axes overflow";
        _richTextBoxScenarioExpectation = "Expected H/V: Visible/Visible";
        richTextBoxNative.WordWrap = false;
        richTextBoxNative.Text = "This is an intentionally very long rich text line that should overflow horizontally and trigger a horizontal scrollbar.\r\n" +
                                 "Line 2 extends content vertically.\r\n" +
                                 "Line 3 extends content vertically.\r\n" +
                                 "Line 4 extends content vertically.\r\n" +
                                 "Line 5 extends content vertically.\r\n" +
                                 "Line 6 extends content vertically.\r\n" +
                                 "Line 7 extends content vertically.\r\n" +
                                 "Line 8 extends content vertically.\r\n" +
                                 "Line 9 extends content vertically.\r\n" +
                                 "Line 10 extends content vertically.";
        _richTextBoxManager?.UpdateScrollbars();
        UpdateRichTextBoxStatus();
    }

    private void SetupDynamicContentExample()
    {
        lblDynamicExample.Text = "Example 4: Dynamic Content - Add/Remove Controls";
        btnAddDynamicContent.Text = "Add Control";
        btnRemoveDynamicContent.Text = "Remove Last";

        KryptonScrollbarManager? dynamicManager = new KryptonScrollbarManager(panelDynamic, ScrollbarManagerMode.Container)
        {
            Enabled = true
        };

        btnAddDynamicContent.Click += (s, e) =>
        {
            _contentCounter++;
            var label = new KryptonLabel
            {
                Text = $"Dynamic Control #{_contentCounter}",
                Location = new Point(10, (_contentCounter - 1) * 30 + 10),
                Size = new Size(200, 25),
                Tag = new Point(10, (_contentCounter - 1) * 30 + 10) // Store original location
            };
            panelDynamic.Controls.Add(label);
            dynamicManager?.UpdateScrollbars();
            UpdateDynamicStatus(dynamicManager);
        };

        btnRemoveDynamicContent.Click += (s, e) =>
        {
            if (panelDynamic.Controls.Count > 0)
            {
                // Remove last non-scrollbar control
                for (int i = panelDynamic.Controls.Count - 1; i >= 0; i--)
                {
                    var control = panelDynamic.Controls[i];
                    if (control is not KryptonHScrollBar and not KryptonVScrollBar)
                    {
                        panelDynamic.Controls.Remove(control);
                        control.Dispose();
                        break;
                    }
                }
                dynamicManager?.UpdateScrollbars();
                UpdateDynamicStatus(dynamicManager);
            }
        };

        // Add initial content
        for (int i = 0; i < 3; i++)
        {
            _contentCounter++;
            var label = new KryptonLabel
            {
                Text = $"Dynamic Control #{_contentCounter}",
                Location = new Point(10, (i * 30) + 10),
                Size = new Size(200, 25),
                Tag = new Point(10, (i * 30) + 10)
            };
            panelDynamic.Controls.Add(label);
        }

        UpdateDynamicStatus(dynamicManager);
    }

    private void SetupScrollbarPropertiesExample()
    {
        lblPropertiesExample.Text = "Example 5: Scrollbar Properties";

        if (_containerManager?.HorizontalScrollBar != null)
        {
            propertyGrid.SelectedObject = _containerManager.HorizontalScrollBar;
        }
    }

    private void AddContentToPanel(KryptonPanel panel)
    {
        int count = 0;
        foreach (Control ctrl in panel.Controls)
        {
            if (ctrl is not KryptonHScrollBar and not KryptonVScrollBar)
            {
                count++;
            }
        }

        var label = new KryptonLabel
        {
            Text = $"Label {count + 1} - This is a longer label to test horizontal scrolling",
            Location = new Point(10, count * 30 + 10),
            Size = new Size(300, 25),
            Tag = new Point(10, count * 30 + 10) // Store original location
        };
        panel.Controls.Add(label);
        _containerManager?.UpdateScrollbars();
    }

    private void ClearPanelContent(KryptonPanel panel)
    {
        var controlsToRemove = new List<Control>();
        foreach (Control ctrl in panel.Controls)
        {
            if (ctrl is not KryptonHScrollBar and not KryptonVScrollBar)
            {
                controlsToRemove.Add(ctrl);
            }
        }

        foreach (var ctrl in controlsToRemove)
        {
            panel.Controls.Remove(ctrl);
            ctrl.Dispose();
        }

        _containerManager?.UpdateScrollbars();
    }

    private void UpdateContainerStatus()
    {
        if (_containerManager != null)
        {
            bool hVisible = _containerManager.HorizontalScrollBar?.Visible ?? false;
            bool vVisible = _containerManager.VerticalScrollBar?.Visible ?? false;
            lblContainerStatus.Text = $"Manager: {(_containerManager.Enabled ? "Enabled" : "Disabled")} | " +
                                     $"H-Scroll: {(hVisible ? "Visible" : "Hidden")} | " +
                                     $"V-Scroll: {(vVisible ? "Visible" : "Hidden")}";
        }
    }

    private void UpdateTextBoxStatus()
    {
        if (_textBoxManager != null)
        {
            bool hVisible = _textBoxManager.HorizontalScrollBar?.Visible ?? false;
            bool vVisible = _textBoxManager.VerticalScrollBar?.Visible ?? false;
            lblTextBoxStatus.Text = $"Manager: {(_textBoxManager.Enabled ? "Enabled" : "Disabled")} | " +
                                   $"Scenario: {_textBoxScenarioName} | " +
                                   $"H-Scroll: {(hVisible ? "Visible" : "Hidden")} | " +
                                   $"V-Scroll: {(vVisible ? "Visible" : "Hidden")} | " +
                                   _textBoxScenarioExpectation;
        }
    }

    private void UpdateRichTextBoxStatus()
    {
        if (_richTextBoxManager != null)
        {
            bool hVisible = _richTextBoxManager.HorizontalScrollBar?.Visible ?? false;
            bool vVisible = _richTextBoxManager.VerticalScrollBar?.Visible ?? false;
            lblRichTextBoxStatus.Text = $"Manager: {(_richTextBoxManager.Enabled ? "Enabled" : "Disabled")} | " +
                                       $"Scenario: {_richTextBoxScenarioName} | " +
                                       $"H-Scroll: {(hVisible ? "Visible" : "Hidden")} | " +
                                       $"V-Scroll: {(vVisible ? "Visible" : "Hidden")} | " +
                                       _richTextBoxScenarioExpectation;
        }
    }

    private void UpdateDynamicStatus(KryptonScrollbarManager? manager)
    {
        if (manager != null)
        {
            int controlCount = 0;
            foreach (Control ctrl in panelDynamic.Controls)
            {
                if (ctrl is not KryptonHScrollBar and not KryptonVScrollBar)
                {
                    controlCount++;
                }
            }

            bool hVisible = manager.HorizontalScrollBar?.Visible ?? false;
            bool vVisible = manager.VerticalScrollBar?.Visible ?? false;
            lblDynamicStatus.Text = $"Controls: {controlCount} | " +
                                   $"H-Scroll: {(hVisible ? "Visible" : "Hidden")} | " +
                                   $"V-Scroll: {(vVisible ? "Visible" : "Hidden")}";
        }
    }

    #endregion
}
