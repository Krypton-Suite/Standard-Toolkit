# Feature: Form fade values and Extended MessageBox fade / timeout (#4188)

## Summary

`VisualForm` / `KryptonForm` gain opt-in, designer-serializable fade in/out via `FadeValues` (off by default). `KryptonMessageBoxExtended` can fade, show a caption countdown, and auto-close with a configured result or button click. The unfinished extended timeout path is completed (including RTL). Message-box `UseFade` is independent of `FadeValues`; do not enable both on the same instance.

## Related issues

- Closes #4188

## Type of change

- [x] Feature / enhancement (`Implemented`)

## Changes

### `Krypton.Toolkit` (`VisualForm` / `KryptonForm`)

- New `FadeValues` storage (`FadingEnabled`, `FadeIn`, `FadeOut`, `FadeSpeed`, `CustomFadeSpeed`).
- `VisualForm` wires auto fade-in on `Shown` and cancel-until-complete fade-out on `FormClosing` (same place as `ShadowValues` / `BlurValues`).
- Public `FadeIn()` / `FadeOut()` / `FadeOutAndClose()` plus `FadeInCompleted` / `FadeOutCompleted`.
- `KryptonForm` skips the borderless first-show opacity snap when a native fade-in is active.
- `KryptonFormFadeController.FadeOut` no longer closes the form (`FadeOutAndClose` still does).
- TestForm `FadeFormTest` uses the native APIs instead of `Thread.Sleep`.

### `Krypton.Toolkit.Utilities` (`KryptonMessageBoxExtended`)

- `KryptonMessageBoxExtendedData`: `UseFade`, `FadeSpeed`, `CustomFadeSpeed`, `UseTimeOut`, `TimeOut`, `TimeOutInterval`, `AutoClose`, `TimeOutResult`, `TimeOutAction`.
- Shared `MessageBoxExtendedLifetimeController` for LTR and RTL forms (WinForms timers; no Toolkit internals).
- Timeout now applies `timerResult` / `TimeOutAction` instead of closing with `DialogResult.None`.
- RTL timeout no longer recursively calls `Show`.
- Data constructor and `Show(data)` / `ShowAsync(data)` honour timeout and fade.
- FoldableDialog-style details expander: `DetailsText`, `Expanded`, `ExpandButtonText`, `CollapseButtonText`, optional `FooterContentType` / `FooterRichTextBoxHeight` (default details height 180). Existing `footerText` Show() parameters and `MoreDetails*` properties remain.
- TestForm demos `MessageBoxExtendedLifetimeDemo` and `MessageBoxExtendedFoldableDemo`.

## Affected packages & target frameworks

- Packages: `Krypton.Toolkit`, `Krypton.Toolkit.Utilities`, `TestForm`
- TFMs verified: `net472` (targeted Debug build of Toolkit, Utilities, and TestForm)

## Validation

- TestForm demo: `FadeFormTest` (registered as "KryptonForm FadeValues").
  1. Open the demo — the form fades in.
  2. Fade Out then Fade In — opacity animates without closing.
  3. Change `FadeValues.FadeSpeed` in the property grid and fade again.
  4. Open faded child — second `KryptonForm` fades in; close it to fade out.
  5. Close the demo — fade-out then close.
  6. `FadingEnabled = false` — no automatic fade on the next open.
- TestForm demo: `MessageBoxExtendedLifetimeDemo` (registered as "4188 MessageBox Extended Fade / Timeout").
  1. Preset: fade only — dialog fades in and out; no caption countdown.
  2. Preset: countdown only — caption counts down; dialog stays until dismissed.
  3. Preset: auto-close OK — closes with `DialogResult.OK` at zero.
  4. Preset: click button 2 — Yes/No/Cancel auto-clicks No.
  5. Preset: fade + timeout — fade in, countdown, fade out, configured result.
  6. Preset: RTL timeout — `VisualRTLMessageBoxExtendedForm` counts down and closes once (no second dialog).
  7. `Show()` timeout overload — existing parameters still auto-close with `timerResult`.
  8. `ShowAsync(data)` — same lifetime behaviour on the async path.
