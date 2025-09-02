# KryptonThemedSystemMenu API Documentation

## Overview

The KryptonThemedSystemMenu system provides a comprehensive solution for replacing the native Windows system menu with a themed `KryptonContextMenu` that seamlessly integrates with the Krypton Toolkit's theming system. This implementation offers full customization capabilities while maintaining native functionality and performance.

## Architecture

The themed system menu implementation consists of several interconnected components:

### Core Components

1. **KryptonThemedSystemMenu** - Main class that manages the themed system menu
2. **KryptonThemedSystemMenuService** - Service wrapper for easy form integration
3. **IKryptonThemedSystemMenu** - Interface defining the contract for themed system menu functionality
4. **KryptonThemedSystemMenuConverter** - Type converter for design-time support

## API Reference

### 1. KryptonThemedSystemMenu Class

**Location**: `Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonThemedSystemMenu.cs`

The main class that provides themed system menu functionality.

#### Constructor
```csharp
public KryptonThemedSystemMenu(Form form)
```
- **Parameters**: `form` - The form to attach the themed system menu to
- **Throws**: `ArgumentNullException` if form is null

#### Properties

##### ContextMenu
```csharp
[Browsable(false)]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
public KryptonContextMenu ContextMenu { get; }
```
- **Description**: Gets the themed context menu instance
- **Access**: Read-only
- **Design-time**: Hidden from designer

##### DesignerMenuItems
```csharp
[Browsable(false)]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
public ThemedSystemMenuItemCollection? DesignerMenuItems { get; set; }
```
- **Description**: Gets or sets designer-configured menu items
- **Access**: Read/Write
- **Design-time**: Hidden from designer

##### Enabled
```csharp
[Category("Behavior")]
[Description("Enables or disables the themed system menu.")]
[DefaultValue(true)]
public bool Enabled { get; set; }
```
- **Description**: Controls whether the themed system menu is active
- **Default**: `true`
- **Design-time**: Visible in property grid

##### MenuItemCount
```csharp
[Category("Appearance")]
[Description("The number of items currently in the themed system menu.")]
[Browsable(false)]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
public int MenuItemCount { get; }
```
- **Description**: Gets the number of items in the themed system menu
- **Access**: Read-only
- **Design-time**: Hidden from designer

##### HasMenuItems
```csharp
[Category("Appearance")]
[Description("Indicates whether the themed system menu contains any items.")]
[Browsable(false)]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
public bool HasMenuItems { get; }
```
- **Description**: Gets whether the menu has been populated with items
- **Access**: Read-only
- **Design-time**: Hidden from designer

##### ShowOnLeftClick
```csharp
[Browsable(false)]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
public bool ShowOnLeftClick { get; set; }
```
- **Description**: Controls whether left-click on title bar shows the themed system menu
- **Default**: `true`
- **Design-time**: Hidden from designer

##### ShowOnRightClick
```csharp
[Browsable(false)]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
public bool ShowOnRightClick { get; set; }
```
- **Description**: Controls whether right-click on title bar shows the themed system menu
- **Default**: `true`
- **Design-time**: Hidden from designer

##### ShowOnAltSpace
```csharp
[Browsable(false)]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
public bool ShowOnAltSpace { get; set; }
```
- **Description**: Controls whether Alt+Space shows the themed system menu
- **Default**: `true`
- **Design-time**: Hidden from designer

##### CurrentIconTheme
```csharp
[Category("Appearance")]
[Description("The current theme name being used for system menu icons.")]
[Browsable(false)]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
public string CurrentIconTheme { get; }
```
- **Description**: Gets the current theme name being used for icons
- **Access**: Read-only
- **Design-time**: Hidden from designer

#### Methods

##### Show
```csharp
public void Show(Point screenLocation)
```
- **Parameters**: `screenLocation` - The screen coordinates where to show the menu
- **Description**: Shows the themed system menu at the specified location
- **Behavior**: Automatically adjusts position to ensure menu is fully visible

##### ShowAtFormTopLeft
```csharp
public void ShowAtFormTopLeft()
```
- **Description**: Shows the themed system menu at the top-left corner of the form
- **Behavior**: Positions menu like the native system menu

