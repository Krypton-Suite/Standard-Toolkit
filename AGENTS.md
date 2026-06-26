# Repository Guidelines

## Recent Tooling Mistakes To Avoid

- Do not combine `cmd.exe` variable assignment and use in the same command line. `%VAR%` is expanded before `set` takes effect, which created a stash named `"%STASH_MSG%"`. Correct example: `git stash push -m "3493-followup" -- .`
- Do not pass complex PowerShell through `cmd.exe` with unescaped `$variables`; `cmd.exe` can strip or alter the command before PowerShell sees it. Correct example: run PowerShell directly with `$path = Join-Path (Get-Location) 'AGENTS.md'; Get-Content -LiteralPath $path -Raw`.
- Do not build long `git commit -m` commands when the body contains tokens such as `--check`; argument parsing can treat body text as options. Correct example: write the message to a temp file and run `git commit -F <message-file>`.
- Do not rely on shell quotes for `gh` arguments with spaces when the wrapper has already mishandled them. Correct example: use a JSON input file with `gh api ... --input <json-file>` or a PowerShell argument array.
- Do not try to rename an existing stash with `git stash store -m`; stash display names may still come from the original stash commit. Correct example: re-apply the stash, then create a fresh `git stash push -m "3493-followup" -- .` if the label matters.
- Do not over-escape regex patterns for `rg`. A pattern like `msbuild\\.exe` can search for the wrong text. Correct example in PowerShell: `$pattern = 'msbuild\.exe'; $root = 'Scripts'; rg -n $pattern $root --glob '*.cmd'`.
- Do not use `findstr` quoted path experiments for ordinary file reads or searches. Correct example: `$path = 'Scripts\VS2022\rebuild-build-nightly.cmd'; Select-String -LiteralPath $path -Pattern 'nightly.proj'`.

## Environment

- OS: Windows
- Shell: Use PowerShell for agentic shell calls. Use `cmd.exe` only when invoking or reproducing Windows batch-script behavior.
- Tools: Visual Studio 2022 (v17) and appropriate .NET SDKs starting with `net472`
- Build scripts are Windows `.cmd` files under `Scripts/`
- Do not run build scripts unless instructed to do so

## Project Structure & Module Organization

- `Source/Krypton Components`: Core libraries (`Krypton.Toolkit`, `Krypton.Ribbon`, `Krypton.Navigator`, `Krypton.Workspace`, `Krypton.Docking`) and the solution `Krypton Toolkit Suite 2022 - VS2022.sln`
- `Source/Krypton Components/TestForm`: WinForms sample app used to validate changes
- `Source/TestHarnesses`: Small repro/test harnesses (e.g., `ThemeSwapRepro`)
- `Scripts/`: Build and packaging scripts; `run.cmd` (root) launches an interactive menu; scripts live under `Scripts/VS2022/`, `Scripts/Current/`, `Scripts/Build/` (e.g., `build-stable.cmd`, `build-canary.cmd`, `build-nightly.cmd`, `build.proj`)
- `Bin/`: Build outputs by configuration (e.g., `Bin/Debug`)
- `Documents/`, `Assets/`, `Logs/`: Docs, images, and build logs
- `Documents/Changelog/Changelog.md`: User-facing release notes for completed bugs and features
- `Documents/Development/`: In-depth developer guides for completed features (APIs, architecture, usage); not listed in `Documents/Changelog/Changelog.md` or `Scripts/ModernBuild/README.md`

## Build, Test, and Development Commands

- Script/CI builds use phased orchestration (`Scripts/Build/Krypton.Orchestration.targets`) with `msbuild /m` for parallel TFMs; do not build all `Krypton.*` projects in one parallel batch (shared `Bin/<Configuration>/<tfm>/` outputs).
- Build solution (Debug):
  - `dotnet build ".\Source\Krypton Components\Krypton Toolkit Suite 2022 - VS2022.sln" -c Debug`
- Run sample app:
  - `dotnet run --project ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug`
