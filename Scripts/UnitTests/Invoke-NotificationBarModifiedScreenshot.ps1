<#
.SYNOPSIS
    Captures the #4557 notification-bar property grid in the fresh and edited states.

.DESCRIPTION
    Hosts Bug4557NotificationBarModifiedDemo and writes two PNGs: the fresh NotificationBar
    row (blank) and the same row after Title is set (Modified).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-NotificationBarModifiedScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDirectory
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDirectory) {
    $OutputDirectory = Join-Path $repoRoot 'Documents\PR'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Bug4557NotificationBarModifiedDemo')
if (-not $formType) {
    throw 'Type TestForm.Bug4557NotificationBarModifiedDemo was not found in TestForm.exe.'
}

$flags = [System.Reflection.BindingFlags]::Instance -bor [System.Reflection.BindingFlags]::NonPublic
$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 600
[System.Windows.Forms.Application]::DoEvents()

$defaultPath = Join-Path $OutputDirectory '4557-notification-bar-default.png'
Save-UnitTestWindowPng -Form $form -Path $defaultPath -SettleMs 200

$ribbon = $formType.GetField('_ribbon', $flags).GetValue($form)
$ribbon.NotificationBar.Title = 'UPDATES AVAILABLE'
[void]$formType.GetMethod('RefreshView', $flags).Invoke($form, $null)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 400
[System.Windows.Forms.Application]::DoEvents()

$modifiedPath = Join-Path $OutputDirectory '4557-notification-bar-modified.png'
Save-UnitTestWindowPng -Form $form -Path $modifiedPath -SettleMs 200

$form.Close()
$form.Dispose()
Write-Host "Wrote $defaultPath"
Write-Host "Wrote $modifiedPath"
