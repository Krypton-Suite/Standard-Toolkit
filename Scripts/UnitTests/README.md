# Unit Test Scripts

PowerShell helpers for interactive / UI-automation checks against Debug `TestForm` builds,
plus CI assert scripts named `UnitTest-*.ps1`.

## CI contract (future-proof)

Every `UnitTest-*.ps1` under `Scripts/UnitTests/` (including subfolders) **must** declare one marker
in the first ~80 lines:

```powershell
# UnitTest-CI: include   # discovered and run by Invoke-AllUnitTests / GitHub Actions
# UnitTest-CI: exclude   # interactive or host-dependent; never auto-run
```

Rules:

| Rule | Behaviour |
|------|-----------|
| `include` | Run in CI via `Invoke-AllUnitTests.ps1 -Strict` |
| `exclude` | Skipped; keep for local interactive use |
| Missing marker | **Fails** under `-Strict` / `UNITTEST_CI=1` (forces authors to opt in or out) |
| Zero `include` scripts | **Fails** under `-Strict` |
| Non-`UnitTest-*` helpers | Never auto-run (`Start-*`, `Invoke-*` drag, `Get-*`, `Convert-*`, `UnitTestCommon.ps1`) |

Shared optional parameters for `include` scripts (forwarded by the invoker):

- `-Configuration` (default `Debug`)
- `-TargetFramework` (default `net472`)
- `-BinDir` (optional override)

Exit `0` on success; non-zero on failure. Prefer STA-safe WinForms work; the invoker launches each include script with `powershell -STA`.

Workflow: [`.github/workflows/unit-tests.yml`](../../.github/workflows/unit-tests.yml) (also `workflow_call`-reusable).

Optional Discord notifications use repository secret `DISCORD_WEBHOOK_UNIT_TESTS`:

- Success and failure both post when the secret is set (cancelled runs are skipped).
- On-demand / reusable runs can opt out with `notify_discord=false` (`workflow_dispatch` default `true`).

