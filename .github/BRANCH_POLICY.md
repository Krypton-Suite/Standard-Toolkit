# Branch and pull request policy

This repository uses a **warn-then-fail** branch policy enforced in CI ([#3610](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3610)).

**Full developer documentation:** [Branch Policy and Workflow Hardening](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/articles/Contributing/BranchPolicyandWorkflowHardening.html) (architecture, rules, operations, troubleshooting).

## Product vs workflow changes

| Flow | Allowed |
|------|---------|
| Topic branch → `master` | Yes (normal features and fixes) |
| Topic branch → `alpha`, `canary`, LTS lines | Yes |
| `master` → downstream branch | **Only** files under `.github/` |
| `alpha` → `alpha-backup` | Yes (automated backup sync) |
| Release line → `master` | **No** (do not merge `alpha` / `canary` / LTS into `master` via PR) |

**Workflow/CI alignment** with `master` is by **file content** under `.github/`, not by Git ancestry. Use **Sync .github from master** (or a manual PR that only touches `.github/`). Release lines may legitimately diverge from `master` in product history; policy does **not** require merging `master` into every downstream branch.

## Warn vs fail

| Repository variable | Value | Behaviour |
|---------------------|-------|-----------|
| `BRANCH_POLICY_ENFORCE` | unset or `false` | Violations emit **`::warning::`**; the check **passes** |
| `BRANCH_POLICY_ENFORCE` | `true` | Violations emit **`::error::`**; the check **fails** |
| `BRANCH_POLICY_DISABLED` | `true` | Skips the PR policy workflow entirely |

**Rollout:** leave enforce off while teams fix existing PRs, then set `BRANCH_POLICY_ENFORCE=true` and add **PR branch policy** as a required status check on protected branches.

## Deploy on `master` first

These workflows and config files must be present on the **default branch (`master`)**, not only on `alpha`:

- `.github/workflows/sync-github-from-master.yml`
- `.github/workflows/pr-branch-policy.yml`
- `.github/branch-policy.json`
- `.github/scripts/Invoke-BranchPolicyCheck.ps1`

GitHub runs scheduled workflows and uses the default-branch copy of workflow definitions. If a PR merges this feature **only** into `alpha`, **Sync .github from master** does not run on the schedule until the same `.github/` tree exists on `master` (merge, cherry-pick, or a `.github/`-only PR `master` → downstream is not the bootstrap path — use **master** directly or **Sync** after `master` has the files).

If `alpha` is later replaced (e.g. with `alpha-recovered`), commits that exist only on the old `alpha` tip can be lost unless this work is also on the recovered line (replay/cherry-pick the same commits).

## Automated `.github` sync

Workflow **Sync .github from master** (`.github/workflows/sync-github-from-master.yml`) opens PRs that copy only `.github/` from `master` onto configured release branches.

Targets include `alpha`, `canary`, `gold`, `prerelease`, `V105-LTS`, `V85-LTS`, and `V110` (see `syncGithubFromMasterTargets` in `branch-policy.json`). The sync uses `git checkout origin/master -- .github` on the target branch tip (path copy, not a merge of `master`).

## Required checks on `master` vs release branches

When enforcing policy, require **PR branch policy** on release-branch rulesets only — not on `master` — so topic PRs into `master` are not gated by this check. Details in the [developer documentation](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/articles/Contributing/BranchPolicyandWorkflowHardening.html#required-check-only-on-release-branches-not-topic-prs--master).

| Variable | Purpose |
|----------|---------|
| `SYNC_GITHUB_FROM_MASTER_DISABLED` | `true` disables the sync workflow |
| `BRANCH_POLICY_ENFORCE` | Independent; controls PR validation only |

## Configuration

Machine-readable rules: [branch-policy.json](branch-policy.json)

Edit `downstreamBranches` and `syncGithubFromMasterTargets` there when adding a new release line.

**Scan code TODOs:** `longLivedHeadBranches` drives the branch dropdown in **Scan code TODOs** (`.github/workflows/scan-code-todos.yml`). After editing `branch-policy.json` on `master`, **Sync scan-code-todos branch options** updates the workflow choices automatically (or run `pwsh .github/scripts/Update-ScanCodeTodosBranchOptions.ps1` locally).

## Branch names

- **`canary`** (lowercase) is used by `build.yml` and `release.yml`.
- **`Canary`** (capital C) is used by `canary.yml` only.
- Policy treats both as downstream lines; prefer consolidating to one branch name over time.
