<#
.SYNOPSIS
    Asserts #4177 async dialog API surface is present on all supported TFMs.

.DESCRIPTION
    Reflection smoke check:

    - On all TFMs (including net472): assert ShowAsync / ShowDialogAsync methods are present.
      Pre-.NET 9 builds degrade to sync ShowDialog inside the library helpers.
    - On net9.0-windows or newer: requires PowerShell 7+ (`pwsh`) to load the managed
      assemblies.

    Exit code 0 on success; non-zero on failure.
    Requires STA when run via Invoke-AllUnitTests.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-AsyncFormApis.ps1 -TargetFramework net472

.EXAMPLE
    pwsh -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-AsyncFormApis.ps1 -TargetFramework net9.0-windows
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
Add-Type -AssemblyName System.Drawing

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
$toolkit = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$utilitiesPath = Join-Path $bin 'Krypton.Toolkit.Utilities.dll'
$utilities = $null
if (Test-Path -LiteralPath $utilitiesPath) {
    $utilities = [System.Reflection.Assembly]::LoadFrom($utilitiesPath)
}

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

$needsPwsh = $TargetFramework -match '^net(9|1[0-9])'
Write-Host "TargetFramework=$TargetFramework PSEdition=$($PSVersionTable.PSEdition)"

if ($needsPwsh -and $PSVersionTable.PSEdition -eq 'Desktop') {
    Write-Host "SKIP: net9+ async API reflection requires PowerShell 7+ (pwsh) to load System.Runtime 9+." -ForegroundColor Yellow
    Write-Host "Re-run: pwsh -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-AsyncFormApis.ps1 -TargetFramework $TargetFramework"
    exit 0
}

function Resolve-Type {
    param(
        [System.Reflection.Assembly]$Assembly,
        [string]$TypeName
    )
    $type = $Assembly.GetType($TypeName, $false)
    if ($null -ne $type) {
        return $type
    }
    try {
        return ($Assembly.GetExportedTypes() | Where-Object { $_.FullName -eq $TypeName } | Select-Object -First 1)
    }
    catch {
        return $null
    }
}

function Test-HasMethod {
    param(
        [System.Reflection.Assembly]$Assembly,
        [string]$TypeName,
        [string]$MethodName
    )
    $type = Resolve-Type -Assembly $Assembly -TypeName $TypeName
    if ($null -eq $type) {
        return $false
    }
    $flags = [System.Reflection.BindingFlags]::Public -bor [System.Reflection.BindingFlags]::Static -bor [System.Reflection.BindingFlags]::Instance
    return [bool]($type.GetMethods($flags) | Where-Object { $_.Name -eq $MethodName } | Select-Object -First 1)
}

$checks = @(
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonMessageBox'; Method = 'ShowAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonTaskDialog'; Method = 'ShowDialogAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonThemeBrowser'; Method = 'ShowAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonStringCollectionEditor'; Method = 'ShowAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonInputBox'; Method = 'ShowAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonPoweredByButton'; Method = 'ShowBinaryInformationAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonGitHubIssueReportDialog'; Method = 'ShowAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.ShellDialogWrapper'; Method = 'ShowDialogAsync' }
)

if ($null -ne $utilities) {
    $checks += @(
        @{ Asm = $utilities; Type = 'Krypton.Toolkit.Utilities.KryptonExceptionDialog'; Method = 'ShowAsync' },
        @{ Asm = $utilities; Type = 'Krypton.Toolkit.Utilities.KryptonComputeFileCheckSum'; Method = 'ShowAsync' },
        @{ Asm = $utilities; Type = 'Krypton.Toolkit.Utilities.KryptonVerifyFileCheckSum'; Method = 'ShowAsync' },
        @{ Asm = $utilities; Type = 'Krypton.Toolkit.Utilities.KryptonMessageBoxExtended'; Method = 'ShowAsync' },
        @{ Asm = $utilities; Type = 'Krypton.Toolkit.Utilities.KryptonGitHubIssueReportDialog'; Method = 'ShowAsync' },
        @{ Asm = $utilities; Type = 'Krypton.Toolkit.Utilities.KryptonToast'; Method = 'ShowBasicNotificationWithBooleanReturnValueAsync' },
        @{ Asm = $utilities; Type = 'Krypton.Toolkit.Utilities.KryptonToast'; Method = 'ShowNotificationAsync' },
        @{ Asm = $utilities; Type = 'Krypton.Toolkit.Utilities.KryptonToast'; Method = 'ShowNotificationWithProgressBarAsync' }
    )
}

$checks += @(
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonColorDialog'; Method = 'ShowDialogAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonFontDialog'; Method = 'ShowDialogAsync' },
    @{ Asm = $toolkit; Type = 'Krypton.Toolkit.KryptonPrintDialog'; Method = 'ShowDialogAsync' }
)

foreach ($c in $checks) {
    $has = Test-HasMethod -Assembly $c.Asm -TypeName $c.Type -MethodName $c.Method
    Assert-True $has "$($c.Type).$($c.Method) present on $TargetFramework"
}

$mbType = Resolve-Type -Assembly $toolkit -TypeName 'Krypton.Toolkit.KryptonMessageBox'
$tdType = Resolve-Type -Assembly $toolkit -TypeName 'Krypton.Toolkit.KryptonTaskDialog'
Assert-True ($null -ne $mbType) 'Resolved KryptonMessageBox type'
Assert-True ($null -ne $tdType) 'Resolved KryptonTaskDialog type'

if ($failed.Count -gt 0) {
    Write-Host ("`n{0} assertion(s) failed." -f $failed.Count) -ForegroundColor Red
    exit 1
}

Write-Host "`nAll async form API assertions passed." -ForegroundColor Green
exit 0
