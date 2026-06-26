## ModernBuild — User Guide

### What is ModernBuild?
ModernBuild is a Windows terminal UI (TUI) tool for building, packing, and publishing the Krypton Suite from the repository root using MSBuild and NuGet. It provides a simple keyboard-driven interface to:
- Build, rebuild, and pack channel-specific `.proj` files
- Create and optionally zip NuGet packages
- Push packages to NuGet.org or GitHub Packages

It uses `Terminal.Gui` for the UI and drives `MSBuild.exe` and `nuget.exe` under the hood. No CLI arguments are required in this version.

### Requirements
- Windows 10/11
- Visual Studio 2022 or later with the MSBuild component
  - ModernBuild auto-detects MSBuild via `vswhere.exe`, then `%ProgramFiles%` install paths. Set `MSBUILDPATH` or `MSBUILD_PATH` to override. The Build Settings panel shows the resolved Visual Studio product, MSBuild path, and MSBuild version.
- `nuget.exe` available on PATH for NuGet operations
  - Download from `https://dist.nuget.org/win-x86-commandline/latest/nuget.exe` and place it in a folder on PATH, or in the repo’s `Scripts/` folder.

### Repository layout ModernBuild expects
- Project files are resolved from the selected scripts profile:
  - `Scripts/VS2022/`
  - `Scripts/Current/`
  - `Scripts/Build/`
  - legacy fallbacks: `Scripts/` and `Scripts/Project-Files/`
- Logs are written to `Logs/`
- Packages are produced into `Bin/Release/`

### Build and run
- Build (optional):
```powershell
dotnet build Scripts/ModernBuild/ModernBuild.csproj -c Release
```
- Run (recommended):
```powershell
dotnet run --project Scripts/ModernBuild/ModernBuild.csproj
```
- Or run the built executable (example path):
```powershell
Scripts/ModernBuild/bin/Debug/net9.0-windows/ModernBuild.exe
```

Tip: Run ModernBuild from the repository root so it can auto-detect the correct `.proj` paths.

### UI layout
- Tasks (top-left): hotkeys and current selections; switches between Ops and NuGet pages
- Build Settings (left): current project, Visual Studio product, MSBuild path/version, and log file locations
- Live Output (right): streaming MSBuild/NuGet output; press Enter on a line to copy it
- Summary (bottom): recent summary/log tail for quick diagnostics (paged)

### Global controls
- F4: switch between Ops and NuGet pages
- F5: start/stop the current action
- F9: cycles scripts profile, except Stable Pack/BuildPack where it cycles PackMode
- ESC or F10: exit
- Auto-Scroll checkbox: toggles following live output

### Ops page (build workflow)
- F1 Channel: cycles Nightly → Canary → Stable
  - When Nightly is selected, the configuration defaults to Debug
  - For other channels, default configuration is the channel-appropriate one (e.g., Canary) or Release
- F2 Action:
  - Nightly cycles Build → Rebuild → Pack → BuildPack → Debug
  - Canary/Stable cycles Build → Pack → BuildPack → Debug
  - Build runs `Build`
  - Rebuild runs `Rebuild` for Nightly only
  - Pack: runs channel-appropriate `Pack`
  - BuildPack: runs `Rebuild` then `Pack` for Nightly, otherwise `Build` then `Pack`
  - Debug: runs a clean, switches to Nightly, then executes `Rebuild`
- F3 Config: toggles Release/Debug (Nightly may auto-switch to Debug)
- F6 Tail: cycles live-output buffer size 200 → 500 → 1000 lines
- F7 Clean: deletes `Bin/`, component `obj/`, and `Logs/`
- F8 Clear: clears the live output and resets horizontal scroll
- F9 Scripts: cycles Auto → VS2022 → Current, except Stable Pack/BuildPack where it cycles Pack → PackLite → PackAll

### NuGet page (packaging and publishing)
- Configuration is set to Release when entering the NuGet page
- F1 Channel: still available (affects which `.proj` may be used for pack steps)
- F2 Action: cycles
  - Rebuild+Pack: `Rebuild` then `Pack` for Nightly, otherwise `Build` then `Pack`
  - Push: push existing packages only
  - Pack+Push: `Pack` then push
  - Rebuild+Pack+Push: `Rebuild`, `Pack`, then push for Nightly, otherwise `Build`, `Pack`, then push
  - Update NuGet: runs `nuget.exe update -Self -NonInteractive`
- F3 Config: toggles Release/Debug (normally keep Release for publishing)
- F5 Run/Stop: executes the selected action(s)
- F6 Symbols: toggles inclusion of `.snupkg` symbol packages when pushing/previewing
- F7 SkipDup: toggles `-SkipDuplicate` for pushes
- F8 Source: cycles Default → NuGet.org → GitHub → Custom
  - NuGet.org: requires an API key
    - Set once: `nuget.exe setapikey <KEY> -Source https://api.nuget.org/v3/index.json`
  - GitHub: ensure a source named `github` exists (e.g., `nuget.exe sources add -Name github -Source https://nuget.pkg.github.com/<OWNER>/index.json`)
  - Custom: not configurable via UI in this version; if selected with no URL set, ModernBuild will emit an error
- Create ZIP: when visible, creates `Bin/<yyyyMMdd>_NuGet_Packages.zip` after packing
- TEST: previews the exact `nuget.exe push` commands in the Summary panel

### Outputs and logs
- Text summary: `Logs/<channel>-build-summary.log`
- Binary log: `Logs/<channel>-build.binlog`
- Packages: `Bin/Release/*.nupkg` (and optionally `*.snupkg`)
- Optional ZIP: `Bin/<yyyyMMdd>_NuGet_Packages.zip`

### Status and navigation
- Status bar shows: RUNNING/DONE/FAILED, elapsed time, and running counts of errors/warnings
- Summary panel is paged:
  - PageUp/PageDown/Home/End adjust the paging (when Summary is not focused)
  - When focused, TextView handles its own navigation

### How ModernBuild chooses the project file
- Nightly channel → `nightly.proj`
- Canary channel → `canary.proj`
- Stable channel → `build.proj`
- Installer (not exposed on Ops page) → `installer.proj`

The Auto scripts profile chooses `VS2022` when MSBuild is detected under Visual Studio 2022, `Current` when MSBuild is detected under a yearly Visual Studio install (major 18+, e.g. VS 2026), and otherwise defaults to `Current`.

The effective profile controls the search order:
- VS2022: `Scripts/VS2022/`, then `Scripts/Current/`, then `Scripts/Build/`
- Current: `Scripts/Current/`, then `Scripts/VS2022/`, then `Scripts/Build/`
- Both profiles then fall back to `Scripts/` and `Scripts/Project-Files/`

### Troubleshooting
- "Could not find MSBuild.exe"
  - Ensure Visual Studio with the MSBuild component is installed; set `MSBUILDPATH` or `MSBUILD_PATH` if needed; rerun ModernBuild
- `nuget.exe` not found
  - Put `nuget.exe` on PATH or in the repo’s `Scripts/` folder
- "Project file not found: ..."
  - Verify the channel project files exist in the selected scripts profile folder or fallback locations
- Build succeeds with no observable work
  - ModernBuild will note this in output; verify your Configuration and the `.proj` target frameworks so targets aren’t skipped
- No packages found to push
  - Ensure packages exist in `Bin/Release/` for the chosen channel/config
- Custom NuGet source
  - Not settable via UI in this version; selecting Custom without a URL will cause validation to fail

### Notes
- ModernBuild kills the entire MSBuild process tree when you stop a running action
- No CLI options are supported in this version; all interaction is via the UI
