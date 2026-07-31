# Verification Scripts

PowerShell helpers for interactive / UI-automation checks against Debug `TestForm` builds.
They are not part of CI; use them when validating WinForms behaviour that is hard to cover with a unit test (caption chrome, drag/tear-out, context menus, and similar).

## Prerequisites

- Build the relevant projects first, for example:

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
```

- Default output folder: `Bin\Debug\net472` (override with `-BinDir` / `-Configuration` / `-TargetFramework` where supported).

## Scripts

| Script | Purpose |
|--------|---------|
| `Start-NavigatorFormIntegrationHost.ps1` | Hosts `NavigatorFormIntegrationDemo` from the Debug bin (STA). |
| `Invoke-CaptionTabDrag.ps1` | Drags from one caption-relative point to another; captures before/during/after screenshots. |
| `Test-NavigatorCaptionTabRemerge.ps1` | Tears out `Settings`, then drags it back onto the main window and asserts a single remaining window. |
| `Get-NavigatorCaptionTabProbe.ps1` | Prints form borders, caption-strip owner, and strip rectangle (debug aid). |

## Typical usage (#925 caption tabs)

```powershell
# Terminal 1 — host the demo
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\Verification\Start-NavigatorFormIntegrationHost.ps1

# Terminal 2 — note the host PID, then drag or remerge
$hp = (Get-CimInstance Win32_Process -Filter "Name='powershell.exe'" |
    Where-Object { $_.CommandLine -like '*Start-NavigatorFormIntegrationHost*' }).ProcessId

powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\Verification\Invoke-CaptionTabDrag.ps1 `
    -HostPid $hp -FromX 200 -FromY 14 -ToX 80 -ToY 14 -Tag join

powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\Verification\Test-NavigatorCaptionTabRemerge.ps1 `
    -HostPid $hp
```

Screenshots are written next to the script output directory you pass (default: `Bin\Debug\net472`).
