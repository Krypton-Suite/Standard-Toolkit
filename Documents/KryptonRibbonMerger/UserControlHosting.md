# UserControl Hosting Guide

## Overview

The `KryptonRibbon` control **fully supports** hosting on `UserControl` instances. This capability is essential for building plugin architectures where each plugin module can define its own ribbon interface independently, without requiring dummy forms or complex workarounds.

## Table of Contents

1. [Why UserControl Hosting?](#why-usercontrol-hosting)
2. [Technical Foundation](#technical-foundation)
3. [Basic Implementation](#basic-implementation)
4. [Advanced Patterns](#advanced-patterns)
5. [Best Practices](#best-practices)
6. [Troubleshooting](#troubleshooting)
7. [Architecture Considerations](#architecture-considerations)

## Why UserControl Hosting?

### The Problem

In traditional plugin architectures, plugins need to contribute UI elements (like ribbon tabs) to the main application. Without UserControl hosting support, developers often resort to:

- Creating dummy forms just to expose ribbon controls
- Complex communication mechanisms between forms and UserControls
- Boilerplate code to facilitate form-to-control communication
- Difficult lifecycle management

### The Solution

With UserControl hosting, plugins can:

1. **Define ribbons directly on UserControls** - No dummy forms needed
2. **Self-contained plugin modules** - Each plugin manages its own ribbon UI
3. **Simplified architecture** - Direct property exposure for merging
4. **Better lifecycle management** - UserControl and ribbon share the same lifecycle
5. **Cleaner code** - Less boilerplate, more maintainable

## Technical Foundation

### Inheritance Hierarchy

```
Control (System.Windows.Forms)
  └── VisualControlBase (Krypton.Toolkit)
      └── VisualControl (Krypton.Toolkit)
          └── VisualSimple (Krypton.Toolkit)
              └── KryptonRibbon (Krypton.Ribbon)
```

Since `KryptonRibbon` ultimately inherits from `Control`, it can be added to any container control, including `UserControl`.

### Docking Behavior

The `KryptonRibbon` has the attribute `[Docking(DockingBehavior.Never)]`, which:

- **Prevents docking in the designer** - This is a design-time restriction
- **Does NOT prevent hosting** - The ribbon can still be added to UserControls programmatically
- **Allows manual docking** - You can set `Dock = DockStyle.Top` in code

### Key Properties

```csharp
public class KryptonRibbon : VisualSimple
{
    public bool AutoSize { get; set; }
    public AutoSizeMode AutoSizeMode { get; set; }
    public DockStyle Dock { get; set; }  // Can be set to DockStyle.Top
    public KryptonRibbonTabCollection RibbonTabs { get; }
    public KryptonRibbonContextCollection RibbonContexts { get; }
}
```

## Basic Implementation

### Step 1: Create the UserControl

```csharp
using Krypton.Ribbon;
using Krypton.Toolkit;

namespace MyApplication.Plugins
{
    public partial class MyPluginUserControl : UserControl
    {
        private readonly KryptonRibbon ribbon;

        public MyPluginUserControl()
        {
            InitializeComponent();
            InitializeRibbon();
        }

        private void InitializeRibbon()
        {
            // Create ribbon instance
            ribbon = new KryptonRibbon
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Configure ribbon tabs and groups
            ConfigureRibbonContent();

            // Add ribbon to UserControl
            Controls.Add(ribbon);
        }

        private void ConfigureRibbonContent()
        {
            // Create a tab
            var tab = new KryptonRibbonTab { Text = "Plugin Features" };
            
            // Create a group
            var group = new KryptonRibbonGroup { TextLine1 = "Actions" };
            
            // Add buttons to group
            var button = new KryptonRibbonGroupButton
            {
                TextLine1 = "Do Something"
            };
            button.Click += (s, e) => OnDoSomething();
            
            var triple = new KryptonRibbonGroupTriple();
            triple.Items.Add(button);
            group.Items.Add(triple);
            
            // Add group to tab
            tab.Groups.Add(group);
            
            // Add tab to ribbon
            ribbon.RibbonTabs.Add(tab);
        }

        // Expose ribbon for merging
        public KryptonRibbon Ribbon => ribbon;

        private void OnDoSomething()
        {
            // Handle button click
        }
    }
}
```

### Step 2: Use in Main Application

```csharp
public class MainForm : KryptonForm
{
    private readonly KryptonRibbon mainRibbon;
    private MyPluginUserControl? pluginControl;

    public MainForm()
    {
        InitializeComponent();
        
        // Create main ribbon
        mainRibbon = new KryptonRibbon
        {
            Dock = DockStyle.Top
        };
        Controls.Add(mainRibbon);
    }

    public void LoadPlugin()
    {
        // Create plugin UserControl
        pluginControl = new MyPluginUserControl();
        
        // Hide ribbon on UserControl (it will be merged)
        pluginControl.Ribbon.Visible = false;
        
        // Merge plugin ribbon into main ribbon
        mainRibbon.Merge(pluginControl.Ribbon);
        
        // Add UserControl to form
        pluginControl.Dock = DockStyle.Fill;
        Controls.Add(pluginControl);
    }

    public void UnloadPlugin()
    {
        if (pluginControl != null)
        {
            // Unmerge ribbon
            mainRibbon.Unmerge(pluginControl.Ribbon);
            
            // Remove and dispose
            Controls.Remove(pluginControl);
            pluginControl.Dispose();
            pluginControl = null;
        }
    }
}
```

## Advanced Patterns

### Pattern 1: Interface-Based Plugin Architecture

Define an interface for plugins that expose ribbons:

```csharp
public interface IPluginUserControl
{
    KryptonRibbon? Ribbon { get; }
    string PluginName { get; }
    void Initialize();
    void Cleanup();
}

public class ImageEditorPlugin : UserControl, IPluginUserControl
{
    private readonly KryptonRibbon ribbon;

    public ImageEditorPlugin()
    {
        InitializeComponent();
        ribbon = CreateRibbon();
        Controls.Add(ribbon);
    }

    public KryptonRibbon Ribbon => ribbon;
    public string PluginName => "Image Editor";

    public void Initialize()
    {
        // Plugin initialization logic
    }

    public void Cleanup()
    {
        // Plugin cleanup logic
    }

    private KryptonRibbon CreateRibbon()
    {
        // Ribbon creation logic
    }
}
```

### Pattern 2: Plugin Manager

Create a centralized plugin manager:

```csharp
public class PluginManager
{
    private readonly KryptonRibbon mainRibbon;
    private readonly Dictionary<string, IPluginUserControl> loadedPlugins = new();

    public PluginManager(KryptonRibbon mainRibbon)
    {
        this.mainRibbon = mainRibbon;
    }

    public void LoadPlugin(IPluginUserControl plugin)
    {
        if (loadedPlugins.ContainsKey(plugin.PluginName))
        {
            throw new InvalidOperationException($"Plugin '{plugin.PluginName}' is already loaded.");
        }

        plugin.Initialize();
        plugin.Ribbon.Visible = false;
        mainRibbon.Merge(plugin.Ribbon);
        loadedPlugins[plugin.PluginName] = plugin;
    }

    public void UnloadPlugin(string pluginName)
    {
        if (!loadedPlugins.TryGetValue(pluginName, out var plugin))
        {
            return;
        }

        mainRibbon.Unmerge(plugin.Ribbon);
        plugin.Cleanup();
        loadedPlugins.Remove(pluginName);
    }

    public void UnloadAll()
    {
        var pluginNames = loadedPlugins.Keys.ToArray();
        foreach (var name in pluginNames)
        {
            UnloadPlugin(name);
        }
    }
}
```

### Pattern 3: Lazy Ribbon Creation

Create ribbon only when needed:

```csharp
public class LazyPluginUserControl : UserControl
{
    private KryptonRibbon? ribbon;

    public KryptonRibbon Ribbon
    {
        get
        {
            if (ribbon == null)
            {
                ribbon = CreateRibbon();
                ribbon.Visible = false;
                Controls.Add(ribbon);
            }
            return ribbon;
        }
    }

    private KryptonRibbon CreateRibbon()
    {
        var ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        
        // Configure ribbon...
        return ribbon;
    }
}
```

## Best Practices

### 1. Ribbon Visibility Management

**Always hide the ribbon on the UserControl after merging:**

```csharp
// ✅ Good
pluginControl.Ribbon.Visible = false;
mainRibbon.Merge(pluginControl.Ribbon);

// ❌ Bad - Shows duplicate ribbon
mainRibbon.Merge(pluginControl.Ribbon);
```

### 2. Lifecycle Management

**Ensure proper cleanup:**

```csharp
protected override void OnFormClosing(FormClosingEventArgs e)
{
    // Unmerge all plugins before closing
    foreach (var plugin in loadedPlugins.Values)
    {
        mainRibbon.Unmerge(plugin.Ribbon);
        plugin.Dispose();
    }
    base.OnFormClosing(e);
}
```

### 3. Ribbon Configuration

**Set proper docking and sizing:**

```csharp
ribbon = new KryptonRibbon
{
    Dock = DockStyle.Top,              // Dock at top
    AutoSize = true,                   // Auto-size enabled
    AutoSizeMode = AutoSizeMode.GrowAndShrink  // Grow and shrink as needed
};
```

### 4. Content Organization

**Keep ribbon content separate from UserControl content:**

```csharp
public class PluginUserControl : UserControl
{
    private readonly KryptonRibbon ribbon;
    private readonly Panel contentPanel;

    public PluginUserControl()
    {
        // Ribbon at top
        ribbon = new KryptonRibbon { Dock = DockStyle.Top };
        Controls.Add(ribbon);

        // Content panel below ribbon
        contentPanel = new Panel { Dock = DockStyle.Fill };
        Controls.Add(contentPanel);
    }
}
```

### 5. Thread Safety

**Always access UI controls on the UI thread:**

```csharp
private void UpdateRibbonFromBackgroundThread()
{
    if (ribbon.InvokeRequired)
    {
        ribbon.Invoke(new Action(UpdateRibbonFromBackgroundThread));
        return;
    }
    
    // Update ribbon here
}
```

## Troubleshooting

### Issue: Ribbon Not Visible After Merging

**Symptoms:** Ribbon tabs don't appear in main ribbon after merge.

**Solutions:**
1. Ensure ribbon has tabs before merging:
   ```csharp
   if (pluginRibbon.RibbonTabs.Count > 0)
   {
       mainRibbon.Merge(pluginRibbon);
   }
   ```

2. Check that ribbon is not disposed:
   ```csharp
   if (!pluginRibbon.IsDisposed)
   {
       mainRibbon.Merge(pluginRibbon);
   }
   ```

3. Verify merge was successful:
   ```csharp
   mainRibbon.Merge(pluginRibbon);
   mainRibbon.PerformLayout();
   mainRibbon.Invalidate();
   ```

### Issue: Duplicate Ribbons Visible

**Symptoms:** Both UserControl ribbon and merged ribbon are visible.

**Solution:** Hide the UserControl ribbon after merging:
```csharp
pluginRibbon.Visible = false;
mainRibbon.Merge(pluginRibbon);
```

### Issue: Ribbon Not Docking Properly

**Symptoms:** Ribbon doesn't dock correctly on UserControl.

**Solution:** Set docking properties explicitly:
```csharp
ribbon.Dock = DockStyle.Top;
ribbon.AutoSize = true;
ribbon.AutoSizeMode = AutoSizeMode.GrowAndShrink;
```

### Issue: Memory Leaks

**Symptoms:** Memory usage increases when loading/unloading plugins.

**Solution:** Ensure proper disposal:
```csharp
public void UnloadPlugin(IPluginUserControl plugin)
{
    mainRibbon.Unmerge(plugin.Ribbon);
    
    // Dispose the UserControl (which will dispose the ribbon)
    if (plugin is IDisposable disposable)
    {
        disposable.Dispose();
    }
}
```

## Architecture Considerations

### Separation of Concerns

- **UserControl**: Manages plugin UI and content
- **Ribbon**: Manages plugin ribbon interface
- **Main Application**: Manages ribbon merging and plugin lifecycle

### Plugin Isolation

Each plugin should be:
- **Self-contained**: All its UI elements in one UserControl
- **Independent**: No dependencies on other plugins
- **Disposable**: Properly clean up resources

### Performance

- **Lazy Loading**: Create ribbons only when plugins are loaded
- **Efficient Merging**: Merge only when necessary
- **Resource Management**: Dispose unused plugins promptly

### Extensibility

Design for:
- **Dynamic Loading**: Plugins loaded at runtime
- **Hot Swapping**: Plugins can be loaded/unloaded without restart
- **Versioning**: Support different plugin versions

## Example: Complete Plugin Architecture

```csharp
// Plugin Interface
public interface IPlugin
{
    string Name { get; }
    string Version { get; }
    UserControl CreateControl();
    void Initialize();
    void Shutdown();
}

// Plugin Base Class
public abstract class PluginBase : IPlugin
{
    public abstract string Name { get; }
    public abstract string Version { get; }
    
    public abstract UserControl CreateControl();
    
    public virtual void Initialize() { }
    public virtual void Shutdown() { }
}

// Concrete Plugin Implementation
public class ImageEditorPlugin : PluginBase
{
    public override string Name => "Image Editor";
    public override string Version => "1.0.0";

    public override UserControl CreateControl()
    {
        return new ImageEditorPluginControl();
    }
}

// Plugin Control with Ribbon
public class ImageEditorPluginControl : UserControl, IPluginUserControl
{
    private readonly KryptonRibbon ribbon;

    public ImageEditorPluginControl()
    {
        InitializeComponent();
        ribbon = CreateRibbon();
        Controls.Add(ribbon);
    }

    public KryptonRibbon Ribbon => ribbon;

    private KryptonRibbon CreateRibbon()
    {
        // Ribbon creation logic
    }
}

// Main Application
public class MainForm : KryptonForm
{
    private readonly KryptonRibbon mainRibbon;
    private readonly PluginManager pluginManager;

    public MainForm()
    {
        InitializeComponent();
        mainRibbon = new KryptonRibbon { Dock = DockStyle.Top };
        Controls.Add(mainRibbon);
        pluginManager = new PluginManager(mainRibbon);
    }

    public void LoadPlugin(IPlugin plugin)
    {
        var control = plugin.CreateControl();
        if (control is IPluginUserControl pluginControl)
        {
            plugin.Initialize();
            pluginManager.LoadPlugin(pluginControl);
            // Add control to form...
        }
    }
}
```

## Summary

UserControl hosting enables clean, maintainable plugin architectures by allowing ribbons to be defined directly on UserControls. This eliminates the need for dummy forms and simplifies plugin development while maintaining full functionality of the KryptonRibbon control.

