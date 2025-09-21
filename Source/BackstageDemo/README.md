# Krypton Ribbon Backstage View Demo

This demo showcases the new **Backstage View** functionality for the Krypton Ribbon, implementing the Office-style backstage interface introduced in Office 2010.

## 🎯 How to Test

1. **Run the Demo**: `dotnet run --project BackstageDemo.csproj`
2. **Click the Application Button** (File button in top-left corner)
3. **Navigate** between different backstage pages using the left navigation panel
4. **Close** the backstage using ESC key or the × button

## 🔧 How to Use in Your Applications

### Basic Setup

```csharp
// Add backstage pages to your ribbon
ribbon.BackstagePages.Add("New").TextTitle = "Create a new document";
ribbon.BackstagePages.Add("Open").TextTitle = "Open an existing document";
ribbon.BackstagePages.Add("Save").TextTitle = "Save your document";

// The app button will automatically show backstage when pages are available
```

### Custom Content

There are two ways to add controls to the backstage content area:

#### Method 1: Simple Text Content (Default)
```csharp
// For simple pages, just set the title and description
var openPage = ribbon.BackstagePages.Add("Open");
openPage.TextTitle = "Open an existing document";
openPage.TextDescription = "Browse for documents on your computer or online locations.";
// The backstage will automatically display this text with proper formatting
```

#### Method 2: Custom Content Panel (Recommended for Rich UI)
```csharp
// Create a page with custom content panel
var settingsPage = ribbon.BackstagePages.Add("Settings");

// Create a panel to hold your controls
var settingsPanel = new KryptonPanel { Dock = DockStyle.Fill };

// Add any Krypton controls you want
var titleLabel = new KryptonLabel
{
    Text = "Application Settings",
    Location = new Point(20, 20),
    StateCommon = { ShortText = { Font = new Font("Segoe UI", 16F, FontStyle.Bold) } }
};
settingsPanel.Controls.Add(titleLabel);

// Add checkboxes
var autoSaveCheck = new KryptonCheckBox
{
    Text = "Enable auto-save",
    Location = new Point(20, 70),
    Checked = true
};
settingsPanel.Controls.Add(autoSaveCheck);

// Add combo boxes
var themeCombo = new KryptonComboBox
{
    Location = new Point(20, 120),
    Size = new Size(200, 21)
};
themeCombo.Items.AddRange(new[] { "Theme1", "Theme2", "Theme3" });
settingsPanel.Controls.Add(themeCombo);

// Add buttons
var saveButton = new KryptonButton
{
    Text = "Save Settings",
    Location = new Point(20, 160),
    Size = new Size(120, 30)
};
saveButton.Click += (s, e) => MessageBox.Show("Settings saved!");
settingsPanel.Controls.Add(saveButton);

// Assign the panel to the page
settingsPage.ContentPanel = settingsPanel;
```

#### Available Krypton Controls for Backstage
You can add any Krypton controls to your backstage pages:
- `KryptonButton` - Buttons with theme integration
- `KryptonCheckBox` - Checkboxes 
- `KryptonRadioButton` - Radio buttons
- `KryptonTextBox` - Text input
- `KryptonComboBox` - Dropdown lists
- `KryptonListBox` - List controls
- `KryptonNumericUpDown` - Numeric input
- `KryptonDateTimePicker` - Date/time selection
- `KryptonGroupBox` - Grouping controls
- `KryptonPanel` - Container panels
- `KryptonLabel` - Text labels
- And many more!

### Programmatic Control

```csharp
// Show/hide backstage programmatically
ribbon.ShowBackstage();
ribbon.HideBackstage();
ribbon.ToggleBackstage();

// Control app button behavior
ribbon.UseBackstageForAppButton = true;  // Use backstage (default)
ribbon.UseBackstageForAppButton = false; // Use traditional app menu

// Check if backstage is visible
if (ribbon.BackstageVisible)
{
    // Backstage is currently shown
}
```

### Events

```csharp
// Handle backstage events
ribbon.BackstageOpening += (s, e) => { /* Handle opening */ };
ribbon.BackstageOpened += (s, e) => { /* Handle opened */ };
ribbon.BackstageClosing += (s, e) => { /* Handle closing, can cancel */ };
ribbon.BackstageClosed += (s, e) => { /* Handle closed */ };
ribbon.BackstagePageSelected += (s, e) => { /* Handle page selection */ };
```

## ✨ Features

- **Office-style Interface**: Dark navigation panel, light content area
- **Full Form Overlay**: Backstage covers the entire application area
- **App Button Integration**: Clicking File button opens backstage
- **Keyboard Support**: ESC key closes backstage
- **Custom Content**: Each page can host any WinForms controls
- **Event System**: Complete event handling for all interactions
- **Theme Integration**: Works with existing Krypton themes

## 🎨 Visual Design

The backstage view matches the Office 2010+ design:
- **Left Navigation**: Dark gray panel (83, 83, 83) with white text
- **Content Area**: Light gray panel (240, 240, 240) for page content
- **Close Button**: Red hover effect in top-right corner
- **Selected State**: Darker navigation button when page is active

This implementation provides a professional, Office-compatible backstage experience for Krypton Ribbon applications.
