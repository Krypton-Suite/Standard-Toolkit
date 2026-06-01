# New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
# Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
#
# Report-only: lists configured branches that do not contain origin/master in their history.

param(
    [Parameter(Mandatory = $true)]
    [string]$PolicyPath,

    [Parameter(Mandatory = $true)]
    [string]$Repository
)

$ErrorActionPreference = 'Stop'

if ($Repository -ne 'Krypton-Suite/Standard-Toolkit') {
    Write-Host "Skipping behind-master report for repository '$Repository'."
    exit 0
}

if (-not (Test-Path -LiteralPath $PolicyPath)) {
    throw "Policy file not found: $PolicyPath"
}

$policy = Get-Content -LiteralPath $PolicyPath -Raw | ConvertFrom-Json
$branches = @($policy.mustContainMasterAncestor) | Select-Object -Unique

$report = @()
$priorEap = $ErrorActionPreference
$ErrorActionPreference = 'Continue'

try {
    $null = git fetch --no-tags --prune origin master 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Host '::warning::Could not fetch origin/master for behind-master report.'
        exit 0
    }

    $masterTip = (git rev-parse origin/master 2>$null)
    if (-not $masterTip) {
        Write-Host '::warning::Could not resolve origin/master.'
        exit 0
    }

    foreach ($branch in $branches) {
        $null = git fetch --no-tags origin "refs/heads/${branch}:refs/remotes/origin/${branch}" 2>&1
        if ($LASTEXITCODE -ne 0) {
            $report += [pscustomobject]@{
                Branch  = $branch
                Status  = 'missing'
                Behind  = ''
                Message = 'Branch not found on origin or fetch failed.'
            }
            continue
        }

        $baseTip = (git rev-parse "origin/$branch" 2>$null)
        if (-not $baseTip) {
            $report += [pscustomobject]@{
                Branch  = $branch
                Status  = 'missing'
                Behind  = ''
                Message = 'Could not resolve branch tip.'
            }
            continue
        }

        $null = git merge-base --is-ancestor $masterTip $baseTip 2>$null
        if ($LASTEXITCODE -eq 0) {
            $report += [pscustomobject]@{
                Branch  = $branch
                Status  = 'ok'
                Behind  = '0'
                Message = 'Contains all commits from master.'
            }
        } else {
            $behind = (git rev-list --count "$baseTip..$masterTip" 2>$null)
            if (-not $behind) { $behind = '?' }
            $report += [pscustomobject]@{
                Branch  = $branch
                Status  = 'behind'
                Behind  = [string]$behind
                Message = "$behind commit(s) on master are not reachable from this branch."
            }
        }
    }
} finally {
    $ErrorActionPreference = $priorEap
}

Write-Host ''
Write-Host '=== Branches behind master (report-only) ==='
$report | Format-Table -AutoSize Branch, Status, Behind, Message | Out-String | Write-Host

foreach ($row in $report) {
    if ($row.Status -eq 'behind') {
        Write-Host "::warning::[behind-master-report] Branch '$($row.Branch)' is behind master: $($row.Message)"
    } elseif ($row.Status -eq 'missing') {
        Write-Host "::notice::[behind-master-report] Branch '$($row.Branch)': $($row.Message)"
    }
}

$behindCount = @($report | Where-Object { $_.Status -eq 'behind' }).Count
Write-Host "Summary: $($report.Count) branch(es) checked, $behindCount behind master."
exit 0
