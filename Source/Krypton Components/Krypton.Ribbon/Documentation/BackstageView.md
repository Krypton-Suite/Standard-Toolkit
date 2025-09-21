# Krypton Ribbon Backstage View

## Overview

The Krypton Ribbon Backstage View provides an Office-style full-screen overlay interface that appears when the application button is clicked. It offers a professional way to display application-level commands, settings, and information pages.

![Backstage View Example](../Assets/backstage-example.png)

## Features

- **Full-screen overlay** that covers the entire form
- **Navigation panel** with themed buttons for page selection
- **Content area** for displaying page content or custom controls
- **Automatic theme integration** with Krypton palette system
- **Designer support** in Visual Studio
- **Event-driven architecture** for page selection and lifecycle events
- **Custom content support** via UserControls or panels

## Architecture

The backstage view is built using several key components:

### Core Classes

- **`KryptonRibbonBackstagePage`** - Represents individual pages in the backstage
- **`KryptonRibbonBackstagePageCollection`** - Manages the collection of pages
- **`VisualPopupBackstage`** - The main backstage UI implementation
- **`BackstagePageEventArgs`** - Event arguments for page selection events

### Layout Structure

```
VisualPopupBackstage (KryptonSplitContainer)
├── Panel1 (Navigation - 500px wide)
│   ├── Navigation Buttons (KryptonButton)
│   └── Back Button
└── Panel2 (Content Area)
    └── Selected Page Content
```

## Getting Started

### 1. Enable Backstage in Ribbon

```csharp
// Enable backstage for the application button
kryptonRibbon1.UseBackstageForAppButton = true;
```

### 2. Add Backstage Pages

```csharp
// Create and add pages programmatically
var openPage = new KryptonRibbonBackstagePage
{
    Text = "Open",
    TextTitle = "Open an existing document",
    TextDescription = "Browse and open files from your computer or cloud storage.",
    Visible = true,
    Enabled = true
};

kryptonRibbon1.BackstagePages.Add(openPage);
```

### 3. Handle Events

```csharp
// Subscribe to backstage events
kryptonRibbon1.BackstageOpening += OnBackstageOpening;
kryptonRibbon1.BackstageOpened += OnBackstageOpened;
kryptonRibbon1.BackstageClosing += OnBackstageClosing;
kryptonRibbon1.BackstageClosed += OnBackstageClosed;
kryptonRibbon1.BackstagePageSelected += OnBackstagePageSelected;

private void OnBackstagePageSelected(object sender, BackstagePageEventArgs e)
{
    Console.WriteLine($"Selected page: {e.Page?.Text}");
}
```

## Usage Scenarios

### Simple Text Pages

For basic informational pages, use the built-in text properties:

```csharp
var aboutPage = new KryptonRibbonBackstagePage
{
    Text = "About",
    TextTitle = "About MyApplication",
    TextDescription = "Version 1.0.0\nCopyright © 2025 My Company",
    Visible = true
};
```

### Custom Content Pages

For complex layouts, provide a custom UserControl:

```csharp
var settingsPage = new KryptonRibbonBackstagePage
{
    Text = "Settings",
    ContentPanel = new SettingsUserControl(),
    Visible = true
};
```

### Dynamic Content

Update page content dynamically:

```csharp
private void UpdateRecentFiles()
{
    var recentPanel = new KryptonPanel();
    
    foreach (var file in GetRecentFiles())
    {
        var button = new KryptonButton
        {
            Text = file.Name,
            Tag = file,
            ButtonStyle = ButtonStyle.ListItem
        };
        button.Click += OnRecentFileClick;
        recentPanel.Controls.Add(button);
    }
    
    recentFilesPage.ContentPanel = recentPanel;
}
```

## Theming

The backstage view automatically integrates with the Krypton theme system:

- **Navigation Panel**: Uses `PaletteBackStyle.PanelAlternate`
- **Content Panel**: Uses `PaletteBackStyle.PanelClient`
- **Navigation Buttons**: Use `ButtonStyle.NavigatorStack`
- **Content Labels**: Use `LabelStyle.TitlePanel` and `LabelStyle.NormalPanel`

## Best Practices

### Page Organization

1. **Limit pages** - Keep to 8-10 pages maximum for usability
2. **Group related functions** - Use logical groupings (File, Settings, Help)
3. **Order by frequency** - Place most-used pages at the top
4. **Use clear names** - Short, descriptive page names

### Content Design

1. **Consistent layout** - Use similar layouts across pages
2. **Proper spacing** - Use adequate padding and margins
3. **Visual hierarchy** - Use title and description text appropriately
4. **Responsive design** - Consider different screen sizes

### Performance

1. **Lazy loading** - Create complex content only when needed
2. **Dispose properly** - Clean up resources in UserControls
3. **Minimal overhead** - Don't load all content at startup

## Advanced Topics

### Custom Navigation Styling

While the navigation uses built-in theming, you can customize button appearance:

```csharp
// Access navigation buttons after backstage creation
private void CustomizeNavigationButtons()
{
    // This would require extending the VisualPopupBackstage class
    // or adding customization properties to KryptonRibbonBackstagePage
}
```

### Integration with Application State

```csharp
private void OnBackstageOpening(object sender, CancelEventArgs e)
{
    // Update dynamic content before showing
    UpdateRecentFiles();
    UpdateUserInfo();
    
    // Cancel opening if needed
    if (SomeCondition)
    {
        e.Cancel = true;
    }
}
```

### Keyboard Navigation

The backstage supports keyboard navigation:

- **Tab/Shift+Tab** - Navigate between buttons
- **Enter/Space** - Activate selected button
- **Escape** - Close backstage

## Troubleshooting

### Common Issues

**Q: Backstage doesn't appear when clicking app button**
A: Ensure `UseBackstageForAppButton = true` and at least one page exists

**Q: Custom content doesn't display**
A: Verify the `ContentPanel` property is set and the control is properly initialized

**Q: Theme colors don't match**
A: Check that you're using Krypton controls in custom content panels

**Q: Events not firing**
A: Ensure event handlers are attached before showing the backstage

### Debugging

Enable debug output to trace backstage operations:

```csharp
// Add to application startup
System.Diagnostics.Debug.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
```

## Migration Notes

If upgrading from a custom backstage implementation:

1. **Replace custom overlays** with `KryptonRibbonBackstagePage`
2. **Update event handling** to use the new event args
3. **Migrate content** to use `ContentPanel` property
4. **Update theming** to use Krypton palette styles

## See Also

- [API Reference](BackstageAPI.md)
- [Examples](BackstageExamples.md)
- [Designer Guide](BackstageDesigner.md)
- [Migration Guide](BackstageMigration.md)
