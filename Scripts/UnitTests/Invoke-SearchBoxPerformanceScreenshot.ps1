<#
.SYNOPSIS
    Opens KryptonSearchBoxPerformanceDemo (#4580) and captures a PNG for the local PR description.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows the demo with a typed filter so the
    suggestion popup is open, and writes Documents/PR/4580-searchbox-performance-demo.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SearchBoxPerformanceScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputPath
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputPath) {
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4580-searchbox-performance-demo.png'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.KryptonSearchBoxPerformanceDemo')
if (-not $formType) {
    throw 'Type TestForm.KryptonSearchBoxPerformanceDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 400

$search = $form.Controls.Find('ksbPerf', $true) | Select-Object -First 1
if (-not $search) {
    throw 'ksbPerf control was not found on the demo form.'
}
$search.Focus()
$search.Text = 'item 12'
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 250
[System.Windows.Forms.Application]::DoEvents()

# PrintWindow of the form HWND only (no Inflate — avoids IDE CopyFromScreen occlusion).
Save-UnitTestWindowPng -Form $form -Path $OutputPath -SettleMs 200

$form.Close()
$form.Dispose()
Write-Output "Saved $OutputPath"
