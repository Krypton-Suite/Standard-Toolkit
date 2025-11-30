# Krypton Ribbon Backstage View Documentation

## Overview

This directory contains comprehensive documentation for the Krypton Ribbon Backstage View feature, introduced to provide Office-style full-screen application interfaces.

## Documentation Files

### [BackstageView.md](BackstageView.md)
**Main documentation** - Complete guide covering:
- Feature overview and architecture
- Getting started tutorial
- Usage scenarios and best practices
- Theming and customization
- Troubleshooting guide

### [BackstageAPI.md](BackstageAPI.md)  
**API Reference** - Detailed documentation of:
- KryptonRibbon backstage properties and methods
- KryptonRibbonBackstagePage class
- KryptonRibbonBackstagePageCollection class
- BackstagePageEventArgs class
- Event handling and lifecycle

### [BackstageExamples.md](BackstageExamples.md)
**Code Examples** - Practical examples including:
- Basic setup and configuration
- Simple text-based pages
- Custom content with UserControls
- Dynamic content updates
- Advanced event handling scenarios

### [BackstageDesigner.md](BackstageDesigner.md)
**Designer Guide** - Visual Studio integration:
- Smart tags and property editors
- Collection editor usage
- Design-time best practices
- Custom UserControl integration
- Troubleshooting designer issues

## Quick Start

### 1. Enable Backstage
```csharp
kryptonRibbon1.UseBackstageForAppButton = true;
```

### 2. Add Pages

**Simple Text Pages:**
```csharp
kryptonRibbon1.AddBackstagePage("Open", "Open Files", "Browse and open documents.");
```

**UserControl Pages (Recommended):**
```csharp
var settingsPage = new MySettingsUserControl();
kryptonRibbon1.AddBackstagePage("Settings", settingsPage);
```

**Traditional Approach:**
```csharp
kryptonRibbon1.BackstagePages.Add(new KryptonRibbonBackstagePage 
{ 
    Text = "Open", 
    TextTitle = "Open Files",
    TextDescription = "Browse and open documents."
});
```

### 3. Handle Events
```csharp
kryptonRibbon1.BackstagePageSelected += (s, e) => 
{
    Console.WriteLine($"Selected: {e.Page?.Text}");
};
```

## Key Features

✅ **Office-Style Interface** - Full-screen backstage overlay  
✅ **UserControl Integration** - Clean separation with UserControl pages  
✅ **Enhanced Designer Support** - Smart tags, collection editors, expandable objects  
✅ **Theme Integration** - Automatic Krypton palette support  
✅ **Dynamic Management** - Add/remove pages at runtime with auto-refresh  
✅ **Multiple Content Types** - UserControls, panels, or simple text  
✅ **Event System** - Rich event handling for page lifecycle  
✅ **Keyboard Navigation** - Full accessibility support  

## Architecture

The backstage view uses a `KryptonSplitContainer` with:
- **Left Panel (500px)** - Navigation buttons with `PaletteBackStyle.PanelAlternate`
- **Right Panel** - Content area with `PaletteBackStyle.PanelClient`
- **Fixed Splitter** - Non-resizable for consistent layout

## Browser Support

| Component | Designer | Runtime | Theming |
|-----------|----------|---------|---------|
| KryptonRibbon | ✅ | ✅ | ✅ |
| BackstagePages | ✅ | ✅ | ✅ |
| Custom Content | ✅ | ✅ | ⚠️* |

*Custom content requires Krypton controls for full theme integration

## Version History

- **v100.25.9.262** - Initial backstage implementation with basic functionality
- **v100.25.9.263** - Added KryptonSplitContainer architecture and theme integration
- **v100.25.9.264** - Enhanced designer support and comprehensive documentation
- **v100.25.9.265** - UserControl integration, expandable objects, and dynamic management

## Contributing

When contributing to the backstage feature:

1. **Follow Krypton patterns** - Use existing palette and theming systems
2. **Maintain compatibility** - Support all target frameworks (.NET 4.7.2+)
3. **Include tests** - Add scenarios to TestForm or BackstageDemo
4. **Update documentation** - Keep all docs current with changes
5. **Designer support** - Ensure Visual Studio integration works

## Related Components

- **KryptonRibbon** - Main ribbon control
- **KryptonSplitContainer** - Layout foundation  
- **KryptonPanel** - Themed container controls
- **KryptonButton** - Navigation and action buttons
- **KryptonLabel** - Themed text display

## Support

For questions and issues:

1. **Check documentation** - Most scenarios are covered in the guides
2. **Review examples** - BackstageExamples.md has extensive code samples
3. **Test with BackstageDemo** - Use the demo app for reference
4. **GitHub Issues** - Report bugs and feature requests
5. **Community Forum** - General usage questions

## License

The backstage view feature is part of the Krypton Standard Toolkit and follows the same BSD 3-Clause License as the main project.

---

*Last Updated: September 2025*  
*Version: 100.25.9.264-alpha*
