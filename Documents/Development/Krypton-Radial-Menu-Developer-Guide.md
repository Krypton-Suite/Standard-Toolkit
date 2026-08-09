# Krypton Radial Menu — Developer Guide

## Overview

`KryptonRadialMenu` is a OneNote-style radial popup menu in the `Krypton.Toolkit.Utilities` assembly (issue [#4172](https://github.com/Krypton-Suite/Standard-Toolkit/issues/4172)). It is a designer `Component` (same role as `KryptonContextMenu`), not a form-hosted control and not tied to BarManager/Ribbon.

Consumers obtain it via the `Krypton.Standard.Toolkit` NuGet package (`Krypton.Toolkit.Utilities` assembly).

## Architecture

| Type | Role |
|------|------|
| `KryptonRadialMenu` | Public component: `Show` / `ShowPopup` / `Close`, lifecycle events, appearance proxies |
| `KryptonRadialMenuItemCollection` | Restricted collection of radial item types |
| `KryptonRadialMenuItem` | Command sector with optional nested `Items` and `KryptonCommand` |
| `KryptonRadialMenuSliderItem` | Ring slider editor |
| `KryptonRadialMenuColorPaletteItem` | Colour palette editor ring |
| `KryptonRadialMenuFontListItem` | Font list editor with scroll paging |
| `KryptonRadialMenuTextItem` | In-ring text editor (confirm / Esc cancel) |
| `KryptonRadialMenuCalendarItem` | Month / day editor (wheel changes month) |
| `VisualRadialMenuPopup` | Circular `VisualPopup` host (hit-test, keyboard, a11y, animations) |
| `RadialLayoutEngine` / `RadialMenuPainter` | Sector geometry and painting |
| `KryptonRadialMenuContextMenuBridge` | Projects supported context-menu types into radial items |
| `KryptonRadialMenuPresenter` | Optional PreferRadial soft hook + cached live projections |

```text
KryptonContextMenu.Show
        │
        ├─ AlternativeShow hook (Utilities Presenter when PreferRadial)
        │         └─ KryptonRadialMenu (live-synced projection)
        │
        └─ null hook → VisualContextMenu (linear)
```

## Public usage

```csharp
var menu = new KryptonRadialMenu();
menu.Items.Add(new KryptonRadialMenuItem("Open", (_, _) => DoOpen()));
menu.Items.Add(new KryptonRadialMenuTextItem { Label = "Note", Text = "Hello" });
menu.Items.Add(new KryptonRadialMenuCalendarItem { SelectedDate = DateTime.Today });
menu.Show(this, clientPoint);
```

### Appearance / behaviour values

Proxied on `KryptonRadialMenu` / `Values`:

- `AnimationStyle` / `AnimationDuration` — open, navigate, and reverse close animation
- `DisplayStyle` / `ItemImageSize` / `ShowShadow` / `ShowCheckedGlyph`
- `StartAngle` — first sector angle (default `-90` = top)
- `MaxVisibleItems` — `0` unlimited; otherwise page window with prev/next affordances
- `HitPadding` — extra annular hit padding for touch (default `4`)
- `SubMenuGlyph` / `OuterRingThickness`

RTL hosts (`RightToLeft.Yes`) mirror start angle / sweep. Image size and hit padding scale with popup DPI when available.

### Context-menu bridge

```csharp
radial.ImportFrom(contextMenu, liveSync: true);
// or
var radial = KryptonRadialMenu.FromContextMenu(contextMenu, liveSync: true);
```

Supported mappings include command items, link labels, check/radio styles, colour columns, image select, combo, progress, **TextBox → `KryptonRadialMenuTextItem`**, **MonthCalendar → `KryptonRadialMenuCalendarItem`**. Separators and headings are skipped.

Live sync:

1. **Collection** — root `Items` changes re-import the projection.
2. **Property** — `PropertyChanged` on bridged `Tag` sources updates common twins (`Text` / `Enabled` / `Visible` / `Checked` / `Image`) without a full rebuild when possible.

This is **not** dual-hosting one item instance in two UIs.

### PreferRadial soft hook

Toolkit exposes a nullable static:

```csharp
KryptonContextMenu.AlternativeShow
```

When `KryptonRadialMenuPresenter.PreferRadialContextMenus` is `true`, Utilities registers the hook so normal `KryptonContextMenu.Show` call sites present the radial projection. There is **no** Toolkit → Utilities project reference.

```csharp
KryptonRadialMenuPresenter.PreferRadialContextMenus = true;
contextMenu.Show(this, pt); // radial via AlternativeShow
```

## Accessibility

`VisualRadialMenuPopup` exposes a custom `AccessibleObject` that surfaces sectors (and editor cells) as menu items and updates the accessible name when tracking changes.

## Validation

- TestForm: **Radial Menu (#4172)** (`RadialMenuDemo`)
- Unit test: `Scripts/UnitTests/UnitTest-RadialMenu.ps1` (STA)
- Screenshot helper: `Scripts/UnitTests/Invoke-RadialMenuScreenshot.ps1`

Manual theme check: exercise the demo under a few `PaletteMode` values; a full theme-matrix GIF farm is out of scope.
