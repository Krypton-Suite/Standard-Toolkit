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
/// This plugin provides image editing functionality.
/// </summary>
public partial class ImageEditorPlugin : UserControl
{
    private readonly KryptonRibbon ribbon;

    public ImageEditorPlugin()
    {
        InitializeComponent();
        
        // Create ribbon on UserControl
        ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        // Create Image Editor tab
        var imageTab = new KryptonRibbonTab { Text = "Image Editor" };
        
        // Editing group
        var editGroup = new KryptonRibbonGroup { TextLine1 = "Editing" };
        var cropButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Crop"
        };
        cropButton.Click += (s, e) => lblPluginStatus.Text = "Crop tool activated";
        
        var rotateButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Rotate"
        };
        rotateButton.Click += (s, e) => lblPluginStatus.Text = "Rotate tool activated";
        
        var triple1 = new KryptonRibbonGroupTriple();
        triple1.Items.Add(cropButton);
        triple1.Items.Add(rotateButton);
        editGroup.Items.Add(triple1);
        
        // Effects group
        var effectsGroup = new KryptonRibbonGroup { TextLine1 = "Effects" };
        var blurButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Blur"
        };
        blurButton.Click += (s, e) => lblPluginStatus.Text = "Blur effect applied";
        
        var sharpenButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Sharpen"
        };
        sharpenButton.Click += (s, e) => lblPluginStatus.Text = "Sharpen effect applied";
        
        var triple2 = new KryptonRibbonGroupTriple();
        triple2.Items.Add(blurButton);
        triple2.Items.Add(sharpenButton);
        effectsGroup.Items.Add(triple2);
        
        imageTab.Groups.Add(editGroup);
        imageTab.Groups.Add(effectsGroup);
        
        ribbon.RibbonTabs.Add(imageTab);
        
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
            Text = "Image Editor Plugin - Ready",
            Dock = DockStyle.Top
        };
        lblPluginStatus.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        lblPluginStatus.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        lblPluginStatus.StateCommon.Padding = new Padding(10);
        contentPanel.Controls.Add(lblPluginStatus);
        
        // Add demo content
        var demoLabel = new KryptonLabel
        {
            Text = "This is the Image Editor plugin content area.\r\n" +
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

