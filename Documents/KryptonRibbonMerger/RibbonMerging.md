# Ribbon Merging Guide

## Overview

Ribbon merging allows you to dynamically combine ribbon controls, enabling plugin architectures where multiple modules can contribute tabs, groups, and contexts to a main application ribbon. This guide provides comprehensive documentation on how to use the ribbon merging functionality.

## Table of Contents

1. [Introduction](#introduction)
2. [Core Concepts](#core-concepts)
3. [Basic Usage](#basic-usage)
4. [Merge Behavior](#merge-behavior)
5. [Advanced Features](#advanced-features)
6. [API Reference](#api-reference)
7. [Best Practices](#best-practices)
8. [Troubleshooting](#troubleshooting)
9. [Performance Considerations](#performance-considerations)

## Introduction

### What is Ribbon Merging?

Ribbon merging is the process of combining ribbon controls by:
- Moving tabs from a source ribbon to a target ribbon
- Merging groups with matching names
- Merging items within groups with matching names
- Preserving the original structure for unmerging

### Why Use Ribbon Merging?

- **Plugin Architectures**: Each plugin can contribute its own ribbon tabs
- **Modular Applications**: Different modules can add functionality dynamically
- **Dynamic UI**: Load and unload features at runtime
- **Clean Separation**: Each module manages its own ribbon independently

## Core Concepts

### Source vs Target Ribbon

- **Target Ribbon**: The main application ribbon that receives merged items
- **Source Ribbon**: The plugin/module ribbon that provides items to merge

### Merge Operations

1. **Merge**: Combine source ribbon into target ribbon
2. **Unmerge**: Restore source ribbon by reversing the merge operation

### Item Tracking

The merger maintains a `HashSet<Component>` to track all merged items, enabling proper unmerging.

## Basic Usage

### Using Extension Methods (Recommended)

The simplest way to merge ribbons:

```csharp
using Krypton.Utilities;

// Merge a plugin ribbon into the main ribbon
mainRibbon.Merge(pluginRibbon);

// Later, unmerge when plugin is unloaded
mainRibbon.Unmerge(pluginRibbon);
```

### Using KryptonRibbonMerger Class

For more control or when reusing a merger instance:

```csharp
using Krypton.Utilities;

// Create a merger instance
var merger = new KryptonRibbonMerger(mainRibbon);

// Merge a plugin ribbon
merger.Merge(pluginRibbon);

// Fix group widths if needed
merger.FixGroupWidths();

// Later, unmerge
merger.Unmerge(pluginRibbon);
```

### Using CreateMerger Extension Method

```csharp
using Krypton.Utilities;

// Create merger using extension method
var merger = mainRibbon.CreateMerger();

// Use the merger
merger.Merge(pluginRibbon);
```

## Merge Behavior

### Tab Merging

**When tabs with the same name exist:**
- Groups from source tab are merged into existing tab
- If groups have matching names, their items are merged
- If groups don't exist, they are added to the existing tab

**When tabs don't exist:**
- Tab is moved from source to target ribbon
- Tab order can be controlled using `Tag` property

**Example:**
```csharp
// Source ribbon has "Image Editor" tab
// Target ribbon already has "Image Editor" tab
// Result: Groups from source are merged into existing tab
mainRibbon.Merge(pluginRibbon);
```

### Group Merging

**When groups with matching names exist:**
- Items from source group are merged into existing group
- Matching is based on `TextLine1` and `TextLine2` properties
- Items are inserted based on `Tag` property for ordering

**When groups don't exist:**
- Group is moved from source tab to target tab
- Group order can be controlled using `Tag` property

**Example:**
```csharp
// Source has group "Editing" with items [Crop, Rotate]
// Target has group "Editing" with items [Copy, Paste]
// Result: Target group has [Copy, Paste, Crop, Rotate]
mainRibbon.Merge(pluginRibbon);
```

### Item Merging

**When merging group items:**
- Items are moved from source group to target group
- Items are inserted based on `Tag` property
- Duplicate items (same reference) are skipped

**Example:**
```csharp
var sourceGroup = new KryptonRibbonGroup { TextLine1 = "Tools" };
var button1 = new KryptonRibbonGroupButton { TextLine1 = "Tool 1" };
button1.Tag = 0; // Insert at beginning
sourceGroup.Items.Add(new KryptonRibbonGroupTriple());
sourceGroup.Items[0].Items.Add(button1);

// Merge into target group
mainRibbon.Merge(pluginRibbon);
```

### Context Merging

**When contexts with matching names exist:**
- Context is skipped (not merged)
- Matching is based on `ContextTitle` property

**When contexts don't exist:**
- Context is moved from source to target ribbon
- Context order can be controlled using `Tag` property

**Example:**
```csharp
var context = new KryptonRibbonContext
{
    ContextTitle = "Image Editing"
};
context.Tag = 1; // Insert at position 1
pluginRibbon.RibbonContexts.Add(context);
mainRibbon.Merge(pluginRibbon);
```

## Advanced Features

### Controlling Merge Order

Use the `Tag` property to control the position of merged items:

```csharp
// Add tab at position 0 (first)
var tab = new KryptonRibbonTab { Text = "First Tab" };
tab.Tag = 0;
ribbon.RibbonTabs.Add(tab);

// Add group at position 2 (third)
var group = new KryptonRibbonGroup { TextLine1 = "Third Group" };
group.Tag = 2;
tab.Groups.Add(group);

// Add item at position 1 (second)
var button = new KryptonRibbonGroupButton { TextLine1 = "Second Button" };
button.Tag = 1;
group.Items[0].Items.Add(button);
```

**Tag Value Rules:**
- Numeric values (int, string that parses to int): Used as index
- `null`: Item is added at the end
- Invalid values: Item is added at the end

### Fixing Group Widths

After merging, groups with long names but little content may be clipped. Use `FixGroupWidths()`:

```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
merger.Merge(pluginRibbon);
merger.FixGroupWidths(); // Ensures proper group sizing
```

This method:
- Measures text width for each group
- Calculates minimum width based on DPI
- Sets `MinimumWidth` property on groups

### Preserving Selection

The merger automatically preserves the selected tab and context:

```csharp
// Before merge
mainRibbon.SelectedTab = mainRibbon.RibbonTabs[0];
mainRibbon.SelectedContext = "Design";

// After merge
mainRibbon.Merge(pluginRibbon);
// SelectedTab and SelectedContext are preserved
```

### Layout Refresh

The merger automatically refreshes layout:

```csharp
merger.Merge(pluginRibbon);
// Automatically calls:
// - TargetRibbon.PerformLayout()
// - TargetRibbon.Invalidate()
// - pluginRibbon.PerformLayout()
// - pluginRibbon.Invalidate()
```

## API Reference

### KryptonRibbonMerger Class

```csharp
public class KryptonRibbonMerger
{
    // Properties
    public KryptonRibbon TargetRibbon { get; }

    // Constructors
    public KryptonRibbonMerger(KryptonRibbon targetRibbon);

    // Methods
    public void Merge(KryptonRibbon? ribbon);
    public void Unmerge(KryptonRibbon? ribbon);
    public void FixGroupWidths();
}
```

### Extension Methods

```csharp
public static class KryptonRibbonExtensions
{
    // Merge source ribbon into target ribbon
    public static void Merge(
        this KryptonRibbon targetRibbon, 
        KryptonRibbon? sourceRibbon);

    // Unmerge source ribbon from target ribbon
    public static void Unmerge(
        this KryptonRibbon targetRibbon, 
        KryptonRibbon? sourceRibbon);

    // Create a merger instance
    public static KryptonRibbonMerger CreateMerger(
        this KryptonRibbon targetRibbon);
}
```

### Method Details

#### Merge Method

```csharp
public void Merge(KryptonRibbon? ribbon)
```

**Parameters:**
- `ribbon`: The source ribbon to merge. Can be `null` (no-op).

**Behavior:**
1. Preserves current selection (tab and context)
2. Merges tabs from source to target
3. Merges contexts from source to target
4. Refreshes layout on both ribbons
5. Restores selection
6. Tracks all merged items for unmerging

**Example:**
```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
merger.Merge(pluginRibbon);
```

#### Unmerge Method

```csharp
public void Unmerge(KryptonRibbon? ribbon)
```

**Parameters:**
- `ribbon`: The source ribbon to unmerge. Can be `null` (no-op).

**Behavior:**
1. Preserves current selection
2. Unmerges contexts from target to source
3. Unmerges tabs from target to source
4. Refreshes layout on both ribbons
5. Restores selection (or resets if tab no longer exists)

**Example:**
```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
merger.Unmerge(pluginRibbon);
```

#### FixGroupWidths Method

```csharp
public void FixGroupWidths()
```

**Behavior:**
- Measures text width for each group in each tab
- Calculates minimum width based on DPI scaling
- Sets `MinimumWidth` property to prevent clipping

**Example:**
```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
merger.Merge(pluginRibbon);
merger.FixGroupWidths();
```

## Best Practices

### 1. Always Unmerge Before Disposal

```csharp
// ✅ Good
public void UnloadPlugin(IPlugin plugin)
{
    mainRibbon.Unmerge(plugin.Ribbon);
    plugin.Dispose();
}

// ❌ Bad - Can cause issues
public void UnloadPlugin(IPlugin plugin)
{
    plugin.Dispose(); // Ribbon still merged!
}
```

### 2. Hide Source Ribbon After Merging

```csharp
// ✅ Good
pluginRibbon.Visible = false;
mainRibbon.Merge(pluginRibbon);

// ❌ Bad - Shows duplicate ribbon
mainRibbon.Merge(pluginRibbon);
```

### 3. Use Tag Property for Ordering

```csharp
// ✅ Good - Explicit ordering
tab.Tag = 0; // First position
group.Tag = 2; // Third position

// ❌ Bad - Unpredictable order
// No Tag set, items added at end
```

### 4. Reuse Merger Instance

```csharp
// ✅ Good - Reuse merger
var merger = mainRibbon.CreateMerger();
merger.Merge(plugin1Ribbon);
merger.Merge(plugin2Ribbon);
merger.Unmerge(plugin1Ribbon);

// ❌ Bad - Creates new merger each time
mainRibbon.Merge(plugin1Ribbon);
mainRibbon.Merge(plugin2Ribbon);
```

### 5. Handle Null Ribbons

```csharp
// ✅ Good - Null-safe
if (pluginRibbon != null)
{
    mainRibbon.Merge(pluginRibbon);
}

// Extension methods handle null automatically
mainRibbon.Merge(pluginRibbon); // Safe even if pluginRibbon is null
```

### 6. Fix Group Widths After Merging

```csharp
// ✅ Good
var merger = mainRibbon.CreateMerger();
merger.Merge(pluginRibbon);
merger.FixGroupWidths(); // Prevents clipping

// ❌ Bad - Groups may be clipped
mainRibbon.Merge(pluginRibbon);
```

## Troubleshooting

### Issue: Tabs Not Appearing After Merge

**Symptoms:** Merged tabs don't appear in target ribbon.

**Possible Causes:**
1. Source ribbon has no tabs
2. Ribbon is disposed
3. Layout not refreshed

**Solutions:**
```csharp
// Check tabs exist
if (pluginRibbon.RibbonTabs.Count > 0)
{
    mainRibbon.Merge(pluginRibbon);
}

// Ensure not disposed
if (!pluginRibbon.IsDisposed)
{
    mainRibbon.Merge(pluginRibbon);
}

// Force layout refresh
mainRibbon.Merge(pluginRibbon);
mainRibbon.PerformLayout();
mainRibbon.Invalidate();
```

### Issue: Groups Not Merging Correctly

**Symptoms:** Groups with same name create duplicates instead of merging.

**Possible Causes:**
1. Group names don't match exactly (case-sensitive)
2. `TextLine1` or `TextLine2` differ

**Solutions:**
```csharp
// Ensure exact match
var group = new KryptonRibbonGroup
{
    TextLine1 = "Editing",  // Must match exactly
    TextLine2 = ""         // Must match exactly
};
```

### Issue: Items Not Merging Into Groups

**Symptoms:** Items don't appear in merged groups.

**Possible Causes:**
1. Group structure mismatch
2. Items not properly added to containers

**Solutions:**
```csharp
// Ensure proper structure
var group = new KryptonRibbonGroup { TextLine1 = "Tools" };
var triple = new KryptonRibbonGroupTriple();
var button = new KryptonRibbonGroupButton { TextLine1 = "Tool" };
triple.Items.Add(button);
group.Items.Add(triple);
```

### Issue: Unmerge Not Working

**Symptoms:** Items not restored to source ribbon after unmerge.

**Possible Causes:**
1. Source ribbon disposed
2. Items not tracked properly
3. Unmerge called multiple times

**Solutions:**
```csharp
// Ensure source ribbon exists
if (!pluginRibbon.IsDisposed)
{
    mainRibbon.Unmerge(pluginRibbon);
}

// Don't unmerge twice
if (loadedPlugins.Contains(plugin))
{
    mainRibbon.Unmerge(plugin.Ribbon);
    loadedPlugins.Remove(plugin);
}
```

### Issue: Performance Problems

**Symptoms:** Slow merge/unmerge operations.

**Possible Causes:**
1. Too many items
2. Frequent merge/unmerge operations
3. Layout refresh overhead

**Solutions:**
```csharp
// Batch operations
SuspendLayout();
foreach (var plugin in plugins)
{
    mainRibbon.Merge(plugin.Ribbon);
}
ResumeLayout();

// Reuse merger instance
var merger = mainRibbon.CreateMerger();
foreach (var plugin in plugins)
{
    merger.Merge(plugin.Ribbon);
}
```

## Performance Considerations

### Merge Performance

- **Time Complexity**: O(n) where n is the number of items to merge
- **Space Complexity**: O(n) for tracking merged items
- **Layout Refresh**: Can be expensive for large ribbons

### Optimization Tips

1. **Batch Operations**: Merge multiple plugins at once
2. **Suspend Layout**: Use `SuspendLayout()`/`ResumeLayout()` for multiple operations
3. **Reuse Merger**: Create one merger instance and reuse it
4. **Lazy Merging**: Only merge when plugins are actually loaded

### Example: Optimized Merging

```csharp
public void LoadPlugins(IEnumerable<IPlugin> plugins)
{
    var merger = mainRibbon.CreateMerger();
    
    // Suspend layout for batch operations
    mainRibbon.SuspendLayout();
    
    try
    {
        foreach (var plugin in plugins)
        {
            var control = plugin.CreateControl();
            if (control is IPluginUserControl pluginControl)
            {
                pluginControl.Ribbon.Visible = false;
                merger.Merge(pluginControl.Ribbon);
                loadedPlugins.Add(plugin, pluginControl);
            }
        }
        
        // Fix widths once after all merges
        merger.FixGroupWidths();
    }
    finally
    {
        mainRibbon.ResumeLayout();
        mainRibbon.PerformLayout();
    }
}
```

## Summary

Ribbon merging provides a powerful mechanism for building dynamic, plugin-based applications. By understanding merge behavior, using best practices, and following the patterns outlined in this guide, you can create robust, maintainable plugin architectures.

Key takeaways:
- Use extension methods for simple cases
- Use `KryptonRibbonMerger` class for advanced scenarios
- Always unmerge before disposal
- Use `Tag` property for ordering
- Fix group widths after merging
- Handle null values appropriately

