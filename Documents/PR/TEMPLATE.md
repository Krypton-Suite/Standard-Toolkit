# <Type>: <Short imperative title> (#<issue>)

<!--
Copy this file to Documents/PR/<issue-or-branch>-<short-title>.md and fill it in.
Type = "Fix" for bug fixes (Resolved) or "Feature" for enhancements (Implemented).
Delete any section that does not apply.
-->

## Summary

<1-3 sentences describing what changed and why it matters to consumers of the toolkit.>

## Related issues

- Closes #<issue>

## Type of change

- [ ] Bug fix (`Resolved`)
- [ ] Feature / enhancement (`Implemented`)
- [ ] **Breaking change** (API removal/rename, behavior change requiring migration, assembly/namespace move)
- [ ] Documentation only

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

<Before/after images for any UI change. Remove if not a UI change.>

## Changelog

- Entry added to `Documents/Changelog/Changelog.md`:

```markdown
* Resolved [#<issue>](https://github.com/Krypton-Suite/Standard-Toolkit/issues/<issue>), <short user-facing summary>.
```

## Breaking changes & migration

<None, or describe what broke and exactly what consumers must update.>

## Developer documentation

- `Documents/Development/<guide>.md` (add for substantial new features; omit for trivial fixes).

## Checklist

- [ ] Builds for `net472` and is C# 7.3 compatible where required
- [ ] New compiler/analyzer warnings in touched code addressed
- [ ] `Documents/Changelog/Changelog.md` updated
- [ ] TestForm demo added or updated (features / observable bug fixes)
- [ ] `Documents/Development/` guide added (substantial features)
- [ ] Screenshots/GIFs included (UI changes)
- [ ] Breaking-change impact and TFM notes documented above
