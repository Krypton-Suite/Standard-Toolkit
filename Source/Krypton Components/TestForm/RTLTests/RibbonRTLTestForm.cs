#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Ribbon;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// A test form to demonstrate and verify RTL (Right-To-Left) support in KryptonRibbon.
/// - Contains a ribbon with multiple tabs and groups
/// - Contains a button to toggle RTL mode (RightToLeft and RightToLeftLayout).
/// - Dynamically updates the button text to indicate the current RTL state.
/// - Shows how the ribbon elements are mirrored in RTL mode.
/// </summary>
public partial class RibbonRTLTestForm : KryptonForm
{
    private KryptonRibbon _ribbon;
    private KryptonButton _btnToggleRtl;
    private KryptonLabel _lblStatus;

    public RibbonRTLTestForm()
    {
        InitializeComponent();
        SetupRibbon();
        SetupRtlToggle();
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        
        // Form setup
        this.Text = "Ribbon RTL Test Form";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        
        // Create main panel
        var mainPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };
        
        // Create status label
        _lblStatus = new KryptonLabel
        {
            Text = "RTL Mode: Disabled - Test KryptonRibbon RTL Support",
            Dock = DockStyle.Top,
            Height = 30
        };
        
        // Create RTL toggle button
        _btnToggleRtl = new KryptonButton
        {
            Text = "RTL: OFF",
            Dock = DockStyle.Top,
            Height = 30
        };
        _btnToggleRtl.Click += BtnToggleRtl_Click;
        
        // Add controls to panel
        mainPanel.Controls.Add(_lblStatus);
        mainPanel.Controls.Add(_btnToggleRtl);
        
        // Add panel to form
        this.Controls.Add(mainPanel);
        
        this.ResumeLayout(false);
    }

    private void SetupRibbon()
    {
        // Create ribbon
        _ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            Height = 120
        };

        // Add ribbon to form
        this.Controls.Add(_ribbon);

        // Create tabs
        CreateHomeTab();
        CreateInsertTab();
        CreateViewTab();
    }

    private void CreateHomeTab()
    {
        var homeTab = new KryptonRibbonTab
        {
            Text = "Home",
            Tag = "Home"
        };

        // Clipboard group
        var clipboardGroup = new KryptonRibbonGroup
        {
            TextLine1 = "Clipboard",
            TextLine2 = "Clipboard"
        };

        var pasteButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Paste",
            TextLine2 = "Paste"
        };
        clipboardGroup.Items.Add(pasteButton);

        var cutButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Cut",
            TextLine2 = "Cut"
        };
        clipboardGroup.Items.Add(cutButton);

        homeTab.Groups.Add(clipboardGroup);

        // Font group
        var fontGroup = new KryptonRibbonGroup
        {
            TextLine1 = "Font",
            TextLine2 = "Font"
        };

        var boldButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Bold",
            TextLine2 = "Bold"
        };
        fontGroup.Items.Add(boldButton);

        var italicButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Italic",
            TextLine2 = "Italic"
        };
        fontGroup.Items.Add(italicButton);

        homeTab.Groups.Add(fontGroup);

        _ribbon.RibbonTabs.Add(homeTab);
    }

    private void CreateInsertTab()
    {
        var insertTab = new KryptonRibbonTab
        {
            Text = "Insert",
            Tag = "Insert"
        };

        // Pages group
        var pagesGroup = new KryptonRibbonGroup
        {
            TextLine1 = "Pages",
            TextLine2 = "Pages"
        };

        var pageBreakButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Page Break",
            TextLine2 = "Page Break"
        };
        pagesGroup.Items.Add(pageBreakButton);

        insertTab.Groups.Add(pagesGroup);

        _ribbon.RibbonTabs.Add(insertTab);
    }

    private void CreateViewTab()
    {
        var viewTab = new KryptonRibbonTab
        {
            Text = "View",
            Tag = "View"
        };

        // Document Views group
        var documentViewsGroup = new KryptonRibbonGroup
        {
            TextLine1 = "Document Views",
            TextLine2 = "Document Views"
        };

        var printLayoutButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Print Layout",
            TextLine2 = "Print Layout"
        };
        documentViewsGroup.Items.Add(printLayoutButton);

        viewTab.Groups.Add(documentViewsGroup);

        _ribbon.RibbonTabs.Add(viewTab);
    }

    private void SetupRtlToggle()
    {
        // Set initial RTL state
        UpdateRtlDisplay();
    }

    private void BtnToggleRtl_Click(object sender, EventArgs e)
    {
        // Toggle RTL settings
        // Note: RightToLeftLayout is only available on the form level, not on KryptonRibbon
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        RightToLeftLayout = !RightToLeftLayout;

        // Update ribbon RTL settings - only use RightToLeft since Ribbon doesn't have RightToLeftLayout
        _ribbon.RightToLeft = RightToLeft;

        // Force form title bar rebuild for RTL changes
        PerformLayout();
        Invalidate();

        // Update display
        UpdateRtlDisplay();
    }

    /// <summary>
    /// Updates the display to show current RTL state and test KryptonRibbon functionality.
    /// </summary>
    private void UpdateRtlDisplay()
    {
        // Update button text
        _btnToggleRtl.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
        
        // Update status label
        _lblStatus.Text = $"RTL Mode: {(RightToLeft == RightToLeft.Yes ? "Enabled" : "Disabled")} - Test KryptonRibbon RTL Support";
    }
} 