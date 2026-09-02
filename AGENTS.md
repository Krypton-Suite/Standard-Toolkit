# Repository Guidelines

## Recent Tooling Mistakes To Avoid

These are recurring issues observed when using AI coding agents and shell wrappers. Follow these guidelines even if the commands appear syntactically correct.

- Do not combine `cmd.exe` variable assignment and use in the same command line. `%VAR%` is expanded before `set` takes effect, which created a stash named `"%STASH_MSG%"`. Correct example: `git stash push -m "3493-followup" -- .`
- Do not pass complex PowerShell through `cmd.exe` with unescaped `$variables`; `cmd.exe` can strip or alter the command before PowerShell sees it. Correct example: run PowerShell directly with `$path = Join-Path (Get-Location) 'AGENTS.md'; Get-Content -LiteralPath $path -Raw`.
- Do not build long `git commit -m` commands when the body contains tokens such as `--check`; argument parsing can treat body text as options. Correct example: write the message to a temp file and run `git commit -F <message-file>`.
- Do not rely on shell quotes for `gh` arguments with spaces when the wrapper has already mishandled them. Correct example: use a JSON input file with `gh api ... --input <json-file>` or a PowerShell argument array.
- Do not try to rename an existing stash with `git stash store -m`; stash display names may still come from the original stash commit. Correct example: re-apply the stash, then create a fresh `git stash push -m "3493-followup" -- .` if the label matters.
- Do not over-escape regex patterns for `rg`. A pattern like `msbuild\\.exe` can search for the wrong text. Correct example in PowerShell: `$pattern = 'msbuild\.exe'; $root = 'Scripts'; rg -n $pattern $root --glob '*.cmd'`.
- Do not use `findstr` quoted path experiments for ordinary file reads or searches. Correct example: `$path = 'Scripts\VS2022\rebuild-build-nightly.cmd'; Select-String -LiteralPath $path -Pattern 'nightly.proj'`.
- Do not `git clone` [Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos) when `..\Standard-Toolkit-Demos` already exists. Reuse that working tree: switch to `alpha` if not already on it, then create a new `alpha-…` branch from `alpha`. Clone only when the parent folder is missing (see **Standard-Toolkit-Demos**).

## Always

Before considering a task complete:

- Build the affected project if instructed.
- Fix any compiler or analyzer warnings introduced by the change; treat new warnings as part of the build (do not leave them for later). Prefer fixing pre-existing warnings in files you already touch when the fix is small and local; do not expand into a repo-wide warning cleanup unless asked.
- Check files you create or edit for UTF-8 BOM encoding issues and fix them (see **Coding Style & Naming Conventions**). Do not leave UTF-8-without-BOM or wrong-encoding files when the repo expects UTF-8 with BOM; do not expand into a repo-wide encoding cleanup unless asked.
- Update TestForm when adding a feature.
- When adding a feature, also add or append a comprehensive consumer demo in [Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos) (see **Standard-Toolkit-Demos**). Reuse `..\Standard-Toolkit-Demos` if it already exists (do **not** clone again); clone into the parent only if that folder is missing. Then switch to `alpha` (if not already on it) and create a new `alpha-…` branch from `alpha`. It is not part of this repository. If an example already exists, do not overwrite it; append.
- Update Changelog.md for completed features and bug fixes.
- When a change is **breaking** for consumers, also update `README.md` under **Breaking Changes** (see **Breaking Changes (README)**). The entry must follow the existing pattern in that section.
- Add developer documentation for substantial new features (see **Feature Developer Documentation**). Keep `Documents/Development/` files **out of pull requests**.
- Write a PR description in `Documents/PR/` for completed features and bug fixes, and use that file as the GitHub PR body. Do **not** include the PR description file in the pull request (see **Pull Request Descriptions**).
- For UI-visible changes, capture screenshots (or a short GIF when motion is the point) into the local `Documents/PR/` description. Do not leave **Screenshots / GIFs** as a placeholder, and do **not** upload or attach the images to the GitHub pull request (see **UI Screenshots / GIFs**).
- When UI behaviour is verified with ad-hoc PowerShell / UI Automation (mouse synthesise, screenshots, hosted `TestForm` demos), **keep those scripts under `Scripts/UnitTests/`** instead of leaving them only under `Bin/` or deleting them after the session. Prefer reusable, named scripts with a short note in `Scripts/UnitTests/README.md` (see **Unit Test Scripts**).

## Shell Guidelines

- Prefer PowerShell for shell commands.
- Use cmd.exe only when reproducing Windows batch behavior.
- Use PowerShell cmdlets instead of findstr where possible.
- Avoid relying on cmd.exe variable expansion for complex commands.
- For complex Git operations, prefer temporary files or PowerShell arrays over long quoted command lines.

## Environment

- OS: Windows
- Tools: Visual Studio 2022 (v17) and appropriate .NET SDKs starting with `net472`
- Build scripts are Windows `.cmd` files under `Scripts/`; do not run them unless explicitly instructed (see **Build, Test, and Development Commands**)

## Project Structure & Module Organization

- `Source/Krypton Components`: Core libraries (`Krypton.Toolkit`, `Krypton.Themes`, `Krypton.Ribbon`, `Krypton.Navigator`, `Krypton.Workspace`, `Krypton.Docking`) and the solution `Krypton Toolkit Suite 2022 - VS2022.sln`
- `Source/Krypton Components/TestForm`: WinForms sample app used to validate changes; add or extend demos here when features or bugs are completed (see **TestForm Demos**). Library folders omit `.` from assembly names (`KryptonToolkit`, not `Krypton.Toolkit`); feature demos sit under that folder’s `Feature` subfolder and issue repros under `Bugs`.
- [Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos) is a **separate** repo in the directory above this project (`..\Standard-Toolkit-Demos`), not a folder inside Standard-Toolkit. Reuse that folder if it exists (do **not** clone again); clone there only if missing. When completing a feature, add a consumer example (or **append** if one exists; do not overwrite) on a new `alpha-…` branch from `alpha` (see **Standard-Toolkit-Demos**)
- `Source/TestHarnesses`: Small repro/test harnesses (e.g., `ThemeSwapRepro`)
- `Scripts/`: Build and packaging scripts; `run.cmd` (root) launches an interactive menu; scripts live under `Scripts/VS2022/`, `Scripts/Current/`, `Scripts/Build/` (e.g., `build-stable.cmd`, `build-canary.cmd`, `build-nightly.cmd`, `build-rc.cmd`, `build.proj`)
- `Scripts/UnitTests/`: Reusable PowerShell UI-automation helpers for interactive validation of `TestForm` scenarios (see **Unit Test Scripts**)
- `Bin/`: Build outputs by configuration (e.g., `Bin/Debug`)
- `Documents/`, `Assets/`, `Logs/`: Docs, images, and build logs
- `README.md`: Consumer-facing project overview; **Breaking Changes** lists migration notes for each major version (see **Breaking Changes (README)**)
- `Documents/Changelog/Changelog.md`: User-facing release notes for completed bugs and features
- `Documents/Development/`: In-depth developer guides for completed features (APIs, architecture, usage); not listed in `Documents/Changelog/Changelog.md` or `Scripts/ModernBuild/README.md`; **do not include these files in new or existing PRs**
- `Documents/PR/`: One Markdown PR description per completed bug fix or feature, drafted locally and used as the GitHub PR body; **do not include that description file in new or existing PRs** (see **Pull Request Descriptions**)

## Architecture

- `Krypton.Toolkit` contains the shared infrastructure.
- `Krypton.Interop` holds shared internal Win32/P/Invoke and net472 nullable polyfills; referenced by `Krypton.Toolkit` and consumed transitively by sibling assemblies.
- `Krypton.Themes` holds **extra** builtin palettes (optional assembly, auto-discovered). Toolkit must **not** project-reference Themes (cycle).
- `Krypton.Ribbon` depends on `Krypton.Toolkit`.
- `Krypton.Navigator` depends on `Krypton.Toolkit`.
- Rendering flows through the palette and renderer abstractions.
- New controls should integrate with the palette system rather than hardcoding appearance.