- TestForm demo: `MessageBoxExtendedFoldableDemo` (registered as "Message Box Extended - Foldable Footer").
  1. Show with footer text / RichTextBox — expander uses ▼ Show Details / ▲ Hide Details.
  2. JIT preset — `DetailsText` + `Expanded` + FoldableDialog expand/collapse captions.
  3. Start collapsed then expand — dialog grows by the details region (default 180px for RichTextBox).
- Build: `dotnet build ".\Source\Krypton Components\Krypton.Toolkit\Krypton.Toolkit 2022.csproj" -c Debug -f net472`, `dotnet build ".\Source\Krypton Components\Krypton.Toolkit.Utilities\Krypton.Toolkit.Utilities.csproj" -c Debug -f net472`, and `dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472`

## Screenshots / GIFs

Exercise in TestForm **KryptonForm FadeValues**, **4188 MessageBox Extended Fade / Timeout**, and **Message Box Extended - Foldable Footer**.

## Changelog

- Entries added to `Documents/Changelog/Changelog.md`:

```markdown
* Implemented, Configurable fade in/out on `VisualForm` / `KryptonForm` via `FadeValues` (opt-in; default off).
   * Set `FadeValues.FadingEnabled` to fade in on show and out on close. `FadeIn` / `FadeOut` / `FadeSpeed` / `CustomFadeSpeed` are designer-serializable. Call `FadeIn()`, `FadeOut()`, or `FadeOutAndClose()` at any time.
* Implemented [#4188](https://github.com/Krypton-Suite/Standard-Toolkit/issues/4188), Extended Messagebox enhancements
   * Fade in/out, caption timeout, and auto-close for `KryptonMessageBoxExtended`.
   * FoldableDialog-style collapsible details on `KryptonMessageBoxExtended` (`DetailsText`, `Expanded`, `ExpandButtonText`, `CollapseButtonText`).
   * A non-empty `DetailsText` shows the expander (same rule as `KryptonFoldableDialog`). Existing `Show(..., footerText, footerExpanded, footerContentType)` overloads and `MoreDetails*` data properties still work.
   * Configure via `KryptonMessageBoxExtendedData` (`UseFade`, `UseTimeOut`, `AutoClose`, `TimeOutAction`, `TimeOutResult`). Existing `Show(..., useTimeOut, timeOut, timerResult)` overloads now close with the configured result. RTL timeout no longer opens a second dialog.
   * To use, you will need to download the `Krypton.Standard.Toolkit` NuGet package, as this control is part of the `Krypton.Toolkit.Utilities` assembly.
```

## Breaking changes & migration

- **Forms:** none for existing forms (`FadingEnabled` defaults to `false`). Internal `KryptonFormFadeController.FadeOut` now hides at opacity 0 instead of closing; callers that needed close already use `FadeOutAndClose`.
- **Extended message box:** timeout was previously incomplete (close without applying `timerResult`; RTL opened a nested `Show`). Callers that relied on a timed-out dialog returning `DialogResult.None` should set `TimeOutResult` / `timerResult` explicitly; `None` now falls back to the default button result.

## Developer documentation

- `Documents/Development/KryptonForm-FadeValues.md`
- `Documents/Development/Krypton-MessageBoxExtended-Fade-Timeout.md`
- `Documents/Development/Krypton-MessageBoxExtended-Foldable.md`

## Checklist

- [x] Builds for `net472` and is C# 7.3 compatible where required
- [x] New compiler/analyzer warnings in touched code addressed
- [x] `Documents/Changelog/Changelog.md` updated
- [x] TestForm demo added or updated (features / observable bug fixes)
- [x] `Documents/Development/` guide added (substantial features)
- [ ] Screenshots/GIFs included (UI changes)
- [x] Breaking-change impact and TFM notes documented above
