# Krypton Ribbon Backstage Examples

## Table of Contents
- [UserControl Integration](#usercontrol-integration)
- [Basic Setup](#basic-setup)
- [Simple Text Pages](#simple-text-pages)
- [Custom Content Pages](#custom-content-pages)
- [Dynamic Content](#dynamic-content)
- [Event Handling](#event-handling)
- [Advanced Scenarios](#advanced-scenarios)

## UserControl Integration

The most efficient way to create rich backstage pages is using UserControls. This approach provides clean separation of concerns and excellent designer support.

### Creating UserControl Pages

#### Method 1: Using Convenience Methods (Recommended)

```csharp
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        SetupUserControlBackstage();
    }

    private void SetupUserControlBackstage()
    {
        // Enable backstage
        kryptonRibbon1.BackstageValues.UseBackstageForAppButton = true;
        kryptonRibbon1.BackstageValues.NavigationWidth = 400;

        // Method 1: Add UserControl pages directly
        var newDocumentPage = new NewDocumentUserControl();
        kryptonRibbon1.AddBackstagePage("New", newDocumentPage);

        var settingsPage = new SettingsUserControl();
        kryptonRibbon1.AddBackstagePage("Settings", settingsPage);

        // Method 2: Add with text content (no UserControl needed)
        kryptonRibbon1.AddBackstagePage("Open", "Open an existing document", 
            "Browse for documents on your computer or online locations.");

        // Method 3: Traditional approach with property assignment
        var helpPage = kryptonRibbon1.AddBackstagePage("Help");
        helpPage.ContentPanel = new HelpUserControl { Dock = DockStyle.Fill };
    }
}
```

#### Method 2: Using Collection Methods

```csharp
private void SetupBackstageWithCollectionMethods()
{
    // Using collection Add overloads
    var newPage = kryptonRibbon1.BackstagePages.Add("New", new NewDocumentUserControl());
    var openPage = kryptonRibbon1.BackstagePages.Add("Open", "Open Document", "Browse for files...");
    var settingsPage = kryptonRibbon1.BackstagePages.Add("Settings", new SettingsUserControl());
    
    // Mix and match approaches
    var customPanel = new KryptonPanel { Dock = DockStyle.Fill };
    // ... add controls to customPanel ...
    var customPage = kryptonRibbon1.BackstagePages.Add("Custom", customPanel);
}
```

### Sample UserControl Implementation

Here's an example of a settings UserControl that can be used in the backstage:

```csharp
public partial class SettingsUserControl : UserControl
{
    public SettingsUserControl()
    {
        InitializeComponent();
        SetupControls();
    }

    private void SetupControls()
    {
        // Title
        var titleLabel = new KryptonLabel
        {
            Text = "Application Settings",
            Location = new Point(30, 30),
            Size = new Size(400, 40),
            StateCommon = { ShortText = { Font = new Font("Segoe UI", 18F, FontStyle.Bold) } }
        };
        Controls.Add(titleLabel);

        // Auto Save Setting
        var autoSaveCheck = new KryptonCheckBox
        {
            Text = "Enable auto-save every 5 minutes",
            Location = new Point(30, 90),
            Size = new Size(300, 25),
            Checked = Properties.Settings.Default.AutoSave
        };
        autoSaveCheck.CheckedChanged += (s, e) => 
        {
            Properties.Settings.Default.AutoSave = autoSaveCheck.Checked;
            Properties.Settings.Default.Save();
        };
        Controls.Add(autoSaveCheck);

        // Theme Selection
        var themeLabel = new KryptonLabel
        {
            Text = "Application Theme:",
            Location = new Point(30, 140),
            Size = new Size(150, 25)
        };
        Controls.Add(themeLabel);

        var themeCombo = new KryptonComboBox
        {
            Location = new Point(30, 170),
            Size = new Size(200, 25)
        };
        themeCombo.Items.AddRange(new[] { "Microsoft365Blue", "Office2010Blue", "Office2010Silver" });
        themeCombo.SelectedIndexChanged += (s, e) => ApplyTheme(themeCombo.Text);
        Controls.Add(themeCombo);

        // Action Buttons
        var saveButton = new KryptonButton
        {
            Text = "Save Settings",
            Location = new Point(30, 220),
            Size = new Size(120, 35)
        };
        saveButton.Click += (s, e) => SaveSettings();
        Controls.Add(saveButton);
    }

    private void ApplyTheme(string themeName)
    {
        // Apply theme logic here
        MessageBox.Show($"Applied theme: {themeName}");
    }

    private void SaveSettings()
    {
        Properties.Settings.Default.Save();
        MessageBox.Show("Settings saved successfully!");
    }
}
```

### Dynamic UserControl Management

```csharp
private void AddUserControlPageAtRuntime()
{
    // Create a new UserControl
    var dynamicUserControl = new MyDynamicUserControl();
    
    // Add it to the backstage
    var dynamicPage = kryptonRibbon1.AddBackstagePage($"Dynamic {DateTime.Now:HHmm}", dynamicUserControl);
    
    // The navigation will automatically refresh
    MessageBox.Show("Dynamic UserControl page added!");
}

private void RemoveUserControlPage(string pageText)
{
    // Remove by text
    if (kryptonRibbon1.RemoveBackstagePage(pageText))
    {
        MessageBox.Show($"Removed page: {pageText}");
    }
}
```

### UserControl Best Practices

1. **Design for Backstage**: Make UserControls with appropriate sizing and padding
2. **Use Krypton Controls**: Leverage KryptonButton, KryptonLabel, etc. for theme consistency
3. **Handle Events**: Implement proper event handling within the UserControl
4. **State Management**: Save/restore UserControl state as needed
5. **Resource Cleanup**: Properly dispose of resources when pages are removed

## Basic Setup

### Minimal Backstage Implementation

```csharp
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        SetupBackstage();
    }

    private void SetupBackstage()
    {
        // Enable backstage for the application button
        kryptonRibbon1.UseBackstageForAppButton = true;

        // Add a simple page
        var openPage = new KryptonRibbonBackstagePage
        {
            Text = "Open",
            TextTitle = "Open an existing document",
            TextDescription = "Browse and open files from your computer.",
            Visible = true
        };

        kryptonRibbon1.BackstagePages.Add(openPage);
    }
}
```

## Simple Text Pages

### Information Pages

```csharp
private void CreateInformationPages()
{
    // About page
    var aboutPage = new KryptonRibbonBackstagePage
    {
        Text = "About",
        TextTitle = "About MyApplication",
        TextDescription = "Version 1.2.3\n" +
                         "Built on .NET 8.0\n" +
                         "© 2025 My Company\n\n" +
                         "For support, visit our website or contact us at support@mycompany.com",
        Visible = true
    };

    // Help page
    var helpPage = new KryptonRibbonBackstagePage
    {
        Text = "Help",
        TextTitle = "Get Help",
        TextDescription = "Access documentation, tutorials, and support resources.\n\n" +
                         "• Online Documentation\n" +
                         "• Video Tutorials\n" +
                         "• Community Forums\n" +
                         "• Contact Support",
        Visible = true
    };

    kryptonRibbon1.BackstagePages.Add(aboutPage);
    kryptonRibbon1.BackstagePages.Add(helpPage);
}
```

### Recent Files Page

```csharp
private void CreateRecentFilesPage()
{
    var recentPage = new KryptonRibbonBackstagePage
    {
        Text = "Recent",
        TextTitle = "Recent Documents",
        TextDescription = "Quickly access your recently opened files:",
        Visible = true
    };

    kryptonRibbon1.BackstagePages.Add(recentPage);
}
```

## Custom Content Pages

### Settings Page with Custom Control

```csharp
// Custom UserControl for settings
public partial class SettingsControl : UserControl
{
    public SettingsControl()
    {
        InitializeComponent();
        CreateSettingsUI();
    }

    private void CreateSettingsUI()
    {
        var mainPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        // Theme selection
        var themeLabel = new KryptonLabel
        {
            Text = "Theme:",
            Location = new Point(20, 20),
            LabelStyle = LabelStyle.BoldControl
        };

        var themeCombo = new KryptonComboBox
        {
            Location = new Point(100, 17),
            Width = 200
        };
        themeCombo.Items.AddRange(new[] { "Office 2010 Blue", "Office 2010 Silver", "Office 2013 White" });

        // Auto-save option
        var autoSaveCheck = new KryptonCheckBox
        {
            Text = "Enable auto-save",
            Location = new Point(20, 60)
        };

        // Save button
        var saveButton = new KryptonButton
        {
            Text = "Save Settings",
            Location = new Point(20, 100),
            ButtonStyle = ButtonStyle.Standalone
        };
        saveButton.Click += OnSaveSettings;

        mainPanel.Controls.AddRange(new Control[] 
        { 
            themeLabel, themeCombo, autoSaveCheck, saveButton 
        });
        
        Controls.Add(mainPanel);
    }

    private void OnSaveSettings(object sender, EventArgs e)
    {
        MessageBox.Show("Settings saved!", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

// Usage in main form
private void CreateSettingsPage()
{
    var settingsPage = new KryptonRibbonBackstagePage
    {
        Text = "Settings",
        ContentPanel = new SettingsControl(),
        Visible = true
    };

    kryptonRibbon1.BackstagePages.Add(settingsPage);
}
```

### File Operations Page

```csharp
public partial class FileOperationsControl : UserControl
{
    public FileOperationsControl()
    {
        InitializeComponent();
        CreateFileUI();
    }

    private void CreateFileUI()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4,
            Padding = new Padding(20)
        };

        // Open section
        var openLabel = new KryptonLabel { Text = "Open", LabelStyle = LabelStyle.TitlePanel };
        var openButton = new KryptonButton 
        { 
            Text = "Browse Files...", 
            ButtonStyle = ButtonStyle.Standalone 
        };
        openButton.Click += OnOpenFile;

        // Save section
        var saveLabel = new KryptonLabel { Text = "Save", LabelStyle = LabelStyle.TitlePanel };
        var saveButton = new KryptonButton 
        { 
            Text = "Save As...", 
            ButtonStyle = ButtonStyle.Standalone 
        };
        saveButton.Click += OnSaveFile;

        // Export section
        var exportLabel = new KryptonLabel { Text = "Export", LabelStyle = LabelStyle.TitlePanel };
        var exportButton = new KryptonButton 
        { 
            Text = "Export to PDF", 
            ButtonStyle = ButtonStyle.Standalone 
        };
        exportButton.Click += OnExportFile;

        // Print section
        var printLabel = new KryptonLabel { Text = "Print", LabelStyle = LabelStyle.TitlePanel };
        var printButton = new KryptonButton 
        { 
            Text = "Print Document", 
            ButtonStyle = ButtonStyle.Standalone 
        };
        printButton.Click += OnPrintFile;

        // Add to layout
        layout.Controls.Add(openLabel, 0, 0);
        layout.Controls.Add(openButton, 1, 0);
        layout.Controls.Add(saveLabel, 0, 1);
        layout.Controls.Add(saveButton, 1, 1);
        layout.Controls.Add(exportLabel, 0, 2);
        layout.Controls.Add(exportButton, 1, 2);
        layout.Controls.Add(printLabel, 0, 3);
        layout.Controls.Add(printButton, 1, 3);

        Controls.Add(layout);
    }

    private void OnOpenFile(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Handle file opening
            MessageBox.Show($"Opening: {dialog.FileName}");
        }
    }

    private void OnSaveFile(object sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Handle file saving
            MessageBox.Show($"Saving: {dialog.FileName}");
        }
    }

    private void OnExportFile(object sender, EventArgs e)
    {
        MessageBox.Show("Export functionality would be implemented here.");
    }

    private void OnPrintFile(object sender, EventArgs e)
    {
        MessageBox.Show("Print functionality would be implemented here.");
    }
}
```

## Dynamic Content

### Recent Files with Dynamic Updates

```csharp
public class RecentFilesManager
{
    private readonly List<string> _recentFiles = new();
    private KryptonRibbonBackstagePage _recentPage;
    private KryptonPanel _contentPanel;

    public void Initialize(KryptonRibbon ribbon)
    {
        _contentPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient,
            Padding = new Padding(20)
        };

        _recentPage = new KryptonRibbonBackstagePage
        {
            Text = "Recent",
            ContentPanel = _contentPanel,
            Visible = true
        };

        ribbon.BackstagePages.Add(_recentPage);
        LoadRecentFiles();
        UpdateRecentFilesUI();
    }

    public void AddRecentFile(string filePath)
    {
        _recentFiles.Remove(filePath); // Remove if already exists
        _recentFiles.Insert(0, filePath); // Add to top

        // Keep only last 10 files
        if (_recentFiles.Count > 10)
        {
            _recentFiles.RemoveAt(_recentFiles.Count - 1);
        }

        SaveRecentFiles();
        UpdateRecentFilesUI();
    }

    private void UpdateRecentFilesUI()
    {
        _contentPanel.Controls.Clear();

        var titleLabel = new KryptonLabel
        {
            Text = "Recent Documents",
            Location = new Point(0, 0),
            LabelStyle = LabelStyle.TitlePanel,
            AutoSize = true
        };
        _contentPanel.Controls.Add(titleLabel);

        int yPos = 50;
        foreach (var file in _recentFiles)
        {
            var fileButton = new KryptonButton
            {
                Text = Path.GetFileName(file),
                Tag = file,
                Location = new Point(0, yPos),
                Size = new Size(400, 35),
                ButtonStyle = ButtonStyle.ListItem,
                TextAlign = ContentAlignment.MiddleLeft
            };
            fileButton.Click += OnRecentFileClick;

            var pathLabel = new KryptonLabel
            {
                Text = Path.GetDirectoryName(file),
                Location = new Point(410, yPos + 8),
                LabelStyle = LabelStyle.NormalPanel,
                AutoSize = true
            };

            _contentPanel.Controls.Add(fileButton);
            _contentPanel.Controls.Add(pathLabel);
            yPos += 40;
        }

        if (_recentFiles.Count == 0)
        {
            var noFilesLabel = new KryptonLabel
            {
                Text = "No recent files available.",
                Location = new Point(0, 50),
                LabelStyle = LabelStyle.ItalicPanel,
                AutoSize = true
            };
            _contentPanel.Controls.Add(noFilesLabel);
        }
    }

    private void OnRecentFileClick(object sender, EventArgs e)
    {
        if (sender is KryptonButton button && button.Tag is string filePath)
        {
            // Open the file
            MessageBox.Show($"Opening: {filePath}");
            // Add logic to actually open the file
        }
    }

    private void LoadRecentFiles()
    {
        // Load from registry, config file, etc.
        var saved = Properties.Settings.Default.RecentFiles;
        if (!string.IsNullOrEmpty(saved))
        {
            _recentFiles.AddRange(saved.Split('|'));
        }
    }

    private void SaveRecentFiles()
    {
        Properties.Settings.Default.RecentFiles = string.Join("|", _recentFiles);
        Properties.Settings.Default.Save();
    }
}

// Usage
private RecentFilesManager _recentFilesManager;

private void SetupDynamicContent()
{
    _recentFilesManager = new RecentFilesManager();
    _recentFilesManager.Initialize(kryptonRibbon1);
}

private void OnFileOpened(string filePath)
{
    _recentFilesManager.AddRecentFile(filePath);
}
```

## Event Handling

### Comprehensive Event Handling

```csharp
private void SetupEventHandling()
{
    // Backstage lifecycle events
    kryptonRibbon1.BackstageOpening += OnBackstageOpening;
    kryptonRibbon1.BackstageOpened += OnBackstageOpened;
    kryptonRibbon1.BackstageClosing += OnBackstageClosing;
    kryptonRibbon1.BackstageClosed += OnBackstageClosed;
    kryptonRibbon1.BackstagePageSelected += OnBackstagePageSelected;
}

private void OnBackstageOpening(object sender, CancelEventArgs e)
{
    // Update dynamic content before showing
    UpdateUserInfo();
    RefreshRecentFiles();
    
    // Example: Cancel if in specific application state
    if (IsDocumentBeingEdited() && HasUnsavedChanges())
    {
        var result = MessageBox.Show(
            "You have unsaved changes. Continue to backstage?",
            "Unsaved Changes",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);
            
        if (result == DialogResult.No)
        {
            e.Cancel = true;
        }
    }
}

private void OnBackstageOpened(object sender, EventArgs e)
{
    // Log analytics, update status, etc.
    Logger.Log("Backstage opened");
    
    // Update application state
    SetApplicationState(AppState.BackstageOpen);
}

private void OnBackstageClosing(object sender, CancelEventArgs e)
{
    // Validate any pending operations
    if (IsSettingsPageDirty())
    {
        var result = MessageBox.Show(
            "You have unsaved settings. Save before closing?",
            "Unsaved Settings",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question);
            
        switch (result)
        {
            case DialogResult.Yes:
                SaveSettings();
                break;
            case DialogResult.Cancel:
                e.Cancel = true;
                return;
        }
    }
}

private void OnBackstageClosed(object sender, EventArgs e)
{
    // Restore application state
    SetApplicationState(AppState.Normal);
    Logger.Log("Backstage closed");
}

private void OnBackstagePageSelected(object sender, BackstagePageEventArgs e)
{
    Logger.Log($"Backstage page selected: {e.Page?.Text}");
    
    // Update status or perform page-specific actions
    switch (e.Page?.Text)
    {
        case "Recent":
            RefreshRecentFiles();
            break;
        case "Settings":
            LoadCurrentSettings();
            break;
        case "Account":
            RefreshUserInfo();
            break;
    }
}

// Helper methods
private bool IsDocumentBeingEdited() => /* implementation */;
private bool HasUnsavedChanges() => /* implementation */;
private void UpdateUserInfo() => /* implementation */;
private void RefreshRecentFiles() => /* implementation */;
private bool IsSettingsPageDirty() => /* implementation */;
private void SaveSettings() => /* implementation */;
private void SetApplicationState(AppState state) => /* implementation */;
```

## Advanced Scenarios

### Conditional Page Visibility

```csharp
private void UpdatePageVisibility()
{
    // Show admin page only for administrators
    var adminPage = kryptonRibbon1.BackstagePages
        .FirstOrDefault(p => p.Text == "Admin");
    if (adminPage != null)
    {
        adminPage.Visible = CurrentUser.IsAdmin;
    }

    // Show account page only when logged in
    var accountPage = kryptonRibbon1.BackstagePages
        .FirstOrDefault(p => p.Text == "Account");
    if (accountPage != null)
    {
        accountPage.Visible = CurrentUser.IsLoggedIn;
        accountPage.Enabled = !CurrentUser.IsGuest;
    }

    // Show sync page only when online
    var syncPage = kryptonRibbon1.BackstagePages
        .FirstOrDefault(p => p.Text == "Sync");
    if (syncPage != null)
    {
        syncPage.Visible = NetworkManager.IsOnline;
    }
}
```

### Theme-Aware Custom Content

```csharp
public partial class ThemedContentControl : UserControl
{
    private KryptonManager _manager;
    
    public ThemedContentControl()
    {
        InitializeComponent();
        _manager = KryptonManager.CurrentGlobalManager;
        _manager.GlobalPaletteChanged += OnPaletteChanged;
        ApplyTheme();
    }

    private void OnPaletteChanged(object sender, EventArgs e)
    {
        ApplyTheme();
    }

    private void ApplyTheme()
    {
        var palette = _manager.GlobalPalette;
        
        // Update custom drawing based on current theme
        BackColor = palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
        ForeColor = palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
        
        // Update child controls
        foreach (Control control in Controls)
        {
            if (control is Label label)
            {
                label.ForeColor = ForeColor;
            }
        }
        
        Invalidate();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && _manager != null)
        {
            _manager.GlobalPaletteChanged -= OnPaletteChanged;
        }
        base.Dispose(disposing);
    }
}
```

### Integration with Application Commands

```csharp
public class BackstageCommandIntegration
{
    private readonly ICommandManager _commandManager;
    private readonly KryptonRibbon _ribbon;

    public BackstageCommandIntegration(KryptonRibbon ribbon, ICommandManager commandManager)
    {
        _ribbon = ribbon;
        _commandManager = commandManager;
        SetupCommandPages();
    }

    private void SetupCommandPages()
    {
        // File operations page
        var fileOpsPage = new KryptonRibbonBackstagePage
        {
            Text = "File",
            ContentPanel = CreateFileOperationsPanel(),
            Visible = true
        };

        _ribbon.BackstagePages.Add(fileOpsPage);
    }

    private Control CreateFileOperationsPanel()
    {
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient,
            Padding = new Padding(20)
        };

        // Create buttons that execute application commands
        var newButton = CreateCommandButton("New Document", "File.New");
        var openButton = CreateCommandButton("Open...", "File.Open");
        var saveButton = CreateCommandButton("Save", "File.Save");
        var saveAsButton = CreateCommandButton("Save As...", "File.SaveAs");

        panel.Controls.AddRange(new[] { newButton, openButton, saveButton, saveAsButton });

        return panel;
    }

    private KryptonButton CreateCommandButton(string text, string commandId)
    {
        var button = new KryptonButton
        {
            Text = text,
            ButtonStyle = ButtonStyle.Standalone,
            Size = new Size(150, 35)
        };

        button.Click += (s, e) =>
        {
            _commandManager.ExecuteCommand(commandId);
            _ribbon.HideBackstage(); // Close backstage after command execution
        };

        // Enable/disable based on command state
        var command = _commandManager.GetCommand(commandId);
        if (command != null)
        {
            button.Enabled = command.CanExecute;
            command.CanExecuteChanged += (s, e) => button.Enabled = command.CanExecute;
        }

        return button;
    }
}
```

These examples demonstrate various ways to implement and customize the Krypton Ribbon Backstage View for different application scenarios.
