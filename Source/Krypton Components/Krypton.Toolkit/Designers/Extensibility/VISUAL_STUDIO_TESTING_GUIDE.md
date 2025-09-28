# Visual Studio Testing Guide: WinForms Designer Extensibility SDK

## 🎯 Overview

This guide provides step-by-step instructions for testing the WinForms Designer Extensibility SDK implementation in Visual Studio to verify that smart tags and action lists are working correctly.

## 📋 Prerequisites

- Visual Studio 2022 or later
- .NET Framework 4.7.2+ or .NET 8+ SDK
- Built Krypton libraries (from the migration)
- Basic familiarity with Visual Studio Windows Forms designer

## 🧪 Testing Checklist

### ✅ Pre-Testing Setup

- [ ] Visual Studio 2022 is installed and up to date
- [ ] .NET Framework 4.7.2+ and .NET 8+ SDKs are installed
- [ ] Krypton libraries have been built successfully
- [ ] Test harness project builds without errors

### ✅ Framework Testing

Test on each target framework:
- [ ] .NET Framework 4.7.2
- [ ] .NET Framework 4.8
- [ ] .NET Framework 4.8.1
- [ ] .NET 8.0-windows
- [ ] .NET 9.0-windows
- [ ] .NET 10.0-windows

## 🚀 Step-by-Step Testing

### Step 1: Create Test Project

1. **Open Visual Studio 2022**
2. **Create New Project**:
   - File → New → Project
   - Select "Windows Forms App" template
   - Choose target framework (start with .NET 8.0-windows)
   - Name: "KryptonDesignerTest"
   - Location: Choose appropriate directory

### Step 2: Add Krypton References

1. **Right-click project** → "Add" → "Project Reference"
2. **Add references** to built Krypton libraries:
   - `Krypton.Toolkit 2022.csproj`
   - `Krypton.Docking 2022.csproj`
   - `Krypton.Navigator 2022.csproj`
   - `Krypton.Workspace 2022.csproj`
   - `Krypton.Ribbon 2022.csproj`

### Step 3: Add Krypton Controls

1. **Open Form Designer** (Form1.cs)
2. **Open Toolbox** (View → Toolbox)
3. **Add Krypton controls** to the form:
   - Drag `KryptonButton` onto the form
   - Drag `KryptonTextBox` onto the form
   - Drag `KryptonLabel` onto the form
   - Drag `KryptonCheckBox` onto the form
   - Drag `KryptonRadioButton` onto the form
   - Add controls from other components as available

### Step 4: Test Smart Tags

1. **Select a Krypton control** on the form
2. **Look for smart tag** (lightning bolt icon) in the top-right corner
3. **Click the smart tag** to open the action list panel
4. **Verify the panel opens** with organized properties

### Step 5: Test Action List Properties

1. **Check property categories**:
   - Appearance (Text, Image, Style, etc.)
   - Behavior (Enabled, Visible, etc.)
   - Data (DataSource, Value, etc.)
   - Layout (Dock, Anchor, etc.)

2. **Test property changes**:
   - Change Text property
   - Change Enabled property
   - Change Visible property
   - Verify changes reflect immediately on the form

### Step 6: Test Undo/Redo

1. **Make a property change** via smart tag
2. **Press Ctrl+Z** to undo
3. **Verify change is reverted**
4. **Press Ctrl+Y** to redo
5. **Verify change is restored**

### Step 7: Test Property Grid Integration

1. **Select a Krypton control**
2. **Open Properties window** (View → Properties Window)
3. **Verify properties appear** in the property grid
4. **Make changes** in both smart tag and property grid
5. **Verify both update** the control consistently

## 🔍 Detailed Testing Scenarios

### Scenario 1: Basic Smart Tag Functionality

**Test**: Verify smart tags appear and function on all Krypton controls

**Steps**:
1. Add various Krypton controls to form
2. Select each control
3. Verify smart tag (lightning bolt) appears
4. Click smart tag to open action list
5. Verify action list opens with properties

**Expected Results**:
- ✅ Smart tag appears on all Krypton controls
- ✅ Action list opens when clicked
- ✅ Properties are organized and categorized
- ✅ Property descriptions are helpful

### Scenario 2: Property Change Reflection

**Test**: Verify property changes reflect immediately in the designer

**Steps**:
1. Select a KryptonButton
2. Open smart tag
3. Change Text property to "Test Button"
4. Verify button text updates immediately
5. Change Enabled property to false
6. Verify button appears disabled

**Expected Results**:
- ✅ Text change reflects immediately
- ✅ Enabled change reflects immediately
- ✅ Visual feedback is accurate
- ✅ No need to rebuild or refresh

### Scenario 3: Cross-Framework Compatibility

**Test**: Verify smart tags work on different target frameworks

**Steps**:
1. Create test project targeting .NET Framework 4.8
2. Add Krypton controls and test smart tags
3. Change target framework to .NET 8.0-windows
4. Rebuild and test smart tags again
5. Repeat for other frameworks

**Expected Results**:
- ✅ Smart tags work on .NET Framework
- ✅ Smart tags work on .NET 8+
- ✅ Behavior is consistent across frameworks
- ✅ No framework-specific issues

### Scenario 4: Complex Control Testing

**Test**: Verify smart tags work on complex controls

**Steps**:
1. Add KryptonDataGridView to form
2. Test smart tag functionality
3. Add KryptonNavigator to form
4. Test smart tag functionality
5. Add KryptonRibbon to form
6. Test smart tag functionality

