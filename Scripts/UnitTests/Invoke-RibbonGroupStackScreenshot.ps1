<#
.SYNOPSIS
    Hosts RibbonGroupStackDemo and captures the wide and narrow layouts.

.DESCRIPTION
    Loads TestForm in-process (STA) and writes Documents/PR/4560-ribbon-group-stack-*.png
    via Save-UnitTestWindowPng (PrintWindow).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-RibbonGroupStackScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDir) {
    $OutputDir = Join-Path $repoRoot 'Documents\PR'
}
if (-not (Test-Path -LiteralPath $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Themes.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.RibbonGroupStackDemo')
if (-not $formType) {
    throw 'Type TestForm.RibbonGroupStackDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true

function Save-StackShot {
    param($Target, [string]$FileName)
    $Target.Show()
    $Target.Activate()
    $Target.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($Target.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400
    $path = Join-Path $OutputDir $FileName
    Save-UnitTestWindowPng -Form $Target -Path $path
    Write-Host "Wrote $path"
}

$form.Width = 1100
Save-StackShot -Target $form -FileName '4560-ribbon-group-stack-wide.png'
$form.Width = 520
Save-StackShot -Target $form -FileName '4560-ribbon-group-stack-narrow.png'
$form.Close()
