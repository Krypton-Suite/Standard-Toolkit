#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm;

public partial class RibbonTest : KryptonForm
{
    public RibbonTest()
    {
        InitializeComponent();
        CreateBackstageDemo();
    }

    private void CreateBackstageDemo()
    {
        // Demo: backstage view with pages, commands, images, and item sizes
        var backstage = new KryptonBackstageView
        {
            Dock = DockStyle.Fill,
            OverlayMode = BackstageOverlayMode.FullClient // Can also use BelowRibbon
        };

        // Create pages with different item sizes
        var infoPage = new KryptonBackstagePage
        {
            Text = @"Info",
            ItemSize = BackstageItemSize.Large // Large item for prominence
            // Image can be set if you have resources: Image = Properties.Resources.InfoIcon
        };

        var actionsPage = new KryptonBackstagePage
        {
            Text = @"Actions",
            ItemSize = BackstageItemSize.Small // Small item
        };

        // Add content to Info page
        var infoLabel = new KryptonLabel
        {
            Text = "Backstage demo page (Info).\n\nThis demonstrates:\n- Pages with Large/Small item sizes\n- Command items\n- Images in navigation\n- Overlay modes",
            Dock = DockStyle.Top,
            Padding = new Padding(20)
        };
        infoPage.Controls.Add(infoLabel);

        // Add content to Actions page
        var closeButton = new KryptonButton
        {
            Text = @"Close Backstage",
            Dock = DockStyle.Top,
            Margin = new Padding(20, 10, 20, 10)
        };
        closeButton.Click += (_, _) => kryptonRibbon.CloseBackstageView();
        actionsPage.Controls.Add(closeButton);

        backstage.Pages.Add(infoPage);
        backstage.Pages.Add(actionsPage);

        // Add command items (no page, just actions)
        var printCommand = new KryptonBackstageCommand("Print")
        {
            ItemSize = BackstageItemSize.Large // Make it prominent
            // Image can be set if you have resources: Image = Properties.Resources.PrintIcon
        };
        printCommand.Click += (_, _) =>
        {
            MessageBox.Show("Print command clicked!", "Backstage Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };
        backstage.Commands.Add(printCommand);

        var exitCommand = new KryptonBackstageCommand("Exit")
        {
            ItemSize = BackstageItemSize.Small
            // Image can be set if you have resources: Image = Properties.Resources.ExitIcon
        };
        exitCommand.Click += (_, _) =>
        {
            if (MessageBox.Show("Exit application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        };
        backstage.Commands.Add(exitCommand);

        // Handle Close button (permanent button at bottom)
        backstage.CloseRequested += (_, e) =>
        {
            var result = MessageBox.Show(
                "Close the application?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; // Prevent close
            }
        };

        kryptonRibbon.RibbonFileAppTab.UseBackstageView = true;
        kryptonRibbon.RibbonFileAppTab.BackstageView = backstage;
    }

    private void krgbtnTest1715_Click(object sender, EventArgs e)
    {
        kryptonRibbon.SelectedTab!.ContextName = @"Testing";
    }
}
