# PR: KryptonCommand `CommandType` for ButtonSpec styles (#1133)

## Summary

Implements [issue #1133](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1133) by centralizing built-in `ButtonSpec` behaviour on `KryptonCommand.CommandType` instead of maintaining separate command classes with duplicated per-palette image logic.

- Setting `CommandType` to a toolbar or help value applies palette-aware images (normal, disabled, pressed, tracking), default text, and transparent colour from the current global palette.
- `AssignedButtonSpec` syncs the linked `ButtonSpecAny` (`Type`, `KryptonCommand`, and `ImageStates`).
- `ButtonSpec.GetImage` resolves per-state images from the palette when the linked command has a non-`General` `CommandType` (previously only `ImageSmall` was returned for all states).
- `KryptonCommand` listens for `GlobalPaletteChanged` and refreshes typed command configuration when the theme changes.
- `KryptonIntegratedToolbar*Command` and `KryptonHelpCommand` are reduced to obsolete thin wrappers over `KryptonButtonSpecTypedCommand` for backward compatibility.
- `IntegratedToolBarCommandValues` now exposes `KryptonCommand?` properties; `IntegratedToolBarCommandWiring` and `RefreshToolBarCommands()` connect commands to the integrated toolbar button array.
- Fixed a static initialization cycle in `KryptonIntegratedToolBarManager` (command values must initialize before button values; `ApplyTo` is not called from `SetupToolBar()` during static ctor).

**Net effect:** ~10,500 lines removed, ~400 lines added (mostly new helpers and slim wrappers).

## Motivation

Previously, each integrated toolbar action and form help required a dedicated `KryptonCommand` subclass (`KryptonIntegratedToolbarPasteCommand`, `KryptonHelpCommand`, etc.) with hundreds of lines of duplicated `switch (PaletteMode)` image loading. `KryptonCommand` already had `KryptonCommandType` and `AssignedButtonSpec`, but they were only partially wired. This PR completes that design using existing `PaletteButtonSpecStyle` and `palette.GetButtonSpecImage()`.

## Key changes

| Area | Change |
|------|--------|
| `KryptonCommandButtonSpecMappings` | Maps `KryptonCommandType` → `PaletteButtonSpecStyle` and default text |
| `KryptonCommand` | Palette-driven `ApplyCommandTypeConfiguration()`, `GetButtonSpecImage()`, palette change handler |
| `ButtonSpec` | Per-state images from typed commands; `CommandType` property change propagation |
| `KryptonButtonSpecTypedCommand` | Base class for obsolete wrappers |
| `IntegratedToolBarCommandValues` | `KryptonCommand?` properties + `ApplyTo()` |
| `IntegratedToolBarCommandWiring` | Wires command values to 14 toolbar buttons |
| `KryptonIntegratedToolBarManager` | `ToolBarCommands` re-enabled; `RefreshToolBarCommands()`; static init order fix |
| TestForm | `KryptonCommandButtonSpecDemo` + Start Screen entry; Main print command migrated to `KryptonCommand` |

## Usage (preferred API)

```csharp
var pasteCmd = new KryptonCommand
{
    CommandType = KryptonCommandType.IntegratedToolBarPasteCommand
};
pasteCmd.Execute += (_, _) => { /* paste */ };

var pasteSpec = new ButtonSpecAny();
pasteSpec.KryptonCommand = pasteCmd;  // sets AssignedButtonSpec on the command
```

Integrated toolbar example:

```csharp
KryptonIntegratedToolBarManager.IntegratedToolBarCommandValues.PasteButtonCommand = pasteCmd;
toolBarManager.RefreshToolBarCommands();
toolBarManager.AttachIntegratedToolBarToParent(form);
```

## Breaking / migration notes

- **Binary compatible:** Obsolete types (`KryptonIntegratedToolbarPasteCommand`, etc.) remain and delegate to `CommandType`.
- **`IntegratedToolBarCommandValues`:** Property types changed from `KryptonIntegratedToolbar*Command` to `KryptonCommand?`. Existing subclasses still assign because they inherit `KryptonCommand`.
- **`ToolBar*Button` getters:** No longer return `new ButtonSpecAny()` when unset; they return `null` (or the assigned spec).
- **Designer:** Prefer `KryptonCommand` with `CommandType` instead of obsolete toolbar command components.