## Built-in Palettes (Theme Catalog)

When adding a new **builtin** palette (not a custom XML/`KryptonCustomPaletteBase` theme), follow this checklist. Prefer **extra** placement in `Krypton.Themes` unless the user explicitly asks for a **core** Toolkit palette. Full walkthrough: `Documents/Development/KryptonThemesCatalog.md`.

### Placement

| Kind | Assembly | Register in | `KryptonManager.Palette*` return type |
|------|----------|-------------|----------------------------------------|
| **Extra** (default) | `Krypton.Themes` | `KryptonExtendedThemeProvider` | `PaletteBase` via `GetPaletteForMode` |
| **Core** (explicit only) | `Krypton.Toolkit` | `KryptonCoreThemeProvider` | Concrete typed property + lazy field |

**Core today (do not expand casually):** Professional System / Office 2003; Office 2007 / 2010 / Microsoft 365 **Blue, Silver, Black**; Sparkle **Blue, Orange, Purple**.

Concrete palette types always use namespace `Krypton.Toolkit`, even when the file lives in `Krypton.Themes`.

### Required steps (every new builtin)

1. **`PaletteMode`** — Add the enum member in `Palette Base/PaletteMode.cs`. Keep **the same order** as `PaletteModeStrings.SupportedThemes`. **`Custom` must remain last.**
2. **`PaletteModeStrings`** — Add display-name constant, property, `SupportedThemes` dictionary entry, and `Reset` / equality helpers so enum ↔ string stay in sync.
3. **Palette class** — Implement `PaletteXxx` reusing an existing base/renderer (Office, Sparkle, Material, Visual Studio, …). Match surrounding file layout under `Palette Builtin\…`. New files: current Standard Toolkit BSD header only; UTF-8 with BOM; CRLF.
4. **Catalog registration**
   - Extra: add `Extra(PaletteMode.…, KryptonThemeFamilies.…, KryptonThemeChromeKind.…, typeof(PaletteXxx), () => new PaletteXxx())` in `Krypton.Themes\KryptonExtendedThemeProvider.cs`. Pass `KryptonThemeShieldIconStyle` only when it is not `KryptonThemeChrome.DefaultShieldIconStyle(chrome)`. Add a `KryptonThemeFamilies` constant when introducing a new family key.
   - Core: add `Core(…)` in `KryptonCoreThemeProvider` with family and chrome kind. `KryptonThemeCatalog.CorePaletteCount` reflects registered core descriptors (expected 14 today).
5. **`KryptonManager` accessor**
   - Extra: do **not** add a `Palette*` singleton unless a consumer still needs a named property. Use `GetPaletteForMode`. Existing extra accessors stay `PaletteBase`.
   - Core: typed property with a private static lazy field (same pattern as `PaletteSparkleBlue`).
   - Do **not** add `PaletteMode` arms to toolbar or shield switches; those read `KryptonThemeChromeKind` / `KryptonThemeShieldIconStyle` from the descriptor.
6. **Converters** — `PaletteModeConverter` picks up `SupportedThemes` automatically. For **core** types only, also map the type in `PaletteClassTypeConverter`’s core dictionary. Extras resolve via `KryptonThemeCatalog.TryGetMode`.
7. **Resources** — Add/update palette schema or image resources only when the palette needs them (follow neighbouring Official/Extra themes).
8. **Validation** — Exercise via theme combo / `ThemeCatalogDemo` (TestForm). After Themes is loaded, `KryptonThemeCatalog.GetUnimplementedBuiltinModes()` must not include the new mode. Prefer extending `Scripts/UnitTests/UnitTest-ThemeCatalog.ps1` when the change is structural.
9. **Docs / release** — Changelog entry; update `Documents/Development/KryptonThemesCatalog.md` if architecture or placement rules change; PR description under `Documents/PR/`.

### Do not

- Add a Toolkit → Themes project reference or type-forward extras from Toolkit.
- Leave `PaletteMode` / `SupportedThemes` order mismatched, or insert values after `Custom`.
- Put new optional themes in Toolkit “for convenience” (keeps the core package large).
- Forget family keys used by `KryptonThemeAvailability.SetFamilyEnabled` (use `extraOnly: true` when hiding Sparkle extras must not hide core Sparkle Blue/Orange/Purple).
- Assume a missing extra palette throws at runtime — it falls back to Microsoft 365 Blue (`KryptonThemeCatalog.MissingThemeFallback`).
- Infer family or chrome from the enum name in `KryptonExtendedThemeProvider` — pass them explicitly.
- Add new `PaletteMode` values after `Custom`, or invent a second theme-id system without a dedicated design (string ids on `Custom` are a future feature, not a shortcut).

### Third-party / sample providers

Extra assemblies can advertise `[assembly: KryptonThemeProvider(typeof(…))]`. They cannot invent new `PaletteMode` values without a Toolkit change; they may only implement modes not already registered. Pass family and `KryptonThemeChromeKind` on the descriptor (the five-argument constructor guesses both). Sample: `Source/TestHarnesses/ThemeProviderSample`.

## Editing Philosophy

- Make the smallest change that correctly solves the task.
- Keep code clean, simple, and maintainable.
- Preserve existing formatting and coding style.
- Do not refactor unrelated code.
- Do not rename identifiers unless requested.
- When adding or changing public/protected API, include scoped documentation per **Code Documentation Guidelines**; do not turn a feature or bug fix into a repo-wide documentation pass unless asked.
- Keep accompanying artefacts (changelog, developer guide, PR description, TestForm demo, UI screenshots / GIFs) consistent with the implementation; do not leave placeholder text from templates.

## Public API

### Compatibility

- New code must remain compatible with `net472`.
- Do not use language features newer than C# 7.3 unless the project already conditionally supports them.

### Stability

- Preserve binary compatibility unless explicitly instructed otherwise.
- Avoid changing public or protected member signatures unless explicitly requested.
- Do not rename public types or namespaces.
- Preserve designer serialization compatibility.

## Performance

- Avoid unnecessary allocations in paint paths.
- Avoid creating disposable GDI objects inside tight rendering loops.
- Reuse existing rendering infrastructure whenever possible.

## Build, Test, and Development Commands

- Script/CI builds use phased orchestration (`Scripts/Build/Krypton.Orchestration.targets`) with `msbuild /m` for parallel TFMs; do not build all `Krypton.*` projects in one parallel batch (shared `Bin/<Configuration>/<tfm>/` outputs).
- Build solution (Debug):
  - `dotnet build ".\Source\Krypton Components\Krypton Toolkit Suite 2022 - VS2022.sln" -c Debug`
- Run sample app:
  - `dotnet run --project ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug`
