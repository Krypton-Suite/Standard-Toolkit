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
/// This plugin provides data management functionality.
/// </summary>
public partial class DataManagerPlugin : UserControl
{
    private readonly KryptonRibbon ribbon;

    public DataManagerPlugin()
    {
        InitializeComponent();

        // Create ribbon on UserControl
        ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        // Create Data Manager tab
        var dataTab = new KryptonRibbonTab { Text = "Data Manager" };

        // Import/Export group
        var importGroup = new KryptonRibbonGroup { TextLine1 = "Import/Export" };
        var importButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Import"
        };
        importButton.Click += (s, e) => lblPluginStatus.Text = "Import data dialog opened";

        var exportButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Export"
        };
        exportButton.Click += (s, e) => lblPluginStatus.Text = "Export data dialog opened";

        var triple1 = new KryptonRibbonGroupTriple();
        triple1.Items?.Add(importButton);
        triple1.Items?.Add(exportButton);
        importGroup.Items.Add(triple1);

        // Analysis group
        var analysisGroup = new KryptonRibbonGroup { TextLine1 = "Analysis" };
        var analyzeButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Analyze"
        };
        analyzeButton.Click += (s, e) => lblPluginStatus.Text = "Data analysis started";

        var reportButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Report"
        };
        reportButton.Click += (s, e) => lblPluginStatus.Text = "Report generated";

        var triple2 = new KryptonRibbonGroupTriple();
        triple2.Items.Add(analyzeButton);
        triple2.Items.Add(reportButton);
        analysisGroup.Items.Add(triple2);

        dataTab.Groups.Add(importGroup);
        dataTab.Groups.Add(analysisGroup);

        ribbon.RibbonTabs.Add(dataTab);

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
            Text = "Data Manager Plugin - Ready",
            Dock = DockStyle.Top
        };
        lblPluginStatus.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        lblPluginStatus.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        lblPluginStatus.StateCommon.Padding = new Padding(10);
        contentPanel.Controls.Add(lblPluginStatus);

        // Add demo content
        var demoLabel = new KryptonLabel
        {
            Text = "This is the Data Manager plugin content area.\r\n" +
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
