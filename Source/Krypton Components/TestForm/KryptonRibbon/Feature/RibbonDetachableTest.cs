#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Test form for demonstrating detachable ribbons feature (Issue #595).
/// 
/// INSTRUCTIONS:
/// =============
/// 
/// This test form demonstrates the detachable ribbons feature that allows a KryptonRibbon
/// to be moved from its parent form into a floating window.
/// 
/// HOW TO USE:
/// -----------
/// 1. The ribbon is initially attached to the top of the form
/// 2. To detach the ribbon:
///    - Click and drag any tab or empty area on the ribbon tab strip / header outward to tear/drag it out into a floating window
///    - Double-click the empty area on the ribbon tab strip
///    - Click the "Detach Ribbon" button (or the Detach button in the ribbon header)
/// 3. The floating window can be moved and positioned anywhere on screen
/// 4. To reattach:
///    - Drag the floating window near the top of the parent window: a semi-transparent dock preview indicator appears, and releasing snaps/reattaches the ribbon!
///    - Double-click the floating window's title bar
///    - Close the floating window
///    - Click the "Reattach Ribbon" button (or the Reattach button in the ribbon header)
/// 
/// FEATURES DEMONSTRATED:
/// ----------------------
/// - AllowDetach property: Enables/disables the detach functionality
/// - AllowDragReattach property: Enables/disables drag-and-drop reattachment with dock preview indicator
/// - Detach() / DetachAndDrag() methods: Moves ribbon to floating window with interactive drag
/// - Reattach() method: Moves ribbon back to original parent
/// - IsDetached property: Indicates current state
/// - RibbonDetached event: Fired when ribbon is detached
/// - RibbonReattached event: Fired when ribbon is reattached
/// 
/// TESTING SCENARIOS:
/// ------------------
/// 1. Basic detach/reattach: Use the buttons to detach and reattach
/// 2. Drag-out to detach: Click and drag a ribbon tab or empty tab strip area down/out to detach and float the ribbon
/// 3. Drag-to-reattach: Drag the floating window over the top of the parent form to see the dock preview and drop to reattach
/// 4. Title bar double-click: Double-click the floating window title bar to reattach
/// 5. Window closing: Close the floating window - ribbon should auto-reattach
/// 6. State persistence: Check that UI updates correctly when state changes
/// 7. Multiple operations: Detach and reattach multiple times
/// 8. Form integration: Verify ribbon displays correctly in both states
/// 
/// NOTES:
/// ------
/// - The ribbon must have AllowDetach = true before calling Detach()
/// - The ribbon must be parented to a control within a Form
/// - Original state (location, size, dock) is preserved and restored
/// - Form integration is automatically disabled when detached
/// </summary>
public partial class RibbonDetachableTest : KryptonForm
{
    public RibbonDetachableTest()
    {
        InitializeComponent();
        CreateRibbon();
        CreateControls();
        UpdateUI();
    }

    /// <summary>
    /// Creates and configures the ribbon control with sample content.
    /// This demonstrates a typical ribbon setup with tabs, groups, and buttons.
    /// </summary>
    private void CreateRibbon()
    {
        // Create the ribbon and enable detach functionality
        // IMPORTANT: AllowDetach must be set to true to enable the feature
        _ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            AllowDetach = true // Enable detach functionality - REQUIRED for feature to work
        };

        // Add custom buttons next to the expand/collapse button
        // These buttons will appear in the tabs area, next to the minimize/expand button
        AddCustomButtons();

        // Create a simple tab with some groups to demonstrate ribbon functionality
        // The ribbon content is not affected by detach/reattach operations
        var homeTab = new KryptonRibbonTab
        {
            Text = @"Home"
        };

        var clipboardGroup = new KryptonRibbonGroup
        {
            TextLine1 = @"Clipboard"
        };

        var pasteButton = new KryptonRibbonGroupButton
        {
            TextLine1 = @"Paste"
            // ImageLarge and ImageSmall can be set if resources are available
        };

        var cutButton = new KryptonRibbonGroupButton
        {
            TextLine1 = @"Cut"
            // ImageLarge and ImageSmall can be set if resources are available
        };

        var copyButton = new KryptonRibbonGroupButton
        {
            TextLine1 = @"Copy"
            // ImageLarge and ImageSmall can be set if resources are available
        };

        // Buttons must be wrapped in a container item (Lines, Triple, etc.)
        var clipboardLines = new KryptonRibbonGroupLines();
        clipboardLines.Items?.Add(pasteButton);
        clipboardLines.Items?.Add(cutButton);
        clipboardLines.Items?.Add(copyButton);
        clipboardGroup.Items.Add(clipboardLines);

        var fontGroup = new KryptonRibbonGroup
        {
            TextLine1 = @"Font"
        };

        var boldButton = new KryptonRibbonGroupButton
        {
            TextLine1 = @"Bold"
            // ImageLarge and ImageSmall can be set if resources are available
        };