- Build script entry points, only when explicitly instructed:
  - `.\run.cmd` launches the interactive menu and lets you choose `Scripts\VS2022` or `Scripts\Current` (VS 2026).
  - Direct VS2022 presets: `.\Scripts\VS2022\build-stable.cmd`, `.\Scripts\VS2022\build-canary.cmd`, `.\Scripts\VS2022\build-nightly.cmd`, `.\Scripts\VS2022\build-rc.cmd`.
  - Direct VS2026 presets: `.\Scripts\Current\build-stable.cmd`, `.\Scripts\Current\build-canary.cmd`, `.\Scripts\Current\build-nightly.cmd`, `.\Scripts\Current\build-rc.cmd`.
  - Build scripts locate MSBuild via `Scripts\Common\find-msbuild.cmd` (`vswhere.exe`, then standard install paths). Profiles: `2019`, `2022`, `current` (newest VS major 18+), or a pinned major (`18`, `19`, …). `Scripts\Current\` uses `current`. Override with `MSBUILDPATH` or `MSBUILD_PATH` pointing at `MSBuild\Current\Bin`.
- Outputs land under `Bin\<Configuration>\<TargetFramework>\` by default; with `UseArtifactsOutput=true`, outputs land under `artifacts\bin\<Configuration>\<TargetFramework>\`.
- Target frameworks are selected by MSBuild properties. VS2019/full MSBuild builds only .NET Framework 4.x TFMs; VS2022/full MSBuild excludes `net10.0-windows` and `net11.0-windows`; VS2026/full MSBuild excludes `net11.0-windows` unless explicitly enabled; CI or SDK-based builds can include `net472`, `net48`, `net481`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`, and `net11.0-windows` when the required SDKs are installed.
- New files must use only the current Standard Toolkit BSD header. Do not add the original ComponentFactory BSD header unless the file is derived from original ComponentFactory source.

## Coding Style & Naming Conventions

- Line endings/encoding: CRLF, UTF-8 with BOM
- Always verify and fix UTF-8 BOM on files you create or edit. Source and text files in this repo use UTF-8 **with** BOM; if a tool or edit strips the BOM (or writes UTF-8 without BOM), restore it before finishing. Prefer fixing encoding on files already in scope; do not expand into a repo-wide BOM pass unless asked. In PowerShell, rewrite with BOM when needed, e.g. `$utf8Bom = New-Object System.Text.UTF8Encoding $true; [System.IO.File]::WriteAllText($path, $content, $utf8Bom)`.
- Follow `Source/.editorconfig` and project analyzers (`EnableNETAnalyzers=true`)
- Indentation: 4 spaces; line endings: CRLF
- Projects use `global using` like in GlobalDeclarations.cs, do not add new usings in other files
- Before adding new variables check for existing ones
- No variable aliasing
- New files must use only the current Standard Toolkit BSD header. Do not add the original ComponentFactory BSD header unless the file is derived from original ComponentFactory source.

## C# Rules

- Preserve the existing nullable reference type annotations and context (`<Nullable>enable</Nullable>` is set at project level).
- Do not enable or disable nullable in individual files unless requested.
- No unneeded `try/catch` blocks if there's no catch handling
- Idioms: use null-propagation and object/collection initializers where consistent
- Prefer [switch expressions](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) for simple value/type dispatch that only returns (or assigns) a value. Keep `switch` statements for complex control flow, multiple statements per arm, or side effects. Prefer a discard arm (`_ => ...`) when exhaustiveness matters. Use only pattern forms already common in this codebase (constant, type, discard, simple property/`when` guards as elsewhere); do not introduce newer pattern syntax that would fight **Public API → Compatibility**. Apply to new and changed code; do not mass-convert unrelated existing code unless asked.
- Prefer the conditional (ternary) operator (`condition ? whenTrue : whenFalse`) for simple value selection in place of an `if`/`else` that only assigns or returns. Keep `if`/`else` when either branch has multiple statements, side effects beyond the assigned value, or when nesting ternaries would hurt readability (prefer a local, `if`/`else`, or a [switch expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) instead of deep nesting). Apply to new and changed code; do not mass-convert unrelated existing code unless asked.
- Prefer [expression-bodied members](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members) (`member => expression;`) when the body is a single expression — methods, properties/`get`/`set`, constructors, finalizers, and indexers. Stay within the C# 7.3 ceiling in **Public API → Compatibility** (do not pull in newer syntax from examples such as primary constructors, `required`, or collection expressions). Keep a block body when the member needs multiple statements, local variables, early returns with nontrivial branching, or when expression form would hurt readability. Apply this to new and changed members; do not mass-convert unrelated existing code unless asked.
- WinForms: `UseWindowsForms=true`; prefer designer-friendly patterns and keep partial classes tidy
- New WinForms forms, controls, and components should follow the standard Visual Studio partial-class pattern with separate `.cs`, `.Designer.cs`, and `.resx` files where appropriate. Prefer designer-backed types over single-file implementations unless explicitly requested otherwise.
- Do not place designer-generated initialization code in the main source file. Keep UI initialization in `InitializeComponent()` within the corresponding `.Designer.cs` file.
- WinForms designer: keep object declarations at file bottom; initialize in `*.Designer.cs` `InitializeComponent()`
- Do not manually edit generated `*.Designer.cs` files unless the task specifically requires it.
- Constraint: do not use `yield return` inside `catch` blocks

## Code Documentation Guidelines

Prefer **scoped meticulous documentation**: thorough XML and maintainer notes on the public/protected surface and on non-obvious implementation, without narrating boilerplate or rewriting unrelated files.

When asked to review or document code — or when adding/changing public API — document to this standard for the types and members in scope. Do not expand into large blocks of unchanged legacy code unrelated to the task (see **Editing Philosophy**).

### What to document

- **Public and protected API** — full `///` XML on types and members you add or change: `<summary>`, and `<param>` / `<returns>` / `<exception>` / `<remarks>` when they add real information (behavior, constraints, nullability contracts, thread affinity, designer impact). Prefer `<see cref="..."/>` and `<c>...</c>` for related types and values.
- **Class-level summaries** for every non-trivial type in scope, especially those in a larger model (composite trees, state machines, store/restore flows, drag hosts). Name sibling types and the role of the class in the hierarchy. Thin subclasses and adapters may use a one-line summary that points at the base or owning type.
- **Inline comments** at decision points for:
  - Multi-step algorithms (store-then-restore, orphan handling, greedy layout shrink)
  - Propagation (`PropogateAction`, `StartUpdate`/`EndUpdate`, reverse child iteration)
  - State machines and message-filter / focus edge cases
  - Drag-drop choreography (hidden float window reuse, target priority, placeholder pages)
  - XML persistence quirks (element order, attribute meaning, misnamed APIs, buffer length)
  - Geometry or ordering that is not obvious from property names (z-order, hot vs draw rects, remainder path parsing)
- **Brief region comments** above enum groups that act as a catalog for a subsystem (e.g. propagation actions).
- **Internal / private helpers** — document with `///` or a short `//` only when the name alone does not convey contracts, ordering requirements, or side effects.

### What not to document

- Obvious boilerplate (`// This constructor creates an instance of X`, `// Return the result`, restating parameter names or type names).
- Members whose existing XML already accurately describes intent; extend or correct rather than rewrite wholesale (see **Comment Style** and **Documentation Stability**).
- **Event Args**, **Resources**, **Designer** / **`.Designer.cs`**, and other thin property-bag or generated files unless logic is non-trivial (then document only that logic).
- Large blocks of unchanged legacy code unrelated to the task — do not “document the world” in a feature or bug PR unless the user explicitly requests a documentation pass.

### Comment Style

- Use `///` XML documentation for public and protected types and members.
- Use `//` comments for implementation notes, algorithms, and non-obvious decisions.
- Do not use C-style block comments (`/* ... */`) or banner comments (`/** ... */` / `/*** ... ***/`) for documentation unless matching existing surrounding code.
- Keep comments close to the code they describe.
- Prefer several short `//` comments over large comment blocks.
- Comments should explain *why* code exists or *why* an approach was chosen, not simply restate what the code does.
- Keep comments **clear and concise** — one or two sentences for inline notes; XML may be slightly longer when describing contracts or edge cases. Prefer plain language over jargon.
- Match surrounding voice (this codebase often uses short `//` notes inside `switch` arms and multi-step flows).
- For preservation, idempotence, and when to stop editing, follow **Documentation Stability**.

Prefer:

```csharp
// Restore orphaned pages before rebuilding the hierarchy.
// This ensures page references remain valid during layout reconstruction.
```

Avoid:

