# Krypton theme catalog

## Overview

V110 splits builtin palettes so `Krypton.Toolkit` stays a smaller core, while extra palettes ship in `Krypton.Themes`. `KryptonManager` auto-discovers `Krypton.Themes.dll` and registers those palettes into `KryptonThemeCatalog`. Developers can hide themes from selectors with `KryptonThemeAvailability` without removing `PaletteMode` values.

### Packages

- `Krypton.Toolkit` — 14 core palettes: Professional System / Office 2003; Office 2007 / 2010 / Microsoft 365 **Blue, Silver, Black**; **Sparkle Blue, Orange, Purple**.
- `Krypton.Themes` — Visual Studio, Material, macOS, Office 2013, Sparkle dark/light variants, dark/light/white/gray/lime/accessibility, Issue #1551 Materialize packs, and related helpers.
- `Krypton.Standard.Toolkit` — bundles Toolkit, Themes, and the other suite assemblies.

### Toolkit-only vs Standard vs Lite

Lite pack channels are a **TFM subset**, not a smaller theme set. `PackLite` and `PackAll` both produce `Krypton.Themes`. Extra palettes appear when the app references that package (or Standard Toolkit) so `Krypton.Themes.dll` is copied next to the executable. A Toolkit-only app keeps the 14 cores. Extra `PaletteMode` values still exist on the enum; without Themes they paint as Microsoft 365 Blue.

## Architecture

