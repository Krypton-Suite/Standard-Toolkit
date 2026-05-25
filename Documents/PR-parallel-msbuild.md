# Phased parallel MSBuild (safe `/m`)

## Summary

Enables multi-processor MSBuild (`/m`) together with **phased project orchestration** and **per-TFM intermediate output**, avoiding file-lock races when building via `run.cmd` or script wrappers.

## Problem

All `Krypton.*` projects write to `Bin/<Configuration>/<TargetFramework>/`. A single `<MSBuild>` batch with `BuildInParallel="true"` on every project caused concurrent writes and local build failures.

## Solution

1. **`Scripts/Build/Krypton.Orchestration.targets`** — Toolkit → Ribbon+Navigator (parallel) → Workspace → Docking (`KryptonBuild` / `KryptonPack`).
2. **`Source/Krypton Components/Directory.Build.props`** — `IntermediateOutputPath` per configuration and TFM.
3. **Orchestration `.proj` files** — `Build` depends on `KryptonBuild`; Clean/Restore/Pack prep stay sequential (`BuildInParallel="false"`).
4. **`/m` on outer MSBuild** — `.cmd`, CI workflows, ModernBuild (parallel TFMs within each phase).

## Test plan

- [ ] `run.cmd` → Nightly twice in a row without `purge.cmd`
- [ ] `Scripts\VS2022\build-stable.cmd`
- [ ] CI `build.yml` / `nightly.yml`