```csharp
/******************************************************************************
 * This method walks the docking tree and restores orphaned pages.
 ******************************************************************************/
```

And avoid restating the obvious (`// Increment the index.` before `index++;`). Prefer intent (`// Iterate in reverse because removing children invalidates forward indices.`).

### Documentation Stability

Documentation should be **deterministic** and converge toward a stable, maintainable state. Identical wording across every agent is not guaranteed; the objective is **substantively equivalent**, convergent documentation — stability over novelty.

For equivalent code, repeated documentation passes should produce the same or substantively equivalent documentation. Documentation passes should:

- Preserve accurate existing comments and XML documentation.
- Correct inaccurate or obsolete documentation.
- Improve incomplete documentation.
- Never reduce the accuracy or usefulness of existing documentation.
- Avoid stylistic rewrites when the existing documentation already satisfies these guidelines.

Documentation updates should be **idempotent**: running another documentation pass over already-compliant code should result in little or no change.

Do not rewrite documentation solely to change wording, sentence structure, or writing style. Only modify documentation when doing one or more of the following:

- Correcting inaccuracies
- Improving clarity where the existing text is unclear or ambiguous
- Documenting new behavior
- Removing obsolete information
- Completing missing contracts (`<param>`, `<returns>`, `<exception>`, nullability, threading, designer impact, and similar)

Preserve existing comments and XML documentation whenever they remain accurate and useful. Extend, clarify, or correct them surgically rather than replacing them wholesale. Remove or rewrite comments only when they are inaccurate, misleading, obsolete, or substantially incomplete — never rewrite solely for style. Historical and architectural notes in this codebase are often valuable; do not erase them casually.

When updating comments or XML documentation, ensure they remain consistent with the implementation after every change. Documentation is part of the code, not an afterthought. Remove or correct comments that are inaccurate, outdated, or misleading; do not leave documentation that contradicts the implementation.

Prefer improving existing documentation over inventing entirely new wording for the same facts.

Each documentation pass should converge toward a stable result. Once documentation satisfies these guidelines, future passes should make few or no changes unless the code changes.

Documentation is considered **complete** when it:

- Accurately describes current behavior
- Explains non-obvious design decisions
- Matches the implementation
- Follows repository conventions in this file
- Contains no redundant or contradictory information

Stop editing documentation that already meets this bar.

### Prioritization (large modules)

For substantial packages (e.g. `Krypton.Docking`) or an explicit documentation pass, work in this order:

1. Root orchestrator and base abstractions (manager, element base, definitions/enums).
2. Core implementation layers (space/edge/group elements, primary controls).
3. Specialized flows (auto-hidden slide, drag targets, persistence load/save).
4. Thin subclasses and adapters last — often a one-line class summary is enough.

Validate documentation-only changes with a targeted `dotnet build` of the affected project when practical.

## Feature Developer Documentation

When a **new feature** is completed (not bug fixes or refactors unless they introduce a substantial new capability), add a **comprehensive developer guide** as a Markdown file under `Documents/Development/`.

### When to write

- New public APIs, components, designer support, build/packaging features, or user-facing subsystems.
- Skip for trivial fixes, comment-only changes, and internal refactors with no new surface area.

### What to include

Each guide should be **in-depth** and **maintainer-focused**, covering as applicable:

- **Overview** — problem solved, scope, and which package(s) own the feature.
- **Architecture** — key types, relationships, and data/control flow (diagrams welcome).
- **Public API** — classes, interfaces, enums, events, and extension points with signatures and behavior.
- **Usage** — minimal code or designer steps; common integration patterns.
- **Configuration / persistence** — settings, XML, flags, or MSBuild properties if relevant.
- **Edge cases** — threading, TFM differences, breaking changes, migration notes.
- **Validation** — how to exercise the feature in `TestForm` or a harness (link to the demo form registered in `StartScreen`), and in [Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos) (reuse `..\Standard-Toolkit-Demos` if present; clone into the parent only if missing).

### TestForm demo

When the feature warrants user-visible validation, add or update a demo per **TestForm Demos** and reference it here. Also add a consumer example per **Standard-Toolkit-Demos** (reuse `..\Standard-Toolkit-Demos` if present; clone into the parent only if missing; **append** if an example already exists — do not overwrite).

### File conventions

- Location: `Documents/Development/`
- Name: descriptive kebab or Pascal-style title, e.g. `Krypton-Docking-Developer-Guide.md` or `Visual-Studio-Templates-Developer-Guide.md`.
- One feature (or cohesive subsystem) per file; cross-link related guides when helpful.
- CRLF, UTF-8 with BOM; match tone and structure of existing repo docs.
- These guides are **local working files**. Do not include them in new or existing pull requests (see **Do not include in pull requests** below).

### Do not list in these files

- **Do not** add changelog entries or release notes for these guides in `Documents/Changelog/Changelog.md`.
- **Do not** add references or index entries for these guides in `Scripts/ModernBuild/README.md`.

Changelog and ModernBuild README stay focused on user-facing release history and build tooling respectively. Developer guides are discovered via `Documents/Development/` and code cross-references only.

### Do not include in pull requests

Write developer guides under `Documents/Development/` as local working files. Do **not** stage, commit, or push them as part of a new or existing pull request. If an existing PR already contains `Documents/Development/` files, remove those paths from the PR so they are no longer in the diff.

## Changelog

When a **bug fix** or **feature** is completed, add an entry to `Documents/Changelog/Changelog.md` in the same change set (or immediately before merge).

### When to update

- **Resolved** — bug fixes, regressions, and defect corrections tied to an issue.
- **Implemented** — new features, enhancements, and new public capability.
- Skip changelog updates for comment-only work, internal refactors with no user-visible effect, and `Documents/Development/` guide files (those are separate from release notes).

### Where to add

- Append to the **current in-progress release** section at the top of the file (the first `##` heading after the table of contents), e.g. `## 2026-11-xx - Build 2611 (V110 Nightly) - November 2026`.
- Add new bullets **after** the section heading, before older entries in that section (newest first within the section).
- If no suitable section exists yet, follow the heading pattern used by adjacent releases and add a table-of-contents link.

### Entry format

Match existing style:

```markdown
* Resolved [#1234](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1234), Short user-facing summary of the fix.
* Implemented [#5678](https://github.com/Krypton-Suite/Standard-Toolkit/issues/5678), Short user-facing summary of the feature.
   * To use, you will need to download the [Krypton.Standard.Toolkit](https://www.nuget.org/packages/Krypton.Standard.Toolkit) NuGet package, as this control is part of the `Krypton.Toolkit.Utilities` assembly.
* Implemented [#9012](https://github.com/Krypton-Suite/Standard-Toolkit/issues/9012), **[Breaking Change]** Summary of what broke and what consumers must update.
```

- Prefix with `Resolved` or `Implemented` (same verbs as existing entries).
- Link the GitHub issue when one exists (`[#NNNN](https://github.com/Krypton-Suite/Standard-Toolkit/issues/NNNN)`).
- If the change is **breaking** for consumers (API removal/rename, behavior change requiring migration, assembly/namespace moves), insert `**[Breaking Change]**` immediately after the issue link comma and before the summary. Also add a matching entry to `README.md` under **Breaking Changes** in the same change set (see **Breaking Changes (README)**). Do not leave a `**[Breaking Change]**` changelog item without a README counterpart.
- If the feature lives in `Krypton.Toolkit.Utilities.csproj` or `Krypton.Navigator.Utilities.csproj`, append the indented NuGet sub-bullet shown in the example above (`To use, you will need to download the Krypton.Standard.Toolkit NuGet package…`). Use the matching assembly name (`Krypton.Toolkit.Utilities` or `Krypton.Navigator.Utilities`).
- One line per item; use indented sub-bullets only when extra user-facing detail is needed (see existing entries).
- Write for **consumers** of the toolkit (what changed and why it matters), not implementation detail—that belongs in `Documents/Development/` or code comments.

