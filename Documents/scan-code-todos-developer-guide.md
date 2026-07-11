# Scan Code TODOs — Developer Guide

This guide explains how the **Scan code TODOs** automation works, including **Code ToDo** and **FIXME → Bug Report** filing.

Implemented for [#3899](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3899).

---

## Table of contents

1. [Overview](#overview)
2. [Marker types and issue templates](#marker-types-and-issue-templates)
3. [Comment conventions](#comment-conventions)
4. [Running the workflow](#running-the-workflow)
5. [Workflow inputs](#workflow-inputs)
6. [Deduplication](#deduplication)
7. [Created issue formats](#created-issue-formats)
8. [Labels and area mapping](#labels-and-area-mapping)
9. [Stale issue closure](#stale-issue-closure)
10. [Local development](#local-development)
11. [Operational playbooks](#operational-playbooks)
12. [Troubleshooting](#troubleshooting)
13. [Reference](#reference)

---

## Overview

The workflow scans a selected branch for inline code markers and opens GitHub issues using the appropriate template format:

| Marker | Default behaviour | Issue format | Template |
|--------|-------------------|--------------|----------|
| `ToDo(issue):` / `TODO(issue):` | Opt-in (default) | `[Code ToDo]: ...` | `code_todo.yml` |
| `FIXME(issue):` | Opt-in (default) | `[Bug]: ...` | `bug_report.yml` |
| Plain `TODO` / `ToDo` / `HACK` | Only when `include_all_markers=true` | Code ToDo | `code_todo.yml` |
| Plain `FIXME` | Only when `include_all_markers=true` | Bug Report | `bug_report.yml` |

Legacy informal comments are **not** filed unless a maintainer explicitly enables full-marker mode.

---

## Marker types and issue templates

### Code ToDo (`todo`)

For technical debt, enhancements, and follow-up work.

```csharp
// ToDo(issue): Create a new API for combo box DPI scaling
```

Creates issues using the **Code ToDo** structure (`### Summary`, `### Source`, fingerprint `<!-- code-todo-scan:key=... -->`).

### Bug Report (`fixme`)

For defects and incorrect behaviour flagged in source.

```csharp
// FIXME(issue): Text is drawn incorrectly at high DPI
```

Creates issues using the **Bug Report** structure from `bug_report.yml`:

- `### Summary`
- `### Description`
- `### Steps to Reproduce` (placeholder — needs manual completion)
- `### Expected Behavior` (placeholder)
- `### Actual Behavior` (placeholder)
- `### Additional Information` (source location table + scan metadata)
- `### Areas Affected` (dropdown values for `auto-label-issue-areas`)
- Fingerprint `<!-- code-fixme-scan:key=... -->`

The `auto-label-issue-areas` workflow then applies `[Bug]:` title prefix (if needed) and `area:*` labels from **Areas Affected**.

---

## Comment conventions

### Opt-in markers (default scan)

```csharp
// ToDo(issue): Description of work to track
// TODO(issue): Description of work to track
// FIXME(issue): Description of defect to track
```

```yaml
# TODO(issue): Remove temporary build guard after November 2026
# FIXME(issue): WebView2 step guard fires on wrong branches
```

### Plain markers (full scan only)

Enable with `include_all_markers=true`:

| Marker | Issue type |
|--------|------------|
| `TODO`, `ToDo`, `HACK` | Code ToDo |
| `FIXME` | Bug Report |

### Already-tracked comments

Skipped when the line contains `#1234` or `issues/1234`.

### Recommended practices

- Use **`ToDo(issue):`** for planned work and refactors
- Use **`FIXME(issue):`** for known bugs and incorrect behaviour
- Keep descriptions specific; identical text is grouped into one issue
- Link resolved work with `#nnnn` in the comment to prevent re-filing

---

## Running the workflow

1. Open **Actions** → **Scan code TODOs**
2. Click **Run workflow**
3. Select branch and inputs
4. Review job **Summary** and `todo-matches.json` artifact

**Prerequisites:** workflow must exist on the default branch; start with `dry_run=true`.

---

## Workflow inputs

| Input | Default | Description |
|-------|---------|-------------|
| `branch` | `master` | Branch to scan |
| `dry_run` | `true` | Log only; no mutations |
| `max_issues` | `25` | Max new issues per run (todo + bug combined) |
| `group_duplicates` | `true` | Group identical comment text |
| `include_all_markers` | `false` | Include plain markers; `FIXME` → Bug Report |
| `close_stale` | `false` | Close fingerprint-tracked issues with no source comment |

**Kill switch:** repository variable `SCAN_CODE_TODOS_DISABLED=true`

---

## Deduplication

Checks run in order before creating an issue:

1. **Fingerprint** — `code-todo-scan` or `code-fixme-scan` key in body
2. **Title** — normalized `[Code ToDo]:` or `[Bug]:` title (scoped by marker type)
3. **Source location** — same `path/file.cs:line` in an existing issue body

### Fingerprint key

Includes marker type so identical text files separately for todo vs fixme:

```
SHA-256("fixme:" + normalizedText)   // when group_duplicates=true
SHA-256("todo:" + normalizedText)
```

### Index sources

- Issues with `todo :spiral_notepad:` label
- Issues found via search: `[Code ToDo]:` and `[Bug]:` in title (full body fetched)

---

## Created issue formats

### Code ToDo example

**Title:** `[Code ToDo]: Create a new API for this in a later version`

**Labels:** `todo :spiral_notepad:`, `backlog`, `version:all versions`, plus path-based `area:*`

### Bug Report (FIXME) example

**Title:** `[Bug]: Text is drawn incorrectly at high DPI`

**Labels:** `bug`, `backlog`, `version:all versions`

**Areas Affected** (in body, for auto-label):

```markdown
### Areas Affected

- Toolkit
```

Path → dropdown mapping:

| Path contains | Areas Affected value |
|---------------|---------------------|
| `Krypton.Docking` | Docking |
| `Krypton.Navigator` | Navigator |
| `Krypton.Ribbon` | Ribbon |
| `Krypton.Workspace` | Workspace |
| `Krypton.Utilities` | Utilities |
| `Krypton.Toolkit`, `TestForm` | Toolkit |

---

## Labels and area mapping

### Code ToDo issues

Path-based `area:*` labels are applied directly by the scan workflow.

### Bug Report (FIXME) issues

`area:*` labels are applied by `auto-label-issue-areas.yml` from the `### Areas Affected` section (same as manual bug reports).

---

## Stale issue closure

When `close_stale=true`, open issues with workflow fingerprints (`code-todo-scan` or `code-fixme-scan`) are closed if the source comment no longer appears in the scan.

Always dry-run first. Scanning the wrong branch can close valid issues.

---

## Local development

```cmd
pwsh .github\scripts\Scan-CodeTodos.ps1
pwsh .github\scripts\Scan-CodeTodos.ps1 -IncludeAllMarkers -GroupDuplicates
```

Output includes marker type:

```
[fixme] Source/.../KryptonTextBox.cs:1447  This should probably rather be drawn...
[todo] Source/.../KryptonCommand.cs:476  Re-enable this once completed
```

**Current codebase (full-marker mode):** ~554 matches (553 todo, 1 fixme) → ~65 grouped issues.

---

## Operational playbooks

### Track a new bug from code

```csharp
// FIXME(issue): Glyph rendering uses bitmap fallback at high DPI
```

Run workflow with `dry_run=false`, `max_issues=1`. Complete **Steps to Reproduce** / **Expected** / **Actual** on the created issue.

### Import legacy FIXME

```cmd
pwsh .github\scripts\Scan-CodeTodos.ps1 -IncludeAllMarkers -GroupDuplicates
```

Review fixme groups, then run workflow with `include_all_markers=true` in small batches.

### Bulk Code ToDo import

Same as above but expect mostly `todo` groups (e.g. `"Re-enable this once completed"`).

---

## Troubleshooting

| Problem | Likely cause |
|---------|--------------|
| FIXME not filed in default mode | Missing `(issue)` opt-in syntax |
| Bug issue missing `area:*` labels | `### Areas Affected` missing or auto-label not run yet |
| FIXME filed as Code ToDo | Marker not recognized as `FIXME` — check casing/spelling |
| Duplicate bug despite new comment | Title or source location matches existing `[Bug]:` issue |
| Steps/Expected/Actual are placeholders | By design — FIXME automation seeds bug template; fill manually |

---

## Reference

| Artifact | Path |
|----------|------|
| Workflow | `.github/workflows/scan-code-todos.yml` |
| Code ToDo template | `.github/ISSUE_TEMPLATE/code_todo.yml` |
| Bug Report template | `.github/ISSUE_TEMPLATE/bug_report.yml` |
| Local script | `.github/scripts/Scan-CodeTodos.ps1` |
| Feature request | https://github.com/Krypton-Suite/Standard-Toolkit/issues/3899 |

### Opt-in syntax summary

```csharp
// ToDo(issue):  -> Code ToDo issue
// FIXME(issue): -> Bug Report issue
```

### Glossary

| Term | Definition |
|------|------------|
| `markerType: todo` | Files a Code ToDo issue |
| `markerType: fixme` | Files a Bug Report issue |
| `code-todo-scan` | Fingerprint namespace for Code ToDo issues |
| `code-fixme-scan` | Fingerprint namespace for FIXME bug issues |

---

*Last updated for FIXME / Bug Report support.*
