# Interactive and hosted-control tooltips

## Overview

[`KryptonToolTip`](../../Source/Krypton%20Components/Krypton.Toolkit/Controls%20Toolkit/KryptonToolTip.cs) can show either the existing SuperTip (title, body, image) or an **interactive** popup that hosts any WinForms `Control` — for example a `KryptonLinkLabel` or a small panel with buttons. This implements [issue #4192](https://github.com/Krypton-Suite/Standard-Toolkit/issues/4192).

Package: `Krypton.Toolkit`. Simple HTML fragments live in `Krypton.Toolkit.Utilities` (`KryptonHtmlToolTipContent`). `KryptonNotifyIcon.ShowPopupTip` shows the same chrome near the cursor (system balloon tips remain available). Issue [#900](https://github.com/Krypton-Suite/Standard-Toolkit/issues/900) is already closed as part of V110.

## Architecture

- `VisualPopupToolTip` paints heading content through `IContentValues` and tooltip chrome (`PaletteBackStyle.ControlToolTip`).
- When a hosted control is supplied, a fill layout sizes from `AutoSize` / `Size` and the control is parented onto the popup for the show lifetime.
- On dispose the hosted control is **unparented**, not destroyed. `KryptonToolTip` owns disposal only when `ownsContent` is true, and only when the association is cleared or the component is disposed.
- Hover SuperTips keep leave / mouse-down / close-timer dismiss.
- Interactive tips linger (`InteractiveLingerDelay`, default 300 ms) so the pointer can travel onto the popup. They dismiss when the pointer is over neither, or on click-away. `CloseIntervalDelay` applies only when `UseCloseTimerForInteractive` is true.
- Keyboard stays inert by default (`EnableInteractiveKeyboard` / `ToolTipValues.EnableInteractiveKeyboard`) so hover does not steal typing. When enabled, Escape dismisses.
- Target mouse-down does **not** dismiss interactive tips unless `DismissInteractiveOnTargetMouseDown` is true.
- `VisualControlBase.ToolTipValues.HostedContent` uses the same popup path; `ToolTipManager` does not cancel on leave while hosted content is set.

## Public API

```csharp
kryptonToolTip.SetToolTip(target, title, description, image);
kryptonToolTip.SetToolTip(target, hostedControl, ownsContent: true);
kryptonToolTip.SetToolTip(target, title, hostedControl, ownsContent: true);
kryptonToolTip.SetLinkToolTip(target, title, linkText, url);
kryptonToolTip.LinkClicked += (s, e) => { /* e.Url, e.Cancel */ };
kryptonToolTip.ClearToolTip(target);

target.ToolTipValues.EnableToolTips = true;
target.ToolTipValues.HostedContent = myPanel;

var html = KryptonHtmlToolTipContent.Create("See <a href=\"https://example.com\">docs</a>.");
kryptonToolTip.SetToolTip(target, "Help", html);

notifyIcon.ShowPopupTip(html, "Notice");
notifyIcon.KryptonContextMenu = kryptonContextMenu;
```

Constraints:

- Hosted content cannot be a `Form` or the hover target.
- The same control instance cannot be hosted by two `KryptonToolTip` associations.
- Prefer `AutoSize` content, or set an explicit `Size`.
- Designer extender `KryptonToolTipContent` can point at another form control; that control leaves the form while the tip is shown. Prefer creating content in code with `ownsContent: true`.

## Validation

TestForm: **KryptonToolTip** (`KryptonToolTipTest`) and **NotifyIcon** (`ShowBalloonTip` also shows a themed popup).

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-InteractiveToolTips.ps1
```
