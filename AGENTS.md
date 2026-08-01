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

## Always

Before considering a task complete:

- Build the affected project if instructed.
- Fix any compiler or analyzer warnings introduced by the change; treat new warnings as part of the build (do not leave them for later). Prefer fixing pre-existing warnings in files you already touch when the fix is small and local; do not expand into a repo-wide warning cleanup unless asked.
- Update TestForm when adding a feature.
- Update Changelog.md for completed features and bug fixes.
- Add developer documentation for substantial new features.
- Write a PR description in `Documents/PR/` for completed features and bug fixes (see **Pull Request Descriptions**).

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

- `Source/Krypton Components`: Core libraries (`Krypton.Toolkit`, `Krypton.Ribbon`, `Krypton.Navigator`, `Krypton.Workspace`, `Krypton.Docking`) and the solution `Krypton Toolkit Suite 2022 - VS2022.sln`
- `Source/Krypton Components/TestForm`: WinForms sample app used to validate changes; add or extend demos here when features or bugs are completed (see **TestForm Demos**)
- `Source/TestHarnesses`: Small repro/test harnesses (e.g., `ThemeSwapRepro`)
- `Scripts/`: Build and packaging scripts; `run.cmd` (root) launches an interactive menu; scripts live under `Scripts/VS2022/`, `Scripts/Current/`, `Scripts/Build/` (e.g., `build-stable.cmd`, `build-canary.cmd`, `build-nightly.cmd`, `build.proj`)
- `Bin/`: Build outputs by configuration (e.g., `Bin/Debug`)
- `Documents/`, `Assets/`, `Logs/`: Docs, images, and build logs
- `Documents/Changelog/Changelog.md`: User-facing release notes for completed bugs and features
- `Documents/Development/`: In-depth developer guides for completed features (APIs, architecture, usage); not listed in `Documents/Changelog/Changelog.md` or `Scripts/ModernBuild/README.md`
- `Documents/PR/`: One Markdown PR description per completed bug fix or feature, drafted before opening the pull request (see **Pull Request Descriptions**)

## Architecture

- `Krypton.Toolkit` contains the shared infrastructure.
- `Krypton.Interop` holds shared internal Win32/P/Invoke and net472 nullable polyfills; referenced by `Krypton.Toolkit` and consumed transitively by sibling assemblies.
- `Krypton.Ribbon` depends on `Krypton.Toolkit`.
- `Krypton.Navigator` depends on `Krypton.Toolkit`.
- Rendering flows through the palette and renderer abstractions.
- New controls should integrate with the palette system rather than hardcoding appearance.

## Editing Philosophy

- Make the smallest change that correctly solves the task.
- Preserve existing formatting and coding style.
- Do not refactor unrelated code.
- Do not rename identifiers unless requested.
- When adding or changing public/protected API, include scoped documentation per **Code Documentation Guidelines**; do not turn a feature or bug fix into a repo-wide documentation pass unless asked.
- Keep accompanying artefacts (changelog, developer guide, PR description, TestForm demo) consistent with the implementation; do not leave placeholder text from templates.

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

- Preserve the existing nullable reference type annotations and context (`<Nullable>enable</Nullable>` is set at project level).
- Do not enable or disable nullable in individual files unless requested.
- No unneeded `try/catch` blocks if there's no catch handling
- Idioms: use null-propagation and object/collection initializers where consistent
- Prefer [switch expressions](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) for simple value/type dispatch that only returns (or assigns) a value. Keep `switch` statements for complex control flow, multiple statements per arm, or side effects. Prefer a discard arm (`_ => ...`) when exhaustiveness matters. Use only pattern forms already common in this codebase (constant, type, discard, simple property/`when` guards as elsewhere); do not introduce newer pattern syntax that would fight **Public API → Compatibility**. Apply to new and changed code; do not mass-convert unrelated existing code unless asked.
- Prefer the conditional (ternary) operator (`condition ? whenTrue : whenFalse`) for simple value selection in place of an `if`/`else` that only assigns or returns. Keep `if`/`else` when either branch has multiple statements, side effects beyond the assigned value, or when nesting ternaries would hurt readability (prefer a local, `if`/`else`, or a [switch expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) instead of deep nesting). Apply to new and changed code; do not mass-convert unrelated existing code unless asked.
- Prefer [expression-bodied members](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members) (`member => expression;`) when the body is a single expression — methods, properties/`get`/`set`, constructors, finalizers, and indexers. Stay within the C# 7.3 ceiling in **Public API → Compatibility** (do not pull in newer syntax from examples such as primary constructors, `required`, or collection expressions). Keep a block body when the member needs multiple statements, local variables, early returns with nontrivial branching, or when expression form would hurt readability. Apply this to new and changed members; do not mass-convert unrelated existing code unless asked.
- WinForms: `UseWindowsForms=true`; prefer designer-friendly patterns and keep partial classes tidy
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
- Members whose existing XML already accurately describes intent; extend or correct rather than rewrite wholesale.
- **Event Args**, **Resources**, **Designer** / **`.Designer.cs`**, and other thin property-bag or generated files unless logic is non-trivial (then document only that logic).
- Large blocks of unchanged legacy code unrelated to the task — do not “document the world” in a feature or bug PR unless the user explicitly requests a documentation pass.

### Style

