<#
.SYNOPSIS
    Hosts TestForm.NavigatorFormIntegrationDemo from a Debug bin folder (STA).

.DESCRIPTION
    Loads Krypton + TestForm assemblies from Bin\<Configuration>\<TFM> and runs the
    Navigator Form Integration (#925) demo. Use as the UI host for the other
    Scripts/Verification helpers.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\Verification\Start-NavigatorFormIntegrationHost.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'VerificationCommon.ps1')

$repoRoot = Get-VerificationRepoRoot
$bin = Get-VerificationBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-VerificationAssemblyResolver -BinDir $bin

Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.NavigatorFormIntegrationDemo')
if (-not $formType) {
    throw 'Type TestForm.NavigatorFormIntegrationDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
Write-Host "Hosting NavigatorFormIntegrationDemo from $bin"
[System.Windows.Forms.Application]::Run($form)
