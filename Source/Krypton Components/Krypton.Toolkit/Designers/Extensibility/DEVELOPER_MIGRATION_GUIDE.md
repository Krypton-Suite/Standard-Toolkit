# Developer Migration Guide: WinForms Designer Extensibility SDK

## 🎯 Overview

This guide provides step-by-step instructions for developers who need to understand, maintain, or extend the WinForms Designer Extensibility SDK implementation in the Krypton Suite.

## 📋 Prerequisites

- Visual Studio 2022 or later
- .NET Framework 4.7.2+ or .NET 8+ SDK
- Basic understanding of WinForms designer architecture
- Familiarity with C# and .NET development

## 🏗️ Architecture Overview

### Migration Status: ✅ COMPLETED

All 53 Krypton controls have been migrated to use the WinForms Designer Extensibility SDK:

- **Krypton.Toolkit**: 45 controls
- **Krypton.Docking**: 1 control  
- **Krypton.Navigator**: 2 controls
- **Krypton.Workspace**: 3 controls
- **Krypton.Ribbon**: 2 controls

### New Architecture

```
Krypton Control
    ↓
[Designer(typeof(ControlExtensibilityDesigner))]
    ↓
ExtensibilityDesignerBase
    ↓
ActionLists Property
    ↓
ControlExtensibilityActionList
    ↓
GetSortedActionItems()
    ↓
Smart Tag Panel (Lightning Bolt)
```

## 🔧 Key Components

### 1. Base Classes

#### KryptonExtensibilityDesignerBase
- **Location**: `Base/KryptonExtensibilityDesignerBase.cs`
- **Purpose**: Common functionality for all control designers
- **Key Features**:
  - Service access (IComponentChangeService, ISelectionService, etc.)
  - Action list management
  - Property change notification

#### KryptonExtensibilityActionListBase
- **Location**: `Base/KryptonExtensibilityActionListBase.cs`
- **Purpose**: Common functionality for all action lists
- **Key Features**:
  - Property value management
  - Change notification
  - Service access

### 2. Designer Classes

Each control has a corresponding designer class:
- **Location**: `Controls/ControlNameExtensibilityDesigner.cs`
- **Purpose**: Handles design-time behavior for specific controls
- **Key Features**:
  - Associates with action list
  - Provides design-time services
  - Handles control-specific behavior

### 3. Action List Classes

Each control has a corresponding action list class:
- **Location**: `ActionLists/ControlNameExtensibilityActionList.cs`
- **Purpose**: Exposes properties in smart tag panels
- **Key Features**:
  - Property organization and categorization
  - Property change handling
  - Smart tag panel content

## 📝 Implementation Patterns

### 1. Designer Attribute

All controls use the new designer attribute:

```csharp
[Designer(typeof(KryptonButtonExtensibilityDesigner))]
public class KryptonButton : Button
{
    // Control implementation
}
```

### 2. Designer Class Implementation

```csharp
public class KryptonButtonExtensibilityDesigner : KryptonExtensibilityDesignerBase
{
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            actionLists.Add(new KryptonButtonExtensibilityActionList(this));
            return actionLists;
        }
    }
}
```

### 3. Action List Implementation

```csharp
public class KryptonButtonExtensibilityActionList : KryptonExtensibilityActionListBase
{
    private readonly KryptonButton _button;

    public KryptonButtonExtensibilityActionList(KryptonExtensibilityDesignerBase owner)
        : base(owner)
    {
        _button = (KryptonButton)Component;
    }

    // Property definitions
    public string Text
    {
        get => _button.Text;
        set => SetPropertyValue(nameof(Text), value);
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();
        
        // Add categorized properties
        items.Add(new DesignerActionHeaderItem("Appearance"));
        items.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Button text."));
        
        return items;
    }
}
```

## 🎨 Property Organization

### Categories

Properties are organized into logical categories:

- **Appearance**: Visual properties (Text, Image, Style, etc.)
- **Behavior**: Functional properties (Enabled, Visible, etc.)
- **Data**: Data-related properties (DataSource, Value, etc.)
- **Layout**: Layout properties (Dock, Anchor, etc.)
- **Design**: Design-time properties (Tag, etc.)

### Example Organization

```csharp
public override DesignerActionItemCollection GetSortedActionItems()
{
    var items = new DesignerActionItemCollection();
    
    // Appearance category
    items.Add(new DesignerActionHeaderItem("Appearance"));
    items.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Button text."));
    items.Add(new DesignerActionPropertyItem(nameof(Image), "Image", "Appearance", "Button image."));
    
    // Behavior category
    items.Add(new DesignerActionHeaderItem("Behavior"));
    items.Add(new DesignerActionPropertyItem(nameof(Enabled), "Enabled", "Behavior", "Whether the button is enabled."));
    items.Add(new DesignerActionPropertyItem(nameof(Visible), "Visible", "Behavior", "Whether the button is visible."));
    
    return items;
}
```

## 🔄 Property Change Handling

### SetPropertyValue Method

All property changes use the `SetPropertyValue` method:

```csharp
protected void SetPropertyValue(string propertyName, object value)
{
    if (Component != null)
    {
        var propertyDescriptor = TypeDescriptor.GetProperties(Component)[propertyName];
        if (propertyDescriptor != null)
        {
            var oldValue = propertyDescriptor.GetValue(Component);
            if (!Equals(oldValue, value))
            {
                propertyDescriptor.SetValue(Component, value);
                _changeService?.OnComponentChanged(Component, propertyDescriptor, oldValue, value);
            }
        }
    }
}
```

### Benefits