##### Refresh
```csharp
public void Refresh()
```
- **Description**: Refreshes the system menu items based on current form state
- **Behavior**: Rebuilds menu, updates item states, and refreshes icons

##### HandleKeyboardShortcut
```csharp
public bool HandleKeyboardShortcut(Keys keyData)
```
- **Parameters**: `keyData` - The key combination pressed
- **Returns**: `true` if the shortcut was handled; otherwise `false`
- **Description**: Handles keyboard shortcuts for system menu actions
- **Supported Shortcuts**:
  - `Alt+F4` - Close form
  - `Alt+Space` - Show system menu

##### AddCustomMenuItem
```csharp
public void AddCustomMenuItem(string text, EventHandler? clickHandler, bool insertBeforeClose = true)
```
- **Parameters**:
  - `text` - The text to display for the menu item
  - `clickHandler` - The action to execute when the item is clicked
  - `insertBeforeClose` - If true, inserts before Close item; otherwise at end
- **Description**: Adds a custom menu item to the system menu
- **Behavior**: Automatically adds separators as needed

##### AddSeparator
```csharp
public void AddSeparator(bool insertBeforeClose = true)
```
- **Parameters**: `insertBeforeClose` - If true, inserts before Close item; otherwise at end
- **Description**: Adds a separator to the themed system menu

##### ClearCustomItems
```csharp
public void ClearCustomItems()
```
- **Description**: Clears all custom items from the themed system menu
- **Behavior**: Preserves standard system menu items

##### GetCustomMenuItems
```csharp
public List<string> GetCustomMenuItems()
```
- **Returns**: List of custom menu item texts
- **Description**: Gets a list of custom menu item texts

##### RefreshThemeIcons
```csharp
public void RefreshThemeIcons()
```
- **Description**: Manually refreshes all icons to match the current theme
- **Usage**: Call when application theme changes

### 2. KryptonThemedSystemMenuService Class

**Location**: `Source/Krypton Components/Krypton.Toolkit/General/KryptonThemedSystemMenuService.cs`

A service wrapper that provides easy integration with forms and manages the lifecycle of the themed system menu.

#### Constructor
```csharp
public KryptonThemedSystemMenuService(Form form)
```
- **Parameters**: `form` - The form to attach the themed system menu to
- **Throws**: `ArgumentNullException` if form is null

#### Properties

##### UseThemedSystemMenu
```csharp
public bool UseThemedSystemMenu { get; set; }
```
- **Description**: Gets or sets whether the themed system menu is enabled
- **Access**: Read/Write

##### ShowThemedSystemMenuOnLeftClick
```csharp
public bool ShowThemedSystemMenuOnLeftClick { get; set; }
```
- **Description**: Gets or sets whether to show the themed system menu on left click
- **Access**: Read/Write

##### ShowThemedSystemMenuOnRightClick
```csharp
public bool ShowThemedSystemMenuOnRightClick { get; set; }
```
- **Description**: Gets or sets whether to show the themed system menu on right click
- **Access**: Read/Write

##### ShowThemedSystemMenuOnAltSpace
```csharp
public bool ShowThemedSystemMenuOnAltSpace { get; set; }
```
- **Description**: Gets or sets whether to show the themed system menu on Alt+Space
- **Access**: Read/Write

##### ThemedSystemMenu
```csharp
public KryptonThemedSystemMenu ThemedSystemMenu { get; }
```
- **Description**: Gets the themed system menu instance for advanced customization
- **Access**: Read-only

#### Methods

##### Dispose
```csharp
public void Dispose()
```
- **Description**: Releases all resources used by the service
- **Implementation**: Implements IDisposable pattern

##### Dispose(bool disposing)
```csharp
protected virtual void Dispose(bool disposing)
```
- **Parameters**: `disposing` - True if called from Dispose(); false if called from finalizer
- **Description**: Releases managed and unmanaged resources

### 3. IKryptonThemedSystemMenu Interface

**Location**: `Source/Krypton Components/Krypton.Toolkit/General/Definitions.cs`

