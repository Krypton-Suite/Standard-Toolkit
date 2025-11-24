# Strongly-typed Color Schemes in Krypton Toolkit

## Overview

Most full palettes in the **Krypton Toolkit** stored its main colors in two parallel `Color[]` arrays:

* `_schemeBaseColors` – ~230 general UI colors ordered like `SchemeBaseColors` (previously: _schemeOfficeColors)
* `_trackBarColors`   – 6 specialised colors for `TrackBar` controls

The arrays had to stay perfectly aligned with their respective enumerations – a single
missing or mis-ordered entry caused subtle glitches or run-time crashes.  Reviewing
such numeric arrays was painful and their effect needed manual, visual review.

The new **strongly-typed color-scheme architecture** eliminates these problems by
replacing both arrays withing a self-describing class and comes with tiny helper extensions.
The SchemeBaseColors enumerator has been expanded by 6 new values to cover TrackBar-colors
that were separately stored in a `_trackBarColors` array.

For the main, initial migration those 2 arrays were chosen to be converted and removed (in a final step). For the sake of reviewability other arrays could later be processed with additional tooling.

| Type | Role |
|------|------|
| `KryptonColorSchemeBase` | Abstract base class that declares one `Color` property per value of `SchemeBaseColors`.  Concrete *“..._BaseScheme”* classes store the actual colors. |
| `SchemeBaseColorsExtensions` | Converts a `KryptonColorSchemeBase` into the legacy `Color[]` layouts via `ToArray()` and `ToTrackBarArray()` (6 entries). |

Nothing in the low-level rendering pipeline had to change – all internal code still
works with simple arrays – but **every public API writen or reviewed becomes
readable and type-safe**.

## Current migration workflow (SchemeGenerator v2)

The `kptheme genscheme` command is the single tool used to maintain, verify and migrate palette files.
It supports **two orthogonal switches**:

* `--migrate`   – perform source-level changes in the palette files themselves (array removal, ctor rewrites…)
* `--dry-run`   – simulate everything **in-memory only** and print a validation report instead of touching any file

### 1. Palette categories

SchemeGenerator distinguishes between two kinds of palette classes:

1. **Family-base palettes** – concrete abstract bases that sit directly on `PaletteBase` and have many derived “theme” subclasses.
   • `PaletteMicrosoft365Base`
   • `PaletteOffice2007Base`
   • `PaletteOffice2010Base`
   • `PaletteOffice2013Base`
   • `PaletteSparkleBase`
   • `PaletteSparkleBlueDarkModeBase`
   • `PaletteVisualStudioBase`

   They **retain the legacy `_ribbonColors` array for one more release**.  Migration only
   – adds a strongly-typed constructor that forwards the scheme to the existing array,
   – injects `[Obsolete(..., false)]` on every `Color[]` constructor,
   – ensures a nullable `BaseColors` field is present.  No array removal or index-to-property conversion is executed yet.

2. **Theme palettes** – every other concrete palette class.  These are migrated fully: colour arrays are deleted, all `[...]` accesses become `BaseColors.<Property>` (in non-base palettes!), and a helper overload taking a `KryptonColorSchemeBase` is inserted (until the next major version when the legacy `Color[]` ctor disappears entirely).  This now also covers Sparkle, Professional, and Visual Studio theme variants.

### 2. Typical commands

• **Preview everything, no writes – safe!**

```bash
kptheme genscheme --migrate --dry-run -d "Source/Krypton Components/Krypton.Toolkit/Palette Builtin"
```

The generator runs all transformations in memory and prints
`[CHECK OK]` / `[CHECK FAIL]` lines for each palette.  Nothing on disk is modified.

• **Generate scheme classes only**

```bash
kptheme genscheme -f PaletteMicrosoft365Black.cs
```

This creates / updates `Schemes/PaletteMicrosoft365Black_BaseScheme.cs` next to the palette but leaves the palette source untouched.

• **Full migration pass** (use after the dry-run is clean!)

```bash
kptheme genscheme --migrate -d "Palette Builtin" --overwrite
```

