# Array Conversion Automation Outline

## 1. Problem Statement

- The codebase includes at least forty-one theme/palette classes, each defining private static `Color[]` arrays for scheme-related values.
- Manual refactoring to expose each array entry as a named enum property is prohibitively time-consuming, error-prone, and risks developer burnout.

- The following private static arrays must be folded into the scheme:
        - `_appButtonNormal`
        - `_appButtonPressed`
        - `_appButtonTrack`
        - `_arrowBorderColors`
        - `_buttonBackColors`
        - `_buttonBorderColors`
        - `_colorsB`
        - `_colorsG`
        - `_colorsS`
        - `_ribbonColors`
        - `_ribbonGroupCollapsedBackContext`
        - `_ribbonGroupCollapsedBackContextTracking`
        - `_ribbonGroupCollapsedBorderContext`
        - `_ribbonGroupCollapsedBorderContextTracking`
        - `_schemeBaseColors`
        - `_schemeOfficeColours`
        - `_schemeVisualStudioColors`
        - `_sparkleColors`
        - `_trackBarColors`
        - `_trackBarColours`
        - `_trackColors`

## 2. Conceptual Solution: C# Test Harness

### 2.1 Array Retrieval via PaletteBase API

- Each palette subtype derives from `PaletteBase` and exposes a new `GetStaticColorArrays()` method.
- The harness can instantiate each concrete palette (e.g. via reflection or a pre-defined list of types) and call:

  ```csharp
  var arrays = palette.GetStaticColorArrays();
  ```

- This returns an `IReadOnlyDictionary<string, Color[]>` mapping each static color-array field name to its fully initialized array, eliminating manual reflection.

### 2.2 Static Initialization Guarantee

- The `GetStaticColorArrays()` implementation internally triggers static constructors as needed, so no manual initializer invocation is required.
- If explicit initialization ordering is still necessary, the harness can call:

  ```csharp
  RuntimeHelpers.RunClassConstructor(type.TypeHandle);
  ```

### 2.3 Array Iteration

- For each `(fieldName, array)` entry in the returned dictionary, iterate its elements.
- Record tuples `(fieldName, index, Color value)` for downstream mapping and code generation.

### 2.4 Index-to-Enum Mapping

- Reference existing enums (e.g., `SchemeBaseColors`, `SchemeExtraColors`, `SchemeMenuStripColors`, etc.) to derive semantic names for each index.
- Where enums are incomplete, apply a naming convention based on the original field and index (e.g., `AppButtonTrack0`, `ButtonBorderColors3`).

### 2.5 Code Generation

- Generate C# property stubs for each slot:

  ```csharp
  public override Color AppButtonTrack0 => Color.FromArgb(…);
  ```

- Optionally produce a new `enum EffectiveColorSlot { AppButtonTrack0, … }` and a lookup method.
- Emit files under a designated project folder (e.g., `Generated/ColorSlots/`).

### 2.6 Verification & Coverage

- Compare the set of generated identifiers against a master list of expected slots to identify omissions or duplicates.
- Produce a human-readable report (JSON or Markdown) summarizing any gaps.

## 3. Implementation Details

- Create a standalone .NET console application or a test project targeting the existing toolkit’s runtime (e.g., .NET Framework 4.7.2).
- Add references to `System.Reflection` and, if desired, Roslyn packages for in-process source generation.
- Execute the harness as part of a CI job or local pre-commit step to keep generated code up to date.
- Write outputs to disk and optionally check them into source control.

## 4. Expected Outcome

- A complete, deterministic listing of every color slot previously hidden in private arrays.
- A set of strongly-typed properties or enum entries ready for integration into `KryptonColorSchemeBase` subclasses.
- Significant reduction in manual effort and elimination of human error in step 1 of the palette unification plan.