- Build script entry points, only when explicitly instructed:
  - `.\run.cmd` launches the interactive menu and lets you choose `Scripts\VS2022` or `Scripts\Current` (VS 2026).
  - Direct VS2022 presets: `.\Scripts\VS2022\build-stable.cmd`, `.\Scripts\VS2022\build-canary.cmd`, `.\Scripts\VS2022\build-nightly.cmd`.
  - Direct VS2026 presets: `.\Scripts\Current\build-stable.cmd`, `.\Scripts\Current\build-canary.cmd`, `.\Scripts\Current\build-nightly.cmd`.
  - Build scripts locate MSBuild via `Scripts\Common\find-msbuild.cmd` (`vswhere.exe`, then standard install paths). Profiles: `2019`, `2022`, `current` (newest VS major 18+), or a pinned major (`18`, `19`, …). `Scripts\Current\` uses `current`. Override with `MSBUILDPATH` or `MSBUILD_PATH` pointing at `MSBuild\Current\Bin`.
- Outputs land under `Bin\<Configuration>\<TargetFramework>\` by default; with `UseArtifactsOutput=true`, outputs land under `artifacts\bin\<Configuration>\<TargetFramework>\`.
- Target frameworks are selected by MSBuild properties. VS2019/full MSBuild builds only .NET Framework 4.x TFMs; VS2022/full MSBuild excludes `net10.0-windows` and `net11.0-windows`; VS2026/full MSBuild excludes `net11.0-windows` unless explicitly enabled; CI or SDK-based builds can include `net472`, `net48`, `net481`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`, and `net11.0-windows` when the required SDKs are installed.
- New files must use only the current Standard Toolkit BSD header. Do not add the original ComponentFactory BSD header unless the file is derived from original ComponentFactory source.

## Coding Style & Naming Conventions

- Line endings/encoding: CRLF, UTF-8 with BOM
- Follow `Source/.editorconfig` and project analyzers (`EnableNETAnalyzers=true`)
- Indentation: 4 spaces; line endings: CRLF
- Projects use `global using` like in GlobalDeclarations.cs, do not add new usings in other files
- Before adding new variables check for existing ones
- No variable aliasing
- New files must use only the current Standard Toolkit BSD header. Do not add the original ComponentFactory BSD header unless the file is derived from original ComponentFactory source.

## C# Rules

- Surgical edits: preserve structure, identifiers, and existing comments; avoid adding defensive checks unless asked
- No unneeded `try/catch` blocks if there's no catch handling
- Idioms: use null-propagation and object/collection initializers where consistent
- Fix compiler, analyzer, and IDE warnings in new or modified code before handing work back
- Prefer switch expressions for simple value/type dispatch that only returns a value; keep switch statements for complex control flow or side effects
- Compatibility: ensure changes build for `net472` and C# 7.3
- WinForms: `UseWindowsForms=true`; prefer designer-friendly patterns and keep partial classes tidy
- WinForms designer: keep object declarations at file bottom; initialize in `*.Designer.cs` `InitializeComponent()`
- Constraint: do not use `yield return` inside `catch` blocks

## Code Documentation Guidelines

When asked to review or document code, add comments only where they help a maintainer understand **non-obvious** behavior. Do not narrate what the code already says.

### What to comment

- **Class-level summaries** for types that participate in a larger model (composite trees, state machines, store/restore flows, drag hosts). Name sibling types and the role of the class in the hierarchy.
- **Inline comments** at decision points for:
  - Multi-step algorithms (store-then-restore, orphan handling, greedy layout shrink)
  - Propagation (`PropogateAction`, `StartUpdate`/`EndUpdate`, reverse child iteration)
  - State machines and message-filter / focus edge cases
  - Drag-drop choreography (hidden float window reuse, target priority, placeholder pages)
  - XML persistence quirks (element order, attribute meaning, misnamed APIs, buffer length)
  - Geometry or ordering that is not obvious from property names (z-order, hot vs draw rects, remainder path parsing)
- **Brief region comments** above enum groups that act as a catalog for a subsystem (e.g. propagation actions).

### What not to comment

- Obvious boilerplate (`// This constructor creates an instance of X`, `// Return the result`, restating parameter names).
- Every public member when XML documentation already describes intent adequately.
- **Event Args**, **Resources**, **Designer** / **`.Designer.cs`**, and other thin property-bag or generated files unless logic is non-trivial.
- Large blocks of unchanged legacy code unrelated to the task.

