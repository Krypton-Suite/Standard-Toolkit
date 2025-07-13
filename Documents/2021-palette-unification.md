# Palette unification plan

---

## 1.  Define **one** authoritative colour contract

* Keep `AbstractBaseColorScheme` (or rename it to `KryptonColorScheme`) and extend it until it contains **every** colour currently exposed through
  * `SchemeBaseColors`
  * `SchemeContextMenuColors`
  * `SchemeExtraColors`
  * `SchemeMenuStripColors`
  * `SchemeToolTipColors`
  * `SchemeTrackingColors`
  * any extra properties that exist in individual colour-table classes, such as
    * `PaletteOffice2007Base.cs` with additional arrays:  
      * `AppButtonNormal`, `AppButtonTrack`, `AppButtonPressed`
      * `ButtonBorderColors`, `ButtonBackColors`
      * `RibbonGroupCollapsedBackContext`, `RibbonGroupCollapsedBackContextTracking`
      * `RibbonGroupCollapsedBorderContext`, `RibbonGroupCollapsedBorderContextTracking`
    * `PaletteSparkleBase.cs` with additional array:
      * `TrackBarColors`
    * other private static color arrays across multiple files:
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

```csharp
public sealed class KryptonColorTable : ProfessionalColorTable
{
    private readonly KryptonColorScheme _scheme;

    public KryptonColorTable(KryptonColorScheme scheme) =>
        _scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));

    // Example override
    public override Color ButtonPressedBorder =>
        _scheme.ButtonPressedBorder;

    // Repeat for every ProfessionalColorTable property that any palette ever used.
}
```

Because this type now fulfils **all** colour requests, you can delete the family of `*365*`, `*2013*`, `*2010*`, `Sparkle*`, `VisualStudio*` colour-table classes once consumers have been migrated.

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

* Delete `SchemeBaseColors`, `SchemeExtraColors`, … enums.  
* Delete any helper structs such as the `Color[]` holder constructors.  
* Optionally mark removed public APIs with `[Obsolete("Replaced by …")]` for one release cycle.

---

## 6.  Impact & follow-ups

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
