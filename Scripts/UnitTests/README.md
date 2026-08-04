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

- Failures always post when the secret is set.
- Successful on-demand runs post when `notify_discord` is enabled (`workflow_dispatch` default `true`).

## Prerequisites

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
```

Default output folder: `Bin\Debug\net472`.

## Scripts

| Script | Purpose | Marker |
|--------|---------|--------|
| `Invoke-AllUnitTests.ps1` | Discovers markers, runs every `include` script in STA children | (entry point) |
| `UnitTest-UnitTestInfrastructure.ps1` | Shared helpers + CI marker discovery smoke assert | `include` |
| `UnitTest-NavigatorTaskbarTabGroups.ps1` | #4129 TabGroup taskbar composites + float taskbar opt-in (needs feature binaries) | `exclude` |
| `UnitTest-NavigatorCaptionTabRemerge.ps1` | Tear-out / remerge (needs `-HostPid`) | `exclude` |
| `Start-NavigatorFormIntegrationHost.ps1` | Hosts `NavigatorFormIntegrationDemo` | n/a |
| `Invoke-CaptionTabDrag.ps1` | Caption drag + screenshots | n/a |
| `Get-NavigatorCaptionTabProbe.ps1` | Caption geometry probe | n/a |
| `Get-NavigatorTabGroupColourShot.ps1` | Tab-group colour screenshot | n/a |

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
# Terminal 1 - host the demo
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Start-NavigatorFormIntegrationHost.ps1

# Terminal 2 - note the host PID, then drag or remerge
$hp = (Get-CimInstance Win32_Process -Filter "Name='powershell.exe'" |
    Where-Object { $_.CommandLine -like '*Start-NavigatorFormIntegrationHost*' }).ProcessId

powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\UnitTests\Invoke-CaptionTabDrag.ps1 `
    -HostPid $hp -FromX 200 -FromY 14 -ToX 80 -ToY 14 -Tag join

powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\UnitTests\UnitTest-NavigatorCaptionTabRemerge.ps1 `
    -HostPid $hp
```

## Typical usage (#4129 taskbar tab groups)

```powershell
dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-NavigatorTaskbarTabGroups.ps1
```
Screenshots from interactive helpers are written under the bin/output directory and are not checked in.
