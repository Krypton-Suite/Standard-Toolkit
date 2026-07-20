# Pull Request Descriptions

This folder holds a **PR description** for every completed bug fix or feature, written as a Markdown file **before the pull request is opened**.

The goal is a durable, reviewable record of *what changed and why* that can be pasted directly into the GitHub PR body.

## When to add a file

Add a new Markdown file here when:

- A **bug** has been fixed (`Resolved`), or
- A **feature** / enhancement has been completed (`Implemented`).

Skip for comment-only work and internal refactors with no user-visible effect (mirrors the changelog policy in [`AGENTS.md`](../../AGENTS.md)).

## How to use

1. Copy [`TEMPLATE.md`](TEMPLATE.md) to a new file in this folder.
2. Name it `<issue-or-branch>-<short-title>.md`, e.g. `3720-foldable-dialog.md` or `2444-agents-md.md`. Use the issue number when one exists.
3. Fill in every applicable section; delete sections that do not apply.
4. When the PR is opened, paste the file contents into the GitHub PR description.

## Conventions

- One file per bug fix or feature (or cohesive set of changes going into a single PR).
- CRLF, UTF-8; match the tone and structure of existing repo docs.
- Keep summaries **consumer-facing** (what changed, why it matters); implementation detail belongs in code comments or `Documents/Development/`.
- These files are **drafts/records for reviewers**; do **not** add changelog entries here (those live in [`Documents/Changelog/Changelog.md`](../Changelog/Changelog.md)).
