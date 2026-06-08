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
- Outputs land under `Bin\<Configuration>\<TargetFramework>\` by default; with `UseArtifactsOutput=true`, outputs land under `artifacts\bin\<Configuration>\<TargetFramework>\`.
- Target frameworks are selected by MSBuild properties. VS2019/full MSBuild builds only .NET Framework 4.x TFMs; VS2022/full MSBuild excludes `net10.0-windows` and `net11.0-windows`; VS2026/full MSBuild excludes `net11.0-windows` unless explicitly enabled; CI or SDK-based builds can include `net472`, `net48`, `net481`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`, and `net11.0-windows` when the required SDKs are installed.

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

## Testing Guidelines

- No formal unit test suite. Validate changes via `TestForm` scenarios and harnesses under `Source/TestHarnesses`
- When fixing a bug, add/adjust a minimal repro in `TestForm` or a harness and describe manual steps in the PR

## Commit & Pull Request Guidelines

- Commits: short, imperative subject; reference issues/PRs (e.g., `Fix autosizing (#2433)` or `2439 V100 datecell autosizing`)
- PRs: clear description, linked issues, screenshots/gifs for UI changes, notes on breaking changes/TFM impact
- Do not add routine validation noise to commit messages or PR descriptions. Mention checks only when they are essential context, unusual, failed, or specifically requested.

## Security & Configuration Tips

- Windows long paths must be enabled to build locally (see README link). Build on Windows for `-windows` TFMs