Writes scheme files *and* updates palette sources in place.  Family-bases keep their arrays, themes lose them.

### 3. Built-in validation in dry-run mode

During `--dry-run` SchemeGenerator simulates the transformation and then asserts that the resulting text obeys the expected rules.

Family-base checks:
    - `[Obsolete]` attribute present on every `Color[]` constructor
    - `_ribbonColors` array still declared
    - No `BaseColors.` property usages
    - New `KryptonColorSchemeBase` overload exists

Theme palette checks:
    - No residual `_ribbonColors[...]` indexers
    - At least one `BaseColors.` property usage
    - `[Obsolete]` attribute present on the legacy ctor

A failing check is printed as `[CHECK FAIL] PaletteName: message` and the process exits with a non-zero status.

### 4. Release timeline

• **Current release** – V100: dual constructors, arrays still available in family-bases, marked obsolete.

• **Next major release** – V110: obsolete array-based constructors will be removed; family-bases will finally drop the arrays and adopt the same strong-typed implementation as themes.

---

## `KryptonColorSchemeBase` (new)

* Purely abstract – **no arrays, no collections**.
* ~239 auto-properties, one per `SchemeBaseColors` enum.
* Every color is accessible by a meaningful name (e.g. `HeaderText`).
* All properties are writable so advanced users can tweak a live palette at run-time
  while the toolkit reflects the changes instantly.

## Usability improvement – base-class overload

`PaletteMicrosoft365Base` (and its descendants) gained an additional constructor that
accepts a `KryptonColorSchemeBase`.  Internally the palette converts the scheme into
the two required arrays via the extension methods shown above (till end of migration).

```csharp
// Modern, safer way – pass a strongly-typed scheme in Palette-family `Base` class
protected readonly KryptonColorSchemeBase? BaseColors;

public PaletteMicrosoft365Black() :
    base(new PaletteMicrosoft365Black_BaseScheme(),
         _checkBoxList,
         _galleryButtonList,
         _radioButtonArray,
         new PaletteMicrosoft365Black_BaseScheme().ToTrackBarArray()) { }
```

### Why this matters in day-to-day coding

* **Named access instead of magic indexes** – write `BaseColors.HeaderText`
  instead of `_ribbonColors[(int)SchemeBaseColors.HeaderText]`.

* **Compiler enforcement** – adding a value to `SchemeBaseColors` will
  immediately highlight every incomplete scheme class.

* **Safer refactoring** – re-ordering an enum no longer risks silently shuffling
  array entries.

* **Clean IntelliSense** – IDEs list meaningful property names and documentation.

## Migration plan

For this migration a certain amount of automation is available, but only palettes
that use a Color[] array named `_schemeBaseColors` and `_trackBarColors`, like
the Microsoft365 and Office palette families.

Follow these three steps to migrate an existing palette:

1. Execute the C# helper located in `Source/Krypton Components/kptheme/` to
   generate one or more `Palette<Theme>_BaseScheme.cs` files in their own
   `Schemes` subfolder. See runtime help for all options!

   Notes: By default source files stay unchanged, existing files won't be overwritten
   and files are stored in subfolder "Schemes" of the source file location.
   With the help of its options, dry-runs are possible; a collective output folder
   can be specified; overwrite mode can be enabled, and - last but not least -
   the "migrate" mode may instruct the script to actually remove the color arrays
   from the source palette file and adapt the ctor (very basic edit).
   The generation of the scheme files can be repeated as long as the arrays exist,
   thus their removal should only be done on the final generation run!
   This is especially helpful while a developer is working on the `SchemeGenerator` class.

2. Commit the newly created `Palette<NAME>_BaseScheme.cs` file(s) and verify
   that the script removed both `_schemeBaseColors` **and** `_trackBarColors`
   fields from the file(s) - in the very last generation run!

3. Ensure the palette constructor now looks like the snippet shown earlier.
   For the `base` class of the derived palettes, that are derived off of `PaletteBase`,
   apply additional code snippets as outlined below in section
   "## Changes made to the `PaletteMicrosoft365Base`"

### Generator highlights

