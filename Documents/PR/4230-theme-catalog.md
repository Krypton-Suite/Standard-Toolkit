# Feature: Theme catalog and extra palettes in Krypton.Themes (#4230)

## Summary

Builtin palettes are split so `Krypton.Toolkit` ships 14 core themes, while extra palettes (Visual Studio, Material, macOS, Office 2013, gray/lime/accessibility, Sparkle variants, and Issue #1551 Materialize packs) live in `Krypton.Themes.dll`. `KryptonManager` auto-discovers that assembly. Extra `PaletteMode` values are unchanged; without Themes they paint as Microsoft 365 Blue.

## Related issues

- Closes #4230
- Relates to #1551

## Type of change

- [x] Feature / enhancement (`Implemented`)
- [x] **Breaking change** (extra `KryptonManager.Palette*` accessors now return `PaletteBase`; extra types moved to `Krypton.Themes`)

## Changes

- **Krypton.Toolkit** — Theme catalog (`KryptonThemeCatalog`, `KryptonThemeAvailability`, `IKryptonThemeProvider`), 14 core palettes, shared palette bases. Extra typed lazy fields removed from `KryptonManager`. Microsoft 365 Blue moved to Official Themes.
- **Krypton.Themes** — Extra palette implementations and `KryptonExtendedThemeProvider`, including Issue #1551 Materialize Blue / Light Blue / Silver Dark Alternate (family `Materialize`).
- **Selectors** — Theme combo/list/ribbon/browser rebuild by name; `ShowExtraThemes`; `PaletteModeConverter` lists selectable available modes.
- **TestForm** — `ThemeCatalogDemo` (#4230) and existing `Issue1551ThemeDemo`.

## Affected packages & target frameworks

- Packages: `Krypton.Toolkit`, `Krypton.Themes`, `Krypton.Standard.Toolkit`, `Krypton.Ribbon` (theme combo), `TestForm`.
- TFMs verified: `net472`.

## Validation

- TestForm demo: `ThemeCatalogDemo` and `Issue1551ThemeDemo` (registered in `StartScreen.AddButtons()`).
- Manual steps:
  1. Open **4230 Theme Catalog**. Extra themes (including Materialize) appear; family check boxes hide them from selectors; Show extra themes lists cores only when unchecked.
  2. Open **1551 Materialize Themes**. Switching variants paints Materialize chrome (not Microsoft 365 Blue fallback).
  3. Run `Scripts/UnitTests/UnitTest-ThemeCatalog.ps1` (`GetUnimplementedBuiltinModes` empty; Materialize registered).
- Build: `dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472`

## Changelog

- Entry added to `Documents/Changelog/Changelog.md` for #4230 and a #1551 Themes placement note.

## Breaking changes & migration

- Extra `KryptonManager.Palette*` properties return `PaletteBase` instead of concrete extra types.
- Apps that referenced only `Krypton.Toolkit` no longer embed extra palettes. Add `Krypton.Themes` (or `Krypton.Standard.Toolkit`) so extra `PaletteMode` values paint their real palettes. Without that assembly they fall back to Microsoft 365 Blue.
- `PaletteMode` values and display names are unchanged. `Custom` remains last.

## Developer documentation

- `Documents/Development/KryptonThemesCatalog.md`

## Checklist

- [x] Builds for `net472` and is C# 7.3 compatible where required
- [x] New compiler/analyzer warnings in touched code addressed
- [x] `Documents/Changelog/Changelog.md` updated
- [x] TestForm demo added or updated (features / observable bug fixes)
- [x] `Documents/Development/` guide added (substantial features)
- [x] Breaking-change impact and TFM notes documented above
