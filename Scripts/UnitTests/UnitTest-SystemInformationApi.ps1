<#
.SYNOPSIS
    Asserts #3176 KryptonSystemInformation public API is present on Krypton.Toolkit.Utilities.

.DESCRIPTION
    Loads Debug binaries and checks that KryptonSystemInformation, Strings, Show, and
    SystemInformationCategoryId.ComponentsNetworkConfiguration exist. Does not open UI.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-SystemInformationApi.ps1
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

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin

Add-Type -AssemblyName System.Windows.Forms

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

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

$api = [Krypton.Toolkit.Utilities.KryptonSystemInformation]
Assert-True ($null -ne $api) 'KryptonSystemInformation type loads'
$showMethods = @($api.GetMethods() | Where-Object { $_.Name -eq 'Show' })
Assert-True ($showMethods.Count -ge 1) 'Show() exists'
Assert-True ($null -ne $api.GetProperty('Strings')) 'Strings property exists'
Assert-True ([Krypton.Toolkit.Utilities.SystemInformationCategoryId]::ComponentsNetworkConfiguration -eq 'components-network-configuration') 'Network configuration category id'

if ($failed.Count -gt 0) {
    Write-Error ("System Information API checks failed:`n" + ($failed -join "`n"))
    exit 1
}

exit 0