* **Dual-array support** – aligns the six TrackBar colors automatically.
* **Index validation** – reports missing / out-of-order / superfluous entries.
* **Idempotent** – never overwrites an existing scheme file, unless option is set.
* **Optional --migrate switch** – removes the obsolete arrays and rewires the constructor.

Gladly, all palette files' color arrays have all their values commented
with their intended enum name. These are being used by the script to correlate
each enum name with the position in the coded array. If at any position a differently
named enum appeared, but the `SchemeBaseColors` enum at that line was different,
this would mean an enum value was missing.
In this rare case, the script would note this for the class implementation as
"// missing value" and assign an EMPTY_COLOR to that enum property.

### Generator common pitfalls

During processing of Palette365*.cs files' color arrays, the script had to be
written to cover multiple syntactical occurences, that could've prevented the
recognition and mapping of colors to enum values.
In the end the Roslyn code analysis tools were integrated for the most precise parsing.

* Problem 1: script could wrongly count `[` as index 0 and result in shifted indexes

```cs
    private static readonly Color[] _schemeBaseColors =
    [
        Color.FromArgb(21, 66, 139), // TextLabelControl
        Color.FromArgb(21, 66, 139), // TextButtonNormal
```cs

* Problem 2: multiple comments, script tries only first, decides "not found"
        
```cs
Color.FromArgb(134, 179, 236), //(155, 187, 227), // RibbonMinimizeBarDark
```

* Problem 3a: statements across lines, with comments potentially in between params

```cs
        Color.FromArgb(55, 100, 160), // RibbonQATOverflow2
        Color.FromArgb(140, 172,
            211), // RibbonGroupSeparatorDark
        Color.FromArgb(248, 250,
            252), // RibbonGroupSeparatorLight
        Color.FromArgb(192, 212,
            241), // ButtonClusterButtonBack1
        Color.FromArgb(200, 219,
            238), // ButtonClusterButtonBack2
        Color.FromArgb(155, 183,
            224), // ButtonClusterButtonBorder1
        Color.FromArgb(117, 150,
            191), // ButtonClusterButtonBorder2
        Color.FromArgb(213, 228, 242), // NavigatorMiniBackColor
```

* Problem 3b: similar, but no `;` just before end of array

```cs
        Color.FromArgb(201, 217,
            239) // ToolTipBottom
    ];
```

Any originally missing enum by name is marked accordingly in the result,
while appropriately keeping the mapping of colors to subsequent entries.

```cs
    public override Color RibbonGroupButtonText  { get; set; } = GlobalStaticValues.EMPTY_COLOR; // missing value
```

Here the `RibbonGroupButtonText` value was NOT present in the original color array
*by comment* at the expected position and marked accordingly.
This had no influence over any subsequent enum-value pairs following that position
as those were fully preserved.

## Compatibility considerations

* **Binary compatibility** is maintained – no public signatures were removed.
* Still runs on **.NET Framework 4.7.2**.
* Palettes not yet migrated continue to work unchanged.

## Contributing guidelines for new palettes

1. Always create a `KryptonColorSchemeBase` implementation.
2. Pass that scheme to the palette base constructor; **never** pass raw `Color[]`.
3. When extending `SchemeBaseColors` also update `KryptonColorSchemeBase` and
   revisit all existing schemes – the compiler will be the guide.

## Runtime color modification

`PaletteMicrosoft365Base` exposes helper methods to alter colors on the fly:

* `SetSchemeColor(SchemeBaseColors index, Color value)`
* `GetSchemeColor(SchemeBaseColors index)`
* `UpdateSchemeColors(Dictionary<SchemeBaseColors, Color> changes)`
* `ApplyScheme(KryptonColorSchemeBase newScheme)`

Once ALL palette families have been fully migrated, these methods shall then
be refactored out of any base class into the `PaletteBase` class!
This is a step for a future pull request.

**Usage example:**

