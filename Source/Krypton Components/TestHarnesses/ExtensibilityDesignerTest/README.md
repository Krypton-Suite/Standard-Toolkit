# Extensibility Designer Test

This test harness validates the WinForms Designer Extensibility SDK implementation for Krypton controls.

## Purpose

This test project demonstrates the new extensibility designers for:
- KryptonButton
- KryptonLabel  
- KryptonTextBox

## Testing Instructions

1. Build the project
2. Open the form in Visual Studio designer
3. Verify that the controls appear correctly in the designer
4. Test the smart tag (action list) functionality
5. Verify property changes are properly serialized to the designer file
6. Test drag and drop operations
7. Verify that the controls work correctly at runtime

## Expected Behavior

- Controls should render properly in the designer
- Smart tags should show the appropriate properties
- Property changes should be reflected in the designer file
- Controls should function normally at runtime
- No designer-related exceptions should occur

## Notes

This is a proof-of-concept implementation to validate the migration approach from the legacy System.ComponentModel.Design to the new WinForms Designer Extensibility SDK.
