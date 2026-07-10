# Fix: Implement `IsDefault` on Values types (#3857)

## Summary

Several `Storage`-derived Values types threw `NotImplementedException` from `IsDefault`, which broke PropertyGrid labels (`ToString()`), designer "Reset to default", and serialization checks. This change implements field-based default comparison for the file-system Values types and returns `true` for empty palette/input-box stubs.

## Related issues

- Closes #3857

## Type of change

- [x] Bug fix (`Resolved`)
- [x] Feature / enhancement (`Implemented`)

## Changes

- `FileSystemTreeViewValues`, `SystemTreeViewValues`, and `FileSystemListViewValues` in `Krypton.Toolkit` and `Krypton.Toolkit.Utilities`: `IsDefault` compares backing fields to their `[DefaultValue]` contract.
- `Krypton.Toolkit` `FileSystemListViewValues`: `_currentPath` field initializer aligned with `[DefaultValue("")]` (was `MyDocuments`).
- `KryptonInputBoxValues` and `KryptonPalettePropertyGrid`: `IsDefault` returns `true` (no serializable properties).

## Affected packages & target frameworks

- Packages: `Krypton.Toolkit`, `Krypton.Toolkit.Utilities`
- TFMs verified: `net472`, `net8.0-windows` (via solution Debug build)

## Validation

- TestForm demo: existing `TreeViewTestForm` (file-system tree/list controls in PropertyGrid).
- Manual steps:
  1. Run TestForm, open TreeView test, select a file-system control in the PropertyGrid.
  2. Confirm expandable Values nodes display without exception; fresh controls show default state; changing a property shows modified state.
- Build: `dotnet build ".\Source\Krypton Components\Krypton Toolkit Suite 2022 - VS2022.sln" -c Debug`

## Changelog

- Entry added to `Documents/Changelog/Changelog.md`:

```markdown
* Implemented [#3857](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3857), `IsDefault` on file-system Values types and related `Storage` stubs so PropertyGrid display, designer reset, and `ToString()` no longer throw
```

## Breaking changes & migration

None. `FileSystemListViewValues.CurrentPath` now initializes to `string.Empty` instead of `MyDocuments`, matching the existing `[DefaultValue("")]` and the Utilities assembly; the list view control already resolves an empty path at load time.

## Checklist

- [x] Builds for `net472` and is C# 7.3 compatible where required
- [x] New compiler/analyzer warnings in touched code addressed
- [x] `Documents/Changelog/Changelog.md` updated
- [ ] TestForm demo added or updated (features / observable bug fixes)
- [ ] `Documents/Development/` guide added (substantial features)
