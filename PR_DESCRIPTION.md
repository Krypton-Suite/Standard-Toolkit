# Fix CI: WebView2 workflows and release Pack restore (NETSDK1005)

## Summary

Stabilises GitHub Actions for **nightly**, **canary**, and **build** by consolidating WebView2 setup into one script, fixing several workflow bugs, and aligning **Restore** with **Pack** when `UseArtifactsOutput=true`.

| Area | Fix |
|------|-----|
| WebView2 | Shared `Scripts/CI/Populate-WebView2.ps1`; prerelease-only for net11 |
| Nightly | PowerShell `Test-Path` / `-and` parse error |
| Canary | Broken inline WebView2 step replaced |
| Build | Duplicated WebView2 logic removed; always `-Prerelease` on alpha/canary |
| Rollout | Overlay copies script when missing on `alpha` / `Canary` checkout |
| Pack Release | **NETSDK1005** — restore uses same `UseArtifactsOutput` layout as Pack |

## Problems addressed

### 1. Nightly: `Test-Path` / `-and` syntax

```text
A parameter cannot be found that matches parameter name 'and'.
```

`if (Test-Path -LiteralPath $tempDir -and -not (...))` binds `-and` to `Test-Path`. Fix: `((Test-Path ...) -and -not (...))`.

### 2. Canary: corrupted WebView2 step

Missing `$packageId`, directory setup, and valid script structure — canary could not populate `Lib\WebView2`.

### 3. Build: duplicated logic and stable WebView2 on PRs

~45 lines duplicated in two jobs; prerelease was inferred only from `GITHUB_REF` on push. PRs to alpha/canary (`refs/pull/...`) could still resolve **stable** WebView2 while building **net11.0-windows** (stable WebView2 does not support .NET 11).

### 4. Script not found on the runner

Nightly checks out **`alpha`**; Canary checks out **`Canary`**. Updated workflow YAML on a PR branch can call `Populate-WebView2.ps1` before that file exists on the build branch:

```text
The term '...\Scripts\CI\Populate-WebView2.ps1' is not recognized...
```

### 5. Build release job: NETSDK1005 on Pack Release

```text
error NETSDK1005: Assets file '...\artifacts\obj\Krypton.Workspace\project.assets.json' does not exist.
```

**Restore** used `dotnet restore` without `UseArtifactsOutput=true`, while **Build** / **Pack** use `msbuild build.proj` with `UseArtifactsOutput=true`. Assets were restored under the legacy `obj` layout; Pack expected `artifacts/obj`.

## Solution

### `Scripts/CI/Populate-WebView2.ps1`

| Switch | Purpose |
|--------|---------|
| `-Prerelease` | NuGet prerelease lookup (required for net11) |
| `-ResolveVersionOnly` | Resolve version only |
| `-WriteGitHubOutputVersion` | Write `version=` to `GITHUB_OUTPUT` |
| `-Version` | Use version from a prior workflow step |

- NuGet search API + flatcontainer fallback  
- Download/extract under `RUNNER_TEMP/webview2` (cache-friendly)  
- Copies Core, WinForms, and WebView2Loader DLLs into `Krypton.Toolkit.Utilities\Lib\WebView2`

### WebView2 workflow pattern (nightly, canary, build)

1. Resolve WebView2 version (`-Prerelease -ResolveVersionOnly -WriteGitHubOutputVersion`)  
2. Restore WebView2 cache  
3. Populate WebView2 (Preview) (`-Prerelease -Version $env:WEBVIEW2_VERSION`)  
4. Save WebView2 cache  

### Nightly / Canary overlay (rollout)

After checkout of `alpha` / `Canary`:

1. Sparse-checkout `Populate-WebView2.ps1` from `github.sha` into `_ci-overlay`  
2. Copy into the workspace if missing on the build branch  

**Still merge the script into `alpha` and `Canary`** so production does not rely on the overlay long term.

### Build `release` job restore (NETSDK1005)

Align with nightly/canary — MSBuild restore through `build.proj` with matching properties:

```yaml
- name: Restore
  run: msbuild /m "Scripts/Build/build.proj" /t:Restore /p:Configuration=Release /p:Platform="Any CPU" /p:UseArtifactsOutput=true /p:TFMs=all
```

**Build** job `dotnet restore` also passes `-p:UseArtifactsOutput=true` so it matches `nightly.proj` Rebuild.

## Files changed

| File | Change |
|------|--------|
| `Scripts/CI/Populate-WebView2.ps1` | **New** shared CI script |
| `.github/workflows/nightly.yml` | Script, cache, overlay, `Test-Path` fix |
| `.github/workflows/canary.yml` | Fix step, script, cache, overlay |
| `.github/workflows/build.yml` | Script, cache, prerelease, restore alignment |

> Exclude unrelated changes from this PR unless intentional.

## Test plan

### WebView2

- [ ] **Build** workflow on alpha/canary (or PR targeting alpha): WebView2 resolve/populate succeed; log shows **prerelease** version (e.g. `-prerelease` suffix).
- [ ] **Nightly Release**: Resolve / Populate succeed when commits exist in last 24h.
- [ ] **Canary Release**: WebView2 steps and cache save succeed.
- [ ] Three DLLs present under `Krypton.Toolkit.Utilities/Lib/WebView2` before MSBuild restore.
- [ ] After merge to `alpha`, overlay logs *“already present on checked-out branch”* (optional).
- [ ] Local: `pwsh -File Scripts/CI/Populate-WebView2.ps1 -Prerelease -ResolveVersionOnly`

### Pack / restore

- [ ] **Build** workflow **release** job (master push or dispatch): Restore → Build Release → **Pack Release** complete without NETSDK1005.
- [ ] Confirm `artifacts/obj/*/project.assets.json` exists after Restore when `UseArtifactsOutput=true`.

## Breaking changes

None for NuGet consumers. CI-only.

## Follow-up

- Merge `Populate-WebView2.ps1` into **`alpha`** and **`Canary`**, not only the PR target branch.
- Keep **`-Prerelease`** until stable `Microsoft.Web.WebView2` supports .NET 11.
