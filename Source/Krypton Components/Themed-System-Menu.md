#  Themed System Menu Implementation

This document describes the implementation of the themed system menu feature for the Krypton Toolkit, as requested in [GitHub Issue #648](https://github.com/Krypton-Suite/Standard-Toolkit/issues/648).

## Overview

The themed system menu replaces the native Windows system menu with a `KryptonContextMenu` that follows the current theme palette, providing consistent visual integration with the rest of the Krypton application.

## Features

- **Theme Integration**: The system menu automatically adopts the current Krypton theme palette
- **Native Functionality**: All standard system menu commands (Restore, Move, Size, Minimize, Maximize, Close) work identically to the native menu
- **Dynamic Updates**: Menu items automatically reflect the current form state (enabled/disabled based on window state)
- **Fallback Support**: If the native system menu cannot be parsed, a basic themed menu is created as fallback
- **Customizable**: Advanced users can add custom menu items or modify the existing menu

## Implementation Details

### New Classes

#### `KryptonThemedSystemMenu`
- **Location**: `Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonThemedSystemMenu.cs`
- **Purpose**: Manages the themed system menu and handles the conversion from native system menu to themed context menu
- **Key Methods**:
  - `Show(Point screenLocation)`: Displays the themed system menu at the specified location
  - `Refresh()`: Updates the menu items based on current form state
  - `Enabled`: Controls whether the themed system menu is active

### Enhanced APIs

#### PlatformInvoke.cs
Added new Win32 APIs for system menu manipulation:
- `GetMenuItemCount(IntPtr hMenu)`: Gets the number of menu items
- `GetMenuItemID(IntPtr hMenu, int nPos)`: Gets the command ID of a menu item
- `GetMenuString(IntPtr hMenu, uint uIDItem, StringBuilder lpString, int nMaxCount, MF_ uFlag)`: Gets menu item text
- `GetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii)`: Gets detailed menu item information

#### New Constants
- `MIIM_*`: Menu item info mask constants
- `MFT_*`: Menu item type constants  
- `MFS_*`: Menu item state constants

#### New Structures
- `MENUITEMINFO`: Win32 structure for menu item information

### Integration with KryptonForm

#### New Properties
- `UseThemedSystemMenu`: Boolean property to enable/disable the themed system menu (default: true)
- `ShowThemedSystemMenuOnLeftClick`: Controls whether left-click on title bar shows the menu (default: true)
- `ShowThemedSystemMenuOnRightClick`: Controls whether right-click on title bar shows the menu (default: true)
- `ShowThemedSystemMenuOnAltSpace`: Controls whether Alt+Space shows the menu (default: true)
- `ThemedSystemMenu`: Advanced access to the themed system menu instance for customization

#### Automatic Integration
- The themed system menu is automatically created when a `KryptonForm` is instantiated
- Multiple trigger methods are supported:
  - **Left-click on title bar**: Shows the themed menu when clicking anywhere on the title bar (except control buttons)
  - **Right-click on title bar**: Shows the themed menu when right-clicking on the title bar (except control buttons)
  - **Alt+Space**: Shows the themed menu when pressing the Alt+Space keyboard shortcut
- The menu automatically refreshes when the window state changes

## Usage

### Basic Usage
```csharp
// The themed system menu is enabled by default
// It can be triggered in multiple ways:
// 1. Left-click on the title bar (except on control buttons)
// 2. Right-click on the title bar (except on control buttons)
// 3. Press Alt+Space keyboard shortcut
```

### Trigger Method Configuration
```csharp
// Configure which trigger methods are enabled
ShowThemedSystemMenuOnLeftClick = true;    // Left-click on title bar
ShowThemedSystemMenuOnRightClick = true;   // Right-click on title bar
ShowThemedSystemMenuOnAltSpace = true;     // Alt+Space keyboard shortcut

// You can disable specific methods while keeping others enabled
ShowThemedSystemMenuOnLeftClick = false;   // Disable left-click trigger
```

### Customization
```csharp
// Add custom menu items
if (ThemedSystemMenu != null)
{
    var customItem = new KryptonContextMenuItem("Custom Action");
    customItem.Click += (sender, e) => MessageBox.Show("Custom action!");
    ThemedSystemMenu.ContextMenu.Items.Add(new KryptonContextMenuSeparator());
    ThemedSystemMenu.ContextMenu.Items.Add(customItem);
}
```

### Disabling the Feature
```csharp
// Disable the themed system menu to fall back to native behavior
UseThemedSystemMenu = false;
```

## Testing

A test form has been created at `Source/Krypton Components/TestForm/ThemedSystemMenuTest.cs` that demonstrates the functionality. To test:

1. Build the Krypton Toolkit project
2. Run the TestForm application
3. Click the "Themed System Menu" button
4. The test form includes checkboxes to control each trigger method:
   - Left-click on title bar
   - Right-click on title bar
   - Alt+Space keyboard shortcut
5. Try different trigger methods to see the themed system menu in action

## Benefits

1. **Visual Consistency**: The system menu now matches the overall application theme
2. **Better Integration**: Seamless visual integration with Krypton controls
3. **Modern Appearance**: Contemporary look that matches user expectations
4. **Customization**: Ability to extend the system menu with custom functionality
5. **Maintainability**: Centralized theming system instead of platform-specific styling

## Technical Notes

- The implementation gracefully falls back to basic menu items if the native system menu cannot be parsed
- All system commands are properly routed through the existing `SendSysCommand` mechanism
- The menu automatically reflects the current form state (minimized, maximized, etc.)
- Performance impact is minimal as the menu is only built when needed

## Future Enhancements

Potential areas for future improvement:
- Support for custom icons in menu items
- Integration with Krypton commands system
- Support for right-to-left (RTL) languages
- Enhanced accessibility features
- Support for custom menu item templates

## Compatibility

- **Target Framework**: .NET Framework 4.6.2 and later
- **Windows Versions**: Windows 7 and later
- **Dependencies**: Existing Krypton Toolkit components
- **Breaking Changes**: None - this is a purely additive feature

## Conclusion

The themed system menu implementation successfully addresses the feature request from GitHub Issue #648, providing a modern, themeable alternative to the native Windows system menu while maintaining full compatibility and functionality.