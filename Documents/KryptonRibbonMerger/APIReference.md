# API Reference

Complete API documentation for the Krypton Ribbon Merger functionality.

## Namespace

```csharp
using Krypton.Utilities;
```

## Classes

### KryptonRibbonMerger

The core class for merging and unmerging ribbons.

#### Properties

##### TargetRibbon

```csharp
public KryptonRibbon TargetRibbon { get; }
```

Gets the target ribbon that will receive the merged items.

**Type:** `Krypton.Ribbon.KryptonRibbon`

**Remarks:**
- Set in constructor
- Cannot be changed after construction
- All merge/unmerge operations affect this ribbon

**Example:**
```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
KryptonRibbon target = merger.TargetRibbon; // Returns mainRibbon
```

#### Constructors

##### KryptonRibbonMerger(KryptonRibbon)

```csharp
public KryptonRibbonMerger(KryptonRibbon targetRibbon)
```

Initializes a new instance of the `KryptonRibbonMerger` class.

**Parameters:**
- `targetRibbon` (`KryptonRibbon`): The target ribbon that will receive items from merged ribbons.

**Exceptions:**
- `ArgumentNullException`: Thrown when `targetRibbon` is `null`.

**Example:**
```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
```

#### Methods

##### Merge(KryptonRibbon?)

```csharp
public void Merge(KryptonRibbon? ribbon)
```

Merges the specified ribbon into the target ribbon.

**Parameters:**
- `ribbon` (`KryptonRibbon?`): The ribbon to merge. Can be `null` (no-op).

**Remarks:**
- Merges tabs, groups, and contexts from source to target
- Preserves selected tab and context
- Refreshes layout automatically
- Tracks merged items for unmerging
- If `ribbon` is `null`, method returns without doing anything

**Behavior:**
1. Preserves current selection (tab and context)
2. Merges tabs:
   - If tab with same name exists: merges groups
   - If tab doesn't exist: moves tab to target
3. Merges contexts:
   - If context with same name exists: skips
   - If context doesn't exist: moves context to target
4. Refreshes layout on both ribbons
5. Restores selection

**Example:**
```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
merger.Merge(pluginRibbon);
```

