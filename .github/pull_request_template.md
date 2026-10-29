Add `&template=fix.md` or `&template=feature.md` to this page's URL and press Enter to load the bug-fix or feature template.

## Summary

What changed, and why it matters to consumers of the toolkit.

## Related issues

- Closes #ISSUE

## Type of change

- [ ] Bug fix (`Resolved`)
- [ ] Feature / enhancement (`Implemented`)
- [ ] **Breaking change** (API removal/rename, behavior change requiring migration, assembly/namespace move)
- [ ] Documentation only

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

Save stills or GIFs as `Documents/PR/ISSUE-short-title-state.png` (or `.gif`). Do not upload or attach them to this pull request. Remove this section when there is no UI change. The local outline that embeds those images is [`Documents/PR/TEMPLATE.md`](https://github.com/Krypton-Suite/Standard-Toolkit/blob/alpha/Documents/PR/TEMPLATE.md) on `alpha`.

- Local draft path: `Documents/PR/ISSUE-short-title-state.png`

## Changelog

- Entry added to `Documents/Changelog/Changelog.md`. Use **Resolved** for a bug fix or **Implemented** for a feature:

```markdown
- Resolved [#ISSUE](https://github.com/Krypton-Suite/Standard-Toolkit/issues/ISSUE), short user-facing summary.
- Implemented [#ISSUE](https://github.com/Krypton-Suite/Standard-Toolkit/issues/ISSUE), short user-facing summary.
```

## Breaking changes & migration

None, or describe what broke and what consumers must update.

## Developer documentation

- `Documents/Development/guide.md` for a substantial new feature. Delete this section for a trivial fix.

## Build log

Save the build-log screenshot as `Documents/PR/ISSUE-short-title-build.png`. Do not upload or attach it to this pull request.

- Local draft path: `Documents/PR/ISSUE-short-title-build.png`

## Checklist

- [ ] Builds for `net472` and is C# 7.3 compatible where required
- [ ] New compiler/analyzer warnings in touched code addressed
- [ ] `Documents/Changelog/Changelog.md` updated
- [ ] TestForm demo added or updated when the change is observable
- [ ] `Documents/Development/` guide added for a substantial feature
- [ ] Screenshots/GIFs saved under `Documents/PR/` when there is a UI change (not attached here)
- [ ] Build log screenshot saved under `Documents/PR/` (not attached here)
- [ ] Breaking-change impact and TFM notes documented above