### Do not add to the changelog

- Entries for developer guides under `Documents/Development/`.
- References to `Scripts/ModernBuild/README.md` or build-script internals unless the change is user-facing.

## Breaking Changes (README)

When a **bug fix** or **feature** is **breaking** for consumers (API removal/rename, behavior change requiring migration, assembly/namespace moves, TFM or runtime support drop), add a matching entry to `README.md` under **Breaking Changes** in the same change set as the changelog entry. Do not leave a `**[Breaking Change]**` changelog item without a README counterpart.

### When to update

- Same trigger as the changelog `**[Breaking Change]**` marker (see **Changelog** above).
- Skip when the change is not breaking for consumers (comment-only work, internal refactors, additive APIs with no migration).

### Where to add

- File: `README.md`, section **Breaking Changes** (after **Version History**).
- Append to the **current in-progress version** heading (the first `## Vxxx.00 (…)` after `## Breaking Changes`), e.g. `## V110.00 (2026-11-xx - Build 2611 - November 2026)`.
- Add new bullets **after** that heading and its intro sentence (if present), before older entries in that version (newest first within the version).
- If that version heading does not exist yet, add it immediately after `## Breaking Changes`, using the same title pattern as adjacent version headings (`## Vxxx.00 (yyyy-MM-dd - Build nnnn - Month yyyy)`), add the intro sentence used by neighbouring versions (`There are list of changes that have occurred during the development of the Vxxx.00 version`), and add a table-of-contents link under `* [Breaking Changes](#breaking-changes)` (newest version first). Match the existing GitHub anchor style (`V110.00` → `#v11000-…`).
- Do **not** create a new version heading when the current in-progress release already has one. Do **not** append breaking items to a previously released version section.

### Entry format

Follow the existing `README.md` **Breaking Changes** pattern. Copy the consumer-facing changelog item (or the parent item when the break is a sub-bullet) and keep `**[Breaking Change]**`. Include indented sub-bullets for what consumers must update.

```markdown
* Implemented [#9012](https://github.com/Krypton-Suite/Standard-Toolkit/issues/9012), **[Breaking Change]** Summary of what broke and what consumers must update.
  * Migration detail (new type, namespace, property path, or package).
```

Match surrounding entries:

- Same `Resolved` / `Implemented` prefix and issue link as the changelog entry.
- `**[Breaking Change]**` immediately after the issue link comma (or on the sub-bullet when only part of the item is breaking — see existing V110 entries such as unused-utility removals and `ToggleSwitchValues` grouping).
- Indented sub-bullets for migration: namespace/assembly moves, renamed members, obsolete replacements, designer values to delete, NuGet package notes.
- Write for **consumers** (what broke and how to update), not implementation detail.
- Do not invent a new heading style, numbered lists, or a second breaking-change document. Do not drop `**[Breaking Change]**` from the README copy.

### Table of contents

When adding a **new** version heading, also add a TOC child under Breaking Changes, newest first, matching the heading text and GitHub slug used by neighbouring version links. Do not add a TOC link for an individual breaking-change bullet.

### Do not

- Put breaking-change documentation only in `Documents/Changelog/Changelog.md` or the PR description.
- Overwrite or rephrase older README breaking-change entries unless they are inaccurate for the same change.
- Add non-breaking changelog items to **Breaking Changes**.

## TestForm Demos

`Source/Krypton Components/TestForm` (`TestForm.csproj`) is the primary interactive validation app. When a **feature** is completed, add a **comprehensive demo** or **append to an existing demo** (do not overwrite) so maintainers and reviewers can exercise the capability without reading source first.

### Folder layout

Group forms by owning library. Folder names **omit the `.`** from the assembly name (`Krypton.Toolkit` → `KryptonToolkit`). Dots in these folder names cause problems with the SDK-style project and Solution Explorer.

Each library folder has a `Feature` subfolder and a `Bugs` subfolder. **Feature demos** go in `Feature` (a multi-file demo may use its own subfolder there). **Issue repros** go in `Bugs`. Do not add new forms at the `TestForm` project root, or directly under the library folder.

| Package | Folder under `TestForm\` |
|---------|--------------------------|
| `Krypton.Toolkit`, `Krypton.Themes`, `Krypton.Toolkit.JumpList` | `KryptonToolkit` |
| `Krypton.Toolkit.Utilities` | `KryptonToolkitUtilities` |
| `Krypton.Navigator`, `Krypton.Navigator.Utilities` | `KryptonNavigator` |
| `Krypton.Ribbon` | `KryptonRibbon` |
| `Krypton.Workspace` | `KryptonWorkspace` |
| `Krypton.Docking` | `KryptonDocking` |

```
TestForm\
  KryptonToolkit\
    Bugs\                      # BugNNNN* repros for this library
    Feature\                   # feature demos for this library
      BorderlessFormDemo.cs    # feature demo (files sit here)
      KryptonTaskDialogDemo\   # multi-file feature demo (subfolder OK)
  KryptonDocking\
    Bugs\
    Feature\
  KryptonNavigator\
    Bugs\
    Feature\
  KryptonRibbon\
    Bugs\
    Feature\
  KryptonToolkitUtilities\
    Bugs\
    Feature\
  KryptonNavigatorUtilities\
    Bugs\
    Feature\
  KryptonWorkspace\
    Bugs\
    Feature\
```

Empty `Feature` and `Bugs` folders are declared in `TestForm.csproj` (`<Folder Include="…\Feature\" />` and `…\Bugs\`) so they appear in Solution Explorer before the first form is added. Keep those includes; create the matching directory when placing the first file.

**Do not**

- Create library folders whose names contain `.` (for example `Krypton.Toolkit`).
- Place feature demos inside `Bugs`, issue repros inside `Feature`, or either kind of form directly under the library folder.
- Add new demo or bug forms at the `TestForm` project root. Existing root-level forms are legacy; leave them there unless the task is to relocate that form.
- Put package demos in cross-cutting folders (`Classes`, `ColorTestimonials`, `PaletteViewer`, `Resources`, `User Experience`).

### When to add or update

- **Features** — new controls, APIs, designer behavior, themes, dialogs, or subsystems: add or expand a demo under the matching library’s `Feature` folder (see **Folder layout**).
- **Existing demo** — if a TestForm demo already exists for that control or feature, **do not overwrite or replace it**. Keep the current form, instructions, and scenarios; **append** (new section, tab, control, or case) so the new capability can be exercised alongside what is already there.
- **Bug fixes** — add a minimal repro in the matching library’s `Bugs` folder when none exists; append to an existing demo when the fix changes observable behavior worth regression-testing.
- Skip demos for comment-only work, pure refactors, or changes with no UI/API surface.

### Registration

- Register every new form in `StartScreen.AddButtons()` via `CreateButton<TForm>(heading, description)`.
- Heading: short title (often includes issue number for bug demos).
- Description: what to try, expected outcome, and which scenarios are covered.
- Follow existing naming: `BugNNNNShortNameDemo` for issue repros (files under the library’s `Bugs` folder); `FeatureNameDemo` or `FeatureNameTest` for broader showcases (files under the library’s `Feature` folder).

### Demo content

A good demo is **comprehensive** for its scope:

- Exercises the main API paths, properties, events, and theme/palette switches relevant to the change.
- Includes short on-form instructions (labels or a read-only text block) so manual steps are obvious.
- Uses `KryptonForm` and Krypton controls for the host unless the scenario requires otherwise.
- Keeps designer-friendly structure: logic in `*.cs`, layout in `*.Designer.cs` `InitializeComponent()`.

### Krypton vs standard WinForms

Where the feature is a **Krypton replacement or wrapper** for a built-in control (or parity/behavior is the point), provide a **side-by-side comparison** when practical:

