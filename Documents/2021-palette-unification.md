# Palette unification plan

---

## 1.  Define **one** authoritative colour contract

* Keep `KryptonColorSchemeBase` and extend it until it contains **every** colour currently exposed through
  * `SchemeBaseColors`
  * `SchemeContextMenuColors`
  * `SchemeExtraColors`
  * `SchemeMenuStripColors`
  * `SchemeToolTipColors`
  * `SchemeTrackingColors`
  * any extra properties that exist in individual colour-table classes, such as
    * `AppButtonNormal`, `AppButtonTrack`, `AppButtonPressed`
    * `ButtonBorderColors`, `ButtonBackColors`
    * `RibbonGroupCollapsedBackContext`, `RibbonGroupCollapsedBackContextTracking`
    * `RibbonGroupCollapsedBorderContext`, `RibbonGroupCollapsedBorderContextTracking`
    * These correspond to private static arrays (listed below) and must be folded into the scheme as named properties:
      _appButtonNormal  
      _appButtonPressed  
      _appButtonTrack  
      _arrowBorderColors  
      _buttonBackColors  
      _buttonBorderColors  
      _colorsB  
      _colorsG  
      _colorsS  
      _ribbonColors  
      _ribbonGroupCollapsedBackContext  
      _ribbonGroupCollapsedBackContextTracking  
      _ribbonGroupCollapsedBorderContext  
      _ribbonGroupCollapsedBorderContextTracking  
      _schemeBaseColors  
      _schemeOfficeColours  
      _schemeVisualStudioColors  
      _sparkleColors  
      _trackBarColors  
      _trackBarColours  
      _trackColors
* Use auto-properties - no arrays, no enums, no “slot” indices.

```csharp
public abstract class KryptonColorScheme
{
    public abstract Color ButtonPressedBorder  { get; }
    public abstract Color ButtonPressedBegin   { get; }
    // …repeat for every colour…
}
```

* The various enum files stay **temporarily** (read-only) so everything still compiles, but the end-goal is to delete them in the breaking-change release.

---

## 2.  Turn `KryptonColorTable` into the single renderer bridge

* Today each theme base class overrides `ColorTable` to call its own derived color-table, for example:

```csharp
public override KryptonColorTable ColorTable =>
    Table ??= new KryptonColorTable365(...);
```

* We want to collapse **all** of those per-theme `KryptonColorTableXXX` types into **one** generic bridge:

  1. Create a single `KryptonColorTable : ProfessionalColorTable` that takes any `KryptonColorSchemeBase` instance:

  ```csharp
  public sealed class KryptonColorTable : ProfessionalColorTable
  {
      private readonly KryptonColorSchemeBase _scheme;
      public KryptonColorTable(KryptonColorSchemeBase scheme)
          => _scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));

      // Forward every ProfessionalColorTable property to the scheme
      public override Color ButtonPressedBorder
          => _scheme.ButtonPressedBorder;
      // …add overrides for each property defined in ProfessionalColorTable…
  }
  ```

  2. In each theme’s `ColorTable` override (e.g., `PaletteMicrosoft365Base`, `PaletteOffice2007Base`, etc.), replace:

  ```csharp
  new KryptonColorTable365(...);
  ```

  with:

  ```csharp
  new KryptonColorTable(myUnifiedScheme);
  ```

  3. After migrating all themes, delete the obsolete `KryptonColorTableXXX` classes.

This results in:
    - A single `ProfessionalColorTable` implementation that delegates to a unified color scheme.
    - Elimination of dozens of nearly identical color-table subclasses.
    - A clear, maintainable path for adding new themes.

That 70-vs-239 gap is exactly what you expect in a split “scheme ↔ table” model, and it doesn’t derail the unification plan—it just underscores it.  

• The **scheme** (`KryptonColorSchemeBase` and its per-theme subclasses) is a superset of *all* palette API slots—today that’s ~239 properties covering back, border, content, ribbon, element, metric, image, etc.  

