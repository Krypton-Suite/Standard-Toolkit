# Color-Scheme Abstraction for Krypton Toolkit

## Overview

Historically each palette in the **Krypton Toolkit** defined a large `Color[]`
(230+ entries) whose indexes had to match the `SchemeBaseColors` enumeration.
While compact, this approach was error-prone:

* missing values → runtime exceptions or incorrect colours
* out-of-order inserts → subtle visual glitches
* hard to review/merge because the meaning of an index is implicit

The new **colour-scheme abstraction** removes these drawbacks by
introducing two glue types that sit between a palette and the colour
enumerations:

| Type | Role |
|------|------|
| `AbstractBaseColorScheme` | Declares one property per entry of `SchemeBaseColors` enum.  Implementations expose meaningful names instead of numeric indexes. |

## `AbstractBaseColorScheme`

* Fully abstract – **no data storage**.
* >230 **get / set** properties, one per `SchemeBaseColors` value.
* Every colour is addressable by a meaningful name (e.g. `HeaderText`).
* Because the properties are writable, advanced users can tweak colours
  of an instantiated scheme at runtime while the toolkit reflects the
  changes immediately.

## Usability improvement – base-class overload

`PaletteMicrosoft365Base` now offers an additional constructor accepting
an `AbstractBaseColorScheme`:

```csharp
// modern, safer way
auto scheme = new AbstractBaseColorScheme(rawColors);
var palette = new MyPaletteMicrosoft365Theme(scheme, …);

// legacy, still supported
var palette = new MyPaletteMicrosoft365Theme(rawColors, …);
```

The legacy overload is kept for binary compatibility; internally it
reflects over the scheme properties to recreate the `Color[]` it needs.

No other code had to change – rendering, colour tables and controls
still consume a plain `Color[]` under the hood.

### Why this matters in day-to-day coding

The abstraction does **not** change how low-level rendering pipes receive
colour data, **but it radically improves how you interact with those colours in code**:

* **Named access instead of magic indexes** – write `scheme.HeaderText`
  rather than `colors[(int)SchemeBaseColors.HeaderText]`, eliminating
  off-by-one errors and making reviews self-explanatory.
* **Compiler enforcement** – if you extend `SchemeBaseColors` enum, the compiler
  instantly flags every colour-scheme implementation that lacks the new
  property; previously these gaps surfaced only at runtime.
* **Safer refactoring** – renaming or re-ordering enumeration entries no
  longer risks mismatching indexes across huge arrays, because each colour
  travels via its dedicated property.
* **Incremental adoption** – existing palettes can opt-in with only two
  lines of code (wrap their array and pass the scheme object), gaining the
  safety benefits without touching long colour arrays until convenient.
* **Cleaner IntelliSense** – IDEs now show meaningful property names,
  helping discoverability and reducing lookup time in reference docs.

## Migration plan

There is a single, easy path — every palette should be backed by its own
`*Scheme` class and the `_schemeBaseColors` array must be deleted.
The `generate_scheme_classes.py` helper handles the heavy lifting
automatically by generating a scheme class next to each palette file and
reporting problems.

```bash
python Scripts/generate_scheme_classes.py \
       --directory "Source/Krypton Components/Krypton.Toolkit/Palette Builtin" \
       --recursive
```

After running the script (see also section below):

1. Remove the `_schemeBaseColors` array from each palette (the generated
   class now contains the literal colours).
2. Add a static instance of the generated scheme and pass it to the
   base-class constructor:

   ```csharp
   private static readonly PaletteMicrosoft365BlackScheme _scheme = new();

   public PaletteMicrosoft365Black() :
       base(_scheme, _checkBoxList, _galleryButtonList, _radioButtonArray, _trackBarColours) { }
   ```

That’s it, no clumsy array, only strongly-typed colours everywhere.

## Compatibility considerations

* **Binary compatibility** is maintained because no public signature was
  removed.
* The toolkit still works on **.NET Framework 4.7.2**.
* Existing palettes that are not yet migrated keep using the legacy path
  without any behavioural change.

## Contributing guidelines for new palettes

1. Prefer creating an `AbstractBaseColorScheme` (or a dedicated
   `MyThemeColorScheme` derived from `AbstractBaseColorScheme`).
2. Use the new base-class overload – *never* pass raw `Color[]`
   directly in new code.
3. If you extend `SchemeBaseColors`, remember to update
   `AbstractBaseColorScheme` **and** review all existing implementations
   – the compiler will help.

## FAQ

**Q – Does the padding hide bugs?**  Padding with
`EMPTY_COLOR` yields transparent values which are visually obvious if
accessed inadvertently, but prevents hard crashes during partial
migration stages.

## Automatic generation helper – `generate_scheme_classes.py`

A Python utility located at `Scripts/generate_scheme_classes.py` can
speed-up the migration from raw colour arrays to strongly-typed
`*Scheme` classes.

### Key features

* **Index validation** – before generating any code the script compares
  the comment labels inside the `_schemeBaseColors` array with the
  `SchemeBaseColors` enumeration:

  * _missing_ labels → the corresponding property is initialised with
    `GlobalStaticValues.EMPTY_COLOR` so the generated class remains
    compile-safe.
  * _out-of-order_ or _extra_ labels are reported on the console but do
    not break generation.
