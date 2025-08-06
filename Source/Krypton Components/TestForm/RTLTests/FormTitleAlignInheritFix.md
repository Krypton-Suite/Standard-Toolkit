# FormTitleAlign Inherit Assertion Failure Fix

## Problem Description

When `FormTitleAlign` was set to `PaletteRelativeAlign.Inherit`, the application would crash with an assertion failure in the `RenderStandard.RightToLeftIndex` method. The error occurred because the switch statement in this method didn't handle the `Inherit` case, causing it to fall through to the default case which threw an `ArgumentOutOfRangeException`.

## Root Cause

The `PaletteRelativeAlign` enum includes an `Inherit = -1` value, but several switch statements throughout the codebase that handle `PaletteRelativeAlign` were missing explicit cases for `Inherit`. When `Inherit` was passed to these methods, they would fall through to default cases that either:

1. Threw `ArgumentOutOfRangeException` (in `RenderStandard.RightToLeftIndex`)
2. Triggered debug assertions
3. Had no default case, causing unexpected behavior

## Files Fixed

### 1. RenderStandard.cs
- **Location**: `Source/Krypton Components/Krypton.Toolkit/Rendering/RenderStandard.cs`
- **Method**: `RightToLeftIndex(RightToLeft rtl, PaletteRelativeAlign align)`
- **Fix**: Added explicit case for `PaletteRelativeAlign.Inherit` that defaults to Near alignment

### 2. AccurateText.cs
- **Location**: `Source/Krypton Components/Krypton.Toolkit/AccurateText/AccurateText.cs`
- **Method**: Text alignment switch statement
- **Fix**: Added explicit case for `PaletteRelativeAlign.Inherit` that defaults to Near alignment

### 3. KryptonComboBox.cs
- **Location**: `Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonComboBox.cs`
- **Method**: Text format flags switch statement
- **Fix**: Added explicit case for `PaletteRelativeAlign.Inherit` and proper default case

### 4. ViewDrawRibbonGalleryButton.cs
- **Location**: `Source/Krypton Components/Krypton.Ribbon/View Draw/ViewDrawRibbonGalleryButton.cs`
- **Method**: Border path creation switch statement
- **Fix**: Added explicit case for `PaletteRelativeAlign.Inherit` and proper default case

## Fix Implementation

For each switch statement, the fix follows this pattern:

```csharp
switch (align)
{
    case PaletteRelativeAlign.Inherit:
        // For Inherit, default to Near alignment
        return rtl == RightToLeft.Yes ? 2 : 0; // or appropriate default behavior
    case PaletteRelativeAlign.Near:
        // existing Near case
        break;
    case PaletteRelativeAlign.Center:
        // existing Center case
        break;
    case PaletteRelativeAlign.Far:
        // existing Far case
        break;
    default:
        // Should never happen!
        Debug.Assert(false);
        break;
}
```

## Behavior When Inherit is Used

When `PaletteRelativeAlign.Inherit` is encountered, the fix treats it as equivalent to `PaletteRelativeAlign.Near` for alignment purposes. This is a reasonable default behavior since:

1. `Inherit` typically means "use the default behavior"
2. `Near` is the most common default alignment
3. This maintains backward compatibility

## Testing

A test form has been created (`FormTitleAlignInheritTest.cs`) that:

1. Sets `FormTitleAlign = PaletteRelativeAlign.Inherit`
2. Toggles RTL mode to trigger the rendering code
3. Verifies no assertion failures occur
4. Tests comprehensive RTL scenarios

## Impact

- **Positive**: Fixes assertion failures when `FormTitleAlign` is set to `Inherit`
- **Positive**: Improves robustness of RTL handling throughout the codebase
- **Positive**: Maintains backward compatibility
- **Neutral**: No performance impact
- **Neutral**: No breaking changes to existing functionality

## Future Considerations

1. **Consistency**: All switch statements handling `PaletteRelativeAlign` should include explicit `Inherit` cases
2. **Documentation**: Consider documenting the default behavior for `Inherit` values
3. **Testing**: Add unit tests for `Inherit` cases in all affected components
4. **Code Review**: Review other enum switch statements for similar issues

## Related Issues

This fix addresses the specific assertion failure shown in the error dialog, but similar issues may exist with other enum values that have `Inherit` cases. A broader review of the codebase for similar patterns is recommended. 