- Keep comments **clear and concise** — one or two sentences for inline notes; XML may be slightly longer when describing contracts or edge cases. Prefer plain language over jargon.
- Preserve existing comments and XML docs; extend or clarify them surgically rather than replacing wholesale unless they are wrong or empty.
- Use `///` XML summaries for types and public/protected API; use `//` for inline implementation notes.
- Match surrounding voice (this codebase often uses short `//` notes inside `switch` arms and multi-step flows).

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
- **Validation** — how to exercise the feature in `TestForm` or a harness (link to the demo form registered in `StartScreen`).

### TestForm demo

When the feature warrants user-visible validation, add or update a demo per **TestForm Demos** and reference it here.

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

## TestForm Demos

`Source/Krypton Components/TestForm` (`TestForm.csproj`) is the primary interactive validation app. When a **feature** is completed, add a **comprehensive demo** or **extend an existing demo** so maintainers and reviewers can exercise the capability without reading source first.

### When to add or update

- **Features** — new controls, APIs, designer behavior, themes, dialogs, or subsystems: add or expand a demo.
- **Bug fixes** — add a minimal repro when none exists; extend an existing demo when the fix changes observable behavior worth regression-testing.
- Skip demos for comment-only work, pure refactors, or changes with no UI/API surface.

### Registration

- Register every new form in `StartScreen.AddButtons()` via `CreateButton<TForm>(heading, description)`.
- Heading: short title (often includes issue number for bug demos).
- Description: what to try, expected outcome, and which scenarios are covered.
- Follow existing naming: `BugNNNNShortNameDemo` for issue repros; `FeatureNameDemo` or `FeatureNameTest` for broader showcases.

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

- Add new `.cs` / `.Designer.cs` / `.resx` files to `TestForm.csproj` if not picked up automatically.
- Reference `Krypton.Toolkit.Utilities` / `Krypton.Navigator.Utilities` when the demo targets those assemblies.
- Run: `dotnet run --project ".\Source\Krypton Components\TestForm\TestForm.csproj" -c Debug`

## Testing Guidelines

- No formal unit test suite. Validate changes via `TestForm` scenarios and harnesses under `Source/TestHarnesses`
- When fixing a bug, add/adjust a minimal repro in `TestForm` or a harness and describe manual steps in the PR
- When completing a **feature**, add or update a comprehensive demo in `TestForm` per **TestForm Demos** (include Krypton vs WinForms comparison where appropriate)
- When completing a bug fix or feature, update `Documents/Changelog/Changelog.md` per **Changelog** in this file

## Commit & Pull Request Guidelines

- Commits: short, imperative subject; reference issues/PRs (e.g., `Fix autosizing (#2433)` or `2439 V100 datecell autosizing`)
- PRs: clear description, linked issues, screenshots/gifs for UI changes, notes on breaking changes/TFM impact
- Completed bugs and features: update `Documents/Changelog/Changelog.md` (see **Changelog** above); add or update a `TestForm` demo for features (see **TestForm Demos**); add a `Documents/Development/` guide when the feature warrants in-depth maintainer docs; write a PR description in `Documents/PR/` (see **Pull Request Descriptions** below).
- Do not add routine validation noise to commit messages or PR descriptions. Mention checks only when they are essential context, unusual, failed, or specifically requested.

## Pull Request Descriptions

When a **bug fix** or **feature** is completed, create a **PR description** as a Markdown file in the `Documents/PR/` folder in the same change set (before the pull request is opened). The file is the reviewer-facing record that can be pasted directly into the GitHub PR body.

### When to add

- **Resolved** — bug fixes, regressions, and defect corrections.
- **Implemented** — new features, enhancements, and new public capability.
- Skip for comment-only work and internal refactors with no user-visible effect (same policy as **Changelog**).

### File conventions

- Location: `Documents/PR/`
- Copy `Documents/PR/TEMPLATE.md` to `Documents/PR/<issue-or-branch>-<short-title>.md`, e.g. `Documents/PR/3720-foldable-dialog.md` or `Documents/PR/2444-agents-md.md`. Use the issue number when one exists.
- One file per bug fix or feature (or the cohesive set of changes going into a single PR).
- CRLF, UTF-8; match the tone and structure of existing repo docs.

### What to include

Fill in every applicable section of `Documents/PR/TEMPLATE.md` (delete those that do not apply):

- **Summary** — consumer-facing description of what changed and why it matters.
- **Related issues** — `Closes #NNNN` when an issue exists.
- **Type of change** — bug fix / feature / breaking change / docs.
- **Changes** — notable changes grouped by area or project.
- **Affected packages & target frameworks** — only those touched/verified.
- **Validation** — `TestForm` demo name, manual steps, and the build command used.
- **Screenshots / GIFs** — for any UI change.
- **Changelog** — the matching `Documents/Changelog/Changelog.md` entry.
- **Breaking changes & migration** — what consumers must update, if anything.
- **Developer documentation** — link to the `Documents/Development/` guide for substantial features.

### Do not

- Do not add changelog entries or release notes inside `Documents/PR/` files — those belong in `Documents/Changelog/Changelog.md`.
- Do not add references or index entries for `Documents/PR/` files in `Scripts/ModernBuild/README.md`.

## Security & Configuration Tips

- Windows long paths must be enabled to build locally (see README link). Build on Windows for `-windows` TFMs
