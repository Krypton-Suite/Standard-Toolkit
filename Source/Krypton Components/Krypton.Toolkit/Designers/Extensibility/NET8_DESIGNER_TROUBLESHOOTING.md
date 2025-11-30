# .NET 8+ Designer Troubleshooting Guide

## Issue: Action Lists and Smart Tags Not Working on .NET 8+

### Problem Description
Action lists and smart tags work correctly on .NET Framework but do not appear or function on .NET 8+ projects.

### Root Cause
.NET 8+ uses an **out-of-process Windows Forms Designer** which has different requirements compared to the in-process designer used in .NET Framework.

## Architecture Differences

### .NET Framework
- Designer runs **in-process** with Visual Studio
- Direct access to design-time services
- Seamless integration of action lists and smart tags

### .NET 8+
- Designer runs **out-of-process** (separate from Visual Studio)
- Limited access to design-time services
- Requires different implementation patterns

## Potential Solutions

### 1. Verify Designer Assembly Loading

The out-of-process designer may not be loading the designer assemblies correctly.

**Check:**
- Designer attributes are properly applied
- Designer assemblies are in the correct location
- No missing dependencies

### 2. Update Designer Implementation

The current implementation may need modifications for out-of-process compatibility.

**Required Changes:**
- Service access patterns
- Action list registration
- Property change notification

### 3. Assembly Binding and Loading

The out-of-process designer may have different assembly loading behavior.

**Solutions:**
- Ensure all designer dependencies are available
- Check for assembly binding redirects
- Verify correct framework targeting

## Diagnostic Steps

### Step 1: Test Simple Designer
Create a minimal designer to test basic functionality:

```csharp
[Designer(typeof(SimpleTestDesigner))]
public class TestControl : Control
{
}

public class SimpleTestDesigner : ControlDesigner
{
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            actionLists.Add(new SimpleTestActionList(this));
            return actionLists;
        }
    }
}

public class SimpleTestActionList : DesignerActionList
{
    public SimpleTestActionList(IComponent component) : base(component)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();
        items.Add(new DesignerActionHeaderItem("Test"));
        items.Add(new DesignerActionTextItem("Test Action", "Test"));
        return items;
    }
}
```

### Step 2: Check Visual Studio Output
1. Open Visual Studio
2. Go to View → Output
3. Select "Show output from: General" or "Design"
4. Look for error messages when opening the designer

### Step 3: Enable Designer Logging
Add diagnostic logging to designer classes:

```csharp
public override DesignerActionListCollection ActionLists
{
    get
    {
        System.Diagnostics.Debug.WriteLine("ActionLists called for " + Component?.GetType().Name);
        // ... rest of implementation
    }
}
```

## Known Issues and Workarounds

### Issue 1: Designer Not Loading
**Symptoms**: Designer shows source code instead of visual designer
**Solution**: Clean and rebuild, check project file configuration

### Issue 2: Action Lists Empty
**Symptoms**: Smart tag appears but no actions
**Solution**: Verify action list implementation and property descriptors

### Issue 3: Services Not Available
**Symptoms**: IComponentChangeService or other services are null
**Solution**: Update service access patterns for out-of-process model

## Microsoft Documentation References

- [WinForms Designer 64-bit Path Forward](https://devblogs.microsoft.com/dotnet/winforms-designer-64-bit-path-forward/)
- [Windows Forms Designer Extensibility](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/designer-extensibility/)
- [Troubleshooting Design-Time Issues](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/troubleshooting-design-time-issues/)

## Community Resources

- [GitHub: WinForms Designer Issues](https://github.com/dotnet/winforms/issues)
- [Stack Overflow: WinForms Designer .NET 8](https://stackoverflow.com/questions/tagged/winforms+.net-8.0)

## Next Steps

1. **Test with Simple Designer**: Create minimal test case
2. **Check Visual Studio Output**: Look for error messages
3. **Update Implementation**: Modify for out-of-process compatibility
4. **Report Issues**: If problems persist, report to Microsoft

---

*This troubleshooting guide is part of the Krypton Suite WinForms Designer Extensibility SDK migration.*  
*Last updated: January 2025*