## Prerequisites

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
```

Default output folder: `Bin\Debug\net472`.

## Scripts

| Script | Purpose | Marker |
|--------|---------|--------|
| `Invoke-AllUnitTests.ps1` | Discovers markers, runs every `include` script in STA children | (entry point) |
| `UnitTest-DesignerSerializationDefaults.ps1` | #4325 toolbox drop: core controls must not report designer `Modified` storage (`IsDefault` false) | `include` |
| `UnitTest-UnitTestInfrastructure.ps1` | Shared helpers + CI marker discovery smoke assert | `include` |
| `UnitTest-ThemeCatalog.ps1` | #4230 catalog: cores, enum/SupportedThemes order, Themes discovery, Materialize chrome, extraOnly Sparkle, Export/Import, sample provider | `include` |
| `UnitTest-RibbonDetachable.ps1` | #595 Ribbon detach/reattach lifecycle, floating window, drag-to-reattach support | `include` |
| `UnitTest-DockingDragTargetHeuristics.ps1` | #3858 Escape cancel + solid first-match priority + docking `FindTarget` removal | `include` |
| `UnitTest-RadialMenu.ps1` | #4172 radial menu API: defaults, Text/Calendar items, bridge, property sync, PreferRadial, show/close | `include` |
| `UnitTest-NavigatorTaskbarTabGroups.ps1` | #4129 TabGroup taskbar composites + float taskbar opt-in | `include` |
| `UnitTest-NavigatorCaptionTabRemerge.ps1` | #925 tear-out / remerge into a single window (in-process; no mouse) | `include` |
| `UnitTest-AsyncFormApis.ps1` | #4177 async dialog API gating (absent on net472; present on net9+ via `pwsh`) | `include` |
| `UnitTest-SystemInformationApi.ps1` | #3176 `KryptonSystemInformation` type/API smoke (no UI) | `include` |
| `UnitTest-SystemInformationUi.ps1` | #3176 host the viewer and wait for System Summary rows (WMI) | `include` |
| `UnitTest-InteractiveToolTips.ps1` | #4192 hosted-control tooltip / HTML helper / NotifyIcon popup API surface | `include` |
| `UnitTest-SplashScreenManager.ps1` | #4180 splash manager API: defaults, Show/SetStatus/Close, Run(steps), throwing step | `include` |
| `UnitTest-KryptonLogProtect.ps1` | #4270 / #4269 `KryptonLog` redacts `{Password}` before file storage | `include` |
| `UnitTest-BugReportEmailBody.ps1` | #4271 bug-report email body omits stack traces and SMTP password; `KryptonTextBox` password masking still works | `include` |
| `UnitTest-CommandLinkArrow.ps1` | #4264 default command-link arrow: helper returns 32x32 image; Windows 7 embedded resource is packaged | `include` |
| `UnitTest-RibbonOverflowGlyph.ps1` | #4253 QAT overflow chevrons paint at 96/144/192 DPI for Office 2007 and Office 2010 | `include` |
| `UnitTest-CustomPaletteBasePaletteMode.ps1` | #1870 `KryptonCustomPaletteBase.BasePaletteMode` inherits the builtin colour table; builtin `BasePalette` keeps catalog mode | `include` |
| `UnitTest-KryptonFormRtl.ps1` | #2103 `KryptonForm` RTL: `ScreenToWindow` stays physical; Close hit-tests on the right in LTR and the left with `RightToLeftLayout`; window region includes both physical left and right chrome | `include` |
| `UnitTest-ContextMenuSubMenuImage.ps1` | #4252 Light Gray Office 2007/2010/Microsoft 365 `GetContextMenuSubMenuImage` returns an image; all catalog palettes must not throw | `include` |
| `UnitTest-TreeViewMultiSelect.ps1` | #4326 `KryptonTreeView.MultiSelect` can be set to false independently of `CheckBoxes` | `include` |
| `Start-AsyncFormsDemoHost.ps1` | Hosts `Feature4177AsyncFormsDemo` | n/a |
| `Start-SplashScreenManagerHost.ps1` | Hosts `Feature4180SplashScreenManagerDemo` (#4180) | n/a |
| `Start-NavigatorFormIntegrationHost.ps1` | Hosts `NavigatorFormIntegrationDemo` | n/a |
| `Invoke-CaptionTabDrag.ps1` | Caption drag + screenshots | n/a |
| `Get-NavigatorCaptionTabProbe.ps1` | Caption geometry probe | n/a |
| `Get-NavigatorTabGroupColourShot.ps1` | Tab-group colour screenshot | n/a |
| `Start-RadialMenuDemoHost.ps1` | Hosts `RadialMenuDemo` (#4172) | n/a |
| `Invoke-RadialMenuScreenshot.ps1` | Opens radial menu and writes `Documents/PR/4172-radial-menu-native.png` | `exclude` |
| `Invoke-TreeViewMultiSelectScreenshot.ps1` | Hosts `Bug4326TreeViewMultiSelectDemo` and writes `Documents/PR/4326-treeview-multiselect-false.png` | `exclude` |
| `Invoke-SchemeStripTextScreenshot.ps1` | Hosts `SchemeStripTextDemo` (#1100) and writes default/contrast PNGs under `Documents/PR/` | `exclude` |
| `Invoke-ListViewStateTrackingScreenshot.ps1` | Hosts `Bug4336ListViewStateTrackingDemo` (#4336) and writes hover PNGs under `Documents/PR/` | `exclude` |
| `Invoke-KryptonFormRtlScreenshot.ps1` | Hosts `RTLFormBorderTest` (#2103) and writes `Documents/PR/2103-kryptonform-rtl-layout.png` | `exclude` |

## Run all CI assert tests (on demand)

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\UnitTests\Invoke-AllUnitTests.ps1 -Strict
```

The invoker prints `[PASS]` / `[FAIL]` / `[SKIP]` banners and a summary table. On GitHub Actions it also writes the Actions job summary and `::notice` / `::error` annotations.

**GitHub (on demand):** Actions → **Unit Tests** → **Run workflow**, or:

```powershell
gh workflow run "Unit Tests" -f configuration=Debug -f target_framework=net472 -f timeout_seconds=600 -f notify_discord=true
```

## Typical usage (#925 caption tabs)

```powershell
# In-process CI assert (no live host / mouse)
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-NavigatorCaptionTabRemerge.ps1

# Interactive mouse (optional): host the demo, then drag
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Start-NavigatorFormIntegrationHost.ps1

$hp = (Get-CimInstance Win32_Process -Filter "Name='powershell.exe'" |
    Where-Object { $_.CommandLine -like '*Start-NavigatorFormIntegrationHost*' }).ProcessId

powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\UnitTests\Invoke-CaptionTabDrag.ps1 `
    -HostPid $hp -FromX 200 -FromY 14 -ToX 80 -ToY 14 -Tag join
```

## Typical usage (#4129 taskbar tab groups)

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-NavigatorTaskbarTabGroups.ps1
```
Screenshots from interactive helpers are written under the bin/output directory and are not checked in.

## Typical usage (#4270 / #4269 log protect)

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-KryptonLogProtect.ps1
```

## Typical usage (#4253 ribbon overflow glyph)

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-RibbonOverflowGlyph.ps1
## Typical usage (#4271 bug-report email body)

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-BugReportEmailBody.ps1
```

## Typical usage (#1100 scheme strip text)

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SchemeStripTextScreenshot.ps1
```

Writes `Documents/PR/1100-scheme-strip-text-*.png` (local PR assets; do not commit).

