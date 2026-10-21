#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
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
        lblTextBoxExample.Text = "Example 2: Native Wrapper Mode - KryptonTextBox with Krypton Scrollbars";
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

        // Add sample text
        textBoxNative.Text = "This is a sample text that should be long enough to require scrolling. " +
                            "Let's add more text to make it scrollable. " +
                            "Here is some more content. " +
                            "And even more text to ensure horizontal scrolling is needed. " +
                            "This text should wrap and create a vertical scrollbar as well. " +
                            "Keep adding more lines to test the vertical scrolling functionality.";

        textBoxNative.Multiline = true;
        textBoxNative.WordWrap = false; // Enable horizontal scrolling
        textBoxNative.ScrollBars = ScrollBars.Both;

        UpdateTextBoxStatus();
    }

    private void SetupNativeWrapperRichTextBoxExample()
    {
        lblRichTextBoxExample.Text = "Example 3: Native Wrapper Mode - KryptonRichTextBox with Krypton Scrollbars";
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

        // Add sample rich text
        richTextBoxNative.Text = "Rich Text Box Example\n\n" +
                                 "This is a rich text box with formatted content.\n" +
                                 "It supports multiple lines and formatting.\n\n" +
                                 "Line 1: Sample text\n" +
                                 "Line 2: More sample text\n" +
                                 "Line 3: Even more text\n" +
                                 "Line 4: Keep scrolling\n" +
                                 "Line 5: More content\n" +
                                 "Line 6: Additional lines\n" +
                                 "Line 7: To test scrolling\n" +
                                 "Line 8: Vertical scrolling\n" +
                                 "Line 9: Works well\n" +
                                 "Line 10: Final line";

        richTextBoxNative.Multiline = true;
        richTextBoxNative.WordWrap = true;

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
                                   $"H-Scroll: {(hVisible ? "Visible" : "Hidden")} | " +
                                   $"V-Scroll: {(vVisible ? "Visible" : "Hidden")}";
        }
    }

    private void UpdateRichTextBoxStatus()
    {
        if (_richTextBoxManager != null)
        {
            bool hVisible = _richTextBoxManager.HorizontalScrollBar?.Visible ?? false;
            bool vVisible = _richTextBoxManager.VerticalScrollBar?.Visible ?? false;
            lblRichTextBoxStatus.Text = $"Manager: {(_richTextBoxManager.Enabled ? "Enabled" : "Disabled")} | " +
                                       $"H-Scroll: {(hVisible ? "Visible" : "Hidden")} | " +
                                       $"V-Scroll: {(vVisible ? "Visible" : "Hidden")}";
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
