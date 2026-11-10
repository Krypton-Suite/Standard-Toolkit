<#
.SYNOPSIS
    Hosts ProgressBarTest and writes light/dark PNGs for issue #4684.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows ProgressBarTest with default
    theme-driven ProgressBarValue fills (TrackBarFillTrack fallback), and writes
    Documents/PR/4684-progressbar-scheme-default-*.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ProgressBarSchemeScreenshot.ps1
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
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Shared.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.ProgressBarTest')
if (-not $formType) {
    throw 'Type TestForm.ProgressBarTest was not found in TestForm.exe.'
}

$shots = @(
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue; Name = '4684-progressbar-scheme-default-light.png' }
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365BlackDarkMode; Name = '4684-progressbar-scheme-default-dark.png' }
)

$mgr = New-Object Krypton.Toolkit.KryptonManager
foreach ($shot in $shots) {
    $mgr.GlobalPaletteMode = $shot.Mode
    [System.Windows.Forms.Application]::DoEvents()

    $form = [Activator]::CreateInstance($formType)
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 80, 80
    $form.Show()
    $form.TopMost = $true
    $form.Activate()
    $form.BringToFront()
    if ($form.Handle -ne [IntPtr]::Zero) {
        [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    }
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 500

    $path = Join-Path $OutputDir $shot.Name
    Save-UnitTestWindowPng -Form $form -Path $path
    Write-Host "Wrote $path (mode=$($shot.Mode))"
    $form.Close()
    $form.Dispose()
}

Write-Host 'Done.'