Defines the contract for themed system menu functionality.

#### Properties
- `bool Enabled { get; set; }` - Controls whether the themed system menu is enabled
- `bool ShowOnLeftClick { get; set; }` - Controls left-click trigger
- `bool ShowOnRightClick { get; set; }` - Controls right-click trigger
- `bool ShowOnAltSpace { get; set; }` - Controls Alt+Space trigger
- `int MenuItemCount { get; }` - Number of items in the menu
- `bool HasMenuItems { get; }` - Whether menu contains items
- `string CurrentIconTheme { get; }` - Current theme name for icons

#### Methods
- `void Show(Point screenLocation)` - Show menu at specified location
- `void ShowAtFormTopLeft()` - Show menu at form's top-left
- `void Refresh()` - Refresh the menu
- `bool HandleKeyboardShortcut(Keys keyData)` - Handle keyboard shortcuts
- `void AddCustomMenuItem(string text, EventHandler? clickHandler, bool insertBeforeClose)` - Add custom item
- `void AddSeparator(bool insertBeforeClose)` - Add separator
- `void ClearCustomItems()` - Clear custom items
- `List<string> GetCustomMenuItems()` - Get custom item texts
- `void RefreshThemeIcons()` - Refresh theme icons

### 4. KryptonThemedSystemMenuConverter Class

**Location**: `Source/Krypton Components/Krypton.Toolkit/Converters/KryptonThemedSystemMenuConverter.cs`

Provides type conversion support for design-time integration.

#### Constructor
```csharp
public KryptonThemedSystemMenuConverter()
```

#### Methods
- `CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)` - Check if conversion is possible
- `ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)` - Perform conversion

## Integration with KryptonForm

### Automatic Integration

The `KryptonForm` class automatically integrates with the themed system menu:

```csharp
// KryptonForm automatically creates the service
private readonly KryptonThemedSystemMenuService? _themedSystemMenuService;

// Service is initialized in constructor
_themedSystemMenuService = new KryptonThemedSystemMenuService(this);

// Property provides access to the themed system menu
public override IKryptonThemedSystemMenu? KryptonSystemMenu => _themedSystemMenuService?.ThemedSystemMenu;
```

### Properties Available on KryptonForm

- `UseThemedSystemMenu` - Enable/disable themed system menu
- `ShowThemedSystemMenuOnLeftClick` - Control left-click trigger
- `ShowThemedSystemMenuOnRightClick` - Control right-click trigger
- `ShowThemedSystemMenuOnAltSpace` - Control Alt+Space trigger
- `KryptonSystemMenu` - Access to themed system menu instance

## Usage Examples

### 1. Basic Usage (Automatic)

```csharp
// Create a KryptonForm - themed system menu is automatically enabled
var form = new KryptonForm();
form.Text = "My Application";

// The themed system menu will work automatically
// Users can trigger it by:
// - Left-clicking on the title bar
// - Right-clicking on the title bar
// - Pressing Alt+Space
```

### 2. Configuration

```csharp
var form = new KryptonForm();

// Configure trigger methods
form.ShowThemedSystemMenuOnLeftClick = true;    // Enable left-click
form.ShowThemedSystemMenuOnRightClick = true;   // Enable right-click
form.ShowThemedSystemMenuOnAltSpace = true;     // Enable Alt+Space

// Disable specific triggers
form.ShowThemedSystemMenuOnLeftClick = false;   // Disable left-click only
```

### 3. Custom Menu Items

```csharp
var form = new KryptonForm();

// Add custom menu items
if (form.KryptonSystemMenu != null)
{
    // Add a custom item before the Close item
    form.KryptonSystemMenu.AddCustomMenuItem("Custom Action", (sender, e) =>
    {
        MessageBox.Show("Custom action executed!");
    }, insertBeforeClose: true);

    // Add a separator
    form.KryptonSystemMenu.AddSeparator(insertBeforeClose: true);

    // Add another custom item
    form.KryptonSystemMenu.AddCustomMenuItem("Settings", (sender, e) =>
    {
        // Open settings dialog
        OpenSettingsDialog();
    }, insertBeforeClose: true);
}
```