- `IKryptonThemeProvider` / `KryptonThemeDescriptor` describe a `PaletteMode`, family key, core vs extra, concrete type, and factory.
- `KryptonCoreThemeProvider` (internal, Toolkit) registers the 14 core palettes.
- `KryptonExtendedThemeProvider` (Themes) is advertised with `[assembly: KryptonThemeProvider(typeof(KryptonExtendedThemeProvider))]`.
- Toolkit `Palette Builtin` keeps cores, shared bases (`PaletteOffice2007Base`, `PaletteMaterialBase`, `PaletteMacOSBase`, `PaletteRetroBase`, `PaletteVisualStudioBase`, `PaletteSparkleBase`, Microsoft 365 base), Theme Catalog types, and `MacOSPaletteSharedAssets`. Extra implementations (including Issue #1551 Materialize packs) live under `Krypton.Themes/Palette Builtin`. Microsoft 365 Blue is a core Official theme.
- `KryptonThemeCatalog.DiscoverThemes()` scans loaded assemblies, tries `Assembly.Load("Krypton.Themes")` once, then probes `Krypton.Themes.dll` beside `AppContext.BaseDirectory`, the Toolkit assembly, and the entry assembly. `LoadFrom` requires the same public key token as Toolkit.
- If an extra `PaletteMode` is requested and Themes is not loaded, `GetPalette` / `GetPaletteForMode` **fall back to Microsoft 365 Blue** (design time and runtime). The requested mode is not rewritten; subscribe to `KryptonThemeCatalog.MissingThemeFallback` to log it. Fallback instances are not cached under the extra mode, so loading Themes later in the same process still yields the real palette.
- Theme combos, lists, ribbon combo, and `KryptonThemeBrowser` use `ThemeManager.GetThemesArray()`. `ShowExtraThemes` / `KryptonThemeBrowserData.ShowExtraThemes` list cores only when false.
- `GetDescriptors()` / `GetFamilies()` / `GetDisplayName()` support custom pickers.

Renderers remain in Toolkit. Extra palettes reuse those renderers.

## Adding a builtin palette

Default: implement the palette as an **extra** in `Krypton.Themes`. Only put a palette in Toolkit when the user explicitly wants a **core** theme.

### 1. Choose placement and family

- **Extra** — file under `Source/Krypton Components/Krypton.Themes/…` (mirror Toolkit `Palette Builtin` folder layout where practical). Register in `KryptonExtendedThemeProvider`.
- **Core** — file under `Source/Krypton Components/Krypton.Toolkit/Palette Builtin/…`. Register in `KryptonCoreThemeProvider`, and update `KryptonThemeCatalog.IsKnownExtraMode` so the new core is not treated as missing-Themes.
- Pick a `KryptonThemeFamilies` key (or add a new constant). Extras that share a family with cores (e.g. Sparkle) should be hideable with `SetFamilyEnabled(family, false, extraOnly: true)`.

Concrete types stay in namespace `Krypton.Toolkit` in both assemblies.

### 2. Keep `PaletteMode` and display strings ordered together

In `PaletteMode.cs` and `PaletteModeStrings.cs`:

1. Insert the new enum member **in the same relative order** as the `SupportedThemes` dictionary (see comment on `PaletteMode`: ticket #1328).
2. Keep **`Custom` last**.
3. Add `DEFAULT_PALETTE_…` constant, public string property, dictionary entry, and `Reset` / equality bookkeeping.

Selectors, converters, and designers all consume `SupportedThemes`; a mismatch shows wrong names or breaks designer round-trips.

### 3. Implement the palette class

- Prefer subclassing an existing base (`PaletteOffice2007Base`, `PaletteSparkleBase`, `PaletteMaterialBase`, …) and reusing Toolkit renderers.
- Follow neighbouring Official / Extra theme files for colours, images, and schema resources.
- New files: current Standard Toolkit BSD header only; UTF-8 **with** BOM; CRLF.

### 4. Register the catalog factory

**Extra** (`KryptonExtendedThemeProvider`):

```csharp
Extra(PaletteMode.MyNewTheme, typeof(PaletteMyNewTheme), () => new PaletteMyNewTheme()),
```

Extend `FamilyFor(PaletteMode)` when the name does not already map to the right `KryptonThemeFamilies` value.

**Core** (`KryptonCoreThemeProvider`):

```csharp
Core(PaletteMode.MyNewTheme, KryptonThemeFamilies.MyFamily, typeof(PaletteMyNewTheme),
    () => KryptonManager.PaletteMyNewTheme),
```

Also add a typed lazy accessor on `KryptonManager` and exclude the mode from `IsKnownExtraMode`.

### 5. `KryptonManager` accessor

- Extra: `public static PaletteBase PaletteMyNewTheme => GetPaletteForMode(PaletteMode.MyNewTheme);`
- Core: typed property with a private static lazy field (same pattern as `PaletteSparkleBlue`).

### 6. Converters and designer

- `PaletteModeConverter` — no change if `SupportedThemes` is updated.
- `PaletteClassTypeConverter` — add **core** type mappings only; extras use `KryptonThemeCatalog.TryGetMode`.

### 7. Validate

1. Build Themes + TestForm (`net472` is enough for a smoke check).
2. Run TestForm **4230 Theme Catalog** (or any theme combo): the display name appears; applying the mode paints correctly.
3. With Themes loaded: `GetUnimplementedBuiltinModes()` must not contain the new mode.
4. Optionally extend `Scripts/UnitTests/UnitTest-ThemeCatalog.ps1`.
5. Changelog + PR description; update this guide if placement rules change.

### Common mistakes

| Mistake | Result |
|---------|--------|
| Toolkit references Themes | Project cycle; build break |
| Enum order ≠ `SupportedThemes` | Wrong labels / designer bugs |
| New value after `Custom` | Serialization / converter issues |
| Extra registered only in Toolkit | Themes never discover it; paints Microsoft 365 Blue until Themes loads |
| Core omitted from `IsKnownExtraMode` allow-list inversion | Treated as missing Themes assembly |
| New Sparkle family disable without `extraOnly` | Hides core Sparkle Blue/Orange/Purple |

## Public API

| Type | Role |
|------|------|
| `KryptonThemeCatalog` | Register, discover, `IsImplementationAvailable`, `IsCoreMode`, `GetPalette`, `GetDescriptors`, `GetFamilies`, `GetDisplayName`, `GetUnimplementedBuiltinModes`, `MissingThemeFallback` |
| `KryptonThemeAvailability` | `SetEnabled`, `SetFamilyEnabled` (`extraOnly`), `AllowCustomThemes`, `IsSelectable`, `Export` / `Import`, `Reset` |
| `KryptonThemeFamilies` | Family keys |
| `KryptonManager.AutoDiscoverThemes` | Opt out of loading extra palettes |
| `KryptonThemeComboBox` / `KryptonThemeListBox` / `KryptonRibbonGroupThemeComboBox` | Rebuild by theme name, `ShowExtraThemes` |
| `KryptonThemeBrowserData.ShowExtraThemes` | Theme browser list filter |

Official Sparkle / Office / 365 core `KryptonManager.Palette*` accessors stay **typed**. Extra accessors return `PaletteBase`.

`PaletteModeConverter.GetStandardValues` lists selectable, available modes plus `Global`.

## Usage

```csharp
KryptonThemeAvailability.SetFamilyEnabled(KryptonThemeFamilies.VisualStudio, false);
KryptonThemeAvailability.SetFamilyEnabled(KryptonThemeFamilies.Sparkle, false, extraOnly: true);
myCombo.ShowExtraThemes = false;
File.WriteAllText(settingsPath, KryptonThemeAvailability.Export());
KryptonThemeAvailability.Import(File.ReadAllText(settingsPath));
myManager.GlobalPaletteMode = PaletteMode.VisualStudio2022Dark;
```

Set `KryptonManager.AutoDiscoverThemes = false` before the first palette lookup to keep only core themes even if the DLL is present.

```csharp
KryptonThemeCatalog.MissingThemeFallback += (_, e) =>
    Debug.WriteLine($"Missing {e.RequestedMode}; using {e.FallbackMode}");
```

### Custom picker

```csharp
foreach (var descriptor in KryptonThemeCatalog.GetDescriptors())
{
    if (KryptonThemeAvailability.IsSelectable(descriptor.Mode))
    {
        list.Add(descriptor.DisplayName); // family: descriptor.Family
    }
}
```

### Third-party provider DLL

See `Source/TestHarnesses/ThemeProviderSample`. Mark the assembly with `KryptonThemeProviderAttribute` and return descriptors for modes you implement. Existing modes are not replaced. New `PaletteMode` values cannot be added without a Toolkit change.

## Edge cases

- Designer `.cs` files keep enum names such as `PaletteMode.VisualStudio2022Dark`. Without Themes, chrome is Microsoft 365 Blue until the DLL is present.
- Direct `new PaletteVisualStudio2022Dark()` requires a Themes reference; `new PaletteSparkleBlue()` stays in Toolkit.
- Type forwarding is not used (Toolkit cannot reference Themes).
- `MacOSCustomPaletteHelper` ships in Themes.
- Hiding Sparkle without `extraOnly: true` also hides core Sparkle Blue/Orange/Purple.
- Only signed `Krypton.Themes.dll` matching Toolkit’s public key token is `LoadFrom`’d. Other provider assemblies must already be loaded (project reference or your own `LoadFrom`).