        var italicButton = new KryptonRibbonGroupButton
        {
            TextLine1 = @"Italic"
            // ImageLarge and ImageSmall can be set if resources are available
        };

        // Buttons must be wrapped in a container item (Lines, Triple, etc.)
        var fontLines = new KryptonRibbonGroupLines();
        fontLines.Items?.Add(boldButton);
        fontLines.Items?.Add(italicButton);
        fontGroup.Items.Add(fontLines);

        homeTab.Groups.Add(clipboardGroup);
        homeTab.Groups.Add(fontGroup);

        _ribbon.RibbonTabs.Add(homeTab);
        _ribbon.SelectedTab = homeTab;

        // Hook up events to monitor state changes
        // These events are fired automatically when the ribbon state changes
        _ribbon.RibbonDetached += OnRibbonDetached;
        _ribbon.RibbonReattached += OnRibbonReattached;
        
        // Update ribbon buttons to reflect initial state
        UpdateRibbonButtons();

        // Add ribbon to the form
        // The ribbon must be added to a parent control before it can be detached
        Controls.Add(_ribbon);
        _ribbon.BringToFront();
    }

    /// <summary>
    /// Adds custom buttons next to the expand/collapse button in the ribbon tabs area.
    /// These buttons control the detach/reattach functionality and are part of the ribbon itself.
    /// </summary>
    private void AddCustomButtons()
    {
        // Detach button - appears when ribbon is attached
        var detachButton = new ButtonSpecAny
        {
            Text = @"Detach",
            ToolTipTitle = @"Detach Ribbon",
            ToolTipBody = @"Move the ribbon into a floating window",
            UniqueName = @"DetachButton",
            Edge = PaletteRelativeEdgeAlign.Far // Right side, next to minimize/expand
        };
        detachButton.Click += (sender, e) =>
        {
            if (_ribbon != null && _ribbon.Detach())
            {
                UpdateRibbonButtons();
            }
        };
        _ribbon.ButtonSpecs.Add(detachButton);

        // Reattach button - appears when ribbon is detached
        var reattachButton = new ButtonSpecAny
        {
            Text = @"Reattach",
            ToolTipTitle = @"Reattach Ribbon",
            ToolTipBody = @"Move the ribbon back to the form",
            UniqueName = @"ReattachButton",
            Edge = PaletteRelativeEdgeAlign.Far, // Right side, next to minimize/expand
            Visible = false // Initially hidden since ribbon starts attached
        };
        reattachButton.Click += (sender, e) =>
        {
            if (_ribbon != null && _ribbon.Reattach())
            {
                UpdateRibbonButtons();
            }
        };
        _ribbon.ButtonSpecs.Add(reattachButton);

        // Store references for updating visibility
        _detachButton = detachButton;
        _reattachButton = reattachButton;
    }

    /// <summary>
    /// Updates the visibility of the detach/reattach buttons based on ribbon state.
    /// </summary>
    private void UpdateRibbonButtons()
    {
        if (_ribbon == null)
        {
            return;
        }

        var isDetached = _ribbon.IsDetached;

        // Show/hide buttons based on state
        // Changing Visible property automatically triggers ButtonSpecPropertyChanged event
        // which causes the ButtonSpecManager to recreate buttons
        if (_detachButton != null)
        {
            _detachButton.Visible = !isDetached;
        }

        if (_reattachButton != null)
        {
            _reattachButton.Visible = isDetached;
        }

        // Force a paint refresh to ensure buttons are updated
        _ribbon.PerformNeedPaint(true);
    }

    /// <summary>
    /// Creates the UI controls for testing the detach/reattach functionality.
    /// These controls allow the user to interact with the feature and see the current state.
    /// </summary>
    private void CreateControls()
    {
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Bottom,
            Size = new Size(50, 240),
            Padding = new Padding(10)
        };

        // Status label shows the current state of the ribbon
        _lblStatus = new KryptonLabel
        {
            Text = @"Ribbon is attached. Click 'Detach' to move it to a floating window.",
            Dock = DockStyle.Top,
            Height = 30
        };

        // Titlebar text customization controls
        _lblFloatingTitle = new KryptonLabel
        {
            Text = @"Floating Window Title:",
            Dock = DockStyle.Top,
            Height = 20
        };

        _txtFloatingTitle = new KryptonTextBox
        {
            Text = _ribbon?.FloatingWindowText ?? @"Ribbon",
            Dock = DockStyle.Top,
            Height = 25
        };
        _txtFloatingTitle.TextChanged += (s, e) =>
        {
            if (_ribbon != null)
            {
                _ribbon.FloatingWindowText = _txtFloatingTitle.Text;
            }
        };

        // CheckBox for toggling drag-to-reattach
        _chkAllowDragReattach = new KryptonCheckBox
        {
            Text = @"Enable Drag-to-Reattach (with dock preview indicator)",
            Checked = true,
            Dock = DockStyle.Top,
            Height = 25
        };
        _chkAllowDragReattach.CheckedChanged += (s, e) =>
        {
            if (_ribbon != null)
            {
                _ribbon.AllowDragReattach = _chkAllowDragReattach.Checked;
            }
        };

        // Detach button - only enabled when ribbon is attached
        _btnDetach = new KryptonButton
        {
            Text = @"Detach Ribbon",
            Dock = DockStyle.Top,
            Height = 35,
            Margin = new Padding(0, 10, 0, 5),
            //ToolTipText = "Moves the ribbon into a floating window that can be positioned anywhere"
        };
        _btnDetach.Click += BtnDetach_Click;

        // Reattach button - only enabled when ribbon is detached
        _btnReattach = new KryptonButton
        {
            Text = @"Reattach Ribbon",
            Dock = DockStyle.Top,
            Height = 35,
            Margin = new Padding(0, 5, 0, 10),
            Enabled = false, // Initially disabled since ribbon starts attached
            //ToolTipValues. ToolTipText = "Moves the ribbon back to its original position on the form"
        };
        _btnReattach.Click += BtnReattach_Click;

        panel.Controls.Add(_btnReattach);
        panel.Controls.Add(_btnDetach);
        panel.Controls.Add(_chkAllowDragReattach);
        panel.Controls.Add(_txtFloatingTitle);
        panel.Controls.Add(_lblFloatingTitle);
        panel.Controls.Add(_lblStatus);

        Controls.Add(panel);
    }

    /// <summary>
    /// Handles the Detach button click event.
    /// Attempts to detach the ribbon into a floating window.
    /// </summary>
    private void BtnDetach_Click(object? sender, EventArgs e)
    {
        // Check that ribbon exists and detach operation succeeds
        // Detach() returns true if successful, false otherwise
        if (_ribbon != null && _ribbon.Detach())
        {
            // Update UI to reflect new state
            // Note: RibbonDetached event will also fire, which calls UpdateUI()
            UpdateUI();
        }
        else
        {
            // Show error message if detach fails
            // Common causes: AllowDetach is false, already detached, or no parent
            MessageBox.Show(
                @"Failed to detach ribbon. Make sure AllowDetach is enabled.", 
                @"Detach Failed", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Warning);
        }
    }

    /// <summary>
    /// Handles the Reattach button click event.
    /// Attempts to reattach the ribbon to its original parent.
    /// </summary>
    private void BtnReattach_Click(object? sender, EventArgs e)
    {
        // Check that ribbon exists and reattach operation succeeds
        // Reattach() returns true if successful, false otherwise
        if (_ribbon != null && _ribbon.Reattach())
        {
            // Update UI to reflect new state
            // Note: RibbonReattached event will also fire, which calls UpdateUI()
            UpdateUI();
        }
        else
        {
            // Show error message if reattach fails
            // Common causes: Not currently detached, or original parent was disposed
            MessageBox.Show(
                @"Failed to reattach ribbon.", 
                @"Reattach Failed", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Warning);
        }
    }

    /// <summary>
    /// Event handler for RibbonDetached event.
    /// Called automatically when the ribbon is successfully detached.
    /// </summary>
    private void OnRibbonDetached(object? sender, EventArgs e)
    {
        // Update UI to reflect detached state
        UpdateUI();
        UpdateRibbonButtons();
        
        // Additional actions you might want to perform:
        // - Save user preference
        // - Log the action
        // - Update other UI elements
        // - Adjust form layout
    }

    /// <summary>
    /// Event handler for RibbonReattached event.
    /// Called automatically when the ribbon is successfully reattached.
    /// </summary>
    private void OnRibbonReattached(object? sender, EventArgs e)
    {
        // Update UI to reflect attached state
        UpdateUI();
        UpdateRibbonButtons();
        
        // Additional actions you might want to perform:
        // - Save user preference
        // - Log the action
        // - Update other UI elements
        // - Adjust form layout
        // - PerformLayout() if needed
    }

    /// <summary>
    /// Updates the UI controls to reflect the current state of the ribbon.
    /// This ensures buttons are enabled/disabled correctly and status text is accurate.
    /// </summary>
    private void UpdateUI()
    {
        if (_ribbon == null)
        {
            return;
        }

        // Get current state - this property is automatically maintained
        var isDetached = _ribbon.IsDetached;

        // Enable/disable buttons based on state
        // Detach button is only enabled when ribbon is attached
        if (_btnDetach != null)
        {
            _btnDetach.Enabled = !isDetached;
        }

        // Reattach button is only enabled when ribbon is detached
        if (_btnReattach != null)
        {
            _btnReattach.Enabled = isDetached;
        }

        // Update status label text to inform user of current state
        if (_lblStatus != null)
        {
            _lblStatus.Text = isDetached
                ? @"Ribbon is detached in a floating window. Drag near parent top to snap/reattach, double-click title bar, or click 'Reattach'."
                : @"Ribbon is attached. Drag tabs/header out, double-click header, or click 'Detach' to float the ribbon.";
        }
    }
}
