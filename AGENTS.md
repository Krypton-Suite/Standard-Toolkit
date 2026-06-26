# Repository Guidelines

## Environment
- OS: Windows
- Shell: Use Command Prompt (`cmd.exe`) only; avoid Bash/Unix commands
- Tools: Visual Studio 2022 (v17) and appropriate .NET SDKs starting with `net472`
- Build scripts are Windows `.cmd` files under `Scripts/`
- Do not run build scripts unless instructed to do so

## Project Structure & Module Organization
- `Source/Krypton Components`: Core libraries (`Krypton.Toolkit`, `Krypton.Ribbon`, `Krypton.Navigator`, `Krypton.Workspace`, `Krypton.Docking`) and the solution `Krypton Toolkit Suite 2022 - VS2022.sln`
- `Source/Krypton Components/TestForm`: WinForms sample app used to validate changes
- `Source/TestHarnesses`: Small repro/test harnesses (e.g., `ThemeSwapRepro`)
- `Scripts/`: Build and packaging scripts (`build-stable.cmd`, `build-canary.cmd`, `build-nightly.cmd`, `build.proj`)
- `Bin/`: Build outputs by configuration (e.g., `Bin/Debug`)
- `Documents/`, `Assets/`, `Logs/`: Docs, images, and build logs

## Build, Test, and Development Commands
- Build solution (Debug):
  - `dotnet build "Source/Krypton Components/Krypton Toolkit Suite 2022 - VS2022.sln" -c Debug`
- Run sample app:
  - `dotnet run --project "Source/Krypton Components/TestForm/TestForm.csproj" -c Debug`
- Preset builds (Windows cmd):
  - `Scripts/VS2022/build-stable.cmd` | `Scripts/VS2022/build-canary.cmd` | `Scripts/VS2022/build-nightly.cmd` (Visual Studio 2022)
  - `Scripts/Current/build-stable.cmd` | `Scripts/Current/build-canary.cmd` | `Scripts/Current/build-nightly.cmd` (Visual Studio 2026 or later)
  - Build scripts locate MSBuild via `Scripts\Common\find-msbuild.cmd` (`vswhere.exe`, then standard install paths). Profiles: `2019`, `2022`, `current` (newest VS major 18+), or a pinned major (`18`, `19`, …). Override with `MSBUILDPATH` or `MSBUILD_PATH` pointing at `MSBuild\Current\Bin`.
- Outputs land in `Bin/<Configuration>/`. VS2019/full MSBuild builds only .NET Framework 4.x TFMs; supported TFMs include `net472`, `net48`, `net481`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`

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

## Security & Configuration Tips
- Windows long paths must be enabled to build locally (see README link). Build on Windows for `-windows` TFMs