### Style

- Keep comments **clear and concise** — one or two sentences; prefer plain language over jargon.
- Preserve existing comments and XML docs; extend or clarify them surgically rather than replacing wholesale.
- Use `///` XML summaries for types and public API; use `//` for inline implementation notes.
- In XML, use `<see cref="..."/>` and `<c>...</c>` to link related types and enum values.
- Match surrounding voice (this codebase often uses short `//` notes inside `switch` arms and multi-step flows).

### Prioritization (large modules)

For substantial packages (e.g. `Krypton.Docking`), work in this order:

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
- **Validation** — how to exercise the feature in `TestForm` or a harness.

### File conventions

- Location: `Documents/Development/`
- Name: descriptive kebab or Pascal-style title, e.g. `Krypton-Docking-Developer-Guide.md` or `Visual-Studio-Templates-Developer-Guide.md`.
- One feature (or cohesive subsystem) per file; cross-link related guides when helpful.
- CRLF, UTF-8; match tone and structure of existing repo docs.

### Do not list in these files

- **Do not** add changelog entries or release notes for these guides in `Documents/Changelog/Changelog.md`.
- **Do not** add references or index entries for these guides in `Scripts/ModernBuild/README.md`.

Changelog and ModernBuild README stay focused on user-facing release history and build tooling respectively. Developer guides are discovered via `Documents/Development/` and code cross-references only.

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
   * To use, you will need to download the `Krypton.Standard.Toolkit` NuGet package, as this control is part of the `Krypton.Toolkit.Utilities` assembly.
* Implemented [#9012](https://github.com/Krypton-Suite/Standard-Toolkit/issues/9012), **[Breaking Change]** Summary of what broke and what consumers must update.
```

- Prefix with `Resolved` or `Implemented` (same verbs as existing entries).
- Link the GitHub issue when one exists (`[#NNNN](https://github.com/Krypton-Suite/Standard-Toolkit/issues/NNNN)`).
- If the change is **breaking** for consumers (API removal/rename, behavior change requiring migration, assembly/namespace moves), insert `**[Breaking Change]**` immediately after the issue link comma and before the summary.
- If the feature lives in `Krypton.Toolkit.Utilities.csproj` or `Krypton.Navigator.Utilities.csproj`, append the indented NuGet sub-bullet shown in the example above (`To use, you will need to download the Krypton.Standard.Toolkit NuGet package…`). Use the matching assembly name (`Krypton.Toolkit.Utilities` or `Krypton.Navigator.Utilities`).
- One line per item; use indented sub-bullets only when extra user-facing detail is needed (see existing entries).
- Write for **consumers** of the toolkit (what changed and why it matters), not implementation detail—that belongs in `Documents/Development/` or code comments.

### Do not add to the changelog

- Entries for developer guides under `Documents/Development/`.
- References to `Scripts/ModernBuild/README.md` or build-script internals unless the change is user-facing.

## Testing Guidelines

- No formal unit test suite. Validate changes via `TestForm` scenarios and harnesses under `Source/TestHarnesses`
- When fixing a bug, add/adjust a minimal repro in `TestForm` or a harness and describe manual steps in the PR
- When completing a bug fix or feature, update `Documents/Changelog/Changelog.md` per **Changelog** in this file

## Commit & Pull Request Guidelines

- Commits: short, imperative subject; reference issues/PRs (e.g., `Fix autosizing (#2433)` or `2439 V100 datecell autosizing`)
- PRs: clear description, linked issues, screenshots/gifs for UI changes, notes on breaking changes/TFM impact
- Completed bugs and features: update `Documents/Changelog/Changelog.md` (see **Changelog** above); add a `Documents/Development/` guide when the feature warrants in-depth maintainer docs.
- Do not add routine validation noise to commit messages or PR descriptions. Mention checks only when they are essential context, unusual, failed, or specifically requested.

## Security & Configuration Tips

- Windows long paths must be enabled to build locally (see README link). Build on Windows for `-windows` TFMs
