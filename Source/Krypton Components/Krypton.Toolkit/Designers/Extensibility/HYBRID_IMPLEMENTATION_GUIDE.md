# Hybrid Designer Implementation Guide

## Overview
This guide provides instructions for implementing the hybrid designer approach across all Krypton controls to ensure compatibility with both .NET Framework and .NET 8+.

## Implementation Pattern

### 1. Designer Attribute Pattern
```csharp
#if NET8_0_OR_GREATER
[Designer(typeof(ControlNameSimpleDesigner))]
#else
[Designer(typeof(ControlNameExtensibilityDesigner))]
#endif
```

### 2. Simple Designer Structure
```csharp
internal class ControlNameSimpleDesigner : ControlDesigner
{
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            if (Component != null)
            {
                actionLists.Add(new ControlNameSimpleActionList(Component));
            }
            return actionLists;
        }
    }
}
```

### 3. Simple Action List Structure
```csharp
internal class ControlNameSimpleActionList : DesignerActionList
{
    private readonly ControlType _control;

    public ControlNameSimpleActionList(IComponent component) : base(component)
    {
        _control = (ControlType)component;
    }

    // Properties with NotifyPropertyChanged
    public string Text
    {
        get => _control.Text;
        set
        {
            if (_control.Text != value)
            {
                _control.Text = value;
                NotifyPropertyChanged("Text", value);
            }
        }
    }

    private void NotifyPropertyChanged(string propertyName, object? value)
    {
        var changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        if (changeService != null)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(_control)[propertyName];
            changeService.OnComponentChanged(_control, propertyDescriptor, null, value);
        }
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();
        if (_control != null)
        {
            actions.Add(new DesignerActionHeaderItem("Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Control text"));
            // Add more properties as needed
        }
        return actions;
    }
}
```

## Controls Updated

### ✅ Completed
- **KryptonButton**: `KryptonButtonSimpleDesigner.cs`
- **KryptonTextBox**: `KryptonTextBoxSimpleDesigner.cs`
- **KryptonLabel**: `KryptonLabelSimpleDesigner.cs`

### 🔄 In Progress
- **KryptonCheckBox**: Needs simple designer
- **KryptonRadioButton**: Needs simple designer
- **KryptonComboBox**: Needs simple designer
- **KryptonListBox**: Needs simple designer
- **KryptonPanel**: Needs simple designer
- **KryptonGroupBox**: Needs simple designer
- **KryptonProgressBar**: Needs simple designer
- **KryptonTrackBar**: Needs simple designer
- **KryptonNumericUpDown**: Needs simple designer
- **KryptonDateTimePicker**: Needs simple designer
- **KryptonMonthCalendar**: Needs simple designer
- **KryptonTreeView**: Needs simple designer
- **KryptonListView**: Needs simple designer
- **KryptonDataGridView**: Needs simple designer
- **KryptonRichTextBox**: Needs simple designer
- **KryptonMaskedTextBox**: Needs simple designer
- **KryptonDomainUpDown**: Needs simple designer
- **KryptonCheckSet**: Needs simple designer
- **KryptonBreadCrumb**: Needs simple designer
- **KryptonCommand**: Needs simple designer
- **KryptonCommandLinkButton**: Needs simple designer
- **KryptonDropButton**: Needs simple designer
- **KryptonSplitButton**: Needs simple designer
- **KryptonToggleButton**: Needs simple designer
- **KryptonCheckButton**: Needs simple designer
- **KryptonRadioButton**: Needs simple designer
- **KryptonLinkLabel**: Needs simple designer
- **KryptonWrapLabel**: Needs simple designer
- **KryptonLinkWrapLabel**: Needs simple designer
- **KryptonHeader**: Needs simple designer
- **KryptonHeaderGroup**: Needs simple designer
- **KryptonGroup**: Needs simple designer
- **KryptonGroupPanel**: Needs simple designer
- **KryptonSeparator**: Needs simple designer
- **KryptonBorderEdge**: Needs simple designer
- **KryptonContextMenu**: Needs simple designer
- **KryptonContextMenuStrip**: Needs simple designer
- **KryptonToolStrip**: Needs simple designer
- **KryptonMenuStrip**: Needs simple designer
- **KryptonStatusStrip**: Needs simple designer
- **KryptonScrollBar**: Needs simple designer
- **KryptonScrollableControl**: Needs simple designer
- **KryptonSplitContainer**: Needs simple designer
- **KryptonTabControl**: Needs simple designer
- **KryptonPropertyGrid**: Needs simple designer
- **KryptonColorButton**: Needs simple designer
- **KryptonFontButton**: Needs simple designer
- **KryptonForm**: Needs simple designer
- **KryptonMessageBox**: Needs simple designer
- **KryptonInputBox**: Needs simple designer
- **KryptonTaskDialog**: Needs simple designer
- **KryptonManager**: Needs simple designer
- **KryptonCustomPaletteBase**: Needs simple designer