* **Colour alignment** – when a palette skips an index (for example the
  *RibbonGroupButtonText* gap in most themes) the **following colours
  are kept in sync with their intended properties** – no accidental shift occurs.
* **Idempotent** – existing `Palette*Scheme.cs` files are never
  overwritten; rerunning the tool is safe.

### Usage

Run the script from the repository root (requires Python ≥ 3.10):

```bash
python Scripts/generate_scheme_classes.py [options]
```

Supported options – they mirror the command-line parser in the source
code:

| Flag | Purpose |
|------|---------|
| `--dry-run` | Preview generation without writing any file |
| `-o, --output <dir>` | Place all generated files into **dir** instead of alongside palette files |
| `-f, --file <path>` | Convert **one** specific palette `.cs` file |
| `-d, --directory <dir>` | Convert palettes in **dir** (use with `-r` for recursion) |
| `-r, --recursive` | With `--directory`, also search sub-directories |

If neither `--file` nor `--directory` is supplied the script prints its
help and exits.

Example – create scheme classes for all Microsoft 365 palettes in dry-run
mode:

```bash
python Scripts/generate_scheme_classes.py \
    --directory "Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Microsoft 365" \
    --recursive --dry-run
```

The tool will list discrepancies (missing, unlabelled, out-of-order or
extra entries) for each palette, followed by an “**Ok!**” confirmation
when no issues were found.

## Runtime Color Modification

`PaletteMicrosoft365Base` provides public methods to modify scheme colors at runtime.
These changes are reflected immediately in the UI after forcing a color table regeneration.

### Methods

* `SetSchemeColor(SchemeBaseColors colorIndex, Color newColor)`: Updates a single color and regenerates the color table.
* `GetSchemeColor(SchemeBaseColors colorIndex)`: Retrieves the current value of a specific color.
* `UpdateSchemeColors(Dictionary<SchemeBaseColors, Color> colorUpdates)`: Batch-updates multiple colors and regenerates the color table.
* `ApplyScheme(AbstractBaseColorScheme newScheme)`: Replaces the entire scheme with a new one and regenerates the color table.

Additionally, for direct access:

* Indexer: `palette[SchemeBaseColors colorIndex]` to get/set individual colors (automatically regenerates table on set).
* `SchemeColors`: Exposes the entire color array for advanced modifications (manual regeneration required by setting `Table = null`!).

### Examples

```csharp
// Create an instance of the black palette
var blackPalette = new PaletteMicrosoft365Black();

// Method 1: Change individual colors
blackPalette.SetSchemeColor(SchemeBaseColors.TextLabelControl, Color.FromArgb(120, 120, 120));
blackPalette.SetSchemeColor(SchemeBaseColors.PanelClient, Color.FromArgb(40, 40, 40));
blackPalette.SetSchemeColor(SchemeBaseColors.ButtonNormalBack1, Color.FromArgb(80, 120, 160));

// Method 2: Update multiple colors at once
var colorUpdates = new Dictionary<SchemeBaseColors, Color>
{
    { SchemeBaseColors.TextLabelControl, Color.White },
    { SchemeBaseColors.PanelClient, Color.FromArgb(30, 30, 30) },
    { SchemeBaseColors.ButtonNormalBack1, Color.DarkBlue },
    { SchemeBaseColors.ButtonNormalBack2, Color.LightBlue }
};
blackPalette.UpdateSchemeColors(colorUpdates);

// Method 3: Apply a completely new scheme
var customScheme = new PaletteMicrosoft365BlackScheme();
customScheme.TextLabelControl = Color.Yellow;
customScheme.PanelClient = Color.DarkRed;
blackPalette.ApplyScheme(customScheme);

// Method 4: Get current color values
Color currentTextColor = blackPalette.GetSchemeColor(SchemeBaseColors.TextLabelControl);

// Method 5: Direct indexer access
blackPalette[SchemeBaseColors.TextLabelControl] = Color.White;
blackPalette[SchemeBaseColors.PanelClient] = Color.DarkGray;

// Method 6: Direct array access
blackPalette.SchemeColors[(int)SchemeBaseColors.ButtonNormalBack1] = Color.Blue;
// Manually regenerate after direct array changes
// (Note: Indexer handles this automatically)
```

After modifications, call `Invalidate()` on controls using the palette to trigger a visual refresh if needed.

## How the Wiring Works

The color-scheme abstraction integrates with the palette system as follows:

1. **Scheme to Array Conversion**: When a palette is constructed with an `AbstractBaseColorScheme`, the private `ConvertSchemeToArray` method uses reflection to extract all property values into a `Color[]` array, matching the order of the `SchemeBaseColors` enum.

2. **Internal Storage**: The palette stores this array in `_ribbonColors` for fast access during rendering.

3. **Color Table Generation**: The `ColorTable` property lazily creates a `KryptonColorTable365` using `_ribbonColors`. Setting `Table = null` forces regeneration on next access.

4. **Runtime Updates**: Modification methods update `_ribbonColors` directly and set `Table = null` to ensure the next UI paint uses the new colors.

5. **Enumeration Safety**: Property names in schemes match enum values, ensuring type-safe access without magic numbers.

This design maintains performance (array lookups) while providing a safe, extensible API for color management.
