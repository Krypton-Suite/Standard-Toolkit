using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;
using Krypton.Ribbon;
using BackstageDemo.UserControls;

namespace BackstageDemo
{
    /// <summary>
    /// Comprehensive demonstration of different ways to add backstage pages with UserControls.
    /// </summary>
    public partial class UserControlDemo : KryptonForm
    {
        private KryptonRibbon _ribbon;

        public UserControlDemo()
        {
            InitializeComponent();
            SetupUserControlDemo();
        }

        private void SetupUserControlDemo()
        {
            // Create ribbon
            _ribbon = new KryptonRibbon
            {
                Dock = DockStyle.Top
            };

            // Configure backstage via the expandable object
            _ribbon.BackstageValues.NavigationWidth = 350; // Wider navigation for better demo
            _ribbon.BackstageValues.AllowNavigationResize = false; // Fixed width
            Controls.Add(_ribbon);

            // Create a simple tab
            var homeTab = new KryptonRibbonTab
            {
                Text = "Home"
            };
            _ribbon.RibbonTabs.Add(homeTab);

            // Add a group to the tab
            var demoGroup = new KryptonRibbonGroup
            {
                TextLine1 = "UserControl Demo"
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
                Text = "Click the Application Button (top-left) to open the backstage view and see different UserControl examples.",
                Location = new Point(20, _ribbon.Height + 20),
                Size = new Size(800, 50),
                StateCommon = { ShortText = { Font = new Font("Segoe UI", 12F), TextV = PaletteRelativeAlign.Near } }
            };
            Controls.Add(instructionLabel);

            // Add demo content
            var demoContentLabel = new KryptonLabel
            {
                Text = "This demo shows 6 different methods for adding backstage pages:\n\n" +
                       "• Method 1: UserControl for New Document (complex UI)\n" +
                       "• Method 2: Simple text content for Open (quick setup)\n" +
                       "• Method 3: UserControl for Settings (form-like interface)\n" +
                       "• Method 4: UserControl for Help (information display)\n" +
                       "• Method 5: Traditional approach for Save (set properties after creation)\n" +
                       "• Method 6: Custom KryptonPanel for Dynamic (runtime page management)",
                Location = new Point(20, _ribbon.Height + 80),
                Size = new Size(800, 200),
                StateCommon = { ShortText = { Font = new Font("Segoe UI", 11F), TextV = PaletteRelativeAlign.Near } }
            };
            Controls.Add(demoContentLabel);
        }

        private void SetupBackstagePages()
        {
            // Method 1: Add page with UserControl (Recommended for complex pages)
            // This is the cleanest approach for developers who want to design pages in separate UserControls
            var newDocumentUserControl = new NewDocumentPage();
            var newPage = _ribbon.AddBackstagePage("New Document", newDocumentUserControl);
            
            // Method 2: Add page with simple text content (Quick and easy for simple pages)
            var openPage = _ribbon.AddBackstagePage("Open File", "Open an existing document", 
                "Browse for documents on your computer, network drives, or online locations. Supports various file formats including DOCX, PDF, TXT, and more.");
            
            // Method 3: Add another UserControl page (Settings example)
            var settingsUserControl = new SettingsPage();
            var settingsPage = _ribbon.AddBackstagePage("Application Settings", settingsUserControl);
            
            // Method 4: Add UserControl page (Help example)
            var helpUserControl = new HelpPage();
            var helpPage = _ribbon.AddBackstagePage("Help & Support", helpUserControl);
            
            // Method 5: Traditional approach - Add empty page and set properties later
            var savePage = _ribbon.AddBackstagePage("Save Document");
            savePage.TextTitle = "Save your document";
            savePage.TextDescription = "Choose a location and format for your document. You can save to your computer, network drives, or cloud storage services like OneDrive, Google Drive, or Dropbox.";
            
            // Method 6: Add page with custom KryptonPanel (for specific styling needs)
            var customPanel = new KryptonPanel 
            { 
                Dock = DockStyle.Fill,
                PanelBackStyle = PaletteBackStyle.PanelClient
            };
            
            // Add content to the custom panel
            var customTitle = new KryptonLabel
            {
                Text = "Dynamic Page Management",
                Location = new Point(30, 30),
                Size = new Size(400, 40),
                StateCommon = { ShortText = { Font = new Font("Segoe UI", 18F, FontStyle.Bold) } }
            };
            customPanel.Controls.Add(customTitle);
            
            var customDesc = new KryptonLabel
            {
                Text = "This page demonstrates dynamic page management. You can add and remove pages at runtime using the buttons below. This is useful for applications that need to modify their backstage based on user actions or loaded content.",
                Location = new Point(30, 80),
                Size = new Size(500, 80),
                StateCommon = { ShortText = { TextV = PaletteRelativeAlign.Near } }
            };
            customPanel.Controls.Add(customDesc);
            
            // Add dynamic page management buttons
            var addPageButton = new KryptonButton
            {
                Text = "Add Runtime Page",
                Location = new Point(30, 180),
                Size = new Size(150, 40)
            };
            addPageButton.Click += (s, e) => {
                var pageCount = _ribbon.BackstagePages.Count;
                var testPage = _ribbon.AddBackstagePage($"Runtime {pageCount}", "Runtime Created Page", 
                    $"This is page number {pageCount} created at runtime. You can add pages dynamically and they will appear in the navigation automatically. This is useful for plugins, dynamic content, or user-customizable interfaces.");
                MessageBox.Show($"Added 'Runtime {pageCount}' page!\n\nThe navigation has been automatically refreshed. Click on the new page to see its content.", "Page Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            customPanel.Controls.Add(addPageButton);
            
            var removePageButton = new KryptonButton
            {
                Text = "Remove Last Runtime",
                Location = new Point(200, 180),
                Size = new Size(150, 40)
            };
            removePageButton.Click += (s, e) => {
                // Find and remove the last "Runtime" page
                KryptonRibbonBackstagePage? lastRuntimePage = null;
                for (int i = _ribbon.BackstagePages.Count - 1; i >= 0; i--)
                {
                    if (_ribbon.BackstagePages[i].Text.StartsWith("Runtime "))
                    {
                        lastRuntimePage = _ribbon.BackstagePages[i];
                        break;
                    }
                }
                
                if (lastRuntimePage != null)
                {
                    _ribbon.RemoveBackstagePage(lastRuntimePage);
                    MessageBox.Show($"Removed '{lastRuntimePage.Text}' page!\n\nThe navigation has been automatically refreshed.", "Page Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No runtime pages to remove!\n\nTry adding some pages first using the 'Add Runtime Page' button.", "No Pages Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            customPanel.Controls.Add(removePageButton);
            
            // Add UserControl example button
            var addUserControlPageButton = new KryptonButton
            {
                Text = "Add UserControl Page",
                Location = new Point(30, 240),
                Size = new Size(150, 40)
            };
            addUserControlPageButton.Click += (s, e) => {
                var newUserControl = new SettingsPage(); // Reuse existing UserControl
                var pageCount = _ribbon.BackstagePages.Count;
                var userControlPage = _ribbon.AddBackstagePage($"UserControl {pageCount}", newUserControl);
                MessageBox.Show($"Added 'UserControl {pageCount}' page with a SettingsPage UserControl!\n\nThis demonstrates how to add UserControl-based pages at runtime.", "UserControl Page Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            customPanel.Controls.Add(addUserControlPageButton);
            
            // Add the custom panel as a page
            var customPage = _ribbon.AddBackstagePage("Dynamic Management", customPanel);
        }
    }
}