```csharp
// 1. Create palette (installs its default scheme)
var black = new PaletteMicrosoft365Black();

// 2. Tweak a few colors
black.SetSchemeColor(SchemeBaseColors.TextLabelControl, Color.Gold);
black.UpdateSchemeColors(new()
{
    [SchemeBaseColors.PanelClient]       = Color.FromArgb(30, 30, 30),
    [SchemeBaseColors.ButtonNormalBack1] = Color.DarkBlue,
});

// 3. Swap the whole scheme
var highContrast = new PaletteMicrosoft365Black_BaseScheme
{
    TextLabelControl = Color.White,
    PanelClient      = Color.Black
};
black.ApplyScheme(highContrast);
```

### Forcing the `ColorTable` to update

The helper methods above invalidate the cached `ColorTable` for you, but **if you ever mutate `_ribbonColors` directly (advanced scenarios, reflection, debugging tools)** make sure to reset the cache manually:

```csharp
palette.Table = null;   // next access to palette.ColorTable will rebuild from current colors
```

`ColorTable` is rebuilt lazily, so clearing the reference is cheap; the heavy work only happens on the following paint cycle.

Changes are reflected immediately; call `Invalidate()` on open windows if the
toolkit is unable to refresh automatically.

Note: the above mentioned methods already include this reset!

## How the wiring works

1. **Scheme → Arrays** – extension method `ToArray()` converts every property to
   a 1-dimensional `Color[]`.  `ToTrackBarArray()` extracts the six specialised
   TrackBar colors.
2. **Internal storage** – the palette keeps both arrays in private fields for
   ultra-fast indexed access during rendering.
3. **Color table generation** – `PaletteBase.ColorTable` is overridden like this:

```csharp
public override KryptonColorTable ColorTable =>
    Table ??= new KryptonColorTable365(
        BaseColors?.ToArray()     // preferred – always reflects the current scheme
        ?? _ribbonColors,         // fallback for legacy palettes without BaseColors
        InheritBool.True,
        this);
```

If you modify `BaseColors` (via `SetSchemeColor`, `ApplyScheme`, etc.) the cache is cleared automatically; the next paint will rebuild the table from the updated scheme.
4. **Runtime updates** – the modification methods above mutate the arrays
   directly (or `BaseColors` when present) and set `Table = null` to ensure the
   next UI paint uses the new colors.
5. **Enumeration safety** – property names in scheme classes exactly match the
   enumeration values, guaranteeing type-safe round-trips.

The result combines the **performance of arrays** with the **safety and
discoverability of expressive property names**.

---

## Changes made to the `PaletteMicrosoft365Base`

With below list of complete changes made to one of the family palette's base class,
any other base class, descendent of `PaletteBase`, can be migrated to the
to handling the new `KryptonColorSchemeBase`-based classes.

### 1. **Added new field for storing strongly-typed color scheme**

As protected member this is accessible in all derived, instantiated classes as their main storage for its scheme, containing all `SchemeBaseColors`-enum and TrackBar related colors:

```csharp
protected readonly KryptonColorSchemeBase? BaseColors;
```

### 2. **Added thread-safety lock object**

```csharp
private readonly object _schemeColorsLock = new object();
```

### 3. **Parameter name standardization**

Optional, but for consistency all occurrences of British spelling
was changed to American spelling:
    - `schemeColours` → `schemeColors`
    - `trackBarColours` → `trackBarColors`

### 4. **Added new constructor overload**

The existing palette's constructor is kept as it is, which often fills the internal arrays.

For now, we preserve the internally used `Color[]` array members as
a) most of the codebase relies on arrays
b) arrays are not a performance issue here

Thus we use the new extension method `ToArray()` to pass the colors down
to the legacy constructor, but store the actual scheme in the new `BaseColors` member.

```csharp
/// <summary>
/// Overload that accepts any KryptonColorSchemeBase implementation.
/// Converts it to a Color[] and forwards to the main constructor.
/// </summary>
protected PaletteMicrosoft365Base(
    KryptonColorSchemeBase scheme,
    ImageList checkBoxList,
    ImageList galleryButtonList,
    Image?[]  radioButtonArray,
    Color[] trackBarColors)
    : this(scheme.ToArray(),
           checkBoxList,
           galleryButtonList,
           radioButtonArray,
           trackBarColors)
{
    BaseColors = scheme;
}
```