### 4. Advanced Customization

```csharp
var form = new KryptonForm();

// Get direct access to the context menu for advanced customization
if (form.KryptonSystemMenu is KryptonThemedSystemMenu themedMenu)
{
    // Access the underlying KryptonContextMenu
    var contextMenu = themedMenu.ContextMenu;

    // Add custom items with full control
    var customItem = new KryptonContextMenuItem("Advanced Action");
    customItem.Click += (sender, e) => PerformAdvancedAction();
    
    // Add to the end of the menu
    contextMenu.Items.Add(new KryptonContextMenuSeparator());
    contextMenu.Items.Add(customItem);
}
```

### 5. Keyboard Shortcut Handling

```csharp
var form = new KryptonForm();

// Override ProcessCmdKey to handle additional shortcuts
protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
{
    // Let the themed system menu handle its shortcuts first
    if (KryptonSystemMenu?.HandleKeyboardShortcut(keyData) == true)
    {
        return true;
    }

    // Handle your own shortcuts
    if (keyData == (Keys.Control | Keys.S))
    {
        SaveDocument();
        return true;
    }

    return base.ProcessCmdKey(ref msg, keyData);
}
```

### 6. Theme-Aware Icons

```csharp
var form = new KryptonForm();

// Refresh icons when theme changes
KryptonManager.GlobalPaletteChanged += (sender, e) =>
{
    if (form.KryptonSystemMenu != null)
    {
        form.KryptonSystemMenu.RefreshThemeIcons();
    }
};
```

### 7. Conditional Menu Items

```csharp
var form = new KryptonForm();

// Add conditional menu items based on application state
if (form.KryptonSystemMenu != null)
{
    // Only show "Save" if document is modified
    if (IsDocumentModified)
    {
        form.KryptonSystemMenu.AddCustomMenuItem("Save", (sender, e) => SaveDocument());
    }

    // Only show "Print" if printer is available
    if (IsPrinterAvailable)
    {
        form.KryptonSystemMenu.AddCustomMenuItem("Print", (sender, e) => PrintDocument());
    }
}
```

### 8. Disabling Themed System Menu

```csharp
var form = new KryptonForm();

// Completely disable themed system menu (falls back to native)
form.UseThemedSystemMenu = false;

// Or disable specific triggers while keeping others
form.ShowThemedSystemMenuOnLeftClick = false;   // Disable left-click
form.ShowThemedSystemMenuOnRightClick = false;  // Disable right-click
// Alt+Space still works
```

## System Menu Items

### Standard Items

The themed system menu automatically includes these standard items:

1. **Restore** - Restore window from minimized/maximized state
2. **Move** - Allow window to be moved
3. **Size** - Allow window to be resized
4. **Minimize** - Minimize the window
5. **Maximize** - Maximize the window
6. **Close** - Close the window

### Dynamic State

Menu items are automatically enabled/disabled based on the current window state:

- **Restore**: Enabled when window is minimized or maximized
- **Move**: Enabled when window is not maximized
- **Size**: Enabled when window is not maximized and is resizable
- **Minimize**: Enabled when window is not minimized
- **Maximize**: Enabled when window is not maximized and can be maximized
- **Close**: Always enabled

## Performance Considerations

### Resource Management

- **Lazy Loading**: Menu items are built only when needed
- **Disposal**: Proper resource cleanup through IDisposable implementation
- **Memory Efficiency**: Minimal memory footprint when not in use

### Optimization Tips

1. **Avoid Frequent Refreshes**: Only call `Refresh()` when necessary
2. **Batch Custom Items**: Add multiple items at once rather than individually
3. **Theme Changes**: Use `RefreshThemeIcons()` instead of full `Refresh()` for theme changes
4. **Disposal**: Always dispose of custom event handlers when removing items

## Error Handling

### Graceful Degradation

The implementation includes multiple fallback mechanisms:

1. **Native Menu Parsing**: If native system menu cannot be parsed, a basic themed menu is created
2. **Exception Handling**: All operations are wrapped in try-catch blocks
3. **Null Checks**: Comprehensive null checking prevents crashes
4. **Resource Cleanup**: Proper disposal prevents resource leaks

