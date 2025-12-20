#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Example plugin UserControl demonstrating ribbon hosting and merging.
/// This plugin provides text editing and formatting tools.
/// </summary>
public partial class TextToolsPlugin : UserControl
{
    private readonly KryptonRibbon ribbon;

    public TextToolsPlugin()
    {
        InitializeComponent();
        
        // Create ribbon on UserControl
        ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        // Create Text Tools tab
        var textTab = new KryptonRibbonTab { Text = "Text Tools" };
        
        // Formatting group
        var formatGroup = new KryptonRibbonGroup { TextLine1 = "Formatting" };
        var boldButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Bold"
        };
        boldButton.Click += (s, e) => lblPluginStatus.Text = "Bold formatting applied";
        
        var italicButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Italic"
        };
        italicButton.Click += (s, e) => lblPluginStatus.Text = "Italic formatting applied";
        
        var underlineButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Underline"
        };
        underlineButton.Click += (s, e) => lblPluginStatus.Text = "Underline formatting applied";
        
        var triple1 = new KryptonRibbonGroupTriple();
        triple1.Items.Add(boldButton);
        triple1.Items.Add(italicButton);
        triple1.Items.Add(underlineButton);
        formatGroup.Items.Add(triple1);
        
        // Alignment group
        var alignGroup = new KryptonRibbonGroup { TextLine1 = "Alignment" };
        var leftAlignButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Left"
        };
        leftAlignButton.Click += (s, e) => lblPluginStatus.Text = "Left alignment applied";
        
        var centerAlignButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Center"
        };
        centerAlignButton.Click += (s, e) => lblPluginStatus.Text = "Center alignment applied";
        
        var rightAlignButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Right"
        };
        rightAlignButton.Click += (s, e) => lblPluginStatus.Text = "Right alignment applied";
        
        var triple2 = new KryptonRibbonGroupTriple();
        triple2.Items.Add(leftAlignButton);
        triple2.Items.Add(centerAlignButton);
        triple2.Items.Add(rightAlignButton);
        alignGroup.Items.Add(triple2);
        
        textTab.Groups.Add(formatGroup);
        textTab.Groups.Add(alignGroup);
        
        ribbon.RibbonTabs.Add(textTab);
        
        // Add ribbon to UserControl
        Controls.Add(ribbon);
        
        // Add content panel below ribbon
        var contentPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill
        };
        Controls.Add(contentPanel);
        
        // Add status label
        lblPluginStatus = new KryptonLabel
        {
            Text = "Text Tools Plugin - Ready",
            Dock = DockStyle.Top
        };
        lblPluginStatus.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        lblPluginStatus.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        lblPluginStatus.StateCommon.Padding = new Padding(10);
        contentPanel.Controls.Add(lblPluginStatus);
        
        // Add demo content
        var demoLabel = new KryptonLabel
        {
            Text = "This is the Text Tools plugin content area.\r\n" +
                   "The ribbon above is hosted on this UserControl and\r\n" +
                   "will be merged into the main application ribbon when loaded.",
            Dock = DockStyle.Fill
        };
        demoLabel.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        demoLabel.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        demoLabel.StateCommon.Padding = new Padding(20);
        contentPanel.Controls.Add(demoLabel);
    }

    public KryptonRibbon Ribbon => ribbon;
}

