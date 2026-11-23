Suggested title: `Fix: short imperative title (#ISSUE)`

Delete any section that does not apply.

## Summary

What changed, and why it matters to consumers of the toolkit.

## Related issues

- Closes #ISSUE

## Type of change

- [x] Bug fix (`Resolved`)
- [ ] **Breaking change** (API removal/rename, behavior change requiring migration, assembly/namespace move)

## Changes

- Notable change, grouped by area or project.

## Affected packages & target frameworks

- Packages: `Krypton.Toolkit`, `Krypton.Ribbon`, `Krypton.Navigator`, `Krypton.Workspace`, `Krypton.Docking`, `Krypton.Toolkit.Utilities`, `Krypton.Navigator.Utilities` (list only those touched).
- TFMs verified: `net472`, `net48`, `net481`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`, `net11.0-windows` (list only those built).

## Validation

- TestForm demo: `FormName` (registered in `StartScreen.AddButtons()`), or N/A.
- Manual steps:
  1. Step
  2. Expected result
- Build: `dotnet build ".\Source\Krypton Components\Krypton Toolkit Suite 2022 - VS2022.sln" -c Debug`

## Screenshots / GIFs

Save stills or GIFs as `Documents/PR/ISSUE-short-title-state.png` (or `.gif`). Do not upload or attach them to this pull request. Remove this section when there is no UI change.

- Local draft path: `Documents/PR/ISSUE-short-title-state.png` (or N/A — no UI change)

## Changelog

- Entry added to `Documents/Changelog/Changelog.md`:

```markdown
- Resolved [#ISSUE](https://github.com/Krypton-Suite/Standard-Toolkit/issues/ISSUE), short user-facing summary.
```

## Breaking changes & migration

None, or describe what broke and what consumers must update.

## Build log

Save the build-log screenshot as `Documents/PR/ISSUE-short-title-build.png`. Do not upload or attach it to this pull request.

- Local draft path: `Documents/PR/ISSUE-short-title-build.png`

## Checklist

- [ ] Builds for `net472` and is C# 7.3 compatible where required
- [ ] New compiler/analyzer warnings in touched code addressed
- [ ] `Documents/Changelog/Changelog.md` updated
- [ ] TestForm demo added or updated (when the bug is observable)
- [ ] Screenshots/GIFs saved under `Documents/PR/` when there is a UI change (not attached here)
- [ ] Build log screenshot saved under `Documents/PR/` (not attached here)
- [ ] Breaking-change impact and TFM notes documented above
