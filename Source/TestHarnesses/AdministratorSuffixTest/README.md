# Administrator Suffix Feature for KryptonForm

This feature adds the ability to display "(Administrator)" in the title bar of a KryptonForm when the application is running with elevated privileges, similar to how Windows native applications behave.

## Features

- **Automatic Detection**: Automatically detects if the application is running with administrator privileges
- **Global Configuration**: Can be enabled/disabled globally via `KryptonManager.ShowAdministratorSuffix`
- **Real-time Updates**: Title bar updates immediately when the global setting is changed
- **Windows-like Behavior**: Matches the standard Windows behavior for elevated applications
- **Localization Ready**: Uses KryptonManager's toolkit strings for future localization support

## Usage

### Global Configuration

```csharp
// Enable administrator suffix globally (enabled by default)
KryptonManager.ShowAdministratorSuffix = true;

// Disable administrator suffix globally
KryptonManager.ShowAdministratorSuffix = false;
```

### Basic Usage

```csharp
public partial class MyForm : KryptonForm
{
    public MyForm()
    {
        InitializeComponent();
        
        // Set the form title
        Text = "My Application";
        
        // The administrator suffix will automatically show if:
        // 1. KryptonManager.ShowAdministratorSuffix is true (default)
        // 2. The application is running with elevated privileges
    }
}
```

### Checking Administrator Status

```csharp
if (IsInAdministratorMode)
{
    // Application is running with elevated privileges
    MessageBox.Show("Running as Administrator");
}
else
{
    // Application is running with normal privileges
    MessageBox.Show("Running as Normal User");
}
```

## Properties

### KryptonManager.ShowAdministratorSuffix (Static)
- **Type**: `bool`
- **Default**: `true`
- **Description**: Gets or sets whether the administrator suffix should be shown in KryptonForm title bars when running with elevated privileges.

### KryptonManager.GlobalShowAdministratorSuffix (Instance)
- **Type**: `bool`
- **Default**: `true`
- **Category**: `Visuals`
- **Description**: Gets or sets whether the administrator suffix should be shown in KryptonForm title bars when running with elevated privileges.

### IsInAdministratorMode
- **Type**: `bool` (read-only)
- **Description**: Gets whether the application is currently running with administrator privileges.

## Testing

To test this feature:

1. **Normal Mode**: Run the application normally - the title bar will show just the application name
2. **Administrator Mode**: Run the application "As Administrator" - the title bar will show "Application Name (Administrator)"

### Test Harnesses

- **FormBorderTest**: Enhanced with a checkbox to toggle the global `ShowAdministratorSuffix` setting
- **AdministratorSuffixTest**: Dedicated test form demonstrating the feature with status display and controls

## Example Output

- **Normal Mode**: "My Application"
- **Administrator Mode**: "My Application (Administrator)"

## Implementation Details

The feature works by:
1. Detecting administrator privileges using Windows security APIs via the existing `GetIsInAdministratorMode()` method
2. Modifying the `GetShortText()` method to append the localized "(Administrator)" string when appropriate
3. Using the global `KryptonManager.ShowAdministratorSuffix` setting to control the feature
4. Using the existing KryptonForm title bar rendering system and `IContentValues` interface

## Compatibility

- **Windows**: All supported Windows versions
- **.NET Framework**: net472 and later
- **.NET**: net6.0-windows and later
- **Krypton Toolkit**: Compatible with all existing KryptonForm functionality

## Migration from Local Property

If you were previously using the local `ShowAdministratorSuffix` property on individual KryptonForm instances, you should now use the global `KryptonManager.ShowAdministratorSuffix` setting instead. This change provides better consistency across all forms in your application.
