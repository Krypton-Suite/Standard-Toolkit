# Feature: Interactive hosted-control tooltips (#4192)

## Summary

`KryptonToolTip` can host any WinForms control inside a themed tooltip (hyperlinks, buttons, HTML fragments, small panels). SuperTip text/image behaviour is unchanged. `KryptonNotifyIcon.ShowPopupTip` uses the same chrome.

## Related issues

- Closes #4192

## Type of change

- [x] Feature / enhancement (`Implemented`)

## Changes

- `VisualPopupToolTip` hosts a `Control` inside tooltip chrome and unparents it on dispose.
- `KryptonToolTip.SetToolTip(target, content)`, `SetLinkToolTip`, `LinkClicked`, extender `KryptonToolTipContent`.
- Linger dismiss; optional keyboard, close timer, and target mouse-down dismiss.
- `ToolTipValues.HostedContent` for built-in Krypton control tips.
- `KryptonHtmlToolTipContent` in Utilities (text + `<a href>` + `<br>`).
- `KryptonNotifyIcon.ShowPopupTip` and `KryptonContextMenu`.

## Affected packages & target frameworks

- Packages: `Krypton.Toolkit`, `Krypton.Toolkit.Utilities`
- TFMs verified: `net472`

## Validation

- TestForm demo: `KryptonToolTipTest`, `NotifyIconTest` balloon also shows a themed popup.
- `Scripts/UnitTests/UnitTest-InteractiveToolTips.ps1` (`include`).
- Build: `dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472`

## Changelog

Entry added to `Documents/Changelog/Changelog.md` for #4192.

## Breaking changes & migration

None.

## Developer documentation

- `Documents/Development/Krypton-Interactive-ToolTips.md`

## Checklist

- [x] Builds for `net472`
- [x] Changelog, TestForm demo, developer guide updated
