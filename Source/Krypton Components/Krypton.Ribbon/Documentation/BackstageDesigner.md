# Krypton Ribbon Backstage Designer Guide

## Overview

The Krypton Ribbon Backstage View includes comprehensive Visual Studio designer support, making it easy to create and configure backstage pages at design time without writing code.

## Designer Features

### Smart Tags (Action Lists)

#### KryptonRibbon Smart Tag
When you select the `KryptonRibbon` control in the designer, the smart tag provides quick access to backstage settings:

**Design Section:**
- **Design Helpers** - Show design time helpers for creating items

**Backstage Section:**
- **Use Backstage for App Button** - Toggle between backstage and traditional app menu
- **Pages Info** - Shows page count and visibility status (e.g., "5 pages (4 visible)")
- **Navigation Width** - Adjust the backstage navigation panel width

**Visuals Section:**
- **Palette** - Set the palette mode for theming

#### KryptonRibbonBackstagePage Smart Tag
Individual backstage pages have enhanced smart tags with context-sensitive actions:

**Appearance Section:**
- **Navigation Text** - Text displayed on the navigation button
- **Content Title** - Title displayed in the content area (when no custom content is set)
- **Content Description** - Description displayed in the content area (when no custom content is set)

**Content Section:**
- **Current Content** - Information about the current content (e.g., "UserControl (5 controls)" or "Default text content")
- **Create Custom Content** - One-click creation of a KryptonPanel for custom content *(shown when no custom content)*
- **Remove Custom Content** - One-click removal to revert to text content *(shown when custom content exists)*

**Behavior Section:**
- **Visible** - Whether the page is visible in the navigation
- **Enabled** - Whether the page is enabled for user interaction

### Collection Editor

The backstage pages collection editor provides a rich interface for managing pages:

1. **Add/Remove Pages** - Use the Add/Remove buttons
2. **Reorder Pages** - Use up/down arrows or drag and drop
3. **Property Grid** - Edit selected page properties
4. **Preview** - See how pages will appear

#### Opening the Collection Editor

**Method 1: Smart Tag**
1. Select the `KryptonRibbon` control
2. Click the smart tag arrow
3. Click "BackstagePages"

**Method 2: Properties Window**
1. Select the `KryptonRibbon` control
2. In Properties window, find "BackstagePages"
3. Click the ellipsis (...) button

**Method 3: Properties Window (Expandable Object)**
1. Select the `KryptonRibbon` control
2. In Properties window, expand "BackstageValues"
3. Find "BackstagePages" and click the ellipsis (...) button

### BackstageValues Expandable Object

The `BackstageValues` property in the Properties window groups all backstage-related settings into an organized, expandable object:

**Behavior Category:**
- `UseBackstageForAppButton` - Enable/disable backstage functionality
- `AllowNavigationResize` - Whether users can resize the navigation panel

**Data Category:**
- `BackstagePages` - Collection of backstage pages (opens collection editor)

**Layout Category:**
- `NavigationWidth` - Width of the navigation panel in pixels

**Appearance Category:**
- `IsVisible` - Current visibility state (runtime only)

This organization makes it easy to find and configure all backstage-related settings in one place.

### UserControl Integration

The designer provides excellent support for UserControl-based backstage pages:

#### Creating UserControl Pages

**Method 1: Smart Tag Approach**
1. Add pages using the collection editor
2. Select a page and use its smart tag
3. Click "Create Custom Content" to generate a KryptonPanel
4. Add your controls to the generated panel

**Method 2: Code Integration**
The designer works seamlessly with code-created UserControl pages:

```csharp
// Create UserControl
var settingsPage = new MySettingsUserControl();

// Add to ribbon (designer will recognize this)
ribbon.AddBackstagePage("Settings", settingsPage);
```

**Method 3: Direct Assignment**
1. Create your UserControl separately
2. In the page's Properties window, set `ContentPanel` to your UserControl
3. The designer will automatically update and show content information

#### Content Management Features

The designer provides intelligent content management:

- **Content Detection**: Automatically detects the type of content (UserControl, KryptonPanel, or default text)
- **Content Information**: Shows content type and control count in smart tags
- **One-Click Creation**: Create custom content panels without writing code
- **One-Click Removal**: Remove custom content and revert to text content
- **Visual Feedback**: Properties window and smart tags update based on content state

### Property Categories

Properties in the designer are organized into logical categories:

#### Appearance
- `Text` - Button text in navigation panel
- `TextTitle` - Title displayed in content area
- `TextDescription` - Description text in content area

#### Behavior  
- `Visible` - Whether page appears in navigation
- `Enabled` - Whether page can be selected
- `ContentPanel` - Custom control for content area

#### Data
- `Tag` - User-defined data object

#### Design
- `Name` - Component name for code access

## Step-by-Step Designer Walkthrough

### 1. Enable Backstage

1. Select your `KryptonRibbon` control
2. In Properties window, set `UseBackstageForAppButton = True`
3. Or use the smart tag to toggle this setting

### 2. Add Your First Page

1. Open the BackstagePages collection editor
2. Click "Add" to create a new page
3. Set basic properties:
   ```
   Text = "Open"
   TextTitle = "Open an existing document"  
   TextDescription = "Browse and open files from your computer."
   Visible = True
   ```
4. Click OK to apply changes

### 3. Add More Pages

Repeat the process to add additional pages:

```
Page 1:
  Text = "Save"
  TextTitle = "Save your document"
  TextDescription = "Save the current document to your computer."

Page 2:  
  Text = "Settings"
  TextTitle = "Application Settings"
  TextDescription = "Configure application preferences and options."

Page 3:
  Text = "About"
  TextTitle = "About MyApplication"
  TextDescription = "Version 1.0\nCopyright © 2025 My Company"
```

### 4. Test at Design Time

1. Build your project (F6)
2. Run the application (F5)  
3. Click the application button to see your backstage
4. Navigate between pages to verify functionality

## Custom Content Integration

### Adding UserControls

For complex page content, create custom UserControls:

#### 1. Create the UserControl

1. Add new item → User Control
2. Name it (e.g., `SettingsControl`)
3. Design your custom interface
4. Build the project

#### 2. Assign to Backstage Page

1. Open the BackstagePages collection editor
2. Select the target page
3. Set `ContentPanel` property:
   - Click the dropdown arrow
   - Select your UserControl type
   - Or click "..." to browse for the control

#### 3. Design-Time Preview

The designer will show a placeholder for custom content panels. To see the actual content:

1. Build the project
2. Close and reopen the designer
3. The custom control should appear in the backstage preview

### UserControl Design Guidelines

When creating UserControls for backstage content:

```csharp
public partial class SettingsControl : UserControl
{
    public SettingsControl()
    {
        InitializeComponent();
        
        // Set appropriate background
        BackColor = Color.Transparent;
        
        // Use Krypton controls for consistency
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };
        
        Controls.Add(panel);
    }
}
```

## Designer Best Practices

### Naming Conventions

Use descriptive names for backstage pages:

```csharp
// Good
backstagePageOpen
backstagePageSave  
backstagePageSettings
backstagePageAbout

// Avoid
kryptonRibbonBackstagePage1
kryptonRibbonBackstagePage2
```

### Organization

Group related pages together:

1. **File Operations** - Open, Save, Recent, Export
2. **Application** - Settings, Options, Preferences  
3. **Information** - About, Help, Updates
4. **Account** - Login, Profile, Sync

### Property Setting Order

Set properties in this order for consistency:

1. `Text` - What users see first
2. `TextTitle` - Content area title
3. `TextDescription` - Additional info
4. `Visible` - Availability
5. `Enabled` - Interaction state
6. `ContentPanel` - Custom content (if needed)
7. `Tag` - Data association (if needed)

## Troubleshooting Designer Issues

### Common Problems and Solutions

#### Problem: Collection editor won't open
**Solution:** 
- Ensure project builds without errors
- Try closing and reopening Visual Studio
- Check that Krypton.Ribbon reference is properly loaded

#### Problem: Custom UserControl not showing in designer
**Solution:**
- Build the project after creating the UserControl
- Make sure UserControl is public
- Check for designer exceptions in Output window

#### Problem: Properties not saving
**Solution:**
- Ensure you click OK in collection editor
- Check that the form's designer file (.Designer.cs) is not read-only
- Verify no build errors are preventing designer updates

#### Problem: Smart tags not appearing
**Solution:**
- Enable smart tags: Tools → Options → Windows Forms Designer → General → Enable smart tags
- Right-click control and select "Show Smart Tag"

### Designer Debugging

Enable designer debugging for troubleshooting:

1. Tools → Options → Windows Forms Designer → General
2. Check "Enable optimized code generation"
3. Check "Enable XAML designer debugging"
4. Check output window for designer errors

## Advanced Designer Scenarios

### Conditional Page Visibility

While you can't set complex conditions in the designer, you can prepare for runtime logic:

1. Set default `Visible = True` in designer
2. Set meaningful `Tag` values for identification
3. Implement runtime visibility logic in code:

```csharp
private void UpdatePageVisibility()
{
    foreach (var page in kryptonRibbon1.BackstagePages)
    {
        switch (page.Tag?.ToString())
        {
            case "AdminOnly":
                page.Visible = CurrentUser.IsAdmin;
                break;
            case "OnlineOnly": 
                page.Visible = NetworkStatus.IsOnline;
                break;
        }
    }
}
```

### Localization Support

Prepare backstage pages for localization:

1. Set all text properties using resource strings
2. Use the resource editor for text values
3. Test with different cultures

```csharp
// In designer-generated code
backstagePageOpen.Text = Resources.Backstage_Open_Text;
backstagePageOpen.TextTitle = Resources.Backstage_Open_Title;
backstagePageOpen.TextDescription = Resources.Backstage_Open_Description;
```

### Theme Testing

Test your backstage with different themes:

1. Add a KryptonManager to your form
2. Set different GlobalPaletteMode values
3. Run application and test backstage appearance
4. Adjust custom content colors as needed

## Integration with Other Krypton Controls

### Ribbon Integration

The backstage automatically integrates with:
- Ribbon application button styling
- Ribbon theme and palette
- Ribbon keyboard navigation

### Form Integration

For best results:
- Use KryptonForm as your main form
- Set appropriate FormBorderStyle
- Consider custom chrome integration

### Control Consistency

In custom content panels, use Krypton controls:
- KryptonButton instead of Button
- KryptonLabel instead of Label  
- KryptonPanel instead of Panel
- KryptonTextBox instead of TextBox

This ensures consistent theming and appearance across your application.

## See Also

- [Backstage View Overview](BackstageView.md)
- [API Reference](BackstageAPI.md)  
- [Code Examples](BackstageExamples.md)