### Common Scenarios

```csharp
// Safe access to themed system menu
if (form.KryptonSystemMenu != null)
{
    try
    {
        form.KryptonSystemMenu.AddCustomMenuItem("Safe Action", (sender, e) =>
        {
            // Your action here
        });
    }
    catch (Exception ex)
    {
        // Handle any errors gracefully
        Debug.WriteLine($"Failed to add custom menu item: {ex.Message}");
    }
}
```

## Design-Time Support

### Property Grid Integration

The themed system menu properties are fully integrated with the Visual Studio property grid:

- **Enabled**: Boolean property with category "Behavior"
- **ShowOnLeftClick**: Boolean property (design-time only)
- **ShowOnRightClick**: Boolean property (design-time only)
- **ShowOnAltSpace**: Boolean property (design-time only)

### Type Converter

The `KryptonThemedSystemMenuConverter` provides:

- **String Conversion**: Converts to/from string representation
- **Design-Time Display**: Shows meaningful text in property grid
- **Serialization Support**: Enables proper serialization in designer

## Testing

### Test Form

A comprehensive test form is available at:
`Source/Krypton Components/TestForm/ThemedSystemMenuTest.cs`

### Test Scenarios

1. **Basic Functionality**: Verify menu appears and standard items work
2. **Trigger Methods**: Test left-click, right-click, and Alt+Space triggers
3. **Custom Items**: Add and test custom menu items
4. **Theme Changes**: Verify icons update when theme changes
5. **Window States**: Test menu behavior in different window states
6. **Error Handling**: Test behavior with invalid inputs

### Manual Testing Steps

1. Build the Krypton Toolkit project
2. Run the TestForm application
3. Navigate to "Themed System Menu" test
4. Test each trigger method:
   - Left-click on title bar
   - Right-click on title bar
   - Press Alt+Space
5. Verify custom items work correctly
6. Test theme changes
7. Test different window states

## Benefits

### 1. Visual Consistency
- **Theme Integration**: Menu matches the overall application theme
- **Modern Appearance**: Contemporary look that matches user expectations
- **Brand Consistency**: Maintains application branding throughout

### 2. Developer Experience
- **Easy Integration**: Automatic integration with KryptonForm
- **Flexible API**: Multiple ways to customize and extend
- **Design-Time Support**: Full Visual Studio integration
- **Comprehensive Documentation**: Clear API documentation and examples

### 3. User Experience
- **Familiar Behavior**: Works exactly like the native system menu
- **Enhanced Functionality**: Can be extended with custom actions
- **Accessibility**: Maintains accessibility features
- **Performance**: Fast and responsive

### 4. Maintenance
- **Future-Proof**: Uses Windows system resources maintained by Microsoft
- **Version Compatibility**: Works across different Windows versions
- **Localization**: Automatically adapts to system language
- **Updates**: Benefits from Krypton Toolkit updates

## Future Enhancements

### Potential Improvements

1. **Icon Customization**: Support for custom icons in menu items
2. **RTL Support**: Right-to-left language support
3. **Accessibility**: Enhanced accessibility features
4. **Animation**: Smooth animations for menu appearance
5. **Templates**: Custom menu item templates
6. **Commands**: Integration with Krypton commands system

### Integration Opportunities

1. **Message Box Integration**: Use themed system menu in message dialogs
2. **Toolbar Integration**: Consistent theming across all UI elements
3. **Menu Integration**: Unified menu system across application
4. **Status Bar Integration**: Consistent status indicators

## Conclusion

The KryptonThemedSystemMenu API provides a comprehensive, flexible, and easy-to-use solution for creating themed system menus in Krypton Toolkit applications. With automatic integration, extensive customization options, and robust error handling, it offers developers the tools they need to create professional, consistent user interfaces while maintaining the familiar behavior users expect from system menus.

The implementation follows Krypton Toolkit best practices, provides excellent design-time support, and delivers a high-quality user experience that enhances application professionalism and usability.
