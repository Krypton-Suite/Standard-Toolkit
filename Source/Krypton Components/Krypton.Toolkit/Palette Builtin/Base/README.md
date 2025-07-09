# Color-Scheme Abstraction for Krypton Toolkit

## Overview

Historically each palette in the **Krypton Toolkit** defined a large
`Color[]` (often 230+ entries) whose indexes had to match the
`SchemeBaseColors` enumeration.  While compact, this approach was
error-prone:

* missing values → runtime exceptions or incorrect colours
* out-of-order inserts → subtle visual glitches
* hard to review/merge because the meaning of an index is implicit

The new **colour-scheme abstraction** removes these drawbacks by
introducing two glue types that sit between a palette and the colour
enumerations:

| Type | Role |
|------|------|
| `AbstractBaseColorScheme` | Declares one property per entry of `SchemeBaseColors`.  Implementations expose meaningful names instead of numeric indexes. |
| `ArrayBaseColorScheme` | Tiny adapter that turns an existing `Color[]` into an `AbstractBaseColorScheme` so legacy data structures can be reused unchanged. |

Both classes live in this **`Base`** folder.

## `AbstractBaseColorScheme`

* Fully abstract – **no data storage**.
* >230 read-only properties, one per `SchemeBaseColors` value.
* Any palette or helper can rely on *named* access, eliminating index
  math and making the compiler guard against omissions.

## `ArrayBaseColorScheme`

* Wraps an existing `Color[]`.
* Constructor automatically **pads** arrays that are too short with
  `GlobalStaticValues.EMPTY_COLOR` – the toolkit continues to compile
  even while palettes migrate incrementally.
* Exposes a `public Color[] Colors` property so older APIs (e.g. the
  original `PaletteMicrosoft365Base(Color[] …)` constructor) continue
  to work.

## Usability improvement – base-class overload

`PaletteMicrosoft365Base` now offers an additional constructor accepting
an `ArrayBaseColorScheme`.  Downstream code can therefore choose the
most convenient representation:

```csharp
// modern, safer way
auto scheme = new ArrayBaseColorScheme(rawColors);
var palette = new MyPaletteMicrosoft365Theme(scheme, …);

// legacy, still supported
var palette = new MyPaletteMicrosoft365Theme(rawColors, …);
```

## Demonstration – `PaletteMicrosoft365Black`

The *Microsoft 365 Black* theme serves as the first consumer of the abstraction:

1. The existing `_schemeBaseColors` array remains untouched (single
   source of truth).
2. A static wrapper is created once:

   ```csharp
   private static readonly ArrayBaseColorScheme _scheme =
       new ArrayBaseColorScheme(_schemeBaseColors);
   ```

3. The palette’s constructor now calls the new base-class overload:

   ```csharp
   public PaletteMicrosoft365Black() :
       base(_scheme, _checkBoxList, _galleryButtonList, _radioButtonArray, _trackBarColors)
   { }
   ```

No other code had to change – rendering, colour tables and controls
continue to consume a plain `Color[]` transparently.

### Why this matters in day-to-day coding

The abstraction does **not** change how low-level rendering pipes receive colour data, **but it radically improves how you interact with those colours in code**:

* **Named access instead of magic indexes** – you now write
  `scheme.HeaderText` rather than `colors[(int)SchemeBaseColors.HeaderText]`,
  eliminating off-by-one errors and making reviews self-explanatory.
* **Compiler enforcement** – if you extend `SchemeBaseColors`, the compiler
  instantly flags every colour-scheme implementation that lacks the new
  property; previously these gaps surfaced only at runtime.
* **Safer refactoring** – renaming or re-ordering enumeration entries no
  longer risks mismatching indexes across huge arrays, because each colour
  travels via its dedicated property.
* **Incremental adoption** – existing palettes can opt-in with only
  two lines of code (create `ArrayBaseColorScheme` and pass it), reaping the
  safety benefits without touching their long colour arrays until you have
  time to convert them.
* **Cleaner IntelliSense** – IDEs now show meaningful property names,
  helping discoverability and reducing lookup time in reference docs.

## Migration plan for remaining palettes

1. **Quick win (recommended)** – wrap each existing colour array in
   `ArrayBaseColorScheme` and pass the *scheme object* to the new
   constructor.  This is a 3-line change per theme and gives immediate
   compile-time safety when accessing colours elsewhere.
2. **Long-term** – optionally create dedicated subclasses of
   `AbstractBaseColorScheme` where each property returns a literal
   colour.  That removes the array completely and lets the compiler
   flag any missing colour at *definition* time, but requires a verbose
   (yet mechanical) class per palette.
3. **Deprecate** – once every palette has migrated, the old
   `PaletteMicrosoft365Base(Color[] …)` constructor can be marked
   `[Obsolete]` and later removed.

## Compatibility considerations

* **Binary compatibility** is maintained because no public signature was
  removed.
* The toolkit still works on **.NET Framework 4.7.2**.
* Existing palettes that are not yet migrated keep using the legacy path
  without any behavioural change.

## Contributing guidelines for new palettes

1. Prefer creating an `ArrayBaseColorScheme` (or a dedicated
   `MyThemeColorScheme` derived from `AbstractBaseColorScheme`).
2. Use the new base-class overload – *never* pass raw `Color[]`
   directly in new code.
3. If you extend `SchemeBaseColors`, remember to update
   `AbstractBaseColorScheme` **and** review all existing implementations
   – the compiler will help.

## FAQ

**Q – Why not delete the colour array right now?**  Because that would
force a large rewrite of every existing palette.  The adapter lets us
migrate incrementally while keeping git diffs small and easy to review.

**Q – Does the padding hide bugs?**  Padding with
`EMPTY_COLOR` yields transparent values which are visually obvious if
accessed inadvertently, but prevents hard crashes during partial
migration stages.
