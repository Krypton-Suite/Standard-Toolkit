# Strongly-typed Color Schemes in Krypton Toolkit

## Overview

For a long time each palette in the **Krypton Toolkit** stored its colours in two parallel `Color[]` arrays:

* `_schemeBaseColors` – ~230 general UI colours ordered like `SchemeBaseColors` (previously: _schemeOfficeColors)
* `_trackBarColors`   –  6 specialised colours for TrackBar controls

The arrays had to stay perfectly aligned with their respective enumerations – a single
missing or mis-ordered entry caused subtle glitches or run-time crashes.  Reviewing
such numeric arrays was next to impossible.

The new **strongly-typed color-scheme architecture** eliminates these problems by
replacing both arrays with self-describing classes and tiny helper extensions.

| Type | Role |
|------|------|
| `KryptonColorSchemeBase` | Abstract base class that declares one `Color` property per value of `SchemeBaseColors`.  Concrete *“..._BaseScheme”* classes store the actual colours. |
| `SchemeBaseColorsExtensions` | Converts a `KryptonColorSchemeBase` into the legacy `Color[]` layouts via `ToArray()` and `ToTrackBarArray()` (6 entries). |

Nothing in the low-level rendering pipeline had to change – all internal code still
works with simple arrays – but **every public API writen or reviewed becomes
readable and type-safe**.

## `KryptonColorSchemeBase`

* Purely abstract – **no arrays, no collections**.
* 230+ auto-properties, one per `SchemeBaseColors`.
* Every colour is accessible by a meaningful name (e.g. `HeaderText`).
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
  instead of `colors[(int)SchemeBaseColors.HeaderText]`.
* **Compiler enforcement** – adding a value to `SchemeBaseColors` will
  immediately highlight every incomplete scheme class.
* **Safer refactoring** – re-ordering an enum no longer risks silently shuffling
  array entries.
* **Clean IntelliSense** – IDEs list meaningful property names and documentation.

## Migration plan

Follow these three steps to migrate an existing palette:

1. Execute the C# helper located in `Source/Krypton Components/kptheme/` to
   generate one or more `Palette<Theme>_BaseScheme.cs` files in their own
   `Schemes` subfolder. See runtime help for all options.

2. Commit the newly created `*Scheme` class and verify that the tool removed
   both `_schemeBaseColors` **and** `_trackBarColors` fields from the file(s).

3. Ensure the palette constructor now looks like the snippet shown earlier.

That’s it – your palette is now fully strongly-typed.

### Generator highlights

* **Dual-array support** – aligns the six TrackBar colours automatically.
* **Index validation** – reports missing / out-of-order / superfluous entries.
* **Idempotent** – never overwrites an existing scheme file, unless option is set.
* **Optional --remove switch** – deletes the obsolete arrays and rewires the constructor.

## Compatibility considerations

* **Binary compatibility** is maintained – no public signatures were removed.
* Still runs on **.NET Framework 4.7.2**.
* Palettes not yet migrated continue to work unchanged.

## Contributing guidelines for new palettes

1. Always create a `KryptonColorSchemeBase` implementation.
2. Pass that scheme to the palette base constructor; **never** pass raw `Color[]`.
3. When extending `SchemeBaseColors` also update `KryptonColorSchemeBase` and
   revisit all existing schemes – the compiler will guide you.

## Runtime colour modification

`PaletteMicrosoft365Base` exposes helper methods to alter colours on the fly:

* `SetSchemeColor(SchemeBaseColors index, Color value)`
* `GetSchemeColor(SchemeBaseColors index)`
* `UpdateSchemeColors(Dictionary<SchemeBaseColors, Color> changes)`
* `ApplyScheme(KryptonColorSchemeBase newScheme)`

**Usage example:**

```csharp
// 1. Create palette (installs its default scheme)
var black = new PaletteMicrosoft365Black();

// 2. Tweak a few colours
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

Changes are reflected immediately; call `Invalidate()` on open windows if the
toolkit is unable to refresh automatically.

## How the wiring works

1. **Scheme → Arrays** – extension method `ToArray()` converts every property to
   a 1-dimensional `Color[]`.  `ToTrackBarArray()` extracts the six specialised
   TrackBar colours.
2. **Internal storage** – the palette keeps both arrays in private fields for
   ultra-fast indexed access during rendering.
3. **Color table generation** – the `ColorTable` property lazily constructs
   `KryptonColorTable365` from the current array values.  Setting `Table = null`
   forces regeneration.
4. **Runtime updates** – the modification methods above mutate the arrays
   directly and invalidate the cached table.
5. **Enumeration safety** – property names in scheme classes exactly match the
   enumeration values, guaranteeing type-safe round-trips.

The result combines the **performance of arrays** with the **safety and
discoverability of expressive property names**.
