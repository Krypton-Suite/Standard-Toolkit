<!--
Suggested PR title: Feature: <Short imperative title> (#<issue>)
Delete any section that does not apply.
-->

## Summary

<1-3 sentences describing what changed and why it matters to consumers of the toolkit.>

## Related issues

- Closes #<issue>

## Type of change

- [x] Feature / enhancement (`Implemented`)
- [ ] **Breaking change** (API removal/rename, behavior change requiring migration, assembly/namespace move)

## Changes

- <Notable change 1, grouped by area/project.>
- <Notable change 2.>

## Affected packages & target frameworks

- Packages: `Krypton.Toolkit`, `Krypton.Ribbon`, `Krypton.Navigator`, `Krypton.Workspace`, `Krypton.Docking`, `Krypton.Toolkit.Utilities`, `Krypton.Navigator.Utilities` (list only those touched).
- TFMs verified: `net472`, `net48`, `net481`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`, `net11.0-windows` (list only those built).

## Validation

- TestForm demo: `<FormName>` (registered in `StartScreen.AddButtons()`).
- Manual steps:
  1. <step>
  2. <expected result>
- Build: `dotnet build ".\Source\Krypton Components\Krypton Toolkit Suite 2022 - VS2022.sln" -c Debug`

## Screenshots / GIFs

<!-- Capture per AGENTS.md into Documents/PR/<issue>-<short-title>-<state>.png or .gif. Do not upload or attach images to this GitHub pull request. -->

- Local draft path: `Documents/PR/<issue>-<short-title>-<state>.png` (or N/A — no UI change)

## Changelog

- Entry added to `Documents/Changelog/Changelog.md`:

```markdown
- Implemented [#<issue>](https://github.com/Krypton-Suite/Standard-Toolkit/issues/<issue>), <short user-facing summary>.
```

## Breaking changes & migration

<None, or describe what broke and exactly what consumers must update.>

## Developer documentation

- `Documents/Development/<guide>.md` (add for substantial new features; omit for trivial enhancements).

## Build log

<!-- Capture per AGENTS.md Build Log Screenshot. Save as Documents/PR/<issue>-<short-title>-build.png. Do not upload or attach it to this GitHub pull request. -->

- Local draft path: `Documents/PR/<issue>-<short-title>-build.png`

## Checklist

- [ ] Builds for `net472` and is C# 7.3 compatible where required
- [ ] New compiler/analyzer warnings in touched code addressed
- [ ] `Documents/Changelog/Changelog.md` updated
- [ ] TestForm demo added or updated
- [ ] `Documents/Development/` guide added (substantial features)
- [ ] Screenshots/GIFs saved under `Documents/PR/` when there is a UI change (not attached here)
- [ ] Build log screenshot saved under `Documents/PR/` (not attached here)
- [ ] Breaking-change impact and TFM notes documented above