**See Also:**
- [Unmerge Method](#unmergekryptonribbon)
- [Merge Behavior Documentation](RibbonMerging.md#merge-behavior)

##### Unmerge(KryptonRibbon?)

```csharp
public void Unmerge(KryptonRibbon? ribbon)
```

Unmerges the specified ribbon from the target ribbon.

**Parameters:**
- `ribbon` (`KryptonRibbon?`): The ribbon to unmerge. Can be `null` (no-op).

**Remarks:**
- Reverses merge operation
- Moves items back to source ribbon
- Preserves selected tab (if still exists)
- Refreshes layout automatically
- If `ribbon` is `null`, method returns without doing anything

**Behavior:**
1. Preserves current selection
2. Unmerges contexts:
   - Moves merged contexts back to source
3. Unmerges tabs:
   - Moves merged tabs back to source
   - Unmerges groups within tabs
4. Refreshes layout on both ribbons
5. Restores selection (or resets if tab no longer exists)

**Example:**
```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
merger.Unmerge(pluginRibbon);
```

**See Also:**
- [Merge Method](#mergekryptonribbon)
- [Unmerge Behavior Documentation](RibbonMerging.md#unmerge-behavior)

##### FixGroupWidths()

```csharp
public void FixGroupWidths()
```

Corrects the clipping for groups that have long names but little content.

**Remarks:**
- Measures text width for each group
- Calculates minimum width based on DPI scaling
- Sets `MinimumWidth` property on groups
- Should be called after merging operations
- Requires parent control to have a handle

**Behavior:**
1. Gets parent control of target ribbon
2. Creates graphics context from parent handle
3. Calculates DPI scaling factor
4. For each tab and group:
   - Measures text width (TextLine1 + TextLine2)
   - Calculates minimum width (text width + padding)
   - Sets `MinimumWidth` property

**Example:**
```csharp
var merger = new KryptonRibbonMerger(mainRibbon);
merger.Merge(pluginRibbon);
merger.FixGroupWidths(); // Ensures proper group sizing
```

**Note:** This method requires the ribbon's parent control to have a handle. If the parent is `null` or doesn't have a handle, the method returns without doing anything.

## Extension Methods

### KryptonRibbonExtensions

Extension methods for `KryptonRibbon` to provide convenient merge/unmerge functionality.

#### Merge(this KryptonRibbon, KryptonRibbon?)

```csharp
public static void Merge(
    this KryptonRibbon targetRibbon, 
    KryptonRibbon? sourceRibbon)
```

Merges the specified ribbon into this ribbon.

**Parameters:**
- `targetRibbon` (`KryptonRibbon`): The target ribbon that will receive the merged items.
- `sourceRibbon` (`KryptonRibbon?`): The ribbon to merge into this ribbon.

**Exceptions:**
- `ArgumentNullException`: Thrown when `targetRibbon` is `null`.

**Remarks:**
- Convenience method that creates a temporary `KryptonRibbonMerger` instance
- For multiple operations, consider using `CreateMerger()` instead
- If `sourceRibbon` is `null`, method returns without doing anything

**Example:**
```csharp
// Simple merge
mainRibbon.Merge(pluginRibbon);

// Null-safe (no exception thrown)
mainRibbon.Merge(null);
```

**See Also:**
- [KryptonRibbonMerger.Merge Method](#mergekryptonribbon)
- [CreateMerger Method](#createmergerthis-kryptonribbon)

#### Unmerge(this KryptonRibbon, KryptonRibbon?)

```csharp
public static void Unmerge(
    this KryptonRibbon targetRibbon, 
    KryptonRibbon? sourceRibbon)
```

Unmerges the specified ribbon from this ribbon.

**Parameters:**
- `targetRibbon` (`KryptonRibbon`): The target ribbon that contains the merged items.
- `sourceRibbon` (`KryptonRibbon?`): The ribbon to unmerge from this ribbon.

**Exceptions:**
- `ArgumentNullException`: Thrown when `targetRibbon` is `null`.

**Remarks:**
- Convenience method that creates a temporary `KryptonRibbonMerger` instance
- For multiple operations, consider using `CreateMerger()` instead
- If `sourceRibbon` is `null`, method returns without doing anything

**Example:**
```csharp
// Simple unmerge
mainRibbon.Unmerge(pluginRibbon);

// Null-safe (no exception thrown)
mainRibbon.Unmerge(null);
```

**See Also:**
- [KryptonRibbonMerger.Unmerge Method](#unmergekryptonribbon)
- [CreateMerger Method](#createmergerthis-kryptonribbon)

#### CreateMerger(this KryptonRibbon)

```csharp
public static KryptonRibbonMerger CreateMerger(
    this KryptonRibbon targetRibbon)
```

Creates a ribbon merger instance for this ribbon.

**Parameters:**
- `targetRibbon` (`KryptonRibbon`): The target ribbon that will receive merged items.

**Returns:**
- `KryptonRibbonMerger`: A new merger instance.

**Exceptions:**
- `ArgumentNullException`: Thrown when `targetRibbon` is `null`.

**Remarks:**
- Use this method when you need more control over the merge process
- Reuse the same merger instance for multiple operations
- More efficient than creating temporary mergers for each operation

**Example:**
```csharp
// Create merger instance
var merger = mainRibbon.CreateMerger();

// Reuse for multiple operations
merger.Merge(plugin1Ribbon);
merger.Merge(plugin2Ribbon);
merger.FixGroupWidths();
merger.Unmerge(plugin1Ribbon);
```

**See Also:**
- [KryptonRibbonMerger Class](#kryptonribbonmerger)

## Type Definitions

### Merge Behavior

#### Tab Merging

- **Same Name**: Groups are merged into existing tab
- **Different Name**: Tab is moved to target ribbon
- **Ordering**: Controlled by `Tag` property (0-based index)

#### Group Merging

- **Matching**: Based on `TextLine1` and `TextLine2` (case-sensitive)
- **Same Name**: Items are merged into existing group
- **Different Name**: Group is moved to target tab
- **Ordering**: Controlled by `Tag` property (0-based index)

#### Item Merging

- **Duplicate Check**: Same object reference is skipped
- **Insertion**: Based on `Tag` property (0-based index)
- **Default**: Items without `Tag` are added at end

#### Context Merging

- **Matching**: Based on `ContextTitle` (case-sensitive)
- **Same Name**: Context is skipped (not merged)
- **Different Name**: Context is moved to target ribbon
- **Ordering**: Controlled by `Tag` property (0-based index)

### Tag Property Usage

The `Tag` property controls merge ordering:

```csharp
// Valid tag values
tab.Tag = 0;           // int: Insert at position 0
tab.Tag = "1";          // string that parses to int: Insert at position 1
tab.Tag = null;         // null: Add at end
tab.Tag = "invalid";    // Invalid: Add at end

// Tag is used for:
// - Tabs: Order in RibbonTabs collection
// - Groups: Order in Groups collection
// - Items: Order in Items collection
// - Contexts: Order in RibbonContexts collection
```

## Error Handling

### ArgumentNullException

Thrown when:
- `KryptonRibbonMerger` constructor receives `null` target ribbon
- Extension methods receive `null` target ribbon

**Example:**
```csharp
// ❌ Throws ArgumentNullException
var merger = new KryptonRibbonMerger(null);

// ❌ Throws ArgumentNullException
KryptonRibbon? nullRibbon = null;
nullRibbon.Merge(pluginRibbon);
```

### Null Source Ribbon

When source ribbon is `null`, merge/unmerge operations are no-ops:

```csharp
// ✅ Safe - no exception
mainRibbon.Merge(null);
mainRibbon.Unmerge(null);

var merger = new KryptonRibbonMerger(mainRibbon);
merger.Merge(null);      // No-op
merger.Unmerge(null);    // No-op
```

### Disposed Ribbons

Operations on disposed ribbons may cause exceptions:

```csharp
// Check before merging
if (!pluginRibbon.IsDisposed)
{
    mainRibbon.Merge(pluginRibbon);
}
```

## Thread Safety

**All methods are NOT thread-safe.** All operations must be performed on the UI thread:

```csharp
// ✅ Good - UI thread
private void LoadPlugin()
{
    mainRibbon.Merge(pluginRibbon);
}

// ❌ Bad - Background thread
private void LoadPluginFromBackgroundThread()
{
    Task.Run(() =>
    {
        mainRibbon.Merge(pluginRibbon); // May cause issues
    });
}

// ✅ Good - Invoke to UI thread
private void LoadPluginFromBackgroundThread()
{
    Task.Run(() =>
    {
        if (mainRibbon.InvokeRequired)
        {
            mainRibbon.Invoke(new Action(() => mainRibbon.Merge(pluginRibbon)));
        }
        else
        {
            mainRibbon.Merge(pluginRibbon);
        }
    });
}
```

## Performance Notes

### Time Complexity

- **Merge**: O(n) where n is the number of items to merge
- **Unmerge**: O(n) where n is the number of merged items
- **FixGroupWidths**: O(t × g) where t is tabs and g is groups per tab

### Memory Usage

- **Tracking**: O(n) space for tracking merged items (`HashSet<Component>`)
- **Layout Refresh**: May cause temporary memory spikes

### Optimization Tips

1. Reuse `KryptonRibbonMerger` instances
2. Batch multiple merge operations
3. Use `SuspendLayout()`/`ResumeLayout()` for multiple operations
4. Call `FixGroupWidths()` once after all merges

## See Also

- [UserControl Hosting Guide](UserControlHosting.md)
- [Ribbon Merging Guide](RibbonMerging.md)
- [Main README](README.md)

