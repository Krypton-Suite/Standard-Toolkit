# Feature: ButtonSpec.FillHeight for tall hosts (#4432) — V105-LTS

## Summary

`ButtonSpec.FillHeight` is an opt-in that stretches a ButtonSpec to the full height of its host allocation. The default remains vertically centred so headers, form chrome, and existing layouts stay unchanged. Tall input controls can opt in per ButtonSpec.

## Related issues

- Closes #4432

## Type of change

- [x] Bug fix (`Resolved`)
- [x] Feature / enhancement (`Implemented`)
- [ ] **Breaking change**
- [ ] Documentation only

## Changes

- `ButtonSpec.FillHeight` (`bool`, default `false`) with designer serialization / `IsDefault` / `Clone` support.
- `ViewLayoutCenter.FillHeight` stretches child height to the docked client area while keeping preferred width and horizontal centring.
- `ButtonSpecView` applies the flag at construction and on property change.
- TestForm demo `Bug4432ButtonSpecFillHeightDemo` (side-by-side centred vs fill, runtime toggle).

## Affected packages & target frameworks

- Packages: `Krypton.Toolkit`
- TFMs verified: Debug `net472` TestForm (see Validation)

## Validation

- TestForm demo: `Bug4432ButtonSpecFillHeightDemo` (registered in `StartScreen.AddButtons()`).
- Manual steps:
  1. Open **Bug 4432 ButtonSpec FillHeight**.
  2. Confirm each tall host shows a centred Close ButtonSpec and a full-height Next ButtonSpec.
  3. Toggle **Apply FillHeight to all specs** and resize — fill specs track control height; centred specs stay mid-control when the toggle is off.
- Build: `dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472`
- Probe: `powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Probe-ButtonSpecFillHeight.ps1`

## Screenshots / GIFs

![Centred vs FillHeight on tall hosts (V105-LTS)](./4432-buttonspec-fillheight-default.png)

## Changelog

```markdown
* Implemented [#4432](https://github.com/Krypton-Suite/Standard-Toolkit/issues/4432), `ButtonSpec.FillHeight` stretches ButtonSpecs to the full height of the host control (default remains vertically centred).
```

## Breaking changes & migration

None. Default behaviour is unchanged. Set `FillHeight = true` on each `ButtonSpec` / `ButtonSpecAny` that should fill the host height.

## Checklist

- [x] Builds for `net472` and is C# 7.3 compatible where required
- [x] `Documents/Changelog/Changelog.md` updated (V105-LTS Patch 4)
- [x] TestForm demo added
- [ ] Screenshots/GIFs included (capture after local TestForm run)
