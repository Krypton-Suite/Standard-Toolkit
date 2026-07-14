# New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
# Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
#
# Syncs scan-code-todos.yml workflow_dispatch branch options from branch-policy.json.
# Example:
#   pwsh .github/scripts/Update-ScanCodeTodosBranchOptions.ps1

param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
)

$ErrorActionPreference = 'Stop'

$policyPath = Join-Path $RepositoryRoot '.github\branch-policy.json'
$workflowPath = Join-Path $RepositoryRoot '.github\workflows\scan-code-todos.yml'

if (-not (Test-Path -LiteralPath $policyPath)) {
    throw "Policy file not found: $policyPath"
}

if (-not (Test-Path -LiteralPath $workflowPath)) {
    throw "Workflow file not found: $workflowPath"
}

$policy = Get-Content -LiteralPath $policyPath -Raw | ConvertFrom-Json
$uniqueBranches = [System.Collections.Generic.List[string]]::new()
$seen = @{}

foreach ($branch in @($policy.longLivedHeadBranches)) {
    if ([string]::IsNullOrWhiteSpace($branch)) {
        continue
    }

    $key = $branch.ToLowerInvariant()
    if ($seen.ContainsKey($key)) {
        continue
    }

    $seen[$key] = $true
    [void]$uniqueBranches.Add($branch.Trim())
}

$ordered = [System.Collections.Generic.List[string]]::new()
foreach ($preferred in @('master', 'main')) {
    $match = $uniqueBranches | Where-Object { $_.Equals($preferred, [System.StringComparison]::OrdinalIgnoreCase) } | Select-Object -First 1
    if ($match) {
        [void]$ordered.Add($match)
        [void]$uniqueBranches.Remove($match)
    }
}

foreach ($branch in ($uniqueBranches | Sort-Object)) {
    [void]$ordered.Add($branch)
}

if ($ordered.Count -eq 0) {
    throw 'No branches found in longLivedHeadBranches.'
}

$indent = '          '
$optionLines = ($ordered | ForEach-Object { "$indent- $_" }) -join "`r`n"
$beginMarker = '# BEGIN: long-lived-branches'
$endMarker = '# END: long-lived-branches'
$replacementBlock = @(
    "$indent$beginMarker (synced from branch-policy.json)"
    $optionLines
    "$indent$endMarker"
) -join "`r`n"

$content = Get-Content -LiteralPath $workflowPath -Raw
$pattern = "(?ms)^[ \t]*$([regex]::Escape($beginMarker))[^\r\n]*\r?\n.*?^[ \t]*$([regex]::Escape($endMarker))"

if ($content -notmatch $pattern) {
    throw "Sync markers not found in $workflowPath. Expected '$beginMarker' and '$endMarker'."
}

$updated = [regex]::Replace($content, $pattern, $replacementBlock, 1)
if ($updated -eq $content) {
    Write-Host 'scan-code-todos.yml branch options are already up to date.'
    exit 0
}

$utf8Bom = New-Object System.Text.UTF8Encoding($true)
[System.IO.File]::WriteAllText($workflowPath, $updated, $utf8Bom)
Write-Host ('Updated branch options in {0}:' -f $workflowPath)
$ordered | ForEach-Object { Write-Host "  - $_" }