• The **table** (`ProfessionalColorTable` → `KryptonColorTable`) only needs to implement the ~70 rendering-specific properties that WinForms actually calls when drawing menus, tool-strips, status-strips, etc.

Under the one-bridge approach we:

1. Keep each theme’s **scheme** class as the single large list of 239 named colors.  

2. Implement **one** `KryptonColorTable : ProfessionalColorTable` bridge that forwards *only* those ~70 table properties into the scheme—exactly the way `public override Color ButtonPressedBorder => _scheme.ButtonPressedBorder;` works today.  

3. Wire every `Palette…Base` to use *that one* `KryptonColorTable` (passing in its scheme), then delete the dozens of per-theme `KryptonColorTableXXX` classes.

The extra ~169 scheme properties beyond the table’s 70 remain in each scheme as the palette API surface—they aren’t lost, they’re simply not consumed by WinForms rendering.  

The size difference is intended and doesn’t change the “one scheme per theme / one table bridge” architecture.

---

## 3.  Create one *scheme* class per theme

```csharp
public sealed class SparkleColorScheme : KryptonColorScheme
{
    // ctor may take nothing or a ThemeVariant flag (Light/Dark).
    public override Color ButtonPressedBorder => Color.Black;
    public override Color ButtonPressedBegin  => _sparkle[8];
    // …and so on…
}
```

If you prefer data-only classes, you can auto-generate these from the existing colour arrays (Roslyn source-generator or simple script).

---

## 4.  Wire-up and replace

1. Wherever code currently does

   ```csharp
   new KryptonColorTable365(colors, roundedEdges, palette);
   ```

   replace with

   ```csharp
   var scheme     = new Microsoft365ColorScheme(colors, roundedEdges);
   var colorTable = new KryptonColorTable(scheme);
   ```

2. Inside the new scheme classes, remove `colors[(int)SchemeBaseColors.X]` array lookups and replace with direct property references.

3. Delete **all** obsolete colour-table concrete classes once everything compiles.

---

## 5.  Clean-up pass (breaking version)

Once you have full coverage in KryptonColorSchemeBase + KryptonColorTable, you can delete all the SchemeBaseColors, SchemeExtraColors, etc. enums and the old palette classes as access will be via properties, not array indices.

* Delete `SchemeBaseColors`, `SchemeExtraColors`, … enums.  
* Delete any helper structs such as the `Color[]` holder constructors.  
* Optionally mark removed public APIs with `[Obsolete("Replaced by …")]` for one release cycle.

---

## 6. Documentation

This is a major change and affects a lot of classes and their interactions as well as interplay with other tools.
Documentation of breaking changes with a high detail level is extremely important, even if tedious (use AI to assist!).
Documenting the new KryptonColorSchemeBase contract and show migration paths is a critical step.

---

## 7.  Impact & follow-ups

1. **Binary breaking change** – Every consumer that instantiated a concrete `KryptonColorTableXYZ` now needs to create a `KryptonColorScheme` + `KryptonColorTable`.  
2. **Smaller DLL footprint** – only slim scheme classes remain.  
3. **No more index errors** – property access is self-documenting.  
4. **Negligible performance impact** – property getter inlines to the same IL as array lookup.  
5. **Future themes** – just add a new `FooColorScheme`; no renderer changes required.

---

### Quick reference checklist

* [ ] Fill `KryptonColorScheme` with **all** colours in `AbstractBaseColorScheme` (+ any missing ones).  
* [ ] Refactor `KryptonColorTable` to proxy every `ProfessionalColorTable` property to `_scheme`.  
* [ ] Write scheme classes: `Microsoft365ColorScheme`, `SparkleColorScheme`, …  
* [ ] Replace constructor calls across the toolkit.  
* [ ] Delete obsolete colour-table classes and enum slot maps.  
* [ ] Ship next major version and highlight the breaking change in release notes.

---