- Place native WinForms control(s) and Krypton control(s) in the same form (e.g. split columns in a `TableLayoutPanel`), matching size, text, and interaction where possible.
- Label each side clearly (e.g. “Native TextBox” / “KryptonTextBox”).
- Document what should match and what is intentionally different.
- See existing patterns: `Bug3342KryptonTextBoxResizeFlickerDemo`, `KryptonFolderBrowserDialogDemo`, `AccessibilityTest`, `Bug3343RichTextBoxEditLossDemo`.

Skip the comparison when there is no meaningful WinForms equivalent (e.g. ribbon-only or docking-only features).

### Project conventions

- Place new forms per **Folder layout** (library folder, no `.` in the name; demos in `Feature`; repros in `Bugs`).
- Add new `.cs` / `.Designer.cs` / `.resx` files to `TestForm.csproj` if not picked up automatically. Keep the existing `<Folder Include="…\Feature\" />` and `…\Bugs\` entries.
- Reference `Krypton.Toolkit.Utilities` / `Krypton.Navigator.Utilities` when the demo targets those assemblies.
- Run: `dotnet run --project ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug`
- `TestForm` does not replace [Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos). For features, also add a consumer example there (reuse `..\Standard-Toolkit-Demos` if present; clone into the parent only if missing), or **append** if one already exists (see **Standard-Toolkit-Demos**).

## Standard-Toolkit-Demos

[Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos) is a **separate GitHub repository**. It is **not** inside this repo. Consumer-facing examples (Krypton Explorer plus per-control sample apps) live there. `TestForm` remains required for maintainer validation. When a **feature** is completed, add a **comprehensive** example in Demos. If an example already exists, **append**; do not overwrite.

### Locate or clone (never re-clone)

Look only in the parent of this repository for a folder named `Standard-Toolkit-Demos`:

```powershell
$toolkitRoot = (Get-Location)   # Standard-Toolkit repo root
$parentDir = Split-Path $toolkitRoot -Parent
$demosRoot = Join-Path $parentDir 'Standard-Toolkit-Demos'
```

Treat the clone as present when `$demosRoot` exists and contains `Source\Krypton Toolkit Examples`. Do **not** look under `Source\` in this repo, do **not** search other drives, and do **not** copy or create Demos projects inside Standard-Toolkit.

**If `$demosRoot` already exists, do not clone again.** Reuse that working tree. Do **not** `git clone` into the parent, do **not** clone over the existing folder, and do **not** create a second copy. Then follow **Git boundary**: switch to `alpha` if not already on it, then create a new `alpha-…` branch from `alpha`.

**If `$demosRoot` does not exist**, clone it into the parent (do not clone over an existing folder):

```powershell
git clone https://github.com/Krypton-Suite/Standard-Toolkit-Demos.git $demosRoot
```

After a fresh clone, still follow **Git boundary** (switch to `alpha`, then create a new `alpha-…` branch).

```
<parent>\Standard-Toolkit\                 # this repository
<parent>\Standard-Toolkit-Demos\           # separate repository (reuse if present; clone only if missing)
```

### Directory structure (Demos repo)

Folder names contain spaces. Use the names on disk, not the stale `Source/Krypton Utilities Examples` link in the Demos `README.md`.

```
Standard-Toolkit-Demos\
  Directory.Build.props                    # TFMs for all examples
  Directory.Build.targets                  # app manifests
  build-all.cmd                            # do not run unless asked
  Source\
    Krypton Toolkit Examples\              # Krypton.Toolkit samples
    Krypton Toolkit Utilities Examples\    # Krypton.Toolkit.Utilities samples
    Krypton Navigator Examples\
    Krypton Navigator Utilities Examples\  # Krypton.Navigator.Utilities samples
    Krypton Ribbon Examples\
    Krypton Workspace Examples\
    Krypton Docking Examples\
    Krypton Explorer\                      # launcher for compiled example .exe files
    WixInstaller\                          # MSI packaging; do not touch for a feature demo
  Binaries\Krypton Demos\<Configuration>\  # shared output (Explorer launches .\Name.exe from here)
