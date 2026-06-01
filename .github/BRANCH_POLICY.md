# Branch and pull request policy

This repository uses a **warn-then-fail** branch policy enforced in CI ([#3610](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3610)).

## Product vs workflow changes

| Flow | Allowed |
|------|---------|
| Topic branch → `master` | Yes (normal features and fixes) |
| Topic branch → `alpha`, `canary`, LTS lines | Yes |
| `master` → downstream branch | **Only** files under `.github/` |
| `alpha` → `alpha-backup` | Yes (automated backup sync) |
| Release line → `master` | **No** (do not merge `alpha` / `canary` / LTS into `master` via PR) |

Downstream branches must **contain** `master` in Git history (they may still differ in product code). If CI reports “behind master”, merge `master` into the target branch or merge an open **Sync .github from master** PR.

## Warn vs fail

| Repository variable | Value | Behaviour |
|---------------------|-------|-----------|
| `BRANCH_POLICY_ENFORCE` | unset or `false` | Violations emit **`::warning::`**; the check **passes** |
| `BRANCH_POLICY_ENFORCE` | `true` | Violations emit **`::error::`**; the check **fails** |
| `BRANCH_POLICY_DISABLED` | `true` | Skips the PR policy workflow entirely |

**Rollout:** leave enforce off while teams fix existing PRs, then set `BRANCH_POLICY_ENFORCE=true` and add **PR branch policy** as a required status check on protected branches.

## Automated `.github` sync

Workflow **Sync .github from master** (`.github/workflows/sync-github-from-master.yml`) opens PRs that copy only `.github/` from `master` onto configured release branches.

| Variable | Purpose |
|----------|---------|
| `SYNC_GITHUB_FROM_MASTER_DISABLED` | `true` disables the sync workflow |
| `BRANCH_POLICY_ENFORCE` | Independent; controls PR validation only |

## Configuration

Machine-readable rules: [branch-policy.json](branch-policy.json)

Edit `downstreamBranches`, `syncGithubFromMasterTargets`, and `mustContainMasterAncestor` there when adding a new release line.

## Branch names

- **`canary`** (lowercase) is used by `build.yml` and `release.yml`.
- **`Canary`** (capital C) is used by `canary.yml` only.
- Policy treats both as downstream lines; prefer consolidating to one branch name over time.