**Expected Results**:
- ✅ Complex controls have smart tags
- ✅ Action lists contain relevant properties
- ✅ Properties are properly categorized
- ✅ No performance issues

### Scenario 5: Error Handling

**Test**: Verify graceful handling of edge cases

**Steps**:
1. Add Krypton control to form
2. Set invalid property values via smart tag
3. Verify error handling
4. Test with null values
5. Test with extreme values

**Expected Results**:
- ✅ Invalid values are handled gracefully
- ✅ Error messages are helpful
- ✅ No crashes or exceptions
- ✅ Designer remains stable

## 🐛 Troubleshooting

### Common Issues and Solutions

#### Issue 1: Smart Tags Not Appearing

**Symptoms**:
- Lightning bolt icon doesn't appear on Krypton controls
- Controls appear as regular WinForms controls

**Possible Causes**:
- Designer attribute not applied
- Designer class compilation errors
- Missing references

**Solutions**:
1. Check that `[Designer(typeof(...))]` attribute is present on control
2. Verify designer class compiles without errors
3. Ensure all required references are added
4. Clean and rebuild solution

#### Issue 2: Action List Empty or Missing Properties

**Symptoms**:
- Smart tag opens but shows no properties
- Properties appear but don't work

**Possible Causes**:
- Action list class not implemented correctly
- Property names don't match control properties
- Missing property descriptors

**Solutions**:
1. Check action list class implementation
2. Verify property names match actual control properties
3. Ensure properties are public and accessible
4. Check for compilation errors in action list

#### Issue 3: Property Changes Not Reflecting

**Symptoms**:
- Changes made via smart tag don't appear on control
- Changes require rebuild to see

**Possible Causes**:
- Missing change notification
- Incorrect property descriptor usage
- Service access issues

**Solutions**:
1. Verify `SetPropertyValue` method is used
2. Check that `IComponentChangeService` is available
3. Ensure property descriptors are correct
4. Test with simple properties first

#### Issue 4: Build Errors

**Symptoms**:
- Project doesn't build
- Compilation errors related to designers

**Possible Causes**:
- Missing using statements
- Incorrect type references
- Assembly attribute conflicts

**Solutions**:
1. Check for missing using statements
2. Verify type references are correct
3. Clean obj and bin folders
4. Rebuild solution

### Debugging Tips

1. **Enable Designer Debugging**:
   - Set breakpoints in designer and action list classes
   - Use Debug → Attach to Process to debug designer

2. **Check Services**:
   - Verify design-time services are available
   - Use service locator to check service availability

3. **Property Inspection**:
   - Use property grid to verify property values
   - Check property descriptors and types

4. **Build Output**:
   - Check for compilation errors and warnings
   - Verify all dependencies are resolved

## 📊 Test Results Template

### Test Results for [Framework Version]

**Date**: [Date]  
**Tester**: [Name]  
**Visual Studio Version**: [Version]  
**Target Framework**: [Framework]

#### Basic Functionality
- [ ] Smart tags appear on Krypton controls
- [ ] Action lists open when clicked
- [ ] Properties are organized and categorized
- [ ] Property descriptions are helpful

#### Property Changes
- [ ] Text property changes reflect immediately
- [ ] Enabled property changes reflect immediately
- [ ] Visible property changes reflect immediately
- [ ] Other properties work correctly

#### Integration
- [ ] Undo/redo functionality works
- [ ] Property grid integration works
- [ ] No conflicts with other designers
- [ ] Performance is acceptable

#### Error Handling
- [ ] Invalid values handled gracefully
- [ ] No crashes or exceptions
- [ ] Designer remains stable
- [ ] Error messages are helpful

#### Notes
[Any additional observations or issues]

---

## 🎯 Success Criteria

### ✅ Testing is Successful When:

1. **Smart Tags Work**: Lightning bolt appears on all Krypton controls
2. **Action Lists Open**: Clicking smart tag opens property panel
3. **Properties Are Organized**: Properties are categorized logically
4. **Changes Reflect Immediately**: Property changes show instantly
5. **Undo/Redo Works**: Design-time undo/redo functions correctly
6. **Cross-Framework**: Works on all target frameworks
7. **No Errors**: No compilation errors or runtime exceptions
8. **Performance**: No noticeable performance degradation

### ❌ Testing Fails When:

1. **Smart Tags Missing**: Lightning bolt doesn't appear
2. **Action Lists Don't Open**: Clicking smart tag does nothing
3. **Properties Missing**: Expected properties don't appear
4. **Changes Don't Reflect**: Property changes don't show
5. **Undo/Redo Broken**: Design-time undo/redo doesn't work
6. **Framework Issues**: Problems on specific frameworks
7. **Errors Present**: Compilation or runtime errors
8. **Performance Issues**: Noticeable slowdown or freezing

## 📞 Support and Reporting

### If Issues Are Found:

1. **Document the Issue**:
   - Describe what you were testing
   - Include steps to reproduce
   - Note the target framework
   - Include error messages if any

2. **Check Documentation**:
   - Review troubleshooting guide
   - Check for known issues
   - Verify setup requirements

3. **Report the Issue**:
   - Include test results
   - Provide environment details
   - Attach screenshots if helpful
   - Include build output if relevant

### Contributing Improvements:

1. **Test Thoroughly**: Verify fixes work across all frameworks
2. **Document Changes**: Update documentation as needed
3. **Follow Patterns**: Use established implementation patterns
4. **Quality Assurance**: Ensure no regressions are introduced

---

*This testing guide is part of the Krypton Suite WinForms Designer Extensibility SDK migration.*  
*Last updated: January 2025*
