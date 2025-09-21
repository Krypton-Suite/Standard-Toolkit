using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;
using Krypton.Ribbon;
using BackstageDemo.UserControls;

namespace BackstageDemo
{
    /// <summary>
    /// Simple demonstration of the backstage functionality.
    /// </summary>
    public partial class BackstageDemo : KryptonForm
    {
        private KryptonRibbon? _ribbon;

        public BackstageDemo()
        {
            InitializeComponent();
            SetupBackstageDemo();
        }

        private void SetupBackstageDemo()
        {
            // Create ribbon
            _ribbon = new KryptonRibbon
            {
                Dock = DockStyle.Top
            };

            // Configure backstage via the expandable object
            _ribbon.BackstageValues.NavigationWidth = 500; // Custom width - wider for better button display
            _ribbon.BackstageValues.AllowNavigationResize = false; // Fixed width
            Controls.Add(_ribbon);

            // Create a simple tab
            var homeTab = new KryptonRibbonTab
            {
                Text = "Home"
            };
            _ribbon.RibbonTabs.Add(homeTab);

            // Create a group
            var demoGroup = new KryptonRibbonGroup
            {
                TextLine1 = "Demo"
            };
            homeTab.Groups.Add(demoGroup);

            // Create a triple container
            var triple = new KryptonRibbonGroupTriple();
            demoGroup.Items.Add(triple);

            // Create a sample button
            var sampleButton = new KryptonRibbonGroupButton
            {
                TextLine1 = "Sample",
                TextLine2 = "Button"
            };
            triple.Items.Add(sampleButton);

            // Set up backstage pages using different approaches
            SetupBackstagePages();

            // Select the home tab
            _ribbon.SelectedTab = homeTab;

            // Add instruction label below the ribbon
            var instructionLabel = new KryptonLabel
            {
                Text = "Click the Application Button (top-left) to open the backstage view and explore different page types.",
                Location = new Point(20, _ribbon.Height + 20),
                Size = new Size(750, 50),
                StateCommon = { ShortText = { Font = new Font("Segoe UI", 12F), TextV = PaletteRelativeAlign.Near } }
            };
            Controls.Add(instructionLabel);

            // Add demo content
            var demoContentLabel = new KryptonLabel
            {
                Text = "This demo showcases the Krypton Ribbon Backstage View with multiple page types:\n\n" +
                       "• Simple text pages (Open, Save)\n" +
                       "• UserControl pages (New, Settings, Help)\n" +
                       "• Dynamic page management (Settings page has Add/Remove buttons)\n" +
                       "• Proper theme integration and designer support\n\n" +
                       "The backstage overlays the entire form area and provides an Office-style interface.",
                Location = new Point(20, _ribbon.Height + 80),
                Size = new Size(750, 200),
                StateCommon = { ShortText = { Font = new Font("Segoe UI", 11F), TextV = PaletteRelativeAlign.Near } }
            };
            Controls.Add(demoContentLabel);
        }

        private void SetupBackstagePages()
        {
            // EXAMPLE: Different ways to add controls to backstage content area
            
            // Method 1: Simple pages with default content (uses TextTitle/TextDescription)
            var openPage = _ribbon.BackstagePages.Add("Open");
            openPage.TextTitle = "Open an existing document";
            openPage.TextDescription = "Browse for documents on your computer or online locations.";

            var savePage = _ribbon.BackstagePages.Add("Save");
            savePage.TextTitle = "Save your document";
            savePage.TextDescription = "Save your document to your computer or online locations.";

            // Method 2: Pages with UserControls (recommended for complex layouts)
            var newDocumentUserControl = new NewDocumentPage();
            var newPage = _ribbon.BackstagePages.Add("New", newDocumentUserControl);

            var settingsUserControl = new SettingsPage();
            var settingsPage = _ribbon.BackstagePages.Add("Settings", settingsUserControl);

            var helpUserControl = new HelpPage();
            var helpPage = _ribbon.BackstagePages.Add("Help", helpUserControl);
        }
    }
}