## Implementation Steps

### Step 1: Create Simple Designer
1. Copy existing simple designer as template
2. Update class names and control type
3. Identify key properties for action list
4. Implement property getters/setters with change notification

### Step 2: Update Control
1. Add conditional compilation directives
2. Reference both designer types
3. Test compilation

### Step 3: Test
1. Build for .NET 8+ target
2. Open in Visual Studio designer
3. Verify smart tags appear
4. Test property changes

## Key Properties by Control Type

### Button Controls
- Text, ButtonStyle, PaletteMode

### Text Controls
- Text, CueHintText, PasswordChar, Multiline, PaletteMode

### Label Controls
- Text, ExtraText, Image, LabelStyle, PaletteMode

### List Controls
- Text, SelectedIndex, Sorted, PaletteMode

### Container Controls
- Text, Caption, PaletteMode

### Input Controls
- Text, Value, Minimum, Maximum, Step, PaletteMode

## Testing Checklist

### ✅ Build Test
- [ ] Compiles without errors
- [ ] No warnings (except known ones)
- [ ] All target frameworks build

### ✅ Designer Test
- [ ] Smart tags appear on .NET 8+
- [ ] Smart tags appear on .NET Framework
- [ ] Action lists open correctly
- [ ] Property changes work
- [ ] Undo/redo works

### ✅ Runtime Test
- [ ] Controls function normally
- [ ] No performance degradation
- [ ] No memory leaks

## Automation Script

### PowerShell Script to Update All Controls
```powershell
# Get all control files
$controlFiles = Get-ChildItem -Path "Source/Krypton Components/Krypton.Toolkit/Controls Toolkit" -Filter "Krypton*.cs" -Recurse

foreach ($file in $controlFiles) {
    $content = Get-Content $file.FullName -Raw
    
    # Check if file has Designer attribute
    if ($content -match '\[Designer\(typeof\(([^)]+)\)\)\]') {
        $currentDesigner = $matches[1]
        $controlName = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)
        
        # Replace with hybrid approach
        $newContent = $content -replace '\[Designer\(typeof\([^)]+\)\)\]', @"
#if NET8_0_OR_GREATER
[Designer(typeof(${controlName}SimpleDesigner))]
#else
[Designer(typeof(${currentDesigner}))]
#endif
"@
        
        Set-Content -Path $file.FullName -Value $newContent
        Write-Host "Updated $($file.Name)"
    }
}
```

## Next Steps

1. **Create remaining simple designers** for all controls
2. **Update all control files** with hybrid approach
3. **Test across all frameworks** (.NET Framework 4.7.2, 4.8, 4.8.1, .NET 8, 9, 10)
4. **Document any issues** and workarounds
5. **Update developer documentation** with new patterns

## Success Criteria

- ✅ All controls build successfully
- ✅ Smart tags work on .NET 8+
- ✅ Smart tags work on .NET Framework
- ✅ No performance regression
- ✅ Maintains backward compatibility

---

*This guide provides a systematic approach to implementing the hybrid designer solution across all Krypton controls.*
