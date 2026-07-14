# New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
# Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
#
# Run Scan code TODOs via GitHub CLI with an interactive branch list.
# Requires: gh auth login
# Example:
#   pwsh .github/scripts/Run-ScanCodeTodos.ps1
#   pwsh .github/scripts/Run-ScanCodeTodos.ps1 -Branch alpha -DryRun

param(
    [string]$Repository = 'Krypton-Suite/Standard-Toolkit',

    [string]$Branch,

    [switch]$DryRun,

    [switch]$IncludeAllMarkers,

    [switch]$ListBranchesOnly
)

$ErrorActionPreference = 'Stop'

function Get-RemoteBranches {
    $lines = gh api "repos/$Repository/branches" --paginate --jq '.[].name'
    return @($lines | Sort-Object)
}

$branches = Get-RemoteBranches
if ($branches.Count -eq 0) {
    throw "No branches returned from $Repository."
}

if ($ListBranchesOnly) {
    $branches | ForEach-Object { Write-Host $_ }
    exit 0
}

if (-not $Branch) {
    Write-Host "Remote branches on $Repository:"
    for ($i = 0; $i -lt $branches.Count; $i++) {
        Write-Host ("[{0,3}] {1}" -f ($i + 1), $branches[$i])
    }

    $selection = Read-Host 'Enter branch number or name'
    if ($selection -match '^\d+$') {
        $index = [int]$selection - 1
        if ($index -lt 0 -or $index -ge $branches.Count) {
            throw "Invalid branch number: $selection"
        }
        $Branch = $branches[$index]
    }
    else {
        $Branch = $selection.Trim()
    }
}

if ($branches -notcontains $Branch) {
    throw "Branch '$Branch' was not found on $Repository."
}

$policyPath = Join-Path (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path '.github\branch-policy.json'
$isPolicyBranch = $false
if (Test-Path -LiteralPath $policyPath) {
    $policy = Get-Content -LiteralPath $policyPath -Raw | ConvertFrom-Json
    $isPolicyBranch = @($policy.longLivedHeadBranches) |
        Where-Object { $_.Equals($Branch, [System.StringComparison]::OrdinalIgnoreCase) } |
        Select-Object -First 1
}

$args = @(
    'workflow', 'run', 'scan-code-todos.yml',
    '--repo', $Repository,
    '-f', "dry_run=$($DryRun.IsPresent)",
    '-f', 'group_duplicates=true',
    '-f', "include_all_markers=$($IncludeAllMarkers.IsPresent)",
    '-f', 'close_stale=false'
)

if ($isPolicyBranch) {
    $args += '-f'
    $args += "branch=$Branch"
}
else {
    $args += '-f'
    $args += 'branch=other'
    $args += '-f'
    $args += "branch_custom=$Branch"
}

Write-Host "Starting Scan code TODOs on branch '$Branch' (dry_run=$($DryRun.IsPresent))..."
gh @args
Write-Host 'Workflow dispatched. Open Actions on GitHub to monitor the run.'
