# Feature: Use source-generated LibraryImport for Win32 interop (#3874)

## Summary

Eligible Win32 P/Invoke declarations now use dual-path `#if NET8_0_OR_GREATER` `[LibraryImport]` / `#else` `[DllImport]` so modern TFMs get source-generated marshalling while .NET Framework remains compatible. Shared imports live in `Krypton.Interop` (`PI`); Toolkit/Utilities/TestForm call sites prefer `PI` facades instead of local imports.

## Related issues

- Closes #3874
- Builds on #3855 / #3860 (`Krypton.Interop` extraction)

## Type of change

- [ ] Bug fix (`Resolved`)
- [x] Feature / enhancement (`Implemented`)
- [ ] **Breaking change** (API removal/rename, behavior change requiring migration, assembly/namespace move)
- [ ] Documentation only

## Changes

- `Krypton.Interop`: `AllowUnsafeBlocks`; ~150 dual-path `LibraryImport` APIs; added `FindWindow`, `GetCursorPos`, `GetWindowTextLength`, `SetTimer`, `ExtractIconEx`, `SHGetStockIconInfo` / `SHSTOCKICONINFO`, command-link `SendMessageW` helpers, `SetProcessDPIAware`, `GetGuiResources`; optional CsWin32 package + empty `NativeMethods.txt`.
- `Krypton.Toolkit`: `ImageNativeMethods` / `StockIconHelper` / hooks / alternate command-link route through `PI`; PrintDlg remains local (class layouts); removed dead LibraryImport stubs.
- `Krypton.Toolkit.Utilities`: zero local `[DllImport]`; `PlatformEvents` and floating toolbars / code editor use `PI`.
- `TestForm`: `Program` uses `PI` for AppUserModelID / DPI; `AllowUnsafeBlocks` for PropertyGrid GDI dual-path helpers.
- `Scripts/Tools/Convert-DllImportToLibraryImport.ps1` and `Documents/Development/LibraryImport-Dual-Path-Interop.md`.

## Affected packages & target frameworks

- Packages: `Krypton.Interop`, `Krypton.Toolkit`, `Krypton.Toolkit.Utilities`, `TestForm`
- TFMs verified: `net472`, `net8.0-windows`

## Validation

- TestForm demo: existing stock-icon / shell / floating-toolbar / extended message-box / print dialog paths (no new dedicated demo; interop is internal).
- Manual steps:
  1. Launch TestForm (`dotnet run --project ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug`).
  2. Exercise DPI-aware startup, stock icons, floating toolbars, extended message box owner positioning, print dialog.
- Build commands listed under Checklist validation above completed for `net472` + `net8.0-windows`.

## Screenshots / GIFs

N/A (internal interop / no UI API change).

## Changelog

- Entry added to `Documents/Changelog/Changelog.md`:

```markdown
* Implemented [#3874](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3874), Use source-generated `LibraryImport` for eligible Win32 P/Invokes on modern TFMs (Framework TFMs keep `DllImport`)
```

## Breaking changes & migration

None. Public control APIs are unchanged; consumers do not need to update calling code.

## Developer documentation

- `Documents/Development/LibraryImport-Dual-Path-Interop.md`

## Checklist

- [x] Builds for `net472` and is C# 7.3 compatible where required
- [x] New compiler/analyzer warnings in touched code addressed
- [x] `Documents/Changelog/Changelog.md` updated
- [ ] TestForm demo added or updated (features / observable bug fixes) — not applicable (internal interop)
- [x] `Documents/Development/` guide added (substantial features)
- [ ] Screenshots/GIFs included (UI changes) — N/A
- [x] Breaking-change impact and TFM notes documented above
