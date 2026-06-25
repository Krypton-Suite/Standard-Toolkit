# ModernBuild - User Guide

## What is ModernBuild?

ModernBuild is a Windows terminal UI (TUI) tool for building, packing, and publishing Krypton Suite artifacts from the repository root using MSBuild and NuGet.

It provides a keyboard-driven workflow to:

- Build channel-specific `.proj` files
- Pack NuGet packages
- Push packages to NuGet feeds
- Preview push commands before execution

ModernBuild uses `Terminal.Gui` and runs `MSBuild.exe` and `nuget.exe` under the hood. No CLI arguments are required.

### Requirements

- Windows 10/11
- Visual Studio with MSBuild (VS2022 and newer supported)
  - ModernBuild uses `vswhere.exe` to locate MSBuild and falls back to `%ProgramFiles%` install paths
  - Batch build scripts share `Scripts/Common/find-msbuild.cmd`; set `MSBUILDPATH` or `MSBUILD_PATH` to override
- `nuget.exe` available on PATH for NuGet operations
  - Example download: `https://dist.nuget.org/win-x86-commandline/latest/nuget.exe`

### Repository layout ModernBuild expects

ModernBuild supports the current scripts structure:

- `Scripts/VS2022/` (VS 2022 toolset)
- `Scripts/Current/` (current/newer toolset, used for VS18/VS2026 style setup)
- `Scripts/Build/` (fallback scripts set)

Legacy fallback locations are still probed:

- `Scripts/`
- `Scripts/Project-Files/`

Logs are written to `Logs/`. NuGet package discovery supports both legacy `Bin/*` outputs and `artifacts/packages/*` outputs.

### Build and run

The project multi-targets `net8.0-windows`, `net9.0-windows`, and `net10.0-windows`. Pass `-f <tfm>` to build or run a specific framework (otherwise the SDK may build all targets).

Build (example, Release on .NET 9):

```powershell
dotnet build Scripts/ModernBuild/ModernBuild.csproj -c Release -f net9.0-windows
```

Run:

```powershell
dotnet run --project Scripts/ModernBuild/ModernBuild.csproj -f net9.0-windows
```

Or run a built executable after building, for example:

```powershell
Scripts\ModernBuild\bin\Release\net9.0-windows\ModernBuild.exe
```

Use `Debug` instead of `Release` when you built a Debug configuration, and substitute `net8.0-windows` or `net10.0-windows` if you targeted that TFM.

### UI layout

- Tasks (top-left): hotkeys and current selections
- Build Settings (left): resolved project file, scripts profile, Visual Studio product, MSBuild path, MSBuild version, logs
- Live Output (right): streaming MSBuild/NuGet output
- Summary (bottom): tail summary of the last run

### Global controls

- `F4`: switch between Ops and NuGet pages
- `F5`: run/stop the current action
- `ESC` or `F10`: exit
- Auto-Scroll checkbox: follow live output

### Scripts profile selection (`F9`)

`F9` can switch script profiles:

- `Auto -> VS2022 -> Current -> Auto`

In Auto mode, ModernBuild chooses an effective profile from the detected MSBuild path:

- MSBuild path containing `\Microsoft Visual Studio\2022\` -> `VS2022`
- MSBuild path containing `\Microsoft Visual Studio\18\` -> `Current`
- otherwise defaults to `Current`

Special case:

- On Ops page, when `Channel = Stable` and `Action = Pack` or `BuildPack`, `F9` cycles `PackMode` instead (`Pack -> PackLite -> PackAll`).

### Ops page (build workflow)

- `F1` Channel: `Nightly -> Canary -> Stable`
- `F2` Action:
  - Nightly: `Build -> Rebuild -> Pack -> BuildPack -> Debug -> Build`
  - Canary/Stable: `Build -> Pack -> BuildPack -> Debug -> Build`
- `F3` Config: toggles `Release` / `Debug`
- `F6` Tail: cycles output buffer size `200 -> 500 -> 1000`
- `F7` Clean: deletes `Bin/`, selected component `obj/`, and `Logs/`
- `F8` Clear: clears live output view

Action behavior:

- `Build` runs MSBuild target `Build`
- `Rebuild` is available on Nightly and runs target `Rebuild`
- `Pack` runs channel pack target (`Pack`, or Stable `PackLite`/`PackAll` via PackMode)
- `BuildPack` runs clean-build target then pack:
  - Nightly uses `Rebuild` then pack
  - Canary/Stable uses `Build` then pack
- `Debug` triggers clean, switches to Nightly, then runs `Rebuild`

### NuGet page (packaging and publishing)

- Entering NuGet page forces configuration to `Release`
- `F1` Channel remains available
- `F2` NuGet Action cycle:
  - `Rebuild+Pack` (Nightly) or `Build+Pack` (Canary/Stable)
  - `Push`
  - `Pack+Push`
  - `Rebuild+Pack+Push` (Nightly) or `Build+Pack+Push` (Canary/Stable)
  - `Update NuGet`
- `F5` Run/Stop: executes selected workflow
- `F6` Symbols: include `.snupkg`
- `F7` SkipDup: toggle `-SkipDuplicate`
- `F8` Source: `Default -> NuGet.org -> GitHub -> Custom`
- `F9` Scripts profile: cycles scripts profile on NuGet page
- `TEST`: preview push commands in Summary

Source notes:

- NuGet.org requires an API key:
  - `nuget.exe setapikey <KEY> -Source https://api.nuget.org/v3/index.json`
- GitHub source must exist in NuGet sources (for example name `github`)
- Custom source must be set in state; if empty, validation fails

### Outputs and logs

- Text summary log: `Logs/<channel>-build-summary.log`
- Binary log: `Logs/<channel>-build.binlog`
- Optional ZIP: `Bin/<yyyyMMdd>_NuGet_Packages.zip`

### How ModernBuild resolves project files

File names by context:

- Nightly -> `nightly.proj`
- Canary -> `canary.proj`
- Stable -> `build.proj`
- Installer -> `installer.proj`

Resolution order depends on effective scripts profile:

- Effective `VS2022`:
  - `Scripts/VS2022/<file>`
  - `Scripts/Current/<file>`
  - `Scripts/Build/<file>`
- Effective `Current`:
  - `Scripts/Current/<file>`
  - `Scripts/VS2022/<file>`
  - `Scripts/Build/<file>`

Legacy fallback (both modes):

- `Scripts/<file>`
- `Scripts/Project-Files/<file>`

### Troubleshooting

- `Could not find MSBuild.exe`
  - Ensure Visual Studio/MSBuild is installed
- `Project file not found: ...`
  - Verify scripts folders and selected scripts profile
- `MSB4057 target ... does not exist`
  - Usually means the chosen action is incompatible with that channel/project file
  - Current versions prevent invalid Ops combos and map non-Nightly rebuild-like flows to `Build`
- `nuget.exe` not found
  - Put `nuget.exe` on PATH
- No packages found for push
  - Ensure expected packages exist under `Bin/Packages/<Configuration>`, `Bin/<Configuration>`, or `artifacts/packages/<Configuration>`

### Notes

- Stopping a run kills the entire spawned process tree
- No command-line options are currently exposed; interaction is through the TUI
