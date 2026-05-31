# Task Plan: Issue 397 Context Menu Colours

## Goal
Fix Standard Toolkit issue #397 so normal WinForms context menus use the same Krypton palette colours as `KContextMenu`, while preserving V105-LTS compatibility.

## Current Phase
Review Fixes

## Phases

### Phase 1: Requirements & Discovery
- [x] Confirm branch/worktree setup
- [x] Run planning-with-files session catchup
- [x] Create planning files before implementation edits
- [x] Inspect upstream issue #397
- [x] Locate context menu colour rendering path
- **Status:** complete

### Phase 2: Approach
- [x] Identify minimal fix location
- [x] Confirm compatibility with net472 / C# 7.3
- [x] Document technical decision
- **Status:** complete

### Phase 3: Implementation
- [x] Apply surgical code changes
- [x] Preserve UTF-8 BOM / CRLF for .cs files
- [x] Add minimal repro/support if useful
- **Status:** complete

### Phase 4: Verification
- [x] Run focused validation only
- [x] Record validation results
- [x] Inspect git diff
- **Status:** complete

### Phase 5: Delivery
- [x] Update planning files
- [x] Summarize files changed and validation
- [x] Do not commit or push
- **Status:** complete

### Phase 6: Review Fixes
- [x] Fix checked/indeterminate item palette path
- [x] Reduce avoidable duplication or document remaining renderer split
- [x] Run focused validation
- [x] Run review again
- **Status:** complete

## Key Questions
1. Which renderer or palette hook controls normal `ContextMenuStrip` colours?
2. How does `KContextMenu` map Krypton palette colours, and can normal menus share that path?
3. Is a minimal sample/repro update needed, or is the library fix sufficient?

## Decisions Made
| Decision | Rationale |
|----------|-----------|
| Use current Codex worktree on local branch `397-V105-LTS-context-menu-colours` | User clarified the physical worktree path may remain internal, but branch name must match the issue. |
| Avoid build scripts and pushes | Repository/user constraints explicitly disallow them unless requested. |
| Map normal ToolStrip context menu rendering to `ContextMenu*` palette styles | `KryptonContextMenu` already uses these palette styles; normal `ContextMenuStrip` had a Krypton renderer but still read generic ToolStrip colour-table values. |
| No TestForm repro changes | The fix is renderer-level and covers existing normal `ContextMenuStrip` paths; focused build validation is sufficient for this change. |
| Reopen after review | The review found checked/indeterminate context menu items still used `CheckBackground`, so that path must map to `ContextMenuItemImage`. |

## Errors Encountered
| Error | Attempt | Resolution |
|-------|---------|------------|
| Tried to read `Krypton.Toolkit.csproj`, but the actual project is named `Krypton.Toolkit 2022.csproj` | 1 | Located project files with `rg --files` and continued with the correct path. |

## Notes
- Baseline branch: `V105-LTS`.
- Current local branch: `397-V105-LTS-context-menu-colours`.
- Keep changes surgical and compatible with C# 7.3 / `net472`.