### 5. **Modified ColorTable property to use BaseColors**

Non-standard formatting used to highlight the explanations.

```csharp
/// <summary>
/// Gets access to the color table instance.
/// </summary>
public override KryptonColorTable ColorTable =>
    Table ??= new KryptonColorTable365(
        BaseColors?.ToArray()   // preferred – always reflects the current scheme
        ?? _ribbonColors,       // fallback for legacy palettes without BaseColors
        InheritBool.True,
        this);
```

### 6. **Added new Palette Helpers region with runtime color modification methods**

Note: this may be implemented only in select palette family base classes!

#### 6a. Protected property to expose ribbon colors array

```csharp
// Make the color array accessible for modification
protected Color[] RibbonColors => _ribbonColors;
```

#### 6b. Thread-safe single-color setter

```csharp
// Thread-safe single-color setter
public void SetSchemeColor(SchemeBaseColors colorIndex, Color newColor)
{
    lock (_schemeColorsLock)
    {
        _ribbonColors[(int)colorIndex] = newColor;
        Table = null; // force color-table regeneration
    }
}
```

#### 6c. Thread-safe single-color getter

```csharp
// Thread-safe single-color getter
public Color GetSchemeColor(SchemeBaseColors colorIndex)
{
    lock (_schemeColorsLock)
    {
        return _ribbonColors[(int)colorIndex];
    }
}
```

#### 6d. Thread-safe batch update method

```csharp
// Thread-safe batch update
public void UpdateSchemeColors(Dictionary<SchemeBaseColors, Color> colorUpdates)
{
    if (colorUpdates is null)
        throw new ArgumentNullException(nameof(colorUpdates));

    lock (_schemeColorsLock)
    {
        foreach (var update in colorUpdates)
        {
            _ribbonColors[(int)update.Key] = update.Value;
        }

        Table = null; // force color-table regeneration
    }
}
```

#### 6e. Thread-safe full scheme replacement method

```csharp
// Thread-safe full-scheme replacement
public void ApplyScheme(KryptonColorSchemeBase newScheme)
{
    if (newScheme is null)
        throw new ArgumentNullException(nameof(newScheme));

    var newColors = newScheme.ToArray();

    lock (_schemeColorsLock)
    {
        Array.Copy(newColors, _ribbonColors, newColors.Length);
        Table = null; // force color-table regeneration
    }
}
```

These changes introduce a strongly-typed color scheme architecture that allows for:

* Type-safe color management through the `KryptonColorSchemeBase` abstraction
* Runtime color modification with thread safety
* Backward compatibility with existing `Color[]` arrays
* Improved maintainability and discoverability of color properties

---

### Changes made to `PaletteMicrosoft365Base` Derived Classes

Below `PaletteMicrosoft365Black.cs` serves as an example for changes to any derived classes.
Most of the code edits are automated via the earlier mentioned `kptheme` generator app.

#### 1. **Create New Base Scheme Class (automated)**

Conversion of the color arrays into a new, concrete scheme class file:
    - Filename: `PaletteMicrosoft365Black_BaseScheme.cs` (following the pattern `Palette[Name]_BaseScheme.cs`)
    - This class contains all the color definitions that were previously in the arrays

#### 2. **Remove Color Arrays (automated)**

Removal of the following static color array declarations:

```csharp
// REMOVE these arrays:
private static readonly Color[] _trackBarColors = [...]
private static readonly Color[] _schemeBaseColors = [...]
```

These arrays contained all the individual color value definitions for the palette.

#### 3. **Update Constructor to Use Base Scheme Class**

The class makes use of the extension methods `ToArray` and `ToTrackBarArray()`.

Replace the constructor implementation to use a new base scheme pattern:

```csharp
// OLD (non-static!) Constructor:
public PaletteMicrosoft365Black() : base(_schemeBaseColors, _checkBoxList, _galleryButtonList, _radioButtonArray, _trackBarColors)
{
}

// NEW Constructor using the new scheme class:
public PaletteMicrosoft365Black() : base(
    new PaletteMicrosoft365Black_BaseScheme(),
    _checkBoxList,
    _galleryButtonList,
    _radioButtonArray,
    new PaletteMicrosoft365Black_BaseScheme().ToTrackBarArray())
{
}
```

Here the base is `PaletteMicrosoft365Base` class, that will store the scheme in member `BaseColors`.

### Summary of Implementation Steps

1. Implied/automated: create a new `Palette[Name]_BaseScheme.cs` file (in `Schemes` subdirectory)
2. Remove all inline color array declarations (`_trackBarColors` and `_schemeBaseColors`)
3. Update the constructor to instantiate a new `Palette[Name]_BaseScheme` class
4. Pass the base scheme instance as the first parameter to the base constructor
5. Call `ToTrackBarArray()` on a new instance of the base scheme class for the last parameter

This refactoring moves the color definitions out of the main palette class into a dedicated scheme class, making the code more modular and maintainable.

## Text replacement

Search regex:
`_ribbonColours\[[(]int[)]SchemeBaseColors\.([A-Za-z0-9_]+)\]`

Replacement:
`BaseColors.$1`

---

## Recent Architecture Improvements

### Unified Color Scheme Access

The toolkit now provides a consistent, reflection-free API for runtime color manipulation across all palette families:

#### 1. **`SchemeColors` Property**

All palette classes now expose their internal color array through an abstract property:

```csharp
// In PaletteBase:
protected abstract Color[] SchemeColors { get; }

// In derived palette families (e.g., PaletteMicrosoft365Base):
protected override Color[] SchemeColors => _ribbonColors;

// In KryptonCustomPaletteBase (delegates to wrapped palette):
protected override Color[] SchemeColors => _basePalette?.GetSchemeColors() ?? Array.Empty<Color>();
```

This eliminates the need for reflection to access private fields when updating colors at runtime.

#### 2. **`CopySchemeColors` Method**

`PaletteBase` now includes a protected helper method for safely updating the color scheme:

```csharp
protected void CopySchemeColors(Color[] source)
{
    // Thread-safe array copy
    lock (_colorLock)
    {
        Array.Copy(source, SchemeColors, Math.Min(source.Length, SchemeColors.Length));
        
        // Clear cached color table (handles Table, _table, or property variants)
        InvalidateColorTableCache();
    }
    
    // Trigger UI refresh
    OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
}
```

The method handles:
    - Thread-safe color updates
    - Automatic cache invalidation (works with `Table`, `_table` fields, or `Table` properties)
    - Immediate UI refresh via `PalettePaint` event

#### 3. **`ApplyBaseColors` Public API**

`KryptonCustomPaletteBase` exposes a simple public method for theme editors:

```csharp
public bool ApplyBaseColors(Color[] colors)
{
    if (colors == null) throw new ArgumentNullException(nameof(colors));
    if (_basePalette == null) return false;
    
    CopySchemeColors(colors);
    return true;
}
```

### Benefits for Palette Designer and Runtime Updates

1. **No Reflection Required** - All color updates use public or protected APIs
2. **Deterministic Behavior** - `CopySchemeColors` handles all palette families consistently
3. **Thread-Safe** - Built-in locking prevents race conditions during color updates
4. **Automatic UI Updates** - `PalettePaint` event ensures immediate visual feedback

### Usage in Theme Editors

When implementing "Load Default Palette" or similar features:

```csharp
// 1. Get colors from source palette (e.g., via BaseScheme)
var schemeColors = sourceScheme.ToArray();

// 2. Apply to custom palette - no reflection needed!
if (customPalette.ApplyBaseColors(schemeColors))
{
    // Success - UI automatically refreshes
}
else
{
    // Fallback to PopulateFromBase() for full clone
    customPalette.PopulateFromBase(false);
}
```

This architecture ensures that runtime color manipulation is:
    - **Safe** - No private field access or reflection hacks
    - **Fast** - Direct array operations with minimal overhead
    - **Maintainable** - Clear API boundaries between palette layers