```

Each example is its **own WinExe project** in a subfolder of the matching `Source\Krypton … Examples` directory (for example `Source\Krypton Toolkit Examples\KryptonToggleSwitch Examples\`). Copy a neighbouring project in that same folder; do not add forms to Krypton Explorer itself except the launch link.

Category solutions (under that `Source\… Examples` folder):

| Category | Solutions on disk today |
|----------|-------------------------|
| Toolkit, Navigator, Ribbon, Workspace, Docking | Four pairs: `(Debug)` / `(Release)` × `Dev` / `Nuget`, each as `.sln` and `.slnx` (names contain `2022` and a double space before `-`) |
| Toolkit Utilities, Navigator Utilities | Debug Dev `.slnx` only |
| Explorer | `Krypton Explorer 2022 - Dev` and `- Nuget` (`.sln` and `.slnx`; no Debug/Release in the name) |

Dev solutions `ProjectReference` this repo as `..\..\..\..\Standard-Toolkit\Source\Krypton Components\…`. Nuget solutions use Canary `PackageReference`s when `$(SolutionName)` ends with `Nuget`. `build-all.md` expects `..\Standard-Toolkit` on a matching branch.

### When to add or update

- **Features** — new controls, APIs, designer behavior, themes, dialogs, or subsystems: add a new example project only when no example for that control or feature exists in the matching `Source\… Examples` folder.
- **Existing example** — if a demo project already exists (same control, dialog, or feature), **do not overwrite, replace, or recreate it**. Keep the existing forms, samples, and instructions. **Append** as required (extra section, tab, page, control, or scenario on `Form1` / the current host) so the new capability sits beside what is already there. Do not rewrite `Form1.cs` / `Form1.Designer.cs` from scratch, delete existing sample content, or substitute a new project with the same name.
- Prefer appending to the existing example over creating a near-duplicate project.
- **Bug fixes** — `TestForm` is enough unless the fix changes observable consumer-facing behaviour of an existing Demos example; then append a case to that example rather than replacing it.
- Skip for comment-only work, pure refactors, or changes with no consumer UI/API surface. If the parent clone cannot be created (clone failed, or a non-Demos folder already occupies that path), say so in the Toolkit PR validation notes and continue without a Demos example.

### Placement

Put the new or updated example under `$demosRoot` (not this repo):

| Package | Folder under `$demosRoot\Source\` |
|---------|-----------------------------------|
| `Krypton.Toolkit` | `Krypton Toolkit Examples` |
| `Krypton.Toolkit.Utilities` | `Krypton Toolkit Utilities Examples` |
| `Krypton.Navigator` | `Krypton Navigator Examples` |
| `Krypton.Navigator.Utilities` | `Krypton Navigator Utilities Examples` |
| `Krypton.Ribbon` | `Krypton Ribbon Examples` |
| `Krypton.Workspace` | `Krypton Workspace Examples` |
| `Krypton.Docking` | `Krypton Docking Examples` |

### New example project

Copy a neighbouring example in the same `Source\… Examples` folder **only when creating a new project** (no existing demo). Keep the same conventions:

- Folder and project names like `KryptonXxx Examples` / `KryptonXxx Example` (match existing spelling, including spaces). Csproj names often include `2022`.
- WinExe SDK-style csproj; designer-backed `Form1` (`*.cs` / `*.Designer.cs` / `*.resx`); `KryptonForm` host; current Standard Toolkit BSD header; UTF-8 with BOM; CRLF. Copy `Krypton.ico` from a neighbour when that folder uses it.
- `AssemblyName` is the executable name **without** `.exe`. Krypton Explorer’s `LaunchApplication(@"…")` starts `.\<AssemblyName>.exe` from `Binaries\Krypton Demos\<Configuration>\`. Copy a neighbour’s `AssemblyName` style (it is not always identical to the folder name).
- `OutputPath`: `..\..\..\Binaries\Krypton Demos\$(Configuration)\` (Explorer uses `..\..\Binaries\Krypton Demos\$(Configuration)\`).
- Dual references: copy the neighbour’s `Choose` / `When` block that tests whether `$(SolutionName)` ends with `Nuget`. Dev arms `ProjectReference` this repo (`Krypton.Toolkit 2022.csproj` plus Navigator/Ribbon/Workspace/Docking/Utilities projects as needed). Nuget arms use the matching Canary packages (`Krypton.Toolkit.Canary`, `Krypton.Navigator.Canary`, `Krypton.Standard.Toolkit.Canary`, …).
- TFMs come from Demos `Directory.Build.props`; do not duplicate a `<TargetFrameworks>` list on the example unless a neighbour does.
- Comprehensive for consumers: main API paths, properties, events, theme/palette switches, short on-form instructions. Use a Krypton vs WinForms side-by-side when the feature is a replacement or wrapper for a built-in control (same rule as **TestForm Demos**). This is not a bug-repro form.

### Registration

- Add the project to **every** `.sln` and `.slnx` in that category folder that already lists neighbouring examples. Do not invent missing Debug/Release/Nuget solutions; Utilities categories currently have only a Debug Dev `.slnx`.
- If neighbouring examples in that category already appear in Krypton Explorer, add a `KryptonLinkLabel` on the same page in `$demosRoot\Source\Krypton Explorer\Main.Designer.cs` and wire `LinkClicked` in `Main.cs` to `LaunchApplication(@"<AssemblyName>")`. Explorer pages today cover Toolkit, Docking, Workspace, Navigator, and Ribbon. Do **not** add a new Explorer tab for Utilities unless the user asks; those examples currently have no Explorer links.
- Add an entry (and screenshot/GIF when you have one) to that category’s `README.md` when the file exists (`Krypton Navigator Utilities Examples` currently has none).
- Do not change `WixInstaller` for a feature demo.

### Git boundary

Demos changes live in the **Demos** working tree. Do **not** stage, commit, or push them as part of a Standard-Toolkit pull request. Do **not** copy Demos projects into this repo.

For a **new feature** demo (new example project, or appends for that feature):

1. If `$demosRoot` already exists, **do not clone**. Use the existing working tree.
2. In `$demosRoot`, `git fetch origin`.
3. Switch to the `alpha` branch if not already on it (`git checkout alpha`). If local `alpha` tracks `origin/alpha`, update it (`git merge --ff-only origin/alpha` or equivalent). After a fresh clone, check out `alpha` before branching.
4. Create and check out a **new branch from `alpha`** with the `alpha-` prefix, e.g. `alpha-1110-krypton-menu-strip` (issue number when one exists, then a short kebab title). Use `git checkout -b alpha-<name>` while on `alpha`. The new branch must be based on `alpha`, not `master`, `gold`, or another feature branch.
5. Do not reuse `master`, `gold`, or an unrelated existing branch. If already on the matching `alpha-<name>` branch for this feature, keep it.
6. If the Demos working tree has unrelated uncommitted changes, do not discard them; stop and tell the user rather than mixing work onto the new branch.

Commit, push, or open a Demos pull request only when the user explicitly asks. When a Demos PR is opened, compare it with `alpha` (`gh pr create --base alpha`), not `master`.

## Testing Guidelines

- No formal xUnit/NUnit suite. Validate changes via `TestForm` scenarios, harnesses under `Source/TestHarnesses`, and PowerShell helpers under `Scripts/UnitTests/` (see **Unit Test Scripts**)
- When fixing a bug, add/adjust a minimal repro in `TestForm` or a harness and describe manual steps in the PR
- When completing a **feature**, add or append a comprehensive demo in `TestForm` per **TestForm Demos** (include Krypton vs WinForms comparison where appropriate; do not overwrite an existing demo), and a consumer example in [Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos) (clone into the parent directory if missing; see **Standard-Toolkit-Demos**)
- When completing a bug fix or feature, update `Documents/Changelog/Changelog.md` per **Changelog** in this file; if the change is breaking, also update `README.md` per **Breaking Changes (README)**
- For UI-visible changes, capture screenshots per **UI Screenshots / GIFs** into the local `Documents/PR/` description; do not leave the template placeholder, and do not upload the images or GIFs to GitHub

## Unit Test Scripts

Use `Scripts/UnitTests/` for PowerShell scripts that drive or inspect a Debug `TestForm` build (host a demo form, synthesise mouse input, capture screenshots, probe non-client geometry). These complement manual `TestForm` checks; they are not a substitute for a demo and are not run by CI unless explicitly wired later.

### When to create or update

- Creating throwaway `.ps1` files under `Bin/` during a bug investigation is fine for the session, but **before the work is finished**, move or rewrite the keepers into `Scripts/UnitTests/` with clear names and brief `.SYNOPSIS` / `.DESCRIPTION` help.
- Prefer extending an existing unit-test script over adding a near-duplicate.
- Document new scripts in `Scripts/UnitTests/README.md` (purpose and a short usage example).
- Do not check in screenshots or `Bin/` output produced by these scripts. Reviewer shots for a PR belong under `Documents/PR/` and are also not committed (see **UI Screenshots / GIFs**).

### Conventions

- Resolve the repo root and `Bin\<Configuration>\<TFM>` via `Scripts/UnitTests/UnitTestCommon.ps1` rather than hard-coding machine paths.
- Host WinForms demos with `-STA` when the script calls `Application.Run`.
- Keep scripts focused on one scenario (host, drag, remerge, probe, …).
- Existing #925 helpers: `Start-NavigatorFormIntegrationHost.ps1`, `Invoke-CaptionTabDrag.ps1`, `UnitTest-NavigatorCaptionTabRemerge.ps1`, `Get-NavigatorCaptionTabProbe.ps1`.

## UI Screenshots / GIFs

When a change is **user-visible**, capture stills (and a short GIF when motion is the point) before treating the work as complete, and embed them in the local `Documents/PR/` description. Do not leave **Screenshots / GIFs** as a placeholder such as “add after a local TestForm run if desired”. Do **not** upload, attach, or host the files on the GitHub pull request.

### When

- Features and bug fixes that change appearance, layout, chrome, themes/palettes, dialogs, or demo UI.
- New or updated TestForm demos that show the capability.
- Skip for API-only work, comment-only changes, and refactors with no visual difference.

### What to capture

- Enough to show the change: typical default look plus the distinctive demo state (for example contrast/override, or before/after).
- Do not capture every theme variant unless the bug or feature is family-specific; then include the affected families.
- **PNG** for stills (colour, layout, chrome, default vs override).
- **Short GIF** when motion is the point (drag, tear-out/remerge, animation, slide, flicker). Do not GIF a static colour or layout change, and do not substitute a single still when the defect is motion.

### How

1. Build Debug TestForm if binaries are stale: `dotnet build ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug`.
2. Host the relevant demo **on-screen** with PowerShell `-STA`. Reuse a `Start-*Host.ps1`, or instantiate the form in-process (pattern: `Scripts/UnitTests/Invoke-RadialMenuScreenshot.ps1`).
3. `Show` / `Activate`, `Application.DoEvents()`, then a short sleep so paint completes. Do not capture off-screen or hidden windows.
4. Capture with `System.Drawing.Graphics.CopyFromScreen` to PNG. Crop to the relevant chrome when a full-desktop shot would hide the change.
5. Read the PNG or GIF in the session so the image is visible for confirmation.
6. If the capture is reusable, keep the script under `Scripts/UnitTests/` with `# UnitTest-CI: exclude` and a README row (see **Unit Test Scripts**). Copy `Scripts/UnitTests/Invoke-RadialMenuScreenshot.ps1` (STA `-File`, in-process form, `CopyFromScreen` to `Documents/PR/`) rather than a long `powershell -Command { … }` one-liner (see **Recent Tooling Mistakes To Avoid**).

