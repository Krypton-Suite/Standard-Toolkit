<#
.SYNOPSIS
    Smoke assert for Scripts/UnitTests shared helpers and CI marker discovery.

.DESCRIPTION
    Verifies UnitTestCommon.ps1 loads and that every UnitTest-*.ps1 under Scripts/UnitTests
    declares a UnitTest-CI include|exclude marker. Does not require TestForm binaries.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\UnitTests\UnitTest-UnitTestInfrastructure.ps1
#>
# UnitTest-CI: include
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$failed = New-Object System.Collections.Generic.List[string]

function Assert-True {
    param([bool]$Condition, [string]$Message)
    if (-not $Condition) {
        $failed.Add($Message)
        Write-Host "FAIL: $Message" -ForegroundColor Red
    }
    else {
        Write-Host "PASS: $Message" -ForegroundColor Green
    }
}

Write-UnitTestBanner -Status INFO -Message 'Validating UnitTestCommon helpers and CI markers'

$repoRoot = Get-UnitTestRepoRoot
Assert-True -Condition ([string]::IsNullOrWhiteSpace($repoRoot) -eq $false) -Message 'Get-UnitTestRepoRoot returns a path'
Assert-True -Condition (Test-Path -LiteralPath (Join-Path $repoRoot 'AGENTS.md')) -Message 'Repo root contains AGENTS.md'

$classification = Get-UnitTestScriptClassification -Root $PSScriptRoot
Assert-True -Condition ($classification.Undeclared.Count -eq 0) -Message 'No UnitTest-*.ps1 scripts missing UnitTest-CI markers'
Assert-True -Condition ($classification.Included.Count -gt 0) -Message 'At least one UnitTest-CI:include script is discovered'

$includeNames = @($classification.Included | ForEach-Object { $_.Name })
Assert-True -Condition ($includeNames -contains 'UnitTest-UnitTestInfrastructure.ps1') -Message 'This infrastructure script is classified as include'

Write-UnitTestBanner -Status INFO -Message ("Included=$($classification.Included.Count) Excluded=$($classification.Excluded.Count) Undeclared=$($classification.Undeclared.Count)")

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message "Infrastructure checks failed ($($failed.Count))"
    exit 1
}

Write-UnitTestBanner -Status PASS -Message 'Infrastructure checks passed'
exit 0
