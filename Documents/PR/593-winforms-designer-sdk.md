# Feature: WinForms Designer Extensibility SDK type routing (#593)

## Summary

Fold the OOP designer spike’s discovery and Client-editor ideas into the existing #593 `*.Design` dual-compile stack. DesignToolsServer registers designers through MEF type routing; Visual Studio-hosted image and folder editors resolve via `Krypton.Toolkit.Design.Client`. TestForm stays on `ProjectReference` (runtime host only).

## Related issues

- Closes #593 (in progress; this is the type-routing / Client-editor follow-through)

## Type of change

- [x] Feature / enhancement (`Implemented`)

## Changes

- Linked `KryptonDesignerTypeRoutingProvider` into every Design.Server project (short name + full name for each `ComponentDesigner`).
- Client `KryptonDesignerEditorNames` assembly-qualified names for image/folder/`InitialDirectory` editors (SDK 1.6 net472 Client has no type-routing API).
- `[Editor]` attributes use `KryptonWinFormsDesignerSdk.ImageEditor` (Framework AQN vs modern Client name).
- Solution folder **DesignerSdk** (`.sln` / `.slnx`). Disk paths unchanged.
- Changelog, README ProjectReference note, TestForm host copy.

## Affected packages & target frameworks

- Packages: `Krypton.Toolkit`, `Krypton.Ribbon`, `Krypton.Navigator`, `Krypton.Workspace`, `Krypton.Toolkit.Utilities`, `Krypton.Navigator.Utilities` (Design/Client/Protocol).
- TFMs verified: `net472`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`.

## Validation

- TestForm demo: `WinFormsDesignerSdkDemo` (StartScreen **593 WinForms Designer SDK**). Runtime host only.
- OOP designers: pack NuGet, consume from a net8+-windows WinForms app (not TestForm ProjectReference), restart Visual Studio.
- Build: `dotnet build ".\Source\Krypton Components\Krypton.Toolkit.Design\Krypton.Toolkit.Design.csproj" -c Debug /p:ExcludeNet11=true`

## Screenshots / GIFs

No designer chrome from TestForm: `ProjectReference` does not load OOP designers. Capture VS designer stills after a packed-nupkg host run; keep them local next to this file and do not upload them to GitHub.

## Changelog

```markdown
* Implemented [#593](https://github.com/Krypton-Suite/Standard-Toolkit/issues/593), **[Breaking Change]** Use the WinForms Designer Extensibility SDK for .NET
 * DesignToolsServer discovers designers via MEF type routing (short name and full name) in addition to assembly-qualified `[Designer]` attributes. Visual Studio-hosted image and folder editors resolve from `Krypton.Toolkit.Design.Client` via assembly-qualified `[Editor]` names.
 * OOP designer features require a NuGet reference (including a local feed). `ProjectReference` (including TestForm in this repo) does not populate `designer.deps.json`, so the .NET designer falls back to the default `ControlDesigner`.
```

## Breaking changes & migration

Same V110 #593 notes. Additional contributor note: OOP designers are not visible from a `ProjectReference` to this repo; use a packed nupkg.

## Developer documentation

- `Documents/Development/WinForms-Designer-Extensibility-SDK.md` (local; not in the GitHub PR)

## Checklist

- [x] Builds for `net472` and is C# 7.3 compatible where required
- [ ] New compiler/analyzer warnings in touched code addressed
- [x] `Documents/Changelog/Changelog.md` updated
- [x] TestForm demo added or updated (features / observable bug fixes)
- [x] `Documents/Development/` guide added (substantial features)
- [ ] Screenshots/GIFs included (UI changes) — pending nupkg-fed VS designer host
- [x] Breaking-change impact and TFM notes documented above
