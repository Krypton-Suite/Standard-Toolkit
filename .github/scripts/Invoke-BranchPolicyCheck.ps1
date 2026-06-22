# New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
# Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
#
# Validates pull-request base/head pairing for Standard-Toolkit (no master ancestry requirement).
# Called from .github/workflows/pr-branch-policy.yml

param(
    [Parameter(Mandatory = $true)]
    [string]$BaseRef,

    [Parameter(Mandatory = $true)]
    [string]$HeadRef,

    [Parameter(Mandatory = $true)]
    [string]$ChangedFilesFile,

    [Parameter(Mandatory = $true)]
    [string]$PolicyPath,

    [Parameter(Mandatory = $true)]
    [string]$Enforce,

    [Parameter(Mandatory = $true)]
    [string]$Repository
)

$ErrorActionPreference = 'Stop'

$ChangedFiles = @()
if (Test-Path -LiteralPath $ChangedFilesFile) {
    $ChangedFiles = @(Get-Content -LiteralPath $ChangedFilesFile -Raw | ConvertFrom-Json)
}

function Write-PolicyMessage {
    param(
        [ValidateSet('Warning', 'Error')]
        [string]$Level,
        [string]$Message
    )

    if ($Level -eq 'Error') {
        Write-Host "::error::$Message"
    } else {
        Write-Host "::warning::$Message"
    }
}

function Test-BranchInList {
    param(
        [string]$Branch,
        [string[]]$List
    )
    return $List -contains $Branch
}

function Test-PathsUnderPrefix {
    param(
        [string[]]$Files,
        [string]$Prefix
    )

    if ($Files.Count -eq 0) {
        return $true
    }

    foreach ($file in $Files) {
        $normalized = $file -replace '\\', '/'
        if (-not $normalized.StartsWith($Prefix, [System.StringComparison]::OrdinalIgnoreCase)) {
            return $false
        }
    }

    return $true
}

function Invoke-PolicyExit {
    param(
        [int]$ViolationCount,
        [string]$EnforceMode
    )

    if ($ViolationCount -eq 0) {
        Write-Host 'Branch policy check passed.'
        exit 0
    }

    $suffix = " ($ViolationCount violation(s))"
    if ($EnforceMode -eq 'true') {
        Write-Host "Branch policy check failed$suffix."
        exit 1
    }

    Write-Host "::notice::Branch policy is in warn-only mode. Set repository variable BRANCH_POLICY_ENFORCE=true to fail PRs on violations."
    Write-Host "Branch policy reported warnings$suffix."
    exit 0
}

if ($Repository -ne 'Krypton-Suite/Standard-Toolkit') {
    Write-Host "Skipping branch policy for repository '$Repository'."
    exit 0
}

if (-not (Test-Path -LiteralPath $PolicyPath)) {
    throw "Policy file not found: $PolicyPath"
}

$policy = Get-Content -LiteralPath $PolicyPath -Raw | ConvertFrom-Json
$violations = 0
$enforceMode = $Enforce.ToLowerInvariant()

function Add-Violation {
    param(
        [string]$RuleId,
        [string]$Message
    )

    $script:violations++
    $label = if ($enforceMode -eq 'true') { 'Error' } else { 'Warning' }
    Write-PolicyMessage -Level $label -Message "[$RuleId] $Message"
}

# --- Rule: master -> downstream must be .github only ---
$downstream = @($policy.downstreamBranches)
if ($HeadRef -eq 'master' -and (Test-BranchInList -Branch $BaseRef -List $downstream)) {
    $prefix = '.github/'
    if (-not (Test-PathsUnderPrefix -Files $ChangedFiles -Prefix $prefix)) {
        $outside = $ChangedFiles | Where-Object {
            $n = $_ -replace '\\', '/'
            -not $n.StartsWith($prefix, [System.StringComparison]::OrdinalIgnoreCase)
        }
        $sample = ($outside | Select-Object -First 5) -join ', '
        Add-Violation -RuleId 'master-github-downstream' -Message (
            "Pull request master -> $BaseRef must only change files under '.github/'. " +
            "Use workflow 'Sync .github from master' or limit the PR to CI files. Outside paths (sample): $sample"
        )
    }
}

# --- Rule: alpha -> alpha-backup only ---
if ($BaseRef -eq 'alpha-backup' -and $HeadRef -ne 'alpha') {
    Add-Violation -RuleId 'alpha-to-alpha-backup' -Message (
        "Pull requests into alpha-backup must use head branch 'alpha' (current head: '$HeadRef')."
    )
}

# --- Rule: long-lived branch must not target master for product merges ---
$longLived = @($policy.longLivedHeadBranches)
if ($BaseRef -eq 'master' -and (Test-BranchInList -Branch $HeadRef -List $longLived)) {
    Add-Violation -RuleId 'no-long-lived-to-master' -Message (
        "Pull request $HeadRef -> master is not allowed. Merge topic branches into master, or use a .github-only sync into downstream branches."
    )
}

Invoke-PolicyExit -ViolationCount $violations -EnforceMode $enforceMode