- **Immediate Feedback**: Changes reflect instantly in the designer
- **Undo/Redo**: Full support for design-time undo/redo
- **Change Notification**: Proper notification to the design environment
- **Type Safety**: Compile-time checking of property names

## 🧪 Testing Guidelines

### 1. Build Testing

Test builds on all target frameworks:

```bash
# .NET Framework
dotnet build -f net472
dotnet build -f net48
dotnet build -f net481

# .NET 8+
dotnet build -f net8.0-windows
dotnet build -f net9.0-windows
dotnet build -f net10.0-windows
```

### 2. Design-Time Testing

1. **Create Test Project**: New Windows Forms project
2. **Add References**: Reference Krypton libraries
3. **Add Controls**: Drag Krypton controls onto form
4. **Test Smart Tags**: Click lightning bolt on controls
5. **Verify Properties**: Check that expected properties appear
6. **Test Changes**: Modify properties and verify immediate feedback
7. **Test Undo/Redo**: Verify undo/redo functionality

### 3. Cross-Framework Testing

Test the same project on different target frameworks:
- .NET Framework 4.7.2
- .NET Framework 4.8
- .NET 8.0-windows
- .NET 9.0-windows

## 🚀 Adding New Controls

### Step 1: Create Designer Class

```csharp
public class NewControlExtensibilityDesigner : KryptonExtensibilityDesignerBase
{
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            actionLists.Add(new NewControlExtensibilityActionList(this));
            return actionLists;
        }
    }
}
```

### Step 2: Create Action List Class

```csharp
public class NewControlExtensibilityActionList : KryptonExtensibilityActionListBase
{
    private readonly NewControl _control;

    public NewControlExtensibilityActionList(KryptonExtensibilityDesignerBase owner)
        : base(owner)
    {
        _control = (NewControl)Component;
    }

    // Add properties here
    public string Text
    {
        get => _control.Text;
        set => SetPropertyValue(nameof(Text), value);
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();
        
        items.Add(new DesignerActionHeaderItem("Appearance"));
        items.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Control text."));
        
        return items;
    }
}
```

### Step 3: Add Designer Attribute

```csharp
[Designer(typeof(NewControlExtensibilityDesigner))]
public class NewControl : Control
{
    // Control implementation
}
```

## 🔍 Troubleshooting

### Common Issues

#### 1. Smart Tags Not Appearing

**Symptoms**: Lightning bolt doesn't appear on control
**Causes**:
- Missing designer attribute
- Incorrect designer class
- Build errors in designer/action list

**Solutions**:
- Verify `[Designer(typeof(...))]` attribute is present
- Check that designer class compiles without errors
- Ensure action list class is properly implemented

#### 2. Properties Not Appearing

**Symptoms**: Smart tag opens but properties are missing
**Causes**:
- Incorrect property names in action list
- Missing property descriptors
- Type mismatches

**Solutions**:
- Verify property names match actual control properties
- Check property types and accessibility
- Ensure properties are public and have getters/setters

#### 3. Property Changes Not Reflecting

**Symptoms**: Changes don't appear immediately in designer
**Causes**:
- Missing change notification
- Incorrect property descriptor usage
- Service access issues

**Solutions**:
- Use `SetPropertyValue` method for all property changes
- Verify `IComponentChangeService` is available
- Check property descriptor implementation

### Debugging Tips

1. **Enable Designer Debugging**: Set breakpoints in designer and action list classes
2. **Check Services**: Verify design-time services are available
3. **Property Inspection**: Use property grid to verify property values
4. **Build Output**: Check for compilation errors and warnings

## 📚 Additional Resources

### Documentation Files

- **[MIGRATION_COMPLETE.md](MIGRATION_COMPLETE.md)**: Complete migration summary
- **[TECHNICAL_REFERENCE.md](TECHNICAL_REFERENCE.md)**: Technical implementation details
- **[DEVELOPER_GUIDE.md](DEVELOPER_GUIDE.md)**: Comprehensive developer guide
- **[QUICK_START.md](QUICK_START.md)**: Quick start guide

### External Resources

- [WinForms Designer Extensibility SDK Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/designer-extensibility/)
- [System.ComponentModel.Design Namespace](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.design)
- [DesignerActionList Class](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.design.designeractionlist)

## 🎯 Best Practices

### 1. Property Organization

- Group related properties together
- Use descriptive category names
- Provide helpful property descriptions
- Order properties logically

### 2. Error Handling

- Always check for null references
- Provide meaningful error messages
- Handle edge cases gracefully
- Log errors for debugging

### 3. Performance

- Cache property descriptors when possible
- Minimize service calls
- Use efficient property change detection
- Avoid unnecessary object creation

### 4. Maintainability

- Follow established patterns
- Use consistent naming conventions
- Document complex logic
- Keep action lists focused and organized

## 🚀 Future Considerations

### Potential Enhancements

1. **Dynamic Properties**: Add/remove properties based on control state
2. **Custom Editors**: Implement custom property editors
3. **Validation**: Add property validation and error reporting
4. **Localization**: Support for localized property names and descriptions

### .NET Version Compatibility

- Monitor new .NET versions for compatibility
- Test on new Visual Studio versions
- Update documentation as needed
- Maintain backward compatibility

---

## 📞 Support

For questions, issues, or contributions:

1. **Check Documentation**: Review available documentation first
2. **Test Thoroughly**: Verify issues across multiple frameworks
3. **Provide Details**: Include error messages, steps to reproduce, and environment details
4. **Contribute**: Submit improvements and fixes

---

*This guide is part of the Krypton Suite WinForms Designer Extensibility SDK migration.*  
*Last updated: January 2025*
