# Findings & Decisions

## Requirements
- Implement upstream issue https://github.com/Krypton-Suite/Standard-Toolkit/issues/397: normal context menus should share the same colours as `KContextMenu`.
- Work from the current clean `V105-LTS` baseline.
- Use local branch `397-V105-LTS-context-menu-colours`.
- Use planning files before implementation edits.
- Use PowerShell for agentic shell commands unless a repo `.cmd` script is explicitly required.
- Do not run build scripts, commit, or push unless explicitly requested.
- Preserve CRLF and UTF-8 with BOM for `.cs` files.
- Keep changes compatible with `net472` and C# 7.3.

## Research Findings
- Planning-with-files skill opened and Windows session catchup check ran before implementation.
- Current worktree was detached at clean baseline and has been switched to a new local branch named `397-V105-LTS-context-menu-colours`.
- Upstream issue #397 says that after testing issue #371, normal context menus did not share the same colours as a `KContextMenu`; issue is open and labeled for themes/toolkit plus V105 LTS.
- Literal search for `KContextMenu` under `Source` returned no matches; the codebase likely uses `KryptonContextMenu` naming.
- `ContextMenuStrip` support is present in Krypton controls: `VisualControlBase`, `VisualContainerControlBase`, `VisualPanel`, `KryptonWebBrowser`, `KryptonWrapLabel`, and related controls assign a palette-created `ToolStripRenderer` when the menu opens.
- `KryptonManager` sets `ToolStripManager.Renderer` from `CurrentGlobalPalette.GetRenderer().RenderToolStrip(CurrentGlobalPalette)`, so ordinary WinForms `ContextMenuStrip` controls should receive a Krypton renderer globally.
- Renderer investigation should focus on `KryptonColorTable` / `KryptonProfessionalRenderer` and concrete renderers, because the renderer is installed but the colours differ from `KryptonContextMenu`.
- `KryptonContextMenu` initializes `PaletteContextMenuRedirect`, which maps to palette styles including `ContextMenuInner`, `ContextMenuOuter`, `ContextMenuItemImageColumn`, `ContextMenuItemHighlight`, and `ContextMenuItemTextStandard`.
- Concrete ToolStrip renderers paint normal `ContextMenuStrip` backgrounds, borders, image margins, separators, and selected item backgrounds from `KryptonColorTable` properties such as `ToolStripDropDownBackground`, `MenuBorder`, and `ImageMarginGradient*`; those values are not necessarily the same as the `ContextMenu*` palette styles.
- Implemented shared `KryptonColorTable` context-menu palette accessors and updated ToolStrip renderers to use them for normal `ContextMenuStrip` / `ToolStripDropDownMenu` painting.
- `KryptonToolStripMenuItem` per-item palette overrides still take priority; the generic context-menu highlight is used when there is no per-item override.
- Review found checked and indeterminate normal menu item check boxes still used `CheckBackground`; this was fixed by adding `ContextMenuItemImageBack`, `ContextMenuItemImageBorder`, and `ContextMenuItemImageText`, then routing renderer check-box fills, borders, and glyphs through those values.
- A renderer scan now shows no remaining `KCT.ToolStripDropDownBackground`, `KCT.ImageMarginGradient*`, `KCT.MenuBorder`, or `KCT.CheckBackground` references under `Krypton.Toolkit\Rendering`.

## Technical Decisions
| Decision | Rationale |
|----------|-----------|
| Branch in existing Codex worktree instead of creating a separate physical directory | User clarified the internal hash path is acceptable if the branch/work item name is issue-specific. |
| Add shared context-menu palette accessors and route ToolStrip context menu painting through them | Keeps the fix in the existing renderer path and avoids changing public menu APIs or component behavior outside context-menu painting. |
| Do not add a sample change | Existing normal context menus already exercise the renderer path; no new public API or repro-only control code is needed. |

## Issues Encountered
| Issue | Resolution |
|-------|------------|

## Resources
- Issue: https://github.com/Krypton-Suite/Standard-Toolkit/issues/397
- Worktree: `C:\Users\tobias\.codex\worktrees\ebe5\StdTk-105`
- `Source\Krypton Components\Krypton.Toolkit\Controls Toolkit\KryptonManager.cs`
- `Source\Krypton Components\Krypton.Toolkit\Controls Visuals\VisualContainerControlBase.cs`
- `Source\Krypton Components\Krypton.Toolkit\Palette Controls\KryptonColorTable.cs`
- `Source\Krypton Components\Krypton.Toolkit\Rendering\KryptonProfessionalRenderer.cs`

## Visual/Browser Findings
- None yet.