**GIF (motion only):** same host, STA, on-screen, and crop rules as PNG. Capture a short frame sequence during the interaction (`CopyFromScreen` on a timer, or before / during / after plus in-between frames for a drag). Encode to an animated GIF and save next to the description. Prefer `ffmpeg` or ImageMagick `magick` if on PATH; otherwise assemble frames with WPF `GifBitmapEncoder` (`Add-Type -AssemblyName PresentationCore`). Keep it to a few seconds, cropped, looping. If no encoder is available, capture labelled stills (`-before.png`, `-during.png`, `-after.png`) instead of skipping — do not invent a GIF.

### Where

- Save reviewer shots as `Documents/PR/<issue-or-branch>-<short-title>-<state>.png` or `.gif` next to the PR description (for example `1100-scheme-strip-text-default.png`, `925-caption-tab-remerge.gif`).
- These files are **local**, like the PR description file: do **not** stage, commit, or push them. Do **not** leave the only copy under `Bin/`.
- Embed in `Documents/PR/<file>.md` with relative markdown images and a one-line caption stating the state shown:

```markdown
![Default builtin theme](./1100-scheme-strip-text-default.png)
![Caption tab remerge](./925-caption-tab-remerge.gif)
```

- Do **not** upload, attach, or host these files on the GitHub pull request (no `user-attachments` URLs, no drag-and-drop onto the PR, no `gh` image attach). GitHub will not display local relative paths; that is intended.
- Demos category README: add a screenshot/GIF when you have one (see **Standard-Toolkit-Demos**). Those live in the Demos repo, not on the Toolkit GitHub PR.

### Do not

- Skip screenshots or GIFs for UI work, or leave the template placeholder.
- Commit PNGs, GIFs, or `Bin/` capture output in the Standard-Toolkit pull request.
- Upload or attach screenshot or GIF files to the GitHub pull request.
- Invent or draw substitute images. If capture is impossible (no interactive desktop), say so in **Validation** instead of faking a shot.

## Commit & Pull Request Guidelines

- Commits: short, imperative subject; reference issues/PRs (e.g., `Fix autosizing (#2433)` or `2439 V100 datecell autosizing`)
- PRs: clear description, linked issues, notes on breaking changes/TFM impact. UI screenshots and GIFs stay in the local `Documents/PR/` description (see **UI Screenshots / GIFs**); do not upload them to GitHub.
- If a pull request is opened or created, it must be compared with `alpha`, not `master`, `gold`, or `canary`. When using `gh pr create`, set the base branch to `alpha` (for example `--base alpha`).
- Completed bugs and features: update `Documents/Changelog/Changelog.md` (see **Changelog** above); if the change is breaking, also update `README.md` under **Breaking Changes** (see **Breaking Changes (README)**); add or append a `TestForm` demo for features (see **TestForm Demos**; do not overwrite an existing demo); also add a consumer example in [Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos) or append if one exists (clone into the parent directory if missing; work on an `alpha-…` branch from `alpha`; see **Standard-Toolkit-Demos**); write a `Documents/Development/` guide when the feature warrants in-depth maintainer docs, and a PR description in `Documents/PR/` (see **Pull Request Descriptions** below). **Do not include** `Documents/Development/` files or the per-change `Documents/PR/` description file in the Standard-Toolkit pull request (new or existing). Demos files belong only in the Demos repo. Use the PR description file as the GitHub PR body (`gh pr create --base alpha --body-file Documents/PR/<file>.md`).
- Do not add routine validation noise to commit messages or PR descriptions. Mention checks only when they are essential context, unusual, failed, or specifically requested.

## Pull Request Descriptions

When a **bug fix** or **feature** is completed, create a **PR description** as a Markdown file in the `Documents/PR/` folder **before** the pull request is opened. The file is the reviewer-facing record: use it **as the GitHub PR body** (`gh pr create --base alpha --body-file Documents/PR/<file>.md`), and do **not** include that file in the pull request. When the pull request is opened or created, compare it with `alpha`, not `master`, `gold`, or `canary` (see **Commit & Pull Request Guidelines**).

### When to add

- **Resolved** — bug fixes, regressions, and defect corrections.
- **Implemented** — new features, enhancements, and new public capability.
- Skip for comment-only work and internal refactors with no user-visible effect (same policy as **Changelog**).

### File conventions

- Location: `Documents/PR/`
- Copy `Documents/PR/TEMPLATE.md` to `Documents/PR/<issue-or-branch>-<short-title>.md`, e.g. `Documents/PR/3720-foldable-dialog.md` or `Documents/PR/2444-agents-md.md`. Use the issue number when one exists.
- One file per bug fix or feature (or the cohesive set of changes going into a single PR).
- CRLF, UTF-8 with BOM; match the tone and structure of existing repo docs.
- Keep the file **local**: do not stage, commit, or push it as part of the pull request. Matching screenshot PNGs and GIFs next to it are local as well (see **UI Screenshots / GIFs**).

### Opening the pull request

- Use this file **as** the GitHub PR description. Do not write a second body.
- Prefer `gh pr create --base alpha --body-file Documents/PR/<file>.md` (or the equivalent `--body-file` when updating). On Windows PowerShell, pass the path as a single argument; do not rely on shell quotes around a pasted body (see **Recent Tooling Mistakes To Avoid**).
- Do not include this file, or any file under `Documents/Development/`, in the commits that make up a new or existing PR.
- Do **not** upload or attach screenshot PNGs/GIFs to the GitHub pull request. Relative image links in this file are for the local draft only.

### What to include

Fill in every applicable section of `Documents/PR/TEMPLATE.md` (delete those that do not apply):

- **Summary** — consumer-facing description of what changed and why it matters.
- **Related issues** — `Closes #NNNN` when an issue exists.
- **Type of change** — bug fix / feature / breaking change / docs.
- **Changes** — notable changes grouped by area or project.
- **Affected packages & target frameworks** — only those touched/verified.
- **Validation** — `TestForm` demo name, [Standard-Toolkit-Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos) example name and `alpha-…` branch (or a note if clone/branch failed), manual steps, and the build command used.
- **Screenshots / GIFs** — required for any UI change; capture them locally per **UI Screenshots / GIFs**. Do not leave the template placeholder. Remove the section only when there is no UI change. Do not upload the images or GIFs to GitHub.
- **Changelog** — the matching `Documents/Changelog/Changelog.md` entry.
- **Breaking changes & migration** — what consumers must update, if anything. If the change is breaking, the matching `README.md` **Breaking Changes** entry must exist and follow the existing pattern (see **Breaking Changes (README)**).
- **Developer documentation** — link to the `Documents/Development/` guide for substantial features.

### Do not

- Do not add changelog entries or release notes inside `Documents/PR/` files — those belong in `Documents/Changelog/Changelog.md`.
- Do not add references or index entries for `Documents/PR/` files in `Scripts/ModernBuild/README.md`.
- Do **not** include the per-change PR description file (`Documents/PR/<issue-or-branch>-<short-title>.md`) or matching screenshot PNGs/GIFs in a new or existing pull request. Write them locally, use the Markdown as the GitHub PR body, and leave them untracked (or unstaged) relative to the PR. Do **not** upload or attach the screenshot or GIF files to GitHub. Leave `TEMPLATE.md` and `README.md` in this folder alone unless the task is to update those shared files.
- Do **not** include files under `Documents/Development/` in a new or existing pull request. If an existing PR already contains those files or the per-change PR description, remove them from the PR so they are no longer in the diff.

## Security & Configuration Tips

- Windows long paths must be enabled to build locally (see README link). Build on Windows for `-windows` TFMs
