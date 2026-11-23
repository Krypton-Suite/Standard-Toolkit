<#
.SYNOPSIS
    Hosts TestForm.Feature4177AsyncFormsDemo from a Debug bin folder (STA).

.DESCRIPTION
    Loads Krypton + TestForm assemblies and runs the #4177 async forms demo for
    interactive validation of ShowAsync / ShowDialogAsync wrappers.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Start-AsyncFormsDemoHost.ps1 -TargetFramework net9.0-windows
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net9.0-windows',
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
$formType = $asm.GetType('TestForm.Feature4177AsyncFormsDemo')
if (-not $formType) {
    throw 'Type TestForm.Feature4177AsyncFormsDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
Write-Host "Hosting Feature4177AsyncFormsDemo from $bin"
[System.Windows.Forms.Application]::Run($form)
