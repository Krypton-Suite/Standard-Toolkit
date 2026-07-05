# Cross-project source linking (`Krypton.Interop`)

Closes [#3855](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3855)

## Summary

Replaces ad hoc MSBuild compile links for shared Win32/P/Invoke with a dedicated internal **`Krypton.Interop`** assembly, centralizes remaining compile-link definitions in **`Krypton.Shared`**, bundles `Krypton.Interop.dll` into module NuGet packages, and adds CI verification that packed `.nupkg` files contain the interop DLL.

## Motivation

Several source files under `Krypton.Toolkit` were compiled into multiple assemblies via `<Compile Include="..\..." Link="...">` (notably `PlatformInvoke.cs`). That pattern:

- Duplicated a large interop surface in every consumer assembly
- Was undocumented and easy to extend inconsistently
- Made packaging and maintenance harder as Utilities and sibling modules grew

Issue [#3855](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3855) requested documenting the approach and extracting shared interop when linking spread.

## What changed

### New `Krypton.Interop` project (internal, not packable standalone)

Shared sources moved from `Krypton.Toolkit` into `Source/Krypton Components/Krypton.Interop/`:

| File | Notes |
|------|--------|
| `General/PlatformInvoke.cs` | Internal `PI`, `Libraries`, Win32 surface |
| `General/HResult.cs` | `partial class PI` (must live with PlatformInvoke) |
| `General/Scroll Bars/WIN32ScrollBars.cs` | Public scroll-bar structs |
| `Utilities/AllowNullAttribute.cs` | net472 nullable polyfills |
| `Global/GlobalDeclarations.cs` | `[InternalsVisibleTo]` for Toolkit, Ribbon, Navigator, Docking, Workspace, both Utilities assemblies |

**Wiring:** only `Krypton.Toolkit` adds a direct `ProjectReference` to `Krypton.Interop`. Other modules consume interop transitively through Toolkit.

### `Krypton.Shared` MSBuild infrastructure

| File | Purpose |
|------|---------|
| `Krypton.SharedCompileItems.props` | Opt-in compile links for files that must still ship per-assembly |
| `Krypton.Interop.Package.targets` | Bundles `Krypton.Interop.dll` (and `.xml`/`.pdb`) into packable module nuspecs |

**Remaining compile links** (by design — suppressions apply per assembly; Utilities helpers still live under Toolkit on disk):

- `GlobalSuppressions.cs` → Ribbon, Navigator, Docking (`IncludeKryptonGlobalSuppressionsLink=true`)
- `FileSystemIconHelper.cs`, `GeneralToolkitUtilities.cs` → Toolkit.Utilities (`IncludeKryptonUtilitiesSourceLinks=true`)

### Consumer `.csproj` updates

Removed per-project `<Compile Include="..\Krypton.Toolkit\...">` links for interop/polyfill files from Ribbon, Navigator, Docking, Workspace, Toolkit.Utilities, and Navigator.Utilities.

### Build orchestration

`Scripts/Build/Krypton.Orchestration.targets`:

- Builds **`Krypton.Interop` before `Krypton.Toolkit`**
- Uses **`Restore;Build`** for Interop (avoids `NETSDK1004` when `obj` assets are cleared)

### NuGet packaging

- **`Krypton.Standard.Toolkit`**: explicit `Krypton.Interop.dll` / `.xml` / `.pdb` entries in `AddReferencedAssembliesToPackage`
- **Individual module packages** (`Krypton.Toolkit`, `Krypton.Ribbon`, etc.): `Krypton.Interop.Package.targets` imported from `Directory.Build.targets`

`Krypton.Interop.dll` is **bundled** into `lib/<tfm>/` — it is **not** published as a separate NuGet package or listed as a dependency.

### CI

New script: `Scripts/CI/Test-KryptonInteropInPackages.ps1`

After each workflow **Pack** step, verifies packable module `.nupkg` files contain `lib/*/Krypton.Interop.dll`. Uses `-SkipIfInteropProjectMissing` so LTS branches without the Interop project skip gracefully.

Updated workflows: `build.yml`, `canary.yml`, `nightly.yml`, `release.yml`, `canary-lts-release.yml`.

### Documentation

- `Documents/Changelog/Changelog.md` — V110 entry for [#3855](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3855)
- `AGENTS.md` — architecture note for `Krypton.Interop`
- Header comments on moved/linked source files pointing to the guide

### Solution

- Added `Krypton.Interop` to `Krypton Toolkit Suite 2022 - VS2022.sln` / `.slnx`

## Runtime impact for consumers

Applications referencing any Krypton module that uses internal interop now require **`Krypton.Interop.dll`** alongside the module DLL(s).

- **`Krypton.Standard.Toolkit`** aggregate package: included automatically
- **Individual module packages**: included in each packable module `.nupkg` under `lib/<tfm>/`

No public API surface was added; `PI` remains internal.

## Breaking change?

**Soft breaking change for manual/binary deployments:** if an app copies only `Krypton.Toolkit.dll` (or another module DLL) without transitive references, it must also deploy `Krypton.Interop.dll`. NuGet package consumers receive the DLL via bundled `lib` folders.

No public type or namespace changes for consumer code.

## Test plan

- [ ] `dotnet restore "Source/Krypton Components/Krypton Toolkit Suite 2022 - VS2022.sln" -p:Configuration=Debug`
- [ ] `dotnet build "Source/Krypton Components/Krypton Toolkit Suite 2022 - VS2022.sln" -c Debug`
- [ ] Confirm `Bin/Debug/<tfm>/Krypton.Interop.dll` is produced for each built TFM
- [ ] Pack one module (e.g. Release + `TFMs=lite`) and inspect `.nupkg` contains `lib/<tfm>/Krypton.Interop.dll`
- [ ] Run `Scripts/CI/Test-KryptonInteropInPackages.ps1` against packed output
- [ ] Smoke TestForm scenarios using P/Invoke-heavy paths (message box, file icons, scroll bars, forms)
- [ ] Verify CI Pack + **Verify Krypton.Interop in NuGet packages** steps pass on PR

## Post-merge notes for maintainers

1. **First build after pull:** run solution restore once if Visual Studio reports `NETSDK1004` for `Krypton.Interop`.
2. **New shared interop:** add to `Krypton.Interop`, extend `[InternalsVisibleTo]` if a new consumer assembly is introduced.
3. **Do not add new `partial class PI` files under `Krypton.Toolkit`** — they belong in `Krypton.Interop`.
4. **Per-assembly analyzer suppressions:** keep using compile links via `Krypton.SharedCompileItems.props`, not Interop.
