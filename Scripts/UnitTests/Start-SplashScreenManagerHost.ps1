<#
.SYNOPSIS
    Hosts TestForm.Feature4180SplashScreenManagerDemo from a Debug bin folder (STA).

.DESCRIPTION
    Loads Krypton + TestForm assemblies and runs the #4180 splash screen manager demo for
    interactive validation of fade, live status/progress, logging, exceptions, and opacity.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Start-SplashScreenManagerHost.ps1
#>
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
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Feature4180SplashScreenManagerDemo')
if (-not $formType) {
    throw 'Type TestForm.Feature4180SplashScreenManagerDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
Write-Host "Hosting Feature4180SplashScreenManagerDemo from $bin"
[System.Windows.Forms.Application]::Run($form)
