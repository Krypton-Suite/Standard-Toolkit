# Krypton Ribbon Merger

This module provides functionality for merging and unmerging Krypton Ribbon controls, enabling dynamic ribbon management in plugin architectures and modular applications.

## Features

- **Ribbon Merging**: Merge tabs, groups, and contexts from one ribbon into another
- **Ribbon Unmerging**: Reverse merge operations to restore original ribbon state
- **UserControl Hosting**: The KryptonRibbon can be hosted directly on UserControl instances (fully supported)
- **Extension Methods**: Convenient extension methods for easy ribbon merging
- **Built-in Support**: All functionality is built into the Krypton.Utilities library

## Documentation

For detailed developer documentation, see:

- **[UserControl Hosting Guide](UserControlHosting.md)** - Complete guide to hosting KryptonRibbon on UserControls
- **[Ribbon Merging Guide](RibbonMerging.md)** - Comprehensive guide to merging and unmerging ribbons
- **[API Reference](APIReference.md)** - Complete API documentation

## Quick Start

### UserControl Hosting

```csharp
using Krypton.Ribbon;

public class PluginUserControl : UserControl
{
    private readonly KryptonRibbon ribbon;

    public PluginUserControl()
    {
        ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        
        // Configure ribbon tabs and groups...
        Controls.Add(ribbon);
    }

    public KryptonRibbon Ribbon => ribbon;
}
```

### Ribbon Merging

```csharp
using Krypton.Utilities;

// Merge a plugin ribbon into the main ribbon
mainRibbon.Merge(pluginRibbon);

// Later, unmerge when plugin is unloaded
mainRibbon.Unmerge(pluginRibbon);
```

## Examples

See the comprehensive demo in `TestForm/RibbonMergerDemo.cs` for a complete working example of both features.

## Support

For issues, questions, or contributions, please visit the [Krypton Suite GitHub repository](https://github.com/Krypton-Suite/Standard-Toolkit).

